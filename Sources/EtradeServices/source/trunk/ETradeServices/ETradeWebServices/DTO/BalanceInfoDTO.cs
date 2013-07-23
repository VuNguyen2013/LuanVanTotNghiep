// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BakanceInfoDTO.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the BakanceInfoDTO type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeServices.DTO
{
    public class BalanceInfoDTO
    {
        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>The total.</value>
        public System.Decimal Total { get; set; }

        /// <summary>
        /// Gets or sets the with drawable.
        /// </summary>
        /// <value>The with drawable.</value>
        public System.Decimal WithDrawable { get; set; }

        /// <summary>
        /// Gets or sets the t1.
        /// </summary>
        /// <value>The t1.</value>
        public System.Decimal T1 { get; set; }

        /// <summary>
        /// Gets or sets the t2.
        /// </summary>
        /// <value>The t2.</value>
        public System.Decimal T2 { get; set; }

        /// <summary>
        /// Gets or sets the t3.
        /// </summary>
        /// <value>The t3.</value>
        public System.Decimal T3 { get; set; }

        /// <summary>
        /// Gets or sets the total buy.
        /// </summary>
        /// <value>The total buy.</value>
        public System.Decimal TotalBuy { get; set; }

        /// <summary>
        /// Gets or sets the total sell.
        /// </summary>
        /// <value>The total sell.</value>
        public System.Decimal TotalSell { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can buy.
        /// </summary>
        /// <value><c>true</c> if this instance can buy; otherwise, <c>false</c>.</value>
        public System.Boolean CanBuy { get; set; }
    }
}