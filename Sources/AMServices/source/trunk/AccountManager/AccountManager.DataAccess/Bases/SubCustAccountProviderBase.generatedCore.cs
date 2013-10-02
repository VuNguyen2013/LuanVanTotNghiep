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
	/// This class is the base class for any <see cref="SubCustAccountProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class SubCustAccountProviderBaseCore : EntityProviderBase<AccountManager.Entities.SubCustAccount, AccountManager.Entities.SubCustAccountKey>
	{		
		#region Get from Many To Many Relationship Functions
		#region GetByCustServicesPermissionIdFromSubCustAccountPermission
		
		/// <summary>
		///		Gets SubCustAccount objects from the datasource by CustServicesPermissionID in the
		///		SubCustAccountPermission table. Table SubCustAccount is related to table CustServicesPermission
		///		through the (M:N) relationship defined in the SubCustAccountPermission table.
		/// </summary>
		/// <param name="_custServicesPermissionId"></param>
		/// <returns>Returns a typed collection of SubCustAccount objects.</returns>
		public TList<SubCustAccount> GetByCustServicesPermissionIdFromSubCustAccountPermission(System.Int32 _custServicesPermissionId)
		{
			int count = -1;
			return GetByCustServicesPermissionIdFromSubCustAccountPermission(null,_custServicesPermissionId, 0, int.MaxValue, out count);
			
		}
		
		/// <summary>
		///		Gets AccountManager.Entities.SubCustAccount objects from the datasource by CustServicesPermissionID in the
		///		SubCustAccountPermission table. Table SubCustAccount is related to table CustServicesPermission
		///		through the (M:N) relationship defined in the SubCustAccountPermission table.
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_custServicesPermissionId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a TList of SubCustAccount objects.</returns>
		public TList<SubCustAccount> GetByCustServicesPermissionIdFromSubCustAccountPermission(System.Int32 _custServicesPermissionId, int start, int pageLength)
		{
			int count = -1;
			return GetByCustServicesPermissionIdFromSubCustAccountPermission(null, _custServicesPermissionId, start, pageLength, out count);
		}
		
		/// <summary>
		///		Gets SubCustAccount objects from the datasource by CustServicesPermissionID in the
		///		SubCustAccountPermission table. Table SubCustAccount is related to table CustServicesPermission
		///		through the (M:N) relationship defined in the SubCustAccountPermission table.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_custServicesPermissionId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of SubCustAccount objects.</returns>
		public TList<SubCustAccount> GetByCustServicesPermissionIdFromSubCustAccountPermission(TransactionManager transactionManager, System.Int32 _custServicesPermissionId)
		{
			int count = -1;
			return GetByCustServicesPermissionIdFromSubCustAccountPermission(transactionManager, _custServicesPermissionId, 0, int.MaxValue, out count);
		}
		
		
		/// <summary>
		///		Gets SubCustAccount objects from the datasource by CustServicesPermissionID in the
		///		SubCustAccountPermission table. Table SubCustAccount is related to table CustServicesPermission
		///		through the (M:N) relationship defined in the SubCustAccountPermission table.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_custServicesPermissionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of SubCustAccount objects.</returns>
		public TList<SubCustAccount> GetByCustServicesPermissionIdFromSubCustAccountPermission(TransactionManager transactionManager, System.Int32 _custServicesPermissionId,int start, int pageLength)
		{
			int count = -1;
			return GetByCustServicesPermissionIdFromSubCustAccountPermission(transactionManager, _custServicesPermissionId, start, pageLength, out count);
		}
		
		/// <summary>
		///		Gets SubCustAccount objects from the datasource by CustServicesPermissionID in the
		///		SubCustAccountPermission table. Table SubCustAccount is related to table CustServicesPermission
		///		through the (M:N) relationship defined in the SubCustAccountPermission table.
		/// </summary>
		/// <param name="_custServicesPermissionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of SubCustAccount objects.</returns>
		public TList<SubCustAccount> GetByCustServicesPermissionIdFromSubCustAccountPermission(System.Int32 _custServicesPermissionId,int start, int pageLength, out int count)
		{
			
			return GetByCustServicesPermissionIdFromSubCustAccountPermission(null, _custServicesPermissionId, start, pageLength, out count);
		}


		/// <summary>
		///		Gets SubCustAccount objects from the datasource by CustServicesPermissionID in the
		///		SubCustAccountPermission table. Table SubCustAccount is related to table CustServicesPermission
		///		through the (M:N) relationship defined in the SubCustAccountPermission table.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <param name="_custServicesPermissionId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a TList of SubCustAccount objects.</returns>
		public abstract TList<SubCustAccount> GetByCustServicesPermissionIdFromSubCustAccountPermission(TransactionManager transactionManager,System.Int32 _custServicesPermissionId, int start, int pageLength, out int count);
		
		#endregion GetByCustServicesPermissionIdFromSubCustAccountPermission
		
		#endregion	
		
		#region Delete Methods

		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager">A <see cref="TransactionManager"/> object.</param>
		/// <param name="key">The unique identifier of the row to delete.</param>
		/// <returns>Returns true if operation suceeded.</returns>
		public override bool Delete(TransactionManager transactionManager, AccountManager.Entities.SubCustAccountKey key)
		{
			return Delete(transactionManager, key.SubCustAccountId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_subCustAccountId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.String _subCustAccountId)
		{
			return Delete(null, _subCustAccountId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_subCustAccountId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.String _subCustAccountId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_SubCustAccount_MainCustAccount key.
		///		FK_SubCustAccount_MainCustAccount Description: 
		/// </summary>
		/// <param name="_mainCustAccountId"></param>
		/// <returns>Returns a typed collection of AccountManager.Entities.SubCustAccount objects.</returns>
		public TList<SubCustAccount> GetByMainCustAccountId(System.String _mainCustAccountId)
		{
			int count = -1;
			return GetByMainCustAccountId(_mainCustAccountId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_SubCustAccount_MainCustAccount key.
		///		FK_SubCustAccount_MainCustAccount Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_mainCustAccountId"></param>
		/// <returns>Returns a typed collection of AccountManager.Entities.SubCustAccount objects.</returns>
		/// <remarks></remarks>
		public TList<SubCustAccount> GetByMainCustAccountId(TransactionManager transactionManager, System.String _mainCustAccountId)
		{
			int count = -1;
			return GetByMainCustAccountId(transactionManager, _mainCustAccountId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_SubCustAccount_MainCustAccount key.
		///		FK_SubCustAccount_MainCustAccount Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_mainCustAccountId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AccountManager.Entities.SubCustAccount objects.</returns>
		public TList<SubCustAccount> GetByMainCustAccountId(TransactionManager transactionManager, System.String _mainCustAccountId, int start, int pageLength)
		{
			int count = -1;
			return GetByMainCustAccountId(transactionManager, _mainCustAccountId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_SubCustAccount_MainCustAccount key.
		///		fkSubCustAccountMainCustAccount Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_mainCustAccountId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AccountManager.Entities.SubCustAccount objects.</returns>
		public TList<SubCustAccount> GetByMainCustAccountId(System.String _mainCustAccountId, int start, int pageLength)
		{
			int count =  -1;
			return GetByMainCustAccountId(null, _mainCustAccountId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_SubCustAccount_MainCustAccount key.
		///		fkSubCustAccountMainCustAccount Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_mainCustAccountId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AccountManager.Entities.SubCustAccount objects.</returns>
		public TList<SubCustAccount> GetByMainCustAccountId(System.String _mainCustAccountId, int start, int pageLength,out int count)
		{
			return GetByMainCustAccountId(null, _mainCustAccountId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_SubCustAccount_MainCustAccount key.
		///		FK_SubCustAccount_MainCustAccount Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_mainCustAccountId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of AccountManager.Entities.SubCustAccount objects.</returns>
		public abstract TList<SubCustAccount> GetByMainCustAccountId(TransactionManager transactionManager, System.String _mainCustAccountId, int start, int pageLength, out int count);
		
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
		public override AccountManager.Entities.SubCustAccount Get(TransactionManager transactionManager, AccountManager.Entities.SubCustAccountKey key, int start, int pageLength)
		{
			return GetBySubCustAccountId(transactionManager, key.SubCustAccountId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_SubCustAccount index.
		/// </summary>
		/// <param name="_subCustAccountId"></param>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.SubCustAccount"/> class.</returns>
		public AccountManager.Entities.SubCustAccount GetBySubCustAccountId(System.String _subCustAccountId)
		{
			int count = -1;
			return GetBySubCustAccountId(null,_subCustAccountId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_SubCustAccount index.
		/// </summary>
		/// <param name="_subCustAccountId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.SubCustAccount"/> class.</returns>
		public AccountManager.Entities.SubCustAccount GetBySubCustAccountId(System.String _subCustAccountId, int start, int pageLength)
		{
			int count = -1;
			return GetBySubCustAccountId(null, _subCustAccountId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_SubCustAccount index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_subCustAccountId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.SubCustAccount"/> class.</returns>
		public AccountManager.Entities.SubCustAccount GetBySubCustAccountId(TransactionManager transactionManager, System.String _subCustAccountId)
		{
			int count = -1;
			return GetBySubCustAccountId(transactionManager, _subCustAccountId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_SubCustAccount index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_subCustAccountId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.SubCustAccount"/> class.</returns>
		public AccountManager.Entities.SubCustAccount GetBySubCustAccountId(TransactionManager transactionManager, System.String _subCustAccountId, int start, int pageLength)
		{
			int count = -1;
			return GetBySubCustAccountId(transactionManager, _subCustAccountId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_SubCustAccount index.
		/// </summary>
		/// <param name="_subCustAccountId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.SubCustAccount"/> class.</returns>
		public AccountManager.Entities.SubCustAccount GetBySubCustAccountId(System.String _subCustAccountId, int start, int pageLength, out int count)
		{
			return GetBySubCustAccountId(null, _subCustAccountId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_SubCustAccount index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_subCustAccountId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.SubCustAccount"/> class.</returns>
		public abstract AccountManager.Entities.SubCustAccount GetBySubCustAccountId(TransactionManager transactionManager, System.String _subCustAccountId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;SubCustAccount&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;SubCustAccount&gt;"/></returns>
		public static TList<SubCustAccount> Fill(IDataReader reader, TList<SubCustAccount> rows, int start, int pageLength)
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
				
				AccountManager.Entities.SubCustAccount c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("SubCustAccount")
					.Append("|").Append((System.String)reader[((int)SubCustAccountColumn.SubCustAccountId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<SubCustAccount>(
					key.ToString(), // EntityTrackingKey
					"SubCustAccount",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new AccountManager.Entities.SubCustAccount();
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
					c.SubCustAccountId = (System.String)reader[((int)SubCustAccountColumn.SubCustAccountId - 1)];
					c.OriginalSubCustAccountId = c.SubCustAccountId;
					c.Name = (reader.IsDBNull(((int)SubCustAccountColumn.Name - 1)))?null:(System.String)reader[((int)SubCustAccountColumn.Name - 1)];
					c.Actived = (reader.IsDBNull(((int)SubCustAccountColumn.Actived - 1)))?null:(System.Boolean?)reader[((int)SubCustAccountColumn.Actived - 1)];
					c.LockAccountReason = (reader.IsDBNull(((int)SubCustAccountColumn.LockAccountReason - 1)))?null:(System.Int16?)reader[((int)SubCustAccountColumn.LockAccountReason - 1)];
					c.MainCustAccountId = (reader.IsDBNull(((int)SubCustAccountColumn.MainCustAccountId - 1)))?null:(System.String)reader[((int)SubCustAccountColumn.MainCustAccountId - 1)];
					c.CreatedDate = (reader.IsDBNull(((int)SubCustAccountColumn.CreatedDate - 1)))?null:(System.DateTime?)reader[((int)SubCustAccountColumn.CreatedDate - 1)];
					c.CreatedUser = (reader.IsDBNull(((int)SubCustAccountColumn.CreatedUser - 1)))?null:(System.String)reader[((int)SubCustAccountColumn.CreatedUser - 1)];
					c.UpdatedDate = (reader.IsDBNull(((int)SubCustAccountColumn.UpdatedDate - 1)))?null:(System.DateTime?)reader[((int)SubCustAccountColumn.UpdatedDate - 1)];
					c.UpdatedUser = (reader.IsDBNull(((int)SubCustAccountColumn.UpdatedUser - 1)))?null:(System.String)reader[((int)SubCustAccountColumn.UpdatedUser - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="AccountManager.Entities.SubCustAccount"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AccountManager.Entities.SubCustAccount"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AccountManager.Entities.SubCustAccount entity)
		{
			if (!reader.Read()) return;
			
			entity.SubCustAccountId = (System.String)reader[((int)SubCustAccountColumn.SubCustAccountId - 1)];
			entity.OriginalSubCustAccountId = (System.String)reader["SubCustAccountID"];
			entity.Name = (reader.IsDBNull(((int)SubCustAccountColumn.Name - 1)))?null:(System.String)reader[((int)SubCustAccountColumn.Name - 1)];
			entity.Actived = (reader.IsDBNull(((int)SubCustAccountColumn.Actived - 1)))?null:(System.Boolean?)reader[((int)SubCustAccountColumn.Actived - 1)];
			entity.LockAccountReason = (reader.IsDBNull(((int)SubCustAccountColumn.LockAccountReason - 1)))?null:(System.Int16?)reader[((int)SubCustAccountColumn.LockAccountReason - 1)];
			entity.MainCustAccountId = (reader.IsDBNull(((int)SubCustAccountColumn.MainCustAccountId - 1)))?null:(System.String)reader[((int)SubCustAccountColumn.MainCustAccountId - 1)];
			entity.CreatedDate = (reader.IsDBNull(((int)SubCustAccountColumn.CreatedDate - 1)))?null:(System.DateTime?)reader[((int)SubCustAccountColumn.CreatedDate - 1)];
			entity.CreatedUser = (reader.IsDBNull(((int)SubCustAccountColumn.CreatedUser - 1)))?null:(System.String)reader[((int)SubCustAccountColumn.CreatedUser - 1)];
			entity.UpdatedDate = (reader.IsDBNull(((int)SubCustAccountColumn.UpdatedDate - 1)))?null:(System.DateTime?)reader[((int)SubCustAccountColumn.UpdatedDate - 1)];
			entity.UpdatedUser = (reader.IsDBNull(((int)SubCustAccountColumn.UpdatedUser - 1)))?null:(System.String)reader[((int)SubCustAccountColumn.UpdatedUser - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="AccountManager.Entities.SubCustAccount"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AccountManager.Entities.SubCustAccount"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AccountManager.Entities.SubCustAccount entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.SubCustAccountId = (System.String)dataRow["SubCustAccountID"];
			entity.OriginalSubCustAccountId = (System.String)dataRow["SubCustAccountID"];
			entity.Name = Convert.IsDBNull(dataRow["Name"]) ? null : (System.String)dataRow["Name"];
			entity.Actived = Convert.IsDBNull(dataRow["Actived"]) ? null : (System.Boolean?)dataRow["Actived"];
			entity.LockAccountReason = Convert.IsDBNull(dataRow["LockAccountReason"]) ? null : (System.Int16?)dataRow["LockAccountReason"];
			entity.MainCustAccountId = Convert.IsDBNull(dataRow["MainCustAccountID"]) ? null : (System.String)dataRow["MainCustAccountID"];
			entity.CreatedDate = Convert.IsDBNull(dataRow["CreatedDate"]) ? null : (System.DateTime?)dataRow["CreatedDate"];
			entity.CreatedUser = Convert.IsDBNull(dataRow["CreatedUser"]) ? null : (System.String)dataRow["CreatedUser"];
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
		/// <param name="entity">The <see cref="AccountManager.Entities.SubCustAccount"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AccountManager.Entities.SubCustAccount Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AccountManager.Entities.SubCustAccount entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region MainCustAccountIdSource	
			if (CanDeepLoad(entity, "MainCustAccount|MainCustAccountIdSource", deepLoadType, innerList) 
				&& entity.MainCustAccountIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.MainCustAccountId ?? string.Empty);
				MainCustAccount tmpEntity = EntityManager.LocateEntity<MainCustAccount>(EntityLocator.ConstructKeyFromPkItems(typeof(MainCustAccount), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.MainCustAccountIdSource = tmpEntity;
				else
					entity.MainCustAccountIdSource = DataRepository.MainCustAccountProvider.GetByMainCustAccountId(transactionManager, (entity.MainCustAccountId ?? string.Empty));		
				
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
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetBySubCustAccountId methods when available
			
			#region BuyRightCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<BuyRight>|BuyRightCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'BuyRightCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.BuyRightCollection = DataRepository.BuyRightProvider.GetBySubCustAccountId(transactionManager, entity.SubCustAccountId);

				if (deep && entity.BuyRightCollection.Count > 0)
				{
					deepHandles.Add("BuyRightCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<BuyRight>) DataRepository.BuyRightProvider.DeepLoad,
						new object[] { transactionManager, entity.BuyRightCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region CustomerActionHistoryCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<CustomerActionHistory>|CustomerActionHistoryCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'CustomerActionHistoryCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.CustomerActionHistoryCollection = DataRepository.CustomerActionHistoryProvider.GetBySubCustAccountId(transactionManager, entity.SubCustAccountId);

				if (deep && entity.CustomerActionHistoryCollection.Count > 0)
				{
					deepHandles.Add("CustomerActionHistoryCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<CustomerActionHistory>) DataRepository.CustomerActionHistoryProvider.DeepLoad,
						new object[] { transactionManager, entity.CustomerActionHistoryCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region SubCustAccountPermissionCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<SubCustAccountPermission>|SubCustAccountPermissionCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'SubCustAccountPermissionCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.SubCustAccountPermissionCollection = DataRepository.SubCustAccountPermissionProvider.GetBySubCustAccountId(transactionManager, entity.SubCustAccountId);

				if (deep && entity.SubCustAccountPermissionCollection.Count > 0)
				{
					deepHandles.Add("SubCustAccountPermissionCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<SubCustAccountPermission>) DataRepository.SubCustAccountPermissionProvider.DeepLoad,
						new object[] { transactionManager, entity.SubCustAccountPermissionCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region CustServicesPermissionIdCustServicesPermissionCollection_From_SubCustAccountPermission
			// RelationshipType.ManyToMany
			if (CanDeepLoad(entity, "List<CustServicesPermission>|CustServicesPermissionIdCustServicesPermissionCollection_From_SubCustAccountPermission", deepLoadType, innerList))
			{
				entity.CustServicesPermissionIdCustServicesPermissionCollection_From_SubCustAccountPermission = DataRepository.CustServicesPermissionProvider.GetBySubCustAccountIdFromSubCustAccountPermission(transactionManager, entity.SubCustAccountId);			 
		
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'CustServicesPermissionIdCustServicesPermissionCollection_From_SubCustAccountPermission' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.CustServicesPermissionIdCustServicesPermissionCollection_From_SubCustAccountPermission != null)
				{
					deepHandles.Add("CustServicesPermissionIdCustServicesPermissionCollection_From_SubCustAccountPermission",
						new KeyValuePair<Delegate, object>((DeepLoadHandle< CustServicesPermission >) DataRepository.CustServicesPermissionProvider.DeepLoad,
						new object[] { transactionManager, entity.CustServicesPermissionIdCustServicesPermissionCollection_From_SubCustAccountPermission, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the AccountManager.Entities.SubCustAccount object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AccountManager.Entities.SubCustAccount instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AccountManager.Entities.SubCustAccount Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AccountManager.Entities.SubCustAccount entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
			#endregion Composite Parent Properties

			// Save Root Entity through Provider
			if (!entity.IsDeleted)
				this.Save(transactionManager, entity);
			
			//used to hold DeepSave method delegates and fire after all the local children have been saved.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();

			#region CustServicesPermissionIdCustServicesPermissionCollection_From_SubCustAccountPermission>
			if (CanDeepSave(entity.CustServicesPermissionIdCustServicesPermissionCollection_From_SubCustAccountPermission, "List<CustServicesPermission>|CustServicesPermissionIdCustServicesPermissionCollection_From_SubCustAccountPermission", deepSaveType, innerList))
			{
				if (entity.CustServicesPermissionIdCustServicesPermissionCollection_From_SubCustAccountPermission.Count > 0 || entity.CustServicesPermissionIdCustServicesPermissionCollection_From_SubCustAccountPermission.DeletedItems.Count > 0)
				{
					DataRepository.CustServicesPermissionProvider.Save(transactionManager, entity.CustServicesPermissionIdCustServicesPermissionCollection_From_SubCustAccountPermission); 
					deepHandles.Add("CustServicesPermissionIdCustServicesPermissionCollection_From_SubCustAccountPermission",
						new KeyValuePair<Delegate, object>((DeepSaveHandle<CustServicesPermission>) DataRepository.CustServicesPermissionProvider.DeepSave,
						new object[] { transactionManager, entity.CustServicesPermissionIdCustServicesPermissionCollection_From_SubCustAccountPermission, deepSaveType, childTypes, innerList }
					));
				}
			}
			#endregion 
	
			#region List<BuyRight>
				if (CanDeepSave(entity.BuyRightCollection, "List<BuyRight>|BuyRightCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(BuyRight child in entity.BuyRightCollection)
					{
						if(child.SubCustAccountIdSource != null)
						{
							child.SubCustAccountId = child.SubCustAccountIdSource.SubCustAccountId;
						}
						else
						{
							child.SubCustAccountId = entity.SubCustAccountId;
						}

					}

					if (entity.BuyRightCollection.Count > 0 || entity.BuyRightCollection.DeletedItems.Count > 0)
					{
						//DataRepository.BuyRightProvider.Save(transactionManager, entity.BuyRightCollection);
						
						deepHandles.Add("BuyRightCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< BuyRight >) DataRepository.BuyRightProvider.DeepSave,
							new object[] { transactionManager, entity.BuyRightCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
	
			#region List<CustomerActionHistory>
				if (CanDeepSave(entity.CustomerActionHistoryCollection, "List<CustomerActionHistory>|CustomerActionHistoryCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(CustomerActionHistory child in entity.CustomerActionHistoryCollection)
					{
						if(child.SubCustAccountIdSource != null)
						{
							child.SubCustAccountId = child.SubCustAccountIdSource.SubCustAccountId;
						}
						else
						{
							child.SubCustAccountId = entity.SubCustAccountId;
						}

					}

					if (entity.CustomerActionHistoryCollection.Count > 0 || entity.CustomerActionHistoryCollection.DeletedItems.Count > 0)
					{
						//DataRepository.CustomerActionHistoryProvider.Save(transactionManager, entity.CustomerActionHistoryCollection);
						
						deepHandles.Add("CustomerActionHistoryCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< CustomerActionHistory >) DataRepository.CustomerActionHistoryProvider.DeepSave,
							new object[] { transactionManager, entity.CustomerActionHistoryCollection, deepSaveType, childTypes, innerList }
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
						if(child.SubCustAccountIdSource != null)
						{
								child.SubCustAccountId = child.SubCustAccountIdSource.SubCustAccountId;
						}

						if(child.CustServicesPermissionIdSource != null)
						{
								child.CustServicesPermissionId = child.CustServicesPermissionIdSource.CustServicesPermissionId;
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
	
	#region SubCustAccountChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>AccountManager.Entities.SubCustAccount</c>
	///</summary>
	public enum SubCustAccountChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>MainCustAccount</c> at MainCustAccountIdSource
		///</summary>
		[ChildEntityType(typeof(MainCustAccount))]
		MainCustAccount,
	
		///<summary>
		/// Collection of <c>SubCustAccount</c> as OneToMany for BuyRightCollection
		///</summary>
		[ChildEntityType(typeof(TList<BuyRight>))]
		BuyRightCollection,

		///<summary>
		/// Collection of <c>SubCustAccount</c> as OneToMany for CustomerActionHistoryCollection
		///</summary>
		[ChildEntityType(typeof(TList<CustomerActionHistory>))]
		CustomerActionHistoryCollection,

		///<summary>
		/// Collection of <c>SubCustAccount</c> as OneToMany for SubCustAccountPermissionCollection
		///</summary>
		[ChildEntityType(typeof(TList<SubCustAccountPermission>))]
		SubCustAccountPermissionCollection,

		///<summary>
		/// Collection of <c>SubCustAccount</c> as ManyToMany for CustServicesPermissionCollection_From_SubCustAccountPermission
		///</summary>
		[ChildEntityType(typeof(TList<CustServicesPermission>))]
		CustServicesPermissionIdCustServicesPermissionCollection_From_SubCustAccountPermission,
	}
	
	#endregion SubCustAccountChildEntityTypes
	
	#region SubCustAccountFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;SubCustAccountColumn&gt;"/> class
	/// that is used exclusively with a <see cref="SubCustAccount"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class SubCustAccountFilterBuilder : SqlFilterBuilder<SubCustAccountColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SubCustAccountFilterBuilder class.
		/// </summary>
		public SubCustAccountFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the SubCustAccountFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public SubCustAccountFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the SubCustAccountFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public SubCustAccountFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion SubCustAccountFilterBuilder
	
	#region SubCustAccountParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;SubCustAccountColumn&gt;"/> class
	/// that is used exclusively with a <see cref="SubCustAccount"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class SubCustAccountParameterBuilder : ParameterizedSqlFilterBuilder<SubCustAccountColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SubCustAccountParameterBuilder class.
		/// </summary>
		public SubCustAccountParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the SubCustAccountParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public SubCustAccountParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the SubCustAccountParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public SubCustAccountParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion SubCustAccountParameterBuilder
	
	#region SubCustAccountSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;SubCustAccountColumn&gt;"/> class
	/// that is used exclusively with a <see cref="SubCustAccount"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class SubCustAccountSortBuilder : SqlSortBuilder<SubCustAccountColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SubCustAccountSqlSortBuilder class.
		/// </summary>
		public SubCustAccountSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion SubCustAccountSortBuilder
	
} // end namespace
