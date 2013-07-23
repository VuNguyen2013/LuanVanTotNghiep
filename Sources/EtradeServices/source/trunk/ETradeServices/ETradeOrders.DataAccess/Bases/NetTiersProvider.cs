﻿
#region Using directives

using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Configuration.Provider;

using ETradeOrders.Entities;

#endregion

namespace ETradeOrders.DataAccess.Bases
{	
	///<summary>
	/// The base class to implements to create a .NetTiers provider.
	///</summary>
	public abstract class NetTiersProvider : NetTiersProviderBase
	{
		
		///<summary>
		/// Current ConditionOrderProviderBase instance.
		///</summary>
		public virtual ConditionOrderProviderBase ConditionOrderProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current QuickOrderProviderBase instance.
		///</summary>
		public virtual QuickOrderProviderBase QuickOrderProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current ExecOrderProviderBase instance.
		///</summary>
		public virtual ExecOrderProviderBase ExecOrderProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current ConditionOrderDetailProviderBase instance.
		///</summary>
		public virtual ConditionOrderDetailProviderBase ConditionOrderDetailProvider{get {throw new NotImplementedException();}}
		
		
	}
}
