﻿using System;
using System.ComponentModel;

namespace RTStockData.Entities
{
	/// <summary>
	///		The data structure representation of the 'Index_VN30' table via interface.
	/// </summary>
	/// <remarks>
	/// 	This struct is generated by a tool and should never be modified.
	/// </remarks>
	public interface IIndexVn30 
	{
		/// <summary>			
		/// ID : 
		/// </summary>
		/// <remarks>Member of the primary key of the underlying table "Index_VN30"</remarks>
		System.Int64 Id { get; set; }
				
		
		
		/// <summary>
		/// TradeDate : 
		/// </summary>
		System.DateTime?  TradeDate  { get; set; }
		
		/// <summary>
		/// Index : 
		/// </summary>
		System.Int64?  Index  { get; set; }
		
		/// <summary>
		/// TotalShares : 
		/// </summary>
		System.Int64?  TotalShares  { get; set; }
		
		/// <summary>
		/// TotalValues : 
		/// </summary>
		System.Int64?  TotalValues  { get; set; }
		
		/// <summary>
		/// Up : 
		/// </summary>
		System.Int64?  Up  { get; set; }
		
		/// <summary>
		/// Down : 
		/// </summary>
		System.Int64?  Down  { get; set; }
		
		/// <summary>
		/// NoChange : 
		/// </summary>
		System.Int64?  NoChange  { get; set; }
		
		/// <summary>
		/// Time : 
		/// </summary>
		System.Int64?  Time  { get; set; }
			
		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		System.Object Clone();
		
		#region Data Properties

		#endregion Data Properties

	}
}

