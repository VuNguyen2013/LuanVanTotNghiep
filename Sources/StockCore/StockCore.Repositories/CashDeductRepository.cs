using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockCore.Repositories
{
    public class CashDeductRepository
    {
        private readonly Models.DataStockCoreEntities _entities = new Models.DataStockCoreEntities();
        public bool Insert(Models.CashTempDeduction cashTempDeduct)
        {
            try
            {
                _entities.CashTempDeductions.Add(cashTempDeduct);
                _entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Models.CashTempDeduction GetByOrderId(long orderId)
        {
            return _entities.CashTempDeductions.Where(x=>x.OrderId==orderId).SingleOrDefault();
        }
        public bool Update(Models.CashTempDeduction cashDeduct)
        {
            try
            {
                _entities.CashTempDeductions.Attach(cashDeduct);
                var entry = _entities.Entry(cashDeduct);
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
