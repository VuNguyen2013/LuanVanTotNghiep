// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActualTrade.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the ActualTrade type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCore.Entities
{
    public class ActualTrade
    {
        /// <summary>
        /// Gets or sets the customer no.
        /// </summary>
        /// <value>The customer no.</value>
        public System.String CustomerNo { get; set; }

        /// <summary>
        /// Gets or sets the trade date.
        /// </summary>
        /// <value>The trade date.</value>
        public System.DateTime TradeDate { get; set; }

        /// <summary>
        /// Gets or sets the symbol.
        /// </summary>
        /// <value>The symbol.</value>
        public System.String Symbol { get; set; }

        /// <summary>
        /// Gets or sets the con price.
        /// </summary>
        /// <value>The con price.</value>
        public System.String ConPrice { get; set; }

        /// <summary>
        /// Gets or sets the side.
        /// </summary>
        /// <value>The side.</value>
        public System.String Side { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>The volume.</value>
        public System.Decimal Volume { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>The price.</value>
        public System.Decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the commission.
        /// </summary>
        /// <value>The commission.</value>
        public System.Decimal Commission { get; set; }

        /// <summary>
        /// Gets or sets the settlement date.
        /// </summary>
        /// <value>The settlement date.</value>
        public System.DateTime SettlementDate { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>The amount.</value>
        public System.Decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the PIT.
        /// </summary>
        /// <value>The PIT.</value>
        public System.Decimal PIT { get; set; }
    }
}