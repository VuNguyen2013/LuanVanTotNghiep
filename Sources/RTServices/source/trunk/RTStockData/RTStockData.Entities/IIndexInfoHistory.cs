﻿using System;
using System.ComponentModel;

namespace RTStockData.Entities
{
	/// <summary>
	///		The data structure representation of the 'IndexInfoHistory' table via interface.
	/// </summary>
	/// <remarks>
	/// 	This struct is generated by a tool and should never be modified.
	/// </remarks>
	public interface IIndexInfoHistory 
	{
		/// <summary>			
		/// ID : 
		/// </summary>
		/// <remarks>Member of the primary key of the underlying table "IndexInfoHistory"</remarks>
		System.Int64 Id { get; set; }
				
		
		
		/// <summary>
		/// IndexId : 
		/// </summary>
		System.Int64?  IndexId  { get; set; }
		
		/// <summary>
		/// IndexCode : 
		/// </summary>
		System.String  IndexCode  { get; set; }
		
		/// <summary>
		/// Name : 
		/// </summary>
		System.String  Name  { get; set; }
		
		/// <summary>
		/// Description : 
		/// </summary>
		System.String  Description  { get; set; }
		
		/// <summary>
		/// TradingDate : 
		/// </summary>
		System.DateTime?  TradingDate  { get; set; }
		
		/// <summary>
		/// Time : 
		/// </summary>
		System.Int64?  Time  { get; set; }
		
		/// <summary>
		/// CurrentStatus : 
		/// </summary>
		System.Decimal?  CurrentStatus  { get; set; }
		
		/// <summary>
		/// TotalStock : 
		/// </summary>
		System.Decimal?  TotalStock  { get; set; }
		
		/// <summary>
		/// Advances : 
		/// </summary>
		System.Decimal?  Advances  { get; set; }
		
		/// <summary>
		/// Nochange : 
		/// </summary>
		System.Decimal?  Nochange  { get; set; }
		
		/// <summary>
		/// Declines : 
		/// </summary>
		System.Decimal?  Declines  { get; set; }
		
		/// <summary>
		/// TotalQtty : 
		/// </summary>
		System.Decimal?  TotalQtty  { get; set; }
		
		/// <summary>
		/// TotalValue : 
		/// </summary>
		System.Decimal?  TotalValue  { get; set; }
		
		/// <summary>
		/// PriorIndexVal : 
		/// </summary>
		System.Decimal?  PriorIndexVal  { get; set; }
		
		/// <summary>
		/// ChgIndex : 
		/// </summary>
		System.Decimal?  ChgIndex  { get; set; }
		
		/// <summary>
		/// PctIndex : 
		/// </summary>
		System.Decimal?  PctIndex  { get; set; }
		
		/// <summary>
		/// CurrentIndex : 
		/// </summary>
		System.Decimal?  CurrentIndex  { get; set; }
		
		/// <summary>
		/// HighestIndex : 
		/// </summary>
		System.Decimal?  HighestIndex  { get; set; }
		
		/// <summary>
		/// LowestIndex : 
		/// </summary>
		System.Decimal?  LowestIndex  { get; set; }
		
		/// <summary>
		/// SessionNo : 
		/// </summary>
		System.Decimal?  SessionNo  { get; set; }
		
		/// <summary>
		/// TypeIndex : 
		/// </summary>
		System.Decimal?  TypeIndex  { get; set; }
		
		/// <summary>
		/// CloseIndex : 
		/// </summary>
		System.Decimal?  CloseIndex  { get; set; }
		
		/// <summary>
		/// TradeDate : 
		/// </summary>
		System.DateTime?  TradeDate  { get; set; }
			
		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		System.Object Clone();
		
		#region Data Properties

		#endregion Data Properties

	}
}


