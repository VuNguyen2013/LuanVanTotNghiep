using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETradeWebServices.Entities
{
    public class MMarketStatusAndInfo
    {
        public string MarketId { get; set; }
        public string MarketName { get; set; }
        public double IndexValue { get; set; }
        public double Change { get; set; }
        public double PerChange { get; set; }
        public long Volume { get; set; }
        public double Value { get; set; }
        public short VolUp { get; set; }
        public short VolNoChange { get; set; }
        public int VolNoTrade { get; set; }
        public short VolDown { get; set; }
        public int VolCeilling { get; set; }
        public int VolFloor { get; set; }
        public string MarketStatus { get; set; }
        public string MarketTradingStatus { get; set; }
        public string MarketStatusMessage { get; set; }
        public string MarketOrderSession { get; set; }
        public string MarketAlert { get; set; }
    }
}