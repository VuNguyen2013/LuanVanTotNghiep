﻿using System;
using System.ComponentModel;

namespace RTStockData.Entities
{
	/// <summary>
	///		The data structure representation of the 'hastc_market' table via interface.
	/// </summary>
	/// <remarks>
	/// 	This struct is generated by a tool and should never be modified.
	/// </remarks>
	public interface IHastcMarket 
	{
		/// <summary>			
		/// id : 
		/// </summary>
		/// <remarks>Member of the primary key of the underlying table "hastc_market"</remarks>
		System.Int64 Id { get; set; }
				
		
		
		/// <summary>
		/// TradeDate : 
		/// </summary>
		System.DateTime?  TradeDate  { get; set; }
		
		/// <summary>
		/// SetIndex : 
		/// </summary>
		System.Double?  SetIndex  { get; set; }
		
		/// <summary>
		/// TotalTrade : 
		/// </summary>
		System.Int64?  TotalTrade  { get; set; }
		
		/// <summary>
		/// Totalshare : 
		/// </summary>
		System.Int64?  Totalshare  { get; set; }
		
		/// <summary>
		/// TotalValue : 
		/// </summary>
		System.Int64?  TotalValue  { get; set; }
		
		/// <summary>
		/// Advances : 
		/// </summary>
		System.Int16?  Advances  { get; set; }
		
		/// <summary>
		/// Nochange : 
		/// </summary>
		System.Int16?  Nochange  { get; set; }
		
		/// <summary>
		/// Declines : 
		/// </summary>
		System.Int16?  Declines  { get; set; }
		
		/// <summary>
		/// Time : 
		/// </summary>
		System.Int64?  Time  { get; set; }
		
		/// <summary>
		/// OpenIndex : 
		/// </summary>
		System.Double?  OpenIndex  { get; set; }
			
		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		System.Object Clone();
		
		#region Data Properties

		#endregion Data Properties

	}
}


