
#region Using directives

using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Configuration.Provider;

using AccountManager.Entities;

#endregion

namespace AccountManager.DataAccess.Bases
{	
	///<summary>
	/// The base class to implements to create a .NetTiers provider.
	///</summary>
	public abstract class NetTiersProvider : NetTiersProviderBase
	{
		
		///<summary>
		/// Current OpenCustAccountProviderBase instance.
		///</summary>
		public virtual OpenCustAccountProviderBase OpenCustAccountProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BrokerAccountProviderBase instance.
		///</summary>
		public virtual BrokerAccountProviderBase BrokerAccountProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current MainCustAccountProviderBase instance.
		///</summary>
		public virtual MainCustAccountProviderBase MainCustAccountProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current ResearchProviderBase instance.
		///</summary>
		public virtual ResearchProviderBase ResearchProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current SmsCountProviderBase instance.
		///</summary>
		public virtual SmsCountProviderBase SmsCountProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current SubCustAccountProviderBase instance.
		///</summary>
		public virtual SubCustAccountProviderBase SubCustAccountProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current LanguageProviderBase instance.
		///</summary>
		public virtual LanguageProviderBase LanguageProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current SubCustAccountPermissionProviderBase instance.
		///</summary>
		public virtual SubCustAccountPermissionProviderBase SubCustAccountPermissionProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current HolidaysProviderBase instance.
		///</summary>
		public virtual HolidaysProviderBase HolidaysProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BrokerAmPermissionProviderBase instance.
		///</summary>
		public virtual BrokerAmPermissionProviderBase BrokerAmPermissionProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BuyRightProviderBase instance.
		///</summary>
		public virtual BuyRightProviderBase BuyRightProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BrokerPermissionProviderBase instance.
		///</summary>
		public virtual BrokerPermissionProviderBase BrokerPermissionProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current CustServicesPermissionProviderBase instance.
		///</summary>
		public virtual CustServicesPermissionProviderBase CustServicesPermissionProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current ConfigurationsProviderBase instance.
		///</summary>
		public virtual ConfigurationsProviderBase ConfigurationsProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current WorkingDaysProviderBase instance.
		///</summary>
		public virtual WorkingDaysProviderBase WorkingDaysProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current CustomerActionHistoryProviderBase instance.
		///</summary>
		public virtual CustomerActionHistoryProviderBase CustomerActionHistoryProvider{get {throw new NotImplementedException();}}
		
		
	}
}
