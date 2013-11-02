using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace StockCore.InfoSender.Entities
{
    class IPInfo
    {
        public string IP { get; set; }
        public string CompanyName { get; set; }
        public IPInfo() { }
        public IPInfo(OleDbDataReader reader)
        {
            IP = reader["IP"].ToString();
            CompanyName = reader["CompanyName"].ToString();
        }
        public bool Insert()
        {
            var con = new OleDbConnection(StaticValues.ConnectionString);
            var command = con.CreateCommand();
            command.CommandText = "Insert into ListIP(IP,CompanyName) values('" + IP + "','" + CompanyName + "')";
            try
            {
                con.Open();
                var excute = command.ExecuteNonQuery();
                if (excute > 0)
                {
                    con.Close();
                    return true;
                }
                con.Close();
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool Update()
        {
            var con = new SqlConnection(StaticValues.ConnectionString);
            var command = con.CreateCommand();
            command.CommandText = "Update MemberStockCompany  set ServerIp='" + IP + "',CompanyName='" + CompanyName + "')";
            try
            {
                con.Open();
                var excute = command.ExecuteNonQuery();
                if (excute > 0)
                {
                    con.Close();
                    return true;
                }
                con.Close();
                return false;
            }
            catch
            {
                return false;
            }
        }
        public static bool Delete(string ip)
        {
            var con = new SqlConnection(StaticValues.ConnectionString);
            var command = con.CreateCommand();
            command.CommandText = "Delete from MemberStockCompany  where ServerIp='" + ip + "'";
            try
            {
                con.Open();
                var excute = command.ExecuteNonQuery();
                if (excute > 0)
                {
                    con.Close();
                    return true;
                }
                con.Close();
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
