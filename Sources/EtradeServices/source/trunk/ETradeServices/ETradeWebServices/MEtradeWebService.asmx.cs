// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ETradeServicesWebServices.asmx.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Summary description for ETradeServicesWebServices
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics;
using System.Linq;
using System.Web.Configuration;
using System.Xml.Linq;
using ETradeGWServices;
using ETradeHistory.Entities;
using ETradeHistory.Services;
using ETradeOrders.Services;
using ETradeWebServices.Services;
using ETradeWebServices.Utils;
using System.Data.SqlClient;

namespace ETradeWebServices
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Caching;
    using System.Web.Script.Serialization;
    using System.Web.Script.Services;
    using System.Web.Services;
    using ETradeCommon;
    using ETradeCommon.Enums;
    using ETradeFinance.Entities;
    using ETradeCore.Entities;
    using ETradeOrders.Entities;
    using AMServices;

    using CashAdvance = ETradeCore.Entities.CashAdvance;
    using SubCustAccount = AccountManager.Entities.SubCustAccount;
    using AccountManager.Entities;
    /// <summary>
    /// Summary description for ETradeServicesWebServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class MEtradeWebService : System.Web.Services.WebService
    {

        private static readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();

        /// <summary>
        /// Exception policy
        /// </summary>
        public const string WEB_SERVICE_POLICY = "WebServiceExceptionPolicy";

        /// <summary>
        /// </summary>
        private static readonly AccountManagerServices AccountManagerServices = new AccountManagerServices();

        private static readonly ETradeWebServices.Services.ETradeServices ETradeServices = new ETradeWebServices.Services.ETradeServices();
        private static readonly ValidateServices ValidateServices = new ValidateServices();
        private static readonly ConditionOrderService ConditionOrderService = new ConditionOrderService();
        private static readonly ConditionOrderDetailService ConditionOrderDetailService = new ConditionOrderDetailService();
        private static readonly RTServices.Service rtDataService = new RTServices.Service();
        #region account manager

        /// <summary>
        /// Logins the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// 	<para>A <see cref="ResultObject{T}">ResultObject&lt;MainCustAccount&gt;</see> object contains returned code, returned message and
        /// main customer account information.</para>
        /// 	<para>RET_CODE=SUCCESS: Log in successfully.</para>
        /// 	<para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Logon for investor, authType = 0: pin/pass, 1: RSA, 2: TODOS, 3: ENTRUST return ResultObject<MainCustAccount>",
            EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Login(string username, string password)
        {
            int result;
            try
            {
                result =
                int.TryParse(
                    AccountManagerServices.AuthenticateCustLogon(username, password).ToString(),
                    out result)
                    ? result
                    : 0;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<Entities.MMainCustAccount>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }

            if (result != (int)CommonEnums.RET_CODE.SUCCESS)
            {
                return
                    Serializer.Serialize(
                        new ResultObject<Entities.MMainCustAccount>
                        {
                            Result = null,
                            RetCode = (CommonEnums.RET_CODE)result,
                            ErrorMessage = ((CommonEnums.RET_CODE)result).ToString()
                        });
            }

            string retVal = AccountManagerServices.GetCustomerNoSession(username);
            var mainCustAccount = Serializer.Deserialize<ResultObject<MainCustAccount>>(
                retVal);

            Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] = username;
            Session[CommonEnums.SESSION_KEY.PASSWORD.ToString()] = mainCustAccount.Result.Password;
            Session[CommonEnums.SESSION_KEY.PIN.ToString()] = mainCustAccount.Result.Pin;
            Session[CommonEnums.SESSION_KEY.CUSTOMER_TYPE.ToString()] = mainCustAccount.Result.CustomerType;
            string apiKey = Guid.NewGuid().ToString();
            Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] = apiKey;

            var subCustAccountIdList = new List<string>();
            var subCustAccountCollection = mainCustAccount.Result.SubCustAccountCollection;

            foreach (SubCustAccount subCustAccount in subCustAccountCollection)
            {
                Session[subCustAccount.SubCustAccountId + CommonEnums.SESSION_KEY.TRADING_ACCOUNT] = subCustAccount;
                subCustAccountIdList.Add(subCustAccount.SubCustAccountId);
            }
            Session[CommonEnums.SESSION_KEY.LIST_SUB_ACCOUNTS.ToString()] = subCustAccountIdList;

            // Put sessionId into cache to compare user session later
            string cacheKey = username;
            var cacheTimeout = new TimeSpan(0, 0, HttpContext.Current.Session.Timeout, 0, 0);
            if (HttpContext.Current.Cache[cacheKey] != null)
            {
                HttpContext.Current.Cache.Remove(cacheKey);
            }

            HttpContext.Current.Cache.Insert(
                cacheKey, Session.SessionID, null, Cache.NoAbsoluteExpiration, cacheTimeout);
            //set value for return val
            var returnVal = new Entities.MMainCustAccount();
            returnVal.RetCode = mainCustAccount.RetCode;
            returnVal.ErrorMessage = mainCustAccount.ErrorMessage;
            if (returnVal.RetCode == CommonEnums.RET_CODE.SUCCESS)
            {
                returnVal.MainCustAccountId = mainCustAccount.Result.MainCustAccountId;
                returnVal.Phone = mainCustAccount.Result.Phone;
                returnVal.Email = mainCustAccount.Result.Email;
                returnVal.SubCustAccountCollection = mainCustAccount.Result.SubCustAccountCollection.ToList();
                returnVal.IsNewPin = mainCustAccount.Result.PinIsNew;
                returnVal.ApiKey = apiKey;
            }
            return Serializer.Serialize(returnVal);
        }
        /// <summary>
        /// Logouts the specified session id.
        /// </summary>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;CommonEnums.RET_CODE&gt;</see> object contains returned code, returned message and 
        /// result of logging out.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=SUCCESS: Log out successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Logout for investor, return ResultObject<CommonEnums.RET_CODE>", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Logout(string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<CommonEnums.RET_CODE>
                            {
                                Result = CommonEnums.RET_CODE.NOT_LOGIN,
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                else if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<CommonEnums.RET_CODE>
                            {
                                Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                                RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                                ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                            });
                }

                var resultObject = new ResultObject<CommonEnums.RET_CODE>
                {
                    RetCode = CommonEnums.RET_CODE.SUCCESS,
                    Result = CommonEnums.RET_CODE.SUCCESS,
                    ErrorMessage =
                        CommonEnums.RET_CODE.SUCCESS.ToString()
                };

                Session.Abandon();
                string accountNo = Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()].ToString();
                AccountManagerServices.CustomerLogout(accountNo);
                if (HttpContext.Current.Cache[accountNo] != null)
                {
                    HttpContext.Current.Cache.Remove(accountNo);
                }
                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<CommonEnums.RET_CODE>
                            {
                                Result = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }
        ///<summary>
        /// Get list of online customer
        ///</summary>
        ///<returns></returns>
        [WebMethod(Description = "Get list of online customers")]
        public string GetListOnlineCustomers(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey) || Session == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
            {
                return
                    Serializer.Serialize(
                        new ResultObject<CommonEnums.RET_CODE>
                        {
                            Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                        });
            }
            var customerList = new List<string>();
            var resultObject = new ResultObject<List<string>> { RetCode = CommonEnums.RET_CODE.SUCCESS };
            try
            {
                if ((HttpContext.Current != null) && (HttpContext.Current.Cache != null))
                {
                    var enumerator = HttpContext.Current.Cache.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        var username = (string)enumerator.Key;
                        if (username.Length == 10)
                        {
                            customerList.Add((string)enumerator.Key);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
            }
            resultObject.Result = customerList;
            return Serializer.Serialize(resultObject);
        }
        /// <summary>
        /// Gets the account information.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;MainCustAccount&gt;</see> object contains returned code, returned message and 
        /// MainCustAccount object that contains main customer information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=SUCCESS: Get account successfully.</para>
        /// <para>RET_CODE=FAIL: Failed to get account.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get account information, return ResultObject<MainCustAccount>", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetAccountInfo(string accountId, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<AccountManager.Entities.MainCustAccount>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                else if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<CommonEnums.RET_CODE>
                            {
                                Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                                RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                                ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                            });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(accountId, string.Empty))
                {
                    return
                    Serializer.Serialize(
                        new ResultObject<MainCustAccount>
                        {
                            Result = null,
                            RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                            ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                        });
                }

                string returnVal = AccountManagerServices.GetCustomerNoSession(accountId);

                return returnVal;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<AccountManager.Entities.MainCustAccount>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }
        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="mainCustAccountId">The main cust account id.</param>
        /// <param name="identifyNumber">The identify number.</param>
        /// <param name="messagePhone">The message phone.</param>
        /// <returns></returns>
        [WebMethod(Description = "Get account information, return ResultObject<MainCustAccount>", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int ForgetPassword(string mainCustAccountId, string identifyNumber, string messagePhone, string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey) || Session == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
            {
                return (int)CommonEnums.RET_CODE.INVAILID_API_KEY;
            }
            try
            {
                string result = AccountManagerServices.ForgetPassword(mainCustAccountId, identifyNumber, messagePhone);
                var resultObject = Serializer.Deserialize<ResultObject<string>>(result);
                if (resultObject != null)
                {
                    return (int)resultObject.RetCode;
                }
                return (int)CommonEnums.RET_CODE.FAIL;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.FAIL;
            }
        }
        /// <summary>
        /// Request new pin.
        /// </summary>
        /// <param name="mainCustAccountId">Main customer account id.</param>
        /// <returns>
        /// <para>RET_CODE=INCORECT_INFORMATION: Wrong some information.</para>
        /// <para>RET_CODE=SUCCESS: Generate pin successfully.</para>
        ///  <para>RET_CODE=NO_EXIST_DATA: wrong main cust account id</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// <para>RET_CODE=FAIL: Action fail.</para>
        /// <para>RET_CODE=ERROR_SENT_MESSAGE: Send message fail.</para>
        /// </returns>
        [WebMethod(Description = "Send message to customer", EnableSession = true)]
        public int ForgetPin(string mainCustAccountId, string apiKey)
        {
            if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
            {
                return (int)CommonEnums.RET_CODE.NOT_LOGIN;
            }
            else if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
            {
                return (int)CommonEnums.RET_CODE.INVAILID_API_KEY;
            }
            //Check multiple account by an customer
            if (IsMultiAccount(mainCustAccountId, string.Empty))
            {
                return (int)CommonEnums.RET_CODE.NOT_LOGIN;
            }
            string result = AccountManagerServices.ForgetPin(mainCustAccountId);
            var resultObject = Serializer.Deserialize<ResultObject<string>>(result);
            if (resultObject != null)
            {
                if (resultObject.RetCode == CommonEnums.RET_CODE.SUCCESS)
                {
                    Session[CommonEnums.SESSION_KEY.PIN.ToString()] = resultObject.Result; // Update pin to session                    
                }
                return (int)resultObject.RetCode;
            }
            return (int)CommonEnums.RET_CODE.FAIL;
        }
        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="mainCustAccId">The main customer account id.</param>
        /// <param name="oldPass">The old pass.</param>
        /// <param name="newPass">The new pass.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject</see> object includes returned code, returned message, 
        /// and <see cref="CommonEnums.RET_CODE"/> information that contains result of changing password.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=SUCCESS: Changing password successfully.</para>
        /// <para>RET_CODE=FAILED: Fail to change password successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Change password for investor", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ChangePassword(string mainCustAccId, string oldPass, string newPass, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<int>
                            {
                                Result = (int)CommonEnums.RET_CODE.NOT_LOGIN,
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                else if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    Serializer.Serialize(
                           new ResultObject<int>
                           {
                               Result = (int)CommonEnums.RET_CODE.INVAILID_API_KEY,
                               RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                               ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                           });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(mainCustAccId, string.Empty))
                {
                    return Serializer.Serialize(new ResultObject<int>
                    {
                        Result = (int)CommonEnums.RET_CODE.NOT_LOGIN,
                        RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                        ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                    });
                }

                int result = AccountManagerServices.ChangeCustPassword(mainCustAccId, oldPass, newPass);

                var resultObject = new ResultObject<int> { RetCode = (CommonEnums.RET_CODE)result, Result = result };

                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<int>
                            {
                                Result = -1,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }
        /// <summary>
        /// Chang pin of an investor
        /// </summary>
        /// <param name="accountId">
        /// The account id.
        /// </param>
        /// <param name="oldPin">
        /// The old pin.
        /// </param>
        /// <param name="newPin">
        /// The new pin.
        /// </param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject</see> object includes returned code, returned message, 
        /// and <see cref="CommonEnums.RET_CODE"/> information that contains result of changing pin.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=SUCCESS: Changing pin successfully.</para>
        /// <para>RET_CODE=FAILED: Fail to change pin successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Change pin for investor", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ChangePin(string accountId, string oldPin, string newPin, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<int>
                            {
                                Result = (int)CommonEnums.RET_CODE.NOT_LOGIN,
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                else if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    Serializer.Serialize(
                           new ResultObject<int>
                           {
                               Result = (int)CommonEnums.RET_CODE.INVAILID_API_KEY,
                               RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                               ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                           });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(accountId, string.Empty))
                {
                    return Serializer.Serialize(new ResultObject<int>
                    {
                        Result = (int)CommonEnums.RET_CODE.NOT_LOGIN,
                        RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                        ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                    });
                }

                int result = AccountManagerServices.ChangeCustPin(accountId, oldPin, newPin);
                if (result == (int)CommonEnums.RET_CODE.SUCCESS)
                    Session[CommonEnums.SESSION_KEY.PIN.ToString()] = PasswordHandlerMd5.Encrypt(newPin);
                var resultObject = new ResultObject<int> { RetCode = (CommonEnums.RET_CODE)result, Result = result };

                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<int>
                            {
                                Result = -1,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }
        /// <summary>
        /// Connect to LinkOPS
        /// </summary>
        /// <returns>
        /// ResultObject of bool to show result of connection. 
        /// </returns>
        [WebMethod(Description = "Connect to LinkOPS, return a ResultObject<bool>", EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Connect(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey) || Session == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
            {
                Serializer.Serialize(
                       new ResultObject<int>
                       {
                           Result = (int)CommonEnums.RET_CODE.INVAILID_API_KEY,
                           RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                           ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                       });
            }
            try
            {
                ResultObject<bool> resultObject = ETradeServices.Connect(AppConfig.LinkOPSAddress, AppConfig.LinkOPSPort);

                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<bool>
                            {
                                Result = false,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR
                            });
            }
        }
        /// <summary>
        /// Disconnect from LinkOPS
        /// </summary>
        /// <returns>
        /// ResultObject of bool to show result of disconnection. 
        /// </returns>
        [WebMethod(Description = "Disconnect from LinkOPS, return a ResultObject<bool>", EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Disconnect(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey) || Session == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
            {
                Serializer.Serialize(
                       new ResultObject<int>
                       {
                           Result = (int)CommonEnums.RET_CODE.INVAILID_API_KEY,
                           RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                           ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                       });
            }
            try
            {
                ResultObject<bool> resultObject = ETradeServices.Disconnect();

                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<bool>
                            {
                                Result = false,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR
                            });
            }
        }
        /// <summary>
        /// Is connected to LinkOPS
        /// </summary>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;bool&gt;</see> object contains returned code, returned message and 
        /// the result of checking connection.</para>
        /// <para>RET_CODE=SUCCESS: Checking successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Is connected to LinkOPS, return a ResultObject<bool>", EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string IsConnected(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey) || Session == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
            {
                Serializer.Serialize(
                       new ResultObject<int>
                       {
                           Result = (int)CommonEnums.RET_CODE.INVAILID_API_KEY,
                           RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                           ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                       });
            }
            try
            {
                ResultObject<bool> resultObject = ETradeServices.IsConnected();

                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<int>
                            {
                                Result = -1,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR
                            });
            }
        }

        #endregion account manager
        #region market
        /// <summary>
        /// Gets the state of the trading.
        /// </summary>
        /// <param name="marketId">The market id.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;char&gt;&gt;&gt;</see> object contains returned code, returned message and 
        /// trading state information. If GW not connect to LinkOPS yet, the trading status wil be 'W'</para>
        /// <para>RET_CODE=SUCCESS: Getting data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get current trading status, return objectResult of market status")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetTradingState(int marketId)
        {
            try
            {
                var tradingState = ETradeServices.GetTradingState(marketId);
                var resultObject = new ResultObject<char>
                {
                    Result = tradingState,
                    RetCode = CommonEnums.RET_CODE.SUCCESS,
                };

                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<char>
                            {
                                Result = (char)CommonEnums.MARKET_STATUS.UNVAILABLE,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }
        /// <summary>
        /// Gets all states of the trading.
        /// </summary>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;char[]&gt;</see> object contains returned code, returned message and 
        /// trading state information of all markets. If GW not connect to LinkOPS yet, the trading status wil be 'W'.
        /// Please refer to the MARKET_STATUS in CommonEnums.cs.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get current trading status, return objectResult of market status")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetAllTradingState()
        {
            var tradingState = new[]
                                      {
                                          (char) CommonEnums.MARKET_STATUS.UNVAILABLE,
                                          (char) CommonEnums.MARKET_STATUS.UNVAILABLE,
                                          (char) CommonEnums.MARKET_STATUS.UNVAILABLE
                                      };
            try
            {
                tradingState = ETradeServices.GetAllTradingState();
                var resultObject = new ResultObject<char[]>
                {
                    Result = tradingState,
                    RetCode = CommonEnums.RET_CODE.SUCCESS,
                };

                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<char[]>
                            {
                                Result = tradingState,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }
        /// <summary>
        /// Gets order session of all markets.
        /// </summary>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;char[]&gt;</see> object contains returned code, returned message and 
        /// order session of all markets.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get current order session, return objectResult of order session")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetAllOrderSession()
        {
            var orderSessions = new[]
                                      {
                                          (char) CommonEnums.ORDER_SESSION.SESSION0,
                                          (char) CommonEnums.ORDER_SESSION.SESSION0,
                                          (char) CommonEnums.ORDER_SESSION.SESSION0
                                      };
            try
            {
                orderSessions = ETradeServices.GetAllOrderSession();
                var resultObject = new ResultObject<char[]>
                {
                    Result = orderSessions,
                    RetCode = CommonEnums.RET_CODE.SUCCESS,
                };

                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<char[]>
                            {
                                Result = orderSessions,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }
        [WebMethod(Description = "Get all market status and trading state, using for mobile version",EnableSession=true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string MGetAllMarketStatusAndInfo(string apiKey,string culture)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(culture);
            if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
            {
                return Serializer.Serialize(
                        new ResultObject<int>
                        {
                            Result = -1,
                            RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                            ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                        });
            }
            else if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
            {
                return
                Serializer.Serialize(
                    new ResultObject<CommonEnums.RET_CODE>
                    {
                        Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                        RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                        ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                    });
            }
            //processing market status
            var marketStatusJson = GetAllTradingState();
            var marketStatusObj = Serializer.Deserialize<ETradeCommon.ResultObject<char[]>>(marketStatusJson);
            char[] marketStatus;
            if (marketStatusObj.RetCode == ETradeCommon.Enums.CommonEnums.RET_CODE.SUCCESS)
            {
                marketStatus = marketStatusObj.Result;
            }
            else
            {
                marketStatus = new char[]
                {
                    (char)CommonEnums.MARKET_STATUS.UNVAILABLE,
                    (char)CommonEnums.MARKET_STATUS.UNVAILABLE,
                    (char)CommonEnums.MARKET_STATUS.UNVAILABLE
                };
            }
            //end processing market status
            //processing order session
            var orderSessionJson = GetAllOrderSession();
            var orderSessionObj = Serializer.Deserialize<ETradeCommon.ResultObject<char[]>>(orderSessionJson);
            char[] orderSession;
            if (orderSessionObj.RetCode == ETradeCommon.Enums.CommonEnums.RET_CODE.SUCCESS)
            {
                orderSession = orderSessionObj.Result;
            }
            else
            {
                orderSession = new char[]
                {
                    (char) CommonEnums.ORDER_SESSION.SESSION0,
                    (char) CommonEnums.ORDER_SESSION.SESSION0,
                    (char) CommonEnums.ORDER_SESSION.SESSION0
                };
            }
            //end processing order session
            //processing market info
            var marketInfoJson = rtDataService.AllMarketInfoAndStatus();
            var marketInfobj = Serializer.Deserialize<ResultObject<List<RTDataServices.Entities.MarketInfo>>>(marketInfoJson);
            List<Entities.MMarketStatusAndInfo> returnData = new List<Entities.MMarketStatusAndInfo>();
            if (marketInfobj.RetCode == CommonEnums.RET_CODE.SUCCESS)
            {
                foreach(var data in marketInfobj.Result)
                {
                    var marketStatusAndInfo = new Entities.MMarketStatusAndInfo();
                    double value=0;
                    marketStatusAndInfo.MarketId = data.MarketId.ToString();
                    marketStatusAndInfo.MarketName = Entities.MCommon.GetMarketName(data.MarketId);
                    marketStatusAndInfo.IndexValue = data.SetIndex;
                    marketStatusAndInfo.Change = data.IndexChanged;
                    double refIndex = data.SetIndex = data.IndexChanged;
                    double perChange = (refIndex > 0) ? (data.IndexChanged / 100) : 0;
                    marketStatusAndInfo.PerChange = perChange;
                    switch ((int)data.MarketId)
                    {
                        case (int)CommonEnums.MARKET_ID.HOSE: value = (double)data.TotalValues / 10000; break;
                        case (int)CommonEnums.MARKET_ID.HNX: value = (double)data.TotalValues / 1000000000; break;
                        case (int)CommonEnums.MARKET_ID.UPCoM: value = (double)data.TotalValues / 1000000000; break;
                    }
                    marketStatusAndInfo.Value = value;
                    marketStatusAndInfo.Volume = data.TotalShares;
                    marketStatusAndInfo.VolUp = data.Advances;
                    marketStatusAndInfo.VolNoChange = data.NoChange;
                    marketStatusAndInfo.VolNoTrade = data.CountNoChange;
                    marketStatusAndInfo.VolDown = data.Declines;
                    marketStatusAndInfo.VolCeilling = data.CountCeiling;
                    marketStatusAndInfo.VolFloor = data.CountFloor;
                    marketStatusAndInfo.MarketStatusMessage = Entities.MCommon.GetMarketStatusFromOrderSession((int)data.MarketId, orderSession[(int)data.MarketId - 1]);
                    marketStatusAndInfo.MarketStatus = data.Status.ToString();
                    marketStatusAndInfo.MarketTradingStatus = marketStatus[(int)data.MarketId - 1].ToString();
                    marketStatusAndInfo.MarketOrderSession = marketStatus[(int)data.MarketId - 1].ToString();
                    marketStatusAndInfo.MarketAlert = Entities.MCommon.GetMarketAlertFromOrderSession((int)data.MarketId,orderSession[(int)data.MarketId - 1]);
                    //marketStatusAndInfo.VN30 = data.VN30;
                    returnData.Add(marketStatusAndInfo);
                }
            }
            else
            {
                double _DEFAULTMARKETVALUE = 0;
                for (int i = 1; i < 4; i++)
                {
                    var marketStatusAndInfo = new Entities.MMarketStatusAndInfo();                   
                    marketStatusAndInfo.MarketId = i.ToString();
                    marketStatusAndInfo.MarketName = Entities.MCommon.GetMarketName(i);
                    marketStatusAndInfo.IndexValue = _DEFAULTMARKETVALUE;
                    marketStatusAndInfo.Change = _DEFAULTMARKETVALUE;                  
                    marketStatusAndInfo.PerChange = _DEFAULTMARKETVALUE;
                    marketStatusAndInfo.Value = _DEFAULTMARKETVALUE;
                    marketStatusAndInfo.Volume = 0;
                    marketStatusAndInfo.VolUp = 0;
                    marketStatusAndInfo.VolNoChange = 0;
                    marketStatusAndInfo.VolDown = 0;
                    marketStatusAndInfo.VolCeilling = 0;
                    marketStatusAndInfo.VolFloor = 0;
                    marketStatusAndInfo.MarketStatusMessage = Entities.MCommon.GetMarketStatus(i, (char)ETradeCommon.Enums.CommonEnums.MARKET_STATUS.CLOSE_PT);
                    marketStatusAndInfo.MarketStatus = ((char)ETradeCommon.Enums.CommonEnums.MARKET_STATUS.READY).ToString();
                    //marketStatusAndInfo.MarketTradingStatus = GetMarketStatus(i);
                    string marketStatusJson2 = GetTradingState(i);
                    var marketInfoObject = Serializer.Deserialize<ETradeCommon.ResultObject<string>>(marketStatusJson);
                    if (marketInfoObject.RetCode == CommonEnums.RET_CODE.SUCCESS)
                    {
                        marketStatusAndInfo.MarketTradingStatus = marketInfoObject.Result;
                    }
                    else
                    {
                        marketStatusAndInfo.MarketTradingStatus = string.Empty;
                    }
                    returnData.Add(marketStatusAndInfo);
                }
            }
            //end processing market info
            return
                Serializer.Serialize(
                    new ResultObject<List<Entities.MMarketStatusAndInfo>>
                    {
                        Result = returnData,
                        RetCode = CommonEnums.RET_CODE.SUCCESS,
                    });
        }
        #endregion
        #region balance
        /// <summary>
        /// Get portfolio of an investor. This is list stock available in portfolio, includes
        /// 1. Sellable stock
        /// 2. Pledge stock
        /// 3. Limittransfer
        /// 4. Wait to receive intra-day
        /// 5. Wait to send intra-day
        /// 6. Wait to receive T1, T2, T3
        /// 7. Wait to send T1, T2, T3
        /// </summary>
        /// <param name="accountNo">
        /// The account no.
        /// </param>
        /// <param name="pageNumber">
        /// The page number.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="accountType">
        /// The account type.
        /// </param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;PortfolioInfo&gt;, PortfolioInfo, PortfolioInfo&gt;&gt;</see> object contains returned code, returned message and 
        /// list of portfolio objects.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=ERROR_ACCOUNT: The account does not exist.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Getting data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get portfolio - stock balance of an investor (it for statement), accountType: 1 for normal account, 1 for margin account", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetPortfolio(string accountNo, int pageNumber, int pageSize, int accountType, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<PagingObject<List<PortfolioInfo>, PortfolioInfo, PortfolioInfo>>
                            {
                                Result = new PagingObject<List<PortfolioInfo>, PortfolioInfo, PortfolioInfo>(),
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                else if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return
                    Serializer.Serialize(
                        new ResultObject<CommonEnums.RET_CODE>
                        {
                            Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                        });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, accountNo))
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<PagingObject<List<PortfolioInfo>, PortfolioInfo, PortfolioInfo>>
                            {
                                Result = new PagingObject<List<PortfolioInfo>, PortfolioInfo, PortfolioInfo>(),
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                var subCustAccount = (SubCustAccount)Session[accountNo + CommonEnums.SESSION_KEY.TRADING_ACCOUNT];

                if (subCustAccount == null)
                {
                    return Serializer.Serialize(
                            new ResultObject<PagingObject<List<PortfolioInfo>, PortfolioInfo, PortfolioInfo>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.ERROR_ACCOUNT,
                                ErrorMessage = CommonEnums.RET_CODE.ERROR_ACCOUNT.ToString()
                            });
                }
                var subCustAccounts = (List<string>)Session[CommonEnums.SESSION_KEY.LIST_SUB_ACCOUNTS.ToString()];
                ResultObject<PagingObject<List<PortfolioInfo>, PortfolioInfo, PortfolioInfo>> resultObject = ETradeServices.GetPortfolio(
                    accountNo, pageNumber, pageSize, accountType, subCustAccount, subCustAccounts);
                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<PagingObject<List<PortfolioInfo>, PortfolioInfo, PortfolioInfo>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }
        /// <summary>
        /// Get list of portfolio of an investor. This is list stock available in portfolio, includes
        /// 1. Sellable stock
        /// 2. Pledge stock
        /// 3. Limittransfer
        /// 4. Wait to receive intra-day
        /// 5. Wait to send intra-day
        /// 6. Wait to receive T1, T2, T3
        /// 7. Wait to send T1, T2, T3
        /// </summary>
        /// <param name="accountNo">
        /// The account no.
        /// </param>
        /// <param name="accountType">
        /// The account type.
        /// </param>
        /// <returns>
        /// <para>A ResultObject&lt;PagingObject&lt;List&lt;string&gt;&gt;&gt; object contains returned code, 
        /// returned message and a list of portfolios.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not logged in.</para>
        /// <para>RET_CODE=ERROR_ACCOUNT: Data of user does not exist.</para>
        /// <para>RET_CODE=SUCCESS: Create data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get portfolio - stock balance of an investor (it for statement), accountType: 0 for normal account, 1 for margin account", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListPortfolio(string accountNo, int accountType, string apiKey)
        {
            var resultObject = new ResultObject<List<string>>();
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    resultObject.Result = null;
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    resultObject.ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString();
                    return Serializer.Serialize(resultObject);
                }
                else if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return
                    Serializer.Serialize(
                        new ResultObject<CommonEnums.RET_CODE>
                        {
                            Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                        });
                }
                var subCustAccount = (SubCustAccount)Session[accountNo + CommonEnums.SESSION_KEY.TRADING_ACCOUNT];

                if (subCustAccount == null)
                {
                    resultObject.Result = null;
                    resultObject.RetCode = CommonEnums.RET_CODE.ERROR_ACCOUNT;
                    resultObject.ErrorMessage = CommonEnums.RET_CODE.ERROR_ACCOUNT.ToString();
                    return Serializer.Serialize(resultObject);
                }

                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, accountNo))
                {
                    resultObject.Result = null;
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    resultObject.ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString();
                    return Serializer.Serialize(resultObject);
                }

                var listPortfolio = ETradeServices.GetListPortfolio(accountNo, accountType);
                resultObject.Result = listPortfolio;
                resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.Result = null;
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                resultObject.ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString();
                return Serializer.Serialize(resultObject);
            }
        }

        /// <summary>
        /// Get cash available os an investor. There are some account types:
        /// 1. Normal account (cash account with 1 in rail): There just BuyCredit
        /// 2. Margin account (with 6 in rail): There BuyCredit, PP, IM
        /// And other accounts
        /// </summary>
        /// <param name="subAccountNo">The account no.</param>
        /// <param name="accountType">The account type.</param>
        /// <param name="isConditionOrder">if set to <c>true</c> [is condition order].</param>
        /// <returns>
        /// 	<para>A <see cref="ResultObject{T}">ResultObject&lt;CashAvailable&gt;</see> object contains returned code, returned message and
        /// list of CashAvailable objects that contains available cash information.</para>
        /// 	<para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// 	<para>RET_CODE=ERROR_NOT_CASH_AVAILABLE: There is no data.</para>
        /// 	<para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// 	<para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(
            Description = "Get available cash for buy order, accountType = 0 for normal account, 1 for margin account",
            EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetAvailableCash(string subAccountNo, int accountType, bool isConditionOrder, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<CashAvailable>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                else if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return
                    Serializer.Serialize(
                        new ResultObject<CommonEnums.RET_CODE>
                        {
                            Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                        });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, subAccountNo))
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<CashAvailable>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                ResultObject<CashAvailable> cashAvailable = ETradeServices.GetAvailableCash(subAccountNo, accountType, isConditionOrder);

                return Serializer.Serialize(cashAvailable);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<CashAvailable>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }
        /// <summary>
        /// Get available stock of an investor. This just sellable share
        /// </summary>
        /// <param name="subAccountNo">The account no.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="accountType">The account type.</param>
        /// <param name="?">The ?.</param>
        /// <param name="isConditionOrder">if set to <c>true</c> [is condition order].</param>
        /// <returns>
        /// 	<para>A <see cref="ResultObject{T}">ResultObject&lt;StockAvailable&gt;</see> object contains returned code, returned message and
        /// list of StockAvailable objects that contains available stock information.</para>
        /// 	<para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// 	<para>RET_CODE=ERROR_NOT_STOCK_AVAILABLE: There is no data.</para>
        /// 	<para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// 	<para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get stock available of a specified symbol for sell order, accountType: 0 for normal account, 1 for margin account", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetAvailableStock(string subAccountNo, string symbol, int accountType, bool isConditionOrder, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<StockAvailable>
                            {
                                Result = new StockAvailable(),
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                else if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return
                    Serializer.Serialize(
                        new ResultObject<CommonEnums.RET_CODE>
                        {
                            Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                        });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, subAccountNo))
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<StockAvailable>
                            {
                                Result = new StockAvailable(),
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                var resultObject = ETradeServices.GetAvailableStock(subAccountNo, symbol, accountType, isConditionOrder);

                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<StockAvailable>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }
        /// <summary>
        /// Get cash balance of an investor. This includes:
        /// 1. Buycredit
        /// 2. CashBalance for margin account
        /// 3. PP, IM for margin account
        /// 4. Withdraw
        /// 5. Net amount of T1, T2, T3
        /// 6. Total buy intra-day
        /// 7. Total sell intra-day
        /// </summary>
        /// <param name="subAccountNo">The account no.</param>
        /// <param name="accountType">The account type.</param>
        /// <returns>
        /// 	<para>A <see cref="ResultObject{T}">ResultObject&lt;CashBalance;&gt;</see> object contains returned code, returned message and
        /// CashAdvance object that contains cash advance information.</para>
        /// 	<para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// 	<para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// 	<para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// 	<para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get cash balance information (it for statement), accountType: 0 for normal account, 1 for margin account", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetCashBalance(string subAccountNo, int accountType, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<CashBalance>
                            {
                                Result = new CashBalance(),
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                else if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return
                    Serializer.Serialize(
                        new ResultObject<CommonEnums.RET_CODE>
                        {
                            Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                        });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, subAccountNo))
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<CashBalance>
                            {
                                Result = new CashBalance(),
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                var resultObject = ETradeServices.GetCashBalance(subAccountNo, accountType);
                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<CashBalance>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }

        /// <summary>
        /// Gets the portfolio direct.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="accountType">Type of the account.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;List&lt;Portfolio&gt;&gt;</see> object contains returned code, returned message and 
        /// list of portfolio objects.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=SUCCESS: Getting data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get portfolio information. This option for accessing direct table", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetPortfolioDirect(string accountNo, int accountType, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<List<Portfolio>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                else if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return
                    Serializer.Serialize(
                        new ResultObject<CommonEnums.RET_CODE>
                        {
                            Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                        });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, accountNo))
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<List<Portfolio>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                var resultObject = ETradeServices.GetPortfolioDirect(accountNo, accountType);

                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<List<Portfolio>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }
        [WebMethod(Description = "Get stock balance data. Using for mobile version", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string MGetStockBalanceData(string accountNo, int pageNumber, int pageSize, int accountType, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<PagingObject<List<Entities.MStockBalanceData>, Entities.MStockBalanceData, Entities.MStockBalanceData>>
                            {
                                Result = new PagingObject<List<Entities.MStockBalanceData>, Entities.MStockBalanceData, Entities.MStockBalanceData>(),
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                else if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return
                    Serializer.Serialize(
                        new ResultObject<CommonEnums.RET_CODE>
                        {
                            Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                        });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, accountNo))
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<PagingObject<List<Entities.MStockBalanceData>, Entities.MStockBalanceData, Entities.MStockBalanceData>>
                            {
                                Result = new PagingObject<List<Entities.MStockBalanceData>, Entities.MStockBalanceData, Entities.MStockBalanceData>(),
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                var subCustAccount = (SubCustAccount)Session[accountNo + CommonEnums.SESSION_KEY.TRADING_ACCOUNT];

                if (subCustAccount == null)
                {
                    return Serializer.Serialize(
                            new ResultObject<PagingObject<List<Entities.MStockBalanceData>, Entities.MStockBalanceData, Entities.MStockBalanceData>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.ERROR_ACCOUNT,
                                ErrorMessage = CommonEnums.RET_CODE.ERROR_ACCOUNT.ToString()
                            });
                }
                var subCustAccounts = (List<string>)Session[CommonEnums.SESSION_KEY.LIST_SUB_ACCOUNTS.ToString()];
                ResultObject<PagingObject<List<PortfolioInfo>, PortfolioInfo, PortfolioInfo>> resultObject = ETradeServices.GetPortfolio(
                    accountNo, pageNumber, pageSize, accountType, subCustAccount, subCustAccounts);
                List<ETradeWebServices.Entities.MStockBalanceData> listMStockBD = new List<Entities.MStockBalanceData>();
                foreach (var porfolio in resultObject.Result.Data)
                {
                    var stockInfo = Serializer.Deserialize<ResultObject<RTDataServices.Entities.StockInfo>>(rtDataService.GetStockInfo(porfolio.Symbol)).Result;
                    var stockBalanceData = new Entities.MStockBalanceData();
                    double priceCur,changeCur;
                    if(stockInfo!=null){
					    if(stockInfo.Last>0)
                        {
						    priceCur = stockInfo.Last;
					    }
					    else{
						    priceCur = stockInfo.RefPrice;
					    }
					    changeCur = priceCur - stockInfo.RefPrice;
					    stockBalanceData.FloorPrice = stockInfo.Floor;
					    stockBalanceData.CeillingPrice = stockInfo.Ceiling;
					    stockBalanceData.RefPrice = stockInfo.RefPrice;
                        stockBalanceData.ErrorMessage = string.Empty;
                        stockBalanceData.Best1Bid = stockInfo.Best1Bid;
                        stockBalanceData.Best1Offer = stockInfo.Best1Offer;
				    }
				    else{
					    priceCur = 0;
					    changeCur = 0;
                        stockBalanceData.ErrorMessage = "Can get price for this stock";
				    }
                    stockBalanceData.MarketPrice=porfolio.MarketPrice;
                    stockBalanceData.Symbol=porfolio.Symbol;
                    stockBalanceData.SellableShare=porfolio.SellableShare;
                    stockBalanceData.Total=porfolio.Total;
                    stockBalanceData.WTR_T3=porfolio.WTR_T3;
                    stockBalanceData.WTR_T2=porfolio.WTR_T2;
                    stockBalanceData.WTR_T1=porfolio.WTR_T1;
                    stockBalanceData.WTS_T3=porfolio.WTS_T3;
                    stockBalanceData.WTS_T2=porfolio.WTS_T2;
                    stockBalanceData.WTS_T1=porfolio.WTS_T1;
                    stockBalanceData.WTS=porfolio.WTS;
                    stockBalanceData.WTR=porfolio.WTR;
                    stockBalanceData.AvgPrice=porfolio.AvgPrice;
                    stockBalanceData.CurrentPrice=priceCur;
                    stockBalanceData.CurrentChange=changeCur;
                    stockBalanceData.InvestValue=porfolio.InvestValue / 100;
                    stockBalanceData.CurrentValue=porfolio.CurrentValue / 100;
                    stockBalanceData.GainLoss=porfolio.GainLoss / 100;
                    stockBalanceData.GainLostToday=porfolio.GainLostToday / 100;
                    stockBalanceData.Percent=porfolio.Percent / 100;
                    stockBalanceData.CanBuy=porfolio.CanBuy;
                    stockBalanceData.CanSell=porfolio.CanSell;
                    listMStockBD.Add(stockBalanceData);
                }
                if (listMStockBD.Count > 0)
                {
                    var returnResult = new PagingObject<List<Entities.MStockBalanceData>, Entities.MStockBalanceData, Entities.MStockBalanceData>();
                    returnResult.Data = listMStockBD;
                    return Serializer.Serialize(
                            new ResultObject<PagingObject<List<Entities.MStockBalanceData>,Entities.MStockBalanceData, Entities.MStockBalanceData>>
                            {
                                Result = returnResult,
                                RetCode = CommonEnums.RET_CODE.SUCCESS,
                                ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString()
                            });
                }
                else
                {
                    return Serializer.Serialize(
                           new ResultObject<PagingObject<List<Entities.MStockBalanceData>, Entities.MStockBalanceData, Entities.MStockBalanceData>>
                           {
                               RetCode = CommonEnums.RET_CODE.ERROR_EMPTY,
                               ErrorMessage = CommonEnums.RET_CODE.ERROR_EMPTY.ToString()
                           });
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<PagingObject<List<PortfolioInfo>, PortfolioInfo, PortfolioInfo>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }
        #endregion
        #region check account methods
        /// <summary>
        /// Check if user is using multiple accounts or not.
        /// </summary>
        /// <param name="sendingMainCustAccountId">Main customer account id sent from website</param>
        /// <param name="sendingSubCustAccountId">Sub customer account id sent from website</param>
        /// <returns>true: if using multiple account; otherwise false.</returns>
        private bool IsMultiAccount(string sendingMainCustAccountId, string sendingSubCustAccountId)
        {
            if (!string.IsNullOrEmpty(sendingMainCustAccountId))
            {
                var mainCustAccountId = (string)Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()];
                if (mainCustAccountId != sendingMainCustAccountId)
                {
                    return true;
                }
            }
            if (!string.IsNullOrEmpty(sendingSubCustAccountId))
            {
                var subCustAccountIdList = (List<string>)Session[CommonEnums.SESSION_KEY.LIST_SUB_ACCOUNTS.ToString()];
                if (subCustAccountIdList != null)
                {
                    foreach (var subId in subCustAccountIdList)
                    {
                        if (subId == sendingSubCustAccountId)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }
        #endregion check account methods
        #region order
        /// <summary>
        /// Puts new order.
        /// </summary>
        /// <param name="market">
        /// The market id. Please refer to the enum MARKET_ID in CommonEnums.cs to know the values.
        /// </param>
        /// <param name="accountNo">
        /// The sub account ID such as 0088661, 0088666
        /// </param>
        /// <param name="pin">
        /// The pin code
        /// </param>
        /// <param name="secSymbol">
        /// The sec symbol of HOSE/HNX/UpCom
        /// </param>
        /// <param name="side">
        /// The side includes values BUY: "B", SELL: "S". Please refer to the enum TRADE_SIDE in CommonEnums.cs to know the values.
        /// </param>
        /// <param name="volume">
        /// The volume.
        /// </param>
        /// <param name="price">
        /// The price.
        /// </param>
        /// <param name="conPrice">
        /// The con price includes values ATO: 'A', ATC: 'C', LO: ' '. Please refer to the enum TRADE_SIDE in Constants.cs to know the values.
        /// </param>
        /// <param name="accountType">
        /// Type of the account includes values Normal account (such as 0088661): 0, Margin account (such as 0088666): 1
        /// </param>
        /// <param name="orderSource">Order source: From web or sms.</param>
        /// <returns>
        /// <para>
        /// Result of putting order.
        /// If the RetCode is CommonEnums.RET_CODE.SUCCESS then Result of ResultObject is the order id.
        /// Otherwise, it is a reject code. Please refer to the reject code in the enum REJECT_REASON of CommonEnums.cs
        /// </para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=INCORRECT_PIN: The pin is incorrect.</para>
        /// <para>RET_CODE=ERROR_ACCOUNT: Account does not exist.</para>
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
        [WebMethod(Description = "Put Order, return a ResultObject<Integer>", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string PutOrder(int market, string accountNo, string pin, string secSymbol, char side, int volume,
            decimal price, char conPrice, int accountType, char orderSource, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return Serializer.Serialize(
                            new ResultObject<int>
                            {
                                Result = -1,
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                else if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return
                    Serializer.Serialize(
                        new ResultObject<CommonEnums.RET_CODE>
                        {
                            Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                        });
                }
                if (Session[accountNo + CommonEnums.SESSION_KEY.TRADING_ACCOUNT] == null)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<int>
                            {
                                ErrorMessage = CommonEnums.RET_CODE.ERROR_ACCOUNT.ToString(),
                                Result = (int)CommonEnums.RET_CODE.ERROR_ACCOUNT,
                                RetCode = CommonEnums.RET_CODE.ERROR_ACCOUNT
                            });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, accountNo))
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<int>
                            {
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString(),
                                Result = (int)CommonEnums.RET_CODE.NOT_LOGIN,
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN
                            });
                }
                string sessionPin = Session[CommonEnums.SESSION_KEY.PIN.ToString()].ToString();

                if (PasswordHandlerMd5.Encrypt(pin) != sessionPin)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<int>
                            {
                                ErrorMessage = CommonEnums.RET_CODE.INCORRECT_PIN.ToString(),
                                Result = (int)CommonEnums.RET_CODE.FAIL,
                                RetCode = CommonEnums.RET_CODE.INCORRECT_PIN
                            });
                }

                var subCustAccount = (SubCustAccount)Session[accountNo + CommonEnums.SESSION_KEY.TRADING_ACCOUNT];

                var subCustAccounts = (List<string>)Session[CommonEnums.SESSION_KEY.LIST_SUB_ACCOUNTS.ToString()];

                int customerType;
                customerType = int.TryParse(Session[CommonEnums.SESSION_KEY.CUSTOMER_TYPE.ToString()].ToString(),
                                            out customerType)
                                   ? customerType
                                   : (int)CommonEnums.CUSTOMER_TYPE.INTERNAL;

                ResultObject<int> resultObject = ETradeServices.PutOrder(market, accountNo, secSymbol, side, volume,
                                                                         price, conPrice, accountType, customerType,
                                                                         subCustAccount, subCustAccounts, orderSource,(char)CommonEnums.CONDITION.NO_CONDITION);

                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<int>
                            {
                                Result = -1,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }

        /// <summary>
        /// Cancels an existed order. This order must is pending or is already confirmed from SET or FIS
        /// </summary>
        /// <param name="orderId">The order id that was return by PutOrder APIs</param>
        /// <param name="accountNo">The sub accountId such as 0088661, 0088666</param>
        /// <param name="pin">The pin code</param>
        /// <returns>
        /// <para>ResultObject of interger. 
        /// If the RetCode is CommonEnums.RET_CODE.SUCCESS then Result of ResultObject is the order id.
        /// Otherwise, it is a reject code. Please refer to the reject code in the enum REJECT_REASON of CommonEnums.cs</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=INCORRECT_PIN: The pin is incorrect.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=ERROR_CANNOT_CANCEL_ODD_LOT_ORDER: Cannot cancel odd lot order because it's in incorrect state.</para>
        /// <para>RET_CODE=SUCCESS: Cancel successfully.</para>
        /// <para>RET_CODE=FAIL: Fail to cancel odd lot order.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Cancel Order, return a ResultObject<Integer>", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CancelOrder(int orderId, string accountNo, string pin, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<int>
                            {
                                Result = -1,
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                else if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return
                    Serializer.Serialize(
                        new ResultObject<CommonEnums.RET_CODE>
                        {
                            Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                        });
                }
                string sessionPin = Session[CommonEnums.SESSION_KEY.PIN.ToString()].ToString();

                if (PasswordHandlerMd5.Encrypt(pin) != sessionPin)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<int>
                            {
                                ErrorMessage = CommonEnums.RET_CODE.INCORRECT_PIN.ToString(),
                                Result = (int)CommonEnums.RET_CODE.FAIL,
                                RetCode = CommonEnums.RET_CODE.INCORRECT_PIN
                            });
                }

                ResultObject<int> resultObject = ETradeServices.CancelOrder(orderId);

                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<int>
                            {
                                Result = -1,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR
                            });
            }

        }
        /// <summary>
        /// Gets all the newest order information that put to system from all sources.
        /// </summary>
        /// <param name="pageSize">Size of the page.</param> 
        /// <param name="pageIndex">The page number. if pageIndex = 0, it will return all orders</param>
        /// <param name="accountNo">The account no.</param>
        /// <param name="isPending">You want it to return the pending orders. Values is True/False.</param>
        /// <param name="isMatched">You want it to return the matched orders. Values is True/False.</param>
        /// <param name="isSemiMatched">You want it to return the semimatched orders. Values is True/False.</param>
        /// <param name="isCanceling">You want it to return the canceling orders. Values is True/False.</param>
        /// <param name="isCancelled">You want it to return the canceled orders. Values is True/False.</param>
        /// <param name="isRejected">You want it to return the rejected orders. Values is True/False.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;ExecOrder&gt;&gt;&gt;</see> object contains returned code, returned message and 
        /// list of ExecOrder objects that contains order information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Putting order successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get newest order status Order. Return ResultObject<PagingObject<List<ExecOrder>>>", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string NewestOrdStatus(int pageSize, int pageIndex, string accountNo,
                                             bool isPending, bool isMatched, bool isSemiMatched, bool isCanceling, bool isCancelled, bool isRejected, string side, short marketId, string symbol, string apiKey, string conPrice)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<PagingObject<List<ExecOrder>>>
                            {
                                Result = new PagingObject<List<ExecOrder>>(),
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                else if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return
                    Serializer.Serialize(
                        new ResultObject<CommonEnums.RET_CODE>
                        {
                            Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                        });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, accountNo))
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<PagingObject<List<ExecOrder>>>
                            {
                                Result = new PagingObject<List<ExecOrder>>(),
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                ResultObject<PagingObject<List<ExecOrder>>> resultObject = ETradeServices.GetNewsestOrderStatus(pageSize, pageIndex, accountNo, isPending, isMatched, isSemiMatched, isCanceling, isCancelled, isRejected);
                if (resultObject.RetCode == CommonEnums.RET_CODE.SUCCESS)
                {
                    var listOrders = resultObject.Result.Data;

                    resultObject.Result.isNew = IsOrdersUpdated(listOrders);
                }
                else
                {
                    resultObject.Result.isNew = false;
                }

                var resultList = new List<ExecOrder>();
                var RTService = new RTServices.Service();
                foreach (var obj in resultObject.Result.Data)
                {                   
                    if (marketId != -1)
                    {
                        var stockInfoStr = RTService.GetStockInfo(obj.SecSymbol);
                        var stockInfo = Serializer.Deserialize<ResultObject<RTDataServices.Entities.StockInfo>>(stockInfoStr);
                        if (marketId != stockInfo.Result.MarketID)
                            break;
                    }                    
                    if (side != "-1")
                    {
                        if (side != obj.Side)
                            break;
                    }                    
                    if (conPrice != "-1")
                    {
                        if (conPrice == "LO")
                        {
                            if(obj.ConPrice == "C" || obj.ConPrice == "A")
                                break;
                        }                           
                        else if (conPrice != obj.ConPrice)
                            break;                        
                    }      

                    resultList.Add(obj);                    
                }
                resultObject.Result.Data = resultList;
                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<PagingObject<List<ExecOrder>>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR
                            });
            }
        }
        /// <summary>
        /// Compare to find out the different between new and old order status. Update new ones to sesssion
        /// </summary>
        /// <param name="listOrders">Order list to check.</param>
        /// <returns>true if the order list is new; otherwise false.</returns>
        private Boolean IsOrdersUpdated(List<ExecOrder> listOrders)
        {
            if (listOrders == null || listOrders.Count == 0)
                return false;

            if (Session[CommonEnums.SESSION_KEY.STOCK_ORDERS.ToString()] == null)
            {
                Session[CommonEnums.SESSION_KEY.STOCK_ORDERS.ToString()] = listOrders;

                return true;
            }

            var oldListOrders = (List<ExecOrder>)Session[CommonEnums.SESSION_KEY.STOCK_ORDERS.ToString()];

            if (listOrders.Count != oldListOrders.Count)
            {
                Session[CommonEnums.SESSION_KEY.STOCK_ORDERS.ToString()] = listOrders;

                return true;
            }

            for (int index = 0; index < listOrders.Count; index++)
            {
                ExecOrder oldItem = oldListOrders[index];
                ExecOrder newItem = listOrders[index];

                if (oldItem.OrderId != newItem.OrderId ||
                    oldItem.OrderStatus != newItem.OrderStatus ||
                    oldItem.NumOfMatch != newItem.NumOfMatch ||
                    oldItem.canCancel != newItem.canCancel)
                {
                    Session[CommonEnums.SESSION_KEY.STOCK_ORDERS.ToString()] = listOrders;

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the total orders that put to system from all sources.
        /// </summary>
        /// <param name="accountId">The account no.</param>
        /// <param name="isPending">You want it to return the pending orders. Values is True/False.</param>
        /// <param name="isMatched">You want it to return the matched orders. Values is True/False.</param>
        /// <param name="isSemiMatched">You want it to return the semimatched orders. Values is True/False.</param>
        /// <param name="isCanceling">You want it to return the canceling orders. Values is True/False.</param>
        /// <param name="isCancelled">You want it to return the canceled orders. Values is True/False.</param>
        /// <param name="isRejected">You want it to return the rejected orders. Values is True/False.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;int&gt;</see> object contains returned code, returned message and 
        /// total records.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=SUCCESS: Putting order successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get newest order status Order", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string NewestOrdCount(string accountId, bool isPending, bool isMatched, bool isSemiMatched, bool isCanceling, bool isCancelled, bool isRejected, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<int>
                            {
                                Result = -1,
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                else if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return
                    Serializer.Serialize(
                        new ResultObject<CommonEnums.RET_CODE>
                        {
                            Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                        });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, accountId))
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<int>
                            {
                                Result = -1,
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                ResultObject<int> resultObject = ETradeServices.GetNewsestOrderCount(accountId, isPending, isMatched, isSemiMatched, isCanceling, isCancelled, isRejected);

                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<int>
                            {
                                Result = -1,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR
                            });
            }
        }

        /// <summary>
        /// Get order history of an investor
        /// </summary>
        /// <param name="accountNo">
        /// The sub account no. such as 0088661, 0088666
        /// </param>
        /// <param name="fromDate">
        /// The from date. Format(YYYYMMDD)
        /// </param>
        /// <param name="toDate">
        /// The to date. Format(YYYYMMDD)
        /// </param>
        /// <param name="symbol">
        /// The symbol.
        /// </param>
        /// <param name="orderStatus">
        /// The order status. please refer the values from FILTER_ORDER_STATUS in CommonEnums.cs
        /// </param>
        /// <param name="pageIndex">
        /// The page index. if pageIndex = 0 then it will return all results.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;OrderHistory&gt;&gt;&gt;</see> object contains returned code, returned message and 
        /// list of OrderHistory objects.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=ERROR_INVALID_DATETIME: The date is invalid.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Getting data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get order history", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetOrderHistory(string accountNo, string fromDate, string toDate, string symbol, int status, int pageIndex, int pageSize, string apiKey, string side, string conPrice, int marketId)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<PagingObject<List<OrderHistory>>>
                            {
                                Result = new PagingObject<List<OrderHistory>>(),
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                else if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return
                    Serializer.Serialize(
                        new ResultObject<CommonEnums.RET_CODE>
                        {
                            Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                        });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, accountNo))
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<PagingObject<List<OrderHistory>>>
                            {
                                Result = new PagingObject<List<OrderHistory>>(),
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                side = side.Trim();
                conPrice = conPrice.Trim();
                var resultObject = ETradeServices.GetOrderHistory(accountNo, fromDate, toDate, symbol, status, pageIndex, pageSize);
                var returnData = new List<OrderHistory>();
                if (side != "-1" || conPrice != "-1" || marketId != -1)
                {
                    RTServices.Service rtService = new RTServices.Service();
                    foreach (var obj in resultObject.Result.Data)
                    {
                        if (side != "-1" && side != obj.Side)
                        {
                            break;
                        }
                        if (conPrice != "-1")
                        {
                            if (conPrice == "LO")
                            {
                                if (obj.ConditionPrice == "C" || obj.ConditionPrice == "A")
                                    break;
                            }
                            else if (conPrice != obj.ConditionPrice)
                                break;                           
                        }
                        if (marketId != -1)
                        {
                            var stockInfoStr = rtService.GetStockInfo(obj.Symbol);
                            var stockInfo = Serializer.Deserialize<ResultObject<RTDataServices.Entities.StockInfo>>(stockInfoStr);
                            if (stockInfo.Result.MarketID != marketId) break;
                        }
                        returnData.Add(obj);
                    }
                }
                resultObject.Result.Data = returnData;
                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<PagingObject<List<OrderHistory>>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }

        /// <summary>
        /// Gets the order history count.
        /// </summary>
        /// <param name="accountNo">
        /// The sub account no. such as 0088661, 0088666
        /// </param>
        /// <param name="fromDate">
        /// The from date. Format(YYYYMMDD)
        /// </param>
        /// <param name="toDate">
        /// The to date. Format(YYYYMMDD)
        /// </param>
        /// <param name="symbol">
        /// The symbol.
        /// </param>
        /// <param name="orderStatus">
        /// The order status. please refer the values from FILTER_ORDER_STATUS in CommonEnums.cs
        /// </param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;int&gt;</see> object contains returned code, returned message and 
        /// total records.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Getting data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get order history count, return ResultObject<int>", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetOrderHistoryCount(string accountNo, string fromDate, string toDate, string symbol, int orderStatus, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<int>
                            {
                                Result = 0,
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                else if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return
                    Serializer.Serialize(
                        new ResultObject<CommonEnums.RET_CODE>
                        {
                            Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                        });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, accountNo))
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<int>
                            {
                                Result = 0,
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                var resultObject = ETradeServices.GetOrderHistoryCount(accountNo, fromDate, toDate, symbol, orderStatus);

                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<int>
                            {
                                Result = -1,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }
        /// <summary>
        /// Get deal detail of order history
        /// </summary>
        /// <param name="orderNo">
        /// The order no that was returned by GetOrderHistory.
        /// </param>
        /// <param name="dealDate">
        /// The deal date. Format (YYYYMMDD)
        /// </param>
        /// <param name="page">
        /// The page. page = 0 is all.
        /// </param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;List&lt;DealHistory&gt;&gt;</see> object contains returned code, returned message and 
        /// list of DealHistory object that contains deal history information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=ERROR_INVALID_DATETIME: The dealDate is invalid.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get deal history, return ResultObject<List<DealHistory>>", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetDealHistory(decimal orderNo, string dealDate, int page, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<List<DealHistory>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                else if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return
                    Serializer.Serialize(
                        new ResultObject<CommonEnums.RET_CODE>
                        {
                            Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                        });
                }
                var resultObject = ETradeServices.GetDealHistory(orderNo, dealDate, page);

                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<List<DealHistory>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }
        /// <summary>
        /// Gets the deal intra day.
        /// </summary>
        /// <param name="orderNo">The order no that was returned by PutOrder.</param>
        /// <param name="page">The page. If page = 0, it return all results.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;List&lt;DealInfo&gt;&gt;</see> object contains returned code, returned message and 
        /// list of DealInfo object that contains deal information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=ERROR_INVALID_DATETIME: The dealDate is invalid.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get deal information for intra-day, return ResultObject<List<DealInfo>>", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetDealIntraDay(decimal orderNo, int page, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<List<DealInfo>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                else if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return
                    Serializer.Serialize(
                        new ResultObject<CommonEnums.RET_CODE>
                        {
                            Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                        });
                }
                var resultObject = ETradeServices.GetDealIntraDay(orderNo, page);

                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<List<DealInfo>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }

        /// <summary>
        /// Gets the information that support for user that can refer before puting order.
        /// </summary>
        /// <param name="accountNo">The sub account no. Such as 0088661, 0088666</param>
        /// <param name="symbol">The symbol of HOSE/HNX/UPCOM</param>
        /// <param name="accountType">Type of the account.
        /// 0: is normal account (such as 0088661).
        /// 1: is margin account (such as 0088666).
        /// Please refer to ACCOUNT_TYPE in CommonEnums.cs</param>
        /// <param name="side">The side of trading.
        /// 'B': buy, 'S': sell.
        /// Please refer to TRADE_SIDE in CommonEnums.cs</param>
        /// <param name="isConditionOrder">if set to <c>true</c> [is condition order].</param>
        /// <returns>
        /// 	<para>A <see cref="ResultObject{T}">ResultObject&lt;PreTradeInfo&gt;</see> object contains returned code, returned message and
        /// pretrade information.</para>
        /// 	<para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// 	<para>RET_CODE=SUCCESS: Getting data successfully.</para>
        /// 	<para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get pre trade information, it includes: stock available, cash available, stock information, tradign state", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetPreTradeInfo(string accountNo, string symbol, int accountType, char side, bool isConditionOrder, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<PreTradeInfo>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                else if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return
                    Serializer.Serialize(
                        new ResultObject<CommonEnums.RET_CODE>
                        {
                            Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                        });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, accountNo))
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<PreTradeInfo>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                var resultObject = ETradeServices.GetPreTradeInfo(accountNo, symbol, accountType, side, isConditionOrder);
                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<PreTradeInfo>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }

        /// <summary>
        /// Put advance orders.
        /// </summary>
        /// <param name="market">Market Id</param>
        /// <param name="accountNo">Sub account Id</param>
        /// <param name="pin">Pin</param>
        /// <param name="secSymbol">security symbol</param>
        /// <param name="side">Side "B":buy; "S":Sell</param>
        /// <param name="volume">Volume to trade</param>
        /// <param name="price">Price to trade</param>
        /// <param name="strEffDate">Effected date </param>
        /// <param name="strExpDate">Expired date</param>
        /// <param name="type">Type of advance order</param>
        /// <param name="minValue">Minimum price value</param>
        /// <param name="maxValue">Maximum price value</param>
        /// <returns>
        /// <para>
        /// Result of putting advance order.
        /// If the RetCode is CommonEnums.RET_CODE.SUCCESS then Result of ResultObject is the order id.
        /// Otherwise, it is a reject code. Please refer to the reject code in the enum REJECT_REASON of CommonEnums.cs
        /// </para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=INCORRECT_PIN: The pin is incorrect.</para>
        /// <para>RET_CODE=ERROR_ACCOUNT: Account does not exist.</para>
        /// <para>RET_CODE=NOT_ALLOW: Customer isn't allowed to buy or sell stock.</para>
        /// <para>RET_CODE=SUCCESS: Putting order successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// <para>REJECT_REASON</para>
        /// <para>REJECT_REASON=NOT_ADVANCE_TIME: This time is not advance time.</para>
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
        /// <para>REJECT_REASON=ERROR_LOCK_ACCOUNT: Account is locked.</para>
        /// <para>REJECT_REASON=ERROR_ACCOUNT_NOT_CONDITION_ORDER: Account is not allowed to put advance order.</para>
        /// <para>REJECT_REASON=NOT_ENOUGH_CASH: Customer has not enough money.</para>
        /// <para>REJECT_REASON=NOT_ENOUGH_STOCK: Customer has not enough stocks.</para>
        /// <para>REJECT_REASON=IS_VALID: This is a valid order.</para>
        /// </returns>
        [WebMethod(Description = "Put advance orders for a range of days", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string PutAdvanceOrder(int market, string accountNo, string pin, string secSymbol, char side, int volume, decimal price, string strEffDate, string strExpDate, short type, decimal minValue, decimal maxValue, string apiKey)
        {
            var resultObject = new ResultObject<int>();
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    return Serializer.Serialize(resultObject);
                }
                else if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return
                    Serializer.Serialize(
                        new ResultObject<CommonEnums.RET_CODE>
                        {
                            RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                        });
                }
                if (Session[accountNo + CommonEnums.SESSION_KEY.TRADING_ACCOUNT] == null)
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.ERROR_ACCOUNT;
                    return Serializer.Serialize(resultObject);
                }

                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, accountNo))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    return Serializer.Serialize(resultObject);
                }

                if (!ETradeCommon.Utils.IsValidDate(strEffDate) || !ETradeCommon.Utils.IsValidDate(strExpDate))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.ERROR_INVALID_DATETIME;
                    return Serializer.Serialize(resultObject);
                }

                string sessionPin = Session[CommonEnums.SESSION_KEY.PIN.ToString()].ToString();

                // Validate pin
                if (PasswordHandlerMd5.Encrypt(pin) != sessionPin)
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.INCORRECT_PIN;
                    return Serializer.Serialize(resultObject);
                }

                // Check permission
                var subCustAccount = (SubCustAccount)Session[accountNo + CommonEnums.SESSION_KEY.TRADING_ACCOUNT];
                var permissionList = subCustAccount.SubCustAccountPermissionCollection;
                bool isAllow = false;
                if (permissionList != null)
                {
                    foreach (var subCustAccountPermission in permissionList)
                    {
                        if (subCustAccountPermission.CustServicesPermissionId == (int)CommonEnums.SUB_ACCOUNT_PERMISSIONS.CONDITION_ORDER)
                        {
                            isAllow = true;
                            break;
                        }
                    }
                }
                if (!isAllow)
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_ALLOW;
                    return Serializer.Serialize(resultObject);
                }
                char conPrice = Constants.ORDER_TYPE_LO;
                if (type == (short)CommonEnums.CONDITION_ORDER_TYPE.ATO)
                {
                    conPrice = Constants.ORDER_TYPE_ATO;
                    price = -1;
                }
                else if (type == (short)CommonEnums.CONDITION_ORDER_TYPE.MP)
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.FAIL;
                    return Serializer.Serialize(resultObject);
                }
                int accountType = ETradeCommon.Utils.GetAccountType(accountNo);
                int customerType = int.TryParse(Session[CommonEnums.SESSION_KEY.CUSTOMER_TYPE.ToString()].ToString(),
                                                out customerType)
                                       ? customerType
                                       : (int)CommonEnums.CUSTOMER_TYPE.INTERNAL;
                bool isMargin = false;
                if (side == (char)CommonEnums.TRADE_SIDE.BUY)
                {
                    if (ETradeCommon.Utils.GetAccountType(accountNo) == (int)CommonEnums.ACCOUNT_TYPE.MARGIN)
                    {
                        isMargin = true;
                        CommonEnums.REJECT_REASON isValidBuyMarginAccount = ValidateServices.IsValidBuyMarginAccount(accountNo, secSymbol, price, volume, strEffDate, true);
                        if (isValidBuyMarginAccount != CommonEnums.REJECT_REASON.IS_VALID)
                        {
                            resultObject.RetCode = CommonEnums.RET_CODE.FAIL;
                            resultObject.Result = (int)isValidBuyMarginAccount;
                            return Serializer.Serialize(resultObject);
                        }
                    }
                }

                var rejectCode = ValidateServices.IsValidAdvanceOrder(market, accountNo, secSymbol, side, volume, price,
                                                                      conPrice, accountType, customerType,
                                                                      subCustAccount, isMargin, strEffDate);
                // Putting advance orders
                if (rejectCode == CommonEnums.REJECT_REASON.IS_VALID)
                {
                    var conditionOrder = new ConditionOrder
                    {
                        SecSymbol = secSymbol,
                        Side = side.ToString(),
                        Price = price,
                        Volume = volume,
                        MatchedVolume = 0,
                        SubCustAccountId = accountNo,
                        MainCustAccountId = (string)Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()],
                        Market = market.ToString(),
                        TypeOfCond = type,
                        MinValue = minValue,
                        MaxValue = maxValue,
                        Status = ((int)CommonEnums.CONDITION_ORDER_STATUS.WAITING).ToString(),
                        TradeTime = DateTime.Now
                    };
                    if (!string.IsNullOrEmpty(strEffDate))
                    {
                        var effDate = DateTime.ParseExact(strEffDate, "yyyyMMdd", null);
                        conditionOrder.EffDate = effDate;
                    }
                    if (!string.IsNullOrEmpty(strExpDate))
                    {
                        var expDate = DateTime.ParseExact(strExpDate, "yyyyMMdd", null);
                        conditionOrder.ExpDate = expDate;
                    }

                    var conditionOrderService = new ConditionOrderService();
                    bool result = conditionOrderService.Insert(conditionOrder);
                    if (result)
                    {
                        resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                    }
                }
                else
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.FAIL;
                    resultObject.Result = (int)rejectCode;
                }

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                resultObject.ErrorMessage = e.Message;
            }
            return Serializer.Serialize(resultObject);
        }

        /// <summary>
        /// Cancel advance orders.
        /// </summary>
        /// <param name="conditionOrderId">Condition Order Id</param>
        /// <param name="subCustAccountId">Sub account Id</param>
        /// <param name="pin">Pin</param>
        /// <returns>
        /// <para>Result of cancelling advance order.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=ERROR_ACCOUNT: Information of account in session does not exist.</para>
        /// <para>RET_CODE=INCORRECT_PIN: The pin is incorrect.</para>
        /// <para>RET_CODE=ADVANCE_ORDER_STATUS_INCORRECT_STATE: The order is not in the correct state to cancel.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: Data does not exist.</para>
        /// <para>RET_CODE=SUCCESS: Cancel successfully.</para>
        /// <para>RET_CODE=FAIL: Fail to cancel order.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Cancel advance orders", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CancelAdvanceOrder(long conditionOrderId, string subCustAccountId, string pin, string apiKey)
        {
            var resultObject = new ResultObject<int>();
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    return Serializer.Serialize(resultObject);
                }
                else if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return
                    Serializer.Serialize(
                        new ResultObject<CommonEnums.RET_CODE>
                        {
                            Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                        });
                }
                if (Session[subCustAccountId + CommonEnums.SESSION_KEY.TRADING_ACCOUNT] == null)
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.ERROR_ACCOUNT;
                    return Serializer.Serialize(resultObject);
                }

                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, subCustAccountId))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    return Serializer.Serialize(resultObject);
                }

                string sessionPin = Session[CommonEnums.SESSION_KEY.PIN.ToString()].ToString();

                // Validate pin
                if (PasswordHandlerMd5.Encrypt(pin) != sessionPin)
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.INCORRECT_PIN;
                    return Serializer.Serialize(resultObject);
                }

                var result = ConditionOrderService.CancelConditionOrder(conditionOrderId, subCustAccountId);
                resultObject.RetCode = result;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                resultObject.ErrorMessage = e.Message;
            }
            return Serializer.Serialize(resultObject);
        }

        /// <summary>
        /// Put orders of condition orders at the beginning of trading days
        /// </summary>
        /// <returns>
        /// <para>Result of putting condition orders.</para>
        /// </returns>
        [WebMethod(Description = "Put advance orders at the beginning of day")]
        public int PutConditionOrder(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey) || Session == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
            {
                return (int)CommonEnums.RET_CODE.INVAILID_API_KEY;

            }
            LogHandler.Log("Start putting condition orders", "PutConditionOrder", TraceEventType.Information);
            if (ETradeCommon.Utils.IsWorkingDay(DateTime.Now, SysConfig.Holidays, SysConfig.WorkingDays))
            {
                ETradeServices.StartPutConditionOrderThread();
            }
            return 0;
        }

        /// <summary>
        /// Get list of condition orders. 
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="side">Buy or Sell side</param>
        /// <param name="symbol">Stock symbol</param>
        /// <param name="status">Status of orders</param>
        /// <param name="fromDate">Searched effective date from</param>
        /// <param name="toDate">Searched effective date to</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="fromTool"></param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;ConditionOrder&gt;&gt;&gt;</see> object contains returned code, returned message and 
        /// list of ConditionOrder objects that contains condition order information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=ERROR_INVALID_DATETIME: The sent date is invalid.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list of condition orders")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListConditionOrder(string subAccountId, string side, string symbol,
            string fromDate, string toDate, int pageIndex, int pageSize, int[] status, int fromTool, short maketId)
        {
            var resultObject = new ResultObject<PagingObject<List<ConditionOrder>>> { Result = null };
            try
            {
                if (!string.IsNullOrEmpty(toDate))
                {
                    var tmpValue = DateTime.ParseExact(toDate, "yyyyMMdd", null);
                    tmpValue = tmpValue.AddDays(1);
                    toDate = tmpValue.ToString("yyyyMMdd");
                }

                int count;                
                var list = ConditionOrderService.GetListConditionOrder(subAccountId, side, symbol, status, fromDate,
                                                                       toDate, pageIndex, pageSize, out count, fromTool);
                if (list != null && list.Count > 0)
                {
                    var rtService = new RTServices.Service();
                    if (maketId != -1)
                    {
                        var returnList = new List<ConditionOrder>();
                        foreach (var obj in list)
                        {
                            var stockInfoStr = rtService.GetStockInfo(obj.SecSymbol);
                            var stockInfo = Serializer.Deserialize<ResultObject<RTDataServices.Entities.StockInfo>>(stockInfoStr);
                            if (stockInfo.Result.MarketID == maketId) returnList.Add(obj);
                        }
                        list = returnList;
                    }
                    var pagingObject = new PagingObject<List<ConditionOrder>> { Count = count, Data = list };
                    resultObject.Result = pagingObject;
                    resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                }
                else
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
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

        /*[WebMethod(Description = "Get list of condition orders", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string TestGetListConditionOrder(string accountNo, string side, string symbol,
            string fromTradedDate, string toTradedDate, string fromEffDate, string toEffDate, string fromEndDate,
            string toEndDate, int pageIndex, int pageSize)
        {
            return GetListConditionOrder(accountNo, side, symbol, new int[] {0, 1}, fromTradedDate, toTradedDate,
                                         fromEffDate, toEffDate, fromEndDate, toEndDate, 1, 10);
        }*/

        /// <summary>
        /// Get list of condition order details. 
        /// </summary>
        /// <param name="conditionOrderId">Condition order Id</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;List&lt;ConditionOrderDetail&gt;&gt</see> object contains returned code, returned message and 
        /// list of ConditionOrderDetail objects that contains condition order detail information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get order history", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListConditionOrderDetail(long conditionOrderId, string apiKey)
        {
            var resultObject = new ResultObject<List<ConditionOrderDetail>> { Result = null };
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    return Serializer.Serialize(resultObject);
                }
                else if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return
                    Serializer.Serialize(
                        new ResultObject<CommonEnums.RET_CODE>
                        {
                            Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                        });
                }
                var list = ConditionOrderDetailService.GetByConditionOrderId(conditionOrderId);
                if (list != null && list.Count > 0)
                {
                    var returnList = list.ToList();
                    resultObject.Result = returnList;
                    resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                }
                else
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
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

        /// <summary>
        /// Update expire condition orders and rejected condition orders.
        /// </summary>
        /// <returns>
        /// <para>Result of reseting condition orders.</para>
        /// </returns>
        [WebMethod(Description = "Update expire condition orders and rejected condition orders.")]
        public int ResetConditionOrder()
        {
            try
            {
                LogHandler.Log("Start Reseting condition orders", "ResetConditionOrder", TraceEventType.Information);
                ConditionOrderService.UpdateExpiredData(1);
                return (int)CommonEnums.RET_CODE.SUCCESS;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        /// <summary>
        /// Check is valid limit quantity advance order if not enough cash available
        /// </summary>
        /// <param name="side">The side.</param>
        /// <param name="subCustAccountId">The sub cust account id.</param>
        /// <param name="symbol">The symbol.</param>
        /// <returns>
        /// 	<c>true</c> if [is valid limit quantity advance order] [the specified side]; otherwise, <c>false</c>.
        /// </returns>
        [WebMethod(Description = "Count unfinished condition order", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string IsValidLimitQuantityAdvanceOrder(char side, string subCustAccountId, string symbol, string apiKey)
        {
            var resultObject = new ResultObject<bool>();
            try
            {
                resultObject.Result = false;
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    return Serializer.Serialize(resultObject);
                }
                else if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return
                    Serializer.Serialize(
                        new ResultObject<CommonEnums.RET_CODE>
                        {
                            Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                        });
                }
                resultObject.Result = ValidateServices.IsValidLimitQuantityAdvanceOrder(side, subCustAccountId, symbol);
                resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                resultObject.RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR;
                return Serializer.Serialize(resultObject);
            }
        }

        #endregion order
        #region Odd Lot Order
        /// <summary>
        /// Gets the odd lot info.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="accountType">Type of the account.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;List&lt;OddLotOrderInfo&gt;&gt;</see> object contains returned code, returned message and 
        /// list of OddLotOrderInfo objects.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Getting data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list odd lot info", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetOddLotOrderInfo(string accountNo, int accountType, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return Serializer.Serialize(
                            new ResultObject<List<OddLotOrderInfo>>
                            {
                                Result = new List<OddLotOrderInfo>(),
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return
                    Serializer.Serialize(
                        new ResultObject<CommonEnums.RET_CODE>
                        {
                            Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                        });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, accountNo))
                {
                    return Serializer.Serialize(
                            new ResultObject<List<OddLotOrderInfo>>
                            {
                                Result = new List<OddLotOrderInfo>(),
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                var listOddLotOrderInfo = ETradeServices.GetOddLotOrderInfo(accountNo, accountType);
                return Serializer.Serialize(listOddLotOrderInfo);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<List<OddLotOrderInfo>>
                            {
                                Result = new List<OddLotOrderInfo>(),
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
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
        /// <param name="pageIndex">Page index.</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;OddLotOrder&gt;&gt;&gt;</see> object contains returned code, returned message and 
        /// list of OddLotOrder objects that contains odd lot order information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=ERROR_INVALID_DATETIME: The date is invalid.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list odd lot order history", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListOddLotOrderHist(string secSymbol, string side, string fromDate, string toDate, string subCustAccountID, string market, int status, string brokerID, string note, int pageIndex, int pageSize, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return Serializer.Serialize(
                            new ResultObject<PagingObject<List<OddLotOrderInfo>>>
                            {
                                Result = new PagingObject<List<OddLotOrderInfo>>(),
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return
                    Serializer.Serialize(
                        new ResultObject<CommonEnums.RET_CODE>
                        {
                            Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                            ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                        });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, subCustAccountID))
                {
                    return Serializer.Serialize(
                            new ResultObject<PagingObject<List<OddLotOrderInfo>>>
                            {
                                Result = new PagingObject<List<OddLotOrderInfo>>(),
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                var listOddLotOrderHist = ETradeServices.GetListOddLotOrderHist(secSymbol, side, fromDate, toDate, subCustAccountID, market, status, brokerID, note, pageIndex, pageSize);
                return Serializer.Serialize(listOddLotOrderHist);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<PagingObject<List<OddLotOrderInfo>>>
                            {
                                Result = new PagingObject<List<OddLotOrderInfo>>(),
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
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
        /// <returns>
        /// List of odd lot orders
        /// </returns>
        [WebMethod(Description = "Get list odd lot order today", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListOddLotOrder(string secSymbol, string side, string subCustAccountID, string market, int status, string brokerID, string note, int pageIndex, int pageSize, string apiKey)
        {
            return GetListOddLotOrderHist(secSymbol, side, string.Empty, string.Empty, subCustAccountID, market, status,
                                          brokerID, note, pageIndex, pageSize, apiKey);
        }

        /// <summary>
        /// Cancel the odd lot order.
        /// </summary>
        /// <param name="id">The odd lot order id.</param>
        /// <param name="pin">Customer pin.</param>
        /// <param name="note">The note.</param>
        /// <returns>
        /// <para>Result of cancelling odd lot order.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=INCORRECT_PIN: The pin is incorrect.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=ERROR_CANNOT_CANCEL_ODD_LOT_ORDER: Cannot cancel odd lot order because it's in incorrect state.</para>
        /// <para>RET_CODE=SUCCESS: Cancel successfully.</para>
        /// <para>RET_CODE=FAIL: Fail to cancel odd lot order.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Cancel a odd lot order request", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int CancelOddLotOrder(long id, string pin, string note, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }
                if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return (int)CommonEnums.RET_CODE.INVAILID_API_KEY;
                }
                if (!PasswordHandlerMd5.Encrypt(pin).Equals(Session[CommonEnums.SESSION_KEY.PIN.ToString()]))
                {
                    return (int)CommonEnums.RET_CODE.INCORRECT_PIN;
                }
                return ETradeServices.CancelOddLotOrder(id, note);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
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
        /// <param name="pin">Pin</param>
        /// <returns>
        /// <para>
        /// Result of putting odd lot order.
        /// </para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=INCORRECT_PIN: The pin is incorrect.</para>
        /// <para>RET_CODE=FAIL: Putting order failed.</para>
        /// <para>RET_CODE=SUCCESS: Putting order successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Insert a odd lot order request", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int PutOddLotOrder(string secSymbol, string side, decimal price, long volume, string subCustAccountID, string market, string note, string pin, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }
                if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return (int)CommonEnums.RET_CODE.INVAILID_API_KEY;
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, subCustAccountID))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }
                if (!PasswordHandlerMd5.Encrypt(pin).Equals(Session[CommonEnums.SESSION_KEY.PIN.ToString()]))
                {
                    return (int)CommonEnums.RET_CODE.INCORRECT_PIN;
                }
                return ETradeServices.PutOddLotOrder(secSymbol, side, price, volume, subCustAccountID, market, note);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        #endregion
        
        #region Cash Transfer

        /// <summary>
        /// Cancels the cash transfer.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="pin">Customer's pin.</param>
        /// <param name="note">The note.</param>
        /// <returns>
        /// <para>Result of cancelling cash transfer.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=INCORRECT_PIN: The pin is incorrect.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=ERROR_CANNOT_CANCEL_CASH_TRANSFER: Cannot cancel cash transfer because it's in incorrect state.</para>
        /// <para>RET_CODE=SUCCESS: Cancel successfully.</para>
        /// <para>RET_CODE=FAIL: Fail to cancel cash transfer.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Cancel a cash transfer order, return ret code", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int CancelCashTransfer(long id, string pin, string note, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }
                if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return (int)CommonEnums.RET_CODE.INVAILID_API_KEY;
                }
                if (!PasswordHandlerMd5.Encrypt(pin).Equals(Session[CommonEnums.SESSION_KEY.PIN.ToString()]))
                {
                    return (int)CommonEnums.RET_CODE.INCORRECT_PIN;
                }
                return ETradeServices.CancelCashTransfer(id, note);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        /// <summary>
        /// Cashes the trans order hist.
        /// </summary>
        /// <param name="sourceAccountID">The source account ID.</param>
        /// <param name="destAccountID">The dest account ID.</param>
        /// <param name="transType">Type of the trans.</param>
        /// <param name="status">The status.</param>
        /// <param name="note">The note.</param>
        /// <param name="brokerID">The broker ID.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;CashTransfer&gt;&gt;&gt;</see> object contains returned code, returned message and 
        /// list of CashTransfer objects that contains cash transfer information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=ERROR_INVALID_DATETIME: The sent date is invalid.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list paged cash tranfer today,return string serialized of ResultObject PagingObject List CashTransfer", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListCashTransOrder(string sourceAccountID, string destAccountID, int transType, int status, string note, string brokerID, int pageIndex, int pageSize, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<PagingObject<List<CashTransfer>>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    Serializer.Serialize(
                            new ResultObject<PagingObject<List<CashTransfer>>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                                ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                            });
                }
                var resultObject = ETradeServices.GetListCashTransOrderHist(sourceAccountID, destAccountID,
                                                                                     string.Empty, string.Empty, transType, status,
                                                                                     note, brokerID, pageIndex, pageSize);

                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {

                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<PagingObject<List<CashTransfer>>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }
        /// <summary>
        /// Get transfered cash order history data.
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
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=ERROR_INVALID_DATETIME: The sent date is invalid.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list paged cash tranfer history,return string serialized of ResultObject PagingObject List CashTransfer", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListCashTransOrderHist(string sourceAccountID, string destAccountID, string fromDate, string toDate, int transType, int status, string note, string brokerID, int pageIndex, int pageSize, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<PagingObject<List<CashTransfer>>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    Serializer.Serialize(
                            new ResultObject<PagingObject<List<CashTransfer>>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                                ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                            });
                }
                var resultObject = ETradeServices.GetListCashTransOrderHist(sourceAccountID, destAccountID,
                                                                                     fromDate, toDate, transType, status,
                                                                                     note, brokerID, pageIndex, pageSize);

                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {

                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<PagingObject<List<CashTransfer>>>()
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }

        /// <summary>
        /// Puts the cash trans order.
        /// </summary>
        /// <param name="sourceAccountID">The source account ID.</param>
        /// <param name="destAccountID">The destination account ID.</param>
        /// <param name="Pin">Customer's pin.</param>
        /// <param name="requestAmt">The request amount.</param>
        /// <param name="transType">Type of the transaction.</param>
        /// <param name="note">The note.</param>
        /// <returns>
        /// <para>
        /// Result of putting cash transfer order.
        /// </para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=INCORRECT_PIN: The pin is incorrect.</para>
        /// <para>RET_CODE=ERROR_ACCOUNT: The account does not exist.</para>
        /// <para>RET_CODE=NOT_ALLOW: Customer is not allowed to do this action.</para>
        /// <para>RET_CODE=ERROR_REQUEST_AMOUNT: The amount is incorrect.</para>
        /// <para>RET_CODE=ERROR_INVALID_WITHDRAWAL: The withdrawal amount is incorrect.</para>
        /// <para>RET_CODE=ERROR_NOT_CASH_AVAILABLE: There is no available cash.</para>
        /// <para>RET_CODE=FAIL: Putting order failed.</para>
        /// <para>RET_CODE=SUCCESS: Putting order successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Put cash transfer request, return int RET CODE", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //note: send sms function has been commented by Vu.Nguyen for testing
        public int PutCashTransOrder(string sourceAccountID, string destAccountID, string bankName, string branchName, string Pin, decimal requestAmt, int transType, string note, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }
                if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return (int)CommonEnums.RET_CODE.INVAILID_API_KEY;
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, sourceAccountID))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }

                if (!PasswordHandlerMd5.Encrypt(Pin).Equals(Session[CommonEnums.SESSION_KEY.PIN.ToString()]))
                    return (int)CommonEnums.RET_CODE.INCORRECT_PIN;

                var subCustAccountList = GetListSubCustAccountFromSession();

                return ETradeServices.PutCashTransOrder(subCustAccountList, sourceAccountID, destAccountID, bankName, branchName, requestAmt, transType, note);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        /// <summary>
        /// Get list of sub customer account from session
        /// </summary>
        /// <returns>List of sub customer account information from session.</returns>
        private List<SubCustAccount> GetListSubCustAccountFromSession()
        {
            var subCustAccountIdList = (List<string>)Session[CommonEnums.SESSION_KEY.LIST_SUB_ACCOUNTS.ToString()];
            var subCustAccountList = new List<SubCustAccount>();
            if (subCustAccountIdList != null)
            {
                foreach (var subCustAccountId in subCustAccountIdList)
                {
                    var subCustAccount =
                        (SubCustAccount)Session[subCustAccountId + CommonEnums.SESSION_KEY.TRADING_ACCOUNT];
                    if (subCustAccount != null)
                    {
                        subCustAccountList.Add(subCustAccount);
                    }
                }
            }
            return subCustAccountList;
        }

        /// <summary>
        /// Gets the cash transfer info.
        /// </summary>
        /// <param name="subAccountId">The sub Account Id.</param>
        /// <param name="accountType">Type of the account.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;CashTransferInfo;&gt;</see> object contains returned code, returned message and 
        /// CashTransferInfo object that contains cash transfer information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get cash transfer info, return ResultObject<CashTransferInfo>", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetCashTransferInfo(string subAccountId, int accountType, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<CashTransferInfo>
                            {
                                Result = new CashTransferInfo(),
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    Serializer.Serialize(
                            new ResultObject<PagingObject<List<CashTransfer>>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                                ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                            });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, subAccountId))
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<CashTransferInfo>
                            {
                                Result = new CashTransferInfo(),
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }

                var cashTransferInfo = ETradeServices.GetCashTransferInfo(subAccountId, accountType);
                return Serializer.Serialize(cashTransferInfo);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<CashTransferInfo>
                            {
                                Result = new CashTransferInfo(),
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }
        #endregion
        #region actual trade

        /// <summary>
        /// Gets the actual trade.
        /// </summary>
        /// <param name="subAccountNo">The sub account no.</param>
        /// <param name="fromDate">From date.Format (YYYYMMDD)</param>
        /// <param name="toDate">To date.Format (YYYYYMMDD)</param>
        /// <param name="symbol">
        /// The symbol.
        /// "all" or " ": get all symbol.
        /// </param>
        /// <param name="pageNumber">The page number. if pageNumber = 0 it will return all results</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;ActualTrade&gt;&gt;&gt;</see> object contains returned code, returned message and 
        /// list of ActualTrade objects that contains trade information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=FAIL: Failed to get data.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get actual trading. Return a ResultObject<PagingObject<List<ActualTrade>>>", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetActualTrade(string subAccountNo, string fromDate, string toDate, string symbol, string side, int pageNumber, int pageSize, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<PagingObject<List<ActualTrade>>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    Serializer.Serialize(
                            new ResultObject<PagingObject<List<CashTransfer>>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                                ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                            });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, subAccountNo))
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<PagingObject<List<ActualTrade>>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                var resultObject = ETradeServices.GetActualTrade(
                    subAccountNo, fromDate, toDate, symbol, pageNumber, pageSize);
                var listActualTrade = new List<ActualTrade>();
                if (side.Trim() != "-1")
                {
                    foreach (var obj in resultObject.Result.Data)
                    {
                        if (obj.Side == side)
                        {
                            listActualTrade.Add(obj);
                        }
                    }
                    resultObject.Result.Data = listActualTrade;
                }
                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<PagingObject<List<ActualTrade>>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }

        #endregion
        #region cash advance

        /// <summary>
        /// Gets the advance history.
        /// </summary>
        /// <param name="subAccountNo">The account no. Such as 0088661, 0088666</param>
        /// <param name="fromAdvanceDate">From advance date.Format(YYYYMMDD)</param>
        /// <param name="toAdvanceDate">To advance date.Format(YYYYMMDD)</param>
        /// <param name="fromSellDate">From sell date.Format(YYYYMMDD)</param>
        /// <param name="toSellDate">To sell date.Format(YYYYMMDD)</param>
        /// <param name="advanceStatus">The advance status</param>
        /// <param name="contractNo">The contract number</param>
        /// <param name="pageIndex">The page index. If pageIndex = 0 then it will return all results.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;CashAdvance&gt;&gt;&gt;</see> object contains returned code, returned message and 
        /// list of CashAdvance objects that contains cash advance information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=FAIL: Failed to get data.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get advance history from Core, return ResultObject<PagingObject<List<CashAdvance>>>", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetAdvanceHistory(
            string subAccountNo,
            string fromAdvanceDate,
            string toAdvanceDate,
            string fromSellDate,
            string toSellDate,
            int advanceStatus,
            string contractNo,
            int pageIndex,
            int pageSize, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<PagingObject<List<CashAdvance>>>
                            {
                                Result = new PagingObject<List<CashAdvance>>(),
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                else if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<CommonEnums.RET_CODE>
                            {
                                Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                                RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                                ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                            });
                }
                if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    Serializer.Serialize(
                            new ResultObject<PagingObject<List<CashTransfer>>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                                ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                            });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, subAccountNo))
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<PagingObject<List<CashAdvance>>>
                            {
                                Result = new PagingObject<List<CashAdvance>>(),
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }

                var resultObject = ETradeServices.GetAdvanceHistory(
                    subAccountNo,
                    fromAdvanceDate,
                    toAdvanceDate,
                    fromSellDate,
                    toSellDate,
                    advanceStatus,
                    contractNo,
                    pageIndex,
                    pageSize);

                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<PagingObject<List<CashAdvance>>>
                            {
                                Result = new PagingObject<List<CashAdvance>>(),
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }

        /// <summary>
        /// Gets the advance info that support to create the request to cash advance.
        /// </summary>
        /// <param name="accountNo">The account no. such as 0088661, 0088666</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;AdvanceInfo&gt;, AdvanceInfo, AdvanceInfo&gt;&gt;</see> object contains returned code, returned message and 
        /// list of AdvanceInfo objects that contains cash advance information and total information for each page/all.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=FAIL: Failed to get data.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get advance information, return ResultObject<PagingObject<List<AdvanceInfo>, AdvanceInfo, AdvanceInfo>>")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetAdvanceInfo(string accountNo)
        {
            try
            {
                var resultObject = ETradeServices.GetAdvanceInfo(accountNo);
                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<PagingObject<List<AdvanceInfo>, AdvanceInfo, AdvanceInfo>>
                            {
                                Result = new PagingObject<List<AdvanceInfo>, AdvanceInfo, AdvanceInfo>(),
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }

        /// <summary>
        /// News the cash advance.
        /// </summary>
        /// <param name="accountNo">The sub account no.</param>
        /// <param name="pin">Pin</param>
        /// <param name="sellAmt">The sellAmt is the mount that was sold after fee/vat. </param>
        /// <param name="cashAdvance">The mount you would like to cash advance.</param>
        /// <param name="maxCanAdvance">The maximum mount that you can advance.</param>
        /// <param name="tradeDate">The sell date</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;int&gt;</see> object contains returned code, returned message and 
        /// contract no.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=ERROR_INVALID_CASH_ADVANCE: Cash advance is invalid.</para>
        /// <para>RET_CODE=ERROR_CANNOT_ADVANCE_OUTOF_TIME: Not time for cash advance.</para>
        /// <para>RET_CODE=ERROR_CANNOT_ADVANCE_IN_DUE_DATE: Cash advance due date is invalid.</para>
        /// <para>RET_CODE=ERROR_NOT_ENOUGH_CASH_TO_ADVANCE: There is not enough cash to request cash advance.</para>
        /// <para>RET_CODE=SUCCESS: Putting order successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "New a cash advance, return ResultObject<string>", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string NewCashAdvance(string accountNo, string pin,
            decimal sellAmt,
            decimal cashAdvance,
            decimal maxCanAdvance,
            string tradeDate, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return Serializer.Serialize(
                            new ResultObject<string>
                            {
                                Result = string.Empty,
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    Serializer.Serialize(
                            new ResultObject<PagingObject<List<CashTransfer>>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                                ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                            });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, accountNo))
                {
                    return Serializer.Serialize(
                        new ResultObject<string>
                        {
                            Result = string.Empty,
                            RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                            ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                        });
                }
                if (!PasswordHandlerMd5.Encrypt(pin).Equals(Session[CommonEnums.SESSION_KEY.PIN.ToString()]))
                {
                    return Serializer.Serialize(
                            new ResultObject<string>
                            {
                                Result = string.Empty,
                                RetCode = CommonEnums.RET_CODE.INCORRECT_PIN,
                                ErrorMessage = CommonEnums.RET_CODE.INCORRECT_PIN.ToString()
                            });
                }

                var resultObject = ETradeServices.NewCashAdvance(
                    accountNo, sellAmt, cashAdvance, maxCanAdvance, tradeDate);

                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<string>
                            {
                                Result = string.Empty,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });

            }
        }

        /// <summary>
        /// Cancels the cash advance.
        /// </summary>
        /// <param name="subCustAccountId">The sub account no.</param>
        /// <param name="pin">Customer's pin.</param>
        /// <param name="contractNo">The contract no that was created by NewCashAdvance APIs..</param>
        /// <returns>
        /// <para>Result of cancelling cash advance.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=INCORRECT_PIN: The pin is incorrect.</para>
        /// <para>RET_CODE=ERROR_CANNOT_CANCEL_ADVANCE_CANCELED: Cannot cancel a canceled cash advance.</para>
        /// <para>RET_CODE=ERROR_CANNOT_CANCEL_ADVANCE_REFJECTED: Cannot cancel a rejected cash advance.</para>
        /// <para>RET_CODE=ERROR_CANNOT_CANCEL_IN_PROCESSING: Cannot cancel a cash advance which is in processing state.</para>
        /// <para>RET_CODE=ERROR_CANNOT_CANCEL_ADVANCE_FINISHED: Cannot cancel a finished cash advance.</para>
        /// <para>RET_CODE=SUCCESS: Cancel successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Cancel a cash advance, return ResultObject of RET_CODE.", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int CancelCashAdvance(string subCustAccountId, string pin, string contractNo, string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }
                if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    Serializer.Serialize(
                            new ResultObject<PagingObject<List<CashTransfer>>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                                ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                            });
                }

                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, subCustAccountId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }

                if (!PasswordHandlerMd5.Encrypt(pin).Equals(Session[CommonEnums.SESSION_KEY.PIN.ToString()]))
                {
                    return (int)CommonEnums.RET_CODE.INCORRECT_PIN;
                }
                var retCode = ETradeServices.CancelAdvance(subCustAccountId, contractNo);
                return (int)retCode;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
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
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;CashAdvance&gt;&gt;&gt;</see> object contains returned code, returned message and 
        /// list of CashAdvance objects that contains cash advance information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=ERROR_INVALID_DATETIME: The date is incorrect.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get cash advance status", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetCashAdvanceStatus(string accountNo, string fromDate, string toDate, int status, int pageIndex, int pageSize, string apiKey)
        {
            try
            {
                var resultObject = new ResultObject<PagingObject<List<ETradeFinance.Entities.CashAdvance>>>();
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    return Serializer.Serialize(resultObject);
                }
                if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    Serializer.Serialize(
                            new ResultObject<PagingObject<List<CashTransfer>>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                                ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                            });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, accountNo))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    return Serializer.Serialize(resultObject);
                }
                resultObject = ETradeServices.GetCashAdvanceStatus(accountNo, fromDate, toDate, status, pageIndex, pageSize);
                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<PagingObject<List<ETradeFinance.Entities.CashAdvance>>>
                            {
                                Result = new PagingObject<List<ETradeFinance.Entities.CashAdvance>>(),
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }

        /// <summary>
        /// Gets the advance fee.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="sellAmt">The sell amt.</param>
        /// <param name="advanceDays">The advance days.</param>
        /// <returns></returns>
        [WebMethod(Description = "Get advance fee", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetAdvanceFee(string accountNo, decimal sellAmt, int advanceDays, string apiKey)
        {
            try
            {
                var resultObject = new ResultObject<decimal>();
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    return Serializer.Serialize(resultObject);
                }
                if (string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    Serializer.Serialize(
                            new ResultObject<PagingObject<List<CashTransfer>>>
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                                ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                            });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, accountNo))
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NOT_LOGIN;
                    return Serializer.Serialize(resultObject);
                }
                decimal advanceFee = ETradeServices.GetAdvanceFee(sellAmt, advanceDays);
                resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                resultObject.Result = advanceFee;
                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<decimal>
                            {
                                Result = 0,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }
        #endregion   
        #region Additional methods for mobile app
        [WebMethod]
        public string GetCurrentTime(string languageId)
        {
            string result = "";
            var currentDate = DateTime.Now;
            var dayOfWeek = (int)currentDate.DayOfWeek;
            string txtDayOfWeek = "";
            switch (dayOfWeek)
            {
                case 0:
                    if (languageId == Constants.ENGLISH_LANGUAGE_ID)
                    {
                        txtDayOfWeek = "Sunday";
                    }
                    else
                    {
                        txtDayOfWeek = "Chủ nhật";
                    }
                    break;
                case 1:
                    if (languageId == Constants.ENGLISH_LANGUAGE_ID)
                    {
                        txtDayOfWeek = "Monday";
                    }
                    else
                    {
                        txtDayOfWeek = "Thứ 2";
                    }
                    break;
                case 2:
                    if (languageId == Constants.ENGLISH_LANGUAGE_ID)
                    {
                        txtDayOfWeek = "Tuesday";
                    }
                    else
                    {
                        txtDayOfWeek = "Thứ 3";
                    }
                    break;
                case 3:
                    if (languageId == Constants.ENGLISH_LANGUAGE_ID)
                    {
                        txtDayOfWeek = "Wednesday";
                    }
                    else
                    {
                        txtDayOfWeek = "Thứ 4";
                    }
                    break;
                case 4:
                    if (languageId == Constants.ENGLISH_LANGUAGE_ID)
                    {
                        txtDayOfWeek = "Thursday";
                    }
                    else
                    {
                        txtDayOfWeek = "Thứ 5";
                    }
                    break;
                case 5:
                    if (languageId == Constants.ENGLISH_LANGUAGE_ID)
                    {
                        txtDayOfWeek = "Friday";
                    }
                    else
                    {
                        txtDayOfWeek = "Thứ 6";
                    }
                    break;
                case 6:
                    if (languageId == Constants.ENGLISH_LANGUAGE_ID)
                    {
                        txtDayOfWeek = "Saturday";
                    }
                    else
                    {
                        txtDayOfWeek = "Thứ 7";
                    }
                    break;
            }
            if (languageId == Constants.ENGLISH_LANGUAGE_ID)
            {
                result = "<b><font color='#FFFF00'>" + string.Format("{0:HH:mm:ss}", currentDate) + "</font>, " + txtDayOfWeek + " " + string.Format("{0:MM-dd-yyyy}", currentDate) + "</b>";
            }
            else result = "<b><font color='#FFFF00'>" + string.Format("{0:HH:mm:ss}", currentDate) + "</font>, " + txtDayOfWeek + " " + string.Format("{0:dd-MM-yyyy}", currentDate) + "</b>";
            return result;
        }
        [WebMethod(Description = "Get list company autocomplete info, using for autocomplete on mobile coltrol", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string MGetListCompanyAutoComplete(string apiKey,string langId)
        {            
            if (Session == null||string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
            {
                return
                Serializer.Serialize(
                    new ResultObject<CommonEnums.RET_CODE>
                    {
                        Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                        RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                        ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                    });
            }
            var listComInfoStr  = rtDataService.GetListCompany(0,langId);
            var listComObj = Serializer.Deserialize<ETradeCommon.ResultObject<List<RTDataServices.Entities.CompanyInfo>>>(listComInfoStr);
            if (listComObj.RetCode == CommonEnums.RET_CODE.SUCCESS)
            {
                var result = new List<string>();
                var listCom = listComObj.Result;
                foreach (var item in listCom)
                {
                    string resultItem = item.Code + " " + item.FullName;
                    result.Add(resultItem);
                }
                return
                Serializer.Serialize(
                    new ResultObject<List<string>>
                    {
                        Result = result,
                        RetCode = CommonEnums.RET_CODE.SUCCESS,
                        ErrorMessage = string.Empty
                    });
            }
            else
            {
                return
                Serializer.Serialize(
                    new ResultObject<CommonEnums.RET_CODE>
                    {
                        Result = CommonEnums.RET_CODE.NO_EXISTED_DATA,
                        RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA,
                        ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString()
                    });
            }
        }
        [WebMethod(Description = "Get data for chart on mobile application", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string MGetDataForChart(string stockSymbol,string marketId,string apiKey)
        {
            var charData = new Entities.MChartData();
            if (Session == null || string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
            {
                return
                Serializer.Serialize(
                    new ResultObject<CommonEnums.RET_CODE>
                    {
                        Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                        RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                        ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                    });
            }
            
            try
            {
                //get data from store
                var listMDataTransactionRow = new List<Entities.MDataTransactionRow>();
                using (
                    SqlConnection connection =
                        new SqlConnection(
                            System.Configuration.ConfigurationManager.ConnectionStrings["OTSPriceBoardConnectionString"]
                                .ConnectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "PriceBoard_GetTransaction";
                    command.Parameters.AddWithValue("@MarketId", marketId);
                    command.Parameters.AddWithValue("@Symbol", stockSymbol);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var mDataTransactionRow = new Entities.MDataTransactionRow(reader);
                        listMDataTransactionRow.Add(mDataTransactionRow);
                    }
                }
                //get DetailMatched
                var listObj =
                    (from Entities.MDataTransactionRow rows in listMDataTransactionRow
                     where rows.StockSymbol.Trim() == stockSymbol.Trim()
                     group rows by rows.Price
                         into p
                         select new { Price = p.Key, Volume = p.Sum(rows => rows.Vol) }
                    ).ToList();
                List<Entities.MMainMatchedPricesInfo> matchedList = listObj.Select(variable => new Entities.MMainMatchedPricesInfo
                {
                    Price = variable.Price / 1000,
                    Volume = variable.Volume
                }).ToList();
                charData.DetailMatched = matchedList;
                //get prioClosePrice
                float priorClosePrice = 0;
                using (
                    SqlConnection connection =
                        new SqlConnection(
                            System.Configuration.ConfigurationManager.ConnectionStrings["OTSPriceBoardConnectionString"]
                                .ConnectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "PriceBoard_GetPriorClosePrice";
                    command.Parameters.AddWithValue("@MarketId", marketId);
                    command.Parameters.AddWithValue("@Symbol", stockSymbol);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        priorClosePrice = (reader["PriorClosePrice"] != DBNull.Value) ? float.Parse(reader["PriorClosePrice"].ToString()) : 0;
                    }
                }
                //get Deal Matched
                var listDetailMatched = (from rows in listMDataTransactionRow
                                         where rows.StockSymbol.Trim() == stockSymbol.Trim()
                                         select rows
                                        ).ToList();
                List<Entities.MDealDetail> dealDetails = listDetailMatched.Select(p => new Entities.MDealDetail
                {
                    Changed =
                        Math.Round(
                            double.Parse(
                                (p.Price / 1000 -
                                 priorClosePrice).
                                    ToString()), 2),
                    Time = p.Time,
                    Val = p.Val,
                    Vol = p.Vol,
                    Price = p.Price / 1000,
                }).ToList();
                charData.DealDetail = dealDetails;
                charData.PriorClosePrice = priorClosePrice;
                return Serializer.Serialize(
                    new ResultObject<Entities.MChartData>
                    {
                        Result = charData,
                        RetCode = CommonEnums.RET_CODE.SUCCESS,
                        ErrorMessage = string.Empty
                    });
            }
            catch (Exception ex)
            {
                LogHandler.Log("page:MEtradeWebService, " + ex.Message, "MGetDataForChart", TraceEventType.Error);
                return
                Serializer.Serialize(
                    new ResultObject<CommonEnums.RET_CODE>
                    {
                        Result = CommonEnums.RET_CODE.SYSTEM_ERROR,
                        RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                        ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                    });
            }            
        }
        [WebMethod(Description = "Get news data for mobile application", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string MGetNews(string langId,string apiKey)
        {
            if (Session == null || string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
            {
                return
                Serializer.Serialize(
                    new ResultObject<CommonEnums.RET_CODE>
                    {
                        Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                        RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                        ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                    });
            }
            try
            {
                List<Entities.MNews> listNews = new List<Entities.MNews>();
                using (
                        SqlConnection connection =
                            new SqlConnection(
                                System.Configuration.ConfigurationManager.ConnectionStrings["ETradeWebConnectionString"]
                                    .ConnectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "News_GetLatest";
                    command.Parameters.AddWithValue("@LanguageId", langId);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Entities.MNews news = new Entities.MNews();
                        news.Id = (reader["Id"] != DBNull.Value) ? (reader["Id"].ToString()): "";
                        news.Title = (reader["Title"] != DBNull.Value) ? ((string)reader["Title"]) : "";
                        //news.Description = (reader["Description"] != DBNull.Value) ? ((string)reader["Description"]) : "";
                        //news.Content = (reader["Content"] != DBNull.Value) ? ((string)reader["Content"]) : "";
                        if (langId.CompareTo("vi") == 0)
                        {
                            news.ArticleModifiedDate = (reader["ArticleModifiedDate"] != DBNull.Value) ? String.Format("{0:d/M/yyyy HH:mm:ss}", reader["ArticleModifiedDate"]) : "";
                        }
                        else
                        {
                            news.ArticleModifiedDate = (reader["ArticleModifiedDate"] != DBNull.Value) ? String.Format("{0:M/d/yyyy HH:mm:ss}", reader["ArticleModifiedDate"]) : "";
                        }
                        listNews.Add(news);
                    }
                }
                return Serializer.Serialize(
                    new ResultObject<List<Entities.MNews>>
                    {
                        Result = listNews,
                        RetCode = CommonEnums.RET_CODE.SUCCESS
                    });
            }
            catch(Exception ex)
            {
                LogHandler.Log("page:MEtradeWebService, " + ex.Message, "MGetNews", TraceEventType.Error);
                return
                Serializer.Serialize(
                    new ResultObject<CommonEnums.RET_CODE>
                    {
                        Result = CommonEnums.RET_CODE.SYSTEM_ERROR,
                        RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                        ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                    });
            }
        }
        [WebMethod(Description = "Get news detail data for mobile application", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string MGetNewsDetail(string id,string langId,string apiKey)
        {
            if (Session == null || string.IsNullOrEmpty(apiKey) || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
            {
                return
                Serializer.Serialize(
                    new ResultObject<CommonEnums.RET_CODE>
                    {
                        Result = CommonEnums.RET_CODE.INVAILID_API_KEY,
                        RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY,
                        ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString()
                    });
            }
            try
            {
                var news = new Entities.MNews();
                using (
                        SqlConnection connection =
                            new SqlConnection(
                                System.Configuration.ConfigurationManager.ConnectionStrings["ETradeWebConnectionString"]
                                    .ConnectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "News_GetById";
                    command.Parameters.AddWithValue("@ArticleId", id);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        news.Title = (reader["Title"] != DBNull.Value) ? ((string)reader["Title"]) : "";
                        news.Description = (reader["Description"] != DBNull.Value) ? ((string)reader["Description"]) : "";
                        news.Content = (reader["Content"] != DBNull.Value) ? ((string)reader["Content"]) : "";
                        if (langId.CompareTo("vi") == 0)
                        {
                            news.ArticleModifiedDate = (reader["ArticleModifiedDate"] != DBNull.Value) ? String.Format("{0:d/M/yyyy HH:mm:ss}", reader["ArticleModifiedDate"]) : "";
                        }
                        else
                        {
                            news.ArticleModifiedDate = (reader["ArticleModifiedDate"] != DBNull.Value) ? String.Format("{0:M/d/yyyy HH:mm:ss}", reader["ArticleModifiedDate"]) : "";
                        }
                    }
                }
                return Serializer.Serialize(
                    new ResultObject<Entities.MNews>
                    {
                        Result = news,
                        RetCode = CommonEnums.RET_CODE.SUCCESS
                    });
            }
            catch (Exception ex)
            {
                LogHandler.Log("page:MEtradeWebService, " + ex.Message, "MGetNews", TraceEventType.Error);
                return
                Serializer.Serialize(
                    new ResultObject<CommonEnums.RET_CODE>
                    {
                        Result = CommonEnums.RET_CODE.SYSTEM_ERROR,
                        RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                        ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                    });
            }
        }
        #endregion
        #region XR Order

        /// <summary>
        /// Gets the list buy right.
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// 	<para>A <see cref="ResultObject{T}">ResultObject&lt;PagingObject&lt;List&lt;AccountManager.Entities.BuyRight&gt;&gt;&gt;</see> object contains returned code, returned message and
        /// list of BuyRignt objects that contains buy right information.</para>
        /// 	<para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// 	<para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// 	<para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list buy right ", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListBuyRight(string subAccountId, int pageIndex, int pageSize,string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return Serializer.Serialize(new ResultObject<PagingObject<List<BuyRight>>>
                    {
                        Result = null,
                        ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString(),
                        RetCode = CommonEnums.RET_CODE.NOT_LOGIN
                    });
                }
                else if (Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey)!=0)
                {
                    return Serializer.Serialize(new ResultObject<PagingObject<List<BuyRight>>>
                    {
                        Result = null,
                        ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString(),
                        RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY
                    });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, subAccountId))
                {
                    return Serializer.Serialize(new ResultObject<PagingObject<List<BuyRight>>>
                    {
                        Result = null,
                        ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString(),
                        RetCode = CommonEnums.RET_CODE.NOT_LOGIN
                    });
                }
                var resultObject = new ResultObject<PagingObject<List<BuyRight>>>();
                resultObject.Result = ETradeServices.GetListBuyRight(subAccountId, string.Empty, pageIndex, pageSize);
                if (resultObject.Result.Count == 0 || resultObject.Result.Data == null)
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
                    resultObject.ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString();
                }
                else
                {
                    resultObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                    resultObject.ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString();
                }
                return Serializer.Serialize(resultObject);

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(new ResultObject<PagingObject<List<AccountManager.Entities.BuyRight>>>
                {
                    Result = null,
                    ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString(),
                    RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR
                });
            }
        }

        /// <summary>
        /// Puts the XR order.
        /// </summary>
        /// <param name="subAccountId">The sub account ID.</param>
        /// <param name="pin">The pin.</param>
        /// <param name="secSymbol">The security symbol.</param>
        /// <param name="market">The market.</param>
        /// <param name="requestVol">The request vol.</param>
        /// <param name="note">The note.</param>
        /// <returns>
        /// 	<para>Result of putting XR order.</para>
        /// 	<para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// 	<para>RET_CODE=INCORRECT_PIN: The pin is incorrect.</para>
        /// 	<para>RET_CODE=ERROR_REQUEST_VOLUME_BUY_RIGHT: Requested volume is incorrect.</para>
        /// 	<para>RET_CODE=ERROR_OVER_REQUEST_CAN_BUY_RIGHT: Requested volume is higher than allowed volume.</para>
        /// 	<para>RET_CODE=ERROR_NOT_EXIST_BUY_RIGHT: There is no buy right data.</para>
        /// 	<para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// 	<para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Put a XR Order. Return ret code", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int PutXROrder(string subAccountId, string pin, string secSymbol, char market, long requestVol, string note,string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }
                else if (Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return (int)CommonEnums.RET_CODE.INVAILID_API_KEY;
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, subAccountId))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }
                if (!PasswordHandlerMd5.Encrypt(pin).Equals(Session[CommonEnums.SESSION_KEY.PIN.ToString()]))
                    return (int)CommonEnums.RET_CODE.INCORRECT_PIN;
                return ETradeServices.PutXROrder(subAccountId, secSymbol, market, requestVol, note);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        /// <summary>
        /// Cancels the XR order.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="pin">Customer pin.</param>
        /// <param name="note">The note.</param>
        /// <returns>
        /// <para>Result of cancelling XR order.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=INCORRECT_PIN: The pin is incorrect.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=ERROR_CANNOT_CANCEL_XRORDER: Cannot cancel XR order because it's in incorrect state.</para>
        /// <para>RET_CODE=SUCCESS: Cancel successfully.</para>
        /// <para>RET_CODE=FAIL: Fail to cancel odd lot order.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Cancel a xr order. Return ret code", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int CancelXROrder(long id, string pin, string note,string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }
                else if (Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return (int)CommonEnums.RET_CODE.INVAILID_API_KEY;
                }
                if (!PasswordHandlerMd5.Encrypt(pin).Equals(Session[CommonEnums.SESSION_KEY.PIN.ToString()]))
                {
                    return (int)CommonEnums.RET_CODE.INCORRECT_PIN;
                }
                return ETradeServices.CancelXROrder(id, note);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
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
        /// <para>RET_CODE=NOT_LOGIN: User has not logged in.</para>
        /// <para>RET_CODE=ERROR_INVALID_DATETIME: The date is invalid.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: The is no data.</para>
        /// <para>RET_CODE=SUCCESS: Create data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list xr order history. Return ResultObject PagingObject List XR ORder", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListXROrderHist(long id, string subAccountId, string secSymbol, string market, string fromDate, string toDate, int status, string brokerID, string note, int pageIndex, int pageSize,string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return Serializer.Serialize(new ResultObject<PagingObject<List<XrOrders>>>()
                    {
                        Result = null,
                        ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString(),
                        RetCode = CommonEnums.RET_CODE.NOT_LOGIN
                    });
                }
                else if (Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return Serializer.Serialize(new ResultObject<PagingObject<List<BuyRight>>>
                    {
                        Result = null,
                        ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString(),
                        RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY
                    });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, subAccountId))
                {
                    return Serializer.Serialize(new ResultObject<PagingObject<List<XrOrders>>>()
                    {
                        Result = null,
                        ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString(),
                        RetCode = CommonEnums.RET_CODE.NOT_LOGIN
                    });
                }              
                var resultObject = ETradeServices.GetListXROrderHist(id, subAccountId, secSymbol, market, fromDate,
                                                                     toDate, status, brokerID, note, pageIndex, pageSize);
                return Serializer.Serialize(resultObject);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(new ResultObject<PagingObject<List<XrOrders>>>
                {
                    Result = null,
                    ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString(),
                    RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR
                });
            }
        }

        #endregion
        #region Stock Transfer

        /// <summary>
        /// Cancels the stock transfer.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="pin">Customer pin.</param>
        /// <param name="note">The note.</param>
        /// <returns>
        /// <para>Result of cancelling stock transfer.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=INCORRECT_PIN: The pin is incorrect.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=ERROR_CANNOT_CANCEL_STOCK_TRANSFER: Cannot cancel stock transfer because it's in incorrect state.</para>
        /// <para>RET_CODE=SUCCESS: Cancel successfully.</para>
        /// <para>RET_CODE=FAIL: Fail to cancel odd lot order.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Cancel a stock transfer order, return ret code", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int CancelStockTransfer(long id, string pin, string note,string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }
                else if (Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return (int)CommonEnums.RET_CODE.INVAILID_API_KEY;
                }
                if (!PasswordHandlerMd5.Encrypt(pin).Equals(Session[CommonEnums.SESSION_KEY.PIN.ToString()]))
                {
                    return (int)CommonEnums.RET_CODE.INCORRECT_PIN;
                }
                return ETradeServices.CancelStockTransfer(id, note);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        /// <summary>
        /// Puts the stock trans order.
        /// </summary>
        /// <param name="sourceAccountID">The source account ID.</param>
        /// <param name="destAccountID">The dest account ID.</param>
        /// <param name="pin"></param>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <param name="requestAmt">The request amt.</param>
        /// <param name="transType">Type of the trans.</param>
        /// <param name="note">The note.</param>
        /// <returns>
        /// <para>Result of putting stock transfer order.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=INCORRECT_PIN: The pin is incorrect.</para>
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
        [WebMethod(Description = "put stock transfer order,return ret code", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int PutStockTransOrder(string sourceAccountID, string destAccountID, string pin, string secSymbol, long requestAmt, int transType, string note,string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }
                else if (Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return (int)CommonEnums.RET_CODE.INVAILID_API_KEY;
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, sourceAccountID))
                {
                    return (int)CommonEnums.RET_CODE.NOT_LOGIN;
                }
                if (!PasswordHandlerMd5.Encrypt(pin).Equals(Session[CommonEnums.SESSION_KEY.PIN.ToString()]))
                    return (int)CommonEnums.RET_CODE.INCORRECT_PIN;
                var subCustAccountList = GetListSubCustAccountFromSession();

                return ETradeServices.PutStockTransOrder(subCustAccountList, sourceAccountID, destAccountID, secSymbol, requestAmt, transType, note);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return (int)CommonEnums.RET_CODE.SYSTEM_ERROR;
            }
        }

        /// <summary>
        /// Gets the stock transfer info.
        /// </summary>
        /// <param name="subAccountId">The sub account id.</param>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <param name="accountType">Type of the account.</param>
        /// <returns>
        /// <para>A <see cref="ResultObject{T}">ResultObject&lt;StockTransferInfo&gt;</see> object contains returned code, returned message and 
        /// stock transfer information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not loged in or multiple login.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=SUCCESS: Getting data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get stock transfer info, return ResultObject<StockTransferInfo>", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetStockTransferInfo(string subAccountId, string secSymbol, int accountType,string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<StockTransferInfo>
                            {
                                Result = new StockTransferInfo(),
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                else if (Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return Serializer.Serialize(new ResultObject<StockTransferInfo>
                    {
                        Result = null,
                        ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString(),
                        RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY
                    });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, subAccountId))
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<StockTransferInfo>
                            {
                                Result = new StockTransferInfo(),
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                var stockTransferInfo = ETradeServices.GetStockTransferInfo(subAccountId, secSymbol, accountType);
                return Serializer.Serialize(stockTransferInfo);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<StockTransferInfo>
                            {
                                Result = new StockTransferInfo(),
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }

        /// <summary>
        /// Gets the list stock transfer info.
        /// </summary>
        /// <param name="subAccountId">The sub account id.</param>
        /// <param name="accountType">Type of the account.</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>
        /// <para>A ResultObject&lt;List&lt;StockTransferInfo&gt;&gt; object contains returned code, 
        /// returned message and a list of StockTransferInfo objects that contains stock transfer information.</para>
        /// <para>RET_CODE=NOT_LOGIN: User has not logged in.</para>
        /// <para>RET_CODE=ERROR_NOT_CASH_AVAILABLE: The is no data of cash.</para>
        /// <para>RET_CODE=ERROR_DEBT_ACCOUNT: Account is in debt.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: The is no data.</para>
        /// <para>RET_CODE=SUCCESS: Create data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "Get list stock transfer info, return ResultObject PagingObject List StockTransferInfo", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListStockTransferInfo(string subAccountId, int accountType, int pageIndex, int pageSize,string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<List<StockTransferInfo>>
                            {
                                Result = new List<StockTransferInfo>(),
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                else if (Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return Serializer.Serialize(new ResultObject<List<StockTransferInfo>>
                    {
                        Result = new List<StockTransferInfo>(),
                        ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString(),
                        RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY
                    });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, subAccountId))
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<List<StockTransferInfo>>
                            {
                                Result = new List<StockTransferInfo>(),
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                var listStockTransferInfo = ETradeServices.GetListStockTransferInfo(subAccountId, accountType, pageIndex, pageSize);
                return Serializer.Serialize(listStockTransferInfo);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<List<StockTransferInfo>>
                            {
                                Result = new List<StockTransferInfo>(),
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }
        /// <summary>
        /// Gets the list stock trans order.
        /// </summary>
        /// <param name="sourceAccountID">The source account ID.</param>
        /// <param name="destAccountID">The dest account ID.</param>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <param name="transType">Type of the trans.</param>
        /// <param name="status">The status.</param>
        /// <param name="note">The note.</param>
        /// <param name="brokerID">The broker ID.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [WebMethod(Description = "get list stock transfer to day, return ResultObject<PagingObjectList<<StockTransfer>>", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListStockTransOrder(string sourceAccountID, string destAccountID, string secSymbol, int transType, int status, string note, string brokerID, int pageIndex, int pageSize,string apidKey)
        {
            string listStockTransferOrder = GetListStockTransOrderHist(sourceAccountID,
                                                                       destAccountID, secSymbol,
                                                                       string.Empty, string.Empty, transType,
                                                                       status, note, brokerID,
                                                                       pageIndex, pageSize,apidKey);
            return listStockTransferOrder;
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
        /// <para>RET_CODE=NOT_LOGIN: User has not logged in.</para>
        /// <para>RET_CODE=ERROR_INVALID_DATETIME: The date is invalid.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: The is no data.</para>
        /// <para>RET_CODE=SUCCESS: Create data successfully.</para>
        /// <para>RET_CODE=SYSTEM_ERROR: System error.</para>
        /// </returns>
        [WebMethod(Description = "get list stock transfer history, return ResultObject<PagingObjectList<<StockTransfer>>", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListStockTransOrderHist(string sourceAccountID, string destAccountID, string secSymbol, string fromDate, string toDate, int transType, int status, string note, string brokerID, int pageIndex, int pageSize,string apiKey)
        {
            try
            {
                if (Session == null || Session[CommonEnums.SESSION_KEY.CUSTOMER_ACCOUNT.ToString()] == null)
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<PagingObject<List<StockTransfer>>>()
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                else if (Session[CommonEnums.SESSION_KEY.API_KEY.ToString()] == null || Session[CommonEnums.SESSION_KEY.API_KEY.ToString()].ToString().CompareTo(apiKey) != 0)
                {
                    return Serializer.Serialize(new ResultObject<List<StockTransferInfo>>
                    {
                        Result = new List<StockTransferInfo>(),
                        ErrorMessage = CommonEnums.RET_CODE.INVAILID_API_KEY.ToString(),
                        RetCode = CommonEnums.RET_CODE.INVAILID_API_KEY
                    });
                }
                //Check multiple account by an customer
                if (IsMultiAccount(string.Empty, sourceAccountID))
                {
                    return
                        Serializer.Serialize(
                            new ResultObject<PagingObject<List<StockTransfer>>>()
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.NOT_LOGIN,
                                ErrorMessage = CommonEnums.RET_CODE.NOT_LOGIN.ToString()
                            });
                }
                var listStockTransferOrderHist = ETradeServices.GetListStockTransOrderHist(sourceAccountID,
                                                                                           destAccountID, secSymbol,
                                                                                           fromDate, toDate, transType,
                                                                                           status, note, brokerID,
                                                                                           pageIndex, pageSize);
                return Serializer.Serialize(listStockTransferOrderHist);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, WEB_SERVICE_POLICY);
                return Serializer.Serialize(
                            new ResultObject<PagingObject<List<StockTransfer>>>()
                            {
                                Result = null,
                                RetCode = CommonEnums.RET_CODE.SYSTEM_ERROR,
                                ErrorMessage = CommonEnums.RET_CODE.SYSTEM_ERROR.ToString()
                            });
            }
        }

        #endregion
    }
}
