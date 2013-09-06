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
    }
}
