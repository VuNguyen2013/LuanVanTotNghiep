using Db4objects.Db4o.Query;
using RTDataServices.Entities;

namespace RTWebService.Utils
{
    public class TransactionDescComparator : IQueryComparator
    {
        public int Compare(object first, object second)
        {
            var firstObject = (TransactionInfo) first;
            var secondObject = (TransactionInfo) second;
            return secondObject.Time - firstObject.Time;
        }
    }
}
