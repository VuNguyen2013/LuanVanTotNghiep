	

#region Using Directives
using System;
using System.ComponentModel;
using System.Collections;
using System.Xml.Serialization;
using System.Data;

using RTStockData.Entities;
using RTStockData.Entities.Validation;

using RTStockData.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;

#endregion

namespace RTStockData.Services
{		
	/// <summary>
	/// An component type implementation of the 'hose_transactions' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class HoseTransactionsService : RTStockData.Services.HoseTransactionsServiceBase
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the HoseTransactionsService class.
		/// </summary>
		public HoseTransactionsService() : base()
		{
		}
		#endregion Constructors

        ///<summary>
        /// Get all records with id > sending id
        ///</summary>
        ///<param name="id">Sending id</param>
        ///<returns></returns>
        public TList<HoseTransactions> GetAllById(long id)
        {
            string where = string.Format("id > {0}", id);
            int count;
            var list = GetPaged(where, string.Empty, 0, int.MaxValue, out count);
            return list;
        }
		
	}//End Class

} // end namespace
