// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TradePermissionServices.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the TradePermissionServices type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCore.Services
{
    using DataAccess;
    using DataAccess.SqlClient;
    using Entities;

    public class TradePermissionServices
    {
        private readonly ISbaCoreProvider _sbaCoreProvider = new SqlInformixProvider();

        /// <summary>
        /// Gets the trade permission.
        /// </summary>
        /// <param name="accountNo">The account no.</param>
        /// <returns>TradePermission</returns>
        public TradePermission GetTradePermission(string accountNo)
        {
            return _sbaCoreProvider.GetTradePermission(accountNo);
        }
    }
}