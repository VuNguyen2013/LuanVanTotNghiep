using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;

namespace Updater.Repository
{
    public class OrderRepository
    {
        private readonly string _etradeOrdersTestConString = ConfigurationManager.ConnectionStrings["EtradeOrdersTest"].ConnectionString;
        public void BrowseNewOrder()
        {
            SqlConnection con = new SqlConnection(_etradeOrdersTestConString);
            var com = con.CreateCommand();
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.CommandText = "GetAllNewOrder";            
            StockCoreServices.StockCoreServicesSoapClient service = new StockCoreServices.StockCoreServicesSoapClient();
            while (true)
            {
                con.Open();
                var reader = com.ExecuteReader();
                while (reader.Read())
                {
                    var result = service.ReceiveOrder(long.Parse(reader["OrderID"].ToString()),reader["SubCustAccountID"].ToString(),reader["SecSymbol"].ToString(),(long)(Decimal.Parse(reader["Price"].ToString())),short.Parse(reader["Volume"].ToString()),char.Parse(reader["Side"].ToString()));
                    if (result == (short)StockCore.Common.Enums.PUT_ORDER_STATUS.SUCCESS)
                    {
                        var con2 = new SqlConnection(_etradeOrdersTestConString);
                        var com2 = con2.CreateCommand();
                        com2.CommandType = System.Data.CommandType.StoredProcedure;
                        con2.Open();
                        com2.CommandText = "UpdateOrderStatus";
                        com2.Parameters.AddWithValue("@OrderId",reader["OrderId"].ToString());
                        com2.ExecuteNonQuery();
                        con2.Close();                        
                    }
                }
                con.Close();
                Thread.Sleep(2000);   
            }
        }
        public void UpdateStatusFromCore(string [] arrData)
        {
            SqlConnection con = new SqlConnection(_etradeOrdersTestConString);
            var com = con.CreateCommand();
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.CommandText = "HV_ExcuteOrderUpdateStatus";
            com.Parameters.AddWithValue("@OrderId",arrData[1]);
            com.Parameters.AddWithValue("@MatchedVolume", arrData[2]);
            var returnStatus = short.Parse(arrData[3]);
            short status = 1;
            if (returnStatus == (short)StockCore.Common.Enums.ORDER_STATUS.ALL_MATCHED)
            {
                status = (short)StockCore.Common.Enums.ORDER_STATUS_CLIENT.FULL_MATCHED;
            }
            else if (returnStatus == (short)StockCore.Common.Enums.ORDER_STATUS.PARTIAL_MATCHED)
            {
                status = (short)StockCore.Common.Enums.ORDER_STATUS_CLIENT.SEMI_MATCHED;
            }
            com.Parameters.AddWithValue("@Status",status);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
        }
    }
}
