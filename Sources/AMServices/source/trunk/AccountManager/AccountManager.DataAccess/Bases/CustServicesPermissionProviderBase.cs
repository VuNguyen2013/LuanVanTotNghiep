﻿#region Using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

using System.Diagnostics;
using AccountManager.Entities;
using AccountManager.DataAccess;

#endregion

namespace AccountManager.DataAccess.Bases
{	
	///<summary>
	/// This class is the base class for any <see cref="CustServicesPermissionProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class CustServicesPermissionProviderBase : CustServicesPermissionProviderBaseCore
	{
	} // end class
} // end namespace