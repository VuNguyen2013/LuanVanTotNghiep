// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISbaCoreProvider.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the ISbaCoreProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AccountManager.DataAccess
{
    using System.Collections.Generic;

    using AccountManager.Entities;

    public interface ISbaCoreProvider
    {
        /// <summary>
        /// Gets the cust info from core.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <returns></returns>
        List<CoreAccountInfo> GetCustInfoFromCore(string accountId);
    }
}