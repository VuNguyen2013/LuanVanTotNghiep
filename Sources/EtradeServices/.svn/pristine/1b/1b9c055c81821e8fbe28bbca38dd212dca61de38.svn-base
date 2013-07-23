// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CashServicesTest.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the CashServicesTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCore.UnitTests
{
    using System;
    using System.Web.Script.Serialization;

    using ETradeCommon.Enums;

    using ETradeCore.Entities;
    using ETradeCore.Services;

    using NUnit.Framework;

    [TestFixture]
    [Ignore("Ignore a fixture")]
    public class CashServicesTest
    {
        private static readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();

        [Test]
        public void GetCashAvailable4NormalAccount()
        {
            CashServices cashAvailable = new CashServices();

            bool isConditionOrder = false;
            string accountNo = "0088661";
            CashAvailable cashBalance = cashAvailable.GetAvailableCash(accountNo, (int)CommonEnums.ACCOUNT_TYPE.NORMAL,isConditionOrder);
            
            string retVal = Serializer.Serialize(cashBalance);

            Assert.IsTrue(retVal != null);

            Console.WriteLine("BuyCredit: " + cashBalance.BuyCredit);
        }

        [Test]
        public void GetCashAvailable4MarginAccount()
        {
            CashServices cashService = new CashServices();

            string accountNo = "0000016";
            bool isConditionOrder = false;
            CashAvailable cashAvailable = cashService.GetAvailableCash(accountNo, (int)CommonEnums.ACCOUNT_TYPE.MARGIN,isConditionOrder);

            string retVal = Serializer.Serialize(cashAvailable);

            Assert.IsTrue(retVal != null);

            Console.WriteLine("BuyCredit: " + cashAvailable.BuyCredit);
            Console.WriteLine("EE: " + cashAvailable.EE);
            Console.WriteLine("PP: " + cashAvailable.PP);
            Console.WriteLine("IM: " + cashAvailable.IM);
        }

        [Test]
        public void GetCashBalance4MarginAccount()
        {
            CashServices cashService = new CashServices();

            string accountNo = "0000016";
            CashBalance cashBalance = cashService.GetCashBalance(accountNo, (int)CommonEnums.ACCOUNT_TYPE.MARGIN);

            string retVal = Serializer.Serialize(cashBalance);

            Assert.IsTrue(retVal != null);

            Console.WriteLine("BuyCredit: " + cashBalance.BuyCredit);
            Console.WriteLine("AMT_T1: " + cashBalance.AMT_T1);
            Console.WriteLine("AMT_T2: " + cashBalance.AMT_T2);
            Console.WriteLine("AMT_T3: " + cashBalance.AMT_T3);
            Console.WriteLine("CallForeSell: " + cashBalance.CallForeSell);
            Console.WriteLine("CallMargin: " + cashBalance.CallMargin);
            Console.WriteLine("CashBal: " + cashBalance.CashBal);
            Console.WriteLine("Dept: " + cashBalance.Dept);
            Console.WriteLine("EE: " + cashBalance.EE);
            Console.WriteLine("PP: " + cashBalance.PP);
            Console.WriteLine("OverDue: " + cashBalance.OverDue);
            Console.WriteLine("Payment: " + cashBalance.Payment);
            Console.WriteLine("Payment: " + cashBalance.PP);
            Console.WriteLine("TotalBuy: " + cashBalance.TotalBuy);
            Console.WriteLine("TotalSell: " + cashBalance.TotalSell);
            Console.WriteLine("WithDraw: " + cashBalance.WithDraw);
        }

        [Test]
        public void GetCashBalance4NormalAccount()
        {
            CashServices cashService = new CashServices();

            string accountNo = "0088661";
            CashBalance cashBalance = cashService.GetCashBalance(accountNo, (int)CommonEnums.ACCOUNT_TYPE.NORMAL);

            string retVal = Serializer.Serialize(cashBalance);

            Assert.IsTrue(retVal != null);

            Console.WriteLine("BuyCredit: " + cashBalance.BuyCredit);
            Console.WriteLine("AMT_T1: " + cashBalance.AMT_T1);
            Console.WriteLine("AMT_T2: " + cashBalance.AMT_T2);
            Console.WriteLine("AMT_T3: " + cashBalance.AMT_T3);
            Console.WriteLine("Dept: " + cashBalance.Dept);
            Console.WriteLine("OverDue: " + cashBalance.OverDue);
            Console.WriteLine("Payment: " + cashBalance.Payment);
            Console.WriteLine("Payment: " + cashBalance.PP);
            Console.WriteLine("TotalBuy: " + cashBalance.TotalBuy);
            Console.WriteLine("TotalSell: " + cashBalance.TotalSell);
            Console.WriteLine("WithDraw: " + cashBalance.WithDraw);
        }
    }
}