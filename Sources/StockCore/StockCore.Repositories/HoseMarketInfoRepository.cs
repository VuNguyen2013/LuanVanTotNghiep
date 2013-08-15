using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StockCore.Models;

namespace StockCore.Repositories
{
    public class HoseMarketInfoRepository
    {
        private readonly DataStockCoreEntities _dataStockCore = new DataStockCoreEntities();
        public List<Common.HoseMarketInfoData> GetAll()
        {
            var result = new List<Common.HoseMarketInfoData>();
            var value = (from x in _dataStockCore.HoseMarketInfoes select x).ToList();
            foreach (var item in value)
            {
                Common.HoseMarketInfoData hoseMarketInfoData = new Common.HoseMarketInfoData()
                    {
                          id = item.id,
                          TradeDate =item.TradeDate,
                          SetIndex = item.SetIndex,
                          TotalTrade = item.TotalTrade,
                          Totalshare = item.Totalshare,
                          TotalValue = item.TotalValue,
                          UpVolume = item.UpVolume,
                          DownVolume = item.DownVolume,
                          NoChangeVolume = item.NoChangeVolume,
                          Advances = item.Advances,
                          Declines = item.Declines,
                          Nochange = item.Nochange,
                          Status = item.Status
                    };
                
            }
            return result;
        }
    }
}
