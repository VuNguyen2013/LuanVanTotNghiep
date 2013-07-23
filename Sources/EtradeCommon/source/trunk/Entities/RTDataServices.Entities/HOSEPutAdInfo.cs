namespace RTDataServices.Entities
{
    public class HOSEPutAdInfo
    {
        public virtual System.Int32 PutAdId { get; set; }
        public virtual System.DateTime TradeDate { get; set; }
        public virtual System.Int16 TradeID { get; set; }
        public virtual System.Int16 StockNo { get; set; }
        public virtual System.Int32 Vol { get; set; }
        public virtual System.Double Price { get; set; }
        public virtual System.Int32 FirmNo { get; set; }
        public virtual System.String Side { get; set; }
        public virtual System.String Board { get; set; }
        public virtual System.Int32 Time { get; set; }
        public virtual System.String Flag { get; set; }

        public HOSEPutAdInfo()
        {}

        public HOSEPutAdInfo(System.Int32 PutAdId, System.DateTime TradeDate, System.Int16 TradeID, System.Int16 StockNo,
                          System.Int32 Vol, System.Double Price, System.Int32 FirmNo,System.String Side,
                          System.String Board, System.Int32 Time, System.String Flag)
        {
            this.PutAdId    = PutAdId;
            this.TradeDate  = TradeDate;
            this.TradeID    = TradeID;
            this.StockNo    = StockNo;
            this.Vol        = Vol;
            this.Price      = Price;
            this.FirmNo     = FirmNo;
            this.Side       = Side;
            this.Board      = Board;
            this.Time       = Time;
        }
    }
}