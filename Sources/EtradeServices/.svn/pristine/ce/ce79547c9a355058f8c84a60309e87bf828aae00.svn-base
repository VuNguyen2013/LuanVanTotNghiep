// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StockServicesTest.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the StockServicesTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCore.UnitTests
{
    using System;
    using System.Collections.Generic;
    using Entities;

    //using ETradeBalance.DataAccess.SqlClient;

    using ETradeCore.DataAccess;
    using ETradeCore.DataAccess.SqlClient;

    using Services;
    using NUnit.Framework;

    [TestFixture]
    [Ignore("Ignore a fixture")]
    public class StockServicesTest
    {
        /// <summary>
        /// Gets the stock available4 normal account.
        /// </summary>
        [Test]
        public void GetStockAvailable4NormalAccount()
        {
            StockServices stockServices = new StockServices();

            string accountNo = "0088661";
            string symbol = "ACB";

            StockAvailable stockBalance = stockServices.GetStockAvailable(accountNo, symbol,
                                                                        (int)ETradeCommon.Enums.CommonEnums.ACCOUNT_TYPE.NORMAL);

            Assert.IsNotNull(stockBalance);

            Console.WriteLine("AvaiVolume: " + stockBalance.AvaiVolume);
        }

        /// <summary>
        /// Gets the stock available4 margin account.
        /// </summary>
        [Test]
        public void GetStockAvailable4MarginAccount()
        {
            StockServices stockServices = new StockServices();

            string accountNo = "0000016";
            string symbol = "ACB";

            StockAvailable stockBalance = stockServices.GetStockAvailable(accountNo, symbol,
                                                                        (int)ETradeCommon.Enums.CommonEnums.ACCOUNT_TYPE.MARGIN);

            Assert.IsNotNull(stockBalance);

            Console.WriteLine("AvaiVolume: " + stockBalance.AvaiVolume);
        }

        [Test]
        public void GetPortfolio4MarginAccount()
        {
            StockServices stockServices = new StockServices();

            string accountNo = "0000016";

            Dictionary<String, PortfolioInfo> portfolioInfos = stockServices.GetPortfolioInfo
                (
                accountNo,
                (int)ETradeCommon.Enums.CommonEnums.ACCOUNT_TYPE.MARGIN
                );

            Assert.IsNotNull(portfolioInfos);

            foreach (KeyValuePair<string, PortfolioInfo> portfolioInfo in portfolioInfos)
            {
                Console.WriteLine("=========================================");
                Console.WriteLine("AvgPrice: " + portfolioInfo.Value.AvgPrice);
                Console.WriteLine("CanSell: " + portfolioInfo.Value.CanSell);
                Console.WriteLine("CurrentValue: " + portfolioInfo.Value.CurrentValue);
                Console.WriteLine("InvestValue: " + portfolioInfo.Value.GainLoss);
                Console.WriteLine("InvestValue: " + portfolioInfo.Value.InvestValue);
                Console.WriteLine("MarketPrice: " + portfolioInfo.Value.MarketPrice);
                Console.WriteLine("Percent: " + portfolioInfo.Value.Percent);
                Console.WriteLine("PledgeShare: " + portfolioInfo.Value.PledgeShare);
                Console.WriteLine("SecType: " + portfolioInfo.Value.SecType);
                Console.WriteLine("SellableShare: " + portfolioInfo.Value.SellableShare);
                Console.WriteLine("Symbol: " + portfolioInfo.Value.Symbol);
                Console.WriteLine("WTR: " + portfolioInfo.Value.WTR);
                Console.WriteLine("WTR_T1: " + portfolioInfo.Value.WTR_T1);
                Console.WriteLine("WTR_T2: " + portfolioInfo.Value.WTR_T2);
                Console.WriteLine("WTR_T3: " + portfolioInfo.Value.WTR_T3);
                Console.WriteLine("WTS: " + portfolioInfo.Value.WTS);
                Console.WriteLine("WTS_T1: " + portfolioInfo.Value.WTS_T1);
                Console.WriteLine("WTS_T2: " + portfolioInfo.Value.WTS_T2);
                Console.WriteLine("WTS_T3: " + portfolioInfo.Value.WTS_T3);
            }
        }

        [Test]
        public void GetPortfolio4NormalAccount()
        {
            StockServices stockServices = new StockServices();

            string accountNo = "0088661";

            Dictionary<String, PortfolioInfo> portfolioInfos = stockServices.GetPortfolioInfo
                (
                accountNo,
                (int)ETradeCommon.Enums.CommonEnums.ACCOUNT_TYPE.MARGIN
                );

            Assert.IsNotNull(portfolioInfos);

            foreach (KeyValuePair<string, PortfolioInfo> portfolioInfo in portfolioInfos)
            {
                Console.WriteLine("=========================================");
                Console.WriteLine("AvgPrice: " + portfolioInfo.Value.AvgPrice);
                Console.WriteLine("CanSell: " + portfolioInfo.Value.CanSell);
                Console.WriteLine("CurrentValue: " + portfolioInfo.Value.CurrentValue);
                Console.WriteLine("InvestValue: " + portfolioInfo.Value.GainLoss);
                Console.WriteLine("InvestValue: " + portfolioInfo.Value.InvestValue);
                Console.WriteLine("MarketPrice: " + portfolioInfo.Value.MarketPrice);
                Console.WriteLine("Percent: " + portfolioInfo.Value.Percent);
                Console.WriteLine("PledgeShare: " + portfolioInfo.Value.PledgeShare);
                Console.WriteLine("SecType: " + portfolioInfo.Value.SecType);
                Console.WriteLine("SellableShare: " + portfolioInfo.Value.SellableShare);
                Console.WriteLine("Symbol: " + portfolioInfo.Value.Symbol);
                Console.WriteLine("WTR: " + portfolioInfo.Value.WTR);
                Console.WriteLine("WTR_T1: " + portfolioInfo.Value.WTR_T1);
                Console.WriteLine("WTR_T2: " + portfolioInfo.Value.WTR_T2);
                Console.WriteLine("WTR_T3: " + portfolioInfo.Value.WTR_T3);
                Console.WriteLine("WTS: " + portfolioInfo.Value.WTS);
                Console.WriteLine("WTS_T1: " + portfolioInfo.Value.WTS_T1);
                Console.WriteLine("WTS_T2: " + portfolioInfo.Value.WTS_T2);
                Console.WriteLine("WTS_T3: " + portfolioInfo.Value.WTS_T3);
            }
        }

        [Test]
        public void GetPortfolioBySql4NormalAccount()
        {
            StockServices stockServices = new StockServices();

            string accountNo = "0088661";

            List<Portfolio> portfolioInfos = stockServices.GetPortfolioDirect4NormalAccount(accountNo);

            Assert.IsNotNull(portfolioInfos);

            foreach (Portfolio portfolioInfo in portfolioInfos)
            {
                Console.WriteLine("=====================================");
                Console.WriteLine("Available: " + portfolioInfo.Available);
                Console.WriteLine("Available: " + portfolioInfo.SecType);
                Console.WriteLine("SecType: " + portfolioInfo.Symbol);
                Console.WriteLine("AvgPrice: " + portfolioInfo.AvgPrice);
                Console.WriteLine("StartPrice: " + portfolioInfo.StartPrice);
            }
        }
    }
}