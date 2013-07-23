// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StockBalance.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the StockBalance type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeServices.DTO
{
    public class StockBalanceDTO
    {
        /// <summary>
        /// Gets or sets the avai volume.
        /// </summary>
        /// <value>The avai volume.</value>
        public System.Decimal AvaiVolume { get; set; }

        /// <summary>
        /// Gets or sets the symbol.
        /// </summary>
        /// <value>The symbol.</value>
        public System.String Symbol { get; set; }
    }
}