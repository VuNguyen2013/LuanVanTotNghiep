using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

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
        public short ReceiveOrder(long clientOrderID,string accountNo, string stockSymbol, long price, short volume, char side)
        {
            Repositories.OrderRepository orderRep = new Repositories.OrderRepository();
            return orderRep.Insert(clientOrderID, accountNo, stockSymbol, price, volume, side);
        }
    }
}
