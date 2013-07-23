// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XD.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the XD type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCore.Entities
{
    public class BankAccountInfo
    {
        /// <summary>
        /// Gets or sets the type of the bank account.
        /// </summary>
        /// <value>The type of the bank account.</value>
        public ETradeCommon.Enums.CommonEnums.BANK_ACCOUNT_TYPE BankAccountType { get; set; }

        /// <summary>
        /// Gets or sets the account no.
        /// </summary>
        /// <value>The account no.</value>
        public string AccountNo { get; set; }

        /// <summary>
        /// Gets or sets the bank code.
        /// </summary>
        /// <value>The bank code.</value>
        public string BankCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the bank.
        /// </summary>
        /// <value>The name of the bank.</value>
        public string BankName { get; set; }

        /// <summary>
        /// Gets or sets the bank acc no.
        /// </summary>
        /// <value>The bank acc no.</value>
        public string BankAccNo { get; set; }

        /// <summary>
        /// Gets or sets the comp acc no.
        /// </summary>
        /// <value>The comp acc no.</value>
        public string CompAccNo { get; set; }

        /// <summary>
        /// Gets or sets the type of the receive.
        /// </summary>
        /// <value>The type of the receive.</value>
        public string ReceiveType { get; set; }

        /// <summary>
        /// Gets or sets the type of the payment.
        /// </summary>
        /// <value>The type of the payment.</value>
        public string PaymentType { get; set; }
        
    }
}