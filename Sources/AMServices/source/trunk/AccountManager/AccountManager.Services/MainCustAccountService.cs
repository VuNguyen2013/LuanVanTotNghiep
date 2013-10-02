	

#region Using Directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AccountManager.DataAccess.Bases;
using AccountManager.Entities;
using AccountManager.DataAccess;
using ETradeCommon;
using ETradeCommon.Enums;
using ETradeCore.Services;
using ETradeCore.Entities;
using System.Collections;
#endregion

namespace AccountManager.Services
{		
	/// <summary>
	/// An component type implementation of the 'MainCustAccount' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class MainCustAccountService : MainCustAccountServiceBase
	{
        private static readonly bool noTranByDefault = false;
        private static readonly string layerExceptionPolicy = "ServiceLayerExceptionPolicy";

	    /// <summary>
	    ///  Method that get customer account information based on MainCustAccountId
	    /// </summary>
	    /// <param name="mainCustAccountId"></param>
	    /// <param name="fullInfo">true if get all sub account; otherwise, false</param>
	    /// <returns>Returns an instance of the <see cref="MainCustAccount"/> class.</returns>
	    [DataObjectMethod(DataObjectMethodType.Select)]
        public virtual MainCustAccount GetAccountInfo(string mainCustAccountId, bool fullInfo)
        {
            #region Security check
            // throws security exception if not authorized
            SecurityContext.IsAuthorized("AuthenticateCustLogon");
            #endregion Security check

            #region Initialisation
            MainCustAccount mainCustAccount = null;
            TransactionManager transactionManager = null;
            NetTiersProvider dataProvider = null;
            #endregion Initialisation

            try
            {
                transactionManager = ConnectionScope.ValidateOrCreateTransaction(noTranByDefault);
                dataProvider = ConnectionScope.Current.DataProvider;

                // Get MainCustAccount information from database.
                mainCustAccount =
                    dataProvider.MainCustAccountProvider.GetByMainCustAccountId(transactionManager, mainCustAccountId);

                // Check if this account is actived or not
                if (mainCustAccount != null)
                {
                    mainCustAccount.SubCustAccountCollection = new TList<SubCustAccount>();

                    // Get sub account list
                    var subAccList = dataProvider.SubCustAccountProvider.GetByMainCustAccountId(mainCustAccountId);

                    if (fullInfo)
                    {
                        foreach (var subCustAccount in subAccList)
                        {
                            // Get sub account permission
                            var subAccPermList =
                                dataProvider.SubCustAccountPermissionProvider.GetBySubCustAccountId(
                                    subCustAccount.SubCustAccountId);

                            subCustAccount.SubCustAccountPermissionCollection = subAccPermList;

                            //Get bank account type
                            BankServices bankServices=new BankServices();                            
                            BankAccountInfo bankAccountInfo= bankServices.GetBankAccountInfo(subCustAccount.SubCustAccountId);
                            if(bankAccountInfo!=null)
                            {
                                subCustAccount.BankAccountType = bankAccountInfo.BankAccountType;
                                subCustAccount.BankAccountNo = bankAccountInfo.BankAccNo;
                                subCustAccount.BankName = bankAccountInfo.BankName;
                            }
                            // Add this sub account to main account
                            mainCustAccount.SubCustAccountCollection.Add(subCustAccount);
                        }                                                           
                    }
                    else
                    {
                        foreach (var subCustAccount in subAccList)
                        {
                            if (subCustAccount.Actived == true)
                            {
                                // Get sub account permission
                                var subAccPermList =
                                    dataProvider.SubCustAccountPermissionProvider.GetBySubCustAccountId(
                                        subCustAccount.SubCustAccountId);
                                subCustAccount.SubCustAccountPermissionCollection = subAccPermList;                               

                                //Get bank account type
                                BankServices bankServices = new BankServices();
                                BankAccountInfo bankAccountInfo = bankServices.GetBankAccountInfo(subCustAccount.SubCustAccountId);
                                if (bankAccountInfo != null)
                                {
                                    subCustAccount.BankAccountType = bankAccountInfo.BankAccountType;
                                    subCustAccount.BankAccountNo = !string.IsNullOrEmpty(bankAccountInfo.BankAccNo) ? bankAccountInfo.BankAccNo : string.Empty;
                                    subCustAccount.BankName = bankAccountInfo.BankName;
                                }

                                // Add this sub account to main account
                                mainCustAccount.SubCustAccountCollection.Add(subCustAccount);
                            }
                        }
                        ////visible permission stock transfer of bank account that has no payees account
                        var listBankAccountType = subAccList.Where(n => n.BankAccountType == CommonEnums.BANK_ACCOUNT_TYPE.BANKACC).ToList();
                        if (listBankAccountType != null && listBankAccountType.Count > 0)
                        {
                            foreach (var bankAccount in listBankAccountType)
                            {
                                if (mainCustAccount.SubCustAccountCollection.Where(n => n.SubCustAccountId != bankAccount.SubCustAccountId && Utils.GetAccountType(n.SubCustAccountId) == (int)CommonEnums.ACCOUNT_TYPE.MARGIN).Count() == 0)
                                {
                                    bankAccount.SubCustAccountPermissionCollection.Remove(
                                        bankAccount.SubCustAccountPermissionCollection.Where(
                                            n =>n.CustServicesPermissionId ==(int) CommonEnums.SUB_ACCOUNT_PERMISSIONS.STOCK_TRANSFER).FirstOrDefault());
                                    
                                }
                            }
                        }
                    }

                    //union bank account no && bank name
                    if(subAccList!=null && subAccList.Count>0)
                    {
                        var subBankNo = subAccList.Where(n => !string.IsNullOrEmpty(n.BankAccountNo)).FirstOrDefault();
                        var subBankName = subAccList.Where(n => !string.IsNullOrEmpty(n.BankName)).FirstOrDefault();
                        foreach (var account in subAccList)
                        {
                            account.BankAccountNo = subBankNo!=null? subBankNo.BankAccountNo:account.BankAccountNo;
                            account.BankName = subBankName != null ? subBankName.BankName : account.BankName;
                        }
                    }
                    ////assign bank account no from margin account to normal account
                    //foreach (var marginAccount in subAccList)
                    //{
                    //    if(Utils.GetAccountType(marginAccount.SubCustAccountId)==(int)CommonEnums.ACCOUNT_TYPE.MARGIN)
                    //    {
                    //        SubCustAccount normalAccount = subAccList.Where(subAccount => Utils.GetAccountType(subAccount.SubCustAccountId) == (int)CommonEnums.ACCOUNT_TYPE.NORMAL).FirstOrDefault();
                    //        if (normalAccount != null)
                    //            normalAccount.BankAccountNo = marginAccount.BankAccountNo;
                    //    }
                    //}                        
                }
            }
            catch (Exception exc)
            {
                #region Handle transaction rollback and exception
                if (transactionManager != null && transactionManager.IsOpen)
                    transactionManager.Rollback();

                //Handle exception based on policy
                if (DomainUtil.HandleException(exc, layerExceptionPolicy))
                    throw;
                #endregion Handle transaction rollback and exception
            }

            return mainCustAccount;
        }

	    ///<summary>
	    /// Change customer password
	    ///</summary>
	    ///<param name="mainCustAccountId"></param>
	    ///<param name="oldPassword"></param>
	    ///<param name="newPassword"></param>
	    ///<param name="updatedUserId"></param>
	    ///<returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update)]
	    public int ChangeCustPassword(string mainCustAccountId, string oldPassword, string newPassword, string updatedUserId)
        {
            #region Security and validation check
            // throws security exception if not authorized
            SecurityContext.IsAuthorized("Update");

            #endregion Security and validation check

            #region Initialisation
            bool result = false;
            bool isBorrowedTransaction = false;
            TransactionManager transactionManager = null;
            NetTiersProvider dataProvider;
            #endregion Initialisation

            try
            {
                var customerActionHistoryService = new CustomerActionHistoryService();
                isBorrowedTransaction = ConnectionScope.Current.HasTransaction;
                transactionManager = ConnectionScope.ValidateOrCreateTransaction(noTranByDefault);
                dataProvider = ConnectionScope.Current.DataProvider;

                //Validate password format (empty, min length, max length, format)
                if (string.IsNullOrEmpty(newPassword))
                {
                    // New password is empty
                    return (int)CommonEnums.RET_CODE.ERROR_EMPTY;
                }
                if(newPassword.Length < 8)
                {
                    // Leng of new password is less than 8
                    return (int)CommonEnums.RET_CODE.ERROR_MIN_LENGTH;
                } 
                if (newPassword.Length > 32)
                {
                    // Leng of new password is greater than 8
                    return (int)CommonEnums.RET_CODE.ERROR_MAX_LENGTH;
                }

                // Get MainCustAccount information from database.
                var mainCustAccount =
                    dataProvider.MainCustAccountProvider.GetByMainCustAccountId(transactionManager, mainCustAccountId);

                // Check if this account is actived or not
                if ((mainCustAccount != null))
                {
                    if (mainCustAccount.Password == PasswordHandlerMd5.Encrypt(oldPassword))
                    {
                        mainCustAccount.Password = PasswordHandlerMd5.Encrypt(newPassword);
                        mainCustAccount.UpdatedDate = DateTime.Now;
                        mainCustAccount.UpdatedUser = updatedUserId;
                        dataProvider.MainCustAccountProvider.Update(mainCustAccount);
                    }
                    else
                    {
                        return (int)CommonEnums.RET_CODE.INCORRECT_PASSWORD;
                    }
                }
                else
                {
                    return (int) CommonEnums.RET_CODE.ERROR_ACCOUNT;
                }
                customerActionHistoryService.InsertCustomerActionHistory(string.Empty, DateTime.Now, mainCustAccountId,
                                                                         string.Empty,
                                                                         (int) CommonEnums.ACTION_TYPE.CHANGE_PASSWORD,
                                                                         -1);
                if (!isBorrowedTransaction && transactionManager != null && transactionManager.IsOpen)
                    transactionManager.Commit();
                return (int)CommonEnums.RET_CODE.SUCCESS;
            }
            catch (Exception exc)
            {
                #region Handle transaction rollback and exception
                if (transactionManager != null && transactionManager.IsOpen)
                    transactionManager.Rollback();

                //Handle exception based on policy
                if (DomainUtil.HandleException(exc, layerExceptionPolicy))
                    throw;
                #endregion Handle transaction rollback and exception
            }

            return (int)CommonEnums.RET_CODE.SUCCESS;
        }

	    ///<summary>
	    /// Change customer password
	    ///</summary>
	    ///<param name="mainCustAccountId"></param>
	    ///<param name="oldPin"></param>
	    ///<param name="newPin"></param>
	    ///<param name="updatedUserId"></param>
	    ///<returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public int ChangeCustPin(string mainCustAccountId, string oldPin, string newPin, string updatedUserId)
        {
            #region Security and validation check
            // throws security exception if not authorized
            SecurityContext.IsAuthorized("Update");

            #endregion Security and validation check

            #region Initialisation
            bool result = false;
            bool isBorrowedTransaction = false;
            TransactionManager transactionManager = null;
            NetTiersProvider dataProvider;
            #endregion Initialisation

            try
            {
                isBorrowedTransaction = ConnectionScope.Current.HasTransaction;
                transactionManager = ConnectionScope.ValidateOrCreateTransaction(noTranByDefault);
                dataProvider = ConnectionScope.Current.DataProvider;

                //Validate pin format (empty, min length, max length, format)
                if (string.IsNullOrEmpty(newPin))
                {
                    // New password is empty
                    return (int)CommonEnums.RET_CODE.ERROR_EMPTY;
                }
                if (newPin.Length < 8)
                {
                    // Leng of new password is less than 8
                    return (int)CommonEnums.RET_CODE.ERROR_MIN_LENGTH;
                }
                if (newPin.Length > 32)
                {
                    // Leng of new password is greater than 8
                    return (int)CommonEnums.RET_CODE.ERROR_MAX_LENGTH;
                }

                // Get MainCustAccount information from database.
                var mainCustAccount =
                    dataProvider.MainCustAccountProvider.GetByMainCustAccountId(transactionManager, mainCustAccountId);

                // Check if this account is actived or not
                if ((mainCustAccount != null))
                {
                    if (mainCustAccount.Pin == PasswordHandlerMd5.Encrypt(oldPin))
                    {
                        mainCustAccount.Pin = PasswordHandlerMd5.Encrypt(newPin);
                        mainCustAccount.UpdatedDate = DateTime.Now;
                        mainCustAccount.UpdatedUser = updatedUserId;
                        dataProvider.MainCustAccountProvider.Update(mainCustAccount);
                    }
                    else
                    {
                        return (int)CommonEnums.RET_CODE.INCORRECT_PASSWORD;
                    }
                }
                else
                {
                    return (int)CommonEnums.RET_CODE.ERROR_ACCOUNT;
                }
                var customerActionHistoryService = new CustomerActionHistoryService();
                customerActionHistoryService.InsertCustomerActionHistory(string.Empty, DateTime.Now, mainCustAccountId,
                                                                     string.Empty,
                                                                     (int)CommonEnums.ACTION_TYPE.CHANGE_PIN,
                                                                     -1);
                if (!isBorrowedTransaction && transactionManager != null && transactionManager.IsOpen)
                    transactionManager.Commit();
                return (int)CommonEnums.RET_CODE.SUCCESS;
            }
            catch (Exception exc)
            {
                #region Handle transaction rollback and exception
                if (transactionManager != null && transactionManager.IsOpen)
                    transactionManager.Rollback();

                //Handle exception based on policy
                if (DomainUtil.HandleException(exc, layerExceptionPolicy))
                    throw;
                #endregion Handle transaction rollback and exception
            }

            return (int)CommonEnums.RET_CODE.SUCCESS;
        }

	    /// <summary>
	    /// Activate or deactivate a customer account to allow to use online trading system.
	    /// </summary>
	    /// <param name="mainCustAccountId">Id of main customer account</param>
	    /// <param name="actived">true if main customer account is actived; otherwise, false</param>
	    /// <param name="lockReason">Reason lock</param>
	    /// <param name="updatedUserId">Id of login broker</param>
	    /// <returns>
	    /// <para>Result of the activating action.</para>
	    /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
	    /// <para>RET_CODE=FAIL: Failed to activate main customer account information.</para>
	    /// <para>RET_CODE=SUCCESS: Activate main customer successfully.</para>
	    /// </returns>
	    [DataObjectMethod(DataObjectMethodType.Update)]
        public int ActivateMainCustomer(string mainCustAccountId, bool actived, int lockReason, string updatedUserId)
        {
            #region Security and validation check
            // throws security exception if not authorized
            SecurityContext.IsAuthorized("Update");

            #endregion Security and validation check

            #region Initialisation

            bool result = false;
	        TransactionManager transactionManager = null;
            NetTiersProvider dataProvider;
            #endregion Initialisation

            try
            {
                
                bool isBorrowedTransaction = ConnectionScope.Current.HasTransaction;
                transactionManager = ConnectionScope.ValidateOrCreateTransaction(noTranByDefault);
                dataProvider = ConnectionScope.Current.DataProvider;

                // Get MainCustAccount information from database.
                var mainCustAccount =
                    dataProvider.MainCustAccountProvider.GetByMainCustAccountId(transactionManager, mainCustAccountId);

                // Check if this account is actived or not
                if ((mainCustAccount != null))
                {
                    mainCustAccount.Actived = actived;
                    if(actived)
                    {
                        mainCustAccount.LockReason = (int)CommonEnums.LOCK_ACCOUNT_REASON.NOTHING;
                    }
                    else
                    {
                        mainCustAccount.LockReason = lockReason;
                    }
                    
                    mainCustAccount.UpdatedDate = DateTime.Now;
                    mainCustAccount.UpdatedUser = updatedUserId;
                    result = dataProvider.MainCustAccountProvider.Update(mainCustAccount);
                    if (actived)
                    {
                        var customerActionHistoryService = new CustomerActionHistoryService();
                        customerActionHistoryService.InsertCustomerActionHistory(updatedUserId, DateTime.Now, mainCustAccountId,
                                                                             string.Empty,
                                                                             (int)CommonEnums.ACTION_TYPE.BROKER_ACTIVATE_ACCOUNT,
                                                                             -1);
                    }
                    else
                    {
                        var customerActionHistoryService = new CustomerActionHistoryService();
                        customerActionHistoryService.InsertCustomerActionHistory(updatedUserId, DateTime.Now, mainCustAccountId,
                                                                             string.Empty,
                                                                             (int)CommonEnums.ACTION_TYPE.BROKER_LOCK_ACCOUNT,
                                                                             lockReason);
                    }
                    
                }
                else
                {
                    return (int) CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }
                if (!isBorrowedTransaction && transactionManager != null && transactionManager.IsOpen)
                    transactionManager.Commit();
            }
            catch (Exception exc)
            {
                #region Handle transaction rollback and exception
                if (transactionManager != null && transactionManager.IsOpen)
                    transactionManager.Rollback();

                //Handle exception based on policy
                if (DomainUtil.HandleException(exc, layerExceptionPolicy))
                    throw;
                #endregion Handle transaction rollback and exception
            }

	        if (!result)
	        {
	            return (int) CommonEnums.RET_CODE.FAIL;
	        }
	        return (int) CommonEnums.RET_CODE.SUCCESS;
        }


	    ///<summary>
	    /// Update main customer.
	    ///</summary>
        ///<param name="newMainCustAccount">Information of main customer account</param>
        /// <returns>
        /// <para>Result of updating main customer account information.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=FAIL: Failed to update main customer account information.</para>
        /// <para>RET_CODE=SUCCESS: Update main customer successfully.</para>
        /// </returns>
	    [DataObjectMethod(DataObjectMethodType.Update)]
        public int UpdateMainCustomer(MainCustAccount newMainCustAccount)
        {
            #region Security and validation check
            // throws security exception if not authorized
            SecurityContext.IsAuthorized("Update");

            #endregion Security and validation check

            #region Initialisation

            bool result = false;
	        TransactionManager transactionManager = null;
            NetTiersProvider dataProvider;
            #endregion Initialisation

            try
            {
                bool isBorrowedTransaction = ConnectionScope.Current.HasTransaction;
                transactionManager = ConnectionScope.ValidateOrCreateTransaction(noTranByDefault);
                dataProvider = ConnectionScope.Current.DataProvider;

                // Get MainCustAccount information from database.
                var mainCustAccount =
                    dataProvider.MainCustAccountProvider.GetByMainCustAccountId(transactionManager, newMainCustAccount.MainCustAccountId);

                // Check if this account is actived or not
                if ((mainCustAccount != null))
                {
                    // Get action type and reason to insert into history table
                    int actionType = (int)CommonEnums.ACTION_TYPE.BROKER_CHANGE_INFORMATION;
                    int reason = -1;
                    if (mainCustAccount.Actived != newMainCustAccount.Actived)
                    {
                        if (newMainCustAccount.Actived)
                        {
                            actionType = (int)CommonEnums.ACTION_TYPE.BROKER_ACTIVATE_ACCOUNT;
                        }
                        else
                        {
                            actionType = (int)CommonEnums.ACTION_TYPE.BROKER_LOCK_ACCOUNT;
                            reason = (int)CommonEnums.LOCK_ACCOUNT_REASON.BY_BROKER;
                        }
                    }
                    else if (mainCustAccount.PassLockReason != newMainCustAccount.PassLockReason)
                    {
                        if (mainCustAccount.PassLockReason == (int)CommonEnums.LOCK_ACCOUNT_REASON.NOTHING)
                        {
                            actionType = (int)CommonEnums.ACTION_TYPE.BROKER_ACTIVATE_PASS;
                        }
                        else
                        {
                            actionType = (int)CommonEnums.ACTION_TYPE.BROKER_LOCK_PASS;
                            reason = (int)CommonEnums.LOCK_ACCOUNT_REASON.BY_BROKER;
                        }
                    }
                    else if (mainCustAccount.PinLockReason != newMainCustAccount.PinLockReason)
                    {
                        if (mainCustAccount.PinLockReason == (int)CommonEnums.LOCK_ACCOUNT_REASON.NOTHING)
                        {
                            actionType = (int)CommonEnums.ACTION_TYPE.BROKER_ACTIVATE_PIN;
                        }
                        else
                        {
                            actionType = (int)CommonEnums.ACTION_TYPE.BROKER_LOCK_PIN;
                            reason = (int)CommonEnums.LOCK_ACCOUNT_REASON.BY_BROKER;
                        }
                    }
                    //Update account information
                    mainCustAccount.FullName = newMainCustAccount.FullName;
                    mainCustAccount.Email = newMainCustAccount.Email;
                    mainCustAccount.Phone = newMainCustAccount.Phone;
                    mainCustAccount.Actived = newMainCustAccount.Actived;
                    if (!string.IsNullOrEmpty(newMainCustAccount.Password))
                    {
                        mainCustAccount.Password = PasswordHandlerMd5.Encrypt(newMainCustAccount.Password);
                    }
                    if (!string.IsNullOrEmpty(newMainCustAccount.Pin))
                    {
                        mainCustAccount.Pin = PasswordHandlerMd5.Encrypt(newMainCustAccount.Pin);
                    }
                    mainCustAccount.PassLockReason = newMainCustAccount.PassLockReason;
                    mainCustAccount.PinLockReason = newMainCustAccount.PinLockReason;
                    mainCustAccount.BrokerId = newMainCustAccount.BrokerId;
                    mainCustAccount.TokenId = newMainCustAccount.TokenId;
                    mainCustAccount.TokenName = newMainCustAccount.TokenName;
                    mainCustAccount.TokenActived = newMainCustAccount.TokenActived;
                    mainCustAccount.CustomerType = newMainCustAccount.CustomerType;
                    mainCustAccount.LanguageId = newMainCustAccount.LanguageId;
                    mainCustAccount.LockReason = newMainCustAccount.LockReason;
                    mainCustAccount.UpdatedDate = DateTime.Now;
                    mainCustAccount.UpdatedUser = newMainCustAccount.UpdatedUser;
                    result = dataProvider.MainCustAccountProvider.Update(mainCustAccount);
                    
                    var customerActionHistoryService = new CustomerActionHistoryService();
                    customerActionHistoryService.InsertCustomerActionHistory(mainCustAccount.UpdatedUser, DateTime.Now,
                                                                             mainCustAccount.MainCustAccountId,
                                                                             string.Empty, actionType, reason);
                }
                else
                {
                    return (int)CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }
                if (!isBorrowedTransaction && transactionManager != null && transactionManager.IsOpen)
                    transactionManager.Commit();
            }
            catch (Exception exc)
            {
                #region Handle transaction rollback and exception
                if (transactionManager != null && transactionManager.IsOpen)
                    transactionManager.Rollback();

                //Handle exception based on policy
                if (DomainUtil.HandleException(exc, layerExceptionPolicy))
                    throw;
                #endregion Handle transaction rollback and exception
            }

	        if (!result)
	        {
	            return (int) CommonEnums.RET_CODE.FAIL;
	        }
            return (int)CommonEnums.RET_CODE.SUCCESS;
        }

        /// <summary>
        ///  Method that get customer account information based on MainCustAccountId
        /// </summary>
        /// <param name="mainCustAccountId"></param>
        /// <returns>Returns an instance of the <see cref="MainCustAccount"/> class.</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public MainCustAccount GetMainCustAccount(string mainCustAccountId)
        {
            #region Security check
            // throws security exception if not authorized
            SecurityContext.IsAuthorized("AuthenticateCustLogon");
            #endregion Security check

            #region Initialisation
            MainCustAccount mainCustAccount = null;
            TransactionManager transactionManager = null;
            NetTiersProvider dataProvider = null;
            #endregion Initialisation

            try
            {
                transactionManager = ConnectionScope.ValidateOrCreateTransaction(noTranByDefault);
                dataProvider = ConnectionScope.Current.DataProvider;

                // Get MainCustAccount information from database.
                mainCustAccount =
                    dataProvider.MainCustAccountProvider.GetByMainCustAccountId(transactionManager, mainCustAccountId);

                // Check if this account is actived or not
                if (mainCustAccount != null)
                {
                    var broker = dataProvider.BrokerAccountProvider.GetByBrokerId(mainCustAccount.BrokerId);
                    if (broker != null)
                    {
                        mainCustAccount.BrokerName = broker.Name;
                    }
                }
            }
            catch (Exception exc)
            {
                #region Handle transaction rollback and exception
                if (transactionManager != null && transactionManager.IsOpen)
                    transactionManager.Rollback();

                //Handle exception based on policy
                if (DomainUtil.HandleException(exc, layerExceptionPolicy))
                    throw;
                #endregion Handle transaction rollback and exception
            }

            return mainCustAccount;
        }

        ///<summary>
        /// Generate customer password
        ///</summary>
        ///<param name="mainCustAccountId"></param>
        ///<param name="updatedUserId"></param>
        ///<returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public string GenerateCustPassword(string mainCustAccountId, string updatedUserId)
        {
            #region Security and validation check
            // throws security exception if not authorized
            SecurityContext.IsAuthorized("Update");

            #endregion Security and validation check

            #region Initialisation

            TransactionManager transactionManager = null;
            NetTiersProvider dataProvider;
            #endregion Initialisation

            string password = "";
            try
            {
                bool isBorrowedTransaction = ConnectionScope.Current.HasTransaction;
                transactionManager = ConnectionScope.ValidateOrCreateTransaction(noTranByDefault);
                dataProvider = ConnectionScope.Current.DataProvider;
                int passwordLength = int.Parse(ConfigurationManager.AppSettings["GeneratedPasswordLength"]);
                password = PasswordGenerator.GeneratePassword(passwordLength, false, true, false);

                // Get MainCustAccount information from database.
                var mainCustAccount =
                    dataProvider.MainCustAccountProvider.GetByMainCustAccountId(transactionManager, mainCustAccountId);

                // Check if this account is actived or not
                if ((mainCustAccount != null))
                {
                    mainCustAccount.PassLockReason = (int) CommonEnums.LOCK_ACCOUNT_REASON.NOTHING;
                    mainCustAccount.FailedLoginCount = 0;
                    mainCustAccount.FailedLoginTime = null;
                    mainCustAccount.Password = PasswordHandlerMd5.Encrypt(password);
                    mainCustAccount.PassIsNew = true;
                    mainCustAccount.AuthType = Convert.ToInt16((int)CommonEnums.AUTHENTICATION_TYPE.PIN_PASS);
                    mainCustAccount.TokenId = null;
                    mainCustAccount.UpdatedUser = updatedUserId;
                    mainCustAccount.UpdatedDate = DateTime.Now;
                    bool result = dataProvider.MainCustAccountProvider.Update(mainCustAccount);
                    if (!result)
                    {
                        password = "";
                    }
                    else
                    {
                        var customerActionHistoryService = new CustomerActionHistoryService();
                        customerActionHistoryService.InsertCustomerActionHistory(updatedUserId, DateTime.Now,
                                                                             mainCustAccountId, string.Empty,
                                                                             (int)CommonEnums.ACTION_TYPE.BROKER_ACTIVATE_PASS,
                                                                             -1);
                    }

                }
                else
                {
                    password = "";
                }

                if (!isBorrowedTransaction && transactionManager != null && transactionManager.IsOpen)
                    transactionManager.Commit();
            }
            catch (Exception exc)
            {
                #region Handle transaction rollback and exception
                if (transactionManager != null && transactionManager.IsOpen)
                    transactionManager.Rollback();

                //Handle exception based on policy
                if (DomainUtil.HandleException(exc, layerExceptionPolicy))
                    throw;
                #endregion Handle transaction rollback and exception
            }

            return password;
        }


        /// <summary>
        /// Adds the token id.
        /// </summary>
        /// <param name="mainCustAccountId">The main cust account id.</param>
        /// <param name="tokenId">The token id.</param>
        /// <param name="updatedUserId">The updated user id.</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public int AddTokenId(string mainCustAccountId, string tokenId, string updatedUserId)
        {
            #region Security and validation check
            // throws security exception if not authorized
            SecurityContext.IsAuthorized("Update");

            #endregion Security and validation check

            #region Initialisation

            TransactionManager transactionManager = null;
            NetTiersProvider dataProvider;
            #endregion Initialisation

            
            try
            {
                bool isBorrowedTransaction = ConnectionScope.Current.HasTransaction;
                transactionManager = ConnectionScope.ValidateOrCreateTransaction(noTranByDefault);
                dataProvider = ConnectionScope.Current.DataProvider;
                if(!string.IsNullOrEmpty(tokenId))
                {
                    int count = 0;
                    var listMainCustAccount = dataProvider.MainCustAccountProvider.GetPaged(string.Format("TokenID ='{0}' AND MainCustAccountID!='{1}'", tokenId, mainCustAccountId), "", 0, int.MaxValue, out count);
                    if (count > 0)
                        return (int)CommonEnums.RET_CODE.EXISTED_DATA;

                }
                
                var mainCustAccount =
                    dataProvider.MainCustAccountProvider.GetByMainCustAccountId(transactionManager, mainCustAccountId);

                // Check if this account is actived or not
                if ((mainCustAccount != null))
                {
                    mainCustAccount.TokenId = tokenId;
                    if (!string.IsNullOrEmpty(tokenId))
                        mainCustAccount.PassLockReason = (int)CommonEnums.LOCK_ACCOUNT_REASON.NOTHING;

                    mainCustAccount.AuthType = Convert.ToInt16((int)CommonEnums.AUTHENTICATION_TYPE.RSA);
                    mainCustAccount.Password = null;
                    mainCustAccount.UpdatedUser = updatedUserId;
                    mainCustAccount.UpdatedDate = DateTime.Now;
                    bool result = dataProvider.MainCustAccountProvider.Update(mainCustAccount);
                    if (result)                   
                    {
                        var customerActionHistoryService = new CustomerActionHistoryService();
                        customerActionHistoryService.InsertCustomerActionHistory(updatedUserId, DateTime.Now,
                                                                             mainCustAccountId, string.Empty,
                                                                             (int)CommonEnums.ACTION_TYPE.BROKER_ACTIVATE_RSA,
                                                                             -1);
                    }
                    else
                    {
                        return (int)CommonEnums.RET_CODE.FAIL;
                    }
                }
                else
                {
                    return (int)CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }
               

                if (!isBorrowedTransaction && transactionManager != null && transactionManager.IsOpen)
                    transactionManager.Commit();

                return (int)CommonEnums.RET_CODE.SUCCESS;
            }
            catch (Exception exc)
            {
                #region Handle transaction rollback and exception
                if (transactionManager != null && transactionManager.IsOpen)
                    transactionManager.Rollback();

                //Handle exception based on policy
                if (DomainUtil.HandleException(exc, layerExceptionPolicy))
                    throw;
                #endregion Handle transaction rollback and exception
            }

            return (int)CommonEnums.RET_CODE.FAIL;
        }
      
        ///<summary>
        /// Generate customer pin
        ///</summary>
        ///<param name="mainCustAccountId"></param>
        ///<param name="updatedUserId"></param>
        ///<returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public string GenerateCustPin(string mainCustAccountId, string updatedUserId)
        {
            #region Security and validation check
            // throws security exception if not authorized
            SecurityContext.IsAuthorized("Update");

            #endregion Security and validation check

            #region Initialisation

            TransactionManager transactionManager = null;
            NetTiersProvider dataProvider;
            #endregion Initialisation

            string pin = "";
            try
            {
                bool isBorrowedTransaction = ConnectionScope.Current.HasTransaction;
                transactionManager = ConnectionScope.ValidateOrCreateTransaction(noTranByDefault);
                dataProvider = ConnectionScope.Current.DataProvider;
                int passwordLength = int.Parse(ConfigurationManager.AppSettings["GeneratedPasswordLength"]);
                pin = PasswordGenerator.GeneratePassword(passwordLength, false, true, false);

                // Get MainCustAccount information from database.
                var mainCustAccount =
                    dataProvider.MainCustAccountProvider.GetByMainCustAccountId(transactionManager, mainCustAccountId);

                // Check if this account is actived or not
                if ((mainCustAccount != null))
                {
                    mainCustAccount.PinLockReason = (int) CommonEnums.LOCK_ACCOUNT_REASON.NOTHING;
                    mainCustAccount.Pin = PasswordHandlerMd5.Encrypt(pin);
                    mainCustAccount.PinIsNew = true;
                    mainCustAccount.UpdatedUser = updatedUserId;
                    mainCustAccount.PinType = Convert.ToInt16((int)CommonEnums.AUTHENTICATION_TYPE.PIN_PASS);
                    mainCustAccount.UpdatedDate = DateTime.Now;
                    bool result = dataProvider.MainCustAccountProvider.Update(mainCustAccount);
                    if (!result)
                    {
                        pin = "";
                    }
                    else
                    {
                        var customerActionHistoryService = new CustomerActionHistoryService();
                        customerActionHistoryService.InsertCustomerActionHistory(updatedUserId, DateTime.Now,
                                                                             mainCustAccountId, string.Empty,
                                                                             (int)CommonEnums.ACTION_TYPE.BROKER_ACTIVATE_PIN,
                                                                             -1);
                    }
                }
                else
                {
                    pin = "";
                }

                if (!isBorrowedTransaction && transactionManager != null && transactionManager.IsOpen)
                    transactionManager.Commit();
            }
            catch (Exception exc)
            {
                #region Handle transaction rollback and exception
                if (transactionManager != null && transactionManager.IsOpen)
                    transactionManager.Rollback();

                //Handle exception based on policy
                if (DomainUtil.HandleException(exc, layerExceptionPolicy))
                    throw;
                #endregion Handle transaction rollback and exception
            }

            return pin;
        }

	    /// <summary>
	    /// Create a new main customer account.
	    /// </summary>
        /// <param name="mainCustAccount">Information of main customer account</param>
	    /// <returns>
	    /// <para>Result of creating new customer.</para>
        /// <para>RET_CODE=EXISTED_DATA: Account is already existed.</para>
	    /// <para>RET_CODE=FAIL: Failed to create main customer account information.</para>
	    /// <para>RET_CODE=SUCCESS: Create main customer successfully.</para>
	    /// </returns>
	    [DataObjectMethod(DataObjectMethodType.Insert)]
        public int CreateMainCustomer(MainCustAccount mainCustAccount)
        {
            #region Security and validation check
            // throws security exception if not authorized
            SecurityContext.IsAuthorized("CreateMainCustomer");

            /*if (!entity.IsValid)
                throw new EntityNotValidException(entity, "CreateMainCustomer", entity.Error);*/
            #endregion Security and validation check

            #region Initialisation

	        TransactionManager transactionManager = null;
            NetTiersProvider dataProvider;
            #endregion Initialisation

            try
            {
                bool isBorrowedTransaction = ConnectionScope.Current.HasTransaction;
                transactionManager = ConnectionScope.ValidateOrCreateTransaction(noTranByDefault);
                dataProvider = ConnectionScope.Current.DataProvider;

                // Check exist
                var mainCustomer =
                    dataProvider.MainCustAccountProvider.GetByMainCustAccountId(mainCustAccount.MainCustAccountId);
                if (mainCustomer != null)
                {
                    return (int)CommonEnums.RET_CODE.EXISTED_DATA;
                }

                bool result = dataProvider.MainCustAccountProvider.Insert(mainCustAccount);
                if (!result)
                {
                    return (int)CommonEnums.RET_CODE.FAIL;
                }
                var customerActionHistoryService = new CustomerActionHistoryService();
                customerActionHistoryService.InsertCustomerActionHistory(mainCustAccount.CreatedUser, DateTime.Now, 
                                                                     mainCustAccount.MainCustAccountId, string.Empty,
                                                                     (int)CommonEnums.ACTION_TYPE.CREATE,
                                                                     -1);
                if (!isBorrowedTransaction && transactionManager != null && transactionManager.IsOpen)
                    transactionManager.Commit();
            }
            catch (Exception exc)
            {
                #region Handle transaction rollback and exception
                if (transactionManager != null && transactionManager.IsOpen)
                    transactionManager.Rollback();

                //Handle exception based on policy
                if (DomainUtil.HandleException(exc, layerExceptionPolicy))
                    throw;
                #endregion Handle transaction rollback and exception
            }
            // Check exist
            
            return (int)CommonEnums.RET_CODE.SUCCESS;
        }

        /// <summary>
        /// Get a complete collection of <see cref="BrokerAccount" /> entities.
        /// </summary>
        /// <returns></returns>
        public PagingObject<List<MainCustAccount>> GetList(string mainCustAccountId, string fullname, string email, 
            string phone, int actived, int passwordLockReason, int pinLockReason, int customerType, int lockReason, 
            string brokerId, short authType, short pinType, int pageIndex, int pageSize)
        {
            // Create dynamic query
            var whereClause = new StringBuilder();
            if (!string.IsNullOrEmpty(mainCustAccountId))
            {
                whereClause.AppendFormat("AND MainCustAccountID LIKE {0} ", "'%" + mainCustAccountId + "%' ");
            }

            if (!string.IsNullOrEmpty(fullname))
            {
                whereClause.AppendFormat("AND FullName LIKE {0} ", "'%" + fullname + "%'");
            }

            if (!string.IsNullOrEmpty(email))
            {
                whereClause.AppendFormat("AND Email LIKE {0} ", "'%" + email + "%'");
            }

            if (!string.IsNullOrEmpty(phone))
            {
                whereClause.AppendFormat("AND Phone LIKE {0} ", "'%" + phone + "%'");
            }

            if (actived == (int)CommonEnums.SEARCH_BOOL.FALSE)
            {
                whereClause.AppendFormat("AND Actived = 0 ");
            }
            else if (actived == (int)CommonEnums.SEARCH_BOOL.TRUE)
            {
                whereClause.AppendFormat("AND Actived = 1 ");
            }

            if (passwordLockReason >= 0)
            {
                whereClause.AppendFormat("AND PassLockReason = {0} ", passwordLockReason);
            }

            if (pinLockReason >= 0)
            {
                whereClause.AppendFormat("AND PinLockReason = {0} ", pinLockReason);
            }

            if (customerType >= 0)
            {
                whereClause.AppendFormat("AND CustomerType = {0} ", customerType);
            }

            if (lockReason >= 0)
            {
                whereClause.AppendFormat("AND LockReason = {0} ", lockReason);
            }

            if (!string.IsNullOrEmpty(brokerId))
            {
                whereClause.AppendFormat("AND BrokerId LIKE {0} ", "'%" + brokerId + "%'");
            }

            if (authType >= 0)
            {
                whereClause.AppendFormat("AND AuthType = {0} ", authType);
            }

            if (pinType >= 0)
            {
                whereClause.AppendFormat("AND PinType = {0} ", pinType);
            }

            string finalWhereClause = whereClause.ToString();
            if (!string.IsNullOrEmpty(finalWhereClause))
            {
                finalWhereClause = finalWhereClause.Substring(4);
            }

            int totalRecord;
            pageIndex = pageIndex - 1;
            var list = GetPaged(finalWhereClause, "", pageIndex, pageSize, out totalRecord);
            var listMainCustAccounts = list.ToList();
            var returnObject = new PagingObject<List<MainCustAccount>> { Data = listMainCustAccounts, Count = totalRecord };
            return returnObject;
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="mainCustAccountId">The main cust account id.</param>
        /// <param name="messagePhone">The message phone.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public CommonEnums.RET_CODE ForgetPassword(string mainCustAccountId, string messagePhone, out string newPassword,out MainCustAccount mainCustAccount)
        {
            #region Security and validation check
            // throws security exception if not authorized
            SecurityContext.IsAuthorized("Update");

            #endregion Security and validation check

            #region Initialisation

            TransactionManager transactionManager = null;
            NetTiersProvider dataProvider;
            #endregion Initialisation

            newPassword = string.Empty;
            mainCustAccount=new MainCustAccount();
            try
            {
                bool isBorrowedTransaction = ConnectionScope.Current.HasTransaction;
                transactionManager = ConnectionScope.ValidateOrCreateTransaction(noTranByDefault);
                dataProvider = ConnectionScope.Current.DataProvider;

                // Get MainCustAccount information from database.
                mainCustAccount =
                    dataProvider.MainCustAccountProvider.GetByMainCustAccountId(transactionManager, mainCustAccountId);

                if (mainCustAccount == null)
                {
                    newPassword = string.Empty;
                    return CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }

                if(mainCustAccount.AuthType==(int)CommonEnums.AUTHENTICATION_TYPE.RSA)
                    return CommonEnums.RET_CODE.CAN_NOT_CHANGE_PASS_RSA;

                if ( !mainCustAccount.Phone.Equals(messagePhone))
                {
                    newPassword = string.Empty;
                    return CommonEnums.RET_CODE.INCORECT_INFORMATION;
                }

                int passwordLength = int.Parse(ConfigurationManager.AppSettings["GeneratedPasswordLength"]);
                newPassword = PasswordGenerator.GeneratePassword(passwordLength, false, true, false);
                mainCustAccount.PassLockReason = (int)CommonEnums.LOCK_ACCOUNT_REASON.NOTHING;
                mainCustAccount.FailedLoginCount = 0;
                mainCustAccount.FailedLoginTime = null;
                mainCustAccount.Actived = true;
                mainCustAccount.Password = PasswordHandlerMd5.Encrypt(newPassword);
                mainCustAccount.FailedLoginCount = 0;
                mainCustAccount.PassIsNew = true;

                bool result = dataProvider.MainCustAccountProvider.Update(mainCustAccount);
                if (!result)
                {
                    newPassword = string.Empty;
                    return CommonEnums.RET_CODE.FAIL;
                }
                var customerActionHistoryService = new CustomerActionHistoryService();
                customerActionHistoryService.InsertCustomerActionHistory(string.Empty, DateTime.Now,
                                                                         mainCustAccountId, string.Empty,
                                                                         (int)CommonEnums.ACTION_TYPE.CUSTOMER_FORGET_PASS,
                                                                         -1);

                if (!isBorrowedTransaction && transactionManager != null && transactionManager.IsOpen)
                    transactionManager.Commit();
                return CommonEnums.RET_CODE.SUCCESS;
            }
            catch (Exception exc)
            {
                #region Handle transaction rollback and exception
                if (transactionManager != null && transactionManager.IsOpen)
                    transactionManager.Rollback();

                //Handle exception based on policy
                if (DomainUtil.HandleException(exc, layerExceptionPolicy))
                    throw;
                #endregion Handle transaction rollback and exception
            }
            return CommonEnums.RET_CODE.FAIL;
        }

        /// <summary>
        /// Forgets the pin.
        /// </summary>
        /// <param name="mainCustAccountId">The main cust account id.</param>
        /// <param name="newPin">The new pin.</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public CommonEnums.RET_CODE ForgetPin(string mainCustAccountId, out string newPin)
        {
            #region Security and validation check
            // throws security exception if not authorized
            SecurityContext.IsAuthorized("Update");

            #endregion Security and validation check

            #region Initialisation

            TransactionManager transactionManager = null;
            NetTiersProvider dataProvider;
            #endregion Initialisation

            newPin = string.Empty;
            try
            {
                bool isBorrowedTransaction = ConnectionScope.Current.HasTransaction;
                transactionManager = ConnectionScope.ValidateOrCreateTransaction(noTranByDefault);
                dataProvider = ConnectionScope.Current.DataProvider;

                // Get MainCustAccount information from database.
                var mainCustAccount =
                    dataProvider.MainCustAccountProvider.GetByMainCustAccountId(transactionManager, mainCustAccountId);

                if (mainCustAccount == null)
                {
                    newPin = string.Empty;
                    return CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }

                int pinLength = int.Parse(ConfigurationManager.AppSettings["GeneratedPasswordLength"]);
                newPin = PasswordGenerator.GeneratePassword(pinLength, false, true, false);

                mainCustAccount.PinLockReason = (int)CommonEnums.LOCK_ACCOUNT_REASON.NOTHING;                
                mainCustAccount.Pin = PasswordHandlerMd5.Encrypt(newPin);
                mainCustAccount.PinIsNew = true;

                bool result = dataProvider.MainCustAccountProvider.Update(mainCustAccount);
                if (!result)
                {
                    newPin = string.Empty;
                    return CommonEnums.RET_CODE.FAIL;
                }
                var customerActionHistoryService = new CustomerActionHistoryService();
                customerActionHistoryService.InsertCustomerActionHistory(string.Empty, DateTime.Now,
                                                                         mainCustAccountId, string.Empty,
                                                                         (int)CommonEnums.ACTION_TYPE.CUSTOMER_FORGET_PIN,
                                                                         -1);

                if (!isBorrowedTransaction && transactionManager != null && transactionManager.IsOpen)
                    transactionManager.Commit();
                return CommonEnums.RET_CODE.SUCCESS;
            }
            catch (Exception exc)
            {
                #region Handle transaction rollback and exception
                if (transactionManager != null && transactionManager.IsOpen)
                    transactionManager.Rollback();

                //Handle exception based on policy
                if (DomainUtil.HandleException(exc, layerExceptionPolicy))
                    throw;
                #endregion Handle transaction rollback and exception
            }
            return CommonEnums.RET_CODE.FAIL;
        }

        /// <summary>
        /// Method that get language id of account information based on MainCustAccountId
        /// </summary>
        /// <param name="mainCustAccountId">The main cust account id.</param>
        /// <returns>
        /// Returns an instance of the <see cref="MainCustAccount"/> class.
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public string GetLanguageId(string mainCustAccountId)
        {
            #region Security check
            // throws security exception if not authorized
            SecurityContext.IsAuthorized("GetLanguageId");
            #endregion Security check

            #region Initialisation
            MainCustAccount mainCustAccount = null;
            TransactionManager transactionManager = null;
            NetTiersProvider dataProvider;
            #endregion Initialisation
            try
            {
                transactionManager = ConnectionScope.ValidateOrCreateTransaction(noTranByDefault);
                dataProvider = ConnectionScope.Current.DataProvider;

                // Get MainCustAccount information from database.
                mainCustAccount =
                    dataProvider.MainCustAccountProvider.GetByMainCustAccountId(transactionManager, mainCustAccountId);
                if (mainCustAccount == null)
                    return string.Empty;
            }
            catch (Exception exc)
            {
                #region Handle transaction rollback and exception
                if (transactionManager != null && transactionManager.IsOpen)
                    transactionManager.Rollback();

                //Handle exception based on policy
                if (DomainUtil.HandleException(exc, layerExceptionPolicy))
                    throw;
                #endregion Handle transaction rollback and exception
            }
            return mainCustAccount.LanguageId;
        }
	}//End Class

} // end namespace
