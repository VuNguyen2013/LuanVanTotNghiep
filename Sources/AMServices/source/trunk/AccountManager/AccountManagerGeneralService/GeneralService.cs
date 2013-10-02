using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using AccountManager.Entities;
using AccountManager.Services;
using ETradeCommon;
using ETradeCommon.Enums;
using ETradeCore.Services;
using ETradeFinance.Entities;
using ETradeFinance.Services;

namespace AccountManagerGeneralService
{
    public class GeneralService : IGeneralService
    {
        private static readonly MainCustAccountService MainCustAccountService = new MainCustAccountService();
        private static readonly IAuthenticationService AuthenticationService = new RSAAuthenticationServices();

        #region Cash Advance
        ///<summary>
        /// Get list of Cash advance history
        ///</summary>
        ///<param name="subAccountId">Sub account id</param>
        ///<param name="fromDate">Advance date from, format DD/MM/YYYY</param>
        ///<param name="toDate">Advance date to, format DD/MM/YYYY</param>
        ///<param name="contractNo">Contract no</param>
        ///<param name="sellDueDateTo">Sell Due date from, format DD/MM/YYYY</param>
        ///<param name="sellDueDateFrom">Sell Due date from, format DD/MM/YYYY</param>
        ///<param name="status">Status</param>
        ///<param name="tradeType">Trade type</param>
        ///<param name="brokerId">Broker Id</param>
        ///<param name="pageIndex">Page index</param>
        ///<param name="pageSize">Page size</param>
        ///<param name="count">Total records</param>
        ///<returns>
        /// <para>Return list of CashAdvance object that contains CashAdvance information.</para>
        /// </returns>
        public List<CashAdvanceHistory> GetListCashAdvanceHistory(string subAccountId, string fromDate, string toDate,
            string contractNo, string sellDueDateFrom, string sellDueDateTo, int status, int tradeType,
            string brokerId, int pageIndex, int pageSize, out int count)
        {
            // Get cash advance history list     
            count = 0;
            List<CashAdvanceHistory> listCashAdvanceFromOTSDb = new List<CashAdvanceHistory>();
            List<ETradeCore.Entities.CashAdvance> listCashAdvanceFromCore = new List<ETradeCore.Entities.CashAdvance>();
            ETradeCore.Services.CashAdvanceServices _cashAdvanceServices = new CashAdvanceServices();
            var cashAdvanceHistoryService = new ETradeFinance.Services.CashAdvanceHistoryService();
           
            switch (status)
            {
                case (int)CommonEnums.ADVANCE_STATUS.FINISHED:
                    // Get advance finished from core
                    listCashAdvanceFromCore = _cashAdvanceServices.GetAdvanceHistoryFromCore(
                        subAccountId, fromDate, toDate, sellDueDateFrom, sellDueDateTo, contractNo);
                    break;
                case (int)CommonEnums.ADVANCE_STATUS.CANCELLED:
                case (int)CommonEnums.ADVANCE_STATUS.PENDING:
                case (int)CommonEnums.ADVANCE_STATUS.REJECTED:
                case (int)CommonEnums.ADVANCE_STATUS.PROCESSING:
                    // Get advance history (cancelled, rejected) from Ots database
                    listCashAdvanceFromOTSDb = cashAdvanceHistoryService.GetListCashAdvanceHistory(subAccountId, fromDate, toDate,
                                                                                 contractNo, sellDueDateFrom,
                                                                                 sellDueDateTo, status, tradeType,
                                                                                 brokerId, pageIndex, pageSize,
                                                                                 out count);
                    break;

                default:
                case (int)CommonEnums.ADVANCE_STATUS.ALL:
                    // Get advance finished from core
                    listCashAdvanceFromCore = _cashAdvanceServices.GetAdvanceHistoryFromCore(
                       subAccountId, fromDate, toDate, sellDueDateFrom, sellDueDateTo, contractNo);

                    // Get advance history (cancelled, rejected) from Ots database
                    listCashAdvanceFromOTSDb = cashAdvanceHistoryService.GetListCashAdvanceHistory(subAccountId, fromDate, toDate,
                                                                                  contractNo, sellDueDateFrom,
                                                                                  sellDueDateTo, status, tradeType,
                                                                                  brokerId, pageIndex, pageSize,
                                                                                  out count);
                    listCashAdvanceFromOTSDb =
                       listCashAdvanceFromOTSDb.Where(
                           cashAdvance => cashAdvance.Status != (int)CommonEnums.ADVANCE_STATUS.FINISHED).ToList();
                    break;
            }
            var brokerAccountService = new BrokerAccountService();
            var brokerList = brokerAccountService.GetList(string.Empty, string.Empty, -1, 1, string.Empty, string.Empty,
                                                          1, int.MaxValue);
            List<CashAdvanceHistory> listTotalCashAdvanceHistory=new List<CashAdvanceHistory>();
            if(listCashAdvanceFromOTSDb!=null && listCashAdvanceFromOTSDb.Count>0)
            {
                listTotalCashAdvanceHistory.AddRange(listCashAdvanceFromOTSDb);                
            }
            if (listCashAdvanceFromCore!=null && listCashAdvanceFromCore.Count>0)
            {
                count += listCashAdvanceFromCore.Count;
                foreach (var cashAdvance in listCashAdvanceFromCore)
                {
                    CashAdvanceHistory cashAdvanceHistory=new CashAdvanceHistory();
                    cashAdvanceHistory.AdvanceDate = cashAdvance.EditDate;
                    cashAdvanceHistory.BrokerId = cashAdvance.BrokerId;
                    
                    cashAdvanceHistory.CashRequest = cashAdvance.GrossWithDraw;
                    cashAdvanceHistory.CashDueDate = cashAdvance.DueDate;
                    cashAdvanceHistory.CashReceived = cashAdvance.NetWithDraw;
                    cashAdvanceHistory.ContractNo = cashAdvance.ContractNo;
                    cashAdvanceHistory.ExecTime = cashAdvance.EditDate;
                    cashAdvanceHistory.Fee = cashAdvance.AdvFee;
                    cashAdvanceHistory.SellDueDate = cashAdvance.TradeDate;
                    cashAdvanceHistory.Status =(int) CommonEnums.ADVANCE_STATUS.FINISHED;
                    cashAdvanceHistory.SubAccountId = cashAdvance.AccountNo2;
                    cashAdvanceHistory.Vat = cashAdvance.AdvVat;
                    listTotalCashAdvanceHistory.Add(cashAdvanceHistory);
                }
            }
            listTotalCashAdvanceHistory =
                listTotalCashAdvanceHistory.OrderBy(cashAdvance => cashAdvance.AdvanceDate).ToList();
                     
            // Update broker name to cash advance history
            foreach (var cashAdvanceHistory in listTotalCashAdvanceHistory)
            {
                if (!string.IsNullOrEmpty(cashAdvanceHistory.BrokerId))
                {
                    BrokerAccount brokerAccount =
                       brokerList.Data.Where(broker => broker.BrokerId.Equals(cashAdvanceHistory.BrokerId)).FirstOrDefault();
                    if (brokerAccount != null)
                    {
                        cashAdvanceHistory.BrokerName = brokerAccount.Name;
                    }
                }
            }

            if(listTotalCashAdvanceHistory.Count>0)
            {
                // Paging advance history
                int startIndex = 0;                
                if (pageIndex == 0)
                {
                    startIndex = 0;
                    count = listTotalCashAdvanceHistory.Count;
                }
                else
                {
                    startIndex = (pageIndex - 1) * pageSize;
                    int remainsItemCount = listTotalCashAdvanceHistory.Count - startIndex;
                    count = (remainsItemCount > pageSize) ? pageSize : remainsItemCount;
                }

                return listTotalCashAdvanceHistory.GetRange(startIndex, count);
            }

            return listTotalCashAdvanceHistory;
        }

        ///<summary>
        /// Get list of Cash advances
        ///</summary>
        ///<param name="subAccountId">Sub account id</param>
        ///<param name="contractNo">Contract no</param>
        ///<param name="status">Status</param>
        ///<param name="tradeType">Trade type</param>
        ///<param name="pageIndex">Page index</param>
        ///<param name="pageSize">Page size</param>
        ///<param name="count">Total of records</param>
        ///<returns>
        /// <para>Return list of CashAdvance object that contains CashAdvance information.</para>
        /// </returns>
        public List<CashAdvance> GetListCashAdvance(string subAccountId, string contractNo, int status,
            int tradeType, int pageIndex, int pageSize, out int count)
        {
            var cashAdvanceService = new ETradeFinance.Services.CashAdvanceService();            
            var cashAdvanceList = cashAdvanceService.GetListCashAdvance(subAccountId, contractNo, status, tradeType,
                                                                        pageIndex, pageSize, out count);


            // Get broker name list
            var brokerIdList = (from cashAdvance in cashAdvanceList
                                where !string.IsNullOrEmpty(cashAdvance.BrokerId)
                                select cashAdvance.BrokerId).ToList();

            var brokerAccountService = new BrokerAccountService();
            var brokerList = brokerAccountService.GetList(brokerIdList);
            var brokerDictionary = new Dictionary<string, string>();
            foreach (var brokerAccount in brokerList)
            {
                if (!brokerDictionary.ContainsKey(brokerAccount.BrokerId))
                {
                    brokerDictionary.Add(brokerAccount.BrokerId, brokerAccount.Name);
                }
            }
            // Update broker name to cash advance history
            foreach (var cashAdvance in cashAdvanceList)
            {
                if (!string.IsNullOrEmpty(cashAdvance.BrokerId))
                {
                    string brokerName;
                    if (brokerDictionary.TryGetValue(cashAdvance.BrokerId, out brokerName))
                    {
                        cashAdvance.BrokerName = brokerName;
                    }
                }
            }
            return cashAdvanceList;
        }
        #endregion

        #region XROrder
        /// <summary>
        /// Gets the total unfinished XR order register amount.
        /// </summary>
        /// <param name="buyRightId">The buy right id.</param>
        /// <param name="subAccountId">The sub account id.</param>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <param name="market">The market.</param>
        /// <returns>Return amount of registered XR order.</returns>
        public long GetTotalUnfinishedXROrderRegisterAmount(long buyRightId, string subAccountId, string secSymbol, string market)
        {
            var xrOrdersService=new ETradeFinance.Services.XrOrdersService();
            return xrOrdersService.GetTotalRegistedXROrderAmount(buyRightId, subAccountId, secSymbol, market);
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
        /// <returns>Return list of XrOrders that contain XrOrders information.</returns>
        public PagingObject<List<XrOrders>> GetListXROrderHist(long id, string subAccountId, string secSymbol,
                                                               string market, string fromDate, string toDate, int status,
                                                               string brokerID, string note, int pageIndex, int pageSize)
        {
            var xrOrdersService = new ETradeFinance.Services.XrOrdersService();
            return xrOrdersService.GetListXROrderHist(id, subAccountId, secSymbol, market, fromDate, toDate, status,
                                                      brokerID, note, pageIndex, pageSize);
        }

        #endregion

        #region Authentication
        /// <summary>
        /// Authenticate customer login information. This methods is called from ETrade system.
        /// </summary>
        /// <param name="mainCustAccount">The main cust account.</param>
        /// <param name="password">Password username</param>
        /// <param name="updatedUserId">Id of broker</param>
        /// <returns>
        /// 	<para>Authentication result.</para>
        /// 	<para>RET_CODE=ERROR_ACCOUNT: Account does not exist.</para>
        /// 	<para>RET_CODE=ACCOUNT_INACTIVED: Account is inactived.</para>
        /// 	<para>RET_CODE=PASSWORD_INACTIVED: Password is inactived.</para>
        /// 	<para>RET_CODE=INCORRECT_PASSWORD: Password is incorrect.</para>
        /// 	<para>RET_CODE=SHOW_CAPTCHA: Show captcha image.</para>
        /// 	<para>RET_CODE=ACCOUNT_LOCKED: Account is locked.</para>
        /// 	<para>RET_CODE=FAIL: Login failed.</para>
        /// 	<para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        public int AuthenticateCustLogon(MainCustAccount mainCustAccount, string password, string updatedUserId)
        {
            var returnValue = (int)CommonEnums.RET_CODE.INCORRECT_PASSWORD;
            var customerActionHistoryService = new CustomerActionHistoryService();
            var actionType = (int)CommonEnums.ACTION_TYPE.LOGIN_FAILED;
            int reason = -1;

            if (mainCustAccount.Actived == false)
            {
                returnValue = (int)CommonEnums.RET_CODE.ACCOUNT_INACTIVE;
                reason = returnValue;
            }
            else if (mainCustAccount.PassLockReason != (int)CommonEnums.LOCK_ACCOUNT_REASON.NOTHING)
            {
                returnValue = (int)CommonEnums.RET_CODE.PASSWORD_INACTIVED;
                reason = returnValue;
            }
            else // Authenticate password
            {
                if (mainCustAccount.AuthType == (int)CommonEnums.AUTHENTICATION_TYPE.PIN_PASS) 
                {
                    if (mainCustAccount.Password == PasswordHandlerMd5.Encrypt(password))
                    {
                        returnValue = (int)CommonEnums.RET_CODE.SUCCESS;
                    }
                }
                else if (mainCustAccount.AuthType == (int)CommonEnums.AUTHENTICATION_TYPE.RSA)
                {
                    returnValue = AuthenticationService.Authenticate(mainCustAccount.MainCustAccountId, password);
                }

                if (returnValue != (int) CommonEnums.RET_CODE.SUCCESS)
                {
                    if((returnValue != (int)CommonEnums.RET_CODE.FAIL) && (returnValue != (int) CommonEnums.RET_CODE.SYSTEM_ERROR))
                    {
                        int continuousloginPeriod = int.Parse(ConfigurationManager.AppSettings["ContinuousLoginPeriod"]);
                        //int smsTimes = int.Parse(ConfigurationManager.AppSettings["FailedLoginSMSTime"]);
                        int captcharTimes = int.Parse(ConfigurationManager.AppSettings["FailedLoginCaptchar"]);
                        int lockTimes = int.Parse(ConfigurationManager.AppSettings["FailedLoginLock"]);

                        if ((mainCustAccount.FailedLoginCount == null) || (mainCustAccount.FailedLoginCount == 0))
                        {
                            // First time failed
                            mainCustAccount.FailedLoginCount = 1;
                        }
                        else
                        {
                            if (mainCustAccount.FailedLoginTime != null)
                            {
                                var previousLogin = (DateTime)mainCustAccount.FailedLoginTime;

                                if (previousLogin.AddMinutes(continuousloginPeriod).CompareTo(DateTime.Now) >= 0)
                                {
                                    // Login within period
                                    mainCustAccount.FailedLoginCount = mainCustAccount.FailedLoginCount + 1;
                                }
                                else
                                {
                                    // Not continuous login
                                    mainCustAccount.FailedLoginCount = 1;
                                }
                            }
                        }

                        mainCustAccount.FailedLoginTime = DateTime.Now;
                        mainCustAccount.UpdatedDate = DateTime.Now;
                        mainCustAccount.UpdatedUser = mainCustAccount.MainCustAccountId;

                        /*if (mainCustAccount.FailedLoginCount == smsTimes)
                        {
                            LogHandler.Log("Send SMS to warning user", GetType() + ".AuthenticateCustLogon()",
                                           TraceEventType.Information);
                            returnValue = (int)CommonEnums.RET_CODE.SEND_WARNING_SMS;
                        }*/
                        if (mainCustAccount.FailedLoginCount == captcharTimes)
                        {
                            LogHandler.Log("Show captchar", GetType() + ".AuthenticateCustLogon()",
                                           TraceEventType.Information);
                            returnValue = (int)CommonEnums.RET_CODE.SHOW_CAPTCHA;
                        }
                        else if (mainCustAccount.FailedLoginCount == lockTimes)
                        {
                            LogHandler.Log("Lock password", GetType() + ".AuthenticateCustLogon()",
                                           TraceEventType.Information);
                            returnValue = (int)CommonEnums.RET_CODE.ACCOUNT_LOCKED;
                            mainCustAccount.PassLockReason = (int)CommonEnums.LOCK_ACCOUNT_REASON.WRONG_PASS;
                            actionType = (int)CommonEnums.ACTION_TYPE.ACCOUNT_LOCKED;

                        }
                        reason = (int)CommonEnums.LOCK_ACCOUNT_REASON.WRONG_PASS;
                        bool result = MainCustAccountService.Update(mainCustAccount);
                        if (!result)
                        {
                            returnValue = (int)CommonEnums.RET_CODE.FAIL;
                        }
                    }
                }
                else // Success login
                {
                    mainCustAccount.FailedLoginCount = 0;
                    mainCustAccount.FailedLoginTime = null;
                    mainCustAccount.UpdatedDate = DateTime.Now;
                    mainCustAccount.UpdatedUser = mainCustAccount.MainCustAccountId;
                    bool result = MainCustAccountService.Update(mainCustAccount);
                    if (!result)
                    {
                        returnValue = (int)CommonEnums.RET_CODE.FAIL;
                    }
                    else
                    {
                        returnValue = (int)CommonEnums.RET_CODE.SUCCESS;
                        actionType = (int)CommonEnums.ACTION_TYPE.LOGIN;
                    }
                }
            }
            
            // Update customer action history
            customerActionHistoryService.InsertCustomerActionHistory(string.Empty, DateTime.Now,
                                                                         mainCustAccount.MainCustAccountId,
                                                                         string.Empty,
                                                                         actionType, reason);

            return returnValue;
        }
        #endregion
    }
}
