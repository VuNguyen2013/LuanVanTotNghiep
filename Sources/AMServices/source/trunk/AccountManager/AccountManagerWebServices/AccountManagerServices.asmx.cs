﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using AccountManager.Entities;
using AccountManager.Services;
using AccountManagerGeneralService;
using AccountManagerWebServices.Utils;
using ETradeCommon;
using ETradeCommon.Enums;
using ETradeFinance.Entities;
using ETradeFinance.Services;
using Constants = AccountManagerWebServices.Utils.Constants;

namespace AccountManagerWebServices
{
    /// <summary>
    /// Contain methods to manage account and broker information.
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class AccountManagerServices : WebService
    {
        private static readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();

        private const string WEB_SERVICE_POLICY = "WebServiceExceptionPolicy";

        readonly CoreServices _coreServices = new CoreServices();

        private static readonly IGeneralService GeneralService = new GeneralService();

        private static readonly SmsCountService SmsCountService = new SmsCountService();

        #region Broker
        /// <summary>
        /// Method for brokers, administrators to login into account manager system.
        /// Add sessionId to cache to check multi login later.
        /// </summary>
        /// <param name="userName">Login username</param>
        /// <param name="password">Login password</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{BrokerAccount}">ResultObject</see> object includes returned code, returned message, 
        /// and <see cref="BrokerAccount">BrokerAccount</see> object that contains broker’s account information.</para>
        /// <para>RET_CODE=ACCOUNT_INACTIVED: Account is inactived.</para>
        /// <para>RET_CODE=INCORRECT_USER_PASSWORD: Incorrect username or password.</para>
        /// <para>RET_CODE=SUCCESS: Login successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Login service for broker", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string BrokerLogin(string userName, string password)
        {
            var resultObject = new ResultObject<BrokerAccount>();
            try
            {
                var brokerAccountService = new BrokerAccountService();
                //var brokerAccount = brokerAccountService.GetByBrokerId(userName);
                var brokerAccount = brokerAccountService.GetBroker(userName);

                if((brokerAccount != null) && brokerAccount.Actived == false)
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.ACCOUNT_LOCKED;
                    resultObject.Result = null;
                }
                else if ((brokerAccount != null) && (brokerAccount.Password == PasswordHandlerMd5.Encrypt(password)))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                    resultObject.Result = brokerAccount;
                    Session[Constants.BROKER] = brokerAccount.BrokerId;
                    Session[Constants.BROKER_TYPE] = brokerAccount.AccountType;

                    // Put sessionId into cache to compare user session later
                    string cacheKey = userName;
                    var cacheTimeout = new TimeSpan(0, 0, HttpContext.Current.Session.Timeout, 0, 0);
                    if (HttpContext.Current.Cache[cacheKey] != null)
                    {
                        HttpContext.Current.Cache.Remove(cacheKey);
                    }
                    HttpContext.Current.Cache.Insert(cacheKey, Session.SessionID, null, Cache.NoAbsoluteExpiration,
                                                     cacheTimeout);
                }
                else
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.INCORRECT_USER_PASSWORD;
                    resultObject.Result = null;
                }
            }
            catch (Exception e)
            {
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                resultObject.Result = null;
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(resultObject);
            }
            return Serializer.Serialize(resultObject);
        }
      
        /// <summary>
        /// Log out method for broker.
        /// </summary>
        /// <returns>true if logout success; otherwise, false.</returns>
        [WebMethod(Description = "Logout service for broker", EnableSession = true)]
        public bool BrokerLogout()
        {
            var loginBrokerId = (string)Session[Constants.BROKER];
            if (!string.IsNullOrEmpty(loginBrokerId))
            {
                HttpContext.Current.Cache.Remove(loginBrokerId);
            }
            Session.Abandon();
            
            return true;
        }

        /// <summary>
        /// Create new broker.
        /// </summary>
        /// <param name="brokerId">Id of broker</param>
        /// <param name="name">Name of broker</param>
        /// <param name="password">Password for broker to login</param>
        /// <param name="accountType">Broker type: Admin or broker</param>
        /// <param name="actived">True if broker is actived; otherwise, false.</param>
        /// <param name="mobilePhone">Mobile phone of broker</param>
        /// <param name="email">Email of broker</param>
        /// <param name="permissionList">The permission list of broker</param>
        /// <returns>
        /// <para>Result of creating broker.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NOT_ALLOW: User has no right to do this function.</para>
        /// <para>RET_CODE=EXISTED_DATA: Broker already exists.</para>
        /// <para>RET_CODE=SUCCESS: Created successfully.</para>
        /// <para>RET_CODE=FAIL: Failed to create broker.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Create a new broker", EnableSession = true)]
        public int CreateBroker(string brokerId, string name, string password, short accountType, 
            bool actived, string mobilePhone, string email, List<int> permissionList)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }
                //Only user with admin type can use this function.
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    return (int) CommonEnums.RET_CODE.NOT_ALLOW;
                }
                var brokerAccountService = new BrokerAccountService();
                var brokerAccount = CreateBrokerAccount(brokerId, name, password, accountType, actived, mobilePhone,
                                                        email);
                brokerAccount.CreatedDate = DateTime.Now;
                brokerAccount.CreatedUser = loginBrokerId;
                brokerAccount.UpdatedDate = DateTime.Now;
                brokerAccount.UpdatedUser = loginBrokerId;
                int returnCode = brokerAccountService.SaveBroker(brokerAccount, permissionList);
                return returnCode;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        /// <summary>
        /// Update an existing broker.
        /// </summary>
        /// <param name="brokerId">Id of broker</param>
        /// <param name="name">Name of broker</param>
        /// <param name="password">Password for broker to login</param>
        /// <param name="accountType">Broker type: Admin or broker</param>
        /// <param name="actived">True if broker is actived; otherwise, false.</param>
        /// <param name="mobilePhone">Mobile phone of broker</param>
        /// <param name="email">Email of broker</param>
        /// <param name="permissionList">The permission list of broker</param>
        /// <returns>
        /// <para>Result of updating broker.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NOT_ALLOW: User has no right to do this function.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: Data is not existing.</para>
        /// <para>RET_CODE=SUCCESS: Updated successfully.</para>
        /// <para>RET_CODE=FAIL: Failed to update broker.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Update an existing broker", EnableSession = true)]
        public int UpdateBroker(string brokerId, string name, string password, short accountType,
            bool actived, string mobilePhone, string email, List<int> permissionList)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }
                // Only user with admin type can use this function.
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    return (int)CommonEnums.RET_CODE.NOT_ALLOW;
                }

                //Do not allow to delete admin account.
                // Do not allow to activate/inactivate current login broker
                if ((brokerId == Constants.ADMIN) || (brokerId == loginBrokerId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_ALLOW;
                }

                var brokerAccountService = new BrokerAccountService();
                int result = brokerAccountService.UpdateBroker(brokerId, name, password, accountType, actived,
                                                                mobilePhone, email, loginBrokerId, permissionList);
                return result;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        /// <summary>
        /// Change broker's password.
        /// </summary>
        /// <param name="brokerId">Id of broker</param>
        /// <param name="oldPassword">The old password</param>
        /// <param name="newPassword">The new password</param>
        /// <returns>
        /// <para>Result of changing broker password.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=INCORRECT_PASSWORD: Old password is incorrect.</para>
        /// <para>RET_CODE=PASSWORD_NOT_MATCH: Old password and new password don't match.</para>
        /// <para>RET_CODE=ERROR_ACCOUNT: Broker account does not exist.</para>
        /// <para>RET_CODE=SUCCESS: Updated successfully.</para>
        /// <para>RET_CODE=FAIL: Failed to update broker.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Change broker password", EnableSession = true)]
        public int ChangeBrokerPassword(string brokerId, string oldPassword, string newPassword)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }

                // Only user with admin type can use this function and broker can change his password.
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {// Can only change himself
                    if (loginBrokerId != brokerId)
                    {
                        return (int)CommonEnums.RET_CODE.NOT_ALLOW;
                    }
                }
                else if ((brokerId == Constants.ADMIN) && (loginBrokerId != Constants.ADMIN))
                { // if the login broker is not "admin", he cannot change "admin"'s password
                    return (int)CommonEnums.RET_CODE.NOT_ALLOW;
                }

                var brokerAccountService = new BrokerAccountService();
                int result = brokerAccountService.ChangeBrokerPassword(brokerId, oldPassword, newPassword,
                                                                        loginBrokerId);
                return result;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        /// <summary>
        /// Create a broker account object.
        /// </summary>
        /// <param name="brokerId">Id of broker</param>
        /// <param name="name">Name of broker</param>
        /// <param name="password">Password for broker to login</param>
        /// <param name="accountType">Broker type: Admin or broker</param>
        /// <param name="actived">True if broker is actived; otherwise, false.</param>
        /// <param name="mobilePhone">Mobile phone of broker</param>
        /// <param name="email">Email of broker</param>
        /// <returns>A <see cref="BrokerAccount">BrokerAccount</see> object that contains broker's information.</returns>
        private static BrokerAccount CreateBrokerAccount(string brokerId, string name, string password, short accountType, 
            bool actived, string mobilePhone, string email)
        {
            var brokerAccount = new BrokerAccount
            {
                BrokerId = brokerId,
                Name = name,
                Password = PasswordHandlerMd5.Encrypt(password),
                AccountType = accountType,
                Actived = actived,
                MobilePhone = mobilePhone,
                EmailAddr = email
            };
            return brokerAccount;
        }

        /// <summary>
        /// Activate or deactivate a broker account.
        /// </summary>
        /// <param name="brokerId">Id of broker</param>
        /// <param name="isActived">true if actived; otherwise, false.</param>
        /// <returns>
        /// <para>Result of activating or deactivating a broker account.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NOT_ALLOW: User has no right to do this function.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: Data is not existing.</para>
        /// <para>RET_CODE=SUCCESS: Activate or deactivate successfully.</para>
        /// <para>RET_CODE=FAIL: Failed to Activate or deactivate broker.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Activate or deactivate a broker", EnableSession = true)]
        public int ActivateBroker(string brokerId, bool isActived)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }
                //Only user with admin type can use this function.
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    return (int)CommonEnums.RET_CODE.NOT_ALLOW;
                }

                //Do not allow to delete admin account.
                // Do not allow to activate/inactivate current login broker
                if ((brokerId == Constants.ADMIN) || (brokerId == loginBrokerId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_ALLOW;
                }

                var brokerAccountService = new BrokerAccountService();
                int result = brokerAccountService.ActivateBroker(brokerId, isActived,loginBrokerId);
                return result;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
            
        }

        /// <summary>
        /// Get list of brokers from system.
        /// </summary>
        /// <param name="brokerId">Broker Id</param>
        /// <param name="name">Name of broker</param>
        /// <param name="accountType">Account type (All, Admin, Broker, ...)</param>
        /// <param name="actived">Actived status (All, True, False)</param>
        /// <param name="mobilePhone">Phone number</param>
        /// <param name="email">Email</param>
        /// <param name="pageIndex">Page index, start from 1</param>
        /// <param name="pageSize">Number of records on 1 page</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;BrokerAccount&gt;&gt;&gt;</see> object contains returned code, 
        /// returned message, a list of <see cref="BrokerAccount">BrokerAccount</see> objects that contain broker's information, 
        /// and total of records.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NOT_ALLOW: User has no right to do this function.</para>
        /// <para>RET_CODE=SUCCESS: Get list of brokers successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list of broker from system", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListBroker(string brokerId, string name, short accountType,
            int actived, string mobilePhone, string email, int pageIndex, int pageSize)
        {
            var resultObject = new ResultObject<PagingObject<List<BrokerAccount>>>();
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    resultObject.Result = null;
                    return Serializer.Serialize(resultObject);
                }
                var brokerAccountService = new BrokerAccountService();
                var brokerPagingObject = brokerAccountService.GetList(brokerId, name, accountType, actived, 
                                                            mobilePhone, email, pageIndex, pageSize);

                // Convert result to List type
                
                resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                resultObject.Result = brokerPagingObject;
                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.Result = null;
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                return Serializer.Serialize(resultObject);
            }
            
        }



        /// <summary>
        /// Sends the SMS alert cash transfer.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        [WebMethod(Description = "Send SMS to brokers have permission receive sms alert cash transfer")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int SendSMSAlertCashTransfer(string message)
        {           
            try
            {               
                var brokerPermissionService = new BrokerPermissionService();
                var brokerAccountService = new BrokerAccountService();
                var broker = new BrokerAccount();
                var listBorker = brokerPermissionService.GetBrokerByPermission((int)CommonEnums.BROKER_PERMISSIONS.RECEIVE_SMS_CASH_TRANSFER);
                if (listBorker != null)
                {
                    foreach (var brokerID in listBorker)
                    {
                        broker = brokerAccountService.GetBroker(brokerID);
                        if (broker != null && broker.Actived && !string.IsNullOrEmpty(broker.MobilePhone))
                        {
                            SMSSender.SendSMS(broker.MobilePhone, message, "");
                        }
                    }
                }
                return (int) CommonEnums.RET_CODE.SUCCESS;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int) CommonEnums.RET_CODE.SYSTEM_ERROR;
            }

        }
        /// <summary>
        /// Get information of a broker.
        /// </summary>
        /// <param name="brokerId">Id of broker</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject</see> object contains returned code, returned message, 
        /// and a <see cref="BrokerAccount">BrokerAccount</see> object that contains broker's information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Get broker successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get a broker", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetBroker(string brokerId)
        {
            var resultObject = new ResultObject<BrokerAccount>();
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    resultObject.Result = null;
                    return Serializer.Serialize(resultObject);
                }
                var brokerAccountService = new BrokerAccountService();
                
                var brokerAccount = brokerAccountService.GetBroker(brokerId);

                if (brokerAccount != null)
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                }
                else
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }

                resultObject.Result = brokerAccount;
                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.Result = null;
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                return Serializer.Serialize(resultObject);
            }
            
        }

        /// <summary>
        /// Get list of broker permission items.
        /// </summary>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;List&lt;BrokerPermission&gt;&gt;</see> object contains returned code, returned message and 
        /// List object that contains <see cref="BrokerAmPermission">BrokerAmPermission</see> items.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Get list of customer permissions successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list of broker permission items.", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListBrokerPermission()
        {
            var resultObject = new ResultObject<List<BrokerAmPermission>>();
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    resultObject.Result = null;
                    return Serializer.Serialize(resultObject);
                }
                var brokerAmPermissionService = new BrokerAmPermissionService();
                var permissionList = brokerAmPermissionService.GetAll();
                if (permissionList != null && permissionList.Count > 0)
                {
                    var permissions = permissionList.ToList();
                    resultObject.Result = permissions;
                    resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                }
                else
                {
                    resultObject.Result = null;
                    resultObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.ErrorMessage = e.Message;
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                resultObject.Result = null;
            }

            return Serializer.Serialize(resultObject);
        }
        #endregion

        #region Customer

        /// <summary>
        /// Create a new main customer account.
        /// </summary>
        /// <param name="mainCustAccountId">Id of main customer account</param>
        /// <param name="fullname">Fullname of main customer</param>
        /// <param name="email">Email of main customer</param>
        /// <param name="phone">Phone of main customer</param>
        /// <param name="actived">true if this customer is actived; otherwise, false</param>
        /// <param name="passwordLockReason">The reason to lock password</param>
        /// <param name="pinLockReason">The reason to lock pin</param>
        /// <param name="tokenId">Id of main customer's token</param>
        /// <param name="tokenName">Name of token</param>
        /// <param name="tokenActived">Status of token</param>
        /// <param name="customerType">Type of customer: Individual, Foreigner or Organization</param>
        /// <param name="lockReason">Reason of locking</param>
        /// <param name="brokerId">Id of broker of this customer</param>
        /// <param name="authType">Authenticate type</param>
        /// <param name="pinType">Pin type</param>
        /// <param name="languageId">The language id.</param>
        /// <returns>
        /// 	<para>Result of creating new customer.</para>
        /// 	<para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// 	<para>RET_CODE=EXISTED_DATA: Account is already existed.</para>
        /// 	<para>RET_CODE=FAIL: Failed to create main customer account information.</para>
        /// 	<para>RET_CODE=SUCCESS: Create main customer successfully.</para>
        /// 	<para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Create a new main customer account", EnableSession = true)]
        public int CreateMainCustomer(string mainCustAccountId, string fullname, string email, string phone, 
            bool actived, int passwordLockReason, int pinLockReason,
            string tokenId, string tokenName, string tokenActived, int customerType, int lockReason, string brokerId,
            short authType, short pinType,string languageId)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }

                //Only user with admin type can use this function.
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    return (int)CommonEnums.RET_CODE.NOT_ALLOW;
                }

                // Remove company code 085C123456
                mainCustAccountId = ProcessAccountId(mainCustAccountId);

                var mainCustAccService = new MainCustAccountService();
                
                var mainCustomer = CreateMainCustAccount(mainCustAccountId, fullname, email, phone, actived,
                                                         passwordLockReason, pinLockReason, tokenId, tokenName, tokenActived,
                                                         customerType, lockReason, brokerId, authType, pinType, languageId);
                mainCustomer.Password = PasswordHandlerMd5.Encrypt(GeneratePassOrPin());
                mainCustomer.Pin = PasswordHandlerMd5.Encrypt(GeneratePassOrPin());
                mainCustomer.PinIsNew = true;
                mainCustomer.PassIsNew = true;
                mainCustomer.LanguageId = languageId;
                mainCustomer.CreatedDate = DateTime.Now;
                mainCustomer.CreatedUser = loginBrokerId;
                mainCustomer.UpdatedDate = DateTime.Now;
                mainCustomer.UpdatedUser = loginBrokerId;

                int result = mainCustAccService.CreateMainCustomer(mainCustomer);
                return result;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int) CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        /// <summary>
        /// Create a new sub account of customer.
        /// </summary>
        /// <param name="subCustAccountId">Id of sub customer account</param>
        /// <param name="name">Name of sub customer</param>
        /// <param name="actived">true if sub customer account is actived; otherwise, false</param>
        /// <param name="lockAccountReason">Reason of locking account</param>
        /// <param name="mainCustAccountId">Id of main customer account</param>
        /// <param name="permissionList">List of permission of sub customer account</param>
        /// <returns>
        /// <para>Result of creating new sub customer account.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=EXISTED_DATA: Account is already existed.</para>
        /// <para>RET_CODE=FAIL: Failed to create sub customer account information.</para>
        /// <para>RET_CODE=SUCCESS: Create sub customer successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Create a new of sub customer account", EnableSession = true)]
        public int CreateSubCustomer(string subCustAccountId, string name, bool actived, short lockAccountReason, 
            string mainCustAccountId, List<int> permissionList)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }

                //Only user with admin type can use this function.
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    var brokerPermissionService = new BrokerPermissionService();
                    var brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_EDIT);
                    if (brokerPermission == null)
                    {
                        return (int)CommonEnums.RET_CODE.NOT_ALLOW;
                    }  
                }

                // Remove company code 085C123456
                subCustAccountId = ProcessAccountId(subCustAccountId);

                var subCustAccount = CreateSubCustAccount(subCustAccountId, name, actived, lockAccountReason,
                                                          mainCustAccountId);
                subCustAccount.CreatedDate = DateTime.Now;
                subCustAccount.CreatedUser = loginBrokerId;
                subCustAccount.UpdatedDate = DateTime.Now;
                subCustAccount.UpdatedUser = loginBrokerId;
                var subCustAccountService = new SubCustAccountService();
                int result = subCustAccountService.SaveSubCustAccount(subCustAccount, permissionList);
                return result;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }



        /// <summary>
        /// Adds the token id.
        /// </summary>
        /// <param name="mainCustAccountId">The main cust account id.</param>
        /// <param name="tokenId">The token id.</param>
        /// <returns>
        /// 	<para>Result of creating new sub customer account.</para>
        /// 	<para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// 	<para>RET_CODE=EXISTED_DATA: Account is already existed.</para>
        /// 	<para>RET_CODE=FAIL: Failed to create sub customer account information.</para>
        /// 	<para>RET_CODE=SUCCESS: Create sub customer successfully.</para>
        /// 	<para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Add token Id for main cust account", EnableSession = true)]
        public int AddTokenId(string mainCustAccountId, string tokenId)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }

                //Only user with admin type can use this function.
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    var brokerPermissionService = new BrokerPermissionService();
                    var brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_EDIT);
                    if (brokerPermission == null)
                    {
                        return (int)CommonEnums.RET_CODE.NOT_ALLOW;
                    }  
                }

                var mainCustAccountService = new MainCustAccountService();
                int result = mainCustAccountService.AddTokenId(mainCustAccountId, tokenId, loginBrokerId);
                return result;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        /// <summary>
        /// Update information of main customer account.
        /// </summary>
        /// <param name="mainCustAccountId">Id of main customer account</param>
        /// <param name="fullname">Fullname of main customer</param>
        /// <param name="email">Email of main customer</param>
        /// <param name="phone">Phone of main customer</param>
        /// <param name="actived">true if this customer is actived; otherwise, false</param>
        /// <param name="password">Password of main customer</param>
        /// <param name="pin">pin of main customer</param>
        /// <param name="passwordLockReason">The reason to lock password</param>
        /// <param name="pinLockReason">The reason to lock pin</param>
        /// <param name="tokenId">Id of main customer's token</param>
        /// <param name="tokenName">Name of token</param>
        /// <param name="tokenActived">Status of token</param>
        /// <param name="customerType">Type of customer</param>
        /// <param name="lockReason">Lock reason</param>
        /// <param name="brokerId">Id of broker of customer</param>
        /// <param name="authType">Authenticate type</param>
        /// <param name="pinType">Pin type</param>
        /// <param name="languageId">The language id.</param>
        /// <returns>
        /// 	<para>Result of updating main customer account information.</para>
        /// 	<para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// 	<para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// 	<para>RET_CODE=FAIL: Failed to update main customer account information.</para>
        /// 	<para>RET_CODE=SUCCESS: Update main customer successfully.</para>
        /// 	<para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Update information of main customer account", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int UpdateMainCustomer(string mainCustAccountId, string fullname, string email, string phone, 
            bool actived, string password, string pin, int passwordLockReason, int pinLockReason,
            string tokenId, string tokenName, string tokenActived, int customerType, int lockReason, string brokerId, 
            short authType, short pinType,string languageId)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }

                // Check permission
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    var brokerPermissionService = new BrokerPermissionService();
                    var brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_EDIT);
                    if (brokerPermission == null)
                    {
                        return (int)CommonEnums.RET_CODE.NOT_ALLOW;
                    }
                }

                // Remove company code 085C123456
                mainCustAccountId = ProcessAccountId(mainCustAccountId);

                var mainCustomer = CreateMainCustAccount(mainCustAccountId, fullname, email, phone, actived,
                                                         passwordLockReason, pinLockReason, tokenId, tokenName, tokenActived,
                                                         customerType, lockReason, brokerId, authType, pinType, languageId);
                if (!string.IsNullOrEmpty(password))
                {
                    mainCustomer.Password = PasswordHandlerMd5.Encrypt(password);
                }

                if (!string.IsNullOrEmpty(pin))
                {
                    mainCustomer.Pin = PasswordHandlerMd5.Encrypt(pin);
                }
                mainCustomer.UpdatedUser = loginBrokerId;

                var mainCustAccService = new MainCustAccountService();
                int result = mainCustAccService.UpdateMainCustomer(mainCustomer);
                return result;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        /// <summary>
        /// Update information of sub customer account.
        /// </summary>
        /// <param name="subCustAccountId">Id of sub customer account</param>
        /// <param name="name">Name of sub customer</param>
        /// <param name="actived">true if sub customer account is actived; otherwise, false</param>
        /// <param name="lockAccountReason">Reason of locking account</param>
        /// <param name="mainCustAccountId">Id of main customer account</param>
        /// <param name="permissionList">List of permission of sub customer account</param>
        /// <returns>
        /// <para>Result of updating sub customer account information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=FAIL: Failed to update sub customer account information.</para>
        /// <para>RET_CODE=SUCCESS: Update sub customer successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Update information of sub customer account", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int UpdateSubCustomer(string subCustAccountId, string name, bool actived, short lockAccountReason, 
            string mainCustAccountId, List<int> permissionList)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }

                // Check permission
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    var brokerPermissionService = new BrokerPermissionService();
                    var brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_EDIT);
                    if (brokerPermission == null)
                    {
                        return (int)CommonEnums.RET_CODE.NOT_ALLOW;
                    }
                }

                // Remove company code 085C123456
                subCustAccountId = ProcessAccountId(subCustAccountId);

                var subCustAccount = CreateSubCustAccount(subCustAccountId, name, actived, lockAccountReason, mainCustAccountId);
                subCustAccount.UpdatedUser = loginBrokerId;
                var subCustAccountService = new SubCustAccountService();
                int result = subCustAccountService.UpdateSubCustAccount(subCustAccount, permissionList);
                return result;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        /// <summary>
        /// Activate or deactivate a customer account to allow to use online trading system.
        /// </summary>
        /// <param name="mainCustAccountId">Id of main customer account</param>
        /// <param name="actived">true if main customer account is actived; otherwise, false</param>
        /// <param name="lockReason">Reason lock</param>
        /// <returns>
        /// <para>Result of the activating action.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=FAIL: Failed to activate main customer account information.</para>
        /// <para>RET_CODE=SUCCESS: Activate main customer successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Activate a customer account", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int ActivateMainCustomer(string mainCustAccountId, bool actived, int lockReason)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }

                // Check permission
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    var brokerPermissionService = new BrokerPermissionService();
                    var brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId, 
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_ACTIVE);
                    if (brokerPermission == null)
                    {
                        return (int)CommonEnums.RET_CODE.NOT_ALLOW;
                    }
                }

                // Remove company code 085C123456
                mainCustAccountId = ProcessAccountId(mainCustAccountId);

                var mainCustAccService = new MainCustAccountService();

                int result = mainCustAccService.ActivateMainCustomer(mainCustAccountId, actived, lockReason,
                                                                     loginBrokerId);
                return result;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
            
        }

        /// <summary>
        /// Activate or deactivate a sub customer account to allow to use this sub customer account.
        /// </summary>
        /// <param name="subCustAccountId">Id of sub customer account</param>
        /// <param name="actived">true if sub customer account is actived; otherwise, false</param>
        /// <param name="lockReason">Lock reason</param>
        /// <returns>
        /// <para>Result of the activating action.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=FAIL: Failed to activate sub customer account information.</para>
        /// <para>RET_CODE=SUCCESS: Activate sub customer successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Inactivate a customer", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int ActivateSubCustomer(string subCustAccountId, bool actived, short lockReason)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }

                // Check permission
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    var brokerPermissionService = new BrokerPermissionService();
                    var brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_ACTIVE);
                    if (brokerPermission == null)
                    {
                        return (int)CommonEnums.RET_CODE.NOT_ALLOW;
                    }
                }

                // Remove company code 085C123456
                subCustAccountId = ProcessAccountId(subCustAccountId);

                var subCustAccountService = new SubCustAccountService();
                int result = subCustAccountService.ActivateSubCustAccount(subCustAccountId, actived, lockReason,
                                                                          loginBrokerId);
                return result;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
            
        }

        /// <summary>
        /// Get full customer information including main customer account, its sub account and permission.
        /// </summary>
        /// <param name="mainCustAccountId">Id of main customer account</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject</see> object contains returned code, returned message and 
        /// <see cref="MainCustAccount">MainCustAccount</see> object that contains full customer information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Get customer successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get full customer information", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetCustomer(string mainCustAccountId)
        {
            var loginBrokerId = (string)Session[Constants.BROKER];
            if (string.IsNullOrEmpty(loginBrokerId))
            {
                var resultObject = new ResultObject<MainCustAccount>
                {
                    RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                    Result = null
                };
                return Serializer.Serialize(resultObject);
            }
            // Remove company code 085C123456
            mainCustAccountId = ProcessAccountId(mainCustAccountId);

            return GetAllCustomerInformation(mainCustAccountId, true);
        }

        /// <summary>
        /// Get full customer information including main customer account, its sub account and permission.
        /// This method is used to call from Etrade website with no session information.
        /// </summary>
        /// <param name="mainCustAccountId">Id of main customer account</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject</see> object contains returned code, returned message and 
        /// <see cref="MainCustAccount">MainCustAccount</see> object that contains full customer information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Get customer successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get full customer information. This method is used to call from Etrade website.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetCustomerNoSession(string mainCustAccountId)
        {

            try
            {
                var resultObject = new ResultObject<MainCustAccount>();
                var mainCustAccountService = new MainCustAccountService();
                var mainCustAccount = mainCustAccountService.GetAccountInfo(mainCustAccountId, false);
                if (mainCustAccount != null)
                {
                    foreach (var subCustAccount in mainCustAccount.SubCustAccountCollection)
                    {
                        if (subCustAccount.SubCustAccountPermissionCollection != null)
                        {
                            ConfigurationsService configurationsService = new ConfigurationsService();
                            //is enable odd lot order
                            Configurations configurations = configurationsService.GetByName(CommonEnums.CONFIGURATIONS.IS_ENABLE_ODD_LOT_ORDER.ToString());
                            if (configurations != null)
                            {
                                if (string.IsNullOrEmpty(configurations.Value) || configurations.Value.Equals("0"))
                                {
                                    subCustAccount.SubCustAccountPermissionCollection.Remove(subCustAccount.SubCustAccountPermissionCollection.Where(permissionCollection => permissionCollection.CustServicesPermissionId == (int)CommonEnums.SUB_ACCOUNT_PERMISSIONS.ODD_SLOT_EXCHANGE).FirstOrDefault());
                                }
                            }
                            //is enable buy right
                            configurations = configurationsService.GetByName(CommonEnums.CONFIGURATIONS.IS_ENABLE_BUY_RIGHT.ToString());
                            if (configurations != null)
                            {
                                if (string.IsNullOrEmpty(configurations.Value) || configurations.Value.Equals("0"))
                                {
                                    subCustAccount.SubCustAccountPermissionCollection.Remove(subCustAccount.SubCustAccountPermissionCollection.Where(permissionCollection => permissionCollection.CustServicesPermissionId == (int)CommonEnums.SUB_ACCOUNT_PERMISSIONS.PRICE_TO_BUY).FirstOrDefault());
                                }
                            }
                        }
                    }
                    resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                }
                else
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }
                resultObject.Result = mainCustAccount;
                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                var resultObject = new ResultObject<MainCustAccount>
                {
                    RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                    Result = null
                };
                return Serializer.Serialize(resultObject);
            }
        }

        /// <summary>
        /// Method to get all information of main customer and sub customer.
        /// </summary>
        /// <param name="mainCustAccountId">Id of main customer account</param>
        /// <param name="fullInfo">true if get all sub customer account; otherwise, false</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject</see> object contains returned code, returned message and 
        /// <see cref="MainCustAccount">MainCustAccount</see> object that contains full customer information.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Get customer successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        private static string GetAllCustomerInformation(string mainCustAccountId, bool fullInfo)
        {
            try
            {
                var resultObject = new ResultObject<MainCustAccount>();
                var mainCustAccountService = new MainCustAccountService();
                var mainCustAccount = mainCustAccountService.GetAccountInfo(mainCustAccountId, fullInfo);
                if (mainCustAccount != null)
                {                    
                    resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                }
                else
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }
                resultObject.Result = mainCustAccount;
                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                var resultObject = new ResultObject<MainCustAccount>
                {
                    RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                    Result = null
                };
                return Serializer.Serialize(resultObject);
            }
            
        }

        ///<summary>
        /// Get information of only main customer account.
        ///</summary>
        ///<param name="mainCustAccountId">Id of main customer account</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject</see> object contains returned code, returned message and 
        /// <see cref="MainCustAccount">MainCustAccount</see> object that contains customer information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Get main customer information successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get only information of main customer account", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetMainCustAccount(string mainCustAccountId)
        {
            try
            {
                var resultObject = new ResultObject<MainCustAccount>();
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    resultObject.Result = null;
                    return Serializer.Serialize(resultObject);
                }
                // Remove company code 085C123456
                mainCustAccountId = ProcessAccountId(mainCustAccountId);

                var mainCustAccountService = new MainCustAccountService();
                var mainCustAccount = mainCustAccountService.GetMainCustAccount(mainCustAccountId);
                if (mainCustAccount != null)
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                }
                else
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }
                resultObject.Result = mainCustAccount;
                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                var resultObject = new ResultObject<MainCustAccount>
                {
                    RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                    Result = null
                };
                return Serializer.Serialize(resultObject);
            }
            
        }

        ///<summary>
        /// Get information of sub customer account and permission.
        ///</summary>
        ///<param name="subCustAccountId">Id of sub customer account</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject</see> object contains returned code, returned message and 
        /// <see cref="SubCustAccount">SubCustAccount</see> object that contains sub customer information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Get sub customer information successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get only information of main customer account", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetSubCustAccount(string subCustAccountId)
        {
            try
            {
                var resultObject = new ResultObject<SubCustAccount>();
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    resultObject.Result = null;
                    return Serializer.Serialize(resultObject);
                }
                // Remove company code 085C123456
                subCustAccountId = ProcessAccountId(subCustAccountId);

                var subCustAccountService = new SubCustAccountService();
                var subCustAccount = subCustAccountService.GetSubCustAccount(subCustAccountId);
                if (subCustAccount != null)
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                }
                else
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }
                resultObject.Result = subCustAccount;
                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                var resultObject = new ResultObject<SubCustAccount>
                {
                    RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                    Result = null
                };
                return Serializer.Serialize(resultObject);
            }

        }

        /// <summary>
        /// Get list of customers.
        /// </summary>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;MainCustAccount&gt;&gt;&gt;</see> object contains returned code, 
        /// returned message and a list of <see cref="MainCustAccount">MainCustAccount</see> object that contains customer information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NOT_ALLOW: User has no right to do this function.</para>
        /// <para>RET_CODE=SUCCESS: Get list of main customer information successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list of customer information", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListMainCustomer(string mainCustAccountId, string fullname, string email, string phone,
            int actived, int passwordLockReason, int pinLockReason, int customerType, int lockReason, string brokerId,
            short authType, short pinType, int pageIndex, int pageSize)
        {
            try
            {
                var resultObject = new ResultObject<PagingObject<List<MainCustAccount>>>();
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    resultObject.Result = null;
                    return Serializer.Serialize(resultObject);
                }
                var brokerType = (short)Session[Constants.BROKER_TYPE];

                var mainCustAccountService = new MainCustAccountService();
                PagingObject<List<MainCustAccount>> returnList;
                if (brokerType == (int) CommonEnums.BROKER_TYPE.ADMIN)
                {
                    returnList = mainCustAccountService.GetList(mainCustAccountId, fullname, email, phone,
                                                                         actived, passwordLockReason, pinLockReason,
                                                                         customerType, lockReason, brokerId, authType,
                                                                         pinType, pageIndex, pageSize);
                }
                else
                {
                    returnList = mainCustAccountService.GetList(mainCustAccountId, fullname, email, phone,
                                                                         actived, passwordLockReason, pinLockReason,
                                                                         customerType, lockReason, loginBrokerId, authType,
                                                                         pinType, pageIndex, pageSize);
                }
                
                resultObject.Result = returnList;
                resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                var resultObject = new ResultObject<MainCustAccount>
                {
                    RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                    Result = null
                };
                return Serializer.Serialize(resultObject);
            }
            
        }

        /// <summary>
        /// Get customer information from SBA.
        /// </summary>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject</see> object contains returned code, returned message and 
        /// a <see cref="CoreAccountInfo">CoreAccountInfo</see> object that contains customer information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=SUCCESS: Get customer information from core successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get customer information from SBA. AccountId is a full account (involve prefix)", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetCustInfoFromCore(string accountId)
        {
            var resultObject = new ResultObject<List<CoreAccountInfo>>();
            var loginBrokerId = (string)Session[Constants.BROKER];
            if (string.IsNullOrEmpty(loginBrokerId))
            {
                resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                resultObject.Result = null;
                return Serializer.Serialize(resultObject);
            }
            try
            {
                resultObject = _coreServices.GetCustInfoFromCore(accountId);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.ErrorMessage = e.Message;
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                resultObject.Result = null;
            }

            return Serializer.Serialize(resultObject);
        }

        /// <summary>
        /// Generate new customer password.
        /// </summary>
        /// <param name="mainCustAccId">Id of main customer account</param>
        /// <param name="authType">Authentication type</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject</see> object contains returned code, returned message and 
        /// string object that contains generated password.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=FAIL: Failed to generate password.</para>
        /// <para>RET_CODE=SUCCESS: Generate password successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Generate customer password", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GenerateCustPass(string mainCustAccId, int authType)
        {
            var resultObject = new ResultObject<string>();
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    resultObject.Result = null;
                    return Serializer.Serialize(resultObject);
                }
                if (!string.IsNullOrEmpty(mainCustAccId))
                {
                    // Check permission
                    var brokerType = (short)Session[Constants.BROKER_TYPE];
                    if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                    {
                        var brokerPermissionService = new BrokerPermissionService();
                        var brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                                (int)CommonEnums.BROKER_PERMISSIONS.CAN_CHANGE_PASS);
                        if (brokerPermission == null)
                        {
                            resultObject.RetCode = CommonEnums.RET_CODE.NOT_ALLOW;
                            resultObject.Result = null;
                            return Serializer.Serialize(resultObject);
                        }
                    }

                    // Remove company code 085C123456
                    mainCustAccId = ProcessAccountId(mainCustAccId);

                    if (authType == (int)CommonEnums.AUTHENTICATION_TYPE.PIN_PASS)
                    {
                        var mainCustAccService = new MainCustAccountService();
                        string password = mainCustAccService.GenerateCustPassword(mainCustAccId, loginBrokerId);
                        if (string.IsNullOrEmpty(password))
                        {
                            resultObject.RetCode = CommonEnums.RET_CODE.FAIL;
                            resultObject.Result = "";
                        }
                        else
                        {
                            resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                            resultObject.Result = password;
                        }
                    }
                }
                else
                {
                    string password = GeneratePassOrPin();
                    resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                    resultObject.Result = password;
                }
                
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.ErrorMessage = e.Message;
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                resultObject.Result = "";
            }

            return Serializer.Serialize(resultObject);
        }

        /// <summary>
        /// Generate password or pin
        /// </summary>
        /// <returns>Generated password</returns>
        private static string GeneratePassOrPin()
        {
            int passwordLength = int.Parse(ConfigurationManager.AppSettings["GeneratedPasswordLength"]);
            return PasswordGenerator.GeneratePassword(passwordLength, false, true, false);
        }

        /// <summary>
        /// Generate new customer pin.
        /// </summary>
        /// <param name="mainCustAccId">Id of main customer account</param>
        /// <param name="authType">Authentication type</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject</see> object contains returned code, returned message and 
        /// string object that contains generated pin.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=FAIL: Failed to generate pin.</para>
        /// <para>RET_CODE=SUCCESS: Generate pin successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Generate customer pin", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GenerateCustPin(string mainCustAccId, int authType)
        {
            var resultObject = new ResultObject<string>();
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    resultObject.Result = null;
                    return Serializer.Serialize(resultObject);
                }
                if (!string.IsNullOrEmpty(mainCustAccId))
                {

                    // Check permission
                    var brokerType = (short)Session[Constants.BROKER_TYPE];
                    if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                    {
                        var brokerPermissionService = new BrokerPermissionService();
                        var brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                                (int)CommonEnums.BROKER_PERMISSIONS.CAN_CHANGE_PIN);
                        if (brokerPermission == null)
                        {
                            resultObject.RetCode = CommonEnums.RET_CODE.NOT_ALLOW;
                            resultObject.Result = null;
                            return Serializer.Serialize(resultObject);
                        }
                    }

                    // Remove company code 085C123456
                    mainCustAccId = ProcessAccountId(mainCustAccId);

                    if (authType == (int)CommonEnums.AUTHENTICATION_TYPE.PIN_PASS)
                    {
                        var mainCustAccService = new MainCustAccountService();
                        string pin = mainCustAccService.GenerateCustPin(mainCustAccId, loginBrokerId);
                        if (string.IsNullOrEmpty(pin))
                        {
                            resultObject.RetCode = CommonEnums.RET_CODE.FAIL;
                            resultObject.Result = "";
                        }
                        else
                        {
                            resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                            resultObject.Result = pin;
                        }
                    }
                }
                else
                {
                    string pin = GeneratePassOrPin();
                    resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                    resultObject.Result = pin;
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.ErrorMessage = e.Message;
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                resultObject.Result = "";
            }

            return Serializer.Serialize(resultObject);
        }


        /// <summary>
        /// Get list of sub permission items.
        /// </summary>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject</see> object contains returned code, returned message and 
        /// List object that contains <see cref="CustServicesPermission"/> items.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Get list of customer permissions successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list of sub permission items.", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListCustServicePermission()
        {
            var resultObject = new ResultObject<List<CustServicesPermission>>();
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    resultObject.Result = null;
                    return Serializer.Serialize(resultObject);
                }
                var custServicePermissionService = new CustServicesPermissionService();
                var permissionList = custServicePermissionService.GetAll();
                if (permissionList != null && permissionList.Count > 0)
                {
                    var permissions = permissionList.ToList();
                    resultObject.Result = permissions;
                    resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                }
                else
                {
                    resultObject.Result = null;
                    resultObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }
                
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.ErrorMessage = e.Message;
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                resultObject.Result = null;
            }

            return Serializer.Serialize(resultObject);
        }

        /// <summary>
        /// Authenticate customer login information. This methods is called from ETrade system.
        /// </summary>
        /// <param name="username">Login username</param>
        /// <param name="password">Password username</param>
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
        [WebMethod(Description = "Authenticate customer login information")]
        public int AuthenticateCustLogon(string username, string password)
        {
            var returnCode = (int)CommonEnums.RET_CODE.SUCCESS;
            try
            {
                var mainCustAccountService = new MainCustAccountService();
                var mainCustAccount = mainCustAccountService.GetByMainCustAccountId(username);
                if(mainCustAccount==null)
                    return (int)CommonEnums.RET_CODE.ERROR_ACCOUNT;

                returnCode = GeneralService.AuthenticateCustLogon(mainCustAccount, password, username);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
            return returnCode;
        }
        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="mainCustAccountId">The main cust account id.</param>
        /// <param name="identityNumber">The identity number.</param>
        /// <param name="messagePhone">The message phone.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject</see> object contains returned code, returned message and 
        /// string object that contains generated new password.</para>       
        /// <para>RET_CODE=INCORECT_INFORMATION: Wrong some information.</para>
        /// <para>RET_CODE=SUCCESS: Generate pass successfully.</para>
        /// /// <para>RET_CODE=NO_EXIST_DATE: wrong main cust account id</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Forget password")]
        public string ForgetPassword(string mainCustAccountId, string identityNumber, string messagePhone)
        {
            var resultObject = new ResultObject<string>();
            try
            {
                var mainCustAccountService = new MainCustAccountService();
                string newPassword;
                var mainCustAccount = new MainCustAccount();
                CommonEnums.RET_CODE result = mainCustAccountService.ForgetPassword(mainCustAccountId, messagePhone, out newPassword, out mainCustAccount);
                resultObject.RetCode = result;
                resultObject.Result = newPassword;
                string languageId = mainCustAccount.LanguageId;
                CultureInfo ci = ETradeCommon.Utils.GetCultureInfo(languageId);
                if (result == CommonEnums.RET_CODE.SUCCESS)
                {
                    var message = (string)HttpContext.GetGlobalResourceObject("Resource", "GEN_PASS", ci);
                    if (!string.IsNullOrEmpty(message))
                        message = string.Format(message, mainCustAccountId, resultObject.Result);
                    if (SendMessage(mainCustAccountId, message) != (int)CommonEnums.RET_CODE.SUCCESS)
                    {
                        resultObject.RetCode = CommonEnums.RET_CODE.ERROR_SENT_MESSAGE;
                    }
                    resultObject.Result = PasswordHandlerMd5.Encrypt(newPassword);
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                resultObject.Result = null;
            }
            return Serializer.Serialize(resultObject);
        }

        /// <summary>
        /// Forgets the pin.
        /// </summary>
        /// <param name="mainCustAccountId">The main cust account id.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject</see> object contains returned code, returned message and 
        /// string object that contains generated new pin.</para>       
        /// <para>RET_CODE=INCORECT_INFORMATION: Wrong some information.</para>
        /// <para>RET_CODE=SUCCESS: Generate pass successfully.</para>
        /// /// <para>RET_CODE=NO_EXIST_DATE: wrong main cust account id</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Forget pin")]
        public string ForgetPin(string mainCustAccountId)
        {
            var resultObject = new ResultObject<string>();
            try
            {
                var mainCustAccountService = new MainCustAccountService();
                string newPin;
                CommonEnums.RET_CODE result = mainCustAccountService.ForgetPin(mainCustAccountId, out newPin);
                resultObject.RetCode = result;
                resultObject.Result = newPin;
                string languageId = mainCustAccountService.GetLanguageId(mainCustAccountId);
                CultureInfo ci = ETradeCommon.Utils.GetCultureInfo(languageId);
                if (result == CommonEnums.RET_CODE.SUCCESS)
                {                    
                    var message = (string)HttpContext.GetGlobalResourceObject("Resource", "GEN_PIN", ci);
                    if (!string.IsNullOrEmpty(message))
                        message = string.Format(message, mainCustAccountId, newPin);
                    if (SendMessage(mainCustAccountId, message) != (int)CommonEnums.RET_CODE.SUCCESS)
                    {
                        resultObject.RetCode = CommonEnums.RET_CODE.ERROR_SENT_MESSAGE;
                    }
                    resultObject.Result = PasswordHandlerMd5.Encrypt(newPin);
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                resultObject.Result = null;
            }
            return Serializer.Serialize(resultObject);
        }

        ///<summary>
        /// Send message to customer.
        ///</summary>
        ///<param name="username">Customer account</param>
        ///<param name="message">Message to send</param>
        ///<returns>Result of sending message</returns>
        [WebMethod(Description = "Send message to customer")]
        public int SendMessage(string username, string message)
        {
            int returnCode;
            try
            {
                var mainCustAccountService = new MainCustAccountService();
                var mainCustAccount = mainCustAccountService.GetByMainCustAccountId(username);
                if ((mainCustAccount != null) && (!string.IsNullOrEmpty(mainCustAccount.Phone)))
                {
                    returnCode = (int) SMSSender.SendSMS(mainCustAccount.Phone, message, "");
                }
                else
                {
                    returnCode = (int)CommonEnums.RET_CODE.FAIL;
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
            return returnCode;
        }

        /// <summary>
        /// Change customer password.
        /// </summary>
        /// <param name="mainCustAccId">Id of main customer account</param>
        /// <param name="oldPassword">Old password</param>
        /// <param name="newPassword">New password</param>
        /// <returns>
        /// <para>Result of changing password.</para>
        /// <para>RET_CODE=ERROR_ACCOUNT: Account does not exist.</para>
        /// <para>RET_CODE=PASSWORD_NOT_MATCH: Old password and new password don't match.</para>
        /// <para>RET_CODE=INCORRECT_PASSWORD: Old password is incorrect.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Change customer password")]
        public int ChangeCustPassword(string mainCustAccId, string oldPassword, string newPassword)
        {
            try
            {
                var mainCustAccountService = new MainCustAccountService();
                return mainCustAccountService.ChangeCustPassword(mainCustAccId, oldPassword, newPassword, mainCustAccId);
            } catch(Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        /// <summary>
        /// Change customer pin
        /// </summary>
        /// <param name="mainCustAccId">Id of main customer account</param>
        /// <param name="oldPin">Old pin</param>
        /// <param name="newPin">New pin</param>
        /// <returns>
        /// <para>Result of changing pin.</para>
        /// <para>RET_CODE=ERROR_ACCOUNT: Account does not exist.</para>
        /// <para>RET_CODE=PASSWORD_NOT_MATCH: Old password and new password don't match.</para>
        /// <para>RET_CODE=INCORRECT_PASSWORD: Old password is incorrect.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Change customer pin")]
        public int ChangeCustPin(string mainCustAccId, string oldPin, string newPin)
        {
            try
            {
                var mainCustAccountService = new MainCustAccountService();
                return mainCustAccountService.ChangeCustPin(mainCustAccId, oldPin, newPin, mainCustAccId);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        /// <summary>
        /// Create main customer account.
        /// </summary>
        /// <param name="mainCustAccountId">Id of main customer account</param>
        /// <param name="fullname">Fullname of main customer</param>
        /// <param name="email">Email of main customer</param>
        /// <param name="phone">Phone of main customer</param>
        /// <param name="actived">true if this customer is actived; otherwise, false</param>
        /// <param name="passwordLockReason">The reason to lock password</param>
        /// <param name="pinLockReason">The reason to lock pin</param>
        /// <param name="tokenId">Id of main customer's token</param>
        /// <param name="tokenName">Name of token</param>
        /// <param name="tokenActived">Status of token</param>
        /// <param name="customerType">Customer type</param>
        /// <param name="lockReason">Reason of locking</param>
        /// <param name="brokerId">Id of login broker</param>
        /// <param name="authType">Authenticate type</param>
        /// <param name="pinType">Pin type</param>
        /// <param name="languageId">The language id.</param>
        /// <returns>
        /// 	<see cref="MainCustAccount"/> object that contains customer account information.
        /// </returns>
        private static MainCustAccount CreateMainCustAccount(string mainCustAccountId, string fullname, string email,
            string phone, bool actived, int passwordLockReason, int pinLockReason, string tokenId, string tokenName, 
            string tokenActived, int customerType, int lockReason, string brokerId, short authType, short pinType,string languageId)
        {
            var mainCustomer = new MainCustAccount
            {
                MainCustAccountId = mainCustAccountId,
                Actived = actived,
                BrokerId = brokerId,
                FullName = fullname,
                Email = email,
                Phone = phone,
                PassLockReason = passwordLockReason,
                PinLockReason = pinLockReason,
                TokenId = tokenId,
                TokenName = tokenName,
                TokenActived = tokenActived,
                CustomerType = customerType,
                LockReason = lockReason,
                AuthType = authType,
                PinType = pinType,
                LanguageId = languageId
            };
            return mainCustomer;
        }

        /// <summary>
        /// Create sub customer account.
        /// </summary>
        /// <param name="subCustAccountId">Id of sub customer account</param>
        /// <param name="name">Name of sub customer</param>
        /// <param name="actived">true if sub customer account is actived; otherwise, false</param>
        /// <param name="lockAccountReason">Reason of locking account</param>
        /// <param name="mainCustAccountId">Id of main customer account</param>
        /// <returns>
        /// 	<see cref="SubCustAccount">SubCustAccount</see> object that contains sub customer information.
        /// </returns>
        private static SubCustAccount CreateSubCustAccount(string subCustAccountId, string name, bool actived, short lockAccountReason,
            string mainCustAccountId)
        {  
            var subCustAccount = new SubCustAccount
                                     {
                                         SubCustAccountId = subCustAccountId,
                                         Name = name,                                         
                                         Actived = actived,
                                         LockAccountReason = lockAccountReason,
                                         MainCustAccountId = mainCustAccountId
                                     };
            return subCustAccount;
        }

        /// <summary>
        /// Convert 085C123456 to 123456
        /// </summary>
        /// <param name="accountId">Account Id</param>
        /// <returns>String after being converted.</returns>
        private static string ProcessAccountId(string accountId)
        {
            /*if (accountId.Length > 4)
            {
                accountId = accountId.Substring(4, accountId.Length - 4);
            }*/
            return accountId;
        }

        /// <summary>
        /// Get list of customer action history.
        /// </summary>
        /// <param name="brokerId">BrokerId Id</param>
        /// <param name="fromDate">Search from date (DD/MM/YYYY)</param>
        /// <param name="toDate">Search to date (DD/MM/YYYY)</param>
        /// <param name="mainCustAccountId">Main customer account id</param>
        /// <param name="subCustAccountId">Sub customer account id</param>
        /// <param name="actionType">Account type</param>
        /// <param name="reason">Reason</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;CustomerActionHistory&gt;&gt;&gt;</see> object contains returned code, 
        /// returned message and a list of <see cref="MainCustAccount">MainCustAccount</see> object that contains customer action history information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=SUCCESS: Get list of main customer information successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list of customer action history information", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListCustomerActionHistory(string brokerId, string fromDate, string toDate, string mainCustAccountId,
            string subCustAccountId, int actionType, int reason, int pageIndex, int pageSize)
        {
            try
            {
                var resultObject = new ResultObject<PagingObject<List<CustomerActionHistory>>>();
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    resultObject.Result = null;
                    return Serializer.Serialize(resultObject);
                }

                // Increase 1 day to search
                if (!string.IsNullOrEmpty(toDate))
                {
                    var toDateSearch = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                    toDateSearch = toDateSearch.AddDays(1);
                    toDate = toDateSearch.ToString("dd/MM/yyyy");
                }
                var customerActionHistoryService = new CustomerActionHistoryService();
                int totalRecord;
                var returnList = customerActionHistoryService.GetList(brokerId, fromDate, toDate, mainCustAccountId,
                                                                  subCustAccountId, actionType, reason, pageIndex,
                                                                  pageSize, out totalRecord);
                resultObject.Result = returnList;
                resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                
                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                var resultObject = new ResultObject<PagingObject<List<CustomerActionHistory>>>
                {
                    RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                    Result = null
                };
                return Serializer.Serialize(resultObject);
            }

        }

        /// <summary>
        /// Customer logout.
        /// </summary>
        /// <param name="mainCustAccountId">Main customer account id</param>
        [WebMethod(Description = "Customer logout")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int CustomerLogout(string mainCustAccountId)
        {
            try
            {
                var customerActionHistoryService = new CustomerActionHistoryService();
                customerActionHistoryService.InsertCustomerActionHistory(string.Empty, DateTime.Now, mainCustAccountId,
                                                                     string.Empty, (int)CommonEnums.ACTION_TYPE.LOGOUT,
                                                                     -1);
                return (int)CommonEnums.RET_CODE.SUCCESS;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }
        #endregion

        #region OpenCustAccount
        ///<summary>
        /// Allow user to register to open account online.
        ///</summary>
        ///<param name="cardId">Card Id</param>
        ///<param name="cardIssue">Card issued date</param>
        ///<param name="placeIssue">Place of issued card</param>
        ///<param name="name">Customer name</param>
        ///<param name="birthday">Birthday of customer</param>
        ///<param name="sex">Sex of customer</param>
        ///<param name="occupation">Occupation of customer</param>
        ///<param name="nationality">Nationality of customer</param>
        ///<param name="address1">Address 1 of customer</param>
        ///<param name="telephone1">Telephone 1 of customer</param>
        ///<param name="fax1">Fax 1 of customer</param>
        ///<param name="address2">Address 2 of customer</param>
        ///<param name="telephone2">Telephone 2 of customer</param>
        ///<param name="fax2">Fax 2 of customer</param>
        ///<param name="address3">Address 3 of customer</param>
        ///<param name="telephone3">Telephone 3 of customer</param>
        ///<param name="fax3">Fax 3 of customer</param>
        ///<param name="email">Email of customer</param>
        ///<param name="branchCode">Branch code</param>
        ///<param name="branchName">Branch name</param>
        ///<param name="custodian">Custodian of customer (0 = normail customer, 1 = custodian)</param>
        ///<param name="customerType">Customer type (N = Normal, H = High Network)</param>
        ///<param name="tradeAtCompany">True if allowed to trade at securities company; otherwise false</param>
        ///<param name="tradeByTelephone">True if allowed to trade by phone; otherwise false</param>
        ///<param name="tradeOnline">True if allowed to trade online; otherwise false</param>
        ///<param name="existedAccount">True if account is existing; otherwise false</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject</see> object contains returned code, returned message and 
        /// string object that contains open id of this record.</para>
        /// <para>RET_CODE=SUCCESS: Create account successfully.</para>
        /// <para>RET_CODE=FAIL: Failed to create account.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Allow user to register to open account online")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CreateOpenCustAccount(string cardId, DateTime cardIssue, string placeIssue, string name,
                                          DateTime birthday, bool sex, string occupation, string nationality,
                                          string address1, string telephone1, string fax1, string address2,
                                          string telephone2, string fax2, string address3, string telephone3,
                                          string fax3, string email, string branchCode, string branchName,
                                          bool custodian, string customerType, bool tradeAtCompany,
                                          bool tradeByTelephone, bool tradeOnline, bool existedAccount)
        {
            var resultObject = new ResultObject<string>();
            try
            {
                var custAccount = new OpenCustAccount
                {
                    OpenId = PasswordGenerator.GeneratePassword(6, false, true, true),
                    RegisterDate = DateTime.Now,
                    CardId = cardId,
                    CardIssue = cardIssue,
                    PlaceIssue = placeIssue,
                    Name = name,
                    Birthday = birthday,
                    Sex = sex,
                    Occupation = occupation,
                    Nationality = nationality,
                    Address1 = address1,
                    Telephone1 = telephone1,
                    Fax1 = fax1,
                    Address2 = address2,
                    Telephone2 = telephone2,
                    Fax2 = fax2,
                    Address3 = address3,
                    Telephone3 = telephone3,
                    Fax3 = fax3,
                    Email = email,
                    BranchCode = branchCode,
                    BranchName = branchName,
                    Custodian = custodian,
                    CustomerType = customerType,
                    TradeAtCompany = tradeAtCompany,
                    TradeByTelephone = tradeByTelephone,
                    TradeOnline = tradeOnline,
                    ExistedAccount = existedAccount
                };
                var openCustAccountService = new OpenCustAccountService();
                var result = openCustAccountService.Insert(custAccount);
                if (!result)
                {
                    resultObject.Result = "";
                    resultObject.RetCode = CommonEnums.RET_CODE.FAIL;
                }
                else
                {
                    resultObject.Result = custAccount.OpenId;
                    resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.Result = "";
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
            return Serializer.Serialize(resultObject);
        }

        ///<summary>
        /// Allow to update open account.
        ///</summary>
        ///<param name="openId">Open Id</param>
        ///<param name="cardId">Card Id</param>
        ///<param name="cardIssue">Card issued date</param>
        ///<param name="placeIssue">Place of issued card</param>
        ///<param name="name">Customer name</param>
        ///<param name="birthday">Birthday of customer</param>
        ///<param name="sex">Sex of customer</param>
        ///<param name="occupation">Occupation of customer</param>
        ///<param name="nationality">Nationality of customer</param>
        ///<param name="address1">Address 1 of customer</param>
        ///<param name="telephone1">Telephone 1 of customer</param>
        ///<param name="fax1">Fax 1 of customer</param>
        ///<param name="address2">Address 2 of customer</param>
        ///<param name="telephone2">Telephone 2 of customer</param>
        ///<param name="fax2">Fax 2 of customer</param>
        ///<param name="address3">Address 3 of customer</param>
        ///<param name="telephone3">Telephone 3 of customer</param>
        ///<param name="fax3">Fax 3 of customer</param>
        ///<param name="email">Email of customer</param>
        ///<param name="branchCode">Branch code</param>
        ///<param name="branchName">Branch name</param>
        ///<param name="custodian">Custodian of customer (0 = normail customer, 1 = custodian)</param>
        ///<param name="customerType">Customer type (N = Normal, H = High Network)</param>
        ///<param name="tradeAtCompany">True if allowed to trade at securities company; otherwise false</param>
        ///<param name="tradeByTelephone">True if allowed to trade by phone; otherwise false</param>
        ///<param name="tradeOnline">True if allowed to trade online; otherwise false</param>
        ///<param name="existedAccount">True if account is existing; otherwise false</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject</see> object contains returned code, returned message and 
        /// string object that contains open id of this record.</para>
        /// <para>RET_CODE=SUCCESS: Create account successfully.</para>
        /// <para>RET_CODE=FAIL: Failed to create account.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Allow user to update open account online")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int UpdateOpenCustAccount(string openId, string cardId, DateTime cardIssue, string placeIssue, string name,
                                          DateTime birthday, bool sex, string occupation, string nationality,
                                          string address1, string telephone1, string fax1, string address2,
                                          string telephone2, string fax2, string address3, string telephone3,
                                          string fax3, string email, string branchCode, string branchName,
                                          bool custodian, string customerType, bool tradeAtCompany,
                                          bool tradeByTelephone, bool tradeOnline, bool existedAccount)
        {
            try
            {
                var custAccount = new OpenCustAccount
                {
                    OpenId = openId,
                    RegisterDate = DateTime.Now,
                    CardId = cardId,
                    CardIssue = cardIssue,
                    PlaceIssue = placeIssue,
                    Name = name,
                    Birthday = birthday,
                    Sex = sex,
                    Occupation = occupation,
                    Nationality = nationality,
                    Address1 = address1,
                    Telephone1 = telephone1,
                    Fax1 = fax1,
                    Address2 = address2,
                    Telephone2 = telephone2,
                    Fax2 = fax2,
                    Address3 = address3,
                    Telephone3 = telephone3,
                    Fax3 = fax3,
                    Email = email,
                    BranchCode = branchCode,
                    BranchName = branchName,
                    Custodian = custodian,
                    CustomerType = customerType,
                    TradeAtCompany = tradeAtCompany,
                    TradeByTelephone = tradeByTelephone,
                    TradeOnline = tradeOnline,
                    ExistedAccount = existedAccount
                };
                var openCustAccountService = new OpenCustAccountService();
                int result = openCustAccountService.UpdateOpenCustAccount(custAccount);
                return result;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int) CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        ///<summary>
        /// Delete an account registered by web.
        ///</summary>
        ///<param name="openId">Open id of customer's record</param>
        /// <returns>
        /// <para>Result of deleting open customer account.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=SUCCESS: Delete account successfully.</para>
        /// <para>RET_CODE=FAIL: Fail to delete account.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Delete an account registered by web", EnableSession = true)]
        public int DeleteOpenCustAccount(string openId)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int) CommonEnums.RET_CODE.NOT_LOGIN;
                }
                var openCustAccountService = new OpenCustAccountService();
                bool result = openCustAccountService.Delete(openId);
                if (!result)
                {
                    return (int)CommonEnums.RET_CODE.FAIL;
                }
                return (int)CommonEnums.RET_CODE.SUCCESS;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        ///<summary>
        /// Get the information that customer registered.
        ///</summary>
        ///<param name="openId">Open id of customer's record</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject</see> object contains returned code, returned message and 
        /// <see cref="OpenCustAccount">OpenCustAccount</see> object that contains open customer account information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=SUCCESS: Get account successfully.</para>
        /// <para>RET_CODE=FAIL: Fail to get account.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get the information that customer registered", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetOpenCustAccount(string openId)
        {
            var resultObject = new ResultObject<OpenCustAccount>();
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    resultObject.Result = null;
                    return Serializer.Serialize(resultObject);
                }
                var openCustAccountService = new OpenCustAccountService();
                var openCustAccount = openCustAccountService.GetByOpenId(openId);
                if (openCustAccount == null)
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.FAIL;
                    resultObject.Result = null;
                }
                else
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                    resultObject.Result = openCustAccount;
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                resultObject.Result = null;
            }
            return Serializer.Serialize(resultObject);
        }

        ///<summary>
        /// Get list of account registered by web.
        ///</summary>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;OpenCustAccount&gt;&gt;&gt;</see> object contains returned code, 
        /// returned message and a list of <see cref="OpenCustAccount">OpenCustAccount</see> object that contains open customer account information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=SUCCESS: Get account successfully.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list of account registered by web", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListOpenCustAccount(string cardId, string cardIssue, string placeIssue, string name,
                                          string birthday, int sex, string occupation, string nationality,
                                          string address1, string telephone1, string fax1, string address2,
                                          string telephone2, string fax2, string address3, string telephone3,
                                          string fax3, string email, string branchCode, string branchName,
                                          int custodian, string customerType, int tradeAtCompany,
                                          int tradeByTelephone, int tradeOnline, int existedAccount, 
                                          int pageIndex, int pageSize)
        {
            var resultObject = new ResultObject<PagingObject<List<OpenCustAccount>>>();
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    resultObject.Result = null;
                    return Serializer.Serialize(resultObject);
                }
                var openCustAccountService = new OpenCustAccountService();
                var openCustAccountList = openCustAccountService.GetList(cardId, cardIssue, placeIssue, branchName,
                                                                         birthday, sex, occupation, nationality,
                                                                         address1, telephone1, fax1, address2,
                                                                         telephone2, fax2, address3, telephone3, fax3,
                                                                         email, branchCode, branchName, custodian,
                                                                         customerType, tradeAtCompany, tradeByTelephone,
                                                                         tradeOnline, existedAccount, pageIndex,
                                                                         pageSize);


                if ((openCustAccountList == null) || (openCustAccountList.Count == 0))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
                    resultObject.Result = null;
                }
                else
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                    resultObject.Result = openCustAccountList;
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                resultObject.Result = null;
            }
            return Serializer.Serialize(resultObject);
        }

        #endregion

        #region Working days, holidays
        ///<summary>
        /// Get list of days in a week and its working status.
        ///</summary>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;List&lt;WorkingDays&gt;&gt;</see> object contains returned code, 
        /// returned message and a list of <see cref="WorkingDays">WorkingDays</see> objects that contains day status.</para>
        /// <para>RET_CODE=SUCCESS: Get account successfully.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list of days in a week and its working status.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListWorkingDays()
        {
            var resultObject = new ResultObject<List<WorkingDays>>();
            try
            {
                var workingDaysService = new WorkingDaysService();
                var workingDaysList = workingDaysService.GetAll();

                if ((workingDaysList == null) || (workingDaysList.Count == 0))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
                    resultObject.Result = null;
                }
                else
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                    resultObject.Result = workingDaysList.ToList();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                resultObject.Result = null;
            }
            return Serializer.Serialize(resultObject);
        }

        ///<summary>
        /// Update working days status
        ///</summary>
        ///<param name="mondayStatus">Monday status</param>
        ///<param name="tuesdayStatus">Tuesday status</param>
        ///<param name="wednesdayStatus">Wednesday status</param>
        ///<param name="thursdayStatus">Thursday status</param>
        ///<param name="fridayStatus">Friday status</param>
        ///<param name="saturdayStatus">Saturday status</param>
        ///<param name="sundayStatus">Sunday status</param>
        /// <returns>
        /// <para>Result of updating working days</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not logged in.</para>
        /// <para>RET_CODE=NOT_ALLOW: User is not allowed to do this function.</para>
        /// <para>RET_CODE=SUCCESS: Get account successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Update working days status", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int UpdateWorkingDays(bool mondayStatus, bool tuesdayStatus, bool wednesdayStatus, bool thursdayStatus, 
            bool fridayStatus, bool saturdayStatus, bool sundayStatus)
        {
            int result;
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }
                //Only user with admin type can use this function.
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    return (int)CommonEnums.RET_CODE.NOT_ALLOW;
                }
                var workingDaysService = new WorkingDaysService();
                result = workingDaysService.UpdateWorkingDays(mondayStatus, tuesdayStatus, wednesdayStatus,
                                                                  thursdayStatus, fridayStatus, saturdayStatus,
                                                                  sundayStatus);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                result = (int) CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
            return result;
        }

        ///<summary>
        /// Create holiday
        ///</summary>
        ///<param name="holiday">The holiday, format DD/MM/YYYY</param>
        ///<param name="note">The description</param>
        /// <returns>
        /// <para>Result of creating holiday</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not logged in.</para>
        /// <para>RET_CODE=NOT_ALLOW: User is not allowed to do this function.</para>
        /// <para>RET_CODE=INCORRECT_FORMAT: The format is incorrect.</para>
        /// <para>RET_CODE=EXISTED_DATA: Data is existing.</para>
        /// <para>RET_CODE=FAIL: Fail to create holiday.</para>
        /// <para>RET_CODE=SUCCESS: Create holiday successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Create holiday", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int CreateHoliday(string holiday, string note)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }
                //Only user with admin type can use this function.
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    return (int)CommonEnums.RET_CODE.NOT_ALLOW;
                }
                var holidaysService = new HolidaysService();
                int result = holidaysService.CreateHoliday(holiday, note);
                return result;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        ///<summary>
        /// Update holiday
        ///</summary>
        ///<param name="holiday">The holiday, format DD/MM/YYYY</param>
        ///<param name="note">The description</param>
        /// <returns>
        /// <para>Result of updating holiday</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not logged in.</para>
        /// <para>RET_CODE=NOT_ALLOW: User is not allowed to do this function.</para>
        /// <para>RET_CODE=INCORRECT_FORMAT: The format is incorrect.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: Data is not existing.</para>
        /// <para>RET_CODE=FAIL: Fail to update holiday.</para>
        /// <para>RET_CODE=SUCCESS: Update holiday successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Update holiday", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int UpdateHoliday(string holiday, string note)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }
                //Only user with admin type can use this function.
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    return (int)CommonEnums.RET_CODE.NOT_ALLOW;
                }
                var holidaysService = new HolidaysService();
                int result = holidaysService.UpdateHoliday(holiday, note);
                return result;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        ///<summary>
        /// Delete holiday
        ///</summary>
        ///<param name="holiday">The holiday, format DD/MM/YYYY</param>
        /// <returns>
        /// <para>Result of deleting holiday</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not logged in.</para>
        /// <para>RET_CODE=NOT_ALLOW: User is not allowed to do this function.</para>
        /// <para>RET_CODE=INCORRECT_FORMAT: The format is incorrect.</para>
        /// <para>RET_CODE=FAIL: Fail to delete holiday.</para>
        /// <para>RET_CODE=SUCCESS: Update holiday successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Delete holiday", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int DeleteHoliday(string holiday)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }
                //Only user with admin type can use this function.
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    return (int)CommonEnums.RET_CODE.NOT_ALLOW;
                }
                try
                {
                    var holidayDate = DateTime.ParseExact(holiday, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var holidaysService = new HolidaysService();
                    bool result = holidaysService.Delete(holidayDate);
                    if (result)
                    {
                        return (int) CommonEnums.RET_CODE.SUCCESS;
                    }
                    return (int)CommonEnums.RET_CODE.FAIL;
                }
                catch (FormatException)
                {
                    return (int)CommonEnums.RET_CODE.INCORRECT_FORMAT;
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        ///<summary>
        /// Get holiday
        ///</summary>
        ///<param name="holiday">The holiday, format DD/MM/YYYY</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;Holidays&gt;</see> object contains returned code, 
        /// returned message and a <see cref="Holidays">Holidays</see> object that contains holiday information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not logged in.</para>
        /// <para>RET_CODE=NOT_ALLOW: User is not allowed to do this function.</para>
        /// <para>RET_CODE=INCORRECT_FORMAT: The format is incorrect.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: .</para>
        /// <para>RET_CODE=SUCCESS: Update holiday successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get holiday")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetHoliday(string holiday)
        {
            var resultObject = new ResultObject<Holidays>();
            try
            {
                try
                {
                    var holidayDate = DateTime.ParseExact(holiday, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var holidaysService = new HolidaysService();
                    var holidayObject = holidaysService.GetByHoliday(holidayDate);
                    if (holidayObject != null)
                    {
                        resultObject.Result = holidayObject;
                        resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                        return Serializer.Serialize(resultObject);
                    }
                    resultObject.Result = null;
                    resultObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
                    return Serializer.Serialize(resultObject);
                }
                catch (FormatException)
                {
                    resultObject.Result = null;
                    resultObject.RetCode = CommonEnums.RET_CODE.INCORRECT_FORMAT;
                    return Serializer.Serialize(resultObject);
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.Result = null;
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                return Serializer.Serialize(resultObject);
            }
        }

        ///<summary>
        /// Get list holiday
        ///</summary>
        ///<param name="fromDate">The search from date, format DD/MM/YYYY</param>
        ///<param name="toDate">The search to date, format DD/MM/YYYY</param>
        ///<param name="pageIndex">Page index, begin with 1</param>
        ///<param name="pageSize">Page size</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;Holidays&gt;&gt;&gt;</see> object contains total record, returned code, 
        /// returned message and a list of <see cref="Holidays">Holidays</see> object that contains holiday information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not logged in.</para>
        /// <para>RET_CODE=NOT_ALLOW: User is not allowed to do this function.</para>
        /// <para>RET_CODE=FAIL: Fail to delete holiday.</para>
        /// <para>RET_CODE=SUCCESS: Update holiday successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list holiday")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListHoliday(string fromDate, string toDate, int pageIndex, int pageSize)
        {
            var resultObject = new ResultObject<PagingObject<List<Holidays>>>();
            try
            {
                try
                {
                    var holidaysService = new HolidaysService();
                    var pagingObject = holidaysService.GetListHolidays(fromDate, toDate, pageIndex - 1, pageSize);
                    if (pagingObject != null)
                    {
                        resultObject.Result = pagingObject;
                        resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                        return Serializer.Serialize(resultObject);
                    }
                    resultObject.Result = null;
                    resultObject.RetCode = CommonEnums.RET_CODE.FAIL;
                    return Serializer.Serialize(resultObject);
                }
                catch (FormatException)
                {
                    resultObject.Result = null;
                    resultObject.RetCode = CommonEnums.RET_CODE.INCORRECT_FORMAT;
                    return Serializer.Serialize(resultObject);
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.Result = null;
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                return Serializer.Serialize(resultObject);
            }
        }
        #endregion

        #region Configuration
        ///<summary>
        /// Get list of configurations
        ///</summary>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;List&lt;Configurations&gt;&gt;</see> object contains returned code, 
        /// returned message and a list of <see cref="Configurations">Configurations</see> object that contains holiday information.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: No data.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list configurations")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListConfiguration()
        {
            var resultObject = new ResultObject<List<Configurations>>();
            try
            {
                var configurationsService = new ConfigurationsService();
                var list = configurationsService.GetAll();
                if ((list != null) && (list.Count > 0))
                {
                    resultObject.Result = list.ToList();
                    resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                }
                else
                {
                    resultObject.Result = null;
                    resultObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.Result = null;
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                
            }
            return Serializer.Serialize(resultObject);
        }

        ///<summary>
        /// Get configuration.
        ///</summary>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;Holidays&gt;&gt;&gt;</see> object contains total record, returned code, 
        /// returned message and a list of <see cref="Holidays">Holidays</see> object that contains holiday information.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: No data.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get configuration")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetConfiguration(string name)
        {
            var resultObject = new ResultObject<Configurations>();
            try
            {
                var configurationsService = new ConfigurationsService();
                var configuration = configurationsService.GetByName(name);
                if (configuration != null)
                {
                    resultObject.Result = configuration;
                    resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                }
                else
                {
                    resultObject.Result = null;
                    resultObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.Result = null;
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;

            }
            return Serializer.Serialize(resultObject);
        }

        ///<summary>
        /// Create configuration
        ///</summary>
        ///<param name="name">Name of configuration</param>
        ///<param name="value">Value of configuration</param>
        /// <returns>
        /// <para>Result of creating configuration</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not logged in.</para>
        /// <para>RET_CODE=NOT_ALLOW: User is not allowed to do this function.</para>
        /// <para>RET_CODE=EXISTED_DATA: Data is existing.</para>
        /// <para>RET_CODE=FAIL: Fail to create data.</para>
        /// <para>RET_CODE=SUCCESS: Create data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Create configuration", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int CreateConfiguration(string name, string value)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }
                //Only user with admin type can use this function.
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    return (int)CommonEnums.RET_CODE.NOT_ALLOW;
                }
                var configurationService = new ConfigurationsService();
                int result = configurationService.CreateConfiguration(name, value);
                return result;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        ///<summary>
        /// Update Configuration
        ///</summary>
        ///<param name="name">Name of configuration</param>
        ///<param name="value">Value of configuration</param>
        /// <returns>
        /// <para>Result of updating configuration</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not logged in.</para>
        /// <para>RET_CODE=NOT_ALLOW: User is not allowed to do this function.</para>
        /// <para>RET_CODE=INCORRECT_FORMAT: The format is incorrect.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: Data is not existing.</para>
        /// <para>RET_CODE=FAIL: Fail to update data.</para>
        /// <para>RET_CODE=SUCCESS: Update data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Update configuration", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int UpdateConfiguration(string name, string value)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }
                //Only user with admin type can use this function.
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    return (int)CommonEnums.RET_CODE.NOT_ALLOW;
                }
                var configurationService = new ConfigurationsService();
                int result = configurationService.UpdateConfiguration(name, value);
                return result;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        ///<summary>
        /// Delete configuration
        ///</summary>
        ///<param name="name">Name of configuration</param>
        /// <returns>
        /// <para>Result of deleting configuration</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not logged in.</para>
        /// <para>RET_CODE=NOT_ALLOW: User is not allowed to do this function.</para>
        /// <para>RET_CODE=FAIL: Fail to delete configuration.</para>
        /// <para>RET_CODE=SUCCESS: Update configuration successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Delete configuration", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int DeleteConfiguration(string name)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }
                //Only user with admin type can use this function.
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    return (int)CommonEnums.RET_CODE.NOT_ALLOW;
                }
                var configurationService = new ConfigurationsService();
                bool result = configurationService.Delete(name);
                if (result)
                {
                    return (int) CommonEnums.RET_CODE.SUCCESS;
                }
                return (int) CommonEnums.RET_CODE.FAIL;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }
        #endregion

        #region Buy Right

        /// <summary>
        /// Puts the buy right.
        /// </summary>
        /// <param name="subCustAccountId">The sub cust account ID.</param>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <param name="market">The market.</param>
        /// <param name="execDate">The exec date.</param>
        /// <param name="owningVol">The owning vol.</param>
        /// <param name="allowedVol">The allowed vol.</param>
        /// <param name="right">The right.</param>
        /// <param name="rateRight">The rate right.</param>
        /// <param name="price">The price.</param>
        /// <param name="beginDateToRegister">The begin date to register.</param>
        /// <param name="endDateToRegister">The end date to register.</param>
        /// <param name="beginDateToTransfer">The begin date to transfer.</param>
        /// <param name="endDateToTransfer">The end date to transfer.</param>
        /// <param name="receivedDate">The received date.</param>
        /// <param name="note">The note.</param>
        /// <returns>
        /// <para>Result of putting buy right.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not logged in.</para>
        /// <para>RET_CODE=NOT_ALLOW: User is not allowed to do this function.</para>
        /// <para>RET_CODE=FAIL: Fail to putting buy right.</para>
        /// <para>RET_CODE=SUCCESS: Putting buy right successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Put a buy right. Return ret code", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public  int PutBuyRight(string subCustAccountId,string secSymbol,string market,string execDate ,long owningVol,long allowedVol,decimal right,decimal rateRight,decimal price,string beginDateToRegister,string endDateToRegister,string beginDateToTransfer,string endDateToTransfer,string receivedDate,string note)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int) CommonEnums.RET_CODE.NOT_LOGIN;                    
                }

                //Only user with admin type can use this function and broker with right CAN_VIEW_BUY_RIGHT
                // and CAN_PROCESS_BUY_RIGHT
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    //var brokerPermissionService = new BrokerPermissionService();
                    //var brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                    //                                        (int)CommonEnums.BROKER_PERMISSIONS.CAN_VIEW_BUY_RIGHT);
                    //if (brokerPermission == null)
                    //{
                    //    brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                    //                                        (int)CommonEnums.BROKER_PERMISSIONS.CAN_PROCESS_BUY_RIGHT);
                    //    if (brokerPermission == null)
                    //    {
                    //        return (int) CommonEnums.RET_CODE.NOT_ALLOW;                            
                    //    }
                    //}
                    return (int)CommonEnums.RET_CODE.NOT_ALLOW;   
                }
                                
                var buyRightService=new BuyRightService();
                return buyRightService.PutBuyRight(subCustAccountId, secSymbol, market, execDate, owningVol, allowedVol, rateRight, rateRight, price, beginDateToRegister, endDateToRegister, beginDateToTransfer, endDateToTransfer, receivedDate, note, loginBrokerId);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);                
                return (int) CommonEnums.RET_CODE.SYSTEM_ERROR;                
            }
        }

        /// <summary>
        /// Updates the buy right.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <param name="market">The market.</param>
        /// <param name="execDate">The exec date.</param>
        /// <param name="owningVol">The owning vol.</param>
        /// <param name="allowedVol">The allowed vol.</param>
        /// <param name="right">The right.</param>
        /// <param name="rateRight">The rate right.</param>
        /// <param name="price">The price.</param>
        /// <param name="beginDateToRegister">The begin date to register.</param>
        /// <param name="endDateToRegister">The end date to register.</param>
        /// <param name="beginDateToTransfer">The begin date to transfer.</param>
        /// <param name="endDateToTransfer">The end date to transfer.</param>
        /// <param name="receivedDate">The received date.</param>
        /// <param name="note">The note.</param>
        /// <returns>
        /// <para>Result of putting buy right.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not logged in.</para>
        /// <para>RET_CODE=NOT_ALLOW: User is not allowed to do this function.</para>
        /// <para>RET_CODE=FAIL: Fail to updating buy right.</para>
        /// <para>RET_CODE=SUCCESS: Updating buy right successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Put a buy right. Return ret code", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int UpdateBuyRight(long id, string secSymbol,string market, string execDate, long owningVol, long allowedVol, decimal right, decimal rateRight, decimal price, string beginDateToRegister, string endDateToRegister, string beginDateToTransfer, string endDateToTransfer, string receivedDate, string note)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }

                //Only user with admin type can use this function and broker with right CAN_VIEW_BUY_RIGHT
                // and CAN_PROCESS_BUY_RIGHT
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    //var brokerPermissionService = new BrokerPermissionService();
                    //var brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                    //                                        (int)CommonEnums.BROKER_PERMISSIONS.CAN_VIEW_BUY_RIGHT);
                    //if (brokerPermission == null)
                    //{
                    //    brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                    //                                        (int)CommonEnums.BROKER_PERMISSIONS.CAN_PROCESS_BUY_RIGHT);
                    //    if (brokerPermission == null)
                    //    {
                    //        return (int)CommonEnums.RET_CODE.NOT_ALLOW;
                    //    }
                    //}
                    return (int)CommonEnums.RET_CODE.NOT_ALLOW;   
                }

                var buyRightService = new BuyRightService();
                return buyRightService.UpdateBuyRight(id, secSymbol, market, execDate, owningVol, allowedVol, right, rateRight, price, beginDateToRegister, endDateToRegister, beginDateToTransfer, endDateToTransfer, receivedDate, note, loginBrokerId);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        /// <summary>
        /// Gets the list buy right.
        /// </summary>
        /// <param name="id">Record id.</param>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <param name="subCustAccountId">Sub customer account id.</param>
        /// <param name="execDate">The exec date.</param>
        /// <param name="beginDateToRegister">The begin date to register.</param>
        /// <param name="endDateToRegister">The end date to register.</param>
        /// <param name="beginDateToTransfer">The begin date to transfer.</param>
        /// <param name="endDateToTransfer">The end date to transfer.</param>
        /// <param name="receivedDate">The received date.</param>
        /// <param name="note">The note.</param>
        /// <param name="brokerId">The broker ID.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="market">The market.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;BuyRight&gt;&gt;&gt;</see> object contains total record, returned code, 
        /// returned message and a list of <see cref="BuyRight">BuyRight</see> object that contains buy right information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not logged in.</para>
        /// <para>RET_CODE=NOT_ALLOW: User is not allowed to do this function.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: No data.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list buy right. Return ResultObject PagingObject List BuyRight", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListBuyRight(long id, string secSymbol,string market, string subCustAccountId, string execDate, 
            string beginDateToRegister, string endDateToRegister, string beginDateToTransfer, string endDateToTransfer, 
            string receivedDate, string note, string brokerId, int pageIndex, int pageSize)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return Serializer.Serialize(new ResultObject<PagingObject<List<BuyRight>>>
                    {
                        Result = null,
                        ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString(),
                        RetCode = CommonEnums.RET_CODE.NOT_LOGIN
                    });
                }
                //Only user with admin type can use this function and broker with right CAN_VIEW_BUY_RIGHT
                // and CAN_PROCESS_BUY_RIGHT
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    //var brokerPermissionService = new BrokerPermissionService();
                    //var brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                    //                                        (int)CommonEnums.BROKER_PERMISSIONS.CAN_VIEW_BUY_RIGHT);
                    //if (brokerPermission == null)
                    //{
                    //    brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                    //                                        (int)CommonEnums.BROKER_PERMISSIONS.CAN_PROCESS_BUY_RIGHT);
                    //    if (brokerPermission == null)
                    //    {
                    //        return Serializer.Serialize( new ResultObject<PagingObject<List<BuyRight>>>
                    //        {
                    //            Result = null,
                    //            ErrorMessage = CommonEnums.RET_CODE.NOT_ALLOW.ToString(),
                    //            RetCode = CommonEnums.RET_CODE.NOT_ALLOW
                    //        }); 
                    //    }
                    //}
                            return Serializer.Serialize( new ResultObject<PagingObject<List<BuyRight>>>
                            {
                                Result = null,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_ALLOW.ToString(),
                                RetCode = CommonEnums.RET_CODE.NOT_ALLOW
                            }); 
                }
                
                var buyRightService=new BuyRightService();
                PagingObject<List<BuyRight>> listBuyRight = buyRightService.GetListBuyRight(id, secSymbol, market, subCustAccountId, execDate,
                                                                                            beginDateToRegister,
                                                                                            endDateToRegister,
                                                                                            beginDateToTransfer,
                                                                                            endDateToTransfer,
                                                                                            receivedDate, note, brokerId,
                                                                                            pageIndex, pageSize);
                if(listBuyRight!=null && listBuyRight.Count>0)
                {
                    var generalService=new GeneralService();
                    foreach (var buyRight in listBuyRight.Data)
                    {
                        long registeredAmount = generalService.GetTotalUnfinishedXROrderRegisterAmount(buyRight.Id,string.Empty,buyRight.SecSymbol,string.Empty);
                        buyRight.RegisteredVol = registeredAmount;
                    }
                    return Serializer.Serialize( new ResultObject<PagingObject<List<BuyRight>>>
                               {
                                   Result = listBuyRight,
                                   ErrorMessage= CommonEnums.RET_CODE.SUCCESS.ToString(),
                                   RetCode =  CommonEnums.RET_CODE.SUCCESS
                               });
                }
                return Serializer.Serialize(new ResultObject<PagingObject<List<BuyRight>>>
                                                {
                                                    Result = null,
                                                    ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                                                    RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                                                });
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);     
                return  Serializer.Serialize(new ResultObject<PagingObject<List<BuyRight>>>
                           {
                               Result = null,
                               ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString(),
                               RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR
                           });
            }
        }

        /// <summary>
        /// Gets the list buy right no session.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <param name="subCustAccountID">Sub customer account id.</param>
        /// <param name="execDate">The exec date.</param>
        /// <param name="beginDateToRegister">The begin date to register.</param>
        /// <param name="endDateToRegister">The end date to register.</param>
        /// <param name="beginDateToTransfer">The begin date to transfer.</param>
        /// <param name="endDateToTransfer">The end date to transfer.</param>
        /// <param name="receivedDate">The received date.</param>
        /// <param name="note">The note.</param>
        /// <param name="brokerID">The broker ID.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="market">The market.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;BuyRight&gt;&gt;&gt;</see> object contains total record, returned code, 
        /// returned message and a list of <see cref="BuyRight">BuyRight</see> object that contains buy right information.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: No data.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list buy right not validate broker's authentication . Return ResultObject PagingObject List BuyRight")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListBuyRightNoSession(long id, string secSymbol,string market, string subCustAccountID, 
            string execDate, string beginDateToRegister, string endDateToRegister, string beginDateToTransfer, 
            string endDateToTransfer, string receivedDate, string note, string brokerID, int pageIndex, int pageSize)
        {
            try
            {                
                if (!string.IsNullOrEmpty(execDate))
                {
                    DateTime execDateTime=new DateTime();
                    if (ETradeCommon.Utils.IsValidDate(execDate,ref execDateTime))
                    {
                        execDate = execDateTime.ToString("dd/MM/yyyy");
                    }
                }

                if (!string.IsNullOrEmpty(beginDateToRegister))
                {
                    DateTime beginDateToRegisterSearch=new DateTime();
                    if(ETradeCommon.Utils.IsValidDate(beginDateToRegister,ref beginDateToRegisterSearch))
                    {
                        beginDateToRegister = beginDateToRegisterSearch.ToString("dd/MM/yyyy");
                    }
                }

                if (!string.IsNullOrEmpty(endDateToRegister))
                {
                    DateTime endDateToRegisterSearch = new DateTime();
                    if(ETradeCommon.Utils.IsValidDate(endDateToRegister,ref endDateToRegisterSearch))
                    {
                        endDateToRegister = endDateToRegisterSearch.ToString("dd/MM/yyyy");
                    }
                }
                if (!string.IsNullOrEmpty(beginDateToTransfer))
                {
                    DateTime beginDateToTransferSearch = new DateTime();
                    if(ETradeCommon.Utils.IsValidDate(beginDateToTransfer,ref beginDateToTransferSearch))
                    {
                        beginDateToTransfer = beginDateToTransferSearch.ToString("dd/MM/yyyy");
                    }
                }
                if (!string.IsNullOrEmpty(endDateToTransfer))
                {
                    DateTime endDateToTransferSearch = new DateTime();     
                    if(ETradeCommon.Utils.IsValidDate(endDateToTransfer,ref endDateToTransferSearch))
                    {
                        endDateToTransfer = endDateToTransferSearch.ToString("dd/MM/yyyy");
                    }
                }
                if (!string.IsNullOrEmpty(receivedDate))
                {
                    DateTime receivedDateTime = new DateTime();
                    if(ETradeCommon.Utils.IsValidDate(receivedDate,ref receivedDateTime))
                    {
                        receivedDate = receivedDateTime.ToString("dd/MM/yyyy");
                    }
                }

                var buyRightService = new BuyRightService();
                PagingObject<List<BuyRight>> listBuyRight = buyRightService.GetListBuyRight(id, secSymbol, market, subCustAccountID, execDate,
                                                                                            beginDateToRegister,
                                                                                            endDateToRegister,
                                                                                            beginDateToTransfer,
                                                                                            endDateToTransfer,
                                                                                            receivedDate, note, brokerID,
                                                                                            pageIndex, pageSize);
                if (listBuyRight != null && listBuyRight.Count > 0)
                {
                    var generalService = new GeneralService();
                    foreach (var buyRight in listBuyRight.Data)
                    {
                        long registeredAmount = generalService.GetTotalUnfinishedXROrderRegisterAmount(buyRight.Id, string.Empty, buyRight.SecSymbol, string.Empty);
                        buyRight.RegisteredVol = registeredAmount;
                    }

                    return Serializer.Serialize(new ResultObject<PagingObject<List<BuyRight>>>
                    {
                        Result = listBuyRight,
                        ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                        RetCode = CommonEnums.RET_CODE.SUCCESS
                    });
                }
                return Serializer.Serialize(new ResultObject<PagingObject<List<BuyRight>>>
                                                {
                                                    Result = null,
                                                    ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                                                    RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                                                });
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(new ResultObject<PagingObject<List<BuyRight>>>
                {
                    Result = null,
                    ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString(),
                    RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR
                });
            }            
        }

        /// <summary>
        /// Deletes the buy right.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Result of deleting buy right.</returns>
        [WebMethod(Description = "Delete a buy right. Return ret code", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int DeleteBuyRight(long id)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int) CommonEnums.RET_CODE.NOT_LOGIN;
                }
                //Only user with admin type can use this function and broker with right CAN_VIEW_BUY_RIGHT
                // and CAN_PROCESS_BUY_RIGHT
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    //var brokerPermissionService = new BrokerPermissionService();
                    //var brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                    //                                        (int)CommonEnums.BROKER_PERMISSIONS.CAN_VIEW_BUY_RIGHT);
                    //if (brokerPermission == null)
                    //{
                    //    brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                    //                                        (int)CommonEnums.BROKER_PERMISSIONS.CAN_PROCESS_BUY_RIGHT);
                    //    if (brokerPermission == null)
                    //    {
                    //        return (int) CommonEnums.RET_CODE.NOT_ALLOW;
                    //    }
                    //}
                    return (int)CommonEnums.RET_CODE.NOT_ALLOW;   
                }               
                var generalService= new GeneralService();
                PagingObject<List<XrOrders>> resultObject = generalService.GetListXROrderHist(id, String.Empty,
                                                                                              String.Empty, String.Empty,
                                                                                              String.Empty, String.Empty,
                                                                                              -1, String.Empty,
                                                                                              String.Empty, 1,
                                                                                              int.MaxValue);
                if (resultObject != null && resultObject.Count > 0)
                    return (int) CommonEnums.RET_CODE.ERROR_CANNOT_DELETE;

                var buyRightService=new BuyRightService();
                return buyRightService.DeleteBuyRight(id);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int) CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        #endregion

        #region Finance
        #region AdvanceTime
        ///<summary>
        /// Get list of Advance times
        ///</summary>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;List&lt;AdvanceTime&gt;&gt;</see> object contains returned code, 
        /// returned message and a list of <see cref="AdvanceTime">AdvanceTime</see> object that contains advance time information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not logged in.</para>
        /// <para>RET_CODE=NOT_ALLOW: User is not allowed to do this function.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: No data.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list Advance Time", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetLisAdvanceTime()
        {
            var resultObject = new ResultObject<List<AdvanceTime>>();
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    resultObject.Result = null;
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                }
                var advanceTimeService = new AdvanceTimeService();
                var list = advanceTimeService.GetAll();
                if ((list != null) && (list.Count > 0))
                {
                    resultObject.Result = list.ToList();
                    resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                }
                else
                {
                    resultObject.Result = null;
                    resultObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.Result = null;
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;

            }
            return Serializer.Serialize(resultObject);
        }

        ///<summary>
        /// Update Advance Time
        ///</summary>
        ///<param name="advanceTimeList">List of arry objects with 3 elements</param>
        /// <returns>
        /// <para>Result of updating Advance Time</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not logged in.</para>
        /// <para>RET_CODE=NOT_ALLOW: User is not allowed to do this function.</para>
        /// <para>RET_CODE=INCORRECT_FORMAT: The time is incorrect.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: Data is not existing.</para>
        /// <para>RET_CODE=FAIL: Fail to update data.</para>
        /// <para>RET_CODE=SUCCESS: Update data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Update Advance Time", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int UpdateAdvanceTime(List<string[]> advanceTimeList)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }
                //Only user with admin type can use this function.
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    return (int)CommonEnums.RET_CODE.NOT_ALLOW;
                }
                var advanceTimeService = new AdvanceTimeService();
                int result = advanceTimeService.UpdateAdvanceTime(advanceTimeList);
                return result;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }
        
        #endregion

        #region Fee
        ///<summary>
        /// Create fee
        ///</summary>
        ///<param name="minValue">Min value of fee range</param>
        ///<param name="maxValue">Max value of fee range</param>
        ///<param name="minFee">Name of configuration</param>
        /// <param name="feeRatio">Fee ratio</param>
        /// <param name="feeType">Fee type</param>
        /// <param name="vat">Vat fee</param>
        /// <returns>
        /// <para>Result of creating fee</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not logged in.</para>
        /// <para>RET_CODE=NOT_ALLOW: User is not allowed to do this function.</para>
        /// <para>RET_CODE=RANGE_OVERLAP: Data is overlap with data in database.</para>
        /// <para>RET_CODE=EXISTED_DATA: Data is existing.</para>
        /// <para>RET_CODE=FAIL: Fail to create data.</para>
        /// <para>RET_CODE=SUCCESS: Create data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Create fee", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int CreateFee(decimal minValue, decimal maxValue, decimal minFee, decimal feeRatio, int feeType, decimal vat)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }
                //Only user with admin type can use this function.
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    return (int)CommonEnums.RET_CODE.NOT_ALLOW;
                }
                var feeService = new FeeService();
                int result = feeService.CreateFee(minValue, maxValue, minFee, feeRatio, feeType, vat);
                return result;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        ///<summary>
        /// Update fee
        ///</summary>
        ///<param name="feeId">Fee id</param>
        ///<param name="minValue">Min value of fee range</param>
        ///<param name="maxValue">Max value of fee range</param>
        ///<param name="minFee">Name of configuration</param>
        /// <param name="feeRatio">Fee ratio</param>
        /// <param name="feeType">Fee type</param>
        /// <param name="vat">Vat fee</param>
        /// <returns>
        /// <para>Result of updating fee</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not logged in.</para>
        /// <para>RET_CODE=NOT_ALLOW: User is not allowed to do this function.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: Data is not existing.</para>
        /// <para>RET_CODE=FAIL: Fail to update data.</para>
        /// <para>RET_CODE=SUCCESS: Update data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Update fee", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int UpdateFee(int feeId, decimal minValue, decimal maxValue, decimal minFee, decimal feeRatio, int feeType, decimal vat)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }
                //Only user with admin type can use this function.
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    return (int)CommonEnums.RET_CODE.NOT_ALLOW;
                }
                var feeService = new FeeService();
                int result = feeService.UpdateFee(feeId, minValue, maxValue, minFee, feeRatio, feeType, vat);
                return result;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        ///<summary>
        /// Delete fee
        ///</summary>
        ///<param name="feeId">Fee id</param>
        /// <returns>
        /// <para>Result of deleting fee</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not logged in.</para>
        /// <para>RET_CODE=NOT_ALLOW: User is not allowed to do this function.</para>
        /// <para>RET_CODE=FAIL: Fail to delete data.</para>
        /// <para>RET_CODE=SUCCESS: Delete data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Delete fee", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int DeleteFee(int feeId)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }
                //Only user with admin type can use this function.
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    return (int)CommonEnums.RET_CODE.NOT_ALLOW;
                }
                var feeService = new FeeService();
                bool result = feeService.Delete(feeId);
                if (result)
                {
                    return (int) CommonEnums.RET_CODE.SUCCESS;
                }

                return (int) CommonEnums.RET_CODE.FAIL;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        ///<summary>
        /// Get list of fees
        ///</summary>
        ///<param name="feeType">Fee type</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;List&lt;Fee&gt;&gt;</see> object contains returned code, 
        /// returned message and a list of <see cref="Fee">Fee</see> object that contains Fee information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not logged in.</para>
        /// <para>RET_CODE=NOT_ALLOW: User is not allowed to do this function.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list fee", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListFee(int feeType)
        {
            var resultObject = new ResultObject<List<Fee>> {Result = null};
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    return Serializer.Serialize(resultObject);
                }
                var feeService = new FeeService();
                var list = feeService.GetListFee(feeType);
                if ((list != null) && (list.Count == 0))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }
                else
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                    resultObject.Result = list;
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
            return Serializer.Serialize(resultObject);
        }
        #endregion

        #region CashAdvance
        /// <summary>
        /// Rejects the  cash advance expired.
        /// </summary>
        /// <param name="reason">The reason.</param>
        /// <returns></returns>
        [WebMethod(Description = "Reject list cash advance expired")]
        public void RejectCashAdvanceExpired(string reason)
        {
            try
            {
                var cashAdvanceService = new CashAdvanceService();
                cashAdvanceService.RejectCashAdvanceExpired(reason);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);                
            }
        }

        ///<summary>
        /// Update Cash advance
        ///</summary>
        ///<param name="id">Cash advance id</param>
        ///<param name="cashReceived">Cash received</param>
        ///<param name="status">Status</param>
        ///<param name="reason">Reason</param>
        /// <returns>
        /// <para>Result of updating Cash advance</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not logged in.</para>
        /// <para>RET_CODE=NOT_ALLOW: User is not allowed to do this function.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: Data is not existing.</para>
        /// <para>RET_CODE=FAIL: Fail to update data.</para>
        /// <para>RET_CODE=SUCCESS: Update data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Update Cash advance", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int UpdateCashAdvance(long id, decimal cashReceived, int status, string reason)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }
                //Only user with admin type can use this function and broker with right CAN_PROCESS_CASH_ADVANCE.
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    var brokerPermissionService = new BrokerPermissionService();
                    var brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_PROCESS_CASH_ADVANCE);
                    if (brokerPermission == null)
                    {
                        return (int) CommonEnums.RET_CODE.NOT_ALLOW;
                    }
                }
                var cashAdvanceService = new CashAdvanceService();
                int result = cashAdvanceService.UpdateCashAdvance(id, cashReceived, status, reason, loginBrokerId);
                return result;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
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
        ///<returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject$gt;List&lt;CashAdvance&gt;&gt;&gt;</see> object contains returned code, 
        /// returned message and a list of <see cref="CashAdvance">CashAdvance</see> object that contains CashAdvance information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not logged in.</para>
        /// <para>RET_CODE=NOT_ALLOW: User is not allowed to do this function.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: Data is not existing.</para>
        /// <para>RET_CODE=FAIL: Fail to update data.</para>
        /// <para>RET_CODE=SUCCESS: Update data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list of Cash advances", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListCashAdvance(string subAccountId, string contractNo, int status, int tradeType,
            int pageIndex, int pageSize)
        {
            var resultObject = new ResultObject<PagingObject<List<CashAdvance>>> {Result = null};
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    return Serializer.Serialize(resultObject);
                }
                //Only user with admin type can use this function and broker with right CAN_VIEW_CASH_ADVANCE
                // and CAN_PROCESS_CASH_ADVANCE.
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    var brokerPermissionService = new BrokerPermissionService();
                    var brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_VIEW_CASH_ADVANCE);
                    if (brokerPermission == null)
                    {
                        brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_PROCESS_CASH_ADVANCE);
                        if (brokerPermission == null)
                        {
                            resultObject.RetCode = CommonEnums.RET_CODE.NOT_ALLOW;
                            return Serializer.Serialize(resultObject);
                        }
                    }
                }

                var generalService = new GeneralService();
                int count;
                var result = generalService.GetListCashAdvance(subAccountId, contractNo, status, tradeType,
                                                                   pageIndex, pageSize, out count);
                if ((result == null) || (result.Count == 0))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }
                else
                {
                    var pagingObject = new PagingObject<List<CashAdvance>> {Count = count, Data = result};
                    resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                    resultObject.Result = pagingObject;
                }
                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                return Serializer.Serialize(resultObject);
            }
        }

        ///<summary>
        /// Get list of Cash advance history data
        ///</summary>
        ///<param name="subAccountId">Sub account id</param>
        ///<param name="fromDate">Advance date from, format DD/MM/YYYY</param>
        ///<param name="toDate">Advance date to, format DD/MM/YYYY</param>
        ///<param name="contractNo">Contract no</param>
        ///<param name="sellDueDateTo">Sell Due date from, format DD/MM/YYYY</param>
        ///<param name="sellDueDateFrom">Sell Due date from, format DD/MM/YYYY</param>
        ///<param name="status">Status</param>
        ///<param name="tradeType">Trade type</param>
        ///<param name="pageIndex">Page index</param>
        ///<param name="pageSize">Page size</param>
        ///<returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject$gt;List&lt;CashAdvanceHistory&gt;&gt;&gt;</see> object contains returned code, 
        /// returned message and a list of <see cref="CashAdvanceHistory">CashAdvanceHistory</see> object that contains CashAdvanceHistory information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not logged in.</para>
        /// <para>RET_CODE=NOT_ALLOW: User is not allowed to do this function.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: Data is not existing.</para>
        /// <para>RET_CODE=FAIL: Fail to update data.</para>
        /// <para>RET_CODE=SUCCESS: Update data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list of Cash advance history data", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListCashAdvanceHistory(string subAccountId, string fromDate, string toDate, string contractNo, 
            string sellDueDateFrom, string sellDueDateTo, int status, int tradeType, int pageIndex, int pageSize)
        {
            var resultObject = new ResultObject<PagingObject<List<CashAdvanceHistory>>> { Result = null };
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    return Serializer.Serialize(resultObject);
                }
                //Only user with admin type can use this function and broker with right CAN_VIEW_CASH_ADVANCE
                // and CAN_PROCESS_CASH_ADVANCE.
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    var brokerPermissionService = new BrokerPermissionService();
                    var brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_VIEW_CASH_ADVANCE);
                    if (brokerPermission == null)
                    {
                        brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_PROCESS_CASH_ADVANCE);
                        if (brokerPermission == null)
                        {
                            resultObject.RetCode = CommonEnums.RET_CODE.NOT_ALLOW;
                            return Serializer.Serialize(resultObject);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(fromDate))
                {
                    var fromDateSearch = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                    fromDate = fromDateSearch.ToString("yyyyMMdd");
                }

                // Increase 1 day to search
                if (!string.IsNullOrEmpty(toDate))
                {
                    var toDateSearch = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                    toDateSearch = toDateSearch.AddDays(1);
                    toDate = toDateSearch.ToString("yyyyMMdd");
                }

                if (!string.IsNullOrEmpty(sellDueDateFrom))
                {
                    var sellDueDateFromSearch = DateTime.ParseExact(sellDueDateFrom, "dd/MM/yyyy", null);
                    sellDueDateFrom = sellDueDateFromSearch.ToString("yyyyMMdd");
                }

                // Increase 1 day to search
                if (!string.IsNullOrEmpty(sellDueDateTo))
                {
                    var toDateSearch = DateTime.ParseExact(sellDueDateTo, "dd/MM/yyyy", null);
                    toDateSearch = toDateSearch.AddDays(1);
                    sellDueDateTo = toDateSearch.ToString("yyyyMMdd");
                }

                int count;
                var generalService = new GeneralService();
                var result = generalService.GetListCashAdvanceHistory(subAccountId, fromDate, toDate,
                                                                      contractNo, sellDueDateFrom,
                                                                      sellDueDateTo, status, tradeType,
                                                                      loginBrokerId, pageIndex, pageSize,
                                                                      out count);
                
                if ((result == null) || (result.Count == 0))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }
                else
                {
                    var pagingObject = new PagingObject<List<CashAdvanceHistory>> { Count = count, Data = result };
                    resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                    resultObject.Result = pagingObject;
                }
                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                return Serializer.Serialize(resultObject);
            }
        }
        #endregion

        #region Cash Transfer
        /// <summary>
        /// Updates the cash trans order.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="status">The status.</param>
        /// <param name="approvedAmt">The approved amt.</param>
        /// <param name="note">The note.</param>
        /// <returns>Result of updating Cash Transfer Order.</returns>
        [WebMethod(Description = "Update cash transfer", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int UpdateCashTransOrder(long id, int status, decimal approvedAmt, string note)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                   return(int) CommonEnums.RET_CODE.NOT_LOGIN;                    
                }

                //Only user with admin type can use this function and broker with right CAN_VIEW_CASH_TRANSFER
                // and CAN_PROCESS_CASH_TRANSFER
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    var brokerPermissionService = new BrokerPermissionService();
                    var brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_VIEW_CASH_TRANSFER);
                    if (brokerPermission == null)
                    {
                        brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_PROCESS_CASH_TRANSFER);
                        if (brokerPermission == null)
                        {
                            return (int) CommonEnums.RET_CODE.NOT_ALLOW;                            
                        }
                    }
                }

                var cashTransferService = new CashTransferService();
                return cashTransferService.UpdateCashTransOrder(id, status, approvedAmt, note, loginBrokerId,
                                                                DateTime.Now);
            }
            catch (Exception)
            {
                return (int) CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }
        /// <summary>
        /// Gets the list cash trans order.
        /// </summary>
        /// <param name="sourceAccountID">The source account ID.</param>
        /// <param name="destAccountID">The dest account ID.</param>
        /// <param name="transType">Type of the trans.</param>
        /// <param name="note">The note.</param>
        /// <param name="brokerID">The broker ID.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        ///<returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject$gt;List&lt;CashTransfer&gt;&gt;&gt;</see> object contains returned code, 
        /// returned message and a list of <see cref="CashTransfer">CashTransfer</see> object that contains CashTransfer information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not logged in.</para>
        /// <para>RET_CODE=NOT_ALLOW: User is not allowed to do this function.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: Data is not existing.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list paged cash tranfer today,return string serialized of ResultObject PagingObject List CashTransfer", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListCashTransOrder(string sourceAccountID, string destAccountID, int transType, string note, string brokerID, int pageIndex, int pageSize)
        {
            var resultObject = new ResultObject<PagingObject<List<CashTransfer>>> { Result = null };
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    return Serializer.Serialize(resultObject);
                }

                //Only user with admin type can use this function and broker with right CAN_VIEW_CASH_TRANSFER
                // and CAN_PROCESS_CASH_TRANSFER
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    var brokerPermissionService = new BrokerPermissionService();
                    var brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_VIEW_CASH_TRANSFER);
                    if (brokerPermission == null)
                    {
                        brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_PROCESS_CASH_TRANSFER);
                        if (brokerPermission == null)
                        {
                            resultObject.RetCode = CommonEnums.RET_CODE.NOT_ALLOW;
                            return Serializer.Serialize(resultObject);
                        }
                    }
                }
             

                var cashTransferService = new CashTransferService();
                var result = cashTransferService.GetListCashTransOrder(sourceAccountID, destAccountID, transType, note, brokerID,
                                                                          pageIndex, pageSize);
                if (result != null)
                {
                    return Serializer.Serialize(new ResultObject<PagingObject<List<CashTransfer>>>
                    {
                        Result = result,
                        ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                        RetCode = CommonEnums.RET_CODE.SUCCESS
                    });
                }
                return Serializer.Serialize(new ResultObject<PagingObject<List<CashTransfer>>>
                                                {
                                                    Result = null,
                                                    ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                                                    RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                                                });
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                return Serializer.Serialize(resultObject);
            }
        }

        /// <summary>
        /// Gets the cash transfer.
        /// </summary>
        /// <param name="cashTransferId">The cash transfer id.</param>
        /// <returns></returns>
        [WebMethod(Description = "Get list paged cash tranfer today,return string serialized of ResultObject PagingObject List CashTransfer", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetCashTransfer(int cashTransferId)
        {
            var resultObject = new ResultObject<CashTransfer> { Result = null };
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    return Serializer.Serialize(resultObject);
                }

                //Only user with admin type can use this function and broker with right CAN_VIEW_CASH_TRANSFER
                // and CAN_PROCESS_CASH_TRANSFER
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    var brokerPermissionService = new BrokerPermissionService();
                    var brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_VIEW_CASH_TRANSFER);
                    if (brokerPermission == null)
                    {
                        brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_PROCESS_CASH_TRANSFER);
                        if (brokerPermission == null)
                        {
                            resultObject.RetCode = CommonEnums.RET_CODE.NOT_ALLOW;
                            return Serializer.Serialize(resultObject);
                        }
                    }
                }


                var cashTransferService = new CashTransferService();
                var result = cashTransferService.GetCashTransfer(cashTransferId);
                if (result != null)
                {
                    return Serializer.Serialize(new ResultObject<CashTransfer>
                    {
                        Result = result,
                        ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                        RetCode = CommonEnums.RET_CODE.SUCCESS
                    });
                }
                return Serializer.Serialize(new ResultObject<CashTransfer>
                {
                    Result = null,
                    ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                    RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                });
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                return Serializer.Serialize(resultObject);
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
        ///<returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject$gt;List&lt;CashTransfer&gt;&gt;&gt;</see> object contains returned code, 
        /// returned message and a list of <see cref="CashTransfer">CashTransfer</see> object that contains CashTransfer information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not logged in.</para>
        /// <para>RET_CODE=NOT_ALLOW: User is not allowed to do this function.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: Data is not existing.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list paged cash tranfer history,return string serialized of ResultObject PagingObject List CashTransfer", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListCashTransOrderHist(string sourceAccountID,string destAccountID,string fromDate,string toDate,int transType,int status,string note,string brokerID,int pageIndex,int pageSize)
        {
            var resultObject= new ResultObject<PagingObject<List<CashTransfer>>> { Result = null };
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    return Serializer.Serialize(resultObject);
                }

                //Only user with admin type can use this function and broker with right CAN_VIEW_CASH_TRANSFER
                // and CAN_PROCESS_CASH_TRANSFER
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    var brokerPermissionService = new BrokerPermissionService();
                    var brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_VIEW_CASH_TRANSFER);
                    if (brokerPermission == null)
                    {
                        brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_PROCESS_CASH_TRANSFER);
                        if (brokerPermission == null)
                        {
                            resultObject.RetCode = CommonEnums.RET_CODE.NOT_ALLOW;
                            return Serializer.Serialize(resultObject);
                        }
                    }
                }

                // Increase 1 day to search
                if (!string.IsNullOrEmpty(toDate))
                {
                    var toDateSearch = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                    toDateSearch = toDateSearch.AddDays(1);
                    toDate = toDateSearch.ToString("dd/MM/yyyy");
                }

                var cashTransferService=new CashTransferService();
                var result = cashTransferService.GetListCashTransOrderHist(sourceAccountID, destAccountID, fromDate,
                                                                          toDate, transType, status, note, brokerID,
                                                                          pageIndex, pageSize);
                if (result != null)
                {
                    return Serializer.Serialize(new ResultObject<PagingObject<List<CashTransfer>>>
                                                    {
                                                        Result = result,
                                                        ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                                                        RetCode = CommonEnums.RET_CODE.SUCCESS
                                                    });
                }
                return Serializer.Serialize(new ResultObject<PagingObject<List<CashTransfer>>>
                                                {
                                                    Result = null,
                                                    ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                                                    RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                                                });
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                return Serializer.Serialize(resultObject);
            }
        }

        #endregion

        #region Stock Transfer
        /// <summary>
        /// Updates the stock trans order.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="status">The status.</param>
        /// <param name="approvedAmt">The approved amt.</param>
        /// <param name="note">The note.</param>
        /// <returns>Result of updating stock transfer order.</returns>
        [WebMethod(Description = "Update  the stock tranfer request ,return ret code", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int UpdateStockTransOrder(long id, int status, long approvedAmt, string note)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }

                var stockTransferService=new StockTransferService();
                return stockTransferService.UpdateStockTransOrder(id, status, approvedAmt, note, loginBrokerId,
                                                                  DateTime.Now);
            }
            catch (Exception)
            {
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }
        /// <summary>
        /// Gets the list stock trans order.
        /// </summary>
        /// <param name="sourceAccountID">The source account ID.</param>
        /// <param name="destAccountID">The dest account ID.</param>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <param name="transType">Type of the trans.</param>
        /// <param name="note">The note.</param>
        /// <param name="brokerID">The broker ID.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;StockTransfer&gt;&gt;&gt;</see> object contains returned code, 
        /// returned message and a list of <see cref="StockTransfer">StockTransfer</see> objects that contains stock transfer information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NOT_ALLOW: User has no right to do this function.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list paged stock tranfer today,return string serialized of ResultObject PagingObject List StockTransfer", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListStockTransOrder(string sourceAccountID, string destAccountID, string secSymbol, int transType, string note, string brokerID, int pageIndex, int pageSize)
        {
            var resultObject = new ResultObject<PagingObject<List<StockTransfer>>> { Result = null };
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    return Serializer.Serialize(resultObject);
                }

                //Only user with admin type can use this function and broker with right CAN_VIEW_STOCK_TRANSFER
                // and CAN_PROCESS_STOCK_TRANSFER
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    var brokerPermissionService = new BrokerPermissionService();
                    var brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_VIEW_STOCK_TRANSFER);
                    if (brokerPermission == null)
                    {
                        brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_PROCESS_STOCK_TRANSFER);
                        if (brokerPermission == null)
                        {
                            resultObject.RetCode = CommonEnums.RET_CODE.NOT_ALLOW;
                            return Serializer.Serialize(resultObject);
                        }
                    }
                }

                var stockTransferService = new StockTransferService();
                var listStockTransferOrderHist = stockTransferService.GetListStockTransOrder(sourceAccountID,
                                                                                             destAccountID, secSymbol,
                                                                                             transType, note, brokerID,
                                                                                             pageIndex, pageSize);
                if (listStockTransferOrderHist != null)
                {
                    return Serializer.Serialize(new ResultObject<PagingObject<List<StockTransfer>>>
                    {
                        Result = listStockTransferOrderHist,
                        ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                        RetCode = CommonEnums.RET_CODE.SUCCESS
                    });
                }
                return Serializer.Serialize(new ResultObject<PagingObject<List<StockTransfer>>>
                                                {
                                                    Result = null,
                                                    ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                                                    RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                                                });
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                return Serializer.Serialize(resultObject);
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
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;StockTransfer&gt;&gt;&gt;</see> object contains returned code, 
        /// returned message and a list of <see cref="StockTransfer">StockTransfer</see> objects that contains stock transfer information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NOT_ALLOW: User has no right to do this function.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list paged stock tranfer history,return string serialized of ResultObject PagingObject List StockTransfer", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListStockTransOrderHist(string sourceAccountID,string destAccountID,string secSymbol,string fromDate,string toDate,int transType,int status,string note,string brokerID,int pageIndex,int pageSize)
        {
            var resultObject = new ResultObject<PagingObject<List<StockTransfer>>> { Result = null };
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    return Serializer.Serialize(resultObject);
                }

                //Only user with admin type can use this function and broker with right CAN_VIEW_STOCK_TRANSFER
                // and CAN_PROCESS_STOCK_TRANSFER
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    var brokerPermissionService = new BrokerPermissionService();
                    var brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_VIEW_STOCK_TRANSFER);
                    if (brokerPermission == null)
                    {
                        brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_PROCESS_STOCK_TRANSFER);
                        if (brokerPermission == null)
                        {
                            resultObject.RetCode = CommonEnums.RET_CODE.NOT_ALLOW;
                            return Serializer.Serialize(resultObject);
                        }
                    }
                }

                // Increase 1 day to search
                if (!string.IsNullOrEmpty(toDate))
                {
                    var toDateSearch = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                    toDateSearch = toDateSearch.AddDays(1);
                    toDate = toDateSearch.ToString("dd/MM/yyyy");
                }

                var stockTransferService=new StockTransferService();
                var listStockTransferOrderHist = stockTransferService.GetListStockTransOrderHist(sourceAccountID,
                                                                                                 destAccountID,
                                                                                                 secSymbol, fromDate,
                                                                                                 toDate, transType,
                                                                                                 status, note, brokerID,
                                                                                                 pageIndex, pageSize);
                if(listStockTransferOrderHist!=null)
                {
                    return Serializer.Serialize(new ResultObject<PagingObject<List<StockTransfer>>>
                                                    {
                                                        Result = listStockTransferOrderHist,
                                                        ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                                                        RetCode = CommonEnums.RET_CODE.SUCCESS
                                                    });
                }
                return Serializer.Serialize(new ResultObject<PagingObject<List<StockTransfer>>>
                                                {
                                                    Result = null,
                                                    ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                                                    RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                                                });
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                return Serializer.Serialize(resultObject);
            }
        }
        #endregion

        #region Odd lot order
        /// <summary>
        /// Updates the odd lot order.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="execPrice">The exec price.</param>
        /// <param name="execVol">The exec vol.</param>
        /// <param name="canceledVol">The canceled vol.</param>
        /// <param name="status">The status.</param>
        /// <param name="brokerID">The broker ID.</param>
        /// <param name="execTime">The exec time.</param>
        /// <param name="note">The note.</param>
        /// <returns>
        /// <para>Result of updating odd lot order.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NOT_ALLOW: User has no right to do this function.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: Data don't exist.</para>
        /// <para>RET_CODE=ERROR_CANNOT_CANCEL_ODD_LOT_ORDER: Cannot cancel the order.</para>
        /// <para>RET_CODE=SUCCESS: Updated successfully.</para>
        /// <para>RET_CODE=FAIL: Failed to update data.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Update a odd lot order ,return ret code", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int UpdateOddLotOrder(long id,decimal execPrice,long execVol,long canceledVol,int status,string brokerID,DateTime execTime,string note)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int) CommonEnums.RET_CODE.NOT_LOGIN;                     
                }

                //Only user with admin type can use this function and broker with right CAN_VIEW_ODD_LOT_ORDER
                // and CAN_PROCESS_ODD_LOT_ORDER
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    var brokerPermissionService = new BrokerPermissionService();
                    var brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_VIEW_ODD_LOT_ORDER);
                    if (brokerPermission == null)
                    {
                        brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_PROCESS_ODD_LOT_ORDER);
                        if (brokerPermission == null)
                        {
                            return (int)CommonEnums.RET_CODE.NOT_ALLOW;                            
                        }
                    }
                }

                var oddLotOrderService=new OddLotOrderService();
                return oddLotOrderService.UpdateOddLotOrder(id, execPrice, execVol, canceledVol, status, brokerID, note);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;                 
            }
        }

        /// <summary>
        /// Gets the list odd lot order.
        /// </summary>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <param name="side">The side.</param>
        /// <param name="subCustAccountID">The sub cust account ID.</param>
        /// <param name="market">The market.</param>
        /// <param name="brokerID">The broker ID.</param>
        /// <param name="note">The note.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;OddLotOrder&gt;&gt;&gt;</see> object contains returned code, 
        /// returned message and a list of <see cref="OddLotOrder">OddLotOrder</see> objects that contains OddLotOrder information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NOT_ALLOW: User has no right to do this function.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list paged odd lot order today,return string serialized of ResultObject PagingObject List StockTransfer", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListOddLotOrder(string secSymbol, string side, string subCustAccountID, string market, string brokerID, string note, int pageIndex, int pageSize)
        {
            var resultObject = new ResultObject<PagingObject<List<OddLotOrder>>> { Result = null };
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    return Serializer.Serialize(resultObject);
                }

                //Only user with admin type can use this function and broker with right CAN_VIEW_ODD_LOT_ORDER
                // and CAN_PROCESS_ODD_LOT_ORDER
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    var brokerPermissionService = new BrokerPermissionService();
                    var brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_VIEW_ODD_LOT_ORDER);
                    if (brokerPermission == null)
                    {
                        brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_PROCESS_ODD_LOT_ORDER);
                        if (brokerPermission == null)
                        {
                            resultObject.RetCode = CommonEnums.RET_CODE.NOT_ALLOW;
                            return Serializer.Serialize(resultObject);
                        }
                    }
                }               

                var oddLotOrderService = new OddLotOrderService();
                var listOddLotOrder = oddLotOrderService.GetListOddLotOrder(secSymbol, side, subCustAccountID, market,
                                                                            brokerID, note, pageIndex, pageSize);

                if (listOddLotOrder != null && listOddLotOrder.Data != null && listOddLotOrder.Data.Count > 0)
                {
                    return Serializer.Serialize(new ResultObject<PagingObject<List<OddLotOrder>>>
                    {
                        Result = listOddLotOrder,
                        ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                        RetCode = CommonEnums.RET_CODE.SUCCESS
                    });
                }
                return Serializer.Serialize(new ResultObject<PagingObject<List<OddLotOrder>>>
                                                {
                                                    Result = null,
                                                    ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                                                    RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                                                });
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                return Serializer.Serialize(resultObject);
            }
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
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;OddLotOrder&gt;&gt;&gt;</see> object contains returned code, 
        /// returned message and a list of <see cref="OddLotOrder">OddLotOrder</see> objects that contains OddLotOrder information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NOT_ALLOW: User has no right to do this function.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list paged odd lot order history,return string serialized of ResultObject PagingObject List StockTransfer", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListOddLotOrderHist(string secSymbol,string side,string fromDate,string toDate,string subCustAccountID,string market,int status,string brokerID,string note,int pageIndex,int pageSize)
        {
            var resultObject = new ResultObject<PagingObject<List<OddLotOrder>>>{Result = null};
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    return Serializer.Serialize(resultObject);
                }

                //Only user with admin type can use this function and broker with right CAN_VIEW_ODD_LOT_ORDER
                // and CAN_PROCESS_ODD_LOT_ORDER
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    var brokerPermissionService = new BrokerPermissionService();
                    var brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_VIEW_ODD_LOT_ORDER);
                    if (brokerPermission == null)
                    {
                        brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_PROCESS_ODD_LOT_ORDER);
                        if (brokerPermission == null)
                        {
                            resultObject.RetCode = CommonEnums.RET_CODE.NOT_ALLOW;
                            return Serializer.Serialize(resultObject);
                        }
                    }
                }

                // Increase 1 day to search
                if (!string.IsNullOrEmpty(toDate))
                {
                    var toDateSearch = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                    toDateSearch = toDateSearch.AddDays(1);
                    toDate = toDateSearch.ToString("dd/MM/yyyy");
                }

                var oddLotOrderService=new OddLotOrderService();
                var listOddLotOrder = oddLotOrderService.GetListOddLotOrderHist(secSymbol, side, fromDate, toDate,
                                                                                       subCustAccountID, market, status,
                                                                                       brokerID, note, pageIndex,
                                                                                       pageSize);
                if (listOddLotOrder != null && listOddLotOrder.Data != null && listOddLotOrder.Data.Count>0)
                {
                    return Serializer.Serialize(new ResultObject<PagingObject<List<OddLotOrder>>>
                                                    {
                                                        Result = listOddLotOrder,
                                                        ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                                                        RetCode = CommonEnums.RET_CODE.SUCCESS
                                                    });
                }
                return Serializer.Serialize(new ResultObject<PagingObject<List<OddLotOrder>>>
                                                {
                                                    Result = null,
                                                    ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                                                    RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                                                });
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                return Serializer.Serialize(resultObject);
            }
            
        }
        #endregion

        #region XR Order

        /// <summary>
        /// Gets the list XR order.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="subAccountId">The sub account id.</param>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <param name="market">The market.</param>
        /// <param name="brokerID">The broker ID.</param>
        /// <param name="note">The note.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;XrOrders&gt;&gt;&gt;</see> object contains returned code, 
        /// returned message and a list of <see cref="XrOrders">XrOrders</see> objects that contains XrOrder information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NOT_ALLOW: User has no right to do this function.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list paged list xr order to day, return  string serialized of ResultObject PagingObject List XROrder", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListXROrder(long id, string subAccountId, string secSymbol, string market, string brokerID, string note, int pageIndex, int pageSize)
        {
            var resultObject = new ResultObject<PagingObject<List<XrOrders>>> { Result = null };
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    return Serializer.Serialize(resultObject);
                }

                //Only user with admin type can use this function and broker with right CAN_VIEW_XRORDER
                // and CAN_PROCESS_XRORDER
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    var brokerPermissionService = new BrokerPermissionService();
                    var brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_VIEW_XRORDER);
                    if (brokerPermission == null)
                    {
                        brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_VIEW_XRORDER);
                        if (brokerPermission == null)
                        {
                            resultObject.RetCode = CommonEnums.RET_CODE.NOT_ALLOW;
                            return Serializer.Serialize(resultObject);
                        }
                    }
                }

                var xrOrdersService = new XrOrdersService();
                var result = xrOrdersService.GetListXROrder(id, subAccountId, secSymbol, market, brokerID, note,
                                                            pageIndex, pageSize);
                if (result != null)
                {
                    return Serializer.Serialize(new ResultObject<PagingObject<List<XrOrders>>>
                    {
                        Result = result,
                        ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                        RetCode = CommonEnums.RET_CODE.SUCCESS
                    });
                }
                return Serializer.Serialize(new ResultObject<PagingObject<List<XrOrders>>>
                                                {
                                                    Result = null,
                                                    ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                                                    RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                                                });
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                return Serializer.Serialize(resultObject);
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
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;XrOrders&gt;&gt;&gt;</see> object contains returned code, 
        /// returned message and a list of <see cref="XrOrders">XrOrders</see> objects that contains XrOrder information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NOT_ALLOW: User has no right to do this function.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list paged list xr order history, return  string serialized of ResultObject PagingObject List XROrder", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListXROrderHist(long id,string subAccountId,string secSymbol,string market,string fromDate,string toDate,int status,string brokerID,string note,int pageIndex,int pageSize)
        {
            var resultObject = new ResultObject<PagingObject<List<XrOrders>>> { Result = null };
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    return Serializer.Serialize(resultObject);
                }

                //Only user with admin type can use this function and broker with right CAN_VIEW_XRORDER
                // and CAN_PROCESS_XRORDER
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    var brokerPermissionService = new BrokerPermissionService();
                    var brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_VIEW_XRORDER);
                    if (brokerPermission == null)
                    {
                        brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_VIEW_XRORDER);
                        if (brokerPermission == null)
                        {
                            resultObject.RetCode = CommonEnums.RET_CODE.NOT_ALLOW;
                            return Serializer.Serialize(resultObject);
                        }
                    }
                }

                var xrOrdersService=new XrOrdersService();
                var result = xrOrdersService.GetListXROrderHist(id, subAccountId, secSymbol, market, fromDate, toDate,
                                                                status, brokerID, note, pageIndex, pageSize);
                if (result != null)
                {
                    return Serializer.Serialize(new ResultObject<PagingObject<List<XrOrders>>>
                                                    {
                                                        Result = result,
                                                        ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                                                        RetCode = CommonEnums.RET_CODE.SUCCESS
                                                    });
                }
                return Serializer.Serialize(new ResultObject<PagingObject<List<XrOrders>>>
                                                {
                                                    Result = null,
                                                    ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                                                    RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                                                });
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                return Serializer.Serialize(resultObject);
            }
        }
        /// <summary>
        /// Updates the XR order.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="approvedVol">The approved vol.</param>
        /// <param name="status">The status.</param>
        /// <param name="note">The note.</param>
        /// <returns>
        /// <para>Result of updating XR order.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NOT_ALLOW: User has no right to do this function.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: Data don't exist.</para>
        /// <para>RET_CODE=ERROR_CANNOT_CANCEL_BUY_RIGHT: Cannot cancel the order.</para>
        /// <para>RET_CODE=SUCCESS: Updated successfully.</para>
        /// <para>RET_CODE=FAIL: Failed to update data.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Update a xrOrder, return retcode", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int UpdateXROrder(long id,long approvedVol,int status,string note)
        {
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    return (int) CommonEnums.RET_CODE.NOT_LOGIN;                    
                }

                //Only user with admin type can use this function and broker with right CAN_VIEW_XRORDER
                // and CAN_PROCESS_XRORDER
                var brokerType = (short)Session[Constants.BROKER_TYPE];
                if (brokerType != (int)CommonEnums.BROKER_TYPE.ADMIN)
                {
                    var brokerPermissionService = new BrokerPermissionService();
                    var brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_VIEW_XRORDER);
                    if (brokerPermission == null)
                    {
                        brokerPermission = brokerPermissionService.GetByBrokerIdPermissionId(loginBrokerId,
                                                            (int)CommonEnums.BROKER_PERMISSIONS.CAN_VIEW_XRORDER);
                        if (brokerPermission == null)
                        {
                            return (int) CommonEnums.RET_CODE.NOT_ALLOW;                            
                        }
                    }
                }
                var xrOrdersService=new XrOrdersService();
                return xrOrdersService.UpdateXROrder(id, approvedVol, status, loginBrokerId, DateTime.Now, note);
            }
            catch (Exception)
            {
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }
        #endregion
        #endregion

        #region Get exchange rate
        /// <summary>
        /// Gets the exchange rate USD.
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "Get rate exchange USD", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public decimal GetExchangeRateUSD()
        {
           var etradeService = new EtradeService.ETradeServicesWebServices();
            return etradeService.GetExchangeRateUSD();
        }
        #endregion

        #region SMS Management
        /// <summary>
        /// Count total SMS
        /// </summary>
        /// <param name="fromDate">From date to search. Format dd/MM/yyyy</param>
        /// <param name="toDate">To date to search.Format dd/MM/yyyy</param>
        /// <returns>Total of messages.</returns>
        [WebMethod(Description = "Count total SMS", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CountTotalSMS(string fromDate, string toDate)
        {
            var resultObject = new ResultObject<long>();
            try
            {
                var loginBrokerId = (string)Session[Constants.BROKER];
                if (string.IsNullOrEmpty(loginBrokerId))
                {
                    resultObject.Result = 0;
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                }
                if (fromDate == null)
                {
                    fromDate = string.Empty;
                }
                if (toDate == null)
                {
                    toDate = string.Empty;
                }
                var total = SmsCountService.CountSMS(fromDate, toDate);
                resultObject.Result = total;
                resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.Result = 0;
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
            return Serializer.Serialize(resultObject);
        }
        #endregion

        #region Language
        /// <summary>
        /// Gets the list language.
        /// </summary>
        /// <param name="languageId">The language id.</param>
        /// <param name="languageName">Name of the language.</param>
        /// <returns></returns>
        [WebMethod(Description = "Get list of language information", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListLanguage(string languageId, string languageName)
        {
            try
            {
                var resultObject = new ResultObject<List<Language>>();
                var languageService = new LanguageService();
                List<Language> listLanguage = languageService.GetList(languageId, languageName);
                if (listLanguage != null)
                    resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                else
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }
                resultObject.Result = listLanguage;
                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                var resultObject = new ResultObject<PagingObject<List<MainCustAccount>>>
                {
                    RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                    Result = null
                };
                return Serializer.Serialize(resultObject);
            }
        }

        /// <summary>
        /// Gets the language.
        /// </summary>
        /// <param name="languageId">The language id.</param>
        /// <returns></returns>
        [WebMethod(Description = "Get only information of language", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetLanguage(string languageId)
        {
            try
            {
                var resultObject = new ResultObject<Language>();

                var languageService = new LanguageService();
                var language = languageService.GetLanguage(languageId);
                if (language != null)
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                }
                else
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }
                resultObject.Result = language;
                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                var resultObject = new ResultObject<Language>
                {
                    RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                    Result = null
                };
                return Serializer.Serialize(resultObject);
            }

        }
        #endregion
    }
}