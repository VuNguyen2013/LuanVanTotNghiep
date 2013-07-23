// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountServicesTest.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the AccountServicesTest type.
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
    public class AccountServicesTest
    {
        [Test]
        public void GetTradePermission()
        {
            ISbaCoreProvider sbaCoreProvider = new SqlInformixProvider();
            
            string accountNo = "0088661";

            TradePermission tradePermission = sbaCoreProvider.GetTradePermission(accountNo);

            Assert.IsNotNull(tradePermission);

            Console.WriteLine("CanBuy: " + tradePermission.CanBuy);
            Console.WriteLine("CanSell: " + tradePermission.CanSell);
        }
    }
}