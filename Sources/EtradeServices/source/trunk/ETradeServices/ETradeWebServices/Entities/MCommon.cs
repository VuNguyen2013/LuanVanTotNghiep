using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETradeWebServices.Entities
{
    public class MCommon
    {
        
        public static string GetMarketName(int marketId)
        {
            if (marketId == (int)ETradeCommon.Enums.CommonEnums.MARKET_ID.HOSE)
            {
                return "HOSE";
            }
            if (marketId == (int)ETradeCommon.Enums.CommonEnums.MARKET_ID.HNX)
            {
                return "HNX";
            }
            if (marketId == (int)ETradeCommon.Enums.CommonEnums.MARKET_ID.UPCoM)
            {
                return "UPCOM";
            }
            return string.Empty;
        }
        public static string GetMarketAlertFromOrderSession(int marketId, char orderSession)
        {
            string marketName = GetMarketName(marketId);
            switch (marketId)
            {
                case (int)ETradeCommon.Enums.CommonEnums.MARKET_ID.HOSE:
                    switch (orderSession)
                    {
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION0:
                            return string.Format(Resources.Resource.MarketAlert_NotReady, new string[] { marketName });
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION1:
                            return string.Format(Resources.Resource.MarketAlert_Ready, new string[] { marketName });
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION2:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION3:
                            return string.Format(Resources.Resource.MarketAlert_RejectATO, new string[] { marketName });
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION4:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION5:
                            return string.Format(Resources.Resource.MarketAlert_RejectResttime, new string[] { marketName });
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION6:
                            return string.Format(Resources.Resource.MarketAlert_Ready, new string[] { marketName });
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION7:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION8:
                            return string.Format(Resources.Resource.MarketAlert_RejectATC, new string[] { marketName });
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION9:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION10:
                            return string.Format(Resources.Resource.MarketAlert_RejectMatched, new string[] { marketName });
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION11:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION12:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION13:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION14:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION15:
                            return string.Empty;
                        default: return string.Empty;
                    }
                case (int)ETradeCommon.Enums.CommonEnums.MARKET_ID.HNX:
                    switch (orderSession)
                    {
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION0:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION1:
                            return string.Format(Resources.Resource.MarketAlert_Ready, new string[] { marketName });
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION2:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION3:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION4:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION5:
                            return string.Format(Resources.Resource.MarketAlert_RejectResttime, new string[] { marketName });
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION6:
                            return string.Format(Resources.Resource.MarketAlert_Ready, new string[] { marketName });
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION7:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION8:
                            return string.Format(Resources.Resource.MarketAlert_RejectATC, new string[] { marketName });
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION9:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION10:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION11:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION12:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION13:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION14:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION15:
                            return string.Empty;
                        default: return string.Empty;
                    }
                case (int)ETradeCommon.Enums.CommonEnums.MARKET_ID.UPCoM:
                    switch (orderSession)
                    {
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION0:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION1:
                            return string.Format(Resources.Resource.MarketAlert_Ready, new string[] { marketName });
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION2:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION3:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION4:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION5:
                            return string.Format(Resources.Resource.MarketAlert_RejectResttime, new string[] { marketName });
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION6:
                            return string.Format(Resources.Resource.MarketAlert_Ready, new string[] { marketName });
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION7:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION8:
                            return string.Format(Resources.Resource.MarketAlert_RejectATC, new string[] { marketName });
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION9:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION10:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION11:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION12:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION13:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION14:
                            return string.Empty;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION15:
                            return string.Empty;
                        default: return string.Empty;
                    }
                default: return string.Empty;
            }
        }
            public static string GetMarketStatusFromOrderSession(int marketId, char orderSession)
            {
            switch (marketId)
            {
                case (int)ETradeCommon.Enums.CommonEnums.MARKET_ID.HOSE:
                    switch (orderSession)
                    {
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION0:
                            return Resources.Resource.MarketStatus_Unavailable;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION1:
                            return Resources.Resource.MarketStatus_Ready;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION2:
                            return Resources.Resource.MarketStatus_Session1;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION3:
                            return Resources.Resource.MarketStatus_Session1;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION4:
                            return Resources.Resource.MarketStatus_Session2;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION5:
                            return Resources.Resource.MarketStatus_Session2;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION6:
                            return Resources.Resource.MarketStatus_Ready;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION7:
                            return Resources.Resource.MarketStatus_Session2;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION8:
                            return Resources.Resource.MarketStatus_Session3;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION9:
                            return Resources.Resource.MarketStatus_Session3;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION10:
                            return Resources.Resource.MarketStatus_Deal;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION11:
                            return Resources.Resource.MarketStatus_Deal;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION12:
                            return Resources.Resource.MarketStatus_Close;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION13:
                            return Resources.Resource.MarketStatus_Close;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION14:
                            return Resources.Resource.MarketStatus_Unavailable;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION15:
                            return Resources.Resource.MarketStatus_Unavailable;
                        default: return Resources.Resource.MarketStatus_Unavailable;
                    }
                case (int)ETradeCommon.Enums.CommonEnums.MARKET_ID.HNX:
                    switch (orderSession)
                    {
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION0:
                            return Resources.Resource.MarketStatus_Unavailable;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION1:
                            return Resources.Resource.MarketStatus_Ready;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION2:
                            return Resources.Resource.MarketStatus_Open;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION3:
                            return Resources.Resource.MarketStatus_Open;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION4:
                            return Resources.Resource.MarketStatus_Open;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION5:
                            return Resources.Resource.MarketStatus_Open;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION6:
                            return Resources.Resource.MarketStatus_Ready;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION7:
                            return Resources.Resource.MarketStatus_Open;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION8:
                            return Resources.Resource.MarketStatus_Open;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION9:
                            return Resources.Resource.MarketStatus_Session3;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION10:
                            return Resources.Resource.MarketStatus_Session3;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION11:
                            return Resources.Resource.MarketStatus_Close;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION12:
                            return Resources.Resource.MarketStatus_Close;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION13:
                            return Resources.Resource.MarketStatus_Close;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION14:
                            return Resources.Resource.MarketStatus_CloseAfter;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION15:
                            return Resources.Resource.MarketStatus_Close;
                        default: return Resources.Resource.MarketStatus_Unavailable;
                    }
                case (int)ETradeCommon.Enums.CommonEnums.MARKET_ID.UPCoM:
                    switch (orderSession)
                    {
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION0:
                            return Resources.Resource.MarketStatus_Unavailable;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION1:
                            return Resources.Resource.MarketStatus_Ready;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION2:
                            return Resources.Resource.MarketStatus_Open;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION3:
                            return Resources.Resource.MarketStatus_Open;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION4:
                            return Resources.Resource.MarketStatus_Open;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION5:
                            return Resources.Resource.MarketStatus_Open;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION6:
                            return Resources.Resource.MarketStatus_Ready;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION7:
                            return Resources.Resource.MarketStatus_Open;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION8:
                            return Resources.Resource.MarketStatus_Open;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION9:
                            return Resources.Resource.MarketStatus_Session3;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION10:
                            return Resources.Resource.MarketStatus_Session3;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION11:
                            return Resources.Resource.MarketStatus_Close;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION12:
                            return Resources.Resource.MarketStatus_Close;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION13:
                            return Resources.Resource.MarketStatus_Close;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION14:
                            return Resources.Resource.MarketStatus_CloseAfter;
                        case (char)ETradeCommon.Enums.CommonEnums.ORDER_SESSION.SESSION15:
                            return Resources.Resource.MarketStatus_Close;
                        default: return Resources.Resource.MarketStatus_Unavailable;
                    }
                default: return string.Empty;
            }
        
        }
            public static string GetMarketStatus(int marketId, char statusId)
            {
                switch (marketId)
                {
                    case (int)ETradeCommon.Enums.CommonEnums.MARKET_ID.HOSE:
                        switch (statusId)
                        {
                            case (char)ETradeCommon.Enums.CommonEnums.MARKET_STATUS.PRE_OPEN:
                                return Resources.Resource.MarketStatus_Session1;
                            case (char)ETradeCommon.Enums.CommonEnums.MARKET_STATUS.OPEN:
                                return Resources.Resource.MarketStatus_Session2;
                            case (char)ETradeCommon.Enums.CommonEnums.MARKET_STATUS.PRE_CLOSE:
                                return Resources.Resource.MarketStatus_Session3;
                            case (char)ETradeCommon.Enums.CommonEnums.MARKET_STATUS.CLOSE:
                                return Resources.Resource.MarketStatus_Deal;
                            case (char)ETradeCommon.Enums.CommonEnums.MARKET_STATUS.CLOSE_PT:
                                return Resources.Resource.MarketStatus_Close;
                            case (char)ETradeCommon.Enums.CommonEnums.MARKET_STATUS.READY:
                                return Resources.Resource.MarketStatus_Close;
                            default: return "---";
                        }
                    case (int)ETradeCommon.Enums.CommonEnums.MARKET_ID.HNX:
                        switch (statusId)
                        {
                            case (char)ETradeCommon.Enums.CommonEnums.MARKET_STATUS.OPEN:
                                return Resources.Resource.MarketStatus_Open;
                            case (char)ETradeCommon.Enums.CommonEnums.MARKET_STATUS.CLOSE:
                                return Resources.Resource.MarketStatus_Close;
                            case (char)ETradeCommon.Enums.CommonEnums.MARKET_STATUS.READY:
                                return Resources.Resource.MarketStatus_Close;
                            default: return "---";
                        }
                    case (int)ETradeCommon.Enums.CommonEnums.MARKET_ID.UPCoM:
                        switch (statusId)
                        {
                            case (char)ETradeCommon.Enums.CommonEnums.MARKET_STATUS.OPEN:
                                return Resources.Resource.MarketStatus_Open;
                            case (char)ETradeCommon.Enums.CommonEnums.MARKET_STATUS.OPEN_2:
                                return Resources.Resource.MarketStatus_Open;
                            case (char)ETradeCommon.Enums.CommonEnums.MARKET_STATUS.CLOSE:
                                return Resources.Resource.MarketStatus_Close;
                            case (char)ETradeCommon.Enums.CommonEnums.MARKET_STATUS.HAFT:
                                return Resources.Resource.MarketStatus_Close;
                            case (char)ETradeCommon.Enums.CommonEnums.MARKET_STATUS.READY:
                                return Resources.Resource.MarketStatus_Close;
                            default: return "---";
                        }
                    default: return string.Empty;
                }
            }
    }
}