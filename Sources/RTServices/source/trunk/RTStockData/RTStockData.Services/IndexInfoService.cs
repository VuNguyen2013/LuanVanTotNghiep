	

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
	/// An component type implementation of the 'IndexInfo' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class IndexInfoService : RTStockData.Services.IndexInfoServiceBase
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the IndexInfoService class.
		/// </summary>
		public IndexInfoService() : base()
		{
		}
		#endregion Constructors
		
	}//End Class

} // end namespace
