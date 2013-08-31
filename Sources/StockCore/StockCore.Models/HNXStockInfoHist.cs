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
    public partial class HNXStockInfoHist
    {
        public long id { get; set; }
        public System.DateTime TradeDate { get; set; }
        public long Ceiling { get; set; }
        public long Floor { get; set; }
        public long AvrPrice { get; set; }
        public long Last { get; set; }
        public long LastVol { get; set; }
        public long LastVal { get; set; }
        public long Highest { get; set; }
        public long Lowest { get; set; }
        public long Totalshares { get; set; }
        public long TotalValue { get; set; }
        public long Best1Bid { get; set; }
        public long Best1BidVolume { get; set; }
        public long Best2Bid { get; set; }
        public long Best2BidVolume { get; set; }
        public long Best3Bid { get; set; }
        public long Best3BidVolume { get; set; }
        public long Best1Offer { get; set; }
        public long Best1OfferVolume { get; set; }
        public long Best2Offer { get; set; }
        public long Best2OfferVolume { get; set; }
        public long Best3Offer { get; set; }
        public long Best3OfferVolume { get; set; }
        public long SELL_FOREIGN_QTTY { get; set; }
        public long BUY_FOREIGN_QTTY { get; set; }
    
        public virtual CompanyInfo CompanyInfo { get; set; }
    }
    
}