// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StockAvailable.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the StockBalance type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCore.Entities
{
    public class StockAvailable
    {
        /// <summary>
        /// Gets or sets the sec symbol.
        /// </summary>
        /// <value>The sec symbol.</value>
        public System.String SecSymbol { get; set; }
        /// <summary>
        /// Gets or sets the avai volume.
        /// </summary>
        /// <value>The avai volume.</value>
        public System.Decimal AvaiVolume { get; set; }


        /// <summary>
        /// Gets or sets the avg price.
        /// </summary>
        /// <value>The avg price.</value>
        public System.Decimal AvgPrice { get; set; }

        /// <summary>
        /// Gets or sets the stock transfered amount.
        /// </summary>
        /// <value>The stock transfered amount.</value>
        public System.Decimal StockTransferedAmount { get; set; }

        /// <summary>
        /// Gets or sets the advance ordered amount.
        /// </summary>
        /// <value>The advance ordered amount.</value>
        public System.Decimal AdvanceOrderedAmount { get; set; }

        /// <summary>
        /// Gets or sets the stock wait to receive
        /// </summary>
        /// <value>The stock wait to receive.</value>
        public System.Decimal WTR { get; set; }
        /// <summary>
        /// Gets or sets the stock wait to receive T1
        /// </summary>
        /// <value>The stock wait to receive T1.</value>
        public System.Decimal WTR_T1 { get; set; }
        /// <summary>
        /// Gets or sets the stock wait to receive T2
        /// </summary>
        /// <value>The stock wait to receive T2.</value>
        public System.Decimal WTR_T2 { get; set; }
        /// <summary>
        /// Gets or sets the stock wait to receive T3
        /// </summary>
        /// <value>The stock wait to receive T3.</value>
        public System.Decimal WTR_T3 { get; set; }
        /// <summary>
        /// Gets or sets the date of stock wait to receive
        /// </summary>
        /// <value>The the date of stock wait to receive</value>
        public System.DateTime Date_WTR { get; set; }
        /// <summary>
        /// Gets or sets the date of stock wait to receive T1
        /// </summary>
        /// <value>The the date of stock wait to receive T1</value>
        public System.DateTime Date_WTR_T1 { get; set; }
        /// <summary>
        /// Gets or sets the date of stock wait to receive T2
        /// </summary>
        /// <value>The the date of stock wait to receive T2</value>
        public System.DateTime Date_WTR_T2 { get; set; }
        /// <summary>
        /// Gets or sets the date of stock wait to receive T3
        /// </summary>
        /// <value>The the date of stock wait to receive T3</value>
        public System.DateTime Date_WTR_T3 { get; set; }

        /// <summary>
        /// Gets or sets the WTR_Amt_T1.
        /// </summary>
        /// <value>The WTR_Amt_T1.</value>
        public System.Decimal WTR_Amt_T1 { get; set; }

        /// <summary>
        /// Gets or sets the WTR_Amt_T2.
        /// </summary>
        /// <value>The WTR_Amt_T2.</value>
        public System.Decimal WTR_Amt_T2 { get; set; }

        /// <summary>
        /// Gets or sets the WTR_Amt_T3.
        /// </summary>
        /// <value>The WTR_Amt_T3.</value>
        public System.Decimal WTR_Amt_T3 { get; set; }
    }
}