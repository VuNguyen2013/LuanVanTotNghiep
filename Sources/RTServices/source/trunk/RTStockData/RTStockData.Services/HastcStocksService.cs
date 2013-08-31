﻿	

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
	/// An component type implementation of the 'hastc_stocks' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class HastcStocksService : RTStockData.Services.HastcStocksServiceBase
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the HastcStocksService class.
		/// </summary>
		public HastcStocksService() : base()
		{
		}
		#endregion Constructors
		
	}//End Class

} // end namespace