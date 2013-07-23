// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CashDueInfo.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the CashDueInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCore.Entities
{
    public class CashDueInfo
    {

        /// <summary>
        /// Gets or sets the over due.
        /// </summary>
        /// <value>The over due.</value>
        public System.Decimal OverDue { get; set; }

        /// <summary>
        /// Gets or sets the payment.
        /// </summary>
        /// <value>The payment.</value>
        public System.Decimal Payment { get; set; }

        /// <summary>
        /// Gets or sets the AMT_T1.
        /// </summary>
        /// <value>The AMT_T1.</value>
        public System.Decimal AMT_T1 { get; set; }

        /// <summary>
        /// Gets or sets the AMT_T2.
        /// </summary>
        /// <value>The AMT_T2.</value>
        public System.Decimal AMT_T2 { get; set; }

        /// <summary>
        /// Gets or sets the AMT_T3.
        /// </summary>
        /// <value>The AMT_T3.</value>
        public System.Decimal AMT_T3 { get; set; }
    }
}