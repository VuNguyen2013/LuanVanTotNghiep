using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace Updater.Repository
{
    public class HoseStockInfoRepository
    {
        private readonly string RTStockDataConnectionString = ConfigurationManager.ConnectionStrings["RTStockDataTest"].ConnectionString;
        public bool Insert(string content)
        {
            System.Web.Script.Serialization.JavaScriptSerializer _serialization = new System.Web.Script.Serialization.JavaScriptSerializer();
            var listData = _serialization.Deserialize<List<StockCore.Common.HoseStockInfoData>>(content);
            try
            {
                SqlConnection con = new SqlConnection(RTStockDataConnectionString);               
                foreach (var item in listData)
                {
                    SqlCommand com = con.CreateCommand();
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    com.CommandText = "HV_InsertHoseStockInfo";
                    com.Parameters.AddWithValue("@StockSymbol", item.StockSymbol);
                    com.Parameters.AddWithValue("@TradeDate",item.TradeDate);
                    com.Parameters.AddWithValue("@Ceiling",item.Ceiling);
                    com.Parameters.AddWithValue("@Floor",item.Floor);
                    com.Parameters.AddWithValue("@AvrPrice",item.AvrPrice);
                    com.Parameters.AddWithValue("@Last",item.Last);
                    com.Parameters.AddWithValue("@LastVol",item.LastVol);
                    com.Parameters.AddWithValue("@LastVal",item.LastVal);
                    com.Parameters.AddWithValue("@Highest",item.Highest);
                    com.Parameters.AddWithValue("@Lowest",item.Lowest);
                    com.Parameters.AddWithValue("@Totalshares",item.Totalshares);
                    com.Parameters.AddWithValue("@TotalValue",item.TotalValue);
                    com.Parameters.AddWithValue("@Best1Bid",item.Best1Bid);
                    com.Parameters.AddWithValue("@Best1BidVolume",item.Best1BidVolume);
                    com.Parameters.AddWithValue("@Best2Bid",item.Best2Bid);
                    com.Parameters.AddWithValue("@Best2BidVolume",item.Best2BidVolume);
                    com.Parameters.AddWithValue("@Best3Bid",item.Best3Bid);
                    com.Parameters.AddWithValue("@Best3BidVolume",item.Best3BidVolume);
                    com.Parameters.AddWithValue("@Best1Offer",item.Best1Offer);
                    com.Parameters.AddWithValue("@Best1OfferVolume",item.Best1OfferVolume);
                    com.Parameters.AddWithValue("@Best2Offer",item.Best2Offer);
                    com.Parameters.AddWithValue("@Best2OfferVolume",item.Best1OfferVolume);
                    com.Parameters.AddWithValue("@Best3Offer",item.Best3Offer);
                    com.Parameters.AddWithValue("@Best3OfferVolume",item.Best3OfferVolume);
                    com.Parameters.AddWithValue("@CurrentRoom",item.CurrentRoom);
                    com.Parameters.AddWithValue("@StartRoom", item.StartRoom);
                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
