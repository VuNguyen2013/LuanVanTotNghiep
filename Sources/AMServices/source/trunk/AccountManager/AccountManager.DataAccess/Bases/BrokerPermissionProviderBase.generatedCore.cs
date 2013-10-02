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
	/// This class is the base class for any <see cref="BrokerPermissionProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class BrokerPermissionProviderBaseCore : EntityProviderBase<AccountManager.Entities.BrokerPermission, AccountManager.Entities.BrokerPermissionKey>
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
		public override bool Delete(TransactionManager transactionManager, AccountManager.Entities.BrokerPermissionKey key)
		{
			return Delete(transactionManager, key.BrokerId, key.PermissionId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_brokerId">. Primary Key.</param>
		/// <param name="_permissionId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.String _brokerId, System.Int32 _permissionId)
		{
			return Delete(null, _brokerId, _permissionId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_brokerId">. Primary Key.</param>
		/// <param name="_permissionId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.String _brokerId, System.Int32 _permissionId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_BrokerPermission_BrokerAccount key.
		///		FK_BrokerPermission_BrokerAccount Description: 
		/// </summary>
		/// <param name="_brokerId"></param>
		/// <returns>Returns a typed collection of AccountManager.Entities.BrokerPermission objects.</returns>
		public TList<BrokerPermission> GetByBrokerId(System.String _brokerId)
		{
			int count = -1;
			return GetByBrokerId(_brokerId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_BrokerPermission_BrokerAccount key.
		///		FK_BrokerPermission_BrokerAccount Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_brokerId"></param>
		/// <returns>Returns a typed collection of AccountManager.Entities.BrokerPermission objects.</returns>
		/// <remarks></remarks>
		public TList<BrokerPermission> GetByBrokerId(TransactionManager transactionManager, System.String _brokerId)
		{
			int count = -1;
			return GetByBrokerId(transactionManager, _brokerId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_BrokerPermission_BrokerAccount key.
		///		FK_BrokerPermission_BrokerAccount Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_brokerId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AccountManager.Entities.BrokerPermission objects.</returns>
		public TList<BrokerPermission> GetByBrokerId(TransactionManager transactionManager, System.String _brokerId, int start, int pageLength)
		{
			int count = -1;
			return GetByBrokerId(transactionManager, _brokerId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_BrokerPermission_BrokerAccount key.
		///		fkBrokerPermissionBrokerAccount Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_brokerId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AccountManager.Entities.BrokerPermission objects.</returns>
		public TList<BrokerPermission> GetByBrokerId(System.String _brokerId, int start, int pageLength)
		{
			int count =  -1;
			return GetByBrokerId(null, _brokerId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_BrokerPermission_BrokerAccount key.
		///		fkBrokerPermissionBrokerAccount Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_brokerId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AccountManager.Entities.BrokerPermission objects.</returns>
		public TList<BrokerPermission> GetByBrokerId(System.String _brokerId, int start, int pageLength,out int count)
		{
			return GetByBrokerId(null, _brokerId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_BrokerPermission_BrokerAccount key.
		///		FK_BrokerPermission_BrokerAccount Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_brokerId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of AccountManager.Entities.BrokerPermission objects.</returns>
		public abstract TList<BrokerPermission> GetByBrokerId(TransactionManager transactionManager, System.String _brokerId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_BrokerPermission_BrokerAMPermission key.
		///		FK_BrokerPermission_BrokerAMPermission Description: 
		/// </summary>
		/// <param name="_permissionId"></param>
		/// <returns>Returns a typed collection of AccountManager.Entities.BrokerPermission objects.</returns>
		public TList<BrokerPermission> GetByPermissionId(System.Int32 _permissionId)
		{
			int count = -1;
			return GetByPermissionId(_permissionId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_BrokerPermission_BrokerAMPermission key.
		///		FK_BrokerPermission_BrokerAMPermission Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_permissionId"></param>
		/// <returns>Returns a typed collection of AccountManager.Entities.BrokerPermission objects.</returns>
		/// <remarks></remarks>
		public TList<BrokerPermission> GetByPermissionId(TransactionManager transactionManager, System.Int32 _permissionId)
		{
			int count = -1;
			return GetByPermissionId(transactionManager, _permissionId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_BrokerPermission_BrokerAMPermission key.
		///		FK_BrokerPermission_BrokerAMPermission Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_permissionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AccountManager.Entities.BrokerPermission objects.</returns>
		public TList<BrokerPermission> GetByPermissionId(TransactionManager transactionManager, System.Int32 _permissionId, int start, int pageLength)
		{
			int count = -1;
			return GetByPermissionId(transactionManager, _permissionId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_BrokerPermission_BrokerAMPermission key.
		///		fkBrokerPermissionBrokerAmPermission Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_permissionId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AccountManager.Entities.BrokerPermission objects.</returns>
		public TList<BrokerPermission> GetByPermissionId(System.Int32 _permissionId, int start, int pageLength)
		{
			int count =  -1;
			return GetByPermissionId(null, _permissionId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_BrokerPermission_BrokerAMPermission key.
		///		fkBrokerPermissionBrokerAmPermission Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_permissionId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AccountManager.Entities.BrokerPermission objects.</returns>
		public TList<BrokerPermission> GetByPermissionId(System.Int32 _permissionId, int start, int pageLength,out int count)
		{
			return GetByPermissionId(null, _permissionId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_BrokerPermission_BrokerAMPermission key.
		///		FK_BrokerPermission_BrokerAMPermission Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_permissionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of AccountManager.Entities.BrokerPermission objects.</returns>
		public abstract TList<BrokerPermission> GetByPermissionId(TransactionManager transactionManager, System.Int32 _permissionId, int start, int pageLength, out int count);
		
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
		public override AccountManager.Entities.BrokerPermission Get(TransactionManager transactionManager, AccountManager.Entities.BrokerPermissionKey key, int start, int pageLength)
		{
			return GetByBrokerIdPermissionId(transactionManager, key.BrokerId, key.PermissionId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_BrokerPermission index.
		/// </summary>
		/// <param name="_brokerId"></param>
		/// <param name="_permissionId"></param>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.BrokerPermission"/> class.</returns>
		public AccountManager.Entities.BrokerPermission GetByBrokerIdPermissionId(System.String _brokerId, System.Int32 _permissionId)
		{
			int count = -1;
			return GetByBrokerIdPermissionId(null,_brokerId, _permissionId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_BrokerPermission index.
		/// </summary>
		/// <param name="_brokerId"></param>
		/// <param name="_permissionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.BrokerPermission"/> class.</returns>
		public AccountManager.Entities.BrokerPermission GetByBrokerIdPermissionId(System.String _brokerId, System.Int32 _permissionId, int start, int pageLength)
		{
			int count = -1;
			return GetByBrokerIdPermissionId(null, _brokerId, _permissionId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_BrokerPermission index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_brokerId"></param>
		/// <param name="_permissionId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.BrokerPermission"/> class.</returns>
		public AccountManager.Entities.BrokerPermission GetByBrokerIdPermissionId(TransactionManager transactionManager, System.String _brokerId, System.Int32 _permissionId)
		{
			int count = -1;
			return GetByBrokerIdPermissionId(transactionManager, _brokerId, _permissionId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_BrokerPermission index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_brokerId"></param>
		/// <param name="_permissionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.BrokerPermission"/> class.</returns>
		public AccountManager.Entities.BrokerPermission GetByBrokerIdPermissionId(TransactionManager transactionManager, System.String _brokerId, System.Int32 _permissionId, int start, int pageLength)
		{
			int count = -1;
			return GetByBrokerIdPermissionId(transactionManager, _brokerId, _permissionId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_BrokerPermission index.
		/// </summary>
		/// <param name="_brokerId"></param>
		/// <param name="_permissionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.BrokerPermission"/> class.</returns>
		public AccountManager.Entities.BrokerPermission GetByBrokerIdPermissionId(System.String _brokerId, System.Int32 _permissionId, int start, int pageLength, out int count)
		{
			return GetByBrokerIdPermissionId(null, _brokerId, _permissionId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_BrokerPermission index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_brokerId"></param>
		/// <param name="_permissionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.BrokerPermission"/> class.</returns>
		public abstract AccountManager.Entities.BrokerPermission GetByBrokerIdPermissionId(TransactionManager transactionManager, System.String _brokerId, System.Int32 _permissionId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#region _BrokerPermission_DeleteByBrokerId 
		
		/// <summary>
		///	This method wrap the '_BrokerPermission_DeleteByBrokerId' stored procedure. 
		/// </summary>
		/// <param name="brokerId"> A <c>System.String</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void DeleteByBrokerId(System.String brokerId)
		{
			 DeleteByBrokerId(null, 0, int.MaxValue , brokerId);
		}
		
		/// <summary>
		///	This method wrap the '_BrokerPermission_DeleteByBrokerId' stored procedure. 
		/// </summary>
		/// <param name="brokerId"> A <c>System.String</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void DeleteByBrokerId(int start, int pageLength, System.String brokerId)
		{
			 DeleteByBrokerId(null, start, pageLength , brokerId);
		}
				
		/// <summary>
		///	This method wrap the '_BrokerPermission_DeleteByBrokerId' stored procedure. 
		/// </summary>
		/// <param name="brokerId"> A <c>System.String</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void DeleteByBrokerId(TransactionManager transactionManager, System.String brokerId)
		{
			 DeleteByBrokerId(transactionManager, 0, int.MaxValue , brokerId);
		}
		
		/// <summary>
		///	This method wrap the '_BrokerPermission_DeleteByBrokerId' stored procedure. 
		/// </summary>
		/// <param name="brokerId"> A <c>System.String</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public abstract void DeleteByBrokerId(TransactionManager transactionManager, int start, int pageLength , System.String brokerId);
		
		#endregion
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;BrokerPermission&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;BrokerPermission&gt;"/></returns>
		public static TList<BrokerPermission> Fill(IDataReader reader, TList<BrokerPermission> rows, int start, int pageLength)
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
				
				AccountManager.Entities.BrokerPermission c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("BrokerPermission")
					.Append("|").Append((System.String)reader[((int)BrokerPermissionColumn.BrokerId - 1)])
					.Append("|").Append((System.Int32)reader[((int)BrokerPermissionColumn.PermissionId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<BrokerPermission>(
					key.ToString(), // EntityTrackingKey
					"BrokerPermission",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new AccountManager.Entities.BrokerPermission();
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
					c.BrokerId = (System.String)reader[((int)BrokerPermissionColumn.BrokerId - 1)];
					c.OriginalBrokerId = c.BrokerId;
					c.PermissionId = (System.Int32)reader[((int)BrokerPermissionColumn.PermissionId - 1)];
					c.OriginalPermissionId = c.PermissionId;
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="AccountManager.Entities.BrokerPermission"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AccountManager.Entities.BrokerPermission"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AccountManager.Entities.BrokerPermission entity)
		{
			if (!reader.Read()) return;
			
			entity.BrokerId = (System.String)reader[((int)BrokerPermissionColumn.BrokerId - 1)];
			entity.OriginalBrokerId = (System.String)reader["BrokerID"];
			entity.PermissionId = (System.Int32)reader[((int)BrokerPermissionColumn.PermissionId - 1)];
			entity.OriginalPermissionId = (System.Int32)reader["PermissionID"];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="AccountManager.Entities.BrokerPermission"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AccountManager.Entities.BrokerPermission"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AccountManager.Entities.BrokerPermission entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.BrokerId = (System.String)dataRow["BrokerID"];
			entity.OriginalBrokerId = (System.String)dataRow["BrokerID"];
			entity.PermissionId = (System.Int32)dataRow["PermissionID"];
			entity.OriginalPermissionId = (System.Int32)dataRow["PermissionID"];
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
		/// <param name="entity">The <see cref="AccountManager.Entities.BrokerPermission"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AccountManager.Entities.BrokerPermission Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AccountManager.Entities.BrokerPermission entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region BrokerIdSource	
			if (CanDeepLoad(entity, "BrokerAccount|BrokerIdSource", deepLoadType, innerList) 
				&& entity.BrokerIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.BrokerId;
				BrokerAccount tmpEntity = EntityManager.LocateEntity<BrokerAccount>(EntityLocator.ConstructKeyFromPkItems(typeof(BrokerAccount), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.BrokerIdSource = tmpEntity;
				else
					entity.BrokerIdSource = DataRepository.BrokerAccountProvider.GetByBrokerId(transactionManager, entity.BrokerId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'BrokerIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.BrokerIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.BrokerAccountProvider.DeepLoad(transactionManager, entity.BrokerIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion BrokerIdSource

			#region PermissionIdSource	
			if (CanDeepLoad(entity, "BrokerAmPermission|PermissionIdSource", deepLoadType, innerList) 
				&& entity.PermissionIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.PermissionId;
				BrokerAmPermission tmpEntity = EntityManager.LocateEntity<BrokerAmPermission>(EntityLocator.ConstructKeyFromPkItems(typeof(BrokerAmPermission), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.PermissionIdSource = tmpEntity;
				else
					entity.PermissionIdSource = DataRepository.BrokerAmPermissionProvider.GetByPermissionId(transactionManager, entity.PermissionId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PermissionIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.PermissionIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.BrokerAmPermissionProvider.DeepLoad(transactionManager, entity.PermissionIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion PermissionIdSource
			
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
		/// Deep Save the entire object graph of the AccountManager.Entities.BrokerPermission object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AccountManager.Entities.BrokerPermission instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AccountManager.Entities.BrokerPermission Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AccountManager.Entities.BrokerPermission entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region BrokerIdSource
			if (CanDeepSave(entity, "BrokerAccount|BrokerIdSource", deepSaveType, innerList) 
				&& entity.BrokerIdSource != null)
			{
				DataRepository.BrokerAccountProvider.Save(transactionManager, entity.BrokerIdSource);
				entity.BrokerId = entity.BrokerIdSource.BrokerId;
			}
			#endregion 
			
			#region PermissionIdSource
			if (CanDeepSave(entity, "BrokerAmPermission|PermissionIdSource", deepSaveType, innerList) 
				&& entity.PermissionIdSource != null)
			{
				DataRepository.BrokerAmPermissionProvider.Save(transactionManager, entity.PermissionIdSource);
				entity.PermissionId = entity.PermissionIdSource.PermissionId;
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
	
	#region BrokerPermissionChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>AccountManager.Entities.BrokerPermission</c>
	///</summary>
	public enum BrokerPermissionChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>BrokerAccount</c> at BrokerIdSource
		///</summary>
		[ChildEntityType(typeof(BrokerAccount))]
		BrokerAccount,
			
		///<summary>
		/// Composite Property for <c>BrokerAmPermission</c> at PermissionIdSource
		///</summary>
		[ChildEntityType(typeof(BrokerAmPermission))]
		BrokerAmPermission,
		}
	
	#endregion BrokerPermissionChildEntityTypes
	
	#region BrokerPermissionFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;BrokerPermissionColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BrokerPermission"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BrokerPermissionFilterBuilder : SqlFilterBuilder<BrokerPermissionColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BrokerPermissionFilterBuilder class.
		/// </summary>
		public BrokerPermissionFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the BrokerPermissionFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BrokerPermissionFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BrokerPermissionFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BrokerPermissionFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BrokerPermissionFilterBuilder
	
	#region BrokerPermissionParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;BrokerPermissionColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BrokerPermission"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BrokerPermissionParameterBuilder : ParameterizedSqlFilterBuilder<BrokerPermissionColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BrokerPermissionParameterBuilder class.
		/// </summary>
		public BrokerPermissionParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the BrokerPermissionParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BrokerPermissionParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BrokerPermissionParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BrokerPermissionParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BrokerPermissionParameterBuilder
	
	#region BrokerPermissionSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;BrokerPermissionColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BrokerPermission"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class BrokerPermissionSortBuilder : SqlSortBuilder<BrokerPermissionColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BrokerPermissionSqlSortBuilder class.
		/// </summary>
		public BrokerPermissionSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion BrokerPermissionSortBuilder
	
} // end namespace
