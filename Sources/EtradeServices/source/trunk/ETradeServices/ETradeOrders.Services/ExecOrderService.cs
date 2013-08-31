﻿	

#region Using Directives
using System;
using System.ComponentModel;
using System.Collections;
using System.Xml.Serialization;
using System.Data;

using ETradeOrders.Entities;
using ETradeOrders.Entities.Validation;

using ETradeOrders.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Logging;

#endregion

namespace ETradeOrders.Services
{		
	/// <summary>
	/// An component type implementation of the 'ExecOrder' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class ExecOrderService : ETradeOrders.Services.ExecOrderServiceBase
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the ExecOrderService class.
		/// </summary>
		public ExecOrderService() : base()
		{
		}
		#endregion Constructors

        ///<summary>
        /// Get max Sequence
        ///</summary>
        ///<returns></returns>
        public int GetMaxSeq()
        {
            var data = GetMaxSequence();
            if (data != null)
            {
                return !string.IsNullOrEmpty(data.Tables[0].Rows[0][0].ToString())? (int)data.Tables[0].Rows[0][0]:0;
            }
            return 0;
        }
		
	}//End Class

} // end namespace