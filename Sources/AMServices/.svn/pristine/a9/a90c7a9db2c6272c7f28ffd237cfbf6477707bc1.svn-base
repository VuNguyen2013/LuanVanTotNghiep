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
	/// This class is the base class for any <see cref="BrokerAccountProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class BrokerAccountProviderBaseCore : EntityProviderBase<AccountManager.Entities.BrokerAccount, AccountManager.Entities.BrokerAccountKey>
	{		
		#region Get from Many To Many Relationship Functions
		#region GetByPermissionIdFromBrokerPermission
		
		/// <summary>
		///		Gets BrokerAccount objects from the datasource by PermissionID in the
		///		BrokerPermission table. Table BrokerAccount is related to table BrokerAMPermission
		///		through the (M:N) relationship defined in the BrokerPermission table.
		/// </summary>
		/// <param name="_permissionId"></param>
		/// <returns>Returns a typed collection of BrokerAccount objects.</returns>
		public TList<BrokerAccount> GetByPermissionIdFromBrokerPermission(System.Int32 _permissionId)
		{
			int count = -1;
			return GetByPermissionIdFromBrokerPermission(null,_permissionId, 0, int.MaxValue, out count);
			
		}
		
		/// <summary>
		///		Gets AccountManager.Entities.BrokerAccount objects from the datasource by PermissionID in the
		///		BrokerPermission table. Table BrokerAccount is related to table BrokerAMPermission
		///		through the (M:N) relationship defined in the BrokerPermission table.
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_permissionId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a TList of BrokerAccount objects.</returns>
		public TList<BrokerAccount> GetByPermissionIdFromBrokerPermission(System.Int32 _permissionId, int start, int pageLength)
		{
			int count = -1;
			return GetByPermissionIdFromBrokerPermission(null, _permissionId, start, pageLength, out count);
		}
		
		/// <summary>
		///		Gets BrokerAccount objects from the datasource by PermissionID in the
		///		BrokerPermission table. Table BrokerAccount is related to table BrokerAMPermission
		///		through the (M:N) relationship defined in the BrokerPermission table.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_permissionId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of BrokerAccount objects.</returns>
		public TList<BrokerAccount> GetByPermissionIdFromBrokerPermission(TransactionManager transactionManager, System.Int32 _permissionId)
		{
			int count = -1;
			return GetByPermissionIdFromBrokerPermission(transactionManager, _permissionId, 0, int.MaxValue, out count);
		}
		
		
		/// <summary>
		///		Gets BrokerAccount objects from the datasource by PermissionID in the
		///		BrokerPermission table. Table BrokerAccount is related to table BrokerAMPermission
		///		through the (M:N) relationship defined in the BrokerPermission table.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_permissionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of BrokerAccount objects.</returns>
		public TList<BrokerAccount> GetByPermissionIdFromBrokerPermission(TransactionManager transactionManager, System.Int32 _permissionId,int start, int pageLength)
		{
			int count = -1;
			return GetByPermissionIdFromBrokerPermission(transactionManager, _permissionId, start, pageLength, out count);
		}
		
		/// <summary>
		///		Gets BrokerAccount objects from the datasource by PermissionID in the
		///		BrokerPermission table. Table BrokerAccount is related to table BrokerAMPermission
		///		through the (M:N) relationship defined in the BrokerPermission table.
		/// </summary>
		/// <param name="_permissionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of BrokerAccount objects.</returns>
		public TList<BrokerAccount> GetByPermissionIdFromBrokerPermission(System.Int32 _permissionId,int start, int pageLength, out int count)
		{
			
			return GetByPermissionIdFromBrokerPermission(null, _permissionId, start, pageLength, out count);
		}


		/// <summary>
		///		Gets BrokerAccount objects from the datasource by PermissionID in the
		///		BrokerPermission table. Table BrokerAccount is related to table BrokerAMPermission
		///		through the (M:N) relationship defined in the BrokerPermission table.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <param name="_permissionId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a TList of BrokerAccount objects.</returns>
		public abstract TList<BrokerAccount> GetByPermissionIdFromBrokerPermission(TransactionManager transactionManager,System.Int32 _permissionId, int start, int pageLength, out int count);
		
		#endregion GetByPermissionIdFromBrokerPermission
		
		#endregion	
		
		#region Delete Methods

		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager">A <see cref="TransactionManager"/> object.</param>
		/// <param name="key">The unique identifier of the row to delete.</param>
		/// <returns>Returns true if operation suceeded.</returns>
		public override bool Delete(TransactionManager transactionManager, AccountManager.Entities.BrokerAccountKey key)
		{
			return Delete(transactionManager, key.BrokerId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_brokerId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.String _brokerId)
		{
			return Delete(null, _brokerId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_brokerId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.String _brokerId);		
		
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
		public override AccountManager.Entities.BrokerAccount Get(TransactionManager transactionManager, AccountManager.Entities.BrokerAccountKey key, int start, int pageLength)
		{
			return GetByBrokerId(transactionManager, key.BrokerId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_BrokerAccount index.
		/// </summary>
		/// <param name="_brokerId"></param>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.BrokerAccount"/> class.</returns>
		public AccountManager.Entities.BrokerAccount GetByBrokerId(System.String _brokerId)
		{
			int count = -1;
			return GetByBrokerId(null,_brokerId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_BrokerAccount index.
		/// </summary>
		/// <param name="_brokerId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.BrokerAccount"/> class.</returns>
		public AccountManager.Entities.BrokerAccount GetByBrokerId(System.String _brokerId, int start, int pageLength)
		{
			int count = -1;
			return GetByBrokerId(null, _brokerId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_BrokerAccount index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_brokerId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.BrokerAccount"/> class.</returns>
		public AccountManager.Entities.BrokerAccount GetByBrokerId(TransactionManager transactionManager, System.String _brokerId)
		{
			int count = -1;
			return GetByBrokerId(transactionManager, _brokerId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_BrokerAccount index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_brokerId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.BrokerAccount"/> class.</returns>
		public AccountManager.Entities.BrokerAccount GetByBrokerId(TransactionManager transactionManager, System.String _brokerId, int start, int pageLength)
		{
			int count = -1;
			return GetByBrokerId(transactionManager, _brokerId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_BrokerAccount index.
		/// </summary>
		/// <param name="_brokerId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.BrokerAccount"/> class.</returns>
		public AccountManager.Entities.BrokerAccount GetByBrokerId(System.String _brokerId, int start, int pageLength, out int count)
		{
			return GetByBrokerId(null, _brokerId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_BrokerAccount index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_brokerId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.BrokerAccount"/> class.</returns>
		public abstract AccountManager.Entities.BrokerAccount GetByBrokerId(TransactionManager transactionManager, System.String _brokerId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;BrokerAccount&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;BrokerAccount&gt;"/></returns>
		public static TList<BrokerAccount> Fill(IDataReader reader, TList<BrokerAccount> rows, int start, int pageLength)
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
				
				AccountManager.Entities.BrokerAccount c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("BrokerAccount")
					.Append("|").Append((System.String)reader[((int)BrokerAccountColumn.BrokerId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<BrokerAccount>(
					key.ToString(), // EntityTrackingKey
					"BrokerAccount",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new AccountManager.Entities.BrokerAccount();
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
					c.BrokerId = (System.String)reader[((int)BrokerAccountColumn.BrokerId - 1)];
					c.OriginalBrokerId = c.BrokerId;
					c.Name = (System.String)reader[((int)BrokerAccountColumn.Name - 1)];
					c.Password = (System.String)reader[((int)BrokerAccountColumn.Password - 1)];
					c.AccountType = (System.Int16)reader[((int)BrokerAccountColumn.AccountType - 1)];
					c.Actived = (System.Boolean)reader[((int)BrokerAccountColumn.Actived - 1)];
					c.MobilePhone = (reader.IsDBNull(((int)BrokerAccountColumn.MobilePhone - 1)))?null:(System.String)reader[((int)BrokerAccountColumn.MobilePhone - 1)];
					c.EmailAddr = (reader.IsDBNull(((int)BrokerAccountColumn.EmailAddr - 1)))?null:(System.String)reader[((int)BrokerAccountColumn.EmailAddr - 1)];
					c.CreatedDate = (System.DateTime)reader[((int)BrokerAccountColumn.CreatedDate - 1)];
					c.CreatedUser = (System.String)reader[((int)BrokerAccountColumn.CreatedUser - 1)];
					c.UpdatedDate = (reader.IsDBNull(((int)BrokerAccountColumn.UpdatedDate - 1)))?null:(System.DateTime?)reader[((int)BrokerAccountColumn.UpdatedDate - 1)];
					c.UpdatedUser = (reader.IsDBNull(((int)BrokerAccountColumn.UpdatedUser - 1)))?null:(System.String)reader[((int)BrokerAccountColumn.UpdatedUser - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="AccountManager.Entities.BrokerAccount"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AccountManager.Entities.BrokerAccount"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AccountManager.Entities.BrokerAccount entity)
		{
			if (!reader.Read()) return;
			
			entity.BrokerId = (System.String)reader[((int)BrokerAccountColumn.BrokerId - 1)];
			entity.OriginalBrokerId = (System.String)reader["BrokerID"];
			entity.Name = (System.String)reader[((int)BrokerAccountColumn.Name - 1)];
			entity.Password = (System.String)reader[((int)BrokerAccountColumn.Password - 1)];
			entity.AccountType = (System.Int16)reader[((int)BrokerAccountColumn.AccountType - 1)];
			entity.Actived = (System.Boolean)reader[((int)BrokerAccountColumn.Actived - 1)];
			entity.MobilePhone = (reader.IsDBNull(((int)BrokerAccountColumn.MobilePhone - 1)))?null:(System.String)reader[((int)BrokerAccountColumn.MobilePhone - 1)];
			entity.EmailAddr = (reader.IsDBNull(((int)BrokerAccountColumn.EmailAddr - 1)))?null:(System.String)reader[((int)BrokerAccountColumn.EmailAddr - 1)];
			entity.CreatedDate = (System.DateTime)reader[((int)BrokerAccountColumn.CreatedDate - 1)];
			entity.CreatedUser = (System.String)reader[((int)BrokerAccountColumn.CreatedUser - 1)];
			entity.UpdatedDate = (reader.IsDBNull(((int)BrokerAccountColumn.UpdatedDate - 1)))?null:(System.DateTime?)reader[((int)BrokerAccountColumn.UpdatedDate - 1)];
			entity.UpdatedUser = (reader.IsDBNull(((int)BrokerAccountColumn.UpdatedUser - 1)))?null:(System.String)reader[((int)BrokerAccountColumn.UpdatedUser - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="AccountManager.Entities.BrokerAccount"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AccountManager.Entities.BrokerAccount"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AccountManager.Entities.BrokerAccount entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.BrokerId = (System.String)dataRow["BrokerID"];
			entity.OriginalBrokerId = (System.String)dataRow["BrokerID"];
			entity.Name = (System.String)dataRow["Name"];
			entity.Password = (System.String)dataRow["Password"];
			entity.AccountType = (System.Int16)dataRow["AccountType"];
			entity.Actived = (System.Boolean)dataRow["Actived"];
			entity.MobilePhone = Convert.IsDBNull(dataRow["MobilePhone"]) ? null : (System.String)dataRow["MobilePhone"];
			entity.EmailAddr = Convert.IsDBNull(dataRow["EmailAddr"]) ? null : (System.String)dataRow["EmailAddr"];
			entity.CreatedDate = (System.DateTime)dataRow["CreatedDate"];
			entity.CreatedUser = (System.String)dataRow["CreatedUser"];
			entity.UpdatedDate = Convert.IsDBNull(dataRow["UpdatedDate"]) ? null : (System.DateTime?)dataRow["UpdatedDate"];
			entity.UpdatedUser = Convert.IsDBNull(dataRow["UpdatedUser"]) ? null : (System.String)dataRow["UpdatedUser"];
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
		/// <param name="entity">The <see cref="AccountManager.Entities.BrokerAccount"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AccountManager.Entities.BrokerAccount Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AccountManager.Entities.BrokerAccount entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByBrokerId methods when available
			
			#region PermissionIdBrokerAmPermissionCollection_From_BrokerPermission
			// RelationshipType.ManyToMany
			if (CanDeepLoad(entity, "List<BrokerAmPermission>|PermissionIdBrokerAmPermissionCollection_From_BrokerPermission", deepLoadType, innerList))
			{
				entity.PermissionIdBrokerAmPermissionCollection_From_BrokerPermission = DataRepository.BrokerAmPermissionProvider.GetByBrokerIdFromBrokerPermission(transactionManager, entity.BrokerId);			 
		
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PermissionIdBrokerAmPermissionCollection_From_BrokerPermission' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.PermissionIdBrokerAmPermissionCollection_From_BrokerPermission != null)
				{
					deepHandles.Add("PermissionIdBrokerAmPermissionCollection_From_BrokerPermission",
						new KeyValuePair<Delegate, object>((DeepLoadHandle< BrokerAmPermission >) DataRepository.BrokerAmPermissionProvider.DeepLoad,
						new object[] { transactionManager, entity.PermissionIdBrokerAmPermissionCollection_From_BrokerPermission, deep, deepLoadType, childTypes, innerList }
					));
				}
			}
			#endregion
			
			
			
			#region BrokerPermissionCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<BrokerPermission>|BrokerPermissionCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'BrokerPermissionCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.BrokerPermissionCollection = DataRepository.BrokerPermissionProvider.GetByBrokerId(transactionManager, entity.BrokerId);

				if (deep && entity.BrokerPermissionCollection.Count > 0)
				{
					deepHandles.Add("BrokerPermissionCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<BrokerPermission>) DataRepository.BrokerPermissionProvider.DeepLoad,
						new object[] { transactionManager, entity.BrokerPermissionCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region MainCustAccountCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<MainCustAccount>|MainCustAccountCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'MainCustAccountCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.MainCustAccountCollection = DataRepository.MainCustAccountProvider.GetByBrokerId(transactionManager, entity.BrokerId);

				if (deep && entity.MainCustAccountCollection.Count > 0)
				{
					deepHandles.Add("MainCustAccountCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<MainCustAccount>) DataRepository.MainCustAccountProvider.DeepLoad,
						new object[] { transactionManager, entity.MainCustAccountCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
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
		/// Deep Save the entire object graph of the AccountManager.Entities.BrokerAccount object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AccountManager.Entities.BrokerAccount instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AccountManager.Entities.BrokerAccount Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AccountManager.Entities.BrokerAccount entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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

			#region PermissionIdBrokerAmPermissionCollection_From_BrokerPermission>
			if (CanDeepSave(entity.PermissionIdBrokerAmPermissionCollection_From_BrokerPermission, "List<BrokerAmPermission>|PermissionIdBrokerAmPermissionCollection_From_BrokerPermission", deepSaveType, innerList))
			{
				if (entity.PermissionIdBrokerAmPermissionCollection_From_BrokerPermission.Count > 0 || entity.PermissionIdBrokerAmPermissionCollection_From_BrokerPermission.DeletedItems.Count > 0)
				{
					DataRepository.BrokerAmPermissionProvider.Save(transactionManager, entity.PermissionIdBrokerAmPermissionCollection_From_BrokerPermission); 
					deepHandles.Add("PermissionIdBrokerAmPermissionCollection_From_BrokerPermission",
						new KeyValuePair<Delegate, object>((DeepSaveHandle<BrokerAmPermission>) DataRepository.BrokerAmPermissionProvider.DeepSave,
						new object[] { transactionManager, entity.PermissionIdBrokerAmPermissionCollection_From_BrokerPermission, deepSaveType, childTypes, innerList }
					));
				}
			}
			#endregion 
	
			#region List<BrokerPermission>
				if (CanDeepSave(entity.BrokerPermissionCollection, "List<BrokerPermission>|BrokerPermissionCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(BrokerPermission child in entity.BrokerPermissionCollection)
					{
						if(child.BrokerIdSource != null)
						{
								child.BrokerId = child.BrokerIdSource.BrokerId;
						}

						if(child.PermissionIdSource != null)
						{
								child.PermissionId = child.PermissionIdSource.PermissionId;
						}

					}

					if (entity.BrokerPermissionCollection.Count > 0 || entity.BrokerPermissionCollection.DeletedItems.Count > 0)
					{
						//DataRepository.BrokerPermissionProvider.Save(transactionManager, entity.BrokerPermissionCollection);
						
						deepHandles.Add("BrokerPermissionCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< BrokerPermission >) DataRepository.BrokerPermissionProvider.DeepSave,
							new object[] { transactionManager, entity.BrokerPermissionCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
	
			#region List<MainCustAccount>
				if (CanDeepSave(entity.MainCustAccountCollection, "List<MainCustAccount>|MainCustAccountCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(MainCustAccount child in entity.MainCustAccountCollection)
					{
						if(child.BrokerIdSource != null)
						{
							child.BrokerId = child.BrokerIdSource.BrokerId;
						}
						else
						{
							child.BrokerId = entity.BrokerId;
						}

					}

					if (entity.MainCustAccountCollection.Count > 0 || entity.MainCustAccountCollection.DeletedItems.Count > 0)
					{
						//DataRepository.MainCustAccountProvider.Save(transactionManager, entity.MainCustAccountCollection);
						
						deepHandles.Add("MainCustAccountCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< MainCustAccount >) DataRepository.MainCustAccountProvider.DeepSave,
							new object[] { transactionManager, entity.MainCustAccountCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
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
	
	#region BrokerAccountChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>AccountManager.Entities.BrokerAccount</c>
	///</summary>
	public enum BrokerAccountChildEntityTypes
	{

		///<summary>
		/// Collection of <c>BrokerAccount</c> as ManyToMany for BrokerAmPermissionCollection_From_BrokerPermission
		///</summary>
		[ChildEntityType(typeof(TList<BrokerAmPermission>))]
		PermissionIdBrokerAmPermissionCollection_From_BrokerPermission,

		///<summary>
		/// Collection of <c>BrokerAccount</c> as OneToMany for BrokerPermissionCollection
		///</summary>
		[ChildEntityType(typeof(TList<BrokerPermission>))]
		BrokerPermissionCollection,

		///<summary>
		/// Collection of <c>BrokerAccount</c> as OneToMany for MainCustAccountCollection
		///</summary>
		[ChildEntityType(typeof(TList<MainCustAccount>))]
		MainCustAccountCollection,
	}
	
	#endregion BrokerAccountChildEntityTypes
	
	#region BrokerAccountFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;BrokerAccountColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BrokerAccount"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BrokerAccountFilterBuilder : SqlFilterBuilder<BrokerAccountColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BrokerAccountFilterBuilder class.
		/// </summary>
		public BrokerAccountFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the BrokerAccountFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BrokerAccountFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BrokerAccountFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BrokerAccountFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BrokerAccountFilterBuilder
	
	#region BrokerAccountParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;BrokerAccountColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BrokerAccount"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BrokerAccountParameterBuilder : ParameterizedSqlFilterBuilder<BrokerAccountColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BrokerAccountParameterBuilder class.
		/// </summary>
		public BrokerAccountParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the BrokerAccountParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BrokerAccountParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BrokerAccountParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BrokerAccountParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BrokerAccountParameterBuilder
	
	#region BrokerAccountSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;BrokerAccountColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BrokerAccount"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class BrokerAccountSortBuilder : SqlSortBuilder<BrokerAccountColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BrokerAccountSqlSortBuilder class.
		/// </summary>
		public BrokerAccountSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion BrokerAccountSortBuilder
	
} // end namespace
