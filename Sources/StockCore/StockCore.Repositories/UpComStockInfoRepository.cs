using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StockCore.Models;

namespace StockCore.Repositories
{
    public class UpComStockInfoRepository
    {
        private readonly DataStockCoreEntities _dataStockCore = new DataStockCoreEntities();
        public List<Common.UpComStockInfoData> GetAll()
        {
            List<Common.UpComStockInfoData> result = new List<Common.UpComStockInfoData>();
            var listData = (from x in _dataStockCore.UpComStockInfoes select x).ToList();
            foreach (var item in listData)
            {
                var upcomStockInfo = new Common.UpComStockInfoData()
                {
                    id = item.id,
                    StockSymbol = item.CompanyInfo.Code,
                    TradeDate = item.TradeDate,
                    Ceiling = item.Ceiling,
                    Floor = item.Floor,
                    AvrPrice = item.AvrPrice,
                    Last = item.Last,
                    LastVol = item.LastVol,
                    LastVal = item.LastVal,
                    Highest = item.Highest,
                    Lowest = item.Lowest,
                    Totalshares = item.Totalshares,
                    TotalValue = item.TotalValue,
                    Best1Bid = item.Best1Bid,
                    Best1BidVolume = item.Best1BidVolume,
                    Best2Bid = item.Best2Bid,
                    Best2BidVolume = item.Best2BidVolume,
                    Best3Bid = item.Best3Bid,
                    Best3BidVolume = item.Best3BidVolume,
                    Best1Offer = item.Best1Offer,
                    Best1OfferVolume = item.Best1OfferVolume,
                    Best2Offer = item.Best2Offer,
                    Best2OfferVolume = item.Best2OfferVolume,
                    Best3Offer = item.Best3Offer,
                    Best3OfferVolume = item.Best3OfferVolume,
                    SELL_FOREIGN_QTTY = item.SELL_FOREIGN_QTTY,
                    BUY_FOREIGN_QTTY = item.BUY_FOREIGN_QTTY
                };
                result.Add(upcomStockInfo);
            }
            return result;
        }
        public UpComStockInfo GetByStockSymbol(string code)
        {
            return (from x in _dataStockCore.UpComStockInfoes where x.StockSymbol.Replace(" ", string.Empty) == code.Replace(" ", string.Empty) select x).SingleOrDefault();
        }
        public bool Update(Models.UpComStockInfo stockInfo)
        {
            try
            {
                _dataStockCore.UpComStockInfoes.Attach(stockInfo);
                var entry = _dataStockCore.Entry(stockInfo);
                entry.State = System.Data.EntityState.Modified;
                _dataStockCore.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
