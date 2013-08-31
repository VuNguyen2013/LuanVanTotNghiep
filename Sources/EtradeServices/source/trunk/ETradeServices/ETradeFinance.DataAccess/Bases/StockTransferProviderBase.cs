﻿#region Using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

using System.Diagnostics;
using ETradeFinance.Entities;
using ETradeFinance.DataAccess;

#endregion

namespace ETradeFinance.DataAccess.Bases
{	
	///<summary>
	/// This class is the base class for any <see cref="StockTransferProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class StockTransferProviderBase : StockTransferProviderBaseCore
	{
	} // end class
} // end namespace