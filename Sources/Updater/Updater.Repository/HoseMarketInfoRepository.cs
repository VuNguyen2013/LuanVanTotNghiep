﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace Updater.Repository
{
    public class HoseMarketInfoRepository
    {
        private readonly string RTStockDataConnectionString = ConfigurationManager.ConnectionStrings["RTStockDataTest"].ConnectionString;
        public bool Insert(string content)
        {
            System.Web.Script.Serialization.JavaScriptSerializer _serialization = new System.Web.Script.Serialization.JavaScriptSerializer();
            var listData = _serialization.Deserialize<List<StockCore.Common.HoseMarketInfoData>>(content);
            try
            {
                SqlConnection con = new SqlConnection(RTStockDataConnectionString);
                SqlCommand com = con.CreateCommand();
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "HV_InsertHoseMarketInfo";
                com.Parameters.AddWithValue("@TradeDate",StockCore.Common.CommonFunction.ConvertToSqlDateTime(listData[0].TradeDate));                
                com.Parameters.AddWithValue("@SetIndex", listData[0].SetIndex);
                com.Parameters.AddWithValue("@TotalTrade", listData[0].TotalTrade);
                com.Parameters.AddWithValue("@Totalshare", listData[0].Totalshare);
                com.Parameters.AddWithValue("@TotalValue", listData[0].TotalValue);
                com.Parameters.AddWithValue("@UpVolume", listData[0].UpVolume);
                com.Parameters.AddWithValue("@DownVolume", listData[0].DownVolume);
                com.Parameters.AddWithValue("@NoChangeVolume", listData[0].NoChangeVolume);
                com.Parameters.AddWithValue("@Advances", listData[0].Advances);
                com.Parameters.AddWithValue("@Declines", listData[0].Declines);
                com.Parameters.AddWithValue("@Nochange", listData[0].Nochange);
                com.Parameters.AddWithValue("@Status", listData[0].Status);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
