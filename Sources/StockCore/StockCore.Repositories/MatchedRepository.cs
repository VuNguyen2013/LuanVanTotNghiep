using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockCore.Repositories
{
    public class MatchedRepository
    {
        private readonly Models.DataStockCoreEntities _entities = new Models.DataStockCoreEntities();
        public bool Insert(Models.Matched matched)
        {
            try
            {
                _entities.Matcheds.Add(matched);
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
