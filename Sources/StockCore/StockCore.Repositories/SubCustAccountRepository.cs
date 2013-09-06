using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockCore.Repositories
{
    public class SubCustAccountRepository
    {
        private readonly Models.DataStockCoreEntities _entitites = new Models.DataStockCoreEntities();
        public Models.SubCustAccount GetById(string accountId)
        {
            return _entitites.SubCustAccounts.Where(x=>x.SubCustAccountID==accountId).SingleOrDefault();
        }
        public bool Update(Models.SubCustAccount account)
        {
            try
            {
                _entitites.SubCustAccounts.Attach(account);
                var entry = _entitites.Entry(account);
                entry.State = System.Data.EntityState.Modified;
                _entitites.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
