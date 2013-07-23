#region Using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

using System.Diagnostics;
using RTStockData.Entities;
using RTStockData.Data;

#endregion

namespace RTStockData.Data.Bases
{	
	///<summary>
	/// This class is the base class for any <see cref="IndexVn30HistoryProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class IndexVn30HistoryProviderBase : IndexVn30HistoryProviderBaseCore
	{
	} // end class
} // end namespace
