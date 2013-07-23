	

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
	/// An component type implementation of the 'XROrders' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class XrOrdersService : ETradeFinance.Services.XrOrdersServiceBase
	{
        private const string LAYER_EXCEPTION_POLICY = "ServiceLayerExceptionPolicy";
        private static readonly bool noTranByDefault = false;

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the XrOrdersService class.
		/// </summary>
		public XrOrdersService() : base()
		{
		}
		#endregion Constructors

        #region Method

        /// <summary>
        /// Puts the XR order.
        /// </summary>
        /// <param name="xrOrders">The xr orders.</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public int PutXROrder(XrOrders xrOrders)
        {
            #region Security and validation check

            // throws security exception if not authorized
            SecurityContext.IsAuthorized("Insert");

            if (!xrOrders.IsValid)
                throw new EntityNotValidException(xrOrders, "Insert", xrOrders.Error);

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
                result = dataProvider.XrOrdersProvider.Insert(xrOrders);

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
        /// Updates the XR order.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="approvedVol">The approved vol.</param>
        /// <param name="status">The status.</param>
        /// <param name="brokerID">The broker ID.</param>
        /// <param name="ExecTime">The exec time.</param>
        /// <param name="note">The note.</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public int UpdateXROrder(long id,long approvedVol,int status,string brokerID,DateTime ExecTime,string note)
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

                var xrOder = dataProvider.XrOrdersProvider.GetById(id);
                if (xrOder == null)
                    return (int) CommonEnums.RET_CODE.NO_EXISTED_DATA;

                if (status == xrOder.Status)
                    return (int)CommonEnums.RET_CODE.INCORECT_STATE;

                if (status == (int)CommonEnums.XRORDER_STATUS.PENDING)
                    return (int)CommonEnums.RET_CODE.INCORECT_STATE;

                if (status == (int)CommonEnums.XRORDER_STATUS.PROCESSING)
                    if (xrOder.Status != (int)CommonEnums.XRORDER_STATUS.PENDING)
                        return (int)CommonEnums.RET_CODE.INCORECT_STATE;

                if (status == (int)CommonEnums.XRORDER_STATUS.CANCELLED)
                {
                    if (xrOder.Status != (int)CommonEnums.XRORDER_STATUS.PENDING)
                        return (int)CommonEnums.RET_CODE.INCORECT_STATE;

                    if (xrOder.Status == (int)CommonEnums.XRORDER_STATUS.PROCESSING || xrOder.Status == (int)CommonEnums.XRORDER_STATUS.FINISHED)
                        return (int)CommonEnums.RET_CODE.ERROR_CANNOT_CANCEL_BUY_RIGHT;

                    xrOder.ApprovedVol = 0;
                }

                if (status == (int)CommonEnums.XRORDER_STATUS.FINISHED)
                {
                    if (xrOder.Status == (int)CommonEnums.XRORDER_STATUS.CANCELLED || xrOder.Status == (int)CommonEnums.XRORDER_STATUS.REJECTED)
                        return (int)CommonEnums.RET_CODE.INCORECT_STATE;

                    xrOder.ApprovedVol = approvedVol;
                    xrOder.RegisteredVol = approvedVol;
                }

                if (status == (int)CommonEnums.XRORDER_STATUS.REJECTED)
                {
                    if (xrOder.Status == (int)CommonEnums.XRORDER_STATUS.FINISHED)
                        return (int)CommonEnums.RET_CODE.INCORECT_STATE;

                    xrOder.ApprovedVol = 0;
                }              
                xrOder.Status = status;
                xrOder.BrokerId = brokerID;
                xrOder.ExecTime = ExecTime;
                xrOder.Note = note;

                result = dataProvider.XrOrdersProvider.Update(transactionManager, xrOder);

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
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public int CancelXROrder(long id,string note)
        {
            try
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

                bool isBorrowedTransaction = ConnectionScope.Current.HasTransaction;
                transactionManager = ConnectionScope.ValidateOrCreateTransaction(noTranByDefault);
                dataProvider = ConnectionScope.Current.DataProvider;

                var xrOder = dataProvider.XrOrdersProvider.GetById(id);
                if (xrOder == null)
                    return (int)CommonEnums.RET_CODE.NO_EXISTED_DATA;

                if (xrOder.Status != (int)CommonEnums.XRORDER_STATUS.PENDING)
                    return (int)CommonEnums.RET_CODE.ERROR_CANNOT_CANCEL_XRORDER;

                xrOder.Status = (int)CommonEnums.XRORDER_STATUS.CANCELLED;
                xrOder.ApprovedVol = 0;
                xrOder.ExecTime = DateTime.Now;
                xrOder.Note = note;

                result = dataProvider.XrOrdersProvider.Update(transactionManager, xrOder);
                if (!result)
                {
                    return (int)CommonEnums.RET_CODE.FAIL;
                }
                return (int)CommonEnums.RET_CODE.SUCCESS;
            }
            catch (Exception exc)
            {
                //#region Handle transaction rollback and exception
                //if (transactionManager != null && transactionManager.IsOpen)
                //    transactionManager.Rollback();

                //Handle exception based on policy
                if (DomainUtil.HandleException(exc, LAYER_EXCEPTION_POLICY))
                    throw;
                //#endregion Handle transaction rol
            }
            return (int)CommonEnums.RET_CODE.SUCCESS;
        }

	    /// <summary>
        /// Gets the list unfinished XR order.
        /// </summary>
        /// <param name="subAccountId">The sub account id.</param>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <param name="market">The market.</param>
        /// <returns></returns>
        public List<XrOrders> GetListUnfinishedXROrder(long buyRightId, string subAccountId,string secSymbol,string market)
        {
            var whereClause = new StringBuilder();
            if(buyRightId>0)
            {
                whereClause.AppendFormat(" AND BuyRightID='{0}'", buyRightId);
            }
            if(!string.IsNullOrEmpty(subAccountId))
            {
                whereClause.AppendFormat("AND SubAccountID='{0}'",subAccountId);
            }
            if(!string.IsNullOrEmpty(secSymbol))
            {
                whereClause.AppendFormat("AND SecSymbol='{0}'", secSymbol);
            }
            if(!string.IsNullOrEmpty(market))
            {
                whereClause.AppendFormat("AND Market='{0}'",market);
            }
            whereClause.AppendFormat("AND (Status='{0}' OR Status='{1}')", (int)ETradeCommon.Enums.CommonEnums.XRORDER_STATUS.PENDING, (int)ETradeCommon.Enums.CommonEnums.XRORDER_STATUS.PROCESSING);
            var finalWhereClause = whereClause.ToString();
            if (!string.IsNullOrEmpty(finalWhereClause))
                finalWhereClause = finalWhereClause.Substring(4);

            int totalCount = 0;
            var list = GetPaged(finalWhereClause, "RequestTime DESC", 0, int.MaxValue, out totalCount);
            return list.ToList();
        }
        /// <summary>
        /// Gets the list finished XR order.
        /// </summary>
        /// <param name="buyRightId">The buy right id.</param>
        /// <param name="subAccountId">The sub account id.</param>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <param name="market">The market.</param>
        /// <returns></returns>
        public List<XrOrders> GetListFinishedXROrder(long buyRightId, string subAccountId,string secSymbol,string market)
        {
            var whereClause = new StringBuilder();
            if (buyRightId > 0)
            {
                whereClause.AppendFormat(" AND BuyRightID='{0}'", buyRightId);
            }
            if (!string.IsNullOrEmpty(subAccountId))
            {
                whereClause.AppendFormat("AND SubAccountID='{0}'", subAccountId);
            }
            if (!string.IsNullOrEmpty(secSymbol))
            {
                whereClause.AppendFormat("AND SecSymbol='{0}'", secSymbol);
            }
            if (!string.IsNullOrEmpty(market))
            {
                whereClause.AppendFormat("AND Market='{0}'", market);
            }
            whereClause.AppendFormat("AND Status='{0}' ", (int)ETradeCommon.Enums.CommonEnums.XRORDER_STATUS.FINISHED);
            var finalWhereClause = whereClause.ToString();
            if (!string.IsNullOrEmpty(finalWhereClause))
                finalWhereClause = finalWhereClause.Substring(4);

            int totalCount = 0;
            var list = GetPaged(finalWhereClause, "RequestTime DESC", 0, int.MaxValue, out totalCount);
            return list.ToList();
        }

	    /// <summary>
        /// Gets the total unfinished XR order register amount.
        /// </summary>
        /// <param name="subAccountId">The sub account id.</param>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <param name="market">The market.</param>
        /// <returns></returns>
        public long GetTotalUnfinishedXROrderRegisterAmount(long buyRightId, string subAccountId, string secSymbol, string market)
        {
            long registerAmount = 0;
            List<XrOrders> listUnfinishedBuyRight = GetListUnfinishedXROrder(buyRightId,subAccountId, secSymbol, market);
            if (listUnfinishedBuyRight != null && listUnfinishedBuyRight.Count > 0)
                registerAmount = listUnfinishedBuyRight.Sum(n => n.RequestVol.Value);
            return registerAmount;
        }

        /// <summary>
        /// Gets the total registed XR order amount.
        /// </summary>
        /// <param name="buyRightId">The buy right id.</param>
        /// <param name="subAccountId">The sub account id.</param>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <param name="market">The market.</param>
        /// <returns></returns>
        public long GetTotalRegistedXROrderAmount(long buyRightId, string subAccountId, string secSymbol, string market)
        {
            long totalUnfinishedXROrderRegisterAmount = GetTotalUnfinishedXROrderRegisterAmount(buyRightId, subAccountId, secSymbol, market);
            long totalFinishedXROrderRegisterAmount = GetTotalFinishedXROrderRegisterAmount(buyRightId, subAccountId,secSymbol, market);
            return totalFinishedXROrderRegisterAmount + totalUnfinishedXROrderRegisterAmount;
        }

	    /// <summary>
        /// Gets the total finished XR order register amount.
        /// </summary>
        /// <param name="buyRightId">The buy right id.</param>
        /// <param name="subAccountId">The sub account id.</param>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <param name="market">The market.</param>
        /// <returns></returns>
        public long GetTotalFinishedXROrderRegisterAmount(long buyRightId, string subAccountId, string secSymbol, string market)
        {
            long approvedAmount = 0;
            List<XrOrders> listFinishedBuyRight = GetListFinishedXROrder(buyRightId, subAccountId, secSymbol, market);
            if (listFinishedBuyRight != null && listFinishedBuyRight.Count > 0)
                approvedAmount = listFinishedBuyRight.Sum(n => n.ApprovedVol.Value);
            return approvedAmount;
        }

        public PagingObject<List<XrOrders>> GetListXROrder(long id, string subAccountId, string secSymbol, string market, string brokerID, string note, int pageIndex, int pageSize)
        {
            var whereClause = new StringBuilder();
            if (id > 0)
            {
                whereClause.AppendFormat(" AND ID='{0}'", id);
            }
            if (!string.IsNullOrEmpty(subAccountId))
            {
                whereClause.AppendFormat("AND SubAccountID LIKE '%{0}%'", subAccountId);
            }
            if (!string.IsNullOrEmpty(secSymbol))
            {
                whereClause.AppendFormat("AND SecSymbol LIKE '%{0}%'", secSymbol);
            }
            if (!string.IsNullOrEmpty(market))
            {
                whereClause.AppendFormat("AND Market LIKE '%{0}%'", market);
            }

            //get all xr order intra day
            whereClause.AppendFormat("AND (RequestTime >= " + ETradeCommon.Constants.SQL_CONVERT_DATETIME + ") ", DateTime.Now.ToString("dd/MM/yyyy"));
            whereClause.AppendFormat("AND (RequestTime < " + ETradeCommon.Constants.SQL_CONVERT_DATETIME + ") ", DateTime.Now.AddDays(1).ToString("dd/MM/yyyy"));
            
            if (!string.IsNullOrEmpty(brokerID))
            {
                whereClause.AppendFormat("AND BrokerID LIKE '{0}'", brokerID);
            }
            if (!string.IsNullOrEmpty(note))
            {
                whereClause.AppendFormat("AND Note LIKE '%{0}%'", note);
            }

            //get cash transfer pending or processing in history
            whereClause.AppendFormat("OR (Status='{0}' OR Status='{1}')",
                                     (int)ETradeCommon.Enums.CommonEnums.XRORDER_STATUS.PENDING,
                                     (int)ETradeCommon.Enums.CommonEnums.XRORDER_STATUS.PROCESSING);

            var finalWhereClause = whereClause.ToString();
            if (!string.IsNullOrEmpty(finalWhereClause))
                finalWhereClause = finalWhereClause.Substring(4);

            int totalCount = 0;
            var list = GetPaged(finalWhereClause, "RequestTime DESC", pageIndex - 1, pageSize, out totalCount);
            return new PagingObject<List<XrOrders>>() { Data = list.ToList(), Count = totalCount };
        }

	    /// <summary>
        /// Gets the XR order hist.
        /// </summary>
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
        /// <returns></returns>
        public PagingObject<List<XrOrders>> GetListXROrderHist(long id,string subAccountId,string secSymbol,string market,string fromDate,string toDate,int status,string brokerID,string note,int pageIndex,int pageSize)
        {
            var whereClause = new StringBuilder();
            if(id>0)
            {
                whereClause.AppendFormat(" AND ID='{0}'", id);
            }
	        if(!string.IsNullOrEmpty(subAccountId))
            {
                whereClause.AppendFormat("AND SubAccountID LIKE '%{0}%'", subAccountId);
            }
            if(!string.IsNullOrEmpty(secSymbol))
            {
                whereClause.AppendFormat("AND SecSymbol LIKE '%{0}%'", secSymbol);
            }
            if(!string.IsNullOrEmpty(market))
            {
                whereClause.AppendFormat("AND Market LIKE '%{0}%'", market);
            }
            if(!string.IsNullOrEmpty(fromDate))
            {
                var fromDateSearch = DateTime.ParseExact(toDate, "yyyyMMdd", null);
                fromDate = fromDateSearch.ToString("dd/MM/yyyy");
                whereClause.AppendFormat("AND (RequestTime >= " + ETradeCommon.Constants.SQL_CONVERT_DATETIME + ") ", fromDate);
            }
            if(!string.IsNullOrEmpty(toDate))
            {
                var toDateSearch = DateTime.ParseExact(toDate, "yyyyMMdd", null);
                toDateSearch = toDateSearch.AddDays(1);
                toDate = toDateSearch.ToString("dd/MM/yyyy");
                whereClause.AppendFormat("AND (RequestTime < " + ETradeCommon.Constants.SQL_CONVERT_DATETIME + ") ", toDate);
            }
           
            if(status>0)
            {
                whereClause.AppendFormat("AND Status='{0}'",status);
            }
            if(!string.IsNullOrEmpty(brokerID))
            {
                whereClause.AppendFormat("AND BrokerID LIKE '{0}'", brokerID);
            }
            if(!string.IsNullOrEmpty(note))
            {
                whereClause.AppendFormat("AND Note LIKE '%{0}%'", note);
            }
            var finalWhereClause = whereClause.ToString();
            if (!string.IsNullOrEmpty(finalWhereClause))
                finalWhereClause = finalWhereClause.Substring(4);

            int totalCount = 0;
            var list = GetPaged(finalWhereClause, "RequestTime DESC", pageIndex-1, pageSize, out totalCount);
            return new PagingObject<List<XrOrders>>(){Data = list.ToList(),Count = totalCount};
        }
	    #endregion
    }//End Class

} // end namespace
