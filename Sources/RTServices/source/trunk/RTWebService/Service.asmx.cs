﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using ETradeCommon;
using ETradeCommon.Enums;
using RTDataServices.Entities;
using RTDdataServices.Entities;
using RTStockData.Entities;
using RTWebService.Updater;
using CompanyInfo = RTDataServices.Entities.CompanyInfo;
using IndexInfo = RTDataServices.Entities.IndexInfo;

namespace RTWebService
{
    /// <summary>
    /// Summary description for Service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class Service : WebService
    {
        private static readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();
        public static List<CommonEnums.MARKET_STATUS> UdMarketStatus = new List<CommonEnums.MARKET_STATUS>();
        //RealTimeStockUpdater updater = new RealTimeStockUpdater();

        static Service()
        {
            UdMarketStatus.Add(CommonEnums.MARKET_STATUS.UNVAILABLE);
            UdMarketStatus.Add(CommonEnums.MARKET_STATUS.UNVAILABLE);
            UdMarketStatus.Add(CommonEnums.MARKET_STATUS.UNVAILABLE);
        }

        [WebMethod(Description = "Get Index_VN30")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetIndexVN30()
        {
            var retObject = new ResultObject<List<IndexInfo>> {RetCode = CommonEnums.RET_CODE.SUCCESS, Result = null};
            var listIndex = new List<IndexInfo>();
            IndexInfo indexInfo = DbServices.GetIndexVn30();
            if (indexInfo == null)
            {
                retObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
            }
            else
            {
                listIndex.Add(indexInfo);
                retObject.Result = listIndex;
            }
            return Serializer.Serialize(retObject);
        }

        [WebMethod(Description = "Get Market Info")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetMarketInfo(int marketId)
        {
            var retObject = new ResultObject<List<MarketInfo>> {RetCode = CommonEnums.RET_CODE.SUCCESS, Result = null};

            var listMarketInfos = new List<MarketInfo>();

            try
            {
                if (marketId == 0)
                {
                    MarketInfo hoseMarketInfo =
                        (DbServices.GetCurrentMarketInfo((int) MARKET_ID.HOSE, true) ??
                         DbServices.GetCurrentMarketInfo((int) MARKET_ID.HOSE, false));
                    MarketInfo hnxMarketInfo =
                        (DbServices.GetCurrentMarketInfo((int) MARKET_ID.HASTC, true) ??
                         DbServices.GetCurrentMarketInfo((int) MARKET_ID.HASTC, false));
                    MarketInfo upcomMarketInfo =
                        (DbServices.GetCurrentMarketInfo((int) MARKET_ID.UPCoM, true) ??
                         DbServices.GetCurrentMarketInfo((int) MARKET_ID.UPCoM, false));

                    if (hoseMarketInfo == null)
                    {
                        hoseMarketInfo = new MarketInfo
                                             {
                                                 MarketId = (int) MARKET_ID.HOSE,
                                                 Status = DbServices.MarketStatus(null, marketId),
                                                 SetIndex = DbServices.IndexLastDay(marketId)
                                             };
                    }
                    IndexInfo indexInfo = DbServices.GetIndexVn30();
                    IndexVn30History indexVn30History = DbServices.GetIndexVn30History();
                    if (indexVn30History != null)
                    {
                        if (indexInfo == null)
                        {
                            indexInfo = new IndexInfo();
                            if (indexVn30History.Index.HasValue)
                                indexInfo.Index = (indexVn30History.Index.Value*1.0)/100;
                            indexInfo.Change = 0;
                            indexInfo.PerChange = 0;
                        }
                        else
                        {
                            if (indexVn30History.Change != null)
                                indexInfo.Change = double.Parse(indexVn30History.Change.Value.ToString())/100;
                            if (indexVn30History.PerChange.HasValue)
                                indexInfo.PerChange = indexVn30History.PerChange.Value;
                        }
                    }
                    hoseMarketInfo.VN30 = indexInfo;

                    if (Utils.Common.RunMode == "TEST")
                    {
                        hoseMarketInfo.Status = (char) UdMarketStatus[0];
                    }

                    listMarketInfos.Add(hoseMarketInfo);

                    if (hnxMarketInfo == null)
                    {
                        hnxMarketInfo = new MarketInfo
                                            {
                                                MarketId = (int) MARKET_ID.HASTC,
                                                Status = DbServices.MarketStatus(null, marketId),
                                                SetIndex = DbServices.IndexLastDay(marketId)
                                            };
                    }
                    IndexInfo hnx30 = DbServices.GetHnx30() ?? new IndexInfo();
                    hnxMarketInfo.VN30 = hnx30;

                    if (Utils.Common.RunMode == "TEST")
                    {
                        hnxMarketInfo.Status = (char) UdMarketStatus[1];
                    }

                    listMarketInfos.Add(hnxMarketInfo);

                    if (upcomMarketInfo == null)
                    {
                        upcomMarketInfo = new MarketInfo
                                              {
                                                  MarketId = (int) MARKET_ID.UPCoM,
                                                  Status = DbServices.MarketStatus(null, marketId),
                                                  SetIndex = DbServices.IndexLastDay(marketId)
                                              };
                    }

                    if (Utils.Common.RunMode == "TEST")
                    {
                        upcomMarketInfo.Status = (char) UdMarketStatus[1];
                    }

                    listMarketInfos.Add(upcomMarketInfo);
                }
                else
                {
                    MarketInfo marInfo = DbServices.GetCurrentMarketInfo(marketId, true) ??
                                         DbServices.GetCurrentMarketInfo(marketId, false);

                    if (marInfo == null)
                    {
                        marInfo = new MarketInfo
                                      {
                                          MarketId = (short) marketId,
                                          Status = DbServices.MarketStatus(null, marketId),
                                          SetIndex = DbServices.IndexLastDay(marketId)
                                      };
                    }
                    if (marketId == (short) CommonEnums.MARKET_ID.HOSE)
                    {
                        IndexInfo indexInfo = DbServices.GetIndexVn30();
                        IndexVn30History indexVn30History = DbServices.GetIndexVn30History();
                        if (indexVn30History.Change != null) indexInfo.Change = indexVn30History.Change.Value;
                        if (indexVn30History.PerChange.HasValue) indexInfo.PerChange = indexVn30History.PerChange.Value;
                        marInfo.VN30 = indexInfo;
                    }
                    else if(marketId == (short)CommonEnums.MARKET_ID.HNX)
                    {
                        IndexInfo hnx30 = DbServices.GetHnx30() ?? new IndexInfo();
                        marInfo.VN30 = hnx30;
                    }

                    if (Utils.Common.RunMode == "TEST")
                    {
                        marInfo.Status = (char) UdMarketStatus[marketId - 1];
                    }

                    listMarketInfos.Add(marInfo);
                }

                retObject.Result = listMarketInfos;
            }
            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }
            return Serializer.Serialize(retObject);
        }


        [WebMethod(Description = "Get Market Status")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string MarketStatus(int marketID)
        {
            var retObject = new ResultObject<char>
                                {
                                    RetCode = CommonEnums.RET_CODE.SUCCESS,
                                    Result = (char) CommonEnums.MARKET_STATUS.UNVAILABLE
                                };

            try
            {
                if (Utils.Common.RunMode == "TEST")
                {
                    retObject.Result = (char) UdMarketStatus[marketID - 1];

                    return Serializer.Serialize(retObject);
                }

                MarketInfo marInfo = DbServices.GetCurrentMarketInfo(marketID, false);

                retObject.Result = DbServices.MarketStatus(marInfo, marketID);
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return Serializer.Serialize(retObject);
        }

        [WebMethod(Description = "Get all Market Status")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string AllMarketStatus()
        {
            var retObject = new ResultObject<char[]>
                                {
                                    RetCode = CommonEnums.RET_CODE.SUCCESS,
                                    Result = new[]
                                                 {
                                                     (char) CommonEnums.MARKET_STATUS.UNVAILABLE,
                                                     (char) CommonEnums.MARKET_STATUS.UNVAILABLE,
                                                     (char) CommonEnums.MARKET_STATUS.UNVAILABLE
                                                 }
                                };

            try
            {
                var status = new char[3];
                if (Utils.Common.RunMode == "TEST")
                {
                    status[0] = (char) UdMarketStatus[(int) MARKET_ID.HOSE - 1];
                    status[1] = (char) UdMarketStatus[(int) MARKET_ID.HASTC - 1];
                    status[2] = (char) UdMarketStatus[(int) MARKET_ID.UPCoM - 1];
                    retObject.Result = status;
                    return Serializer.Serialize(retObject);
                }

                MarketInfo marInfo = DbServices.GetCurrentMarketInfo((int) MARKET_ID.HOSE, true);

                status[0] = DbServices.MarketStatus(marInfo, (int) MARKET_ID.HOSE);

                marInfo = DbServices.GetCurrentMarketInfo((int) MARKET_ID.HASTC, true);

                status[1] = DbServices.MarketStatus(marInfo, (int) MARKET_ID.HASTC);

                marInfo = DbServices.GetCurrentMarketInfo((int) MARKET_ID.UPCoM, true);

                status[2] = DbServices.MarketStatus(marInfo, (int) MARKET_ID.UPCoM);

                retObject.Result = status;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return Serializer.Serialize(retObject);
        }

        [WebMethod(Description = "Get all Market info and Status")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string AllMarketInfoAndStatus()
        {
            var retObject = new ResultObject<List<MarketInfo>> {RetCode = CommonEnums.RET_CODE.SUCCESS, Result = null};

            var listMarketInfos = new List<MarketInfo>();

            try
            {
                MarketInfo hoseMarketInfo =
                    DbServices.GetCurrentMarketInfo((int) MARKET_ID.HOSE, false);
                MarketInfo hnxMarketInfo =
                    DbServices.GetCurrentMarketInfo((int) MARKET_ID.HASTC, false);
                MarketInfo upcomMarketInfo =
                    DbServices.GetCurrentMarketInfo((int) MARKET_ID.UPCoM, false);

                if (hoseMarketInfo == null)
                {
                    hoseMarketInfo = new MarketInfo
                                         {
                                             MarketId = (int) MARKET_ID.HOSE,
                                             Status = DbServices.MarketStatus(null, (int) MARKET_ID.HOSE),
                                             SetIndex = DbServices.IndexLastDay((int)MARKET_ID.HOSE)
                                         };
                }
                else
                {
                    hoseMarketInfo.Status = DbServices.MarketStatus(hoseMarketInfo,
                                                                    (int) MARKET_ID.HOSE);
                }
                IndexInfo vn30 = DbServices.GetIndexVn30();
                IndexVn30History indexVn30History = DbServices.GetIndexVn30History();
                if (indexVn30History != null)
                {
                    if (vn30 == null)
                    {
                        vn30 = new IndexInfo();
                        if (indexVn30History.Index.HasValue)
                            vn30.Index = (indexVn30History.Index.Value*1.0)/100;
                        vn30.Change = 0;
                        vn30.PerChange = 0;
                    }
                    else
                    {
                        if (indexVn30History.Change != null)
                            vn30.Change = double.Parse(indexVn30History.Change.Value.ToString())/100;
                        if (indexVn30History.PerChange.HasValue) vn30.PerChange = indexVn30History.PerChange.Value;
                    }
                }
                hoseMarketInfo.VN30 = vn30;
                RTDataServices.Entities.IndexInfo hnx30 = DbServices.GetHnx30();
                if (hnxMarketInfo == null)
                {
                    hnxMarketInfo = new MarketInfo
                                        {
                                            MarketId = (int) MARKET_ID.HASTC,
                                            Status = DbServices.MarketStatus(null, (int) MARKET_ID.HASTC),
                                            SetIndex = DbServices.IndexLastDay((int)MARKET_ID.HASTC)
                                        };
                }
                else
                {
                    hnxMarketInfo.Status = DbServices.MarketStatus(hnxMarketInfo,
                                                                   (int) MARKET_ID.HASTC);
                }
                if (hnx30 == null)
                {
                    hnx30 = new IndexInfo();
                    hnx30.Index = DbServices.Hnx30IndexLastDay();
                }

                hnxMarketInfo.VN30 = hnx30;

                if (upcomMarketInfo == null)
                {
                    upcomMarketInfo = new MarketInfo();

                    upcomMarketInfo.MarketId = (int) MARKET_ID.UPCoM;
                    upcomMarketInfo.Status = DbServices.MarketStatus(null, (int) MARKET_ID.UPCoM);
                    upcomMarketInfo.SetIndex = DbServices.IndexLastDay((int)MARKET_ID.UPCoM);
                }
                else
                {
                    upcomMarketInfo.Status = DbServices.MarketStatus(upcomMarketInfo,
                                                                     (int) MARKET_ID.UPCoM);
                }
                if (Utils.Common.RunMode == "TEST")
                {
                    hoseMarketInfo.Status = (char) UdMarketStatus[0];
                    hnxMarketInfo.Status = (char) UdMarketStatus[1];
                    upcomMarketInfo.Status = (char) UdMarketStatus[2];
                }

                listMarketInfos.Add(hoseMarketInfo);
                listMarketInfos.Add(hnxMarketInfo);
                listMarketInfos.Add(upcomMarketInfo);

                retObject.Result = listMarketInfos;
            }
            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return Serializer.Serialize(retObject);
        }

        [WebMethod(Description = "Set Market Status")]
        [ScriptMethod]
        public string SetMatketStatus(int marketId, string marketStatus)
        {
            marketId = marketId - 1;

            if (marketStatus == ((char) CommonEnums.MARKET_STATUS.CLOSE).ToString())
                UdMarketStatus[marketId] = CommonEnums.MARKET_STATUS.CLOSE;
            else if (marketStatus == ((char) CommonEnums.MARKET_STATUS.CLOSE_PT).ToString())
                UdMarketStatus[marketId] = CommonEnums.MARKET_STATUS.CLOSE_PT;
            else if (marketStatus == ((char) CommonEnums.MARKET_STATUS.INIT_APP).ToString())
                UdMarketStatus[marketId] = CommonEnums.MARKET_STATUS.INIT_APP;
            else if (marketStatus == ((char) CommonEnums.MARKET_STATUS.OPEN).ToString())
                UdMarketStatus[marketId] = CommonEnums.MARKET_STATUS.OPEN;
            else if (marketStatus == ((char) CommonEnums.MARKET_STATUS.PRE_CLOSE).ToString())
                UdMarketStatus[marketId] = CommonEnums.MARKET_STATUS.PRE_CLOSE;
            else if (marketStatus == ((char) CommonEnums.MARKET_STATUS.PRE_OPEN).ToString())
                UdMarketStatus[marketId] = CommonEnums.MARKET_STATUS.PRE_OPEN;
            else if (marketStatus == ((char) CommonEnums.MARKET_STATUS.READY).ToString())
                UdMarketStatus[marketId] = CommonEnums.MARKET_STATUS.READY;
            else if (marketStatus == ((char) CommonEnums.MARKET_STATUS.UNVAILABLE).ToString())
                UdMarketStatus[marketId] = CommonEnums.MARKET_STATUS.UNVAILABLE;
            else if (marketStatus == ((char) CommonEnums.MARKET_STATUS.WAITING).ToString())
                UdMarketStatus[marketId] = CommonEnums.MARKET_STATUS.WAITING;
            else if (marketStatus == ((char) CommonEnums.MARKET_STATUS.HAFT).ToString())
                UdMarketStatus[marketId] = CommonEnums.MARKET_STATUS.HAFT;
            else if (marketStatus == ((char) CommonEnums.MARKET_STATUS.OPEN_2).ToString())
                UdMarketStatus[marketId] = CommonEnums.MARKET_STATUS.OPEN_2;

            return "Set market status successfull";
        }

        [WebMethod(Description = "Update Company Info From Automation program")]
        [ScriptMethod]
        public bool UpdateCompanyInfo()
        {
            try
            {
                //DbServices.UpdateCompanyInfo();
                DbServices.UpdateLangCompanyInfo();
                return true;
            }
            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
                return false;
            }
        }

        [WebMethod(Description = "Get Company List")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListCompany(int marketId, string languageId)
        {
            var retObject = new ResultObject<List<CompanyInfo>> {RetCode = CommonEnums.RET_CODE.SUCCESS, Result = null};
            try
            {
                List<CompanyInfo> 
                        listCompanyInfos = DbServices.GetListCompanyInfoByLanguageId(languageId,
                                                                                     (short) marketId);
                retObject.Result = listCompanyInfos;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return Serializer.Serialize(retObject);
        }

        [WebMethod(Description = "Get NewestWorkingDates")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetNewestWorkingDates()
        {
            var retObject = new ResultObject<List<NewestWorkingDatesInfo>>
                                {RetCode = CommonEnums.RET_CODE.SUCCESS, Result = null};

            try
            {
                List<NewestWorkingDatesInfo> listNewestWorkingDates = DbServices.Select_All_NewestWorkingDates();

                retObject.Result = listNewestWorkingDates;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return Serializer.Serialize(retObject);
        }

        [WebMethod(Description = "Get All Stock From Market")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetAllStockFromMarket(short marketID)
        {
            var retObject = new ResultObject<List<StockInfo>> {RetCode = CommonEnums.RET_CODE.SUCCESS, Result = null};

            try
            {
                List<StockInfo> listStockInfos = DbServices.Select_All_StockInfo(marketID);

                retObject.Result = listStockInfos;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return Serializer.Serialize(retObject);
        }

        [WebMethod(Description = "Get Best1Bid")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetBestCurrentPrice(string symbol)
        {
            var retObject = new ResultObject<StockInfo> {RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA, Result = null};
            try
            {
                retObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;

                StockInfo stockInfo = DbServices.GetStock(symbol);

                if (stockInfo == null)
                {
                    retObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }
                else
                {
                    retObject.RetCode = CommonEnums.RET_CODE.SUCCESS;
                    MarketInfo marInfo = DbServices.GetCurrentMarketInfo(stockInfo.MarketID, true);

                    char marketStatus = DbServices.MarketStatus(marInfo, stockInfo.MarketID);

                    switch (marketStatus)
                    {
                        case (char) CommonEnums.MARKET_STATUS.OPEN_2:
                        case (char) CommonEnums.MARKET_STATUS.OPEN:
                        case (char) CommonEnums.MARKET_STATUS.HAFT:
                        case (char) CommonEnums.MARKET_STATUS.PRE_CLOSE:
                        case (char) CommonEnums.MARKET_STATUS.PRE_OPEN:
                            stockInfo.Last = stockInfo.Best1Bid;
                            break;
                        default:
                            switch (stockInfo.MarketID)
                            {
                                case (short) CommonEnums.MARKET_ID.HOSE:
                                    stockInfo.Last = stockInfo.Last;
                                    break;
                                case (short) CommonEnums.MARKET_ID.HNX:
                                    stockInfo.Last = stockInfo.ClosePrice;
                                    break;
                                case (short) CommonEnums.MARKET_ID.UPCoM:
                                    stockInfo.Last = stockInfo.ClosePrice;
                                    break;
                                default:
                                    stockInfo.Last = stockInfo.Last;
                                    break;
                            }
                            break;
                    }
                    retObject.Result = stockInfo;
                }
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return Serializer.Serialize(retObject);
        }

        [WebMethod(Description = "Get Stock Info")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetStockInfo(string symbol)
        {
            var retObject = new ResultObject<StockInfo> {RetCode = CommonEnums.RET_CODE.SUCCESS, Result = null};

            try
            {
                retObject.RetCode = CommonEnums.RET_CODE.SUCCESS;

                StockInfo stockInfo = DbServices.GetStock(symbol);

                if (stockInfo == null)
                {
                    retObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }

                retObject.Result = stockInfo;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return Serializer.Serialize(retObject);
        }

        [WebMethod(Description = "Get Stock information and transaction information")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetStockDetail(string symbol, int id)
        {
            var retObject = new ResultObject<StockInfo> {RetCode = CommonEnums.RET_CODE.SUCCESS, Result = null};

            try
            {
                retObject.RetCode = CommonEnums.RET_CODE.SUCCESS;

                StockInfo stockInfo = DbServices.GetStock(symbol);

                if (stockInfo == null)
                {
                    retObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }
                else
                {
                    var list = new List<DealDetail>();
                    switch (stockInfo.MarketID)
                    {
                        case (int) CommonEnums.MARKET_ID.HOSE:
                            list = GetHOSETransactionInfo(symbol, id);
                            break;
                        case (int) CommonEnums.MARKET_ID.HNX:
                            list = GetHNXTransactionInfo(symbol, id);
                            break;
                        case (int) CommonEnums.MARKET_ID.UPCoM:
                            list = GetUPCOMTransactionInfo(symbol, id);
                            break;
                    }
                    stockInfo.DealDetails = list;
                }

                retObject.Result = stockInfo;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return Serializer.Serialize(retObject);
        }

        [WebMethod(Description = "Get Stock Info")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetStockInfoByLanguge(string symbol, string languageId)
        {
            var retObject = new ResultObject<StockInfo> {RetCode = CommonEnums.RET_CODE.SUCCESS, Result = null};

            try
            {
                retObject.RetCode = CommonEnums.RET_CODE.SUCCESS;

                StockInfo stockInfo = DbServices.GetStock(symbol, languageId);

                if (stockInfo == null)
                {
                    retObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
                }

                retObject.Result = stockInfo;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return Serializer.Serialize(retObject);
        }

        [WebMethod(Description = "Get List Stock")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListStockInfos(string listCodes, string languageId)
        {
            var retObject = new ResultObject<List<StockInfo>> {RetCode = CommonEnums.RET_CODE.SUCCESS, Result = null};

            try
            {
                List<StockInfo> listStockInfos = DbServices.Select_List_StockInfos(listCodes, languageId);

                retObject.Result = listStockInfos;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return Serializer.Serialize(retObject);
        }

        private string GetHOSETransactionInfo(string symbol)
        {
            var retObject = new ResultObject<List<HOSETransactionInfo>>
                                {RetCode = CommonEnums.RET_CODE.SUCCESS, Result = null};

            try
            {
                List<HOSETransactionInfo> listTransactionInfos = DbServices.SelectHoseTransactionInfo(symbol);

                retObject.Result = listTransactionInfos;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return Serializer.Serialize(retObject);
        }

        private static List<DealDetail> GetHOSETransactionInfo(string symbol, int id)
        {
            var dealDetails = new List<DealDetail>();
            try
            {
                List<HOSETransactionInfo> listTransactionInfos = DbServices.SelectHoseTransactionInfo(symbol, id);
                if (listTransactionInfos != null)
                {
                    foreach (HOSETransactionInfo transactionInfo in listTransactionInfos)
                    {
                        var dealDetail = new DealDetail
                                             {
                                                 Id = transactionInfo.Id,
                                                 Time = transactionInfo.Time,
                                                 Price = transactionInfo.Price,
                                                 Changed = transactionInfo.Changed,
                                                 Val = transactionInfo.Val,
                                                 Vol = transactionInfo.Vol
                                             };
                        dealDetails.Add(dealDetail);
                    }
                }
                return dealDetails;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
                return null;
            }
        }

        private string GetHNXTransactionInfo(string symbol)
        {
            var retObject = new ResultObject<List<HNXTransactionInfo>>
                                {RetCode = CommonEnums.RET_CODE.SUCCESS, Result = null};

            try
            {
                List<HNXTransactionInfo> listTransactionInfos = DbServices.SelectHnxTransactionInfo(symbol);

                retObject.Result = listTransactionInfos;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return Serializer.Serialize(retObject);
        }

        private List<DealDetail> GetHNXTransactionInfo(string symbol, int id)
        {
            var dealDetails = new List<DealDetail>();
            try
            {
                List<HNXTransactionInfo> listTransactionInfos = DbServices.SelectHnxTransactionInfo(symbol, id);
                if (listTransactionInfos != null)
                {
                    foreach (HNXTransactionInfo transactionInfo in listTransactionInfos)
                    {
                        var dealDetail = new DealDetail
                                             {
                                                 Id = transactionInfo.Id,
                                                 Time = transactionInfo.Time,
                                                 Price = transactionInfo.Price,
                                                 Changed = transactionInfo.Changed,
                                                 Val = transactionInfo.Val,
                                                 Vol = transactionInfo.Vol
                                             };
                        dealDetails.Add(dealDetail);
                    }
                }
                return dealDetails;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
                return null;
            }
        }

        private string GetUPCOMTransactionInfo(string symbol)
        {
            var retObject = new ResultObject<List<UPCOMTransactionInfo>>
                                {RetCode = CommonEnums.RET_CODE.SUCCESS, Result = null};

            try
            {
                List<UPCOMTransactionInfo> listTransactionInfos = DbServices.SelectUpcomTransactionInfo(symbol);

                retObject.Result = listTransactionInfos;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return Serializer.Serialize(retObject);
        }

        private static List<DealDetail> GetUPCOMTransactionInfo(string symbol, int id)
        {
            var dealDetails = new List<DealDetail>();
            try
            {
                List<UPCOMTransactionInfo> listTransactionInfos = DbServices.SelectUpcomTransactionInfo(symbol, id);
                if (listTransactionInfos != null)
                {
                    foreach (UPCOMTransactionInfo transactionInfo in listTransactionInfos)
                    {
                        var dealDetail = new DealDetail
                                             {
                                                 Id = transactionInfo.Id,
                                                 Time = transactionInfo.Time,
                                                 Price = transactionInfo.Price,
                                                 Changed = transactionInfo.Changed,
                                                 Val = transactionInfo.Val,
                                                 Vol = transactionInfo.Vol
                                             };
                        dealDetails.Add(dealDetail);
                    }
                }
                return dealDetails;
            }
            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
                return null;
            }
        }

        [WebMethod(Description = "Get All Transaction")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetTransactions(string symbol, int marketID)
        {
            switch (marketID)
            {
                case (int) CommonEnums.MARKET_ID.HOSE:
                    return GetHOSETransactionInfo(symbol);
                case (int) CommonEnums.MARKET_ID.HNX:
                    return GetHNXTransactionInfo(symbol);
                case (int) CommonEnums.MARKET_ID.UPCoM:
                    return GetUPCOMTransactionInfo(symbol);
            }

            var retObject = new ResultObject<List<HOSETransactionInfo>>
                                {RetCode = CommonEnums.RET_CODE.FAIL, Result = null};

            return Serializer.Serialize(retObject);
        }

        private string GetHOSEMainMatchedPriceInfo(string symbol)
        {
            var retObject = new ResultObject<List<MainMatchedPricesInfo>>
                                {RetCode = CommonEnums.RET_CODE.SUCCESS, Result = null};

            try
            {
                List<MainMatchedPricesInfo> listTransactionInfos = DbServices.GetHoseMainMatchedPrices(symbol);

                retObject.Result = listTransactionInfos;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return Serializer.Serialize(retObject);
        }

        private string GetHNXMainMatchedPriceInfo(string symbol)
        {
            var retObject = new ResultObject<List<MainMatchedPricesInfo>>
                                {RetCode = CommonEnums.RET_CODE.SUCCESS, Result = null};

            try
            {
                List<MainMatchedPricesInfo> listTransactionInfos = DbServices.GetHnxMainMatchedPrices(symbol);

                retObject.Result = listTransactionInfos;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return Serializer.Serialize(retObject);
        }

        private string GetUPCOMMainMatchedPriceInfo(string symbol)
        {
            var retObject = new ResultObject<List<MainMatchedPricesInfo>>
                                {RetCode = CommonEnums.RET_CODE.SUCCESS, Result = null};

            try
            {
                List<MainMatchedPricesInfo> listTransactionInfos = DbServices.GetUpcomMainMatchedPrices(symbol);

                retObject.Result = listTransactionInfos;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return Serializer.Serialize(retObject);
        }

        [WebMethod(Description = "Get Main Matched Prices")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetMainMatchedPrices(string symbol, int marketID)
        {
            switch (marketID)
            {
                case (int) CommonEnums.MARKET_ID.HOSE:
                    return GetHOSEMainMatchedPriceInfo(symbol);
                case (int) CommonEnums.MARKET_ID.HNX:
                    return GetHNXMainMatchedPriceInfo(symbol);
                case (int) CommonEnums.MARKET_ID.UPCoM:
                    return GetUPCOMMainMatchedPriceInfo(symbol);
            }

            var retObject = new ResultObject<List<MainMatchedPricesInfo>>
                                {RetCode = CommonEnums.RET_CODE.FAIL, Result = null};

            return Serializer.Serialize(retObject);
        }

        [WebMethod(Description = "Get Ticker Info List")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetTickerInfos(string listCodesInfos)
        {
            List<TickerInfo> listTickerInfos = DbServices.GetTickerListByListCode(listCodesInfos);

            string listTickers = ConvertTickerInfo(listTickerInfos);
            /*var retObject = new ResultObject<List<TickerInfo>> { RetCode = CommonEnums.RET_CODE.SUCCESS, Result = null };

            retObject.Result = listTickerInfos;

            return Serializer.Serialize(retObject);*/
            return listTickers;
        }

        /// <summary>
        /// Convert ticker information to correct format.
        /// </summary>
        /// <param name="listTickerInfos">List of ticker</param>
        /// <returns></returns>
        private static string ConvertTickerInfo(IEnumerable<TickerInfo> listTickerInfos)
        {
            var stockStringBuilder = new StringBuilder();
            foreach (TickerInfo tickerInfo in listTickerInfos)
            {
                stockStringBuilder.AppendFormat(
                    "[\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\", \"{6}\", \"{7}\", \"{8}\"],", tickerInfo.Id,
                    tickerInfo.StockSymbol, tickerInfo.Price, tickerInfo.Vol,
                    tickerInfo.Changed, tickerInfo.Side, tickerInfo.RefPrice, tickerInfo.Ceiling, tickerInfo.Floor);
            }
            string stockString = stockStringBuilder.ToString();
            if (!string.IsNullOrEmpty(stockString))
            {
                stockString = stockString.Substring(0, stockString.Length - 1);
            }
            string result = string.Format("[{0}]", stockString);
            return result;
        }

        [WebMethod(Description = "Get All Ticker Info")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetAllTickerInfos(int marketId, int id)
        {
            List<TickerInfo> listTickerInfos = DbServices.GetTickerListById(marketId, id);

            var retObject = new ResultObject<List<TickerInfo>> {RetCode = CommonEnums.RET_CODE.SUCCESS, Result = null};

            retObject.Result = listTickerInfos;

            return Serializer.Serialize(retObject);
        }

        [WebMethod(Description = "Get Put Ad")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetHOSEPutAd()
        {
            var retObject = new ResultObject<List<HOSEPutAdInfo>>
                                {RetCode = CommonEnums.RET_CODE.SUCCESS, Result = null};

            try
            {
                List<HOSEPutAdInfo> listHOSEPutAdInfo = DbServices.SelectAllHosePutAdInfo();

                retObject.Result = listHOSEPutAdInfo;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return Serializer.Serialize(retObject);
        }

        [WebMethod(Description = "Get Put Exec")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetHOSEPutExec()
        {
            var retObject = new ResultObject<List<HOSEPutExecInfo>>
                                {RetCode = CommonEnums.RET_CODE.SUCCESS, Result = null};

            try
            {
                List<HOSEPutExecInfo> listHOSEPutExecInfo = DbServices.SelectAllHosePutExecInfo();

                retObject.Result = listHOSEPutExecInfo;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return Serializer.Serialize(retObject);
        }

        [WebMethod]
        public int StartUpdater()
        {
            RealTimeStockUpdater.StartUpdater();

            return 0;
        }

        [WebMethod]
        public void StartUpdaterForMarket(int marketId)
        {
            RealTimeStockUpdater.StartUpdater(marketId);
        }

        [WebMethod]
        public string UpdatingStatus()
        {
            var retObject = new ResultObject<Boolean[]>
                                {
                                    RetCode = CommonEnums.RET_CODE.SUCCESS,
                                    Result = new[]
                                                 {
                                                     false,
                                                     false,
                                                     false
                                                 }
                                };

            try
            {
                var status = new Boolean[3];

                status[0] = RealTimeStockUpdater.UpdatingStatus((int) MARKET_ID.HOSE);
                status[1] = RealTimeStockUpdater.UpdatingStatus((int) MARKET_ID.HASTC);
                status[2] = RealTimeStockUpdater.UpdatingStatus((int) MARKET_ID.UPCoM);

                retObject.Result = status;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return Serializer.Serialize(retObject);
        }

        [WebMethod]
        public void StartIntraD(int marketID)
        {
            //RealTimeStockUpdater.EnableUpdatingIntraD = true;
            string marketName = "";
            switch (marketID)
            {
                case (int) CommonEnums.MARKET_ID.HOSE:
                    RealTimeStockUpdater.EnableIntraDhose = true;
                    marketName = "Hose";
                    break;
                case (int) CommonEnums.MARKET_ID.HNX:
                    RealTimeStockUpdater.EnableIntraDhnx = true;
                    marketName = "Hnx";
                    break;
                case (int) CommonEnums.MARKET_ID.UPCoM:
                    RealTimeStockUpdater.EnableIntraDupcom = true;
                    marketName = "Upcom";
                    break;
            }
            LogHandler.Log("Start Intraday for " + marketName, "StartIntraD", TraceEventType.Information);
        }

        [WebMethod]
        public int StopUpdater()
        {
            RealTimeStockUpdater.StopUpdater();

            return 0;
        }

        [WebMethod]
        public void StopUpdaterForMarket(int marketId)
        {
            RealTimeStockUpdater.StopUpdater(marketId);
        }

        [WebMethod]
        public void StopIntraD(int marketID)
        {
            //RealTimeStockUpdater.EnableUpdatingIntraD = false;
            string marketName = "";
            switch (marketID)
            {
                case (int) CommonEnums.MARKET_ID.HOSE:
                    RealTimeStockUpdater.EnableIntraDhose = false;
                    marketName = "Hose";
                    break;
                case (int) CommonEnums.MARKET_ID.HNX:
                    RealTimeStockUpdater.EnableIntraDhnx = false;
                    marketName = "Hnx";
                    break;
                case (int) CommonEnums.MARKET_ID.UPCoM:
                    RealTimeStockUpdater.EnableIntraDupcom = false;
                    marketName = "Upcom";
                    break;
            }
            LogHandler.Log("Stop Intraday for " + marketName, "StopIntraD", TraceEventType.Information);
        }

        [WebMethod]
        public string IsIntradayRunning()
        {
            var retObject = new ResultObject<Boolean[]>
                                {
                                    RetCode = CommonEnums.RET_CODE.SUCCESS,
                                    Result = new[]
                                                 {
                                                     false,
                                                     false,
                                                     false
                                                 }
                                };

            try
            {
                var status = new Boolean[3];

                status[0] = RealTimeStockUpdater.IsIntradayRunning((int) CommonEnums.MARKET_ID.HOSE);
                status[1] = RealTimeStockUpdater.IsIntradayRunning((int) CommonEnums.MARKET_ID.HNX);
                status[2] = RealTimeStockUpdater.IsIntradayRunning((int) CommonEnums.MARKET_ID.UPCoM);

                retObject.Result = status;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return Serializer.Serialize(retObject);
        }


        [WebMethod(Description = "Check if data is latest data.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CheckLatestData()
        {
            var retObject = new ResultObject<bool[]>
                                {
                                    RetCode = CommonEnums.RET_CODE.SUCCESS,
                                    Result = new[]
                                                 {
                                                     false,
                                                     false,
                                                     false
                                                 }
                                };

            try
            {
                var status = new bool[3];
                status[0] = DbServices.HaveStocksForToday((int) CommonEnums.MARKET_ID.HOSE);
                status[1] = DbServices.HaveStocksForToday((int) CommonEnums.MARKET_ID.HNX);
                status[2] = DbServices.HaveStocksForToday((int) CommonEnums.MARKET_ID.UPCoM);

                retObject.Result = status;
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return Serializer.Serialize(retObject);
        }

        [WebMethod(Description = "Restart RTService")]
        public bool RestartRTService()
        {
            return RealTimeStockUpdater.RestartRtService();
        }

        [WebMethod(Description = "Select First Vn30 item")]
        public string GetFirstIndexVN30()
        {
            var retObject = new ResultObject<List<IndexInfo>> {RetCode = CommonEnums.RET_CODE.SUCCESS, Result = null};
            var listIndex = new List<IndexInfo>();
            IndexInfo indexInfo = DbServices.GetFirstIndexVn30();
            if (indexInfo == null)
            {
                retObject.RetCode = CommonEnums.RET_CODE.NO_EXISTED_DATA;
            }
            else
            {
                listIndex.Add(indexInfo);
                retObject.Result = listIndex;
            }
            return Serializer.Serialize(retObject);
        }

        [WebMethod]
        public string GetAllIndexVN30()
        {
            var retObject = new ResultObject<List<IndexInfo>>
                                {RetCode = CommonEnums.RET_CODE.SUCCESS, Result = null};
            var listIndex = new List<IndexInfo>();
            listIndex = DbServices.GetAllIndexVn30();
            retObject.Result = listIndex;
            return Serializer.Serialize(retObject);
        }
        [WebMethod]
        public void ResetHnx30()
        {
            DbServices.ResetTotalHnx30();
        }

        #region Priceboard

        /// <summary>
        /// Get static stock information to send to priceboard
        /// </summary>
        /// <param name="listCodes">List of stock code</param>
        /// <param name="languageId">Language id</param>
        /// <returns></returns>
        [WebMethod]
        public string GetStaticStockInfo(string listCodes, int languageId)
        {
            string result = string.Empty;
            try
            {
                List<StockInfo> listStockInfos = DbServices.Select_List_StockInfos(listCodes);

                result = ConvertStaticInfo(listStockInfos);
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return result;
        }

        private static bool CanTrade(int marketID)
        {
            if (UdMarketStatus[marketID - 1] == CommonEnums.MARKET_STATUS.READY ||
                UdMarketStatus[marketID - 1] == CommonEnums.MARKET_STATUS.OPEN ||
                UdMarketStatus[marketID - 1] == CommonEnums.MARKET_STATUS.PRE_OPEN ||
                UdMarketStatus[marketID - 1] == CommonEnums.MARKET_STATUS.PRE_CLOSE ||
                UdMarketStatus[marketID - 1] == CommonEnums.MARKET_STATUS.OPEN_2
                )
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get static information from list stocks.
        /// </summary>
        /// <param name="listStockInfos">List of stocks</param>
        /// <returns></returns>
        private static string ConvertStaticInfo(IEnumerable<StockInfo> listStockInfos)
        {
            var stockStringBuilder = new StringBuilder();
            foreach (StockInfo stockInfo in listStockInfos)
            {
                string market = string.Empty;
                switch (stockInfo.MarketID)
                {
                    case 1:
                        market = "HO";
                        break;
                    case 2:
                        market = "HN";
                        break;
                    case 3:
                        market = "UP";
                        break;
                }
                stockStringBuilder.AppendFormat("\r\n[\"{0}\", {1}, {2}, {3}, \"{4}\", \"{5}\"],", stockInfo.StockSymbol,
                                                stockInfo.RefPrice, stockInfo.Ceiling,
                                                stockInfo.Floor, market, stockInfo.Name);
            }
            string stockString = stockStringBuilder.ToString();
            if (!string.IsNullOrEmpty(stockString))
            {
                stockString = stockString.Substring(0, stockString.Length - 1);
            }
            string result = string.Format("[{0}\r\n]", stockString);
            return result;
        }

        /// <summary>
        /// Get dynamic stock information to send to priceboard
        /// </summary>
        /// <param name="listCodes">List of stock code</param>
        /// <param name="languageId">Language id</param>
        /// <returns></returns>
        [WebMethod]
        public string GetDynamicStockInfo(string listCodes, int languageId)
        {
            string result = string.Empty;
            try
            {
                List<StockInfo> listStockInfos = DbServices.Select_List_StockInfos(listCodes);

                result = ConvertDynamicInfo(listStockInfos);
            }

            catch (Exception ex)
            {
                Utils.Common.Log(ex.ToString());
            }

            return result;
        }

        /// <summary>
        /// Get dynamic information from list stocks.
        /// </summary>
        /// <param name="listStockInfos">List of stocks</param>
        /// <returns></returns>
        private static string ConvertDynamicInfo(IEnumerable<StockInfo> listStockInfos)
        {
            var stockStringBuilder = new StringBuilder();
            foreach (StockInfo stockInfo in listStockInfos)
            {
                double change = 0;
                if (Math.Round(stockInfo.Last, 1) != 0)
                {
                    change = Math.Round(stockInfo.Last - stockInfo.RefPrice, 1);
                }
                string market = string.Empty;
                Int32 best1BidVolume = 0;
                Int32 best1OfferVolume = 0;
                Int32 best2BidVolume = 0;
                Int32 best2OfferVolume = 0;
                Int32 best3BidVolume = 0;
                Int32 best3OfferVolume = 0;
                Int32 lastVol = 0;
                Int64 totalShare = 0;
                Int64 fRBoughtVol = 0;
                Int64 fRSoldVol = 0;
                switch (stockInfo.MarketID)
                {
                    case 1:
                        market = "HO";
                        best1BidVolume = stockInfo.Best1BidVolume/Utils.Common.HOSE_VOLUME_UNIT;
                        best1OfferVolume = stockInfo.Best1OfferVolume/Utils.Common.HOSE_VOLUME_UNIT;
                        best2BidVolume = stockInfo.Best2BidVolume/Utils.Common.HOSE_VOLUME_UNIT;
                        best2OfferVolume = stockInfo.Best2OfferVolume/Utils.Common.HOSE_VOLUME_UNIT;
                        best3BidVolume = stockInfo.Best3BidVolume/Utils.Common.HOSE_VOLUME_UNIT;
                        best3OfferVolume = stockInfo.Best3OfferVolume/Utils.Common.HOSE_VOLUME_UNIT;
                        lastVol = stockInfo.LastVol/Utils.Common.HOSE_VOLUME_UNIT;
                        totalShare = stockInfo.TotalShare/Utils.Common.HOSE_VOLUME_UNIT;
                        fRBoughtVol = stockInfo.FRBoughtVol/Utils.Common.HOSE_VOLUME_UNIT;
                        fRSoldVol = stockInfo.FRSoldVol/Utils.Common.HOSE_VOLUME_UNIT;
                        break;
                    case 2:
                        market = "HN";
                        best1BidVolume = stockInfo.Best1BidVolume/Utils.Common.HNX_VOLUME_UNIT;
                        best1OfferVolume = stockInfo.Best1OfferVolume/Utils.Common.HNX_VOLUME_UNIT;
                        best2BidVolume = stockInfo.Best2BidVolume/Utils.Common.HNX_VOLUME_UNIT;
                        best2OfferVolume = stockInfo.Best2OfferVolume/Utils.Common.HNX_VOLUME_UNIT;
                        best3BidVolume = stockInfo.Best3BidVolume/Utils.Common.HNX_VOLUME_UNIT;
                        best3OfferVolume = stockInfo.Best3OfferVolume/Utils.Common.HNX_VOLUME_UNIT;
                        lastVol = stockInfo.LastVol/Utils.Common.HNX_VOLUME_UNIT;
                        totalShare = stockInfo.TotalShare/Utils.Common.HNX_VOLUME_UNIT;
                        fRBoughtVol = stockInfo.FRBoughtVol/Utils.Common.HNX_VOLUME_UNIT;
                        fRSoldVol = stockInfo.FRSoldVol/Utils.Common.HNX_VOLUME_UNIT;
                        break;
                    case 3:
                        market = "UP";
                        best1BidVolume = stockInfo.Best1BidVolume/Utils.Common.UPCOM_VOLUME_UNIT;
                        best1OfferVolume = stockInfo.Best1OfferVolume/Utils.Common.UPCOM_VOLUME_UNIT;
                        best2BidVolume = stockInfo.Best2BidVolume/Utils.Common.UPCOM_VOLUME_UNIT;
                        best2OfferVolume = stockInfo.Best2OfferVolume/Utils.Common.UPCOM_VOLUME_UNIT;
                        best3BidVolume = stockInfo.Best3BidVolume/Utils.Common.UPCOM_VOLUME_UNIT;
                        best3OfferVolume = stockInfo.Best3OfferVolume/Utils.Common.UPCOM_VOLUME_UNIT;
                        lastVol = stockInfo.LastVol/Utils.Common.UPCOM_VOLUME_UNIT;
                        totalShare = stockInfo.TotalShare/Utils.Common.UPCOM_VOLUME_UNIT;
                        fRBoughtVol = stockInfo.FRBoughtVol/Utils.Common.UPCOM_VOLUME_UNIT;
                        fRSoldVol = stockInfo.FRSoldVol/Utils.Common.UPCOM_VOLUME_UNIT;
                        break;
                }

                /*if (RTDataServices.Common.RunMode == "TEST")
                {
                    stockInfo.CanTrade = CanTrade(stockInfo.MarketID) ? 1 : 0;
                }
                else
                {
                    stockInfo.CanTrade = (DBServices.IsOpenedMarket(stockInfo.MarketID) ? 1 : 0);
                }*/

                stockStringBuilder.AppendFormat(
                    "\r\n['{0}',{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},'{25}','{26}','{27}','{28}'],",
                    stockInfo.StockSymbol, stockInfo.Best3Bid, best3BidVolume, stockInfo.Best2Bid,
                    best2BidVolume, stockInfo.Best1Bid, best1BidVolume, change, stockInfo.Last,
                    lastVol, totalShare, stockInfo.Best1Offer, best1OfferVolume,
                    stockInfo.Best2Offer, best2OfferVolume, stockInfo.Best3Offer, best3OfferVolume,
                    stockInfo.AvrPrice, stockInfo.OpenPrice, stockInfo.Highest, stockInfo.Lowest, fRBoughtVol,
                    fRSoldVol, stockInfo.CanTrade, stockInfo.CanTrade, market,stockInfo.RefPrice,stockInfo.Ceiling,stockInfo.Floor);
            }
            string stockString = stockStringBuilder.ToString();
            if (!string.IsNullOrEmpty(stockString))
            {
                stockString = stockString.Substring(0, stockString.Length - 1);
            }
            string result = string.Format("[{0}\r\n]", stockString);
            return result;
        }

        #endregion
    }
}