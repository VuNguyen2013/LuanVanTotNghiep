using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using InnoStockPortal.Entities;
using InnoStockPortal.Services;
using RTStockData.Entities;
using RTStockData.Services;


namespace AppUpdateIndexInnoStock
{
    class Program
    {
        private static string fullPath = Assembly.GetExecutingAssembly().Location;
        public static string currentDir = Path.GetDirectoryName(fullPath);
        private static string fileName = Path.GetFileNameWithoutExtension(fullPath);

        public static string configFile = currentDir + @"\" + fileName + ".ini";
        public static string logFile = currentDir + @"\Log\" + fileName + "-" + DateTime.Now.ToString("yyyyMMdd") + ".log";
        static void Main(string[] args)
        {
            try
            {
                string whereClauseRtIndex = "TRADED_DATE > GETDATE()-10";
                string orderBy = "TRADED_DATE DESC";
                int total;
                var rtService = new IndexsService();
                var listRtIndexs = rtService.GetPaged(whereClauseRtIndex, orderBy, 0, int.MaxValue, out total);
                if (total <= 0) return;
                DateTime? maxDate = listRtIndexs[0].TradedDate;
                if (!maxDate.HasValue) return;
                List<Indexs> ieIndex = listRtIndexs.Where(p => p.TradedDate.Value.Date == maxDate.Value.Date && p.MarketId.Trim().ToUpper().Equals("HCM")).ToList();
                if(ieIndex.Any())
                {
                    var rtIndexs = ieIndex[0];
                    if(!rtIndexs.TradedDate.HasValue) return;
                    whereClauseRtIndex = "VNINDEX_DATE > GETDATE()- 10";
                    orderBy = "VNINDEX_DATE DESC";
                    total = 0;
                    var vnindexService = new VnindexService();
                    var listVnIndex = vnindexService.GetPaged(whereClauseRtIndex, orderBy, 0, int.MaxValue, out total);
                    if(total>0)
                    {
                        DateTime? dateVnIndex = listVnIndex[0].VnindexDate;
                        var listItemVnIndex = listVnIndex.Where(p => p.ThitruongId.Trim().ToUpper().Equals(rtIndexs.MarketId.Trim().ToUpper()) && p.VnindexDate == dateVnIndex).ToList();
                        

                        if (listItemVnIndex.Count == 0 || !dateVnIndex.HasValue || dateVnIndex.Value.Date < rtIndexs.TradedDate.Value.Date)
                        {
                            InsertVnIndexs( rtIndexs);
                        }
                    }
                    else
                    {
                        InsertVnIndexs(rtIndexs);
                    }

                }
            }
            catch (Exception ex)
            {
                Log(ex.ToString());
            }
            
        }

        private static void InsertVnIndexs( Indexs rtIndexs)
        {
            VnindexService service = new VnindexService();
            var vnindex = new Vnindex
                              {
                                  VnindexDate = rtIndexs.TradedDate,
                                  Open = rtIndexs.Open,
                                  Close = rtIndexs.Close,
                                  Change = rtIndexs.Change,
                                  Unchange = rtIndexs.Unchange,
                                  High = rtIndexs.High,
                                  Low = rtIndexs.Low,
                                  Up = rtIndexs.Up,
                                  Down = rtIndexs.Down,
                                  Average = rtIndexs.Average,
                                  Val = rtIndexs.Val,
                                  Vol = rtIndexs.Vol,
                                  Attribute1 = rtIndexs.Attribute1,
                                  Attribute3 = rtIndexs.Attribute3,
                                  Totaltrade = rtIndexs.Totaltrade,
                                  ThitruongId = rtIndexs.MarketId,
                                  Status = rtIndexs.Status,
                                  Trans = rtIndexs.Trans
                              };
            if(service.Insert(vnindex))
            {
                Log("Update Success");
            }
            else
            {
                Log("Update Fail");
            }
        }
        public static void Log(string sErrMsg)
        {
            DirectoryInfo dir = new DirectoryInfo(currentDir + @"\Log\");
            if (!dir.Exists)
            {
                dir.Create();
            }
            bool isNotOpened = true;

            string sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " +
                                DateTime.Now.ToLongTimeString().ToString() + " ==> ";

            while (isNotOpened)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(logFile, true);
                    sw.WriteLine(sLogFormat + sErrMsg);
                    sw.Flush();
                    sw.Close();

                    isNotOpened = false;
                }
                catch
                {
                    System.Threading.Thread.Sleep(5);
                }
            }
        }
    }
}
