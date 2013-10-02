	

#region Using Directives
using System;
using AccountManager.Entities;

#endregion

namespace AccountManager.Services
{		
	/// <summary>
	/// An component type implementation of the 'SMSCount' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class SmsCountService : AccountManager.Services.SmsCountServiceBase
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the SmsCountService class.
		/// </summary>
		public SmsCountService() : base()
		{
		}
		#endregion Constructors

        ///<summary>
        /// Get total messages in a range of days.
        ///</summary>
        ///<param name="fromDate">FromDate to search.</param>
        ///<param name="toDate">ToDate to search.</param>
        ///<returns>Total of messages.</returns>
        public long CountSMS(string fromDate, string toDate)
        {
            var result = GetTotal(fromDate, toDate);
            var row = result.Tables[0].Rows[0];
            var total = (int) row[0];
            return total;
        }
        ///<summary>
        /// Increase total sent messages today
        ///</summary>
        ///<param name="count"></param>
        ///<returns></returns>
        public bool UpdateCount(int count)
        {
            bool result = false;
            DateTime currentDate = DateTime.Now.Date;
            var smsCount = GetBySendDate(currentDate);
            if (smsCount == null)
            {
                smsCount = new SmsCount {SendDate = currentDate, Total = count};
                result = Insert(smsCount);
            }
            else
            {
                smsCount.Total = smsCount.Total + count;
                result = Update(smsCount);
            }
            return result;
        }
		
	}//End Class

} // end namespace
