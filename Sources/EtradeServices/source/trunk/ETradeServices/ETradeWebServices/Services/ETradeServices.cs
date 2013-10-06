﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ETradeServices.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   This is main service and used to call other services
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using AccountManager.Entities;
using ETradeCommon;
using ETradeCommon.Enums;
using ETradeCore.Entities;
using ETradeCore.Services;
using ETradeFinance.Entities;
using ETradeFinance.Services;
using ETradeOrders.Entities;
using ETradeOrders.Services;
using ETradeWebServices.AMServices;
using ETradeWebServices.RTServices;
using ETradeWebServices.Utils;
using RTDataServices.Entities;
using CashAdvance = ETradeCore.Entities.CashAdvance;
using System.Linq;
namespace ETradeWebServices.Services
{
    /// <summary>
    /// This is main service and used to call other services
    /// </summary>
    public class ETradeServices
    {
        /// <summary>
        /// The cash services: related all cash informations
        /// </summary>
        private readonly CashServices _cashServices = new CashServices();

        /// <summary>
        /// eTradeGW: related orders
        /// </summary>
        //private readonly ETradeGWServices.ETradeGW _eTradeGW = new ETradeGWServices.ETradeGW();

        private readonly ExecOrderService _execOrderService = new ExecOrderService();

        /// <summary>
        /// The stock services: related all stock informations
        /// </summary>
        private readonly StockServices _stockServices = new StockServices();

        /// <summary>
        /// The order history services: related all order history informations
        /// </summary>
        private readonly OrderHistoryServices _orderHistoryServices = new OrderHistoryServices();

        private readonly DealServices _dealServices = new DealServices();

        private readonly ValidateServices _validateServices = new ValidateServices();

        private readonly Service _rtServices = new Service();

        private static readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();

        private readonly CashAdvanceServices _cashAdvanceServices = new CashAdvanceServices();

        private readonly ActualTradeServices _actualTradeServices = new ActualTradeServices();

        private readonly ConditionOrderService _conditionOrderService = new ConditionOrderService();

        private readonly ConditionOrderDetailService _conditionOrderDetailService = new ConditionOrderDetailService();

        private readonly AccountManagerServices _accountManagerServices = new AccountManagerServices();

        private readonly BankServices _bankServices = new BankServices();

        private readonly  MarginServices _marginServices=new MarginServices();

        private readonly CashTransferService _cashTransferService=new CashTransferService();

        private readonly StockTransferService _stockTransferService=new StockTransferService();

        private readonly XrOrdersService _xrOrdersService=new XrOrdersService();

        private readonly OddLotOrderService _oddLotOrderService=new OddLotOrderService();

        private static bool _conditionOrderThreadRun = false;

        private List<ConditionOrderMessage> _successfulOrderHOSE = new List<ConditionOrderMessage>();

        private List<ConditionOrderMessage> _successfulOrderHNX = new List<ConditionOrderMessage>();

        private List<ConditionOrderMessage> _successfulOrderUPCOM = new List<ConditionOrderMessage>();

        public static bool _conditionOrderThreadHOSERun;
        public static bool _conditionOrderThreadHNXRun;
        public static bool _conditionOrderThreadUPCOMRun;
        private static int _hoseSuccessConditionOrderCount;
        private static int _hnxSuccessConditionOrderCount;
        private static int _upcomSuccessConditionOrderCount;
        private static int _hoseFailConditionOrderCount;
        private static int _hnxFailConditionOrderCount;
        private static int _upcomFailConditionOrderCount;
        private static int _hoseCancelledConditionOrderCount;
        private static int _hnxCancelledConditionOrderCount;
        private static int _upcomCancelledConditionOrderCount;
        /// <summary>
        /// Recovery lost packages from begin sequence to end sequence.
        /// </summary>
        /// <param name="beginSeq">Begin Sequence.</param>
        /// <param name="endSeq">End Sequence.</param>
        /// <returns>ResultObject&lt;Boolean&gt; to show recovery result.</returns>
        public ResultObject<Boolean> Recovery(int beginSeq, int endSeq)
        {
            Boolean ret = true;// _eTradeGW.Recovery(beginSeq, endSeq);

            return new ResultObject<Boolean>
            {
                ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                Result = ret,
                RetCode = CommonEnums.RET_CODE.SUCCESS
            };
        }

        /// <summary>
        /// Connect to LinkOPS.
        /// </summary>
        /// <param name="ipAddress">IP Address</param>
        /// <param name="port">Port.</param>
        /// <returns>ResultObject&lt;Boolean&gt;</returns>
        public ResultObject<Boolean> Connect(string ipAddress, string port)
        {
            Boolean ret = true;// _eTradeGW.Connect(ipAddress, port);

            return new ResultObject<Boolean>
            {
                ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                Result = ret,
                RetCode = CommonEnums.RET_CODE.SUCCESS
            };
        }

        /// <summary>
        /// Disconnect from LinkOPS.
        /// </summary>
        /// <returns>ResultObject&lt;Boolean&gt;</returns>
        public ResultObject<Boolean> Disconnect()
        {
            if (!AppConfig.CheckGWConnection)
            {
                return new ResultObject<Boolean>
                {
                    ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                    Result = true,
                    RetCode = CommonEnums.RET_CODE.SUCCESS
                };
            }
            Boolean ret = true;// _eTradeGW.Disconnect();

            return new ResultObject<Boolean>
            {
                ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                Result = ret,
                RetCode = CommonEnums.RET_CODE.SUCCESS
            };
        }

        /// <summary>
        /// Is connected to LinkOPS.
        /// </summary>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;bool&gt;</see> object contains returned code, returned message and 
        /// the result of checking connection.</para>
        /// <para>RET_CODE=SUCCESS: Checking successfully.</para>
        /// </returns>
        public ResultObject<Boolean> IsConnected()
        {
            if (!AppConfig.CheckGWConnection)
            {
                return new ResultObject<Boolean>
                {
                    ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                    Result = true,
                    RetCode = CommonEnums.RET_CODE.SUCCESS
                };
            }
            Boolean ret = true;// _eTradeGW.IsConnected();

            return new ResultObject<Boolean>
            {
                ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                Result = ret,
                RetCode = CommonEnums.RET_CODE.SUCCESS
            };
        }

        #region Cash
        /// <summary>
        /// Gets the available cash.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="accountType">Type of the account.</param>
        /// <param name="isConditionOrder">if set to <c>true</c> [is condition order].</param>
        /// <returns>
        /// 	<para>A <see cref="ResultObject{T}">ResultObject&lt;CashAvailable&gt;</see> object contains returned code, returned message and
        /// list of CashAvailable objects that contains available cash information.</para>
        /// 	<para>RET_CODE=ERROR_NOT_CASH_AVAILABLE: There is no data.</para>
        /// 	<para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// </returns>
        public ResultObject<CashAvailable> GetAvailableCash(string accountNo, int accountType,bool isConditionOrder)
        {
            CashAvailable cashAvailable = _cashServices.GetAvailableCash(accountNo, accountType,isConditionOrder);
            
            if (cashAvailable == null)
            {
                return new ResultObject<CashAvailable>
                {
                    Result = null,
                    ErrorMessage = CommonEnums.RET_CODE.ERROR_NOT_CASH_AVAILABLE.ToString(),
                    RetCode = CommonEnums.RET_CODE.ERROR_NOT_CASH_AVAILABLE
                };
            }
            /*
              CashTransferService cashTransferService = new CashTransferService();
             //total cash transfered amount in status pending & processing 
             cashAvailable.CashTransferedAmount = cashTransferService.GetTotalUnfinishedCashTransferAmount(accountNo);
             //total advance ordered amount in status pending & processing
             cashAvailable.AdvanceOrderedAmount = _validateServices.GetTotalConditionOrderMoney((char)CommonEnums.TRADE_SIDE.BUY, accountNo, -1);
             cashAvailable.BuyCredit = cashAvailable.BuyCredit - (cashAvailable.CashTransferedAmount + cashAvailable.AdvanceOrderedAmount);           
             */

            if (isConditionOrder)
            {
                //total condition order money
                cashAvailable.BuyCredit -= _validateServices.GetTotalConditionOrderMoney((char)CommonEnums.TRADE_SIDE.BUY,accountNo, -1);

                DateTime nextWorkingDay = ETradeCommon.Utils.GetNextWorkingDay(DateTime.Now, SysConfig.Holidays, SysConfig.WorkingDays);
                //cash AMT T1
                cashAvailable.Date_WTR_T1 = nextWorkingDay;

                //cash AMT T2
                nextWorkingDay = ETradeCommon.Utils.GetNextWorkingDay(nextWorkingDay, SysConfig.Holidays, SysConfig.WorkingDays);
                cashAvailable.Date_WTR_T2 = nextWorkingDay;

                //cash AMT T3
                nextWorkingDay = ETradeCommon.Utils.GetNextWorkingDay(nextWorkingDay, SysConfig.Holidays, SysConfig.WorkingDays);
                cashAvailable.Date_WTR_T3 = nextWorkingDay;

                //cash AMT
                nextWorkingDay = ETradeCommon.Utils.GetNextWorkingDay(nextWorkingDay, SysConfig.Holidays, SysConfig.WorkingDays);
                cashAvailable.Date_WTR = nextWorkingDay;                               

            }
            return new ResultObject<CashAvailable>
            {
                ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                Result = cashAvailable,
                RetCode = CommonEnums.RET_CODE.SUCCESS
            };
        }

        /// <summary>
        /// Gets the available cash.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="accountType">Type of the account.</param>
        /// <param name="tradeDate">The trade date.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="isConditionOrder">if set to <c>true</c> [is condition order].</param>
        /// <returns>
        /// 	<para>A <see cref="ResultObject{T}">ResultObject&lt;CashAvailable&gt;</see> object contains returned code, returned message and
        /// list of CashAvailable objects that contains available cash information.</para>
        /// 	<para>RET_CODE=ERROR_INVALID_DATETIME: The trade date is invalid.</para>
        /// 	<para>RET_CODE=ERROR_NOT_CASH_AVAILABLE: There is no data.</para>
        /// 	<para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// </returns>
        public ResultObject<CashAvailable> GetAvailableCash(string accountNo, int accountType,string tradeDate,string symbol,bool isConditionOrder)
        {
            if(!ETradeCommon.Utils.IsValidDate(tradeDate))
            {
                return new ResultObject<CashAvailable>
                {
                    Result = null,
                    ErrorMessage = CommonEnums.RET_CODE.ERROR_INVALID_DATETIME.ToString(),
                    RetCode = CommonEnums.RET_CODE.ERROR_INVALID_DATETIME
                };
            }

            CashAvailable cashAvailable = _cashServices.GetAvailableCash(accountNo, accountType,tradeDate,symbol,isConditionOrder );

            if (cashAvailable == null)
            {
                return new ResultObject<CashAvailable>
                {
                    Result = null,
                    ErrorMessage = CommonEnums.RET_CODE.ERROR_NOT_CASH_AVAILABLE.ToString(),
                    RetCode = CommonEnums.RET_CODE.ERROR_NOT_CASH_AVAILABLE
                };
            }
            /*
              CashTransferService cashTransferService = new CashTransferService();
             //total cash transfered amount in status pending & processing 
             cashAvailable.CashTransferedAmount = cashTransferService.GetTotalUnfinishedCashTransferAmount(accountNo);
             //total advance ordered amount in status pending & processing
             cashAvailable.AdvanceOrderedAmount = _validateServices.GetTotalConditionOrderMoney((char)CommonEnums.TRADE_SIDE.BUY, accountNo, -1);
             cashAvailable.BuyCredit = cashAvailable.BuyCredit - (cashAvailable.CashTransferedAmount + cashAvailable.AdvanceOrderedAmount);           
             */
            if (isConditionOrder)
            {
                //total condition order money
                cashAvailable.BuyCredit -= _validateServices.GetTotalConditionOrderMoney((char)CommonEnums.TRADE_SIDE.BUY, accountNo, -1);

                DateTime nextWorkingDay = ETradeCommon.Utils.GetNextWorkingDay(DateTime.Now, SysConfig.Holidays, SysConfig.WorkingDays);
                //cash AMT T1
                cashAvailable.Date_WTR_T1 = nextWorkingDay;

                //cash AMT T2
                nextWorkingDay = ETradeCommon.Utils.GetNextWorkingDay(nextWorkingDay, SysConfig.Holidays, SysConfig.WorkingDays);
                cashAvailable.Date_WTR_T2 = nextWorkingDay;

                //cash AMT T3
                nextWorkingDay = ETradeCommon.Utils.GetNextWorkingDay(nextWorkingDay, SysConfig.Holidays, SysConfig.WorkingDays);
                cashAvailable.Date_WTR_T3 = nextWorkingDay;

                //cash AMT
                nextWorkingDay = ETradeCommon.Utils.GetNextWorkingDay(nextWorkingDay, SysConfig.Holidays, SysConfig.WorkingDays);
                cashAvailable.Date_WTR = nextWorkingDay;
            }
            return new ResultObject<CashAvailable>
            {
                ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                Result = cashAvailable,
                RetCode = CommonEnums.RET_CODE.SUCCESS
            };
        }
        /// <summary>
        /// Gets the cash balance.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="accountType">Type of the account.</param>
        /// <returns>
        /// 	<para>A <see cref="ResultObject{T}">ResultObject&lt;CashBalance;&gt;</see> object contains returned code, returned message and
        /// CashAdvance object that contains cash advance information.</para>
        /// 	<para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// 	<para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// </returns>
        public ResultObject<CashBalance> GetCashBalance(string accountNo, int accountType)
        {
            CashBalance cashBalance = _cashServices.GetCashBalance(accountNo, accountType);

            if (cashBalance == null)
                return new ResultObject<CashBalance>
                {
                    ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                    Result = null,
                    RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                };

            

            if(cashBalance.BuyCredit<= 0 && cashBalance.WithDraw<= 0 && cashBalance.AMT_T1<= 0&& cashBalance.AMT_T2<= 0&& cashBalance.AMT_T3<= 0&& cashBalance.TotalBuy<= 0&& cashBalance.TotalSell<=0)
            {
                cashBalance.CanBuy = false;
                return new ResultObject<CashBalance>
                {
                    ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                    Result = cashBalance,
                    RetCode = CommonEnums.RET_CODE.SUCCESS
                };
            }

            cashBalance.CanBuy = _validateServices.CanBuy();

            return new ResultObject<CashBalance>
            {
                ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                Result = cashBalance,
                RetCode = CommonEnums.RET_CODE.SUCCESS
            };
        }
        #endregion      

        #region Stock
        /// <summary>
        /// Gets the stock balance.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="accountType">The account Type.</param>
        /// <param name="isConditionOrder">if set to <c>true</c> [is condition order].</param>
        /// <returns>
        /// 	<para>A <see cref="ResultObject{T}">ResultObject&lt;StockAvailable&gt;</see> object contains returned code, returned message and
        /// list of StockAvailable objects that contains available stock information.</para>
        /// 	<para>RET_CODE=ERROR_NOT_STOCK_AVAILABLE: There is no data.</para>
        /// 	<para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// </returns>
        public ResultObject<StockAvailable> GetAvailableStock(string accountNo, string symbol, int accountType, bool isConditionOrder)
        {
            StockAvailable stockAvailable = _stockServices.GetStockAvailable(accountNo, symbol, accountType);

            if (stockAvailable == null)
                return new ResultObject<StockAvailable>
                {
                    ErrorMessage = CommonEnums.RET_CODE.ERROR_NOT_STOCK_AVAILABLE.ToString(),
                    Result = null,
                    RetCode = CommonEnums.RET_CODE.ERROR_NOT_STOCK_AVAILABLE
                };
            /*
                        stockBalance.SecSymbol = symbol;
                        StockTransferService stockTransferService=new StockTransferService();
                        //total stock transfered amount in status pending & processing 
                        stockBalance.StockTransferedAmount = stockTransferService.GetTotalUnfinishedStockTransferAmount(accountNo,symbol);
                        //total advance ordered amount in status pending & processing
                        stockBalance.AdvanceOrderedAmount =_validateServices.GetTotalConditionOrderStock((char) CommonEnums.TRADE_SIDE.SELL, accountNo, -1, symbol);
                        stockBalance.AvaiVolume = stockBalance.AvaiVolume -(stockBalance.StockTransferedAmount + stockBalance.AdvanceOrderedAmount);
             */
            if (isConditionOrder)
            {
                Dictionary<string, PortfolioInfo> portfolioInfos = _stockServices.GetPortfolioInfo(accountNo, accountType);
                if (portfolioInfos != null)
                {
                    var portfolio = new PortfolioInfo();
                    DateTime nextWorkingDay = ETradeCommon.Utils.GetNextWorkingDay(DateTime.Now, SysConfig.Holidays, SysConfig.WorkingDays);
                    //date wtr1
                    stockAvailable.Date_WTR_T1 = ETradeCommon.Utils.GetNextWorkingDay(nextWorkingDay, SysConfig.Holidays, SysConfig.WorkingDays);
                    //date wtr2
                    nextWorkingDay = ETradeCommon.Utils.GetNextWorkingDay(stockAvailable.Date_WTR_T1, SysConfig.Holidays, SysConfig.WorkingDays);
                    stockAvailable.Date_WTR_T2 = nextWorkingDay;
                    //date wtr3
                    nextWorkingDay = ETradeCommon.Utils.GetNextWorkingDay(stockAvailable.Date_WTR_T2, SysConfig.Holidays, SysConfig.WorkingDays);
                    stockAvailable.Date_WTR_T3 = nextWorkingDay;
                    //date wtr
                    nextWorkingDay = ETradeCommon.Utils.GetNextWorkingDay(stockAvailable.Date_WTR_T3, SysConfig.Holidays, SysConfig.WorkingDays);
                    stockAvailable.Date_WTR = nextWorkingDay;
                    
                    if (portfolioInfos.TryGetValue(symbol, out portfolio))
                    {
                        //total condition order money
                        stockAvailable.AvaiVolume -= _validateServices.GetTotalConditionOrderStock((char)CommonEnums.TRADE_SIDE.SELL, accountNo, -1, symbol);

                        //Stock WTR T1
                        stockAvailable.WTR_T1 = portfolio.WTR_T1;                        
                        //Stock WTR T2                        
                        stockAvailable.WTR_T2 = portfolio.WTR_T2;
                        
                        //Stock WTR T3                        
                        stockAvailable.WTR_T3 = portfolio.WTR_T3;

                        stockAvailable.WTR_Amt_T1 = portfolio.WTR_Amt_T1;
                        stockAvailable.WTR_Amt_T2 = portfolio.WTR_Amt_T2;
                        stockAvailable.WTR_Amt_T3 = portfolio.WTR_Amt_T3;
                        
                        //Stock WTR                        
                        stockAvailable.WTR = portfolio.WTR;                        
                    }
                }
            }
            return new ResultObject<StockAvailable>
            {
                Result = stockAvailable,
                RetCode = CommonEnums.RET_CODE.SUCCESS,
                ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString()
            };
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
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;OrderHistory&gt;&gt;&gt;</see> object contains returned code, returned message and 
        /// list of OrderHistory objects.</para>
        /// <para>RET_CODE=ERROR_INVALID_DATETIME: The date is invalid.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Getting data successfully.</para>
        /// </returns>
        public ResultObject<PagingObject<List<OrderHistory>>> GetOrderHistory(string accountNo, string fromDate, string toDate, string symbol, int orderStatus, int pageNumber, int pageSize)
        {
            if(!ETradeCommon.Utils.IsValidDate(fromDate) || !ETradeCommon.Utils.IsValidDate(toDate))
            {
                return new ResultObject<PagingObject<List<OrderHistory>>>
                {
                    ErrorMessage = CommonEnums.RET_CODE.ERROR_INVALID_DATETIME.ToString(),
                    Result = null,
                    RetCode = CommonEnums.RET_CODE.ERROR_INVALID_DATETIME
                };
            }

            List<OrderHistory> list = _orderHistoryServices.GetOrderHistory(accountNo, fromDate, toDate, symbol, orderStatus, pageNumber, pageSize);

            if (list == null)
                return new ResultObject<PagingObject<List<OrderHistory>>>
                {
                    ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                    Result = null,
                    RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                };

            // Paging order history
            int startIndex;
            int count;
            if (pageNumber == 0)
            {
                startIndex = 0;
                count = list.Count;
            }
            else
            {
                startIndex = (pageNumber - 1) * pageSize;
                int remainsItemCount = list.Count - startIndex;
                count = (remainsItemCount > pageSize) ? pageSize : remainsItemCount;
            }

            var returnValue = list.GetRange(startIndex, count);

            return new ResultObject<PagingObject<List<OrderHistory>>>
            {
                ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                Result = new PagingObject<List<OrderHistory>>
                {
                    Count = list.Count,
                    Data = returnValue
                },
                RetCode = CommonEnums.RET_CODE.SUCCESS
            };
        }

        /// <summary>
        /// Gets the order history count.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="orderStatus">The order status.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;int&gt;</see> object contains returned code, returned message and 
        /// total records.</para>
        /// <para>RET_CODE=ERROR_INVALID_DATETIME: The date is invalid.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Getting data successfully.</para>
        /// </returns>
        public ResultObject<int> GetOrderHistoryCount(string accountNo, string fromDate, string toDate, string symbol, int orderStatus)
        {
             if(!ETradeCommon.Utils.IsValidDate(fromDate) || ETradeCommon.Utils.IsValidDate(toDate))
             {
                 return new ResultObject<int>
                 {
                     ErrorMessage = CommonEnums.RET_CODE.ERROR_INVALID_DATETIME.ToString(),
                     Result = 0,
                     RetCode = CommonEnums.RET_CODE.ERROR_INVALID_DATETIME
                 };
             }
            List<OrderHistory> list = _orderHistoryServices.GetOrderHistory(accountNo, fromDate, toDate, symbol, orderStatus, 0, 10);

            if (list == null)
                return new ResultObject<int>
                {
                    ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                    Result = 0,
                    RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                };

            return new ResultObject<int>
            {
                ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                Result = list.Count,
                RetCode = CommonEnums.RET_CODE.SUCCESS
            };
        }

        /// <summary>
        /// Puts the order.
        /// </summary>
        /// <param name="market">The market.</param>
        /// <param name="accountNo">The account no.</param>
        /// <param name="secSymbol">The security symbol.</param>
        /// <param name="side">Buy or sell side.</param>
        /// <param name="volume">The volume.</param>
        /// <param name="price">The price.</param>
        /// <param name="conPrice">The con price.</param>
        /// <param name="accountType">Type of the account.</param>
        /// <param name="customerType">Type of the customer.</param>
        /// <param name="subCustAccount">The sub cust account.</param>
        /// <param name="subCustAccounts">The sub cust accounts.</param>
        /// <param name="orderSource">Order source. From web or SMS.</param>
        /// <returns>
        /// <para>
        /// Result of putting order.
        /// If the RetCode is CommonEnums.RET_CODE.SUCCESS then Result of ResultObject is the order id.
        /// Otherwise, it is a reject code. Please refer to the reject code in the enum REJECT_REASON of CommonEnums.cs
        /// </para>
        /// <para>RET_CODE=ERROR_GW_NOT_CONNECTED: LinkOPS hasn't been connected.</para>
        /// <para>RET_CODE=ERROR_GW_NOT_SEND: Sending message failed.</para>
        /// <para>RET_CODE=SUCCESS: Putting order successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// <para>REJECT_REASON</para>
        /// <para>REJECT_REASON=ERROR_MARGIN_ACCOUNT_CANNOT_BUY_THAT_SYMBOL: Margin account cannot buy this symbol.</para>
        /// <para>REJECT_REASON=ERROR_OVER_LIMIT_LOAN_PER_CUSTOMER: The value of price after fee overs the limit of loan per customer.</para>
        /// <para>REJECT_REASON=ERROR_OVER_LIMIT_LOAN_PER_SECSYMBOL: The value of price after fee over the limit of loan per symbol.</para>
        /// <para>REJECT_REASON=ERROR_OVER_LIMIT_COMPANY_CAPITAL: The value of price after fee over the limit of the company capital.</para>
        /// <para>REJECT_REASON=ERROR_OVER_LIMIT_MAX_BUY: The value of price after fee over max buy of that account.</para>
        /// <para>REJECT_REASON=ERROR_OVER_LIMIT_MAX_BUY_OF_SECSYMBOL: The value of price after fee over max buy of symbol.</para>
        /// <para>REJECT_REASON=ERROR_MARKET_CLOSE: Market closed.</para>
        /// <para>REJECT_REASON=ERROR_ATO_NOT_IN_READY_AND_SESSION1: Cannot put ATO order in READY and SESSION1 session.</para>
        /// <para>REJECT_REASON=ERROR_ATC_NOT_IN_SESSION3: Cannot put ATC in SESSION3 session.</para>
        /// <para>REJECT_REASON=ERROR_PRICE_NOT_MULTIPLE_100_FOR_HOSE: Price is incorrect for HOSE market.</para>
        /// <para>REJECT_REASON=ERROR_PRICE_NOT_MULTIPLE_500_FOR_HOSE: Price is incorrect for HOSE market.</para>
        /// <para>REJECT_REASON=ERROR_PRICE_NOT_MULTIPLE_1000_FOR_HOSE: Price is incorrect for HOSE market.</para>
        /// <para>REJECT_REASON=ERROR_HNX_NOT_USE_ATO_ATC: Cannot put ATO, ATC order in HNX market.</para>
        /// <para>REJECT_REASON=ERROR_PRICE_NOT_MULTIPLE_100_FOR_HNX: Price is incorrect for HNX market.</para>
        /// <para>REJECT_REASON=ERROR_UPCOM_NOT_USE_ATO_ATC: Cannot put ATO, ATC order in UPCOM market.</para>
        /// <para>REJECT_REASON=ERROR_PRICE_NOT_MULTIPLE_100_FOR_UPCOM: Price is incorrect for UPCOM market.</para>
        /// <para>REJECT_REASON=INCORRECT_SIDE: The side is not buy or sell side.</para>
        /// <para>REJECT_REASON=INCORRECT_VOL: The volume is incorrect.</para>
        /// <para>REJECT_REASON=OVER_MAX_VOL: The volume is over max allowed volume.</para>
        /// <para>REJECT_REASON=INCORRECT_STOCK: Stock is incorrect.</para>
        /// <para>REJECT_REASON=STOCK_IS_HALT: Stock is halt.</para>
        /// <para>REJECT_REASON=PRICE_BELOW_FLOOR: Price is below floor price.</para>
        /// <para>REJECT_REASON=PRICE_ABOVE_CEILING: Price is over ceiling price.</para>
        /// <para>REJECT_REASON=NOT_BUY_SELL_THE_SAME_STOCK: Not allow to buy and sell the same stock.</para>
        /// <para>REJECT_REASON=ERROR_LOCK_ACCOUNT: Account is locked.</para>
        /// <para>REJECT_REASON=ERROR_ACCOUNT_NOT_BUY_PERMISSION: Account is not allowed to buy stocks.</para>
        /// <para>REJECT_REASON=ERROR_ACCOUNT_NOT_SELL_PERMISSION: Account is not allowed to sell stocks.</para>
        /// <para>REJECT_REASON=NOT_ENOUGH_CASH: Customer has not enough money.</para>
        /// <para>REJECT_REASON=NOT_ENOUGH_STOCK: Customer has not enough stocks.</para>
        /// <para>REJECT_REASON=OVER_REMAIN_VOLUME: Available volume is not enough.</para>
        /// <para>REJECT_REASON=IS_VALID: This is a valid order.</para>
        /// </returns>
        public ResultObject<int> PutOrder(
            int market,
            string accountNo,
            string secSymbol,
            char side,
            int volume,
            decimal price,
            char conPrice,
            int accountType,
            int customerType,
            SubCustAccount subCustAccount,
            List<string> subCustAccounts, char orderSource,char condition)
        {
            var resultObject = new ResultObject<int> { RetCode = CommonEnums.RET_CODE.FAIL, Result = -1 };

            var stockAvailable = new StockAvailable();

            decimal avgPrice = 0;
            bool isMargin = false;

            try
            {
                if (side == (char)CommonEnums.TRADE_SIDE.SELL)
                {
                    stockAvailable = _stockServices.GetStockAvailable(accountNo, secSymbol, accountType);
                    avgPrice = stockAvailable != null ? stockAvailable.AvgPrice : 0;
                }
                else
                {
                    if (side == (char)CommonEnums.TRADE_SIDE.BUY)
                    {
                         if(ETradeCommon.Utils.GetAccountType(accountNo) ==(int)CommonEnums.ACCOUNT_TYPE.MARGIN)
                         {
                            isMargin = true;
                            CommonEnums.REJECT_REASON isValidBuyMarginAccount = _validateServices.IsValidBuyMarginAccount(accountNo, secSymbol, price, volume,string.Empty,false);
                            if(isValidBuyMarginAccount!=CommonEnums.REJECT_REASON.IS_VALID)
                            {
                                return new ResultObject<int>
                                {
                                    ErrorMessage = isValidBuyMarginAccount.ToString(),
                                    Result = (int)isValidBuyMarginAccount,
                                    RetCode = CommonEnums.RET_CODE.FAIL
                                };
                            }
                         }                        
                    }
                    else
                    {
                        return new ResultObject<int>
                        {
                            ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString(),
                            Result = (int)CommonEnums.RET_CODE.SYSTEM_ERROR,
                            RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR
                        };
                    }
                }
                CommonEnums.REJECT_REASON rejectReason = _validateServices.IsValidNewOrder(
                        market, accountNo, secSymbol, side, volume, price, conPrice, accountType, customerType,
                        subCustAccount, subCustAccounts, stockAvailable, -1, isMargin, condition);

                if (rejectReason != CommonEnums.REJECT_REASON.IS_VALID)
                {
                    return new ResultObject<int>
                               {
                                   ErrorMessage = rejectReason.ToString(),
                                   Result = (int) rejectReason,
                                   RetCode = CommonEnums.RET_CODE.FAIL
                               };
                }

                var orderSession = MarketServices.GetOrderSession(market);
                int orderID;
                var execOrder=new ExecOrder();
                CommonEnums.RET_CODE ret = PutOrder(market, orderSession, accountNo, secSymbol, side, volume,
                                                              price, conPrice, avgPrice, null, out orderID,
                                                              out execOrder, orderSource, condition);

                resultObject.Result = execOrder.OrderId;
                resultObject.RetCode = ret;
                resultObject.ErrorMessage = ret.ToString();

                return resultObject;
                
            }
            catch (Exception exception)
            {
                LogHandler.Log("PutOrder Account = " + accountNo + " side = " + side + " Symbol = " + secSymbol + " price = " + price + " Exception = " + exception,
                     GetType() + ".PutOrder()",
                                         TraceEventType.Error);

                return new ResultObject<int>
                {
                    ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString(),
                    Result = (int)CommonEnums.RET_CODE.SYSTEM_ERROR,
                    RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR
                };
            }
        }

        public ResultObject<int> PutOrderTest(
            int market,
            string accountNo,
            string secSymbol,
            char side,
            int volume,
            decimal price,
            char conPrice,
            int accountType,
            int customerType,
            SubCustAccount subCustAccount,
            List<string> subCustAccounts, char orderSource,char condition)
        {
            var resultObject = new ResultObject<int> { RetCode = CommonEnums.RET_CODE.FAIL, Result = -1 };

            decimal avgPrice = 0;

            try
            {
                
                var orderSession = MarketServices.GetOrderSession(market);
                int orderID;
                ExecOrder execOrder;
                CommonEnums.RET_CODE ret = PutOrder(market, orderSession, accountNo, secSymbol, side, volume,
                                                              price, conPrice, avgPrice, null, out orderID,
                                                              out execOrder, orderSource,condition);

                resultObject.Result = orderID;
                resultObject.RetCode = ret;
                resultObject.ErrorMessage = ret.ToString();

                return resultObject;

            }
            catch (Exception exception)
            {
                LogHandler.Log("PutOrder Account = " + accountNo + " side = " + side + " Symbol = " + secSymbol + " price = " + price + " Exception = " + exception,
                     GetType() + ".PutOrder()",
                                         TraceEventType.Error);

                return new ResultObject<int>
                {
                    ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString(),
                    Result = (int)CommonEnums.RET_CODE.SYSTEM_ERROR,
                    RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR
                };
            }
        }
        public CommonEnums.RET_CODE PutOrder(int market, CommonEnums.ORDER_SESSION orderSession, string accountId, string secSymbol,
            char side, int volume, decimal price, char conPrice, decimal avgPrice, long? conditionOrderId, out int orderID, out ExecOrder outExecOrder, char orderSource, char condition)
        {
            var execOrder = new ExecOrder();
            orderID = 0;

            try
            {
                execOrder.ExecutedVol = 0;
                execOrder.NumOfMatch = 0;
                execOrder.CancelledVolume = 0;

                execOrder.Market = market.ToString();
                execOrder.MarketStatus = ((char)orderSession).ToString();
                execOrder.MessageType = Constants.DATA_NEW_ORDER;
                execOrder.SubCustAccountId = accountId;
                execOrder.SecSymbol = secSymbol;
                execOrder.Side = side.ToString();
                execOrder.Volume = volume;
                execOrder.Price = price;
                execOrder.ConPrice = conPrice.ToString();
                execOrder.TradeTime = DateTime.Now;
                execOrder.ExecTransType = (int)CommonEnums.TRANS_TYPE.TRANS_NEW;
                execOrder.OrderStatus = (int)CommonEnums.ORDER_STATUS.NEW_ORDER;
                execOrder.OrdRejReason = (int)CommonEnums.REJECT_REASON.NOTHING;
                execOrder.IsNewOrder = true;
                execOrder.ConditionOrderId = conditionOrderId;

                execOrder.AvgPrice = avgPrice;
                execOrder.Condition = condition.ToString();


                execOrder = _execOrderService.Save(execOrder); // first, save to generate the OrderId.

                //string refOrderId = EtradeGWCommonUtils.GetRefOrderID(execOrder.OrderId, AppConfig.ServiceName);
                string refOrderId = "1";
                //char orderSource = (char) EtradeGWCommonUtils.GetOrderSource(refOrderId);

                execOrder.OrderSource = orderSource.ToString();
                execOrder.RefOrderId = refOrderId;
                _execOrderService.Update(execOrder);
                orderID = execOrder.OrderId;
                outExecOrder = execOrder;
                return CommonEnums.RET_CODE.SUCCESS;
            }

            catch (Exception ex)
            {
                LogHandler.Log("put order error:" + execOrder.SubCustAccountId + " " + execOrder.Side + " " +
                        execOrder.SecSymbol + " " + execOrder.Price, "PutOrder", TraceEventType.Error);
               

                orderID = 0;
                ExceptionHandler.HandleException(ex, Constants.EXCEPTION_POLICY);
                outExecOrder = null;
                return CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }
        /// <summary>
        /// Update the order.
        /// </summary>
        /// <param name="orderIdentifier">Order identifier.</param>
        /// <param name="accountNo">The account no.</param>
        /// <param name="accountType">Type of the account.</param>
        /// <param name="newPrice">New price.</param>
        /// <param name="subCustAccount">The sub cust account.</param>
        /// <param name="subCustAccounts">The sub cust accounts.</param>
        /// <returns></returns>
        public ResultObject<int> UpdateOrder(int orderId, string accountNo, int accountType, decimal newPrice, int newVolume,int customerType, SubCustAccount subCustAccount, List<string> subCustAccounts, char condition)
        {
            var resultObject = new ResultObject<int>();
            var stockAvailable = new StockAvailable();

            //decimal avgPrice = 0;
            bool isMargin = false;

            try
            {
                var execOrder = _execOrderService.GetByOrderId(orderId);
                if (execOrder == null)
                {
                    LogHandler.Log(String.Format("ExecOrder data of orderId {0} does not exist.", orderId),
                                   GetType() + ".UpdateOrder", TraceEventType.Error);
                    resultObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
                    return resultObject;
                }
                // Check if this is a valid updated order.

                var retValidUpdatedOrder = _validateServices.IsValidUpdatedOrder(execOrder);
                if (retValidUpdatedOrder != CommonEnums.REJECT_REASON.IS_VALID)
                {
                    LogHandler.Log("Can not update order " + orderId + ", Reason " + retValidUpdatedOrder,
                                   GetType() + ".UpdateOrder", TraceEventType.Warning);
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_ALLOW;
                    return resultObject;
                }

                char side = execOrder.Side[0];
                string secSymbol = execOrder.SecSymbol;

                int executedVolume = 0;
                if (execOrder.ExecutedVol != null)
                {
                    executedVolume = (int)execOrder.ExecutedVol;
                }
                int volume = execOrder.Volume - executedVolume; // Remaining volume

                if (newVolume > volume)
                {
                    LogHandler.Log(string.Format("Can not update order {0}, reason: newvolume({1}) > remaining volume({2})", orderId, newVolume, volume), GetType() + ".UpdateOrder", TraceEventType.Error);
                }

                char conPrice = execOrder.ConPrice[0];

                if (side == (char)CommonEnums.TRADE_SIDE.SELL)
                {
                    stockAvailable = _stockServices.GetStockAvailable(accountNo, secSymbol, accountType);
                    //avgPrice = stockAvailable != null ? stockAvailable.AvgPrice : 0;
                }
                else
                {
                    if (side == (char)CommonEnums.TRADE_SIDE.BUY)
                    {
                        if (ETradeCommon.Utils.GetAccountType(accountNo) == (int)CommonEnums.ACCOUNT_TYPE.MARGIN)
                        {
                            isMargin = true;
                            CommonEnums.REJECT_REASON isValidBuyMarginAccount =
                                _validateServices.IsValidBuyMarginAccount(accountNo, secSymbol, newPrice, newVolume, string.Empty, false);
                            if (isValidBuyMarginAccount != CommonEnums.REJECT_REASON.IS_VALID)
                            {
                                resultObject.RetCode = CommonEnums.RET_CODE.FAIL;
                                resultObject.Result = (int)isValidBuyMarginAccount;
                                return resultObject;
                            }
                        }
                    }
                    else
                    {
                        resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                        return resultObject;
                    }
                }
                CommonEnums.REJECT_REASON rejectReason = _validateServices.IsValidNewOrder(int.Parse(execOrder.Market), accountNo,
                                                                                          secSymbol, side, volume,
                                                                                          newPrice, conPrice,
                                                                                          accountType,
                                                                                          customerType,
                                                                                          subCustAccount,
                                                                                          subCustAccounts,
                                                                                          stockAvailable, 
                                                                                          -1, 
                                                                                          isMargin,condition
                                                                                          );

                if (rejectReason != CommonEnums.REJECT_REASON.IS_VALID)
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.FAIL;
                    resultObject.Result = (int)rejectReason;
                    return resultObject;
                }

                CommonEnums.RET_CODE ret = UpdateOrder(execOrder, newPrice);
                resultObject.RetCode = ret;
                if (ret == CommonEnums.RET_CODE.FAIL)
                {
                    resultObject.Result = (int)CommonEnums.REJECT_REASON.UNIDENTIFIED_ERROR;
                }
                return resultObject;

            }
            catch (Exception exception)
            {
                LogHandler.Log(string.Format("Change order Account = {0} orderId = {1} new price = {2} Exception = {3}", accountNo, orderId, newPrice, exception),
                     GetType() + ".PutOrder()", TraceEventType.Error);
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                return resultObject;
            }
        }
        public CommonEnums.RET_CODE UpdateOrder(ExecOrder execOrder, decimal newPrice)
        {
            try
            {
                execOrder.MessageType = Constants.DATA_CHANGE_ORDER;
                execOrder.ChangedOrderStatus = (short)CommonEnums.CHANGED_ORDER_STATUS.PROCESSING;

                //use for test without LinkOPS connection.
                if (!AppConfig.CheckGWConnection)
                {
                    execOrder.Price = newPrice;
                    execOrder.NewPrice = newPrice;
                    execOrder.ChangedOrderStatus = (short)CommonEnums.CHANGED_ORDER_STATUS.ACCEPTED;
                    bool result = _execOrderService.Update(execOrder);
                    if (!result)
                    {
                        return CommonEnums.RET_CODE.FAIL;
                    }
                    return CommonEnums.RET_CODE.SUCCESS;
                }
                if (execOrder.FisOrderId != null)
                {
                    //Update database
                    execOrder.NewPrice = newPrice;
                    bool result = _execOrderService.Update(execOrder);
                    if (!result)
                    {
                        return CommonEnums.RET_CODE.FAIL;
                    }
                    LogHandler.Log("GW has sent the new order:" + execOrder.OrderId, "PutOrder", TraceEventType.Information);
                    return CommonEnums.RET_CODE.SUCCESS;
                }
                return CommonEnums.RET_CODE.NOT_ALLOW;
            }

            catch (Exception ex)
            {
                LogHandler.Log("Change order error:" + execOrder.OrderId + " " + newPrice, "PutOrder", TraceEventType.Error);

                ExceptionHandler.HandleException(ex, Constants.EXCEPTION_POLICY);
                return CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }
        ///<summary>
        ///</summary>
        public void StartPutConditionOrderThread()
        {
            // Start a new thread for putting condition orders at the beginning of the trading days.
            try
            {
                _hoseSuccessConditionOrderCount = 0;
                _hoseFailConditionOrderCount = 0;
                _hoseCancelledConditionOrderCount = 0;
                _hnxSuccessConditionOrderCount = 0;
                _hnxFailConditionOrderCount = 0;
                _hnxCancelledConditionOrderCount = 0;
                _upcomSuccessConditionOrderCount = 0;
                _upcomFailConditionOrderCount = 0;
                _upcomCancelledConditionOrderCount = 0;

                if (!_conditionOrderThreadHOSERun)
                {
                    _conditionOrderThreadHOSERun = true;
                    var hoseConditionOrderThread = new Thread(PutHOSEConditionOrder) { IsBackground = true };
                    hoseConditionOrderThread.Start();
                }
                if (!_conditionOrderThreadHNXRun)
                {
                    _conditionOrderThreadHNXRun = true;
                    var hnxConditionOrderThread = new Thread(PutHNXConditionOrder) { IsBackground = true };
                    hnxConditionOrderThread.Start();
                }
                if (!_conditionOrderThreadUPCOMRun)
                {
                    _conditionOrderThreadUPCOMRun = true;
                    var upcomConditionOrderThread = new Thread(PutUPCOMConditionOrder) { IsBackground = true };
                    upcomConditionOrderThread.Start();
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception, Constants.EXCEPTION_POLICY);
            }
            _conditionOrderThreadRun = false;
        }

        ///<summary>
        ///</summary>
        public void StopPuttingConditionOrderThread()
        {
            _conditionOrderThreadHOSERun = false;
            _conditionOrderThreadHNXRun = false;
            _conditionOrderThreadUPCOMRun = false;
        }

        /// <summary>
        /// Put condition orders for hose market
        /// </summary>
        public void PutHOSEConditionOrder()
        {
            PutConditionOrder((int)CommonEnums.MARKET_ID.HOSE);
            _conditionOrderThreadHOSERun = false;
        }

        /// <summary>
        /// Put condition orders for hnx market
        /// </summary>
        public void PutHNXConditionOrder()
        {
            PutConditionOrder((int)CommonEnums.MARKET_ID.HNX);
            _conditionOrderThreadHNXRun = false;
        }

        /// <summary>
        /// Put condition orders for upcom market
        /// </summary>
        public void PutUPCOMConditionOrder()
        {
            PutConditionOrder((int)CommonEnums.MARKET_ID.UPCoM);
            _conditionOrderThreadUPCOMRun = false;
        }

        /// <summary>
        /// Put condition orders for hose market
        /// </summary>
        public void PutHOSEConditionOrder(object obj, EventArgs e)
        {
            PutConditionOrder((int)CommonEnums.MARKET_ID.HOSE);
        }

        /// <summary>
        /// Put condition orders for hnx market
        /// </summary>
        public void PutHNXConditionOrder(object obj, EventArgs e)
        {
            PutConditionOrder((int)CommonEnums.MARKET_ID.HNX);
        }

        /// <summary>
        /// Put condition orders for upcom market
        /// </summary>
        public void PutUPCOMConditionOrder(object obj, EventArgs e)
        {
            PutConditionOrder((int)CommonEnums.MARKET_ID.UPCoM);
        }

        /// <summary>
        /// Get result of putting condition orders.
        /// </summary>
        /// <returns>Result of putting condition orders.</returns>
        public int[] GetAllConditionOrderResult()
        {
            var result = new int[3];
            result[0] = _hoseSuccessConditionOrderCount + _hnxSuccessConditionOrderCount +
                        _upcomSuccessConditionOrderCount;
            result[1] = _hoseFailConditionOrderCount + _hnxFailConditionOrderCount + _upcomFailConditionOrderCount;
            result[2] = _hoseCancelledConditionOrderCount + _hnxCancelledConditionOrderCount +
                        _upcomCancelledConditionOrderCount;
            return result;
        }

        /// <summary>
        /// Put condition order at the beginning of trading day
        /// </summary>
        public void PutConditionOrder(int marketId)
        {
            try
            {
                int checkTradingStateTime = int.Parse(ConfigurationManager.AppSettings["CheckTradingStateTime"]);
                //Check Session S of market status
                var tradingState = GetTradingState(marketId);
                // Loop until market is ready
                while (tradingState != (char)CommonEnums.MARKET_STATUS.READY)
                {
                    if ((tradingState != (char)CommonEnums.MARKET_STATUS.INIT_APP)
                        && (tradingState != (char)CommonEnums.MARKET_STATUS.UNVAILABLE)
                        && (tradingState != (char)CommonEnums.MARKET_STATUS.WAITING))
                    {
                        string marketName = ETradeCommon.Utils.GetMarketName(marketId);
                        LogHandler.Log(
                            "Thi truong " + marketName + " chua o trang thai san sang. Trang thai thi truong: " +
                            tradingState, GetType() + ".PutConditionOrder", TraceEventType.Error);
                        return;
                    }
                    Thread.Sleep(checkTradingStateTime);
                    tradingState = GetTradingState(marketId);
                }
                
                var todayOrderList = _conditionOrderService.GetListTodayOrders(marketId.ToString());
                LogHandler.Log("Putting condition order list " + todayOrderList.Count + " . Market " + marketId,
                               GetType() + ".PutConditionOrder()", TraceEventType.Information);
                bool isMargin = false;
                switch (marketId)
                {
                    case (int)CommonEnums.MARKET_ID.HOSE:
                        _successfulOrderHOSE = new List<ConditionOrderMessage>();
                        break;
                    case (int)CommonEnums.MARKET_ID.HNX:
                        _successfulOrderHNX = new List<ConditionOrderMessage>();
                        break;
                    case (int)CommonEnums.MARKET_ID.UPCoM:
                        _successfulOrderUPCOM = new List<ConditionOrderMessage>();
                        break;
                }
                foreach (var todayOrder in todayOrderList)
                {
                    bool conditionOrderThreadRun = false;
                    switch (marketId)
                    {
                        case (int)CommonEnums.MARKET_ID.HOSE:
                            conditionOrderThreadRun = _conditionOrderThreadHOSERun;
                            break;
                        case (int)CommonEnums.MARKET_ID.HNX:
                            conditionOrderThreadRun = _conditionOrderThreadHNXRun;
                            break;
                        case (int)CommonEnums.MARKET_ID.UPCoM:
                            conditionOrderThreadRun = _conditionOrderThreadUPCOMRun;
                            break;
                    }
                    if (conditionOrderThreadRun == false)
                    {
                        break;
                    }
                    // Validate condition order
                    var stockAvailable = new StockAvailable();
                    decimal avgPrice = 0;
                    MainCustAccount mainCustAccount = null;
                    try
                    {

                        var conditionOrderId = todayOrder.ConditionOrderId;
                        var secSymbol = todayOrder.SecSymbol;
                        var side = (todayOrder.Side)[0];
                        var price = todayOrder.Price;

                        var mainVolume = todayOrder.Volume;
                        var matchedVolume = todayOrder.MatchedVolume;
                        var volume = mainVolume - matchedVolume;//Remaining volume to put order

                        var subCustAccountId = todayOrder.SubCustAccountId;
                        var mainCustAccountId = todayOrder.MainCustAccountId;
                        var market = int.Parse(todayOrder.Market);
                        int accountType = ETradeCommon.Utils.GetAccountType(subCustAccountId);
                        var typeOfOrder = todayOrder.TypeOfCond;
                        var customerType = (int)CommonEnums.CUSTOMER_TYPE.INTERNAL;
                        var orderType = todayOrder.TypeOfCond;

                        

                        if (orderType == (short) CommonEnums.CONDITION_ORDER_TYPE.ATO)
                        {
                            price = (int)CommonEnums.COND_PRICE.ATO;
                        }
                        else if (orderType == (short)CommonEnums.CONDITION_ORDER_TYPE.ATC)
                        {
                            price = (int)CommonEnums.COND_PRICE.ATC;
                        }
                        else if (orderType == (short)CommonEnums.CONDITION_ORDER_TYPE.MP)
                        {
                            price = (int)CommonEnums.COND_PRICE.MP;
                        }
                        else if (orderType == (short)CommonEnums.CONDITION_ORDER_TYPE.MAK)
                        {
                            price = (int)CommonEnums.COND_PRICE.MAK;
                        }
                        else if (orderType == (short)CommonEnums.CONDITION_ORDER_TYPE.MOK)
                        {
                            price = (int)CommonEnums.COND_PRICE.MOK;
                        }

                        char conPrice = Constants.ORDER_TYPE_LO;
                        if (typeOfOrder == (short)CommonEnums.CONDITION_ORDER_TYPE.ATO)
                        {
                            conPrice = Constants.ORDER_TYPE_ATO;
                        }
                        else if (typeOfOrder == (short)CommonEnums.CONDITION_ORDER_TYPE.ATC)
                        {
                            conPrice = Constants.ORDER_TYPE_ATC;
                        }
                        else if (typeOfOrder == (short)CommonEnums.CONDITION_ORDER_TYPE.MP)
                        {
                            conPrice = Constants.ORDER_TYPE_MP;
                        }
                        else if (typeOfOrder == (short)CommonEnums.CONDITION_ORDER_TYPE.MAK)
                        {
                            conPrice = Constants.ORDER_TYPE_MAK;
                        }
                        else if (typeOfOrder == (short)CommonEnums.CONDITION_ORDER_TYPE.MOK)
                        {
                            conPrice = Constants.ORDER_TYPE_MOK;
                        }
                        LogHandler.Log(
                            "Putting condition order " + conditionOrderId + " " + subCustAccountId + " " + secSymbol +
                            " " + market + " " + side + " " + price + " " + volume + " " + conPrice,
                            GetType() + ".PutConditionOrder()", TraceEventType.Information);
                        // Get sub customer account information
                        var subCustAccountIdList = new List<string>();
                        SubCustAccount subCustAccount = null;
                        var strMainCustAccount = _accountManagerServices.GetCustomerNoSession(mainCustAccountId);
                        var mainResultObject = Serializer.Deserialize<ResultObject<MainCustAccount>>(strMainCustAccount);
                        mainCustAccount = mainResultObject.Result;
                        if (mainCustAccount != null)
                        {
                            customerType = mainCustAccount.CustomerType;
                            var subCustAccounts = mainCustAccount.SubCustAccountCollection;
                            if (subCustAccounts != null)
                            {
                                foreach (var tmp in subCustAccounts)
                                {
                                    subCustAccountIdList.Add(tmp.SubCustAccountId);
                                    if (tmp.SubCustAccountId == subCustAccountId)
                                    {
                                        subCustAccount = tmp;
                                    }
                                }
                            }
                        }

                        if (side == (char)CommonEnums.TRADE_SIDE.SELL)
                        {
                            stockAvailable = _stockServices.GetStockAvailable(subCustAccountId, secSymbol, accountType);

                            avgPrice = stockAvailable != null ? stockAvailable.AvgPrice : 0;
                        }
                        else
                        {
                            if (side == (char)CommonEnums.TRADE_SIDE.BUY)
                            {
                                if (ETradeCommon.Utils.GetAccountType(subCustAccountId) == (int)CommonEnums.ACCOUNT_TYPE.MARGIN)
                                {
                                    isMargin = true;
                                    CommonEnums.REJECT_REASON isValidBuyMarginAccount = _validateServices.IsValidBuyMarginAccount(subCustAccountId, secSymbol, avgPrice, volume,string.Empty,false);
                                    if (isValidBuyMarginAccount != CommonEnums.REJECT_REASON.IS_VALID)
                                    {
                                        // Send failure message
                                        if (mainCustAccount != null)
                                        {
                                            SendSMSMessageConditionOrder(mainCustAccount.MainCustAccountId, subCustAccountId, CommonEnums.REJECT_REASON.NOTHING,
                                                                         market, secSymbol, volume, price, side, mainCustAccount.LanguageId);
                                        }
                                    }
                                }
                            }
                        }

                        CommonEnums.REJECT_REASON rejectReason = _validateServices.IsValidNewOrder(
                                   market, subCustAccountId, secSymbol, side, volume, price, conPrice, accountType,
                                   customerType, subCustAccount, subCustAccountIdList, stockAvailable, conditionOrderId, 
                                   isMargin,(char)CommonEnums.CONDITION.NO_CONDITION);

                        if (rejectReason == CommonEnums.REJECT_REASON.IS_VALID)
                        {
                            var orderSession = MarketServices.GetOrderSession(market);
                            int orderID;

                            ExecOrder execOrder;
                            CommonEnums.RET_CODE ret = PutOrder(market, orderSession, subCustAccountId,
                                                                          secSymbol, side,
                                                                          volume, price, conPrice, avgPrice,
                                                                          conditionOrderId, out orderID, out execOrder,
                                                                          (char) CommonEnums.ORDER_SOURCE.FROM_WEB,(char)CommonEnums.CONDITION.NO_CONDITION);
                            if (ret == CommonEnums.RET_CODE.SUCCESS)
                            {
                                //Send success sms message
                                if (mainCustAccount != null)
                                {
                                    var conditionOrderMessage = new ConditionOrderMessage
                                                                    {
                                                                        ConditionOrderId = conditionOrderId,
                                                                        MainCustAccountId = mainCustAccount.MainCustAccountId,
                                                                        SubCustAccountId =  subCustAccountId,
                                                                        MarketId = market,
                                                                        SecSymbol = secSymbol,
                                                                        Volume = volume,
                                                                        Price = price,
                                                                        Side = side
                                                                    };
                                    switch (marketId)
                                    {
                                        case (int)CommonEnums.MARKET_ID.HOSE:
                                            _successfulOrderHOSE.Add(conditionOrderMessage);
                                            break;
                                        case (int)CommonEnums.MARKET_ID.HNX:
                                            _successfulOrderHNX.Add(conditionOrderMessage);
                                            break;
                                        case (int)CommonEnums.MARKET_ID.UPCoM:
                                            _successfulOrderUPCOM.Add(conditionOrderMessage);
                                            break;
                                    }
                                }

                                // Update condition order status
                                _conditionOrderService.UpdateConditionOrderStatus(conditionOrderId,
                                                                                  ((int)
                                                                                   CommonEnums.CONDITION_ORDER_STATUS.
                                                                                       ACTIVED).ToString(),
                                                                                  (int)
                                                                                  CommonEnums.REJECT_REASON.NOTHING);
                            } 
                            else
                            { // Failed to put order to core, unknown reason
                                switch (marketId)
                                {
                                    case (int)CommonEnums.MARKET_ID.HOSE:
                                        _hoseFailConditionOrderCount++;
                                        break;
                                    case (int)CommonEnums.MARKET_ID.HNX:
                                        _hnxFailConditionOrderCount++;
                                        break;
                                    case (int)CommonEnums.MARKET_ID.UPCoM:
                                        _upcomFailConditionOrderCount++;
                                        break;
                                }
                                //Send message for failed orders
                                if (mainCustAccount != null)
                                {
                                    SendSMSMessageConditionOrder(mainCustAccount.MainCustAccountId, subCustAccountId, CommonEnums.REJECT_REASON.UNIDENTIFIED_ERROR,
                                                                 market, secSymbol, volume, price, side, mainCustAccount.LanguageId);
                                }

                                // Update condition order status
                                _conditionOrderService.UpdateConditionOrderStatus(conditionOrderId,
                                                                                  ((int)
                                                                                   CommonEnums.CONDITION_ORDER_STATUS.
                                                                                       REJECTED).ToString(),
                                                                                  (int)
                                                                                  CommonEnums.REJECT_REASON.
                                                                                      UNIDENTIFIED_ERROR);
                            }
                            // Insert condition order detail
                            var conditionOrderDetail = new ConditionOrderDetail
                            {
                                ConditionOrderId = conditionOrderId,
                                Volume = mainVolume,
                                MatchedVolume = matchedVolume,
                                OrderStatus = execOrder.OrderStatus,
                                CreatedDateTime = DateTime.Now,
                                FisOrderId = execOrder.FisOrderId,
                                NumOfMatch = execOrder.NumOfMatch,
                                CancelledVol = execOrder.CancelledVolume

                            };
                            if (ret == CommonEnums.RET_CODE.SUCCESS)
                            {
                                conditionOrderDetail.OrdRejReason = execOrder.OrdRejReason;
                            }
                            else
                            {
                                conditionOrderDetail.OrdRejReason =
                                    (int) CommonEnums.REJECT_REASON.ERROR_PUT_ORDER_FAILED;
                            }
                            var result = _conditionOrderDetailService.Insert(conditionOrderDetail);
                            if (!result)
                            {
                                LogHandler.Log(string.Format("Insert chi tiet lenh dat cua TK {0} cho CP {1}, gia {2}, KL {3} bi loi.",
                                    subCustAccountId, secSymbol, price, volume),
                                   GetType() + ".PutConditionOrder()", TraceEventType.Error);
                            }
                        }
                        else
                        {
                            switch (marketId)
                            {
                                case (int)CommonEnums.MARKET_ID.HOSE:
                                    _hoseFailConditionOrderCount++;
                                    break;
                                case (int)CommonEnums.MARKET_ID.HNX:
                                    _hnxFailConditionOrderCount++;
                                    break;
                                case (int)CommonEnums.MARKET_ID.UPCoM:
                                    _upcomFailConditionOrderCount++;
                                    break;
                            }
                            if (mainCustAccount != null)
                            {
                                SendSMSMessageConditionOrder(mainCustAccount.MainCustAccountId, subCustAccountId, rejectReason, market, secSymbol,
                                                             volume, price, side, mainCustAccount.LanguageId);
                            }

                            // Update condition order status
                            _conditionOrderService.UpdateConditionOrderStatus(conditionOrderId,
                                                                              ((int)
                                                                               CommonEnums.CONDITION_ORDER_STATUS.
                                                                                   REJECTED).ToString(),
                                                                              (int) rejectReason);

                            // Insert condition order detail
                            var conditionOrderDetail = new ConditionOrderDetail
                            {
                                ConditionOrderId = conditionOrderId,
                                Volume = mainVolume,
                                MatchedVolume = matchedVolume,
                                OrderStatus = (short) CommonEnums.ORDER_STATUS.ORDER_REJECTED,
                                CreatedDateTime = DateTime.Now,
                                FisOrderId = 0,
                                OrdRejReason = (int) rejectReason,
                                NumOfMatch = 0,
                                CancelledVol = 0

                            };

                            var result = _conditionOrderDetailService.Insert(conditionOrderDetail);
                            if (!result)
                            {
                                LogHandler.Log(string.Format("Insert chi tiet lenh dat cua TK {0} cho CP {1}, gia {2}, KL {3} bi loi.",
                                    mainCustAccount.MainCustAccountId, secSymbol, price, volume),
                                   GetType() + ".PutConditionOrder()", TraceEventType.Error);
                            }
                            
                        }
                    }
                    catch (Exception exception)
                    {
                        switch (marketId)
                        {
                            case (int)CommonEnums.MARKET_ID.HOSE:
                                _hoseFailConditionOrderCount++;
                                break;
                            case (int)CommonEnums.MARKET_ID.HNX:
                                _hnxFailConditionOrderCount++;
                                break;
                            case (int)CommonEnums.MARKET_ID.UPCoM:
                                _upcomFailConditionOrderCount++;
                                break;
                        }
                        ExceptionHandler.HandleException(exception, Constants.EXCEPTION_POLICY);
                        // Send failure message
                        if (mainCustAccount != null)
                        {
                            LogHandler.Log(string.Format("Lenh dat cua TK {0} cho CP {1}, gia {2}, KL {3} bi loi.", 
                                       mainCustAccount.MainCustAccountId, todayOrder.SecSymbol, todayOrder.Price, todayOrder.Volume),
                                       GetType() + ".PutConditionOrder()", TraceEventType.Error);
                            SendSMSMessageConditionOrder(mainCustAccount.MainCustAccountId, todayOrder.SubCustAccountId, 
                                                         CommonEnums.REJECT_REASON.NOTHING, int.Parse(todayOrder.Market),
                                                         todayOrder.SecSymbol, todayOrder.Volume, todayOrder.Price,
                                                         (todayOrder.Side)[0], mainCustAccount.LanguageId);
                        }

                    }
                }

                // Check status and send message
                Thread.Sleep(1000 * 90); // Sleep 90 seconds

                List<ConditionOrderMessage> messageList = null;
                switch (marketId)
                {
                    case (int)CommonEnums.MARKET_ID.HOSE:
                        messageList = _successfulOrderHOSE;
                        break;
                    case (int)CommonEnums.MARKET_ID.HNX:
                        messageList = _successfulOrderHNX;
                        break;
                    case (int)CommonEnums.MARKET_ID.UPCoM:
                        messageList = _successfulOrderUPCOM;
                        break;
                }

                if (messageList != null)
                {
                    LogHandler.Log("Checking orders and send message for market " + marketId + " size " + messageList.Count, 
                        "PutConditionOrder()", TraceEventType.Information);    
                }
                
                if ((messageList != null) && (messageList.Count > 0))
                {
                    var conditionOrderIdString = messageList.Aggregate(string.Empty, (current, conditionOrderMessage) => current + "," + conditionOrderMessage.ConditionOrderId);
                    if (!string.IsNullOrEmpty(conditionOrderIdString))
                    {
                        conditionOrderIdString = conditionOrderIdString.Substring(1);
                        string whereClause = string.Format("ConditionOrderID IN ({0})", conditionOrderIdString);
                        int count;
                        var execOrders = _execOrderService.GetPaged(whereClause, string.Empty, 0, int.MaxValue, out count);
                        foreach(var execOrder in execOrders)
                        {
                            for (int i = 0; i < messageList.Count; i++)
                            {
                                var conditionOrderMessage = messageList[i];
                                string languageId = Constants.VIETNAMESE_ID;
                                try
                                {
                                    // Get language id
                                    var strMainCustAccount =
                                        _accountManagerServices.GetCustomerNoSession(
                                            conditionOrderMessage.MainCustAccountId);
                                    var mainResultObject = Serializer.Deserialize<ResultObject<MainCustAccount>>(strMainCustAccount);
                                    var mainCustAccount = mainResultObject.Result;
                                    
                                    if (mainCustAccount != null)
                                    {
                                        languageId = mainCustAccount.LanguageId;
                                    }
                                }
                                catch (Exception exception)
                                {
                                    ExceptionHandler.HandleException(exception, Constants.EXCEPTION_POLICY);
                                }

                                try
                                {
                                    if (execOrder.ConditionOrderId == conditionOrderMessage.ConditionOrderId)
                                    { // Send result messages
                                        

                                        if (execOrder.OrderStatus == (short)CommonEnums.ORDER_STATUS.ORDER_REJECTED)
                                        {
                                            CommonEnums.REJECT_REASON rejectReason =
                                                CommonEnums.REJECT_REASON.NOTHING;
                                            if (execOrder.OrdRejReason != null)
                                            {
                                                rejectReason = (CommonEnums.REJECT_REASON)execOrder.OrdRejReason;
                                            }
                                            SendSMSMessageConditionOrder(conditionOrderMessage.MainCustAccountId,
                                                                         conditionOrderMessage.SubCustAccountId, rejectReason,
                                                                         conditionOrderMessage.MarketId, conditionOrderMessage.SecSymbol,
                                                                         conditionOrderMessage.Volume, conditionOrderMessage.Price,
                                                                         conditionOrderMessage.Side, languageId);
                                            switch (marketId)
                                            {
                                                case (int)CommonEnums.MARKET_ID.HOSE:
                                                    _hoseFailConditionOrderCount++;
                                                    break;
                                                case (int)CommonEnums.MARKET_ID.HNX:
                                                    _hnxFailConditionOrderCount++;
                                                    break;
                                                case (int)CommonEnums.MARKET_ID.UPCoM:
                                                    _upcomFailConditionOrderCount++;
                                                    break;
                                            }
                                        }
                                        else if (execOrder.OrderStatus == (short)CommonEnums.ORDER_STATUS.NEW_ORDER)
                                        {
                                            switch (marketId)
                                            {
                                                case (int)CommonEnums.MARKET_ID.HOSE:
                                                    _hoseFailConditionOrderCount++;
                                                    break;
                                                case (int)CommonEnums.MARKET_ID.HNX:
                                                    _hnxFailConditionOrderCount++;
                                                    break;
                                                case (int)CommonEnums.MARKET_ID.UPCoM:
                                                    _upcomFailConditionOrderCount++;
                                                    break;
                                            }
                                            SendSMSMessageConditionOrder(conditionOrderMessage.MainCustAccountId,
                                                                         conditionOrderMessage.SubCustAccountId, CommonEnums.REJECT_REASON.NO_RESPONSE,
                                                                         conditionOrderMessage.MarketId, conditionOrderMessage.SecSymbol,
                                                                         conditionOrderMessage.Volume, conditionOrderMessage.Price,
                                                                         conditionOrderMessage.Side, languageId);
                                        }
                                        else
                                        { //success
                                            switch (marketId)
                                            {
                                                case (int)CommonEnums.MARKET_ID.HOSE:
                                                    _hoseSuccessConditionOrderCount++;
                                                    break;
                                                case (int)CommonEnums.MARKET_ID.HNX:
                                                    _hnxSuccessConditionOrderCount++;
                                                    break;
                                                case (int)CommonEnums.MARKET_ID.UPCoM:
                                                    _upcomSuccessConditionOrderCount++;
                                                    break;
                                            }
                                            SendSMSMessageConditionOrder(conditionOrderMessage.MainCustAccountId,
                                                                         conditionOrderMessage.SubCustAccountId, CommonEnums.REJECT_REASON.IS_VALID,
                                                                         conditionOrderMessage.MarketId, conditionOrderMessage.SecSymbol,
                                                                         conditionOrderMessage.Volume, conditionOrderMessage.Price,
                                                                         conditionOrderMessage.Side, languageId);
                                        }
                                        messageList.RemoveAt(i);
                                        break;
                                    }
                                }
                                catch (Exception exception)
                                {
                                    ExceptionHandler.HandleException(exception, Constants.EXCEPTION_POLICY);
                                    // Send failure message
                                    if (conditionOrderMessage != null)
                                    {
                                        LogHandler.Log(string.Format("Lenh dat cua TK {0} cho CP {1}, gia {2}, KL {3} bi loi.",
                                                   conditionOrderMessage.MainCustAccountId, conditionOrderMessage.SecSymbol, conditionOrderMessage.Price, conditionOrderMessage.Volume),
                                                   GetType() + ".PutConditionOrder()", TraceEventType.Error);
                                        SendSMSMessageConditionOrder(conditionOrderMessage.MainCustAccountId,
                                                                     conditionOrderMessage.SubCustAccountId,
                                                                     CommonEnums.REJECT_REASON.NOTHING,
                                                                     conditionOrderMessage.MarketId,
                                                                     conditionOrderMessage.SecSymbol,
                                                                     conditionOrderMessage.Volume,
                                                                     conditionOrderMessage.Price,
                                                                     conditionOrderMessage.Side, languageId);
                                    }
                                }
                            }
                        }
                        foreach (var conditionOrderMessage in messageList)
                        { // Successful order but cannot be found in Exec Order table
                            switch (marketId)
                            {
                                case (int)CommonEnums.MARKET_ID.HOSE:
                                    _hoseFailConditionOrderCount++;
                                    break;
                                case (int)CommonEnums.MARKET_ID.HNX:
                                    _hnxFailConditionOrderCount++;
                                    break;
                                case (int)CommonEnums.MARKET_ID.UPCoM:
                                    _upcomFailConditionOrderCount++;
                                    break;
                            }
                            string languageId = Constants.VIETNAMESE_ID;
                            try
                            {
                                // Get language id
                                var strMainCustAccount =
                                    _accountManagerServices.GetCustomerNoSession(
                                        conditionOrderMessage.MainCustAccountId);
                                var mainResultObject = Serializer.Deserialize<ResultObject<MainCustAccount>>(strMainCustAccount);
                                var mainCustAccount = mainResultObject.Result;

                                if (mainCustAccount != null)
                                {
                                    languageId = mainCustAccount.LanguageId;
                                }
                            }
                            catch (Exception exception)
                            {
                                ExceptionHandler.HandleException(exception, Constants.EXCEPTION_POLICY);
                            }
                            SendSMSMessageConditionOrder(conditionOrderMessage.MainCustAccountId,
                                                         conditionOrderMessage.SubCustAccountId,
                                                         CommonEnums.REJECT_REASON.NOTHING,
                                                         conditionOrderMessage.MarketId, conditionOrderMessage.SecSymbol,
                                                         conditionOrderMessage.Volume, conditionOrderMessage.Price,
                                                         conditionOrderMessage.Side, languageId);
                        }
                    }
                }
            } 
            catch(Exception ex)
            {
                ExceptionHandler.HandleException(ex, Constants.EXCEPTION_POLICY);
            }
        }

        /// <summary>
        /// Check unsent condition orders for hose market
        /// </summary>
        public void CheckHOSEConditionOrder(object obj, EventArgs e)
        {
            CheckConditionOrder((int)CommonEnums.MARKET_ID.HOSE);
        }

        /// <summary>
        /// Check unsent condition orders for hnx market
        /// </summary>
        public void CheckHNXConditionOrder(object obj, EventArgs e)
        {
            CheckConditionOrder((int)CommonEnums.MARKET_ID.HNX);
        }

        /// <summary>
        /// Check unsent condition orders for upcom market
        /// </summary>
        public void CheckUPCOMConditionOrder(object obj, EventArgs e)
        {
            CheckConditionOrder((int)CommonEnums.MARKET_ID.UPCoM);
        }

        /// <summary>
        /// Check condition orders' status.
        /// </summary>
        public void CheckConditionOrder(int marketId)
        {
            try
            {
                int checkTradingStateTime = int.Parse(ConfigurationManager.AppSettings["CheckTradingStateTime"]);

                bool conditionOrderThreadRun = false;
                switch (marketId)
                {
                    case (int)CommonEnums.MARKET_ID.HOSE:
                        conditionOrderThreadRun = _conditionOrderThreadHOSERun;
                        break;
                    case (int)CommonEnums.MARKET_ID.HNX:
                        conditionOrderThreadRun = _conditionOrderThreadHNXRun;
                        break;
                    case (int)CommonEnums.MARKET_ID.UPCoM:
                        conditionOrderThreadRun = _conditionOrderThreadUPCOMRun;
                        break;
                }

                // Loop until market is ready
                while (conditionOrderThreadRun)
                {
                    Thread.Sleep(checkTradingStateTime);
                    switch (marketId)
                    {
                        case (int)CommonEnums.MARKET_ID.HOSE:
                            conditionOrderThreadRun = _conditionOrderThreadHOSERun;
                            break;
                        case (int)CommonEnums.MARKET_ID.HNX:
                            conditionOrderThreadRun = _conditionOrderThreadHNXRun;
                            break;
                        case (int)CommonEnums.MARKET_ID.UPCoM:
                            conditionOrderThreadRun = _conditionOrderThreadUPCOMRun;
                            break;
                    }
                }

                var unsentList = _conditionOrderService.GetListTodayOrders(marketId.ToString());
                LogHandler.Log("Event to check condition order list " + unsentList.Count + " . Market " + marketId,
                               GetType() + ".CheckConditionOrder()", TraceEventType.Information);

                foreach (var todayOrder in unsentList)
                {
                    try
                    {
                        // If not putted into ExecOrder table, raise error.
                        var execOrderList = _execOrderService.GetByConditionOrderId(todayOrder.ConditionOrderId);
                        if ((execOrderList == null) || (execOrderList.Count == 0))
                        {
                            var strMainCustAccount = _accountManagerServices.GetCustomerNoSession(todayOrder.MainCustAccountId);
                            var mainResultObject = Serializer.Deserialize<ResultObject<MainCustAccount>>(strMainCustAccount);
                            var mainCustAccount = mainResultObject.Result;

                            if (mainCustAccount != null)
                            {
                                //Send message for failed orders
                                SendSMSMessageConditionOrder(mainCustAccount.MainCustAccountId, todayOrder.SubCustAccountId,
                                                             CommonEnums.REJECT_REASON.NOTHING, int.Parse(todayOrder.Market),
                                                             todayOrder.SecSymbol, todayOrder.Volume, todayOrder.Price,
                                                             todayOrder.Side[0], mainCustAccount.LanguageId);
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        ExceptionHandler.HandleException(exception, Constants.EXCEPTION_POLICY);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, Constants.EXCEPTION_POLICY);
            }
        }

        /// <summary>
        /// Send result of putting condition order to customers
        /// </summary>
        /// <param name="mainCustAccountId">Main customer account id.</param>
        /// <param name="subCustAccountId">Sub customer account id.</param>
        /// <param name="rejectReason">Reject reason or success</param>
        /// <param name="market">Market id</param>
        /// <param name="symbol">Stock symbol.</param>
        /// <param name="volume">Volume.</param>
        /// <param name="price">Stock price.</param>
        /// <param name="side">Buy or sell side.</param>
        /// <param name="languageId">Language id.</param>
        private void SendSMSMessageConditionOrder(string mainCustAccountId, string subCustAccountId, CommonEnums.REJECT_REASON rejectReason,
            int market, string symbol, int volume, decimal price, char side, string languageId)
        {
            try
            {
                CultureInfo ci = ETradeCommon.Utils.GetCultureInfo(languageId);
                string message;
                string sideString;
                if (side == (char)CommonEnums.TRADE_SIDE.SELL)
                {
                    sideString = ((string)HttpContext.GetGlobalResourceObject("Resource", "SELL", ci)).ToUpper();
                }
                else
                {
                    sideString = ((string)HttpContext.GetGlobalResourceObject("Resource", "BUY", ci)).ToUpper();
                }
                string strPrice = price.ToString();
                if (price == (int)CommonEnums.COND_PRICE.ATO)
                {
                    strPrice = "ATO";
                }
                else if (price == (int)CommonEnums.COND_PRICE.ATC)
                {
                    strPrice = "ATC";
                }
                else if (price == (int)CommonEnums.COND_PRICE.MP)
                {
                    strPrice = "MP";
                }
                else if (price == (int)CommonEnums.COND_PRICE.MAK)
                {
                    strPrice = "MAK";
                }
                else if (price == (int)CommonEnums.COND_PRICE.MOK)
                {
                    strPrice = "MOK";
                }

                string accountMessage = ETradeCommon.Utils.CreateAccountMessage(mainCustAccountId, subCustAccountId, languageId);
                if (rejectReason == CommonEnums.REJECT_REASON.IS_VALID)
                {
                    message = string.Format(((string)HttpContext.GetGlobalResourceObject("Resource", "PUT_ORDER_SUCCESS", ci)), accountMessage, sideString, symbol, volume, strPrice);
                }
                else if (rejectReason == CommonEnums.REJECT_REASON.NO_RESPONSE)
                {
                    message = string.Format(((string)HttpContext.GetGlobalResourceObject("Resource", "PUT_ORDER_NO_RESPONSE", ci)), accountMessage, sideString, symbol, volume, strPrice);
                }
                else
                {
                    message = string.Format(((string)HttpContext.GetGlobalResourceObject("Resource", "PUT_ORDER_UNSUCCESS", ci)), accountMessage, sideString, symbol, volume, strPrice);
                }
                switch (rejectReason)
                {

                    case CommonEnums.REJECT_REASON.IS_VALID:
                        // Lenh dat truoc cua quy khach da duoc dat thanh cong.
                        break;
                    //case CommonEnums.REJECT_REASON.MP_WITHOUT_CONTRA_SIDE:
                    //case CommonEnums.REJECT_REASON.ILLEGAL_PRICE_SPREAD:
                    case CommonEnums.REJECT_REASON.INCORRECT_VOL:
                        // Incorrect market volume
                        message = message + " " + string.Format(((string)HttpContext.GetGlobalResourceObject("Resource", "INCORRECT_VOL", ci)), volume);
                        break;
                    //case CommonEnums.REJECT_REASON.MARKET_CLOSE:
                    case CommonEnums.REJECT_REASON.INCORRECT_STOCK:
                        // Stock is not exist
                        message = message + " " + string.Format(((string)HttpContext.GetGlobalResourceObject("Resource", "INCORRECT_STOCK", ci)), symbol);
                        break;
                    //case CommonEnums.REJECT_REASON.INCORRECT_FIRM:
                    //case CommonEnums.REJECT_REASON.INCORRECT_TRADER_ID:
                    //case CommonEnums.REJECT_REASON.INCORRECT_CONFIRM_NO:
                    //case CommonEnums.REJECT_REASON.LATE_REQ_ACTION:
                    //case CommonEnums.REJECT_REASON.INCORRECT_REFER_NO:
                    //case CommonEnums.REJECT_REASON.INCORRECT_CONDITION:
                    //case CommonEnums.REJECT_REASON.TRADING_HALT:
                    //case CommonEnums.REJECT_REASON.INCORRECT_BOARD:
                    //case CommonEnums.REJECT_REASON.MISSING_CLIENT_ID:
                    //case CommonEnums.REJECT_REASON.INCORRECT_ORDER_TYPE:
                    //case CommonEnums.REJECT_REASON.INCORRECT_FLAG:
                    //case CommonEnums.REJECT_REASON.INCORRECT_CODE:
                    case CommonEnums.REJECT_REASON.INCORRECT_SIDE:
                        // Must be Buy or Sell side
                        message = message + " " + ((string)HttpContext.GetGlobalResourceObject("Resource", "INCORRECT_SIDE", ci));
                        break;
                    //case CommonEnums.REJECT_REASON.INCORRECT_ORDER_NO:
                    //case CommonEnums.REJECT_REASON.INCORRECT_TIME:
                    //case CommonEnums.REJECT_REASON.INCORRECT_DATE:
                    //case CommonEnums.REJECT_REASON.NOT_DO_ODD_LOT_BOARD:
                    //case CommonEnums.REJECT_REASON.INCORRECT_SUB_BROKER_ID:
                    //case CommonEnums.REJECT_REASON.ILLEGAL_TRUSTEE_ID:
                    //case CommonEnums.REJECT_REASON.SECURITY_SUSPEND:
                    //case CommonEnums.REJECT_REASON.MISSING_PC_FLAG:
                    //case CommonEnums.REJECT_REASON.MISSING_SUB_BROKER_ID:
                    //case CommonEnums.REJECT_REASON.NO_VAILABLE_ROOM:
                    //case CommonEnums.REJECT_REASON.MARKET_INTERMISSION:
                    //case CommonEnums.REJECT_REASON.MARKET_HALT:
                    //case CommonEnums.REJECT_REASON.INCORRECT_PUB_VOL:
                    //case CommonEnums.REJECT_REASON.DISALLOW_CHANGE_DEAL:
                    //case CommonEnums.REJECT_REASON.DISALLW_PUB_VOL:
                    //case CommonEnums.REJECT_REASON.DISALLOW_TRADING_STOCK:
                    case CommonEnums.REJECT_REASON.PRICE_ABOVE_CEILING:
                        //Price is above ceiling price
                        message = message + " " + ((string)HttpContext.GetGlobalResourceObject("Resource", "PRICE_ABOVE_CEILING", ci));
                        break;
                    case CommonEnums.REJECT_REASON.PRICE_BELOW_FLOOR:
                        // Price is below floor price
                        message = message + " " + ((string)HttpContext.GetGlobalResourceObject("Resource", "PRICE_BELOW_FLOOR", ci));
                        break;
                    //case CommonEnums.REJECT_REASON.PTHR_INCORRECT_FORMAT:
                    //case CommonEnums.REJECT_REASON.DISALLW_CANCEL_AUTOMATCH_DEAL:
                    //case CommonEnums.REJECT_REASON.PTHR_INCORRECT_VOL:
                    //case CommonEnums.REJECT_REASON.INCORRECT_MARKET_MAKER:
                    //case CommonEnums.REJECT_REASON.ILLEGAL_SHORT_SALES_ORDER:
                    //case CommonEnums.REJECT_REASON.ILLEGAL_MARKET_ID:
                    //case CommonEnums.REJECT_REASON.ILLEGAL_MARKET_TYPE:
                    //case CommonEnums.REJECT_REASON.ILLEGAL_MESSAGE_LENGTH:
                    //case CommonEnums.REJECT_REASON.PRICE_OVER:
                    //case CommonEnums.REJECT_REASON.DISAPPROVE_ORDER:
                    //case CommonEnums.REJECT_REASON.REJECT_FROM_FIS:
                    //case CommonEnums.REJECT_REASON.HALTED_TRADER_ID:
                    //case CommonEnums.REJECT_REASON.UNIDENTIFIED_ERROR:
                    //case CommonEnums.REJECT_REASON.INCORRECT_ACCOUNT_ID:
                    case CommonEnums.REJECT_REASON.NOT_ENOUGH_CASH:
                        // Not enough cash
                        message = message + " " + ((string)HttpContext.GetGlobalResourceObject("Resource", "NOT_ENOUGH_CASH", ci));
                        break;
                    case CommonEnums.REJECT_REASON.NOT_ENOUGH_STOCK:
                        // Not enough stock
                        message = message + " " + ((string)HttpContext.GetGlobalResourceObject("Resource", "NOT_ENOUGH_STOCK", ci));
                        break;
                    case CommonEnums.REJECT_REASON.NOT_BUY_SELL_THE_SAME_STOCK:
                        // Not allow to buy or sell the same stock
                        message = message + " " + ((string)HttpContext.GetGlobalResourceObject("Resource", "NOT_BUY_SELL_THE_SAME_STOCK", ci));
                        break;
                    //case CommonEnums.REJECT_REASON.NOT_CANCEL_ORDER_FROM_DIFF_SOURCE:
                    //case CommonEnums.REJECT_REASON.NOT_CANCEL_ATO_ATC:
                    //case CommonEnums.REJECT_REASON.NOT_CANCEL_IN_THIS_PERIOD_PHASE:
                    case CommonEnums.REJECT_REASON.OVER_REMAIN_VOLUME:
                        // over remain room
                        message = message + " " + ((string)HttpContext.GetGlobalResourceObject("Resource", "OVER_REMAIN_VOLUME", ci));
                        break;
                    case CommonEnums.REJECT_REASON.STOCK_IS_HALT:
                        // Stock is halt
                        message = message + " " + string.Format(((string)HttpContext.GetGlobalResourceObject("Resource", "STOCK_IS_HALT", ci)), symbol);
                        break;
                    case CommonEnums.REJECT_REASON.OVER_MAX_VOL:
                        // Over max volume
                        message = message + " " + ((string)HttpContext.GetGlobalResourceObject("Resource", "OVER_MAX_VOL", ci));
                        break;
                    //case CommonEnums.REJECT_REASON.NOT_ALLOW_TRADE_BONDS:
                    //case CommonEnums.REJECT_REASON.NOT_CANCEL_ORDER_CANCELED:
                    case CommonEnums.REJECT_REASON.ERROR_PRICE_NOT_MULTIPLE_100_FOR_HOSE:
                        // Incorrect step price
                        message = message + " " + ((string)HttpContext.GetGlobalResourceObject("Resource", "ERROR_PRICE_NOT_MULTIPLE_100_FOR_HOSE", ci));
                        break;
                    case CommonEnums.REJECT_REASON.ERROR_PRICE_NOT_MULTIPLE_500_FOR_HOSE:
                        // Incorrect step price
                        message = message + " " + ((string)HttpContext.GetGlobalResourceObject("Resource", "ERROR_PRICE_NOT_MULTIPLE_500_FOR_HOSE", ci));
                        break;
                    case CommonEnums.REJECT_REASON.ERROR_PRICE_NOT_MULTIPLE_1000_FOR_HOSE:
                        // Incorrect step price
                        message = message + " " + ((string)HttpContext.GetGlobalResourceObject("Resource", "ERROR_PRICE_NOT_MULTIPLE_1000_FOR_HOSE", ci));
                        break;
                    case CommonEnums.REJECT_REASON.ERROR_HNX_NOT_USE_ATO_ATC:
                        // No ATO or ATC for HNX
                        message = message + " " + ((string)HttpContext.GetGlobalResourceObject("Resource", "ERROR_HNX_NOT_USE_ATO_ATC", ci));
                        break;
                    case CommonEnums.REJECT_REASON.ERROR_PRICE_NOT_MULTIPLE_100_FOR_HNX:
                        // Incorrect step price
                        message = message + " " + ((string)HttpContext.GetGlobalResourceObject("Resource", "ERROR_PRICE_NOT_MULTIPLE_100_FOR_HNX", ci));
                        break;
                    case CommonEnums.REJECT_REASON.ERROR_LOCK_ACCOUNT:
                        // Account is locked
                        message = message + " " + ((string)HttpContext.GetGlobalResourceObject("Resource", "ERROR_LOCK_ACCOUNT", ci));
                        break;
                    case CommonEnums.REJECT_REASON.ERROR_ACCOUNT_NOT_BUY_PERMISSION:
                        // No buy permission
                        message = message + " " + ((string)HttpContext.GetGlobalResourceObject("Resource", "ERROR_ACCOUNT_NOT_BUY_PERMISSION", ci));
                        break;
                    case CommonEnums.REJECT_REASON.ERROR_ACCOUNT_NOT_SELL_PERMISSION:
                        // No sell permission
                        message = message + " " + ((string)HttpContext.GetGlobalResourceObject("Resource", "ERROR_ACCOUNT_NOT_SELL_PERMISSION", ci));
                        break;
                    case CommonEnums.REJECT_REASON.ERROR_ACCOUNT_NOT_TRADE_PERMISSION:
                        // No trade permission
                        message = message + " " + ((string)HttpContext.GetGlobalResourceObject("Resource", "ERROR_ACCOUNT_NOT_TRADE_PERMISSION", ci));
                        break;
                    case CommonEnums.REJECT_REASON.ERROR_NOT_AVAILABLE_STOCK:
                        // No available stock
                        message = message + " " + ((string)HttpContext.GetGlobalResourceObject("Resource", "ERROR_NOT_AVAILABLE_STOCK", ci));
                        break;
                    case CommonEnums.REJECT_REASON.ERROR_MARKET_CLOSE:
                        // Thi truong da dong cua
                        message = message + " " +
                                  string.Format(((string)HttpContext.GetGlobalResourceObject("Resource", "ERROR_MARKET_CLOSE", ci)),
                                                ETradeCommon.Utils.GetMarketName(market));
                        break;
                    //case CommonEnums.REJECT_REASON.ERROR_ATO_NOT_IN_READY_AND_SESSION1:
                    //case CommonEnums.REJECT_REASON.ERROR_ATC_NOT_IN_SESSION3:
                    //case CommonEnums.REJECT_REASON.NOT_CANCEL_ORDER_MATCHED:
                    case CommonEnums.REJECT_REASON.ERROR_UPCOM_NOT_USE_ATO_ATC:
                        // No ATO or ATC for HNX
                        message = message + " " + ((string)HttpContext.GetGlobalResourceObject("Resource", "ERROR_UPCOM_NOT_USE_ATO_ATC", ci));
                        break;
                    case CommonEnums.REJECT_REASON.ERROR_PRICE_NOT_MULTIPLE_100_FOR_UPCOM:
                        // Incorrect step price
                        message = message + " " + ((string)HttpContext.GetGlobalResourceObject("Resource", "ERROR_PRICE_NOT_MULTIPLE_100_FOR_UPCOM", ci));
                        break;
                    default:
                        // Cannot put order. Unknown error. No Response
                        message = message + " " + string.Format(((string)HttpContext.GetGlobalResourceObject("Resource", "ERROR_DEFAULT", ci)), Constants.COMPANY_NAME);
                        break;
                }

                _accountManagerServices.SendMessage(mainCustAccountId, message);
            } 
            catch(Exception exception)
            {
                ExceptionHandler.HandleException(exception, Constants.EXCEPTION_POLICY);
            }
            
        }

        /// <summary>
        /// Cancels the order.
        /// </summary>
        /// <param name="orderId">The order id.</param>
        /// <returns>
        /// <para>ResultObject of interger. 
        /// If the RetCode is CommonEnums.RET_CODE.SUCCESS then Result of ResultObject is the order id.
        /// Otherwise, it is a reject code. Please refer to the reject code in the enum REJECT_REASON of CommonEnums.cs</para>
        /// <para>RET_CODE=ERROR_GW_NOT_CONNECTED: The LinkOPS is not connected.</para>
        /// <para>RET_CODE=INCORRECT_PIN: The pin is incorrect.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Cancel successfully.</para>
        /// <para>RET_CODE=FAIL: Fail to cancel order.</para>
        /// <para>REJECT_REASON=INCORRECT_ORDER_NO: The order id is incorrect.</para>
        /// <para>REJECT_REASON=NOT_CANCEL_ORDER_CANCELED: Cannot cancel a cancelled order.</para>
        /// <para>REJECT_REASON=NOT_CANCEL_ORDER_MATCHED: Cannot cancel a full matched order.</para>
        /// <para>REJECT_REASON=NOT_CANCEL_ORDER_FROM_DIFF_SOURCE: Cannot cancel a order from other source.</para>
        /// <para>REJECT_REASON=NOT_CANCEL_IN_THIS_PERIOD_PHASE: Cannot cancel a order in this phase.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        public ResultObject<int> CancelOrder(int orderId)
        {
            try
            {
                ExecOrder execOrder = _execOrderService.GetByOrderId(orderId);
                if (execOrder == null)
                {
                    return new ResultObject<int>
                    {
                        ErrorMessage = CommonEnums.REJECT_REASON.INCORRECT_ORDER_NO.ToString(),
                        Result = (int)CommonEnums.REJECT_REASON.INCORRECT_ORDER_NO,
                        RetCode = CommonEnums.RET_CODE.FAIL
                    };
                }

                CommonEnums.REJECT_REASON rejectReason = _validateServices.IsValidCancelOrder(execOrder);

                if (rejectReason != CommonEnums.REJECT_REASON.IS_VALID)
                {
                    return new ResultObject<int>
                    {
                        ErrorMessage = rejectReason.ToString(),
                        Result = (int)rejectReason,
                        RetCode = CommonEnums.RET_CODE.FAIL
                    };
                }

                CommonEnums.RET_CODE retCode = CancelOrderHandler(orderId);

                return new ResultObject<int>
                {
                    RetCode = retCode,
                    Result = (int)retCode,
                    ErrorMessage = retCode.ToString()
                };
            }
            catch (Exception exception)
            {
                LogHandler.Log("CancelOrder OrderID = " + orderId + " Exception = " + exception,
                    GetType() + ".CancelOrder()",
                                        TraceEventType.Error);

                return new ResultObject<int>
                {
                    ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString(),
                    Result = (int)CommonEnums.RET_CODE.SYSTEM_ERROR,
                    RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR
                };
            }
        }
        public CommonEnums.RET_CODE CancelOrderHandler(int orderID)
        {
            ExecOrder orderInfo = null;

            try
            {

                orderInfo = _execOrderService.GetByOrderId(orderID);
                if (orderInfo == null)
                {
                    return CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }
                //This is a new order that has not confirmed from FIS/SET yet
                if (orderInfo.OrderStatus == (short)CommonEnums.ORDER_STATUS.NEW_ORDER || !AppConfig.CheckGWConnection)
                {
                    orderInfo.MessageType = Constants.DATA_EXEC_REPORT;
                    orderInfo.ExecutedVol = 0;
                    orderInfo.ExecutedPrice = 0;
                    orderInfo.CancelledVolume = orderInfo.Volume;
                    orderInfo.CancelledTime = DateTime.Now;
                    orderInfo.OrderStatus = (short)CommonEnums.ORDER_STATUS.CANCELLED;
                    orderInfo.IsNewOrder = true;

                    _execOrderService.Update(orderInfo);

                    LogHandler.Log("Cancelled the order:" + orderID, "CancelOrder", TraceEventType.Information);
                    

                    return CommonEnums.RET_CODE.SUCCESS;
                }

               
                orderInfo.MessageType = Constants.DATA_CANCEL_ORDER;
                orderInfo.ExecTransType = (int)CommonEnums.TRANS_TYPE.TRANS_CANCEL;
                orderInfo.OrderStatus = (short)CommonEnums.ORDER_STATUS.NEW_CANCEL;

                _execOrderService.Update(orderInfo);

                LogHandler.Log("cancel order:" + orderID, "CancelOrder", TraceEventType.Information);
              

                return CommonEnums.RET_CODE.SUCCESS;
            }

            catch (Exception ex)
            {
                LogHandler.Log("cancel order error:" + orderID, "CancelOrder", TraceEventType.Error);
               

                ExceptionHandler.HandleException(ex, Constants.EXCEPTION_POLICY);

                return CommonEnums.RET_CODE.SYSTEM_ERROR;

            }
        }
        /// <summary>
        /// Gets the newest order status.
        /// </summary>
        /// <param name="pageSize">Size of the page.</param> 
        /// <param name="pageIndex">The page number.</param>
        /// <param name="accountId">The account id.</param>
        /// <param name="isPending">Is Pending.</param>
        /// <param name="isMatched">Is Matched.</param>
        /// <param name="isSemiMatched">Is SemiMatched.</param>
        /// <param name="isCanceling">Is Canceling.</param>
        /// <param name="isCancelled">Is Cancelled.</param>
        /// <param name="isRejected">Is rejected.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;ExecOrder&gt;&gt;&gt;</see> object contains returned code, returned message and 
        /// list of ExecOrder objects that contains order information.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Putting order successfully.</para>
        /// </returns>
        public ResultObject<PagingObject<List<ExecOrder>>> GetNewsestOrderStatus(
            int pageSize,
            int pageIndex,
            string accountId,
            bool isPending,
            bool isMatched,
            bool isSemiMatched,
            bool isCanceling,
            bool isCancelled,
            bool isRejected)
        {
            var resultObject = new ResultObject<PagingObject<List<ExecOrder>>>
            {
                RetCode = CommonEnums.RET_CODE.SUCCESS,
                Result = new PagingObject<List<ExecOrder>>(),
                ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString()
            };

            string whereClause = GetWhereClause(
                accountId, isPending, isMatched, isSemiMatched, isCanceling, isCancelled, isRejected);

            int totalCount = 0;

            var orders = new ETradeOrders.Entities.TList<ExecOrder>();
            if (pageIndex == 0)
            {
                // Get all pages
                orders = _execOrderService.GetPaged(
                whereClause, "OrderID DESC", pageIndex, int.MaxValue, out totalCount);
            }
            else if (pageIndex > 0)
            {
                orders = _execOrderService.GetPaged(
                whereClause, "OrderID DESC", pageIndex - 1, pageSize, out totalCount);
            }

            if (orders == null || orders.Count == 0)
            {
                resultObject.Result = new PagingObject<List<ExecOrder>>();
                resultObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
                resultObject.ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString();

                return resultObject;
            }

            var result = new List<ExecOrder>();

            // validate this order can be canceled or not
            foreach (ExecOrder order in orders)
            {
                order.canCancel = (CommonEnums.REJECT_REASON.IS_VALID == _validateServices.IsValidCancelOrder(order));
                order.canUpdate = (CommonEnums.REJECT_REASON.IS_VALID == _validateServices.IsValidUpdatedOrder(order));
                result.Add(order);
            }

            resultObject.Result = new PagingObject<List<ExecOrder>>
            {
                Count = totalCount,
                Data = result
            };

            return resultObject;
        }

        /// <summary>
        /// Gets the newest order count.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <param name="isPending">Is Pending.</param>
        /// <param name="isMatched">Is Matched.</param>
        /// <param name="isSemiMatched">Is SemiMatched.</param>
        /// <param name="isCanceling">Is Canceling.</param>
        /// <param name="isCancelled">Is Cancelled.</param>
        /// <param name="isRejected">Is Rejected.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;int&gt;</see> object contains returned code, returned message and 
        /// total records.</para>
        /// <para>RET_CODE=SUCCESS: Putting order successfully.</para>
        /// </returns>
        public ResultObject<int> GetNewsestOrderCount(string accountId, bool isPending, bool isMatched, bool isSemiMatched, bool isCanceling, bool isCancelled, bool isRejected)
        {
            var resultObject = new ResultObject<int> { RetCode = CommonEnums.RET_CODE.SUCCESS, Result = 0 };

            string whereClause = GetWhereClause(accountId, isPending, isMatched, isSemiMatched, isCanceling, isCancelled, isRejected);
            int totalCount;

            resultObject.Result = _execOrderService.GetTotalItems(whereClause, out totalCount);

            return resultObject;
        }

        /// <summary>
        /// Gets the GetWhereClause
        /// </summary>
        /// <param name="accountId">
        /// The account no.
        /// </param>
        /// <param name="isPending">
        /// From date.
        /// </param>
        /// <param name="isMatched">
        /// To date.
        /// </param>
        /// <param name="isSemiMatched">
        /// The symbol.
        /// </param>
        /// <param name="isCanceling">
        /// The order status.
        /// </param>
        /// <param name="isCancelled">
        /// The order status.
        /// </param>
        /// <param name="isRejected">
        /// The order status.
        /// </param>
        /// <returns>
        /// string
        /// </returns>
        private static string GetWhereClause(string accountId, bool isPending, bool isMatched, bool isSemiMatched,
            bool isCanceling, bool isCancelled, bool isRejected)
        {
            var whereClause = new StringBuilder();
            const string OR_STRING = " OR ";
            string whereString;

            if (isPending && isMatched && isSemiMatched && isCanceling && isCancelled && isRejected)
            {
                whereString = string.Format("SubCustAccountID = '{0}'", accountId);
            }
            else
            {
                if (isPending)
                {
                    whereClause.Append(
                        string.Format(
                            "(OrderStatus = {0} OR OrderStatus = {1} OR OrderStatus = {2})",
                            (int)CommonEnums.ORDER_STATUS.NEW_ORDER,
                            (int)CommonEnums.ORDER_STATUS.CONFIRMED_FIS,
                                                (int)CommonEnums.ORDER_STATUS.CONFIRMED_SET));
                }

                if (isMatched)
                {
                    whereClause.Append(OR_STRING);
                    whereClause.AppendFormat(" (OrderStatus = {0})", (int)CommonEnums.ORDER_STATUS.FULL_MATCHED);
                }

                if (isSemiMatched)
                {
                    whereClause.Append(OR_STRING);
                    whereClause.AppendFormat(
                        " (OrderStatus = {0} OR (ExecutedVol > 0 AND (((Volume - ExecutedVol) > 0) OR (CancelledVolume > 0))))",
                        (int)CommonEnums.ORDER_STATUS.SEMI_MATCHED);
                }

                if (isCanceling)
                {
                    whereClause.Append(OR_STRING);
                    whereClause.AppendFormat(
                        " (OrderStatus = {0} OR OrderStatus = {1})",
                        (int)CommonEnums.ORDER_STATUS.NEW_CANCEL,
                        (int)CommonEnums.ORDER_STATUS.WAITING_CANCEL);
                }

                if (isCancelled)
                {
                    whereClause.Append(OR_STRING);
                    whereClause.AppendFormat(" (OrderStatus = {0})", (int)CommonEnums.ORDER_STATUS.CANCELLED);
                }

                if (isRejected)
                {
                    whereClause.Append(OR_STRING);
                    whereClause.AppendFormat(
                        " (OrderStatus = {0} OR OrderStatus = {1})",
                        (int)CommonEnums.ORDER_STATUS.ORDER_REJECTED,
                        (int)CommonEnums.ORDER_STATUS.CANCEL_REJECTED);
                }

                whereString = whereClause.ToString();
                if (string.IsNullOrEmpty(whereString.Trim()))
                {
                    whereString = string.Format("SubCustAccountID = '{0}'", accountId);
                }
                else
                {
                    if (whereString.StartsWith(OR_STRING))
                    {
                        whereString = whereString.Substring(OR_STRING.Length, whereString.Length - OR_STRING.Length);
                    }
                    whereString = "SubCustAccountID = '" + accountId + "' AND (" + whereString + ")";
                }
            }

            return whereString;
        }
        #endregion

        #region Deal
        /// <summary>
        /// Gets the deal history.
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        /// <param name="dealDate">The deal date.</param>
        /// <param name="page">The page.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;List&lt;DealHistory&gt;&gt;</see> object contains returned code, returned message and 
        /// list of DealHistory object that contains deal history information.</para>
        /// <para>RET_CODE=ERROR_INVALID_DATETIME: The dealDate is invalid.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        public ResultObject<List<DealHistory>> GetDealHistory(decimal orderNo, string dealDate, int page)
        {
            try
            {
                if(!ETradeCommon.Utils.IsValidDate(dealDate))
                {
                    return new ResultObject<List<DealHistory>>
                    {
                        ErrorMessage = CommonEnums.RET_CODE.ERROR_INVALID_DATETIME.ToString(),
                        Result = null,
                        RetCode = CommonEnums.RET_CODE.ERROR_INVALID_DATETIME
                    };
                }
                List<DealHistory> list = _dealServices.GetDealHistory(orderNo, dealDate, page);

                if (list == null)
                    return new ResultObject<List<DealHistory>>
                    {
                        ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                        Result = null,
                        RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                    };

                return new ResultObject<List<DealHistory>>
                {
                    ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                    Result = list,
                    RetCode = CommonEnums.RET_CODE.SUCCESS
                };
            }
            catch (Exception exception)
            {
                LogHandler.Log(
                    "GetDealHistory orderNo = " + orderNo + " dealDate = " + dealDate + " Exception = " + exception,
                    GetType() + ".PutOrder()",
                    TraceEventType.Error);

                return new ResultObject<List<DealHistory>>
                {
                    ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString(),
                    Result = null,
                    RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR
                };
            }
        }
        /// <summary>
        /// Gets the deal intra day.
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        /// <param name="page">The page.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;List&lt;DealInfo&gt;&gt;</see> object contains returned code, returned message and 
        /// list of DealInfo object that contains deal information.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// </returns>
        public ResultObject<List<DealInfo>> GetDealIntraDay(decimal orderNo, int page)
        {
            List<DealInfo> list = _dealServices.GetDealIntraDay(orderNo, page);

            if (list == null)
                return new ResultObject<List<DealInfo>>
                {
                    ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                    Result = null,
                    RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                };

            return new ResultObject<List<DealInfo>>
            {
                ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                Result = list,
                RetCode = CommonEnums.RET_CODE.SUCCESS
            };
        }
        #endregion        

        #region Portfolio

        /// <summary>
        /// Caculates the portfolio.
        /// </summary>
        /// <param name="portfolioInfos">The portfolio infos.</param>
        /// <param name="accountNo"></param>
        /// <param name="accountType"></param>
        /// <returns></returns>
        private void CaculatePortfolio(ref Dictionary<string, PortfolioInfo> portfolioInfos, string accountNo, int accountType)
        {
            //try
            //{
            if (portfolioInfos != null)
            {
                List<OrderInfo> listOrderIntraDay = _stockServices.GetListOrderIntraDay(accountNo);
                foreach (KeyValuePair<string, PortfolioInfo> portfolioInfo in portfolioInfos)
                {
                    var stockInfo =
                        Serializer.Deserialize<ResultObject<StockInfo>>(
                            _rtServices.GetStockInfo(portfolioInfo.Value.Symbol));

                    if (stockInfo.Result != null)
                    {
                        portfolioInfo.Value.RefPrice = (Decimal) stockInfo.Result.RefPrice;
                        portfolioInfo.Value.MarketPrice = ((Decimal) stockInfo.Result.Last > 0)
                                                              ? (Decimal) stockInfo.Result.Last
                                                              : (Decimal) stockInfo.Result.RefPrice;

                    }
                    else
                    {
                        portfolioInfo.Value.MarketPrice = 0;
                        portfolioInfo.Value.RefPrice = 0;
                    }

                    if (portfolioInfo.Value != null)
                    {
                        if (portfolioInfo.Value.MarketPrice == 0)
                        {
                            portfolioInfo.Value.GainLostToday = 0;
                        }
                        else
                        {
                            if (portfolioInfo.Value.Total == portfolioInfo.Value.WTR)
                            {
                                portfolioInfo.Value.GainLostToday = (portfolioInfo.Value.MarketPrice -
                                                                     portfolioInfo.Value.AvgPrice)*
                                                                    (portfolioInfo.Value.Total);
                                portfolioInfo.Value.GainLostToday =
                                    Math.Round(portfolioInfo.Value.GainLostToday*Constants.MONEY_UNIT, 2);
                            }
                            else
                            {
                                portfolioInfo.Value.GainLostToday = (portfolioInfo.Value.MarketPrice -
                                                                     portfolioInfo.Value.RefPrice)*
                                                                    portfolioInfo.Value.Total;
                                portfolioInfo.Value.GainLostToday =
                                    Math.Round(portfolioInfo.Value.GainLostToday*Constants.MONEY_UNIT, 2);
                            }
                        }

                        //
                        KeyValuePair<string, PortfolioInfo> info = portfolioInfo;
                        List<OrderInfo> orders =new List<OrderInfo>();
                        if (listOrderIntraDay != null && listOrderIntraDay.Count > 0)
                            orders =
                                listOrderIntraDay.Where(
                                    orderInfo =>
                                    orderInfo.Symbol.ToUpper().Equals(info.Value.Symbol.ToUpper()) &&
                                    orderInfo.MatchVolume > 0 && orderInfo.Side.Trim().Equals("B")).ToList();
                        var fee = new FeeService();
                        decimal investValueToday = 0;
                        Fee tradeFee;
                        if (orders.Count > 0)
                        {
                            foreach (OrderInfo orderInfo in orders)
                            {
                                decimal investTemp = orderInfo.MatchVolume * orderInfo.Price;
                                tradeFee = fee.GetTradeFee(CommonEnums.FEE_TYPE.FEE_TRADE, investTemp * Constants.MONEY_UNIT);
                                investValueToday += (investTemp * (1 + tradeFee.FeeRatio / 100));
                            }
                            // Because query call DB2INST1.STR_PORTFOLIO_CB('0050956',0) 
                            // only return stock_type=42(WTR) when there is stock buying today
                            portfolioInfo.Value.InvestValue =
                                Math.Round((portfolioInfo.Value.Amount + investValueToday) * Constants.MONEY_UNIT, 2);
                            
                        }
                        else
                        {
                            portfolioInfo.Value.InvestValue =
                                Math.Round(
                                    (portfolioInfo.Value.Amount + investValueToday +
                                     portfolioInfo.Value.WTR_Amt_T2 + portfolioInfo.Value.WTR_Amt_T3) *
                                    Constants.MONEY_UNIT, 2);
                        }
                        LogHandler.Log("InvestValues of AccountNo: " + accountNo + ",StockSymbol: " + portfolioInfo.Value.Symbol + ", InvestToDay: " + investValueToday + ", Amount: " + portfolioInfo.Value.Amount, "CaculatePortfolio", TraceEventType.Information);
                        
                        LogHandler.Log("InvestValues alter Calc: " + portfolioInfo.Value.InvestValue + ",StockSymbol: " + portfolioInfo.Value.Symbol, "CaculatePortfolio", TraceEventType.Information);
                        
                        //portfolioInfo.Value.InvestValue =
                        //    Math.Round(portfolioInfo.Value.AvgPrice*(portfolioInfo.Value.Total)*Constants.MONEY_UNIT, 2);
                        
                        

                        //portfolioInfo.Value.InvestValue =
                        //    Math.Round(portfolioInfo.Value.WTRAVGPrice*portfolioInfo.Value.WTR*Constants.MONEY_UNIT, 2);
                        portfolioInfo.Value.CurrentValue =
                            Math.Round(
                                portfolioInfo.Value.MarketPrice*(portfolioInfo.Value.Total)*Constants.MONEY_UNIT, 2);


                        if (portfolioInfo.Value.Total == portfolioInfo.Value.WTR)
                            portfolioInfo.Value.GainLoss = portfolioInfo.Value.GainLostToday;
                        else
                        {
                            portfolioInfo.Value.GainLoss =
                                Math.Round(
                                    portfolioInfo.Value.MarketPrice*portfolioInfo.Value.Total*Constants.MONEY_UNIT, 2) -
                                portfolioInfo.Value.InvestValue;
                        }


                        if (portfolioInfo.Value.InvestValue != 0 && portfolioInfo.Value.AvgPrice!=0)
                        {
                            if (portfolioInfo.Value.Total == portfolioInfo.Value.WTR)
                            {

                                portfolioInfo.Value.Percent = (portfolioInfo.Value.MarketPrice -
                                                               portfolioInfo.Value.AvgPrice)/
                                                              (portfolioInfo.Value.AvgPrice*100);
                            }
                            else
                            {
                                portfolioInfo.Value.Percent = (portfolioInfo.Value.GainLoss/
                                                               portfolioInfo.Value.InvestValue)*100;
                            }
                        }
                        else
                        {
                            if ((portfolioInfo.Value.Total) > 0)
                            {
                                portfolioInfo.Value.Percent = 100;
                            }
                            else
                            {
                                portfolioInfo.Value.Percent = 0;
                            }
                        }
                    }
                }
            }
            //}
            //catch (Exception exception)
            //{
            //    LogHandler.Log(
            //    "CaculatePortfolio: Exception =" + exception,
            //        GetType() + ".CaculatePortfolio()",
            //        TraceEventType.Error);
            //}
        }

        /// <summary>
        /// Caculate the profit and lost for each page or total portfolio (pageNumber == 0)
        /// </summary>
        /// <param name="portfolioInfos"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private static PortfolioInfo CaculateSumPortfolio(List<PortfolioInfo> portfolioInfos, int pageNumber, int pageSize)
        {
            var sumpage = new PortfolioInfo();

            try
            {
                int startIndex;
                int count;

                if (pageNumber == 0)
                {
                    startIndex = 0;
                    count = portfolioInfos.Count;
                }
                else
                {
                    startIndex = (pageNumber - 1) * pageSize;
                    int remainsItemCount = portfolioInfos.Count - startIndex;
                    count = (remainsItemCount > pageSize) ? pageSize : remainsItemCount;
                }

                var pagePortfolio = portfolioInfos.GetRange(startIndex, count);

                // Add a row for sumary portfolio
                sumpage.Total = 0;
                sumpage.CurrentValue = 0;
                sumpage.InvestValue = 0;

                foreach (PortfolioInfo item in pagePortfolio)
                {
                    if (item.Symbol != Constants.SUM_PORTFOLIO_PAGE &&
                        item.Symbol != Constants.SUM_PORTFOLIO_TOTAL)
                    {
                        sumpage.Total += item.Total;
                        sumpage.InvestValue += item.InvestValue;
                        sumpage.CurrentValue += item.CurrentValue;
                        sumpage.GainLostToday += item.GainLostToday;
                    }
                }

                sumpage.GainLoss = sumpage.CurrentValue - sumpage.InvestValue;

                if (sumpage.InvestValue != 0)
                {
                    sumpage.Percent = Math.Round(sumpage.GainLoss / sumpage.InvestValue * 100, 2);
                }
                else if (sumpage.GainLoss != 0)
                {
                    sumpage.Percent = 100;
                }
                else
                {
                    sumpage.Percent = 0;
                }


                sumpage.Symbol = pageNumber > 0 ? Constants.SUM_PORTFOLIO_PAGE : Constants.SUM_PORTFOLIO_TOTAL;


            }
            catch (Exception exception)
            {
                LogHandler.Log(
                "CaculateSumPortfolio: Exception =" + exception,
                    "CaculateSumPortfolio()",
                    TraceEventType.Error);
            }

            return sumpage;
        }

        /// <summary>
        /// Gets the portfolio.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="accountType">Type of the account.</param>
        /// <param name="subCustAccount">The sub cust account.</param>
        /// <param name="subCustAccounts">List of sub customer accounts.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;PortfolioInfo&gt;, PortfolioInfo, PortfolioInfo&gt;&gt;</see> object contains returned code, returned message and 
        /// list of portfolio objects.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Getting data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        public ResultObject<PagingObject<List<PortfolioInfo>, PortfolioInfo, PortfolioInfo>> GetPortfolio(
            string accountNo, int pageNumber, int pageSize, int accountType, SubCustAccount subCustAccount,List<string>subCustAccounts)
        {
            Dictionary<string, PortfolioInfo> portfolioInfos; 
            try
            {
                portfolioInfos = _stockServices.GetPortfolioInfo(accountNo, accountType);
                if (portfolioInfos == null)
                {
                    //portfolioInfos = new Dictionary<string, PortfolioInfo>();
                    //portfolioInfos.Add("ACB", new PortfolioInfo() { Symbol = "ACB", SellableShare = 500, Total = 1000, WTR_T3 = 100, WTR_T2 = 100, WTR_T1 = 300, WTR = 0, WTS = 100, AvgPrice = Convert.ToDecimal("39.6"), MarketPrice = Convert.ToDecimal("45.4"), RefPrice = Convert.ToDecimal("42.6") });
                    //portfolioInfos.Add("SSI", new PortfolioInfo() { Symbol = "SSI", SellableShare = 400, Total = 500, WTR_T3 = 0, WTR_T2 = 100, WTR_T1 = 0, WTR = 100, WTS = 0, AvgPrice = Convert.ToDecimal("40.2"), MarketPrice = Convert.ToDecimal("61.5"), RefPrice = Convert.ToDecimal("60.11") });
                    //portfolioInfos.Add("FPT", new PortfolioInfo() { Symbol = "FPT", SellableShare = 0, Total = 0, WTR_T3 = 0, WTR_T2 = 0, WTR_T1 = 0, WTR = 2000, WTS = 0, AvgPrice = Convert.ToDecimal("60"), MarketPrice = Convert.ToDecimal("69"), RefPrice = Convert.ToDecimal("67.61") });
                    //portfolioInfos.Add("HPG", new PortfolioInfo() { Symbol = "HPG", SellableShare = 250, Total = 250, WTR_T3 = 0, WTR_T2 = 0, WTR_T1 = 0, WTR = 0, WTS = 0, AvgPrice = Convert.ToDecimal("45.5"), MarketPrice = Convert.ToDecimal("50.5"), RefPrice = Convert.ToDecimal("49.11") });
                    //CaculatePortfolio(ref portfolioInfos, accountNo, accountType); ;
                    return new ResultObject<PagingObject<List<PortfolioInfo>, PortfolioInfo, PortfolioInfo>>
                    {
                        ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                        RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA,
                        Result = new PagingObject<List<PortfolioInfo>, PortfolioInfo, PortfolioInfo>()
                    };                    
                }
                CaculatePortfolio(ref portfolioInfos,accountNo,accountType);                
            }
            catch (Exception exception)
            {
                LogHandler.Log(
                    String.Format("GetPortfolio: accountNo = {0}, Exception = {1}", accountNo, exception),
                    GetType() + ".GetPortfolio()",
                    TraceEventType.Error);

                return new ResultObject<PagingObject<List<PortfolioInfo>, PortfolioInfo, PortfolioInfo>>
                {
                    Result = null,
                    RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                    ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                };
            }

            if (portfolioInfos == null)
            {
                return new ResultObject<PagingObject<List<PortfolioInfo>, PortfolioInfo, PortfolioInfo>>
                {
                    ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                    Result = null,
                    RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                };
            }

            // Create the list of portfolio
            var listporfolio = new List<PortfolioInfo>();
            foreach (KeyValuePair<string, PortfolioInfo> portfolioInfo in portfolioInfos)
            {
                // should validate cansell by:
                // 1. market status 
                // 2. sellable share 
                // 3. canbuy/cansell permission.
                portfolioInfo.Value.CanSell = _validateServices.CanSell(portfolioInfo.Value.Symbol, portfolioInfo.Value.SellableShare, accountNo, subCustAccount,subCustAccounts);
                portfolioInfo.Value.CanBuy = _validateServices.CanBuy(portfolioInfo.Value.Symbol,accountNo,subCustAccount,subCustAccounts);
                listporfolio.Add(portfolioInfo.Value);
            }
           
            var sumInPage = new PortfolioInfo();
            if (pageNumber > 0)
            {
                // caculate the sum profit and lost for this page.
                sumInPage = CaculateSumPortfolio(listporfolio, pageNumber, pageSize);
            }

            // caculate the sum profit and lost for total portfolio
            var totalSum = CaculateSumPortfolio(listporfolio, 0, 0);

            // paging
            List<PortfolioInfo> result = Paging(listporfolio, pageNumber, pageSize);

            return new ResultObject<PagingObject<List<PortfolioInfo>, PortfolioInfo, PortfolioInfo>>
            {
                RetCode = CommonEnums.RET_CODE.SUCCESS,
                Result =
                    new PagingObject<List<PortfolioInfo>, PortfolioInfo, PortfolioInfo>
                    {
                        Count = portfolioInfos.Count,
                        Data = result,
                        SumInPage = sumInPage,
                        TotalSum = totalSum
                    },
                ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString()
            };
        }

        /// <summary>
        /// Get the portfolio list.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="accountType">Type of the account.</param>
        /// <returns>List of portfolios</returns>
        public List<string> GetListPortfolio(string accountNo, int accountType)
        {
            var portfolioList = _stockServices.GetListPortfolio(accountNo, accountType);
            return portfolioList;
        }
        /// <summary>
        /// Gets the portfolio direct.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="accounType">Type of the accoun.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;List&lt;Portfolio&gt;&gt;</see> object contains returned code, returned message and 
        /// list of portfolio objects.</para>
        /// <para>RET_CODE=SUCCESS: Getting data successfully.</para>
        /// </returns>
        public ResultObject<List<Portfolio>> GetPortfolioDirect(string accountNo, int accounType)
        {
            var returnVal = new List<Portfolio>();
            if (accounType == (int)CommonEnums.ACCOUNT_TYPE.NORMAL)
            {
                returnVal = _stockServices.GetPortfolioDirect4NormalAccount(accountNo);
            }

            return new ResultObject<List<Portfolio>>
            {
                ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                Result = returnVal,
                RetCode = CommonEnums.RET_CODE.SUCCESS
            };

        }
        #endregion
        
        #region paging

        /// <summary>
        /// Pagings the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        private List<PortfolioInfo> Paging(List<PortfolioInfo> data, int pageIndex, int pageSize)
        {
            int startIndex;
            int count;

            if (pageIndex == 0)
            {
                startIndex = 0;
                count = data.Count;
            }
            else
            {
                startIndex = (pageIndex - 1) * pageSize;
                int remainsItemCount = data.Count - startIndex;
                count = (remainsItemCount > pageSize) ? pageSize : remainsItemCount;
            }

            var pagePortfolio = data.GetRange(startIndex, count);

            return pagePortfolio;
        }
      
        #endregion

        #region Trade
        /// <summary>
        /// Gets the state of the trading.
        /// </summary>
        /// <param name="marketId">The market id.</param>
        /// <returns>Trading state.</returns>
        public char GetTradingState(int marketId)
        {
            if (AppConfig.CheckGWConnection)
            {
                /*LogHandler.Log("Chua ket noi LinkOPS!" + AppConfig.CheckGWConnection + " " + _eTradeGW.IsConnected(),
                               "GetTradingState()", TraceEventType.Error);*/
                return (char)CommonEnums.MARKET_STATUS.WAITING;
            }

            char tradingStatus = MarketServices.TradingStatus(marketId);

            return tradingStatus;
        }

        /// <summary>
        /// Gets the state of the trading.
        /// </summary>
        /// <returns>ResultObject of char</returns>
        public char[] GetAllTradingState()
        {
            if (AppConfig.CheckGWConnection)
            {
                return new[]
                           {
                               (char) CommonEnums.MARKET_STATUS.WAITING, (char) CommonEnums.MARKET_STATUS.WAITING,
                               (char) CommonEnums.MARKET_STATUS.WAITING
                           };
            }

            var tradingStatus = MarketServices.AllTradingStatus();

            return tradingStatus;
        }

        /// <summary>
        /// Check if this is in trading time session or advance session.
        /// </summary>
        /// <returns>
        /// -1: Unavailable
        /// 0: In trading time session
        /// 1: Advance session.
        /// </returns>
        public int[] CheckOrderSession()
        {
            var orderSessions = new[]
                                      {
                                          (int) CommonEnums.PUT_ORDER_SESSION.UNAVAILABLE,
                                          (int) CommonEnums.PUT_ORDER_SESSION.UNAVAILABLE,
                                          (int) CommonEnums.PUT_ORDER_SESSION.UNAVAILABLE
                                      };
            var strNewData = _rtServices.CheckLatestData();
            var newData = Serializer.Deserialize<ResultObject<bool[]>>(strNewData);
            var newDataStatus = newData.Result;

            // Status of HOSE
            var tradingState = GetTradingState((int)CommonEnums.MARKET_ID.HOSE);
            orderSessions[0] = GetOrderSessionState(tradingState, newDataStatus[0]);

            tradingState = GetTradingState((int)CommonEnums.MARKET_ID.HNX);
            orderSessions[1] = GetOrderSessionState(tradingState, newDataStatus[1]);

            tradingState = GetTradingState((int)CommonEnums.MARKET_ID.UPCoM);
            orderSessions[2] = GetOrderSessionState(tradingState, newDataStatus[2]);

            return orderSessions;
        }

        /// <summary>
        /// Get trading time session or advance session.
        /// </summary>
        /// <param name="tradingState">Trading state.</param>
        /// <param name="newData">New data or not.</param>
        /// <returns>
        /// -1: Unavailable
        /// 0: In trading time session
        /// 1: Advance session.
        /// </returns>
        private int GetOrderSessionState(char tradingState, bool newData)
        {
            int orderSession;
            switch (tradingState)
            {
                case (char)CommonEnums.MARKET_STATUS.CLOSE:
                case (char)CommonEnums.MARKET_STATUS.CLOSE_PT:
                    orderSession = (int)CommonEnums.PUT_ORDER_SESSION.CLOSE_SESSION;
                    break;
                case (char)CommonEnums.MARKET_STATUS.INIT_APP:
                case (char)CommonEnums.MARKET_STATUS.UNVAILABLE:
                case (char)CommonEnums.MARKET_STATUS.WAITING:
                    orderSession = (int)CommonEnums.PUT_ORDER_SESSION.UNAVAILABLE;
                    break;
                default:
                    if (newData)
                    {
                        orderSession = (int)CommonEnums.PUT_ORDER_SESSION.TRADING_TIME_SESSION;
                    }
                    else
                    {
                        orderSession = (int)CommonEnums.PUT_ORDER_SESSION.UNAVAILABLE;
                    }

                    break;
            }
            return orderSession;
        }

        /// <summary>
        /// Get order session of all markets.
        /// </summary>
        /// <returns>char[]</returns>
        public char[] GetAllOrderSession()
        {
            if (AppConfig.CheckGWConnection)
            {
                return new[]
                           {
                               (char) CommonEnums.ORDER_SESSION.SESSION0, 
                               (char) CommonEnums.ORDER_SESSION.SESSION0,
                               (char) CommonEnums.ORDER_SESSION.SESSION0
                           };
                
            }
            var orderSessions = MarketServices.AllOrderSession();

            return orderSessions;
        }

        /// <summary>
        /// Gets the actual trade.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public ResultObject<PagingObject<List<ActualTrade>>> GetActualTrade(string accountNo, string fromDate, string toDate, string symbol, int pageNumber, int pageSize)
        {
            if(!ETradeCommon.Utils.IsValidDate(fromDate) || !ETradeCommon.Utils.IsValidDate(toDate) )
            {
                return new ResultObject<PagingObject<List<ActualTrade>>>
                {
                    ErrorMessage = CommonEnums.RET_CODE.ERROR_INVALID_DATETIME.ToString(),
                    Result = new PagingObject<List<ActualTrade>>(),
                    RetCode = CommonEnums.RET_CODE.ERROR_INVALID_DATETIME
                };
            }
            List<ActualTrade> actualTrades = _actualTradeServices.GetActualTrading(accountNo, fromDate, toDate, symbol);

            if (actualTrades == null)
            {
                return new ResultObject<PagingObject<List<ActualTrade>>>
                {
                    ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                    Result = new PagingObject<List<ActualTrade>>(),
                    RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                };
            }

            // Paging actual trading
            int startIndex;
            int count;
            if (pageNumber == 0)
            {
                startIndex = 0;
                count = actualTrades.Count;
            }
            else
            {
                startIndex = (pageNumber - 1) * pageSize;
                int remainsItemCount = actualTrades.Count - startIndex;
                count = (remainsItemCount > pageSize) ? pageSize : remainsItemCount;
            }

            var returnValue = actualTrades.GetRange(startIndex, count);

            return new ResultObject<PagingObject<List<ActualTrade>>>
            {
                ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                Result = new PagingObject<List<ActualTrade>>
                {
                    Count = actualTrades.Count,
                    Data = returnValue
                },
                RetCode = CommonEnums.RET_CODE.SUCCESS
            };
        }
        /// <summary>
        /// Gets the pre trade info.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="accountType">Type of the account.</param>
        /// <param name="side">The side.</param>
        /// <param name="isConditionOrder">if set to <c>true</c> [is condition order].</param>
        /// <returns>
        /// 	<para>A <see cref="ResultObject{T}">ResultObject&lt;PreTradeInfo&gt;</see> object contains returned code, returned message and
        /// pretrade information.</para>
        /// 	<para>RET_CODE=SUCCESS: Getting data successfully.</para>
        /// 	<para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        public ResultObject<PreTradeInfo> GetPreTradeInfo(string accountNo, string symbol, int accountType, char side,bool isConditionOrder)
        {
            var retObject = new ResultObject<PreTradeInfo>
            {
                ErrorMessage =
                    CommonEnums.RET_CODE.SYSTEM_ERROR.ToString(),
                Result = null,
                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR
            };

            var stockInfo = Serializer.Deserialize<ResultObject<StockInfo>>(_rtServices.GetStockInfo(symbol));

            if (stockInfo.Result == null)
            {
                return retObject;
            }
            var preTradeInfo = new PreTradeInfo();
            var tradingState = GetTradingState(stockInfo.Result.MarketID);
            var orderSession = MarketServices.GetOrderSession(stockInfo.Result.MarketID);
            preTradeInfo.OrderSession = orderSession;
            if (side == (char)CommonEnums.TRADE_SIDE.BUY)
            {                                

                var cashAvailable = GetAvailableCash(accountNo, accountType,isConditionOrder);
                preTradeInfo.CashAvailable=cashAvailable.Result;
                preTradeInfo.StockInfo = stockInfo.Result;
                preTradeInfo.TradingState = (CommonEnums.MARKET_STATUS)tradingState;
                preTradeInfo.listFee = FeeService.ListFees;
                return new ResultObject<PreTradeInfo>
                {
                    ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                    Result = preTradeInfo,                      
                    RetCode = CommonEnums.RET_CODE.SUCCESS
                };
            }
                  
            //for SELL side
            var stockAvailable = GetAvailableStock(accountNo, symbol, accountType, isConditionOrder);
            preTradeInfo.StockAvailable = stockAvailable.Result;
            preTradeInfo.StockInfo = stockInfo.Result;
            preTradeInfo.TradingState = (CommonEnums.MARKET_STATUS) tradingState;
            preTradeInfo.listFee = FeeService.ListFees;
            return new ResultObject<PreTradeInfo>
            {
                ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                Result =preTradeInfo,                  
                RetCode = CommonEnums.RET_CODE.SUCCESS
            };
        }
        #endregion        

        #region XD
        /// <summary>
        /// Pagings the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        private List<XD> Paging(List<XD> data, int pageIndex, int pageSize)
        {
            int startIndex;
            int count;

            if (pageIndex == 0)
            {
                startIndex = 0;
                count = data.Count;
            }
            else
            {
                startIndex = (pageIndex - 1) * pageSize;
                int remainsItemCount = data.Count - startIndex;
                count = (remainsItemCount > pageSize) ? pageSize : remainsItemCount;
            }

            var xds = data.GetRange(startIndex, count);

            return xds;
        }
        /// <summary>
        /// Gets the XD info.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;XD&gt;&gt;&gt;</see> object contains returned code, returned message and 
        /// list of XD information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=ERROR_INVALID_DATETIME: The date is invalid.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Getting data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        public ResultObject<PagingObject<List<XD>>> GetXDInfo(string accountNo, string symbol, string fromDate, string toDate, int pageIndex, int pageSize)
        {
            if(!ETradeCommon.Utils.IsValidDate(fromDate) ||!ETradeCommon.Utils.IsValidDate(toDate) )
            {
                return new ResultObject<PagingObject<List<XD>>>
                {
                    ErrorMessage = CommonEnums.RET_CODE.ERROR_INVALID_DATETIME.ToString(),
                    RetCode = CommonEnums.RET_CODE.ERROR_INVALID_DATETIME,
                    Result = new PagingObject<List<XD>>()
                };
            }
            List<XD> list = _stockServices.GetXDInfo(accountNo, symbol, fromDate, toDate);

            if (list == null)
            {
                return new ResultObject<PagingObject<List<XD>>>
                {
                    ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                    RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA,
                    Result = new PagingObject<List<XD>>()
                };
            }

            // Paging XD
            var returnValue = Paging(list, pageIndex, pageSize);

            return new ResultObject<PagingObject<List<XD>>>
            {
                ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                Result = new PagingObject<List<XD>>
                {
                    Data = returnValue,
                    Count = list.Count
                },
                RetCode = CommonEnums.RET_CODE.SUCCESS
            };
        }
        #endregion
        
        #region XR
        /// <summary>
        /// Pagings the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        private List<XR> Paging(List<XR> data, int pageIndex, int pageSize)
        {
            int startIndex;
            int count;

            if (pageIndex == 0)
            {
                startIndex = 0;
                count = data.Count;
            }
            else
            {
                startIndex = (pageIndex - 1) * pageSize;
                int remainsItemCount = data.Count - startIndex;
                count = (remainsItemCount > pageSize) ? pageSize : remainsItemCount;
            }

            var xds = data.GetRange(startIndex, count);

            return xds;
        }

        /// <summary>
        /// Gets the XR info.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="xType">Type.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;XR&gt;&gt;&gt;</see> object contains returned code, returned message and 
        /// list of XR information.</para>
        /// <para>RET_CODE=ERROR_INVALID_DATETIME: The date is invalid.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Checking successfully.</para>
        /// </returns>
        public ResultObject<PagingObject<List<XR>>> GetXRInfo(string accountNo, string symbol, string fromDate, string toDate, int xType, int pageIndex, int pageSize)
        {
            if(!ETradeCommon.Utils.IsValidDate(fromDate) || !ETradeCommon.Utils.IsValidDate(toDate))
            {
                return new ResultObject<PagingObject<List<XR>>>
                {
                    ErrorMessage = CommonEnums.RET_CODE.ERROR_INVALID_DATETIME.ToString(),
                    RetCode = CommonEnums.RET_CODE.ERROR_INVALID_DATETIME,
                    Result = new PagingObject<List<XR>>()
                };
            }
            int []arrayXType = null;
            if(xType==(int)CommonEnums.RIGHTTYPE.ALL_RIGHT)
            {
                arrayXType = new int[] { (int)CommonEnums.RIGHTTYPE.STOCK_DIVIDENT, (int)CommonEnums.RIGHTTYPE.STOCK_BONUS };
            }
            else
                arrayXType=new int[]{xType};
            List<XR> list = _stockServices.GetXRInfo(accountNo, symbol, fromDate, toDate, arrayXType);

            if (list == null)
            {
                return new ResultObject<PagingObject<List<XR>>>
                {
                    ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                    RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA,
                    Result = new PagingObject<List<XR>>()
                };
            }
                      

            // Paging XR
            var returnValue = Paging(list, pageIndex, pageSize);

            return new ResultObject<PagingObject<List<XR>>>
            {
                ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                Result = new PagingObject<List<XR>>
                {
                    Data = returnValue,
                    Count = list.Count
                },
                RetCode = CommonEnums.RET_CODE.SUCCESS
            };
        }
        #endregion
   
        #region system configuration

        /// <summary>
        /// Saves the config.
        /// </summary>
        /// <param name="holidayses">The holidayses.</param>
        public static void SaveHolidayConfig(IEnumerable<Holidays> holidayses)
        {
            foreach (var holidayse in holidayses)
            {
                if (!SysConfig.Holidays.ContainsKey(holidayse.Holiday.ToString("yyyyMMdd")))
                {
                    SysConfig.Holidays.Add(holidayse.Holiday.ToString("yyyyMMdd"), holidayse.Holiday);
                }
                else
                {
                    SysConfig.Holidays[holidayse.Holiday.ToString("yyyyMMdd")] = holidayse.Holiday;
                }
            }
        }

        /// <summary>
        /// Saves the working days config.
        /// </summary>
        /// <param name="workingDayses">The working dayses.</param>
        public static void SaveWorkingDaysConfig(IEnumerable<WorkingDays> workingDayses)
        {
            foreach (var workingDayse in workingDayses)
            {
                if (!SysConfig.WorkingDays.ContainsKey(workingDayse.DateId))
                {
                    SysConfig.WorkingDays.Add(workingDayse.DateId, workingDayse.IsWorkingDay);
                }
                else
                {
                    SysConfig.WorkingDays[workingDayse.DateId] = workingDayse.IsWorkingDay;
                }
            }
        }

        /// <summary>
        /// Saves all configuration.
        /// </summary>
        /// <param name="configurationses">The configurationses.</param>
        public static void SaveAllConfiguration(List<Configurations> configurationses)
        {
            foreach (Configurations configurationse in configurationses)
            {
                if (!SysConfig.Configurations.ContainsKey(configurationse.Name))
                {
                    SysConfig.Configurations.Add(configurationse.Name, configurationse.Value);
                }
                else
                {
                    SysConfig.Configurations[configurationse.Name] = configurationse.Value;
                }
            }
        }
        /// <summary>
        /// Gets the exchange rate USD.
        /// </summary>
        /// <returns>Exchange rate.</returns>
        public decimal GetExchangeRateUSD()
        {
            decimal exchangeRateUSD = 0;
            if(SysConfig.Configurations.ContainsKey(CommonEnums.CONFIGURATIONS.USD.ToString()))
            {
                string value = "0";
                SysConfig.Configurations.TryGetValue(CommonEnums.CONFIGURATIONS.USD.ToString(),out value);
                decimal.TryParse(value, out exchangeRateUSD);
            }
            return exchangeRateUSD;
        }

        /*/// <summary>
        /// Saves the advance time.
        /// </summary>
        /// <param name="advanceTimes">The advance times.</param>
        public static void SaveAdvanceTime(List<AdvanceTime> advanceTimes)
        {
            foreach (AdvanceTime advanceTime in advanceTimes)
            {
                if (!SysConfig.AdvanceTimes.ContainsKey(advanceTime.AdvanceType))
                {
                    SysConfig.AdvanceTimes.Add(advanceTime.AdvanceType, advanceTime);
                }
                else
                {
                    SysConfig.AdvanceTimes[advanceTime.AdvanceType] = advanceTime;
                }
            }
        }*/

        #endregion
        
        #region Cash Advance
        /// <summary>
        /// Gets the advance history.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="fromAdvanceDate">From advance date.</param>
        /// <param name="toAdvanceDate">To advance date.</param>
        /// <param name="fromSellDate">From sell date.</param>
        /// <param name="toSellDate">To sell date.</param>
        /// <param name="advanceStatus">The advance status.</param>
        /// <param name="contractNo">The contract no.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public ResultObject<PagingObject<List<CashAdvance>>> GetAdvanceHistory(
            string accountNo,
            string fromAdvanceDate,
            string toAdvanceDate,
            string fromSellDate,
            string toSellDate,
            int advanceStatus,
            string contractNo,
            int pageIndex,
            int pageSize)
        {
            if(!ETradeCommon.Utils.IsValidDate(fromAdvanceDate) || !ETradeCommon.Utils.IsValidDate(fromSellDate) || !ETradeCommon.Utils.IsValidDate(toAdvanceDate) || !ETradeCommon.Utils.IsValidDate(toSellDate))
            {
                return new ResultObject<PagingObject<List<CashAdvance>>>
                {
                    ErrorMessage = CommonEnums.RET_CODE.ERROR_INVALID_DATETIME.ToString(),
                    Result = new PagingObject<List<CashAdvance>>(),
                    RetCode = CommonEnums.RET_CODE.ERROR_INVALID_DATETIME
                };
            }
            List<CashAdvance> cashAdvHistFromCore = null;
            List<CashAdvance> cashAdvHistFromOtsDb = null;

            switch (advanceStatus)
            {
                case (int)CommonEnums.ADVANCE_STATUS.FINISHED:
                    // Get advance finished from core
                    cashAdvHistFromCore = _cashAdvanceServices.GetAdvanceHistoryFromCore(
                        accountNo, fromAdvanceDate, toAdvanceDate, fromSellDate, toSellDate, contractNo);
                    break;
                case (int)CommonEnums.ADVANCE_STATUS.CANCELLED:
                case (int)CommonEnums.ADVANCE_STATUS.PENDING:
                case (int)CommonEnums.ADVANCE_STATUS.REJECTED:
                case (int)CommonEnums.ADVANCE_STATUS.PROCESSING:
                    // Get advance history (cancelled, rejected) from Ots database
                    cashAdvHistFromOtsDb = _cashAdvanceServices.GetAdvanceHistoryFromOtsDb(
                        accountNo, fromAdvanceDate, toAdvanceDate, fromSellDate, toSellDate, advanceStatus, contractNo);
                    break;

                default:
                
                    // Get advance finished from core
                    cashAdvHistFromCore = _cashAdvanceServices.GetAdvanceHistoryFromCore(
                        accountNo, fromAdvanceDate, toAdvanceDate, fromSellDate, toSellDate, contractNo);

                    // Get advance history (cancelled, rejected) from Ots database
                    cashAdvHistFromOtsDb = _cashAdvanceServices.GetAdvanceHistoryFromOtsDb(
                        accountNo, fromAdvanceDate, toAdvanceDate, fromSellDate, toSellDate, advanceStatus, contractNo);
                    cashAdvHistFromOtsDb =
                        cashAdvHistFromOtsDb.Where(
                            cashAdvance => cashAdvance.Status != (int) CommonEnums.ADVANCE_STATUS.FINISHED).ToList();
                    break;
            }

            // No data found
            if ((cashAdvHistFromCore == null && cashAdvHistFromOtsDb == null) ||
                ((cashAdvHistFromCore != null && cashAdvHistFromCore.Count == 0)
                && (cashAdvHistFromOtsDb != null && cashAdvHistFromOtsDb.Count == 0)))
            {
                return new ResultObject<PagingObject<List<CashAdvance>>>
                {
                    ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                    Result = new PagingObject<List<CashAdvance>>(),
                    RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                };
            }

            var totalCashAdvance = new List<CashAdvance>();

            // Join cash advance from core and cash advance from Otsdb
            if (cashAdvHistFromCore != null)
            {
                totalCashAdvance.AddRange(cashAdvHistFromCore);
            }

            if (cashAdvHistFromOtsDb != null)
            {
                totalCashAdvance.AddRange(cashAdvHistFromOtsDb);
            }
            totalCashAdvance= totalCashAdvance.OrderBy(cashAdvance => cashAdvance.TradeDate).ToList();

            // Paging advance history
            int startIndex;
            int count;

            if (pageIndex == 0)
            {
                startIndex = 0;
                count = totalCashAdvance.Count;
            }
            else
            {
                startIndex = (pageIndex - 1) * pageSize;
                int remainsItemCount = totalCashAdvance.Count - startIndex;
                count = (remainsItemCount > pageSize) ? pageSize : remainsItemCount;
            }

            var returnValue = totalCashAdvance.GetRange(startIndex, count);


            return new ResultObject<PagingObject<List<CashAdvance>>>
            {
                ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                Result = new PagingObject<List<CashAdvance>>
                {
                    Data = returnValue,
                    Count = totalCashAdvance.Count
                },
                RetCode = CommonEnums.RET_CODE.SUCCESS
            };
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
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;CashAdvance&gt;&gt;&gt;</see> object contains returned code, returned message and 
        /// list of CashAdvance objects that contains cash advance information.</para>
        /// <para>RET_CODE=ERROR_INVALID_DATETIME: The date is incorrect.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        public ResultObject<PagingObject<List<ETradeFinance.Entities.CashAdvance>>> GetCashAdvanceStatus(string accountNo, string fromDate, string toDate, int status, int pageIndex, int pageSize)
        {
            try
            {
                if(!ETradeCommon.Utils.IsValidDate(fromDate) || !ETradeCommon.Utils.IsValidDate(toDate))
                {
                    return new ResultObject<PagingObject<List<ETradeFinance.Entities.CashAdvance>>>
                    {
                        ErrorMessage = CommonEnums.RET_CODE.ERROR_INVALID_DATETIME.ToString(),
                        Result = null,
                        RetCode = CommonEnums.RET_CODE.ERROR_INVALID_DATETIME
                    };
                }
                var cashAdvances =
                _cashAdvanceServices.GetCashAdvanceStatus(accountNo, fromDate, toDate, status, pageIndex, pageSize);

                if (cashAdvances == null || cashAdvances.Data == null)
                {
                    LogHandler.Log(
                    "GetCashAdvanceStatus: NO_EXISTED_DATA, accountNo = " + accountNo + ", fromDate = " +
                    fromDate + ", toDate = " + toDate + ", status = " + status + ", pageIndex = " + pageIndex +
                    ", pageSize = " + pageSize,
                    GetType() + ".GetCashAdvanceStatus()",
                    TraceEventType.Information);
                    return new ResultObject<PagingObject<List<ETradeFinance.Entities.CashAdvance>>>
                    {
                        ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                        Result = new PagingObject<List<ETradeFinance.Entities.CashAdvance>>(),
                        RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                    };
                }

                return new ResultObject<PagingObject<List<ETradeFinance.Entities.CashAdvance>>>
                {
                    ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                    Result = cashAdvances,
                    RetCode = CommonEnums.RET_CODE.SUCCESS
                };
            }
            catch (Exception exception)
            {
                LogHandler.Log(
                    "GetCashAdvanceStatus: exception = " + exception + " accountNo = " + accountNo + ", fromDate = " +
                    fromDate + ", toDate = " + toDate + ", status = " + status + ", pageIndex = " + pageIndex +
                    ", pageSize = " + pageSize,
                    GetType() + ".GetCashAdvanceStatus()",
                    TraceEventType.Error);

                return new ResultObject<PagingObject<List<ETradeFinance.Entities.CashAdvance>>>
                {
                    ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString(),
                    Result = new PagingObject<List<ETradeFinance.Entities.CashAdvance>>(),
                    RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR
                };
            }
        }
        /// <summary>
        /// Gets the advance info.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns></returns>
        public ResultObject<PagingObject<List<AdvanceInfo>, AdvanceInfo, AdvanceInfo>> GetAdvanceInfo(string accountNo)
        {
            var newestWorkingDatesInfo = Serializer.Deserialize<ResultObject<List<NewestWorkingDatesInfo>>>(_rtServices.GetNewestWorkingDates());

            if (newestWorkingDatesInfo == null || newestWorkingDatesInfo.Result == null || newestWorkingDatesInfo.RetCode != CommonEnums.RET_CODE.SUCCESS || newestWorkingDatesInfo.Result.Count==0)
            {
                LogHandler.Log(
                    "GetAdvanceInfo: GetNewestWorkingDates of RTServices return null",
                    GetType() + ".GetAdvanceInfo()",
                    TraceEventType.Warning);
                return new ResultObject<PagingObject<List<AdvanceInfo>, AdvanceInfo, AdvanceInfo>>
                {
                    ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                    Result = new PagingObject<List<AdvanceInfo>, AdvanceInfo, AdvanceInfo>(),
                    RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                };
            }

            newestWorkingDatesInfo.Result[0].T = newestWorkingDatesInfo.Result[0].T.ToLocalTime();
            newestWorkingDatesInfo.Result[0].T1 = newestWorkingDatesInfo.Result[0].T1.ToLocalTime();
            newestWorkingDatesInfo.Result[0].T2 = newestWorkingDatesInfo.Result[0].T2.ToLocalTime();
            newestWorkingDatesInfo.Result[0].T3 = newestWorkingDatesInfo.Result[0].T3.ToLocalTime();

            List<AdvanceInfo> advanceInfos = _cashAdvanceServices.GetAdvanceInfo(accountNo,
                                                                                 newestWorkingDatesInfo.Result[0],
                                                                                 SysConfig.AdvanceTimes,
                                                                                 SysConfig.Holidays,
                                                                                 SysConfig.WorkingDays);

            var sumInPage = new AdvanceInfo();

            if (advanceInfos == null)
                return new ResultObject<PagingObject<List<AdvanceInfo>, AdvanceInfo, AdvanceInfo>>
                {
                    ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                    Result = new PagingObject<List<AdvanceInfo>, AdvanceInfo, AdvanceInfo>(),
                    RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                };

            foreach (var advanceInfo in advanceInfos)
            {
                sumInPage.SellAmt += advanceInfo.SellAmt;
                sumInPage.MaxCanAdvance += advanceInfo.MaxCanAdvance;
                sumInPage.AdvanceFinished += advanceInfo.AdvanceFinished;
            }

            return new ResultObject<PagingObject<List<AdvanceInfo>, AdvanceInfo, AdvanceInfo>>
            {
                ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                Result = new PagingObject<List<AdvanceInfo>, AdvanceInfo, AdvanceInfo>
                {
                    Count = advanceInfos.Count,
                    Data = advanceInfos,
                    SumInPage = sumInPage,
                    TotalSum = new AdvanceInfo()
                },
                RetCode = CommonEnums.RET_CODE.SUCCESS
            };
        }

        /// <summary>
        /// News the cash advance.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="sellAmt">The sell amt.</param>
        /// <param name="cashAdvance">The cash advance.</param>
        /// <param name="maxCanAdvance">The max can advance.</param>
        /// <param name="tradeDate">The trade date.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;int&gt;</see> object contains returned code, returned message and 
        /// contract no.</para>
        /// <para>RET_CODE=ERROR_INVALID_CASH_ADVANCE: Cash advance is invalid.</para>
        /// <para>RET_CODE=ERROR_CANNOT_ADVANCE_OUTOF_TIME: Not time for cash advance.</para>
        /// <para>RET_CODE=ERROR_CANNOT_ADVANCE_IN_DUE_DATE: Cash advance due date is invalid.</para>
        /// <para>RET_CODE=ERROR_NOT_ENOUGH_CASH_TO_ADVANCE: There is not enough cash to request cash advance.</para>
        /// <para>RET_CODE=SUCCESS: Putting order successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        public ResultObject<string> NewCashAdvance(string accountNo,
            decimal sellAmt,
            decimal cashAdvance,
            decimal maxCanAdvance,
            string tradeDate)
        {
            CommonEnums.RET_CODE retCode = CommonEnums.RET_CODE.SUCCESS;

            // calculate dueDate
            DateTime dueDate = _cashAdvanceServices.CalculateDueDate(tradeDate, SysConfig.Holidays, SysConfig.WorkingDays);

            decimal advanceFee = _cashAdvanceServices.CalculateAdvanceFee(cashAdvance, dueDate);

            // TODO: shoule calculate tax per advance fee
            decimal tax = 0;

            // Validate new advance
            retCode = _cashAdvanceServices.IsValidAdvance(maxCanAdvance, cashAdvance, dueDate, advanceFee, tax,
                                                          SysConfig.AdvanceTimes, SysConfig.Holidays,
                                                          SysConfig.WorkingDays);
            if (retCode != CommonEnums.RET_CODE.SUCCESS)
            {
                return new ResultObject<string>
                {
                    RetCode = retCode,
                    ErrorMessage = retCode.ToString(),
                    Result = string.Empty
                };
            }

            //validate the amount of advance
            if (cashAdvance > _cashAdvanceServices.GetMaxAdvance(accountNo, tradeDate, SysConfig.AdvanceTimes, SysConfig.Holidays, SysConfig.WorkingDays))
            {
                retCode = CommonEnums.RET_CODE.ERROR_NOT_ENOUGH_CASH_TO_ADVANCE;
                return new ResultObject<string>
                {
                    RetCode = retCode,
                    ErrorMessage = retCode.ToString(),
                    Result = string.Empty
                };
            }

            DateTime _tradeDate;

            try
            {
                // convert trade date string to DateTime value
                _tradeDate = new DateTime(
                    int.Parse(tradeDate.Substring(0, 4)),
                    int.Parse(tradeDate.Substring(4, 2)),
                    int.Parse(tradeDate.Substring(6, 2)));
            }
            catch
            {
                _tradeDate = new DateTime();
                LogHandler.Log(
                    "NewCashAdvance: tradeDate was incorrect format (yyyyMMdd), tradeDate = " + tradeDate,
                    GetType() + ".NewCashAdvance()",
                    TraceEventType.Warning);
            }

            var contractNo = _cashAdvanceServices.BuildContractNo(DateTime.Now);

            var advanceInfo = new ETradeFinance.Entities.CashAdvance
            {
                ContractNo = contractNo,
                SubAccountId = accountNo,
                AdvanceDate = DateTime.Now,
                CashReceived = 0,
                Fee = advanceFee,
                Status = (int)CommonEnums.ADVANCE_STATUS.PENDING,
                SellDueDate = _tradeDate,
                CashDueDate = dueDate,
                Vat = tax,
                CashAvailable = maxCanAdvance,
                Reason = string.Empty,
                TotalSellValue = sellAmt,
                CashRequest = cashAdvance
            };

            // Insert new advance to DB
            bool result =
                _cashAdvanceServices.InsertCashAdvance(advanceInfo);

            //Update to cashAdvanceHistories.
            if (result)
            {
                var advanceInfoHistory = new CashAdvanceHistory()
                {
                    ContractNo = contractNo,
                    SubAccountId = accountNo,
                    AdvanceDate = DateTime.Now,
                    CashReceived = 0,
                    Fee = advanceFee,
                    Status = (int)CommonEnums.ADVANCE_STATUS.PENDING,
                    SellDueDate = _tradeDate,
                    CashDueDate = dueDate,
                    Vat = tax,
                    CashAvilable = maxCanAdvance,
                    Reason = string.Empty,
                    TotalSellValue = sellAmt,
                    CashRequest = cashAdvance
                };
                result = _cashAdvanceServices.InsertCashAdvanceHistory(advanceInfoHistory);
            }

            retCode = !result ? CommonEnums.RET_CODE.SYSTEM_ERROR : CommonEnums.RET_CODE.SUCCESS;

            return new ResultObject<string>
            {
                ErrorMessage = retCode.ToString(),
                Result = contractNo,
                RetCode = retCode
            };
        }

        /// <summary>
        /// Cancel the cash advance.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="contractNo">The contract no.</param>
        /// <returns>
        /// <para>Result of cancelling cash advance.</para>
        /// <para>RET_CODE=ERROR_CANNOT_CANCEL_ADVANCE_CANCELED: Cannot cancel a canceled cash advance.</para>
        /// <para>RET_CODE=ERROR_CANNOT_CANCEL_ADVANCE_REFJECTED: Cannot cancel a rejected cash advance.</para>
        /// <para>RET_CODE=ERROR_CANNOT_CANCEL_IN_PROCESSING: Cannot cancel a cash advance which is in processing state.</para>
        /// <para>RET_CODE=ERROR_CANNOT_CANCEL_ADVANCE_FINISHED: Cannot cancel a finished cash advance.</para>
        /// <para>RET_CODE=SUCCESS: Cancel successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        public CommonEnums.RET_CODE CancelAdvance(string accountNo, string contractNo)
        {
            var cashAdvanceService = new CashAdvanceService();
            var cashAdvanceHistoryService = new CashAdvanceHistoryService();

            string whereClause = string.Format(" ContractNo = '{0}' AND SubAccountId = '{1}'", contractNo, accountNo);
            int totalRecords = 0;

            var cashAdvances =
                cashAdvanceService.GetPaged(whereClause, "ID DESC", 0, int.MaxValue, out totalRecords);

            var cashAdvance = cashAdvances[0];

            var retCode = _cashAdvanceServices.IsValidCancelAdvance(cashAdvance);

            if (retCode != CommonEnums.RET_CODE.SUCCESS)
            {
                return retCode;
            }

            cashAdvance.Status = (int)CommonEnums.ADVANCE_STATUS.CANCELLED;
            cashAdvance.Fee = 0;
            cashAdvance.ExecTime = DateTime.Now;
            cashAdvance.BrokerId = string.Empty;
            bool result = cashAdvanceService.Update(cashAdvance);

            //Update to cashAdvanceHistories.
            if (result)
            {
                var cashAdvanceHistories = cashAdvanceHistoryService.GetPaged(whereClause, "ID DESC", 0, int.MaxValue, out totalRecords);
                var cashAdvanceHistory = cashAdvanceHistories[0];
                cashAdvanceHistory.Status = (int)CommonEnums.ADVANCE_STATUS.CANCELLED;
                cashAdvanceHistory.Fee = 0;
                cashAdvanceHistory.ExecTime = DateTime.Now;
                cashAdvanceHistory.BrokerId = string.Empty;
                result = cashAdvanceHistoryService.Update(cashAdvanceHistory);
            }

            return !result ? CommonEnums.RET_CODE.SYSTEM_ERROR : CommonEnums.RET_CODE.SUCCESS;
        }

        /// <summary>
        /// Gets the advance fee.
        /// </summary>
        /// <param name="sellAmt">The sell amt.</param>
        /// <param name="advanceDays">The advance days.</param>
        /// <returns></returns>
        public decimal GetAdvanceFee(decimal sellAmt,int advanceDays)
        {
            return _cashAdvanceServices.CalculateAdvanceFee(sellAmt, advanceDays);
        }
        #endregion

        #region Bank Account Info
        /// <summary>
        /// Gets the bank account info.
        /// </summary>
        /// <param name="acccountNo">The acccount no.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;BankAccountInfo&gt;</see> object contains returned code, returned message and 
        /// BankAccountInfo objects that contains bank account information.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// </returns>
        public ResultObject<BankAccountInfo> GetBankAccountInfo(string acccountNo)
        {
            BankAccountInfo bankAccountInfo = _bankServices.GetBankAccountInfo(acccountNo);
            if(bankAccountInfo!=null)
            {
                return new ResultObject<BankAccountInfo>()
                {
                    Result = bankAccountInfo,
                    ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                    RetCode = CommonEnums.RET_CODE.SUCCESS
                };
            }
            else
            {
                return new ResultObject<BankAccountInfo>()
                           {
                               Result = null,
                               ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                               RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                           };
            }
        }
        #endregion

        #region Margin
        /// <summary>
        /// Determines whether [is call margin] [the specified account no].
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;bool&gt;</see> object contains returned code, returned message and 
        /// checking of call margin.</para>
        /// <para>RET_CODE=SUCCESS: Checking successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        public ResultObject<bool> IsCallMargin(string accountNo)
        {
           return new ResultObject<bool>()
                      {
                          Result = _marginServices.IsCallMargin(accountNo),
                          ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                          RetCode = CommonEnums.RET_CODE.SUCCESS
                      };
        }
        /// <summary>
        /// Determines whether this account is force sell or not.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;bool&gt;</see> object contains returned code, returned message and 
        /// checking of call force sell.</para>
        /// <para>RET_CODE=SUCCESS: Checking successfully.</para>
        /// </returns>
        public ResultObject<bool> IsCallForceSell(string accountNo)
        {
            return new ResultObject<bool>()
                       {
                           Result = _marginServices.IsCallForceSell(accountNo),
                           ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                           RetCode = CommonEnums.RET_CODE.SUCCESS
                       };
        }

        /// <summary>
        /// Gets the margin ratio.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;MarginRatioInfo&gt;</see> object contains returned code, returned message and 
        /// MarginRatioInfo object.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Getting data successfully.</para>
        /// </returns>
        public ResultObject<MarginRatioInfo> GetMarginRatio(string accountNo)
        {
            MarginRatioInfo marginRatioInfo = _marginServices.GetMarginRatio(accountNo);
            if(marginRatioInfo==null)
            {
                return new ResultObject<MarginRatioInfo>()
                           {
                               Result = null,
                               ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                               RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                           };
            }
            else
            {
                return new ResultObject<MarginRatioInfo>()
                           {
                               Result = marginRatioInfo,
                               ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                               RetCode = CommonEnums.RET_CODE.SUCCESS
                           };
            }
        }

        /// <summary>
        /// Gets the margin portfolio.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns>
        /// <para>A ResultObject&lt;List&lt;MarginPortfolio&gt;&gt; object contains returned code, 
        /// returned message and a list of MarginPortfolio objects that contains portfolio of the margin account.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: The is no data.</para>
        /// <para>RET_CODE=SUCCESS: Create data successfully.</para>
        /// </returns>
        public ResultObject<List<MarginPortfolio>> GetMarginPortfolio(string accountNo)
        {
            List<MarginPortfolio> listMarginPortfolio = _marginServices.GetMarginPortfolio(accountNo);
            if(listMarginPortfolio==null || listMarginPortfolio.Count==0)
            {
                return new ResultObject<List<MarginPortfolio>>()
                           {
                               Result = null,
                               ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                               RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                           };
            }
            else
            {
                return new ResultObject<List<MarginPortfolio>>()
                {
                    Result = listMarginPortfolio,
                    ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                    RetCode = CommonEnums.RET_CODE.SUCCESS
                };
            }
        }

        #endregion

        #region Cash Transfer
        /// <summary>
        /// Puts the cash trans order.
        /// </summary>
        /// <param name="sourceAccountID">The source account ID.</param>
        /// <param name="destAccountID">The dest account ID.</param>
        /// <param name="requestAmt">The request amt.</param>
        /// <param name="transType">Type of the trans.</param>
        /// <param name="note">The note.</param>
        /// <returns>
        /// <para> Result of putting cash transfer order. </para>
        /// <para>RET_CODE=ERROR_ACCOUNT: The account does not exist.</para>
        /// <para>RET_CODE=NOT_ALLOW: Customer is not allowed to do this action.</para>
        /// <para>RET_CODE=ERROR_REQUEST_AMOUNT: The amount is incorrect.</para>
        /// <para>RET_CODE=ERROR_INVALID_WITHDRAWAL: The withdrawal amount is incorrect.</para>
        /// <para>RET_CODE=ERROR_NOT_CASH_AVAILABLE: There is no available cash.</para>
        /// <para>RET_CODE=FAIL: Putting order failed.</para>
        /// <para>RET_CODE=SUCCESS: Putting order successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        public int PutCashTransOrder(List<AccountManager.Entities.SubCustAccount> TlistSubCustAccount, string sourceAccountID, string destAccountID,string bankName,string branchName,decimal requestAmt, int transType, string note)
        {
            try
            {
                if (TlistSubCustAccount == null)
                    return (int) CommonEnums.RET_CODE.FAIL;

                //only account with margin type can do
                SubCustAccount subCustAccount=TlistSubCustAccount.Where(n => n.SubCustAccountId.Equals(sourceAccountID)).FirstOrDefault();
                if(subCustAccount==null)
                    return (int) CommonEnums.RET_CODE.ERROR_ACCOUNT;

                int bankAccountType = (int)subCustAccount.BankAccountType;
                if (bankAccountType == (int)CommonEnums.BANK_ACCOUNT_TYPE.BANKACC)
                    return (int)CommonEnums.RET_CODE.NOT_ALLOW;

                //check valid account & same in a main account       
                //if (TlistSubCustAccount.Where(n => n.SubCustAccountId.Equals(sourceAccountID)).Count() == 0 || TlistSubCustAccount.Where(n => n.SubCustAccountId.Equals(destAccountID)).Count() == 0)                               
                //    return (int) CommonEnums.RET_CODE.ERROR_NOT_SAME_ACCOUNT;

                //check Request Amount 
                if (requestAmt <= 0)
                    return (int) CommonEnums.RET_CODE.ERROR_REQUEST_AMOUNT;

                ResultObject<CashTransferInfo> resultObjectCashTransferInfo = GetCashTransferInfo(sourceAccountID,
                                                                        ETradeCommon.Utils.GetAccountType(sourceAccountID));

                if (resultObjectCashTransferInfo.RetCode == CommonEnums.RET_CODE.SUCCESS && resultObjectCashTransferInfo.Result != null)
                {
                    //Request Amount > Available Amount -> fail
                    if (resultObjectCashTransferInfo.Result.AvilableAmt < requestAmt)
                        return (int)CommonEnums.RET_CODE.ERROR_REQUEST_AMOUNT;

                    CashTransfer cashTransfer=new CashTransfer();
                    cashTransfer.WithdrawableAmt = resultObjectCashTransferInfo.Result.WithdrawableAmt;
                    cashTransfer.TransferedAmt = resultObjectCashTransferInfo.Result.TransferedAmt;
                    cashTransfer.AdvOrderAmt = resultObjectCashTransferInfo.Result.AdvOrderAmt;
                    cashTransfer.AvilableAmt =cashTransfer.ApprovedAmt = resultObjectCashTransferInfo.Result.AvilableAmt;
                    cashTransfer.RequestAmt = requestAmt;
                    cashTransfer.RequestTime = DateTime.Now;
                    cashTransfer.BankName = bankName;
                    cashTransfer.BranchName = branchName;
                    cashTransfer.Fee = 0;
                    cashTransfer.Vat = 0;
                    cashTransfer.AmtAfterFee = cashTransfer.RequestAmt - cashTransfer.Fee;
                    cashTransfer.SrcAccountId = sourceAccountID;
                    cashTransfer.DestAccountId = destAccountID;
                    cashTransfer.TransType = transType;
                    cashTransfer.Status =(int) CommonEnums.CASH_TRANSFER_STATUS.PENDING;
                                                            
                    // transfer from Account Type 6 to 1
                    if (ETradeCommon.Utils.GetAccountType(sourceAccountID) == (int)CommonEnums.ACCOUNT_TYPE.MARGIN && ETradeCommon.Utils.GetAccountType(destAccountID) == (int)CommonEnums.ACCOUNT_TYPE.NORMAL)
                    {
                        //check invalid with drawal
                        if (cashTransfer.WithdrawableAmt < 0)
                            return (int)CommonEnums.RET_CODE.ERROR_INVALID_WITHDRAWAL;

                        var marginRatio = _marginServices.GetMarginRatio(sourceAccountID);
                        if (marginRatio != null)
                            if (marginRatio.EE < 0)
                                return (int)CommonEnums.RET_CODE.INVALID_EE_RATIO;
                    }
                    var result=_cashTransferService.PutCashTransOrder(cashTransfer);
                    if(result==(int)CommonEnums.RET_CODE.SUCCESS)
                    {
                        //send message
                        string message = string.Format(Resources.Resource.SMS_CASH_TRANSFER, sourceAccountID);
                        //_accountManagerServices.SendSMSAlertCashTransfer(message);
                    }
                    return result;
                }
                else
                    return (int) CommonEnums.RET_CODE.ERROR_NOT_CASH_AVAILABLE;
            }
            catch (Exception e)
            {
                return (int) CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        /// <summary>
        /// Gets the list unfinished cash transfer.
        /// </summary>
        /// <param name="subAccountId">The sub Account Id</param>
        /// <returns>ResultObject List CashTransfer</returns>
        public ResultObject<List<CashTransfer>> GetListUnfinishedCashTransfer(string subAccountId)
        {
            List<CashTransfer> listUnfinishedCashTransfer = _cashTransferService.GetListUnfinishedCashTransfer(subAccountId);
            if(listUnfinishedCashTransfer!=null && listUnfinishedCashTransfer.Count>0)
            {
                return new ResultObject<List<CashTransfer>>()
                           {
                               Result = listUnfinishedCashTransfer,
                               ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                               RetCode = CommonEnums.RET_CODE.SUCCESS
                           };
            }
            else
            {
                return new ResultObject<List<CashTransfer>>()
                {
                    Result = null,
                    ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                    RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                };
            }
        }

        /// <summary>
        /// Gets the total unfinished transfer amount (on status pending or processing)
        /// </summary>
        /// <param name="subAccountId">The sub account id.</param>
        /// <returns></returns>
        public decimal GetTotalUnfinishedTransferAmount(string subAccountId)
        {
            return _cashTransferService.GetTotalUnfinishedCashTransferAmount(subAccountId);
        }

        /// <summary>
        /// Gets the cash transfer info.
        /// </summary>
        /// <param name="subAccountId">The subAccountId.</param>
        /// <param name="accountType">Type of the account.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;CashTransferInfo;&gt;</see> object contains returned code, returned message and 
        /// CashTransferInfo object that contains cash transfer information.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// </returns>
        public ResultObject<CashTransferInfo> GetCashTransferInfo(string subAccountId, int accountType)
        {
            CashBalance cashBalance = _cashServices.GetCashBalance(subAccountId, accountType);

            if(cashBalance!=null)
            {
                CashTransferInfo cashTransferInfo = new CashTransferInfo();
                cashTransferInfo.subAccountId = subAccountId;
                cashTransferInfo.WithdrawableAmt = cashBalance.WithDraw;

                
                    /*
                    cashTransferInfo.AdvOrderAmt =
                        _validateServices.GetTotalConditionOrderMoney((char) CommonEnums.TRADE_SIDE.BUY,
                                                                      subAccountId, -1);
                     */

                cashTransferInfo.TransferedAmt = GetTotalUnfinishedTransferAmount(subAccountId);

                    /*cashTransferInfo.AvilableAmt = cashTransferInfo.WithdrawableAmt - cashTransferInfo.AdvOrderAmt -
                                                   cashTransferInfo.TransferedAmt;*/

                cashTransferInfo.AvilableAmt = cashTransferInfo.WithdrawableAmt - cashTransferInfo.TransferedAmt;
                cashTransferInfo.AvilableAmt = cashTransferInfo.AvilableAmt < 0 ? 0 : cashTransferInfo.AvilableAmt;

                return new ResultObject<CashTransferInfo>()
                           {
                               Result = cashTransferInfo,
                               ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                               RetCode = CommonEnums.RET_CODE.SUCCESS
                           };
            }
            else
            {
                return new ResultObject<CashTransferInfo>()
                           {
                                Result=null,
                                ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                                RetCode =CommonEnums.RET_CODE.NO_EXISTED_DATA
                           };
            }
        }

        /// <summary>
        /// Cashes the trans order hist.
        /// </summary>
        /// <param name="sourceAccountID">The source account ID.</param>
        /// <param name="destAccountID">The dest account ID.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="transType">Type of the trans.</param>
        /// <param name="status">The status.</param>
        /// <param name="note">The note.</param>
        /// <param name="brokerID">The broker ID.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;CashTransfer&gt;&gt;&gt;</see> object contains returned code, returned message and 
        /// list of CashTransfer objects that contains cash transfer information.</para>
        /// <para>RET_CODE=ERROR_INVALID_DATETIME: The sent date is invalid.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// </returns>
        public ResultObject<PagingObject<List<CashTransfer>>> GetListCashTransOrderHist(string sourceAccountID,string destAccountID,string fromDate,string toDate,int transType,int status,string note,string brokerID,int pageIndex,int pageSize)
        {    
            try
            {
                if (!ETradeCommon.Utils.IsValidDate(fromDate) || !ETradeCommon.Utils.IsValidDate(toDate))
                {
                    return new ResultObject<PagingObject<List<CashTransfer>>>()
                    {
                        Result = null,
                        ErrorMessage = CommonEnums.RET_CODE.ERROR_INVALID_DATETIME.ToString(),
                        RetCode = CommonEnums.RET_CODE.ERROR_INVALID_DATETIME
                    };
                }
                // convert from date string to DateTime value
                if (!string.IsNullOrEmpty(fromDate))
                {
                    var fromDateSearch=new DateTime(
                    int.Parse(fromDate.Substring(0, 4)),
                    int.Parse(fromDate.Substring(4, 2)),
                    int.Parse(fromDate.Substring(6, 2)));
                    fromDate = fromDateSearch.ToString("dd/MM/yyyy");
                }                
            }
            catch
            {               
                LogHandler.Log(
                    "GetListCashTransOrderHist: fromDate was incorrect format (yyyyMMdd), fromDate = " + fromDate,
                    GetType() + ".GetListCashTransOrderHist()",
                    TraceEventType.Warning);
            }

            try
            {
                // convert to date string to DateTime value
                if (!string.IsNullOrEmpty(toDate))
                {
                    var toDateSearch = new DateTime(
                    int.Parse(toDate.Substring(0, 4)),
                    int.Parse(toDate.Substring(4, 2)),
                    int.Parse(toDate.Substring(6, 2)));
                    toDate = toDateSearch.AddDays(1).ToString("dd/MM/yyyy");
                }
            }
            catch
            {
                LogHandler.Log(
                    "GetListCashTransOrderHist: toDate was incorrect format (yyyyMMdd), toDate = " + toDate,
                    GetType() + ".GetListCashTransOrderHist()",
                    TraceEventType.Warning);
            }

            PagingObject<List<CashTransfer>> listCashTransfer = _cashTransferService.GetListCashTransOrderHist(
                                                                sourceAccountID, destAccountID, fromDate, toDate, transType,
                                                                status, note, brokerID, pageIndex, pageSize);
            if(listCashTransfer!=null && listCashTransfer.Data!=null)
            {
                return new ResultObject<PagingObject<List<CashTransfer>>>
                           {
                               Result = listCashTransfer,
                               ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                               RetCode = CommonEnums.RET_CODE.SUCCESS
                           };
            }
            return new ResultObject<PagingObject<List<CashTransfer>>>
                       {
                           Result = listCashTransfer,
                           ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                           RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                       };
        }

        /// <summary>
        /// Cancel the cash transfer.
        /// </summary>
        /// <param name="id">The cash transfer id.</param>
        /// <param name="note">The note.</param>
        /// <returns>
        /// <para>Result of cancelling cash transfer.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=ERROR_CANNOT_CANCEL_CASH_TRANSFER: Cannot cancel cash transfer because it's in incorrect state.</para>
        /// <para>RET_CODE=SUCCESS: Cancel successfully.</para>
        /// <para>RET_CODE=FAIL: Fail to cancel cash transfer.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        public int CancelCashTransfer(long id,string note)
        {
            try
            {
                return _cashTransferService.CancelCashTransfer(id, DateTime.Now, note);
            }
            catch (Exception)
            {

                return (int) CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        #endregion

        #region Stock Transfer
        /// <summary>
        /// Puts the stock trans order.
        /// </summary>
        /// <param name="TlistSubCustAccount">The tlist sub cust account.</param>
        /// <param name="sourceAccountID">The source account ID.</param>
        /// <param name="destAccountID">The dest account ID.</param>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <param name="requestAmt">The request amt.</param>
        /// <param name="transType">Type of the trans.</param>
        /// <param name="note">The note.</param>
        /// <returns>
        /// <para>Result of putting stock transfer order.</para>
        /// <para>RET_CODE=ERROR_ACCOUNT: Account does not exist.</para>
        /// <para>RET_CODE=NOT_ALLOW: Customer is not allowed to put order.</para>
        /// <para>RET_CODE=ERROR_REQUEST_AMOUNT: The requested amount is incorrect.</para>
        /// <para>RET_CODE=ERROR_NOT_CASH_AVAILABLE: There is no available cash.</para>
        /// <para>RET_CODE=ERROR_DEBT_ACCOUNT: The account is in debt.</para>
        /// <para>RET_CODE=ERROR_NOT_STOCK_AVAILABLE: There is no available stock.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=FAIL: Failed to get data.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        public int PutStockTransOrder(List<AccountManager.Entities.SubCustAccount> TlistSubCustAccount, string sourceAccountID, string destAccountID,string secSymbol,long requestAmt, int transType, string note)
        {
            try
            {
                if (TlistSubCustAccount == null)
                    return (int) CommonEnums.RET_CODE.FAIL;

                ////only account with margin type can do
                SubCustAccount sourceCustAccount = TlistSubCustAccount.Where(n => n.SubCustAccountId.Equals(sourceAccountID)).FirstOrDefault();
                if (sourceCustAccount == null)
                    return (int)CommonEnums.RET_CODE.ERROR_ACCOUNT;

                int bankAccountType = (int)sourceCustAccount.BankAccountType;
                if (bankAccountType == (int)CommonEnums.BANK_ACCOUNT_TYPE.BANKACC)
                    return (int)CommonEnums.RET_CODE.NOT_ALLOW;

                //check Request Amount 
                if (requestAmt <= 0)
                    return (int) CommonEnums.RET_CODE.ERROR_REQUEST_AMOUNT;

                ResultObject<StockTransferInfo> resultObjectStockTransferInfo = GetStockTransferInfo(sourceAccountID,secSymbol,
                                                                                                     ETradeCommon.Utils.GetAccountType(sourceAccountID));


                if (resultObjectStockTransferInfo.RetCode == CommonEnums.RET_CODE.SUCCESS && resultObjectStockTransferInfo.Result != null)
                {
                    //transfer from margin account -> normal account
                    if(ETradeCommon.Utils.GetAccountType(sourceAccountID)==(int)CommonEnums.ACCOUNT_TYPE.MARGIN && ETradeCommon.Utils.GetAccountType(destAccountID)==(int)CommonEnums.ACCOUNT_TYPE.NORMAL)
                    {
                        CashBalance cashBalance = _cashServices.GetCashBalance(sourceAccountID,ETradeCommon.Utils.GetAccountType(sourceAccountID));
                        if(cashBalance==null)
                            return (int)CommonEnums.RET_CODE.ERROR_NOT_CASH_AVAILABLE;
                        
                        if(cashBalance.WithDraw<0)
                            return (int)CommonEnums.RET_CODE.ERROR_DEBT_ACCOUNT;

                        var marginRatio = _marginServices.GetMarginRatio(sourceAccountID);
                        if(marginRatio!=null)
                            if(marginRatio.EE<0)
                                return (int)CommonEnums.RET_CODE.INVALID_EE_RATIO;
                    }

                    //Request Amount > Available Amount -> fail
                    if (resultObjectStockTransferInfo.Result.AvilableAmt < requestAmt || resultObjectStockTransferInfo.Result.AvilableAmt==0)
                        return (int)CommonEnums.RET_CODE.ERROR_REQUEST_AMOUNT;
                    
                    StockTransfer stockTransfer = new StockTransfer();
                    stockTransfer.SecSymbol = secSymbol;
                    stockTransfer.WithdrawableAmt = resultObjectStockTransferInfo.Result.WithdrawableAmt;
                    stockTransfer.TransferedAmt = resultObjectStockTransferInfo.Result.TransferedAmt;
                    stockTransfer.AdvOrderAmt = resultObjectStockTransferInfo.Result.AdvOrderAmt;
                    stockTransfer.AvilableAmt = resultObjectStockTransferInfo.Result.AvilableAmt;
                    stockTransfer.RequestAmt = requestAmt;
                    stockTransfer.RequestTime = DateTime.Now;
                    stockTransfer.SrcAccountId = sourceAccountID;
                    stockTransfer.DestAccountId = destAccountID;
                    stockTransfer.TransType = transType;
                    stockTransfer.Status = (int)CommonEnums.CASH_TRANSFER_STATUS.PENDING;
                    stockTransfer.ApprovedAmt = 0;
                    return _stockTransferService.PutStockTransOrder(stockTransfer);
                }
               
                return (int) CommonEnums.RET_CODE.ERROR_NOT_STOCK_AVAILABLE;
            }
            catch (Exception e)
            {
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }
        /// <summary>
        /// Gets the list unfinished stock transfer.
        /// </summary>
        /// <param name="subAccountId">The sub account id.</param>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <returns></returns>
        public ResultObject<List<StockTransfer>> GetListUnfinishedStockTransfer(string subAccountId,string secSymbol)
        {
            List<StockTransfer> listUnfinishedStockTransfer =
                _stockTransferService.GetListUnfinishedStockTransfer(subAccountId, secSymbol);

            if(listUnfinishedStockTransfer!=null && listUnfinishedStockTransfer.Count>0)
            {
                return new ResultObject<List<StockTransfer>>()
                           {
                               Result = listUnfinishedStockTransfer,
                               ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                               RetCode = CommonEnums.RET_CODE.SUCCESS
                           };
            }
            else
            {
                return new ResultObject<List<StockTransfer>>()
                           {
                               Result = null,
                               ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                               RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                           };
            }
        }

        /// <summary>
        /// Gets the list stock trans order hist.
        /// </summary>
        /// <param name="sourceAccountID">The source account ID.</param>
        /// <param name="destAccountID">The dest account ID.</param>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="transType">Type of the trans.</param>
        /// <param name="status">The status.</param>
        /// <param name="note">The note.</param>
        /// <param name="brokerID">The broker ID.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// <para>A ResultObject&lt;PagingObject&lt;List&lt;StockTransfer&gt;&gt;&gt; object contains returned code, 
        /// returned message and a list of StockTransfer objects that contains stock transfer information.</para>
        /// <para>RET_CODE=ERROR_INVALID_DATETIME: The date is invalid.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: The is no data.</para>
        /// <para>RET_CODE=SUCCESS: Create data successfully.</para>
        /// </returns>
        public ResultObject<PagingObject<List<StockTransfer>>> GetListStockTransOrderHist(string sourceAccountID,string destAccountID,string secSymbol,string fromDate,string toDate,int transType,int status,string note,string brokerID,int pageIndex,int pageSize)
        {
            try
            {
                if (!ETradeCommon.Utils.IsValidDate(fromDate) || !ETradeCommon.Utils.IsValidDate(toDate))
                {
                    return new ResultObject<PagingObject<List<StockTransfer>>>()
                    {
                        Result = null,
                        ErrorMessage = CommonEnums.RET_CODE.ERROR_INVALID_DATETIME.ToString(),
                        RetCode = CommonEnums.RET_CODE.ERROR_INVALID_DATETIME
                    };
                }
                // convert from date string to DateTime value
                if (!string.IsNullOrEmpty(fromDate))
                {
                    var fromDateSearch = new DateTime(
                    int.Parse(fromDate.Substring(0, 4)),
                    int.Parse(fromDate.Substring(4, 2)),
                    int.Parse(fromDate.Substring(6, 2)));
                    fromDate = fromDateSearch.ToString("dd/MM/yyyy");
                }
            }
            catch
            {
                LogHandler.Log(
                    "GetListStockTransOrderHist: fromDate was incorrect format (yyyyMMdd), fromDate = " + fromDate,
                    GetType() + ".GetListStockTransOrderHist()",
                    TraceEventType.Warning);
            }
            try
            {
                // convert to date string to DateTime value
                if (!string.IsNullOrEmpty(toDate))
                {
                    var toDateSearch = new DateTime(
                    int.Parse(toDate.Substring(0, 4)),
                    int.Parse(toDate.Substring(4, 2)),
                    int.Parse(toDate.Substring(6, 2)));
                    toDate = toDateSearch.AddDays(1).ToString("dd/MM/yyyy");
                }
            }
            catch
            {
                LogHandler.Log(
                    "GetListStockTransOrderHist: toDate was incorrect format (yyyyMMdd), toDate = " + toDate,
                    GetType() + ".GetListStockTransOrderHist()",
                    TraceEventType.Warning);
            }
            PagingObject<List<StockTransfer>> listStockTransfer =
                _stockTransferService.GetListStockTransOrderHist(sourceAccountID, destAccountID, secSymbol, fromDate,
                                                                 toDate, transType, status, note, brokerID, pageIndex,
                                                                 pageSize);
            if(listStockTransfer!=null && listStockTransfer.Count>0)
            {
                return new ResultObject<PagingObject<List<StockTransfer>>>()
                           {
                               Result = listStockTransfer,
                               ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                               RetCode = CommonEnums.RET_CODE.SUCCESS
                           };
            }            
            else
            {
                return new ResultObject<PagingObject<List<StockTransfer>>>()
                           {
                               Result = null,
                               ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                               RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                           };
            }
        }

        /// <summary>
        /// Gets the cash transfer info.
        /// </summary>
        /// <param name="subAccountId">The sub account id.</param>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <param name="accountType">Type of the account.</param>
        /// <returns>
        /// <para>A ResultObject&lt;StockTransferInfo&gt; object contains returned code, 
        /// returned message and a list of StockTransferInfo objects that contains stock transfer information.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// </returns>
        public ResultObject<StockTransferInfo> GetStockTransferInfo(string subAccountId,string secSymbol, int accountType)
        {
            StockAvailable stockAvailable = _stockServices.GetStockAvailable(subAccountId, secSymbol, accountType);
            if(stockAvailable!=null)
            {
                StockTransferInfo stockTransferInfo=new StockTransferInfo();
                stockTransferInfo.subAccountId = subAccountId;
                stockTransferInfo.SecSymbol = secSymbol;
                stockTransferInfo.WithdrawableAmt = Convert.ToInt64(stockAvailable.AvaiVolume);
                stockTransferInfo.TransferedAmt =_stockTransferService.GetTotalUnfinishedStockTransferAmount(subAccountId, secSymbol);
                stockTransferInfo.AdvOrderAmt = Convert.ToInt64(_validateServices.GetTotalConditionOrderStock((char)CommonEnums.TRADE_SIDE.BUY,subAccountId,-1,secSymbol));
                /*stockTransferInfo.AvilableAmt = stockTransferInfo.WithdrawableAmt - stockTransferInfo.TransferedAmt -stockTransferInfo.AdvOrderAmt;*/
                stockTransferInfo.AvilableAmt = stockTransferInfo.WithdrawableAmt - stockTransferInfo.TransferedAmt;
                stockTransferInfo.AvilableAmt = stockTransferInfo.AvilableAmt < 0 ? 0 : stockTransferInfo.AvilableAmt;


                return new ResultObject<StockTransferInfo>()
                           {
                               Result = stockTransferInfo,
                               ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                               RetCode =CommonEnums.RET_CODE.SUCCESS
                           };
            }
            else
            {
                return new ResultObject<StockTransferInfo>()
                           {
                               Result = null,
                               ErrorMessage=CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                               RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                           };
            }
        }

        /// <summary>
        /// Gets the list stock transfer info.
        /// </summary>
        /// <param name="subAccountId">The sub account id.</param>
        /// <param name="accountType">Type of the account.</param>
        /// <param name="pageIndex">Page index.</param>
        /// <param name="pageSize">Page size.</param>
        /// <returns>
        /// <para>A ResultObject&lt;List&lt;StockTransferInfo&gt;&gt; object contains returned code, 
        /// returned message and a list of StockTransferInfo objects that contains stock transfer information.</para>
        /// <para>RET_CODE=ERROR_NOT_CASH_AVAILABLE: The is no data of cash.</para>
        /// <para>RET_CODE=ERROR_DEBT_ACCOUNT: Account is in debt.</para>
        /// <para>RET_CODE=SUCCESS: Create data successfully.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: The is no data.</para>
        /// </returns>
        public ResultObject<PagingObject<List<StockTransferInfo>>> GetListStockTransferInfo(string subAccountId, int accountType,int pageIndex,int pageSize)
        {
            if (accountType == (int)CommonEnums.ACCOUNT_TYPE.MARGIN)
            {
                CashBalance cashBalance = _cashServices.GetCashBalance(subAccountId, accountType);
                if (cashBalance == null)
                    return new ResultObject<PagingObject<List<StockTransferInfo>>>()
                               {
                                   Result = null,
                                   ErrorMessage = CommonEnums.RET_CODE.ERROR_NOT_CASH_AVAILABLE.ToString(),
                                   RetCode = CommonEnums.RET_CODE.ERROR_NOT_CASH_AVAILABLE
                               };

                if (cashBalance.WithDraw < 0)
                    return new ResultObject<PagingObject<List<StockTransferInfo>>>()
                               {
                                   Result = null,
                                   ErrorMessage = CommonEnums.RET_CODE.ERROR_DEBT_ACCOUNT.ToString(),
                                   RetCode = CommonEnums.RET_CODE.ERROR_DEBT_ACCOUNT
                               };
            }

            List<string> listPortfolio = _stockServices.GetListPortfolio(subAccountId, accountType);
            if(listPortfolio!=null && listPortfolio.Count>0)
            {
                PagingObject<List<StockTransferInfo>> listStockTransferInfo=new PagingObject<List<StockTransferInfo>>();
                listStockTransferInfo.Data = new List<StockTransferInfo>();
                foreach (var portfolioInfo in listPortfolio)
                {
                    StockAvailable stockAvailable = _stockServices.GetStockAvailable(subAccountId, portfolioInfo.ToString(), accountType);
                    if (stockAvailable != null)
                    {
                        StockTransferInfo stockTransferInfo = new StockTransferInfo();
                        stockTransferInfo.subAccountId = subAccountId;
                        stockTransferInfo.SecSymbol = portfolioInfo.ToString();
                        stockTransferInfo.WithdrawableAmt = Convert.ToInt64(stockAvailable.AvaiVolume);
                        stockTransferInfo.TransferedAmt = _stockTransferService.GetTotalUnfinishedStockTransferAmount(subAccountId, portfolioInfo);
                        //stockTransferInfo.AdvOrderAmt = Convert.ToInt64(_validateServices.GetTotalConditionOrderStock((char)CommonEnums.TRADE_SIDE.BUY, subAccountId, -1, portfolioInfo));
                        //stockTransferInfo.AvilableAmt = stockTransferInfo.WithdrawableAmt - stockTransferInfo.TransferedAmt - stockTransferInfo.AdvOrderAmt;
                        stockTransferInfo.AvilableAmt = stockTransferInfo.WithdrawableAmt -
                                                        stockTransferInfo.TransferedAmt;
                        stockTransferInfo.AvilableAmt = stockTransferInfo.AvilableAmt < 0
                                                            ? 0
                                                            : stockTransferInfo.AvilableAmt;
                        listStockTransferInfo.Data.Add(stockTransferInfo);
                    }
                }
                listStockTransferInfo.Data =
                    listStockTransferInfo.Data.Skip(((pageIndex - 1)*pageSize)).Take(pageSize).ToList();
                listStockTransferInfo.Count = listPortfolio.Count;

                return new ResultObject<PagingObject<List<StockTransferInfo>>>()
                           {
                               Result = listStockTransferInfo,
                               ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                               RetCode = CommonEnums.RET_CODE.SUCCESS
                           };
            }
           
            return new ResultObject<PagingObject<List<StockTransferInfo>>>()
            {
                Result = null,
                ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
            };
        }
       
        /// <summary>
        /// Cancels the stock transfer.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="note">The note.</param>
        /// <returns>
        /// <para>Result of cancelling stock transfer.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=ERROR_CANNOT_CANCEL_STOCK_TRANSFER: Cannot cancel stock transfer because it's in incorrect state.</para>
        /// <para>RET_CODE=SUCCESS: Cancel successfully.</para>
        /// <para>RET_CODE=FAIL: Fail to cancel odd lot order.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        public int CancelStockTransfer(long id,string note)
        {
            try
            {
                return _stockTransferService.CancelStockTransfer(id, DateTime.Now, note);
            }
            catch (Exception)
            {

                return (int) CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        #endregion

        #region XR Order
        
        /// <summary>
        /// Gets the list buy right.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;AccountManager.Entities.BuyRight&gt;&gt;&gt;</see> object contains returned code, returned message and
        /// list of BuyRignt objects that contains buy right information.
        /// </returns>
        public PagingObject<List<BuyRight>> GetListBuyRight(string accountNo,string tradeDate, int pageIndex,int pageSize)
        {            
            var listBuyRight = new PagingObject<List<BuyRight>>();
            if (string.IsNullOrEmpty(tradeDate))
            {
                listBuyRight.Data=_stockServices.GetListBuyRight(accountNo, DateTime.Now.ToString("yyyyMMdd"));                
            }
            if(listBuyRight.Data!=null)
            {
                foreach (var buyRight in listBuyRight.Data)
                {
                    var stockInfo =Serializer.Deserialize<ResultObject<StockInfo>>(
                                _rtServices.GetStockInfo(buyRight.SecSymbol));

                    if (stockInfo.Result != null)
                    {
                        buyRight.Market = ETradeCommon.Utils.GetMarketName(stockInfo.Result.MarketID);                        
                    }
                    long registeredAmount = _xrOrdersService.GetTotalUnfinishedXROrderRegisterAmount(-1, accountNo, buyRight.SecSymbol, string.Empty);
                    buyRight.RegisteredVol += registeredAmount;
                    buyRight.AllowedVol = buyRight.AllowedVol - buyRight.RegisteredVol;
                }
                listBuyRight.Count = listBuyRight.Data.Count;
                listBuyRight.Data = listBuyRight.Data.Skip(pageIndex - 1).Take(pageSize).ToList();
            }
            return listBuyRight;            
        }

        /// <summary>
        /// Puts the XR order.
        /// </summary>
        /// <param name="subAccountId">The sub account ID.</param>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <param name="market">The market.</param>
        /// <param name="requestVol">The request vol.</param>
        /// <param name="note">The note.</param>
        /// <returns>
        /// 	<para>Result of putting XR order.</para>
        /// 	<para>RET_CODE=ERROR_REQUEST_VOLUME_BUY_RIGHT: Requested volume is incorrect.</para>
        /// 	<para>RET_CODE=ERROR_OVER_REQUEST_CAN_BUY_RIGHT: Requested volume is higher than allowed volume.</para>
        /// 	<para>RET_CODE=ERROR_NOT_EXIST_BUY_RIGHT: There is no buy right data.</para>
        /// 	<para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        public int PutXROrder(string subAccountId, string secSymbol, char market, long requestVol, string note)
        {
            try
            {
                var pagingObjectListBuyRight= GetListBuyRight(subAccountId, string.Empty, 1,int.MaxValue);                

                if (pagingObjectListBuyRight.Data != null && pagingObjectListBuyRight.Data.Count > 0)
                {
                    BuyRight buyRight = pagingObjectListBuyRight.Data.Where(n=>n.SecSymbol.Equals(secSymbol)).FirstOrDefault();
                    if(buyRight!=null)
                    {
                        if(requestVol<=0)
                        {
                            return (int)CommonEnums.RET_CODE.ERROR_REQUEST_VOLUME_BUY_RIGHT; 
                        }
                        if (requestVol > buyRight.AllowedVol  || buyRight.AllowedVol < 0)
                        {
                            return (int)CommonEnums.RET_CODE.ERROR_OVER_REQUEST_CAN_BUY_RIGHT;
                        }
                        int accounttype = ETradeCommon.Utils.GetAccountType(subAccountId);
                        var cashAvailable = _cashServices.GetAvailableCash(subAccountId, accounttype, false);
                        if(cashAvailable!=null)
                        {
                            if (accounttype == (int)CommonEnums.ACCOUNT_TYPE.NORMAL)
                            {
                                if (cashAvailable.BuyCredit < requestVol * buyRight.Price * Constants.MONEY_UNIT)
                                    return (int)CommonEnums.RET_CODE.ERROR_NOT_ENOUGH_CASH;
                            }
                            else
                            {
                                if (cashAvailable.EE < requestVol * buyRight.Price * Constants.MONEY_UNIT)
                                    return (int) CommonEnums.RET_CODE.ERROR_NOT_ENOUGH_CASH;
                            }
                        }
                        else
                        {
                            return (int)CommonEnums.RET_CODE.ERROR_NOT_ENOUGH_CASH;
                        }

                        XrOrders xrOrders=new XrOrders();
                        xrOrders.SubAccountId=subAccountId;                        
                        xrOrders.SecSymbol=secSymbol;
                        xrOrders.Market = market.ToString();
                        xrOrders.Volume = buyRight.AllowedVol;
                        xrOrders.Price = buyRight.Price;
                        xrOrders.RegisteredVol = buyRight.RegisteredVol;                        
                        xrOrders.RequestVol = requestVol;
                        xrOrders.RequestTime = DateTime.Now;
                        xrOrders.ApprovedVol = 0;
                        xrOrders.Status =(int) CommonEnums.XRORDER_STATUS.PENDING;
                        xrOrders.Note = note;
                        return _xrOrdersService.PutXROrder(xrOrders);
                    }
                    else
                    {
                        return (int)CommonEnums.RET_CODE.ERROR_NOT_EXIST_BUY_RIGHT;
                    }
                }
                else
                {
                    return (int)CommonEnums.RET_CODE.ERROR_NOT_EXIST_BUY_RIGHT;
                }
            }
            catch (Exception e)
            {
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }            
        }

        /// <summary>
        /// Cancels the XR order.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="note">The note.</param>
        /// <returns>
        /// <para>Result of cancelling XR order.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=ERROR_CANNOT_CANCEL_XRORDER: Cannot cancel XR order because it's in incorrect state.</para>
        /// <para>RET_CODE=SUCCESS: Cancel successfully.</para>
        /// <para>RET_CODE=FAIL: Fail to cancel odd lot order.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        public int CancelXROrder(long id,string note)
        {
            try
            {
                return _xrOrdersService.CancelXROrder(id, note);               
            }
            catch (Exception e)
            {
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        /// <summary>
        /// Gets the list XR order hist.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="subAccountId">The sub account id.</param>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <param name="market">The market.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="status">The status.</param>
        /// <param name="brokerID">The broker ID.</param>
        /// <param name="note">The note.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// <para>A ResultObject&lt;PagingObject&lt;List&lt;XrOrders&gt;&gt;&gt; object contains returned code, 
        /// returned message and a list of XrOrders objects that contains XR order information.</para>
        /// <para>RET_CODE=ERROR_INVALID_DATETIME: The date is invalid.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: The is no data.</para>
        /// <para>RET_CODE=SUCCESS: Create data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        public ResultObject<PagingObject<List<XrOrders>>> GetListXROrderHist(long id,string subAccountId,string secSymbol,string market,string fromDate,string toDate,int status,string brokerID,string note,int pageIndex,int pageSize)
        {
            try
            {
                if (!ETradeCommon.Utils.IsValidDate(fromDate) || !ETradeCommon.Utils.IsValidDate(toDate))
                {
                    return new ResultObject<PagingObject<List<XrOrders>>>
                    {
                        Result = null,
                        ErrorMessage = CommonEnums.RET_CODE.ERROR_INVALID_DATETIME.ToString(),
                        RetCode = CommonEnums.RET_CODE.ERROR_INVALID_DATETIME
                    };
                }
                PagingObject<List<XrOrders>> resultObject = _xrOrdersService.GetListXROrderHist(id,
                                                                                              subAccountId,
                                                                                              secSymbol,
                                                                                              market,
                                                                                              fromDate,
                                                                                              toDate,
                                                                                              status,
                                                                                              brokerID,
                                                                                              note,
                                                                                              pageIndex,
                                                                                              pageSize);
                if(resultObject!=null && resultObject.Count>0)
                {
                    return new ResultObject<PagingObject<List<XrOrders>>>()
                    {
                        Result = resultObject,
                        ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                        RetCode = CommonEnums.RET_CODE.SUCCESS
                    };
                }
                else
                {
                    return new ResultObject<PagingObject<List<XrOrders>>>()
                    {
                        Result = null,
                        ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                        RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                    };
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, Constants.EXCEPTION_POLICY);
                return new ResultObject<PagingObject<List<XrOrders>>>()
                           {
                               Result = null,
                               ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString(),
                               RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR
                           };
            }
        }

        #endregion

        #region Odd Lot Order
        /// <summary>
        /// Gets the odd lot info.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="accountType">Type of the account.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;List&lt;OddLotOrderInfo&gt;&gt;</see> object contains returned code, returned message and 
        /// list of OddLotOrderInfo objects.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Getting data successfully.</para>
        /// </returns>
          public ResultObject<List<OddLotOrderInfo>> GetOddLotOrderInfo(string accountNo,int accountType)
        {
            var listPortfolio = _stockServices.GetListPortfolio(accountNo, accountType);
            if(listPortfolio!=null && listPortfolio.Count>0)
            {
                List<OddLotOrderInfo> listOddLotOrderInfo = new List<OddLotOrderInfo>();
                foreach (var symbol in listPortfolio)
                {
                    StockAvailable stockAvailable = _stockServices.GetStockAvailable(accountNo, symbol,
                                                                                         accountType);
                    var resultObjectStockInfo =Serializer.Deserialize<ResultObject<StockInfo>>(_rtServices.GetStockInfo(symbol));

                    if (stockAvailable !=null && resultObjectStockInfo.Result!=null)
                    {
                        OddLotOrderInfo oddLotOrder=new OddLotOrderInfo();
                        oddLotOrder.AccountNo = accountNo;
                        oddLotOrder.Symbol = symbol;
                        oddLotOrder.AvgPrice = Convert.ToDecimal(resultObjectStockInfo.Result.RefPrice * Constants.PERCENT_PRICE_BUY_ODD_LOT);
                        int oddLot = 0;
                        long totalUnfinished = _oddLotOrderService.GetTotalUnfinishedOddLotOrderAmount(accountNo, symbol);
                        bool isShow = true;
                        switch (resultObjectStockInfo.Result.MarketID)
                        {
                            case (int)CommonEnums.MARKET_ID.HOSE:
                                oddLot = Convert.ToInt16(stockAvailable.AvaiVolume % 10);
                                if(oddLot-totalUnfinished<=0)
                                {
                                    isShow = false;
                                    break;
                                }

                                if (oddLot >= 1 && oddLot <= 9)
                                {
                                    oddLotOrder.AvaiVolume = oddLot;
                                    oddLotOrder.MarketId = resultObjectStockInfo.Result.MarketID;
                                }
                                break;

                            case (int)CommonEnums.MARKET_ID.HNX:
                            case (int)CommonEnums.MARKET_ID.UPCoM:
                                oddLot = Convert.ToInt16(stockAvailable.AvaiVolume % 100);
                                if (oddLot - totalUnfinished <= 0)
                                {
                                    isShow = false;
                                    break;
                                }
                                if (oddLot >= 1 && oddLot <= 99)
                                {
                                    oddLotOrder.AvaiVolume = oddLot;
                                    oddLotOrder.MarketId = resultObjectStockInfo.Result.MarketID;
                                }
                                break;
                            
                            default:
                                isShow = false;
                                break;
                        }
                        
                        if(isShow)
                            listOddLotOrderInfo.Add(oddLotOrder);                                                
                    }
                }
                return new ResultObject<List<OddLotOrderInfo>>()
                         {
                             Result = listOddLotOrderInfo,
                             ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                             RetCode =CommonEnums.RET_CODE.SUCCESS
                       };
            }

            return new ResultObject<List<OddLotOrderInfo>>
                       {
                             Result = null,
                             ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                             RetCode =CommonEnums.RET_CODE.NO_EXISTED_DATA
                       };
        }

        /// <summary>
        /// Gets the list odd lot order hist.
        /// </summary>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <param name="side">The side.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="subCustAccountID">The sub cust account ID.</param>
        /// <param name="market">The market.</param>
        /// <param name="status">The status.</param>
        /// <param name="brokerID">The broker ID.</param>
        /// <param name="note">The note.</param>
        /// <param name="pageIndex">Page index.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;OddLotOrder&gt;&gt;&gt;</see> object contains returned code, returned message and 
        /// list of OddLotOrder objects that contains odd lot order information.</para>
        /// <para>RET_CODE=ERROR_INVALID_DATETIME: The date is invalid.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// </returns>
        public ResultObject<PagingObject<List<OddLotOrder>>> GetListOddLotOrderHist(string secSymbol,string side,string fromDate,string toDate,string subCustAccountID,string market,int status,string brokerID,string note,int pageIndex,int pageSize)
        {
            try
            {
                if (!ETradeCommon.Utils.IsValidDate(fromDate) || !ETradeCommon.Utils.IsValidDate(toDate))
                {
                    return new ResultObject<PagingObject<List<OddLotOrder>>>
                    {
                        Result = null,
                        ErrorMessage = CommonEnums.RET_CODE.ERROR_INVALID_DATETIME.ToString(),
                        RetCode = CommonEnums.RET_CODE.ERROR_INVALID_DATETIME
                    };
                }
                // convert from date string to DateTime value
                if (!string.IsNullOrEmpty(fromDate))
                {
                    var fromDateSearch=new DateTime(
                    int.Parse(fromDate.Substring(0, 4)),
                    int.Parse(fromDate.Substring(4, 2)),
                    int.Parse(fromDate.Substring(6, 2)));
                    fromDate = fromDateSearch.ToString("dd/MM/yyyy");
                }                
            }
            catch
            {               
                LogHandler.Log(
                    "GetListOddLotOrderHist: fromDate was incorrect format (yyyyMMdd), fromDate = " + fromDate,
                    GetType() + ".GetListOddLotOrderHist()",
                    TraceEventType.Warning);
            }

            try
            {
                // convert to date string to DateTime value
                if (!string.IsNullOrEmpty(toDate))
                {
                    var toDateSearch = new DateTime(
                    int.Parse(toDate.Substring(0, 4)),
                    int.Parse(toDate.Substring(4, 2)),
                    int.Parse(toDate.Substring(6, 2)));
                    toDate = toDateSearch.AddDays(1).ToString("dd/MM/yyyy");
                }
            }
            catch
            {
                LogHandler.Log(
                    "GetListOddLotOrderHist: toDate was incorrect format (yyyyMMdd), toDate = " + toDate,
                    GetType() + ".GetListOddLotOrderHist()",
                    TraceEventType.Warning);
            }
              PagingObject<List<OddLotOrder>> pagingOddLotOrder = _oddLotOrderService.GetListOddLotOrderHist(secSymbol,
                                                                                                             side,
                                                                                                             fromDate,
                                                                                                             toDate,
                                                                                                             subCustAccountID,
                                                                                                             market,
                                                                                                             status,
                                                                                                             brokerID,
                                                                                                             note,pageIndex, pageSize );

            if (pagingOddLotOrder != null && pagingOddLotOrder.Data!=null)
            {
                return new ResultObject<PagingObject<List<OddLotOrder>>>
                           {
                               Result = pagingOddLotOrder,
                               ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                               RetCode = CommonEnums.RET_CODE.SUCCESS    
                           };
            }
            return new ResultObject<PagingObject<List<OddLotOrder>>>()
            {
                Result = null,
                ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
            };
        }

        /// <summary>
        /// Gets the list odd lot order.
        /// </summary>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <param name="side">The side.</param>
        /// <param name="subCustAccountID">The sub cust account ID.</param>
        /// <param name="market">The market.</param>
        /// <param name="status">The status.</param>
        /// <param name="brokerID">The broker ID.</param>
        /// <param name="note">The note.</param>
        /// <param name="pageIndex">Page index.</param>
        /// <param name="pageSize">Page size.</param>
        /// <returns></returns>
        public ResultObject<PagingObject<List<OddLotOrder>>> GetListOddLotOrder(string secSymbol,string side,string subCustAccountID,string market,int status,string brokerID,string note,int pageIndex,int pageSize)
        {
            return GetListOddLotOrderHist(secSymbol, side, string.Empty, string.Empty, subCustAccountID, market, status,brokerID, note,pageIndex, pageSize);
        }

        /// <summary>
        /// Cancel the odd lot order.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="note">The note.</param>
        /// <returns>
        /// <para>Result of cancelling odd lot order.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=ERROR_CANNOT_CANCEL_ODD_LOT_ORDER: Cannot cancel odd lot order because it's in incorrect state.</para>
        /// <para>RET_CODE=SUCCESS: Cancel successfully.</para>
        /// <para>RET_CODE=FAIL: Fail to cancel odd lot order.</para>
        /// </returns>
        public int CancelOddLotOrder(long id,string note)
        {
            try
            {
                return _oddLotOrderService.CancelOddLotOrder(id, DateTime.Now, note);
            }
            catch (Exception e)
            {
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        /// <summary>
        /// Puts the odd lot order.
        /// </summary>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <param name="side">The side.</param>
        /// <param name="price">The price.</param>
        /// <param name="volume">The volume.</param>
        /// <param name="subCustAccountID">The sub cust account ID.</param>
        /// <param name="market">The market.</param>
        /// <param name="note">The note.</param>
        /// <returns>
        /// <para>
        /// Result of putting odd lot order.
        /// </para>
        /// <para>RET_CODE=FAIL: Putting order failed.</para>
        /// <para>RET_CODE=SUCCESS: Putting order successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>       
        public int PutOddLotOrder(string secSymbol,string side,decimal price,long volume,string subCustAccountID,string market,string note)
        {
            try
            {
                if (volume <= 0)
                    return (int) CommonEnums.RET_CODE.FAIL;
                
                //validate put odd lot order
                StockAvailable stockAvailable = _stockServices.GetStockAvailable(subCustAccountID, secSymbol,
                                                                                         ETradeCommon.Utils.GetAccountType(subCustAccountID));
                var resultObjectStockInfo = Serializer.Deserialize<ResultObject<StockInfo>>(_rtServices.GetStockInfo(secSymbol));

                if (stockAvailable != null && resultObjectStockInfo.Result != null)
                {
                    int oddLot;
                    long totalUnfinished = _oddLotOrderService.GetTotalUnfinishedOddLotOrderAmount(subCustAccountID, secSymbol);
                    bool isValid = true;
                    switch (resultObjectStockInfo.Result.MarketID)
                    {
                        case (int)CommonEnums.MARKET_ID.HOSE:
                            oddLot = Convert.ToInt16(stockAvailable.AvaiVolume % 10);
                            if (oddLot - totalUnfinished <= 0)                            
                                isValid = false;                                
                            
                            break;
                        case (int)CommonEnums.MARKET_ID.HNX:
                        case (int)CommonEnums.MARKET_ID.UPCoM:
                            oddLot = Convert.ToInt16(stockAvailable.AvaiVolume % 100);
                            if (oddLot - totalUnfinished <= 0)                            
                                isValid = false;
                            break;

                        default:
                            isValid = false;
                            break;
                    }

                    if (!isValid)
                    {
                        return (int) CommonEnums.RET_CODE.FAIL;
                    }                    
                }

                var oddLotOrder=new OddLotOrder();
                oddLotOrder.SecSymbol=secSymbol;
                oddLotOrder.Side=side;
                oddLotOrder.Price=price;
                oddLotOrder.Volume=volume;
                oddLotOrder.SubCustAccountId=subCustAccountID;
                oddLotOrder.Market=market;
                oddLotOrder.ExecPrice = 0;
                oddLotOrder.ExecVol = 0;
                oddLotOrder.CanceledVol = 0;
                oddLotOrder.Status =(int) CommonEnums.ODD_LOT_ORDER_STATUS.PENDING;
                oddLotOrder.RequestTime = DateTime.Now;
                oddLotOrder.Note = note;
                return _oddLotOrderService.PutOddLotOrder(oddLotOrder);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, Constants.EXCEPTION_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }
        #endregion
    }
}
