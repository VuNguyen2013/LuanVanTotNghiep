﻿using System;
using System.ComponentModel;

namespace AccountManager.Entities
{
	/// <summary>
	///		The data structure representation of the 'ChangedPassHistory' table via interface.
	/// </summary>
	/// <remarks>
	/// 	This struct is generated by a tool and should never be modified.
	/// </remarks>
	public interface IChangedPassHistory 
	{
		/// <summary>			
		/// ID : 
		/// </summary>
		/// <remarks>Member of the primary key of the underlying table "ChangedPassHistory"</remarks>
		System.Int32 Id { get; set; }
				
		/// <summary>
		/// keep a copy of the original so it can be used for editable primary keys.
		/// </summary>
		System.Int32 OriginalId { get; set; }
			
		
		
		/// <summary>
		/// BrokerID : 
		/// </summary>
		System.String  BrokerId  { get; set; }
		
		/// <summary>
		/// BrokerName : 
		/// </summary>
		System.String  BrokerName  { get; set; }
		
		/// <summary>
		/// MainAccountName : 
		/// </summary>
		System.String  MainAccountName  { get; set; }
		
		/// <summary>
		/// ChangedTime : 
		/// </summary>
		System.DateTime?  ChangedTime  { get; set; }
		
		/// <summary>
		/// MainCustAccountID : 
		/// </summary>
		System.String  MainCustAccountId  { get; set; }
		
		/// <summary>
		/// PINorPass : 
		/// </summary>
		System.Boolean?  PiNorPass  { get; set; }
			
		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		System.Object Clone();
		
		#region Data Properties

		#endregion Data Properties

	}
}


