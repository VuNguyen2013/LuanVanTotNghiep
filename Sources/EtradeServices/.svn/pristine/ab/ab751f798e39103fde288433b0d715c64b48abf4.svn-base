// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderHistoryServicesTest.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the OrderHistoryServicesTest type.
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
    public class OrderHistoryServicesTest
    {
        /// <summary>
        /// Gets the order history.
        /// </summary>
        [Test]
        public void GetOrderHistoryFromFis()
        {
            OrderHistoryServices orderHistoryServices = new OrderHistoryServices();

            string accountNo = "0088661";
            string symbol = "all";
            string fromDate = "20101020";
            string toDate = "20101020";
            int orderStatus = (int)ETradeCommon.Enums.CommonEnums.FILTER_ORDER_STATUS.ALL;
            int pageNumber = 1;
            int pageSize = 10;

            List<OrderHistory> orderHistories = orderHistoryServices.GetOrderHistory(accountNo, fromDate, toDate, symbol, orderStatus, pageNumber, pageSize);

            Assert.IsTrue(orderHistories != null && orderHistories.Count > pageSize);

            foreach (OrderHistory orderHistory in orderHistories)
            {
                Console.WriteLine("====================================");
                Console.WriteLine("AccountNo: " + orderHistory.AccountNo);
                Console.WriteLine("CancelTime: " + orderHistory.CancelTime);
                Console.WriteLine("CancelVolume: " + orderHistory.CancelVolume);
                Console.WriteLine("Condition: " + orderHistory.Condition);
                Console.WriteLine("ConditionPrice: " + orderHistory.ConditionPrice);
                Console.WriteLine("EnterId: " + orderHistory.EnterId);
                Console.WriteLine("MatchVolume: " + orderHistory.MatchVolume);
                Console.WriteLine("OrderDate: " + orderHistory.OrderDate);
                Console.WriteLine("OrderNo: " + orderHistory.OrderNo);
                Console.WriteLine("OrderSeqNo: " + orderHistory.OrderSeqNo);
                Console.WriteLine("OrderStatus: " + orderHistory.OrderStatus);
                Console.WriteLine("OrderTime: " + orderHistory.OrderTime);
                Console.WriteLine("OrderType: " + orderHistory.OrderType);
                Console.WriteLine("Price: " + orderHistory.Price);
                Console.WriteLine("ServiceType: " + orderHistory.ServiceType);
                Console.WriteLine("Side: " + orderHistory.Side);
                Console.WriteLine("Symbol: " + orderHistory.Symbol);
                Console.WriteLine("Volume: " + orderHistory.Volume);
            }

        }

        [Test]
        public void GetOrderHistoryFromSbaDao()
        {
            ISbaCoreProvider sbaCoreProvider = new SqlInformixProvider();

            string accountNo = "0000191";
            string symbol = "CII";
            string fromDate = "";
            string toDate = "";
            int orderStatus = (int)ETradeCommon.Enums.CommonEnums.FILTER_ORDER_STATUS.ALL;
            int pageNumber = 1;
            int pageSize = 10;

            // Not filter by date
            List<OrderHistory> returnVal = sbaCoreProvider.GetOrderHistory(
                accountNo, fromDate, toDate, symbol, orderStatus, pageNumber, pageSize);

            Assert.IsNotNull(returnVal);

            foreach (OrderHistory orderHistory in returnVal)
            {
                Console.WriteLine("===============================");
                Console.WriteLine("AccountNo: " + orderHistory.AccountNo);
                Console.WriteLine("OrderDate: " + orderHistory.OrderDate.Split(' ')[0]);
                Console.WriteLine("OrderNo: " + orderHistory.OrderNo);
                Console.WriteLine("OrderStatus: " + orderHistory.OrderStatus);
                Console.WriteLine("OrderTime: " + orderHistory.OrderDate.Split(' ')[1]);
                Console.WriteLine("Price: " + orderHistory.Price);
                Console.WriteLine("Symbol: " + orderHistory.Symbol);
                Console.WriteLine("Volume: " + orderHistory.Volume);
            }
        }

        /// <summary>
        /// Gets the order history from sba services.
        /// </summary>
        [Test]
        public void GetOrderHistoryFromSbaServices()
        {
            string accountNo = "0000191";
            string symbol = "CII";
            string fromDate = "20100112";
            string toDate = "20100116";
            int orderStatus = (int)ETradeCommon.Enums.CommonEnums.FILTER_ORDER_STATUS.ALL;
            int pageNumber = 1;
            int pageSize = 10;
            
            OrderHistoryServices orderHistoryServices = new OrderHistoryServices();
            
            // Filter by date
            List<OrderHistory> orderHistories = orderHistoryServices.GetOrderHistory(
                accountNo, fromDate, toDate, symbol, orderStatus, pageNumber, pageSize);

            Assert.IsTrue(orderHistories != null && orderHistories.Count > 0);

            foreach (OrderHistory orderHistory in orderHistories)
            {
                Console.WriteLine("===============================");
                Console.WriteLine("AccountNo: " + orderHistory.AccountNo);
                Console.WriteLine("OrderDate: " + orderHistory.OrderDate);
                Console.WriteLine("OrderNo: " + orderHistory.OrderNo);
                Console.WriteLine("OrderStatus: " + orderHistory.OrderStatus);
                Console.WriteLine("OrderTime: " + orderHistory.OrderDate);
                Console.WriteLine("Price: " + orderHistory.Price);
                Console.WriteLine("Symbol: " + orderHistory.Symbol);
                Console.WriteLine("Volume: " + orderHistory.Volume);
            }
        }
    }
}