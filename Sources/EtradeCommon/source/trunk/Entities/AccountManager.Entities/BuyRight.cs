#region Using directives

using System;

#endregion

namespace AccountManager.Entities
{	
	///<summary>
	/// An object representation of the 'BuyRight' table. [No description found the database]	
	///</summary>
	/// <remarks>
	/// This file is generated once and will never be overwritten.
	/// </remarks>	
	[Serializable]
	[CLSCompliant(true)]
	public partial class BuyRight : BuyRightBase
	{
        /// <summary>
        /// Gets or sets the last date register.
        /// </summary>
        /// <value>The last date register.</value>
        public DateTime LastDateRegister { get; set; }
	}
}
