// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlInformixProvider.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the SqlInformixProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCore.DataAccess.SqlClient
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Odbc;
    using System.Diagnostics;
    using System.Text;
    using System.Linq;
    using ETradeCommon;

    using ETradeCore.Entities;
    using AccountManager.Entities;

    using ETradeCoreDB.Helper;

    /// <summary>
    /// Implement ISbaCoreProvider
    /// </summary>
    public class SqlInformixProvider : ISbaCoreProvider
    {
        #region Trade Permission
        /// <summary>
        /// Gets the trade permission.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns>TradePermission</returns>
        public TradePermission GetTradePermission(string accountNo)
        {

            OdbcConnection conn = null;
            OdbcDataReader dataReader = null;

            try
            {
                OdbcCommand cmd;
                //string cmdText =
                //    "SELECT accountno, accountno2, customertype, acctstatus, opendate, closedate, canbuy, cansell " +
                //    " FROM " + Constants.SBA_SCHEMA_NAME + Constants.CUSTOMER_VIEW + " where accountno2 = '" + accountNo + "'";
                string cmdText =
                    "SELECT accountno, accountno2, customertype, acctstatus, opendate, closedate, canbuy, cansell " +
                    " FROM " + Constants.CUSTOMER_VIEW + " where accountno2 = '" + accountNo + "'";

                conn = DaoCommon.Connect();
                if (conn == null)
                {
                    LogHandler.Log(
                        "GetTradePermission: Error to connect to informix: " + AppConfig.AliasInformix,
                        GetType() + ".GetTradePermission",
                        TraceEventType.Error);
                    return null;
                }

                cmd = conn.CreateCommand();
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;

                // Retry out put
                dataReader = cmd.ExecuteReader();
                if (!dataReader.HasRows)
                {
                    LogHandler.Log(
                        "GetTradePermission: " + accountNo + " not exist in SBA",
                        GetType() + ".GetTradePermission",
                        TraceEventType.Information);
                    return null;
                }

                var tradePermission = new TradePermission();

                while (dataReader.Read())
                {
                    tradePermission.CanBuy = DaoCommon.GetFieldStringValue(dataReader, "canbuy") ==
                                             ((char)ETradeCommon.Enums.CommonEnums.CORE_TRADE_PERMISSION.CANBUY).
                                                 ToString()
                                                 ? true
                                                 : false;
                    tradePermission.CanSell = DaoCommon.GetFieldStringValue(dataReader, "cansell") ==
                                              ((char)ETradeCommon.Enums.CommonEnums.CORE_TRADE_PERMISSION.CANSELL).
                                                  ToString()
                                                  ? true
                                                  : false;
                    tradePermission.IsLock = DaoCommon.GetFieldStringValue(dataReader, "acctstatus") ==
                                             ((char)ETradeCommon.Enums.CommonEnums.CORE_TRADE_PERMISSION.STATUS).
                                                 ToString()
                                                 ? false
                                                 : true;

                    break;
                }

                DaoCommon.DisConnect(conn, dataReader);

                return tradePermission;
            }
            catch (OdbcException e)
            {
                DaoCommon.DisConnect(conn, dataReader);
                LogHandler.Log(
                    String.Format("GetTradePermission: EXCEPTION, ex = {0}, customerId = {1}", e, accountNo),
                    GetType() + ".GetTradePermission",
                    TraceEventType.Error);
                return null;
            }
        }
        #endregion

        #region Order history
        /// <summary>
        /// Gets the order history.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="orderStatus">The order status.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public List<OrderHistory> GetOrderHistory(string accountNo, string fromDate, string toDate, string symbol, int orderStatus, int pageNumber, int pageSize)
        {
            OdbcConnection conn = null;
            OdbcDataReader orderHistDataReader = null;

            try
            {
                OdbcCommand cmd;
                //string cmdText = "SELECT accountno, accountno2, orderno, orderdate_time, orderstatus, " +
                //                 "ordertype1, ordertype2, ordertype3, ordertype4, stocksymbol, stockvolume, price " +
                //                 " FROM " + Constants.SBA_SCHEMA_NAME + Constants.ORDERHIST_VIEW +
                //                 this.BuildOrderHistCondition(accountNo, fromDate, toDate, symbol);

                string cmdText = "SELECT accountno, accountno2, orderno, orderdate_time, orderstatus, " +
                                 "ordertype1, ordertype2, ordertype3, ordertype4, stocksymbol, stockvolume, price " +
                                 " FROM " + Constants.ORDERHIST_VIEW +
                                 this.BuildOrderHistCondition(accountNo, fromDate, toDate, symbol);

                conn = DaoCommon.Connect();
                if (conn == null)
                {
                    LogHandler.Log(
                        "GetOrderHistory: Error to connect to informix: " + AppConfig.AliasInformix,
                        GetType() + ".GetOrderHistory",
                        TraceEventType.Error);
                    return null;
                }

                cmd = conn.CreateCommand();
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;

                // Retry out put
                orderHistDataReader = cmd.ExecuteReader();
                if (!orderHistDataReader.HasRows)
                {
                    LogHandler.Log(
                        "GetOrderHistory: " + accountNo + " no order history from " + fromDate + " to " + toDate +
                        ", symbol = " + symbol + ", pageNumber = " + pageNumber + ", pageSize = " + pageSize,
                        GetType() + ".GetOrderHistory",
                        TraceEventType.Information);
                    return null;
                }

                var orderHistories = new List<OrderHistory>();

                while (orderHistDataReader.Read())
                {
                    OrderHistory orderHistory = this.GetOrderHistory(orderHistDataReader);

                    orderHistories.Add(orderHistory);
                }

                DaoCommon.DisConnect(conn, orderHistDataReader);

                return orderHistories;
            }
            catch (OdbcException e)
            {
                DaoCommon.DisConnect(conn, orderHistDataReader);
                LogHandler.Log(
                    "GetOrderHistory: EXCEPTION, ex = " + e + ", accountNo = " + accountNo + ", fromDate = " + fromDate +
                    " toDate = " + toDate + ", symbol = " + symbol + ", pageNumber = " + pageNumber + ", pageSize = " +
                    pageSize,
                    GetType() + ".GetOrderHistory",
                    TraceEventType.Error);
                return null;
            }
        }

        /// <summary>
        /// Builds the order hist condition.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="symbol">The symbol.</param>
        /// <returns></returns>
        private string BuildOrderHistCondition(string accountNo, string fromDate, string toDate, string symbol)
        {
            try
            {
                /* We cannot filter by date in SQL, because orderdate_time type is 'yyyy-MM-dd HH:mm:ss'.
                   This work incorrect. So we will filter in code*/
                var condition = new StringBuilder(500);

                condition.Append(" WHERE ");

                condition.Append("(accountno2 = '" + accountNo + "')");

                condition.Append(" AND ");

                condition.Append(
                    "((stocksymbol = '" + symbol + "') OR (" +
                    (symbol == string.Empty || symbol == "all" ? "1=1" : "1=2") + "))");

                return condition.ToString();
            }
            catch (Exception exception)
            {
                LogHandler.Log(
                    "BuildOrderHistCondition: exception, ex = " + exception,
                    this.GetType() + ".BuildOrderHistCondition",
                    TraceEventType.Error);
                return string.Empty;
            }
        }


        /// <summary>
        /// Gets the order history.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns></returns>
        private OrderHistory GetOrderHistory(OdbcDataReader dataReader)
        {
            var orderHistory = new OrderHistory
            {
                AccountNo = DaoCommon.GetFieldStringValue(dataReader, "accountno2"),
                Price = Constants.MONEY_UNIT!=0? DaoCommon.GetFieldDecimalValue(dataReader, "price") / Constants.MONEY_UNIT:0,
                Side = DaoCommon.GetFieldStringValue(dataReader, "ordertype1"),
                Volume = DaoCommon.GetFieldDecimalValue(dataReader, "stockvolume"),
                Symbol = DaoCommon.GetFieldStringValue(dataReader, "stocksymbol"),
                OrderStatus = DaoCommon.GetFieldStringValue(dataReader, "orderstatus"),
                OrderNo = DaoCommon.GetFieldIntegerValue(dataReader, "orderno"),
                OrderDate = DaoCommon.GetFieldStringValue(dataReader, "orderdate_time").Split(' ')[0],
                OrderTime = DaoCommon.GetFieldStringValue(dataReader, "orderdate_time").Split(' ')[1]
            };

            return orderHistory;
        }
        #endregion

        #region Deal History
        /// <summary>
        /// Gets the deal history.
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        /// <param name="dealDate">The deal date.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public List<DealHistory> GetDealHistory(decimal orderNo, string dealDate, int page)
        {
            OdbcConnection conn = null;
            OdbcDataReader dealHistDataReader = null;

            try
            {
                OdbcCommand cmd;
                //string cmdText = "SELECT accountno, accountno2, orderno, stocksymbol, dealno, " +
                //    "dealstatus, dealdate_time, dealvolume, dealprice" +
                //    " FROM " + Constants.SBA_SCHEMA_NAME + Constants.DEALHIST_VIEW +
                //                 this.BuildDealHistCondition((int)orderNo, dealDate, page);
                string cmdText = "SELECT accountno, accountno2, orderno, stocksymbol, dealno, " +
                    "dealstatus, dealdate_time, dealvolume, dealprice" +
                    " FROM " + Constants.DEALHIST_VIEW +
                                 this.BuildDealHistCondition((int)orderNo, dealDate, page);

                conn = DaoCommon.Connect();
                if (conn == null)
                {
                    LogHandler.Log(
                        "GetDealHistory: Error to connect to informix: " + AppConfig.AliasInformix,
                        GetType() + ".GetDealHistory",
                        TraceEventType.Error);
                    return null;
                }

                cmd = conn.CreateCommand();
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;

                // Retry out put
                dealHistDataReader = cmd.ExecuteReader();
                if (!dealHistDataReader.HasRows)
                {
                    LogHandler.Log(
                        "GetDealHistory: " + orderNo + " not exist in " +
                        Constants.DEALHIST_VIEW + ", dealDate = " + dealDate + ", page = " + page,
                        GetType() + ".GetDealHistory",
                        TraceEventType.Information);
                    return null;
                }

                var dealHistories = new List<DealHistory>();

                while (dealHistDataReader.Read())
                {
                    DealHistory dealHistory = this.GetDealHistory(dealHistDataReader);

                    dealHistories.Add(dealHistory);
                }

                DaoCommon.DisConnect(conn, dealHistDataReader);

                return dealHistories;
            }
            catch (OdbcException e)
            {
                DaoCommon.DisConnect(conn, dealHistDataReader);
                LogHandler.Log(
                    "GetDealHistory: EXCEPTION, ex = " + e + ", orderNo = " + orderNo + ", dealDate = " + dealDate +
                    " page = " + page,
                    GetType() + ".GetDealHistory",
                    TraceEventType.Error);
                return null;
            }
        }

        /// <summary>
        /// Builds the deal hist condition.
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        /// <param name="dealDate">The deal date.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        private string BuildDealHistCondition(int orderNo, string dealDate, int page)
        {
            try
            {
                var condition = new StringBuilder(500);

                condition.Append(" WHERE orderno = " + orderNo);

                return condition.ToString();
            }
            catch (Exception exception)
            {
                LogHandler.Log(
                    "BuildDealHistCondition: exception = " + exception + ", dealDate = " + dealDate + ", orderNo = " +
                    orderNo + ", page = " + page,
                    this.GetType() + ".BuildDealHistCondition",
                    TraceEventType.Error);

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the deal history.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns></returns>
        private DealHistory GetDealHistory(OdbcDataReader dataReader)
        {
            var dealHistory = new DealHistory
            {
                DealDate = DaoCommon.GetFieldStringValue(dataReader, "dealdate_time").Split(' ')[0],
                DealTime = DaoCommon.GetFieldStringValue(dataReader, "dealdate_time").Split(' ')[1],
                DealPrice = Constants.MONEY_UNIT!=0? DaoCommon.GetFieldDecimalValue(dataReader, "dealprice") / Constants.MONEY_UNIT:0,
                DealVolume = DaoCommon.GetFieldDecimalValue(dataReader, "dealvolume"),
                OrderNo = DaoCommon.GetFieldIntegerValue(dataReader, "orderno"),
            };

            return dealHistory;
        }
        #endregion

        #region Advance History
        /// <summary>
        /// Gets the advance history.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="fromAdvanceDate">From advance date.</param>
        /// <param name="toAdvanceDate">To advance date.</param>
        /// <param name="fromSellDate">From sell date.</param>
        /// <param name="toSellDate">To sell date.</param>
        /// <param name="contractNo">The contract no.</param>
        /// <returns></returns>
        public List<CashAdvance> GetAdvanceHistory(
            string accountNo, string fromAdvanceDate, string toAdvanceDate, string fromSellDate, string toSellDate, string contractNo)
        {
            OdbcConnection conn = null;
            OdbcDataReader advHistDataReader = null;

            try
            {
                OdbcCommand cmd;
                string cmdText = String.Format("SELECT accountno, accountno2, name, tradedate, tradeamt, tradecommission, duedate, referno, withdraw, fee, vat, netwithdraw, userid, userbranch, editdate, edittime, confirmuser, confirmtime, sellvalue, sellamt, pledgevalue, pledgeamt, loanamt,confirmuser, remark FROM {0}{1}", Constants.CASHADVANCE_VIEW, this.BuildCashAdvanceCondition(accountNo, fromAdvanceDate, toAdvanceDate, fromSellDate, toSellDate, contractNo));

                conn = DaoCommon.Connect();
                if (conn == null)
                {
                    LogHandler.Log(
                        "GetAdvanceHistory: Error to connect to informix: " + AppConfig.AliasInformix,
                        GetType() + ".GetAdvanceHistory",
                        TraceEventType.Error);
                    return null;
                }

                cmd = conn.CreateCommand();
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;

                // Retry out put
                advHistDataReader = cmd.ExecuteReader();
                if (!advHistDataReader.HasRows)
                {
                    /*LogHandler.Log(
                        "GetAdvanceHistory: " + accountNo + " no advance history from " + fromDate + " to " + toDate,
                        GetType() + ".GetAdvanceHistory",
                        TraceEventType.Information);*/
                    return null;
                }

                var advHistories = new List<CashAdvance>();

                while (advHistDataReader.Read())
                {
                    CashAdvance cashAdvance = this.GetAdvanceHistory(advHistDataReader);

                    cashAdvance.Status = (short)ETradeCommon.Enums.CommonEnums.ADVANCE_STATUS.FINISHED;
                    advHistories.Add(cashAdvance);
                }

                DaoCommon.DisConnect(conn, advHistDataReader);

                return advHistories;
            }
            catch (OdbcException e)
            {
                DaoCommon.DisConnect(conn, advHistDataReader);
                LogHandler.Log(
                    "GetAdvanceHistory: EXCEPTION, ex = " + e + ", accountNo = " + accountNo + ", fromDate = " + fromAdvanceDate +
                    " toDate = " + toAdvanceDate,
                    GetType() + ".GetAdvanceHistory",
                    TraceEventType.Error);
                return null;
            }
        }

        /// <summary>
        /// Builds the cash advance condition.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="fromAdvanceDate">From advance date.</param>
        /// <param name="toAdvanceDate">To advance date.</param>
        /// <param name="fromSellDate">From sell date.</param>
        /// <param name="toSellDate">To sell date.</param>
        /// <param name="contractNo">The contract no.</param>
        /// <returns></returns>
        private string BuildCashAdvanceCondition(
            string accountNo,
            string fromAdvanceDate,
            string toAdvanceDate,
            string fromSellDate,
            string toSellDate,
            string contractNo)
        {
            try
            {
                var condition = new StringBuilder(500);

                condition.Append(" WHERE ");
                if(!string.IsNullOrEmpty(accountNo))
                {
                    condition.Append(" accountno2 = '" + accountNo + "' AND ");
                }                

                condition.Append(
                    "((tradedate BETWEEN '" + fromSellDate + "' AND '" + toSellDate + "') OR (" +
                    (fromSellDate == string.Empty && toSellDate == string.Empty ? "1=1" : "1=2") + "))");

                condition.Append(" AND ");

                condition.Append(
                    "((editdate BETWEEN '" + fromAdvanceDate + "' AND '" + toAdvanceDate + "') OR (" +
                    (fromAdvanceDate == string.Empty && toAdvanceDate == string.Empty ? "1=1" : "1=2") + "))");

                condition.Append(" AND ");

                condition.Append(
                    "(referno = '" + contractNo + "' OR  " +
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
        /// Gets the advance history.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns></returns>
        private CashAdvance GetAdvanceHistory(OdbcDataReader dataReader)
        {
            return new CashAdvance
            {
                ConfirmTime = DaoCommon.GetFieldDateTimeValue(dataReader, "confirmtime"),
                ContractNo = DaoCommon.GetFieldStringValue(dataReader, "referno"),
                CustomerName = DaoCommon.GetFieldStringValue(dataReader, "name"),
                CustomerNo = DaoCommon.GetFieldStringValue(dataReader, "accountno"),
                EditDate = DaoCommon.GetFieldDateTimeValue(dataReader, "editdate"),
                EditTime = DaoCommon.GetFieldDateTimeValue(dataReader, "edittime"),
                AdvFee = DaoCommon.GetFieldDecimalValue(dataReader, "fee"),
                AdvVat = DaoCommon.GetFieldDecimalValue(dataReader, "vat"),
                GrossWithDraw = DaoCommon.GetFieldDecimalValue(dataReader, "withdraw"),
                LoanAmt = DaoCommon.GetFieldDecimalValue(dataReader, "loanamt"),
                NetWithDraw = DaoCommon.GetFieldDecimalValue(dataReader, "netwithdraw"),
                PledgeAmt = DaoCommon.GetFieldDecimalValue(dataReader, "pledgeamt"),
                PledgeValue = DaoCommon.GetFieldDecimalValue(dataReader, "pledgevalue"),
                Remark = DaoCommon.GetFieldStringValue(dataReader, "remark"),
                SellAmt = DaoCommon.GetFieldDecimalValue(dataReader, "sellamt"),
                SellValue = DaoCommon.GetFieldDecimalValue(dataReader, "sellvalue"),
                TradeCommission = DaoCommon.GetFieldDecimalValue(dataReader, "tradecommission"),
                TradeDate = DaoCommon.GetFieldDateTimeValue(dataReader, "tradedate"),
                UserId = DaoCommon.GetFieldStringValue(dataReader, "userid"),
                ConfirmUser = DaoCommon.GetFieldStringValue(dataReader, "confirmuser"),
                BrokerId = DaoCommon.GetFieldStringValue(dataReader, "confirmuser"),
                DueDate = DaoCommon.GetFieldDateTimeValue(dataReader,"duedate"),
                AccountNo2 = DaoCommon.GetFieldStringValue(dataReader, "accountno2")
            };
        }
        #endregion

        #region Actual Trade
        /// <summary>
        /// Gets the actual trading.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="symbol">The symbol.</param>
        /// <returns></returns>
        public List<ActualTrade> GetActualTrading(string accountNo, string fromDate, string toDate, string symbol)
        {
            OdbcConnection conn = null;
            OdbcDataReader actTradeDataReader = null;

            try
            {
                OdbcCommand cmd;               
                string cmdText = "select tradedate,accountno, accountno2,  stocksymbol, ordertype1, type, sum( stockvolume) as stockvolume, price, commission, settlementdate, sum(pit) as pit  " +                   
                    " FROM " +Constants.ACTTRADE_VIEW +
                                 this.BuildAcctTradeCondition(accountNo, fromDate, toDate, symbol);

                conn = DaoCommon.Connect();
                if (conn == null)
                {
                    LogHandler.Log(
                        "GetActualTrading: Error to connect to informix: " + AppConfig.AliasInformix,
                        GetType() + ".GetActualTrading",
                        TraceEventType.Error);
                    return null;
                }

                cmd = conn.CreateCommand();
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;

                // Retry out put
                actTradeDataReader = cmd.ExecuteReader();
                if (!actTradeDataReader.HasRows)
                {
                    LogHandler.Log(
                        "GetActualTrading: " + accountNo + " no actual trade from " + fromDate + " to " + toDate +
                        ", symbol = " + symbol,
                        GetType() + ".GetActualTrading",
                        TraceEventType.Information);
                    return null;
                }

                var actTrades = new List<ActualTrade>();

                while (actTradeDataReader.Read())
                {
                    ActualTrade actualTrade = this.GetActualTrade(actTradeDataReader);
                    actTrades.Add(actualTrade);
                }

                DaoCommon.DisConnect(conn, actTradeDataReader);                
                return actTrades;
            }
            catch (OdbcException e)
            {
                DaoCommon.DisConnect(conn, actTradeDataReader);
                LogHandler.Log(
                    "GetActualTrading: EXCEPTION, ex = " + e + ", accountNo = " + accountNo + ", fromDate = " + fromDate +
                    " toDate = " + toDate + ", symbol = " + symbol,
                    GetType() + ".GetActualTrading",
                    TraceEventType.Error);
                return null;
            }
        }

        /// <summary>
        /// Builds the acct trade condition.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="symbol">The symbol.</param>
        /// <returns></returns>
        private string BuildAcctTradeCondition(string accountNo, string fromDate, string toDate, string symbol)
        {
            try
            {
                var condition = new StringBuilder(500);

                condition.Append(" WHERE (accountno2 = '" + accountNo + "')");

                if(!string.IsNullOrEmpty(symbol) && !symbol.ToLower().Equals("all"))               
                {
                    condition.Append(" AND ");
                    condition.Append(" stocksymbol = '" + symbol + "'");
                }

                if(!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                {
                    condition.Append(" AND ");

                    condition.Append(" (tradedate BETWEEN '" + fromDate + "' AND '" + toDate + "') ");
                }
                condition.Append(
                    " group by tradedate,accountno, accountno2,  stocksymbol, ordertype1, type, price, commission, settlementdate,pit ");
                condition.Append(" ORDER BY tradedate desc");
                return condition.ToString();
            }
            catch (Exception exception)
            {
                LogHandler.Log(
                    "BuildAcctTradeCondition: exception = " + exception + ", accountNo = " + accountNo + ", fromDate = " +
                    fromDate + ", toDate = " + toDate + ", symbol = " + symbol,
                    this.GetType() + ".BuildAcctTradeCondition",
                    TraceEventType.Error);

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the actual trade.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns></returns>
        private ActualTrade GetActualTrade(OdbcDataReader dataReader)
        {
            return new ActualTrade
            {
                Commission = DaoCommon.GetFieldDecimalValue(dataReader, "commission"),
                ConPrice = DaoCommon.GetFieldStringValue(dataReader, "type"),
                CustomerNo = DaoCommon.GetFieldStringValue(dataReader, "accountno"),
                Price = Constants.MONEY_UNIT!= 0 ? DaoCommon.GetFieldDecimalValue(dataReader, "price") / Constants.MONEY_UNIT:0,
                SettlementDate = DaoCommon.GetFieldDateTimeValue(dataReader, "settlementdate"),
                Side = DaoCommon.GetFieldStringValue(dataReader, "ordertype1"),
                Symbol = DaoCommon.GetFieldStringValue(dataReader, "stocksymbol"),
                TradeDate = DaoCommon.GetFieldDateTimeValue(dataReader, "tradedate"),
                Volume = DaoCommon.GetFieldDecimalValue(dataReader, "stockvolume"),
                PIT = DaoCommon.GetFieldDecimalValue(dataReader, "pit"),
            };
        }
        #endregion

        #region XD
             
        /// <summary>
        /// Gets the XD info.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns></returns>
        public List<XD> GetXDInfo(string accountNo, string symbol, string fromDate, string toDate)
        {
            OdbcConnection conn = null;
            OdbcDataReader xdDataReader = null;

            try
            {
                OdbcCommand cmd;
                string cmdText = "SELECT account, sharecode, compunitbfxd, closedate, paydate, payrate, compamt, remark " +
                                 " FROM " + Constants.XD_VIEW +
                                 this.BuildXDCondition(accountNo, fromDate, toDate, symbol);

                conn = DaoCommon.Connect();
                if (conn == null)
                {
                    LogHandler.Log(
                        "GetXDInfo: Error to connect to informix: " + AppConfig.AliasInformix,
                        GetType() + ".GetXDInfo",
                        TraceEventType.Error);
                    return null;
                }

                cmd = conn.CreateCommand();
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;

                // Retry out put
                xdDataReader = cmd.ExecuteReader();
                if (!xdDataReader.HasRows)
                {
                    LogHandler.Log(
                        "GetXDInfo: " + accountNo + " no XD from " + fromDate + " to " + toDate +
                        ", symbol = " + symbol,
                        GetType() + ".GetXDInfo",
                        TraceEventType.Information);
                    return null;
                }

                var xds = new List<XD>();

                while (xdDataReader.Read())
                {
                    XD xd = this.GetXD(xdDataReader);

                    xds.Add(xd);
                }

                DaoCommon.DisConnect(conn, xdDataReader);

                return xds;
            }
            catch (OdbcException e)
            {
                DaoCommon.DisConnect(conn, xdDataReader);
                LogHandler.Log(
                    "GetXDInfo: EXCEPTION, ex = " + e + ", accountNo = " + accountNo + ", fromDate = " + fromDate +
                    " toDate = " + toDate + ", symbol = " + symbol,
                    GetType() + ".GetXDInfo",
                    TraceEventType.Error);
                return null;
            }
        }

        /// <summary>
        /// Builds the XD condition.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="symbol">The symbol.</param>
        /// <returns></returns>
        private string BuildXDCondition(string accountNo, string fromDate, string toDate, string symbol)
        {
            try
            {
                var condition = new StringBuilder(500);

                condition.Append(" WHERE ");

                condition.Append("(account = '" + accountNo + "')");

                
                if(!string.IsNullOrEmpty(symbol))
                {
                    condition.Append(
                        " (AND sharecode = '" + symbol + "')");
                }               

                condition.Append(" AND ");

                condition.Append("paydate BETWEEN '" + fromDate + "'" + " AND '" + toDate + "'");

                return condition.ToString();
            }
            catch (Exception exception)
            {
                LogHandler.Log(
                    "BuildXDCondition: exception, ex = " + exception,
                    this.GetType() + ".BuildXDCondition",
                    TraceEventType.Error);
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the XD.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns></returns>
        private XD GetXD(OdbcDataReader dataReader)
        {
            var xd = new XD()
                         {
                             Symbol = DaoCommon.GetFieldStringValue(dataReader, "sharecode"),
                             AmountAfterVAT = DaoCommon.GetFieldDecimalValue(dataReader, "compamt"),
                             AvaiVolume = DaoCommon.GetFieldDecimalValue(dataReader, "compunitbfxd"),
                             CloseDate = DaoCommon.GetFieldDateTimeValue(dataReader, "closedate"),
                             PayDate = DaoCommon.GetFieldDateTimeValue(dataReader, "paydate"),
                             PayRate = DaoCommon.GetFieldDecimalValue(dataReader, "payrate"),
                             Remark = Utils.DecodeUtf8(DaoCommon.GetFieldStringValue(dataReader, "remark"))
                         };

            return xd;
        }
        #endregion

        #region XR
        public List<BuyRight> GetListBuyRight(string accountNo,string tradeDate)
        {
            OdbcConnection conn = null;
            OdbcDataReader xrDataReader = null;
            try
            {
                OdbcCommand cmd;
                string cmdText = string.Format("SELECT * FROM {0} WHERE account = '{1}' AND ('{2}' BETWEEN  setfromdate AND  paydate)", Constants.XR_VIEW,accountNo,tradeDate);                                
                conn = DaoCommon.Connect();
                if (conn == null)
                {
                    LogHandler.Log(
                        "GetListBuyRight: Error to connect to informix: " + AppConfig.AliasInformix,
                        GetType() + ".GetListBuyRight",
                        TraceEventType.Error);
                    return null;
                }

                cmd = conn.CreateCommand();
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;

                // Retry out put
                xrDataReader = cmd.ExecuteReader();
                if (!xrDataReader.HasRows)
                {
                    LogHandler.Log(
                        String.Format("GetListBuyRight: {0} no list buy right ", accountNo),
                        GetType() + ".GetListBuyRight",
                        TraceEventType.Information);
                    return null;
                }

                var listBuyRight = new List<BuyRight>();

                while (xrDataReader.Read())
                {
                    var buyRight = new BuyRight();                    
                    buyRight.SubCustAccountId = accountNo;
                    buyRight.SecSymbol = DaoCommon.GetFieldStringValue(xrDataReader, "sharecode");
                    buyRight.AllowedVol = Convert.ToInt64(DaoCommon.GetFieldDecimalValue(xrDataReader, "compunitnew"));
                    //buyRight.RegisteredVol = Convert.ToInt64(DaoCommon.GetFieldDecimalValue(xrDataReader, "compunitconfirm"));
                    buyRight.Price = DaoCommon.GetFieldDecimalValue(xrDataReader, "price") / Constants.MONEY_UNIT;
                    buyRight.LastDateRegister = DaoCommon.GetFieldDateTimeValue(xrDataReader, "paydate");                    
                    listBuyRight.Add(buyRight);
                }

                DaoCommon.DisConnect(conn, xrDataReader);

                return listBuyRight;
            }
            catch (OdbcException e)
            {
                DaoCommon.DisConnect(conn, xrDataReader);
                LogHandler.Log(
                        String.Format("GetListBuyRight: {0}: {1}", accountNo, e.Message),
                        GetType() + ".GetListBuyRight",
                        TraceEventType.Error);
                return null;
            }
        }

        /// <summary>
        /// Gets the XR info.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="xType"></param>
        /// <returns></returns>
        public List<XR> GetXRInfo(string accountNo, string symbol, string fromDate, string toDate, int []xType)
        {
            OdbcConnection conn = null;
            OdbcDataReader xrDataReader = null;

            try
            {
                OdbcCommand cmd;
                string cmdText = "SELECT xtype, account, sharecode, compunitbfxr, compunitnew, " +
                    "compunitconfirm, xdate, closedate, old, new, price, transferfromdate, " +
                    "transfertodate, setfromdate, paydate, xtradedate, remark,confirmflag " +
                                 "  FROM " + Constants.XR_VIEW +
                                 BuildXRCondition(accountNo, fromDate, toDate, symbol, xType);

                conn = DaoCommon.Connect();
                if (conn == null)
                {
                    LogHandler.Log(
                        "GetXRInfo: Error to connect to informix: " + AppConfig.AliasInformix,
                        GetType() + ".GetXRInfo",
                        TraceEventType.Error);
                    return null;
                }

                cmd = conn.CreateCommand();
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;

                // Retry out put
                xrDataReader = cmd.ExecuteReader();
                if (!xrDataReader.HasRows)
                {
                    LogHandler.Log(
                        "GetXRInfo: " + accountNo + " no XR from " + fromDate +
                        " to " + toDate + ", xtype = " + xType + ", symbol = " + symbol,
                        GetType() + ".GetXRInfo",
                        TraceEventType.Information);
                    return null;
                }

                var xrs = new List<XR>();

                while (xrDataReader.Read())
                {
                    XR xd = GetXR(xrDataReader);
                    xrs.Add(xd);                   
                }

                DaoCommon.DisConnect(conn, xrDataReader);

                return xrs;
            }
            catch (OdbcException e)
            {
                DaoCommon.DisConnect(conn, xrDataReader);
                LogHandler.Log(
                        "GetXRInfo: " + accountNo + "from " + fromDate +
                        " to " + toDate + ", xtype = " + xType + ", symbol = " + symbol + ": " + e.Message,
                        GetType() + ".GetXRInfo",
                        TraceEventType.Error);
                return null;
            }
        }

        /// <summary>
        /// Builds the XR condition.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="xType">Type of the x.</param>
        /// <returns></returns>
        private string BuildXRCondition(string accountNo, string fromDate, string toDate, string symbol, int []xType)
        {
            try
            {
                var condition = new StringBuilder(500);
                condition.Append(" WHERE account = '" + accountNo + "'");                

                if(!string.IsNullOrEmpty(symbol))
                {                  
                    condition.Append(" AND sharecode = '" + symbol + "'");
                }                
                          
                if (xType.Count() > 0)
                {
                    string strStatus = xType.Aggregate(string.Empty,(current, iStatus) => current + "'" + iStatus + "',");
                    if (!string.IsNullOrEmpty(strStatus))
                    {
                        strStatus = strStatus.Substring(0, strStatus.Length - 1);                            
                        condition.Append(" AND xtype IN ("+ strStatus +") ");
                    }                    
                }                
                condition.Append(" AND closedate BETWEEN '" + fromDate + "'" + " AND '" + toDate + "'");

                return condition.ToString();
            }
            catch (Exception exception)
            {
                LogHandler.Log(
                    "BuildXRCondition: exception, ex = " + exception,
                    this.GetType() + ".BuildXRCondition",
                    TraceEventType.Error);
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the XR.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns></returns>
        private XR GetXR(OdbcDataReader dataReader)
        {           
            var xd = new XR();
            xd.Symbol = DaoCommon.GetFieldStringValue(dataReader, "sharecode");
            xd.xType = DaoCommon.GetFieldDecimalValue(dataReader, "xtype");
            xd.Remark = DaoCommon.GetFieldStringValue(dataReader, "remark");            
            xd.Price = DaoCommon.GetFieldDecimalValue(dataReader, "price");
            xd.RateRight = DaoCommon.GetFieldDecimalValue(dataReader, "new");
            xd.Right = DaoCommon.GetFieldDecimalValue(dataReader, "old");
            xd.RightVolume = DaoCommon.GetFieldDecimalValue(dataReader, "compunitnew");
            xd.SellFromDate = DaoCommon.GetFieldDateTimeValue(dataReader, "setfromdate");
            xd.SellToDate = DaoCommon.GetFieldDateTimeValue(dataReader, "paydate");
            xd.TradeDate = DaoCommon.GetFieldDateTimeValue(dataReader, "xtradedate");

            int confirmflag = DaoCommon.GetFieldIntegerValue(dataReader, "confirmflag");
            if (confirmflag==1)            
                xd.BoughtRightVolume = DaoCommon.GetFieldDecimalValue(dataReader, "compunitconfirm");                           
            else         
                xd.BoughtRightVolume = 0;                
            
            xd.TransferFromDate = DaoCommon.GetFieldDateTimeValue(dataReader, "transferfromdate");
            xd.TransferToDate = DaoCommon.GetFieldDateTimeValue(dataReader, "transfertodate");
            xd.Volume = DaoCommon.GetFieldDecimalValue(dataReader, "compunitbfxr");
            xd.VolumePaid = DaoCommon.GetFieldDecimalValue(dataReader, "compunitnew");
            xd.CloseDate = DaoCommon.GetFieldDateTimeValue(dataReader, "closedate");          
            return xd;
        }
        #endregion
       
        #region Bank Account Info
        /// <summary>
        /// Gets the bank account.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns></returns>
        private BankAccountInfo GetBankAccount(OdbcDataReader dataReader)
        {
            var bankAccountInfo = new BankAccountInfo() 
                                  {
                                        AccountNo=DaoCommon.GetFieldStringValue(dataReader,"account"),
                                        BankCode = DaoCommon.GetFieldStringValue(dataReader, "bankcode"),
                                        BankName = Utils.DecodeUtf8(DaoCommon.GetFieldStringValue(dataReader, "banktnam")),
                                        BankAccNo = DaoCommon.GetFieldStringValue(dataReader, "bankaccno"),
                                        CompAccNo = DaoCommon.GetFieldStringValue(dataReader, "compaccno"),
                                        ReceiveType =DaoCommon.GetFieldStringValue(dataReader,"receivetype"),
                                        PaymentType = DaoCommon.GetFieldStringValue(dataReader, "paymenttype")
                                  };
            return bankAccountInfo;
        }
        /// <summary>
        /// Gets the bank account info.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns></returns>
        public BankAccountInfo GetBankAccountInfo(string accountNo)
        {
            OdbcConnection conn = null;
            OdbcDataReader bankAccountInfoDataReader = null;
            try
            {
                OdbcCommand cmd;
                string cmdText = "SELECT account, bankcode, banktnam,bankaccno,compaccno,paymenttype,receivetype " +
                                 " FROM " + Constants.ACCINFO_VIEW + this.BuildBankAccountInfoCondition(accountNo);

                conn = DaoCommon.Connect();
                if (conn == null)
                {
                    LogHandler.Log(
                        "GetBankAccountInfo: Error to connect to informix: " + AppConfig.AliasInformix,
                        GetType() + ".GetBankAccountInfo",
                        TraceEventType.Error);
                    return null;
                }

                cmd = conn.CreateCommand();
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;

                // Retry out put
                bankAccountInfoDataReader = cmd.ExecuteReader();
                if (!bankAccountInfoDataReader.HasRows)
                {
                    //LogHandler.Log(
                    //    "GetBankAccountInfo: " + accountNo + " no exist in " + Constants.ACCINFO_VIEW,
                    //    ".GetBankAccountInfo",TraceEventType.Information);
                    return null;
                }

                var backAccountInfos = new BankAccountInfo();

                while (bankAccountInfoDataReader.Read())
                {
                    backAccountInfos= this.GetBankAccount(bankAccountInfoDataReader);
                    break;
                }

                DaoCommon.DisConnect(conn, bankAccountInfoDataReader);

                return backAccountInfos;
            }
            catch (OdbcException e)
            {
                DaoCommon.DisConnect(conn, bankAccountInfoDataReader);
                LogHandler.Log(
                        "GetBankAccountInfo: EXCEPTION,ex= " + e +" account no=" + accountNo ,
                        GetType() + ".GetBankAccountInfo",
                        TraceEventType.Error);
                return null;
            }
        }

        /// <summary>
        /// Builds the bank account info condition.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns></returns>
        private string BuildBankAccountInfoCondition(string accountNo)
        {
            try
            {
                var condition = new StringBuilder(500);
                condition.Append(" WHERE ");
                condition.Append("(account = '" + accountNo + "')");
                return condition.ToString();
            }
            catch (Exception exception)
            {
                
                LogHandler.Log(
                    "BuildBankAccountInfoCondition: exception, ex = " + exception,
                    this.GetType() + ".BuildBankAccountInfoCondition",
                    TraceEventType.Error);
                return string.Empty;
            }
        }
        #endregion

        #region Magin Sec
        /// <summary>
        /// Gets the magin sec info.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns></returns>
        private MaginSecInfo GetMaginSecInfo(OdbcDataReader dataReader)
        {
            var marginSecInfo = new MaginSecInfo()
                                    {
                                        SecSymbol = DaoCommon.GetFieldStringValue(dataReader, "sharecode"),
                                        FromDate = DaoCommon.GetFieldDateTimeValue(dataReader, "effdate"),
                                        ToDate = DaoCommon.GetFieldDateTimeValue(dataReader, "enddate"),
                                        IM = DaoCommon.GetFieldDecimalValue(dataReader, "margrate")
                                    };
            return marginSecInfo;
        }

        /// <summary>
        /// Builds the magin sec info condition.
        /// </summary>
        /// <param name="tradeDate">trade Date.</param>
        /// <param name="symbol">The symbol.</param>
        /// <returns></returns>
        private string BuildMaginSecInfoCondition(string tradeDate,string symbol)
        {
            try
            {
                var condition = new StringBuilder(500);
                condition.Append(" WHERE ");               
                condition.AppendFormat(" effdate<='{0}' ", DateTime.Now.ToString("yyyy/MM/dd"));
                condition.AppendFormat(" AND sharecode='{0}' order by effdate desc", symbol);                
                return condition.ToString();
            }
            catch (Exception exception)
            {

                LogHandler.Log(
                    "BuildMaginSecInfoCondition: exception, ex = " + exception,
                    this.GetType() + ".BuildMaginSecInfoCondition",
                    TraceEventType.Error);
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the list magin sec info.
        /// </summary>
        /// <param name="tradeDate">trade Date</param>
        /// <returns></returns>
        public MaginSecInfo GetMaginSecInfo(string tradeDate,string symbol)
        {
            OdbcConnection conn = null;
            OdbcDataReader dataReader = null;
            try
            {
                OdbcCommand cmd;
                string cmdText = "SELECT first 1 sharecode, effdate, enddate,margrate " +
                                " FROM " + Constants.MAGINSEC + this.BuildMaginSecInfoCondition(tradeDate,symbol);

                conn = DaoCommon.Connect();
                if (conn == null)
                {
                    LogHandler.Log(
                        "GetMaginSecInfo: Error to connect to informix: " + AppConfig.AliasInformix,
                        GetType() + ".GetMaginSecInfo",
                        TraceEventType.Error);
                    return null;
                }

                cmd = conn.CreateCommand();
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;

                // Retry out put
                dataReader = cmd.ExecuteReader();
                if (!dataReader.HasRows)
                {
                    LogHandler.Log(
                        "GetMaginSecInfo: between " + tradeDate + " no exist in " + Constants.MAGINSEC,
                        ".GetMaginSecInfo", TraceEventType.Information);
                    return null;
                }


                var maginSecInfo = new MaginSecInfo();
                while (dataReader.Read())
                {
                    maginSecInfo = this.GetMaginSecInfo(dataReader);
                    break;
                }

                DaoCommon.DisConnect(conn, dataReader);

                return maginSecInfo;
            }
            catch (OdbcException e)
            {
                DaoCommon.DisConnect(conn, dataReader);
                LogHandler.Log(
                        "GetMaginSecInfo: EXCEPTION,ex= " + e + " trade date=" + tradeDate,
                        GetType() + ".GetListMaginSecInfo",
                        TraceEventType.Error);
                return null;
            }
        }

        #endregion

        #region Capfund
        /// <summary>
        /// Gets the cap fund.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns></returns>
        private void GetCapFund(OdbcDataReader dataReader, CapFundInfo capFundInfo)
        {                   
            string name = DaoCommon.GetFieldStringValue(dataReader, "colname");
            switch (name.ToUpper())
            {
                case "CAPITALMARGFUND":
                    capFundInfo.CAPITALMARGFUND_Val = DaoCommon.GetFieldDecimalValue(dataReader, "colval");
                    break;
                case "MINEQUITY":
                    capFundInfo.MINEQUITY_Val = DaoCommon.GetFieldDecimalValue(dataReader, "colval");
                    break;
                case "CUSLOANLIMIT":
                    capFundInfo.CUSLOANLIMIT_Val = DaoCommon.GetFieldDecimalValue(dataReader, "colval");
                    break;
                case "TOTALLOANLIMIT":
                    capFundInfo.TOTALLOANLIMIT_Val = DaoCommon.GetFieldDecimalValue(dataReader, "colval");
                    break;
                case "STOCKLOANLIMIT":
                    capFundInfo.STOCKLOANLIMIT_Val = DaoCommon.GetFieldDecimalValue(dataReader, "colval");
                    break;
                default :
                    break;
            }            
        }

        /// <summary>
        /// Gets the cap fund info.
        /// </summary>
        /// <returns></returns>
        public CapFundInfo GetCapFundInfo()
        {
            OdbcConnection conn = null;
            OdbcDataReader dataReader = null;
            try
            {
                OdbcCommand cmd;
                string cmdText = "SELECT first 5 *  " +
                                " FROM " + Constants.CAPFUND + " WHERE category='CB' order by effdate desc";

                conn = DaoCommon.Connect();
                if (conn == null)
                {
                    LogHandler.Log(
                        "GetCapFundInfo: Error to connect to informix: " + AppConfig.AliasInformix,
                        GetType() + ".GetCapFundInfo",
                        TraceEventType.Error);
                    return null;
                }

                cmd = conn.CreateCommand();
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;

                // Retry out put
                dataReader = cmd.ExecuteReader();
                if (!dataReader.HasRows)
                {
                    LogHandler.Log(
                        "GetCapFundInfo: no exist in " + Constants.CAPFUND,
                        ".GetCapFundInfo", TraceEventType.Information);
                    return null;
                }


                var capFundInfo = new CapFundInfo();
                while (dataReader.Read())
                {
                    GetCapFund(dataReader,capFundInfo);                                        
                }

                DaoCommon.DisConnect(conn, dataReader);

                return capFundInfo;
            }
            catch (OdbcException e)
            {
                DaoCommon.DisConnect(conn, dataReader);
                LogHandler.Log(
                        "GetCapFundInfo: EXCEPTION,ex= " + e + " ",
                        GetType() + ".GetCapFundInfo",
                        TraceEventType.Error);
                return null;
            }
        }

        #endregion
    }
}