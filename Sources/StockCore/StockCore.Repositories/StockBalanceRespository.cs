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
    }
}
