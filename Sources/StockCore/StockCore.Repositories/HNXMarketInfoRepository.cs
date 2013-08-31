using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StockCore.Models;

namespace StockCore.Repositories
{
    public class HNXMarketInfoRepository
    {
        private readonly DataStockCoreEntities _dataStockCore = new DataStockCoreEntities();
        public List<Common.HNXMarketInfoData> GetAll()
        {
            var result = new List<Common.HNXMarketInfoData>();
            var listHnxMarketInfo = (from x in _dataStockCore.HNXMarketInfoes select x).ToList();
            foreach (var item in listHnxMarketInfo)
            {
                var hnxMarketInfo = new Common.HNXMarketInfoData()
                {
                    Advances = item.Advances,
                    Declines = item.Declines,
                    id = item.id,
                    Nochange = item.Nochange,
                    OpenIndex = item.OpenIndex,
                    SetIndex = item.SetIndex,
                    Status = item.Status,
                    Totalshare = item.Totalshare,
                    TotalTrade = item.TotalTrade,
                    TotalValue = item.TotalValue,
                    TradeDate = item.TradeDate
                };
                result.Add(hnxMarketInfo);
            }
            return result;
        }
    }
}
