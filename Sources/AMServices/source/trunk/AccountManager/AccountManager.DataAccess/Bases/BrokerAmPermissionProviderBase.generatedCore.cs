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
	/// This class is the base class for any <see cref="BrokerAmPermissionProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class BrokerAmPermissionProviderBaseCore : EntityProviderBase<AccountManager.Entities.BrokerAmPermission, AccountManager.Entities.BrokerAmPermissionKey>
	{		
		#region Get from Many To Many Relationship Functions
		#region GetByBrokerIdFromBrokerPermission
		
		/// <summary>
		///		Gets BrokerAMPermission objects from the datasource by BrokerID in the
		///		BrokerPermission table. Table BrokerAMPermission is related to table BrokerAccount
		///		through the (M:N) relationship defined in the BrokerPermission table.
		/// </summary>
		/// <param name="_brokerId"></param>
		/// <returns>Returns a typed collection of BrokerAmPermission objects.</returns>
		public TList<BrokerAmPermission> GetByBrokerIdFromBrokerPermission(System.String _brokerId)
		{
			int count = -1;
			return GetByBrokerIdFromBrokerPermission(null,_brokerId, 0, int.MaxValue, out count);
			
		}
		
		/// <summary>
		///		Gets AccountManager.Entities.BrokerAmPermission objects from the datasource by BrokerID in the
		///		BrokerPermission table. Table BrokerAMPermission is related to table BrokerAccount
		///		through the (M:N) relationship defined in the BrokerPermission table.
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_brokerId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a TList of BrokerAmPermission objects.</returns>
		public TList<BrokerAmPermission> GetByBrokerIdFromBrokerPermission(System.String _brokerId, int start, int pageLength)
		{
			int count = -1;
			return GetByBrokerIdFromBrokerPermission(null, _brokerId, start, pageLength, out count);
		}
		
		/// <summary>
		///		Gets BrokerAmPermission objects from the datasource by BrokerID in the
		///		BrokerPermission table. Table BrokerAMPermission is related to table BrokerAccount
		///		through the (M:N) relationship defined in the BrokerPermission table.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_brokerId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of BrokerAMPermission objects.</returns>
		public TList<BrokerAmPermission> GetByBrokerIdFromBrokerPermission(TransactionManager transactionManager, System.String _brokerId)
		{
			int count = -1;
			return GetByBrokerIdFromBrokerPermission(transactionManager, _brokerId, 0, int.MaxValue, out count);
		}
		
		
		/// <summary>
		///		Gets BrokerAmPermission objects from the datasource by BrokerID in the
		///		BrokerPermission table. Table BrokerAMPermission is related to table BrokerAccount
		///		through the (M:N) relationship defined in the BrokerPermission table.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_brokerId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of BrokerAMPermission objects.</returns>
		public TList<BrokerAmPermission> GetByBrokerIdFromBrokerPermission(TransactionManager transactionManager, System.String _brokerId,int start, int pageLength)
		{
			int count = -1;
			return GetByBrokerIdFromBrokerPermission(transactionManager, _brokerId, start, pageLength, out count);
		}
		
		/// <summary>
		///		Gets BrokerAmPermission objects from the datasource by BrokerID in the
		///		BrokerPermission table. Table BrokerAMPermission is related to table BrokerAccount
		///		through the (M:N) relationship defined in the BrokerPermission table.
		/// </summary>
		/// <param name="_brokerId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of BrokerAMPermission objects.</returns>
		public TList<BrokerAmPermission> GetByBrokerIdFromBrokerPermission(System.String _brokerId,int start, int pageLength, out int count)
		{
			
			return GetByBrokerIdFromBrokerPermission(null, _brokerId, start, pageLength, out count);
		}


		/// <summary>
		///		Gets BrokerAMPermission objects from the datasource by BrokerID in the
		///		BrokerPermission table. Table BrokerAMPermission is related to table BrokerAccount
		///		through the (M:N) relationship defined in the BrokerPermission table.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <param name="_brokerId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a TList of BrokerAmPermission objects.</returns>
		public abstract TList<BrokerAmPermission> GetByBrokerIdFromBrokerPermission(TransactionManager transactionManager,System.String _brokerId, int start, int pageLength, out int count);
		
		#endregion GetByBrokerIdFromBrokerPermission
		
		#endregion	
		
		#region Delete Methods

		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager">A <see cref="TransactionManager"/> object.</param>
		/// <param name="key">The unique identifier of the row to delete.</param>
		/// <returns>Returns true if operation suceeded.</returns>
		public override bool Delete(TransactionManager transactionManager, AccountManager.Entities.BrokerAmPermissionKey key)
		{
			return Delete(transactionManager, key.PermissionId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_permissionId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Int32 _permissionId)
		{
			return Delete(null, _permissionId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_permissionId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Int32 _permissionId);		
		
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
		public override AccountManager.Entities.BrokerAmPermission Get(TransactionManager transactionManager, AccountManager.Entities.BrokerAmPermissionKey key, int start, int pageLength)
		{
			return GetByPermissionId(transactionManager, key.PermissionId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_BrokerAMPermission index.
		/// </summary>
		/// <param name="_permissionId"></param>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.BrokerAmPermission"/> class.</returns>
		public AccountManager.Entities.BrokerAmPermission GetByPermissionId(System.Int32 _permissionId)
		{
			int count = -1;
			return GetByPermissionId(null,_permissionId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_BrokerAMPermission index.
		/// </summary>
		/// <param name="_permissionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.BrokerAmPermission"/> class.</returns>
		public AccountManager.Entities.BrokerAmPermission GetByPermissionId(System.Int32 _permissionId, int start, int pageLength)
		{
			int count = -1;
			return GetByPermissionId(null, _permissionId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_BrokerAMPermission index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_permissionId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.BrokerAmPermission"/> class.</returns>
		public AccountManager.Entities.BrokerAmPermission GetByPermissionId(TransactionManager transactionManager, System.Int32 _permissionId)
		{
			int count = -1;
			return GetByPermissionId(transactionManager, _permissionId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_BrokerAMPermission index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_permissionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.BrokerAmPermission"/> class.</returns>
		public AccountManager.Entities.BrokerAmPermission GetByPermissionId(TransactionManager transactionManager, System.Int32 _permissionId, int start, int pageLength)
		{
			int count = -1;
			return GetByPermissionId(transactionManager, _permissionId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_BrokerAMPermission index.
		/// </summary>
		/// <param name="_permissionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.BrokerAmPermission"/> class.</returns>
		public AccountManager.Entities.BrokerAmPermission GetByPermissionId(System.Int32 _permissionId, int start, int pageLength, out int count)
		{
			return GetByPermissionId(null, _permissionId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_BrokerAMPermission index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_permissionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.BrokerAmPermission"/> class.</returns>
		public abstract AccountManager.Entities.BrokerAmPermission GetByPermissionId(TransactionManager transactionManager, System.Int32 _permissionId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;BrokerAmPermission&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;BrokerAmPermission&gt;"/></returns>
		public static TList<BrokerAmPermission> Fill(IDataReader reader, TList<BrokerAmPermission> rows, int start, int pageLength)
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
				
				AccountManager.Entities.BrokerAmPermission c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("BrokerAmPermission")
					.Append("|").Append((System.Int32)reader[((int)BrokerAmPermissionColumn.PermissionId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<BrokerAmPermission>(
					key.ToString(), // EntityTrackingKey
					"BrokerAmPermission",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new AccountManager.Entities.BrokerAmPermission();
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
					c.PermissionId = (System.Int32)reader[((int)BrokerAmPermissionColumn.PermissionId - 1)];
					c.OriginalPermissionId = c.PermissionId;
					c.PermissionName = (System.String)reader[((int)BrokerAmPermissionColumn.PermissionName - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="AccountManager.Entities.BrokerAmPermission"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AccountManager.Entities.BrokerAmPermission"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AccountManager.Entities.BrokerAmPermission entity)
		{
			if (!reader.Read()) return;
			
			entity.PermissionId = (System.Int32)reader[((int)BrokerAmPermissionColumn.PermissionId - 1)];
			entity.OriginalPermissionId = (System.Int32)reader["PermissionID"];
			entity.PermissionName = (System.String)reader[((int)BrokerAmPermissionColumn.PermissionName - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="AccountManager.Entities.BrokerAmPermission"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AccountManager.Entities.BrokerAmPermission"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AccountManager.Entities.BrokerAmPermission entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.PermissionId = (System.Int32)dataRow["PermissionID"];
			entity.OriginalPermissionId = (System.Int32)dataRow["PermissionID"];
			entity.PermissionName = (System.String)dataRow["PermissionName"];
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
		/// <param name="entity">The <see cref="AccountManager.Entities.BrokerAmPermission"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AccountManager.Entities.BrokerAmPermission Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AccountManager.Entities.BrokerAmPermission entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByPermissionId methods when available
			
			#region BrokerIdBrokerAccountCollection_From_BrokerPermission
			// RelationshipType.ManyToMany
			if (CanDeepLoad(entity, "List<BrokerAccount>|BrokerIdBrokerAccountCollection_From_BrokerPermission", deepLoadType, innerList))
			{
				entity.BrokerIdBrokerAccountCollection_From_BrokerPermission = DataRepository.BrokerAccountProvider.GetByPermissionIdFromBrokerPermission(transactionManager, entity.PermissionId);			 
		
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'BrokerIdBrokerAccountCollection_From_BrokerPermission' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.BrokerIdBrokerAccountCollection_From_BrokerPermission != null)
				{
					deepHandles.Add("BrokerIdBrokerAccountCollection_From_BrokerPermission",
						new KeyValuePair<Delegate, object>((DeepLoadHandle< BrokerAccount >) DataRepository.BrokerAccountProvider.DeepLoad,
						new object[] { transactionManager, entity.BrokerIdBrokerAccountCollection_From_BrokerPermission, deep, deepLoadType, childTypes, innerList }
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

				entity.BrokerPermissionCollection = DataRepository.BrokerPermissionProvider.GetByPermissionId(transactionManager, entity.PermissionId);

				if (deep && entity.BrokerPermissionCollection.Count > 0)
				{
					deepHandles.Add("BrokerPermissionCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<BrokerPermission>) DataRepository.BrokerPermissionProvider.DeepLoad,
						new object[] { transactionManager, entity.BrokerPermissionCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the AccountManager.Entities.BrokerAmPermission object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AccountManager.Entities.BrokerAmPermission instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AccountManager.Entities.BrokerAmPermission Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AccountManager.Entities.BrokerAmPermission entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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

			#region BrokerIdBrokerAccountCollection_From_BrokerPermission>
			if (CanDeepSave(entity.BrokerIdBrokerAccountCollection_From_BrokerPermission, "List<BrokerAccount>|BrokerIdBrokerAccountCollection_From_BrokerPermission", deepSaveType, innerList))
			{
				if (entity.BrokerIdBrokerAccountCollection_From_BrokerPermission.Count > 0 || entity.BrokerIdBrokerAccountCollection_From_BrokerPermission.DeletedItems.Count > 0)
				{
					DataRepository.BrokerAccountProvider.Save(transactionManager, entity.BrokerIdBrokerAccountCollection_From_BrokerPermission); 
					deepHandles.Add("BrokerIdBrokerAccountCollection_From_BrokerPermission",
						new KeyValuePair<Delegate, object>((DeepSaveHandle<BrokerAccount>) DataRepository.BrokerAccountProvider.DeepSave,
						new object[] { transactionManager, entity.BrokerIdBrokerAccountCollection_From_BrokerPermission, deepSaveType, childTypes, innerList }
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
						if(child.PermissionIdSource != null)
						{
								child.PermissionId = child.PermissionIdSource.PermissionId;
						}

						if(child.BrokerIdSource != null)
						{
								child.BrokerId = child.BrokerIdSource.BrokerId;
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
	
	#region BrokerAmPermissionChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>AccountManager.Entities.BrokerAmPermission</c>
	///</summary>
	public enum BrokerAmPermissionChildEntityTypes
	{

		///<summary>
		/// Collection of <c>BrokerAmPermission</c> as ManyToMany for BrokerAccountCollection_From_BrokerPermission
		///</summary>
		[ChildEntityType(typeof(TList<BrokerAccount>))]
		BrokerIdBrokerAccountCollection_From_BrokerPermission,

		///<summary>
		/// Collection of <c>BrokerAmPermission</c> as OneToMany for BrokerPermissionCollection
		///</summary>
		[ChildEntityType(typeof(TList<BrokerPermission>))]
		BrokerPermissionCollection,
	}
	
	#endregion BrokerAmPermissionChildEntityTypes
	
	#region BrokerAmPermissionFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;BrokerAmPermissionColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BrokerAmPermission"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BrokerAmPermissionFilterBuilder : SqlFilterBuilder<BrokerAmPermissionColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BrokerAmPermissionFilterBuilder class.
		/// </summary>
		public BrokerAmPermissionFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the BrokerAmPermissionFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BrokerAmPermissionFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BrokerAmPermissionFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BrokerAmPermissionFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BrokerAmPermissionFilterBuilder
	
	#region BrokerAmPermissionParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;BrokerAmPermissionColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BrokerAmPermission"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BrokerAmPermissionParameterBuilder : ParameterizedSqlFilterBuilder<BrokerAmPermissionColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BrokerAmPermissionParameterBuilder class.
		/// </summary>
		public BrokerAmPermissionParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the BrokerAmPermissionParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BrokerAmPermissionParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BrokerAmPermissionParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BrokerAmPermissionParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BrokerAmPermissionParameterBuilder
	
	#region BrokerAmPermissionSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;BrokerAmPermissionColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BrokerAmPermission"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class BrokerAmPermissionSortBuilder : SqlSortBuilder<BrokerAmPermissionColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BrokerAmPermissionSqlSortBuilder class.
		/// </summary>
		public BrokerAmPermissionSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion BrokerAmPermissionSortBuilder
	
} // end namespace
