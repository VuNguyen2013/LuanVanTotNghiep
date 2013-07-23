// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MaginSecInfo.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the XD type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace ETradeCore.Entities
{
    public class MaginSecInfo
    {
        /// <summary>
        /// Gets or sets the sec symbol.
        /// </summary>
        /// <value>The sec symbol.</value>
        public String SecSymbol { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>From date.</value>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Gets or sets to date.
        /// </summary>
        /// <value>To date.</value>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Gets or sets the IM.
        /// </summary>
        /// <value>The IM.</value>
        public Decimal IM { get; set; }
    }
}