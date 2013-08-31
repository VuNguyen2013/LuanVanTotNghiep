// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlDB2Provider.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the DB2Services type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCore.DataAccess.SqlClient
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Diagnostics;
    using System.Linq;

    using ETradeCommon;

    using ETradeCore.Entities;

    using ETradeCoreDB.Helper;

    public class SqlDb2Provider : BaseDao, IFisCoreProvider
    {
        #region Cash
        /// <summary>
        /// Gets the cash due.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns></returns>
        public CashDueInfo GetCashDue(string accountNo)
        {
            try
            {
                var fisDb = CreateFisDBInstance();
                DbParameter[] parameters = fisDb.CreateParametersArray(1);
                parameters[0] = fisDb.CreateParameter("@accountNo", accountNo);

                DataTable dtCashDue = fisDb.ExecuteDataTable(string.Empty, String.Format("call {0}STR_CASH_DUE_0609(?)", Constants.FISDB_INSTANCE_NAME), parameters);

                if (dtCashDue == null || dtCashDue.Rows.Count == 0)
                {
                    LogHandler.Log(
                        String.Format("GetCashDue: {0} no cash due", accountNo),
                        GetType() + ".GetCashDue()",
                        TraceEventType.Information);
                    return null;
                }

                DataRow drCashDue = dtCashDue.Rows[0];

                var cashDue = new CashDueInfo
                {
                    AMT_T1 = Math.Abs(DaoCommon.GetFieldDecimalValue(drCashDue, "AMT_T1") * Constants.MONEY_UNIT),
                    AMT_T2 = Math.Abs(DaoCommon.GetFieldDecimalValue(drCashDue, "AMT_T2") * Constants.MONEY_UNIT),
                    AMT_T3 = Math.Abs(DaoCommon.GetFieldDecimalValue(drCashDue, "AMT_T3") * Constants.MONEY_UNIT),
                    Payment = Math.Abs(DaoCommon.GetFieldDecimalValue(drCashDue, "PAYMENT") * Constants.MONEY_UNIT),
                    OverDue = Math.Abs(DaoCommon.GetFieldDecimalValue(drCashDue, "OVERDUE") * Constants.MONEY_UNIT)
                };

                return cashDue;
            }
            catch (Exception exception)
            {
                LogHandler.Log(
                    String.Format("GetCashDue: EXCEPTION, accountNo = {0}, ex = {1}", accountNo, exception),
                    GetType() + ".GetCashDue()",
                    TraceEventType.Error);
                return null;
            }
        }

        /// <summary>
        /// Gets the cash balance.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns></returns>
        public CashAvailable GetCashAvailable4NormalAccount(string accountNo)
        {
            try
            {
                var fisDb = CreateFisDBInstance();
                DbParameter[] parameters = fisDb.CreateParametersArray(2);
                parameters[0] = fisDb.CreateParameter("@accountNo", accountNo);
                parameters[1] = fisDb.CreateParameter("@page", 0);

                DataTable dtCashAvailable = fisDb.ExecuteDataTable(string.Empty, String.Format("call {0}STR_ACCOUNTINFO_0907(?,?)", Constants.FISDB_INSTANCE_NAME), parameters);

                if (dtCashAvailable == null || dtCashAvailable.Rows.Count == 0)
                {
                    LogHandler.Log(String.Format("GetCashAvailable4NormalAccount: {0} no cash available", accountNo),
                                   GetType() + ".GetCashAvailable4NormalAccount()", TraceEventType.Information);
                    return null;
                }

                DataRow drCashAvailable = dtCashAvailable.Rows[0];

                var cashAvailable = new CashAvailable();

                cashAvailable.BuyCredit = DaoCommon.GetFieldDecimalValue(drCashAvailable, "BuyCredit") * Constants.MONEY_UNIT;
                cashAvailable.WTR = DaoCommon.GetFieldDecimalValue(drCashAvailable, "TOTALSELL") * Constants.MONEY_UNIT;

                return cashAvailable;
            }
            catch (Exception exception)
            {
                LogHandler.Log(
                    String.Format("GetCashAvailable4NormalAccount: EXCEPTION, accountNo = {0}, ex = {1}", accountNo, exception),
                    GetType() + ".GetCashAvailable4NormalAccount()", TraceEventType.Error);
                return null;
            }

        }

        /// <summary>
        /// Gets the cash available for margin account.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns></returns>
        public CashBalance GetCashBalance4MarginAccount(string accountNo)
        {
            try
            {
                //Get the cash balance and today buy & today sell by the same store procedure of normal account
                var cashBalance = GetCashBalance4NormalAccount(accountNo);

                //Get the withraw by store str_account_cb
                var fisDb = CreateFisDBInstance();
                DbParameter[] parameters = fisDb.CreateParametersArray(2);
                parameters[0] = fisDb.CreateParameter("@accountNo", accountNo);
                parameters[1] = fisDb.CreateParameter("@page", 0);

                DataTable dtCashbalance = fisDb.ExecuteDataTable(string.Empty, String.Format("call {0}STR_ACCOUNTINFO_CB(?,?)", Constants.FISDB_INSTANCE_NAME), parameters);

                if (dtCashbalance == null || dtCashbalance.Rows.Count == 0)
                {
                    LogHandler.Log(string.Format("GetCashBalance4MarginAccount: {0} no cash available", accountNo),
                                   GetType() + ".GetCashBalance4MarginAccount()", TraceEventType.Information);
                    return null;
                }

                DataRow drCashBalance = dtCashbalance.Rows[0];
                cashBalance.BuyCredit = DaoCommon.GetFieldDecimalValue(drCashBalance, "PP") * Constants.MONEY_UNIT;
                cashBalance.WithDraw = DaoCommon.GetFieldDecimalValue(drCashBalance, "WITHDRAWAL") * Constants.MONEY_UNIT;               
                
                cashBalance.TotalBuy = DaoCommon.GetFieldDecimalValue(drCashBalance, "AR") * Constants.MONEY_UNIT;
                cashBalance.TotalSell = DaoCommon.GetFieldDecimalValue(drCashBalance, "AP") * Constants.MONEY_UNIT;

                cashBalance.Dept = DaoCommon.GetFieldDecimalValue(drCashBalance, "DEBT") * Constants.MONEY_UNIT;
                cashBalance.CallMargin = DaoCommon.GetFieldDecimalValue(drCashBalance, "CALL_MARGIN") * Constants.MONEY_UNIT;
                cashBalance.CallForeSell = DaoCommon.GetFieldDecimalValue(drCashBalance, "CALL_FORCE") * Constants.MONEY_UNIT;

                cashBalance.EE = DaoCommon.GetFieldDecimalValue(drCashBalance, "EE") *Constants.MONEY_UNIT;
                cashBalance.PP = DaoCommon.GetFieldDecimalValue(drCashBalance, "PP") * Constants.MONEY_UNIT;
                //IM == EE/PP)
                if (cashBalance.PP!=0)
                    cashBalance.IM = cashBalance.PP != 0 ? (cashBalance.EE / cashBalance.PP) : 0;
                else
                {
                    cashBalance.IM = 0;
                }               

                return cashBalance;
            }
            catch (Exception exception)
            {
                LogHandler.Log(
                    String.Format("GetCashBalance4MarginAccount: EXCEPTION, accountNo = {0}, ex = {1}", accountNo, exception),
                    GetType() + ".GetCashBalance4MarginAccount()", TraceEventType.Error);
                return null;
            }
        }

        /// <summary>
        /// Gets the cash balance for normal account.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns></returns>
        public CashBalance GetCashBalance4NormalAccount(string accountNo)
        {
            try
            {
                var fisDb = CreateFisDBInstance();
                DbParameter[] parameters = fisDb.CreateParametersArray(2);
                parameters[0] = fisDb.CreateParameter("@accountNo", accountNo);
                parameters[1] = fisDb.CreateParameter("@page", 0);

                DataTable dtCashbalance = fisDb.ExecuteDataTable(string.Empty, String.Format("call {0}STR_ACCOUNTINFO_0907(?,?)", Constants.FISDB_INSTANCE_NAME), parameters);

                if (dtCashbalance == null || dtCashbalance.Rows.Count == 0)
                {
                    LogHandler.Log(String.Format("GetCashBalance4NormalAccount: {0} no cash available", accountNo),
                                   GetType() + ".GetCashBalance4NormalAccount()", TraceEventType.Information);
                    return null;
                }

                DataRow drCashBalance = dtCashbalance.Rows[0];

                CashBalance cashBalance = new CashBalance();

                cashBalance.BuyCredit = DaoCommon.GetFieldDecimalValue(drCashBalance, "BUYCREDIT") * Constants.MONEY_UNIT;
                cashBalance.WithDraw = DaoCommon.GetFieldDecimalValue(drCashBalance, "BUYCREDIT") * Constants.MONEY_UNIT;
                cashBalance.TotalBuy = DaoCommon.GetFieldDecimalValue(drCashBalance, "TOTALBUY") * Constants.MONEY_UNIT;
                cashBalance.TotalSell = DaoCommon.GetFieldDecimalValue(drCashBalance, "TOTALSELL") * Constants.MONEY_UNIT;

                return cashBalance;
            }
            catch (Exception exception)
            {
                LogHandler.Log(
                    String.Format("GetCashBalance4NormalAccount: EXCEPTION, accountNo = {0}, ex = {1}", accountNo, exception),
                    GetType() + ".GetCashBalance4NormalAccount()", TraceEventType.Error);
                return null;
            }
        }

        /// <summary>
        /// Gets the cash balance4 margin account.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns></returns>
        public CashAvailable GetCashAvailable4MarginAccount(string accountNo)
        {
            try
            {
                var fisDb = CreateFisDBInstance();

                DbParameter[] parameters = fisDb.CreateParametersArray(2);
                parameters[0] = fisDb.CreateParameter("@accountNo", accountNo);
                parameters[1] = fisDb.CreateParameter("@page", 0);

                DataTable dtCashAvailable = fisDb.ExecuteDataTable(string.Empty, String.Format("call {0}STR_ACCOUNTINFO_CB(?,?)", Constants.FISDB_INSTANCE_NAME), parameters);

                if (dtCashAvailable == null || dtCashAvailable.Rows.Count == 0)
                {
                    LogHandler.Log(String.Format("GetCashAvailable4MarginAccount: {0} no cash available", accountNo),
                                   GetType() + ".GetCashAvailable4MarginAccount()", TraceEventType.Information);
                    return null;
                }

                CashAvailable cashAvailable = new CashAvailable();
                DataRow drCashAvailable = dtCashAvailable.Rows[0];
                cashAvailable.WTR = DaoCommon.GetFieldDecimalValue(drCashAvailable, "AP") * Constants.MONEY_UNIT;                
                cashAvailable.BuyCredit = DaoCommon.GetFieldDecimalValue(drCashAvailable, "PP") * Constants.MONEY_UNIT;
                cashAvailable.EE = DaoCommon.GetFieldDecimalValue(drCashAvailable, "EE") * Constants.MONEY_UNIT;
                cashAvailable.PP = DaoCommon.GetFieldDecimalValue(drCashAvailable, "PP") * Constants.MONEY_UNIT;
                //IM == EE/PP)
                if (cashAvailable.PP!=0)
                    cashAvailable.IM = cashAvailable.PP != 0 ? (cashAvailable.EE / cashAvailable.PP) : 0;
                else
                {
                    cashAvailable.IM = 0;
                }

                return cashAvailable;
            }
            catch (Exception exception)
            {
                LogHandler.Log(
                    string.Format("GetCashAvailable4MarginAccount: EXCEPTION, accountNo = {0}, ex = {1}", accountNo, exception),
                    GetType() + ".GetCashAvailable4MarginAccount()", TraceEventType.Error);
                return null;
            }
        }

        #endregion

        #region Stock
        /// <summary>
        /// Gets the stock due.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns></returns>
        public Dictionary<string, StockDueInfo> GetStockDue(string accountNo)
        {
            try
            {
                var fisDb = CreateFisDBInstance();

                DbParameter[] parameters = fisDb.CreateParametersArray(1);
                parameters[0] = fisDb.CreateParameter("@accountNo", accountNo);

                //DataTable dtStockBakance = fisDb.ExecuteDataTable(string.Empty, string.Format("call {0}STR_STOCK_DUE_1809(?)", Constants.FISDB_INSTANCE_NAME), parameters);
                DataTable dtStockBakance = fisDb.ExecuteDataTable(string.Empty, string.Format("call {0}STR_STOCK_DUE_0312(?)", Constants.FISDB_INSTANCE_NAME), parameters);
                if (dtStockBakance == null || dtStockBakance.Rows.Count == 0)
                {
                    //LogHandler.Log("GetStockDue: " + accountNo + " no stock due information", GetType() + ".GetStockDue()", TraceEventType.Information);
                    return null;
                }

                Dictionary<string, StockDueInfo> list = new Dictionary<string, StockDueInfo>();

                foreach (DataRow row in dtStockBakance.Rows)
                {
                    StockDueInfo stockDueInfo = new StockDueInfo();

                    //stockDueInfo.Symbol = DaoCommon.GetFieldStringValue(row, "FULLSYMBOL");
                    stockDueInfo.Symbol = DaoCommon.GetFieldStringValue(row, "SECSYMBOL");
                    stockDueInfo.WTR = DaoCommon.GetFieldDecimalValue(row, "WTR");
                    stockDueInfo.WTS = DaoCommon.GetFieldDecimalValue(row, "WTS");

                    stockDueInfo.WTR_T1 = DaoCommon.GetFieldDecimalValue(row, "WTR_T1");
                    stockDueInfo.WTR_T2 = DaoCommon.GetFieldDecimalValue(row, "WTR_T2");
                    stockDueInfo.WTR_T3 = DaoCommon.GetFieldDecimalValue(row, "WTR_T3");

                    stockDueInfo.WTS_T1 = DaoCommon.GetFieldDecimalValue(row, "WTS_T1");
                    stockDueInfo.WTS_T2 = DaoCommon.GetFieldDecimalValue(row, "WTS_T2");
                    stockDueInfo.WTS_T3 = DaoCommon.GetFieldDecimalValue(row, "WTS_T3");

                    stockDueInfo.NET = DaoCommon.GetFieldDecimalValue(row, "NET");
                    stockDueInfo.MarketPrice = DaoCommon.GetFieldDecimalValue(row, "MKT_VALUE");
                    stockDueInfo.DueAvgPrice = DaoCommon.GetFieldDecimalValue(row, "WTR_AVG_COST_PRICE");
                    //Add New Amout
                    stockDueInfo.WTR_Amt_T1 = DaoCommon.GetFieldDecimalValue(row, "WTR_AMT_T1");
                    stockDueInfo.WTR_Amt_T2 = DaoCommon.GetFieldDecimalValue(row, "WTR_AMT_T2");
                    stockDueInfo.WTR_Amt_T3 = DaoCommon.GetFieldDecimalValue(row, "WTR_AMT_T3");
                    if (!list.ContainsKey(stockDueInfo.Symbol) && !list.ContainsValue(stockDueInfo))
                        list.Add(stockDueInfo.Symbol, stockDueInfo);
                }

                return list;
            }
            catch (Exception exception)
            {
                LogHandler.Log(String.Format("GetStockDue: EXCEPTION, accountNo = {0}, ex = {1}", accountNo, exception),
                               GetType() + ".GetStockDue()", TraceEventType.Error);
                return null;
            }
        }

        /// <summary>
        /// Gets the stock balance.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="symbol">The symbol.</param>
        /// <returns></returns>
        public StockAvailable GetStockAvailable4NormalAccount(string accountNo, string symbol)
        {
            try
            {
                var fisDb = CreateFisDBInstance();
                accountNo = accountNo.Trim();
                symbol = symbol.Trim();

                DbParameter[] parameters = fisDb.CreateParametersArray(2);
                parameters[0] = fisDb.CreateParameter("@accountNo", accountNo);
                parameters[1] = fisDb.CreateParameter("@page", 0);

                DataTable dtStockBakance = fisDb.ExecuteDataTable(string.Empty,
                                                                  "call " + Constants.FISDB_INSTANCE_NAME +
                                                                  "STR_PORTFOLIO_E01(?,?)", parameters);

                if (dtStockBakance == null || dtStockBakance.Rows.Count == 0)
                {
                    LogHandler.Log("GetStockBalance4NormalAccount: " + accountNo + " there no " + symbol + " in portfolio", GetType() + ".GetStockBalance4NormalAccount()", TraceEventType.Information);

                    return null;
                }

                var stockBalance = new StockAvailable();

                foreach (DataRow row in dtStockBakance.Rows)
                {
                    string secSymbol = DaoCommon.GetFieldStringValue(row, "SECSYMBOL");

                    int secType = DaoCommon.GetFieldIntegerValue(row, "SECTYPE");

                    secSymbol = secSymbol.Trim();

                    if (secSymbol.Equals(symbol) && secType == (int)ETradeCommon.Enums.CommonEnums.SEC_TYPE.SELLABLE_SHARE)
                    {
                        stockBalance.AvaiVolume = DaoCommon.GetFieldDecimalValue(row, "AVAIVOLUME");
                        stockBalance.AvgPrice = DaoCommon.GetFieldDecimalValue(row, "AVGPRICE");
                        break;
                    }
                }

                return stockBalance;
            }
            catch (Exception exception)
            {
                LogHandler.Log(
                    "GetStockBalance4NormalAccount: EXCEPTION, accountNo = " + accountNo + ", symbol = " + symbol + ", ex = " +
                    exception, GetType() + ".GetStockBalance4NormalAccount()", TraceEventType.Error);
                return null;
            }
        }

        /// <summary>
        /// Gets the stock balance4 margin account.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="symbol">The symbol.</param>
        /// <returns></returns>
        public StockAvailable GetStockAvailable4MarginAccount(string accountNo, string symbol)
        {
            try
            {
                var fisDb = CreateFisDBInstance();
                accountNo = accountNo.Trim();
                symbol = symbol.Trim();

                DbParameter[] parameters = fisDb.CreateParametersArray(2);
                parameters[0] = fisDb.CreateParameter("@accountNo", accountNo);
                parameters[1] = fisDb.CreateParameter("@page", 0);
               
                DataTable dtStockBakance = fisDb.ExecuteDataTable(string.Empty,
                                                                  "call " + Constants.FISDB_INSTANCE_NAME +
                                                                  "STR_PORTFOLIO_CB(?,?)", parameters);

                if (dtStockBakance == null || dtStockBakance.Rows.Count == 0)
                {
                    LogHandler.Log(
                        string.Format("GetStockAvailable4MarginAccount: {0} there no {1} in portfolio", accountNo, symbol),
                        GetType() + ".GetStockAvailable4MarginAccount()",
                        TraceEventType.Information);

                    return null;
                }
                var stockBalance = new StockAvailable();

                foreach (DataRow row in dtStockBakance.Rows)
                {
                    string secSymbol = DaoCommon.GetFieldStringValue(row, "STOCK_SYM");
                    secSymbol = secSymbol.Trim();

                    int secType = DaoCommon.GetFieldIntegerValue(row, "STOCK_TYPE");
                    if (secSymbol.Equals(symbol) && secType == (int)ETradeCommon.Enums.CommonEnums.SEC_TYPE.SELLABLE_SHARE)
                    {
                        stockBalance.AvaiVolume = DaoCommon.GetFieldDecimalValue(row, "AVAIVOLUME");
                        stockBalance.AvgPrice = DaoCommon.GetFieldDecimalValue(row, "AVG_COST");
                        break;
                    }
                }

                return stockBalance;
            }
            catch (Exception exception)
            {
                LogHandler.Log(
                    "GetStockAvailable4MarginAccount: EXCEPTION, accountNo = " + accountNo + ", symbol = " + symbol +
                    ", ex = " + exception,
                    GetType() + ".GetStockAvailable4MarginAccount()",
                    TraceEventType.Error);
                return null;
            }
        }

        #endregion

        #region Portfolio
        /// <summary>
        /// Gets the portfolio by SQL 4 normal account.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns></returns>
        public List<Portfolio> GetPortfolioBySql4NormalAccount(string accountNo)
        {
            try
            {
                var fisDb = CreateFisDBInstance();

                string sql = string.Format(Constants.PORTFOLIO_SQL, accountNo);

                DataTable dtStockBakance = fisDb.ExecuteDataTable(string.Empty, sql, null);

                if (dtStockBakance == null || dtStockBakance.Rows.Count == 0)
                {
                    LogHandler.Log(
                        "GetPortfolioBySQL4NormalAccount: " + accountNo + " no portfolio",
                        GetType() + ".GetPortfolioBySQL4NormalAccount()",
                        TraceEventType.Information);
                    return null;
                }

                var list = new List<Portfolio>();

                foreach (DataRow row in dtStockBakance.Rows)
                {
                    Portfolio portfolioInfo = new Portfolio();

                    portfolioInfo.AvgPrice = DaoCommon.GetFieldDecimalValue(row, "AVGPRICE");
                    portfolioInfo.Symbol = DaoCommon.GetFieldStringValue(row, "SECSYMBOL");
                    portfolioInfo.Available = DaoCommon.GetFieldDecimalValue(row, "AVAIVOLUME");
                    portfolioInfo.SecType = DaoCommon.GetFieldIntegerValue(row, "SECTYPE");
                    portfolioInfo.StartPrice = DaoCommon.GetFieldDecimalValue(row, "STARTPRICE");

                    list.Add(portfolioInfo);
                }

                return list;
            }
            catch (Exception exception)
            {
                LogHandler.Log(
                    "GetPortfolioBySQL4NormalAccount: EXCEPTION, accountNo = " + accountNo + ", ex = " + exception,
                    GetType() + ".GetPortfolioBySQL4NormalAccount()",
                    TraceEventType.Error);
                return null;
            }
        }
        /// <summary>
        /// Gets the portfolio.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns></returns>
        public List<Portfolio> GetPortfolio4NormalAccount(string accountNo)
        {
            try
            {
                var fisDb = CreateFisDBInstance();

                DbParameter[] parameters = fisDb.CreateParametersArray(2);
                parameters[0] = fisDb.CreateParameter("@accountNo", accountNo);
                parameters[1] = fisDb.CreateParameter("@page", 0);

                DataTable dtStockBakance = fisDb.ExecuteDataTable(string.Empty, "call " + Constants.FISDB_INSTANCE_NAME + "STR_PORTFOLIO_E01(?,?)", parameters);

                if (dtStockBakance == null || dtStockBakance.Rows.Count == 0)
                {
                    LogHandler.Log(
                        string.Format("GetPortfolio4NormalAccount: {0} no portfolio", accountNo),
                        GetType() + ".GetPortfolio4NormalAccount()",
                        TraceEventType.Information);
                    return null;
                }

                var list = new List<Portfolio>();

                foreach (DataRow row in dtStockBakance.Rows)
                {
                    Portfolio portfolioInfo = new Portfolio();

                    portfolioInfo.AvgPrice = DaoCommon.GetFieldDecimalValue(row, "AVGPRICE");
                    portfolioInfo.Symbol = DaoCommon.GetFieldStringValue(row, "SECSYMBOL");
                    portfolioInfo.Available = DaoCommon.GetFieldDecimalValue(row, "AVAIVOLUME");
                    portfolioInfo.SecType = DaoCommon.GetFieldIntegerValue(row, "SECTYPE");
                    portfolioInfo.StartPrice = DaoCommon.GetFieldDecimalValue(row, "STARTPRICE");
                    portfolioInfo.Amount = DaoCommon.GetFieldDecimalValue(row, "AMOUNT");
                    portfolioInfo.Total = DaoCommon.GetFieldDecimalValue(row, "ACTUALVOLUME");
                    list.Add(portfolioInfo);
                }

                return list;
            }
            catch (Exception exception)
            {
                LogHandler.Log(
                    String.Format("GetPortfolio4NormalAccount: EXCEPTION, accountNo = {0}, ex = {1}", accountNo, exception),
                    GetType() + ".GetPortfolio4NormalAccount()",
                    TraceEventType.Error);
                return null;
            }
        }

        /// <summary>
        /// Gets the portfolio4 margin account.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns></returns>
        public List<Portfolio> GetPortfolio4MarginAccount(string accountNo)
        {
            try
            {
                var fisDb = CreateFisDBInstance();

                DbParameter[] parameters = fisDb.CreateParametersArray(2);
                parameters[0] = fisDb.CreateParameter("@accountNo", accountNo);
                parameters[1] = fisDb.CreateParameter("@page", 0);

                DataTable dtStockBakance = fisDb.ExecuteDataTable(string.Empty, String.Format("call {0}STR_PORTFOLIO_CB(?,?)", Constants.FISDB_INSTANCE_NAME), parameters);

                if (dtStockBakance == null || dtStockBakance.Rows.Count == 0)
                {
                    LogHandler.Log(String.Format("GetPortfolio4MarginAccount: {0} no portfolio", accountNo), GetType() + ".GetPortfolio4MarginAccount()",
                                   TraceEventType.Information);
                    return null;
                }

                List<Portfolio> list = new List<Portfolio>();

                foreach (DataRow row in dtStockBakance.Rows)
                {
                    Portfolio portfolioInfo = new Portfolio();

                    portfolioInfo.AvgPrice = DaoCommon.GetFieldDecimalValue(row, "AVG_COST");
                    portfolioInfo.Symbol = DaoCommon.GetFieldStringValue(row, "STOCK_SYM");
                    portfolioInfo.Available = DaoCommon.GetFieldDecimalValue(row, "AVAIVOLUME");
                    portfolioInfo.SecType = DaoCommon.GetFieldIntegerValue(row, "STOCK_TYPE");
                    portfolioInfo.MktValue = DaoCommon.GetFieldDecimalValue(row, "MKT_VALUE");
                    portfolioInfo.LastSale = DaoCommon.GetFieldDecimalValue(row, "LASTSALE");
                    portfolioInfo.StartPrice = DaoCommon.GetFieldDecimalValue(row, "STARTCLOSE");
                    portfolioInfo.Amount = DaoCommon.GetFieldDecimalValue(row, "AMOUNT");
                    portfolioInfo.Total = DaoCommon.GetFieldDecimalValue(row, "ACTUAL_VOL");

                    list.Add(portfolioInfo);
                }

                return list;
            }
            catch (Exception exception)
            {
                LogHandler.Log(
                    "GetPortfolio4MarginAccount: EXCEPTION, accountNo = " + accountNo + ", ex = " + exception,
                    GetType() + ".GetPortfolio4MarginAccount()", TraceEventType.Error);
                return null;
            }
        }

        #endregion

        #region Order
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
            try
            {
                if (symbol == string.Empty)
                {
                    symbol = "all";
                }

                var fisDb = CreateFisDBInstance();
                DbParameter[] parameters = fisDb.CreateParametersArray(5);
                parameters[0] = fisDb.CreateParameter("@accountNo", accountNo);
                parameters[1] = fisDb.CreateParameter("@fromDate", fromDate);
                parameters[2] = fisDb.CreateParameter("@toDate", toDate);
                parameters[3] = fisDb.CreateParameter("@symbol", symbol);
                parameters[4] = fisDb.CreateParameter("@page", pageNumber);

                var dtOrderHistory = fisDb.ExecuteDataTable(
                    string.Empty,
                    "call " + Constants.FISDB_INSTANCE_NAME + "STR_ORDER_HISTORY(?, ?, ?, ?, ?)",
                    parameters);

                if (dtOrderHistory == null || dtOrderHistory.Rows.Count == 0)
                {
                    //LogHandler.Log(
                    //    "GetOrderHistory: " + accountNo + " no order history from " + fromDate + " to " + toDate +
                    //    ", symbol = " + symbol + ", orderStatus = " + orderStatus,
                    //    GetType() + ".GetOrderHistory()",
                    //    TraceEventType.Information);
                    return null;
                }
                List<OrderHistory> listOrderHistory= (from DataRow row in dtOrderHistory.Rows                        
                        select
                            new OrderHistory
                            {
                                AccountNo = DaoCommon.GetFieldStringValue(row, "ACCOUNTNO"),
                                CancelTime = DaoCommon.GetFieldStringValue(row, "CANCELTIME"),
                                CancelVolume = DaoCommon.GetFieldDecimalValue(row, "CANCELVOLUME"),
                                Condition = DaoCommon.GetFieldStringValue(row, "CONDITION"),
                                ConditionPrice = DaoCommon.GetFieldStringValue(row, "CONDITIONPRICE"),
                                EnterId = DaoCommon.GetFieldStringValue(row, "ENTERID"),
                                MatchVolume = DaoCommon.GetFieldDecimalValue(row, "MATCHVOLUME"),
                                OrderDate = DaoCommon.GetFieldStringValue(row, "ORDERDATE"),
                                OrderNo = DaoCommon.GetFieldDecimalValue(row, "ORDERNO"),
                                OrderSeqNo = DaoCommon.GetFieldDecimalValue(row, "ORDERSEQNO"),
                                OrderStatus = DaoCommon.GetFieldStringValue(row, "ORDERSTATUS"),
                                OrderTime = DaoCommon.GetFieldStringValue(row, "ORDERTIME"),
                                OrderType = DaoCommon.GetFieldStringValue(row, "ORDERTYPE"),
                                Price = DaoCommon.GetFieldDecimalValue(row, "PRICE"),
                                ServiceType = DaoCommon.GetFieldStringValue(row, "SERVICETYPE"),
                                Side = DaoCommon.GetFieldStringValue(row, "SIDE"),
                                Symbol = DaoCommon.GetFieldStringValue(row, "SECSYMBOL"),
                                Volume = DaoCommon.GetFieldDecimalValue(row, "VOLUME")
                            }).ToList();
                listOrderHistory = listOrderHistory.OrderByDescending(n => n.OrderDate).ToList();//.OrderByDescending(n => n.OrderTime)
                return listOrderHistory;
            }
            catch (Exception exception)
            {
                LogHandler.Log(
                    "GetOrderHistory: EXCEPTION, accountNo = " + accountNo + " no order history from " + fromDate +
                    " to " + toDate +
                    ", symbol = " + symbol + ", orderStatus = " + orderStatus + ", ex = " + exception +
                    Environment.NewLine + ", StackTrace = " + exception.StackTrace,
                    GetType() + ".GetOrderHistory()",
                    TraceEventType.Error);

                return null;
            }
        }

        /// <summary>
        /// Gets the order intra day.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <returns></returns>
        public List<OrderInfo> GetOrderIntraDay(string accountNo, int pageIndex)
        {
            try
            {

                var fisDb = CreateFisDBInstance();
                DbParameter[] parameters = fisDb.CreateParametersArray(2);
                parameters[0] = fisDb.CreateParameter("@accountNo", accountNo);
                parameters[1] = fisDb.CreateParameter("@page", pageIndex);

                DataTable dtOrderInfo = fisDb.ExecuteDataTable(string.Empty,
                                                             "call " + Constants.FISDB_INSTANCE_NAME +
                                                             "STR_ORDER(?, ?)", parameters);

                if (dtOrderInfo == null || dtOrderInfo.Rows.Count == 0)
                {
                    LogHandler.Log(
                        "GetOrderIntraDay: " + accountNo + " no order intraday ",
                        GetType() + ".GetOrderIntraDay()", TraceEventType.Information);
                    return null;
                }

                List<OrderInfo> retVal = new List<OrderInfo>();

                foreach (DataRow row in dtOrderInfo.Rows)
                {
                    OrderInfo orderInfo = new OrderInfo();

                    orderInfo.AccountNo = DaoCommon.GetFieldStringValue(row, "ACCOUNTNO");
                    orderInfo.CancelTime = DaoCommon.GetFieldStringValue(row, "CANCELTIME");
                    orderInfo.CancelVolume = DaoCommon.GetFieldDecimalValue(row, "CANCELVOLUME");
                    orderInfo.Condition = DaoCommon.GetFieldStringValue(row, "CONDITION");
                    orderInfo.ConditionPrice = DaoCommon.GetFieldStringValue(row, "CONDITIONPRICE");
                    orderInfo.EnterId = DaoCommon.GetFieldStringValue(row, "ENTERID");
                    orderInfo.MatchVolume = DaoCommon.GetFieldDecimalValue(row, "MATCHVOLUME");
                    orderInfo.OrderDate = DaoCommon.GetFieldStringValue(row, "ORDERDATE");
                    orderInfo.OrderNo = DaoCommon.GetFieldDecimalValue(row, "ORDERNO");
                    orderInfo.OrderStatus = DaoCommon.GetFieldStringValue(row, "ORDERSTATUS");
                    orderInfo.OrderTime = DaoCommon.GetFieldStringValue(row, "ORDERTIME");
                    orderInfo.OrderType = DaoCommon.GetFieldStringValue(row, "ORDERTYPE");
                    orderInfo.Price = DaoCommon.GetFieldDecimalValue(row, "PRICE");
                    orderInfo.ServiceType = DaoCommon.GetFieldStringValue(row, "SERVICETYPE");
                    orderInfo.Side = DaoCommon.GetFieldStringValue(row, "SIDE");
                    orderInfo.Symbol = DaoCommon.GetFieldStringValue(row, "SECSYMBOL");
                    orderInfo.Volume = DaoCommon.GetFieldDecimalValue(row, "VOLUME");

                    retVal.Add(orderInfo);
                }

                return retVal;
            }
            catch (Exception exception)
            {
                LogHandler.Log(
                    "GetOrderIntraDay: EXCEPTION, accountNo = " + accountNo + " no order intraday " + ", ex = " + exception +
                    Environment.NewLine + ", StackTrace = " + exception.StackTrace,
                    GetType() + ".GetOrderIntraDay()", TraceEventType.Error);

                return null;
            }
        }        
        #endregion

        #region Deal
        /// <summary>
        /// Gets the deal history.
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        /// <param name="dealDate">The deal date.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public List<DealHistory> GetDealHistory(decimal orderNo, string dealDate, int page)
        {
            try
            {
                var fisDb = CreateFisDBInstance();
                DbParameter[] parameters = fisDb.CreateParametersArray(4);
                parameters[0] = fisDb.CreateParameter("@orderNo", orderNo);
                parameters[1] = fisDb.CreateParameter("@fromDate", dealDate);
                parameters[2] = fisDb.CreateParameter("@toDate", dealDate);
                parameters[3] = fisDb.CreateParameter("@page", page);

                DataTable dtDealHistory = fisDb.ExecuteDataTable(string.Empty,
                                                             "call " + Constants.FISDB_INSTANCE_NAME +
                                                             "STR_DEAL_HISTORY(?, ?, ?, ?)", parameters);

                if (dtDealHistory == null || dtDealHistory.Rows.Count == 0)
                {
                    /*
                    LogHandler.Log(
                        "GetDealHistory: " + orderNo + " not found in FISDB, dealDate = " + dealDate + ", page = " +
                        page,
                        GetType() + ".GetDealHistory()", TraceEventType.Information);
                     */
                    return null;
                }

                List<DealHistory> retVal = new List<DealHistory>();

                foreach (DataRow row in dtDealHistory.Rows)
                {
                    DealHistory dealHistory = new DealHistory();

                    dealHistory.ConfirmNo = DaoCommon.GetFieldDecimalValue(row, "CONFIRMNO");
                    dealHistory.DealDate = DaoCommon.GetFieldStringValue(row, "DEALDATE");
                    dealHistory.DealPrice = DaoCommon.GetFieldDecimalValue(row, "DEALPRICE");
                    dealHistory.DealTime = DaoCommon.GetFieldStringValue(row, "DEALTIME");
                    dealHistory.DealVolume = DaoCommon.GetFieldDecimalValue(row, "DEALVOLUME");
                    dealHistory.OrderNo = DaoCommon.GetFieldDecimalValue(row, "ORDERNO");
                    dealHistory.SumComm = DaoCommon.GetFieldDecimalValue(row, "SUMCOMM");
                    dealHistory.SumVat = DaoCommon.GetFieldDecimalValue(row, "SUMVAT");

                    retVal.Add(dealHistory);
                }

                return retVal;
            }
            catch (Exception exception)
            {
                LogHandler.Log(
                    "GetDealHistory: EXCEPTION, orderNo = " + orderNo + ", dealDate = " + dealDate
                    + ", page = " + page + ", ex = " + exception +
                    Environment.NewLine + ", StackTrace = " + exception.StackTrace,
                    GetType() + ".GetDealHistory()", TraceEventType.Error);

                return null;
            }
        }
        /// <summary>
        /// Gets the deal intra day.
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public List<DealInfo> GetDealIntraDay(decimal orderNo, int page)
        {
            try
            {
                var fisDb = CreateFisDBInstance();
                DbParameter[] parameters = fisDb.CreateParametersArray(2);
                parameters[0] = fisDb.CreateParameter("@orderNo", orderNo);
                parameters[1] = fisDb.CreateParameter("@page", page);

                DataTable dtDealIntraDay = fisDb.ExecuteDataTable(string.Empty,
                                                             "call " + Constants.FISDB_INSTANCE_NAME +
                                                             "STR_DEAL(?, ?)", parameters);

                if (dtDealIntraDay == null || dtDealIntraDay.Rows.Count == 0)
                {
                    LogHandler.Log(
                        "GetDealIntraDay: " + orderNo + " not found in FISDB, page = " +
                        page,
                        GetType() + ".GetDealIntraDay()", TraceEventType.Information);
                    return null;
                }

                List<DealInfo> retVal = new List<DealInfo>();

                foreach (DataRow row in dtDealIntraDay.Rows)
                {
                    DealInfo dealInfo = new DealInfo();

                    dealInfo.DealDate = DaoCommon.GetFieldStringValue(row, "DEALDATE");
                    dealInfo.DealPrice = DaoCommon.GetFieldDecimalValue(row, "DEALPRICE");
                    dealInfo.DealTime = DaoCommon.GetFieldStringValue(row, "DEALTIME");
                    dealInfo.DealVolume = DaoCommon.GetFieldDecimalValue(row, "DEALVOLUME");
                    dealInfo.OrderNo = DaoCommon.GetFieldDecimalValue(row, "ORDERNO");
                    dealInfo.SumComm = DaoCommon.GetFieldDecimalValue(row, "SUMCOMM");
                    dealInfo.SumVat = DaoCommon.GetFieldDecimalValue(row, "SUMVAT");
                    dealInfo.ComfirmNo = DaoCommon.GetFieldIntegerValue(row, "CONFIRMNO");

                    retVal.Add(dealInfo);
                }

                return retVal;
            }
            catch (Exception exception)
            {
                LogHandler.Log(
                    "GetDealIntraDay: EXCEPTION, orderNo = " + orderNo + ", page = " + page + ", ex = " + exception +
                    Environment.NewLine + ", StackTrace = " + exception.StackTrace,
                    GetType() + ".GetDealIntraDay()", TraceEventType.Error);

                return null;
            }
        }
        #endregion        
  
        #region Margin
        /// <summary>
        /// Gets the margin ratio.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns>The Margin Ratio Info</returns>
        public MarginRatioInfo GetMarginRatio(string accountNo)
        {
            try
            {
                var fisDb = CreateFisDBInstance();

                DbParameter[] parameters = fisDb.CreateParametersArray(2);
                parameters[0] = fisDb.CreateParameter("@accountNo", accountNo);
                parameters[1] = fisDb.CreateParameter("@page", 0);

                DataTable dtMarginRatio = fisDb.ExecuteDataTable(string.Empty, String.Format("call {0}STR_ACCOUNTINFO_CB(?,?)", Constants.FISDB_INSTANCE_NAME), parameters);

                if (dtMarginRatio == null || dtMarginRatio.Rows.Count == 0)
                {
                    LogHandler.Log(
                        String.Format("GetMarginRatio: {0} there no margin ratio ", accountNo),
                        GetType() + ".GetMarginRatio()",
                        TraceEventType.Information);

                    return null;
                }

                MarginRatioInfo marginRatioInfo = new MarginRatioInfo();
                foreach (DataRow row in dtMarginRatio.Rows)
                {
                    marginRatioInfo.accountNo = accountNo;
                    marginRatioInfo.BuyCR50Percent = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "EE_50");
                    marginRatioInfo.BuyCR60Percent = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "EE_60");
                    marginRatioInfo.BuyCR70Percent = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "EE_70");
                    marginRatioInfo.Assets = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "ASSET");
                    marginRatioInfo.MR = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "MR");
                    marginRatioInfo.CalForce = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "CALL_FORCE");
                    marginRatioInfo.Liabilities = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "LIABILITY");
                    marginRatioInfo.Buy_MR = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "BUYMR");
                    marginRatioInfo.Equity = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "EQUITY");
                    marginRatioInfo.ShortageForce = marginRatioInfo.Equity - marginRatioInfo.CalForce;
                    marginRatioInfo.Sell_MR = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "SELLMR");
                    marginRatioInfo.Call_LMV = DaoCommon.GetFieldDecimalValue(row, "BRK_CALL_LMV");
                    marginRatioInfo.Cash_BAL = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "CASH_BALANCE");
                    marginRatioInfo.EE =Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "EE");
                    marginRatioInfo.Call_SMV = DaoCommon.GetFieldDecimalValue(row, "BRK_CALL_SMV");
                    marginRatioInfo.LMV = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "LMV");
                    marginRatioInfo.PP = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "PP");
                    marginRatioInfo.Force_LMV = DaoCommon.GetFieldDecimalValue(row, "BRK_SELL_LMV");
                    marginRatioInfo.Collateral = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "COLLAT");
                    marginRatioInfo.CallMargin = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "CALL_MARGIN");
                    marginRatioInfo.Force_SMV = DaoCommon.GetFieldDecimalValue(row, "BRK_SELL_SMV");
                    marginRatioInfo.Debt = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "DEBT");
                    marginRatioInfo.ShortageCall = marginRatioInfo.Equity - marginRatioInfo.CallMargin;
                    if (marginRatioInfo.Assets != 0)
                        marginRatioInfo.MarginRatio = marginRatioInfo.Equity / marginRatioInfo.Assets;
                    else
                        marginRatioInfo.MarginRatio = 0;
                    marginRatioInfo.SMV = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "SMV");
                    marginRatioInfo.Action = DaoCommon.GetFieldStringValue(row, "ACTION");
                    marginRatioInfo.WithDrawal = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "WITHDRAWAL");
                    marginRatioInfo.AR = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "AR");
                    marginRatioInfo.AR_T1 = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "AR_T1");
                    marginRatioInfo.AR_T2 = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "AR_T2");
                    marginRatioInfo.AP = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "AP");
                    marginRatioInfo.AP_T1 = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "AP_T1");
                    marginRatioInfo.AP_T2 = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "AP_T2");
                    marginRatioInfo.BuyUnmatch = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "BUYUNMATCH");
                    marginRatioInfo.SellUnmatch = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "SELLUNMATCH");
                    marginRatioInfo.MTM_EE = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "EE_MTM");
                }

                return marginRatioInfo;
            }
            catch (Exception exception)
            {
                LogHandler.Log(
                                    String.Format("GetMarginRatio: EXCEPTION, accountNo = {0}, ex = {1}", accountNo, exception),
                                    GetType() + ".GetMarginRatio()",
                                    TraceEventType.Error);
                return null;
            }
        }


        /// <summary>
        /// Gets the margin portfolio.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns>List margin portfolio</returns>
        public List<MarginPortfolio> GetMarginPortfolio(string accountNo)
        {
            try
            {

                var fisDB = CreateFisDBInstance();

                DbParameter[] dbParameters = fisDB.CreateArrayParameters(2);
                dbParameters[0] = fisDB.CreateParameter("@accountno", accountNo);
                dbParameters[1] = fisDB.CreateParameter("@page", 0);

                DataTable dtMarginPortfolio = fisDB.ExecuteDataTable(string.Empty,
                                                                     "call " + Constants.FISDB_INSTANCE_NAME +
                                                                     "STR_PORTFOLIO_CB(?,?)", dbParameters);

                if (dtMarginPortfolio == null || dtMarginPortfolio.Rows.Count == 0)
                {
                    LogHandler.Log(
                        String.Format("GetMarginPortfolio: {0} there no portfolio ", accountNo),
                        GetType() + ".GetMarginPortfolio()",
                        TraceEventType.Information);
                    return null;
                }

                List<MarginPortfolio> listMarginPortfolio = new List<MarginPortfolio>();

                foreach (DataRow row in dtMarginPortfolio.Rows)
                {
                    MarginPortfolio marginPortfolioInfo = new MarginPortfolio();
                    marginPortfolioInfo.accountNo = accountNo;
                    marginPortfolioInfo.Stock = DaoCommon.GetFieldStringValue(row, "STOCK_SYM");
                    marginPortfolioInfo.G = DaoCommon.GetFieldStringValue(row, "GRADE");
                    marginPortfolioInfo.TY = DaoCommon.GetFieldStringValue(row, "STOCK_TYPE");
                    marginPortfolioInfo.Actual_Vol = DaoCommon.GetFieldDecimalValue(row, "ACTUAL_VOL");                    
                    marginPortfolioInfo.AVG_Cost = DaoCommon.GetFieldDecimalValue(row, "AVG_COST");
                    marginPortfolioInfo.Last = DaoCommon.GetFieldDecimalValue(row, "LASTSALE");
                    marginPortfolioInfo.MKT_Value = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "MKT_VALUE");
                    marginPortfolioInfo.MR = Constants.MONEY_UNIT * DaoCommon.GetFieldDecimalValue(row, "MR");
                    marginPortfolioInfo.Amount = Constants.MONEY_UNIT * marginPortfolioInfo.Actual_Vol * marginPortfolioInfo.AVG_Cost;
                    listMarginPortfolio.Add(marginPortfolioInfo);
                }
                return listMarginPortfolio;
            }
            catch (Exception exception)
            {
                LogHandler.Log(
                                   "GetMarginPortfolio: EXCEPTION, accountNo = " + accountNo + ", ex = " + exception,
                                   GetType() + ".GetMarginPortfolio()",
                                   TraceEventType.Error);
                return null;
            }
        }

        #endregion

        #region Market
        /// <summary>
        /// Gets the market price.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <returns></returns>
        public decimal GetMarketPrice(string symbol)
        {
            try
            {
                var fisDb = CreateFisDBInstance();

                DbParameter[] parameters = fisDb.CreateParametersArray(1);
                parameters[0] = fisDb.CreateParameter("@symbol", symbol);

                DataTable dtStockInfo = fisDb.ExecuteDataTable(string.Empty, "call " + Constants.FISDB_INSTANCE_NAME + "STR_STOCKINFO_0907(?)", parameters);

                if (dtStockInfo == null || dtStockInfo.Rows.Count == 0)
                {
                    LogHandler.Log("GetMarketPrice: " + symbol + " no exist in FISDB", GetType() + ".GetMarketPrice()", TraceEventType.Information);
                    return 0;
                }

                DataRow drStockInfo = dtStockInfo.Rows[0];

                decimal lastSale = DaoCommon.GetFieldDecimalValue(drStockInfo, "LASTSALE");

                decimal prior = DaoCommon.GetFieldDecimalValue(drStockInfo, "PRIOR");

                return lastSale == 0 ? prior : lastSale;

            }
            catch (Exception exception)
            {
                LogHandler.Log(
                    "GetMarketPrice: EXCEPTION, symbol = " + symbol + ", ex = " + exception,
                    GetType() + ".GetMarketPrice()",
                    TraceEventType.Error);
                return 0;
            }
        }
        #endregion
    }
}