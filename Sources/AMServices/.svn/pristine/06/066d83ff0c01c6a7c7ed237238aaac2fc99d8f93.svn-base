	

#region Using Directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Data;

using AccountManager.Entities;
using AccountManager.Entities.Validation;

using AccountManager.DataAccess;
using ETradeCommon;
using Microsoft.Practices.EnterpriseLibrary.Logging;

#endregion

namespace AccountManager.Services
{		
	/// <summary>
	/// An component type implementation of the 'CustomerActionHistory' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class CustomerActionHistoryService : AccountManager.Services.CustomerActionHistoryServiceBase
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the CustomerActionHistoryService class.
		/// </summary>
		public CustomerActionHistoryService() : base()
		{
		}
		#endregion Constructors

        ///<summary>
        ///</summary>
        ///<param name="brokerId"></param>
        ///<param name="actionTime"></param>
        ///<param name="mainCustAccountId"></param>
        ///<param name="subCustAccountId"></param>
        ///<param name="actionType"></param>
        ///<param name="reason"></param>
        ///<returns></returns>
        public bool InsertCustomerActionHistory(string brokerId, DateTime actionTime,
                                                string mainCustAccountId, string subCustAccountId, int actionType,
                                                int reason)
        {
            var customerActionHistory = new CustomerActionHistory
                                            {
                                                ActionTime = actionTime,
                                                MainCustAccountId = mainCustAccountId,
                                                ActionType = actionType
                                            };
            if (!string.IsNullOrEmpty(brokerId))
            {
                customerActionHistory.BrokerId = brokerId;
            }
            if (!string.IsNullOrEmpty(subCustAccountId))
            {
                customerActionHistory.SubCustAccountId = subCustAccountId;
            }
            if (reason >= 0)
            {
                customerActionHistory.Reason = reason;
            }
            var result = Insert(customerActionHistory);
            return result;
        }

	    /// <summary>
	    /// Get a collection of <see cref="CustomerActionHistory" /> entities.
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
	    ///<param name="totalRecord">Total record</param>
	    ///<returns></returns>
	    public PagingObject<List<CustomerActionHistory>> GetList(string brokerId, string fromDate, string toDate, 
            string mainCustAccountId, string subCustAccountId, int actionType, int reason, int pageIndex, int pageSize, out int totalRecord)
        {
            // Create dynamic query
            var whereClause = new StringBuilder();
            if (!string.IsNullOrEmpty(mainCustAccountId))
            {
                whereClause.AppendFormat("AND MainCustAccountID LIKE {0} ", "'%" + mainCustAccountId + "%' ");
            }

            if (!string.IsNullOrEmpty(fromDate))
            {
                whereClause.AppendFormat("AND (ActionTime >= " + Constants.SQL_CONVERT_DATETIME + ")",
                                         fromDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                whereClause.AppendFormat("AND (ActionTime < " + Constants.SQL_CONVERT_DATETIME + ")",
                                         toDate);
            }

            if (!string.IsNullOrEmpty(brokerId))
            {
                whereClause.AppendFormat("AND BrokerID LIKE {0} ", "'%" + brokerId + "%'");
            }

            if (!string.IsNullOrEmpty(subCustAccountId))
            {
                whereClause.AppendFormat("AND SubCustAccountId LIKE {0} ", "'%" + subCustAccountId + "%'");
            }

            if (actionType >= 0)
            {
                whereClause.AppendFormat("AND ActionType = {0} ", actionType);
            }

            if (reason >= 0)
            {
                whereClause.AppendFormat("AND Reason = {0} ", reason);
            }

            string finalWhereClause = whereClause.ToString();
            if (!string.IsNullOrEmpty(finalWhereClause))
            {
                finalWhereClause = finalWhereClause.Substring(4);
            }

            pageIndex = pageIndex - 1;
            var list = GetPaged(finalWhereClause, "ActionTime DESC", pageIndex, pageSize, out totalRecord);
            var listCustomerActionHistory = list.ToList();
            var returnObject = new PagingObject<List<CustomerActionHistory>> { Data = listCustomerActionHistory, Count = totalRecord };
            return returnObject;
        }
		
	}//End Class

} // end namespace
