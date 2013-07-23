// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CashAdvanceServices.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the CashAdvanceServices type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Text;
using AccountManager.Entities;

namespace ETradeCore.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using ETradeCommon;
    using ETradeCommon.Enums;

    using ETradeCore.DataAccess;
    using ETradeCore.DataAccess.SqlClient;
    using ETradeCore.Entities;

    using ETradeFinance.Entities;
    using ETradeFinance.Services;

    using RTDataServices.Entities;

    using CashAdvance = ETradeCore.Entities.CashAdvance;

    public class CashAdvanceServices
    {
        /// <summary>
        /// informixProvider
        /// </summary>  
        private readonly ISbaCoreProvider informixProvider = new SqlInformixProvider();

        /// <summary>
        /// db2Provider
        /// </summary>
        private readonly IFisCoreProvider db2Provider = new SqlDb2Provider();

        /// <summary>
        /// cashAdvanceService
        /// </summary>
        private readonly CashAdvanceService cashAdvanceService = new CashAdvanceService();

        private readonly CashAdvanceHistoryService cashAdvanceHistoryService = new CashAdvanceHistoryService();

        private readonly FeeService feeServices = new FeeService();

        /// <summary>
        /// Gets the advance history from core.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="fromSellDate">From sell date.</param>
        /// <param name="toSellDate">To sell date.</param>
        /// <param name="contractNo">The contract no.</param>
        /// <returns></returns>
        public List<CashAdvance> GetAdvanceHistoryFromCore(
            string accountNo,
            string fromDate,
            string toDate,
            string fromSellDate,
            string toSellDate, 
            string contractNo)
        {
            return this.informixProvider.GetAdvanceHistory(accountNo, fromDate, toDate, fromSellDate, toSellDate, contractNo);
        }

        /// <summary>
        /// Gets the advance history from ots db.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="fromAdvanceDate">From advance date.</param>
        /// <param name="toAdvanceDate">To advance date.</param>
        /// <param name="fromSellDate">From sell date.</param>
        /// <param name="toSellDate">To sell date.</param>
        /// <param name="advanceStatus">The advance status.</param>
        /// <param name="contractNo">The contract no.</param>
        /// <returns></returns>
        public List<CashAdvance> GetAdvanceHistoryFromOtsDb(
            string accountNo,
            string fromAdvanceDate,
            string toAdvanceDate,
            string fromSellDate,
            string toSellDate,
            int advanceStatus,
            string contractNo)
        {
            int totalRecords;
            string whereClause = BuildCashAdvanceCondition(accountNo, fromAdvanceDate, toAdvanceDate, fromSellDate,
                                                           toSellDate, advanceStatus, contractNo);

            TList<ETradeFinance.Entities.CashAdvance> cashAdvances = this.cashAdvanceService.GetPaged(
                whereClause, " ID DESC ", 0, int.MaxValue, out totalRecords);
            
            var returnValue = new List<CashAdvance>();

            returnValue.AddRange(this.Transform(cashAdvances));

            return returnValue;
        }


        private string BuildCashAdvanceCondition(
            string accountNo,
            string fromAdvanceDate,
            string toAdvanceDate,
            string fromSellDate,
            string toSellDate,
            int advanceStatus,
            string contractNo)
        {
            try
            {
                var condition = new StringBuilder(500);
                if (!string.IsNullOrEmpty(accountNo))
                {
                    condition.Append("SubAccountID = '" + accountNo + "' AND ");                    
                }

                condition.Append(
                    "((CONVERT(varchar(8), SellDueDate, 112) BETWEEN '" + fromSellDate + "' AND '" + toSellDate + "') OR (" +
                    (fromSellDate == string.Empty && toSellDate == string.Empty ? "1=1" : "1=2") + "))");

                condition.Append(" AND ");

                condition.Append(
                    "((CONVERT(varchar(8), AdvanceDate, 112) BETWEEN '" + fromAdvanceDate + "' AND '" + toAdvanceDate + "') OR (" +
                    (fromAdvanceDate == string.Empty && toAdvanceDate == string.Empty ? "1=1" : "1=2") + "))");


                if(advanceStatus>-1)
                {
                    condition.Append(" AND ");

                    condition.Append(
                        "(Status = '" + advanceStatus + "' OR  " +
                        (advanceStatus == 0 ? "1=1" : "1=2") + ")");
                }
                

                condition.Append(" AND ");

                condition.Append(
                    "(contractNo = '" + contractNo + "' OR  " +
                    (contractNo == string.Empty || contractNo == "all" ? "1=1" : "1=2") + ")");

                return condition.ToString();
            }
            catch (Exception exception)
            {
                LogHandler.Log(
                    "BuildCashAdvanceCondition: exception = " + exception + ", accountNo = " + accountNo +
                    ", fromDate = " + fromAdvanceDate + ", toDate = " + toAdvanceDate,
                    this.GetType() + ".BuildCashAdvanceCondition",
                    TraceEventType.Error);
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the advance info.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="newestWorkingDatesInfo">The newest working dates info.</param>
        /// <param name="dicAdvanceTimes"></param>
        /// <param name="dicHolidays"></param>
        /// <param name="dicWorkingDays"></param>
        /// <returns></returns>
        public List<AdvanceInfo> GetAdvanceInfo(string accountNo, NewestWorkingDatesInfo newestWorkingDatesInfo, 
            Dictionary<int, AdvanceTime> dicAdvanceTimes, Dictionary<string, DateTime> dicHolidays, 
            Dictionary<int, bool> dicWorkingDays)
        {
            // Because RTStockData save  T3 for intra-day, and T for oldest day

            var advanceInfos = new List<AdvanceInfo>();

            List<string> dayTs = new List<string>();
            dayTs.Add(newestWorkingDatesInfo.T1.ToString("yyyyMMdd"));
            dayTs.Add(newestWorkingDatesInfo.T2.ToString("yyyyMMdd"));

            foreach (var dayT in dayTs)
            {
                var advanceInfoHist = this.GetAdvanceOrderHis(accountNo, dayT, dicAdvanceTimes, dicHolidays, dicWorkingDays);
                if (advanceInfoHist != null)
                {
                    advanceInfos.Add(advanceInfoHist);
                }
            }

            
            var orderInfos = this.db2Provider.GetOrderIntraDay(accountNo, 0);

            var advanceInfoInTraDay = this.GetAdvanceInfo4DealIntraDay(orderInfos, accountNo,
                                                                       newestWorkingDatesInfo.T3.ToString("yyyyMMdd"),
                                                                       dicAdvanceTimes, dicHolidays, dicWorkingDays);

            if (advanceInfoInTraDay != null)
            {
                advanceInfos.Add(advanceInfoInTraDay);    
            }

            return advanceInfos;
        }

        /// <summary>
        /// Gets the advance info for a date.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="dayT">Day T.</param>
        /// <param name="dicAdvanceTimes">List of advance time.</param>
        /// <param name="dicHolidays">List of holidays.</param>
        /// <param name="dicWorkingDays">List of working days.</param>
        /// <returns>
        /// <para>Maximum cash advance.</para>
        /// </returns>
        public decimal GetMaxAdvance(string accountNo, string dayT, Dictionary<int, AdvanceTime> dicAdvanceTimes,
            Dictionary<string, DateTime> dicHolidays, Dictionary<int, bool> dicWorkingDays)
        {
            // Because RTStockData save  T3 for intra-day, and T for oldest day

            decimal maxAdvance = 0;

            var advanceInfoHist = this.GetAdvanceOrderHis(accountNo, dayT, dicAdvanceTimes, dicHolidays, dicWorkingDays);
            if (advanceInfoHist != null)
            {
                maxAdvance = advanceInfoHist.MaxCanAdvance;
            }

            else
            {
                var orderInfos = this.db2Provider.GetOrderIntraDay(accountNo, 0);

                var advanceInfoInTraDay = this.GetAdvanceInfo4DealIntraDay(orderInfos, accountNo, dayT, dicAdvanceTimes,
                                                                           dicHolidays, dicWorkingDays);

                if (advanceInfoInTraDay != null)
                {
                    maxAdvance = advanceInfoInTraDay.MaxCanAdvance;
                }
            }

            return maxAdvance;
        }

        private AdvanceInfo GetAdvanceOrderHis(string accountNo, string dayT, Dictionary<int, AdvanceTime> dicAdvanceTimes,
            Dictionary<string, DateTime> dicHolidays, Dictionary<int, bool> dicWorkingDays)
        {
            var orderHistories = this.db2Provider.GetOrderHistory(
               accountNo,
               dayT,
               dayT,
               string.Empty,
               (int)ETradeCommon.Enums.CommonEnums.FILTER_ORDER_STATUS.MATCHED,
               0,
               int.MaxValue);

            var advanceInfoHist = this.GetAdvanceInfo4DealHistory(orderHistories, accountNo, dayT, dicHolidays, dicWorkingDays);

            if (advanceInfoHist != null)
            {
                
                ETradeCommon.Enums.CommonEnums.RET_CODE ret = IsValidAdvance(advanceInfoHist.MaxCanAdvance, advanceInfoHist.MaxCanAdvance,
                                                            Utils.StringToDateTime(advanceInfoHist.DueDate), advanceInfoHist.AdvFeeRatio,
                                                            advanceInfoHist.AdvFeeRatio, dicAdvanceTimes, dicHolidays, dicWorkingDays);
                advanceInfoHist.CanAdvance = (ret == CommonEnums.RET_CODE.SUCCESS) ? true : false;
            }

            return advanceInfoHist;
        }

        /// <summary>
        /// Determines whether [is matched order] [the specified core ord status].
        /// </summary>
        /// <param name="coreOrdStatus">The core ord status.</param>
        /// <returns>
        /// 	<c>true</c> if [is matched order] [the specified core ord status]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsMatchedOrder(Decimal matchVol)
        {
            /*
            if (coreOrdStatus == "M"
                            || coreOrdStatus == "MD"
                            || coreOrdStatus == "MA"
                            || coreOrdStatus == "m"
                            || coreOrdStatus == "PO"
                            || coreOrdStatus == "PX")
            {
                return true;
            }
             */


            return (matchVol > 0);
        }

        /// <summary>
        /// Gets the advance info for deal history.
        /// </summary>
        /// <param name="orderHistories">The order histories.</param>
        /// <param name="accountNo">The account no.</param>
        /// <param name="orderDueDate"></param>
        /// <param name="dicHolidays">Dictionary of holidays</param>
        /// <param name="dicWorkingDays">Dictionary of workding days</param>
        /// <returns></returns>
        private AdvanceInfo GetAdvanceInfo4DealHistory(List<OrderHistory> orderHistories, string accountNo, string orderDueDate,
            Dictionary<string, DateTime> dicHolidays, Dictionary<int, bool> dicWorkingDays)
        {
            try
            {
                decimal sellAmt = 0;

                if (orderHistories == null)
                {
                    return null;
                }

                foreach (var orderHistory in orderHistories)
                {
                    if (!IsMatchedOrder(orderHistory.MatchVolume) ||
                        (orderHistory.Side[0] != (char) CommonEnums.TRADE_SIDE.SELL))
                    {

                        continue;
                    }

                    var dealHistories = this.db2Provider.GetDealHistory(orderHistory.OrderNo, orderHistory.OrderDate, 0);
                    if (dealHistories != null && dealHistories.Count > 0)
                    {
                            decimal sumDeal =
                                dealHistories.Sum(
                                    dealHistory => dealHistory.DealPrice * Constants.MONEY_UNIT * dealHistory.DealVolume);


                            Fee feeTrade = feeServices.GetTradeFee(CommonEnums.FEE_TYPE.FEE_TRADE, sumDeal);
                            if (feeTrade != null)
                            {

                                if(Constants.PERCENT_UNIT!=0)
                                {
                                    sellAmt +=
                                        Math.Round(
                                            (decimal)(sumDeal * (1 - feeTrade.FeeRatio / Constants.PERCENT_UNIT - feeTrade.Vat / Constants.PERCENT_UNIT)), 
                                            0);                                              
                                }
                            }
                            else
                            {
                                LogHandler.Log("Get the trade fee/vat null",
                                this.GetType() + ".GetAdvanceInfo4DealHistory()",
                                TraceEventType.Warning);

                                continue;
                            }
                    }
                }                
                if (orderDueDate != null && sellAmt > 0)
                {
                    var retAdvanceInfo = CaculateAdvance(orderDueDate, sellAmt, accountNo, dicHolidays, dicWorkingDays);

                    return retAdvanceInfo;
                }

                return null;
                
            }
            catch (Exception e)
            {
                LogHandler.Log(
                    "GetAdvanceInfo4DealHistory: exception = " + e + ", accountNo = " + accountNo,
                    this.GetType() + ".GetAdvanceInfo4DealHistory()",
                    TraceEventType.Error);
                return null;
            }
        }



        /// <summary>
        /// Gets the advance info for deal intra day.
        /// </summary>
        /// <param name="orderInfos">The order infos.</param>
        /// <param name="accountNo">The account no.</param>
        /// <returns></returns>
        private AdvanceInfo GetAdvanceInfo4DealIntraDay(List<OrderInfo> orderInfos, string accountNo, string orderDueDate,
            Dictionary<int, AdvanceTime> dicAdvanceTimes, Dictionary<string, DateTime> dicHolidays, Dictionary<int, bool> dicWorkingDays)
        {
            try
            {

                decimal sellAmt = 0;

                if (orderInfos == null)
                {
                    return null;
                }

                foreach (var orderInfo in orderInfos)
                {
                    if (!IsMatchedOrder(orderInfo.MatchVolume) ||
                        (orderInfo.Side[0] != (char) CommonEnums.TRADE_SIDE.SELL))
                    {

                        continue;
                    }



                    var dealIntraDays = this.db2Provider.GetDealIntraDay(orderInfo.OrderNo, 0);
                    if (dealIntraDays == null || dealIntraDays.Count == 0)
                    {
                        continue;
                    }


                    decimal sumDeal =
                        dealIntraDays.Sum(
                            dealHistory =>
                            dealHistory.DealPrice*Constants.MONEY_UNIT*dealHistory.DealVolume);
                   
                    Fee feeTrade = feeServices.GetTradeFee(CommonEnums.FEE_TYPE.FEE_TRADE, sumDeal);
                    if (feeTrade != null)
                    {

                        if (Constants.PERCENT_UNIT!=0)
                        {
                            sellAmt +=
                            Math.Round(
                                (decimal)(sumDeal * (1 - feeTrade.FeeRatio / Constants.PERCENT_UNIT - feeTrade.Vat / Constants.PERCENT_UNIT)),
                                0);
                        }
                        else
                        {
                            sellAmt += 0;
                        }
                    }
                    else
                    {
                        LogHandler.Log("Get the trade fee/vat null",
                                       this.GetType() + ".GetAdvanceInfo4DealIntraDay()",
                                       TraceEventType.Warning);
                        continue;
                    }
                }

                if (orderDueDate != null && sellAmt > 0)
                {
                    var retAdvanceInfo = CaculateAdvance(orderDueDate, sellAmt, accountNo, dicHolidays, dicWorkingDays);

                    if (retAdvanceInfo != null)
                    {

                        CommonEnums.RET_CODE ret = IsValidAdvance(retAdvanceInfo.MaxCanAdvance, retAdvanceInfo.MaxCanAdvance,
                                                                    Utils.StringToDateTime(retAdvanceInfo.DueDate), retAdvanceInfo.AdvFeeRatio,
                                                                    retAdvanceInfo.AdvFeeRatio, dicAdvanceTimes, dicHolidays, dicWorkingDays);

                        //Not show the advance that is in due date
                        if (ret == CommonEnums.RET_CODE.ERROR_CANNOT_ADVANCE_IN_DUE_DATE)
                        {
                            return null;
                        }

                        retAdvanceInfo.CanAdvance = (ret == CommonEnums.RET_CODE.SUCCESS) ? true : false;
                    }


                    return retAdvanceInfo;
                }

                return null;
            }

            catch (Exception e)
            {
                LogHandler.Log(
                    "GetAdvanceInfo4DealIntraDay: exception = " + e + ", accountNo = " + accountNo,
                    this.GetType() + ".GetAdvanceInfo4DealIntraDay()",
                    TraceEventType.Error);

                return null;
            }
        }

        private AdvanceInfo CaculateAdvance(string orderDueDate, decimal sellAmt, string accountNo,
            Dictionary<string, DateTime> dicHolidays, Dictionary<int, bool> dicWorkingDays)
        {
            DateTime cashDueDate = this.CalculateDueDate(orderDueDate, dicHolidays, dicWorkingDays);
            int advanceDays = (cashDueDate.Date - DateTime.Now.Date).Days; //Confirmed with TCSC.

            var advanceInfo = new AdvanceInfo();


            advanceInfo.AdvanceFinished = this.GetAdvanceFinishedNPending(orderDueDate, accountNo);
            advanceInfo.TradeDate = orderDueDate;
            advanceInfo.DueDate = cashDueDate.ToString("yyyyMMdd");
            advanceInfo.SellAmt = sellAmt;

            Fee feeCashAdv = feeServices.GetTradeFee(CommonEnums.FEE_TYPE.FEE_CASH_ADVANCE, sellAmt - advanceInfo.AdvanceFinished);
            if (feeCashAdv != null)
            {
                if (Constants.PERCENT_UNIT!=0)
                    advanceInfo.AdvFeeRatio = feeCashAdv.FeeRatio / Constants.PERCENT_UNIT;

                advanceInfo.AdvanceDay = advanceDays;
                advanceInfo.AdvanceFee = CalculateAdvanceFee(sellAmt - advanceInfo.AdvanceFinished, advanceDays);
                if(advanceInfo.AdvanceFee<=feeCashAdv.MinFee)
                {
                    advanceInfo.MaxCanAdvance = sellAmt - advanceInfo.AdvanceFinished - advanceInfo.AdvanceFee;
                }
                else
                {
                    if ((1 + advanceInfo.AdvFeeRatio * advanceDays) != 0)
                        advanceInfo.MaxCanAdvance = Math.Round((sellAmt - advanceInfo.AdvanceFinished) / (1 + advanceInfo.AdvFeeRatio * advanceDays));
                    advanceInfo.AdvanceFee = CalculateAdvanceFee(advanceInfo.MaxCanAdvance, advanceDays);    
                }                
            }
            else
            {
                LogHandler.Log("Get the advance fee/vat null",
                               GetType() + ".GetAdvanceInfo4DealIntraDay()",
                               TraceEventType.Warning);
                return null;
            }

            if (advanceInfo.MaxCanAdvance > 0)
            {
                return advanceInfo;
            }

            return null;
        }

        /// <summary>
        /// Calculate duedate
        /// </summary>
        /// <param name="tradeDate">the day sell stock</param>
        /// <param name="dicHolidays">Dictionary of holidays</param>
        /// <param name="dicWorkingDays">Dictionary of working days</param>
        /// <returns>DateTime value</returns>
        public DateTime CalculateDueDate(string tradeDate, Dictionary<string, DateTime> dicHolidays, 
            Dictionary<int, bool> dicWorkingDays)
        {
            DateTime retVal;
            DateTime _tradeDate;

            try
            {
                _tradeDate = new DateTime(
                    int.Parse(tradeDate.Substring(0, 4)),
                    int.Parse(tradeDate.Substring(4, 2)),
                    int.Parse(tradeDate.Substring(6, 2)));
            }
            catch
            {
                _tradeDate = new DateTime();
                LogHandler.Log(
                    "CalculateDueDate: tradeDate was incorrect format (yyyyMMdd), tradeDate = " + tradeDate +
                    ". So we cannot calculate AdvanceFee",
                    this.GetType() + ".CalculateDueDate()",
                    TraceEventType.Warning);
            }

            retVal = _tradeDate;
            int numberOfDueDate = 1;
            while (numberOfDueDate <= Constants.NUMBER_DUE_DATES)
            {
                retVal = retVal.AddDays(1);

                // TODO: Get holidays, workingdays for calculate duedate
                if (dicHolidays != null && !dicHolidays.ContainsKey(retVal.ToString("yyyyMMdd")))
                {
                    // If retVal is not a holiday
                    int dayOffWeek = Utils.ConvertDayOfWeek(retVal.DayOfWeek);
                    if (dicWorkingDays != null && dicWorkingDays.ContainsKey(dayOffWeek))
                    {
                        if (dicWorkingDays[dayOffWeek] == true)
                        {
                            // If retVal is a working day
                            // retVal = retVal.AddDays(1);
                            numberOfDueDate += 1;
                        }
                        else if (dicWorkingDays[dayOffWeek] == false)
                        {
                            ////If retVal is a day off
                            continue;
                        }
                    }
                    else
                    {
                        ////If retVal is a day off
                        continue;
                    }
                }
                else
                {
                    ////If retVal is a holiday, we should add more day for duedate
                    continue;
                }
            }

            return retVal;
        }

        

        /// <summary>
        /// Gets the advance finished N pending.
        /// </summary>
        /// <param name="sellduedate">The sell due date.</param>
        /// <param name="accountNo">The account no.</param>
        /// <returns></returns>
        private decimal GetAdvanceFinishedNPending(string sellduedate, string accountNo)
        {
            try
            {
                int count = 0;

                // Get advance finished direct from SBA 
                // because investor may be advance in TCSC without via WebTrade
                List<CashAdvance> advanceFinished = this.informixProvider.GetAdvanceHistory(
                    accountNo, string.Empty, string.Empty, sellduedate, sellduedate, string.Empty);

                // Get advance pending in Advance table
                var pendingClause =
                        string.Format(
                            " CONVERT(varchar(8), SellDueDate, 112) = '{0}' " +
                            "AND SubAccountID = '{1}' AND (Status = {2} OR Status = {3} )",//OR Status = {4}
                            sellduedate,
                            accountNo,
                            (int)ETradeCommon.Enums.CommonEnums.ADVANCE_STATUS.PENDING, 
                            (int)ETradeCommon.Enums.CommonEnums.ADVANCE_STATUS.PROCESSING
                            //(int)ETradeCommon.Enums.CommonEnums.ADVANCE_STATUS.FINISHED TODO: comment when golive.
                            );

                TList<ETradeFinance.Entities.CashAdvance> advancePending = this.cashAdvanceService.GetPaged(
                        pendingClause, " ID DESC ", 0, int.MaxValue, out count);

                decimal total = 0;

                if (advanceFinished != null)
                {
                    total +=
                        advanceFinished.Sum(
                            cashAdvance => (cashAdvance.NetWithDraw + cashAdvance.AdvFee) );
                }

                if (advancePending != null)
                {
                    total += advancePending.Sum(
                            cashAdvance => (decimal)(cashAdvance.Status == (int)CommonEnums.ADVANCE_STATUS.FINISHED ?
                                (cashAdvance.CashReceived + cashAdvance.Fee) : (cashAdvance.CashRequest + cashAdvance.Fee)));
                }

                return total;
            }
            catch (Exception e)
            {
                LogHandler.Log(
                    "GetAdvanceFinishedNPending: exception = " + e,
                    this.GetType() + ".GetAdvanceFinishedNPending()",
                    TraceEventType.Error);

                return 0;
            }
        }

        /// <summary>
        /// Inserts the cash advance.
        /// </summary>
        /// <param name="cashAdvance">The cash advance.</param>
        /// <returns>Result of inserting cash advance.</returns>
        public bool InsertCashAdvance(ETradeFinance.Entities.CashAdvance cashAdvance)
        {
            try
            {
                this.cashAdvanceService.Save(cashAdvance);

                return true;
            }
            catch (Exception e)
            {
                LogHandler.Log(
                    "InsertCashAdvance: exception = " + e, this.GetType() + ".InsertCashAdvance()", TraceEventType.Error);
                return false;
            }
        }

        /// <summary>
        /// Inserts the cash advance history.
        /// </summary>
        /// <param name="cashAdvance">The cash advance history.</param>
        /// <returns>Result of inserting cash advance history.</returns>
        public bool InsertCashAdvanceHistory(ETradeFinance.Entities.CashAdvanceHistory  cashAdvance)
        {
            try
            {
                this.cashAdvanceHistoryService.Save(cashAdvance);

                return true;
            }
            catch (Exception e)
            {
                LogHandler.Log(
                    "InsertCashAdvanceHistory: exception = " + e, this.GetType() + ".InsertCashAdvance()", TraceEventType.Error);
                return false;
            }
        }

        /// <summary>
        /// Gets the cash advance status.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="status">The status.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public PagingObject<List<ETradeFinance.Entities.CashAdvance>> GetCashAdvanceStatus(string accountNo, string fromDate, string toDate, int status, int pageIndex, int pageSize)
        {
            var returnVal = new PagingObject<List<ETradeFinance.Entities.CashAdvance>>(new List<ETradeFinance.Entities.CashAdvance>());

            string whereClause =
                string.Format(
                " SubAccountId = '{0}' AND CONVERT(VARCHAR(8), AdvanceDate, 112) BETWEEN '{1}' AND '{2}' AND " + (status == 0 ? "(1=1)": "Status = '{3}'"),
                    accountNo,
                    fromDate,
                    toDate, 
                    status);

            int totalRecords;

            if (pageIndex > 0)
            {
                // A page
                var cashAdvances = this.cashAdvanceService.GetPaged(
                    whereClause, " Id DESC", pageIndex - 1, pageSize, out totalRecords);

                if (cashAdvances != null)
                {
                    returnVal.Data.AddRange(cashAdvances);    
                }
            }
            else
            {
                // All pages
                var cashAdvances = this.cashAdvanceService.GetPaged(whereClause, " Id DESC", pageIndex, int.MaxValue, out totalRecords);

                if (cashAdvances != null)
                {
                    returnVal.Data.AddRange(cashAdvances);
                }
            }

            foreach (var advanceItem in returnVal.Data )
            {
                advanceItem.BrokerId=!string.IsNullOrEmpty(advanceItem.BrokerId)?advanceItem.BrokerId:string.Empty;
                advanceItem.BrokerName = !string.IsNullOrEmpty(advanceItem.BrokerName)? advanceItem.BrokerName: string.Empty;
                advanceItem.CanCancel = IsValidCancelAdvance(advanceItem) == CommonEnums.RET_CODE.SUCCESS ? true : false ;
            }

            returnVal.Count = totalRecords;

            return returnVal;
        }

        /// <summary>
        /// Determines whether [is valid advance] [the specified maximum advance].
        /// </summary>
        /// <param name="maximumAdvance">The maximum advance.</param>
        /// <param name="cashAdvance">The cash advance.</param>
        /// <param name="dueDate">The due date.</param>
        /// <param name="advanceFee">The advance fee.</param>
        /// <param name="tax">The tax.</param>
        /// <param name="dicAdvanceTimes">Dictionary of Advance times</param>
        /// <param name="dicHolidays">Dictionary of holidays</param>
        /// <param name="dicWorkingDays">Dictionary of working days</param>
        /// <returns>
        /// <para>Result of validating cash advance.</para>
        /// <para>RET_CODE=ERROR_INVALID_CASH_ADVANCE: Cash advance is invalid.</para>
        /// <para>RET_CODE=ERROR_CANNOT_ADVANCE_OUTOF_TIME: Not time for cash advance.</para>
        /// <para>RET_CODE=ERROR_CANNOT_ADVANCE_IN_DUE_DATE: Cash advance due date is invalid.</para>
        /// <para>RET_CODE=SUCCESS: Putting order successfully.</para>
        /// </returns>
        public CommonEnums.RET_CODE IsValidAdvance(decimal maximumAdvance, decimal cashAdvance, 
            DateTime dueDate, decimal advanceFee, decimal tax, Dictionary<int, AdvanceTime> dicAdvanceTimes,
            Dictionary<string, DateTime> dicHolidays, Dictionary<int, bool> dicWorkingDays)
        {
            // We will check following cases:
            // 1. grossAdvance <= 0
            // 2. IsValidTime: validate time to advance for trading, withdraw
            // 3. IsValidCash: the cash to advance is either enough or not?
            // 4. IsValidDate: tradeDate is either T3 or not?
            if (cashAdvance <= 0)
            {
                LogHandler.Log(
                    "IsValidAdvance: cash advance is " + cashAdvance + ". It must > 0",
                    this.GetType() + ".IsValidAdvance()",
                    TraceEventType.Warning);
                return CommonEnums.RET_CODE.ERROR_INVALID_CASH_ADVANCE;
            }

            // IsValidTime holidays
            if (dicHolidays.ContainsKey(DateTime.Now.ToString("yyyyMMdd")))
            {
                LogHandler.Log(
                    "IsValidAdvance: cash advance of investor input " + cashAdvance + " is in holiday time " +
                    maximumAdvance,
                    this.GetType() + ".IsValidAdvance()",
                    TraceEventType.Warning);

                    return CommonEnums.RET_CODE.ERROR_CANNOT_ADVANCE_OUTOF_TIME;

            }

            var dayOfWeek = Utils.ConvertDayOfWeek(DateTime.Now.DayOfWeek);
            //IsvalidTime working days
            if (!dicWorkingDays.ContainsKey(dayOfWeek))
            {
                LogHandler.Log(
                    "IsValidAdvance: cash advance of investor input " + cashAdvance + " is out of working day " +
                    maximumAdvance,
                    this.GetType() + ".IsValidAdvance()",
                    TraceEventType.Warning);

                    return CommonEnums.RET_CODE.ERROR_CANNOT_ADVANCE_OUTOF_TIME;

            }

            if (!dicWorkingDays[dayOfWeek])
            {
                 LogHandler.Log(
                    "IsValidAdvance: cash advance of investor input " + cashAdvance + " is out of working day " +
                    maximumAdvance,
                    this.GetType() + ".IsValidAdvance()",
                    TraceEventType.Warning);

                    return CommonEnums.RET_CODE.ERROR_CANNOT_ADVANCE_OUTOF_TIME;

            }

            AdvanceTime advanceTime = dicAdvanceTimes[(int)CommonEnums.ADVANCE_TYPE.TRADING];
            //IsValidTime of working hours.
            if(DateTime.Now.TimeOfDay < advanceTime.StartTime.TimeOfDay || DateTime.Now.TimeOfDay > advanceTime.EndTime.TimeOfDay)
            {
                LogHandler.Log(
                    "IsValidAdvance: cash advance of investor input " + cashAdvance + " is out of woring time " +
                    maximumAdvance,
                    this.GetType() + ".IsValidAdvance()",
                    TraceEventType.Warning);

                return CommonEnums.RET_CODE.ERROR_CANNOT_ADVANCE_OUTOF_TIME;
            }

            // IsValidDate
            if (DateTime.Equals(DateTime.Now.Date, dueDate.Date)) //TODO: Apply When golive: OR DateTime.Now.Subtract(dueDate).Days >= 0)
            {
                LogHandler.Log(
                    "IsValidAdvance: cannot advance in due date",
                    this.GetType() + ".IsValidAdvance()",
                    TraceEventType.Warning);
                return CommonEnums.RET_CODE.ERROR_CANNOT_ADVANCE_IN_DUE_DATE;
            }

            return CommonEnums.RET_CODE.SUCCESS;
        }

        /// <summary>
        /// Builds the contract no.
        /// </summary>
        /// <param name="advanceDate">The advance date.</param>
        /// <returns>Contract no.</returns>
        public string BuildContractNo(DateTime advanceDate)
        {
            string whereClause = string.Format(
                "CONVERT(varchar(8), AdvanceDate, 112) = '{0}'", advanceDate.ToString("yyyyMMdd"));

            int totalRecords = 0;
            this.cashAdvanceService.GetPaged(whereClause, " ID DESC ", 0, int.MaxValue, out totalRecords);

            string contractNo = "ADV-" + advanceDate.ToString("yyyyMMdd") + (totalRecords + 1).ToString().PadLeft(5, '0');

            return contractNo;
        }

        /// <summary>
        /// Determine whether this is a valid cancel cash advance order.
        /// </summary>
        /// <param name="cashAdvance">The cash advance data.</param>
        /// <returns>
        /// <para>Result of validating cash advance.</para>
        /// <para>RET_CODE=ERROR_CANNOT_CANCEL_ADVANCE_CANCELED: Cannot cancel a canceled cash advance.</para>
        /// <para>RET_CODE=ERROR_CANNOT_CANCEL_ADVANCE_REFJECTED: Cannot cancel a rejected cash advance.</para>
        /// <para>RET_CODE=ERROR_CANNOT_CANCEL_IN_PROCESSING: Cannot cancel a cash advance which is in processing state.</para>
        /// <para>RET_CODE=ERROR_CANNOT_CANCEL_ADVANCE_FINISHED: Cannot cancel a finished cash advance.</para>
        /// </returns>
        public CommonEnums.RET_CODE IsValidCancelAdvance(ETradeFinance.Entities.CashAdvance cashAdvance)
        {
            if (cashAdvance.Status == (int)CommonEnums.ADVANCE_STATUS.CANCELLED)
            {
                LogHandler.Log(
                    "IsValidCancelAdvance: ERROR_CANNOT_CANCEL_ADVANCE_CANCELED, status = " + cashAdvance.Status,
                    this.GetType() + ".IsValidCancelAdvance()",
                    TraceEventType.Warning);
                return CommonEnums.RET_CODE.ERROR_CANNOT_CANCEL_ADVANCE_CANCELED;
            }

            if (cashAdvance.Status == (int)CommonEnums.ADVANCE_STATUS.REJECTED)
            {
                LogHandler.Log(
                    "IsValidCancelAdvance: ERROR_CANNOT_CANCEL_ADVANCE_REFJECTED, status = " + cashAdvance.Status,
                    this.GetType() + ".IsValidCancelAdvance()",
                    TraceEventType.Warning);
                return CommonEnums.RET_CODE.ERROR_CANNOT_CANCEL_ADVANCE_REFJECTED;
            }

            if (cashAdvance.Status == (int)CommonEnums.ADVANCE_STATUS.PROCESSING)
            {
                LogHandler.Log(
                    "IsValidCancelAdvance: ERROR_CANNOT_CANCEL_IN_PROCESSING, status = " + cashAdvance.Status,
                    this.GetType() + ".IsValidCancelAdvance()",
                    TraceEventType.Warning);
                return CommonEnums.RET_CODE.ERROR_CANNOT_CANCEL_IN_PROCESSING;
            }

            if (cashAdvance.Status == (int)CommonEnums.ADVANCE_STATUS.FINISHED)
            {
                LogHandler.Log(
                    "IsValidCancelAdvance: ERROR_CANNOT_CANCEL_ADVANCE_FINISHED, status = " + cashAdvance.Status,
                    this.GetType() + ".IsValidCancelAdvance()",
                    TraceEventType.Warning);
                return CommonEnums.RET_CODE.ERROR_CANNOT_CANCEL_ADVANCE_FINISHED;
            }

            return CommonEnums.RET_CODE.SUCCESS;
        }

        /// <summary>
        /// Calculates the advance fee.
        /// </summary>
        /// <param name="withDrawAmount">The with draw amount.</param>
        /// <param name="dueDate">The due date.</param>
        /// <returns></returns>
        public decimal CalculateAdvanceFee(decimal withDrawAmount, DateTime dueDate)
        {
            int advanceDays = (dueDate.Date - DateTime.Now.Date).Days; //TODO: need confirm TCSC cashDueDate - Today - 1??
            Fee fee = feeServices.GetTradeFee(CommonEnums.FEE_TYPE.FEE_CASH_ADVANCE, withDrawAmount);
            if (fee != null)
            {
                decimal feeValue = 0;
                if (Constants.PERCENT_UNIT!=0)
                {
                    feeValue= withDrawAmount * (fee.FeeRatio / Constants.PERCENT_UNIT) * advanceDays;
                }
                if (feeValue < (fee.MinFee ?? 0))
                    feeValue = (fee.MinFee ?? 0);
                return Math.Round(feeValue);
            }

            return 0;
        }

        public decimal CalculateAdvanceFee(decimal withDrawAmount, int advanceDays)
        {           
            Fee fee = feeServices.GetTradeFee(CommonEnums.FEE_TYPE.FEE_CASH_ADVANCE, withDrawAmount);
            if (fee != null)
            {
                decimal feeValue = 0;
                if (Constants.PERCENT_UNIT != 0)
                {
                    feeValue = withDrawAmount * (fee.FeeRatio / Constants.PERCENT_UNIT) * advanceDays;
                }
                if (feeValue < (fee.MinFee ?? 0))
                    feeValue = (fee.MinFee ?? 0);
                return Math.Round(feeValue);
            }

            return 0;
        }

        #region transform

        /// <summary>
        /// Transforms the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        private CashAdvance Transform(ETradeFinance.Entities.CashAdvance source)
        {
            return new CashAdvance
                       {
                           AdvFee = source.Fee == null ? 0 : (decimal) source.Fee,
                           AdvVat = source.Vat == null ? 0 : (decimal) source.Vat,
                           ConfirmTime = source.ExecTime == null ? new DateTime() : (DateTime) source.ExecTime,
                           ConfirmUser = source.BrokerId,
                           ContractNo = source.ContractNo,
                           EditDate = source.AdvanceDate,
                           EditTime = source.ExecTime == null ? new DateTime() : (DateTime) source.ExecTime,
                           GrossWithDraw = source.CashRequest == null ? 0 : (decimal) source.CashRequest,
                           NetWithDraw = source.CashReceived == null ? 0 : (decimal) source.CashReceived,
                           SellAmt = source.TotalSellValue == null ? 0 : (decimal) source.TotalSellValue,
                           UserId = source.BrokerId,
                           TradeDate = source.SellDueDate == null ? new DateTime() : (DateTime) source.SellDueDate,
                           SellValue = (decimal) (source.TotalSellValue == null ? 0 : source.TotalSellValue),
                           Remark = source.Reason,
                           Status = (short)source.Status,                                                     
                };
        }

        /// <summary>
        /// Transforms the specified sources.
        /// </summary>
        /// <param name="sources">The sources.</param>
        /// <returns></returns>
        private List<CashAdvance> Transform(TList<ETradeFinance.Entities.CashAdvance> sources)
        {
            return sources.Select(cashAdvance => this.Transform(cashAdvance)).ToList();
        }

        #endregion
    }
}