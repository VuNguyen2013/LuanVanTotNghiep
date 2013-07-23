namespace RTDataServices.Entities
{
    public class HOSETransferedSessionInfo
    {

        public virtual System.Int32 TransferedSessionInfoId { get; set; }
        public virtual System.DateTime TradeDate { get; set; }
        public virtual System.Int16 StockNo { get; set; }
        public virtual System.Double Price { get; set; }
        public virtual System.Int32 AccumulatedVol { get; set; }
        public virtual System.Int64 AccumulatedVal { get; set; }
        public virtual System.Double Highest { get; set; }
        public virtual System.Double Lowest { get; set; }
        public virtual System.Int32 Session { get; set; }
        public virtual System.Int32 Time { get; set; }

        public HOSETransferedSessionInfo()
        {}

        public HOSETransferedSessionInfo(System.Int32 TransferedSessionInfoId, System.DateTime TradeDate, System.Int16 StockNo,
                               System.Double Price, System.Int32 AccumulatedVol,System.Int64 AccumulatedVal,
                               System.Double Highest, System.Double Lowest, System.Int32 Session, System.Int32 Time)
        {
            this.TransferedSessionInfoId = TransferedSessionInfoId;
            this.TradeDate               = TradeDate;
            this.StockNo                 = StockNo;
            this.Price                   = Price;
            this.AccumulatedVol          = AccumulatedVol;
            this.AccumulatedVal          = AccumulatedVal;
            this.Highest                 = Highest;
            this.Lowest                  = Lowest;
            this.Session                 = Session;
            this.Time                    = Time;
        }
    }
}