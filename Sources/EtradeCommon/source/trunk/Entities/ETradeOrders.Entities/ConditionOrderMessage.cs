namespace ETradeOrders.Entities
{
    public class ConditionOrderMessage
    {
        public long ConditionOrderId { get; set; }

        public string MainCustAccountId { get; set; }

        public string SubCustAccountId { get; set; }

        public int MarketId { get; set; }

        public string SecSymbol { get; set; }

        public int Volume { get; set; }

        public decimal Price { get; set; }

        public char Side { get; set; }
    }
}
