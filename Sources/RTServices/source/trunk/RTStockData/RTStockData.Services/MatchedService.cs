	

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
	/// An component type implementation of the 'Matched' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class MatchedService : RTStockData.Services.MatchedServiceBase
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the MatchedService class.
		/// </summary>
		public MatchedService() : base()
		{
		}
		#endregion Constructors
		
	}//End Class

} // end namespace
