// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderHistoryDTO.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the OrderHistoryDTO type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeServicesMock.DTO
{
    public class OrderHistoryDTO
    {
        /// <summary>
        /// Gets or sets the account no.
        /// </summary>
        /// <value>The account no.</value>
        public System.String AccountNo { get; set; }

        /// <summary>
        /// Gets or sets the symbol.
        /// </summary>
        /// <value>The symbol.</value>
        public System.String Symbol { get; set; }

        /// <summary>
        /// Gets or sets the trustee id.
        /// </summary>
        /// <value>The trustee id.</value>
        public System.String TrusteeId { get; set; }

        /// <summary>
        /// Gets or sets the side.
        /// </summary>
        /// <value>The side.</value>
        public System.Char Side { get; set; }

        /// <summary>
        /// Gets or sets the condition.
        /// </summary>
        /// <value>The condition.</value>
        public System.String Condition { get; set; }

        /// <summary>
        /// Gets or sets the condition price.
        /// </summary>
        /// <value>The condition price.</value>
        public System.String ConditionPrice { get; set; }

        /// <summary>
        /// Gets or sets the type of the order.
        /// </summary>
        /// <value>The type of the order.</value>
        public System.String OrderType { get; set; }

        /// <summary>
        /// Gets or sets the enter id.
        /// </summary>
        /// <value>The enter id.</value>
        public System.String EnterId { get; set; }

        /// <summary>
        /// Gets or sets the order date.
        /// </summary>
        /// <value>The order date.</value>
        public System.String OrderDate { get; set; }

        /// <summary>
        /// Gets or sets the order time.
        /// </summary>
        /// <value>The order time.</value>
        public System.String OrderTime { get; set; }

        /// <summary>
        /// Gets or sets the cancelled id.
        /// </summary>
        /// <value>The cancelled id.</value>
        public System.String CancelledId { get; set; }

        /// <summary>
        /// Gets or sets the cancelled time.
        /// </summary>
        /// <value>The cancelled time.</value>
        public System.String CancelledTime { get; set; }

        /// <summary>
        /// Gets or sets the type of the service.
        /// </summary>
        /// <value>The type of the service.</value>
        public System.String ServiceType { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>The price.</value>
        public System.Decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>The volume.</value>
        public System.Int64 Volume { get; set; }

        /// <summary>
        /// Gets or sets the pub volume.
        /// </summary>
        /// <value>The pub volume.</value>
        public System.Int64 PubVolume { get; set; }

        /// <summary>
        /// Gets or sets the order status.
        /// </summary>
        /// <value>The order status.</value>
        public System.Int32 OrderStatus { get; set; }

        /// <summary>
        /// Gets or sets the matched volume.
        /// </summary>
        /// <value>The matched volume.</value>
        public System.Int64 MatchedVolume { get; set; }

        /// <summary>
        /// Gets or sets the matched price.
        /// </summary>
        /// <value>The matched price.</value>
        public System.Decimal MatchedPrice { get; set; }

        /// <summary>
        /// Gets or sets the cancelled volume.
        /// </summary>
        /// <value>The cancelled volume.</value>
        public System.Int64 CancelledVolume { get; set; }

        /// <summary>
        /// Gets or sets the ord seq no.
        /// </summary>
        /// <value>The ord seq no.</value>
        public System.String OrdSeqNo { get; set; }
    }
}