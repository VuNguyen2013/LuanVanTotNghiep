﻿using System;
using System.ComponentModel;

namespace ETradeFinance.Entities
{
	/// <summary>
	///		The data structure representation of the 'XROrders' table via interface.
	/// </summary>
	/// <remarks>
	/// 	This struct is generated by a tool and should never be modified.
	/// </remarks>
	public interface IXrOrders 
	{
		/// <summary>			
		/// ID : 
		/// </summary>
		/// <remarks>Member of the primary key of the underlying table "XROrders"</remarks>
		System.Int64 Id { get; set; }
				
		
		
		/// <summary>
		/// SubAccountID : Tai khoan con dang ky quyen mua
		/// </summary>
		System.String  SubAccountId  { get; set; }
		
		/// <summary>
		/// BuyRightID : Id of buy right table
		/// </summary>
		System.Int64  BuyRightId  { get; set; }
		
		/// <summary>
		/// SecSymbol : Ma CK duoc dang ky mua
		/// </summary>
		System.String  SecSymbol  { get; set; }
		
		/// <summary>
		/// Market : Thi truong cua ma CK nay(HOSE, HNX, Upcom)
		/// </summary>
		System.String  Market  { get; set; }
		
		/// <summary>
		/// Volume : Khoi luong CP duoc phep mua
		/// </summary>
		System.Int64?  Volume  { get; set; }
		
		/// <summary>
		/// Price : Gia mac dinh cho quyen mua CP nay
		/// </summary>
		System.Decimal?  Price  { get; set; }
		
		/// <summary>
		/// RegisteredVol : KL co phieu khach da dang ky mua
		/// </summary>
		System.Int64?  RegisteredVol  { get; set; }
		
		/// <summary>
		/// AvailableVol : KL CP con lai khach duoc phep mua
		/// </summary>
		System.Int64?  AvailableVol  { get; set; }
		
		/// <summary>
		/// RequestVol : KL CP khach hang dang ky mua
		/// </summary>
		System.Int64?  RequestVol  { get; set; }
		
		/// <summary>
		/// RequestTime : Thoi gian (ngay gio) dang ky
		/// </summary>
		System.DateTime?  RequestTime  { get; set; }
		
		/// <summary>
		/// ApprovedVol : KL CP mua da chap thuan
		/// </summary>
		System.Int64?  ApprovedVol  { get; set; }
		
		/// <summary>
		/// Status : Trang thai cua request(dang cho, dang xu ly, da xu ly, da huy boi nha dau tu, tu choi boi broker)
		/// </summary>
		System.Int32  Status  { get; set; }
		
		/// <summary>
		/// BrokerID : Broker ID cua Broker xu ly request
		/// </summary>
		System.String  BrokerId  { get; set; }
		
		/// <summary>
		/// ExecTime : Thoi gian xu ly
		/// </summary>
		System.DateTime?  ExecTime  { get; set; }
		
		/// <summary>
		/// Note : Ghi chu cua Broker
		/// </summary>
		System.String  Note  { get; set; }
			
		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		System.Object Clone();
		
		#region Data Properties

		#endregion Data Properties

	}
}


