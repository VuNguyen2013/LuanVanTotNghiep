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
	/// This class is the base class for any <see cref="SubCustAccountPermissionProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class SubCustAccountPermissionProviderBaseCore : EntityProviderBase<AccountManager.Entities.SubCustAccountPermission, AccountManager.Entities.SubCustAccountPermissionKey>
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
		public override bool Delete(TransactionManager transactionManager, AccountManager.Entities.SubCustAccountPermissionKey key)
		{
			return Delete(transactionManager, key.SubCustAccountId, key.CustServicesPermissionId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_subCustAccountId">. Primary Key.</param>
		/// <param name="_custServicesPermissionId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.String _subCustAccountId, System.Int32 _custServicesPermissionId)
		{
			return Delete(null, _subCustAccountId, _custServicesPermissionId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_subCustAccountId">. Primary Key.</param>
		/// <param name="_custServicesPermissionId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.String _subCustAccountId, System.Int32 _custServicesPermissionId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_SubCustAccountPermission_CustServicesPermission key.
		///		FK_SubCustAccountPermission_CustServicesPermission Description: 
		/// </summary>
		/// <param name="_custServicesPermissionId"></param>
		/// <returns>Returns a typed collection of AccountManager.Entities.SubCustAccountPermission objects.</returns>
		public TList<SubCustAccountPermission> GetByCustServicesPermissionId(System.Int32 _custServicesPermissionId)
		{
			int count = -1;
			return GetByCustServicesPermissionId(_custServicesPermissionId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_SubCustAccountPermission_CustServicesPermission key.
		///		FK_SubCustAccountPermission_CustServicesPermission Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_custServicesPermissionId"></param>
		/// <returns>Returns a typed collection of AccountManager.Entities.SubCustAccountPermission objects.</returns>
		/// <remarks></remarks>
		public TList<SubCustAccountPermission> GetByCustServicesPermissionId(TransactionManager transactionManager, System.Int32 _custServicesPermissionId)
		{
			int count = -1;
			return GetByCustServicesPermissionId(transactionManager, _custServicesPermissionId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_SubCustAccountPermission_CustServicesPermission key.
		///		FK_SubCustAccountPermission_CustServicesPermission Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_custServicesPermissionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AccountManager.Entities.SubCustAccountPermission objects.</returns>
		public TList<SubCustAccountPermission> GetByCustServicesPermissionId(TransactionManager transactionManager, System.Int32 _custServicesPermissionId, int start, int pageLength)
		{
			int count = -1;
			return GetByCustServicesPermissionId(transactionManager, _custServicesPermissionId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_SubCustAccountPermission_CustServicesPermission key.
		///		fkSubCustAccountPermissionCustServicesPermission Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_custServicesPermissionId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AccountManager.Entities.SubCustAccountPermission objects.</returns>
		public TList<SubCustAccountPermission> GetByCustServicesPermissionId(System.Int32 _custServicesPermissionId, int start, int pageLength)
		{
			int count =  -1;
			return GetByCustServicesPermissionId(null, _custServicesPermissionId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_SubCustAccountPermission_CustServicesPermission key.
		///		fkSubCustAccountPermissionCustServicesPermission Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_custServicesPermissionId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AccountManager.Entities.SubCustAccountPermission objects.</returns>
		public TList<SubCustAccountPermission> GetByCustServicesPermissionId(System.Int32 _custServicesPermissionId, int start, int pageLength,out int count)
		{
			return GetByCustServicesPermissionId(null, _custServicesPermissionId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_SubCustAccountPermission_CustServicesPermission key.
		///		FK_SubCustAccountPermission_CustServicesPermission Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_custServicesPermissionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of AccountManager.Entities.SubCustAccountPermission objects.</returns>
		public abstract TList<SubCustAccountPermission> GetByCustServicesPermissionId(TransactionManager transactionManager, System.Int32 _custServicesPermissionId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_SubCustAccountPermission_SubCustAccount key.
		///		FK_SubCustAccountPermission_SubCustAccount Description: 
		/// </summary>
		/// <param name="_subCustAccountId"></param>
		/// <returns>Returns a typed collection of AccountManager.Entities.SubCustAccountPermission objects.</returns>
		public TList<SubCustAccountPermission> GetBySubCustAccountId(System.String _subCustAccountId)
		{
			int count = -1;
			return GetBySubCustAccountId(_subCustAccountId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_SubCustAccountPermission_SubCustAccount key.
		///		FK_SubCustAccountPermission_SubCustAccount Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_subCustAccountId"></param>
		/// <returns>Returns a typed collection of AccountManager.Entities.SubCustAccountPermission objects.</returns>
		/// <remarks></remarks>
		public TList<SubCustAccountPermission> GetBySubCustAccountId(TransactionManager transactionManager, System.String _subCustAccountId)
		{
			int count = -1;
			return GetBySubCustAccountId(transactionManager, _subCustAccountId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_SubCustAccountPermission_SubCustAccount key.
		///		FK_SubCustAccountPermission_SubCustAccount Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_subCustAccountId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AccountManager.Entities.SubCustAccountPermission objects.</returns>
		public TList<SubCustAccountPermission> GetBySubCustAccountId(TransactionManager transactionManager, System.String _subCustAccountId, int start, int pageLength)
		{
			int count = -1;
			return GetBySubCustAccountId(transactionManager, _subCustAccountId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_SubCustAccountPermission_SubCustAccount key.
		///		fkSubCustAccountPermissionSubCustAccount Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_subCustAccountId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AccountManager.Entities.SubCustAccountPermission objects.</returns>
		public TList<SubCustAccountPermission> GetBySubCustAccountId(System.String _subCustAccountId, int start, int pageLength)
		{
			int count =  -1;
			return GetBySubCustAccountId(null, _subCustAccountId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_SubCustAccountPermission_SubCustAccount key.
		///		fkSubCustAccountPermissionSubCustAccount Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_subCustAccountId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AccountManager.Entities.SubCustAccountPermission objects.</returns>
		public TList<SubCustAccountPermission> GetBySubCustAccountId(System.String _subCustAccountId, int start, int pageLength,out int count)
		{
			return GetBySubCustAccountId(null, _subCustAccountId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_SubCustAccountPermission_SubCustAccount key.
		///		FK_SubCustAccountPermission_SubCustAccount Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_subCustAccountId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of AccountManager.Entities.SubCustAccountPermission objects.</returns>
		public abstract TList<SubCustAccountPermission> GetBySubCustAccountId(TransactionManager transactionManager, System.String _subCustAccountId, int start, int pageLength, out int count);
		
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
		public override AccountManager.Entities.SubCustAccountPermission Get(TransactionManager transactionManager, AccountManager.Entities.SubCustAccountPermissionKey key, int start, int pageLength)
		{
			return GetBySubCustAccountIdCustServicesPermissionId(transactionManager, key.SubCustAccountId, key.CustServicesPermissionId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_SubCustAccountPermission index.
		/// </summary>
		/// <param name="_subCustAccountId"></param>
		/// <param name="_custServicesPermissionId"></param>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.SubCustAccountPermission"/> class.</returns>
		public AccountManager.Entities.SubCustAccountPermission GetBySubCustAccountIdCustServicesPermissionId(System.String _subCustAccountId, System.Int32 _custServicesPermissionId)
		{
			int count = -1;
			return GetBySubCustAccountIdCustServicesPermissionId(null,_subCustAccountId, _custServicesPermissionId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_SubCustAccountPermission index.
		/// </summary>
		/// <param name="_subCustAccountId"></param>
		/// <param name="_custServicesPermissionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.SubCustAccountPermission"/> class.</returns>
		public AccountManager.Entities.SubCustAccountPermission GetBySubCustAccountIdCustServicesPermissionId(System.String _subCustAccountId, System.Int32 _custServicesPermissionId, int start, int pageLength)
		{
			int count = -1;
			return GetBySubCustAccountIdCustServicesPermissionId(null, _subCustAccountId, _custServicesPermissionId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_SubCustAccountPermission index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_subCustAccountId"></param>
		/// <param name="_custServicesPermissionId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.SubCustAccountPermission"/> class.</returns>
		public AccountManager.Entities.SubCustAccountPermission GetBySubCustAccountIdCustServicesPermissionId(TransactionManager transactionManager, System.String _subCustAccountId, System.Int32 _custServicesPermissionId)
		{
			int count = -1;
			return GetBySubCustAccountIdCustServicesPermissionId(transactionManager, _subCustAccountId, _custServicesPermissionId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_SubCustAccountPermission index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_subCustAccountId"></param>
		/// <param name="_custServicesPermissionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.SubCustAccountPermission"/> class.</returns>
		public AccountManager.Entities.SubCustAccountPermission GetBySubCustAccountIdCustServicesPermissionId(TransactionManager transactionManager, System.String _subCustAccountId, System.Int32 _custServicesPermissionId, int start, int pageLength)
		{
			int count = -1;
			return GetBySubCustAccountIdCustServicesPermissionId(transactionManager, _subCustAccountId, _custServicesPermissionId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_SubCustAccountPermission index.
		/// </summary>
		/// <param name="_subCustAccountId"></param>
		/// <param name="_custServicesPermissionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.SubCustAccountPermission"/> class.</returns>
		public AccountManager.Entities.SubCustAccountPermission GetBySubCustAccountIdCustServicesPermissionId(System.String _subCustAccountId, System.Int32 _custServicesPermissionId, int start, int pageLength, out int count)
		{
			return GetBySubCustAccountIdCustServicesPermissionId(null, _subCustAccountId, _custServicesPermissionId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_SubCustAccountPermission index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_subCustAccountId"></param>
		/// <param name="_custServicesPermissionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.SubCustAccountPermission"/> class.</returns>
		public abstract AccountManager.Entities.SubCustAccountPermission GetBySubCustAccountIdCustServicesPermissionId(TransactionManager transactionManager, System.String _subCustAccountId, System.Int32 _custServicesPermissionId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;SubCustAccountPermission&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;SubCustAccountPermission&gt;"/></returns>
		public static TList<SubCustAccountPermission> Fill(IDataReader reader, TList<SubCustAccountPermission> rows, int start, int pageLength)
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
				
				AccountManager.Entities.SubCustAccountPermission c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("SubCustAccountPermission")
					.Append("|").Append((System.String)reader[((int)SubCustAccountPermissionColumn.SubCustAccountId - 1)])
					.Append("|").Append((System.Int32)reader[((int)SubCustAccountPermissionColumn.CustServicesPermissionId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<SubCustAccountPermission>(
					key.ToString(), // EntityTrackingKey
					"SubCustAccountPermission",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new AccountManager.Entities.SubCustAccountPermission();
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
					c.SubCustAccountId = (System.String)reader[((int)SubCustAccountPermissionColumn.SubCustAccountId - 1)];
					c.OriginalSubCustAccountId = c.SubCustAccountId;
					c.CustServicesPermissionId = (System.Int32)reader[((int)SubCustAccountPermissionColumn.CustServicesPermissionId - 1)];
					c.OriginalCustServicesPermissionId = c.CustServicesPermissionId;
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="AccountManager.Entities.SubCustAccountPermission"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AccountManager.Entities.SubCustAccountPermission"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AccountManager.Entities.SubCustAccountPermission entity)
		{
			if (!reader.Read()) return;
			
			entity.SubCustAccountId = (System.String)reader[((int)SubCustAccountPermissionColumn.SubCustAccountId - 1)];
			entity.OriginalSubCustAccountId = (System.String)reader["SubCustAccountID"];
			entity.CustServicesPermissionId = (System.Int32)reader[((int)SubCustAccountPermissionColumn.CustServicesPermissionId - 1)];
			entity.OriginalCustServicesPermissionId = (System.Int32)reader["CustServicesPermissionID"];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="AccountManager.Entities.SubCustAccountPermission"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AccountManager.Entities.SubCustAccountPermission"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AccountManager.Entities.SubCustAccountPermission entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.SubCustAccountId = (System.String)dataRow["SubCustAccountID"];
			entity.OriginalSubCustAccountId = (System.String)dataRow["SubCustAccountID"];
			entity.CustServicesPermissionId = (System.Int32)dataRow["CustServicesPermissionID"];
			entity.OriginalCustServicesPermissionId = (System.Int32)dataRow["CustServicesPermissionID"];
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
		/// <param name="entity">The <see cref="AccountManager.Entities.SubCustAccountPermission"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AccountManager.Entities.SubCustAccountPermission Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AccountManager.Entities.SubCustAccountPermission entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region CustServicesPermissionIdSource	
			if (CanDeepLoad(entity, "CustServicesPermission|CustServicesPermissionIdSource", deepLoadType, innerList) 
				&& entity.CustServicesPermissionIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.CustServicesPermissionId;
				CustServicesPermission tmpEntity = EntityManager.LocateEntity<CustServicesPermission>(EntityLocator.ConstructKeyFromPkItems(typeof(CustServicesPermission), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.CustServicesPermissionIdSource = tmpEntity;
				else
					entity.CustServicesPermissionIdSource = DataRepository.CustServicesPermissionProvider.GetByCustServicesPermissionId(transactionManager, entity.CustServicesPermissionId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'CustServicesPermissionIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.CustServicesPermissionIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.CustServicesPermissionProvider.DeepLoad(transactionManager, entity.CustServicesPermissionIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion CustServicesPermissionIdSource

			#region SubCustAccountIdSource	
			if (CanDeepLoad(entity, "SubCustAccount|SubCustAccountIdSource", deepLoadType, innerList) 
				&& entity.SubCustAccountIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.SubCustAccountId;
				SubCustAccount tmpEntity = EntityManager.LocateEntity<SubCustAccount>(EntityLocator.ConstructKeyFromPkItems(typeof(SubCustAccount), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.SubCustAccountIdSource = tmpEntity;
				else
					entity.SubCustAccountIdSource = DataRepository.SubCustAccountProvider.GetBySubCustAccountId(transactionManager, entity.SubCustAccountId);		
				
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
		/// Deep Save the entire object graph of the AccountManager.Entities.SubCustAccountPermission object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AccountManager.Entities.SubCustAccountPermission instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AccountManager.Entities.SubCustAccountPermission Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AccountManager.Entities.SubCustAccountPermission entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region CustServicesPermissionIdSource
			if (CanDeepSave(entity, "CustServicesPermission|CustServicesPermissionIdSource", deepSaveType, innerList) 
				&& entity.CustServicesPermissionIdSource != null)
			{
				DataRepository.CustServicesPermissionProvider.Save(transactionManager, entity.CustServicesPermissionIdSource);
				entity.CustServicesPermissionId = entity.CustServicesPermissionIdSource.CustServicesPermissionId;
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
	
	#region SubCustAccountPermissionChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>AccountManager.Entities.SubCustAccountPermission</c>
	///</summary>
	public enum SubCustAccountPermissionChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>CustServicesPermission</c> at CustServicesPermissionIdSource
		///</summary>
		[ChildEntityType(typeof(CustServicesPermission))]
		CustServicesPermission,
			
		///<summary>
		/// Composite Property for <c>SubCustAccount</c> at SubCustAccountIdSource
		///</summary>
		[ChildEntityType(typeof(SubCustAccount))]
		SubCustAccount,
		}
	
	#endregion SubCustAccountPermissionChildEntityTypes
	
	#region SubCustAccountPermissionFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;SubCustAccountPermissionColumn&gt;"/> class
	/// that is used exclusively with a <see cref="SubCustAccountPermission"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class SubCustAccountPermissionFilterBuilder : SqlFilterBuilder<SubCustAccountPermissionColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SubCustAccountPermissionFilterBuilder class.
		/// </summary>
		public SubCustAccountPermissionFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the SubCustAccountPermissionFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public SubCustAccountPermissionFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the SubCustAccountPermissionFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public SubCustAccountPermissionFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion SubCustAccountPermissionFilterBuilder
	
	#region SubCustAccountPermissionParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;SubCustAccountPermissionColumn&gt;"/> class
	/// that is used exclusively with a <see cref="SubCustAccountPermission"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class SubCustAccountPermissionParameterBuilder : ParameterizedSqlFilterBuilder<SubCustAccountPermissionColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SubCustAccountPermissionParameterBuilder class.
		/// </summary>
		public SubCustAccountPermissionParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the SubCustAccountPermissionParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public SubCustAccountPermissionParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the SubCustAccountPermissionParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public SubCustAccountPermissionParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion SubCustAccountPermissionParameterBuilder
	
	#region SubCustAccountPermissionSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;SubCustAccountPermissionColumn&gt;"/> class
	/// that is used exclusively with a <see cref="SubCustAccountPermission"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class SubCustAccountPermissionSortBuilder : SqlSortBuilder<SubCustAccountPermissionColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SubCustAccountPermissionSqlSortBuilder class.
		/// </summary>
		public SubCustAccountPermissionSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion SubCustAccountPermissionSortBuilder
	
} // end namespace
