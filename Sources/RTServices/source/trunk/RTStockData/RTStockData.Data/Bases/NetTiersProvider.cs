
#region Using directives

using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Configuration.Provider;

using RTStockData.Entities;

#endregion

namespace RTStockData.Data.Bases
{	
	///<summary>
	/// The base class to implements to create a .NetTiers provider.
	///</summary>
	public abstract class NetTiersProvider : NetTiersProviderBase
	{
		
		///<summary>
		/// Current BasketInfoProviderBase instance.
		///</summary>
		public virtual BasketInfoProviderBase BasketInfoProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current IndexInfoProviderBase instance.
		///</summary>
		public virtual IndexInfoProviderBase IndexInfoProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current IndexInfoHistoryProviderBase instance.
		///</summary>
		public virtual IndexInfoHistoryProviderBase IndexInfoHistoryProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current NearestWorkingDatesProviderBase instance.
		///</summary>
		public virtual NearestWorkingDatesProviderBase NearestWorkingDatesProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current CompanyInfoProviderBase instance.
		///</summary>
		public virtual CompanyInfoProviderBase CompanyInfoProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current SecurityRealtimeProviderBase instance.
		///</summary>
		public virtual SecurityRealtimeProviderBase SecurityRealtimeProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current MatchedProviderBase instance.
		///</summary>
		public virtual MatchedProviderBase MatchedProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current UpcomStocksProviderBase instance.
		///</summary>
		public virtual UpcomStocksProviderBase UpcomStocksProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current TotalmarketProviderBase instance.
		///</summary>
		public virtual TotalmarketProviderBase TotalmarketProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current LeProviderBase instance.
		///</summary>
		public virtual LeProviderBase LeProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current UpcomMarketProviderBase instance.
		///</summary>
		public virtual UpcomMarketProviderBase UpcomMarketProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current HastcMarketProviderBase instance.
		///</summary>
		public virtual HastcMarketProviderBase HastcMarketProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current LanguageProviderBase instance.
		///</summary>
		public virtual LanguageProviderBase LanguageProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current HastcStocksProviderBase instance.
		///</summary>
		public virtual HastcStocksProviderBase HastcStocksProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current CompanyInfoLanguageProviderBase instance.
		///</summary>
		public virtual CompanyInfoLanguageProviderBase CompanyInfoLanguageProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current IndexsProviderBase instance.
		///</summary>
		public virtual IndexsProviderBase IndexsProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current HastcTransactionsProviderBase instance.
		///</summary>
		public virtual HastcTransactionsProviderBase HastcTransactionsProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current UpcomTransactionsProviderBase instance.
		///</summary>
		public virtual UpcomTransactionsProviderBase UpcomTransactionsProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current HoseTransactionsProviderBase instance.
		///</summary>
		public virtual HoseTransactionsProviderBase HoseTransactionsProvider{get {throw new NotImplementedException();}}

        ///<summary>
        /// Current IndexVn30ProviderBase instance.
        ///</summary>
        public virtual IndexVn30ProviderBase IndexVn30Provider { get { throw new NotImplementedException(); } }

        ///<summary>
        /// Current IndexVn30HistoryProviderBase instance.
        ///</summary>
        public virtual IndexVn30HistoryProviderBase IndexVn30HistoryProvider { get { throw new NotImplementedException(); } }
		
	}
}
