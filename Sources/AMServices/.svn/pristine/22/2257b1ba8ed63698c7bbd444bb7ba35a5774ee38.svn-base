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
	/// This class is the base class for any <see cref="CustomerActionHistoryProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class CustomerActionHistoryProviderBaseCore : EntityProviderBase<AccountManager.Entities.CustomerActionHistory, AccountManager.Entities.CustomerActionHistoryKey>
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
		public override bool Delete(TransactionManager transactionManager, AccountManager.Entities.CustomerActionHistoryKey key)
		{
			return Delete(transactionManager, key.Id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_id">Auto increase ID. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Int64 _id)
		{
			return Delete(null, _id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">Auto increase ID. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Int64 _id);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_CustomerActionHistory_MainCustAccount key.
		///		FK_CustomerActionHistory_MainCustAccount Description: 
		/// </summary>
		/// <param name="_mainCustAccountId">Main customer account ID</param>
		/// <returns>Returns a typed collection of AccountManager.Entities.CustomerActionHistory objects.</returns>
		public TList<CustomerActionHistory> GetByMainCustAccountId(System.String _mainCustAccountId)
		{
			int count = -1;
			return GetByMainCustAccountId(_mainCustAccountId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_CustomerActionHistory_MainCustAccount key.
		///		FK_CustomerActionHistory_MainCustAccount Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_mainCustAccountId">Main customer account ID</param>
		/// <returns>Returns a typed collection of AccountManager.Entities.CustomerActionHistory objects.</returns>
		/// <remarks></remarks>
		public TList<CustomerActionHistory> GetByMainCustAccountId(TransactionManager transactionManager, System.String _mainCustAccountId)
		{
			int count = -1;
			return GetByMainCustAccountId(transactionManager, _mainCustAccountId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_CustomerActionHistory_MainCustAccount key.
		///		FK_CustomerActionHistory_MainCustAccount Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_mainCustAccountId">Main customer account ID</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AccountManager.Entities.CustomerActionHistory objects.</returns>
		public TList<CustomerActionHistory> GetByMainCustAccountId(TransactionManager transactionManager, System.String _mainCustAccountId, int start, int pageLength)
		{
			int count = -1;
			return GetByMainCustAccountId(transactionManager, _mainCustAccountId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_CustomerActionHistory_MainCustAccount key.
		///		fkCustomerActionHistoryMainCustAccount Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_mainCustAccountId">Main customer account ID</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AccountManager.Entities.CustomerActionHistory objects.</returns>
		public TList<CustomerActionHistory> GetByMainCustAccountId(System.String _mainCustAccountId, int start, int pageLength)
		{
			int count =  -1;
			return GetByMainCustAccountId(null, _mainCustAccountId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_CustomerActionHistory_MainCustAccount key.
		///		fkCustomerActionHistoryMainCustAccount Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_mainCustAccountId">Main customer account ID</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AccountManager.Entities.CustomerActionHistory objects.</returns>
		public TList<CustomerActionHistory> GetByMainCustAccountId(System.String _mainCustAccountId, int start, int pageLength,out int count)
		{
			return GetByMainCustAccountId(null, _mainCustAccountId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_CustomerActionHistory_MainCustAccount key.
		///		FK_CustomerActionHistory_MainCustAccount Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_mainCustAccountId">Main customer account ID</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of AccountManager.Entities.CustomerActionHistory objects.</returns>
		public abstract TList<CustomerActionHistory> GetByMainCustAccountId(TransactionManager transactionManager, System.String _mainCustAccountId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_CustomerActionHistory_SubCustAccount key.
		///		FK_CustomerActionHistory_SubCustAccount Description: 
		/// </summary>
		/// <param name="_subCustAccountId">Sub customer account ID</param>
		/// <returns>Returns a typed collection of AccountManager.Entities.CustomerActionHistory objects.</returns>
		public TList<CustomerActionHistory> GetBySubCustAccountId(System.String _subCustAccountId)
		{
			int count = -1;
			return GetBySubCustAccountId(_subCustAccountId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_CustomerActionHistory_SubCustAccount key.
		///		FK_CustomerActionHistory_SubCustAccount Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_subCustAccountId">Sub customer account ID</param>
		/// <returns>Returns a typed collection of AccountManager.Entities.CustomerActionHistory objects.</returns>
		/// <remarks></remarks>
		public TList<CustomerActionHistory> GetBySubCustAccountId(TransactionManager transactionManager, System.String _subCustAccountId)
		{
			int count = -1;
			return GetBySubCustAccountId(transactionManager, _subCustAccountId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_CustomerActionHistory_SubCustAccount key.
		///		FK_CustomerActionHistory_SubCustAccount Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_subCustAccountId">Sub customer account ID</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AccountManager.Entities.CustomerActionHistory objects.</returns>
		public TList<CustomerActionHistory> GetBySubCustAccountId(TransactionManager transactionManager, System.String _subCustAccountId, int start, int pageLength)
		{
			int count = -1;
			return GetBySubCustAccountId(transactionManager, _subCustAccountId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_CustomerActionHistory_SubCustAccount key.
		///		fkCustomerActionHistorySubCustAccount Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_subCustAccountId">Sub customer account ID</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AccountManager.Entities.CustomerActionHistory objects.</returns>
		public TList<CustomerActionHistory> GetBySubCustAccountId(System.String _subCustAccountId, int start, int pageLength)
		{
			int count =  -1;
			return GetBySubCustAccountId(null, _subCustAccountId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_CustomerActionHistory_SubCustAccount key.
		///		fkCustomerActionHistorySubCustAccount Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_subCustAccountId">Sub customer account ID</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AccountManager.Entities.CustomerActionHistory objects.</returns>
		public TList<CustomerActionHistory> GetBySubCustAccountId(System.String _subCustAccountId, int start, int pageLength,out int count)
		{
			return GetBySubCustAccountId(null, _subCustAccountId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_CustomerActionHistory_SubCustAccount key.
		///		FK_CustomerActionHistory_SubCustAccount Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_subCustAccountId">Sub customer account ID</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of AccountManager.Entities.CustomerActionHistory objects.</returns>
		public abstract TList<CustomerActionHistory> GetBySubCustAccountId(TransactionManager transactionManager, System.String _subCustAccountId, int start, int pageLength, out int count);
		
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
		public override AccountManager.Entities.CustomerActionHistory Get(TransactionManager transactionManager, AccountManager.Entities.CustomerActionHistoryKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_CustomerActionHistory index.
		/// </summary>
		/// <param name="_id">Auto increase ID</param>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.CustomerActionHistory"/> class.</returns>
		public AccountManager.Entities.CustomerActionHistory GetById(System.Int64 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CustomerActionHistory index.
		/// </summary>
		/// <param name="_id">Auto increase ID</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.CustomerActionHistory"/> class.</returns>
		public AccountManager.Entities.CustomerActionHistory GetById(System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CustomerActionHistory index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">Auto increase ID</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.CustomerActionHistory"/> class.</returns>
		public AccountManager.Entities.CustomerActionHistory GetById(TransactionManager transactionManager, System.Int64 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CustomerActionHistory index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">Auto increase ID</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.CustomerActionHistory"/> class.</returns>
		public AccountManager.Entities.CustomerActionHistory GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CustomerActionHistory index.
		/// </summary>
		/// <param name="_id">Auto increase ID</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.CustomerActionHistory"/> class.</returns>
		public AccountManager.Entities.CustomerActionHistory GetById(System.Int64 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CustomerActionHistory index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">Auto increase ID</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.CustomerActionHistory"/> class.</returns>
		public abstract AccountManager.Entities.CustomerActionHistory GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;CustomerActionHistory&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;CustomerActionHistory&gt;"/></returns>
		public static TList<CustomerActionHistory> Fill(IDataReader reader, TList<CustomerActionHistory> rows, int start, int pageLength)
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
				
				AccountManager.Entities.CustomerActionHistory c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("CustomerActionHistory")
					.Append("|").Append((System.Int64)reader[((int)CustomerActionHistoryColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<CustomerActionHistory>(
					key.ToString(), // EntityTrackingKey
					"CustomerActionHistory",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new AccountManager.Entities.CustomerActionHistory();
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
					c.Id = (System.Int64)reader[((int)CustomerActionHistoryColumn.Id - 1)];
					c.BrokerId = (reader.IsDBNull(((int)CustomerActionHistoryColumn.BrokerId - 1)))?null:(System.String)reader[((int)CustomerActionHistoryColumn.BrokerId - 1)];
					c.ActionTime = (System.DateTime)reader[((int)CustomerActionHistoryColumn.ActionTime - 1)];
					c.MainCustAccountId = (System.String)reader[((int)CustomerActionHistoryColumn.MainCustAccountId - 1)];
					c.SubCustAccountId = (reader.IsDBNull(((int)CustomerActionHistoryColumn.SubCustAccountId - 1)))?null:(System.String)reader[((int)CustomerActionHistoryColumn.SubCustAccountId - 1)];
					c.ActionType = (System.Int32)reader[((int)CustomerActionHistoryColumn.ActionType - 1)];
					c.Reason = (reader.IsDBNull(((int)CustomerActionHistoryColumn.Reason - 1)))?null:(System.Int32?)reader[((int)CustomerActionHistoryColumn.Reason - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="AccountManager.Entities.CustomerActionHistory"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AccountManager.Entities.CustomerActionHistory"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AccountManager.Entities.CustomerActionHistory entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int64)reader[((int)CustomerActionHistoryColumn.Id - 1)];
			entity.BrokerId = (reader.IsDBNull(((int)CustomerActionHistoryColumn.BrokerId - 1)))?null:(System.String)reader[((int)CustomerActionHistoryColumn.BrokerId - 1)];
			entity.ActionTime = (System.DateTime)reader[((int)CustomerActionHistoryColumn.ActionTime - 1)];
			entity.MainCustAccountId = (System.String)reader[((int)CustomerActionHistoryColumn.MainCustAccountId - 1)];
			entity.SubCustAccountId = (reader.IsDBNull(((int)CustomerActionHistoryColumn.SubCustAccountId - 1)))?null:(System.String)reader[((int)CustomerActionHistoryColumn.SubCustAccountId - 1)];
			entity.ActionType = (System.Int32)reader[((int)CustomerActionHistoryColumn.ActionType - 1)];
			entity.Reason = (reader.IsDBNull(((int)CustomerActionHistoryColumn.Reason - 1)))?null:(System.Int32?)reader[((int)CustomerActionHistoryColumn.Reason - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="AccountManager.Entities.CustomerActionHistory"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AccountManager.Entities.CustomerActionHistory"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AccountManager.Entities.CustomerActionHistory entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int64)dataRow["ID"];
			entity.BrokerId = Convert.IsDBNull(dataRow["BrokerID"]) ? null : (System.String)dataRow["BrokerID"];
			entity.ActionTime = (System.DateTime)dataRow["ActionTime"];
			entity.MainCustAccountId = (System.String)dataRow["MainCustAccountID"];
			entity.SubCustAccountId = Convert.IsDBNull(dataRow["SubCustAccountID"]) ? null : (System.String)dataRow["SubCustAccountID"];
			entity.ActionType = (System.Int32)dataRow["ActionType"];
			entity.Reason = Convert.IsDBNull(dataRow["Reason"]) ? null : (System.Int32?)dataRow["Reason"];
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
		/// <param name="entity">The <see cref="AccountManager.Entities.CustomerActionHistory"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AccountManager.Entities.CustomerActionHistory Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AccountManager.Entities.CustomerActionHistory entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region MainCustAccountIdSource	
			if (CanDeepLoad(entity, "MainCustAccount|MainCustAccountIdSource", deepLoadType, innerList) 
				&& entity.MainCustAccountIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.MainCustAccountId;
				MainCustAccount tmpEntity = EntityManager.LocateEntity<MainCustAccount>(EntityLocator.ConstructKeyFromPkItems(typeof(MainCustAccount), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.MainCustAccountIdSource = tmpEntity;
				else
					entity.MainCustAccountIdSource = DataRepository.MainCustAccountProvider.GetByMainCustAccountId(transactionManager, entity.MainCustAccountId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'MainCustAccountIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.MainCustAccountIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.MainCustAccountProvider.DeepLoad(transactionManager, entity.MainCustAccountIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion MainCustAccountIdSource

			#region SubCustAccountIdSource	
			if (CanDeepLoad(entity, "SubCustAccount|SubCustAccountIdSource", deepLoadType, innerList) 
				&& entity.SubCustAccountIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.SubCustAccountId ?? string.Empty);
				SubCustAccount tmpEntity = EntityManager.LocateEntity<SubCustAccount>(EntityLocator.ConstructKeyFromPkItems(typeof(SubCustAccount), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.SubCustAccountIdSource = tmpEntity;
				else
					entity.SubCustAccountIdSource = DataRepository.SubCustAccountProvider.GetBySubCustAccountId(transactionManager, (entity.SubCustAccountId ?? string.Empty));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'SubCustAccountIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.SubCustAccountIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.SubCustAccountProvider.DeepLoad(transactionManager, entity.SubCustAccountIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion SubCustAccountIdSource
			
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
		/// Deep Save the entire object graph of the AccountManager.Entities.CustomerActionHistory object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AccountManager.Entities.CustomerActionHistory instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AccountManager.Entities.CustomerActionHistory Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AccountManager.Entities.CustomerActionHistory entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region MainCustAccountIdSource
			if (CanDeepSave(entity, "MainCustAccount|MainCustAccountIdSource", deepSaveType, innerList) 
				&& entity.MainCustAccountIdSource != null)
			{
				DataRepository.MainCustAccountProvider.Save(transactionManager, entity.MainCustAccountIdSource);
				entity.MainCustAccountId = entity.MainCustAccountIdSource.MainCustAccountId;
			}
			#endregion 
			
			#region SubCustAccountIdSource
			if (CanDeepSave(entity, "SubCustAccount|SubCustAccountIdSource", deepSaveType, innerList) 
				&& entity.SubCustAccountIdSource != null)
			{
				DataRepository.SubCustAccountProvider.Save(transactionManager, entity.SubCustAccountIdSource);
				entity.SubCustAccountId = entity.SubCustAccountIdSource.SubCustAccountId;
			}
			#endregion 
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
	
	#region CustomerActionHistoryChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>AccountManager.Entities.CustomerActionHistory</c>
	///</summary>
	public enum CustomerActionHistoryChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>MainCustAccount</c> at MainCustAccountIdSource
		///</summary>
		[ChildEntityType(typeof(MainCustAccount))]
		MainCustAccount,
			
		///<summary>
		/// Composite Property for <c>SubCustAccount</c> at SubCustAccountIdSource
		///</summary>
		[ChildEntityType(typeof(SubCustAccount))]
		SubCustAccount,
		}
	
	#endregion CustomerActionHistoryChildEntityTypes
	
	#region CustomerActionHistoryFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;CustomerActionHistoryColumn&gt;"/> class
	/// that is used exclusively with a <see cref="CustomerActionHistory"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CustomerActionHistoryFilterBuilder : SqlFilterBuilder<CustomerActionHistoryColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CustomerActionHistoryFilterBuilder class.
		/// </summary>
		public CustomerActionHistoryFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the CustomerActionHistoryFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CustomerActionHistoryFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CustomerActionHistoryFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CustomerActionHistoryFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CustomerActionHistoryFilterBuilder
	
	#region CustomerActionHistoryParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;CustomerActionHistoryColumn&gt;"/> class
	/// that is used exclusively with a <see cref="CustomerActionHistory"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CustomerActionHistoryParameterBuilder : ParameterizedSqlFilterBuilder<CustomerActionHistoryColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CustomerActionHistoryParameterBuilder class.
		/// </summary>
		public CustomerActionHistoryParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the CustomerActionHistoryParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CustomerActionHistoryParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CustomerActionHistoryParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CustomerActionHistoryParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CustomerActionHistoryParameterBuilder
	
	#region CustomerActionHistorySortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;CustomerActionHistoryColumn&gt;"/> class
	/// that is used exclusively with a <see cref="CustomerActionHistory"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class CustomerActionHistorySortBuilder : SqlSortBuilder<CustomerActionHistoryColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CustomerActionHistorySqlSortBuilder class.
		/// </summary>
		public CustomerActionHistorySortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion CustomerActionHistorySortBuilder
	
} // end namespace
