#region Using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

using AccountManager.Entities;
using AccountManager.DataAccess;

#endregion

namespace AccountManager.DataAccess.Bases
{	
	///<summary>
	/// This class is the base class for any <see cref="OpenCustAccountProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class OpenCustAccountProviderBaseCore : EntityProviderBase<AccountManager.Entities.OpenCustAccount, AccountManager.Entities.OpenCustAccountKey>
	{		
		#region Get from Many To Many Relationship Functions
		#endregion	
		
		#region Delete Methods

		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager">A <see cref="TransactionManager"/> object.</param>
		/// <param name="key">The unique identifier of the row to delete.</param>
		/// <returns>Returns true if operation suceeded.</returns>
		public override bool Delete(TransactionManager transactionManager, AccountManager.Entities.OpenCustAccountKey key)
		{
			return Delete(transactionManager, key.OpenId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_openId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.String _openId)
		{
			return Delete(null, _openId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_openId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.String _openId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
		#endregion

		#region Get By Index Functions
		
		/// <summary>
		/// 	Gets a row from the DataSource based on its primary key.
		/// </summary>
		/// <param name="transactionManager">A <see cref="TransactionManager"/> object.</param>
		/// <param name="key">The unique identifier of the row to retrieve.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <returns>Returns an instance of the Entity class.</returns>
		public override AccountManager.Entities.OpenCustAccount Get(TransactionManager transactionManager, AccountManager.Entities.OpenCustAccountKey key, int start, int pageLength)
		{
			return GetByOpenId(transactionManager, key.OpenId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_OpenCustAccount index.
		/// </summary>
		/// <param name="_openId"></param>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.OpenCustAccount"/> class.</returns>
		public AccountManager.Entities.OpenCustAccount GetByOpenId(System.String _openId)
		{
			int count = -1;
			return GetByOpenId(null,_openId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_OpenCustAccount index.
		/// </summary>
		/// <param name="_openId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.OpenCustAccount"/> class.</returns>
		public AccountManager.Entities.OpenCustAccount GetByOpenId(System.String _openId, int start, int pageLength)
		{
			int count = -1;
			return GetByOpenId(null, _openId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_OpenCustAccount index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_openId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.OpenCustAccount"/> class.</returns>
		public AccountManager.Entities.OpenCustAccount GetByOpenId(TransactionManager transactionManager, System.String _openId)
		{
			int count = -1;
			return GetByOpenId(transactionManager, _openId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_OpenCustAccount index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_openId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.OpenCustAccount"/> class.</returns>
		public AccountManager.Entities.OpenCustAccount GetByOpenId(TransactionManager transactionManager, System.String _openId, int start, int pageLength)
		{
			int count = -1;
			return GetByOpenId(transactionManager, _openId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_OpenCustAccount index.
		/// </summary>
		/// <param name="_openId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.OpenCustAccount"/> class.</returns>
		public AccountManager.Entities.OpenCustAccount GetByOpenId(System.String _openId, int start, int pageLength, out int count)
		{
			return GetByOpenId(null, _openId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_OpenCustAccount index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_openId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.OpenCustAccount"/> class.</returns>
		public abstract AccountManager.Entities.OpenCustAccount GetByOpenId(TransactionManager transactionManager, System.String _openId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;OpenCustAccount&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;OpenCustAccount&gt;"/></returns>
		public static TList<OpenCustAccount> Fill(IDataReader reader, TList<OpenCustAccount> rows, int start, int pageLength)
		{
			NetTiersProvider currentProvider = DataRepository.Provider;
            bool useEntityFactory = currentProvider.UseEntityFactory;
            bool enableEntityTracking = currentProvider.EnableEntityTracking;
            LoadPolicy currentLoadPolicy = currentProvider.CurrentLoadPolicy;
			Type entityCreationFactoryType = currentProvider.EntityCreationalFactoryType;
			
			// advance to the starting row
			for (int i = 0; i < start; i++)
			{
				if (!reader.Read())
				return rows; // not enough rows, just return
			}
			for (int i = 0; i < pageLength; i++)
			{
				if (!reader.Read())
					break; // we are done
					
				string key = null;
				
				AccountManager.Entities.OpenCustAccount c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("OpenCustAccount")
					.Append("|").Append((System.String)reader[((int)OpenCustAccountColumn.OpenId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<OpenCustAccount>(
					key.ToString(), // EntityTrackingKey
					"OpenCustAccount",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new AccountManager.Entities.OpenCustAccount();
				}
				
				if (!enableEntityTracking ||
					c.EntityState == EntityState.Added ||
					(enableEntityTracking &&
					
						(
							(currentLoadPolicy == LoadPolicy.PreserveChanges && c.EntityState == EntityState.Unchanged) ||
							(currentLoadPolicy == LoadPolicy.DiscardChanges && c.EntityState != EntityState.Unchanged)
						)
					))
				{
					c.SuppressEntityEvents = true;
					c.OpenId = (System.String)reader[((int)OpenCustAccountColumn.OpenId - 1)];
					c.OriginalOpenId = c.OpenId;
					c.RegisterDate = (reader.IsDBNull(((int)OpenCustAccountColumn.RegisterDate - 1)))?null:(System.DateTime?)reader[((int)OpenCustAccountColumn.RegisterDate - 1)];
					c.CardId = (reader.IsDBNull(((int)OpenCustAccountColumn.CardId - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.CardId - 1)];
					c.CardIssue = (reader.IsDBNull(((int)OpenCustAccountColumn.CardIssue - 1)))?null:(System.DateTime?)reader[((int)OpenCustAccountColumn.CardIssue - 1)];
					c.PlaceIssue = (reader.IsDBNull(((int)OpenCustAccountColumn.PlaceIssue - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.PlaceIssue - 1)];
					c.Name = (reader.IsDBNull(((int)OpenCustAccountColumn.Name - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.Name - 1)];
					c.Birthday = (reader.IsDBNull(((int)OpenCustAccountColumn.Birthday - 1)))?null:(System.DateTime?)reader[((int)OpenCustAccountColumn.Birthday - 1)];
					c.Sex = (reader.IsDBNull(((int)OpenCustAccountColumn.Sex - 1)))?null:(System.Boolean?)reader[((int)OpenCustAccountColumn.Sex - 1)];
					c.Occupation = (reader.IsDBNull(((int)OpenCustAccountColumn.Occupation - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.Occupation - 1)];
					c.Nationality = (reader.IsDBNull(((int)OpenCustAccountColumn.Nationality - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.Nationality - 1)];
					c.Address1 = (reader.IsDBNull(((int)OpenCustAccountColumn.Address1 - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.Address1 - 1)];
					c.Telephone1 = (reader.IsDBNull(((int)OpenCustAccountColumn.Telephone1 - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.Telephone1 - 1)];
					c.Fax1 = (reader.IsDBNull(((int)OpenCustAccountColumn.Fax1 - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.Fax1 - 1)];
					c.Address2 = (reader.IsDBNull(((int)OpenCustAccountColumn.Address2 - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.Address2 - 1)];
					c.Telephone2 = (reader.IsDBNull(((int)OpenCustAccountColumn.Telephone2 - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.Telephone2 - 1)];
					c.Fax2 = (reader.IsDBNull(((int)OpenCustAccountColumn.Fax2 - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.Fax2 - 1)];
					c.Address3 = (reader.IsDBNull(((int)OpenCustAccountColumn.Address3 - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.Address3 - 1)];
					c.Telephone3 = (reader.IsDBNull(((int)OpenCustAccountColumn.Telephone3 - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.Telephone3 - 1)];
					c.Fax3 = (reader.IsDBNull(((int)OpenCustAccountColumn.Fax3 - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.Fax3 - 1)];
					c.Email = (reader.IsDBNull(((int)OpenCustAccountColumn.Email - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.Email - 1)];
					c.BranchCode = (reader.IsDBNull(((int)OpenCustAccountColumn.BranchCode - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.BranchCode - 1)];
					c.BranchName = (reader.IsDBNull(((int)OpenCustAccountColumn.BranchName - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.BranchName - 1)];
					c.Custodian = (reader.IsDBNull(((int)OpenCustAccountColumn.Custodian - 1)))?null:(System.Boolean?)reader[((int)OpenCustAccountColumn.Custodian - 1)];
					c.CustomerType = (reader.IsDBNull(((int)OpenCustAccountColumn.CustomerType - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.CustomerType - 1)];
					c.TradeAtCompany = (reader.IsDBNull(((int)OpenCustAccountColumn.TradeAtCompany - 1)))?null:(System.Boolean?)reader[((int)OpenCustAccountColumn.TradeAtCompany - 1)];
					c.TradeByTelephone = (reader.IsDBNull(((int)OpenCustAccountColumn.TradeByTelephone - 1)))?null:(System.Boolean?)reader[((int)OpenCustAccountColumn.TradeByTelephone - 1)];
					c.TradeOnline = (reader.IsDBNull(((int)OpenCustAccountColumn.TradeOnline - 1)))?null:(System.Boolean?)reader[((int)OpenCustAccountColumn.TradeOnline - 1)];
					c.ExistedAccount = (reader.IsDBNull(((int)OpenCustAccountColumn.ExistedAccount - 1)))?null:(System.Boolean?)reader[((int)OpenCustAccountColumn.ExistedAccount - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="AccountManager.Entities.OpenCustAccount"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AccountManager.Entities.OpenCustAccount"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AccountManager.Entities.OpenCustAccount entity)
		{
			if (!reader.Read()) return;
			
			entity.OpenId = (System.String)reader[((int)OpenCustAccountColumn.OpenId - 1)];
			entity.OriginalOpenId = (System.String)reader["OpenID"];
			entity.RegisterDate = (reader.IsDBNull(((int)OpenCustAccountColumn.RegisterDate - 1)))?null:(System.DateTime?)reader[((int)OpenCustAccountColumn.RegisterDate - 1)];
			entity.CardId = (reader.IsDBNull(((int)OpenCustAccountColumn.CardId - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.CardId - 1)];
			entity.CardIssue = (reader.IsDBNull(((int)OpenCustAccountColumn.CardIssue - 1)))?null:(System.DateTime?)reader[((int)OpenCustAccountColumn.CardIssue - 1)];
			entity.PlaceIssue = (reader.IsDBNull(((int)OpenCustAccountColumn.PlaceIssue - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.PlaceIssue - 1)];
			entity.Name = (reader.IsDBNull(((int)OpenCustAccountColumn.Name - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.Name - 1)];
			entity.Birthday = (reader.IsDBNull(((int)OpenCustAccountColumn.Birthday - 1)))?null:(System.DateTime?)reader[((int)OpenCustAccountColumn.Birthday - 1)];
			entity.Sex = (reader.IsDBNull(((int)OpenCustAccountColumn.Sex - 1)))?null:(System.Boolean?)reader[((int)OpenCustAccountColumn.Sex - 1)];
			entity.Occupation = (reader.IsDBNull(((int)OpenCustAccountColumn.Occupation - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.Occupation - 1)];
			entity.Nationality = (reader.IsDBNull(((int)OpenCustAccountColumn.Nationality - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.Nationality - 1)];
			entity.Address1 = (reader.IsDBNull(((int)OpenCustAccountColumn.Address1 - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.Address1 - 1)];
			entity.Telephone1 = (reader.IsDBNull(((int)OpenCustAccountColumn.Telephone1 - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.Telephone1 - 1)];
			entity.Fax1 = (reader.IsDBNull(((int)OpenCustAccountColumn.Fax1 - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.Fax1 - 1)];
			entity.Address2 = (reader.IsDBNull(((int)OpenCustAccountColumn.Address2 - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.Address2 - 1)];
			entity.Telephone2 = (reader.IsDBNull(((int)OpenCustAccountColumn.Telephone2 - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.Telephone2 - 1)];
			entity.Fax2 = (reader.IsDBNull(((int)OpenCustAccountColumn.Fax2 - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.Fax2 - 1)];
			entity.Address3 = (reader.IsDBNull(((int)OpenCustAccountColumn.Address3 - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.Address3 - 1)];
			entity.Telephone3 = (reader.IsDBNull(((int)OpenCustAccountColumn.Telephone3 - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.Telephone3 - 1)];
			entity.Fax3 = (reader.IsDBNull(((int)OpenCustAccountColumn.Fax3 - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.Fax3 - 1)];
			entity.Email = (reader.IsDBNull(((int)OpenCustAccountColumn.Email - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.Email - 1)];
			entity.BranchCode = (reader.IsDBNull(((int)OpenCustAccountColumn.BranchCode - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.BranchCode - 1)];
			entity.BranchName = (reader.IsDBNull(((int)OpenCustAccountColumn.BranchName - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.BranchName - 1)];
			entity.Custodian = (reader.IsDBNull(((int)OpenCustAccountColumn.Custodian - 1)))?null:(System.Boolean?)reader[((int)OpenCustAccountColumn.Custodian - 1)];
			entity.CustomerType = (reader.IsDBNull(((int)OpenCustAccountColumn.CustomerType - 1)))?null:(System.String)reader[((int)OpenCustAccountColumn.CustomerType - 1)];
			entity.TradeAtCompany = (reader.IsDBNull(((int)OpenCustAccountColumn.TradeAtCompany - 1)))?null:(System.Boolean?)reader[((int)OpenCustAccountColumn.TradeAtCompany - 1)];
			entity.TradeByTelephone = (reader.IsDBNull(((int)OpenCustAccountColumn.TradeByTelephone - 1)))?null:(System.Boolean?)reader[((int)OpenCustAccountColumn.TradeByTelephone - 1)];
			entity.TradeOnline = (reader.IsDBNull(((int)OpenCustAccountColumn.TradeOnline - 1)))?null:(System.Boolean?)reader[((int)OpenCustAccountColumn.TradeOnline - 1)];
			entity.ExistedAccount = (reader.IsDBNull(((int)OpenCustAccountColumn.ExistedAccount - 1)))?null:(System.Boolean?)reader[((int)OpenCustAccountColumn.ExistedAccount - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="AccountManager.Entities.OpenCustAccount"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AccountManager.Entities.OpenCustAccount"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AccountManager.Entities.OpenCustAccount entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.OpenId = (System.String)dataRow["OpenID"];
			entity.OriginalOpenId = (System.String)dataRow["OpenID"];
			entity.RegisterDate = Convert.IsDBNull(dataRow["RegisterDate"]) ? null : (System.DateTime?)dataRow["RegisterDate"];
			entity.CardId = Convert.IsDBNull(dataRow["CardID"]) ? null : (System.String)dataRow["CardID"];
			entity.CardIssue = Convert.IsDBNull(dataRow["CardIssue"]) ? null : (System.DateTime?)dataRow["CardIssue"];
			entity.PlaceIssue = Convert.IsDBNull(dataRow["PlaceIssue"]) ? null : (System.String)dataRow["PlaceIssue"];
			entity.Name = Convert.IsDBNull(dataRow["Name"]) ? null : (System.String)dataRow["Name"];
			entity.Birthday = Convert.IsDBNull(dataRow["Birthday"]) ? null : (System.DateTime?)dataRow["Birthday"];
			entity.Sex = Convert.IsDBNull(dataRow["Sex"]) ? null : (System.Boolean?)dataRow["Sex"];
			entity.Occupation = Convert.IsDBNull(dataRow["Occupation"]) ? null : (System.String)dataRow["Occupation"];
			entity.Nationality = Convert.IsDBNull(dataRow["Nationality"]) ? null : (System.String)dataRow["Nationality"];
			entity.Address1 = Convert.IsDBNull(dataRow["Address1"]) ? null : (System.String)dataRow["Address1"];
			entity.Telephone1 = Convert.IsDBNull(dataRow["Telephone1"]) ? null : (System.String)dataRow["Telephone1"];
			entity.Fax1 = Convert.IsDBNull(dataRow["Fax1"]) ? null : (System.String)dataRow["Fax1"];
			entity.Address2 = Convert.IsDBNull(dataRow["Address2"]) ? null : (System.String)dataRow["Address2"];
			entity.Telephone2 = Convert.IsDBNull(dataRow["Telephone2"]) ? null : (System.String)dataRow["Telephone2"];
			entity.Fax2 = Convert.IsDBNull(dataRow["Fax2"]) ? null : (System.String)dataRow["Fax2"];
			entity.Address3 = Convert.IsDBNull(dataRow["Address3"]) ? null : (System.String)dataRow["Address3"];
			entity.Telephone3 = Convert.IsDBNull(dataRow["Telephone3"]) ? null : (System.String)dataRow["Telephone3"];
			entity.Fax3 = Convert.IsDBNull(dataRow["Fax3"]) ? null : (System.String)dataRow["Fax3"];
			entity.Email = Convert.IsDBNull(dataRow["Email"]) ? null : (System.String)dataRow["Email"];
			entity.BranchCode = Convert.IsDBNull(dataRow["BranchCode"]) ? null : (System.String)dataRow["BranchCode"];
			entity.BranchName = Convert.IsDBNull(dataRow["BranchName"]) ? null : (System.String)dataRow["BranchName"];
			entity.Custodian = Convert.IsDBNull(dataRow["Custodian"]) ? null : (System.Boolean?)dataRow["Custodian"];
			entity.CustomerType = Convert.IsDBNull(dataRow["CustomerType"]) ? null : (System.String)dataRow["CustomerType"];
			entity.TradeAtCompany = Convert.IsDBNull(dataRow["TradeAtCompany"]) ? null : (System.Boolean?)dataRow["TradeAtCompany"];
			entity.TradeByTelephone = Convert.IsDBNull(dataRow["TradeByTelephone"]) ? null : (System.Boolean?)dataRow["TradeByTelephone"];
			entity.TradeOnline = Convert.IsDBNull(dataRow["TradeOnline"]) ? null : (System.Boolean?)dataRow["TradeOnline"];
			entity.ExistedAccount = Convert.IsDBNull(dataRow["ExistedAccount"]) ? null : (System.Boolean?)dataRow["ExistedAccount"];
			entity.AcceptChanges();
		}
		#endregion 
		
		#region DeepLoad Methods
		/// <summary>
		/// Deep Loads the <see cref="IEntity"/> object with criteria based of the child 
		/// property collections only N Levels Deep based on the <see cref="DeepLoadType"/>.
		/// </summary>
		/// <remarks>
		/// Use this method with caution as it is possible to DeepLoad with Recursion and traverse an entire object graph.
		/// </remarks>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="entity">The <see cref="AccountManager.Entities.OpenCustAccount"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AccountManager.Entities.OpenCustAccount Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AccountManager.Entities.OpenCustAccount entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			
			//Fire all DeepLoad Items
			foreach(KeyValuePair<Delegate, object> pair in deepHandles.Values)
		    {
                pair.Key.DynamicInvoke((object[])pair.Value);
		    }
			deepHandles = null;
		}
		
		#endregion 
		
		#region DeepSave Methods

		/// <summary>
		/// Deep Save the entire object graph of the AccountManager.Entities.OpenCustAccount object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AccountManager.Entities.OpenCustAccount instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AccountManager.Entities.OpenCustAccount Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AccountManager.Entities.OpenCustAccount entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			#endregion Composite Parent Properties

			// Save Root Entity through Provider
			if (!entity.IsDeleted)
				this.Save(transactionManager, entity);
			
			//used to hold DeepSave method delegates and fire after all the local children have been saved.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			//Fire all DeepSave Items
			foreach(KeyValuePair<Delegate, object> pair in deepHandles.Values)
		    {
                pair.Key.DynamicInvoke((object[])pair.Value);
		    }
			
			// Save Root Entity through Provider, if not already saved in delete mode
			if (entity.IsDeleted)
				this.Save(transactionManager, entity);
				

			deepHandles = null;
						
			return true;
		}
		#endregion
	} // end class
	
	#region OpenCustAccountChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>AccountManager.Entities.OpenCustAccount</c>
	///</summary>
	public enum OpenCustAccountChildEntityTypes
	{
	}
	
	#endregion OpenCustAccountChildEntityTypes
	
	#region OpenCustAccountFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;OpenCustAccountColumn&gt;"/> class
	/// that is used exclusively with a <see cref="OpenCustAccount"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class OpenCustAccountFilterBuilder : SqlFilterBuilder<OpenCustAccountColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the OpenCustAccountFilterBuilder class.
		/// </summary>
		public OpenCustAccountFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the OpenCustAccountFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public OpenCustAccountFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the OpenCustAccountFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public OpenCustAccountFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion OpenCustAccountFilterBuilder
	
	#region OpenCustAccountParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;OpenCustAccountColumn&gt;"/> class
	/// that is used exclusively with a <see cref="OpenCustAccount"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class OpenCustAccountParameterBuilder : ParameterizedSqlFilterBuilder<OpenCustAccountColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the OpenCustAccountParameterBuilder class.
		/// </summary>
		public OpenCustAccountParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the OpenCustAccountParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public OpenCustAccountParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the OpenCustAccountParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public OpenCustAccountParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion OpenCustAccountParameterBuilder
	
	#region OpenCustAccountSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;OpenCustAccountColumn&gt;"/> class
	/// that is used exclusively with a <see cref="OpenCustAccount"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class OpenCustAccountSortBuilder : SqlSortBuilder<OpenCustAccountColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the OpenCustAccountSqlSortBuilder class.
		/// </summary>
		public OpenCustAccountSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion OpenCustAccountSortBuilder
	
} // end namespace
