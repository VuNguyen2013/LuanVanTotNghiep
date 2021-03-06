// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoreServices.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the CoreServices type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

//using ETradeCore.Services;

using ETradeCore.Services;

namespace AccountManager.Services
{
    using System.Collections.Generic;

    using AccountManager.DataAccess;
    using AccountManager.DataAccess.SqlClient;
    using AccountManager.Entities;
    
    using ETradeCommon;
    using ETradeCommon.Enums;
    using ETradeCore.Entities;
    using System.Linq;
    public class CoreServices
    {
        private ISbaCoreProvider coreProvider = new SqlInformixProvider();

        /// <summary>
        /// Gets the cust info from core.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <returns></returns>
        public ResultObject<List<CoreAccountInfo>> GetCustInfoFromCore(string accountId)
        {
            List<CoreAccountInfo> coreAccountInfos = coreProvider.GetCustInfoFromCore(accountId);
            
            if (coreAccountInfos == null)
            {
                return new ResultObject<List<CoreAccountInfo>>
                    {
                        ErrorMessage = CommonEnums.RET_CODE.NO_EXISTED_DATA.ToString(),
                        Result = null,
                        RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA
                    };
            }
            else
            {
                BankServices bankServices = new BankServices();                
                foreach (var coreAccountInfo in coreAccountInfos)
                {
                    BankAccountInfo bankAccountInfo = bankServices.GetBankAccountInfo(coreAccountInfo.SubAccount);
                    if(bankAccountInfo!=null)
                    {                        
                        coreAccountInfo.BankAccountType = bankAccountInfo.BankAccountType;
                    }
                }
                return new ResultObject<List<CoreAccountInfo>>
                    {
                        ErrorMessage = CommonEnums.RET_CODE.SUCCESS.ToString(),
                        Result = coreAccountInfos,
                        RetCode = CommonEnums.RET_CODE.SUCCESS
                    };    
            }            
        }
    }
}