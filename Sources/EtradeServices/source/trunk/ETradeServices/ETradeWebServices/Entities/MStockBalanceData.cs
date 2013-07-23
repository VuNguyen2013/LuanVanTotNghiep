using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETradeWebServices.Entities
{
    public class MStockBalanceData
    {
         public string Symbol{get;set;} 
         public decimal SellableShare{get;set;}
         public double Pledge { get; set; }
         public double Limit { get; set; }
         public decimal Total { get; set; }
         public decimal WTR_T3 { get; set; }
         public decimal WTR_T2 { get; set; }
         public decimal WTR_T1 { get; set; }
         public decimal WTS_T3 { get; set; }
         public decimal WTS_T2 { get; set; }
         public decimal WTS_T1 { get; set; }
         public decimal WTR { get; set; }
         public decimal WTS { get; set; }
         public decimal AvgPrice { get; set; } 
         public double RefPrice{get;set;} 
         public double FloorPrice{get;set;} 
         public double CeillingPrice{get;set;} 
         public double CurrentPrice{get;set;} 
         public double CurrentChange{get;set;}
         public decimal InvestValue { get; set; }
         public decimal CurrentValue { get; set; }
         public decimal GainLostToday { get; set; }
         public decimal GainLoss { get; set; }
         public decimal Percent { get; set; } 
         public bool CanSell{get;set;}
         public bool CanBuy{get;set;}
         public string ErrorMessage{get;set;} 
         public double Best1Bid{get;set;} 
         public double Best1Offer{get;set;} 
         public decimal MarketPrice{get;set;} 
    }
}