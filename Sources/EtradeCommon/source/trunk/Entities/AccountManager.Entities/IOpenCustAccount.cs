﻿using System;
using System.ComponentModel;

namespace AccountManager.Entities
{
	/// <summary>
	///		The data structure representation of the 'OpenCustAccount' table via interface.
	/// </summary>
	/// <remarks>
	/// 	This struct is generated by a tool and should never be modified.
	/// </remarks>
	public interface IOpenCustAccount 
	{
		/// <summary>			
		/// OpenID : 
		/// </summary>
		/// <remarks>Member of the primary key of the underlying table "OpenCustAccount"</remarks>
		System.String OpenId { get; set; }
				
		/// <summary>
		/// keep a copy of the original so it can be used for editable primary keys.
		/// </summary>
		System.String OriginalOpenId { get; set; }
			
		
		
		/// <summary>
		/// RegisterDate : 
		/// </summary>
		System.DateTime?  RegisterDate  { get; set; }
		
		/// <summary>
		/// CardID : 
		/// </summary>
		System.String  CardId  { get; set; }
		
		/// <summary>
		/// CardIssue : 
		/// </summary>
		System.DateTime?  CardIssue  { get; set; }
		
		/// <summary>
		/// PlaceIssue : 
		/// </summary>
		System.String  PlaceIssue  { get; set; }
		
		/// <summary>
		/// Name : 
		/// </summary>
		System.String  Name  { get; set; }
		
		/// <summary>
		/// Birthday : 
		/// </summary>
		System.DateTime?  Birthday  { get; set; }
		
		/// <summary>
		/// Sex : 
		/// </summary>
		System.Boolean?  Sex  { get; set; }
		
		/// <summary>
		/// Occupation : 
		/// </summary>
		System.String  Occupation  { get; set; }
		
		/// <summary>
		/// Nationality : 
		/// </summary>
		System.String  Nationality  { get; set; }
		
		/// <summary>
		/// Address1 : 
		/// </summary>
		System.String  Address1  { get; set; }
		
		/// <summary>
		/// Telephone1 : 
		/// </summary>
		System.String  Telephone1  { get; set; }
		
		/// <summary>
		/// Fax1 : 
		/// </summary>
		System.String  Fax1  { get; set; }
		
		/// <summary>
		/// Address2 : 
		/// </summary>
		System.String  Address2  { get; set; }
		
		/// <summary>
		/// Telephone2 : 
		/// </summary>
		System.String  Telephone2  { get; set; }
		
		/// <summary>
		/// Fax2 : 
		/// </summary>
		System.String  Fax2  { get; set; }
		
		/// <summary>
		/// Address3 : 
		/// </summary>
		System.String  Address3  { get; set; }
		
		/// <summary>
		/// Telephone3 : 
		/// </summary>
		System.String  Telephone3  { get; set; }
		
		/// <summary>
		/// Fax3 : 
		/// </summary>
		System.String  Fax3  { get; set; }
		
		/// <summary>
		/// Email : 
		/// </summary>
		System.String  Email  { get; set; }
		
		/// <summary>
		/// BranchCode : 
		/// </summary>
		System.String  BranchCode  { get; set; }
		
		/// <summary>
		/// BranchName : 
		/// </summary>
		System.String  BranchName  { get; set; }
		
		/// <summary>
		/// Custodian : 
		/// </summary>
		System.Boolean?  Custodian  { get; set; }
		
		/// <summary>
		/// CustomerType : 
		/// </summary>
		System.String  CustomerType  { get; set; }
		
		/// <summary>
		/// TradeAtCompany : 
		/// </summary>
		System.Boolean?  TradeAtCompany  { get; set; }
		
		/// <summary>
		/// TradeByTelephone : 
		/// </summary>
		System.Boolean?  TradeByTelephone  { get; set; }
		
		/// <summary>
		/// TradeOnline : 
		/// </summary>
		System.Boolean?  TradeOnline  { get; set; }
		
		/// <summary>
		/// ExistedAccount : 
		/// </summary>
		System.Boolean?  ExistedAccount  { get; set; }
			
		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		System.Object Clone();
		
		#region Data Properties

		#endregion Data Properties

	}
}


