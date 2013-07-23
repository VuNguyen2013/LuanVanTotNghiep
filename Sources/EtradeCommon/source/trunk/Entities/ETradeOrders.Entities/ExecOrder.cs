﻿#region Using directives

using System;

#endregion

namespace ETradeOrders.Entities
{	
	///<summary>
	/// An object representation of the 'ExecOrder' table. [No description found the database]	
	///</summary>
	/// <remarks>
	/// This file is generated once and will never be overwritten.
	/// </remarks>	
	[Serializable]
	[CLSCompliant(true)]
	public partial class ExecOrder : ExecOrderBase
	{		
		#region Constructors

		///<summary>
		/// Creates a new <see cref="ExecOrder"/> instance.
		///</summary>
		public ExecOrder():base(){}	
		
		#endregion

        /// <summary>
        /// Check this order can be canceled or not.
        /// </summary>
	    public bool canCancel;
        public bool canUpdate;
	}
}
