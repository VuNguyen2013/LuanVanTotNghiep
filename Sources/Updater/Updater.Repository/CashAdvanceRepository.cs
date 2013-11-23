using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;
using System.Configuration;


namespace Updater.Repository
{
    public class CashAdvanceRepository
    {
        private readonly string _etradeOrdersTestConString = ConfigurationManager.ConnectionStrings["EtradeOrdersTest"].ConnectionString;
        public void ProccessNewCashAdvance()
        {
            SqlConnection con = new SqlConnection(_etradeOrdersTestConString);
            var com = con.CreateCommand();
            var com2 = con.CreateCommand();
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com2.CommandType = System.Data.CommandType.StoredProcedure;
            com.CommandText = "GetAllCashAdvance";
            com2.CommandText = "UpdateFinishCashAdvance";
            while (true)
            {
                con.Open();
                var reader = com.ExecuteReader();
                while (reader.Read())
                {
                    long id = long.Parse(reader["ID"].ToString());
                    com2.Parameters.AddWithValue("@ID",id);
                    com2.ExecuteNonQuery();
                }
                con.Close();
                Thread.Sleep(20000);
            }
        }
    }
}
