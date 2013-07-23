// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActualTradeServicesTest.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the ActualTradeServicesTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCore.UnitTests
{
    using System;
    using System.Collections.Generic;

    using ETradeCore.Entities;
    using ETradeCore.Services;

    using NUnit.Framework;

    [TestFixture]
    [Ignore("Ignore a fixture")]
    public class ActualTradeServicesTest
    {
        [Test]
        public void GetActualTrade()
        {
            ActualTradeServices actualTradeServices = new ActualTradeServices();

            string accountNo = "0089981";
            string fromDate = "20081031";
            string toDate = "20081031";
            string symbol = "TSC";

            List<ActualTrade> actualTrades = actualTradeServices.GetActualTrading(accountNo, fromDate, toDate, symbol);

            Assert.IsTrue(actualTrades != null && actualTrades.Count > 0);

            foreach (ActualTrade actualTrade in actualTrades)
            {
                Console.WriteLine("===========================");

                Console.WriteLine("Commission: " + actualTrade.Commission);
                Console.WriteLine("ConPrice: " + actualTrade.ConPrice);
                Console.WriteLine("CustomerNo: " + actualTrade.CustomerNo);
                Console.WriteLine("Price: " + actualTrade.Price);
                Console.WriteLine("SettlementDate: " + actualTrade.SettlementDate);
                Console.WriteLine("Side: " + actualTrade.Side);
                Console.WriteLine("Symbol: " + actualTrade.Symbol);
                Console.WriteLine("TradeDate: " + actualTrade.TradeDate);
                Console.WriteLine("Volume: " + actualTrade.Volume);
            }
        }
    }
}