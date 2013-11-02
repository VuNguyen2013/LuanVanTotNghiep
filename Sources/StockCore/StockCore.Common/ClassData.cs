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
    public class HNXMarketInfoData
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
    }
    public class UpComMarketInfoData
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
    }
    public class HoseStockInfoData
    {
        public long id { get; set; }
        public string StockSymbol { get; set; }
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
        public long CurrentRoom { get; set; }
        public long StartRoom { get; set; }
    }
    public class HNXStockInfoData
    {
        public long id { get; set; }
        public string StockSymbol { get; set; }
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
    }
    public class UpComStockInfoData
    {
        public long id { get; set; }
        public string StockSymbol { get; set; }
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
    }
    public class CashBalanceData       
    {
        public long WithDraw { get; set; }
        public long BuyCredit { get; set; }
        public long TotalBuy { get; set; }
        public long TotalSell{get;set;}
        public long WTR_T1 { get; set; }
        public long WTR_T2 { get; set; }
    }
    public class StockBalanceData
    {
        public string SubCustAccountID { get; set; }
        public string StockSymbol { get; set; }
        public long Available { get; set; }
        public long Total { get; set; }
        public long WTR_T1 { get; set; }
        public long WTR_T2 { get; set; }
        public long WTS_T1 { get; set; }
        public long WTS_T2 { get; set; }
    }
    public class StockInfoCache
    {
        public byte MarketID { get; set; }
        public string Symbol { get; set; }
        public long Floor { get; set; }
        public long Ceil { get; set; }
        public int Volume { get; set; }
    }
}
