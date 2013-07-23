using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.IO;
using Db4objects.Db4o.Query;
using RTDataServices.Entities;
using RTWebService.Utils;

namespace RTWebService
{
    public class Db4OManager
    {
        private static readonly ILog Logging = LogManager.GetLogger("Db4oManager");
        private static IObjectContainer _db;

        public static bool CreateDatabase()
        {
            IEmbeddedConfiguration config = Db4oEmbedded.NewConfiguration();
            config.File.Storage = new PagingMemoryStorage();
            
            _db = Db4oEmbedded.OpenFile(config, "inmemory");
            _db.Ext().Configure().WeakReferences(false);
            _db.Ext().Configure().ActivationDepth(1);
            _db.Ext().Configure().ObjectClass(typeof(HOSETransactionInfo)).ObjectField("Id").Indexed(true);
            _db.Ext().Configure().ObjectClass(typeof(HNXTransactionInfo)).ObjectField("Id").Indexed(true);
            _db.Ext().Configure().ObjectClass(typeof(UPCOMTransactionInfo)).ObjectField("Id").Indexed(true);
            if (_db == null)
                return false;
            return true;
        }

        public static void Insert<T>(T item)
        {
            _db.Store(item);
            _db.Commit();
        }

        public static void Update(HOSETransactionInfo transactionInfo)
        {
            IQuery query = _db.Query();

            IQuery queryTime = query.Descend("_time");

            query.Constrain(typeof(HOSETransactionInfo));

            query.Descend("_stockSymbol").Constrain(transactionInfo.StockSymbol).Equal().And(
                queryTime.Constrain(transactionInfo.Time).Equal());

            IObjectSet result = query.Execute();

            if (result.Count == 0)
            {
                Insert(transactionInfo);
            }
        }

        public static void Update(HNXTransactionInfo transactionInfo)
        {
            IQuery query = _db.Query();

            IQuery queryTime = query.Descend("_time");

            query.Constrain(typeof(HNXTransactionInfo));

            query.Descend("_stockSymbol").Constrain(transactionInfo.StockSymbol).Equal().And(
                queryTime.Constrain(transactionInfo.Time).Equal());

            IObjectSet result = query.Execute();

            if (result.Count == 0)
            {
                Insert(transactionInfo);
            }
        }

        public static List<MainMatchedPricesInfo> GetHoseMainMatchedPrices(string stockSymbol)
        {
            List<HOSETransactionInfo> listTransaction = GetHoseTransactionInfoListBySymbol(stockSymbol);

            var listResult = (from HOSETransactionInfo transactionInfo in listTransaction
                                group transactionInfo by transactionInfo.Price into g 
                        select new { Price = g.Key, Volume = g.Sum(transactionInfo => transactionInfo.Vol) }).ToList();

            return listResult.Select(info => new MainMatchedPricesInfo {Price = info.Price, Volume = info.Volume}).ToList();
        }

        public static List<MainMatchedPricesInfo> GetHnxMainMatchedPrices(string stockSymbol)
        {
            List<HNXTransactionInfo> listTransaction = GetHnxTransactionInfoListBySymbol(stockSymbol);

            var listResult = (from HNXTransactionInfo transactionInfo in listTransaction
                              group transactionInfo by transactionInfo.Price into g
                              select new { Price = g.Key, Volume = g.Sum(transactionInfo => transactionInfo.Vol) }).ToList();

            return listResult.Select(info => new MainMatchedPricesInfo {Price = info.Price, Volume = info.Volume}).ToList();
        }

        public static List<MainMatchedPricesInfo> GetUpcomMainMatchedPrices(string stockSymbol)
        {
            List<UPCOMTransactionInfo> listTransaction = GetUpcomTransactionInfoListBySymbol(stockSymbol);

            var listResult = (from UPCOMTransactionInfo transactionInfo in listTransaction
                              group transactionInfo by transactionInfo.Price into g
                              select new { Price = g.Key, Volume = g.Where(transactionInfo => transactionInfo.StockSymbol == stockSymbol).Sum(transactionInfo => transactionInfo.Vol) }).ToList();

            return listResult.Select(info => new MainMatchedPricesInfo {Price = info.Price, Volume = info.Volume}).ToList();
        }

        public static void Update(UPCOMTransactionInfo transactionInfo)
        {
            IQuery query = _db.Query();

            IQuery queryTime = query.Descend("_time");

            query.Constrain(typeof(UPCOMTransactionInfo));

            query.Descend("_stockSymbol").Constrain(transactionInfo.StockSymbol).Equal().And(
                queryTime.Constrain(transactionInfo.Time).Equal());

            IObjectSet result = query.Execute();

            if (result.Count == 0)
            {
                Insert(transactionInfo);
            }
        }

        public static List<HOSETransactionInfo> ListResult1(IObjectSet result)
        {
            return result.Cast<HOSETransactionInfo>().ToList();
        }

        public static List<HOSETransactionInfo> GetHoseTransactionInfoListBySymbol(string stockSymbol)
        {
            IQuery query = _db.Query();
            query.Constrain(typeof(HOSETransactionInfo));

            query.Descend("_stockSymbol").Constrain(stockSymbol).Equal();

            IObjectSet result = query.Execute();

            return ListResult1(result);
        }

        public static List<HOSETransactionInfo> GetHoseTransactionInfoListBySymbol(string stockSymbol, int id)
        {
            IQuery query = _db.Query();
            query.Constrain(typeof(HOSETransactionInfo));
            
            IConstraint constraint = query.Descend("_id").Constrain(id).Greater();
            query.Descend("_stockSymbol").Constrain(stockSymbol).Equal().And(constraint);
            query.SortBy(new TransactionDescComparator());

            IObjectSet result = query.Execute();

            return ListResult1(result);
        }

        public static List<TickerInfo> GetTickerListBySymbolAndId(string stockSymbol, int id, object type)
        {
            IQuery query = _db.Query();
            query.Constrain(type);

            IConstraint constraint = query.Descend("_id").Constrain(id).Greater();
            query.Descend("_stockSymbol").Constrain(stockSymbol).Equal().And(constraint);
            query.Descend("_id").OrderDescending();

            IObjectSet result = query.Execute();

            return GetTickerList(result, type);
        }

        public static List<TickerInfo> GetTickerListById(int id, object type)
        {
            IQuery query = _db.Query();
            query.Constrain(type);

            query.Descend("_id").Constrain(id).Greater();
            query.Descend("_id").OrderAscending();

            IObjectSet result = query.Execute();

            return GetTickerList(result, type);
        }

        public static List<TickerInfo> GetTickerList(IObjectSet dataList, object type)
        {
            var retList = new List<TickerInfo>();

            foreach (object item in dataList)
            {
                try
                {
                    if (type == typeof (HOSETransactionInfo))
                    {
                        var transactionInfo = (HOSETransactionInfo) item;
                        var tickerInfo = new TickerInfo
                                             {
                                                 Id = transactionInfo.Id,
                                                 StockSymbol = transactionInfo.StockSymbol,
                                                 Price = transactionInfo.Price,
                                                 Changed = transactionInfo.Changed,
                                                 Vol = transactionInfo.Vol,
                                                 Side = transactionInfo.Side,
                                             };

                        retList.Add(tickerInfo);
                    }
                    else if (type == typeof(HNXTransactionInfo))
                    {
                        var transactionInfo = (HNXTransactionInfo)item;
                        var tickerInfo = new TickerInfo
                                                {
                                                    Id = transactionInfo.Id,
                                                    StockSymbol = transactionInfo.StockSymbol,
                                                    Price = transactionInfo.Price,
                                                    Changed = transactionInfo.Changed,
                                                    Vol = transactionInfo.Vol,
                                                    Side = transactionInfo.Side,
                                                };

                        retList.Add(tickerInfo);
                    }
                    else if (type == typeof(UPCOMTransactionInfo))
                    {
                        var transactionInfo = (UPCOMTransactionInfo)item;
                        var tickerInfo = new TickerInfo
                                                {
                                                    Id = transactionInfo.Id,
                                                    StockSymbol = transactionInfo.StockSymbol,
                                                    Price = transactionInfo.Price,
                                                    Changed = transactionInfo.Changed,
                                                    Vol = transactionInfo.Vol,
                                                    Side = transactionInfo.Side,
                                                };

                        retList.Add(tickerInfo);
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }

            return retList;
        }

        public static List<HNXTransactionInfo> ListResult2(IObjectSet result)
        {
            return result.Cast<HNXTransactionInfo>().ToList();
        }

        public static List<HNXTransactionInfo> GetHnxTransactionInfoListBySymbol(string stockSymbol)
        {
            IQuery query = _db.Query();
            query.Constrain(typeof(HNXTransactionInfo));

            query.Descend("_stockSymbol").Constrain(stockSymbol).Equal();

            IObjectSet result = query.Execute();

            return ListResult2(result);
        }

        public static List<HNXTransactionInfo> GetHnxTransactionInfoListBySymbol(string stockSymbol, int id)
        {
            IQuery query = _db.Query();
            query.Constrain(typeof(HNXTransactionInfo));

            IConstraint constraint = query.Descend("_id").Constrain(id).Greater();
            query.Descend("_stockSymbol").Constrain(stockSymbol).Equal().And(constraint);
            query.SortBy(new TransactionDescComparator());

            IObjectSet result = query.Execute();

            return ListResult2(result);
        }

        public static List<UPCOMTransactionInfo> ListResult3(IObjectSet result)
        {
            return result.Cast<UPCOMTransactionInfo>().ToList();
        }

        public static List<UPCOMTransactionInfo> GetUpcomTransactionInfoListBySymbol(string stockSymbol)
        {
            IQuery query = _db.Query();
            query.Constrain(typeof(UPCOMTransactionInfo));

            query.Descend("_stockSymbol").Constrain(stockSymbol).Equal();

            IObjectSet result = query.Execute();

            return ListResult3(result);
        }

        public static List<UPCOMTransactionInfo> GetUpcomTransactionInfoListBySymbol(string stockSymbol, int id)
        {
            IQuery query = _db.Query();
            query.Constrain(typeof(UPCOMTransactionInfo));

            IConstraint constraint = query.Descend("_id").Constrain(id).Greater();
            query.Descend("_stockSymbol").Constrain(stockSymbol).Equal().And(constraint);
            query.SortBy(new TransactionDescComparator());

            IObjectSet result = query.Execute();

            return ListResult3(result);
        }

        //public static IList<TransactionInfo> GetTransactionInfoListBySymbol(string stockSymbol, int marketId)
        //{
        //    //Stopwatch stopwatch = new Stopwatch();
        //    //stopwatch.Start();
        //    IQuery query = _db.Query();

        //    switch (marketId)
        //    {
        //        case (int)RTDdataServices.Entities.MARKET_ID.HOSE:
        //            query.Constrain(typeof(HOSETransactionInfo));

        //            break;
        //        case (int)RTDdataServices.Entities.MARKET_ID.HASTC:
        //            query.Constrain(typeof(HNXTransactionInfo));

        //            break;
        //        case (int)RTDdataServices.Entities.MARKET_ID.UPCoM:
        //            query.Constrain(typeof(UPCOMTransactionInfo));

        //            break;
        //        default:
        //            break;
        //    }

        //    query.Descend("StockSymbol").Constrain(stockSymbol).Equal();
        //    IObjectSet result = query.Execute();

        //    return result;
        //}

        public static void ClearDatabase(short marketId)
        {
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            Logging.Info("Clear database.............................");
            if (marketId == ((short)RTDdataServices.Entities.MARKET_ID.HOSE))
            {
                IObjectSet result = _db.QueryByExample(typeof(HOSETransactionInfo));
                while (result.HasNext())
                {
                    _db.Delete(result.Next());
                }
            }
            else if (marketId == ((short)RTDdataServices.Entities.MARKET_ID.HASTC))
            {
                IObjectSet result1 = _db.QueryByExample(typeof(HNXTransactionInfo));
                while (result1.HasNext())
                {
                    _db.Delete(result1.Next());
                }
            } 
            else if (marketId == ((short)RTDdataServices.Entities.MARKET_ID.UPCoM))
            {
                IObjectSet result2 = _db.QueryByExample(typeof(UPCOMTransactionInfo));
                while (result2.HasNext())
                {
                    _db.Delete(result2.Next());
                }
            }
            
            _db.Commit();
        }
    }
}
