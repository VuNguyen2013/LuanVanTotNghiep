	

#region Using Directives
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AccountManager.Entities;
using ETradeCommon;
using ETradeCommon.Enums;

#endregion

namespace AccountManager.Services
{		
	/// <summary>
	/// An component type implementation of the 'Holidays' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class HolidaysService : AccountManager.Services.HolidaysServiceBase
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the HolidaysService class.
		/// </summary>
		public HolidaysService() : base()
		{
		}

        
		#endregion Constructors

        ///<summary>
        /// Create holiday
        ///</summary>
        ///<param name="holiday">The holiday, format DD/MM/YYYY</param>
        ///<param name="note">The description</param>
        /// <returns>
        /// <para>Result of creating holiday</para>
        /// <para>RET_CODE=INCORRECT_FORMAT: The format is incorrect.</para>
        /// <para>RET_CODE=EXISTED_DATA: Data is existing.</para>
        /// <para>RET_CODE=FAIL: Fail to create holiday.</para>
        /// <para>RET_CODE=SUCCESS: Create holiday successfully.</para>
        /// </returns>
        public int CreateHoliday(string holiday, string note)
        {
            Holidays holidayObject = null;

            try
            {
                var holidayDate = DateTime.ParseExact(holiday, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                holidayObject = new Holidays();
                holidayObject.Holiday = holidayDate;
                holidayObject.Note = note;
            }
            catch (Exception)
            {
                return (int)CommonEnums.RET_CODE.INCORRECT_FORMAT;
            }
            var existedHoliday = GetByHoliday(holidayObject.Holiday);
            if (existedHoliday != null)
            {
                return (int)CommonEnums.RET_CODE.EXISTED_DATA;
            }
            bool result = Insert(holidayObject);
            if (!result)
            {
                return (int)CommonEnums.RET_CODE.FAIL;
            }
            return (int)CommonEnums.RET_CODE.SUCCESS;
        }

        ///<summary>
        /// Update holiday
        ///</summary>
        ///<param name="holiday">The holiday, format DD/MM/YYYY</param>
        ///<param name="note">The description</param>
        /// <returns>
        /// <para>Result of updating holiday</para>
        /// <para>RET_CODE=INCORRECT_FORMAT: The format is incorrect.</para>
        /// <para>RET_CODE=NO_EXISTED_DATA: Data is not existing.</para>
        /// <para>RET_CODE=FAIL: Fail to update holiday.</para>
        /// <para>RET_CODE=SUCCESS: Update holiday successfully.</para>
        /// </returns>
        public int UpdateHoliday(string holiday, string note)
        {
            Holidays holidayObject = null;

            try
            {
                var holidayDate = DateTime.ParseExact(holiday, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                holidayObject = new Holidays();
                holidayObject.Holiday = holidayDate;
            }
            catch (Exception)
            {
                return (int)CommonEnums.RET_CODE.INCORRECT_FORMAT;
            }
            var existedHoliday = GetByHoliday(holidayObject.Holiday);
            if (existedHoliday == null)
            {
                return (int)CommonEnums.RET_CODE.NO_EXISTED_DATA;
            }
            existedHoliday.Note = note;
            bool result = Update(existedHoliday);
            if (!result)
            {
                return (int)CommonEnums.RET_CODE.FAIL;
            }
            return (int)CommonEnums.RET_CODE.SUCCESS;
        }

        ///<summary>
        /// Get list holiday
        ///</summary>
        ///<param name="fromDate">The search from date, format DD/MM/YYYY</param>
        ///<param name="toDate">The search to date, format DD/MM/YYYY</param>
        ///<param name="pageIndex">Page index, begin with 1</param>
        ///<param name="pageSize">Page size</param>
        /// <returns>
        /// <para>A PagingObject&lt;List&lt;Holidays&gt;&gt; object contains total record, returned code, 
        /// returned message and a list of Holidays object that contains holiday information.</para>
        /// </returns>
        public PagingObject<List<Holidays>>  GetListHolidays(string fromDate, string toDate, int pageIndex, int pageSize)
        {
            var whereClause = new StringBuilder();
            if (!string.IsNullOrEmpty(fromDate))
            {
                whereClause.AppendFormat(" AND Holiday >= CONVERT(datetime, '{0}', 103)", fromDate);
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                whereClause.AppendFormat(" AND Holiday <= CONVERT(datetime, '{0}', 103)", toDate);
            }
            string where = whereClause.ToString();
            if (!string.IsNullOrEmpty(where))
            {
                where = where.Substring(4);
            }
            int count;
            var list = GetPaged(where, "", pageIndex, pageSize, out count);
            if (list != null)
            {
                var result = new PagingObject<List<Holidays>> { Count = count, Data = list.ToList() };
                return result;
            }
            return null;
        }
		
	}//End Class

} // end namespace
