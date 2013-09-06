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
    public partial class SubCustAccount
    {
        public SubCustAccount()
        {
            this.Orders = new HashSet<Order>();
            this.StockBalances = new HashSet<StockBalance>();
            this.CashTempDeductions = new HashSet<CashTempDeduction>();
            this.StockTempDeductions = new HashSet<StockTempDeduction>();
        }
    
        public string SubCustAccountID { get; set; }
        public string Name { get; set; }
        public Nullable<bool> Actived { get; set; }
        public Nullable<short> LockAccountReason { get; set; }
        public string MainCustAccountID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public long WithDraw { get; set; }
        public long BuyCredit { get; set; }
        public long TotalBuy { get; set; }
        public long TotalSell { get; set; }
    
        public virtual MainCustAccount MainCustAccount { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<StockBalance> StockBalances { get; set; }
        public virtual ICollection<CashTempDeduction> CashTempDeductions { get; set; }
        public virtual ICollection<StockTempDeduction> StockTempDeductions { get; set; }
    }
    
}
