// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BankServices.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the StockServices type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;

namespace ETradeCore.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using ETradeCommon;
    using ETradeCommon.Enums;

    using DataAccess;
    using DataAccess.SqlClient;
    using Entities;

    public class BankServices
    {
        private readonly ISbaCoreProvider _sbaProvider = new SqlInformixProvider();
       
        #region Bank Account
        /// <summary>
        /// Gets the bank account info.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns></returns>
        public BankAccountInfo GetBankAccountInfo(string accountNo)
        {
            BankAccountInfo bankAccountInfo = _sbaProvider.GetBankAccountInfo(accountNo);
            if (bankAccountInfo == null)
            {
                return null;  
            }

            //02
            if (bankAccountInfo.PaymentType.Equals(Constants.Suffix_Account_Cash) && bankAccountInfo.ReceiveType.Equals(Constants.Suffix_Account_Cash))
            {
                if (!bankAccountInfo.BankCode.Equals("001") && !bankAccountInfo.BankCode.Equals("002") && !bankAccountInfo.BankCode.Equals("003"))
                {
                    return null;     
                }
                else
                {
                    bankAccountInfo.BankAccountType = CommonEnums.BANK_ACCOUNT_TYPE.BANKACC;
                    bankAccountInfo.CompAccNo = string.Empty;
                    return bankAccountInfo;
                }
            }
            //60
            if (bankAccountInfo.PaymentType.Equals(Constants.Suffix_Account_Margin) && bankAccountInfo.ReceiveType.Equals(Constants.Suffix_Account_Margin))
            {
                bankAccountInfo.BankAccountType = CommonEnums.BANK_ACCOUNT_TYPE.COMPACC;
                //bankAccountInfo.BankName = string.Empty;
                //bankAccountInfo.BankCode = string.Empty;
                //bankAccountInfo.BankAccNo = string.Empty;
                return bankAccountInfo;
            }
            //others
            return null;
        }


        #endregion
    }
}