// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StockServices.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the StockServices type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using AccountManager.Entities;

namespace ETradeCore.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using ETradeCommon;
    using ETradeCommon.Enums;

    using DataAccess;
    using DataAccess.SqlClient;
    using Entities;

    public class StockServices
    {
        private readonly IFisCoreProvider _fisProvider = new SqlDb2Provider();

        private readonly ISbaCoreProvider _sbaProvider = new SqlInformixProvider();

        #region Stock
        /// <summary>
        /// Gets the stock balance.
        /// </summary>
        /// <param name="accountNo">
        /// The account no.
        /// </param>
        /// <param name="symbol">
        /// The symbol.
        /// </param>
        /// <param name="accountType">
        /// The account Type.
        /// </param>
        /// <returns>Available stock.</returns>
        public StockAvailable GetStockAvailable(string accountNo, string symbol, int accountType)
        {
            StockAvailable stockBalance;

            if (accountType == (int)CommonEnums.ACCOUNT_TYPE.NORMAL)
            {
                stockBalance = _fisProvider.GetStockAvailable4NormalAccount(accountNo, symbol);
            }
            else
            {
                stockBalance = _fisProvider.GetStockAvailable4MarginAccount(accountNo, symbol);
            }

            if (stockBalance == null)
            {
                LogHandler.Log(string.Format("GetStockBalance: {0} there no {1} in DB", accountNo, symbol), GetType() + ".GetStockBalance()", TraceEventType.Information);
                return null;
            }

            return stockBalance;
        }
        #endregion
      
        #region Portfolio
        /// <summary>
        /// Gets the portfolio info.
        /// </summary>
        /// <param name="accountNo">
        /// The account no.
        /// </param>
        /// <param name="accountType">
        /// The account Type.
        /// </param>
        /// <returns>Dictionary of PortfolioInfo object.</returns>
        public Dictionary<string, PortfolioInfo> GetPortfolioInfo(string accountNo, int accountType)
        {
            
            // Get portfolio information for symbols that can sell
            Dictionary<string, PortfolioInfo> returnVal=new Dictionary<string, PortfolioInfo>();
            var stockCoreServices=new StockCoreServices.StockCoreServices();
            
            returnVal = this.GetPortfolio4Account(accountNo, accountType);

            if (returnVal == null)
            {
                LogHandler.Log(
                    string.Format("GetPortfolioInfo: {0} no portfolio", accountNo),
                    GetType() + ".GetPortfolioInfo()",
                    TraceEventType.Information);
                return null;
            }
            
            return returnVal;
        }

        /// <summary>
        /// Gets list of portfolios.
        /// </summary>
        /// <param name="accountNo">
        /// The account no.
        /// </param>
        /// <param name="accountType">
        /// The account Type.
        /// </param>
        /// <returns>
        /// </returns>
        public List<string> GetListPortfolio(string accountNo, int accountType)
        {
            var returnVal = GetListPortfolio4Account(accountNo, accountType);

            return returnVal;
        }

        /// <summary>
        /// Gets the portfolio4 normal account. Only care about the stocks that can sell/transfer
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="accounttype">Account type</param>
        /// <returns></returns>
        public List<string> GetListPortfolio4Account(string accountNo, int accounttype)
        {

            // Get portfolio information for symbols that can sell
            List<Portfolio> portfolioInfos = null;

            switch (accounttype)
            {
                case (int)CommonEnums.ACCOUNT_TYPE.NORMAL:
                    portfolioInfos = this._fisProvider.GetPortfolio4NormalAccount(accountNo);
                    break;
                case (int)CommonEnums.ACCOUNT_TYPE.MARGIN:
                    portfolioInfos = this._fisProvider.GetPortfolio4MarginAccount(accountNo);
                    break;
                default:
                    break;
            }

            // Get stock due information for symbols wait to reveice or wait to send
            Dictionary<string, StockDueInfo> listStockDueInfos = _fisProvider.GetStockDue(accountNo);

            var returnVal = new Dictionary<string, PortfolioInfo>();

            if (portfolioInfos != null)
            {
                foreach (Portfolio portfolio in portfolioInfos) // sellable share and buy/sell today.
                {
                    PortfolioInfo currentportfolio;

                    // Only care 2 types of stock in this phase.
                    if (portfolio.SecType != (int)CommonEnums.SEC_TYPE.SELLABLE_SHARE &&
                        portfolio.SecType != (int)CommonEnums.SEC_TYPE.WAIT_RECV &&
                        portfolio.SecType != (int)CommonEnums.SEC_TYPE.WAIT_SEND) continue;

                    // don't care row with avilable volume = 0.
                    if (portfolio.Available <= 0) continue;


                    if (!returnVal.ContainsKey(portfolio.Symbol))
                    {
                        currentportfolio = new PortfolioInfo();
                        currentportfolio.Symbol = portfolio.Symbol;
                        returnVal.Add(portfolio.Symbol, currentportfolio);
                    }
                }// for reach
            }


            if (listStockDueInfos != null)
            {
                foreach (var stockdue in listStockDueInfos)
                {
                    if (stockdue.Value.WTR_T1 > 0 ||
                        stockdue.Value.WTR_T2 > 0 ||
                        stockdue.Value.WTR_T3 > 0)
                    {
                        string symbol = stockdue.Key;
                        PortfolioInfo currentportfolio;

                        if (!returnVal.ContainsKey(symbol))
                        {
                            currentportfolio = new PortfolioInfo();
                            currentportfolio.Symbol = symbol;
                            returnVal.Add(symbol, currentportfolio);
                        }
                    }
                }

            }

            var retlistportfolios = new List<String>();
            foreach (var portfolio in returnVal)
            {
                retlistportfolios.Add(portfolio.Key.Trim());
                retlistportfolios.Sort();
            }

            return retlistportfolios;
        }

        /// <summary>
        /// Gets the portfolio4 normal account.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="accounttype"></param>
        /// <returns>Dictionary of PortfolioInfo object.</returns>
        private Dictionary<String, PortfolioInfo> GetPortfolio4Account(string accountNo, int accounttype)
        {
            // Get portfolio information for symbols that can sell

            var service=new StockCoreServices.StockCoreServices();
            var list=service.GetStockBalaceByAccNo(accountNo);
            List<PortfolioInfo> portfolioInfos = list.Select(stockBalanceData => new PortfolioInfo
                {
                    Total = stockBalanceData.Total, Symbol = stockBalanceData.StockSymbol, SellableShare = stockBalanceData.Available,WTR_T1 = stockBalanceData.WTR_T1,WTR_T2 = stockBalanceData.WTR_T2,WTS_T1 = stockBalanceData.WTS_T1,WTS_T2 = stockBalanceData.WTS_T2,Amount = stockBalanceData.Available
                }).ToList();

            return portfolioInfos.ToDictionary(portfolioInfo => accountNo);
        }

        public List<OrderInfo> GetListOrderIntraDay(string accountNo)
        {
            List<OrderInfo> listOrderInfo = this._fisProvider.GetOrderIntraDay(accountNo, 0);
            return listOrderInfo;
        }

        /// <summary>
        /// Caculate the average price of deal with stocksymbol
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="accounttype"></param>
        /// <param name="stocksymbol"></param>
        /// <returns></returns>
        private decimal IntraDAVRPrice(string accountNo, int accounttype, string stocksymbol)
        {
            //Get list of match orders for accountNo and stock symbol by call store procedure STR_ORDER. Match order have matchvolum > 0
            //sumvalue = 0
            //sumvolume = 0
            //sumfee = 0
            //for each match order
            //get orderNo.
            //call store procedure STR_DEAL to get list of deal for this orderNo.
            //for each deal
            //add to sumvalue of dues
            //add to sumvolume of dues
            //add to sumfee for this order.only add one time.
            //end for
            //end for
            // Caculate the average price of deals: (sum value + sumfee ) / sumvolume.

            List<OrderInfo> listOrderInfo = this._fisProvider.GetOrderIntraDay(accountNo, 0);
            if (listOrderInfo != null && listOrderInfo.Count > 0)
                listOrderInfo =listOrderInfo.Where(orderInfo => orderInfo.Symbol.ToUpper().Equals(stocksymbol.ToUpper())).ToList();

            if (listOrderInfo != null)
            {
                decimal sumMatchedVolume = listOrderInfo.Sum(orderInfo => orderInfo.MatchVolume);
                decimal sumValue = 0;
                decimal sumFee = 0;

                foreach (var orderInfo in listOrderInfo)
                {
                    List<DealInfo> listDealInfor = this._fisProvider.GetDealIntraDay(orderInfo.OrderNo, 0);
                    if(listDealInfor!=null && listDealInfor.Count>0)
                    {                    
                        sumFee += listDealInfor.FirstOrDefault().SumComm;
                        sumValue += (listDealInfor.Sum(dealInfor => dealInfor.DealPrice * dealInfor.DealVolume));
                    }
                }
                if (sumMatchedVolume != 0)
                    return (sumValue + sumFee)/sumMatchedVolume;
            }
            return 0;
        }

        /// <summary>
        /// Updates the portfolio.
        /// </summary>
        /// <param name="retVal">The portfolio info.</param>
        /// <param name="stockDueInfo">The stock due info.</param>
        /// <returns></returns>
        private static void UpdateDuePortfolio(ref PortfolioInfo retVal, StockDueInfo stockDueInfo)
        {
            if (stockDueInfo != null)
            {
                   
                retVal.WTR_T1 = stockDueInfo.WTR_T1;
                retVal.WTR_T2 = stockDueInfo.WTR_T2;
                retVal.WTR_T3 = stockDueInfo.WTR_T3;
                retVal.WTS_T1 = stockDueInfo.WTS_T1;
                retVal.WTS_T2 = stockDueInfo.WTS_T2;
                retVal.WTS_T3 = stockDueInfo.WTS_T3;
                retVal.WTR_Amt_T1 = stockDueInfo.WTR_Amt_T1;
                retVal.WTR_Amt_T2 = stockDueInfo.WTR_Amt_T2;
                retVal.WTR_Amt_T3 = stockDueInfo.WTR_Amt_T3;
                retVal.DueAvgPrice = stockDueInfo.DueAvgPrice; //AVRPrice for T+1, T+2, T+3
                
            }
        }

        private static void UpdateTotalAVGPrice (ref PortfolioInfo retVal)
        {
            if (retVal == null)
                return;

            decimal totalStock = retVal.Total + retVal.WTR_T1 + retVal.WTR_T2 +
                                 retVal.WTR_T3 + retVal.WTR;

            retVal.Total = totalStock;

            if (totalStock > 0)
            {

                retVal.AvgPrice = (((retVal.AvgPrice * retVal.SellableShare) +
                                    retVal.DueAvgPrice *
                                    (retVal.WTR_T1 + retVal.WTR_T2 + retVal.WTR_T3) +
                                    retVal.WTR * retVal.WTRAVGPrice) /
                                   (totalStock));
            }
            else
            {
                retVal.AvgPrice = 0;
            }
        }

        /// <summary>
        /// Gets the portfolio direct4 normal account.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns>
        /// <para>List of portfolio objects of normal account.</para>
        /// </returns>
        public List<Portfolio> GetPortfolioDirect4NormalAccount(string accountNo)
        {
            return this._fisProvider.GetPortfolioBySql4NormalAccount(accountNo);
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
        /// <returns>
        /// <para>List of XD information.</para>
        /// </returns>
        public List<XD> GetXDInfo(string accountNo, string symbol, string fromDate, string toDate)
        {
            List<XD> listXD=_sbaProvider.GetXDInfo(accountNo, symbol, fromDate, toDate);
            if(listXD!=null)
            {
                foreach (var xd in listXD)
                {
                    xd.Amount = xd.AvaiVolume*xd.PayRate;
                    xd.VAT = xd.Amount - xd.AmountAfterVAT;
                    var computedVAT = xd.Amount*AppConfig.VAT/100;
                    if (xd.VAT != 0)
                    {
                        if (xd.VAT < (computedVAT/2))
                        {
                            xd.VAT = 0;
                        }
                        else
                        {
                            xd.VAT = computedVAT;
                        }
                    }
                }
            }            
            return listXD;
        }
        #endregion       

        #region XR
        /// <summary>
        /// Gets the XR info.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="xType">Type.</param>
        /// <returns>
        /// <para>List of XR information.</para>
        /// </returns>
        public List<XR> GetXRInfo(string accountNo, string symbol, string fromDate, string toDate, int []xType)
        {
            return _sbaProvider.GetXRInfo(accountNo, symbol, fromDate, toDate,xType);
        }

        /// <summary>
        /// Gets the list buy right.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="tradeDate">The trade date.</param>
        /// <returns></returns>
        public List<BuyRight> GetListBuyRight(string accountNo,string tradeDate)
        {
            return _sbaProvider.GetListBuyRight(accountNo, tradeDate);
        }
        #endregion
        
    }
}