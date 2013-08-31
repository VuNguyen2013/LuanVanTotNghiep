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
    public partial class CompanyInfo
    {
        public CompanyInfo()
        {
            this.HNXStockInfoes = new HashSet<HNXStockInfo>();
            this.HNXStockInfoHists = new HashSet<HNXStockInfoHist>();
            this.HoseStockInfoes = new HashSet<HoseStockInfo>();
            this.HoseStockInfoHists = new HashSet<HoseStockInfoHist>();
            this.Matcheds = new HashSet<Matched>();
            this.StockBalances = new HashSet<StockBalance>();
            this.UpComStockInfoHists = new HashSet<UpComStockInfoHist>();
            this.UpComStockInfoes = new HashSet<UpComStockInfo>();
        }
    
        public string Code { get; set; }
        public string ShortNameVi { get; set; }
        public string ShortNameEn { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public bool IsPublished { get; set; }
    
        public virtual Market Market { get; set; }
        public virtual ICollection<HNXStockInfo> HNXStockInfoes { get; set; }
        public virtual ICollection<HNXStockInfoHist> HNXStockInfoHists { get; set; }
        public virtual ICollection<HoseStockInfo> HoseStockInfoes { get; set; }
        public virtual ICollection<HoseStockInfoHist> HoseStockInfoHists { get; set; }
        public virtual ICollection<Matched> Matcheds { get; set; }
        public virtual ICollection<StockBalance> StockBalances { get; set; }
        public virtual ICollection<UpComStockInfoHist> UpComStockInfoHists { get; set; }
        public virtual ICollection<UpComStockInfo> UpComStockInfoes { get; set; }
    }
    
}