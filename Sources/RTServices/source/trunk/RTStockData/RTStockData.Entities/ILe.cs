﻿using System;
using System.ComponentModel;

namespace RTStockData.Entities
{
	/// <summary>
	///		The data structure representation of the 'le' table via interface.
	/// </summary>
	/// <remarks>
	/// 	This struct is generated by a tool and should never be modified.
	/// </remarks>
	public interface ILe 
	{
		/// <summary>			
		/// id : 
		/// </summary>
		/// <remarks>Member of the primary key of the underlying table "le"</remarks>
		System.Int64 Id { get; set; }
				
		
		
		/// <summary>
		/// TradeDate : 
		/// </summary>
		System.DateTime?  TradeDate  { get; set; }
		
		/// <summary>
		/// StockNo : 
		/// </summary>
		System.Int16?  StockNo  { get; set; }
		
		/// <summary>
		/// Price : 
		/// </summary>
		System.Int64?  Price  { get; set; }
		
		/// <summary>
		/// AccumulatedVol : 
		/// </summary>
		System.Int64?  AccumulatedVol  { get; set; }
		
		/// <summary>
		/// AccumulatedVal : 
		/// </summary>
		System.Int64?  AccumulatedVal  { get; set; }
		
		/// <summary>
		/// Highest : 
		/// </summary>
		System.Int64?  Highest  { get; set; }
		
		/// <summary>
		/// Lowest : 
		/// </summary>
		System.Int64?  Lowest  { get; set; }
		
		/// <summary>
		/// Turn : 
		/// </summary>
		System.Int64?  Turn  { get; set; }
		
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

