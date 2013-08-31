﻿	

#region Using Directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ETradeCommon;
using ETradeCommon.Enums;
using ETradeFinance.DataAccess.Bases;
using ETradeFinance.Entities;
using ETradeFinance.DataAccess;

#endregion

namespace ETradeFinance.Services
{		
	/// <summary>
	/// An component type implementation of the 'CashAdvance' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class CashAdvanceService : ETradeFinance.Services.CashAdvanceServiceBase
	{
        private const string LAYER_EXCEPTION_POLICY = "ServiceLayerExceptionPolicy";
        private const bool NO_TRAN_BY_DEFAULT = false;
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the CashAdvanceService class.
		/// </summary>
		public CashAdvanceService() : base()
		{
		}
		#endregion Constructors

        #region Update
        /// <summary>
        /// Update Cash advance and Cash advance history
        /// </summary>
        /// <param name="id">Cash advance id</param>
        /// <param name="cashReceived">Cash received</param>
        /// <param name="status">Status</param>
        /// <param name="reason">Reason</param>
        /// <param name="brokerId">The broker id.</param>
        /// <returns>
        /// 	<para>Result of updating Cash advance</para>
        /// 	<para>RET_CODE=NO_EXISTED_DATA: Data is not existing.</para>
        /// 	<para>RET_CODE=FAIL: Fail to update data.</para>
        /// 	<para>RET_CODE=SUCCESS: Update data successfully.</para>
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public int UpdateCashAdvance(long id, decimal cashReceived, int status, string reason, string brokerId)
        {
            #region Security and validation check
            // throws security exception if not authorized
            SecurityContext.IsAuthorized("UpdateCashAdvance");

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
                transactionManager = ConnectionScope.ValidateOrCreateTransaction(NO_TRAN_BY_DEFAULT);
                dataProvider = ConnectionScope.Current.DataProvider;

                //Get cash advance
                var cashAdvance = GetById(id);
                if (cashAdvance == null)
                {
                    return (int)CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }

                //Get cash advance history
                var cashAdvanceHistoryBuilder = new CashAdvanceHistoryParameterBuilder();
                cashAdvanceHistoryBuilder.AppendEquals(CashAdvanceHistoryColumn.ContractNo, cashAdvance.ContractNo);
                var cashAdvanceHistoryList = dataProvider.CashAdvanceHistoryProvider.Find(cashAdvanceHistoryBuilder);
                if ((cashAdvanceHistoryList == null) || (cashAdvanceHistoryList.Count == 0))
                {
                    return (int)CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }
                var cashAdvanceHistory = cashAdvanceHistoryList[0];

                cashAdvance.CashReceived = cashReceived;          

                if (status == cashAdvance.Status)
                    return (int)CommonEnums.RET_CODE.INCORECT_STATE;

                if (status == (int)CommonEnums.ADVANCE_STATUS.PENDING)
                    return (int)CommonEnums.RET_CODE.INCORECT_STATE;

                if (status == (int)CommonEnums.ADVANCE_STATUS.PROCESSING)
                    if (cashAdvance.Status != (int)CommonEnums.ADVANCE_STATUS.PENDING)
                        return (int)CommonEnums.RET_CODE.INCORECT_STATE;

                if (status == (int)CommonEnums.ADVANCE_STATUS.CANCELLED)
                {
                    if (cashAdvance.Status != (int)CommonEnums.ADVANCE_STATUS.PENDING)
                        return (int)CommonEnums.RET_CODE.INCORECT_STATE;

                    if (cashAdvance.Status == (int)CommonEnums.ADVANCE_STATUS.PROCESSING || cashAdvance.Status == (int)CommonEnums.ADVANCE_STATUS.FINISHED)
                        return (int)CommonEnums.RET_CODE.ERROR_INVALID_CASH_ADVANCE;                    
                    cashAdvance.CashReceived = 0;
                    cashAdvance.Fee = 0;
                }

                if (status == (int)CommonEnums.ADVANCE_STATUS.FINISHED)
                {
                    if (cashAdvance.Status == (int)CommonEnums.ADVANCE_STATUS.CANCELLED || cashAdvance.Status == (int)CommonEnums.ADVANCE_STATUS.REJECTED)
                        return (int)CommonEnums.RET_CODE.INCORECT_STATE;
                }

                if (status == (int)CommonEnums.ADVANCE_STATUS.REJECTED)
                {
                    if (cashAdvance.Status == (int)CommonEnums.ADVANCE_STATUS.FINISHED)
                        return (int)CommonEnums.RET_CODE.INCORECT_STATE;
                    
                    cashAdvance.CashReceived = 0;
                    cashAdvance.Fee = 0;
                }

                cashAdvance.Status = status;
                cashAdvance.Reason = reason;
                cashAdvance.BrokerId = brokerId;
                cashAdvance.ExecTime = DateTime.Now;

                result = dataProvider.CashAdvanceProvider.Update(transactionManager, cashAdvance);
                if (result)
                {
                    cashAdvanceHistory.CashReceived = cashReceived;
                    cashAdvanceHistory.Status = status;
                    cashAdvanceHistory.Reason = reason;
                    cashAdvanceHistory.BrokerId = brokerId;
                    cashAdvanceHistory.ExecTime = cashAdvance.ExecTime;
                    if (status == (int)CommonEnums.ADVANCE_STATUS.CANCELLED || status == (int)CommonEnums.ADVANCE_STATUS.REJECTED)
                        cashAdvanceHistory.Fee = 0;
                    result = dataProvider.CashAdvanceHistoryProvider.Update(transactionManager, cashAdvanceHistory);
                    if (result)
                    {
                        return (int)CommonEnums.RET_CODE.SUCCESS;
                    }
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
                if (DomainUtil.HandleException(exc, LAYER_EXCEPTION_POLICY))
                    throw;
                #endregion Handle transaction rollback and exception
            }

            return (int)CommonEnums.RET_CODE.FAIL;
        }


        /// <summary>
        /// Rejects the cash advance expired.
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public void RejectCashAdvanceExpired(string reason)
        {
            #region Security and validation check
            // throws security exception if not authorized
            SecurityContext.IsAuthorized("RejectExpiredCashAdvance");

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
                transactionManager = ConnectionScope.ValidateOrCreateTransaction(NO_TRAN_BY_DEFAULT);
                dataProvider = ConnectionScope.Current.DataProvider;

                int count = 0;
                //list cash advace are pending
                var listCashAdvancePending = GetListCashAdvance(string.Empty, string.Empty,
                                                                (int) CommonEnums.ADVANCE_STATUS.PENDING, -1, 1,
                                                                int.MaxValue, out count);

                if(listCashAdvancePending!=null)
                {
                    foreach (var cashAdvance in listCashAdvancePending)
                    {
                        cashAdvance.Status = (int) CommonEnums.ADVANCE_STATUS.REJECTED;
                        cashAdvance.Reason = reason;
                        dataProvider.CashAdvanceProvider.Update(cashAdvance);
                    }
                }
                //list cash advance are processing
                var listCashAdvanceProcessing = GetListCashAdvance(string.Empty, string.Empty,
                                                                (int)CommonEnums.ADVANCE_STATUS.PROCESSING, -1, 1,
                                                                int.MaxValue, out count);

                foreach (var cashAdvance in listCashAdvanceProcessing)
                {
                    cashAdvance.Status = (int)CommonEnums.ADVANCE_STATUS.REJECTED;
                    cashAdvance.Reason = reason;
                    cashAdvance.CashReceived = 0;
                    cashAdvance.Fee = 0;
                    dataProvider.CashAdvanceProvider.Update(cashAdvance);
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
                if (DomainUtil.HandleException(exc, LAYER_EXCEPTION_POLICY))
                    throw;
                #endregion Handle transaction rollback and exception
            }           
        }
        #endregion

        #region Get
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
            var whereClause = new StringBuilder();
            //whereClause.AppendFormat("(BrokerID = '{0}') ", brokerId);
            if (!string.IsNullOrEmpty(subAccountId))
            {
                whereClause.AppendFormat("AND (SubAccountID LIKE '%{0}%') ", subAccountId);
            }
            if (!string.IsNullOrEmpty(contractNo))
            {
                whereClause.AppendFormat("AND (ContractNo LIKE '%{0}%') ", contractNo);
            }
            if (status >= 0)
            {
                whereClause.AppendFormat("AND (Status = {0}) ", status);
            }
            //get all stock transfer intra day
            whereClause.AppendFormat(String.Format("AND (AdvanceDate >= {0}) ", ETradeCommon.Constants.SQL_CONVERT_DATETIME), DateTime.Now.ToString("dd/MM/yyyy"));
            whereClause.AppendFormat("AND (AdvanceDate < " + ETradeCommon.Constants.SQL_CONVERT_DATETIME + ") ", DateTime.Now.AddDays(1).ToString("dd/MM/yyyy"));

            if (tradeType >= 0)
            {
                whereClause.AppendFormat("AND (TradeType = {0}) ", tradeType);
            }

            //get cash advance pending or processing in history
            whereClause.AppendFormat("OR (Status='{0}' OR Status='{1}')",
                                     (int)CommonEnums.ADVANCE_STATUS.PENDING,
                                     (int)CommonEnums.ADVANCE_STATUS.PROCESSING);
            string where = whereClause.ToString();

            if (!string.IsNullOrEmpty(where))
            {
                where = where.Substring(4);
            }
            var list = GetPaged(where, "AdvanceDate DESC", pageIndex - 1, pageSize, out count);
            if (list != null)
            {
                return list.ToList();
            }
            return null;
        }        
        #endregion
	}//End Class

} // end namespace