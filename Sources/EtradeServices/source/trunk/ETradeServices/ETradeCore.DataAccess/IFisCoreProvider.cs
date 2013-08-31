// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFisCoreProvider.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the ICoreProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCore.DataAccess
{
    using System.Collections.Generic;

    using Entities;

    public interface IFisCoreProvider
    {
        /// <summary>
        /// Gets the cash available for normal account.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns>Available cash of normal account.</returns>
        CashAvailable GetCashAvailable4NormalAccount(string accountNo);

        /// <summary>
        /// Gets the cash balance for margin account.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns>Available cash of margin account.</returns>
        CashAvailable GetCashAvailable4MarginAccount(string accountNo);

        /// <summary>
        /// Gets the cash available for margin account.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns>Cash balance of margin account.</returns>
        CashBalance GetCashBalance4MarginAccount(string accountNo);

        /// <summary>
        /// Gets the cash balance for nor mal account.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns>Cash balance of normal account.</returns>
        CashBalance GetCashBalance4NormalAccount(string accountNo);

        /// <summary>
        /// Gets the stock balance.
        /// </summary>
        /// <param name="accountNo">
        /// The account no.
        /// </param>
        /// <param name="symbol">
        /// The symbol.
        /// </param>
        /// <returns> 
        /// Available stock of normal account.
        /// </returns>
        StockAvailable GetStockAvailable4NormalAccount(string accountNo, string symbol);

        /// <summary>
        /// Gets the stock balance4 margin account.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="symbol">The symbol.</param>
        /// <returns>Available stock of margin account.</returns>
        StockAvailable GetStockAvailable4MarginAccount(string accountNo, string symbol);

        /// <summary>
        /// Gets the portfolio.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns>Portfolio of normal account.</returns>
        List<Portfolio> GetPortfolio4NormalAccount(string accountNo);

        /// <summary>
        /// Gets the portfolio4 margin account.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns>Portfolio of margin account.</returns>
        List<Portfolio> GetPortfolio4MarginAccount(string accountNo);

        /// <summary>
        /// Gets the stock due.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns>Stock due.</returns>
        Dictionary<System.String, StockDueInfo> GetStockDue(string accountNo);

        /// <summary>
        /// Gets the market price.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <returns>Market price.</returns>
        System.Decimal GetMarketPrice(string symbol);

        /// <summary>
        /// Gets the cash due.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns>Cash due.</returns>
        CashDueInfo GetCashDue(string accountNo);

        /// <summary>
        /// Gets the order history.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="orderStatus">The order status.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>List of order histoy information.</returns>
        List<OrderHistory> GetOrderHistory(string accountNo, string fromDate, string toDate, string symbol, int orderStatus, int pageNumber, int pageSize);

        /// <summary>
        /// Gets the deal history.
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        /// <param name="dealDate">The deal date.</param>
        /// <param name="page">The page.</param>
        /// <returns>List of deal history information.</returns>
        List<DealHistory> GetDealHistory(decimal orderNo, string dealDate, int page);

        /// <summary>
        /// Gets the order intra day.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <returns>List of order information.</returns>
        List<OrderInfo> GetOrderIntraDay(string accountNo, int pageIndex);

        /// <summary>
        /// Gets the deal intra day.
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        /// <param name="page">The page.</param>
        /// <returns>List of deal information.</returns>
        List<DealInfo> GetDealIntraDay(decimal orderNo, int page);

        /// <summary>
        /// Gets the portfolio by SQ l4 normal account.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns>List of portfolios of normal account.</returns>
        List<Portfolio> GetPortfolioBySql4NormalAccount(string accountNo);

        /// <summary>
        /// Gets the margin ratio.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns>Margin ratio information. </returns>
        MarginRatioInfo GetMarginRatio(string accountNo);


        /// <summary>
        /// Gets the margin portfolio.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns>List of portfolios of margin account.</returns>
        List<MarginPortfolio> GetMarginPortfolio(string accountNo);
    }
}