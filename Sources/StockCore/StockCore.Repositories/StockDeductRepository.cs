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
    }
}
