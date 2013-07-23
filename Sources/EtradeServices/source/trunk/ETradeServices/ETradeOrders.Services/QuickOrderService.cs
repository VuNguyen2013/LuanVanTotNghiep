	

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
	/// An component type implementation of the 'QuickOrder' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class QuickOrderService : ETradeOrders.Services.QuickOrderServiceBase
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the QuickOrderService class.
		/// </summary>
		public QuickOrderService() : base()
		{
		}
		#endregion Constructors
		
	}//End Class

} // end namespace
