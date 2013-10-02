	

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
using ETradeCommon.Enums;
using Microsoft.Practices.EnterpriseLibrary.Logging;

#endregion

namespace AccountManager.Services
{		
	/// <summary>
	/// An component type implementation of the 'OpenCustAccount' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class OpenCustAccountService : AccountManager.Services.OpenCustAccountServiceBase
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the OpenCustAccountService class.
		/// </summary>
		public OpenCustAccountService() : base()
		{
		}
		#endregion Constructors

        /// <summary>
        /// Get a complete collection of <see cref="BrokerAccount" /> entities.
        /// </summary>
        /// <returns></returns>
        public PagingObject<List<OpenCustAccount>> GetList(string cardId, string cardIssue, string placeIssue, 
                                          string name, string birthday, int sex, string occupation, string nationality,
                                          string address1, string telephone1, string fax1, string address2,
                                          string telephone2, string fax2, string address3, string telephone3,
                                          string fax3, string email, string branchCode, string branchName,
                                          int custodian, string customerType, int tradeAtCompany,
                                          int tradeByTelephone, int tradeOnline, int existedAccount,
                                          int pageIndex, int pageSize)
        {
            // Create dynamic query
            var whereClause = new StringBuilder();
            if (!string.IsNullOrEmpty(cardId))
            {
                whereClause.AppendFormat("AND CardId LIKE {0} ", "'%" + cardId + "%' ");
            }

            if (!string.IsNullOrEmpty(cardIssue))
            {//CONVERT(datetime, '{0}', 103)
                whereClause.AppendFormat("AND CardIssue = CONVERT(datetime, '{0}', 103)", cardIssue);
            }

            if (!string.IsNullOrEmpty(placeIssue))
            {
                whereClause.AppendFormat("AND PlaceIssue LIKE {0 } ", "'%" + placeIssue + "%'");
            }

            if (!string.IsNullOrEmpty(name))
            {
                whereClause.AppendFormat("AND Name LIKE {0} ", "'%" + name + "%'");
            }

            if (!string.IsNullOrEmpty(birthday))
            {
                whereClause.AppendFormat("AND Birthday = CONVERT(datetime, '{0}', 103) ", birthday);
            }

            if (sex >= 0)
            {
                whereClause.AppendFormat("AND Sex = {0} ", sex);
            }

            if (!string.IsNullOrEmpty(occupation))
            {
                whereClause.AppendFormat("AND Occupation LIKE {0} ", "'%" + occupation + "%'");
            }

            if (!string.IsNullOrEmpty(nationality))
            {
                whereClause.AppendFormat("AND Nationality LIKE {0} ", "'%" + nationality + "%'");
            }

            if (!string.IsNullOrEmpty(address1))
            {
                whereClause.AppendFormat("AND Address1 LIKE {0} ", "'%" + address1 + "%'");
            }

            if (!string.IsNullOrEmpty(telephone1))
            {
                whereClause.AppendFormat("AND Telephone1 LIKE {0} ", "'%" + telephone1 + "%'");
            }

            if (!string.IsNullOrEmpty(fax1))
            {
                whereClause.AppendFormat("AND Fax1 LIKE {0} ", "'%" + fax1 + "%'");
            }

            if (!string.IsNullOrEmpty(address2))
            {
                whereClause.AppendFormat("AND Address2 LIKE {0} ", "'%" + address2 + "%'");
            }

            if (!string.IsNullOrEmpty(telephone2))
            {
                whereClause.AppendFormat("AND Telephone2 LIKE {0} ", "'%" + telephone2 + "%'");
            }

            if (!string.IsNullOrEmpty(fax2))
            {
                whereClause.AppendFormat("AND Fax2 LIKE {0} ", "'%" + fax2 + "%'");
            }

            if (!string.IsNullOrEmpty(address3))
            {
                whereClause.AppendFormat("AND Address3 LIKE {0} ", "'%" + address3 + "%'");
            }

            if (!string.IsNullOrEmpty(telephone3))
            {
                whereClause.AppendFormat("AND Telephone3 LIKE {0} ", "'%" + telephone3 + "%'");
            }

            if (!string.IsNullOrEmpty(fax3))
            {
                whereClause.AppendFormat("AND Fax3 LIKE {0} ", "'%" + fax3 + "%'");
            }

            if (!string.IsNullOrEmpty(email))
            {
                whereClause.AppendFormat("AND Email LIKE {0} ", "'%" + email + "%'");
            }

            if (!string.IsNullOrEmpty(branchCode))
            {
                whereClause.AppendFormat("AND BranchCode LIKE {0} ", "'%" + branchCode + "%'");
            }

            if (!string.IsNullOrEmpty(branchName))
            {
                whereClause.AppendFormat("AND BranchName LIKE {0} ", "'%" + branchName + "%'");
            }

            if (custodian >= 0)
            {
                whereClause.AppendFormat("AND Custodian = {0} ", custodian);
            }

            if (!string.IsNullOrEmpty(customerType))
            {
                whereClause.AppendFormat("AND CustomerType LIKE {0} ", "'%" + customerType + "%'");
            }

            if (tradeAtCompany >= 0)
            {
                whereClause.AppendFormat("AND TradeAtCompany = {0} ", tradeAtCompany);
            }

            if (tradeByTelephone >= 0)
            {
                whereClause.AppendFormat("AND TradeByTelephone = {0} ", tradeByTelephone);
            }

            if (tradeOnline >= 0)
            {
                whereClause.AppendFormat("AND TradeOnline = {0} ", tradeOnline);
            }

            if (existedAccount >= 0)
            {
                whereClause.AppendFormat("AND ExistedAccount = {0} ", existedAccount);
            }

            string finalWhereClause = whereClause.ToString();
            if (!string.IsNullOrEmpty(finalWhereClause))
            {
                finalWhereClause = finalWhereClause.Substring(4);
            }

            int totalRecord;
            pageIndex = pageIndex - 1;
            var list = GetPaged(finalWhereClause, "", pageIndex, pageSize, out totalRecord);
            var listOpenCustAccounts = list.ToList();
            var returnObject = new PagingObject<List<OpenCustAccount>>
                                   {Data = listOpenCustAccounts, Count = totalRecord};
            return returnObject;
        }

        ///<summary>
        /// Update open cust account
        ///</summary>
        ///<param name="openCustAccount">Open Cust Account information</param>
        ///<returns></returns>
        public int UpdateOpenCustAccount(OpenCustAccount openCustAccount)
        {
            var oldOpenCustAccount = GetByOpenId(openCustAccount.OpenId);
            if (oldOpenCustAccount == null)
            {
                return (int) CommonEnums.RET_CODE.NO_EXISTED_DATA;
            }
            oldOpenCustAccount.CardId = openCustAccount.CardId;
            oldOpenCustAccount.CardIssue = openCustAccount.CardIssue;
            oldOpenCustAccount.PlaceIssue = openCustAccount.PlaceIssue;
            oldOpenCustAccount.Name = openCustAccount.Name;
            oldOpenCustAccount.Birthday = openCustAccount.Birthday;
            oldOpenCustAccount.Sex = openCustAccount.Sex;
            oldOpenCustAccount.Occupation = openCustAccount.Occupation;
            oldOpenCustAccount.Nationality = openCustAccount.Nationality;
            oldOpenCustAccount.Address1 = openCustAccount.Address1;
            oldOpenCustAccount.Telephone1 = openCustAccount.Telephone1;
            oldOpenCustAccount.Fax1 = openCustAccount.Fax1;
            oldOpenCustAccount.Address2 = openCustAccount.Address2;
            oldOpenCustAccount.Telephone2 = openCustAccount.Telephone2;
            oldOpenCustAccount.Fax2 = openCustAccount.Fax2;
            oldOpenCustAccount.Address3 = openCustAccount.Address3;
            oldOpenCustAccount.Telephone3 = openCustAccount.Telephone3;
            oldOpenCustAccount.Fax3 = openCustAccount.Fax3;
            oldOpenCustAccount.Email = openCustAccount.Email;
            oldOpenCustAccount.BranchCode = openCustAccount.BranchCode;
            oldOpenCustAccount.BranchName = openCustAccount.BranchName;
            oldOpenCustAccount.Custodian = openCustAccount.Custodian;
            oldOpenCustAccount.CustomerType = openCustAccount.CustomerType;
            oldOpenCustAccount.TradeAtCompany = openCustAccount.TradeAtCompany;
            oldOpenCustAccount.TradeByTelephone = openCustAccount.TradeByTelephone;
            oldOpenCustAccount.TradeOnline = openCustAccount.TradeOnline;
            oldOpenCustAccount.ExistedAccount = openCustAccount.ExistedAccount;
            bool result = Update(oldOpenCustAccount);
            if (result)
            {
                return (int) CommonEnums.RET_CODE.SUCCESS;
            }
            return (int) CommonEnums.RET_CODE.FAIL;
        }
		
	}//End Class

} // end namespace
