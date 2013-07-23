using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using ETradeCommon;
using HtmlAgilityPack;

namespace ETradeAutomation
{
    class CurrenciesParser
    {
        public void Parse()
        {
            string usdName = ConfigurationManager.AppSettings["USDName"].ToLower();
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            var sqlConnection = new SqlConnection(connectionString);
            int successfulRow = 0;
            try
            {
                var insertCommand = sqlConnection.CreateCommand();
                

                // Connection database
                sqlConnection.Open();

                insertCommand.CommandText =
                    "UPDATE Configurations SET Value=@Value WHERE Name='USD'";

                insertCommand.Parameters.Add("@Value", SqlDbType.VarChar);

                string data;
                string address = ConfigurationManager.AppSettings["CurrencyAddress"];
                var webRequest = WebUtils.CreateWebRequest(address, "", 0, "");

                using (var webResponse = (HttpWebResponse) webRequest.GetResponse())
                {
                    using (var stream = new StreamReader(webResponse.GetResponseStream()))
                    {
                        data = stream.ReadToEnd();
                    }
                }
                if (!string.IsNullOrEmpty(data))
                {
                    byte[] byteArray = Encoding.ASCII.GetBytes(data);
                    using (var memoryStream = new MemoryStream(byteArray))
                    {
                        //HtmlNode.ElementsFlags.Remove("div");
                        var doc = new HtmlDocument();
                        doc.Load(memoryStream);

                        var tableNode = doc.DocumentNode.SelectSingleNode("//table");
                        var trNodes = tableNode.Descendants("tr");
                        foreach (var trNode in trNodes)
                        {
                            var tdNodes = trNode.Descendants("td");
                            int countNode = 0;
                            bool isUSD = false;
                            string value = "0";
                            foreach (var tdNode in tdNodes)
                            {
                                countNode++;
                                if (tdNode.FirstChild != null)
                                {
                                    switch (countNode)
                                    {
                                        case 1:
                                            Console.Write(tdNode.WriteContentTo());
                                            var fullName = tdNode.FirstChild.WriteContentTo().Trim().ToLower();
                                            if (fullName == usdName)
                                            {
                                                isUSD = true;
                                            }
                                            Console.Write(fullName);
                                            break;
                                        case 6:
                                            value = tdNode.FirstChild.WriteContentTo().Trim().Replace(",", "");
                                            Console.WriteLine(" " + value);
                                            
                                            break;
                                    }
                                }
                            }
                            if (isUSD)
                            {
                                insertCommand.Parameters["@Value"].Value = value;

                                successfulRow = successfulRow + insertCommand.ExecuteNonQuery();
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, ETradeCommon.Constants.EXCEPTION_POLICY);
            }
            finally
            {
                sqlConnection.Close();
            }
            Console.WriteLine(string.Format("Successfully inserted {0} rows.", successfulRow));
            LogHandler.Log(string.Format("Successfully inserted {0} rows.", successfulRow), GetType() + ".Parser()",
                           TraceEventType.Information);
        }

        /*private static decimal? GetValue(string value)
        {
            decimal? returnValue;
            try
            {
                value = value.Replace(",", "");
                returnValue = decimal.Parse(value);
            }
            catch (Exception)
            {
                returnValue = null;
            }
            return returnValue;
        }*/
    }
}
