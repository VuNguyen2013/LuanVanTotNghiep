﻿#region Using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

using System.Diagnostics;
using ETradeOrders.Entities;
using ETradeOrders.DataAccess;

#endregion

namespace ETradeOrders.DataAccess.Bases
{	
	///<summary>
	/// This class is the base class for any <see cref="ConditionOrderDetailProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class ConditionOrderDetailProviderBase : ConditionOrderDetailProviderBaseCore
	{
	} // end class
} // end namespace