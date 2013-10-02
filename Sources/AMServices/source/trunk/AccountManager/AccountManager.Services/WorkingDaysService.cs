	

#region Using Directives
using System;
using System.ComponentModel;
using System.Collections;
using System.Xml.Serialization;
using System.Data;

using AccountManager.Entities;
using AccountManager.Entities.Validation;

using AccountManager.DataAccess;
using ETradeCommon.Enums;
using Microsoft.Practices.EnterpriseLibrary.Logging;

#endregion

namespace AccountManager.Services
{		
	/// <summary>
	/// An component type implementation of the 'WorkingDays' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class WorkingDaysService : AccountManager.Services.WorkingDaysServiceBase
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the WorkingDaysService class.
		/// </summary>
		public WorkingDaysService() : base()
		{
		}
		#endregion Constructors

        ///<summary>
        /// Update working days status
        ///</summary>
        ///<param name="mondayStatus">Monday status</param>
        ///<param name="tuesdayStatus">Tuesday status</param>
        ///<param name="wednesdayStatus">Wednesday status</param>
        ///<param name="thursdayStatus">Thursday status</param>
        ///<param name="fridayStatus">Friday status</param>
        ///<param name="saturdayStatus">Saturday status</param>
        ///<param name="sundayStatus">Sunday status</param>
        /// <returns>
        /// <para>Result of updating working days</para>
        /// <para>RET_CODE=SUCCESS: Get account successfully.</para>
        /// </returns>
        public int UpdateWorkingDays(bool mondayStatus, bool tuesdayStatus, bool wednesdayStatus, bool thursdayStatus,
                bool fridayStatus, bool saturdayStatus, bool sundayStatus)
        {
            var workingDayList = GetAll();
            foreach (var workingDay in workingDayList)
            {
                switch (workingDay.DateId)
                {
                    case 2:
                        workingDay.IsWorkingDay = mondayStatus;
                        break;
                    case 3:
                        workingDay.IsWorkingDay = tuesdayStatus;
                        break;
                    case 4:
                        workingDay.IsWorkingDay = wednesdayStatus;
                        break;
                    case 5:
                        workingDay.IsWorkingDay = thursdayStatus;
                        break;
                    case 6:
                        workingDay.IsWorkingDay = fridayStatus;
                        break;
                    case 7:
                        workingDay.IsWorkingDay = saturdayStatus;
                        break;
                    case 8:
                        workingDay.IsWorkingDay = sundayStatus;
                        break;

                }
            }
            Save(workingDayList);
            return (int) CommonEnums.RET_CODE.SUCCESS;
        }
	}//End Class

} // end namespace
