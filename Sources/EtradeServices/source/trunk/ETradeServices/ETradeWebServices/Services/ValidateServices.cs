// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidateServices.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the ValidateServices type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using AccountManager.Entities;
using ETradeCommon;
using ETradeCommon.Enums;
using ETradeCore.Entities;
using ETradeCore.Services;
using ETradeFinance.Services;
using ETradeOrders.Entities;
using ETradeOrders.Services;
using ETradeWebServices.RTServices;
using RTDataServices.Entities;

namespace ETradeWebServices.Services
{
    /// <summary>
    /// The priority of validation:
    /// 1.  Validate market
    /// 2.  Validate stock
    /// 3.  Validate volume unit
    /// 4.  Validate step price
    /// 5.  Validate price
    /// 6.  Validate traderId
    /// 7.  Validate transaction
    /// 8.  Validate trade permission
    /// 9.  Validate balance
    /// 10. Validate account
    /// </summary>
    public class ValidateServices
    {
        private readonly ExecOrderService _execOrderService = new ExecOrderService();

        private readonly Service _rtService = new Service();

        private static readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();

        //private readonly CashServices _cashServices = new CashServices();

        //private readonly StockServices _stockServices = new StockServices();

        private static readonly ConditionOrderService ConditionOrderService = new ConditionOrderService();

        //private static readonly CashTransferService CashTransferService = new CashTransferService();

        private static readonly FeeService FeeService = new FeeService();

        //private static readonly StockTransferService StockTransferService = new StockTransferService();
        

        private static readonly MarginServices MarginServices=new MarginServices();

        private static readonly int BeginTimeForNextDayAdvance = int.Parse(ConfigurationManager.AppSettings["BeginTimeForNextDayAdvance"]);

        private static readonly int EndTimeForNextDayAdvance = int.Parse(ConfigurationManager.AppSettings["EndTimeForNextDayAdvance"]);

        /// <summary>
        /// Determines whether the market session is valid or not.
        /// </summary>
        /// <param name="marketId">The market id.</param>
        /// <param name="condPrice">The con price.</param>
        /// <returns>
        /// <para>Result checking if this is a valid market session.</para>
        /// <para>REJECT_REASON=ERROR_MARKET_CLOSE: Market closed.</para>
        /// <para>REJECT_REASON=ERROR_ATO_NOT_IN_READY_AND_SESSION1: Cannot put ATO order in READY and SESSION1 session.</para>
        /// <para>REJECT_REASON=ERROR_ATC_NOT_IN_SESSION3: Cannot put ATC in SESSION3 session.</para>
        /// <para>REJECT_REASON=IS_VALID: This is a valid order.</para>
        /// </returns>
        private CommonEnums.REJECT_REASON IsValidMarket(int marketId, char condPrice)
        {
            CommonEnums.REJECT_REASON rejCode = CommonEnums.REJECT_REASON.IS_VALID;

            var orderSession = MarketServices.GetOrderSession(marketId);
            switch (marketId)
            {
                case (int) CommonEnums.MARKET_ID.HOSE:
                    switch (condPrice)
                    {
                        case (char)CommonEnums.ORDER_CON_PRICE.LO:
                            if (orderSession != CommonEnums.ORDER_SESSION.SESSION1 && orderSession != CommonEnums.ORDER_SESSION.SESSION2 &&
                            orderSession != CommonEnums.ORDER_SESSION.SESSION3 && orderSession != CommonEnums.ORDER_SESSION.SESSION4 &&
                            orderSession != CommonEnums.ORDER_SESSION.SESSION7 && orderSession != CommonEnums.ORDER_SESSION.SESSION8 && 
                            orderSession != CommonEnums.ORDER_SESSION.SESSION9)
                            {
                                rejCode = CommonEnums.REJECT_REASON.ERROR_MARKET_CLOSE;
                            }
                            break;
                        case (char)CommonEnums.ORDER_CON_PRICE.ATO:
                            if (orderSession != CommonEnums.ORDER_SESSION.SESSION1 && orderSession != CommonEnums.ORDER_SESSION.SESSION2)
                            {
                                LogHandler.Log(
                                            "IsValidMarket: ATO not allowed, condPrice = " + condPrice +
                                            ", order session = " + orderSession + ", market: " + marketId,
                                            GetType() + ".IsValidMarket", TraceEventType.Warning);
                                rejCode = CommonEnums.REJECT_REASON.ERROR_ATO_NOT_ALLOWED;
                            }
                            break;
                        case (char)CommonEnums.ORDER_CON_PRICE.ATC:
                            if (orderSession == CommonEnums.ORDER_SESSION.SESSION4 || orderSession == CommonEnums.ORDER_SESSION.SESSION6 ||
                                orderSession == CommonEnums.ORDER_SESSION.SESSION7 || orderSession == CommonEnums.ORDER_SESSION.SESSION8)
                            {
                                if (!AppConfig.EnablePutATCbeforeHose)
                                {
                                    LogHandler.Log(
                                        "IsValidMarket: ATC not allowed, condPrice = " + condPrice + ", order session = " +
                                        orderSession + ", market: " + marketId, GetType() + ".IsValidMarket", TraceEventType.Warning);
                                    rejCode = CommonEnums.REJECT_REASON.ERROR_ATC_NOT_ALLOWED;
                                }
                            }
                            else if (orderSession != CommonEnums.ORDER_SESSION.SESSION9)
                            {
                                LogHandler.Log(
                                           "IsValidMarket: ATC not allowed, condPrice = " + condPrice +
                                           ", order session = " + orderSession + ", market: " + marketId,
                                           GetType() + ".IsValidMarket", TraceEventType.Warning);
                                rejCode = CommonEnums.REJECT_REASON.ERROR_ATC_NOT_ALLOWED;
                            }
                            break;
                        case (char)CommonEnums.ORDER_CON_PRICE.MP:
                            if (orderSession != CommonEnums.ORDER_SESSION.SESSION4 && orderSession != CommonEnums.ORDER_SESSION.SESSION7)
                            {
                                LogHandler.Log(
                                        "IsValidMarket: MP not allowed, condPrice = " + condPrice +
                                        ", order session = " + orderSession + ", market: " + marketId,
                                        GetType() + ".IsValidMarket", TraceEventType.Warning);
                                rejCode = CommonEnums.REJECT_REASON.ERROR_MP_NOT_ALLOWED;
                            }
                            break;
                        case (char)CommonEnums.ORDER_CON_PRICE.MOK:
                        case (char)CommonEnums.ORDER_CON_PRICE.MAK:
                            LogHandler.Log(
                                            "IsValidMarket: MOK/MAK not allowed, condPrice = " + condPrice +
                                            ", order session = " + orderSession + ", market: " + marketId,
                                            GetType() + ".IsValidMarket", TraceEventType.Warning);
                            rejCode = CommonEnums.REJECT_REASON.ERROR_HOSE_NOT_USE_MAK_MOK;
                            break;
                        default:
                            return CommonEnums.REJECT_REASON.ILLEGAL_CONPRICE;
                    }
                    break;
                case (int)CommonEnums.MARKET_ID.HNX:
                case (int)CommonEnums.MARKET_ID.UPCoM:
                    switch (condPrice)
                    {
                        case (char)CommonEnums.ORDER_CON_PRICE.LO:
                            if (orderSession != CommonEnums.ORDER_SESSION.SESSION1 && orderSession != CommonEnums.ORDER_SESSION.SESSION2 &&
                            orderSession != CommonEnums.ORDER_SESSION.SESSION3 && orderSession != CommonEnums.ORDER_SESSION.SESSION4 &&
                            orderSession != CommonEnums.ORDER_SESSION.SESSION7 && orderSession != CommonEnums.ORDER_SESSION.SESSION8 && 
                            orderSession != CommonEnums.ORDER_SESSION.SESSION9 && orderSession != CommonEnums.ORDER_SESSION.SESSION10)
                            {
                                rejCode = CommonEnums.REJECT_REASON.ERROR_MARKET_CLOSE;
                            }
                            break;
                        case (char)CommonEnums.ORDER_CON_PRICE.ATO:
                            if (orderSession != CommonEnums.ORDER_SESSION.SESSION1 && orderSession != CommonEnums.ORDER_SESSION.SESSION2)
                            {
                                LogHandler.Log(
                                            "IsValidMarket: ATO not allowed, condPrice = " + condPrice +
                                            ", order session = " + orderSession + ", market: " + marketId,
                                            GetType() + ".IsValidMarket", TraceEventType.Warning);
                                rejCode = CommonEnums.REJECT_REASON.ERROR_ATO_NOT_ALLOWED;
                            }
                            break;
                        case (char)CommonEnums.ORDER_CON_PRICE.ATC:
                            if (orderSession == CommonEnums.ORDER_SESSION.SESSION4 || orderSession == CommonEnums.ORDER_SESSION.SESSION6 ||
                                orderSession == CommonEnums.ORDER_SESSION.SESSION7)
                            {
                                if (!AppConfig.EnablePutATCbeforeHnx)
                                {
                                    LogHandler.Log(
                                        "IsValidMarket: ATC not allowed, condPrice = " + condPrice + ", order session = " +
                                        orderSession + ", market: " + marketId, GetType() + ".IsValidMarket", TraceEventType.Warning);
                                    rejCode = CommonEnums.REJECT_REASON.ERROR_ATC_NOT_ALLOWED;
                                }
                            }
                            else if (orderSession != CommonEnums.ORDER_SESSION.SESSION9 && orderSession != CommonEnums.ORDER_SESSION.SESSION10)
                            {
                                LogHandler.Log(
                                            "IsValidMarket: ATC not allowed, condPrice = " + condPrice +
                                            ", order session = " + orderSession + ", market: " + marketId,
                                            GetType() + ".IsValidMarket", TraceEventType.Warning);
                                rejCode = CommonEnums.REJECT_REASON.ERROR_ATC_NOT_ALLOWED;
                            }
                            break;
                        case (char)CommonEnums.ORDER_CON_PRICE.MP:
                        case (char)CommonEnums.ORDER_CON_PRICE.MOK:
                        case (char)CommonEnums.ORDER_CON_PRICE.MAK:
                            if (orderSession != CommonEnums.ORDER_SESSION.SESSION4 && orderSession != CommonEnums.ORDER_SESSION.SESSION7)
                            {
                                if (condPrice == (char)CommonEnums.ORDER_CON_PRICE.MP)
                                {
                                    LogHandler.Log(
                                            "IsValidMarket: MP not allowed, condPrice = " + condPrice +
                                            ", order session = " + orderSession + ", market: " + marketId,
                                            GetType() + ".IsValidMarket", TraceEventType.Warning);
                                    rejCode = CommonEnums.REJECT_REASON.ERROR_MP_NOT_ALLOWED;
                                }
                                else if (condPrice == (char)CommonEnums.ORDER_CON_PRICE.MOK)
                                {
                                    LogHandler.Log(
                                            "IsValidMarket: MOK not allowed, condPrice = " + condPrice +
                                            ", order session = " + orderSession + ", market: " + marketId,
                                            GetType() + ".IsValidMarket", TraceEventType.Warning);
                                    rejCode = CommonEnums.REJECT_REASON.ERROR_MOK_NOT_ALLOWED;
                                }
                                else if (condPrice == (char)CommonEnums.ORDER_CON_PRICE.MAK)
                                {
                                    LogHandler.Log(
                                            "IsValidMarket: MAK not allowed, condPrice = " + condPrice +
                                            ", order session = " + orderSession + ", market: " + marketId,
                                            GetType() + ".IsValidMarket", TraceEventType.Warning);
                                    rejCode = CommonEnums.REJECT_REASON.ERROR_MAK_NOT_ALLOWED;
                                }
                            }
                            break;
                        default:
                            return CommonEnums.REJECT_REASON.ILLEGAL_CONPRICE;
                    }
                    break;

                default:
                    return CommonEnums.REJECT_REASON.ILLEGAL_MARKET_ID;
            }
            return rejCode;
        }

        /// <summary>
        /// Determines whether [is valid stock] [the specified symbol].
        /// 1. Check is existed stock?
        /// 2. Check isHalted
        /// 3. Check isBond
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <param name="isAdvance">true if this is validation for condition order; otherwise, false.</param>
        /// <param name="side">The side.</param>
        /// <param name="conPrice">The con price.</param>
        /// <returns>
        /// <para>Result checking if this is a valid stock.</para>
        /// <para>REJECT_REASON=INCORRECT_STOCK: Stock is incorrect.</para>
        /// <para>REJECT_REASON=STOCK_IS_HALT: Stock is halt.</para>
        /// <para>REJECT_REASON=IS_VALID: This is a valid stock.</para>
        /// </returns>
        private CommonEnums.REJECT_REASON IsValidStock(string symbol, bool isAdvance,char side, char conPrice)
        {
            var resultObject = Serializer.Deserialize<ResultObject<StockInfo>>(_rtService.GetStockInfo(symbol));
            var stockInfo = resultObject.Result;
            if (stockInfo == null || string.IsNullOrEmpty(symbol))
            {
                LogHandler.Log(
                    String.Format("IsValidStock: {0} not existed in collection", symbol),
                    GetType() + ".IsValidStock",
                    TraceEventType.Warning);
                return CommonEnums.REJECT_REASON.INCORRECT_STOCK;
            }
            /*if (conPrice == Constants.ORDER_TYPE_MP)
            {
                if ((side == (char) CommonEnums.TRADE_SIDE.SELL && stockInfo.Best1Bid <= 0) ||
                    (side == (char) CommonEnums.TRADE_SIDE.BUY && stockInfo.Best1Offer <= 0))
                {
                    return CommonEnums.REJECT_REASON.NOT_LO_ORDER;
                }
            }*/
            if (!isAdvance) // Only check when this is not advance order
            {
                if (stockInfo.CanTrade == 0)
                {
                    LogHandler.Log(String.Format("IsValidStock: {0} is halted", symbol), GetType() + ".IsValidStock",
                        TraceEventType.Warning);
                    return CommonEnums.REJECT_REASON.STOCK_IS_HALT;
                }
            }

            return CommonEnums.REJECT_REASON.IS_VALID;
        }

        /// <summary>
        /// Determines whether [is valid vol unit] [the specified market id].
        /// </summary>
        /// <param name="marketId">The market id.</param>
        /// <param name="volume">The volume.</param>
        /// <returns>
        /// <para>Result checking if this is a valid volume unit.</para>
        /// <para>REJECT_REASON=INCORRECT_VOL: The volume is incorrect.</para>
        /// <para>REJECT_REASON=OVER_MAX_VOL: The volume is over max allowed volume.</para>
        /// <para>REJECT_REASON=IS_VALID: This is a valid order.</para>
        /// </returns>
        private CommonEnums.REJECT_REASON IsValidVolUnit(int marketId, int volume)
        {
            decimal volUnit;
            var center = (CommonEnums.MARKET_ID)marketId;

            // Validate the volume unit and step price first
            if (volume <= 0)
            {
                LogHandler.Log(
                    "IsValidVolUnit: Incorrect volume, volume = " + volume + ", marketId = " + marketId,
                    GetType() + ".IsValidVolUnit",
                    TraceEventType.Warning);
                return CommonEnums.REJECT_REASON.INCORRECT_VOL;
            }

            switch (center)
            {
                case CommonEnums.MARKET_ID.HOSE:
                    volUnit = (decimal)CommonEnums.TRADE_RULE.VOL_UNIT_HOSE;

                    if (volume % volUnit != 0)
                    {
                        LogHandler.Log(
                            "IsValidVolUnit: Incorrect volume, volume = " + volume + ", market = " + marketId,
                            GetType() + ".IsValidVolUnit",
                            TraceEventType.Warning);
                        return CommonEnums.REJECT_REASON.INCORRECT_VOL;
                    }

                    if (volume > (decimal)CommonEnums.TRADE_RULE.VOL_MAX_HOSE)
                    {
                        LogHandler.Log(
                            "IsValidVolUnit: volume is over max volume, volume = " + volume + ", market = " + marketId,
                            GetType() + ".IsValidVolUnit",
                            TraceEventType.Warning);
                        return CommonEnums.REJECT_REASON.OVER_MAX_VOL;
                    }

                    break;
                case CommonEnums.MARKET_ID.HNX:
                    volUnit = (decimal)CommonEnums.TRADE_RULE.VOL_UNIT_HNX;

                    if (volume % volUnit != 0)
                    {
                        LogHandler.Log(
                            "IsValidVolUnit: Incorrect volume, volume = " + volume + ", market = " + marketId,
                            GetType() + ".IsValidVolUnit",
                            TraceEventType.Warning);
                        return CommonEnums.REJECT_REASON.INCORRECT_VOL;
                    }

                    if (volume >= (decimal)CommonEnums.TRADE_RULE.VOL_MAX_HNX)
                    {
                        LogHandler.Log(
                            "IsValidVolUnit: volume is over max volume, volume = " + volume + ", market = " + marketId,
                            GetType() + ".IsValidVolUnit",
                            TraceEventType.Warning);
                        return CommonEnums.REJECT_REASON.OVER_MAX_VOL;
                    }

                    break;
                case CommonEnums.MARKET_ID.UPCoM:
                    volUnit = (decimal)CommonEnums.TRADE_RULE.VOL_UNIT_UPCOM;

                    if (volume % volUnit != 0)
                    {
                        LogHandler.Log(
                            "IsValidVolUnit: Incorrect volume, volume = " + volume + ", market = " + marketId,
                            GetType() + ".IsValidVolUnit",
                            TraceEventType.Warning);
                        return CommonEnums.REJECT_REASON.INCORRECT_VOL;
                    }

                    /*if (volume >= (decimal)CommonEnums.TRADE_RULE.VOL_MAX_UPCOM)
                    {
                        LogHandler.Log(
                            "IsValidVolUnit: volume is over max volume, volume = " + volume + ", market = " + marketId,
                            GetType() + ".IsValidVolUnit",
                            TraceEventType.Warning);
                        return CommonEnums.REJECT_REASON.OVER_MAX_VOL;
                    }*/

                    break;
            }

            return CommonEnums.REJECT_REASON.IS_VALID;
        }

        

        /// <summary>
        /// Determines whether [is valid price] [the specified market id].
        /// </summary>
        /// <param name="marketId">The market id.</param>
        /// <param name="price">The price.</param>
        /// <param name="conPrice">The con price.</param>
        /// <param name="symbol">The symbol.</param>
        /// <returns>
        /// <para>Result checking if this is a valid price.</para>
        /// <para>REJECT_REASON=PRICE_BELOW_FLOOR: Price is below floor price.</para>
        /// <para>REJECT_REASON=PRICE_ABOVE_CEILING: Price is over ceiling price.</para>
        /// <para>REJECT_REASON=INCORRECT_STOCK: Stock is incorrect.</para>
        /// <para>REJECT_REASON=IS_VALID: This is a valid price.</para>
        /// </returns>
        private CommonEnums.REJECT_REASON IsValidPrice(int marketId, decimal price, char conPrice, string symbol)
        {
            var stockInfo = Serializer.Deserialize<ResultObject<StockInfo>>(_rtService.GetStockInfo(symbol));

            if (stockInfo == null || stockInfo.Result == null)
            {
                LogHandler.Log(
                    "IsValidPrice: " + symbol + " not existed in collection",
                    GetType() + ".IsValidPrice",
                    TraceEventType.Warning);
                return CommonEnums.REJECT_REASON.INCORRECT_STOCK;
            }

            var center = (CommonEnums.MARKET_ID)marketId;

            if ((center == CommonEnums.MARKET_ID.HOSE) && (conPrice == Constants.ORDER_TYPE_ATO || conPrice == Constants.ORDER_TYPE_ATC || conPrice == Constants.ORDER_TYPE_MP))
            {
                return CommonEnums.REJECT_REASON.IS_VALID;
            }
            else if (center == CommonEnums.MARKET_ID.HNX || center == CommonEnums.MARKET_ID.UPCoM)
            {
                if (conPrice == Constants.ORDER_TYPE_ATO || conPrice == Constants.ORDER_TYPE_ATC ||
                    conPrice == Constants.ORDER_TYPE_MP || conPrice == Constants.ORDER_TYPE_MAK ||
                    conPrice == Constants.ORDER_TYPE_MOK)
                {
                    return CommonEnums.REJECT_REASON.IS_VALID;
                }
            }


            switch (center)
            {
                case CommonEnums.MARKET_ID.HOSE:
                    if (price < (decimal)stockInfo.Result.Floor)
                    {
                        LogHandler.Log(
                            "IsValidPrice: Price below floor, price = " + price + ", floor = " +
                            stockInfo.Result.Floor + ", stock symbol: " + symbol,
                            GetType() + ".IsValidPrice",
                            TraceEventType.Warning);
                        return CommonEnums.REJECT_REASON.PRICE_BELOW_FLOOR;
                    }

                    if (price > (decimal)stockInfo.Result.Ceiling)
                    {
                        LogHandler.Log(
                            "IsValidPrice: Price above ceiling, price = " + price + ", ceiling = " +
                            stockInfo.Result.Ceiling + ", stock symbol: " + symbol,
                            GetType() + ".IsValidPrice",
                            TraceEventType.Warning);
                        return CommonEnums.REJECT_REASON.PRICE_ABOVE_CEILING;
                    }

                    break;
                case CommonEnums.MARKET_ID.HNX:
                    if ((stockInfo.Result.Ceiling == 0) && (stockInfo.Result.Floor == 0))
                    {
                        // This validation for new stock, it without celing and floor.
                        // So we just check range from 1000VND to 1000,000 VND
                        if (price < 1)
                        {
                            LogHandler.Log(
                                "IsValidPrice: Price below floor, price = " + price + ", floor = " +
                                stockInfo.Result.Floor + ", stock symbol: " + symbol,
                                GetType() + ".IsValidPrice",
                                TraceEventType.Warning);
                            return CommonEnums.REJECT_REASON.PRICE_BELOW_FLOOR;
                        }

                        if (price > 1000)
                        {
                            LogHandler.Log(
                                "IsValidPrice: Price above ceiling, price = " + price + ", floor = " +
                                stockInfo.Result.Floor + ", stock symbol: " + symbol,
                                GetType() + ".IsValidPrice",
                                TraceEventType.Warning);
                            return CommonEnums.REJECT_REASON.PRICE_ABOVE_CEILING;
                        }
                    }
                    else
                    {
                        if (price < (decimal)stockInfo.Result.Floor)
                        {
                            LogHandler.Log(
                                "IsValidPrice: Price below floor, price = " + price + ", floor = " +
                                stockInfo.Result.Floor + ", stock symbol: " + symbol,
                                GetType() + ".IsValidPrice",
                                TraceEventType.Warning);
                            return CommonEnums.REJECT_REASON.PRICE_BELOW_FLOOR;
                        }

                        if (price > (decimal)stockInfo.Result.Ceiling)
                        {
                            LogHandler.Log(
                                "IsValidPrice: Price above ceiling, price = " + price + ", floor = " +
                                stockInfo.Result.Floor + ", stock symbol: " + symbol,
                                GetType() + ".IsValidPrice",
                                TraceEventType.Warning);
                            return CommonEnums.REJECT_REASON.PRICE_ABOVE_CEILING;
                        }
                    }

                    break;
                case CommonEnums.MARKET_ID.UPCoM:
                    if ((stockInfo.Result.Ceiling == 0) && (stockInfo.Result.Floor == 0))
                    {
                        // This validation for new stock, it without celing and floor.
                        // So we just check range from 1000VND to 1000,000 VND
                        if (price < 1)
                        {
                            LogHandler.Log(
                                "IsValidPrice: Price below floor, price = " + price + ", floor = " +
                                stockInfo.Result.Floor + ", stock symbol: " + symbol,
                                GetType() + ".IsValidPrice",
                                TraceEventType.Warning);
                            return CommonEnums.REJECT_REASON.PRICE_BELOW_FLOOR;
                        }

                        if (price > 1000)
                        {
                            LogHandler.Log(
                                "IsValidPrice: Price above ceiling, price = " + price + ", floor = " +
                                stockInfo.Result.Floor + ", stock symbol: " + symbol,
                                GetType() + ".IsValidPrice",
                                TraceEventType.Warning);
                            return CommonEnums.REJECT_REASON.PRICE_ABOVE_CEILING;
                        }
                    }
                    else
                    {
                        if (price < (decimal)stockInfo.Result.Floor)
                        {
                            LogHandler.Log(
                                "IsValidPrice: Price below floor, price = " + price + ", floor = " +
                                stockInfo.Result.Floor + ", stock symbol: " + symbol,
                                GetType() + ".IsValidPrice",
                                TraceEventType.Warning);
                            return CommonEnums.REJECT_REASON.PRICE_BELOW_FLOOR;
                        }

                        if (price > (decimal)stockInfo.Result.Ceiling)
                        {
                            LogHandler.Log(
                                "IsValidPrice: Price above ceiling, price = " + price + ", floor = " +
                                stockInfo.Result.Floor + ", stock symbol: " + symbol,
                                GetType() + ".IsValidPrice",
                                TraceEventType.Warning);
                            return CommonEnums.REJECT_REASON.PRICE_ABOVE_CEILING;
                        }
                    }

                    break;
            }

            return CommonEnums.REJECT_REASON.IS_VALID;
        }

        private CommonEnums.REJECT_REASON IsValidTransactionOddLot(string accountNo, string symbol, List<string> subCustAccounts, StockAvailable stockAvailable, int volume)
        {
            var subCustAccountCollection = new StringBuilder(100);
            foreach (var subCustAccountId in subCustAccounts)
            {
                subCustAccountCollection.Append("'" + subCustAccountId + "', ");
            }

            var statusStringOrder = new StringBuilder(100);
            statusStringOrder.Append("" + (int)CommonEnums.ORDER_STATUS.CANCELLED + ", ");
            statusStringOrder.Append("" + (int)CommonEnums.ORDER_STATUS.FULL_MATCHED + ", ");
            statusStringOrder.Append("" + (int)CommonEnums.ORDER_STATUS.ORDER_REJECTED + ", ");
            string stringStatus = statusStringOrder.ToString().Substring(
                0, statusStringOrder.ToString().Length - 2);

            string accountCondition = subCustAccountCollection.ToString().Substring(
                0, subCustAccountCollection.ToString().Length - 2); // Remove ", "

            string conditionStr = string.Empty;
            conditionStr = string.Format(
                " (Side = '{0}') AND (Condition = '{3}') AND  (SubCustAccountID in ({1})) AND (SecSymbol = '{2}') AND (OrderStatus not in ({4}))",
                ((char)CommonEnums.TRADE_SIDE.SELL),
                accountCondition,
                symbol, (char)CommonEnums.CONDITION.ODD,stringStatus);

            int numberOfRecords;
            List<ExecOrder> listExecOrder = _execOrderService.GetPaged(conditionStr, "OrderID DESC", 0, 10, out numberOfRecords).ToList();

            if (numberOfRecords>0)
            {
                int sumVolume = listExecOrder.Sum(p => p.Volume);
                int currentOddLot = Convert.ToInt32(stockAvailable.AvaiVolume % 100);
                if (currentOddLot < (sumVolume + volume)) return CommonEnums.REJECT_REASON.NOT_ENOUGH_STOCK;
            }

            return CommonEnums.REJECT_REASON.IS_VALID;
        }

        /// <summary>
        /// Determines whether [is valid transaction] [the specified account no].
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="side">The side.</param>
        /// <param name="subCustAccounts">The sub cust accounts.</param>
        /// <returns>
        /// <para>Result checking if this is a valid transaction.</para>
        /// <para>REJECT_REASON=NOT_BUY_SELL_THE_SAME_STOCK: Not allow to buy and sell the same stock.</para>
        /// <para>REJECT_REASON=IS_VALID: This is a valid order.</para>
        /// </returns>
        private CommonEnums.REJECT_REASON IsValidTransaction(string accountNo, string symbol, char side, List<string> subCustAccounts)
        {
            var subCustAccountCollection = new StringBuilder(100);
            foreach (var subCustAccountId in subCustAccounts)
            {
                subCustAccountCollection.Append("'" + subCustAccountId + "', ");
            }

            string accountCondition = subCustAccountCollection.ToString().Substring(
                0, subCustAccountCollection.ToString().Length - 2); // Remove ", "

            string conditionStr = string.Empty;

            if (side == (char)CommonEnums.TRADE_SIDE.BUY)
            {
                conditionStr = string.Format(
                    " (Side = '{0}') AND (Condition is null Or Condition = '{3}') AND (SubCustAccountID in ({1})) AND (SecSymbol = '{2}')",
                    ((char)CommonEnums.TRADE_SIDE.SELL),
                    accountCondition,
                    symbol,(char)CommonEnums.CONDITION.NO_CONDITION);
            }
            else if (side == (char)CommonEnums.TRADE_SIDE.SELL)
            {
                conditionStr = string.Format(
                    " (Side = '{0}') AND (Condition is null Or Condition = '{3}') AND (SubCustAccountID in ({1})) AND (SecSymbol = '{2}')",
                    ((char)CommonEnums.TRADE_SIDE.BUY),
                    accountCondition,
                    symbol,(char)CommonEnums.CONDITION.NO_CONDITION);
            }

            int numberOfRecords;
            List<ExecOrder> listExecOrder= _execOrderService.GetPaged(conditionStr, "OrderID DESC", 0, 10, out numberOfRecords).ToList();
            
            bool isExisted = (numberOfRecords > 0) ? true : false;

            if (isExisted)
            {
                //if number of matched == 0 || order stauts == canceled, allow new order
                var cancelledRecordCount =
                    listExecOrder.Count(n => n.OrderStatus.Value == (int) CommonEnums.ORDER_STATUS.CANCELLED);
                var fullMatchedRecordCount =
                    listExecOrder.Count(n => n.OrderStatus.Value == (int) CommonEnums.ORDER_STATUS.FULL_MATCHED);
                var rejectedRecordCount =
                    listExecOrder.Count(n => n.OrderStatus.Value == (int)CommonEnums.ORDER_STATUS.ORDER_REJECTED);
                if (listExecOrder.Count == (cancelledRecordCount + fullMatchedRecordCount + rejectedRecordCount))
                {
                    return CommonEnums.REJECT_REASON.IS_VALID;
                }
                LogHandler.Log(
                    "IsValidTransaction: " + accountNo + " buy and sell same " + symbol + " intra-day", 
                    GetType() + ".IsValidTransaction", 
                    TraceEventType.Warning);
                return CommonEnums.REJECT_REASON.NOT_BUY_SELL_THE_SAME_STOCK;
            }

            return CommonEnums.REJECT_REASON.IS_VALID;
        }

        /// <summary>
        /// Determines whether [is valid trade permission] [the specified account no].
        /// Validation includes:
        /// 1. CanBuy
        /// 2. CanSell
        /// 3. IsActive
        /// </summary>
        /// <param name="subCustAccount">
        /// The sub Cust Account.
        /// </param>
        /// <param name="permissions">
        /// The permissions.
        /// </param>
        /// <returns>
        /// <para>Result checking if this is a valid order.</para>
        /// <para>REJECT_REASON=ERROR_LOCK_ACCOUNT: Account is locked.</para>
        /// <para>REJECT_REASON=ERROR_ACCOUNT_NOT_BUY_PERMISSION: Account is not allowed to buy stocks.</para>
        /// <para>REJECT_REASON=ERROR_ACCOUNT_NOT_SELL_PERMISSION: Account is not allowed to sell stocks.</para>
        /// <para>REJECT_REASON=IS_VALID: This is a valid order.</para>
        /// </returns>
        private CommonEnums.REJECT_REASON IsValidTradePermission(SubCustAccount subCustAccount, CommonEnums.PERMISSION_TYPE permissions)
        {
            if (subCustAccount.Actived == false)
            {
                LogHandler.Log(
                    "IsValidTradePermission: " + subCustAccount.SubCustAccountId + " is locked",
                    GetType() + ".IsValidTradePermission",
                    TraceEventType.Warning);
                return CommonEnums.REJECT_REASON.ERROR_LOCK_ACCOUNT;
            }

            bool canBuy = false;
            bool canSell = false;
            //bool canTrade = false;

            foreach (
                SubCustAccountPermission subCustAccountPermission in subCustAccount.SubCustAccountPermissionCollection)
            {
                /*if (permissions == CommonEnums.PERMISSION_TYPE.VALIDATE_ORDER || permissions == CommonEnums.PERMISSION_TYPE.PORFOLIO)
                {
                    if (subCustAccountPermission.CustServicesPermissionId ==
                        (int)CommonEnums.SUB_ACCOUNT_PERMISSIONS.CAN_TRADE)
                    {
                        canTrade = true;
                    }    
                }
                else
                {
                    canTrade = true;
                }*/

                if (permissions == CommonEnums.PERMISSION_TYPE.VALIDATE_ORDER)
                {
                    if (subCustAccountPermission.CustServicesPermissionId ==
                        (int)CommonEnums.SUB_ACCOUNT_PERMISSIONS.CAN_BUY)
                    {
                        canBuy = true;
                    }    
                }
                else
                {
                    canBuy = true;
                }

                if (permissions == CommonEnums.PERMISSION_TYPE.VALIDATE_ORDER || permissions == CommonEnums.PERMISSION_TYPE.PORFOLIO)
                {
                    if (subCustAccountPermission.CustServicesPermissionId ==
                        (int)CommonEnums.SUB_ACCOUNT_PERMISSIONS.CAN_SELL)
                    {
                        canSell = true;
                    }    
                }
                else
                {
                    canSell = true;
                }
            }

            /*if (canTrade == false)
            {
                LogHandler.Log(
                    "IsValidTradePermission: " + subCustAccount.SubCustAccountId + " not trade permission",
                    GetType() + ".IsValidTradePermission",
                    TraceEventType.Warning);
                return CommonEnums.REJECT_REASON.ERROR_ACCOUNT_NOT_TRADE_PERMISSION;
            }*/

            if (canBuy == false)
            {
                LogHandler.Log(
                    "IsValidTradePermission: " + subCustAccount.SubCustAccountId + " not buy permission",
                    GetType() + ".IsValidTradePermission",
                    TraceEventType.Warning);
                return CommonEnums.REJECT_REASON.ERROR_ACCOUNT_NOT_BUY_PERMISSION;
            }

            if (canSell == false)
            {
                LogHandler.Log(
                    "IsValidTradePermission: " + subCustAccount.SubCustAccountId + " not sell permission",
                    GetType() + ".IsValidTradePermission",
                    TraceEventType.Warning);
                return CommonEnums.REJECT_REASON.ERROR_ACCOUNT_NOT_SELL_PERMISSION;
            }

            return CommonEnums.REJECT_REASON.IS_VALID;
        }

        /// <summary>
        /// Determines whether this account can put advance orders.
        /// </summary>
        /// <param name="subCustAccount">
        /// The sub Cust Account.
        /// </param>
        /// <returns>
        /// <para>Result of validating permission.</para>
        /// <para>REJECT_REASON=ERROR_LOCK_ACCOUNT: Account is locked.</para>
        /// <para>REJECT_REASON=ERROR_ACCOUNT_NOT_CONDITION_ORDER: Account is not allowed to put advance order.</para>
        /// <para>REJECT_REASON=IS_VALID: This is a valid order.</para>
        /// </returns>
        private CommonEnums.REJECT_REASON IsValidAdvanceOrderPermission(SubCustAccount subCustAccount)
        {
            if (subCustAccount.Actived == false)
            {
                LogHandler.Log(
                    "IsValidTradePermission: " + subCustAccount.SubCustAccountId + " is locked",
                    GetType() + ".IsValidTradePermission",
                    TraceEventType.Warning);
                return CommonEnums.REJECT_REASON.ERROR_LOCK_ACCOUNT;
            }

            bool canPutAdvanceOrder = false;
            //bool canTrade = false;

            foreach (
                SubCustAccountPermission subCustAccountPermission in subCustAccount.SubCustAccountPermissionCollection)
            {
                /*if (subCustAccountPermission.CustServicesPermissionId == (int)CommonEnums.SUB_ACCOUNT_PERMISSIONS.CAN_TRADE)
                {
                    canTrade = true;
                }*/

                if (subCustAccountPermission.CustServicesPermissionId == (int)CommonEnums.SUB_ACCOUNT_PERMISSIONS.CONDITION_ORDER)
                {
                    canPutAdvanceOrder = true;
                }
            }

            /*if (canTrade == false)
            {
                LogHandler.Log(
                    "IsValidAdvanceOrderPermission: " + subCustAccount.SubCustAccountId + " not trade permission",
                    GetType() + ".IsValidAdvanceOrderPermission", TraceEventType.Warning);
                return CommonEnums.REJECT_REASON.ERROR_ACCOUNT_NOT_TRADE_PERMISSION;
            }*/

            if (canPutAdvanceOrder == false)
            {
                LogHandler.Log(
                    "IsValidAdvanceOrderPermission: " + subCustAccount.SubCustAccountId + " no condition order permission",
                    GetType() + ".IsValidAdvanceOrderPermission", TraceEventType.Warning);
                return CommonEnums.REJECT_REASON.ERROR_ACCOUNT_NOT_CONDITION_ORDER;
            }

            return CommonEnums.REJECT_REASON.IS_VALID;
        }

        /// <summary>
        /// Determines whether [is valid balance] [the specified account no].
        /// Validation includes:
        /// 1. Validate stock available
        /// 2. Validate cash available
        /// 3. validate remain room
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="side">The side.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="marketId">The market id.</param>
        /// <param name="condPrice">The cond price.</param>
        /// <param name="volume">The volume.</param>
        /// <param name="price">The price.</param>
        /// <param name="conditionOrderId">Condition order id.</param>
        /// <param name="strEffDate">Effect date</param>
        /// <param name="isConditionOrder">if set to <c>true</c> [is condition order].</param>
        /// <returns>
        /// 	<para>Result checking if customer's balance is OK or not.</para>
        /// 	<para>REJECT_REASON=NOT_ENOUGH_CASH: Customer has not enough money.</para>
        /// 	<para>REJECT_REASON=NOT_ENOUGH_STOCK: Customer has not enough stocks.</para>
        /// 	<para>REJECT_REASON=IS_VALID: Customer's balance is OK.</para>
        /// </returns>
        private CommonEnums.REJECT_REASON IsValidBalance(
            string accountNo,
            char side,
            string symbol,
            int marketId,
            char condPrice,
            int volume,
            decimal price,
            long conditionOrderId,
            string strEffDate,
            bool isConditionOrder)
        {
            decimal moneyUnit = 0;

            if (side == (char)CommonEnums.TRADE_SIDE.BUY)
            {
                var center = (CommonEnums.MARKET_ID)marketId;
                var stockInfo = Serializer.Deserialize<ResultObject<StockInfo>>(_rtService.GetStockInfo(symbol));
                switch (center)
                {
                    case CommonEnums.MARKET_ID.HOSE:
                        moneyUnit = (decimal)CommonEnums.TRADE_RULE.MONEY_UNIT_HOSE;

                        if (condPrice == Constants.ORDER_TYPE_ATO || condPrice == Constants.ORDER_TYPE_ATC || condPrice == Constants.ORDER_TYPE_MP)
                        {
                            
                            if (stockInfo != null)
                            {
                                price = (decimal) stockInfo.Result.Ceiling;
                            }
                        }

                        break;
                    case CommonEnums.MARKET_ID.HNX:
                        if (condPrice == Constants.ORDER_TYPE_ATO || condPrice == Constants.ORDER_TYPE_ATC || condPrice == Constants.ORDER_TYPE_MP ||
                            condPrice == Constants.ORDER_TYPE_MAK ||condPrice == Constants.ORDER_TYPE_MOK)
                        {
                            if (stockInfo != null)
                            {
                                price = (decimal)stockInfo.Result.Ceiling;
                            }
                        }
                        moneyUnit = (decimal)CommonEnums.TRADE_RULE.MONEY_UNIT_HNX;
                        break;
                    case CommonEnums.MARKET_ID.UPCoM:
                        if (condPrice == Constants.ORDER_TYPE_ATO || condPrice == Constants.ORDER_TYPE_ATC || condPrice == Constants.ORDER_TYPE_MP ||
                            condPrice == Constants.ORDER_TYPE_MAK || condPrice == Constants.ORDER_TYPE_MOK)
                        {
                            if (stockInfo != null)
                            {
                                price = (decimal)stockInfo.Result.Ceiling;
                            }
                        }
                        moneyUnit = (decimal)CommonEnums.TRADE_RULE.MONEY_UNIT_UPCOM;
                        break;
                }

                var defaultCashAvailable = GetAvailableCash(accountNo, side, conditionOrderId, strEffDate, isConditionOrder);
                
                
                decimal cashAmount = volume * price * moneyUnit;
                // Fee of buying order
                decimal fee = FeeService.CalculateFee(cashAmount, (int) CommonEnums.FEE_TYPE.FEE_TRADE);
                if ((cashAmount + fee) >= defaultCashAvailable)
                {
                    if (isConditionOrder)
                    {
                        if (!IsValidLimitQuantityAdvanceOrder(side, accountNo, string.Empty))
                        {
                            LogHandler.Log(String.Format("IsValidAccount: {0} not enough cash, buy credit = {1}", accountNo, defaultCashAvailable),
                                           GetType() + ".IsValidAccount", TraceEventType.Warning);
                            return CommonEnums.REJECT_REASON.NOT_ENOUGH_CASH;
                        }    
                    }
                    else
                    {
                        LogHandler.Log(String.Format("IsValidAccount: {0} not enough cash, buy credit = {1}", accountNo, defaultCashAvailable),
                                           GetType() + ".IsValidAccount", TraceEventType.Warning);
                        return CommonEnums.REJECT_REASON.NOT_ENOUGH_CASH;
                    }
                }
            }                      
            return CommonEnums.REJECT_REASON.IS_VALID;
        }

        private CommonEnums.REJECT_REASON IsValidStockBalance(char side, string symbol, string accountNo, long conditionOrderId, string strEffDate, bool isConditionOrder, int volume)
        {
            if (side == (char)CommonEnums.TRADE_SIDE.SELL)
            {
                var defaultStockAvailable = GetAvailableStock(accountNo, symbol, side, conditionOrderId, strEffDate,
                                                              isConditionOrder);
                if (defaultStockAvailable < volume)
                {
                    LogHandler.Log(
                        "IsValidAccount: " + accountNo + " not enough stock for sell, available = " +
                        defaultStockAvailable + ", symbol = " + symbol,
                        GetType() + ".IsValidAccount",
                        TraceEventType.Warning);
                    return CommonEnums.REJECT_REASON.NOT_ENOUGH_STOCK;
                }
            }
            return CommonEnums.REJECT_REASON.IS_VALID;
        }

        /// <summary>
        /// Get available cash.
        /// </summary>
        /// <param name="accountNo">Account id</param>
        /// <param name="side">Buy or Sell side</param>
        /// <param name="conditionOrderId">Condition order id</param>
        /// <param name="strEffDate">Effect date</param>
        /// <param name="isCondtionOrder">if set to <c>true</c> [is condtion order].</param>
        /// <returns>Available cash</returns>
        public decimal GetAvailableCash(string accountNo, char side, long conditionOrderId, string strEffDate, bool isCondtionOrder)
        {
            int accountType = ETradeCommon.Utils.GetAccountType(accountNo);
            var etradeService = new ETradeServices();
            var resulCashAvailable = etradeService.GetAvailableCash(accountNo, accountType, isCondtionOrder);
            CashAvailable cashAvailable = resulCashAvailable.Result;

            decimal buyPower;

            if (cashAvailable == null)
            {
                buyPower = 0;
            }
            else
            {
                buyPower = cashAvailable.BuyCredit;
                if(isCondtionOrder)
                {
                    var effectDate = new DateTime();
                    if (ETradeCommon.Utils.IsValidDate(strEffDate, ref effectDate))
                    {
                        if (cashAvailable.Date_WTR_T1 != null && effectDate.Date >= cashAvailable.Date_WTR_T1.Date)
                            buyPower += cashAvailable.WTR_T1;
                        if (cashAvailable.Date_WTR_T2 != null && effectDate.Date >= cashAvailable.Date_WTR_T2.Date)
                            buyPower += cashAvailable.WTR_T2;
                        if (cashAvailable.Date_WTR_T3 != null && effectDate.Date >= cashAvailable.Date_WTR_T3.Date)
                            buyPower += cashAvailable.WTR_T3;
                        if (cashAvailable.Date_WTR != null && effectDate.Date >= cashAvailable.Date_WTR.Date)
                            buyPower += cashAvailable.WTR;
                    }
                }
            }
            
            //    var totalConditionOrderMoney = GetTotalConditionOrderMoney(side, accountNo, conditionOrderId);
            //    buyPower = buyPower - totalConditionOrderMoney;

            // decimal transferedMoney = CashTransferService.GetTotalUnfinishedCashTransferAmount(accountNo);
            //var defaultCashAvailable = buyPower - totalConditionOrderMoney - transferedMoney;

            return buyPower;
        }

        /// <summary>
        /// Get available stock.
        /// </summary>
        /// <param name="accountNo">Account id</param>
        /// <param name="secSymbol">Security symbol</param>
        /// <param name="side">Buy or Sell side</param>
        /// <param name="conditionOrderId">Condition order id</param>
        /// <param name="strEffDate">Effect date.</param>
        /// <param name="isConditionOrder">if set to <c>true</c> [is condition order].</param>
        /// <returns>Available stock</returns>
        public decimal GetAvailableStock(string accountNo, string secSymbol, char side, long conditionOrderId, string strEffDate,bool isConditionOrder)
        {
            int accountType = ETradeCommon.Utils.GetAccountType(accountNo);
            var etradeService = new ETradeServices();
            var resulstockAvailable = etradeService.GetAvailableStock(accountNo, secSymbol, accountType, isConditionOrder);
           
            decimal stockVolume;

            if (resulstockAvailable.Result == null)
            {
                stockVolume = 0;
            }
            else
            {
                stockVolume = resulstockAvailable.Result.AvaiVolume;
                if(isConditionOrder)
                {
                    var effectDate = new DateTime();
                    if (ETradeCommon.Utils.IsValidDate(strEffDate, ref effectDate))
                    {
                        if (resulstockAvailable.Result.Date_WTR_T1 != null && effectDate.Date >= resulstockAvailable.Result.Date_WTR_T1.Date)
                            stockVolume += resulstockAvailable.Result.WTR_T1;
                        if (resulstockAvailable.Result.Date_WTR_T2 != null && effectDate.Date >= resulstockAvailable.Result.Date_WTR_T2.Date)
                            stockVolume += resulstockAvailable.Result.WTR_T2;
                        if (resulstockAvailable.Result != null && effectDate.Date >= resulstockAvailable.Result.Date_WTR_T3.Date)
                            stockVolume += resulstockAvailable.Result.WTR_T3;
                        if (resulstockAvailable.Result.Date_WTR != null && effectDate.Date >= resulstockAvailable.Result.Date_WTR.Date)
                            stockVolume += resulstockAvailable.Result.WTR;
                    }
                }
            }

            // Get total stocks of condition orders
            //var totalConditionOrderStock = GetTotalConditionOrderStock(side, accountNo, conditionOrderId, secSymbol);

            //decimal transferStocks = StockTransferService.GetTotalUnfinishedStockTransferAmount(accountNo, secSymbol); ;

            //var defaultStockAvailable = stockVolume - totalConditionOrderStock - transferStocks;
            //var defaultStockAvailable = stockVolume - transferStocks;

            return stockVolume;
        }
        /// <summary>
        /// Get total money of condition orders of an account.
        /// </summary>
        /// <param name="side">Buy or Sell side</param>
        /// <param name="subCustAccountId">Sub cust</param>
        /// <param name="conditionOrderId"></param>
        /// <returns></returns>
        public decimal GetTotalConditionOrderMoney(char side, string subCustAccountId, long conditionOrderId)
        {
            decimal totalMoney = 0;
            var list = ConditionOrderService.GetListUnfinishedConditionOrders(side, subCustAccountId, string.Empty);
            if (list != null)
            {
                foreach (var conditionOrder in list)
                {
                    if(conditionOrder.ConditionOrderId != conditionOrderId)
                    {
                        decimal price = 0;
                        if (conditionOrder.TypeOfCond == (short) CommonEnums.CONDITION_ORDER_TYPE.ATO)
                        {
                            var stockInfo =
                                Serializer.Deserialize<ResultObject<StockInfo>>(
                                    _rtService.GetStockInfo(conditionOrder.SecSymbol));

                            if (stockInfo != null)
                            {
                                price = (decimal)stockInfo.Result.Ceiling;
                            }
                        }
                        else
                        {
                            price = conditionOrder.Price;
                        }
                        
                        var volume = conditionOrder.Volume - conditionOrder.MatchedVolume;
                        decimal moneyUnit = 0;
                        if (conditionOrder.Market == ((int)CommonEnums.MARKET_ID.HOSE).ToString())
                        {
                            moneyUnit = (decimal) CommonEnums.TRADE_RULE.MONEY_UNIT_HOSE;
                        }
                        else if (conditionOrder.Market == ((int)CommonEnums.MARKET_ID.HNX).ToString())
                        {
                            moneyUnit = (decimal)CommonEnums.TRADE_RULE.MONEY_UNIT_HNX;
                        }
                        else if (conditionOrder.Market == ((int)CommonEnums.MARKET_ID.UPCoM).ToString())
                        {
                            moneyUnit = (decimal)CommonEnums.TRADE_RULE.MONEY_UNIT_UPCOM;
                        }
                        var cashAmount = price * volume * moneyUnit;
                        var fee = FeeService.CalculateFee(cashAmount, (int) CommonEnums.FEE_TYPE.FEE_TRADE);
                        totalMoney = totalMoney + cashAmount + fee;
                    }
                }
            }
            return totalMoney;
        }

        /// <summary>
        /// Get total stock of condition orders of an account.
        /// </summary>
        /// <param name="side">Buy or Sell side</param>
        /// <param name="subCustAccountId">Sub cust</param>
        /// <param name="conditionOrderId">The condition order id.</param>
        /// <param name="symbol">The symbol.</param>
        /// <returns></returns>
        public decimal GetTotalConditionOrderStock(char side, string subCustAccountId, long conditionOrderId, string symbol)
        {
            decimal totalStock = 0;
            var list = ConditionOrderService.GetListUnfinishedConditionOrders(side, subCustAccountId, symbol);
            if (list != null)
            {
                foreach (var conditionOrder in list)
                {
                    if (conditionOrder.ConditionOrderId != conditionOrderId)
                    {
                        totalStock = totalStock + conditionOrder.Volume;
                    }
                }
            }
            return totalStock;
        }

        /// <summary>
        /// Determines whether [is valid account] [the specified account no].
        /// </summary>
        /// <param name="accountNo">
        /// The account no.
        /// </param>
        /// <param name="side">
        /// The side.
        /// </param>
        /// <param name="symbol">
        /// The symbol.
        /// </param>
        /// <param name="volume">
        /// The volume.
        /// </param>
        /// <param name="customerType"></param>
        /// <returns>
        /// <para>Result checking if this is a valid volume for foreign account.</para>
        /// <para>REJECT_REASON=OVER_REMAIN_VOLUME: Available volume is not enough.</para>
        /// <para>REJECT_REASON=IS_VALID: It is valid.</para>
        /// </returns>
        private CommonEnums.REJECT_REASON IsValidAccount(string accountNo, char side, string symbol, int volume, int customerType)
        {
            if (side == (char)CommonEnums.TRADE_SIDE.BUY)
            {
                if (customerType == (int)CommonEnums.CUSTOMER_TYPE.FOREIGN)
                {
                    var stockInfo = Serializer.Deserialize<ResultObject<StockInfo>>(_rtService.GetStockInfo(symbol));

                    if (stockInfo == null || stockInfo.Result == null)
                    {
                        LogHandler.Log(
                            "IsValidAccount: over remain room, " + symbol + " not existed in collection",
                            GetType() + ".IsValidAccount",
                            TraceEventType.Warning);
                        return CommonEnums.REJECT_REASON.OVER_REMAIN_VOLUME;
                    }
                    
                    if (stockInfo.Result.AvailableForeignRoom < 10 || stockInfo.Result.AvailableForeignRoom < volume)
                    {
                        LogHandler.Log(
                            "IsValidAccount: over remain room, acccountNo: " + accountNo + ", symbol: " + symbol +
                            ", remainRoom: " + stockInfo.Result.AvailableForeignRoom,
                            GetType() + ".IsValidAccount",
                            TraceEventType.Warning);

                        return CommonEnums.REJECT_REASON.OVER_REMAIN_VOLUME;
                    }
                    
                }
            }

            return CommonEnums.REJECT_REASON.IS_VALID;
        }


        /// <summary>
        /// Determines whether [is valid new order] [the specified market id].
        /// </summary>
        /// <param name="marketId">The market id.</param>
        /// <param name="accountNo">The account no.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="side">The side.</param>
        /// <param name="volume">The volume.</param>
        /// <param name="price">The price.</param>
        /// <param name="conPrice">The con price.</param>
        /// <param name="accountType">Type of the account.</param>
        /// <param name="customerType">Type of the customer.</param>
        /// <param name="subCustAccount">The sub cust account.</param>
        /// <param name="subCustAccounts">The sub cust accounts.</param>
        /// <param name="stockAvailable">The stock available.</param>
        /// <param name="conditionOrderId">Condition order id.</param>
        /// <param name="isMargin">This account is margin account or not.</param>
        /// <returns>
        /// 	<para>Result checking if this is a valid order.</para>
        /// 	<para>REJECT_REASON=ERROR_MARKET_CLOSE: Market closed.</para>
        /// 	<para>REJECT_REASON=ERROR_ATO_NOT_IN_READY_AND_SESSION1: Cannot put ATO order in READY and SESSION1 session.</para>
        /// 	<para>REJECT_REASON=ERROR_ATC_NOT_IN_SESSION3: Cannot put ATC in SESSION3 session.</para>
        /// 	<para>REJECT_REASON=ERROR_PRICE_NOT_MULTIPLE_100_FOR_HOSE: Price is incorrect for HOSE market.</para>
        /// 	<para>REJECT_REASON=ERROR_PRICE_NOT_MULTIPLE_500_FOR_HOSE: Price is incorrect for HOSE market.</para>
        /// 	<para>REJECT_REASON=ERROR_PRICE_NOT_MULTIPLE_1000_FOR_HOSE: Price is incorrect for HOSE market.</para>
        /// 	<para>REJECT_REASON=ERROR_HNX_NOT_USE_ATO_ATC: Cannot put ATO, ATC order in HNX market.</para>
        /// 	<para>REJECT_REASON=ERROR_PRICE_NOT_MULTIPLE_100_FOR_HNX: Price is incorrect for HNX market.</para>
        /// 	<para>REJECT_REASON=ERROR_UPCOM_NOT_USE_ATO_ATC: Cannot put ATO, ATC order in UPCOM market.</para>
        /// 	<para>REJECT_REASON=ERROR_PRICE_NOT_MULTIPLE_100_FOR_UPCOM: Price is incorrect for UPCOM market.</para>
        /// 	<para>REJECT_REASON=INCORRECT_SIDE: The side is not buy or sell side.</para>
        /// 	<para>REJECT_REASON=INCORRECT_VOL: The volume is incorrect.</para>
        /// 	<para>REJECT_REASON=OVER_MAX_VOL: The volume is over max allowed volume.</para>
        /// 	<para>REJECT_REASON=INCORRECT_STOCK: Stock is incorrect.</para>
        /// 	<para>REJECT_REASON=STOCK_IS_HALT: Stock is halt.</para>
        /// 	<para>REJECT_REASON=PRICE_BELOW_FLOOR: Price is below floor price.</para>
        /// 	<para>REJECT_REASON=PRICE_ABOVE_CEILING: Price is over ceiling price.</para>
        /// 	<para>REJECT_REASON=NOT_BUY_SELL_THE_SAME_STOCK: Not allow to buy and sell the same stock.</para>
        /// 	<para>REJECT_REASON=ERROR_LOCK_ACCOUNT: Account is locked.</para>
        /// 	<para>REJECT_REASON=ERROR_ACCOUNT_NOT_BUY_PERMISSION: Account is not allowed to buy stocks.</para>
        /// 	<para>REJECT_REASON=ERROR_ACCOUNT_NOT_SELL_PERMISSION: Account is not allowed to sell stocks.</para>
        /// 	<para>REJECT_REASON=NOT_ENOUGH_CASH: Customer has not enough money.</para>
        /// 	<para>REJECT_REASON=NOT_ENOUGH_STOCK: Customer has not enough stocks.</para>
        /// 	<para>REJECT_REASON=OVER_REMAIN_VOLUME: Available volume is not enough.</para>
        /// 	<para>REJECT_REASON=IS_VALID: This is a valid order.</para>
        /// </returns>
        public CommonEnums.REJECT_REASON IsValidNewOrder(
            int marketId,
            string accountNo,
            string symbol,
            char side,
            int volume,
            decimal price,
            char conPrice,
            int accountType,
            int customerType,
            SubCustAccount subCustAccount,
            List<string> subCustAccounts,
            StockAvailable stockAvailable, long conditionOrderId, bool isMargin, char condition)
        {


            
            CommonEnums.REJECT_REASON retCode = IsValidMarket(marketId, conPrice);

            if (retCode != CommonEnums.REJECT_REASON.IS_VALID)
            {
                return retCode;
            }

            retCode = ETradeCommon.Utils.IsValidStepPrice(marketId, price, conPrice);

            if (retCode != CommonEnums.REJECT_REASON.IS_VALID)
            {
                return retCode;
            }

            // Validate side
            if (side != (char)CommonEnums.TRADE_SIDE.SELL && side != (char)CommonEnums.TRADE_SIDE.BUY)
            {
                return CommonEnums.REJECT_REASON.INCORRECT_SIDE;
            }

            if (condition == (char)CommonEnums.CONDITION.ODD && side == (char)CommonEnums.TRADE_SIDE.SELL)
            {
                if (volume <= 0 || volume >= 100)
                    return CommonEnums.REJECT_REASON.ERROR_ODD_LOT_INVALID_VOLUME;
                if (stockAvailable == null) 
                    return CommonEnums.REJECT_REASON.ERROR_ODD_LOT_INVALID_VOLUME;
                if (((stockAvailable.AvaiVolume % 100) >= 100) || ((stockAvailable.AvaiVolume % 100) == 0)) 
                    return CommonEnums.REJECT_REASON.ERROR_ODD_LOT_INVALID_VOLUME;
                if (conPrice != (char)CommonEnums.ORDER_CON_PRICE.LO)
                {
                    return CommonEnums.REJECT_REASON.ERROR_ODD_LOT_INVALID_CONPRICE;
                }
            }
            else
            {
                retCode = IsValidVolUnit(marketId, volume);

                if (retCode != CommonEnums.REJECT_REASON.IS_VALID)
                {
                    return retCode;
                }
            }

            

            retCode = IsValidStock(symbol, false,side,conPrice);

            if (retCode != CommonEnums.REJECT_REASON.IS_VALID)
            {
                return retCode;
            }

            retCode = IsValidPrice(marketId, price, conPrice, symbol);

            if (retCode != CommonEnums.REJECT_REASON.IS_VALID)
            {
                return retCode;
            }

            //retCode = IsValidTraderId();

            //if (retCode != CommonEnums.REJECT_REASON.IS_VALID)
            //{
            //    return retCode;
            //}
            

            retCode = IsValidTradePermission(subCustAccount, CommonEnums.PERMISSION_TYPE.VALIDATE_ORDER);

            if (retCode != CommonEnums.REJECT_REASON.IS_VALID)
            {
                return retCode;
            }
            if (condition == (char)CommonEnums.CONDITION.ODD && side == (char)CommonEnums.TRADE_SIDE.SELL)
            {
                retCode = IsValidTransactionOddLot(accountNo, symbol, subCustAccounts, stockAvailable, volume);

                return retCode;
            }
            else
            {
                retCode = IsValidTransaction(accountNo, symbol, side, subCustAccounts);

                if (retCode != CommonEnums.REJECT_REASON.IS_VALID)
                {
                    return retCode;
                }
            }

            if (!(isMargin && (side == (char)CommonEnums.TRADE_SIDE.BUY)))
            {
                retCode = IsValidBalance(accountNo, side, symbol, marketId, conPrice, volume, price, conditionOrderId,string.Empty, false);

                if (retCode != CommonEnums.REJECT_REASON.IS_VALID)
                {
                    return retCode;
                }
            }

            retCode = IsValidAccount(accountNo, side, symbol, volume, customerType);

            if (retCode != CommonEnums.REJECT_REASON.IS_VALID)
            {
                return retCode;
            }

            return CommonEnums.REJECT_REASON.IS_VALID;
        }

        /// <summary>
        /// Determines whether the order is a valid order to cancel.
        /// </summary>
        /// <param name="execOrder">The exec order.</param>
        /// <returns>
        /// <para>Reject reason.</para>
        /// <para>REJECT_REASON=INCORRECT_ORDER_NO: The order id is incorrect.</para>
        /// <para>REJECT_REASON=NOT_CANCEL_ORDER_CANCELED: Cannot cancel a cancelled order.</para>
        /// <para>REJECT_REASON=NOT_CANCEL_ORDER_MATCHED: Cannot cancel a full matched order.</para>
        /// <para>REJECT_REASON=NOT_CANCEL_ORDER_FROM_DIFF_SOURCE: Cannot cancel a order from other source.</para>
        /// <para>REJECT_REASON=NOT_CANCEL_IN_THIS_PERIOD_PHASE: Cannot cancel a order in this phase.</para>
        /// </returns>
        public CommonEnums.REJECT_REASON IsValidCancelOrder(ExecOrder execOrder)
        {
            if (execOrder == null)
            {
                //LogHandler.Log("IsValidCancelOrder: execOrder is null", GetType() + "IsValidCancelOrder", TraceEventType.Warning);
                return CommonEnums.REJECT_REASON.INCORRECT_ORDER_NO;
            }

            if (execOrder.OrderStatus == (short)CommonEnums.ORDER_STATUS.CANCELLED
                || execOrder.OrderStatus == (short)CommonEnums.ORDER_STATUS.ORDER_REJECTED
                || execOrder.OrderStatus == (short)CommonEnums.ORDER_STATUS.NEW_CANCEL
                || execOrder.OrderStatus == (short)CommonEnums.ORDER_STATUS.WAITING_CANCEL)
            {
                /*LogHandler.Log(
                    "IsValidCancelOrder: " + execOrder.OrderId + " is cancelled",
                    GetType() + "IsValidCancelOrder",
                    TraceEventType.Warning);*/
                return CommonEnums.REJECT_REASON.NOT_CANCEL_ORDER_CANCELED;
            }

            if ((execOrder.CancelledVolume + execOrder.ExecutedVol) == execOrder.Volume
                || execOrder.OrderStatus == (short)CommonEnums.ORDER_STATUS.FULL_MATCHED)
            {
                /*LogHandler.Log(
                    "IsValidCancelOrder: " + execOrder.OrderId + " is matched, matchedVol = " + execOrder.ExecutedVol +
                    ", cancelledVolume = " + execOrder.CancelledVolume + ", orderStatus = " + execOrder.OrderStatus,
                    GetType() + "IsValidCancelOrder",
                    TraceEventType.Warning);*/
                return CommonEnums.REJECT_REASON.NOT_CANCEL_ORDER_MATCHED;
            }

            char orderSource = execOrder.OrderSource == null
                                   ? (char)CommonEnums.ORDER_SOURCE.FROM_WEB
                                   : execOrder.OrderSource[0];

            // Only allow cancel the order keyed by itself.
            if (orderSource != AppConfig.OrderSource)
            {
                /*LogHandler.Log(
                    "IsValidCancelOrder: Cannot cancel from other source, Only allow cancel the order keyed by itself, order source = " +
                    orderSource,
                    GetType() + ".IsValidCancelOrder",
                    TraceEventType.Warning);*/
                return CommonEnums.REJECT_REASON.NOT_CANCEL_ORDER_FROM_DIFF_SOURCE;
            }

            // Validate order session
            int marketId = int.Parse(execOrder.Market);
            char conPrice = execOrder.ConPrice != null ? execOrder.ConPrice[0] : Constants.ORDER_TYPE_LO;
            char ordSession = execOrder.MarketStatus != null ? execOrder.MarketStatus[0] : (char)CommonEnums.ORDER_SESSION.SESSION0;

            CommonEnums.MARKET_STATUS marketStatus = MarketServices.GetMarketStatus(marketId);

            var tradingStatus = (CommonEnums.MARKET_STATUS)MarketServices.TradingStatus(marketId);

            CommonEnums.ORDER_SESSION currSession = ETradeCommon.Utils.OrderSession(marketId, marketStatus, tradingStatus);

            if (!IsSessionCancel(marketId, (CommonEnums.ORDER_SESSION)ordSession, conPrice, currSession))
            {
                /*LogHandler.Log(
                    "IsValidCancelOrder: Cannot cancel in this phase, current session = " + currSession,
                    GetType() + ".IsValidCancelOrder",
                    TraceEventType.Warning);*/
                return CommonEnums.REJECT_REASON.NOT_CANCEL_IN_THIS_PERIOD_PHASE;
            }

            return CommonEnums.REJECT_REASON.IS_VALID;
        }

        /// <summary>
        /// Determines whether this order can be updated or not.
        /// </summary>
        /// <param name="order">The exec order.</param>
        /// <returns>REJECT_REASON</returns>
        public CommonEnums.REJECT_REASON IsValidUpdatedOrder(ExecOrder order)
        {
            int marketOrder;
            if (!int.TryParse(order.Market, out marketOrder)) return CommonEnums.REJECT_REASON.ILLEGAL_MARKET_ID;
            if (marketOrder == (int)CommonEnums.MARKET_ID.HOSE)
            {
                return CommonEnums.REJECT_REASON.ERROR_NOT_ALLOW_UPDATE;
            }

            if (order.FisOrderId == null)
            {
                return CommonEnums.REJECT_REASON.ERROR_NOT_ALLOW_UPDATE;
            }

            if (order.ConPrice == ETradeCommon.Constants.ORDER_TYPE_ATO.ToString() || order.ConPrice == ETradeCommon.Constants.ORDER_TYPE_MOK.ToString() ||
                order.ConPrice == ETradeCommon.Constants.ORDER_TYPE_MAK.ToString() || order.ConPrice == ETradeCommon.Constants.ORDER_TYPE_MP.ToString())
            {
                return CommonEnums.REJECT_REASON.ERROR_NOT_ALLOW_UPDATE;
            }

            

            var marketStatus = MarketServices.GetMarketStatus(marketOrder);
            var tradingStatus = (CommonEnums.MARKET_STATUS)MarketServices.TradingStatus(marketOrder);
            char ordSession = order.MarketStatus != null ? order.MarketStatus[0] : (char)CommonEnums.ORDER_SESSION.SESSION0;
            CommonEnums.ORDER_SESSION currSession = ETradeCommon.Utils.OrderSession(marketOrder, marketStatus, tradingStatus);
            if (currSession != CommonEnums.ORDER_SESSION.SESSION1 && currSession != CommonEnums.ORDER_SESSION.SESSION2 &&
                currSession != CommonEnums.ORDER_SESSION.SESSION3 && currSession != CommonEnums.ORDER_SESSION.SESSION4 &&
                currSession != CommonEnums.ORDER_SESSION.SESSION7 && currSession != CommonEnums.ORDER_SESSION.SESSION8 &&
                currSession != CommonEnums.ORDER_SESSION.SESSION9 )
            {
                return CommonEnums.REJECT_REASON.ERROR_NOT_ALLOW_UPDATE;
                //if (ordSession != (char)CommonEnums.ORDER_SESSION.SESSION4)
                //{
                //    return CommonEnums.REJECT_REASON.ERROR_NOT_ALLOW_UPDATE;
                //}
            }
            if (order.ConPrice == ETradeCommon.Constants.ORDER_TYPE_ATC.ToString())
            {
                if (currSession != CommonEnums.ORDER_SESSION.SESSION9)
                {
                    return CommonEnums.REJECT_REASON.ERROR_NOT_ALLOW_UPDATE;
                }
            }
            switch (order.OrderStatus)
            {
                case (short)CommonEnums.ORDER_STATUS.NEW_ORDER:
                    if (currSession != CommonEnums.ORDER_SESSION.SESSION4 && currSession != CommonEnums.ORDER_SESSION.SESSION7 &&
                        currSession != CommonEnums.ORDER_SESSION.SESSION9)
                    {
                        return CommonEnums.REJECT_REASON.ERROR_NOT_ALLOW_UPDATE;
                    }
                    break;
                case (short)CommonEnums.ORDER_STATUS.CONFIRMED_FIS:
                case (short)CommonEnums.ORDER_STATUS.CONFIRMED_SET:
                    if ((order.ExecutedVol != null) && (order.ExecutedVol == order.Volume))
                    {
                        return CommonEnums.REJECT_REASON.ERROR_NOT_ALLOW_UPDATE;
                    }
                    break;
                default:
                    return CommonEnums.REJECT_REASON.ERROR_NOT_ALLOW_UPDATE;
            }
            if (tradingStatus == CommonEnums.MARKET_STATUS.CLOSE || tradingStatus == CommonEnums.MARKET_STATUS.AFTER_CLOSE||
                tradingStatus == CommonEnums.MARKET_STATUS.CLOSE_PT)
            {
                return CommonEnums.REJECT_REASON.ERROR_NOT_ALLOW_UPDATE;
            }
            if (order.OrderSource != ((char)CommonEnums.ORDER_SOURCE.FROM_WEB).ToString())
            {
                return CommonEnums.REJECT_REASON.ERROR_NOT_ALLOW_UPDATE;
            }
            return CommonEnums.REJECT_REASON.IS_VALID;
        }

        /// <summary>
        /// Determines whether this order can be cancelled or not.
        /// </summary>
        /// <param name="marketId"></param>
        /// <param name="ordSession">The ord session.</param>
        /// <param name="condPrice">The cond price.</param>
        /// <param name="currentSession">The current session.</param>
        /// <returns>
        /// 	<c>true</c> if it can be cancelled; otherwise, <c>false</c>.
        /// </returns>
        private static Boolean IsSessionCancel(int marketId, CommonEnums.ORDER_SESSION ordSession, char condPrice, CommonEnums.ORDER_SESSION currentSession)
        {
            switch (marketId)
            {
                case (int)CommonEnums.MARKET_ID.HOSE:
                    switch (currentSession)
                    {
                        case CommonEnums.ORDER_SESSION.SESSION1:
                            if (ordSession == CommonEnums.ORDER_SESSION.SESSION1)
                            {
                                return true;
                            }

                            break;
                        case CommonEnums.ORDER_SESSION.SESSION4:
                            if (condPrice == Constants.ORDER_TYPE_ATO)
                            {
                                return false;
                            }
                            if(condPrice == Constants.ORDER_TYPE_MP)
                            {
                                return true;
                            }
                            if (ordSession == CommonEnums.ORDER_SESSION.SESSION1 ||
                                    ordSession == CommonEnums.ORDER_SESSION.SESSION2 ||
                                    ordSession == CommonEnums.ORDER_SESSION.SESSION3 ||
                                    ordSession == CommonEnums.ORDER_SESSION.SESSION4)
                            {
                                return true;
                            }

                            if (condPrice == Constants.ORDER_TYPE_ATC && 
                                ordSession == CommonEnums.ORDER_SESSION.SESSION4)
                            {
                                return true;
                            }
                            break;
                        case CommonEnums.ORDER_SESSION.SESSION6:
                            if (condPrice == Constants.ORDER_TYPE_ATO)
                            {
                                return false;
                            }
                            if (ordSession == CommonEnums.ORDER_SESSION.SESSION6)
                            {
                                return true;
                            }
                            break;
                        case CommonEnums.ORDER_SESSION.SESSION7:
                            if (condPrice == Constants.ORDER_TYPE_ATO)
                            {
                                return false;
                            }
                            if(condPrice== Constants.ORDER_TYPE_MP)
                            {
                                return true;
                            }
                            if (ordSession == CommonEnums.ORDER_SESSION.SESSION1 ||
                                ordSession == CommonEnums.ORDER_SESSION.SESSION2 ||
                                ordSession == CommonEnums.ORDER_SESSION.SESSION3 ||
                                ordSession == CommonEnums.ORDER_SESSION.SESSION4 ||
                                ordSession == CommonEnums.ORDER_SESSION.SESSION5 ||
                                ordSession == CommonEnums.ORDER_SESSION.SESSION6 ||
                                 ordSession == CommonEnums.ORDER_SESSION.SESSION7)
                            {
                                return true;
                            }
                            break;
                        case CommonEnums.ORDER_SESSION.SESSION8:
                            if ((condPrice == Constants.ORDER_TYPE_ATO) || (condPrice == Constants.ORDER_TYPE_ATC))
                            {
                                return false;
                            }
                            if (ordSession == CommonEnums.ORDER_SESSION.SESSION1 ||
                                ordSession == CommonEnums.ORDER_SESSION.SESSION2 ||
                                ordSession == CommonEnums.ORDER_SESSION.SESSION3 ||
                                ordSession == CommonEnums.ORDER_SESSION.SESSION4 ||
                                ordSession == CommonEnums.ORDER_SESSION.SESSION5 ||
                                ordSession == CommonEnums.ORDER_SESSION.SESSION6 ||
                                ordSession == CommonEnums.ORDER_SESSION.SESSION7 ||
                                ordSession == CommonEnums.ORDER_SESSION.SESSION8)
                            {
                                return true;
                            }


                            break;

                        case CommonEnums.ORDER_SESSION.SESSION9:
                            if ((condPrice == Constants.ORDER_TYPE_ATO) || (condPrice == Constants.ORDER_TYPE_ATC))
                            {
                                return false;
                            }
                            if (ordSession == CommonEnums.ORDER_SESSION.SESSION1 ||
                                ordSession == CommonEnums.ORDER_SESSION.SESSION2 ||
                                ordSession == CommonEnums.ORDER_SESSION.SESSION3 ||
                                ordSession == CommonEnums.ORDER_SESSION.SESSION4 ||
                                ordSession == CommonEnums.ORDER_SESSION.SESSION5 ||
                                ordSession == CommonEnums.ORDER_SESSION.SESSION6 ||
                                ordSession == CommonEnums.ORDER_SESSION.SESSION7 ||
                                ordSession == CommonEnums.ORDER_SESSION.SESSION8)
                            {
                                return true;
                            }

                            break;
                        default:
                            return false;
                    }
                    break;
                case (int)CommonEnums.MARKET_ID.HNX:
                case (int)CommonEnums.MARKET_ID.UPCoM:
                    switch (condPrice)
                    {
                        case (char)CommonEnums.ORDER_CON_PRICE.LO:
                            if (currentSession == CommonEnums.ORDER_SESSION.SESSION1 || currentSession == CommonEnums.ORDER_SESSION.SESSION2 ||
                                currentSession == CommonEnums.ORDER_SESSION.SESSION3 || currentSession == CommonEnums.ORDER_SESSION.SESSION4 ||
                                currentSession == CommonEnums.ORDER_SESSION.SESSION7 || currentSession == CommonEnums.ORDER_SESSION.SESSION8 ||
                                currentSession == CommonEnums.ORDER_SESSION.SESSION9 )
                            {
                                return true;
                            }
                            break;
                        case (char)CommonEnums.ORDER_CON_PRICE.ATO:
                            if (currentSession == CommonEnums.ORDER_SESSION.SESSION1 || currentSession == CommonEnums.ORDER_SESSION.SESSION2 )
                            {
                                return true;
                            }
                            break;
                        case (char)CommonEnums.ORDER_CON_PRICE.ATC:
                            if (currentSession == CommonEnums.ORDER_SESSION.SESSION4 || currentSession == CommonEnums.ORDER_SESSION.SESSION7 ||
                                currentSession == CommonEnums.ORDER_SESSION.SESSION9)
                            {
                                return true;
                            }
                            break;
                    }
                    break;
            }
            return false;
        }

        /// <summary>
        /// Determines whether this instance can sell the specified symbol.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <param name="sellableShare">The sellable share.</param>
        /// <param name="accountNo">The account no.</param>
        /// <param name="subCustAccount">Sub account information.</param>
        /// <param name="subCustAccounts">The sub cust accounts.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can sell the specified symbol; otherwise, <c>false</c>.
        /// </returns>
        public bool CanSell(string symbol, decimal sellableShare, string accountNo, SubCustAccount subCustAccount,List<string> subCustAccounts)
        {
            var stockInfo = Serializer.Deserialize<ResultObject<StockInfo>>(_rtService.GetStockInfo(symbol));

            if (stockInfo == null || stockInfo.Result == null)
            {
                /*LogHandler.Log(
                    String.Format("CanSell: {0} not existed in collection", symbol), 
                    GetType() + ".CanSell", 
                    TraceEventType.Warning);*/
                return false;
            }

            var tradingStatus = (CommonEnums.MARKET_STATUS)MarketServices.TradingStatus(stockInfo.Result.MarketID);

            if (stockInfo.Result.MarketID == (int)CommonEnums.MARKET_ID.HOSE 
                && tradingStatus != CommonEnums.MARKET_STATUS.READY
                && tradingStatus != CommonEnums.MARKET_STATUS.PRE_OPEN
                && tradingStatus != CommonEnums.MARKET_STATUS.OPEN
                && tradingStatus != CommonEnums.MARKET_STATUS.HAFT
                && tradingStatus != CommonEnums.MARKET_STATUS.OPEN_2
                && tradingStatus != CommonEnums.MARKET_STATUS.PRE_CLOSE)
            {
                /*LogHandler.Log(
                    String.Format("CanSell: {0} cannot sell {1} because tradingStatus of HOSE is {2}", accountNo, symbol, tradingStatus), 
                    GetType() + ".CanSell", 
                    TraceEventType.Warning);*/
                return false;
            }

            if (stockInfo.Result.MarketID == (int)CommonEnums.MARKET_ID.HNX
                && tradingStatus != CommonEnums.MARKET_STATUS.READY
                 && tradingStatus != CommonEnums.MARKET_STATUS.PRE_OPEN
                && tradingStatus != CommonEnums.MARKET_STATUS.OPEN
                && tradingStatus != CommonEnums.MARKET_STATUS.HAFT
                && tradingStatus != CommonEnums.MARKET_STATUS.OPEN_2
                 && tradingStatus != CommonEnums.MARKET_STATUS.PRE_CLOSE
                 && tradingStatus != CommonEnums.MARKET_STATUS.PRE_CLOSE2)
            {
                /*LogHandler.Log(
                    String.Format("CanSell: {0} cannot sell {1} because tradingStatus of HNX is {2}", accountNo, symbol, tradingStatus), 
                    GetType() + ".CanSell", 
                    TraceEventType.Warning);*/
                return false;
            }

            if (stockInfo.Result.MarketID == (int)CommonEnums.MARKET_ID.UPCoM
                && tradingStatus != CommonEnums.MARKET_STATUS.READY
                 && tradingStatus != CommonEnums.MARKET_STATUS.PRE_OPEN
                && tradingStatus != CommonEnums.MARKET_STATUS.OPEN
                && tradingStatus != CommonEnums.MARKET_STATUS.HAFT
                && tradingStatus != CommonEnums.MARKET_STATUS.OPEN_2
                 && tradingStatus != CommonEnums.MARKET_STATUS.PRE_CLOSE
                 && tradingStatus != CommonEnums.MARKET_STATUS.PRE_CLOSE2)
            {
                /*LogHandler.Log(
                    String.Format("CanSell: {0} cannot sell {1} because tradingStatus of Upcom is {2}", accountNo, symbol, tradingStatus),
                    GetType() + ".CanSell",
                    TraceEventType.Warning);*/
                return false;
            }

            if (stockInfo.Result.MarketID == (int)CommonEnums.MARKET_ID.HOSE
                && sellableShare < (decimal)CommonEnums.TRADE_RULE.VOL_UNIT_HOSE)
            {
                /*LogHandler.Log(
                    "CanSell: " + accountNo + " cannot sell " + symbol + " to HOSE because sellable share is " + sellableShare, 
                    GetType() + ".CanSell", 
                    TraceEventType.Warning);*/
                return false;
            }

            if (stockInfo.Result.MarketID == (int)CommonEnums.MARKET_ID.HNX 
                && sellableShare < (decimal)CommonEnums.TRADE_RULE.VOL_UNIT_HNX)
            {
                /*LogHandler.Log(
                    String.Format("CanSell: {0} cannot sell {1} to HNX because sellable share is {2}", accountNo, symbol, sellableShare), 
                    GetType() + ".CanSell", 
                    TraceEventType.Warning);*/
                return false;
            }

            CommonEnums.REJECT_REASON rejectReason = IsValidTradePermission(
                subCustAccount, CommonEnums.PERMISSION_TYPE.PORFOLIO);

            if (rejectReason != CommonEnums.REJECT_REASON.IS_VALID)
            {
                /*LogHandler.Log(
                    String.Format("CanSell: {0} cannot sell {1} because {0}{2}{3}", accountNo, symbol, rejectReason, sellableShare), 
                    GetType() + ".CanSell", 
                    TraceEventType.Warning);*/
                return false;
            }
            rejectReason = IsValidTransaction(accountNo, symbol, (char)CommonEnums.TRADE_SIDE.SELL, subCustAccounts);
            if (rejectReason != CommonEnums.REJECT_REASON.IS_VALID)
            {
                /*LogHandler.Log(
                    String.Format("CanSell: {0} cannot sell {1} because {0} bought {1} in transaction ", accountNo, symbol, rejectReason),
                    GetType() + ".CanSell",
                    TraceEventType.Warning);*/
                return false;
            }  
            return true;
        }

        /// <summary>
        /// Determines whether this instance can buy the specified symbol.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <param name="accountNo">The account no.</param>
        /// <param name="subCustAccount">Sub account information.</param>
        /// <param name="subCustAccounts">The sub cust accounts.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can buy the specified symbol; otherwise, <c>false</c>.
        /// </returns>
        public bool CanBuy(string symbol, string accountNo, SubCustAccount subCustAccount,List<string> subCustAccounts)
        {
            var stockInfo = Serializer.Deserialize<ResultObject<StockInfo>>(_rtService.GetStockInfo(symbol));
            if (stockInfo == null || stockInfo.Result == null)
            {
                /*LogHandler.Log(
                    String.Format("CanBuy: {0} not existed in collection", symbol),
                    GetType() + ".CanBuy",
                    TraceEventType.Warning);*/
                return false;
            }

            var tradingStatus = (CommonEnums.MARKET_STATUS)MarketServices.TradingStatus(stockInfo.Result.MarketID);

            if (stockInfo.Result.MarketID == (int)CommonEnums.MARKET_ID.HOSE
                && tradingStatus != CommonEnums.MARKET_STATUS.READY
                && tradingStatus != CommonEnums.MARKET_STATUS.PRE_OPEN
                && tradingStatus != CommonEnums.MARKET_STATUS.OPEN
                && tradingStatus != CommonEnums.MARKET_STATUS.HAFT
                && tradingStatus != CommonEnums.MARKET_STATUS.OPEN_2
                && tradingStatus != CommonEnums.MARKET_STATUS.PRE_CLOSE)
            {
                /*LogHandler.Log(
                    String.Format("CanBuy: {0} cannot buy {1} because tradingStatus of HOSE is {2}", accountNo, symbol, tradingStatus),
                    GetType() + ".CanBuy",
                    TraceEventType.Warning);*/
                return false;
            }

            if (stockInfo.Result.MarketID == (int)CommonEnums.MARKET_ID.HNX
                && tradingStatus != CommonEnums.MARKET_STATUS.READY
                 && tradingStatus != CommonEnums.MARKET_STATUS.PRE_OPEN
                && tradingStatus != CommonEnums.MARKET_STATUS.OPEN
                && tradingStatus != CommonEnums.MARKET_STATUS.HAFT
                && tradingStatus != CommonEnums.MARKET_STATUS.OPEN_2
                 && tradingStatus != CommonEnums.MARKET_STATUS.PRE_CLOSE
                 && tradingStatus != CommonEnums.MARKET_STATUS.PRE_CLOSE2)
            {
                /*LogHandler.Log(
                    String.Format("CanBuy: {0} cannot buy {1} because tradingStatus of HNX is {2}", accountNo, symbol, tradingStatus),
                    GetType() + ".CanBuy",
                    TraceEventType.Warning);*/
                return false;
            }

            if (stockInfo.Result.MarketID == (int)CommonEnums.MARKET_ID.UPCoM
                && tradingStatus != CommonEnums.MARKET_STATUS.READY
                 && tradingStatus != CommonEnums.MARKET_STATUS.PRE_OPEN
                && tradingStatus != CommonEnums.MARKET_STATUS.OPEN
                && tradingStatus != CommonEnums.MARKET_STATUS.HAFT
                && tradingStatus != CommonEnums.MARKET_STATUS.OPEN_2
                 && tradingStatus != CommonEnums.MARKET_STATUS.PRE_CLOSE
                 && tradingStatus != CommonEnums.MARKET_STATUS.PRE_CLOSE2)
            {
                /*LogHandler.Log(
                    String.Format("CanBuy: {0} cannot buy {1} because tradingStatus of Upcom is {2}", accountNo, symbol, tradingStatus),
                    GetType() + ".CanBuy",
                    TraceEventType.Warning);*/
                return false;
            }           

            CommonEnums.REJECT_REASON rejectReason = IsValidTradePermission(
                subCustAccount, CommonEnums.PERMISSION_TYPE.PORFOLIO);

            if (rejectReason != CommonEnums.REJECT_REASON.IS_VALID)
            {
                /*LogHandler.Log(
                    String.Format("CanBuy: {0} cannot buy {1} because {0}{2}", accountNo, symbol, rejectReason),
                    GetType() + ".CanBuy",
                    TraceEventType.Warning);*/
                return false;
            }   
            rejectReason = IsValidTransaction(accountNo, symbol,(char)CommonEnums.TRADE_SIDE.BUY ,subCustAccounts);
            if (rejectReason != CommonEnums.REJECT_REASON.IS_VALID)
            {
                /*LogHandler.Log(
                    String.Format("CanBuy: {0} cannot buy {1} because {0} sold {1} in transaction ", accountNo, symbol, rejectReason),
                    GetType() + ".CanBuy",
                    TraceEventType.Warning);*/
                return false;
            }     
            return true;
        }

        /// <summary>
        /// Determines whether this instance can buy.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if all market open, others wise <c>false</c>.
        /// </returns>
        public bool CanBuy()
        {
            var tradingStatusHOSE = (CommonEnums.MARKET_STATUS)MarketServices.TradingStatus((int)CommonEnums.MARKET_ID.HOSE);
            var tradingStatusHNX=(CommonEnums.MARKET_STATUS)MarketServices.TradingStatus((int)CommonEnums.MARKET_ID.HNX);
            var tradingStatusUPCOM = (CommonEnums.MARKET_STATUS)MarketServices.TradingStatus((int)CommonEnums.MARKET_ID.UPCoM);
            if ((tradingStatusHOSE == CommonEnums.MARKET_STATUS.CLOSE || tradingStatusHOSE== CommonEnums.MARKET_STATUS.UNVAILABLE) &&
                 (tradingStatusHNX == CommonEnums.MARKET_STATUS.CLOSE || tradingStatusHNX == CommonEnums.MARKET_STATUS.UNVAILABLE) &&
                tradingStatusUPCOM == CommonEnums.MARKET_STATUS.CLOSE || tradingStatusUPCOM == CommonEnums.MARKET_STATUS.UNVAILABLE)
            {                
                return false;
            }
            return true;
        }
        /// <summary>
        /// Determines whether [is valid new order] [the specified market id].
        /// </summary>
        /// <param name="marketId">The market id.</param>
        /// <param name="accountNo">The account no.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="side">The side.</param>
        /// <param name="volume">The volume.</param>
        /// <param name="price">The price.</param>
        /// <param name="conPrice">The con price.</param>
        /// <param name="accountType">Type of the account.</param>
        /// <param name="customerType">Type of the customer.</param>
        /// <param name="subCustAccount">The sub cust account.</param>
        /// <param name="isMargin">true if this account is margin account; otherwise false</param>
        /// <param name="strEffDate">Effect date</param>
        /// <returns>
        /// 	<para>
        /// Result of validating order.
        /// </para>
        /// 	<para>REJECT_REASON=NOT_ADVANCE_TIME: This time is not advance time.</para>
        /// 	<para>REJECT_REASON=ERROR_PRICE_NOT_MULTIPLE_100_FOR_HOSE: Price is incorrect for HOSE market.</para>
        /// 	<para>REJECT_REASON=ERROR_PRICE_NOT_MULTIPLE_500_FOR_HOSE: Price is incorrect for HOSE market.</para>
        /// 	<para>REJECT_REASON=ERROR_PRICE_NOT_MULTIPLE_1000_FOR_HOSE: Price is incorrect for HOSE market.</para>
        /// 	<para>REJECT_REASON=ERROR_HNX_NOT_USE_ATO_ATC: Cannot put ATO, ATC order in HNX market.</para>
        /// 	<para>REJECT_REASON=ERROR_PRICE_NOT_MULTIPLE_100_FOR_HNX: Price is incorrect for HNX market.</para>
        /// 	<para>REJECT_REASON=ERROR_UPCOM_NOT_USE_ATO_ATC: Cannot put ATO, ATC order in UPCOM market.</para>
        /// 	<para>REJECT_REASON=ERROR_PRICE_NOT_MULTIPLE_100_FOR_UPCOM: Price is incorrect for UPCOM market.</para>
        /// 	<para>REJECT_REASON=INCORRECT_SIDE: The side is not buy or sell side.</para>
        /// 	<para>REJECT_REASON=INCORRECT_VOL: The volume is incorrect.</para>
        /// 	<para>REJECT_REASON=OVER_MAX_VOL: The volume is over max allowed volume.</para>
        /// 	<para>REJECT_REASON=INCORRECT_STOCK: Stock is incorrect.</para>
        /// 	<para>REJECT_REASON=STOCK_IS_HALT: Stock is halt.</para>
        /// 	<para>REJECT_REASON=ERROR_LOCK_ACCOUNT: Account is locked.</para>
        /// 	<para>REJECT_REASON=ERROR_ACCOUNT_NOT_CONDITION_ORDER: Account is not allowed to put advance order.</para>
        /// 	<para>REJECT_REASON=NOT_ENOUGH_CASH: Customer has not enough money.</para>
        /// 	<para>REJECT_REASON=NOT_ENOUGH_STOCK: Customer has not enough stocks.</para>
        /// 	<para>REJECT_REASON=IS_VALID: This is a valid order.</para>
        /// </returns>
        public CommonEnums.REJECT_REASON IsValidAdvanceOrder(int marketId, string accountNo, string symbol,
            char side, int volume, decimal price, char conPrice, int accountType, int customerType,
            SubCustAccount subCustAccount, bool isMargin, string strEffDate)
        {
            
            /* Don't validate market in advance orders
             * CommonEnums.REJECT_REASON retCode = IsValidMarket(marketId, conPrice);

            if (retCode != CommonEnums.REJECT_REASON.IS_VALID)
            {
                return retCode;
            }*/
            var retCode = IsValidAdvanceTime();
            if (retCode != CommonEnums.REJECT_REASON.IS_VALID)
            {
                return retCode;
            }

            retCode = ETradeCommon.Utils.IsValidStepPrice(marketId, price, conPrice);

            if (retCode != CommonEnums.REJECT_REASON.IS_VALID)
            {
                return retCode;
            }

            // Validate side
            if (side != (char)CommonEnums.TRADE_SIDE.SELL && side != (char)CommonEnums.TRADE_SIDE.BUY)
            {
                return CommonEnums.REJECT_REASON.INCORRECT_SIDE;
            }

            retCode = IsValidVolUnit(marketId, volume);

            if (retCode != CommonEnums.REJECT_REASON.IS_VALID)
            {
                return retCode;
            }

            retCode = IsValidStock(symbol, true, side, conPrice);

            if (retCode != CommonEnums.REJECT_REASON.IS_VALID)
            {
                return retCode;
            }

            /* Don't check over ceiling or under floor
             * retCode = IsValidPrice(marketId, price, conPrice, symbol);

            if (retCode != CommonEnums.REJECT_REASON.IS_VALID)
            {
                return retCode;
            }*/

            /* Don't check this when putting advance orders
             * retCode = IsValidTransaction(accountNo, symbol, side, subCustAccounts);

            if (retCode != CommonEnums.REJECT_REASON.IS_VALID)
            {
                return retCode;
            }*/

            retCode = IsValidAdvanceOrderPermission(subCustAccount);

            if (retCode != CommonEnums.REJECT_REASON.IS_VALID)
            {
                return retCode;
            }

            if (!(isMargin && (side == (char)CommonEnums.TRADE_SIDE.BUY)))
            {
                retCode = IsValidBalance(accountNo, side, symbol, marketId, conPrice, volume, price, -1,strEffDate, true);

                if (retCode != CommonEnums.REJECT_REASON.IS_VALID)
                {
                    return retCode;
                }
            }

            retCode = IsValidStockBalance(side, symbol, accountNo, -1,strEffDate, true,volume);
            if (retCode != CommonEnums.REJECT_REASON.IS_VALID)
            {
                return retCode;
            }

            /* Don't check this in advance orders
             * retCode = IsValidAccount(accountNo, side, symbol, volume, customerType);

            if (retCode != CommonEnums.REJECT_REASON.IS_VALID)
            {
                return retCode;
            }*/

            return CommonEnums.REJECT_REASON.IS_VALID;
        }

        /// <summary>
        /// Check if the time of putting advance order is in allowed advance time or not
        /// </summary>
        /// <returns>
        /// <para>Result of validating advance time.</para>
        /// <para>REJECT_REASON=NOT_ADVANCE_TIME: This time is not advance time.</para>
        /// <para>REJECT_REASON=IS_VALID: This is a valid order.</para>
        /// </returns>
        private static CommonEnums.REJECT_REASON IsValidAdvanceTime()
        {
            var currentHour = DateTime.Now.Hour;
            if (((0 <= currentHour) && (currentHour <= EndTimeForNextDayAdvance)) || ((BeginTimeForNextDayAdvance <= currentHour) && (currentHour <= 23)))
            {
                return CommonEnums.REJECT_REASON.IS_VALID;
            }
            return CommonEnums.REJECT_REASON.NOT_ADVANCE_TIME;
        }

        /// <summary>
        /// Validate conditions buy of margin account
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <param name="price">The price.</param>
        /// <param name="volume">The volume.</param>
        /// <param name="strEffDate">The STR eff date.</param>
        /// <param name="isConditionOrder">if set to <c>true</c> [is condition order].</param>
        /// <returns>
        /// 	<para>Result checking if this is a valid buy for margin account.</para>
        /// 	<para>REJECT_REASON=ERROR_MARGIN_ACCOUNT_CANNOT_BUY_THAT_SYMBOL: Margin account cannot buy this symbol.</para>
        /// 	<para>REJECT_REASON=ERROR_OVER_LIMIT_LOAN_PER_CUSTOMER: The value of price after fee overs the limit of loan per customer.</para>
        /// 	<para>REJECT_REASON=ERROR_OVER_LIMIT_LOAN_PER_SECSYMBOL: The value of price after fee over the limit of loan per symbol.</para>
        /// 	<para>REJECT_REASON=ERROR_OVER_LIMIT_COMPANY_CAPITAL: The value of price after fee over the limit of the company capital.</para>
        /// 	<para>REJECT_REASON=ERROR_OVER_LIMIT_MAX_BUY: The value of price after fee over max buy of that account.</para>
        /// 	<para>REJECT_REASON=ERROR_OVER_LIMIT_MAX_BUY_OF_SECSYMBOL: The value of price after fee over max buy of symbol.</para>
        /// 	<para>REJECT_REASON=IS_VALID: This is a valid order.</para>
        /// </returns>
        public CommonEnums.REJECT_REASON IsValidBuyMarginAccount(string accountNo, string secSymbol, decimal price, int volume, string strEffDate, bool isConditionOrder)
        {           
            //margin account can only buy the symbols in the MaginSec view in BA_VIEW                        
            MaginSecInfo maginSecInfo = MarginServices.GetMaginSecInfo(DateTime.Now.ToString("yyyyMMdd"), secSymbol);
            if (maginSecInfo == null)
            {
                return  CommonEnums.REJECT_REASON.ERROR_MARGIN_ACCOUNT_CANNOT_BUY_THAT_SYMBOL;
            }

            CapFundInfo capFunInfo = MarginServices.GetCapFundInfo();
            if (capFunInfo == null)
            {
                return CommonEnums.REJECT_REASON.NOTHING;
            }

            //old code
            var feeService = new FeeService();
            decimal valueAfterFee = (price * volume) + feeService.CalculateFee((price * volume), (int)CommonEnums.FEE_TYPE.FEE_TRADE);
            valueAfterFee *= Constants.MONEY_UNIT;
            //the value of price after fee overs the limit of loan per customer
            if (valueAfterFee > ((capFunInfo.STOCKLOANLIMIT_Val / 100) * capFunInfo.CAPITALMARGFUND_Val))
            {
                return  CommonEnums.REJECT_REASON.ERROR_OVER_LIMIT_LOAN_PER_CUSTOMER;
            }

            //the value of price after fee over the limit of loan per a secsymbol
            if (valueAfterFee > ((capFunInfo.CUSLOANLIMIT_Val / 100) * capFunInfo.CAPITALMARGFUND_Val))
            {
                return CommonEnums.REJECT_REASON.ERROR_OVER_LIMIT_LOAN_PER_SECSYMBOL;
            }

            //The value of price after fee over the limit of the company capital
            if (valueAfterFee > (capFunInfo.CAPITALMARGFUND_Val * capFunInfo.TOTALLOANLIMIT_Val))
            {
                return CommonEnums.REJECT_REASON.ERROR_OVER_LIMIT_COMPANY_CAPITAL;
            }

            //the value of price after fee over max buy of that account                   
            MarginRatioInfo marginRatioInfo = MarginServices.GetMarginRatio(accountNo);
            
            if (marginRatioInfo == null)
            {
                return CommonEnums.REJECT_REASON.NOTHING;
            }
            var totalMoneyConditionOrder = GetTotalConditionOrderMoney((char)CommonEnums.TRADE_SIDE.BUY, accountNo, -1);
            if (valueAfterFee > (marginRatioInfo.PP - totalMoneyConditionOrder))
            {
                if (isConditionOrder)
                {
                    if (!IsValidLimitQuantityAdvanceOrder((char)CommonEnums.TRADE_SIDE.BUY, accountNo, string.Empty))
                    {
                        return CommonEnums.REJECT_REASON.ERROR_OVER_LIMIT_MAX_BUY;
                    }
                }
                else
                    return CommonEnums.REJECT_REASON.ERROR_OVER_LIMIT_MAX_BUY;
            }
            //the value of price after fee over max buy of symbol
            if (valueAfterFee > (marginRatioInfo.EE / maginSecInfo.IM))
            {
                return CommonEnums.REJECT_REASON.ERROR_OVER_LIMIT_MAX_BUY_OF_SECSYMBOL;
            }            
            return CommonEnums.REJECT_REASON.IS_VALID;
        }

        /// <summary>
        /// Determines whether [is valid limit quantity advance order] [the specified side].
        /// </summary>
        /// <param name="side">The side.</param>
        /// <param name="subCustAccountId">The sub cust account id.</param>
        /// <param name="symbol">The symbol.</param>
        /// <returns>
        /// 	<c>true</c> if [is valid limit quantity advance order] [the specified side]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValidLimitQuantityAdvanceOrder(char side, string subCustAccountId, string symbol)
        {
            var list = ConditionOrderService.GetListUnfinishedConditionOrders(side, subCustAccountId, symbol);
            if (list != null)
                return list.Count < AppConfig.LimitQuantityAdvanceOrder;

            return true;   
        }
    }
}