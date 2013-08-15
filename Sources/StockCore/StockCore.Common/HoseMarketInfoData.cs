using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockCore.Common
{
    public class HoseMarketInfoData
    {
        public long id { get; set; }
        public System.DateTime TradeDate { get; set; }
        public long SetIndex { get; set; }
        public long TotalTrade { get; set; }
        public long Totalshare { get; set; }
        public long TotalValue { get; set; }
        public long UpVolume { get; set; }
        public long DownVolume { get; set; }
        public long NoChangeVolume { get; set; }
        public long Advances { get; set; }
        public long Declines { get; set; }
        public long Nochange { get; set; }
        public string Status { get; set; }
    }
}
