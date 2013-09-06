using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockCore.Repositories
{
    public class OrderRepository
    {
        private readonly StockCore.Models.DataStockCoreEntities _entities = new Models.DataStockCoreEntities();
        public short Insert(long orderId,string accountNo, string stockSymbol, long price, short volume, char side)
        {
            short marketId = (short)Common.Enums.MARKET_ID.HOSE;
            try
            {
                //check price
                HoseStockInfoRepository hoseRep = new HoseStockInfoRepository();
                var hoseStock = hoseRep.GetByStockSymbol(stockSymbol);
                if (hoseStock != null)
                {
                    if (price < hoseStock.Floor)
                    {
                        return (short)Common.Enums.PUT_ORDER_STATUS.PRICE_LESS_FLOOR;
                    }
                    if (price > hoseStock.Ceiling)
                    {
                        return (short)Common.Enums.PUT_ORDER_STATUS.PRICE_GREATER_CEIL;
                    }
                }
                else
                {
                    HNXStockInfoRepository hnxRep = new HNXStockInfoRepository();
                    var hnxStock = hnxRep.GetByStockSymbol(stockSymbol);
                    if(hnxStock!=null)
                    {
                        marketId = (short)Common.Enums.MARKET_ID.HNX;
                        if (price < hnxStock.Floor)
                        {
                            return (short)Common.Enums.PUT_ORDER_STATUS.PRICE_LESS_FLOOR;
                        }
                        if (price > hnxStock.Ceiling)
                        {
                            return (short)Common.Enums.PUT_ORDER_STATUS.PRICE_GREATER_CEIL;
                        }
                    }
                    else
                    {
                        UpComStockInfoRepository upcomRep = new UpComStockInfoRepository();
                        var upcomStock = upcomRep.GetByStockSymbol(stockSymbol);
                        if (upcomStock != null)
                        {
                            marketId = (short)Common.Enums.MARKET_ID.UPCOM;
                            if (price < upcomStock.Floor)
                            {
                                return (short)Common.Enums.PUT_ORDER_STATUS.PRICE_LESS_FLOOR;
                            }
                            if (price > upcomStock.Ceiling)
                            {
                                return (short)Common.Enums.PUT_ORDER_STATUS.PRICE_GREATER_CEIL;
                            }
                        }
                    }
                }
                //check volume
                if (!CheckVolume(marketId, volume))
                {
                    return (short)Common.Enums.PUT_ORDER_STATUS.INVAILID_VOL;
                }
                //check cash balance if buy stock
                var subCustAccRep = new Repositories.SubCustAccountRepository();
                var subCustAccount = subCustAccRep.GetById(accountNo);
                if(subCustAccount==null)
                {
                    return (short)Common.Enums.PUT_ORDER_STATUS.INVAILID_ACCOUNT;
                }
                if (side.ToString() == Common.Enums.Side.BUY.ToString())
                {
                    
                    if (subCustAccount.BuyCredit < (price * volume))
                    {
                            return (short)Common.Enums.PUT_ORDER_STATUS.NOT_ENOUGH_BYCREDIT;
                    }                    
                }
                else
                {
                    //check stock balance
                    var stockRep = new Repositories.StockBalanceRespository();
                    var stockBalance = stockRep.GetByAccountNoAndSymbol(accountNo,stockSymbol);
                    if (stockBalance != null)
                    {
                        if (volume < stockBalance.Available)
                        {
                            return (short)Common.Enums.PUT_ORDER_STATUS.NOT_ENOUGH_STOCK;
                        }
                    }
                    else
                    {
                        return (short)Common.Enums.PUT_ORDER_STATUS.INVAILID_STOCK_BALANCE;
                    }
                }                
                StockCore.Models.Order order = new Models.Order();
                order.Id = orderId;
                order.AccountNo = accountNo;
                order.StockSymbol = stockSymbol;
                order.Price = price;
                order.Volume = volume;
                order.Status = (short)Common.Enums.ORDER_STATUS.WAITING_MATCH;
                _entities.Orders.Add(order);
                _entities.SaveChanges();
                return (short)StockCore.Common.Enums.PUT_ORDER_STATUS.SUCCESS;
            }
            catch
            {
                return (short)StockCore.Common.Enums.PUT_ORDER_STATUS.CORE_ERROR;
            }
        }
        private bool CheckVolume(short marketId,short volume)
        {
            if (marketId == (short)Common.Enums.MARKET_ID.HOSE)
            {
                if((volume % 10 != 0 || volume <= 0))
                    return false;
                return true;
            }
            else
            {
                if ((volume % 100 != 0 || volume <= 0))
                    return false;
                return true;
            }
        }
        public void Update(Models.Order order)
        {
            _entities.Orders.Attach(order);
            var entry = _entities.Entry(order);
            entry.State = System.Data.EntityState.Modified;
            _entities.SaveChanges();
        }
        public void DoMatchedOrder()
        {
            var listOrderBuy = _entities.Orders.Where(x => x.Status == (short)Common.Enums.ORDER_STATUS.WAITING_MATCH || x.Status == (short)Common.Enums.ORDER_STATUS.PARTIAL_MATCHED
                ).OrderByDescending(x => x.Price).ThenByDescending(x => x.Volume).ThenBy(x => x.TradeDate).ToList();
            var listOrderSell = _entities.Orders.Where(x => x.Status == (short)Common.Enums.ORDER_STATUS.WAITING_MATCH || x.Status == (short)Common.Enums.ORDER_STATUS.PARTIAL_MATCHED
               ).OrderBy(x => x.Price).ThenByDescending(x => x.Volume).ThenBy(x => x.TradeDate).ToList();
            foreach (var orderBuy in listOrderBuy)
            {
                foreach (var orderSell in listOrderSell)
                {                    
                    if (orderBuy.Price == orderSell.Price && orderSell.StockSymbol==orderBuy.StockSymbol)//matched
                    {
                        short matchedVol = 0;
                        matchedVol = (short)(orderBuy.Volume > orderSell.Volume ? orderSell.Volume : orderBuy.Volume);

                        //Add matched data
                        Models.Matched matched = new Models.Matched();
                        matched.DateMatched = DateTime.Now;
                        matched.MatchedPrice = orderSell.Price;
                        matched.MatchedVol = matchedVol;
                        matched.OrderBuyID = orderBuy.Id;
                        matched.OrderSellID = orderSell.Id;
                        MatchedRepository matchedRep = new MatchedRepository();
                        matchedRep.Insert(matched);
                        //update sell order status
                        if (orderBuy.Volume > matchedVol)
                        {
                            orderBuy.Status = (short)Common.Enums.ORDER_STATUS.PARTIAL_MATCHED;
                            orderSell.Status = (short)Common.Enums.ORDER_STATUS.ALL_MATCHED;
                        }
                        else if (orderBuy.Volume == matchedVol)
                        {
                            orderBuy.Status = (short)Common.Enums.ORDER_STATUS.ALL_MATCHED;
                            orderSell.Status = (short)Common.Enums.ORDER_STATUS.ALL_MATCHED;
                        }
                        else
                        {
                            orderBuy.Status = (short)Common.Enums.ORDER_STATUS.ALL_MATCHED;
                            orderSell.Status = (short)Common.Enums.ORDER_STATUS.PARTIAL_MATCHED;
                        }
                        Update(orderBuy);
                        Update(orderSell);                        
                        //deduction cash
                        Repositories.CashDeductRepository cashDedRep = new CashDeductRepository();
                        
                        Models.CashTempDeduction orderSellDeduct = new Models.CashTempDeduction();
                        orderSellDeduct.AccountNo = orderSellDeduct.AccountNo;
                        orderSellDeduct.Amount = orderSell.Price * matchedVol;
                        orderSellDeduct.IsAdd = true;
                        orderSellDeduct.Status = (short)Common.Enums.DEDUCTION_STATUS.NEW;
                        orderSellDeduct.DeductedDate = DateTime.Now;
                        cashDedRep.Insert(orderSellDeduct);

                        Models.CashTempDeduction orderBuyDeduct = new Models.CashTempDeduction();
                        orderBuyDeduct.AccountNo = orderBuy.AccountNo;
                        orderBuyDeduct.Amount = orderBuy.Price * matchedVol;
                        orderBuyDeduct.IsAdd = false;
                        orderBuyDeduct.Status = (short)Common.Enums.DEDUCTION_STATUS.NEW;
                        orderBuyDeduct.DeductedDate = DateTime.Now;
                        cashDedRep.Insert(orderBuyDeduct);
                        
                        //deduction stock
                        Repositories.StockDeductRepository stockDedRep = new StockDeductRepository();
                        
                        Models.StockTempDeduction orderSellStockDeduct = new Models.StockTempDeduction();
                        orderSellStockDeduct.AccountNo = orderSell.AccountNo;
                        orderSellStockDeduct.StockSymbol = orderSell.StockSymbol;
                        orderSellStockDeduct.Volume = matchedVol;
                        orderSellStockDeduct.IsAdd = false;
                        orderSellStockDeduct.Status = (short)Common.Enums.DEDUCTION_STATUS.NEW;
                        orderSellStockDeduct.DeductedDate = DateTime.Now;
                        stockDedRep.Insert(orderSellStockDeduct);

                        Models.StockTempDeduction orderBuyStockDeduct = new Models.StockTempDeduction();
                        orderBuyStockDeduct.AccountNo = orderBuy.AccountNo;
                        orderBuyStockDeduct.StockSymbol = orderBuy.StockSymbol;
                        orderBuyStockDeduct.Volume = matchedVol;
                        orderBuyStockDeduct.IsAdd = true;
                        orderBuyStockDeduct.Status = (short)Common.Enums.DEDUCTION_STATUS.NEW;
                        orderBuyStockDeduct.DeductedDate = DateTime.Now;
                        stockDedRep.Insert(orderBuyStockDeduct);

                        //update cash balace for buy account
                        var subAccRep = new SubCustAccountRepository();
                        var sellAccount = subAccRep.GetById(orderBuy.AccountNo);
                        sellAccount.BuyCredit -= matchedVol * orderSell.Price;
                        sellAccount.WithDraw -= matchedVol * orderSell.Price;
                        subAccRep.Update(sellAccount);
                        //update stock balance for sell account
                        var stockBalaceRep = new StockBalanceRespository();
                        var stockBalace = stockBalaceRep.GetByAccountNoAndSymbol(orderSell.AccountNo,orderSell.StockSymbol);
                        stockBalace.Available = matchedVol;
                        stockBalace.Total = matchedVol;
                        stockBalaceRep.Update(stockBalace);
                        //update stock info
                        var hoseStockRep = new HoseStockInfoRepository();
                        var hoseStock = hoseStockRep.GetByStockSymbol(orderSell.StockSymbol);
                        if (hoseStock != null)
                        {
                            //best bid
                            if (orderSell.Price < hoseStock.Best3Bid)
                            {
                                hoseStock.Best3Bid = orderSell.Price;
                                hoseStock.Best3BidVolume = matchedVol;
                            }
                            else if (orderSell.Price < hoseStock.Best2Bid)
                            {
                                hoseStock.Best2Bid = orderSell.Price;
                                hoseStock.Best2BidVolume = matchedVol;
                            }
                            else if (orderSell.Price < hoseStock.Best1Bid)
                            {
                                hoseStock.Best1Bid = orderSell.Price;
                                hoseStock.Best1BidVolume = matchedVol;
                            }
                            //best offer
                            if (orderSell.Price > hoseStock.Best1Offer)
                            {
                                hoseStock.Best1Offer = orderSell.Price;
                                hoseStock.Best1OfferVolume = matchedVol;
                            }
                            else if (orderSell.Price > hoseStock.Best2Bid)
                            {
                                hoseStock.Best2Offer= orderSell.Price;
                                hoseStock.Best2OfferVolume = matchedVol;
                            }
                            else if (orderSell.Price > hoseStock.Best3Offer)
                            {
                                hoseStock.Best3Offer = orderSell.Price;
                                hoseStock.Best3OfferVolume = matchedVol;
                            }
                            hoseStockRep.Update(hoseStock);
                        }
                        else
                        {
                            var hnxStockRep = new HNXStockInfoRepository();
                            var hnxStock = hnxStockRep.GetByStockSymbol(orderSell.StockSymbol);
                            if (hnxStock != null)
                            {
                                //best bid
                                if (orderSell.Price < hnxStock.Best3Bid)
                                {
                                    hnxStock.Best3Bid = orderSell.Price;
                                    hnxStock.Best3BidVolume = matchedVol;
                                }
                                else if (orderSell.Price < hnxStock.Best2Bid)
                                {
                                    hnxStock.Best2Bid = orderSell.Price;
                                    hnxStock.Best2BidVolume = matchedVol;
                                }
                                else if (orderSell.Price < hnxStock.Best1Bid)
                                {
                                    hnxStock.Best1Bid = orderSell.Price;
                                    hnxStock.Best1BidVolume = matchedVol;
                                }
                                //best offer
                                if (orderSell.Price > hnxStock.Best1Offer)
                                {
                                    hnxStock.Best1Offer = orderSell.Price;
                                    hnxStock.Best1OfferVolume = matchedVol;
                                }
                                else if (orderSell.Price > hnxStock.Best2Bid)
                                {
                                    hnxStock.Best2Offer = orderSell.Price;
                                    hnxStock.Best2OfferVolume = matchedVol;
                                }
                                else if (orderSell.Price > hnxStock.Best3Offer)
                                {
                                    hnxStock.Best3Offer = orderSell.Price;
                                    hnxStock.Best3OfferVolume = matchedVol;
                                }
                                hnxStockRep.Update(hnxStock);
                            }
                            else
                            {
                                var upcomStockRep = new UpComStockInfoRepository();
                                var upcomStock = upcomStockRep.GetByStockSymbol(orderSell.StockSymbol);
                                //best bid
                                if (orderSell.Price < upcomStock.Best3Bid)
                                {
                                    upcomStock.Best3Bid = orderSell.Price;
                                    upcomStock.Best3BidVolume = matchedVol;
                                }
                                else if (orderSell.Price < upcomStock.Best2Bid)
                                {
                                    upcomStock.Best2Bid = orderSell.Price;
                                    upcomStock.Best2BidVolume = matchedVol;
                                }
                                else if (orderSell.Price < upcomStock.Best1Bid)
                                {
                                    upcomStock.Best1Bid = orderSell.Price;
                                    upcomStock.Best1BidVolume = matchedVol;
                                }
                                //best offer
                                if (orderSell.Price > upcomStock.Best1Offer)
                                {
                                    upcomStock.Best1Offer = orderSell.Price;
                                    upcomStock.Best1OfferVolume = matchedVol;
                                }
                                else if (orderSell.Price > upcomStock.Best2Bid)
                                {
                                    upcomStock.Best2Offer = orderSell.Price;
                                    upcomStock.Best2OfferVolume = matchedVol;
                                }
                                else if (orderSell.Price > upcomStock.Best3Offer)
                                {
                                    upcomStock.Best3Offer = orderSell.Price;
                                    upcomStock.Best3OfferVolume = matchedVol;
                                }
                                upcomStockRep.Update(upcomStock);
                            }
                        }    
                    
                        if (orderSell.Volume <= matchedVol)
                        {
                            listOrderSell.Remove(orderSell);
                        }
                        if (orderBuy.Volume <= matchedVol)
                            break;
                    }
                }//end foreach 2
            }//end foreach 1            
        }
    }
}
