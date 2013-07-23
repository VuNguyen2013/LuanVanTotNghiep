
#region Using directives

using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Configuration.Provider;

using ETradeFinance.Entities;

#endregion

namespace ETradeFinance.DataAccess.Bases
{	
	///<summary>
	/// The base class to implements to create a .NetTiers provider.
	///</summary>
	public abstract class NetTiersProvider : NetTiersProviderBase
	{
		
		///<summary>
		/// Current OddLotOrderProviderBase instance.
		///</summary>
		public virtual OddLotOrderProviderBase OddLotOrderProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current AdvanceTimeProviderBase instance.
		///</summary>
		public virtual AdvanceTimeProviderBase AdvanceTimeProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current FeeProviderBase instance.
		///</summary>
		public virtual FeeProviderBase FeeProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current StockTransferProviderBase instance.
		///</summary>
		public virtual StockTransferProviderBase StockTransferProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current CashAdvanceProviderBase instance.
		///</summary>
		public virtual CashAdvanceProviderBase CashAdvanceProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current CashTransferProviderBase instance.
		///</summary>
		public virtual CashTransferProviderBase CashTransferProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current XrOrdersProviderBase instance.
		///</summary>
		public virtual XrOrdersProviderBase XrOrdersProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current CashAdvanceHistoryProviderBase instance.
		///</summary>
		public virtual CashAdvanceHistoryProviderBase CashAdvanceHistoryProvider{get {throw new NotImplementedException();}}
		
		
	}
}
