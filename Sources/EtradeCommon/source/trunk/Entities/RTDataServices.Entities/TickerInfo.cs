namespace RTDataServices.Entities
{
    public class TickerInfo
    {
        private System.Int32 _id;
        private System.String _stockSymbol;
        private System.Double _price;
        private System.Double _changed;
        private System.Int32 _vol;
        private System.String _side;
        public virtual System.Double RefPrice { get; set; }
        public virtual System.Double Floor { get; set; }
        public virtual System.Double Ceiling { get; set; }

        public System.Int32 Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public System.String StockSymbol
        {
            get { return _stockSymbol; }
            set { _stockSymbol = value; }
        }

        public System.Double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public System.Double Changed
        {
            get { return _changed; }
            set { _changed = value; }
        }

        public System.Int32 Vol
        {
            get { return _vol; }
            set { _vol = value; }
        }

        public System.String Side
        {
            get { return _side; }
            set { _side = value; }
        }
    }
}