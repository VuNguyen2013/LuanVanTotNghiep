// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DealServices.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the DealServices type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCore.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using DataAccess;
    using DataAccess.SqlClient;
    using Entities;

    using ETradeCommon;
    using ETradeCommon.Enums;

    public class DealServices
    {
        private readonly IFisCoreProvider _db2Provider = new SqlDb2Provider();

        private readonly ISbaCoreProvider _informixProvider = new SqlInformixProvider();

        /// <summary>
        /// Gets the deal history.
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        /// <param name="dealDate">The deal date.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public List<DealHistory> GetDealHistory(decimal orderNo, string dealDate, int page)
        {
            if (AppConfig.DealHistSource == (int)CommonEnums.DEALHIST_SOURCE.FISDB)
            {
                return this._db2Provider.GetDealHistory(orderNo, dealDate, page);    
            }
            
            if (AppConfig.DealHistSource == (int)CommonEnums.DEALHIST_SOURCE.SBA)
            {
                List<DealHistory> dealHistories = this._informixProvider.GetDealHistory(orderNo, dealDate, page);

                // Filter by date
                return dealHistories.Where(dealHistory => this.IsValidDealHistDate(dealHistory.DealDate, dealDate)).ToList();
            }

            return null;
        }

        /// <summary>
        /// Gets the deal intra day.
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        /// <param name="page">The page.</param>
        /// <returns>List of DealInfo</returns>
        public List<DealInfo> GetDealIntraDay(decimal orderNo, int page)
        {
            return _db2Provider.GetDealIntraDay(orderNo, page);
        }

        /// <summary>
        /// Determines whether [is valid deal hist date] [the specified deal date].
        /// </summary>
        /// <param name="dealDate">The deal date.</param>
        /// <param name="conditionDate">The condition date.</param>
        /// <returns>
        /// 	<c>true</c> if [is valid deal hist date] [the specified deal date]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsValidDealHistDate(string dealDate, string conditionDate)
        {
            try
            {
                if (dealDate.IndexOf('-') > 0)
                {
                    DateTime dateTime;
                    DateTime dtFromDate = new DateTime();
                    DateTime dtDealDate = new DateTime();

                    if (conditionDate != string.Empty)
                    {
                        // conditionDate is yyyyMMdd
                        dateTime = new DateTime(
                            int.Parse(conditionDate.Substring(0, 4)),
                            int.Parse(conditionDate.Substring(4, 2)),
                            int.Parse(conditionDate.Substring(6, 2)));

                        dtFromDate = dateTime;
                    }

                    if (dealDate != string.Empty)
                    {
                        // dealDate is yyyy-MM-dd
                        dateTime = new DateTime(
                            int.Parse(dealDate.Substring(0, 4)),
                            int.Parse(dealDate.Substring(5, 2)),
                            int.Parse(dealDate.Substring(8, 2)));

                        dtDealDate = dateTime;
                    }

                    if (dtFromDate.Day == dtDealDate.Day
                        && dtFromDate.Month == dtDealDate.Month
                        && dtFromDate.Year == dtDealDate.Year)
                    {
                        return true;
                    }
                }
                else
                {
                    if (dealDate.Equals(conditionDate))
                    {
                        return true;
                    }    
                }

                return false;
            }
            catch (Exception exception)
            {
                LogHandler.Log(
                    "IsValidDealHistDate: exception = " + exception + ", dealDate = " + dealDate + ", conditionDate = " +
                    conditionDate,
                    this.GetType() + ".IsValidDealHistDate",
                    TraceEventType.Error);
                return false;
            }
        }
    }
}