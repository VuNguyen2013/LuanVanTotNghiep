using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockCore.Repositories
{
    public class MemberStockCompanyRepository
    {
        private readonly Models.DataStockCoreEntities _entities = new Models.DataStockCoreEntities();
        public List<Models.MemberStockCompany> GetAll()
        {
            var result = _entities.MemberStockCompanies.Select(x => x).OrderBy(x=>x.ShortNameVi).ToList();
            return result;
        }
    }
}
