// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SysConfig.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the SysConfig type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


using ETradeFinance.Entities;
using System;
using System.Collections.Generic;

namespace ETradeWebServices.Utils
{
    ///<summary>
    /// Store config of 
    ///</summary>
    public static class SysConfig
    {
        /// <summary>
        /// List of Holidays
        /// </summary>
        public static Dictionary<string, DateTime> Holidays = new Dictionary<string, DateTime>();

        /// <summary>
        /// List of WorkingDays
        /// </summary>
        public static Dictionary<int, bool> WorkingDays = new Dictionary<int, bool>();

        /// <summary>
        /// List of Configuration
        /// </summary>
        public static Dictionary<string, string> Configurations = new Dictionary<string, string>();

        /// <summary>
        /// List of AdvanceTime
        /// </summary>
        public static Dictionary<int, AdvanceTime> AdvanceTimes = new Dictionary<int, AdvanceTime>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SysConfig"/> class.
        /// </summary>
        static SysConfig()
        {

        }
    }
}