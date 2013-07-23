﻿using System;
using System.ComponentModel;

namespace AccountManager.Entities
{
	/// <summary>
	///		The data structure representation of the 'MainCustAccount' table via interface.
	/// </summary>
	/// <remarks>
	/// 	This struct is generated by a tool and should never be modified.
	/// </remarks>
	public interface IMainCustAccount 
	{
		/// <summary>			
		/// MainCustAccountID : Main customer account id
		/// </summary>
		/// <remarks>Member of the primary key of the underlying table "MainCustAccount"</remarks>
		string MainCustAccountId { get; set; }
				
		/// <summary>
		/// keep a copy of the original so it can be used for editable primary keys.
		/// </summary>
		string OriginalMainCustAccountId { get; set; }
			
		
		
		/// <summary>
		/// FullName : Full customer name
		/// </summary>
		string  FullName  { get; set; }
		
		/// <summary>
		/// Email : Customer email
		/// </summary>
		string  Email  { get; set; }
		
		/// <summary>
		/// Phone : Phone
		/// </summary>
		string  Phone  { get; set; }
		
		/// <summary>
		/// Actived : Is actived or not
		/// </summary>
		bool  Actived  { get; set; }
		
		/// <summary>
		/// Password : Password
		/// </summary>
		string  Password  { get; set; }
		
		/// <summary>
		/// PIN : Pin
		/// </summary>
		string  Pin  { get; set; }
		
		/// <summary>
		/// PassLockReason : Password lock reason
		/// </summary>
		System.Int32?  PassLockReason  { get; set; }
		
		/// <summary>
		/// PINLockReason : Pin lock reason
		/// </summary>
		System.Int32?  PinLockReason  { get; set; }
		
		/// <summary>
		/// LockReason : Account lock reason
		/// </summary>
		System.Int32?  LockReason  { get; set; }
		
		/// <summary>
		/// TokenID : Token id
		/// </summary>
		string  TokenId  { get; set; }
		
		/// <summary>
		/// TokenName : Token name
		/// </summary>
		string  TokenName  { get; set; }
		
		/// <summary>
		/// TokenActived : Token is active or not
		/// </summary>
		string  TokenActived  { get; set; }
		
		/// <summary>
		/// BrokerID : Broker id
		/// </summary>
		string  BrokerId  { get; set; }
		
		/// <summary>
		/// PassIsNew : Password is new or not
		/// </summary>
		System.Boolean?  PassIsNew  { get; set; }
		
		/// <summary>
		/// PINIsNew : Pin is new or not
		/// </summary>
		System.Boolean?  PinIsNew  { get; set; }
		
		/// <summary>
		/// PassExpDate : Password expired date
		/// </summary>
		System.DateTime?  PassExpDate  { get; set; }
		
		/// <summary>
		/// PINExpDate : Pin expired date
		/// </summary>
		System.DateTime?  PinExpDate  { get; set; }
		
		/// <summary>
		/// CustomerType : Customer type
		/// </summary>
		int  CustomerType  { get; set; }
		
		/// <summary>
		/// AuthType : Authentication type
		/// </summary>
		short  AuthType  { get; set; }
		
		/// <summary>
		/// PinType : Pin type
		/// </summary>
		short  PinType  { get; set; }
		
		/// <summary>
		/// LanguageId : Language id
		/// </summary>
		System.String  LanguageId  { get; set; }
		
		/// <summary>
		/// FailedLoginCount : Failed login count
		/// </summary>
		System.Int32?  FailedLoginCount  { get; set; }
		
		/// <summary>
		/// FailedLoginTime : Failed login times
		/// </summary>
		System.DateTime?  FailedLoginTime  { get; set; }
		
		/// <summary>
		/// CreatedDate : Customer created date
		/// </summary>
		System.DateTime  CreatedDate  { get; set; }
		
		/// <summary>
		/// CreatedUser : Created User
		/// </summary>
		string  CreatedUser  { get; set; }
		
		/// <summary>
		/// UpdatedUser : Updated user
		/// </summary>
		string  UpdatedUser  { get; set; }
		
		/// <summary>
		/// UpdatedDate : Updated date
		/// </summary>
		System.DateTime?  UpdatedDate  { get; set; }
			
		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		System.Object Clone();
		
		#region Data Properties


		/// <summary>
		///	Holds a collection of entity objects
		///	which are related to this object through the relation _subCustAccountMainCustAccountId
		/// </summary>	
		TList<SubCustAccount> SubCustAccountCollection {  get;  set;}	


		/// <summary>
		///	Holds a collection of entity objects
		///	which are related to this object through the relation _customerActionHistoryMainCustAccountId
		/// </summary>	
		TList<CustomerActionHistory> CustomerActionHistoryCollection {  get;  set;}	

		#endregion Data Properties

	}
}


