namespace RTDataServices.Entities
{
    public class UPCOMStockInfo
    {
        public virtual System.Int32 HNXStockInfoId { get; set; }
        public virtual System.Int64 NMTotalTradedQtty { get; set; }
        public virtual System.Int64 NMTotalTradedValue { get; set; }
        public virtual System.Int32 SellCount { get; set; }
        public virtual System.Int32 BuyCount { get; set; }
        public virtual System.Int64 TotalBidQtty { get; set; }
        public virtual System.Int64 TotalSellTradingQtty { get; set; }
        public virtual System.Int64 TotalOfferQtty { get; set; }
        public virtual System.Int64 TotalBuyTradingQtty { get; set; }
        public virtual System.Int32 BidCount { get; set; }
        public virtual System.Int32 OfferCount { get; set; }
        public virtual System.Int64 BuyForeignQtty { get; set; }
        public virtual System.Int64 BuyForeignValue { get; set; }
        public virtual System.Int64 SellForeignQtty { get; set; }
        public virtual System.Int64 SellForeignValue { get; set; }
        public virtual System.Int64 RemainForeignQtty { get; set; }
        public virtual System.Double PTMatchingPrice { get; set; }
        public virtual System.Int64 PTMatchingQtty { get; set; }
        public virtual System.Int64 PTTotalTradedQtty { get; set; }
        public virtual System.Int64 PTTotalTradedValue { get; set; }
        public virtual System.Int64 TotalListingQtty { get; set; }
        public virtual StockInfo StockInfo { get; set; }
    }
}