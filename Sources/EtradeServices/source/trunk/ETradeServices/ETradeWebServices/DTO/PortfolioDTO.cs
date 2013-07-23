// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PortfolioDTO.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the PortfolioDTO type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeServices.DTO
{
    public class PortfolioDTO
    {
        /// <summary>
        /// Gets or sets the symbol.
        /// </summary>
        /// <value>The symbol.</value>
        public System.String Symbol { get; set; }

        /// <summary>
        /// Gets or sets the sellable share.
        /// </summary>
        /// <value>The sellable share.</value>
        public System.Int64 SellableShare { get; set; }

        /// <summary>
        /// Gets or sets the pledge share.
        /// </summary>
        /// <value>The pledge share.</value>
        public System.Int64 PledgeShare { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>The total.</value>
        public System.Int64 Total { get; set; }

        /// <summary>
        /// Gets or sets the t1.
        /// </summary>
        /// <value>The t1.</value>
        public System.Int64 T1 { get; set; }

        /// <summary>
        /// Gets or sets the t2.
        /// </summary>
        /// <value>The t2.</value>
        public System.Int64 T2 { get; set; }

        /// <summary>
        /// Gets or sets the t3.
        /// </summary>
        /// <value>The t3.</value>
        public System.Int64 T3 { get; set; }

        /// <summary>
        /// Gets or sets the WTS.
        /// </summary>
        /// <value>The WTS.</value>
        public System.Int64 WTS { get; set; }

        /// <summary>
        /// Gets or sets the WTR.
        /// </summary>
        /// <value>The WTR.</value>
        public System.Int64 WTR { get; set; }

        /// <summary>
        /// Gets or sets the avg price.
        /// </summary>
        /// <value>The avg price.</value>
        public System.Decimal AvgPrice { get; set; }

        /// <summary>
        /// Gets or sets the market price.
        /// </summary>
        /// <value>The market price.</value>
        public System.Decimal MarketPrice { get; set; }

        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
        /// <value>The current value.</value>
        public System.Decimal CurrentValue { get; set; }

        /// <summary>
        /// Gets or sets the invest value.
        /// </summary>
        /// <value>The invest value.</value>
        public System.Decimal InvestValue { get; set; }

        /// <summary>
        /// Gets or sets the gain loss.
        /// </summary>
        /// <value>The gain loss.</value>
        public System.Decimal GainLoss { get; set; }

        /// <summary>
        /// Gets or sets the percent.
        /// </summary>
        /// <value>The percent.</value>
        public System.Decimal Percent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can sell.
        /// </summary>
        /// <value><c>true</c> if this instance can sell; otherwise, <c>false</c>.</value>
        public System.Boolean CanSell { get; set; }
    }
}