// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DealInfo.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the DealInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCore.Entities
{
    public class DealInfo
    {
        /// <summary>
        /// Gets or sets the deal price.
        /// </summary>
        /// <value>The deal price.</value>
        public System.Decimal DealPrice { get; set; }

        /// <summary>
        /// Gets or sets the deal volume.
        /// </summary>
        /// <value>The deal volume.</value>
        public System.Decimal DealVolume { get; set; }

        /// <summary>
        /// Gets or sets the deal date.
        /// </summary>
        /// <value>The deal date.</value>
        public System.String DealDate { get; set; }

        /// <summary>
        /// Gets or sets the deal time.
        /// </summary>
        /// <value>The deal time.</value>
        public System.String DealTime { get; set; }

        /// <summary>
        /// Gets or sets the order no.
        /// </summary>
        /// <value>The order no.</value>
        public System.Decimal OrderNo { get; set; }

        /// <summary>
        /// Gets or sets the sum comm.
        /// </summary>
        /// <value>The sum comm.</value>
        public System.Decimal SumComm { get; set; }

        /// <summary>
        /// Sets the sum vat.
        /// </summary>
        /// <value>The sum vat.</value>
        public System.Decimal SumVat { get; set; }

        /// <summary>
        /// Gets or sets the comfirm no.
        /// </summary>
        /// <value>The comfirm no.</value>
        public System.Int32 ComfirmNo { get; set; }
    }
}