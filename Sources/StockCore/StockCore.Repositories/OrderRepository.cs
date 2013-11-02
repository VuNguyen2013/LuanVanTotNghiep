using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockCore.Repositories
{
    public class OrderRepository
    {
        private readonly StockCore.Models.DataStockCoreEntities _entities = new Models.DataStockCoreEntities();
        public short Insert(long clientOrderId,string accountNo, string stockSymbol, long price, short volume, char side)
        {
            short marketId = (short)StockCore.Common.Enums.MARKET_ID.HOSE;
            try
            {
                //check price
                HoseStockInfoRepository hoseRep = new HoseStockInfoRepository();
                var hoseStock = hoseRep.GetByStockSymbol(stockSymbol);
                if (hoseStock != null)
                {
                    if (price < hoseStock.Floor)
                    {
                        return (short)StockCore.Common.Enums.PUT_ORDER_STATUS.PRICE_LESS_FLOOR;
                    }
                    if (price > hoseStock.Ceiling)
                    {
                        return (short)StockCore.Common.Enums.PUT_ORDER_STATUS.PRICE_GREATER_CEIL;
                    }
                }
                else
                {
                    HNXStockInfoRepository hnxRep = new HNXStockInfoRepository();
                    var hnxStock = hnxRep.GetByStockSymbol(stockSymbol);
                    if(hnxStock!=null)
                    {
                        marketId = (short)StockCore.Common.Enums.MARKET_ID.HNX;
                        if (price < hnxStock.Floor)
                        {
                            return (short)StockCore.Common.Enums.PUT_ORDER_STATUS.PRICE_LESS_FLOOR;
                        }
                        if (price > hnxStock.Ceiling)
                        {
                            return (short)StockCore.Common.Enums.PUT_ORDER_STATUS.PRICE_GREATER_CEIL;
                        }
                    }
                    else
                    {
                        UpComStockInfoRepository upcomRep = new UpComStockInfoRepository();
                        var upcomStock = upcomRep.GetByStockSymbol(stockSymbol);
                        if (upcomStock != null)
                        {
                            marketId = (short)StockCore.Common.Enums.MARKET_ID.UPCOM;
                            if (price < upcomStock.Floor)
                            {
                                return (short)StockCore.Common.Enums.PUT_ORDER_STATUS.PRICE_LESS_FLOOR;
                            }
                            if (price > upcomStock.Ceiling)
                            {
                                return (short)StockCore.Common.Enums.PUT_ORDER_STATUS.PRICE_GREATER_CEIL;
                            }
                        }
                    }
                }
                //check volume
                if (!CheckVolume(marketId, volume))
                {
                    return (short)StockCore.Common.Enums.PUT_ORDER_STATUS.INVAILID_VOL;
                }
                //get subcust account
                var subCustAccRep = new Repositories.SubCustAccountRepository();
                var subCustAccount = subCustAccRep.GetById(accountNo);
                if(subCustAccount==null)
                {
                    return (short)StockCore.Common.Enums.PUT_ORDER_STATUS.INVAILID_ACCOUNT;
                }                
                Repositories.CashDeductRepository cashDedRep = new CashDeductRepository();
                Repositories.StockDeductRepository stockDedRep = new StockDeductRepository();
                Models.CashTempDeduction orderDeduct = new Models.CashTempDeduction();
                Models.StockTempDeduction orderStockDeduct = new Models.StockTempDeduction();
                if (side== (char)Common.Enums.Side.BUY)
                {
                    //check cash balance
                    if (subCustAccount.BuyCredit < (price * volume))
                    {
                        return (short)Common.Enums.PUT_ORDER_STATUS.NOT_ENOUGH_BYCREDIT;
                    }    
                    //except cash
                    subCustAccount.BuyCredit -= price * volume;
                    subCustAccount.WithDraw -= price * volume;
                    subCustAccRep.Update(subCustAccount);

                    //deduction cash           

                   
                    orderDeduct.AccountNo = accountNo;
                    orderDeduct.Amount = price * volume;
                    orderDeduct.IsAdd = false;
                    orderDeduct.Status = (short)Common.Enums.DEDUCTION_STATUS.NEW;
                    orderDeduct.DeductedDate = DateTime.Now;
                    cashDedRep.Insert(orderDeduct);

                    //deduction stock
                    orderStockDeduct.AccountNo = accountNo;
                    orderStockDeduct.StockSymbol = stockSymbol;
                    orderStockDeduct.Volume = volume;
                    orderStockDeduct.IsAdd = true;
                    orderStockDeduct.Status = (short)Common.Enums.DEDUCTION_STATUS.NEW;
                    orderStockDeduct.DeductedDate = DateTime.Now;
                    stockDedRep.Insert(orderStockDeduct);

                }
                else
                {
                    //get stock balance
                    var stockRep = new Repositories.StockBalanceRespository();
                    var stockBalance = stockRep.GetByAccountNoAndSymbol(accountNo, stockSymbol);
                    if (stockBalance == null)
                    {
                        return (short)Common.Enums.PUT_ORDER_STATUS.NOT_ENOUGH_STOCK;
                    }
                    //check stock balance
                    if (stockBalance.Available < volume)
                    {
                        return (short)Common.Enums.PUT_ORDER_STATUS.NOT_ENOUGH_STOCK;
                    }
                    //except stock balace
                    stockBalance.Available -= volume;
                    stockRep.Update(stockBalance);
                    //deduction cash
                    
                    orderDeduct.AccountNo = accountNo;
                    orderDeduct.Amount = price * volume;
                    orderDeduct.IsAdd = true;
                    orderDeduct.Status = (short)Common.Enums.DEDUCTION_STATUS.NEW;
                    orderDeduct.DeductedDate = DateTime.Now;
                    cashDedRep.Insert(orderDeduct);

                    //deduction stock                   
                   
                    orderStockDeduct.AccountNo = accountNo;
                    orderStockDeduct.StockSymbol = stockSymbol;
                    orderStockDeduct.Volume = volume;
                    orderStockDeduct.IsAdd = false;
                    orderStockDeduct.Status = (short)Common.Enums.DEDUCTION_STATUS.NEW;
                    orderStockDeduct.DeductedDate = DateTime.Now;
                    stockDedRep.Insert(orderStockDeduct);
                }

                StockCore.Models.Order order = new Models.Order();
                order.ClientID = clientOrderId;
                order.AccountNo = accountNo;
                order.StockSymbol = stockSymbol;
                order.Price = price;
                order.Volume = volume;
                order.MatchedVol = 0;
                order.TradeDate = DateTime.Now;
                order.Side = side.ToString();
                order.Status = (short)Common.Enums.ORDER_STATUS.WAITING_MATCH;
                _entities.Orders.Add(order);
                _entities.SaveChanges();
                orderDeduct.OrderId = order.Id;
                cashDedRep.Update(orderDeduct);
                orderStockDeduct.OrderId = order.Id;
                stockDedRep.Update(orderStockDeduct);
                
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
            while (true)
            {
                var buyString = ((char)Common.Enums.Side.BUY).ToString();
                var sellString = ((char)Common.Enums.Side.SELL).ToString();
                var listOrderBuy = _entities.Orders.Where(x => (x.Status == (short)Common.Enums.ORDER_STATUS.WAITING_MATCH || x.Status == (short)Common.Enums.ORDER_STATUS.PARTIAL_MATCHED
                   ) && x.Side == buyString).OrderByDescending(x => x.Price).ThenByDescending(x => x.Volume).ThenBy(x => x.TradeDate).ToList();
                var listOrderSell = _entities.Orders.Where(x => (x.Status == (short)Common.Enums.ORDER_STATUS.WAITING_MATCH || x.Status == (short)Common.Enums.ORDER_STATUS.PARTIAL_MATCHED
                   ) && x.Side == sellString).OrderBy(x => x.Price).ThenByDescending(x => x.Volume).ThenBy(x => x.TradeDate).ToList();
                int i = 0;                
                while (i<listOrderBuy.Count )
                {
                    var orderBuy = listOrderBuy[i];
                    int j = 0;
                    while (j<listOrderSell.Count)
                    {
                        var orderSell = listOrderSell[j];
                        if (orderBuy.Price == orderSell.Price && orderSell.StockSymbol == orderBuy.StockSymbol)//matched
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

                            //update cash deduction
                            var cashDeductRep = new Repositories.CashDeductRepository();
                            var cashDeductBuy = cashDeductRep.GetByOrderId(orderBuy.Id);
                            cashDeductBuy.Status = (byte)Common.Enums.DEDUCTION_STATUS.PROCESSED;
                            cashDeductRep.Update(cashDeductBuy);

                            var cashDeductSell = cashDeductRep.GetByOrderId(orderSell.Id);
                            cashDeductSell.Status = (byte)Common.Enums.DEDUCTION_STATUS.PROCESSED;
                            cashDeductRep.Update(cashDeductSell);
                            //update stock deduction
                            var stockDeductRep = new Repositories.StockDeductRepository();
                            var stockDeductBuy = stockDeductRep.GetByOrderId(orderBuy.Id);
                            stockDeductBuy.Status = (byte)Common.Enums.DEDUCTION_STATUS.PROCESSED;
                            stockDeductRep.Update(stockDeductBuy);

                            var stockDeductSell = stockDeductRep.GetByOrderId(orderSell.Id);
                            stockDeductSell.Status = (byte)Common.Enums.DEDUCTION_STATUS.PROCESSED;
                            stockDeductRep.Update(stockDeductSell);

                            //update balance
                            SubCustAccountRepository subRep = new SubCustAccountRepository();
                            Repositories.StockBalanceRespository stockRep = new StockBalanceRespository();

                            var accountBuy = subRep.GetById(orderBuy.AccountNo);
                            var accountSell = subRep.GetById(orderSell.AccountNo);


                            accountBuy.WithDraw -= matchedVol * orderBuy.Price;
                            accountBuy.BuyCredit -= matchedVol * orderBuy.Price;
                            subRep.Update(accountBuy);
                            Models.StockBalance stockByBalance = stockRep.GetByAccountNoAndSymbol(accountBuy.SubCustAccountID, orderBuy.StockSymbol);
                            if (stockByBalance != null)
                            {
                                stockByBalance.WTR_T2 += matchedVol;
                                stockRep.Update(stockByBalance);
                            }
                            else
                            {
                                stockByBalance = new Models.StockBalance();
                                stockByBalance.SubCustAccountID = accountBuy.SubCustAccountID;
                                stockByBalance.StockSymbol = orderBuy.StockSymbol;
                                stockByBalance.WTR_T2 = matchedVol;
                                stockRep.Insert(stockByBalance);
                            }


                            var stockSellBalance = stockRep.GetByAccountNoAndSymbol(accountSell.SubCustAccountID, orderSell.StockSymbol);
                            stockSellBalance.Total -= matchedVol;
                            stockSellBalance.Available -= matchedVol;
                            stockRep.Update(stockSellBalance);
                            if (accountSell.WTR_T2 != null)
                                accountSell.WTR_T2 += orderSell.Price * matchedVol;
                            else
                                accountSell.WTR_T2 = orderSell.Price * matchedVol;
                            subRep.Update(accountSell);

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
                                    hoseStock.Best2Offer = orderSell.Price;
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
                            //send matched result to client                            
                            SocketServer socketServer = new SocketServer();

                            //send to buy account                            
                            string buyStr = accountBuy.MainCustAccount.MemberStockCompanyID.ToString() + "|" + orderBuy.ClientID + "|" + matchedVol + "|" + orderBuy.Status;
                            socketServer.SendOrderResultData(buyStr);

                            //send to sell acocunt                            
                            string sellStr = accountSell.MainCustAccount.MemberStockCompanyID.ToString() + "|" + orderSell.ClientID + "|" + matchedVol + "|" + orderSell.Status;
                            socketServer.SendOrderResultData(sellStr);
                            if (orderSell.Volume <= matchedVol)
                            {
                                listOrderSell.Remove(orderSell);
                                continue;
                            }
                            if (orderBuy.Volume <= matchedVol)
                            {
                                i++;
                                break;
                            }                            
                        }
                        j++;
                    }//end while 2
                    i++;
                }//end while 1   
            }
        }
    }
}
