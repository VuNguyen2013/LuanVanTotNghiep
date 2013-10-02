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
	/// This class is the base class for any <see cref="CustServicesPermissionProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class CustServicesPermissionProviderBaseCore : EntityProviderBase<AccountManager.Entities.CustServicesPermission, AccountManager.Entities.CustServicesPermissionKey>
	{		
		#region Get from Many To Many Relationship Functions
		#region GetBySubCustAccountIdFromSubCustAccountPermission
		
		/// <summary>
		///		Gets CustServicesPermission objects from the datasource by SubCustAccountID in the
		///		SubCustAccountPermission table. Table CustServicesPermission is related to table SubCustAccount
		///		through the (M:N) relationship defined in the SubCustAccountPermission table.
		/// </summary>
		/// <param name="_subCustAccountId"></param>
		/// <returns>Returns a typed collection of CustServicesPermission objects.</returns>
		public TList<CustServicesPermission> GetBySubCustAccountIdFromSubCustAccountPermission(System.String _subCustAccountId)
		{
			int count = -1;
			return GetBySubCustAccountIdFromSubCustAccountPermission(null,_subCustAccountId, 0, int.MaxValue, out count);
			
		}
		
		/// <summary>
		///		Gets AccountManager.Entities.CustServicesPermission objects from the datasource by SubCustAccountID in the
		///		SubCustAccountPermission table. Table CustServicesPermission is related to table SubCustAccount
		///		through the (M:N) relationship defined in the SubCustAccountPermission table.
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_subCustAccountId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a TList of CustServicesPermission objects.</returns>
		public TList<CustServicesPermission> GetBySubCustAccountIdFromSubCustAccountPermission(System.String _subCustAccountId, int start, int pageLength)
		{
			int count = -1;
			return GetBySubCustAccountIdFromSubCustAccountPermission(null, _subCustAccountId, start, pageLength, out count);
		}
		
		/// <summary>
		///		Gets CustServicesPermission objects from the datasource by SubCustAccountID in the
		///		SubCustAccountPermission table. Table CustServicesPermission is related to table SubCustAccount
		///		through the (M:N) relationship defined in the SubCustAccountPermission table.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_subCustAccountId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of CustServicesPermission objects.</returns>
		public TList<CustServicesPermission> GetBySubCustAccountIdFromSubCustAccountPermission(TransactionManager transactionManager, System.String _subCustAccountId)
		{
			int count = -1;
			return GetBySubCustAccountIdFromSubCustAccountPermission(transactionManager, _subCustAccountId, 0, int.MaxValue, out count);
		}
		
		
		/// <summary>
		///		Gets CustServicesPermission objects from the datasource by SubCustAccountID in the
		///		SubCustAccountPermission table. Table CustServicesPermission is related to table SubCustAccount
		///		through the (M:N) relationship defined in the SubCustAccountPermission table.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_subCustAccountId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of CustServicesPermission objects.</returns>
		public TList<CustServicesPermission> GetBySubCustAccountIdFromSubCustAccountPermission(TransactionManager transactionManager, System.String _subCustAccountId,int start, int pageLength)
		{
			int count = -1;
			return GetBySubCustAccountIdFromSubCustAccountPermission(transactionManager, _subCustAccountId, start, pageLength, out count);
		}
		
		/// <summary>
		///		Gets CustServicesPermission objects from the datasource by SubCustAccountID in the
		///		SubCustAccountPermission table. Table CustServicesPermission is related to table SubCustAccount
		///		through the (M:N) relationship defined in the SubCustAccountPermission table.
		/// </summary>
		/// <param name="_subCustAccountId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of CustServicesPermission objects.</returns>
		public TList<CustServicesPermission> GetBySubCustAccountIdFromSubCustAccountPermission(System.String _subCustAccountId,int start, int pageLength, out int count)
		{
			
			return GetBySubCustAccountIdFromSubCustAccountPermission(null, _subCustAccountId, start, pageLength, out count);
		}


		/// <summary>
		///		Gets CustServicesPermission objects from the datasource by SubCustAccountID in the
		///		SubCustAccountPermission table. Table CustServicesPermission is related to table SubCustAccount
		///		through the (M:N) relationship defined in the SubCustAccountPermission table.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <param name="_subCustAccountId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a TList of CustServicesPermission objects.</returns>
		public abstract TList<CustServicesPermission> GetBySubCustAccountIdFromSubCustAccountPermission(TransactionManager transactionManager,System.String _subCustAccountId, int start, int pageLength, out int count);
		
		#endregion GetBySubCustAccountIdFromSubCustAccountPermission
		
		#endregion	
		
		#region Delete Methods

		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager">A <see cref="TransactionManager"/> object.</param>
		/// <param name="key">The unique identifier of the row to delete.</param>
		/// <returns>Returns true if operation suceeded.</returns>
		public override bool Delete(TransactionManager transactionManager, AccountManager.Entities.CustServicesPermissionKey key)
		{
			return Delete(transactionManager, key.CustServicesPermissionId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_custServicesPermissionId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Int32 _custServicesPermissionId)
		{
			return Delete(null, _custServicesPermissionId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_custServicesPermissionId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Int32 _custServicesPermissionId);		
		
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
		public override AccountManager.Entities.CustServicesPermission Get(TransactionManager transactionManager, AccountManager.Entities.CustServicesPermissionKey key, int start, int pageLength)
		{
			return GetByCustServicesPermissionId(transactionManager, key.CustServicesPermissionId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_CustServicesPermission index.
		/// </summary>
		/// <param name="_custServicesPermissionId"></param>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.CustServicesPermission"/> class.</returns>
		public AccountManager.Entities.CustServicesPermission GetByCustServicesPermissionId(System.Int32 _custServicesPermissionId)
		{
			int count = -1;
			return GetByCustServicesPermissionId(null,_custServicesPermissionId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CustServicesPermission index.
		/// </summary>
		/// <param name="_custServicesPermissionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.CustServicesPermission"/> class.</returns>
		public AccountManager.Entities.CustServicesPermission GetByCustServicesPermissionId(System.Int32 _custServicesPermissionId, int start, int pageLength)
		{
			int count = -1;
			return GetByCustServicesPermissionId(null, _custServicesPermissionId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CustServicesPermission index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_custServicesPermissionId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.CustServicesPermission"/> class.</returns>
		public AccountManager.Entities.CustServicesPermission GetByCustServicesPermissionId(TransactionManager transactionManager, System.Int32 _custServicesPermissionId)
		{
			int count = -1;
			return GetByCustServicesPermissionId(transactionManager, _custServicesPermissionId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CustServicesPermission index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_custServicesPermissionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.CustServicesPermission"/> class.</returns>
		public AccountManager.Entities.CustServicesPermission GetByCustServicesPermissionId(TransactionManager transactionManager, System.Int32 _custServicesPermissionId, int start, int pageLength)
		{
			int count = -1;
			return GetByCustServicesPermissionId(transactionManager, _custServicesPermissionId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CustServicesPermission index.
		/// </summary>
		/// <param name="_custServicesPermissionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.CustServicesPermission"/> class.</returns>
		public AccountManager.Entities.CustServicesPermission GetByCustServicesPermissionId(System.Int32 _custServicesPermissionId, int start, int pageLength, out int count)
		{
			return GetByCustServicesPermissionId(null, _custServicesPermissionId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CustServicesPermission index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_custServicesPermissionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.CustServicesPermission"/> class.</returns>
		public abstract AccountManager.Entities.CustServicesPermission GetByCustServicesPermissionId(TransactionManager transactionManager, System.Int32 _custServicesPermissionId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;CustServicesPermission&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;CustServicesPermission&gt;"/></returns>
		public static TList<CustServicesPermission> Fill(IDataReader reader, TList<CustServicesPermission> rows, int start, int pageLength)
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
				
				AccountManager.Entities.CustServicesPermission c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("CustServicesPermission")
					.Append("|").Append((System.Int32)reader[((int)CustServicesPermissionColumn.CustServicesPermissionId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<CustServicesPermission>(
					key.ToString(), // EntityTrackingKey
					"CustServicesPermission",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new AccountManager.Entities.CustServicesPermission();
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
					c.CustServicesPermissionId = (System.Int32)reader[((int)CustServicesPermissionColumn.CustServicesPermissionId - 1)];
					c.OriginalCustServicesPermissionId = c.CustServicesPermissionId;
					c.PermissionName = (reader.IsDBNull(((int)CustServicesPermissionColumn.PermissionName - 1)))?null:(System.String)reader[((int)CustServicesPermissionColumn.PermissionName - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="AccountManager.Entities.CustServicesPermission"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AccountManager.Entities.CustServicesPermission"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AccountManager.Entities.CustServicesPermission entity)
		{
			if (!reader.Read()) return;
			
			entity.CustServicesPermissionId = (System.Int32)reader[((int)CustServicesPermissionColumn.CustServicesPermissionId - 1)];
			entity.OriginalCustServicesPermissionId = (System.Int32)reader["CustServicesPermissionID"];
			entity.PermissionName = (reader.IsDBNull(((int)CustServicesPermissionColumn.PermissionName - 1)))?null:(System.String)reader[((int)CustServicesPermissionColumn.PermissionName - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="AccountManager.Entities.CustServicesPermission"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AccountManager.Entities.CustServicesPermission"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AccountManager.Entities.CustServicesPermission entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.CustServicesPermissionId = (System.Int32)dataRow["CustServicesPermissionID"];
			entity.OriginalCustServicesPermissionId = (System.Int32)dataRow["CustServicesPermissionID"];
			entity.PermissionName = Convert.IsDBNull(dataRow["PermissionName"]) ? null : (System.String)dataRow["PermissionName"];
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
		/// <param name="entity">The <see cref="AccountManager.Entities.CustServicesPermission"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AccountManager.Entities.CustServicesPermission Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AccountManager.Entities.CustServicesPermission entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByCustServicesPermissionId methods when available
			
			#region SubCustAccountPermissionCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<SubCustAccountPermission>|SubCustAccountPermissionCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'SubCustAccountPermissionCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.SubCustAccountPermissionCollection = DataRepository.SubCustAccountPermissionProvider.GetByCustServicesPermissionId(transactionManager, entity.CustServicesPermissionId);

				if (deep && entity.SubCustAccountPermissionCollection.Count > 0)
				{
					deepHandles.Add("SubCustAccountPermissionCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<SubCustAccountPermission>) DataRepository.SubCustAccountPermissionProvider.DeepLoad,
						new object[] { transactionManager, entity.SubCustAccountPermissionCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region SubCustAccountIdSubCustAccountCollection_From_SubCustAccountPermission
			// RelationshipType.ManyToMany
			if (CanDeepLoad(entity, "List<SubCustAccount>|SubCustAccountIdSubCustAccountCollection_From_SubCustAccountPermission", deepLoadType, innerList))
			{
				entity.SubCustAccountIdSubCustAccountCollection_From_SubCustAccountPermission = DataRepository.SubCustAccountProvider.GetByCustServicesPermissionIdFromSubCustAccountPermission(transactionManager, entity.CustServicesPermissionId);			 
		
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'SubCustAccountIdSubCustAccountCollection_From_SubCustAccountPermission' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.SubCustAccountIdSubCustAccountCollection_From_SubCustAccountPermission != null)
				{
					deepHandles.Add("SubCustAccountIdSubCustAccountCollection_From_SubCustAccountPermission",
						new KeyValuePair<Delegate, object>((DeepLoadHandle< SubCustAccount >) DataRepository.SubCustAccountProvider.DeepLoad,
						new object[] { transactionManager, entity.SubCustAccountIdSubCustAccountCollection_From_SubCustAccountPermission, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the AccountManager.Entities.CustServicesPermission object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AccountManager.Entities.CustServicesPermission instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AccountManager.Entities.CustServicesPermission Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AccountManager.Entities.CustServicesPermission entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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

			#region SubCustAccountIdSubCustAccountCollection_From_SubCustAccountPermission>
			if (CanDeepSave(entity.SubCustAccountIdSubCustAccountCollection_From_SubCustAccountPermission, "List<SubCustAccount>|SubCustAccountIdSubCustAccountCollection_From_SubCustAccountPermission", deepSaveType, innerList))
			{
				if (entity.SubCustAccountIdSubCustAccountCollection_From_SubCustAccountPermission.Count > 0 || entity.SubCustAccountIdSubCustAccountCollection_From_SubCustAccountPermission.DeletedItems.Count > 0)
				{
					DataRepository.SubCustAccountProvider.Save(transactionManager, entity.SubCustAccountIdSubCustAccountCollection_From_SubCustAccountPermission); 
					deepHandles.Add("SubCustAccountIdSubCustAccountCollection_From_SubCustAccountPermission",
						new KeyValuePair<Delegate, object>((DeepSaveHandle<SubCustAccount>) DataRepository.SubCustAccountProvider.DeepSave,
						new object[] { transactionManager, entity.SubCustAccountIdSubCustAccountCollection_From_SubCustAccountPermission, deepSaveType, childTypes, innerList }
					));
				}
			}
			#endregion 
	
			#region List<SubCustAccountPermission>
				if (CanDeepSave(entity.SubCustAccountPermissionCollection, "List<SubCustAccountPermission>|SubCustAccountPermissionCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(SubCustAccountPermission child in entity.SubCustAccountPermissionCollection)
					{
						if(child.CustServicesPermissionIdSource != null)
						{
								child.CustServicesPermissionId = child.CustServicesPermissionIdSource.CustServicesPermissionId;
						}

						if(child.SubCustAccountIdSource != null)
						{
								child.SubCustAccountId = child.SubCustAccountIdSource.SubCustAccountId;
						}

					}

					if (entity.SubCustAccountPermissionCollection.Count > 0 || entity.SubCustAccountPermissionCollection.DeletedItems.Count > 0)
					{
						//DataRepository.SubCustAccountPermissionProvider.Save(transactionManager, entity.SubCustAccountPermissionCollection);
						
						deepHandles.Add("SubCustAccountPermissionCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< SubCustAccountPermission >) DataRepository.SubCustAccountPermissionProvider.DeepSave,
							new object[] { transactionManager, entity.SubCustAccountPermissionCollection, deepSaveType, childTypes, innerList }
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
	
	#region CustServicesPermissionChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>AccountManager.Entities.CustServicesPermission</c>
	///</summary>
	public enum CustServicesPermissionChildEntityTypes
	{

		///<summary>
		/// Collection of <c>CustServicesPermission</c> as OneToMany for SubCustAccountPermissionCollection
		///</summary>
		[ChildEntityType(typeof(TList<SubCustAccountPermission>))]
		SubCustAccountPermissionCollection,

		///<summary>
		/// Collection of <c>CustServicesPermission</c> as ManyToMany for SubCustAccountCollection_From_SubCustAccountPermission
		///</summary>
		[ChildEntityType(typeof(TList<SubCustAccount>))]
		SubCustAccountIdSubCustAccountCollection_From_SubCustAccountPermission,
	}
	
	#endregion CustServicesPermissionChildEntityTypes
	
	#region CustServicesPermissionFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;CustServicesPermissionColumn&gt;"/> class
	/// that is used exclusively with a <see cref="CustServicesPermission"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CustServicesPermissionFilterBuilder : SqlFilterBuilder<CustServicesPermissionColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CustServicesPermissionFilterBuilder class.
		/// </summary>
		public CustServicesPermissionFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the CustServicesPermissionFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CustServicesPermissionFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CustServicesPermissionFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CustServicesPermissionFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CustServicesPermissionFilterBuilder
	
	#region CustServicesPermissionParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;CustServicesPermissionColumn&gt;"/> class
	/// that is used exclusively with a <see cref="CustServicesPermission"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CustServicesPermissionParameterBuilder : ParameterizedSqlFilterBuilder<CustServicesPermissionColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CustServicesPermissionParameterBuilder class.
		/// </summary>
		public CustServicesPermissionParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the CustServicesPermissionParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CustServicesPermissionParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CustServicesPermissionParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CustServicesPermissionParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CustServicesPermissionParameterBuilder
	
	#region CustServicesPermissionSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;CustServicesPermissionColumn&gt;"/> class
	/// that is used exclusively with a <see cref="CustServicesPermission"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class CustServicesPermissionSortBuilder : SqlSortBuilder<CustServicesPermissionColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CustServicesPermissionSqlSortBuilder class.
		/// </summary>
		public CustServicesPermissionSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion CustServicesPermissionSortBuilder
	
} // end namespace
