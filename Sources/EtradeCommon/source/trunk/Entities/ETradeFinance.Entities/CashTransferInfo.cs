#region Using directives

using System;

#endregion

namespace ETradeFinance.Entities
{
    
    public class CashTransferInfo
    {
        /// <summary>
        /// Gets or sets the sub Account Id
        /// </summary>
        /// <value>The sub Account Id.</value>
        public String subAccountId { get; set; }

        /// <summary>
        /// Gets or sets the withdrawable amt.
        /// </summary>
        /// <value>The withdrawable amt.</value>
        public Decimal WithdrawableAmt { get; set; }

        /// <summary>
        /// Gets or sets the transfered amt.
        /// </summary>
        /// <value>The transfered amt.</value>
        public Decimal TransferedAmt { get; set; }

        /// <summary>
        /// Gets or sets the adv order amt.
        /// </summary>
        /// <value>The adv order amt.</value>
        public Decimal AdvOrderAmt { get; set; }

        /// <summary>
        /// Gets or sets the avilable amt.
        /// </summary>
        /// <value>The avilable amt.</value>
        public Decimal AvilableAmt { get; set; }
    }
	
}
