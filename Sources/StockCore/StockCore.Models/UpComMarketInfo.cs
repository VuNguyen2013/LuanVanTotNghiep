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
    public partial class UpComMarketInfo
    {
        public long id { get; set; }
        public System.DateTime TradeDate { get; set; }
        public double SetIndex { get; set; }
        public long TotalTrade { get; set; }
        public long Totalshare { get; set; }
        public long TotalValue { get; set; }
        public short Advances { get; set; }
        public short Nochange { get; set; }
        public short Declines { get; set; }
        public double OpenIndex { get; set; }
        public string Status { get; set; }
    
        public virtual Market Market { get; set; }
    }
    
}
