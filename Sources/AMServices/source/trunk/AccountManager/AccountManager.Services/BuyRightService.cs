	

#region Using Directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Data;
using AccountManager.DataAccess.Bases;
using AccountManager.Entities;
using AccountManager.Entities.Validation;

using AccountManager.DataAccess;
using ETradeCommon;
using ETradeCommon.Enums;
using Microsoft.Practices.EnterpriseLibrary.Logging;

#endregion

namespace AccountManager.Services
{		
	/// <summary>
	/// An component type implementation of the 'BuyRight' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class BuyRightService : AccountManager.Services.BuyRightServiceBase
	{
        private const string LAYER_EXCEPTION_POLICY = "ServiceLayerExceptionPolicy";
        private static readonly bool noTranByDefault = false;

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the BuyRightService class.
		/// </summary>
		public BuyRightService() : base()
		{
		}
		#endregion Constructors

        #region Method
        /// <summary>
        /// Puts the buy right.
        /// </summary>
        /// <param name="subCustAccountID">The sub cust account ID.</param>
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
        /// <param name="createdUser">The created user.</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public int PutBuyRight(string subCustAccountID, string secSymbol,string market, string execDate, long owningVol, long allowedVol, decimal right, decimal rateRight, decimal price, string beginDateToRegister, string endDateToRegister, string beginDateToTransfer, string endDateToTransfer, string receivedDate, string note,string createdUser)
        {
         
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

                SubCustAccountService subCustAccountService=new SubCustAccountService();
                SubCustAccount subCustAccount= subCustAccountService.GetBySubCustAccountId(subCustAccountID);
                if (subCustAccount == null)
                    return (int) CommonEnums.RET_CODE.ERROR_NOT_EXIST_SUB_ACCOUNT;

                BuyRight buyRight = new BuyRight();
                buyRight.SubCustAccountId = subCustAccountID;
                buyRight.SecSymbol = secSymbol;
                buyRight.Market = market;
                buyRight.ExecDate = DateTime.ParseExact(execDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                buyRight.OwningVol = owningVol;
                buyRight.AllowedVol = allowedVol;
                buyRight.RegisteredVol = 0;
                buyRight.Right = right;
                buyRight.RateRight = rateRight;
                buyRight.Price = price;
                buyRight.BeginDateToRegister = DateTime.ParseExact(beginDateToRegister, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                buyRight.EndDateToRegister = DateTime.ParseExact(endDateToRegister, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                buyRight.BeginDateToTransfer = DateTime.ParseExact(beginDateToTransfer, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                buyRight.EndDateToTransfer = DateTime.ParseExact(endDateToTransfer, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                buyRight.ReceivedDate = DateTime.ParseExact(receivedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                buyRight.Note = note;
                buyRight.CreatedDate = DateTime.Now;
                buyRight.CreatedUser = createdUser;

                result = dataProvider.BuyRightProvider.Insert(buyRight);
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
        /// <param name="updatedUser">The updated user.</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public int UpdateBuyRight(long id, string secSymbol,string market, string execDate, long owningVol, long allowedVol, decimal right, decimal rateRight, decimal price, string beginDateToRegister, string endDateToRegister, string beginDateToTransfer, string endDateToTransfer, string receivedDate, string note,string updatedUser)
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

                var buyRight = dataProvider.BuyRightProvider.GetById(id);
                if (buyRight == null)
                    return (int) CommonEnums.RET_CODE.NO_EXISTED_DATA;
                buyRight.SecSymbol=secSymbol;
                buyRight.Market = market;
                buyRight.ExecDate = DateTime.ParseExact(execDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                buyRight.OwningVol=owningVol;
                buyRight.AllowedVol=allowedVol;
                buyRight.Right=right;
                buyRight.RateRight=rateRight;
                buyRight.Price=price;
                buyRight.BeginDateToRegister= DateTime.ParseExact(beginDateToRegister, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                buyRight.EndDateToRegister = DateTime.ParseExact(endDateToRegister, "dd/MM/yyyy", CultureInfo.InvariantCulture); ;
                buyRight.BeginDateToTransfer = DateTime.ParseExact(beginDateToTransfer, "dd/MM/yyyy", CultureInfo.InvariantCulture); ;
                buyRight.EndDateToTransfer = DateTime.ParseExact(endDateToTransfer, "dd/MM/yyyy", CultureInfo.InvariantCulture); ;
                buyRight.ReceivedDate = DateTime.ParseExact(receivedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture); ;
                buyRight.Note=note;
                buyRight.UpdatedDate = DateTime.Now;
                buyRight.CreatedUser=updatedUser;
                //RegisteredVol from buy right
                result = dataProvider.BuyRightProvider.Update(transactionManager, buyRight);
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
        /// Gets the list buy right.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="secSymbol">The sec symbol.</param>
        /// <param name="market">The market.</param>
        /// <param name="subCustAccountID">The sub cust account ID.</param>
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
        /// <returns></returns>
        public PagingObject<List<BuyRight>> GetListBuyRight(long id, string secSymbol, string market, string subCustAccountID, string execDate, string beginDateToRegister, string endDateToRegister, string beginDateToTransfer, string endDateToTransfer, string receivedDate, string note, string brokerID, int pageIndex, int pageSize)
        {                        
            var whereClause = new StringBuilder();
            if(id>0)
            {
                whereClause.AppendFormat(" AND Id ='{0}'", id);
            }
            if(!string.IsNullOrEmpty(secSymbol))
            {
                whereClause.AppendFormat(" AND SecSymbol LIKE '%{0}%'",secSymbol);
            }
            if(!string.IsNullOrEmpty(market))
            {
                whereClause.AppendFormat(" AND Market='{0}'",market);
            }
            if (!string.IsNullOrEmpty(subCustAccountID))
            {
                whereClause.AppendFormat(" AND SubCustAccountID LIKE '%{0}%'", subCustAccountID);
            }
            if(!string.IsNullOrEmpty(execDate))
            {
                DateTime execDateTime = DateTime.ParseExact(execDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                whereClause.AppendFormat(String.Format("AND (ExecDate >= {0}) ", Constants.SQL_CONVERT_DATETIME), execDateTime.ToString("dd/MM/yyyy"));
                whereClause.AppendFormat(string.Format("AND (ExecDate < {0}) ", Constants.SQL_CONVERT_DATETIME), execDateTime.AddDays(1).ToString("dd/MM/yyyy"));
            }

            if(!string.IsNullOrEmpty(beginDateToRegister))
            {
                var beginDateToRegisterSearch = DateTime.ParseExact(beginDateToRegister, "dd/MM/yyyy", null);                
                beginDateToRegister = beginDateToRegisterSearch.ToString("dd/MM/yyyy");
                whereClause.AppendFormat(string.Format(" AND (BeginDateToRegister >= {0}) ", Constants.SQL_CONVERT_DATETIME), beginDateToRegister);
            }

            if(!string.IsNullOrEmpty(endDateToRegister))
            {
                var endDateToRegisterSearch = DateTime.ParseExact(endDateToRegister, "dd/MM/yyyy", null);
                endDateToRegisterSearch.AddDays(1);
                endDateToRegister = endDateToRegisterSearch.ToString("dd/MM/yyyy");
                whereClause.AppendFormat(string.Format(" AND (EndDateToRegister < {0}) ", Constants.SQL_CONVERT_DATETIME), endDateToRegister);
            }

            if (!string.IsNullOrEmpty(beginDateToTransfer))
            {
                var beginDateToTransferSearch = DateTime.ParseExact(beginDateToTransfer, "dd/MM/yyyy", null);               
                beginDateToTransfer = beginDateToTransferSearch.ToString("dd/MM/yyyy");
                whereClause.AppendFormat(string.Format(" AND (BeginDateToTransfer >= {0}) ", Constants.SQL_CONVERT_DATETIME), beginDateToTransfer);
            }
            if (!string.IsNullOrEmpty(endDateToTransfer))
            {
                var endDateToTransferSearch = DateTime.ParseExact(endDateToTransfer, "dd/MM/yyyy", null);
                endDateToTransferSearch.AddDays(1);
                endDateToTransfer = endDateToTransferSearch.ToString("dd/MM/yyyy");
                whereClause.AppendFormat(string.Format(" AND (EndDateToTransfer < {0}) ", Constants.SQL_CONVERT_DATETIME), endDateToTransfer);
            }
            if(!string.IsNullOrEmpty(receivedDate))
            {               
                DateTime receivedDateTime = DateTime.ParseExact(receivedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                whereClause.AppendFormat(string.Format("AND (ReceivedDate >= {0}) ", Constants.SQL_CONVERT_DATETIME), receivedDateTime.ToString("dd/MM/yyyy"));
                whereClause.AppendFormat(string.Format("AND (ReceivedDate < {0}) ", Constants.SQL_CONVERT_DATETIME), receivedDateTime.AddDays(1).ToString("dd/MM/yyyy"));
            }
            if(!string.IsNullOrEmpty(note))
            {
                whereClause.AppendFormat(" AND Note LIKE '%{0}%'",note);
            }
            if(!string.IsNullOrEmpty(brokerID))
            {
                whereClause.AppendFormat(" AND CreatedUser LIKE '%{0}%'",brokerID);
            }

            var finalWhereClause = whereClause.ToString();
            if (!string.IsNullOrEmpty(finalWhereClause))
                finalWhereClause = finalWhereClause.Substring(4);

            int total = 0;
            var list = GetPaged(finalWhereClause, "CreatedDate DESC", pageIndex - 1, pageSize, out total);
            return new PagingObject<List<BuyRight>>(){Data = list.ToList(),Count = total};
        }

        /// <summary>
        /// Deletes the buy right.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Delete)]
        public int DeleteBuyRight(long id)
        {
            #region Security and validation check
            // throws security exception if not authorized
            SecurityContext.IsAuthorized("Delete");
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

                var buyRight = dataProvider.BuyRightProvider.GetById(id);                
                if (buyRight == null)
                    return (int) CommonEnums.RET_CODE.NO_EXISTED_DATA;
              
                result = dataProvider.BuyRightProvider.Delete(buyRight);
                if(result)
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
            return (int)CommonEnums.RET_CODE.FAIL;
        }

	    #endregion
    }//End Class

} // end namespace
