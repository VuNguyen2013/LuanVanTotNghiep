	

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
	/// An component type implementation of the 'OddLotOrder' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class OddLotOrderService : ETradeFinance.Services.OddLotOrderServiceBase
	{
        private const string LAYER_EXCEPTION_POLICY = "ServiceLayerExceptionPolicy";
        private static readonly bool noTranByDefault = false;
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the OddLotOrderService class.
		/// </summary>
		public OddLotOrderService() : base()
		{
		}
		#endregion Constructors

        #region Method
        /// <summary>
        /// Puts the odd lot order.
        /// </summary>
        /// <param name="oddLotOrder">The odd lot order.</param>
        /// <returns>
        /// <para>
        /// Result of putting odd lot order.
        /// </para>
        /// <para>RET_CODE=FAIL: Putting order failed.</para>
        /// <para>RET_CODE=SUCCESS: Putting order successfully.</para>
        /// </returns>    
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public int PutOddLotOrder(OddLotOrder oddLotOrder)
        {
            #region Security and validation check

            // throws security exception if not authorized
            SecurityContext.IsAuthorized("Insert");

            if (!oddLotOrder.IsValid)
                throw new EntityNotValidException(oddLotOrder, "Insert", oddLotOrder.Error);

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

                result = dataProvider.OddLotOrderProvider.Insert(transactionManager, oddLotOrder);
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
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public int UpdateOddLotOrder(long id,decimal execPrice,long execVol,long canceledVol,int status,string brokerID,string note)
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

                var oddLotOrder = dataProvider.OddLotOrderProvider.GetById(id);
                if (oddLotOrder == null)
                    return (int) CommonEnums.RET_CODE.NO_EXISTED_DATA;

                if (status == oddLotOrder.Status)
                    return (int)CommonEnums.RET_CODE.INCORECT_STATE;

                if (status == (int)CommonEnums.ODD_LOT_ORDER_STATUS.PENDING)
                    return (int)CommonEnums.RET_CODE.INCORECT_STATE;

                if (status == (int)CommonEnums.ODD_LOT_ORDER_STATUS.PROCESSING)
                    if (oddLotOrder.Status != (int)CommonEnums.ODD_LOT_ORDER_STATUS.PENDING)
                        return (int)CommonEnums.RET_CODE.INCORECT_STATE;

                if (status == (int)CommonEnums.ODD_LOT_ORDER_STATUS.CANCELLED)
                {
                    if (oddLotOrder.Status != (int)CommonEnums.ODD_LOT_ORDER_STATUS.PENDING)
                        return (int)CommonEnums.RET_CODE.INCORECT_STATE;

                    if (oddLotOrder.Status == (int)CommonEnums.ODD_LOT_ORDER_STATUS.PROCESSING || oddLotOrder.Status == (int)CommonEnums.ODD_LOT_ORDER_STATUS.FINISHED)
                        return (int)CommonEnums.RET_CODE.ERROR_CANNOT_CANCEL_ODD_LOT_ORDER;

                    oddLotOrder.ExecPrice = 0;
                    oddLotOrder.ExecVol = 0;
                    oddLotOrder.CanceledVol = oddLotOrder.Volume;
                }

                if (status == (int)CommonEnums.ODD_LOT_ORDER_STATUS.FINISHED)
                {
                    if (oddLotOrder.Status == (int)CommonEnums.ODD_LOT_ORDER_STATUS.CANCELLED || oddLotOrder.Status == (int)CommonEnums.ODD_LOT_ORDER_STATUS.REJECTED)
                        return (int)CommonEnums.RET_CODE.INCORECT_STATE;

                    oddLotOrder.ExecPrice = execPrice;
                    oddLotOrder.ExecVol = execVol;
                    oddLotOrder.CanceledVol = canceledVol;
                }

                if (status == (int)CommonEnums.ODD_LOT_ORDER_STATUS.REJECTED)
                {
                    if (oddLotOrder.Status == (int)CommonEnums.ODD_LOT_ORDER_STATUS.FINISHED)
                        return (int)CommonEnums.RET_CODE.INCORECT_STATE;

                    oddLotOrder.ExecPrice = 0;
                    oddLotOrder.ExecVol = 0;
                    oddLotOrder.CanceledVol = oddLotOrder.Volume;
                }

                oddLotOrder.Status = status;
                oddLotOrder.BrokerId = brokerID;
                oddLotOrder.ExecTime = DateTime.Now;
                oddLotOrder.Note = note;

                result = dataProvider.OddLotOrderProvider.Update(transactionManager, oddLotOrder);

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
        /// Cancel the odd lot order.
        /// </summary>
        /// <param name="id">The odd lot order id.</param>
        /// <param name="execTime">The executed time.</param>
        /// <param name="note">The note.</param>
        /// <returns>
        /// <para>Result of cancelling odd lot order.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: There is no data.</para>
        /// <para>RET_CODE=ERROR_CANNOT_CANCEL_ODD_LOT_ORDER: Cannot cancel odd lot order because it's in incorrect state.</para>
        /// <para>RET_CODE=SUCCESS: Cancel successfully.</para>
        /// <para>RET_CODE=FAIL: Fail to cancel odd lot order.</para>
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public int CancelOddLotOrder(long id,DateTime execTime,string note)
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

                var oddLotOrder = dataProvider.OddLotOrderProvider.GetById(id);
                if (oddLotOrder == null)
                    return (int)CommonEnums.RET_CODE.NO_EXISTED_DATA;

                if (oddLotOrder.Status != (int)CommonEnums.ODD_LOT_ORDER_STATUS.PENDING)
                    return (int)CommonEnums.RET_CODE.ERROR_CANNOT_CANCEL_ODD_LOT_ORDER;
               
                oddLotOrder.ExecPrice = 0;
                oddLotOrder.ExecVol = 0;
                oddLotOrder.CanceledVol = oddLotOrder.Volume;
                
                oddLotOrder.Status =(int) CommonEnums.ODD_LOT_ORDER_STATUS.CANCELLED;                
                oddLotOrder.ExecTime = execTime;
                oddLotOrder.Note = note;

                result = dataProvider.OddLotOrderProvider.Update(transactionManager, oddLotOrder);                

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
        /// <returns></returns>
        public PagingObject<List<OddLotOrder>> GetListOddLotOrder(string secSymbol, string side, string subCustAccountID, string market, string brokerID, string note, int pageIndex, int pageSize)
        {
            var whereClause = new StringBuilder();
            if (!string.IsNullOrEmpty(secSymbol))
            {
                whereClause.AppendFormat("AND SecSymbol LIKE '%{0}%'", secSymbol);
            }
            if (!string.IsNullOrEmpty(side))
            {
                whereClause.AppendFormat("AND Side='{0}'", side);
            }

            //get all cash transfer intra day
            whereClause.AppendFormat("AND (RequestTime >= " + ETradeCommon.Constants.SQL_CONVERT_DATETIME + ") ", DateTime.Now.ToString("dd/MM/yyyy"));
            whereClause.AppendFormat("AND (RequestTime < " + ETradeCommon.Constants.SQL_CONVERT_DATETIME + ") ", DateTime.Now.AddDays(1).ToString("dd/MM/yyyy"));
            
            if (!string.IsNullOrEmpty(subCustAccountID))
            {
                whereClause.AppendFormat("AND SubCustAccountID LIKE '%{0}%'", subCustAccountID);
            }
            if (!string.IsNullOrEmpty(market))
            {
                whereClause.AppendFormat("AND Market='{0}'", market);
            }
            
            if (!string.IsNullOrEmpty(brokerID))
            {
                whereClause.AppendFormat("AND BrokerID LIKE '%{0}%'", brokerID);
            }
            if (!string.IsNullOrEmpty(note))
            {
                whereClause.AppendFormat("AND Note LIKE '%{0}%'", note);
            }

            //get odd lot order pending or processing in history
            whereClause.AppendFormat("OR (Status='{0}' OR Status='{1}')",
                                     (int)ETradeCommon.Enums.CommonEnums.ODD_LOT_ORDER_STATUS.PENDING,
                                     (int)ETradeCommon.Enums.CommonEnums.ODD_LOT_ORDER_STATUS.PROCESSING);

            var finalWhereClause = whereClause.ToString();
            if (!string.IsNullOrEmpty(finalWhereClause))
                finalWhereClause = finalWhereClause.Substring(4);
            int totalCount = 0;
            var list = GetPaged(finalWhereClause, "RequestTime DESC", pageIndex - 1, pageSize, out totalCount);
            return new PagingObject<List<OddLotOrder>>() { Data = list.ToList(), Count = totalCount };
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
        /// <returns></returns>
        public PagingObject<List<OddLotOrder>> GetListOddLotOrderHist(string secSymbol,string side,string fromDate,string toDate,string subCustAccountID,string market,int status,string brokerID,string note,int pageIndex,int pageSize)
        {
            var whereClause = new StringBuilder();
            if(!string.IsNullOrEmpty(secSymbol))
            {
                whereClause.AppendFormat("AND SecSymbol LIKE '%{0}%'", secSymbol);
            }
            if(!string.IsNullOrEmpty(side))
            {
                whereClause.AppendFormat("AND Side='{0}'", side);
            }
            if (!string.IsNullOrEmpty(fromDate))
            {
                whereClause.AppendFormat("AND (RequestTime >= " + ETradeCommon.Constants.SQL_CONVERT_DATETIME + ") ", fromDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                whereClause.AppendFormat("AND (RequestTime < " + ETradeCommon.Constants.SQL_CONVERT_DATETIME + ") ", toDate);
            }
           
            if(!string.IsNullOrEmpty(subCustAccountID))
            {
                whereClause.AppendFormat("AND SubCustAccountID LIKE '%{0}%'", subCustAccountID);
            }
            if(!string.IsNullOrEmpty(market))
            {
                whereClause.AppendFormat("AND Market='{0}'",market);
            }
            if (status > 0)
                whereClause.AppendFormat("AND Status='{0}'",status);
            if(!string.IsNullOrEmpty(brokerID))
            {
                whereClause.AppendFormat("AND BrokerID LIKE '%{0}%'", brokerID);
            }
            if(!string.IsNullOrEmpty(note))
            {
                whereClause.AppendFormat("AND Note LIKE '%{0}%'",note);
            }

            var finalWhereClause = whereClause.ToString();
            if (!string.IsNullOrEmpty(finalWhereClause))
                finalWhereClause = finalWhereClause.Substring(4);
            int totalCount = 0;
            var list = GetPaged(finalWhereClause, "RequestTime DESC", pageIndex - 1, pageSize, out totalCount);
            return new PagingObject<List<OddLotOrder>>(){Data = list.ToList(),Count = totalCount};
        }

        /// <summary>
        /// Gets the total unfinished odd lot order amount.
        /// </summary>
        /// <param name="subAccountId">The sub account id.</param>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <returns></returns>
        public long GetTotalUnfinishedOddLotOrderAmount(string subAccountId, string secSymbol)
        {
            long totalUnfinisedOddLotOrderAmount = 0;
            var whereClause = new StringBuilder();
            if (!string.IsNullOrEmpty(secSymbol))
            {
                whereClause.AppendFormat("AND SecSymbol ='{0}'", secSymbol);
            }
            if (!string.IsNullOrEmpty(subAccountId))
            {
                whereClause.AppendFormat("AND SubCustAccountID ='{0}'", subAccountId);
            }
            whereClause.AppendFormat("AND (Status ='{0}' OR Status ='{1}') ",(int)CommonEnums.ODD_LOT_ORDER_STATUS.PENDING,(int) CommonEnums.ODD_LOT_ORDER_STATUS.PROCESSING);
            var finalWhereClause = whereClause.ToString();
            if (!string.IsNullOrEmpty(finalWhereClause))
                finalWhereClause = finalWhereClause.Substring(4);

            int total = 0;
            var list = GetPaged(finalWhereClause, "", 0, int.MaxValue, out total);            
            totalUnfinisedOddLotOrderAmount = (long) list.Sum(n => n.Volume);

            return totalUnfinisedOddLotOrderAmount;
        }

	    #endregion
    }//End Class

} // end namespace
