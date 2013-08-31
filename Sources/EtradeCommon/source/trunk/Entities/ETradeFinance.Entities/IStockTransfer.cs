﻿using System;
using System.ComponentModel;

namespace ETradeFinance.Entities
{
	/// <summary>
	///		The data structure representation of the 'StockTransfer' table via interface.
	/// </summary>
	/// <remarks>
	/// 	This struct is generated by a tool and should never be modified.
	/// </remarks>
	public interface IStockTransfer 
	{
		/// <summary>			
		/// ID : StockTransferID identifies StockTransfer
		/// </summary>
		/// <remarks>Member of the primary key of the underlying table "StockTransfer"</remarks>
		System.Int64 Id { get; set; }
				
		
		
		/// <summary>
		/// SecSymbol : Ma co phieu chuyen
		/// </summary>
		System.String  SecSymbol  { get; set; }
		
		/// <summary>
		/// WithdrawableAmt : Tong so luong CP co the rut/chuyen lay tu core
		/// </summary>
		System.Int64?  WithdrawableAmt  { get; set; }
		
		/// <summary>
		/// TransferedAmt : Tong so luong CP da yeu cau chuyen(dang cho xu ly hoac dang xu ly)
		/// </summary>
		System.Int64?  TransferedAmt  { get; set; }
		
		/// <summary>
		/// AdvOrderAmt : Tong so luong CP da dat ban truoc chua khop (dang cho kich hoat, chua gui vao core)
		/// </summary>
		System.Int64?  AdvOrderAmt  { get; set; }
		
		/// <summary>
		/// AvilableAmt : Tong so luong CP hien tai duoc phep chuyen
		/// </summary>
		System.Int64?  AvilableAmt  { get; set; }
		
		/// <summary>
		/// RequestAmt : So luong CP khach hang yeu cau chuyen
		/// </summary>
		System.Int64?  RequestAmt  { get; set; }
		
		/// <summary>
		/// RequestTime : Thoi gian(ngay, gio) yeu cau chuyen co phieu
		/// </summary>
		System.DateTime  RequestTime  { get; set; }
		
		/// <summary>
		/// SrcAccountID : Tai khoan nguon
		/// </summary>
		System.String  SrcAccountId  { get; set; }
		
		/// <summary>
		/// DestAccountID : Tai khoan dich
		/// </summary>
		System.String  DestAccountId  { get; set; }
		
		/// <summary>
		/// TransType : Loai giao dich: chuyen, rut, khac
		/// </summary>
		System.Int32?  TransType  { get; set; }
		
		/// <summary>
		/// Status : Trang thai cua yeu cau
		/// </summary>
		System.Int32?  Status  { get; set; }
		
		/// <summary>
		/// ExecTime : Ngay gio xu ly
		/// </summary>
		System.DateTime?  ExecTime  { get; set; }
		
		/// <summary>
		/// ApprovedAmt : So luong CP da duoc chap nhan
		/// </summary>
		System.Int64?  ApprovedAmt  { get; set; }
		
		/// <summary>
		/// Note : Ghi chu cua Broker
		/// </summary>
		System.String  Note  { get; set; }
		
		/// <summary>
		/// BrokerID : Id c?a broker xu ly request.
		/// </summary>
		System.String  BrokerId  { get; set; }
			
		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		System.Object Clone();
		
		#region Data Properties

		#endregion Data Properties

	}
}

