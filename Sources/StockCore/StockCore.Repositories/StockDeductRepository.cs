using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockCore.Repositories
{
    public class StockDeductRepository
    {
        private readonly Models.DataStockCoreEntities _entities = new Models.DataStockCoreEntities();
        public bool Insert(Models.StockTempDeduction stockTempDeduct)
        {
            try
            {
                _entities.StockTempDeductions.Add(stockTempDeduct);
                _entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Models.StockTempDeduction GetByOrderId(long orderId)
        {
            return _entities.StockTempDeductions.Where(x=>x.OrderId==orderId).SingleOrDefault();
        }
        public bool Update(Models.StockTempDeduction stockDeduct)
        {
            try
            {
                _entities.StockTempDeductions.Attach(stockDeduct);
                var entry = _entities.Entry(stockDeduct);
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
