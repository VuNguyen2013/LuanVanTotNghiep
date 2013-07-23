// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActualTradeServices.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the ActualTradeServices type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCore.Services
{
    using System.Collections.Generic;

    using ETradeCore.DataAccess;
    using ETradeCore.DataAccess.SqlClient;
    using ETradeCore.Entities;

    public class ActualTradeServices
    {
        private readonly ISbaCoreProvider _sbaCoreProvider = new SqlInformixProvider();

        /// <summary>
        /// Gets the actual trading.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="symbol">The symbol.</param>
        /// <returns></returns>
        public List<ActualTrade> GetActualTrading(string accountNo, string fromDate, string toDate, string symbol)
        {
            List<ActualTrade> listActualTrade=this._sbaCoreProvider.GetActualTrading(accountNo, fromDate, toDate, symbol);
            if(listActualTrade!=null)
            {
                foreach (var actualTrade in listActualTrade)
                {
                    if(actualTrade.Side.Equals(((char)ETradeCommon.Enums.CommonEnums.TRADE_SIDE.SELL).ToString()))
                    {
                        actualTrade.Amount = actualTrade.Price*actualTrade.Volume * ETradeCommon.Constants.MONEY_UNIT - actualTrade.Commission - actualTrade.PIT;
                    }
                    else
                    {
                        actualTrade.Amount = actualTrade.Price * actualTrade.Volume * ETradeCommon.Constants.MONEY_UNIT + actualTrade.Commission;
                    }
                }
            }
            return listActualTrade;            
        }
    }
}