// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ETradeServicesMock.asmx.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Summary description for Service1
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeServicesMock
{
    using System;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;
    using System.Web.Script.Services;
    using System.Web.UI.MobileControls;

    using ETradeCommon;
    using ETradeCommon.Enums;
    using System.Web.Services;

    using global::ETradeServicesMock.DTO;

    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ETradeServicesMock : System.Web.Services.WebService
    {
        private static readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();

        [WebMethod(Description = "Logon for investor")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Login(string username, string password, int authType)
        {

            var resultObject = new ResultObject<LogonDTO>
                {
                    RetCode = CommonEnums.RET_CODE.SUCCESS,
                    Result = new LogonDTO { OTP = "123456", SessionId = "0123asxasfsd232edsa4sasd" }
                };

            return Serializer.Serialize(resultObject);
        }

        [WebMethod(Description = "Logout for investor")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Logout(string sessionId)
        {

            var resultObject = new ResultObject<CommonEnums.RET_CODE>
            {
                RetCode = CommonEnums.RET_CODE.SUCCESS,
                Result = CommonEnums.RET_CODE.SUCCESS
            };

            return Serializer.Serialize(resultObject);
        }

        [WebMethod(Description = "Get account information")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetAccountInfo(string sessionId, string accountId)
        {

            var resultObject = new ResultObject<AccountInfoDTO>
                {
                    RetCode = CommonEnums.RET_CODE.SUCCESS,
                    Result =
                        new AccountInfoDTO
                            {
                                AccountName = "Nguyễn Văn A",
                                AuthType = (Int32)CommonEnums.TOKEN_TYPE.PIN_PASS,
                                IsActived = true,
                                ListPermission =
                                    new List<CustomerPermissionDTO>
                                        {
                                            new CustomerPermissionDTO { PermissionName = "CanBuy", PermissionValue = true },
                                            new CustomerPermissionDTO { PermissionName = "CanSell", PermissionValue = true },
                                            new CustomerPermissionDTO { PermissionName = "CashAdvance", PermissionValue = false },
                                            new CustomerPermissionDTO { PermissionName = "ViewStatement", PermissionValue = true },
                                            new CustomerPermissionDTO { PermissionName = "ViewOrderStatus", PermissionValue = true },
                                            new CustomerPermissionDTO { PermissionName = "ViewBalance", PermissionValue = true },
                                            new CustomerPermissionDTO { PermissionName = "ViewStatement", PermissionValue = true },
                                            new CustomerPermissionDTO { PermissionName = "CashTransfer", PermissionValue = false },
                                            new CustomerPermissionDTO { PermissionName = "StockTransfer", PermissionValue = false },
                                            new CustomerPermissionDTO { PermissionName = "RightRegister", PermissionValue = false },
                                            new CustomerPermissionDTO { PermissionName = "ResearchNAnalyze", PermissionValue = false },
                                            new CustomerPermissionDTO { PermissionName = "ViewAccountInfo", PermissionValue = true }
                                        },
                                ListSubAccount = new List<string> { "0088661", "0088666", "0123451" },
                                LockAccountReason = (Int32)CommonEnums.LOCK_ACCOUNT_REASON.NOTHING
                            }
                };

            return Serializer.Serialize(resultObject);
        }

        [WebMethod(Description = "Get cash avaibale for buy order")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetAvailableCash(string sessionId, string accountNo, string accountType)
        {

            var resultObject = new ResultObject<CashBalanceDTO>
                {
                    RetCode = CommonEnums.RET_CODE.SUCCESS,
                    Result = new CashBalanceDTO { BuyCredit = 1000000, EE = 500, IM = 1000, PP = 500 }
                };

            return Serializer.Serialize(resultObject);
        }

        [WebMethod(Description = "Get stock avaibale for sell order")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetAvailableStock(string sessionId, string accountNo, string symbol)
        {

            var resultObject = new ResultObject<StockBalanceDTO>
            {
                RetCode = CommonEnums.RET_CODE.SUCCESS,
                Result = new StockBalanceDTO() { AvaiVolume = 1100, Symbol = symbol }
            };

            return Serializer.Serialize(resultObject);
        }

        [WebMethod(Description = "Get list order status for intra-day")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetOrderList(string sessionId, string accountNo, int pageSize,
            int pageNumber, bool isPending, bool isMatched,
            bool isSemiMatched, bool isCancelling,
            bool isCancelled, bool isRejected)
        {

            var resultObject = new ResultObject<List<OrderInfoDTO>>
                {
                    RetCode = CommonEnums.RET_CODE.SUCCESS,
                    Result = new List<OrderInfoDTO>
                        {
                            CreateAnOrderInfo(1), 
                            CreateAnOrderInfo(2), 
                            CreateAnOrderInfo(3),
                            CreateAnOrderInfo(4),
                            CreateAnOrderInfo(5),
                            CreateAnOrderInfo(6),
                            CreateAnOrderInfo(7),
                            CreateAnOrderInfo(8),
                            CreateAnOrderInfo(9),
                            CreateAnOrderInfo(10),
                        }
                };

            return Serializer.Serialize(resultObject);
        }

        OrderInfoDTO CreateAnOrderInfo(Int64 OrderId)
        {
            return new OrderInfoDTO()
                {
                    OrderId = OrderId,
                    OrderNo = OrderId,
                    CanCancel = true,
                    CancelledTime = "092215",
                    CancelledVolume = 100,
                    CancelRejReason = (Int32)CommonEnums.REJECT_REASON.NOTHING,
                    CancelStatus = (Int32)CommonEnums.CANCEL_STATUS.CANCELLED,
                    IsNew = false,
                    MatchedPrice = 15.5M,
                    MatchedVolume = 200,
                    NumberOfMatched = 2,
                    OrderSource = (Int32)CommonEnums.ORDER_SOURCE.WEB,
                    OrderStatus = (Int32)CommonEnums.ORDER_STATUS.SEMI_MATCHED,
                    OrderTime = "092000",
                    OrdRejReason = (Int32)CommonEnums.REJECT_REASON.NOTHING,
                    Price = 15,
                    Side = (char)CommonEnums.SIDE.BUY,
                    SourceId = (Int32)CommonEnums.ORDER_SOURCE.WEB,
                    Symbol = "SAM",
                    Volume = 200
                };
        }

        [WebMethod(Description = "Get count of orders (intra-day) by condition")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetOrderListCount(string sessionId, string accountNo, bool isPending, bool isMatched,
            bool isSemiMatched, bool isCancelling,
            bool isCancelled, bool isRejected)
        {

            var resultObject = new ResultObject<Int32>
            {
                RetCode = CommonEnums.RET_CODE.SUCCESS,
                Result = 35
            };

            return Serializer.Serialize(resultObject);
        }

        [WebMethod(Description = "Get deal information (intra-day) by OrderNo")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetDealDetail(string sessionId, string accountNo, string OrderNo)
        {

            var resultObject = new ResultObject<List<DealInfoDTO>>
                {
                    RetCode = CommonEnums.RET_CODE.SUCCESS,
                    Result =
                        new List<DealInfoDTO>
                            {
                                new DealInfoDTO
                                    {
                                        DealDate = "20101020",
                                        DealPrice = 15.5M,
                                        DealTime = "092200",
                                        DealVolume = 50,
                                        OrderNo = 2,
                                        SumComm = 0,
                                        SumVat = 0
                                    },
                                new DealInfoDTO
                                    {
                                        DealDate = "20101020",
                                        DealPrice = 15.5M,
                                        DealTime = "092250",
                                        DealVolume = 50,
                                        OrderNo = 2,
                                        SumComm = 0,
                                        SumVat = 0
                                    }
                            }
                };

            return Serializer.Serialize(resultObject);
        }

        [WebMethod(Description = "Get cash balance information")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetBalanceInformation(string sessionId, string accountNo)
        {

            var resultObject = new ResultObject<BalanceInfoDTO>
                {
                    RetCode = CommonEnums.RET_CODE.SUCCESS,
                    Result =
                        new BalanceInfoDTO
                            {
                                CanBuy = true,
                                T1 = 0,
                                T2 = 0,
                                T3 = 0,
                                Total = 500000,
                                TotalBuy = 1000,
                                TotalSell = 0,
                                WithDrawable = 2000
                            }
                };

            return Serializer.Serialize(resultObject);
        }

        [WebMethod(Description = "Get portfolio information (stock balance)")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetPortfolio(string sessionId, string accountNo)
        {

            var resultObject = new ResultObject<List<PortfolioDTO>>
            {
                RetCode = CommonEnums.RET_CODE.SUCCESS,
                Result = new List<PortfolioDTO>{
                    new PortfolioDTO
                    {
                        AvgPrice = 16.5M,
                        T1 = 0,
                        T2 = 0,
                        T3 = 0,
                        Total = 500000,
                        CanSell = true,
                        CurrentValue                        = 1500,
                        GainLoss                        = 1000,
                        InvestValue                        = 500,
                        MarketPrice                        = 18,
                        Percent                        = 50,
                        PledgeShare                        = 10,
                        SellableShare                        = 200,
                        Symbol                        = "SAM",
                        WTR                        = 50,
                        WTS                        = 0
                    },
                    new PortfolioDTO
                    {
                        AvgPrice = 16.5M,
                        T1 = 0,
                        T2 = 0,
                        T3 = 0,
                        Total = 500000,
                        CanSell = true,
                        CurrentValue                        = 1500,
                        GainLoss                        = 1000,
                        InvestValue                        = 500,
                        MarketPrice                        = 18,
                        Percent                        = 50,
                        PledgeShare                        = 10,
                        SellableShare                        = 200,
                        Symbol                        = "ASP",
                        WTR                        = 50,
                        WTS                        = 0
                    },

                    new PortfolioDTO
                    {
                        AvgPrice = 16.5M,
                        T1 = 0,
                        T2 = 0,
                        T3 = 0,
                        Total = 500000,
                        CanSell = true,
                        CurrentValue                        = 1500,
                        GainLoss                        = 1000,
                        InvestValue                        = 500,
                        MarketPrice                        = 18,
                        Percent                        = 50,
                        PledgeShare                        = 10,
                        SellableShare                        = 200,
                        Symbol                        = "ACB",
                        WTR                        = 50,
                        WTS                        = 0
                    }
                }

            };

            return Serializer.Serialize(resultObject);
        }

        [WebMethod(Description = "Get order history")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetOrderHistory(string sessionId, string accountNo, int pageSize,
            int pageNumber, int orderStatus, string fromDate, string toDate)
        {

            var resultObject = new ResultObject<List<OrderHistoryDTO>>
                {
                    RetCode = CommonEnums.RET_CODE.SUCCESS,
                    Result =
                        new List<OrderHistoryDTO>
                            {
                                new OrderHistoryDTO
                                    {
                                        AccountNo = accountNo,
                                        Symbol = "SAM",
                                        CancelledId = "",
                                        CancelledTime = "",
                                        CancelledVolume = 0,
                                        Condition = "",
                                        ConditionPrice = "",
                                        EnterId = "",
                                        MatchedPrice = 16,
                                        MatchedVolume = 100,
                                        OrderDate = "20101020",
                                        OrderStatus = (Int32)CommonEnums.ORDER_STATUS.MATCHED,
                                        OrderTime = "092250",
                                        OrderType = ((char)CommonEnums.ORDER_TYPE.MATCHED).ToString(),
                                        OrdSeqNo = "",
                                        Price = 16,
                                        PubVolume = 0,
                                        ServiceType = ((char)CommonEnums.SERVICE_TYPE.WEBTRADE).ToString(),
                                        Side = (char)CommonEnums.SIDE.BUY,
                                        TrusteeId = "",
                                        Volume = 100
                                    },
                                new OrderHistoryDTO
                                    {
                                        AccountNo = accountNo,
                                        Symbol = "ACB",
                                        CancelledId = "",
                                        CancelledTime = "",
                                        CancelledVolume = 0,
                                        Condition = "",
                                        ConditionPrice = "",
                                        EnterId = "",
                                        MatchedPrice = 16,
                                        MatchedVolume = 100,
                                        OrderDate = "20101020",
                                        OrderStatus = (Int32)CommonEnums.ORDER_STATUS.MATCHED,
                                        OrderTime = "092250",
                                        OrderType = ((char)CommonEnums.ORDER_TYPE.CANCELLED).ToString(),
                                        OrdSeqNo = "",
                                        Price = 16,
                                        PubVolume = 0,
                                        ServiceType = ((char)CommonEnums.SERVICE_TYPE.WEBTRADE).ToString(),
                                        Side = (char)CommonEnums.SIDE.BUY,
                                        TrusteeId = "",
                                        Volume = 100
                                    },
                                new OrderHistoryDTO
                                    {
                                        AccountNo = accountNo,
                                        Symbol = "ASP",
                                        CancelledId = "",
                                        CancelledTime = "",
                                        CancelledVolume = 0,
                                        Condition = "",
                                        ConditionPrice = "",
                                        EnterId = "",
                                        MatchedPrice = 16,
                                        MatchedVolume = 100,
                                        OrderDate = "20101020",
                                        OrderStatus = (Int32)CommonEnums.ORDER_STATUS.MATCHED,
                                        OrderTime = "092250",
                                        OrderType = ((char)CommonEnums.ORDER_TYPE.REJECTED).ToString(),
                                        OrdSeqNo = "",
                                        Price = 16,
                                        PubVolume = 0,
                                        ServiceType = ((char)CommonEnums.SERVICE_TYPE.WEBTRADE).ToString(),
                                        Side = (char)CommonEnums.SIDE.BUY,
                                        TrusteeId = "",
                                        Volume = 100
                                    }
                            }
                };

            return Serializer.Serialize(resultObject);
        }
    }
}
