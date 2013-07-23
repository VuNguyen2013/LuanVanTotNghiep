// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidateServicesTest.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the ValidateServicesTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using ETradeCore.Entities;
using ETradeWebServices.Services;

namespace ETradeCore.UnitTests
{
    using System;
    using System.Web.Script.Serialization;

    using AccountManager.Entities;

    using AMServices;
    using NUnit.Framework;
    using ETradeCommon.Enums;
    using ETradeCommon;

    [TestFixture]
    [Ignore("Ignore a fixture")]
    public class ValidateServicesTest
    {
        readonly ValidateServices _validateServices = new ValidateServices();

        readonly AccountManagerServices _accountManagerServices = new AccountManagerServices();

        private static readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();

        [Test]
        public void PutOrder()
        {
            string username = "000176";
            char marketId = (char)CommonEnums.MARKET_ID.HOSE;
            string accountNo = "0001761";
            string symbol = "SAM";
            char condPrice = Constants.ORDER_TYPE_ATC;
            char side = (char)CommonEnums.TRADE_SIDE.BUY;
            int volume = 100;
            decimal price = 17.9M;
            int accountType = (int)CommonEnums.ACCOUNT_TYPE.NORMAL;
            int customerType = (int)CommonEnums.CUSTOMER_TYPE.INTERNAL;

            string retVal = this._accountManagerServices.GetCustomerNoSession(username);

            var mainCustAccount = Serializer.Deserialize<ResultObject<MainCustAccount>>(
                retVal);

            var subCustAccountCollection = mainCustAccount.Result.SubCustAccountCollection;
            bool isMargin = ETradeCommon.Utils.GetAccountType(accountNo) ==
                            (int) ETradeCommon.Enums.CommonEnums.ACCOUNT_TYPE.MARGIN
                                ? true
                                : false;
            SubCustAccount tradingAccount = new SubCustAccount();
            foreach (SubCustAccount subCustAccount in subCustAccountCollection)
            {
                tradingAccount = subCustAccount;
            }

            CommonEnums.REJECT_REASON rejectReason = this._validateServices.IsValidNewOrder(marketId, accountNo, symbol,
                                                                                           side, volume, price,
                                                                                           condPrice, accountType,
                                                                                           customerType, tradingAccount,
                                                                                           new List<string>(), 
                                                                                           new StockAvailable
                                                                                               {AvaiVolume = 100}, -1,isMargin,(char)CommonEnums.CONDITION.NO_CONDITION);

            Assert.IsTrue(rejectReason == CommonEnums.REJECT_REASON.IS_VALID);

            Console.WriteLine("rejectReason: " + rejectReason);
        }
    }
}