// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TradePermissionServicesTest.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the TradePermissionServicesTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCore.UnitTests
{
    using System;

    using ETradeCore.DataAccess;
    using ETradeCore.DataAccess.SqlClient;
    using ETradeCore.Entities;

    using NUnit.Framework;

    [TestFixture]
    [Ignore("Ignore a fixture")]
    public class TradePermissionServicesTest
    {
        private ISbaCoreProvider SbaCoreProvider = new SqlInformixProvider();

        [Test]
        public void GetTradePermission()
        {
            string accountNo = "0088661";

            TradePermission tradePermission = SbaCoreProvider.GetTradePermission(accountNo);

            Assert.IsTrue(tradePermission != null);

            Console.WriteLine("CanBuy: " + tradePermission.CanBuy);
            Console.WriteLine("CanSell: " + tradePermission.CanSell);
            Console.WriteLine("IsLock: " + tradePermission.IsLock);
        }
    }
}