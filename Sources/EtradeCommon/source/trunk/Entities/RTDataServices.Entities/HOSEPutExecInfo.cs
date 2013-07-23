namespace RTDataServices.Entities
{
    public class HOSEPutExecInfo
    {
        public virtual System.Int32 PutExecId { get; set; }
        public virtual System.DateTime TradeDate { get; set; }
        public virtual System.Int32 ConfirmNo { get; set; }
        public virtual System.Int16 StockNo { get; set; }
        public virtual System.Int32 Vol { get; set; }
        public virtual System.Double Price { get; set; }
        public virtual System.String Board { get; set; }

        public HOSEPutExecInfo()
        {}

        public HOSEPutExecInfo(System.Int32 PutExecId, System.DateTime TradeDate, System.Int32 ConfirmNo, 
                               System.Int16 StockNo, System.Int32 Vol, System.Double Price, System.String Board)
        {
            this.PutExecId  = PutExecId;
            this.TradeDate  = TradeDate;
            this.ConfirmNo  = ConfirmNo;
            this.StockNo    = StockNo;
            this.Vol        = Vol;
            this.Price      = Price;
            this.Board      = Board;
        }
    }
}