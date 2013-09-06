using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StockCore.Models;

namespace StockCore.Repositories
{
    public class HoseStockInfoRepository
    {
        private readonly DataStockCoreEntities _dataStockCore = new DataStockCoreEntities();
        public List<Common.HoseStockInfoData> GetAll()
        {
            var result = new List<Common.HoseStockInfoData>();
            var listData = (from x in _dataStockCore.HoseStockInfoes select x).ToList();
            foreach (var item in listData)
            {
                var hoseStockInfo = new Common.HoseStockInfoData()
                {
                    id = item.id,
                    StockSymbol = item.CompanyInfo.Code,
                    TradeDate = item.TradeDate,
                    Ceiling = item.Ceiling,
                    Floor = item.Floor,
                    AvrPrice = item.AvrPrice,
                    Last = item.Last,
                    LastVal = item.LastVal,
                    LastVol = item.LastVol,
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
                    CurrentRoom = item.CurrentRoom,
                    StartRoom = item.StartRoom
                };
                result.Add(hoseStockInfo);
            }
            return result;
        }
        public HoseStockInfo GetByStockSymbol(string code)
        {
            return (from x in _dataStockCore.HoseStockInfoes where x.StockSymbol.Replace(" ", string.Empty) == code.Replace(" ", string.Empty) select x).SingleOrDefault();
        }
        public bool Update(Models.HoseStockInfo stockInfo)
        {
            try
            {
                _dataStockCore.HoseStockInfoes.Attach(stockInfo);
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
