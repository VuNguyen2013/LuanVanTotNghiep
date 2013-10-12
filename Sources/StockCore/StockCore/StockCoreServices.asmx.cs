﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using StockCore;

namespace StockCore.Services
{
    /// <summary>
    /// Summary description for StockCoreServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class StockCoreServices : System.Web.Services.WebService
    {

        [WebMethod]
        public short ReceiveOrder(long clientOrderID, string accountNo, string stockSymbol, long price, short volume, char side)
        {
            Repositories.OrderRepository orderRep = new Repositories.OrderRepository();
            return orderRep.Insert(clientOrderID, accountNo, stockSymbol, price, volume, side);
        }
        [WebMethod]
        public Common.CashBalanceData GetCashBalance(string accountNo)
        {
            Common.CashBalanceData result = null;
            Repositories.SubCustAccountRepository subAccRep = new Repositories.SubCustAccountRepository();
            var subAcc = subAccRep.GetById(accountNo);
            if (subAcc != null)
            {
                result = new Common.CashBalanceData();
                result.WithDraw = subAcc.WithDraw;
                result.BuyCredit = subAcc.BuyCredit;
                result.TotalBuy = subAcc.TotalBuy;
                result.TotalSell = subAcc.TotalSell;
            }
            return result;
        }
        [WebMethod]
        public List<Common.StockBalanceData> GetStockBalaceByAccNo(string accountNo)
        {            
            Repositories.StockBalanceRespository stockRep = new Repositories.StockBalanceRespository();
            List<Common.StockBalanceData> result = stockRep.GetByAccountNo(accountNo);
            return result;
        }
        [WebMethod]
        public Common.StockBalanceData GetStockAvailable(string accountNo,string stockSymbol)
        {
            Repositories.StockBalanceRespository stockRep = new Repositories.StockBalanceRespository();
            var stockBalance = stockRep.GetByAccountNoAndSymbol(accountNo,stockSymbol);
            if (stockBalance != null)
            {
                Common.StockBalanceData result = new Common.StockBalanceData();
                result.Available = stockBalance.Available;
                result.StockSymbol = stockBalance.StockSymbol;
                result.SubCustAccountID = stockBalance.SubCustAccountID;
                result.Total = stockBalance.Total;
                result.WTR_T1 = stockBalance.WTR_T1;
                result.WTR_T2 = stockBalance.WTR_T2;
                result.WTS_T1 = stockBalance.WTS_T1;
                result.WTS_T2 = stockBalance.WTS_T2;
                return result;
            }
            else
                return null;
        }
    }
    
}
