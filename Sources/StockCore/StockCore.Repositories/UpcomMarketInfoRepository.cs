using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StockCore.Models;

namespace StockCore.Repositories
{
    public class UpcomMarketInfoRepository
    {
        private readonly DataStockCoreEntities _dataStockCore = new DataStockCoreEntities();
        public List<Common.UpComMarketInfoData> GetAll()
        {
            var result = new List<Common.UpComMarketInfoData>();
            var listUpComMarketInfo = (from x in _dataStockCore.UpComMarketInfoes select x).ToList();
            foreach (var item in listUpComMarketInfo)
            {
                var UpComMarketInfo = new Common.UpComMarketInfoData()
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
                result.Add(UpComMarketInfo);
            }
            return result;
        }
    }
}
