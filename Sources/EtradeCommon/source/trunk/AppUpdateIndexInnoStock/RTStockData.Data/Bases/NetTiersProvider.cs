
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
		/// Current IndexsProviderBase instance.
		///</summary>
		public virtual IndexsProviderBase IndexsProvider{get {throw new NotImplementedException();}}
		
		
	}
}
