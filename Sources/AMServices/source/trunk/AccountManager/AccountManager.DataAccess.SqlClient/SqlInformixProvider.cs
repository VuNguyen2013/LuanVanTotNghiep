// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlInformixProvider.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the SqlInformixProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AccountManager.DataAccess.SqlClient
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Odbc;
    using System.Diagnostics;

    using AccountManager.Entities;

    using ETradeCommon;
    using ETradeCommon.Enums;

    using ETradeCoreDB.Helper;

    public class SqlInformixProvider : ISbaCoreProvider
    {
        /// <summary>
        /// Gets the cust info from core.
        /// </summary>
        /// <param name="accountId">The account id. 
        /// This includes prefix: 085C/085F</param>
        /// <returns></returns>
        public List<CoreAccountInfo> GetCustInfoFromCore(string accountId)
        {
            OdbcConnection conn = null;
            OdbcDataReader dataReader = null;

            try
            {
                OdbcCommand cmd;               
                string cmdText =
                    "SELECT cus.accountno, cus.accountno2, cus.cardid, cus.cardissue, cus.placeissue, cus.name, cus.birthday, cus.sex, " +
                    "cus.occupation, cus.nationality, cus.address1, cus.telephone1, cus.fax1, cus.address2, cus.telephone2, cus.fax2, " +
                    "cus.address3, cus.telephone3, cus.fax3, cus.email, cus.branchcode, cus.branchname, cus.custodian, cus.customertype, " +
                    "cus.acctstatus, cus.opendate, cus.closedate, cus.telpassword, cus.mktid, cus.appcreditline, cus.canbuy, cus.cansell, " +
                    "acc.receivetype, acc.paymenttype " +
                    "FROM custinfo cus, accinfo acc where cus.accountno = '" + accountId + "' and acc.account=cus.accountno2";

                conn = DaoCommon.Connect();
                if (conn == null)
                {
                    LogHandler.Log(
                        "GetCustInfoFromCore: Error to connect to informix: " + AppConfig.AliasInformix,
                        GetType() + ".GetCustInfoFromCore",
                        TraceEventType.Error);
                    return null;
                }

                cmd = conn.CreateCommand();
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;

                // Retry out put
                dataReader = cmd.ExecuteReader();
                if (!dataReader.HasRows)
                {
                    LogHandler.Log(
                        string.Format("GetCustInfoFromCore: {0} not exist in SBA", accountId),
                        GetType() + ".GetCustInfoFromCore",
                        TraceEventType.Information);
                    return null;
                }

                var coreAccountInfos = new List<CoreAccountInfo>();

                while (dataReader.Read())
                {
                    CoreAccountInfo coreAccountInfo = new CoreAccountInfo();

                    coreAccountInfo.AccountNo = DaoCommon.GetFieldStringValue(dataReader, "accountno");
                    coreAccountInfo.AccountStatus = Utils.DecodeUtf8(DaoCommon.GetFieldStringValue(dataReader, "acctstatus"));
                    coreAccountInfo.Address = Utils.DecodeUtf8( DaoCommon.GetFieldStringValue(dataReader, "address1"));
                    coreAccountInfo.BirthDay = DaoCommon.GetFieldDateTimeValue(dataReader, "birthday");
                    coreAccountInfo.BranchCode = DaoCommon.GetFieldStringValue(dataReader, "branchcode");
                    coreAccountInfo.BranchName = Utils.DecodeUtf8(DaoCommon.GetFieldStringValue(dataReader, "branchname"));
                    coreAccountInfo.CanBuy = DaoCommon.GetFieldStringValue(dataReader, "canbuy") ==
                                             ((char)CommonEnums.CORE_TRADE_PERMISSION.CANBUY).
                                                 ToString()
                                                 ? true
                                                 : false;
                    coreAccountInfo.CanSell = DaoCommon.GetFieldStringValue(dataReader, "cansell") ==
                                             ((char)CommonEnums.CORE_TRADE_PERMISSION.CANSELL).
                                                 ToString()
                                                 ? true
                                                 : false;
                    coreAccountInfo.CardId = DaoCommon.GetFieldStringValue(dataReader, "cardid");
                    coreAccountInfo.PlaceIssue = Utils.DecodeUtf8(DaoCommon.GetFieldStringValue(dataReader, "placeissue"));
                    coreAccountInfo.CardIssue = DaoCommon.GetFieldDateTimeValue(dataReader, "cardissue");
                    coreAccountInfo.CloseDate = DaoCommon.GetFieldDateTimeValue(dataReader, "closedate");
                    coreAccountInfo.CustName =Utils.DecodeUtf8( DaoCommon.GetFieldStringValue(dataReader, "name"));
                    coreAccountInfo.CustomerType = DaoCommon.GetFieldStringValue(dataReader, "customertype");
                    coreAccountInfo.Email = DaoCommon.GetFieldStringValue(dataReader, "email");
                    
                    string mktInfo = DaoCommon.GetFieldStringValue(dataReader, "mktid");
                    
                    if (mktInfo.Split(' ').Length > 1)
                    {
                        coreAccountInfo.MktId = mktInfo.Split(' ')[0];
                        coreAccountInfo.MktName = mktInfo.Substring(mktInfo.Split(' ')[0].Length + 1);    
                    }

                    coreAccountInfo.OpenDate = DaoCommon.GetFieldDateTimeValue(dataReader, "opendate");
                    coreAccountInfo.Phone = DaoCommon.GetFieldStringValue(dataReader, "telephone1");
                    coreAccountInfo.Sex = DaoCommon.GetFieldStringValue(dataReader, "sex");
                    coreAccountInfo.SubAccount = DaoCommon.GetFieldStringValue(dataReader, "accountno2");                 
                    coreAccountInfos.Add(coreAccountInfo);
                }

                DaoCommon.DisConnect(conn, dataReader);

                return coreAccountInfos;
            }
            catch (OdbcException e)
            {
                DaoCommon.DisConnect(conn, dataReader);
                LogHandler.Log(
                    "GetCustInfoFromCore: EXCEPTION, ex = " + e + ", accountId = " + accountId,
                    GetType() + ".GetCustInfoFromCore",
                    TraceEventType.Error);
                return null;
            }
        }
    }
}