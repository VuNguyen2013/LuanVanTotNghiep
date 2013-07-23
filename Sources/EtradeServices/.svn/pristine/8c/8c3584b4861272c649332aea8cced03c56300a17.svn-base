// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DealServicesTest.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the DealServicesTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCore.UnitTests
{
    using System;
    using System.Collections.Generic;

    using ETradeCore.DataAccess;
    using ETradeCore.DataAccess.SqlClient;
    using ETradeCore.Entities;
    using ETradeCore.Services;

    using NUnit.Framework;

    [TestFixture]
    [Ignore("Ignore a fixture")]
    public class DealServicesTest
    {
        /// <summary>
        /// Gets the deal history.
        /// </summary>
        [Test]
        public void GetDealHistoryFromFisServices()
        {
            DealServices dealServices = new DealServices();

            string dealDate = "20101020";
            decimal orderNo = 1;
            int page = 0;

            List<DealHistory> dealHistories = dealServices.GetDealHistory(orderNo, dealDate, page);

            Assert.IsNotNull(dealHistories);

            foreach (DealHistory dealHistory in dealHistories)
            {
                Console.WriteLine("====================================");
                Console.WriteLine("ConfirmNo: " + dealHistory.ConfirmNo);
                Console.WriteLine("DealDate: " + dealHistory.DealDate);
                Console.WriteLine("DealPrice: " + dealHistory.DealPrice);
                Console.WriteLine("DealTime: " + dealHistory.DealTime);
                Console.WriteLine("DealVolume: " + dealHistory.DealVolume);
                Console.WriteLine("OrderNo: " + dealHistory.OrderNo);
                Console.WriteLine("SumComm: " + dealHistory.SumComm);
                Console.WriteLine("SumVat: " + dealHistory.SumVat);
            }
        }

        /// <summary>
        /// Gets the deal history.
        /// </summary>
        [Test]
        public void GetDealHistoryDao()
        {
            ISbaCoreProvider sbaCoreProvider = new SqlInformixProvider();

            decimal orderNo = 11;
            string dealDate = "20090707";
            int page = 0;

            // Not filter by date
            List<DealHistory> dealHistories = sbaCoreProvider.GetDealHistory(orderNo, dealDate, page);

            Assert.IsTrue(dealHistories != null && dealHistories.Count > 0);

            foreach (DealHistory dealHistory in dealHistories)
            {
                Console.WriteLine("========================");
                Console.WriteLine("DealDate: " + dealHistory.DealDate);
                Console.WriteLine("DealTime: " + dealHistory.DealTime);
                Console.WriteLine("DealVolume: " + dealHistory.DealVolume);
                Console.WriteLine("DealPrice: " + dealHistory.DealPrice);
                Console.WriteLine("OrderNo: " + dealHistory.OrderNo);
            }
        }

        /// <summary>
        /// Gets the deal history service.
        /// </summary>
        [Test]
        public void GetDealHistoryService()
        {
            DealServices dealServices = new DealServices();

            decimal orderNo = 11;
            string dealDate = "20090707";
            int page = 0;
            
            // Filter by date
            List<DealHistory> dealHistories = dealServices.GetDealHistory(orderNo, dealDate, page);

            Assert.IsTrue(dealHistories != null && dealHistories.Count > 0);

            foreach (DealHistory dealHistory in dealHistories)
            {
                Console.WriteLine("========================");
                Console.WriteLine("DealDate: " + dealHistory.DealDate);
                Console.WriteLine("DealTime: " + dealHistory.DealTime);
                Console.WriteLine("DealVolume: " + dealHistory.DealVolume);
                Console.WriteLine("DealPrice: " + dealHistory.DealPrice);
                Console.WriteLine("OrderNo: " + dealHistory.OrderNo);
            }
        }
    }
}