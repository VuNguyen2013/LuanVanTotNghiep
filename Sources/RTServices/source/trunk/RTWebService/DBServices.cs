#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using ETradeCommon;
using ETradeCommon.Enums;
using RTDataServices.Entities;
using RTDdataServices.Entities;
using RTStockData.Entities;
using RTStockData.Services;
using CompanyInfo = RTDataServices.Entities.CompanyInfo;
using IndexInfo = RTDataServices.Entities.IndexInfo;
using HnxIndex30Service = RTStockData.Services.IndexInfoService;
using HnxIndex30HistoryService = RTStockData.Services.IndexInfoHistoryService;
#endregion

namespace RTWebService
{
    /// <summary>
    ///   Summary description for DBServices
    /// </summary>
    public class DbServices
    {
        public static Dictionary<string, StockInfo> ListStocks = new Dictionary<string, StockInfo>();

        public static Dictionary<int,Indexs> ListIndexs = new Dictionary<int, Indexs>();
        public static Dictionary<int, Dictionary<int, MarketInfo>> ListMarketInfos =
            new Dictionary<int, Dictionary<int, MarketInfo>>();

        public static Dictionary<string , Dictionary<int, IndexInfo>> ListHnxIndex30 = new Dictionary<string, Dictionary<int, IndexInfo>>();
        public static Dictionary<string, Dictionary<DateTime, IndexInfoHistory>> ListHnxIndex30History = new Dictionary<string, Dictionary<DateTime, IndexInfoHistory>>();

        public static Dictionary<string, Dictionary<string, BasketInfo>> ListBasketInfo = new Dictionary<string, Dictionary<string, BasketInfo>>();
        public static string ListStockHnx30Str = string.Empty;

        //public static Dictionary<string, CompanyInfo> ListCompanies = new Dictionary<string, CompanyInfo>();

        public static Dictionary<string, Dictionary<string, CompanyInfo>> ListLangCompanies =
            new Dictionary<string, Dictionary<string, CompanyInfo>>();

        public static Dictionary<int, IndexInfo> ListIndexVn30 = new Dictionary<int, IndexInfo>();
        public static Dictionary<long, IndexVn30History> ListIndexVn30History = new Dictionary<long, IndexVn30History>();


        private static readonly TotalmarketService TotalMarketServive = new TotalmarketService();
        private static readonly HastcMarketService HastcMarketService = new HastcMarketService();
        private static readonly UpcomMarketService UpcomMarketService = new UpcomMarketService();

        private static readonly SecurityRealtimeService SercurityRealtimeService = new SecurityRealtimeService();
        private static readonly HastcStocksService HastcStocksService = new HastcStocksService();
        private static readonly UpcomStocksService UpcomStocksService = new UpcomStocksService();

        private static readonly NearestWorkingDatesService NearestWorkingDatesService = new NearestWorkingDatesService();

        private static readonly HoseTransactionsService HoseTransactionsService = new HoseTransactionsService();
        private static readonly HastcTransactionsService HastcTransactionsService = new HastcTransactionsService();
        private static readonly UpcomTransactionsService UpcomTransactionsService = new UpcomTransactionsService();

        private static readonly CompanyInfoService CompanyInfoService = new CompanyInfoService();

        private static readonly IndexVn30Service IndexVn30Service = new IndexVn30Service();
        private static readonly IndexVn30HistoryService IndexVn30HistoryService = new IndexVn30HistoryService();

        private static readonly IndexInfoService HnxIndex30Service = new IndexInfoService();
        private static readonly IndexInfoHistoryService HnxIndex30HistoryService =new IndexInfoHistoryService();
        private static readonly BasketInfoService BasketInfoServices =new BasketInfoService();
        private static readonly IndexsService IndexsService = new IndexsService();

        private static double _priorIndex = -1;

        private static readonly int HnxTimeStartDay = Int32.Parse(Utils.Common.ReadFromWebConfig("HNXTimeStartDay"));
        private static readonly int HnxTimeOpen = Int32.Parse(Utils.Common.ReadFromWebConfig("HNXTimeOpen"));
        private static readonly int HnxTimeBeginHaft = Int32.Parse(Utils.Common.ReadFromWebConfig("HNXTimeBeginHaft"));
        private static readonly int HnxTimeEndHaft = Int32.Parse(Utils.Common.ReadFromWebConfig("HNXTimeEndHaft"));
        private static readonly int HnxTimeClose = Int32.Parse(Utils.Common.ReadFromWebConfig("HNXTimeClose"));


        private static readonly int UpcomTimeStartDay = Int32.Parse(Utils.Common.ReadFromWebConfig("UPCOMTimeStartDay"));
        private static readonly int UpcomTimeOpen = Int32.Parse(Utils.Common.ReadFromWebConfig("UPCOMTimeOpen"));
        private static readonly int UpcomTimeBeginHalf = Int32.Parse(Utils.Common.ReadFromWebConfig("UPCOMTimeBeginHalf"));
        private static readonly int UpcomTimeEndHalf = Int32.Parse(Utils.Common.ReadFromWebConfig("UPCOMTimeEndHalf"));
        private static readonly int UpcomTimeClose = Int32.Parse(Utils.Common.ReadFromWebConfig("UPCOMTimeClose"));
        private static long _hoseTransactionsId;
        private static long _hastcTransactionsId;
        private static long _upcomTransactionsId;


        private static readonly List<bool> IsResetMarkets = new List<bool> {false, false, false, false};

        private static char _tradingStatusHnx = (char) CommonEnums.MARKET_STATUS.INIT_APP;

        private static DateTime _sessionTimeHnx = DateTime.Now;

        private static char _tradingStatusUpcom = (char) CommonEnums.MARKET_STATUS.INIT_APP;

        private static DateTime _sessionTimeUpcom = DateTime.Now;

        private static bool _isFirstTimeUpdateCompanyInfo = true;

        private static bool _isResetHnx30 = false;

        private DbServices()
        {
        }

        public static void CreateInMemDb()
        {
            _hoseTransactionsId = 0;
            _hastcTransactionsId = 0;
            _upcomTransactionsId = 0;
            Db4OManager.CreateDatabase();
        }

        public static void ResetRtData()
        {
            try
            {
                ListStocks.Clear();
                //ListCompanies.Clear();
                ListLangCompanies.Clear();
                ListMarketInfos.Clear();
                ListIndexVn30.Clear();
                ListIndexVn30History.Clear();
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }
        }

        public static IndexInfo GetIndexVn30()
        {
            if (ListIndexVn30.Count > 0)
            {
                var listInfo = new List<IndexInfo>(ListIndexVn30.Values);
                return listInfo[listInfo.Count - 1];
            }
            return null;
        }

        public static IndexInfo GetFirstIndexVn30()
        {
            if (ListIndexVn30.Count > 0)
            {
                var listInfo = new List<IndexInfo>(ListIndexVn30.Values);

                return listInfo[0];
            }
            return null;
        }

        public static List<IndexInfo> GetAllIndexVn30()
        {
            if (ListIndexVn30.Count > 0)
            {
                var listInfo = new List<IndexInfo>(ListIndexVn30.Values);
                return listInfo;
            }
            return null;
        }

        public static IndexVn30History GetIndexVn30History()
        {
            if (ListIndexVn30History.Count > 0)
            {
                var listInfo = new List<IndexVn30History>(ListIndexVn30History.Values);
                return listInfo[ListIndexVn30History.Count - 1];
            }
            return null;
        }

        public static MarketInfo GetCurrentMarketInfo(int marketId, bool forToday)
        {
            Dictionary<int, MarketInfo> listMarketInfos;
            List<MarketInfo> listInfos = null;
            DateTime currentDate = DateTime.Now.Date;

            try
            {
                if (!ListMarketInfos.ContainsKey(marketId))
                {
                    return null;
                }

                listMarketInfos = ListMarketInfos[marketId];

                if (forToday)
                {
                    listInfos =
                        new List<MarketInfo>(listMarketInfos.Values).FindAll(
                            info => (info.TradeDate.Date == currentDate));
                }
                else
                {
                    listInfos = new List<MarketInfo>(listMarketInfos.Values);
                }

                if (listInfos.Count == 0)
                {
                    return null;
                }
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return ((listInfos == null) ? null : listInfos[listInfos.Count - 1]);
        }

        private static MarketInfo GetFirstMarketInfo(int marketId, bool forToday)
        {
            Dictionary<int, MarketInfo> listMarketInfos;
            List<MarketInfo> listInfos = null;
            DateTime currentDate = DateTime.Now.Date;

            try
            {
                if (!ListMarketInfos.ContainsKey(marketId))
                {
                    return null;
                }

                listMarketInfos = ListMarketInfos[marketId];

                if (forToday)
                {
                    listInfos =
                        new List<MarketInfo>(listMarketInfos.Values).FindAll(
                            info => (info.TradeDate.Date == currentDate));
                }
                else
                {
                    listInfos = new List<MarketInfo>(listMarketInfos.Values);
                }
                if (listInfos.Count == 0)
                {
                    return null;
                }
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return ((listInfos == null) ? null : listInfos[0]);
        }

        /// <summary>
        ///   Get Stock information of symbol
        /// </summary>
        /// <param name = "symbol"></param>
        public static StockInfo GetStock(string symbol)
        {
            StockInfo info = null;

            try
            {
                if (ListStocks.ContainsKey(symbol))
                {
                    info = ListStocks[symbol];
                }
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return info;
        }

        /// <summary>
        ///   Get Stock information of symbol
        /// </summary>
        /// <param name = "symbol"></param>
        /// <param name = "languageId"></param>
        public static StockInfo GetStock(string symbol, string languageId)
        {
            StockInfo info = null;

            try
            {
                if (ListStocks.ContainsKey(symbol))
                {
                    info = ListStocks[symbol];

                    if (ListLangCompanies.ContainsKey(languageId))
                    {
                        Dictionary<string, CompanyInfo> companyInfoEntry = ListLangCompanies[languageId];

                        if (companyInfoEntry.ContainsKey(symbol))
                        {
                            CompanyInfo companyInfo = companyInfoEntry[symbol];

                            info.Name = companyInfo.FullName;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return info;
        }

        public static void ResetAllMarketData()
        {
            ResetData((int) CommonEnums.MARKET_ID.HOSE);
            ResetData((int) CommonEnums.MARKET_ID.HNX);
            ResetData((int) CommonEnums.MARKET_ID.UPCoM);
            ResetTotalHnx30();
        }

        /// <summary>
        ///   Reset the Data info of yesterday for each market
        /// </summary>
        /// <param name = "marketId"></param>
        private static void ResetData(short marketId)
        {
            //Reset all old data
            MarketInfo currentInfo = GetCurrentMarketInfo(marketId, true);
            char marketStatus = MarketStatus(currentInfo, marketId);

            if (!IsResetMarkets[marketId] && marketStatus == (char) CommonEnums.MARKET_STATUS.READY)
            {
                ResetMarketData(marketId);
                RemoveStocks(marketId);
                RemoveTransactionInfos(marketId);
                ResetIndexVn30();
                ResetIndexVn30History();
                if (marketId == (int) CommonEnums.MARKET_ID.HOSE)
                {
                    _priorIndex = -1;
                }

                IsResetMarkets[marketId] = true;
            }
        }

        public static void ResetTotalHnx30()
        {
            try
            {
                if (!_isResetHnx30 && ListHnxIndex30.Count > 0 && ListHnxIndex30.ContainsKey("HNX30"))
                {
                    Dictionary<int, IndexInfo> indexInfos = ListHnxIndex30["HNX30"];
                    if (indexInfos.Count > 0)
                    {
                        DateTime newDate = GetMaxDateHnx30();
                        DateTime oldDate = indexInfos.Min(p => p.Value.TradeDate);
                        if (oldDate.Date != newDate.Date)
                        {
                            ResetHnx30();
                            ResetHnx30History();
                            ResetBasketInfo();
                            _isResetHnx30 = true;
                            LogHandler.Log(
                                string.Format("Reseted Hnx30, Old Date: {0}, New Date: {1}", oldDate, newDate),
                                "ResetTotalHnx30", TraceEventType.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.Log(ex.ToString(), "ResetTotalHnx30", TraceEventType.Error);
                LogHandler.Log(ex.StackTrace, "ResetTotalHnx30", TraceEventType.Error);

            }
        }

        /// <summary>
        ///   Reset the market info of yesterday for each market
        /// </summary>
        /// <param name = "marketId"></param>
        private static void ResetMarketData(short marketId)
        {
            if (!ListMarketInfos.ContainsKey(marketId))
            {
                return;
            }

            Dictionary<int, MarketInfo> listMarketInfos = ListMarketInfos[marketId];

            List<MarketInfo> listInfos =
                new List<MarketInfo>(listMarketInfos.Values).FindAll(info => (info.TradeDate.Date == DateTime.Now.Date));

            ListMarketInfos.Remove(marketId);

            foreach (MarketInfo marketInfo in listInfos)
            {
                UpdateMarketInfo(marketInfo);
            }
        }

        private static void ResetIndexVn30()
        {
            List<IndexInfo> listIndexDateNew =
                new List<IndexInfo>(ListIndexVn30.Values).FindAll(indexs => (indexs.TradeDate.Date == DateTime.Now.Date));
            ListIndexVn30.Clear();
            foreach (IndexInfo vn30 in listIndexDateNew)
            {
                ListIndexVn30.Add(vn30.Time, vn30);
            }
        }
        public static void ResetHnx30()
        {
            var listIndex =new List<IndexInfo>();
            foreach (KeyValuePair<string, Dictionary<int, IndexInfo>> valuePair in ListHnxIndex30)
            {
                List<IndexInfo> item =
                    new List<IndexInfo>(ListHnxIndex30[valuePair.Key].Values).FindAll(
                        index => index.TradeDate.Date == DateTime.Now.Date);
                if(item.Count>0)
                listIndex.AddRange(item);
            }
            ListHnxIndex30.Clear();
            foreach (IndexInfo indexInfo in listIndex)
            {
                if (ListHnxIndex30.ContainsKey(indexInfo.IndexCode))
                {
                    Dictionary<int, IndexInfo> listIndexItem = ListHnxIndex30[indexInfo.IndexCode];
                    if (listIndexItem.ContainsKey(indexInfo.Time))
                        listIndexItem[indexInfo.Time] = indexInfo;
                    else
                        listIndexItem.Add(indexInfo.Time, indexInfo);
                    ListHnxIndex30[indexInfo.IndexCode] = listIndexItem;
                }
                else
                {
                    var listIndexItem = new Dictionary<int, IndexInfo> { { indexInfo.Time, indexInfo } };
                    ListHnxIndex30.Add(indexInfo.IndexCode, listIndexItem);
                }
            }
        }

        private static void ResetIndexVn30History()
        {
            List<IndexVn30History> listIndexDateNew =
                new List<IndexVn30History>(ListIndexVn30History.Values).FindAll(
                    indexs => indexs.TradeDate !=null && indexs.TradeDate.Value.Date == DateTime.Now.Date);
            ListIndexVn30History.Clear();
            foreach (IndexVn30History vn30History in listIndexDateNew)
            {
                if (vn30History.Time != null) ListIndexVn30History.Add(vn30History.Time.Value, vn30History);
            }
        }

        private static void ResetHnx30History()
        {
            var listIndex = new List<IndexInfoHistory>();
            foreach (KeyValuePair<string, Dictionary<DateTime, IndexInfoHistory>> valuePair in ListHnxIndex30History)
            {
                List<IndexInfoHistory> item =
                    new List<IndexInfoHistory>(ListHnxIndex30History[valuePair.Key].Values).FindAll(
                        index => index.TradeDate != null && index.TradeDate.Value.Date == DateTime.Now.Date);
                listIndex.AddRange(item);
            }
            ListHnxIndex30History.Clear();
            foreach (var indexInfo in listIndex)
            {
                if (ListHnxIndex30History.ContainsKey(indexInfo.IndexCode))
                {
                    if (indexInfo.TradeDate != null)
                    {
                        Dictionary<DateTime, IndexInfoHistory> listIndexItem =
                            ListHnxIndex30History[indexInfo.IndexCode];
                        if (listIndexItem.ContainsKey(indexInfo.TradeDate.Value.Date))
                            listIndexItem[indexInfo.TradeDate.Value.Date] = indexInfo;
                        else
                            listIndexItem.Add(indexInfo.TradeDate.Value.Date, indexInfo);
                        ListHnxIndex30History[indexInfo.IndexCode] = listIndexItem;
                    }
                }
                else
                {
                    if (indexInfo.TradeDate != null)
                    {
                        var listIndexItem = new Dictionary<DateTime, IndexInfoHistory>
                                                {{indexInfo.TradeDate.Value.Date, indexInfo}};
                        ListHnxIndex30History.Add(indexInfo.IndexCode, listIndexItem);
                    }
                }
            }
        }

        public static void ResetBasketInfo()
        {
            var listBasketInfo = new List<BasketInfo>();
            foreach (KeyValuePair<string, Dictionary<string, BasketInfo>> keyValuePair in ListBasketInfo)
            {
                List<BasketInfo> listItem =
                    new List<BasketInfo>(ListBasketInfo[keyValuePair.Key].Values).FindAll(
                        basket => basket.TradeDate!=null && basket.TradeDate.Value.Date == DateTime.Now.Date);
                listBasketInfo.AddRange(listItem);
            }
            ListBasketInfo.Clear();
            foreach (BasketInfo basketInfo in listBasketInfo)
            {
                if (ListBasketInfo.ContainsKey(basketInfo.IndexCode.Trim()))
                {
                    Dictionary<string, BasketInfo> listItem = ListBasketInfo[basketInfo.IndexCode.Trim()];
                    if (listItem.ContainsKey(basketInfo.StockCode))
                    {
                        listItem[basketInfo.StockCode] = basketInfo;
                    }
                    else
                        listItem.Add(basketInfo.StockCode, basketInfo);
                }
                else
                {
                    var listItem =
                        new Dictionary<string, BasketInfo> { { basketInfo.StockCode, basketInfo } };
                    ListBasketInfo.Add(basketInfo.IndexCode.Trim(), listItem);
                }
                if (basketInfo.IndexCode.Trim().ToUpper().Equals("HNX30") && ListStockHnx30Str.IndexOf(basketInfo.StockCode.Trim()) < 0)
                {
                    ListStockHnx30Str += (basketInfo.StockCode + ",");
                }
            }
        }


        private static void RemoveTransactionInfos(short marketId)
        {
            Db4OManager.ClearDatabase(marketId);
            switch (marketId)
            {
                case (short)MARKET_ID.HOSE:
                    _hoseTransactionsId = 0;
                    break;
                case (short)MARKET_ID.HASTC:
                    _hastcTransactionsId = 0;
                    break;
                case (short)MARKET_ID.UPCoM:
                    _upcomTransactionsId = 0;
                    break;
            }
        }

        /// <summary>
        ///   Remove all stock with market is marketID
        /// </summary>
        /// <param name = "marketId"></param>
        private static void RemoveStocks(short marketId)
        {
            List<StockInfo> listStocks = Select_All_StockInfo(marketId);

            foreach (StockInfo item in listStocks)
            {
                ListStocks.Remove(item.StockSymbol);
            }
        }

        private static void UpdateStockInfoForHose()
        {
            TList<SecurityRealtime> listSecurityRealtimes = SercurityRealtimeService.GetAll();

            foreach (SecurityRealtime securityInfo in listSecurityRealtimes)
            {
                Thread.Sleep(1);
                try
                {
                    //RemoveOldStockData((DateTime)securityInfo.TradeDate, ((short) RTDdataServices.Entities.MARKET_ID.HOSE));

                    // only update for stocks, no bonds.
                    if (securityInfo.StockType[0] != (char) CommonEnums.STOCK_TYPE.STOCK &&
                        securityInfo.StockType[0] != (char) CommonEnums.STOCK_TYPE.FUND)
                    {
                        continue;
                    }

                    var stockInfo = new StockInfo
                                        {
                                            MarketID = (short) MARKET_ID.HOSE,
                                            TradeDate = (securityInfo.TradeDate ?? DateTime.Now),
                                            StockSymbol = securityInfo.StockSymbol.Trim(),
                                            Name = securityInfo.SecurityName ?? string.Empty,
                                            Floor = (securityInfo.Floor ?? 0)/Utils.Common.PRICE_UNIT,
                                            Ceiling = (securityInfo.Ceiling ?? 0)/Utils.Common.PRICE_UNIT,
                                            AvrPrice = (securityInfo.AvrPrice ?? 0)/Utils.Common.PRICE_UNIT,
                                            OpenPrice = (securityInfo.OpenPrice ?? 0)/Utils.Common.PRICE_UNIT,
                                            RefPrice = (securityInfo.PriorClosePrice ?? 0)/Utils.Common.PRICE_UNIT,
                                            Last = (securityInfo.Last ?? 0)/Utils.Common.PRICE_UNIT,
                                            LastVol = (int) (securityInfo.LastVol ?? 0),
                                            LastVal = securityInfo.LastVal ?? 0,
                                            Highest = (securityInfo.Highest ?? 0)/Utils.Common.PRICE_UNIT,
                                            Lowest = (securityInfo.Lowest ?? 0)/Utils.Common.PRICE_UNIT,
                                            TotalShare = (securityInfo.Totalshares ?? 0)*Utils.Common.VOLUME_UNIT,
                                            TotalForeignRoom = securityInfo.StartRoom ?? 0,
                                            AvailableForeignRoom = securityInfo.CurrentRoom ?? 0
                                        };

                    //stockInfo.Status              = stockInfo.Status;
                    stockInfo.FRBoughtVol = stockInfo.TotalForeignRoom - stockInfo.AvailableForeignRoom;
                    stockInfo.FRSoldVol = 0; // Can not calculate this value for HOSE.

                    stockInfo.Best1Bid = (securityInfo.Best1Bid ?? 0)/Utils.Common.PRICE_UNIT;
                    stockInfo.Best1BidVolume = (int) (securityInfo.Best1BidVolume ?? 0)*Utils.Common.VOLUME_UNIT;
                    stockInfo.Best2Bid = (securityInfo.Best2Bid ?? 0)/Utils.Common.PRICE_UNIT;
                    stockInfo.Best2BidVolume = (int) (securityInfo.Best2BidVolume ?? 0)*Utils.Common.VOLUME_UNIT;
                    stockInfo.Best3Bid = (securityInfo.Best3Bid ?? 0)/Utils.Common.PRICE_UNIT;
                    stockInfo.Best3BidVolume = (int) (securityInfo.Best3BidVolume ?? 0)*Utils.Common.VOLUME_UNIT;
                    stockInfo.Best1Offer = (securityInfo.Best1Offer ?? 0)/Utils.Common.PRICE_UNIT;
                    stockInfo.Best1OfferVolume = (int) (securityInfo.Best1OfferVolume ?? 0)*Utils.Common.VOLUME_UNIT;
                    stockInfo.Best2Offer = (securityInfo.Best2Offer ?? 0)/Utils.Common.PRICE_UNIT;
                    stockInfo.Best2OfferVolume = (int) (securityInfo.Best2OfferVolume ?? 0)*Utils.Common.VOLUME_UNIT;
                    stockInfo.Best3Offer = (securityInfo.Best3Offer ?? 0)/Utils.Common.PRICE_UNIT;
                    stockInfo.Best3OfferVolume = (int) (securityInfo.Best3OfferVolume ?? 0)*Utils.Common.VOLUME_UNIT;
                    stockInfo.Sequence = securityInfo.Sequence ?? 0;
                    stockInfo.IsVn30 = securityInfo.IsVn30.HasValue && securityInfo.IsVn30.Value;

                    stockInfo.CanTrade = ((securityInfo.Suspension[0] != (char) CommonEnums.STATUS_STOCK.SUSPENSION &&
                                           securityInfo.HaltResumeFlag[0] != (char) CommonEnums.STATUS_STOCK.HALT &&
                                           securityInfo.HaltResumeFlag[0] !=
                                           (char) CommonEnums.STATUS_STOCK.HALT_SESSION)
                                              ? 1
                                              : 0);

                    // Check ATO and ATC
                    MarketInfo marketInfo = GetCurrentMarketInfo((short) MARKET_ID.HOSE, true);
                    char marketStatus = MarketStatus(marketInfo, (short) MARKET_ID.HOSE);

                    if (marketStatus == (char) CommonEnums.MARKET_STATUS.PRE_OPEN)
                    {
                        if (stockInfo.Best1Bid == 0 && stockInfo.Best1BidVolume != 0)
                        {
                            stockInfo.Best1Bid = (double) CommonEnums.COND_PRICE.ATO;
                        }
                        if (stockInfo.Best1Offer == 0 && stockInfo.Best1OfferVolume != 0)
                        {
                            stockInfo.Best1Offer = (double) CommonEnums.COND_PRICE.ATO;
                        }

                        //Tinh gia tam khop 
                        stockInfo.Last = (securityInfo.ProjectOpen ?? 0)/Utils.Common.PRICE_UNIT;
                        stockInfo.LastVol = 0;
                    }
                    else if (marketStatus == (char) CommonEnums.MARKET_STATUS.PRE_CLOSE)
                    {
                        if (stockInfo.Best1Bid == 0 && stockInfo.Best1BidVolume != 0)
                        {
                            stockInfo.Best1Bid = (double) CommonEnums.COND_PRICE.ATC;
                        }
                        if (stockInfo.Best1Offer == 0 && stockInfo.Best1OfferVolume != 0)
                        {
                            stockInfo.Best1Offer = (double) CommonEnums.COND_PRICE.ATC;
                        }

                        //Tinh gia tam khop 
                        stockInfo.Last = (securityInfo.ProjectOpen ?? 0)/Utils.Common.PRICE_UNIT;
                        stockInfo.LastVol = 0;
                    }

                    // Advance value
                    stockInfo.Changed = Math.Round(stockInfo.Last - stockInfo.RefPrice, 2);
                    stockInfo.PercentChanged = Math.Round(
                        ((stockInfo.Last - stockInfo.RefPrice)/stockInfo.RefPrice)*100, 2);

                    UpdateStockInfo(stockInfo);
                }
                catch (Exception ex)
                {
                    Utils.Common.Log(ex.ToString());
                }
            }
        }

        private static void UpdateStockInfoForHnx()
        {
            TList<HastcStocks> listHastcStockInfos = HastcStocksService.GetAll();

            foreach (HastcStocks hastcStockInfo in listHastcStockInfos)
            {
                Thread.Sleep(1);
                try
                {
                    //RemoveOldStockData((DateTime)hastcStockInfo.TradeDate, ((short)RTDdataServices.Entities.MARKET_ID.HASTC));

                    if (hastcStockInfo.StockType[0] != (char) CommonEnums.STOCK_TYPE.STOCK &&
                        hastcStockInfo.StockType[0] != (char) CommonEnums.STOCK_TYPE.FUND)
                        // only update for stocks, no bonds.
                    {
                        continue;
                    }

                    var stockInfo = new StockInfo
                                        {
                                            MarketID = (short) MARKET_ID.HASTC,
                                            TradeDate = (hastcStockInfo.TradeDate ?? DateTime.Now),
                                            StockSymbol = hastcStockInfo.StockSymbol,
                                            Name = hastcStockInfo.SecurityName ?? string.Empty,
                                            Floor = (double) hastcStockInfo.Floor/Utils.Common.PRICE_UNIT,
                                            Ceiling = (double) hastcStockInfo.Ceiling/Utils.Common.PRICE_UNIT,
                                            RefPrice = (double) hastcStockInfo.PriorClosePrice/Utils.Common.PRICE_UNIT,
                                            AvrPrice = (hastcStockInfo.Average ?? 0)/Utils.Common.PRICE_UNIT,
                                            OpenPrice = (hastcStockInfo.OpenPrice ?? 0)/Utils.Common.PRICE_UNIT,
                                            ClosePrice = (hastcStockInfo.ClosePrice ?? 0)/Utils.Common.PRICE_UNIT,
                                            Last = (hastcStockInfo.Last ?? 0)/Utils.Common.PRICE_UNIT,
                                            LastVol = (int) (hastcStockInfo.LastVol ?? 0),
                                            LastVal = hastcStockInfo.LastVal ?? 0,
                                            Highest = (hastcStockInfo.Highest ?? 0)/Utils.Common.PRICE_UNIT,
                                            Lowest = (hastcStockInfo.Lowest ?? 0)/Utils.Common.PRICE_UNIT
                                        };

                    //stockInfo.Status              = stockInfo.Status;
                    //stockInfo.TotalShare            = hastcStockInfo.Totalshares ?? 0;
                    stockInfo.TotalShare = stockInfo.NMTotalShare = hastcStockInfo.NmTotalTradedQtty ?? 0;
                    stockInfo.TotalForeignRoom = ((hastcStockInfo.SellForeignQtty ?? 0) +
                                                  (hastcStockInfo.RemainForeignQtty ?? 0));
                    stockInfo.AvailableForeignRoom = (hastcStockInfo.RemainForeignQtty ?? 0);
                    stockInfo.FRBoughtVol = (hastcStockInfo.BuyForeignQtty ?? 0);
                    stockInfo.FRSoldVol = (hastcStockInfo.SellForeignQtty ?? 0);

                    stockInfo.Best1Bid = (hastcStockInfo.Best1Bid ?? 0)/Utils.Common.PRICE_UNIT;
                    stockInfo.Best1BidVolume = (int) (hastcStockInfo.Best1BidVolume ?? 0);
                    stockInfo.Best2Bid = (hastcStockInfo.Best2Bid ?? 0)/Utils.Common.PRICE_UNIT;
                    stockInfo.Best2BidVolume = (int) (hastcStockInfo.Best2BidVolume ?? 0);
                    stockInfo.Best3Bid = (hastcStockInfo.Best3Bid ?? 0)/Utils.Common.PRICE_UNIT;
                    stockInfo.Best3BidVolume = (int) (hastcStockInfo.Best3BidVolume ?? 0);
                    stockInfo.Best1Offer = (hastcStockInfo.Best1Offer ?? 0)/Utils.Common.PRICE_UNIT;
                    stockInfo.Best1OfferVolume = (int) (hastcStockInfo.Best1OfferVolume ?? 0);
                    stockInfo.Best2Offer = (hastcStockInfo.Best2Offer ?? 0)/Utils.Common.PRICE_UNIT;
                    stockInfo.Best2OfferVolume = (int) (hastcStockInfo.Best2OfferVolume ?? 0);
                    stockInfo.Best3Offer = (hastcStockInfo.Best3Offer ?? 0)/Utils.Common.PRICE_UNIT;
                    stockInfo.Best3OfferVolume = (int) (hastcStockInfo.Best3OfferVolume ?? 0);

                    stockInfo.Sequence = (long) hastcStockInfo.Sequence;
                    //TODO: define enum for status of stock.

                    int status = (hastcStockInfo.Status == null ? 0 : Int32.Parse(hastcStockInfo.Status));
                    stockInfo.CanTrade = ((status == (int) CommonEnums.STATUS_STOCK_INFOSHOW.NORMAL ||
                                           (status >= (int) CommonEnums.STATUS_STOCK_INFOSHOW.BE_SERVEILANCED &&
                                            status <= (int) CommonEnums.STATUS_STOCK_INFOSHOW.SHARES_HOLDERS_MEETING))
                                              ? 1
                                              : 0);

                    // Advance value
                    stockInfo.Changed = Math.Round(stockInfo.Last - stockInfo.RefPrice, 2);
                    stockInfo.PercentChanged = Math.Round(
                        ((stockInfo.Last - stockInfo.RefPrice)/stockInfo.RefPrice)*100, 2);
                    if (hastcStockInfo.TotalBidQtty.HasValue)
                        stockInfo.Best4BidVolume = (int) hastcStockInfo.TotalBidQtty -
                                                   (stockInfo.Best1BidVolume + stockInfo.Best2BidVolume +
                                                    stockInfo.Best3BidVolume);
                    if (hastcStockInfo.TotalOfferQtty.HasValue)
                        stockInfo.Best4OfferVolume = (int) hastcStockInfo.TotalOfferQtty -
                                                     (stockInfo.Best1OfferVolume + stockInfo.Best2OfferVolume +
                                                      stockInfo.Best3OfferVolume);

                    UpdateStockInfo(stockInfo);
                }
                catch (Exception ex)
                {
                    Utils.Common.Log(ex.ToString());
                }
            }
        }

        private static void UpdateStockInfoForUpcom()
        {
            TList<UpcomStocks> listUpcomStockInfos = UpcomStocksService.GetAll();

            foreach (UpcomStocks upcomStockInfo in listUpcomStockInfos)
            {
                Thread.Sleep(1);
                try
                {
                    //RemoveOldStockData((DateTime)upcomStockInfo.TradeDate, ((short)RTDdataServices.Entities.MARKET_ID.UPCoM));

                    if (upcomStockInfo.StockType[0] != (char) CommonEnums.STOCK_TYPE.STOCK &&
                        upcomStockInfo.StockType[0] != (char) CommonEnums.STOCK_TYPE.FUND)
                        // only update for stocks, no bonds.
                    {
                        continue;
                    }

                    var stockInfo = new StockInfo
                                        {
                                            MarketID = (short) MARKET_ID.UPCoM,
                                            TradeDate = (upcomStockInfo.TradeDate ?? DateTime.Now),
                                            StockSymbol = upcomStockInfo.StockSymbol,
                                            Name = upcomStockInfo.SecurityName,
                                            Floor = (upcomStockInfo.Floor ?? 0)/Utils.Common.PRICE_UNIT,
                                            Ceiling = (upcomStockInfo.Ceiling ?? 0)/Utils.Common.PRICE_UNIT,
                                            RefPrice = (upcomStockInfo.PriorClosePrice ?? 0)/Utils.Common.PRICE_UNIT,
                                            AvrPrice = (upcomStockInfo.Average ?? 0)/Utils.Common.PRICE_UNIT,
                                            OpenPrice = (upcomStockInfo.OpenPrice ?? 0)/Utils.Common.PRICE_UNIT,
                                            ClosePrice = (upcomStockInfo.ClosePrice ?? 0)/Utils.Common.PRICE_UNIT,
                                            Last = (upcomStockInfo.Last ?? 0)/Utils.Common.PRICE_UNIT,
                                            LastVol = (int) (upcomStockInfo.LastVol ?? 0),
                                            LastVal = upcomStockInfo.LastVal ?? 0,
                                            Highest = (upcomStockInfo.Highest ?? 0)/Utils.Common.PRICE_UNIT,
                                            Lowest = (upcomStockInfo.Lowest ?? 0)/Utils.Common.PRICE_UNIT,
                                            TotalShare = upcomStockInfo.Totalshares ?? 0,
                                            TotalForeignRoom = ((upcomStockInfo.SellForeignQtty ?? 0) +
                                                                (upcomStockInfo.RemainForeignQtty ?? 0)),
                                            AvailableForeignRoom = (upcomStockInfo.RemainForeignQtty ?? 0),
                                            FRBoughtVol = (upcomStockInfo.BuyForeignQtty ?? 0),
                                            FRSoldVol = (upcomStockInfo.SellForeignQtty ?? 0),
                                            Best1Bid = (upcomStockInfo.Best1Bid ?? 0)/Utils.Common.PRICE_UNIT,
                                            Best1BidVolume = (int) (upcomStockInfo.Best1BidVolume ?? 0),
                                            Best2Bid = (upcomStockInfo.Best2Bid ?? 0)/Utils.Common.PRICE_UNIT,
                                            Best2BidVolume = (int) (upcomStockInfo.Best2BidVolume ?? 0),
                                            Best3Bid = (upcomStockInfo.Best3Bid ?? 0)/Utils.Common.PRICE_UNIT,
                                            Best3BidVolume = (int) (upcomStockInfo.Best3BidVolume ?? 0),
                                            Best1Offer = (upcomStockInfo.Best1Offer ?? 0)/Utils.Common.PRICE_UNIT,
                                            Best1OfferVolume = (int) (upcomStockInfo.Best1OfferVolume ?? 0),
                                            Best2Offer = (upcomStockInfo.Best2Offer ?? 0)/Utils.Common.PRICE_UNIT,
                                            Best2OfferVolume = (int) (upcomStockInfo.Best2OfferVolume ?? 0),
                                            Best3Offer = (upcomStockInfo.Best3Offer ?? 0)/Utils.Common.PRICE_UNIT,
                                            Best3OfferVolume = (int) (upcomStockInfo.Best3OfferVolume ?? 0)
                                        };

                    //stockInfo.Status              = stockInfo.Status;

                    stockInfo.CanTrade = ((stockInfo.Status == 0 || (stockInfo.Status >= 3 && stockInfo.Status <= 5))
                                              ? 1
                                              : 0);
                    stockInfo.Sequence = (long) upcomStockInfo.Sequence;

                    // Advance value
                    stockInfo.Changed = Math.Round(stockInfo.Last - stockInfo.RefPrice, 2);
                    stockInfo.PercentChanged = Math.Round(
                        ((stockInfo.Last - stockInfo.RefPrice)/stockInfo.RefPrice)*100, 2);
                    stockInfo.Best4BidVolume = (int) (upcomStockInfo.TotalBidQtty ?? 0) -
                                               (stockInfo.Best1BidVolume + stockInfo.Best2BidVolume +
                                                stockInfo.Best3BidVolume);
                    stockInfo.Best4OfferVolume = (int) (upcomStockInfo.TotalOfferQtty ?? 0) -
                                                 (stockInfo.Best1OfferVolume + stockInfo.Best2OfferVolume +
                                                  stockInfo.Best3OfferVolume);

                    UpdateStockInfo(stockInfo);
                }
                catch (Exception ex)
                {
                    Utils.Common.Log(ex.ToString());
                }
            }
        }

        private static void UpdateStockInfo(StockInfo stockInfo)
        {
            try
            {
                if (ListStocks.ContainsKey(stockInfo.StockSymbol))
                {
                    ListStocks[stockInfo.StockSymbol] = stockInfo;
                }
                else
                {
                    ListStocks.Add(stockInfo.StockSymbol, stockInfo);
                }
            }
            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }
        }

        public static void UpdateAllStockInfo()
        {
            UpdateStockInfoForHose();
            UpdateStockInfoForHnx();
            UpdateStockInfoForUpcom();
            //UpdateCompanyInfo();
            UpdateLangCompanyInfo();
        }

        public static void UpdateAllStockInfo(bool enableUpdatingHose, bool enableUpdatingHnx, bool enableUpdatingUpcom)
        {
            if (enableUpdatingHose)
            {
                UpdateStockInfoForHose();
            }

            if (enableUpdatingHnx)
            {
                UpdateStockInfoForHnx();
            }

            if (enableUpdatingUpcom)
            {
                UpdateStockInfoForUpcom();
            }

            if (_isFirstTimeUpdateCompanyInfo)
            {
                //UpdateCompanyInfo();
                UpdateLangCompanyInfo();
                _isFirstTimeUpdateCompanyInfo = false;
            }
        }

        public static char MarketStatus(MarketInfo marInfo, int marketId)
        {
            var marketStatus = (char) CommonEnums.MARKET_STATUS.UNVAILABLE;

            try
            {
                if (marInfo == null)
                {
                    bool hasStocks = HaveStocksForToday((short) marketId);

                    if (!hasStocks)
                    {
                        marketStatus = (char) CommonEnums.MARKET_STATUS.UNVAILABLE;
                    }
                    else
                    {
                        marketStatus = (char) CommonEnums.MARKET_STATUS.READY;
                    }

                    return marketStatus;
                }

                switch (marketId)
                {
                    case (short) CommonEnums.MARKET_ID.HOSE:
                        {
                            //if (marInfo.Time >= (int)HOSETimePreOpen && marInfo.Time < (int)HOSETimeOpen)
                            //{
                            //    marketStatus = (char)CommonEnums.MARKET_STATUS.PRE_OPEN;
                            //}
                            //else if ((marInfo.Time >= HOSETimeOpen) && (marInfo.Time < HOSETimePreClose))
                            //{
                            //    marketStatus = (char)CommonEnums.MARKET_STATUS.OPEN;
                            //}
                            //else if ((marInfo.Time >= HOSETimePreClose) && (marInfo.Time < HOSETimePutThrough))
                            //{
                            //    marketStatus = (char)CommonEnums.MARKET_STATUS.PRE_CLOSE;
                            //}
                            //else if ((marInfo.Time >= HOSETimePutThrough) && (marInfo.Time < HOSETimeEndTrading))
                            //{
                            //    marketStatus = (char)CommonEnums.MARKET_STATUS.CLOSE;
                            //}
                            //else if (marInfo.Time >= HOSETimeEndTrading)
                            //{
                            //    marketStatus = (char)CommonEnums.MARKET_STATUS.CLOSE_PT;
                            //}

                            marketStatus = marInfo.Status;
                            break;
                        }
                    case (short) CommonEnums.MARKET_ID.HNX:
                        {
                            /*if (marInfo.Time < (int)RTDataServices.Common.MARKET_TIME.CLOSE)
                            {
                                marketStatus = (char)CommonEnums.MARKET_STATUS.OPEN;
                            }
                            else
                            {
                                marketStatus = (char)CommonEnums.MARKET_STATUS.CLOSE;
                            }*/

                            int timeNow = Utils.Common.DateTime2Int(DateTime.Now);


                            if (timeNow < HnxTimeStartDay || timeNow >= HnxTimeClose)
                                marketStatus = (char) CommonEnums.MARKET_STATUS.CLOSE;
                            else if (timeNow >= HnxTimeBeginHaft && timeNow < HnxTimeEndHaft)
                                marketStatus = (char) CommonEnums.MARKET_STATUS.HAFT;
                            else if ((timeNow >= HnxTimeOpen && timeNow < HnxTimeBeginHaft) ||
                                     (marInfo.Time >= HnxTimeOpen && marInfo.Time < HnxTimeBeginHaft))
                                marketStatus = (char) CommonEnums.MARKET_STATUS.OPEN;
                            else if ((timeNow >= HnxTimeEndHaft && timeNow < HnxTimeClose) ||
                                     (marInfo.Time >= HnxTimeEndHaft && marInfo.Time < HnxTimeClose))
                                marketStatus = (char) CommonEnums.MARKET_STATUS.OPEN_2;

                            break;
                        }
                    case (short) CommonEnums.MARKET_ID.UPCoM:
                        {
                            int timeNow = Utils.Common.DateTime2Int(DateTime.Now);

                            if (timeNow < UpcomTimeStartDay || timeNow >= UpcomTimeClose)
                                marketStatus = (char) CommonEnums.MARKET_STATUS.CLOSE;
                            else if (timeNow >= UpcomTimeBeginHalf && timeNow < UpcomTimeEndHalf)
                                marketStatus = (char) CommonEnums.MARKET_STATUS.HAFT;
                            else if ((timeNow >= UpcomTimeOpen && timeNow < UpcomTimeBeginHalf) ||
                                     (marInfo.Time >= UpcomTimeOpen && marInfo.Time < UpcomTimeBeginHalf))
                                marketStatus = (char) CommonEnums.MARKET_STATUS.OPEN;
                            else if ((timeNow >= UpcomTimeEndHalf && timeNow < UpcomTimeClose) ||
                                     (marInfo.Time >= UpcomTimeEndHalf && marInfo.Time < UpcomTimeClose))
                                marketStatus = (char) CommonEnums.MARKET_STATUS.OPEN_2;

                            break;
                        }
                    default:
                        break;
                }
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }
            return marketStatus;
        }

        public static bool IsOpenedMarket(int marketId)
        {
            try
            {
                if (Utils.Common.RunMode == "TEST")
                {
                    return true;
                }
                MarketInfo marInfo = GetCurrentMarketInfo(marketId, true);

                char status = MarketStatus(marInfo, marketId);

                switch (marketId)
                {
                    case (short) CommonEnums.MARKET_ID.HOSE:
                        {
                            return (status == (char) CommonEnums.MARKET_STATUS.READY ||
                                    status == (char) CommonEnums.MARKET_STATUS.PRE_OPEN ||
                                    status == (char) CommonEnums.MARKET_STATUS.OPEN ||
                                    status == (char) CommonEnums.MARKET_STATUS.OPEN_2 ||
                                    status == (char) CommonEnums.MARKET_STATUS.PRE_CLOSE);
                        }
                    case (short) CommonEnums.MARKET_ID.HNX:
                        {
                            return (status == (char) CommonEnums.MARKET_STATUS.READY ||
                                    status == (char) CommonEnums.MARKET_STATUS.OPEN ||
                                    status == (char) CommonEnums.MARKET_STATUS.OPEN_2);
                        }

                    case (short) CommonEnums.MARKET_ID.UPCoM:
                        {
                            return (status == (char) CommonEnums.MARKET_STATUS.READY ||
                                    status == (char) CommonEnums.MARKET_STATUS.OPEN ||
                                    status == (char) CommonEnums.MARKET_STATUS.OPEN_2);
                        }

                    default:
                        return false;
                }
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return false;
        }

        private static void UpdateMarketInfoForHose()
        {
            TList<Totalmarket> listTotalMarketInfos = TotalMarketServive.GetAll();
            foreach (Totalmarket totalMarketInfo in listTotalMarketInfos)
            {
                try
                {
                    Thread.Sleep(1);
                    //RemoveOldMarketInfo((DateTime)totalMarketInfo.TradeDate, (short)RTDdataServices.Entities.MARKET_ID.HOSE);

                    var marketInfo = new MarketInfo
                                         {
                                             MarketId = (short) MARKET_ID.HOSE,
                                             TradeDate = (totalMarketInfo.TradeDate ?? DateTime.Now),
                                             SetIndex = (totalMarketInfo.SetIndex ?? 0)/Utils.Common.PRICE_UNIT
                                         };

                    if (_priorIndex == -1)
                    {
                        MarketInfo firstMarketInfo = GetFirstMarketInfo((int) Utils.Common.MARKET_ID.HOSE, true) ??
                                                     GetFirstMarketInfo((int) Utils.Common.MARKET_ID.HOSE, false);

                        if (firstMarketInfo != null)
                        {
                            _priorIndex = firstMarketInfo.SetIndex;
                        }
                    }

                    marketInfo.IndexChanged = (_priorIndex == -1 ? 0 : marketInfo.SetIndex - _priorIndex);
                    marketInfo.TotalTrade = (int) (totalMarketInfo.TotalTrade ?? 0);
                    marketInfo.TotalShares = totalMarketInfo.Totalshare ?? 0;
                    marketInfo.TotalValues = (totalMarketInfo.TotalValue ?? 0)*10;
                    marketInfo.UpVolume = totalMarketInfo.UpVolume ?? 0;
                    marketInfo.DownVolume = totalMarketInfo.DownVolume ?? 0;
                    marketInfo.NoChangeVolume = totalMarketInfo.NoChangeVolume ?? 0;
                    marketInfo.Advances = Advances(marketInfo.MarketId);
                    marketInfo.NoChange = NoChange(marketInfo.MarketId);
                    marketInfo.Declines = Declines(marketInfo.MarketId);
                    marketInfo.Time = (int) (totalMarketInfo.Time ?? 0);
                    //marketInfo.Status           = MarketStatus(marketInfo, marketInfo.MarketId);
                    marketInfo.Status = totalMarketInfo.Status[0];
                    marketInfo.CountCeiling = GetCeiling(marketInfo.MarketId);
                    marketInfo.CountFloor = GetFloor(marketInfo.MarketId);
                    marketInfo.CountNoChange = GetNoChange(marketInfo.MarketId);
                    UpdateMarketInfo(marketInfo);
                }
                catch (Exception ex)
                {
                    Utils.Common.Log(ex.ToString());
                }
            }
        }

        public static void UpdateIndexVn30History()
        {
            TList<IndexVn30History> listIndexVn30HistoryInfo = IndexVn30HistoryService.GetAll();
            foreach (IndexVn30History vn30History in listIndexVn30HistoryInfo)
            {
                Thread.Sleep(1);
                if (vn30History.Time != null)
                {
                    if (!ListIndexVn30History.ContainsKey(vn30History.Time.Value))
                        ListIndexVn30History.Add(vn30History.Time.Value, vn30History);
                    else
                        ListIndexVn30History[vn30History.Time.Value] = vn30History;
                }
            }
        }

        public static void UpdateIndexVn30()
        {
            const string whereClause = "[Time] = (Select MAX([Time]) from [Index_VN30])";
            int count;
            TList<IndexVn30> listIndexVn30Info = IndexVn30Service.GetPaged(whereClause, string.Empty, 0, int.MaxValue,
                                                                           out count);
            foreach (IndexVn30 indexVn30Info in listIndexVn30Info)
            {
                Thread.Sleep(1);
                var indexInfo = new IndexInfo
                                    {
                                        ID = indexVn30Info.Id,
                                        Index = double.Parse(((int) (indexVn30Info.Index ?? 0)).ToString())/100,
                                        Up = (short) (indexVn30Info.Up ?? 0),
                                        Down = (short) (indexVn30Info.Down ?? 0),
                                        NoChange = NoChangeVn30(),
                                        TotalShares = (indexVn30Info.TotalShares ?? 0),
                                        TotalValues = (indexVn30Info.TotalValues ?? 0),
                                        TradeDate = (indexVn30Info.TradeDate ?? DateTime.Now),
                                        Time = (int) (indexVn30Info.Time ?? 0),
                                        CountCeiling = GetCeilingVn30(),
                                        CountFloor = GetFloorVn30(),
                                        CountNoChange = GetNoChangeVn30()
                                    };
                //indexInfo.NoChange = (short) (indexVn30Info.NoChange ?? 0);
                if (!ListIndexVn30.ContainsKey(indexInfo.Time))
                {
                    ListIndexVn30.Add(indexInfo.Time, indexInfo);
                }
                else
                {
                    ListIndexVn30[indexInfo.Time] = indexInfo;
                }
            }
        }

        private static void UpdateMarketInfoForHnx()
        {
            //var stopwatch = new Stopwatch();
            //stopwatch.Start();
            //TList<HastcMarket> listHastcMarketInfos = hastcMarketService.GetAll();
            const string whereClause = "[Time] = (Select MAX([Time]) from [hastc_market])";
            int count;
            TList<HastcMarket> listHastcMarketInfos = HastcMarketService.GetPaged(whereClause, string.Empty, 0,
                                                                                  int.MaxValue, out count);
            MarketInfo latestMarketInfo = null;
            foreach (HastcMarket hastcMarketInfo in listHastcMarketInfos)
            {
                try
                {
                    //RemoveOldMarketInfo((DateTime)hastcMarketInfo.TradeDate, (short) RTDdataServices.Entities.MARKET_ID.HASTC);

                    var marketInfo = new MarketInfo
                                         {
                                             MarketId = (short) MARKET_ID.HASTC,
                                             TradeDate = (hastcMarketInfo.TradeDate ?? DateTime.Now),
                                             SetIndex = hastcMarketInfo.SetIndex ?? 0
                                         };

                    marketInfo.IndexChanged = marketInfo.SetIndex - (hastcMarketInfo.OpenIndex ?? 0);
                    marketInfo.TotalTrade = (int) (hastcMarketInfo.TotalTrade ?? 0);
                    marketInfo.TotalShares = hastcMarketInfo.Totalshare ?? 0;
                    marketInfo.TotalValues = (hastcMarketInfo.TotalValue ?? 0);
                    marketInfo.UpVolume = 0;
                    marketInfo.DownVolume = 0;
                    marketInfo.NoChangeVolume = 0;
                    marketInfo.Advances = Advances(marketInfo.MarketId);
                    marketInfo.NoChange = NoChange(marketInfo.MarketId);
                    marketInfo.Declines = Declines(marketInfo.MarketId);
                    marketInfo.Time = (int) (hastcMarketInfo.Time ?? 0);
                    marketInfo.Status = MarketStatus(marketInfo, marketInfo.MarketId);
                    marketInfo.CountCeiling = GetCeiling(marketInfo.MarketId);
                    marketInfo.CountFloor = GetFloor(marketInfo.MarketId);
                    marketInfo.CountNoChange = GetNoChange(marketInfo.MarketId);
                    UpdateMarketInfo(marketInfo);
                    latestMarketInfo = marketInfo;
                }
                catch (Exception ex)
                {
                    Utils.Common.Log(ex.ToString());
                }
            }
            if (latestMarketInfo != null)
            {
                char marketState = latestMarketInfo.Status;
                DateTime sessionTime = DateTime.Now;
                switch (_tradingStatusHnx)
                {
                    case (char) CommonEnums.MARKET_STATUS.INIT_APP:
                    case (char) CommonEnums.MARKET_STATUS.UNVAILABLE:
                        _tradingStatusHnx = marketState;
                        _sessionTimeHnx = sessionTime;

                        break;
                    case (char) CommonEnums.MARKET_STATUS.READY:
                        if (marketState == (char) CommonEnums.MARKET_STATUS.OPEN)
                        {
                            _tradingStatusHnx = marketState;
                            _sessionTimeHnx = sessionTime;
                        }

                        break;
                    case (char) CommonEnums.MARKET_STATUS.OPEN:
                        {
                            TimeSpan duration = sessionTime - _sessionTimeHnx;
                            if (duration.TotalSeconds > AppConfig.TradingDurationHNX ||
                                marketState == (char) CommonEnums.MARKET_STATUS.CLOSE)
                            {
                                _tradingStatusHnx = (char) CommonEnums.MARKET_STATUS.CLOSE;
                                _sessionTimeHnx = sessionTime;
                            }
                        }
                        break;

                    default:
                        _tradingStatusHnx = marketState;
                        _sessionTimeHnx = sessionTime;
                        break;
                }
                latestMarketInfo.Status = _tradingStatusHnx;
                //Update latest market information
                UpdateMarketInfo(latestMarketInfo);
            }

            /*stopwatch.Stop();
            LogHandler.Log("UpdateMarketInfoForHNX: time " + stopwatch.ElapsedMilliseconds, "UpdateMarketInfoForHNX",
                           TraceEventType.Information);*/
        }


        private static void UpdateMarketInfoForUpcom()
        {
            /*var stopwatch = new Stopwatch();
            stopwatch.Start();*/
            const string whereClause = "[Time] = (Select MAX([Time]) from [upcom_market])";
            int count;
            TList<UpcomMarket> listUpcomMarketInfos = UpcomMarketService.GetPaged(whereClause, string.Empty, 0,
                                                                                  int.MaxValue, out count);
            MarketInfo latestMarketInfo = null;
            foreach (UpcomMarket upcomMarketInfo in listUpcomMarketInfos)
            {
                try
                {
                    Thread.Sleep(1);
                    //RemoveOldMarketInfo((DateTime)upcomMarketInfo.TradeDate, (short)RTDdataServices.Entities.MARKET_ID.UPCoM);

                    var marketInfo = new MarketInfo
                                         {
                                             MarketId = (short) MARKET_ID.UPCoM,
                                             TradeDate = (upcomMarketInfo.TradeDate ?? DateTime.Now),
                                             SetIndex = upcomMarketInfo.SetIndex ?? 0
                                         };

                    marketInfo.IndexChanged = marketInfo.SetIndex - (upcomMarketInfo.OpenIndex ?? 0);
                    marketInfo.TotalTrade = (int) (upcomMarketInfo.TotalTrade ?? 0);
                    marketInfo.TotalShares = upcomMarketInfo.Totalshare ?? 0;
                    marketInfo.TotalValues = (upcomMarketInfo.TotalValue ?? 0);
                    marketInfo.UpVolume = 0;
                    marketInfo.DownVolume = 0;
                    marketInfo.NoChangeVolume = 0;
                    marketInfo.Advances = Advances(marketInfo.MarketId);
                    marketInfo.NoChange = NoChange(marketInfo.MarketId);
                    marketInfo.Declines = Declines(marketInfo.MarketId);
                    marketInfo.Time = (int) (upcomMarketInfo.Time ?? 0);
                    marketInfo.Status = MarketStatus(marketInfo, marketInfo.MarketId);
                    marketInfo.CountCeiling = GetCeiling(marketInfo.MarketId);
                    marketInfo.CountFloor = GetFloor(marketInfo.MarketId);

                    UpdateMarketInfo(marketInfo);
                    latestMarketInfo = marketInfo;
                }
                catch (Exception ex)
                {
                    Utils.Common.Log(ex.ToString());
                }
            }
            if (latestMarketInfo != null)
            {
                char marketState = latestMarketInfo.Status;
                DateTime sessionTime = DateTime.Now;
                switch (_tradingStatusUpcom)
                {
                    case (char) CommonEnums.MARKET_STATUS.INIT_APP:
                    case (char) CommonEnums.MARKET_STATUS.UNVAILABLE:
                        _tradingStatusUpcom = marketState;
                        _sessionTimeUpcom = sessionTime;

                        break;
                    case (char) CommonEnums.MARKET_STATUS.READY:
                        if (marketState == (char) CommonEnums.MARKET_STATUS.OPEN)
                        {
                            _tradingStatusUpcom = marketState;
                            _sessionTimeUpcom = sessionTime;
                        }

                        break;
                    case (char) CommonEnums.MARKET_STATUS.OPEN:
                        {
                            TimeSpan timeDuration = sessionTime - _sessionTimeUpcom;
                            if (timeDuration.TotalSeconds > AppConfig.TradingDurationUpcomSession1 ||
                                marketState == (char) CommonEnums.MARKET_STATUS.HAFT)
                            {
                                _tradingStatusUpcom = (char) CommonEnums.MARKET_STATUS.HAFT;
                                _sessionTimeUpcom = sessionTime;
                            }
                        }
                        break;
                    case (char) CommonEnums.MARKET_STATUS.HAFT:
                        if (marketState == (char) CommonEnums.MARKET_STATUS.OPEN_2)
                        {
                            _tradingStatusUpcom = marketState;
                            _sessionTimeUpcom = sessionTime;
                        }

                        break;

                    case (char) CommonEnums.MARKET_STATUS.OPEN_2:
                        {
                            TimeSpan timeDuration = sessionTime - _sessionTimeUpcom;
                            if (timeDuration.TotalSeconds > AppConfig.TradingDurationUpcomSession2 ||
                                marketState == (char) CommonEnums.MARKET_STATUS.CLOSE)
                            {
                                _tradingStatusUpcom = (char) CommonEnums.MARKET_STATUS.CLOSE;
                                _sessionTimeUpcom = sessionTime;
                            }
                        }
                        break;

                    default:
                        _tradingStatusUpcom = marketState;
                        _sessionTimeUpcom = sessionTime;
                        break;
                }

                latestMarketInfo.Status = _tradingStatusUpcom;
                //Update latest market information
                UpdateMarketInfo(latestMarketInfo);
            }
            /*stopwatch.Stop();
            LogHandler.Log("UpdateMarketInfoForUPCOM: time " + stopwatch.ElapsedMilliseconds, "UpdateMarketInfoForUPCOM",
                           TraceEventType.Information);*/
        }

        private static void UpdateMarketInfo(MarketInfo marketInfo)
        {
            try
            {
                if (!ListMarketInfos.ContainsKey(marketInfo.MarketId))
                {
                    var marketInfoEntry = new Dictionary<int, MarketInfo> {{marketInfo.Time, marketInfo}};

                    ListMarketInfos.Add(marketInfo.MarketId, marketInfoEntry);

                    return;
                }

                Dictionary<int, MarketInfo> currentMarketInfo = ListMarketInfos[marketInfo.MarketId];

                if (!currentMarketInfo.ContainsKey(marketInfo.Time))
                {
                    currentMarketInfo.Add(marketInfo.Time, marketInfo);
                }
                else
                {
                    currentMarketInfo[marketInfo.Time] = marketInfo;
                }

                ListMarketInfos[marketInfo.MarketId] = currentMarketInfo;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }
        }

        public static void UpdateAllMarketInfo()
        {
            UpdateMarketInfoForHose();
            UpdateMarketInfoForHnx();
            UpdateMarketInfoForUpcom();
        }

        public static void UpdateAllMarketInfo(bool enableUpdatingHose, bool enableUpdatingHnx, bool enableUpdatingUpcom)
        {
            if (enableUpdatingHose)
            {
                UpdateMarketInfoForHose();
            }

            if (enableUpdatingHnx)
            {
                UpdateMarketInfoForHnx();
            }

            if (enableUpdatingUpcom)
            {
                UpdateMarketInfoForUpcom();
            }
        }


        //private static void UpdateCompanyInfo(CommonEnums.MARKET_ID marketId)
        //{
        //    try
        //    {
        //        List<StockInfo> listStockInfo = Select_All_StockInfo((short) marketId);
        //        if (listStockInfo != null && listStockInfo.Count > 0)
        //        {
        //            foreach (StockInfo stockInfo in listStockInfo)
        //            {
        //                Thread.Sleep(1);
        //                var companyInfo = new CompanyInfo
        //                                      {
        //                                          MarketId = stockInfo.MarketID,
        //                                          Code = stockInfo.StockSymbol,
        //                                          FullName = stockInfo.Name
        //                                      };

        //                if (!ListCompanies.ContainsKey(stockInfo.StockSymbol.Trim()))
        //                    ListCompanies.Add(companyInfo.Code.Trim(), companyInfo);
        //                else
        //                    ListCompanies[stockInfo.StockSymbol.Trim()] = companyInfo;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Utils.Common.Log(ex.ToString());
        //    }
        //}

        //public static void UpdateCompanyInfo()
        //{
        //    try
        //    {
        //        UpdateCompanyInfo(CommonEnums.MARKET_ID.HOSE);
        //        UpdateCompanyInfo(CommonEnums.MARKET_ID.HNX);
        //        UpdateCompanyInfo(CommonEnums.MARKET_ID.UPCoM);
        //    }
        //    catch (Exception ex)
        //    {
        //        Utils.Common.Log(ex.ToString());
        //    }
        //}

        public static void UpdateLangCompanyInfo()
        {
            try
            {
                DataSet dataSetCompany = CompanyInfoService.GetAllCompanyInfoLanguageByCode(" ");
                if (dataSetCompany != null)
                {
                    if (dataSetCompany.Tables[0].Rows.Count > 0)
                    {
                        var companyEntiyVi = new Dictionary<string, CompanyInfo>();
                        var companyEntiyEn = new Dictionary<string, CompanyInfo>();

                        foreach (DataRow row in dataSetCompany.Tables[0].Rows)
                        {
                            Thread.Sleep(1);
                            int companyId = int.Parse(row["CompanyId"].ToString());
                            string companyName = row["CompanyName"].ToString().Trim();
                            string languageId = row["LanguageId"].ToString().Trim();
                            short marketId = short.Parse(row["MarketId"].ToString());
                            string code = row["Code"].ToString().Trim();
                            var companyInfo = new CompanyInfo
                                                  {
                                                      Code = code,
                                                      FullName = companyName,
                                                      MarketId = marketId,
                                                      CompanyId = companyId,
                                                      LanguageId = languageId
                                                  };
                            if (languageId.Trim().Equals(Constants.ENGLISH_LANGUAGE_ID))
                            {
                                if (companyEntiyEn.ContainsKey(code))
                                    companyEntiyEn[code] = companyInfo;
                                else
                                    companyEntiyEn.Add(code, companyInfo);
                            }
                            else
                            {
                                if (companyEntiyVi.ContainsKey(code))
                                    companyEntiyVi[code] = companyInfo;
                                else
                                    companyEntiyVi.Add(code, companyInfo);
                            }
                        }
                        if (ListLangCompanies.ContainsKey(Constants.ENGLISH_LANGUAGE_ID))
                            ListLangCompanies[Constants.ENGLISH_LANGUAGE_ID] = companyEntiyEn;
                        else
                            ListLangCompanies.Add(Constants.ENGLISH_LANGUAGE_ID, companyEntiyEn);

                        if (ListLangCompanies.ContainsKey(Constants.VIETNAMESE_LANGUAGE_ID))
                            ListLangCompanies[Constants.VIETNAMESE_LANGUAGE_ID] = companyEntiyVi;
                        else
                            ListLangCompanies.Add(Constants.VIETNAMESE_LANGUAGE_ID, companyEntiyVi);
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }
        }

        /*
        private static void UpdateLangCompanyInfo()
        {
            try
            {
                TList<Language> listLanguages = languageService.GetAll();

                foreach (Language lang in listLanguages)
                {
                    DataSet dsCompanies = companyInfoService.GetCompanyInfoByLanguageId(lang.LanguageId);

                    if (!ListLangCompanies.ContainsKey(lang.LanguageId))
                    {
                        var companyInfoEntry = new Dictionary<string, CompanyInfo>();

                        if (dsCompanies.Tables[0].Rows.Count > 0)
                        {
                            for (int index = 0; index < dsCompanies.Tables[0].Rows.Count; index++)
                            {
                                try
                                {
                                    Thread.Sleep(1);
                                    var companyInfo = new CompanyInfo();

                                    companyInfo.Code = dsCompanies.Tables[0].Rows[index]["Code"].ToString();
                                    companyInfo.FullName = dsCompanies.Tables[0].Rows[index]["CompanyName"].ToString();
                                    companyInfo.MarketId =
                                        Int16.Parse(dsCompanies.Tables[0].Rows[index]["MarketId"].ToString());

                                    if (!companyInfoEntry.ContainsKey(companyInfo.Code))
                                    {
                                        companyInfoEntry.Add(companyInfo.Code, companyInfo);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Common.Log(ex.ToString());
                                }
                            }
                        }

                        ListLangCompanies.Add(lang.LanguageId, companyInfoEntry);
                        
                        continue;
                    }

                    Dictionary<string, CompanyInfo> langCompanyInfoEntry = ListLangCompanies[lang.LanguageId];

                    if (dsCompanies.Tables[0].Rows.Count > 0)
                    {
                        for (int index = 0; index < dsCompanies.Tables[0].Rows.Count; index++)
                        {
                            Thread.Sleep(1);
                            var companyInfo = new CompanyInfo();

                            companyInfo.Code = dsCompanies.Tables[0].Rows[index]["Code"].ToString();
                            companyInfo.FullName = dsCompanies.Tables[0].Rows[index]["CompanyName"].ToString();
                            companyInfo.MarketId =
                                Int16.Parse(dsCompanies.Tables[0].Rows[index]["MarketId"].ToString());

                            if (!langCompanyInfoEntry.ContainsKey(companyInfo.Code))
                            {
                                langCompanyInfoEntry.Add(companyInfo.Code, companyInfo);
                            }
                            else
                            {
                                langCompanyInfoEntry[companyInfo.Code] = companyInfo;
                            }
                        }

                        ListLangCompanies[lang.LanguageId] = langCompanyInfoEntry;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Log(ex.ToString());
            }
        }*/

        public static void Update(HOSEPutAdInfo info)
        {
            //HOSEPutAdInfoData.Update(info);
        }

        public void Update(HOSEPutExecInfo info)
        {
            //HOSEPutExecInfoData.Update(info);
        }

        public void Update(HOSEForeignRoomInfo info)
        {
            //HOSEForeignRoomInfoData.Update(info);
        }

        private static void UpdateTransactionForHose()
        {
            // Get all data from hose transaction id
            TList<HoseTransactions> listTransactions = HoseTransactionsService.GetAllById(_hoseTransactionsId);

            foreach (HoseTransactions hoseTransactions in listTransactions)
            {
                try
                {
                    //RemoveOldTransactionInfo((DateTime)hoseTransactions.TradeDate, (short)RTDdataServices.Entities.MARKET_ID.HOSE);

                    var transactionInfo = new HOSETransactionInfo {Id = (int) hoseTransactions.Id};

                    if (!string.IsNullOrEmpty(hoseTransactions.StockSymbol))
                    {
                        transactionInfo.StockSymbol = hoseTransactions.StockSymbol.Trim();
                    }
                    else
                    {
                        continue;
                    }

                    transactionInfo.TradeDate = (hoseTransactions.TradeDate ?? DateTime.Now);

                    if (hoseTransactions.Price != null)
                    {
                        transactionInfo.Price = (double) hoseTransactions.Price/Utils.Common.PRICE_UNIT;

                        StockInfo stockInfo = GetStock(transactionInfo.StockSymbol);

                        if (stockInfo != null)
                        {
                            transactionInfo.Changed = Math.Round(transactionInfo.Price - stockInfo.RefPrice, 2);
                        }
                    }
                    if (hoseTransactions.Vol != null)
                        transactionInfo.Vol = (int) hoseTransactions.Vol;
                    if (hoseTransactions.Val != null)
                        transactionInfo.Val = (long) hoseTransactions.Val;
                    if (hoseTransactions.AccumulatedVol != null)
                        transactionInfo.AccumulatedVol = (int) hoseTransactions.AccumulatedVol;
                    if (hoseTransactions.AccumulatedVal != null)
                        transactionInfo.AccumulatedVal = (long) hoseTransactions.AccumulatedVal;
                    if (hoseTransactions.Highest != null)
                        transactionInfo.Highest = (double) hoseTransactions.Highest/Utils.Common.PRICE_UNIT;
                    if (hoseTransactions.Lowest != null)
                        transactionInfo.Lowest = (double) hoseTransactions.Lowest/Utils.Common.PRICE_UNIT;
                    if (hoseTransactions.Time != null)
                        transactionInfo.Time = (int) hoseTransactions.Time;

                    transactionInfo.Side = hoseTransactions.Side;

                    Db4OManager.Insert(transactionInfo);
                    _hoseTransactionsId = hoseTransactions.Id;
                }
                catch (Exception ex)
                {
                    Utils.Common.Log(ex.ToString());
                }
            }
        }

        private static void UpdateTransactionForHnx()
        {
            TList<HastcTransactions> listTransactions = HastcTransactionsService.GetAllById(_hastcTransactionsId);

            foreach (HastcTransactions hastcTransactions in listTransactions)
            {
                try
                {
                    //RemoveOldTransactionInfo((DateTime)hastcTransactions.TradeDate, (short)RTDdataServices.Entities.MARKET_ID.HASTC);

                    var transactionInfo = new HNXTransactionInfo {Id = (int) hastcTransactions.Id};

                    if (!string.IsNullOrEmpty(hastcTransactions.StockSymbol))
                    {
                        transactionInfo.StockSymbol = hastcTransactions.StockSymbol;
                    }
                    else
                    {
                        continue;
                    }

                    transactionInfo.TradeDate = (hastcTransactions.TradeDate ?? DateTime.Now);

                    if (hastcTransactions.Price != null)
                    {
                        transactionInfo.Price = (double) hastcTransactions.Price/Utils.Common.PRICE_UNIT;

                        StockInfo stockInfo = GetStock(transactionInfo.StockSymbol);

                        if (stockInfo != null)
                        {
                            transactionInfo.Changed = Math.Round(transactionInfo.Price - stockInfo.RefPrice, 2);
                        }
                    }
                    if (hastcTransactions.Vol != null)
                        transactionInfo.Vol = (int) hastcTransactions.Vol;
                    if (hastcTransactions.Val != null)
                        transactionInfo.Val = (long) hastcTransactions.Val;
                    if (hastcTransactions.AccumulatedVol != null)
                        transactionInfo.AccumulatedVol = (int) hastcTransactions.AccumulatedVol;
                    if (hastcTransactions.AccumulatedVal != null)
                    {
                        transactionInfo.AccumulatedVal = (long) hastcTransactions.AccumulatedVal;
                    }
                    if (hastcTransactions.Highest != null)
                    {
                        transactionInfo.Highest = (double) hastcTransactions.Highest/Utils.Common.PRICE_UNIT;
                    }
                    if (hastcTransactions.Lowest != null)
                    {
                        transactionInfo.Lowest = (double) hastcTransactions.Lowest/Utils.Common.PRICE_UNIT;
                    }
                    if (hastcTransactions.Time != null)
                    {
                        transactionInfo.Time = (int) hastcTransactions.Time;
                    }

                    transactionInfo.Side = hastcTransactions.Side;

                    Db4OManager.Insert(transactionInfo);
                    _hastcTransactionsId = hastcTransactions.Id;
                }
                catch (Exception ex)
                {
                    Utils.Common.Log(ex.ToString());
                }
            }
        }

        private static void UpdateTransactionForUpcom()
        {
            TList<UpcomTransactions> listTransactions = UpcomTransactionsService.GetAllById(_upcomTransactionsId);

            foreach (UpcomTransactions upcomTransactions in listTransactions)
            {
                try
                {
                    //RemoveOldTransactionInfo((DateTime)upcomTransactions.TradeDate, (short)RTDdataServices.Entities.MARKET_ID.UPCoM);

                    var transactionInfo = new UPCOMTransactionInfo {Id = (int) upcomTransactions.Id};

                    if (!string.IsNullOrEmpty(upcomTransactions.StockSymbol))
                    {
                        transactionInfo.StockSymbol = upcomTransactions.StockSymbol;
                    }
                    else
                    {
                        continue;
                    }

                    transactionInfo.TradeDate = (upcomTransactions.TradeDate ?? DateTime.Now);

                    if (upcomTransactions.Price != null)
                    {
                        transactionInfo.Price = (double) upcomTransactions.Price/Utils.Common.PRICE_UNIT;

                        StockInfo stockInfo = GetStock(transactionInfo.StockSymbol);

                        if (stockInfo != null)
                        {
                            transactionInfo.Changed = Math.Round(transactionInfo.Price - stockInfo.RefPrice, 2);
                        }
                    }
                    if (upcomTransactions.Vol != null)
                        transactionInfo.Vol = (int) upcomTransactions.Vol;
                    if (upcomTransactions.Val != null)
                        transactionInfo.Val = (long) upcomTransactions.Val;
                    if (upcomTransactions.AccumulatedVol != null)
                        transactionInfo.AccumulatedVol = (int) upcomTransactions.AccumulatedVol;
                    if (upcomTransactions.AccumulatedVal != null)
                        transactionInfo.AccumulatedVal = (long) upcomTransactions.AccumulatedVal;
                    if (upcomTransactions.Highest != null)
                        transactionInfo.Highest = (double) upcomTransactions.Highest/Utils.Common.PRICE_UNIT;
                    if (upcomTransactions.Lowest != null)
                        transactionInfo.Lowest = (double) upcomTransactions.Lowest/Utils.Common.PRICE_UNIT;
                    if (upcomTransactions.Time != null)
                        transactionInfo.Time = (int) upcomTransactions.Time;
                    transactionInfo.Side = upcomTransactions.Side;

                    Db4OManager.Insert(transactionInfo);
                    _upcomTransactionsId = upcomTransactions.Id;
                }
                catch (Exception ex)
                {
                    Utils.Common.Log(ex.ToString());
                }
            }
        }


        public static void UpdateTransactionInfo()
        {
            UpdateTransactionForHose();
            UpdateTransactionForHnx();
            UpdateTransactionForUpcom();
        }

        public static void UpdateTransactionInfo(bool enableIntraDhose, bool enableintraDhnx, bool enbleIntraDupcom)
        {
            if (enableIntraDhose)
            {
                UpdateTransactionForHose();
            }

            if (enableintraDhnx)
            {
                UpdateTransactionForHnx();
            }

            if (enbleIntraDupcom)
            {
                UpdateTransactionForUpcom();
            }
        }

        public void Update(HOSETransferedSessionInfo info)
        {
            //HOSETransferedSessionInfoData.Update(info);
        }

        private static short Advances(short marketId)
        {
            List<StockInfo> listStockInfos;

            try
            {
                if (marketId == (short) MARKET_ID.HASTC)
                    listStockInfos =
                        new List<StockInfo>(ListStocks.Values).FindAll(
                            info =>
                            (info.MarketID == marketId && (info.Last - info.RefPrice) > 0 && info.NMTotalShare > 0));
                else
                    listStockInfos =
                        new List<StockInfo>(ListStocks.Values).FindAll(
                            info =>
                            (info.MarketID == marketId && (info.Last - info.RefPrice) > 0 && info.TotalShare > 0));
                return (short) listStockInfos.Count;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());

                return 0;
            }
        }

        private static short Declines(short marketId)
        {
            List<StockInfo> listStockInfos;

            try
            {
                if (marketId == (short) MARKET_ID.HASTC)
                    listStockInfos =
                        new List<StockInfo>(ListStocks.Values).FindAll(
                            info =>
                            (info.MarketID == marketId && (info.Last - info.RefPrice) < 0 && info.NMTotalShare > 0));
                else
                    listStockInfos =
                        new List<StockInfo>(ListStocks.Values).FindAll(
                            info =>
                            (info.MarketID == marketId && (info.Last - info.RefPrice) < 0 && info.TotalShare > 0));
                return (short) listStockInfos.Count;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());

                return 0;
            }
        }


        private static short NoChange(short marketId)
        {
            List<StockInfo> listStockInfos;

            try
            {
                // Get all stocks that have no transaction or last price == refprice 
                if (marketId == (short) MARKET_ID.HASTC)
                    listStockInfos =
                        new List<StockInfo>(ListStocks.Values).FindAll(
                            info =>
                            (info.MarketID == marketId &&
                             (((info.Last - info.RefPrice) == 0) || (info.NMTotalShare <= 0))));
                else
                    listStockInfos =
                        new List<StockInfo>(ListStocks.Values).FindAll(
                            info =>
                            (info.MarketID == marketId && (((info.Last - info.RefPrice) == 0) || (info.TotalShare <= 0))));

                return (short) listStockInfos.Count;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());

                return 0;
            }
        }

        private static short NoChangeVn30()
        {
            List<StockInfo> listStockInfos;

            try
            {
                // Get all stocks that have no transaction or last price == refprice 
                const short marketId = (short) MARKET_ID.HOSE;
                listStockInfos =
                    new List<StockInfo>(ListStocks.Values).FindAll(
                        info =>
                        (info.MarketID == marketId && info.IsVn30 &&
                         (((info.Last - info.RefPrice) == 0) || (info.TotalShare <= 0))));

                return (short) listStockInfos.Count;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());

                return 0;
            }
        }

        private static short GetNoChangeVn30()
        {
            List<StockInfo> listStockInfos;

            try
            {
                // Get all stocks that have no transaction or last price == refprice 
                const short marketId = (short) MARKET_ID.HOSE;
                listStockInfos =
                    new List<StockInfo>(ListStocks.Values).FindAll(
                        info => (info.MarketID == marketId && info.IsVn30 && (info.TotalShare <= 0)));

                return (short) listStockInfos.Count;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());

                return 0;
            }
        }

        /// <summary>
        ///   Get Nochange stocks
        /// </summary>
        /// <param name = "marketId">Market Id</param>
        /// <returns></returns>
        private static short GetNoChange(short marketId)
        {
            List<StockInfo> listStockInfos;

            try
            {
                if (marketId == (short) MARKET_ID.HASTC)
                    listStockInfos =
                        new List<StockInfo>(ListStocks.Values).FindAll(
                            info => (info.MarketID == marketId && info.NMTotalShare <= 0));
                else
                    listStockInfos =
                        new List<StockInfo>(ListStocks.Values).FindAll(
                            info => (info.MarketID == marketId && info.TotalShare <= 0));

                return (short) listStockInfos.Count;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());

                return 0;
            }
        }


        /// <summary>
        ///   Get ceiling stocks
        /// </summary>
        /// <param name = "marketId">Market Id</param>
        /// <returns></returns>
        private static short GetCeiling(short marketId)
        {
            List<StockInfo> listStockInfos;

            try
            {
                listStockInfos =
                    new List<StockInfo>(ListStocks.Values).FindAll(
                        info => (info.MarketID == marketId && (info.Last - info.Ceiling) == 0 && info.TotalShare > 0));

                return (short) listStockInfos.Count;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());

                return 0;
            }
        }

        /// <summary>
        ///   Get floor stocks
        /// </summary>
        /// <param name = "marketId">Market Id</param>
        /// <returns></returns>
        private static short GetFloor(short marketId)
        {
            List<StockInfo> listStockInfos;

            try
            {
                listStockInfos =
                    new List<StockInfo>(ListStocks.Values).FindAll(
                        info => (info.MarketID == marketId && (info.Last - info.Floor) == 0 && info.TotalShare > 0));

                return (short) listStockInfos.Count;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());

                return 0;
            }
        }

        /// <summary>
        ///   Get ceiling stocks
        /// </summary>
        /// <returns></returns>
        private static short GetCeilingVn30()
        {
            List<StockInfo> listStockInfos;

            try
            {
                const short marketId = (short) MARKET_ID.HOSE;
                listStockInfos =
                    new List<StockInfo>(ListStocks.Values).FindAll(
                        info =>
                        (info.MarketID == marketId && info.IsVn30 && (info.Last - info.Ceiling) == 0 &&
                         info.TotalShare > 0));

                return (short) listStockInfos.Count;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());

                return 0;
            }
        }

        /// <summary>
        ///   Get floor stocks
        /// </summary>
        /// <returns></returns>
        private static short GetFloorVn30()
        {
            List<StockInfo> listStockInfos;

            try
            {
                const short marketId = (short) MARKET_ID.HOSE;
                listStockInfos =
                    new List<StockInfo>(ListStocks.Values).FindAll(
                        info =>
                        (info.MarketID == marketId && info.IsVn30 && (info.Last - info.Floor) == 0 &&
                         info.TotalShare > 0));

                return (short) listStockInfos.Count;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());

                return 0;
            }
        }

        public static List<HOSETransactionInfo> SelectHoseTransactionInfo(string stockSymbol)
        {
            return Db4OManager.GetHoseTransactionInfoListBySymbol(stockSymbol);
        }

        public static List<HOSETransactionInfo> SelectHoseTransactionInfo(string stockSymbol, int id)
        {
            return Db4OManager.GetHoseTransactionInfoListBySymbol(stockSymbol, id);
        }

        public static List<HNXTransactionInfo> SelectHnxTransactionInfo(string stockSymbol)
        {
            return Db4OManager.GetHnxTransactionInfoListBySymbol(stockSymbol);
        }

        public static List<HNXTransactionInfo> SelectHnxTransactionInfo(string stockSymbol, int id)
        {
            return Db4OManager.GetHnxTransactionInfoListBySymbol(stockSymbol, id);
        }

        public static List<UPCOMTransactionInfo> SelectUpcomTransactionInfo(string stockSymbol)
        {
            return Db4OManager.GetUpcomTransactionInfoListBySymbol(stockSymbol);
        }

        public static List<UPCOMTransactionInfo> SelectUpcomTransactionInfo(string stockSymbol, int id)
        {
            return Db4OManager.GetUpcomTransactionInfoListBySymbol(stockSymbol, id);
        }

        public static List<MainMatchedPricesInfo> GetHoseMainMatchedPrices(string stockSymbol)
        {
            return Db4OManager.GetHoseMainMatchedPrices(stockSymbol);
        }

        public static List<MainMatchedPricesInfo> GetHnxMainMatchedPrices(string stockSymbol)
        {
            return Db4OManager.GetHnxMainMatchedPrices(stockSymbol);
        }

        public static List<MainMatchedPricesInfo> GetUpcomMainMatchedPrices(string stockSymbol)
        {
            return Db4OManager.GetUpcomMainMatchedPrices(stockSymbol);
        }

        private static IEnumerable<TickerInfo> GetTickerListBySymbolAndId(string stockSymbol, int id)
        {
            StockInfo stockInfo = GetStock(stockSymbol);

            List<TickerInfo> tickerInfos = null;
            if (stockInfo != null)
            {
                switch (stockInfo.MarketID)
                {
                    case (short) CommonEnums.MARKET_ID.HOSE:
                        tickerInfos = Db4OManager.GetTickerListBySymbolAndId(stockSymbol, id,
                                                                             typeof (HOSETransactionInfo));
                        break;
                    case (short) CommonEnums.MARKET_ID.HNX:
                        tickerInfos = Db4OManager.GetTickerListBySymbolAndId(stockSymbol, id,
                                                                             typeof (HNXTransactionInfo));
                        break;
                    case (short) CommonEnums.MARKET_ID.UPCoM:
                        tickerInfos = Db4OManager.GetTickerListBySymbolAndId(stockSymbol, id,
                                                                             typeof (UPCOMTransactionInfo));
                        break;
                    default:
                        return null;
                }
                if (tickerInfos != null)
                {
                    foreach (TickerInfo tickerInfo in tickerInfos)
                    {
                        tickerInfo.RefPrice = stockInfo.RefPrice;
                        tickerInfo.Ceiling = stockInfo.Ceiling;
                        tickerInfo.Floor = stockInfo.Floor;
                    }
                }
            }

            return tickerInfos;
        }

        /// <summary>
        ///   Get All Ticker Infos
        /// </summary>
        /// <param name = "marketId">current Max Id</param>
        /// <param name = "id">current Max Id</param>
        /// <returns>List<TickerInfo /></returns>
        public static List<TickerInfo> GetTickerListById(int marketId, int id)
        {
            switch (marketId)
            {
                case (short) CommonEnums.MARKET_ID.HOSE:
                    return Db4OManager.GetTickerListById(id, typeof (HOSETransactionInfo));
                case (short) CommonEnums.MARKET_ID.HNX:
                    return Db4OManager.GetTickerListById(id, typeof (HNXTransactionInfo));
                case (short) CommonEnums.MARKET_ID.UPCoM:
                    return Db4OManager.GetTickerListById(id, typeof (UPCOMTransactionInfo));
                default:
                    return null;
            }
        }

        /// <summary>
        ///   Get Ticker List By List Of Codes
        /// </summary>
        /// <param name = "listCodes">listCodes with format: Code1:id1,Code2:id2,...</param>
        /// <returns>List<TickerInfo/></returns>
        public static List<TickerInfo> GetTickerListByListCode(string listCodes)
        {
            string[] codes = listCodes.Split(',');

            var listTickerInfos = new List<TickerInfo>();

            foreach (string code in codes)
            {
                string[] codeInfo = code.Split(':');

                IEnumerable<TickerInfo> ticketList = GetTickerListBySymbolAndId(codeInfo[0], Int32.Parse(codeInfo[1]));

                if (ticketList != null)
                {
                    listTickerInfos.InsertRange(listTickerInfos.Count == 0 ? 0 : listTickerInfos.Count, ticketList);
                }
            }

            return listTickerInfos;
        }

        public static List<StockInfo> Select_List_StockInfos(string listCodes)
        {
            string[] symbols = listCodes.Split(',');

            return symbols.Select(symbol => GetStock(symbol.Trim())).Where(stockInfo => stockInfo != null).ToList();
        }

        public static List<StockInfo> Select_List_StockInfos(string listCodes, string languageId)
        {
            string[] symbols = listCodes.Split(',');

            return symbols.Select(symbol => GetStock(symbol.Trim(), languageId)).Where(stockInfo => stockInfo != null).ToList();
        }

        public static bool HaveStocksForToday(short marketId)
        {
            DateTime currentDate = DateTime.Now.Date;

            var listStockInfos = new List<StockInfo>(ListStocks.Values);
            List<StockInfo> listRetStockInfos = listStockInfos.FindAll(info => (info.MarketID == marketId && info.TradeDate.Date == currentDate));

            return (listRetStockInfos.Count > 0);
        }

        public static List<StockInfo> Select_All_StockInfo(short marketId)
        {
            List<StockInfo> listStockInfos;
            List<StockInfo> listRetStockInfos = null;

            try
            {
                listStockInfos = new List<StockInfo>(ListStocks.Values);
                listRetStockInfos = listStockInfos.FindAll(info => (info.MarketID == marketId));
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return listRetStockInfos;
        }


        //public static List<CompanyInfo> Select_All_CompanyInfo()
        //{
        //    return new List<CompanyInfo>(ListCompanies.Values);
        //}

        public static List<NewestWorkingDatesInfo> Select_All_NewestWorkingDates()
        {
            List<NewestWorkingDatesInfo> listNewestWorkingDates = null;

            try
            {
                TList<NearestWorkingDates> listNearestWorkingDatesInfos = NearestWorkingDatesService.GetAll();

                listNewestWorkingDates =
                    listNearestWorkingDatesInfos.Select(nearestWorkingDatesInfo => new NewestWorkingDatesInfo
                                                                                       {
                                                                                           MarketId =
                                                                                               (short)
                                                                                               nearestWorkingDatesInfo.
                                                                                                   MarketId,
                                                                                           T =
                                                                                               nearestWorkingDatesInfo.T ??
                                                                                               DateTime.Now,
                                                                                           T1 =
                                                                                               nearestWorkingDatesInfo.
                                                                                                   T1 ?? DateTime.Now,
                                                                                           T2 =
                                                                                               nearestWorkingDatesInfo.
                                                                                                   T2 ?? DateTime.Now,
                                                                                           T3 =
                                                                                               nearestWorkingDatesInfo.
                                                                                                   T3 ?? DateTime.Now
                                                                                       }).ToList();
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return listNewestWorkingDates;
        }

        public static List<CompanyInfo> GetListCompanyInfoByLanguageId(string languageId, short marketId)
        {
            List<CompanyInfo> listObj;
            List<CompanyInfo> listResult;
            try
            {
                switch (languageId)
                {
                    case Constants.ENGLISH_LANGUAGE_ID:
                        listObj = new List<CompanyInfo>(ListLangCompanies[Constants.ENGLISH_LANGUAGE_ID].Values);
                        listResult = listObj.FindAll(p => p.MarketId == marketId || marketId==0);
                        break;
                    default:
                        listObj = new List<CompanyInfo>(ListLangCompanies[Constants.VIETNAMESE_LANGUAGE_ID].Values);
                        listResult = listObj.FindAll(p => p.MarketId == marketId || marketId == 0);
                        break;
                }
            }
            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
                listResult = new List<CompanyInfo>();
            }

            return listResult;
        }

        //public static List<CompanyInfo> Select_All_CompanyInfo(short marketId)
        //{
        //    List<CompanyInfo> listRetCompanyInfos = null;
        //    List<CompanyInfo> listCompanyInfos;

        //    try
        //    {
        //        listCompanyInfos = new List<CompanyInfo>(ListCompanies.Values);

        //        if (marketId == 0)
        //        {
        //            return listCompanyInfos;
        //        }

        //        listRetCompanyInfos =
        //            listCompanyInfos.FindAll(info => (info.MarketId == marketId));
        //    }

        //    catch (Exception ex)
        //    {
        //        Utils.Common.Log(ex.ToString());
        //    }

        //    return listRetCompanyInfos;
        //}

        public static List<HOSEPutAdInfo> SelectAllHosePutAdInfo()
        {
            return new List<HOSEPutAdInfo>();
        }

        public static List<HOSEPutExecInfo> SelectAllHosePutExecInfo()
        {
            return new List<HOSEPutExecInfo>();
        }

        public static IndexInfo GetHnx30()
        {
            if (ListHnxIndex30.Count > 0 && ListHnxIndex30.ContainsKey("HNX30"))
            {
                var indexInfos = new Dictionary<int, IndexInfo>(ListHnxIndex30["HNX30"]);
                if (indexInfos.Count > 0)
                {
                    int max = indexInfos.Max(p => p.Value.Time);
                    List<IndexInfo> listinfo = new List<IndexInfo>(indexInfos.Values).FindAll(p => p.Time == max);
                    if (listinfo.Count > 0)
                    {
                        return listinfo[0];
                    }
                }
            }
            return null;
        }

        private static DateTime GetMaxDateHnx30()
        {
            const string whereClause = "TradeDate = (SELECT MAX(TradeDate) FROM dbo.IndexInfo)";
            int outCount;
            TList<RTStockData.Entities.IndexInfo> listIndex = HnxIndex30Service.GetPaged(whereClause, string.Empty, 0, int.MaxValue, out outCount);
            
            if(listIndex!=null && outCount>0)
            {
                foreach (RTStockData.Entities.IndexInfo indexInfo in listIndex)
                {
                    if(indexInfo.TradeDate!=null) return indexInfo.TradeDate.Value;
                }
            }
            return DateTime.MinValue;
        }

        public static void UpdateHnxIndex30()
        {
            const string whereClause = "[Time] = (Select MAX([Time]) from [IndexInfo] where IndexCode like 'HNX30')";
            int outCount;
            TList<RTStockData.Entities.IndexInfo> listHnxIndex30 = HnxIndex30Service.GetPaged(whereClause, string.Empty,
                                                                                              0, int.MaxValue,
                                                                                              out outCount);
            if (listHnxIndex30 != null)
            {
                MarketInfo currentInfo = GetCurrentMarketInfo((int)CommonEnums.MARKET_ID.HNX, true);
                char marketStatus = MarketStatus(currentInfo, (int)CommonEnums.MARKET_ID.HNX);
                foreach (RTStockData.Entities.IndexInfo indexVn30Info in listHnxIndex30)
                {
                    Thread.Sleep(1);
                    var indexInfo = new IndexInfo
                                        {
                                            ID = indexVn30Info.Id,
                                            Index = double.Parse(((indexVn30Info.CurrentIndex ?? 0)).ToString()),
                                            Up = 0,
                                            Down = 0,
                                            NoChange = 0,
                                            TotalShares = Convert.ToDouble(indexVn30Info.TotalQtty ?? 0),
                                            TotalValues = Convert.ToDouble(indexVn30Info.TotalValue ?? 0),
                                            TradeDate = (indexVn30Info.TradeDate ?? DateTime.Now),
                                            Time = (int) (indexVn30Info.Time ?? 0),
                                            CountCeiling = 0,
                                            CountFloor = 0,
                                            CountNoChange = 0,
                                            IndexCode = indexVn30Info.IndexCode.Trim().ToUpper(),
                                            IndexId = Convert.ToInt64(indexVn30Info.IndexId ?? 0),
                                            PerChange = Convert.ToDouble(indexVn30Info.PctIndex ?? 0),
                                            Change = Convert.ToDouble(indexVn30Info.ChgIndex ?? 0)
                                        };
                    if (marketStatus != (char)CommonEnums.MARKET_STATUS.READY)
                    {
                        indexInfo.Up = GetAdvanceHnx30();
                        indexInfo.Down = GetDeclinesHnx30();
                        indexInfo.NoChange = GetNoChangeHnx30();
                        indexInfo.CountCeiling = GetCountCeilingHnx30();
                        indexInfo.CountFloor = GetCountFloorHnx30();
                        indexInfo.CountNoChange = GetCountNochangeHnx30();
                    }
                    if (ListHnxIndex30.ContainsKey(indexInfo.IndexCode))
                    {
                        Dictionary<int, IndexInfo> listIndexItem = ListHnxIndex30[indexInfo.IndexCode];
                        if (listIndexItem.ContainsKey(indexInfo.Time))
                            listIndexItem[indexInfo.Time] = indexInfo;
                        else
                            listIndexItem.Add(indexInfo.Time, indexInfo);
                        ListHnxIndex30[indexInfo.IndexCode] = listIndexItem;
                    }
                    else
                    {
                        var listIndexItem = new Dictionary<int, IndexInfo> {{indexInfo.Time, indexInfo}};
                        ListHnxIndex30.Add(indexInfo.IndexCode, listIndexItem);
                    }
                }
            }
        }

        public static short GetNoChangeHnx30()
        {
            List<StockInfo> listStockInfos;

            try
            {
                listStockInfos =
                    new List<StockInfo>(ListStocks.Values).FindAll(
                        info =>
                        (info.MarketID == (short)MARKET_ID.HASTC && ListStockHnx30Str.IndexOf(info.StockSymbol.Trim()) >= 0 &&
                         (((info.Last - info.RefPrice) == 0) || (info.NMTotalShare <= 0))));

                return (short) listStockInfos.Count;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
                return 0;
            }
        }

        public static short GetDeclinesHnx30()
        {
            List<StockInfo> listStockInfos;

            try
            {
                    listStockInfos =
                        new List<StockInfo>(ListStocks.Values).FindAll(
                            info =>
                            (info.MarketID == (short)MARKET_ID.HASTC && ListStockHnx30Str.IndexOf(info.StockSymbol.Trim()) >= 0 && (info.Last - info.RefPrice) < 0 && info.NMTotalShare > 0));
                return (short)listStockInfos.Count;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());

                return 0;
            }
        }

        public static short GetAdvanceHnx30()
        {
            List<StockInfo> listStockInfos;

            try
            {
                listStockInfos =
                    new List<StockInfo>(ListStocks.Values).FindAll(
                        info =>
                        (info.MarketID == (short) MARKET_ID.HASTC && ListStockHnx30Str.IndexOf(info.StockSymbol.Trim())>=0 && (info.Last - info.RefPrice) > 0 &&
                         info.NMTotalShare > 0));
                return (short) listStockInfos.Count;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
                return 0;
            }
        }

        public static short GetCountCeilingHnx30()
        {
            List<StockInfo> listStockInfos;

            try
            {
                listStockInfos =
                    new List<StockInfo>(ListStocks.Values).FindAll(
                        info => (info.MarketID == (short)MARKET_ID.HASTC && ListStockHnx30Str.IndexOf(info.StockSymbol.Trim()) >= 0 && (info.Last - info.Ceiling) == 0 && info.TotalShare > 0));

                return (short)listStockInfos.Count;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());

                return 0;
            }
        }

        public static short GetCountFloorHnx30()
        {
            List<StockInfo> listStockInfos;

            try
            {
                listStockInfos =
                    new List<StockInfo>(ListStocks.Values).FindAll(
                        info => (info.MarketID == (short)MARKET_ID.HASTC && ListStockHnx30Str.IndexOf(info.StockSymbol.Trim()) >= 0 && (info.Last - info.Floor) == 0 && info.TotalShare > 0));

                return (short)listStockInfos.Count;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());

                return 0;
            }
        }

        public static short GetCountNochangeHnx30()
        {
            List<StockInfo> listStockInfos;

            try
            {
                listStockInfos =
                    new List<StockInfo>(ListStocks.Values).FindAll(
                        info =>
                        (info.MarketID == (short) MARKET_ID.HASTC &&
                         ListStockHnx30Str.IndexOf(info.StockSymbol.Trim()) >= 0 && info.NMTotalShare <= 0));

                return (short) listStockInfos.Count;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());

                return 0;
            }
        }

        public static void UpdateHnxIndex30History()
        {
            const string whereClause =
                "TradeDate >= (SELECT Top 1 TradeDate FROM dbo.IndexInfoHistory WHERE CONVERT(VARCHAR(10),TradeDate,120) = CONVERT(VARCHAR(10),GETDATE()-15,120))";
            int outCount;
            try
            {
                TList<IndexInfoHistory> indexInfoHistories = HnxIndex30HistoryService.GetPaged(whereClause, string.Empty, 0,
                                                                                           int.MaxValue, out outCount);
                if (indexInfoHistories != null)
                    foreach (IndexInfoHistory indexInfoHistory in indexInfoHistories)
                    {
                        Thread.Sleep(1);
                        if (ListHnxIndex30History.ContainsKey(indexInfoHistory.IndexCode.Trim().ToUpper()))
                        {

                            if (indexInfoHistory.TradeDate != null)
                            {
                                Dictionary<DateTime, IndexInfoHistory> listIndexItem =
                                    ListHnxIndex30History[indexInfoHistory.IndexCode.Trim().ToUpper()];
                                if (listIndexItem.ContainsKey(indexInfoHistory.TradeDate.Value.Date))
                                    listIndexItem[indexInfoHistory.TradeDate.Value.Date] = indexInfoHistory;
                                else
                                    listIndexItem.Add(indexInfoHistory.TradeDate.Value.Date, indexInfoHistory);
                                ListHnxIndex30History[indexInfoHistory.IndexCode.Trim().ToUpper()] = listIndexItem;
                            }

                        }
                        else
                        {
                            if (indexInfoHistory.TradeDate != null)
                            {
                                var listIndexItem = new Dictionary<DateTime, IndexInfoHistory> { { indexInfoHistory.TradeDate.Value.Date, indexInfoHistory } };
                                ListHnxIndex30History.Add(indexInfoHistory.IndexCode.Trim().ToUpper(), listIndexItem);
                            }
                        }
                    }
            }catch(Exception ex)
            {
                LogHandler.Log(ex.ToString(), "UpdateHnxIndex30History", TraceEventType.Error);
            }
            
        }

        public static void UpdateBasketInfo()
        {
            const string whereClause = "IndexCode LIKE 'HNX30'";
            int outCount;
            TList<BasketInfo> basketInfos = BasketInfoServices.GetPaged(whereClause,string.Empty,0,int.MaxValue,out outCount);
            if (basketInfos != null)
                foreach (var basketInfo in basketInfos)
                {
                    if (ListBasketInfo.ContainsKey(basketInfo.IndexCode.Trim()))
                    {
                        Dictionary<string, BasketInfo> listItem = ListBasketInfo[basketInfo.IndexCode.Trim()];
                        if (listItem.ContainsKey(basketInfo.StockCode))
                        {
                            listItem[basketInfo.StockCode] = basketInfo;
                        }else 
                            listItem.Add(basketInfo.StockCode,basketInfo);
                    }
                    else
                    {
                        var listItem =
                            new Dictionary<string, BasketInfo> {{basketInfo.StockCode, basketInfo}};
                        ListBasketInfo.Add(basketInfo.IndexCode.Trim(),listItem);
                    }
                    if(basketInfo.IndexCode.Trim().ToUpper().Equals("HNX30") && ListStockHnx30Str.IndexOf(basketInfo.StockCode.Trim())<0)
                    {
                        ListStockHnx30Str += (basketInfo.StockCode + ",");
                    }
                }
        }

        public static void UpdateIndexs()
        {
            const string whereClause = "TRADED_DATE > GETDATE() - 10";
            int outCount;
            var listIndexs = IndexsService.GetPaged(whereClause, string.Empty, 0, int.MaxValue, out outCount);
            if (outCount <= ListIndexs.Count) return;
            try
            {
                foreach (Indexs indexs in listIndexs)
                {
                    if (ListIndexs.ContainsKey(indexs.VnindexId))
                    {
                        ListIndexs[indexs.VnindexId] = indexs;
                    }
                    else
                    {
                        ListIndexs.Add(indexs.VnindexId, indexs);
                    }
                }
            }
            catch (Exception exception)
            {
                Utils.Common.Log(exception.ToString());
            }
        }

        public static double Hnx30IndexLastDay()
        {
            if (!ListHnxIndex30History.ContainsKey("HNX30")) return 0;
           List<IndexInfoHistory> listIndex = new List<IndexInfoHistory>(ListHnxIndex30History["HNX30"].Values);
           if (listIndex.Count == 0) return 0;
            var maxDate = listIndex.Max(p => p.TradeDate.Value);
            IndexInfoHistory item = listIndex.Single(p => p.TradeDate.Value.Date == maxDate.Date);
            return double.Parse(item.CurrentIndex.Value.ToString());
        }

        public static double IndexLastDay(int marketId)
        {
            try
            {
                if (ListIndexs.Count == 0) return 0;
                var listIndexs = new List<Indexs>(ListIndexs.Values);
                DateTime? maxDate = listIndexs.Max(p => p.TradedDate);
                Indexs indexs = null;
                if (marketId == (short) MARKET_ID.HASTC)
                {
                    indexs = listIndexs.Single(
                        p => p.TradedDate.Value.Date == maxDate.Value.Date && p.MarketId.Trim().ToUpper().Equals("HN"));
                }
                else if (marketId == (short) MARKET_ID.HOSE)
                {
                    indexs = listIndexs.Single(
                        p => p.TradedDate.Value.Date == maxDate.Value.Date && p.MarketId.Trim().ToUpper().Equals("HCM"));
                }
                else if (marketId == (short) MARKET_ID.UPCoM)
                {

                    indexs = listIndexs.Single(
                        p =>
                        p.TradedDate.Value.Date == maxDate.Value.Date && p.MarketId.Trim().ToUpper().Equals("UPCOM"));
                }
                if (indexs == null) return 0;
                return indexs.Close.Value;
            }
            catch (Exception exception)
            {
                Utils.Common.Log(exception.ToString());
                return 0;
            }
            
        }
    }
}