using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockCore.Repositories
{
    public class StockBalanceRespository
    {
        private readonly Models.DataStockCoreEntities _entities = new Models.DataStockCoreEntities();
        public Models.StockBalance GetByAccountNoAndSymbol(string accountNo, string stockSymbol)
        {
            return _entities.StockBalances.Where(x=>x.SubCustAccountID==accountNo && x.StockSymbol==stockSymbol).SingleOrDefault();
        }
        public List<Common.StockBalanceData> GetByAccountNo(string accountNo)
        {
            var listTemp = _entities.StockBalances.Where(x=>x.SubCustAccountID==accountNo).ToList();            
            var result = listTemp.ConvertAll(x=>new Common.StockBalanceData(){SubCustAccountID = x.SubCustAccountID,Available = x.Available,
            StockSymbol=x.StockSymbol,Total = x.Total,WTR_T1 = x.WTR_T1, WTR_T2 = x.WTR_T2, WTS_T1 = x.WTS_T1,WTS_T2 = x.WTS_T2}).ToList();
            return result;
        }
        public bool Update(Models.StockBalance stockBalance)
        {
            try
            {
                _entities.StockBalances.Attach(stockBalance);
                var entry = _entities.Entry(stockBalance);
                entry.State = System.Data.EntityState.Modified;
                _entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Insert(Models.StockBalance stockBalance)
        {
            try
            {
                _entities.StockBalances.Add(stockBalance);
                _entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
