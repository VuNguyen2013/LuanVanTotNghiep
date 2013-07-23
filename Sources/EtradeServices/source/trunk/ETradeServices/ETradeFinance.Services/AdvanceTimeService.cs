	

#region Using Directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Xml.Serialization;
using System.Data;
using ETradeCommon.Enums;
using ETradeFinance.Entities;
using ETradeFinance.Entities.Validation;

using ETradeFinance.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Logging;

#endregion

namespace ETradeFinance.Services
{		
	/// <summary>
	/// An component type implementation of the 'AdvanceTime' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class AdvanceTimeService : ETradeFinance.Services.AdvanceTimeServiceBase
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the AdvanceTimeService class.
		/// </summary>
		public AdvanceTimeService() : base()
		{
		}
		#endregion Constructors

        ///<summary>
        /// Update list of AdvanceTime objects.
        ///</summary>
        ///<param name="advanceTimeList">List of AdvanceTime objects.</param>
        /// <returns>
        /// <para>Result of updating Advance Time</para>
        /// <para>RET_CODE=INCORRECT_FORMAT: The time is incorrect.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: Data is not existing.</para>
        /// <para>RET_CODE=FAIL: Fail to update data.</para>
        /// <para>RET_CODE=SUCCESS: Update data successfully.</para>
        /// </returns>
        public int UpdateAdvanceTime(List<string[]> advanceTimeList)
        {
            var list = GetAll();
            var currentTime = DateTime.Now;
            if ((list != null) && (list.Count > 0))
            {
                foreach (var advanceTime in list)
                {
                    foreach (var tmpObject in advanceTimeList)
                    {
                        var newAdvanceTime = tmpObject;
                        int id = int.Parse(newAdvanceTime[0]);
                        if (id == advanceTime.Id)
                        {
                            // Start time
                            var tmpString = newAdvanceTime[1].Split(':');
                            if (tmpString.Length != 2)
                            {
                                return (int)CommonEnums.RET_CODE.INCORRECT_FORMAT;
                            }
                            advanceTime.StartTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day,
                                                                 int.Parse(tmpString[0]), int.Parse(tmpString[1]), 0, 0);

                            //End time
                            tmpString = newAdvanceTime[2].Split(':');
                            if (tmpString.Length != 2)
                            {
                                return (int)CommonEnums.RET_CODE.INCORRECT_FORMAT;
                            }
                            advanceTime.EndTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day,
                                                                 int.Parse(tmpString[0]), int.Parse(tmpString[1]), 0, 0);
                        }
                    }
                }
                // Update data into database
                Update(list);
                return (int)CommonEnums.RET_CODE.SUCCESS;
            }
            return (int) CommonEnums.RET_CODE.NO_EXISTED_DATA;
        }
		
	}//End Class

} // end namespace
