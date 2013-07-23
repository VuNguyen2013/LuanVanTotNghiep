// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CashBalance.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the CashBalance type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeServices.DTO
{
    public class CashBalanceDTO
    {
        /// <summary>
        /// Gets or sets the buy credit.
        /// </summary>
        /// <value>The buy credit.</value>
        public System.Decimal BuyCredit { get; set; }

        /// <summary>
        /// Gets or sets the PP.
        /// </summary>
        /// <value>The PP.</value>
        public System.Decimal PP { get; set; }

        /// <summary>
        /// Gets or sets the IM.
        /// </summary>
        /// <value>The IM.</value>
        public System.Decimal IM { get; set; }

        /// <summary>
        /// Gets or sets the EE.
        /// </summary>
        /// <value>The EE.</value>
        public System.Decimal EE { get; set; }
    }
}