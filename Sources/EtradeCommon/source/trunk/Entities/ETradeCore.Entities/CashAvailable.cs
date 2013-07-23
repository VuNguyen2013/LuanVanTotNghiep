// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CashAvailable.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the CashAvailable type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCore.Entities
{
    public class CashAvailable
    {
        /// <summary>
        /// Gets or sets the buy credit.
        /// </summary>
        /// <value>The buy credit.</value>
        public System.Decimal BuyCredit { get; set; }

        /// <summary>
        /// Gets or sets the PP of account.
        /// </summary>
        /// <value>The PP.</value>
        public System.Decimal PP { get; set; }

        /// <summary>
        /// Gets or sets the PP of stock.
        /// </summary>
        /// <value>The PP.</value>
        public System.Decimal PPStock { get; set; }

        /// <summary>
        /// Gets or sets the IM.
        /// </summary>
        /// <value>The IM.</value>
        public System.Decimal IM { get; set; }

        /// <summary>
        /// Gets or sets the IM of stock.
        /// </summary>
        /// <value>The IM.</value>
        public System.Decimal IMStock { get; set; }

        /// <summary>
        /// Gets or sets the EE.
        /// </summary>
        /// <value>The EE.</value>
        public System.Decimal EE { get; set; }

        /// <summary>
        /// Gets or sets the advance ordered amount.
        /// </summary>
        /// <value>The advance ordered amount.</value>
        public System.Decimal AdvanceOrderedAmount { get; set; }

        /// <summary>
        /// Gets or sets the cash transfered amount.
        /// </summary>
        /// <value>The cash transfered amount.</value>
        public System.Decimal CashTransferedAmount { get; set; }

        /// <summary>
        /// Gets or sets the cash wait to receive T4.
        /// </summary>
        /// <value>The WTR.</value>
        public System.Decimal WTR { get; set; }
        /// <summary>
        /// Gets or sets the cash wait to receive T1.
        /// </summary>
        /// <value>The cash wait to receive T1.</value>
        public System.Decimal WTR_T1 { get; set; }
        /// <summary>
        /// Gets or sets the cash wait to receive T2.
        /// </summary>
        /// <value>The cash wait to receive T2.</value>
        public System.Decimal WTR_T2 { get; set; }
        /// <summary>
        /// Gets or sets the cash wait to receive T3.
        /// </summary>
        /// <value>The cash wait to receive T3.</value>
        public System.Decimal WTR_T3 { get; set; }
        /// <summary>
        /// Gets or sets the date of cash wait to receive
        /// </summary>
        /// <value>The the date of cash wait to receive</value>
        public System.DateTime Date_WTR { get; set; }
        /// <summary>
        /// Gets or sets the date of cash wait to receive T1
        /// </summary>
        /// <value>The the date of cash wait to receive T1</value>
        public System.DateTime Date_WTR_T1 { get; set; }
        /// <summary>
        /// Gets or sets the date of cash wait to receive T2
        /// </summary>
        /// <value>The the date of cash wait to receive T2</value>
        public System.DateTime Date_WTR_T2 { get; set; }
        /// <summary>
        /// Gets or sets the date of cash wait to receive T3
        /// </summary>
        /// <value>The the date of cash wait to receive T3</value>
        public System.DateTime Date_WTR_T3 { get; set; }
    }
}