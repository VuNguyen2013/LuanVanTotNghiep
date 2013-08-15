using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace StockCore.InfoSender.Repositories
{
    class IpInfoRepository
    {
        public static List<Entities.IPInfo> GetAll()
        {
            List<Entities.IPInfo> result = new List<Entities.IPInfo>();
            var con = new OleDbConnection(StaticValues.ConnectionString);
            var command = con.CreateCommand();
            command.CommandText = "select * from ListIP order by CompanyName";
            try
            {
                con.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Entities.IPInfo(reader));
                }
                con.Close();
            }
            catch
            {
            }
            return result;
        }
    }
}
