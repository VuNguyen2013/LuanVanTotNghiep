﻿	

#region Using Directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Data;
using ETradeCommon.Enums;
using ETradeFinance.DataAccess.Bases;
using ETradeFinance.Entities;
using ETradeFinance.Entities.Validation;

using ETradeFinance.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using ETradeFinance.Entities;
using  ETradeCommon;
#endregion

namespace ETradeFinance.Services
{		
	/// <summary>
	/// An component type implementation of the 'CashTransfer' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class CashTransferService : ETradeFinance.Services.CashTransferServiceBase
	{
        private const string LAYER_EXCEPTION_POLICY = "ServiceLayerExceptionPolicy";
        private static readonly bool noTranByDefault = false;
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the CashTransferService class.
		/// </summary>
		public CashTransferService() : base()
		{
		}
		#endregion Constructors

        #region Method
        /// <summary>
        /// Puts the cash trans order.
        /// </summary>
        /// <param name="cashTransfer">The cash transfer.</param>
        /// <returns>
        /// <para> Result of putting cash transfer order. </para>
        /// <para>RET_CODE=FAIL: Putting order failed.</para>
        /// <para>RET_CODE=SUCCESS: Putting order successfully.</para>
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public int PutCashTransOrder(CashTransfer cashTransfer)
        {
            #region Security and validation check

            // throws security exception if not authorized
            SecurityContext.IsAuthorized("Insert");

            if (!cashTransfer.IsValid)
                throw new EntityNotValidException(cashTransfer, "Insert", cashTransfer.Error);

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
                result = dataProvider.CashTransferProvider.Insert(transactionManager, cashTransfer);
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

            if (!result)
            {
                return (int)CommonEnums.RET_CODE.FAIL;
            }
            return (int)CommonEnums.RET_CODE.SUCCESS;
        }

        /// <summary>
        /// Updates the cash trans order.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="status">The status.</param>
        /// <param name="ApprovedAmt">The approved amt.</param>
        /// <param name="note">The note.</param>
        /// <param name="brokerId">The broker id.</param>
        /// <param name="execTime">The exec time.</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public int UpdateCashTransOrder(long id,int status,decimal ApprovedAmt,string note,string brokerId,DateTime execTime)
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

                var cashTransfer = dataProvider.CashTransferProvider.GetById(id);

                if(cashTransfer==null)
                    return (int)CommonEnums.RET_CODE.NO_EXISTED_DATA;

                if (status == cashTransfer.Status)
                    return (int)CommonEnums.RET_CODE.INCORECT_STATE;

                if (status == (int)CommonEnums.CASH_TRANSFER_STATUS.PENDING)
                    return (int)CommonEnums.RET_CODE.INCORECT_STATE;

                if (status == (int)CommonEnums.CASH_TRANSFER_STATUS.PROCESSING)
                    if (cashTransfer.Status != (int)CommonEnums.CASH_TRANSFER_STATUS.PENDING)
                        return (int)CommonEnums.RET_CODE.INCORECT_STATE;

                if (status == (int)CommonEnums.CASH_TRANSFER_STATUS.CANCELLED)
                {
                    if (cashTransfer.Status != (int)CommonEnums.CASH_TRANSFER_STATUS.PENDING)
                        return (int)CommonEnums.RET_CODE.INCORECT_STATE;

                    if(cashTransfer.Status == (int)CommonEnums.CASH_TRANSFER_STATUS.PROCESSING || cashTransfer.Status == (int)CommonEnums.CASH_TRANSFER_STATUS.FINISHED)
                        return (int)CommonEnums.RET_CODE.ERROR_CANNOT_CANCEL_CASH_TRANSFER;
                }
                              
                if (status == (int)CommonEnums.CASH_TRANSFER_STATUS.FINISHED)
                {                  
                    if(cashTransfer.Status == (int)CommonEnums.CASH_TRANSFER_STATUS.CANCELLED || cashTransfer.Status == (int)CommonEnums.CASH_TRANSFER_STATUS.REJECTED)
                        return (int)CommonEnums.RET_CODE.INCORECT_STATE;

                    cashTransfer.ApprovedAmt = ApprovedAmt;
                    cashTransfer.TransferedAmt = ApprovedAmt;
                }

                if (status == (int)CommonEnums.CASH_TRANSFER_STATUS.REJECTED)
                {
                    if (cashTransfer.Status == (int)CommonEnums.CASH_TRANSFER_STATUS.FINISHED)
                        return (int)CommonEnums.RET_CODE.INCORECT_STATE;                                       
                }

                cashTransfer.Status = status;                
                cashTransfer.Note = note.Trim();
                cashTransfer.BrokerId = brokerId;
                cashTransfer.ExecTime = execTime;

                result = dataProvider.CashTransferProvider.Update(transactionManager, cashTransfer);

                if (!result)
                {
                    return (int)CommonEnums.RET_CODE.FAIL;
                }
                return (int)CommonEnums.RET_CODE.SUCCESS;

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
            return (int)CommonEnums.RET_CODE.SUCCESS;
        }

        /// <summary>
        /// Cancel the cash transfer.
        /// </summary>
        /// <param name="id">The cash transfer id.</param>
        /// <param name="execTime">The exec time.</param>
        /// <param name="note">The note.</param>
        /// <returns>
        /// <para>Result of cancelling cast trasfer.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=ERROR_CANNOT_CANCEL_CASH_TRANSFER: Cannot cancel cash transfer because it's in incorrect state.</para>
        /// <para>RET_CODE=SUCCESS: Cancel successfully.</para>
        /// <para>RET_CODE=FAIL: Fail to cancel cash transfer.</para>
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public int CancelCashTransfer(long id,DateTime execTime,string note)
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

                var cashTransfer = dataProvider.CashTransferProvider.GetById(id);
                if (cashTransfer == null)
                    return (int)CommonEnums.RET_CODE.NO_EXISTED_DATA;

                if (cashTransfer.Status != (int)CommonEnums.CASH_TRANSFER_STATUS.PENDING)
                    return (int)CommonEnums.RET_CODE.ERROR_CANNOT_CANCEL_CASH_TRANSFER;

                cashTransfer.Status = (int)CommonEnums.CASH_TRANSFER_STATUS.CANCELLED;
                cashTransfer.Note = note.Trim();
                cashTransfer.ExecTime = execTime;
                result = dataProvider.CashTransferProvider.Update(transactionManager, cashTransfer);

                if (!result)
                {
                    return (int)CommonEnums.RET_CODE.FAIL;
                }
                return (int)CommonEnums.RET_CODE.SUCCESS;

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
            return (int)CommonEnums.RET_CODE.SUCCESS;
        }

	    /// <summary>
        /// Gets the all list cash transfer by status.
        /// </summary>
        /// <param name="subAccountId">The subAccount Id.</param>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        public List<CashTransfer> GetListUnfinishedCashTransfer(string subAccountId)
        {
            var whereClause = new StringBuilder();

            if(!string.IsNullOrEmpty(subAccountId))
            {
                whereClause.AppendFormat("AND SrcAccountId ='{0}'", subAccountId);
            }

            whereClause.AppendFormat("AND (Status='{0}' OR Status='{1}')", (int)ETradeCommon.Enums.CommonEnums.CASH_TRANSFER_STATUS.PENDING, (int)ETradeCommon.Enums.CommonEnums.CASH_TRANSFER_STATUS.PROCESSING);

            string finalWhereClause = whereClause.ToString();
            if(!string.IsNullOrEmpty(finalWhereClause))
                finalWhereClause = finalWhereClause.Substring(4);

            int totalCount = 0;
            var list = GetPaged(finalWhereClause, "RequestTime DESC", 0, int.MaxValue, out totalCount);
            return list.ToList();
        }

        /// <summary>
        /// Gets the total unfinished transfer amount. (on status pending or processing)
        /// </summary>
        /// <param name="subAccountId">The sub account id.</param>
        /// <returns></returns>
        public decimal GetTotalUnfinishedCashTransferAmount(string subAccountId)
        {
            decimal cashTransferAmount = 0;
            List<CashTransfer> listUnfinishedCashTransfer= GetListUnfinishedCashTransfer(subAccountId);
            if (listUnfinishedCashTransfer != null && listUnfinishedCashTransfer.Count > 0)
                cashTransferAmount = listUnfinishedCashTransfer.Sum(n => n.RequestAmt);
            return cashTransferAmount;
        }


        /// <summary>
        /// Gets the cash transfer.
        /// </summary>
        /// <param name="cashTransferId">The cash transfer id.</param>
        /// <returns></returns>
        public CashTransfer GetCashTransfer(int cashTransferId)
        {
            return GetById(cashTransferId);           
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
        /// <returns></returns>
        public PagingObject<List<CashTransfer>> GetListCashTransOrder(string sourceAccountID, string destAccountID, int transType, string note, string brokerID, int pageIndex, int pageSize)
        {
            var whereClause = new StringBuilder();
            if (!string.IsNullOrEmpty(sourceAccountID))
            {
                whereClause.AppendFormat("AND SrcAccountId LIKE '%{0}%'", sourceAccountID);
            }

            if (!string.IsNullOrEmpty(destAccountID))
            {
                whereClause.AppendFormat("AND DestAccountId LIKE '%{0}%'", destAccountID);
            }

            //get all cash transfer intra day
            whereClause.AppendFormat("AND (RequestTime >= " + ETradeCommon.Constants.SQL_CONVERT_DATETIME + ") ", DateTime.Now.ToString("dd/MM/yyyy"));
            whereClause.AppendFormat("AND (RequestTime < " + ETradeCommon.Constants.SQL_CONVERT_DATETIME + ") ", DateTime.Now.AddDays(1).ToString("dd/MM/yyyy"));


            if (transType >= 0)
            {
                whereClause.AppendFormat("AND TransType = '{0}'", transType);
            }

            if (!string.IsNullOrEmpty(note))
            {
                whereClause.AppendFormat("AND Note LIKE '%{0}%'", note);
            }

            if (!string.IsNullOrEmpty(brokerID))
            {
                whereClause.AppendFormat("AND BrokerId LIKE '%{0}%'", brokerID);
            }

            //get cash transfer pending or processing in history
            whereClause.AppendFormat("OR (Status='{0}' OR Status='{1}')",
                                     (int)ETradeCommon.Enums.CommonEnums.CASH_TRANSFER_STATUS.PENDING,
                                     (int)ETradeCommon.Enums.CommonEnums.CASH_TRANSFER_STATUS.PROCESSING);

            string finalWhereClause = whereClause.ToString();
            if (!string.IsNullOrEmpty(finalWhereClause))
            {
                finalWhereClause = finalWhereClause.Substring(4);
            }

            int totalRecord;
            pageIndex = pageIndex - 1;

            var list = GetPaged(finalWhereClause, "RequestTime DESC", pageIndex, pageSize, out totalRecord);
            var listCashTransferOrderHistory = list.ToList();
            var returnObject = new PagingObject<List<CashTransfer>> { Data = listCashTransferOrderHistory, Count = totalRecord };
            return returnObject;
        }        

	    /// <summary>
        /// Cashes the trans order hist.
        /// </summary>
        /// <param name="sourceAccountID">The source account ID.</param>
        /// <param name="destAccountID">The dest account ID.</param>
        /// <param name="fromDate">From date, pass empty to return today</param>
        /// <param name="toDate">To date, pass empty to return today</param>
        /// <param name="transType">Type of the trans.</param>
        /// <param name="status">The status.</param>
        /// <param name="note">The note.</param>
        /// <param name="brokerID">The broker ID.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public PagingObject<List<CashTransfer>> GetListCashTransOrderHist(string sourceAccountID,string destAccountID,string fromDate,string toDate,int transType,int status,string note,string brokerID,int pageIndex,int pageSize)
        {
            var whereClause = new StringBuilder();
            if(!string.IsNullOrEmpty(sourceAccountID))
            {
                whereClause.AppendFormat("AND SrcAccountId LIKE '%{0}%'", sourceAccountID);
            }

            if(!string.IsNullOrEmpty(destAccountID))
            {
                whereClause.AppendFormat("AND DestAccountId LIKE '%{0}%'", destAccountID);
            }

            if (!string.IsNullOrEmpty(fromDate))
            {
                whereClause.AppendFormat("AND (RequestTime >= " + ETradeCommon.Constants.SQL_CONVERT_DATETIME + ") ", fromDate);
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                whereClause.AppendFormat("AND (RequestTime < " + ETradeCommon.Constants.SQL_CONVERT_DATETIME + ") ", toDate);
            }
          

            if(transType>=0)
            {
                whereClause.AppendFormat("AND TransType = '{0}'",transType);                
            }

            if(status>0)
                whereClause.AppendFormat("AND Status='{0}'",status);

            if(!string.IsNullOrEmpty(note))
            {
                whereClause.AppendFormat("AND Note LIKE '%{0}%'", note);
            }

            if(!string.IsNullOrEmpty(brokerID))
            {
                whereClause.AppendFormat("AND BrokerId LIKE '%{0}%'", brokerID);
            }

            string finalWhereClause = whereClause.ToString();
            if (!string.IsNullOrEmpty(finalWhereClause))
            {
                finalWhereClause = finalWhereClause.Substring(4);
            }

            int totalRecord;
            pageIndex = pageIndex - 1;

            var list = GetPaged(finalWhereClause, "RequestTime DESC", pageIndex, pageSize, out totalRecord);
            var listCashTransferOrderHistory=list.ToList();
            var returnObject = new PagingObject<List<CashTransfer>> {Data = listCashTransferOrderHistory,Count = totalRecord};
            return returnObject;
        }        
	    #endregion
    }//End Class

} // end namespace
