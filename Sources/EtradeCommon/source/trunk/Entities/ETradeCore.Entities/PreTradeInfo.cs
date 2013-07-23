// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PreTradeInfo.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the PreTradeInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace ETradeCore.Entities
{
    using RTDataServices.Entities;


    public class PreTradeInfo
    {
        /// <summary>
        /// Gets or sets the stock info.
        /// </summary>
        /// <value>The stock info.</value>
        public StockInfo StockInfo { get; set; }
        /// <summary>
        /// Gets or sets the cash available.
        /// </summary>
        /// <value>The cash available.</value>
        public CashAvailable CashAvailable { get; set; }
                
        /// <summary>
        /// Gets or sets the stock available.
        /// </summary>
        /// <value>The stock available.</value>
        public StockAvailable StockAvailable { get; set; }

        /// <summary>
        /// Gets or sets the state of the trading.
        /// </summary>
        /// <value>The state of the trading.</value>
        public ETradeCommon.Enums.CommonEnums.MARKET_STATUS TradingState { get; set; }

        /// <summary>
        /// Gets or sets the order session of market.
        /// </summary>
        /// <value>The state of the trading.</value>
        public ETradeCommon.Enums.CommonEnums.ORDER_SESSION OrderSession { get; set; }

        /// <summary>
        /// Gets or sets the list fee of the trading.
        /// </summary>
        /// <value>The list fee.</value>
        public List<ETradeFinance.Entities.Fee> listFee { get; set; }
    }
}   