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
	/// An component type implementation of the 'BasketInfo' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class BasketInfoService : RTStockData.Services.BasketInfoServiceBase
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the BasketInfoService class.
		/// </summary>
		public BasketInfoService() : base()
		{
		}
		#endregion Constructors
		
	}//End Class

} // end namespace