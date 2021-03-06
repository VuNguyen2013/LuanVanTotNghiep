﻿using System;
using System.ComponentModel;

namespace AccountManager.Entities
{
	/// <summary>
	///		The data structure representation of the 'CustomerActionHistory' table via interface.
	/// </summary>
	/// <remarks>
	/// 	This struct is generated by a tool and should never be modified.
	/// </remarks>
	public interface ICustomerActionHistory 
	{
		/// <summary>			
		/// ID : Auto increase ID
		/// </summary>
		/// <remarks>Member of the primary key of the underlying table "CustomerActionHistory"</remarks>
		System.Int64 Id { get; set; }
				
		
		
		/// <summary>
		/// BrokerID : BrokerID
		/// </summary>
		System.String  BrokerId  { get; set; }
		
		/// <summary>
		/// ActionTime : The time action happens
		/// </summary>
		System.DateTime  ActionTime  { get; set; }
		
		/// <summary>
		/// MainCustAccountID : Main customer account ID
		/// </summary>
		System.String  MainCustAccountId  { get; set; }
		
		/// <summary>
		/// SubCustAccountID : Sub customer account ID
		/// </summary>
		System.String  SubCustAccountId  { get; set; }
		
		/// <summary>
		/// ActionType : Action type (login, logout, ...)
		/// </summary>
		System.Int32  ActionType  { get; set; }
		
		/// <summary>
		/// Reason : Further information
		/// </summary>
		System.Int32?  Reason  { get; set; }
			
		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		System.Object Clone();
		
		#region Data Properties

		#endregion Data Properties

	}
}


