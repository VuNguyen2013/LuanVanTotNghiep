using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ETradeRTServicesMock
{
    using System.Web.Script.Serialization;
    using System.Web.Script.Services;

    using ETradeCommon;
    using ETradeCommon.Enums;

    using ETradeRTServicesMock.DTO;

    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {
        private static readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();

        [WebMethod(Description = "Get stock information dynamic")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetStockInfo(string sessionId, string symbol)
        {

            var resultObject = new ResultObject<StockInfoDTO>
                {
                    RetCode = CommonEnums.RET_CODE.SUCCESS,
                    Result =
                        new StockInfoDTO
                            {
                                BestBidPrice1 = 11,
                                BestBidPrice2 = 12,
                                BestBidPrice3 = 13,
                                BestBidVol1 = 10,
                                BestBidVol2 = 20,
                                BestBidVol3 = 30,
                                BestOfferPrice1 = 11,
                                BestOfferPrice2 = 12,
                                BestOfferPrice3 = 13,
                                BestOfferVol1 = 10,
                                BestOfferVol2 = 20,
                                BestOfferVol3 = 30,
                                Ceiling = 15,
                                Change = 15,
                                Floor = 10,
                                High = 14,
                                IsBond = false,
                                IsExistedTransaction = false,
                                IsHalt = false,
                                LastSalePrice = 15,
                                LastSaleVolume = 50,
                                Low = 12,
                                Open = 13,
                                Prior = 13,
                                ProjectOpen = 14,
                                RemainRoom = 500,
                                SecName = "Công ty cổ phần ACB",
                                SecType = (Int32)CommonEnums.SEC_TYPE.SELLABLE_SHARE,
                                Value = 100,
                                Volume = 20
                            }
                };

            return Serializer.Serialize(resultObject);
        }

        [WebMethod(Description = "Get stock information static")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetFixStockInfo(string sessionId, string symbol)
        {

            var resultObject = new ResultObject<FixStockInfoDTO>
            {
                RetCode = CommonEnums.RET_CODE.SUCCESS,
                Result =
                    new FixStockInfoDTO
                    {
                        Ceiling = 15,
                        Floor = 10,
                        IsHalted = false,
                        Reference = 13
                    }
            };

            return Serializer.Serialize(resultObject);
        }

        [WebMethod(Description = "Get list of company for auto-complete")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListCompany(string sessionId, int languageId)
        {

            var resultObject = new ResultObject<List<CompanyDTO>>
            {
                RetCode = CommonEnums.RET_CODE.SUCCESS,
                Result = new List<CompanyDTO>{
                    new CompanyDTO
                    {
                        CompanyCode = "SAM",
                        CompanyName = "Cong ty cổ phần SAM",
                        MarketId = CommonEnums.CENTER_TYPE.HOSE.ToString(),
                    },

                    new CompanyDTO
                    {
                        CompanyCode = "ACB",
                        CompanyName = "Cong ty cổ phần ACB",
                        MarketId = CommonEnums.CENTER_TYPE.HNX.ToString(),
                    }
                }
            };

            return Serializer.Serialize(resultObject);
        }
    }
}
