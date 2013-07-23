using ETradeCommon.Enums;

namespace ETradeCommon
{
    /// <summary>
    /// Class provide common APIs that are relate to orders. EtradeWEbUI and EtradeWebServices use this class
    /// </summary>
    public static class OrderUtils
    {

        /// <summary>
        /// 
        /// </summary>
        public const int SOURCE_ORDER_POS = 54;

        /// <summary>
        /// User Posfix of Reforder ID to find which application sent order. 
        /// </summary>
        /// <param name="refOrderID"></param>
        /// <returns></returns>
        static public CommonEnums.ORDER_SOURCE GetOrderSource(string refOrderID)
        {
            string sourceID = refOrderID.Substring(SOURCE_ORDER_POS, 2);

            switch (sourceID)
            {
                case "00":
                    return CommonEnums.ORDER_SOURCE.FROM_IFIS_BROKER;
                case "01":
                    return CommonEnums.ORDER_SOURCE.FROM_WEB;
                case "02":
                    return CommonEnums.ORDER_SOURCE.FROM_IVR;
                case "03":
                    return CommonEnums.ORDER_SOURCE.FROM_SMS;
                default:
                    return CommonEnums.ORDER_SOURCE.FROM_IFIS_BROKER;
            }
        }

        /// <summary>
        /// Orders the session.
        /// </summary>
        /// <param name="marketStatus">The market status.</param>
        /// <param name="tradingStatus">The trading status.</param>
        /// <returns>CommonEnums.ORDER_SESSION</returns>
        static public CommonEnums.ORDER_SESSION OrderSession(CommonEnums.MARKET_STATUS marketStatus, CommonEnums.MARKET_STATUS tradingStatus)
        {
            if ((marketStatus == CommonEnums.MARKET_STATUS.READY || marketStatus == CommonEnums.MARKET_STATUS.UNVAILABLE) && tradingStatus == CommonEnums.MARKET_STATUS.READY)
            {
                return CommonEnums.ORDER_SESSION.SESSION1;
            }

            if (marketStatus == CommonEnums.MARKET_STATUS.PRE_OPEN && tradingStatus == CommonEnums.MARKET_STATUS.PRE_OPEN)
            {
                return CommonEnums.ORDER_SESSION.SESSION2;
            }

            if (marketStatus == CommonEnums.MARKET_STATUS.PRE_OPEN && tradingStatus == CommonEnums.MARKET_STATUS.OPEN)
            {
                return CommonEnums.ORDER_SESSION.SESSION3;
            }

            if (marketStatus == CommonEnums.MARKET_STATUS.OPEN && tradingStatus == CommonEnums.MARKET_STATUS.OPEN)
            {
                return CommonEnums.ORDER_SESSION.SESSION4;
            }

            if (marketStatus == CommonEnums.MARKET_STATUS.PRE_CLOSE && tradingStatus == CommonEnums.MARKET_STATUS.PRE_CLOSE)
            {
                return CommonEnums.ORDER_SESSION.SESSION5;
            }

            if (marketStatus == CommonEnums.MARKET_STATUS.PRE_CLOSE && tradingStatus == CommonEnums.MARKET_STATUS.CLOSE ||
                marketStatus == CommonEnums.MARKET_STATUS.OPEN && tradingStatus == CommonEnums.MARKET_STATUS.CLOSE) //HNX
            {
                return CommonEnums.ORDER_SESSION.SESSION6;
            }

            if (marketStatus == CommonEnums.MARKET_STATUS.CLOSE && tradingStatus == CommonEnums.MARKET_STATUS.CLOSE)
            {
                return CommonEnums.ORDER_SESSION.SESSION7;
            }

            if (marketStatus == CommonEnums.MARKET_STATUS.CLOSE_PT && tradingStatus == CommonEnums.MARKET_STATUS.CLOSE_PT)
            {
                return CommonEnums.ORDER_SESSION.SESSION8;
            }

            return CommonEnums.ORDER_SESSION.SESSION0;
        }
    }
}
