// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StockInfoDTO.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the StockInfoDTO type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeRTServicesMock.DTO
{
    public class StockInfoDTO
    {
        /// <summary>
        /// Gets or sets the name of the sec.
        /// </summary>
        /// <value>The name of the sec.</value>
        public System.String SecName { get; set; }

        /// <summary>
        /// Gets or sets the ceiling.
        /// </summary>
        /// <value>The ceiling.</value>
        public System.Decimal Ceiling { get; set; }

        /// <summary>
        /// Gets or sets the floor.
        /// </summary>
        /// <value>The floor.</value>
        public System.Decimal Floor { get; set; }

        /// <summary>
        /// Gets or sets the type of the sec.
        /// </summary>
        /// <value>The type of the sec.</value>
        public System.Int32 SecType { get; set; }

        /// <summary>
        /// Gets or sets the last sale price.
        /// </summary>
        /// <value>The last sale price.</value>
        public System.Decimal LastSalePrice { get; set; }

        /// <summary>
        /// Gets or sets the last sale volume.
        /// </summary>
        /// <value>The last sale volume.</value>
        public System.Int64 LastSaleVolume { get; set; }

        /// <summary>
        /// Gets or sets the high.
        /// </summary>
        /// <value>The high.</value>
        public System.Decimal High { get; set; }

        /// <summary>
        /// Gets or sets the low.
        /// </summary>
        /// <value>The low.</value>
        public System.Decimal Low { get; set; }

        /// <summary>
        /// Gets or sets the prior.
        /// </summary>
        /// <value>The prior.</value>
        public System.Decimal Prior { get; set; }

        /// <summary>
        /// Gets or sets the change.
        /// </summary>
        /// <value>The change.</value>
        public System.Decimal Change { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>The volume.</value>
        public System.Int64 Volume { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public System.Decimal Value { get; set; }

        /// <summary>
        /// Gets or sets the project open.
        /// </summary>
        /// <value>The project open.</value>
        public System.Decimal ProjectOpen { get; set; }

        /// <summary>
        /// Gets or sets the open.
        /// </summary>
        /// <value>The open.</value>
        public System.Decimal Open { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is halt.
        /// </summary>
        /// <value><c>true</c> if this instance is halt; otherwise, <c>false</c>.</value>
        public System.Boolean IsHalt { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is bond.
        /// </summary>
        /// <value><c>true</c> if this instance is bond; otherwise, <c>false</c>.</value>
        public System.Boolean IsBond { get; set; }

        /// <summary>
        /// Gets or sets the remain room.
        /// </summary>
        /// <value>The remain room.</value>
        public System.Int64 RemainRoom { get; set; }

        /// <summary>
        /// Gets or sets the best bid price1.
        /// </summary>
        /// <value>The best bid price1.</value>
        public System.Decimal BestBidPrice1 { get; set; }

        /// <summary>
        /// Gets or sets the best bid price2.
        /// </summary>
        /// <value>The best bid price2.</value>
        public System.Decimal BestBidPrice2 { get; set; }

        /// <summary>
        /// Gets or sets the best bid price3.
        /// </summary>
        /// <value>The best bid price3.</value>
        public System.Decimal BestBidPrice3 { get; set; }

        /// <summary>
        /// Gets or sets the best offer price1.
        /// </summary>
        /// <value>The best offer price1.</value>
        public System.Decimal BestOfferPrice1 { get; set; }

        /// <summary>
        /// Gets or sets the best offer price2.
        /// </summary>
        /// <value>The best offer price2.</value>
        public System.Decimal BestOfferPrice2 { get; set; }

        /// <summary>
        /// Gets or sets the best offer price3.
        /// </summary>
        /// <value>The best offer price3.</value>
        public System.Decimal BestOfferPrice3 { get; set; }

        /// <summary>
        /// Gets or sets the best bid vol1.
        /// </summary>
        /// <value>The best bid vol1.</value>
        public System.Int64 BestBidVol1 { get; set; }

        /// <summary>
        /// Gets or sets the best bid vol2.
        /// </summary>
        /// <value>The best bid vol2.</value>
        public System.Int64 BestBidVol2 { get; set; }

        /// <summary>
        /// Gets or sets the best bid vol3.
        /// </summary>
        /// <value>The best bid vol3.</value>
        public System.Int64 BestBidVol3 { get; set; }

        /// <summary>
        /// Gets or sets the best offer vol1.
        /// </summary>
        /// <value>The best offer vol1.</value>
        public System.Int64 BestOfferVol1 { get; set; }

        /// <summary>
        /// Gets or sets the best offer vol2.
        /// </summary>
        /// <value>The best offer vol2.</value>
        public System.Int64 BestOfferVol2 { get; set; }

        /// <summary>
        /// Gets or sets the best offer vol3.
        /// </summary>
        /// <value>The best offer vol3.</value>
        public System.Int64 BestOfferVol3 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is existed transaction.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is existed transaction; otherwise, <c>false</c>.
        /// </value>
        public System.Boolean IsExistedTransaction { get; set; }
    }
}