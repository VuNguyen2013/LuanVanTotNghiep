// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XR.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the XR type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCore.Entities
{
    public class XR
    {
        /// <summary>
        /// Gets or sets the symbol.
        /// </summary>
        /// <value>The symbol.</value>
        public System.String Symbol { get; set; }

        /// <summary>
        /// Get the close date
        /// </summary>
        public System.DateTime CloseDate {get; set; }
        /// <summary>
        /// get the type of right
        /// </summary>
        public System.Decimal xType { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>The volume.</value>
        public System.Decimal Volume { get; set; }

        /// <summary>
        /// Gets or sets the right volume.
        /// </summary>
        /// <value>The right volume.</value>
        public System.Decimal RightVolume { get; set; }

        /// <summary>
        /// Gets or sets the volume paid.
        /// </summary>
        /// <value>The volume paid.</value>
        public System.Decimal VolumePaid { get; set; }

        /// <summary>
        /// Gets or sets the bought right volume.
        /// </summary>
        /// <value>The bought right volume.</value>
        public System.Decimal BoughtRightVolume { get; set; }

        /// <summary>
        /// Gets or sets the right.
        /// </summary>
        /// <value>The right.</value>
        public System.Decimal Right { get; set; }

        /// <summary>
        /// Gets or sets the rate right.
        /// </summary>
        /// <value>The rate right.</value>
        public System.Decimal RateRight { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>The price.</value>
        public System.Decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the sell from date.
        /// </summary>
        /// <value>The sell from date.</value>
        public System.DateTime SellFromDate { get; set; }

        /// <summary>
        /// Gets or sets the sell to date.
        /// </summary>
        /// <value>The sell to date.</value>
        public System.DateTime SellToDate { get; set; }

        /// <summary>
        /// Gets or sets the transfer from date.
        /// </summary>
        /// <value>The transfer from date.</value>
        public System.DateTime TransferFromDate { get; set; }

        /// <summary>
        /// Gets or sets the transfer to date.
        /// </summary>
        /// <value>The transfer to date.</value>
        public System.DateTime TransferToDate { get; set; }

        /// <summary>
        /// Gets or sets the trade date.
        /// </summary>
        /// <value>The trade date.</value>
        public System.DateTime TradeDate { get; set; }

        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        /// <value>The remark.</value>
        public System.String Remark { get; set; }
    }
}