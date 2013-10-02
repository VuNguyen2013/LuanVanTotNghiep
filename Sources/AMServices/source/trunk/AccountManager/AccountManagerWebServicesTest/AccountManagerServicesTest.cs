using System;
using AccountManagerWebServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using AccountManager.Entities;
using System.Collections.Generic;

namespace AccountManagerWebServicesTest
{
    
    
    /// <summary>
    ///This is a test class for AccountManagerServicesTest and is intended
    ///to contain all AccountManagerServicesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AccountManagerServicesTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            //System.Diagnostics.Debugger.Launch();
            //System.Diagnostics.Debugger.Break();
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for CreateBroker
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("E:\\OTS\\svnProjects\\AMServices\\source\\trunk\\AccountManager\\AccountManagerWebServices", "/")]
        [UrlToTest("http://localhost/Sample.aspx")]
        public void CreateBrokerTest()
        {
            try
            {
                System.Diagnostics.Debugger.Break();
                AccountManagerServices target = new AccountManagerServices(); // TODO: Initialize to an appropriate value
                string brokerId = "binh.truong002"; // TODO: Initialize to an appropriate value
                string name = "binh.truong"; // TODO: Initialize to an appropriate value
                string password = "123456"; // TODO: Initialize to an appropriate value
                short accountType = 0; // TODO: Initialize to an appropriate value
                bool actived = true; // TODO: Initialize to an appropriate value
                string mobilePhone = "0907705533"; // TODO: Initialize to an appropriate value
                string email = "binh.truong@ots.vn"; // TODO: Initialize to an appropriate value
                List<int> permissionList = new List<int>(); // TODO: Initialize to an appropriate value
                int expected = 0; // TODO: Initialize to an appropriate value
                int actual;
                actual = target.CreateBroker(brokerId, name, password, accountType, actived, mobilePhone, email, permissionList);
                Assert.AreEqual(expected, actual);
                Assert.Inconclusive("Verify the correctness of this test method.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            
        }
    }
}
