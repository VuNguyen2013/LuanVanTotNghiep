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
using ETradeCommon;
using ETradeCommon.Enums;
using ETradeFinance.DataAccess.Bases;
using ETradeFinance.Entities;
using ETradeFinance.Entities.Validation;

using ETradeFinance.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Logging;

#endregion

namespace ETradeFinance.Services
{		
	/// <summary>
	/// An component type implementation of the 'StockTransfer' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class StockTransferService : ETradeFinance.Services.StockTransferServiceBase
	{
        private const string LAYER_EXCEPTION_POLICY = "ServiceLayerExceptionPolicy";
        private static readonly bool noTranByDefault = false;

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the StockTransferService class.
		/// </summary>
		public StockTransferService() : base()
		{
		}
		#endregion Constructors

        #region Method
        /// <summary>
        /// Puts the stock trans order.
        /// </summary>
        /// <param name="stockTransfer">The stock transfer.</param>
        /// <returns>
        /// <para>Result of putting stock transfer order.</para>
        /// <para>RET_CODE=SUCCESS: Get data successfully.</para>
        /// <para>RET_CODE=FAIL: Failed to get data.</para>
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public int PutStockTransOrder(StockTransfer stockTransfer)
        {
            #region Security and validation check

            // throws security exception if not authorized
            SecurityContext.IsAuthorized("Insert");

            if (!stockTransfer.IsValid)
                throw new EntityNotValidException(stockTransfer, "Insert", stockTransfer.Error);

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
                result = dataProvider.StockTransferProvider.Insert(transactionManager, stockTransfer);
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
        /// Updates the stock trans order.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="status">The status.</param>
        /// <param name="ApprovedAmt">The approved amt.</param>
        /// <param name="note">The note.</param>
        /// <param name="brokerId">The broker id.</param>
        /// <param name="execTime">The exec time.</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public int UpdateStockTransOrder(long id, int status, long ApprovedAmt, string note, string brokerId, DateTime execTime)
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

                var stockTransfer = dataProvider.StockTransferProvider.GetById(id);
                if (stockTransfer == null)
                    return (int)CommonEnums.RET_CODE.NO_EXISTED_DATA;

                if (status == stockTransfer.Status)
                    return (int)CommonEnums.RET_CODE.INCORECT_STATE;

                if (status == (int)CommonEnums.STOCK_TRANSFER_STATUS.PENDING)
                    return (int)CommonEnums.RET_CODE.INCORECT_STATE;

                if (status == (int)CommonEnums.STOCK_TRANSFER_STATUS.PROCESSING)
                    if (stockTransfer.Status != (int)CommonEnums.STOCK_TRANSFER_STATUS.PENDING)
                        return (int)CommonEnums.RET_CODE.INCORECT_STATE;

                if (status == (int)CommonEnums.STOCK_TRANSFER_STATUS.CANCELLED)
                {
                    if (stockTransfer.Status != (int)CommonEnums.STOCK_TRANSFER_STATUS.PENDING)
                        return (int)CommonEnums.RET_CODE.INCORECT_STATE;

                    if (stockTransfer.Status == (int)CommonEnums.STOCK_TRANSFER_STATUS.PROCESSING || stockTransfer.Status == (int)CommonEnums.STOCK_TRANSFER_STATUS.FINISHED)
                        return (int)CommonEnums.RET_CODE.ERROR_CANNOT_CANCEL_STOCK_TRANSFER;

                    stockTransfer.ApprovedAmt = 0;
                }

                if (status == (int)CommonEnums.STOCK_TRANSFER_STATUS.FINISHED)
                {
                    if (stockTransfer.Status == (int)CommonEnums.STOCK_TRANSFER_STATUS.CANCELLED || stockTransfer.Status == (int)CommonEnums.STOCK_TRANSFER_STATUS.REJECTED)
                        return (int)CommonEnums.RET_CODE.INCORECT_STATE;

                    stockTransfer.ApprovedAmt = ApprovedAmt;
                    stockTransfer.TransferedAmt = ApprovedAmt;
                }

                if (status == (int)CommonEnums.STOCK_TRANSFER_STATUS.REJECTED)
                {
                    if (stockTransfer.Status == (int)CommonEnums.STOCK_TRANSFER_STATUS.FINISHED)
                        return (int)CommonEnums.RET_CODE.INCORECT_STATE;

                    stockTransfer.ApprovedAmt = 0;
                }

                stockTransfer.Status = status;
                stockTransfer.Note = note.Trim();
                stockTransfer.BrokerId = brokerId;
                stockTransfer.ExecTime = execTime;

                result = dataProvider.StockTransferProvider.Update(transactionManager, stockTransfer);

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
        /// Cancels the stock transfer.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="execTime">The exec time.</param>
        /// <param name="note">The note.</param>
        /// <returns>
        /// <para>Result of cancelling stock transfer.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=ERROR_CANNOT_CANCEL_STOCK_TRANSFER: Cannot cancel stock transfer because it's in incorrect state.</para>
        /// <para>RET_CODE=SUCCESS: Cancel successfully.</para>
        /// <para>RET_CODE=FAIL: Fail to cancel odd lot order.</para>
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public int CancelStockTransfer(long id,DateTime execTime,string note)
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

                var stockTransfer = dataProvider.StockTransferProvider.GetById(id);
                if (stockTransfer == null)
                    return (int)CommonEnums.RET_CODE.NO_EXISTED_DATA;

                if (stockTransfer.Status != (int)CommonEnums.STOCK_TRANSFER_STATUS.PENDING)
                    return (int)CommonEnums.RET_CODE.ERROR_CANNOT_CANCEL_STOCK_TRANSFER;

                stockTransfer.Status = (int) CommonEnums.STOCK_TRANSFER_STATUS.CANCELLED;
                stockTransfer.Note = note.Trim();                
                stockTransfer.ExecTime = execTime;
                result = dataProvider.StockTransferProvider.Update(transactionManager, stockTransfer);

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
        /// Gets the list unfinished stock transfer.
        /// </summary>
        /// <param name="subAccountId">The sub account id.</param>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <returns>the list stock transfers are unfinished </returns>
        public List<StockTransfer> GetListUnfinishedStockTransfer(string subAccountId,string secSymbol)
        {
            var whereClause = new StringBuilder();
            if(!string.IsNullOrEmpty(subAccountId))
            {
                whereClause.AppendFormat("AND SrcAccountID='{0}'",subAccountId);
            }
            
            if(!string.IsNullOrEmpty(secSymbol))
            {
                whereClause.AppendFormat("AND SecSymbol='{0}'",secSymbol);
            }

            whereClause.AppendFormat("AND (Status='{0}' OR Status='{1}')",(int) ETradeCommon.Enums.CommonEnums.STOCK_TRANSFER_STATUS.PENDING,(int)ETradeCommon.Enums.CommonEnums.STOCK_TRANSFER_STATUS.PROCESSING);

            string finalWhereClause = whereClause.ToString();
            if (!string.IsNullOrEmpty(finalWhereClause))
                finalWhereClause = finalWhereClause.Substring(4);

            int totalCount = 0;
            var list = GetPaged(finalWhereClause, "RequestTime DESC", 0, int.MaxValue, out totalCount);
            return list.ToList();
        }
        /// <summary>
        /// Gets the total unfinished stock transfer amount.
        /// </summary>
        /// <param name="subAccountId">The sub account id.</param>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <returns></returns>
        public long GetTotalUnfinishedStockTransferAmount(string subAccountId,string secSymbol)
        {
            long stockTransferAmount = 0;
            List<StockTransfer> listUnfinishedStockTransfer = GetListUnfinishedStockTransfer(subAccountId, secSymbol);
            if (listUnfinishedStockTransfer != null && listUnfinishedStockTransfer.Count > 0)
                stockTransferAmount = listUnfinishedStockTransfer.Sum(n => n.RequestAmt.Value);
            return stockTransferAmount;
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
        /// <returns></returns>
        public PagingObject<List<StockTransfer>> GetListStockTransOrder(string sourceAccountID, string destAccountID, string secSymbol,int transType, string note, string brokerID, int pageIndex, int pageSize)
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

            if (!string.IsNullOrEmpty(secSymbol))
            {
                whereClause.AppendFormat("AND SecSymbol LIKE '%{0}%'", secSymbol);
            }


            //get all stock transfer intra day
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

            //get stock transfer pending or processing in history
            whereClause.AppendFormat("OR (Status='{0}' OR Status='{1}')",
                                     (int)ETradeCommon.Enums.CommonEnums.STOCK_TRANSFER_STATUS.PENDING,
                                     (int)ETradeCommon.Enums.CommonEnums.STOCK_TRANSFER_STATUS.PROCESSING);


            string finalWhereClause = whereClause.ToString();
            if (!string.IsNullOrEmpty(finalWhereClause))
            {
                finalWhereClause = finalWhereClause.Substring(4);
            }

            int totalCount = 0;
            var list = GetPaged(finalWhereClause, "RequestTime DESC", pageIndex - 1, pageSize, out totalCount);
            var listStockTransferOrderHist = list.ToList();
            var returnObject = new PagingObject<List<StockTransfer>> { Data = listStockTransferOrderHist, Count = totalCount };
            return returnObject;
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
        /// <returns>Stock transfer information.</returns>
        public PagingObject<List<StockTransfer>> GetListStockTransOrderHist(string sourceAccountID,string destAccountID,string secSymbol,string fromDate,string toDate,int transType,int status,string note,string brokerID,int pageIndex,int pageSize)
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

            if(!string.IsNullOrEmpty(secSymbol))
            {
                whereClause.AppendFormat("AND SecSymbol LIKE '%{0}%'", secSymbol);
            }

            if (!string.IsNullOrEmpty(fromDate))
            {
                whereClause.AppendFormat("AND (RequestTime >= " + ETradeCommon.Constants.SQL_CONVERT_DATETIME + ") ", fromDate);
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                whereClause.AppendFormat("AND (RequestTime < " + ETradeCommon.Constants.SQL_CONVERT_DATETIME + ") ", toDate);
            }
          
            if (transType >= 0)
            {
                whereClause.AppendFormat("AND TransType = '{0}'", transType);                
            }
            if (status > 0)
                whereClause.AppendFormat("AND Status='{0}'", status);

            if (!string.IsNullOrEmpty(note))
            {
                whereClause.AppendFormat("AND Note LIKE '%{0}%'", note);
            }

            if (!string.IsNullOrEmpty(brokerID))
            {
                whereClause.AppendFormat("AND BrokerId LIKE '%{0}%'", brokerID);
            }

            string finalWhereClause = whereClause.ToString();
            if (!string.IsNullOrEmpty(finalWhereClause))
            {
                finalWhereClause = finalWhereClause.Substring(4);
            }

            int totalCount = 0;
            var list = GetPaged(finalWhereClause, "RequestTime DESC", pageIndex-1, pageSize, out totalCount);
            var listStockTransferOrderHist = list.ToList();
            var returnObject = new PagingObject<List<StockTransfer>> {Data = listStockTransferOrderHist,Count = totalCount};
            return returnObject;
        }

	    #endregion
    }//End Class

} // end namespace
