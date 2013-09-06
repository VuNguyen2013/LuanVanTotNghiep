//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace StockCore.Models
{
    public partial class HoseMarketInfo
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
        public Nullable<int> MarketId { get; set; }
        public string Status { get; set; }
    
        public virtual Market Market { get; set; }
    }
    
}
