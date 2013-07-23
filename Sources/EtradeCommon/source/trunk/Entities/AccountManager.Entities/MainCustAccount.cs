#region Using directives

using System;
using System.Reflection;
using AccountManager.Entities.Validation;

#endregion

namespace AccountManager.Entities
{	
	///<summary>
	/// An object representation of the 'MainCustAccount' table. [No description found the database]	
	///</summary>
	/// <remarks>
	/// This file is generated once and will never be overwritten.
	/// </remarks>	
	[Serializable]
	[CLSCompliant(true)]
	public partial class MainCustAccount : MainCustAccountBase
	{		
		#region Constructors

		///<summary>
		/// Creates a new <see cref="MainCustAccount"/> instance.
		///</summary>
		public MainCustAccount():base(){}	
		
		#endregion

        ///<summary>
        /// Store broker name.
        ///</summary>
        public string BrokerName { get; set; }
	}
}
