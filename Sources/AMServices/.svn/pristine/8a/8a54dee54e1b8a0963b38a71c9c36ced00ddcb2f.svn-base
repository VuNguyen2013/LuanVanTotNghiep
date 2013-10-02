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
	/// This class is the base class for any <see cref="BuyRightProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class BuyRightProviderBaseCore : EntityProviderBase<AccountManager.Entities.BuyRight, AccountManager.Entities.BuyRightKey>
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
		public override bool Delete(TransactionManager transactionManager, AccountManager.Entities.BuyRightKey key)
		{
			return Delete(transactionManager, key.Id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_id">Auto increase id. Primary Key.</param>
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
		/// <param name="_id">Auto increase id. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Int64 _id);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_BuyRight_SubCustAccount key.
		///		FK_BuyRight_SubCustAccount Description: 
		/// </summary>
		/// <param name="_subCustAccountId"></param>
		/// <returns>Returns a typed collection of AccountManager.Entities.BuyRight objects.</returns>
		public TList<BuyRight> GetBySubCustAccountId(System.String _subCustAccountId)
		{
			int count = -1;
			return GetBySubCustAccountId(_subCustAccountId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_BuyRight_SubCustAccount key.
		///		FK_BuyRight_SubCustAccount Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_subCustAccountId"></param>
		/// <returns>Returns a typed collection of AccountManager.Entities.BuyRight objects.</returns>
		/// <remarks></remarks>
		public TList<BuyRight> GetBySubCustAccountId(TransactionManager transactionManager, System.String _subCustAccountId)
		{
			int count = -1;
			return GetBySubCustAccountId(transactionManager, _subCustAccountId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_BuyRight_SubCustAccount key.
		///		FK_BuyRight_SubCustAccount Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_subCustAccountId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AccountManager.Entities.BuyRight objects.</returns>
		public TList<BuyRight> GetBySubCustAccountId(TransactionManager transactionManager, System.String _subCustAccountId, int start, int pageLength)
		{
			int count = -1;
			return GetBySubCustAccountId(transactionManager, _subCustAccountId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_BuyRight_SubCustAccount key.
		///		fkBuyRightSubCustAccount Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_subCustAccountId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AccountManager.Entities.BuyRight objects.</returns>
		public TList<BuyRight> GetBySubCustAccountId(System.String _subCustAccountId, int start, int pageLength)
		{
			int count =  -1;
			return GetBySubCustAccountId(null, _subCustAccountId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_BuyRight_SubCustAccount key.
		///		fkBuyRightSubCustAccount Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_subCustAccountId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AccountManager.Entities.BuyRight objects.</returns>
		public TList<BuyRight> GetBySubCustAccountId(System.String _subCustAccountId, int start, int pageLength,out int count)
		{
			return GetBySubCustAccountId(null, _subCustAccountId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_BuyRight_SubCustAccount key.
		///		FK_BuyRight_SubCustAccount Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_subCustAccountId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of AccountManager.Entities.BuyRight objects.</returns>
		public abstract TList<BuyRight> GetBySubCustAccountId(TransactionManager transactionManager, System.String _subCustAccountId, int start, int pageLength, out int count);
		
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
		public override AccountManager.Entities.BuyRight Get(TransactionManager transactionManager, AccountManager.Entities.BuyRightKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_BUYRIGHT index.
		/// </summary>
		/// <param name="_id">Auto increase id</param>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.BuyRight"/> class.</returns>
		public AccountManager.Entities.BuyRight GetById(System.Int64 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_BUYRIGHT index.
		/// </summary>
		/// <param name="_id">Auto increase id</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.BuyRight"/> class.</returns>
		public AccountManager.Entities.BuyRight GetById(System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_BUYRIGHT index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">Auto increase id</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.BuyRight"/> class.</returns>
		public AccountManager.Entities.BuyRight GetById(TransactionManager transactionManager, System.Int64 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_BUYRIGHT index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">Auto increase id</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.BuyRight"/> class.</returns>
		public AccountManager.Entities.BuyRight GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_BUYRIGHT index.
		/// </summary>
		/// <param name="_id">Auto increase id</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.BuyRight"/> class.</returns>
		public AccountManager.Entities.BuyRight GetById(System.Int64 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_BUYRIGHT index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">Auto increase id</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.BuyRight"/> class.</returns>
		public abstract AccountManager.Entities.BuyRight GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;BuyRight&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;BuyRight&gt;"/></returns>
		public static TList<BuyRight> Fill(IDataReader reader, TList<BuyRight> rows, int start, int pageLength)
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
				
				AccountManager.Entities.BuyRight c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("BuyRight")
					.Append("|").Append((System.Int64)reader[((int)BuyRightColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<BuyRight>(
					key.ToString(), // EntityTrackingKey
					"BuyRight",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new AccountManager.Entities.BuyRight();
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
					c.Id = (System.Int64)reader[((int)BuyRightColumn.Id - 1)];
					c.SubCustAccountId = (reader.IsDBNull(((int)BuyRightColumn.SubCustAccountId - 1)))?null:(System.String)reader[((int)BuyRightColumn.SubCustAccountId - 1)];
					c.SecSymbol = (System.String)reader[((int)BuyRightColumn.SecSymbol - 1)];
					c.Market = (System.String)reader[((int)BuyRightColumn.Market - 1)];
					c.ExecDate = (System.DateTime)reader[((int)BuyRightColumn.ExecDate - 1)];
					c.OwningVol = (System.Int64)reader[((int)BuyRightColumn.OwningVol - 1)];
					c.AllowedVol = (System.Int64)reader[((int)BuyRightColumn.AllowedVol - 1)];
					c.RegisteredVol = (System.Int64)reader[((int)BuyRightColumn.RegisteredVol - 1)];
					c.Right = (System.Decimal)reader[((int)BuyRightColumn.Right - 1)];
					c.RateRight = (System.Decimal)reader[((int)BuyRightColumn.RateRight - 1)];
					c.Price = (System.Decimal)reader[((int)BuyRightColumn.Price - 1)];
					c.BeginDateToRegister = (reader.IsDBNull(((int)BuyRightColumn.BeginDateToRegister - 1)))?null:(System.DateTime?)reader[((int)BuyRightColumn.BeginDateToRegister - 1)];
					c.EndDateToRegister = (reader.IsDBNull(((int)BuyRightColumn.EndDateToRegister - 1)))?null:(System.DateTime?)reader[((int)BuyRightColumn.EndDateToRegister - 1)];
					c.BeginDateToTransfer = (reader.IsDBNull(((int)BuyRightColumn.BeginDateToTransfer - 1)))?null:(System.DateTime?)reader[((int)BuyRightColumn.BeginDateToTransfer - 1)];
					c.EndDateToTransfer = (reader.IsDBNull(((int)BuyRightColumn.EndDateToTransfer - 1)))?null:(System.DateTime?)reader[((int)BuyRightColumn.EndDateToTransfer - 1)];
					c.ReceivedDate = (reader.IsDBNull(((int)BuyRightColumn.ReceivedDate - 1)))?null:(System.DateTime?)reader[((int)BuyRightColumn.ReceivedDate - 1)];
					c.Note = (reader.IsDBNull(((int)BuyRightColumn.Note - 1)))?null:(System.String)reader[((int)BuyRightColumn.Note - 1)];
					c.CreatedDate = (System.DateTime)reader[((int)BuyRightColumn.CreatedDate - 1)];
					c.CreatedUser = (System.String)reader[((int)BuyRightColumn.CreatedUser - 1)];
					c.UpdatedDate = (reader.IsDBNull(((int)BuyRightColumn.UpdatedDate - 1)))?null:(System.DateTime?)reader[((int)BuyRightColumn.UpdatedDate - 1)];
					c.UpdatedUser = (reader.IsDBNull(((int)BuyRightColumn.UpdatedUser - 1)))?null:(System.String)reader[((int)BuyRightColumn.UpdatedUser - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="AccountManager.Entities.BuyRight"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AccountManager.Entities.BuyRight"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AccountManager.Entities.BuyRight entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int64)reader[((int)BuyRightColumn.Id - 1)];
			entity.SubCustAccountId = (reader.IsDBNull(((int)BuyRightColumn.SubCustAccountId - 1)))?null:(System.String)reader[((int)BuyRightColumn.SubCustAccountId - 1)];
			entity.SecSymbol = (System.String)reader[((int)BuyRightColumn.SecSymbol - 1)];
			entity.Market = (System.String)reader[((int)BuyRightColumn.Market - 1)];
			entity.ExecDate = (System.DateTime)reader[((int)BuyRightColumn.ExecDate - 1)];
			entity.OwningVol = (System.Int64)reader[((int)BuyRightColumn.OwningVol - 1)];
			entity.AllowedVol = (System.Int64)reader[((int)BuyRightColumn.AllowedVol - 1)];
			entity.RegisteredVol = (System.Int64)reader[((int)BuyRightColumn.RegisteredVol - 1)];
			entity.Right = (System.Decimal)reader[((int)BuyRightColumn.Right - 1)];
			entity.RateRight = (System.Decimal)reader[((int)BuyRightColumn.RateRight - 1)];
			entity.Price = (System.Decimal)reader[((int)BuyRightColumn.Price - 1)];
			entity.BeginDateToRegister = (reader.IsDBNull(((int)BuyRightColumn.BeginDateToRegister - 1)))?null:(System.DateTime?)reader[((int)BuyRightColumn.BeginDateToRegister - 1)];
			entity.EndDateToRegister = (reader.IsDBNull(((int)BuyRightColumn.EndDateToRegister - 1)))?null:(System.DateTime?)reader[((int)BuyRightColumn.EndDateToRegister - 1)];
			entity.BeginDateToTransfer = (reader.IsDBNull(((int)BuyRightColumn.BeginDateToTransfer - 1)))?null:(System.DateTime?)reader[((int)BuyRightColumn.BeginDateToTransfer - 1)];
			entity.EndDateToTransfer = (reader.IsDBNull(((int)BuyRightColumn.EndDateToTransfer - 1)))?null:(System.DateTime?)reader[((int)BuyRightColumn.EndDateToTransfer - 1)];
			entity.ReceivedDate = (reader.IsDBNull(((int)BuyRightColumn.ReceivedDate - 1)))?null:(System.DateTime?)reader[((int)BuyRightColumn.ReceivedDate - 1)];
			entity.Note = (reader.IsDBNull(((int)BuyRightColumn.Note - 1)))?null:(System.String)reader[((int)BuyRightColumn.Note - 1)];
			entity.CreatedDate = (System.DateTime)reader[((int)BuyRightColumn.CreatedDate - 1)];
			entity.CreatedUser = (System.String)reader[((int)BuyRightColumn.CreatedUser - 1)];
			entity.UpdatedDate = (reader.IsDBNull(((int)BuyRightColumn.UpdatedDate - 1)))?null:(System.DateTime?)reader[((int)BuyRightColumn.UpdatedDate - 1)];
			entity.UpdatedUser = (reader.IsDBNull(((int)BuyRightColumn.UpdatedUser - 1)))?null:(System.String)reader[((int)BuyRightColumn.UpdatedUser - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="AccountManager.Entities.BuyRight"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AccountManager.Entities.BuyRight"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AccountManager.Entities.BuyRight entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int64)dataRow["Id"];
			entity.SubCustAccountId = Convert.IsDBNull(dataRow["SubCustAccountID"]) ? null : (System.String)dataRow["SubCustAccountID"];
			entity.SecSymbol = (System.String)dataRow["SecSymbol"];
			entity.Market = (System.String)dataRow["Market"];
			entity.ExecDate = (System.DateTime)dataRow["ExecDate"];
			entity.OwningVol = (System.Int64)dataRow["OwningVol"];
			entity.AllowedVol = (System.Int64)dataRow["AllowedVol"];
			entity.RegisteredVol = (System.Int64)dataRow["RegisteredVol"];
			entity.Right = (System.Decimal)dataRow["Right"];
			entity.RateRight = (System.Decimal)dataRow["RateRight"];
			entity.Price = (System.Decimal)dataRow["Price"];
			entity.BeginDateToRegister = Convert.IsDBNull(dataRow["BeginDateToRegister"]) ? null : (System.DateTime?)dataRow["BeginDateToRegister"];
			entity.EndDateToRegister = Convert.IsDBNull(dataRow["EndDateToRegister"]) ? null : (System.DateTime?)dataRow["EndDateToRegister"];
			entity.BeginDateToTransfer = Convert.IsDBNull(dataRow["BeginDateToTransfer"]) ? null : (System.DateTime?)dataRow["BeginDateToTransfer"];
			entity.EndDateToTransfer = Convert.IsDBNull(dataRow["EndDateToTransfer"]) ? null : (System.DateTime?)dataRow["EndDateToTransfer"];
			entity.ReceivedDate = Convert.IsDBNull(dataRow["ReceivedDate"]) ? null : (System.DateTime?)dataRow["ReceivedDate"];
			entity.Note = Convert.IsDBNull(dataRow["Note"]) ? null : (System.String)dataRow["Note"];
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
		/// <param name="entity">The <see cref="AccountManager.Entities.BuyRight"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AccountManager.Entities.BuyRight Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AccountManager.Entities.BuyRight entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

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
		/// Deep Save the entire object graph of the AccountManager.Entities.BuyRight object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AccountManager.Entities.BuyRight instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AccountManager.Entities.BuyRight Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AccountManager.Entities.BuyRight entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
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
	
	#region BuyRightChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>AccountManager.Entities.BuyRight</c>
	///</summary>
	public enum BuyRightChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>SubCustAccount</c> at SubCustAccountIdSource
		///</summary>
		[ChildEntityType(typeof(SubCustAccount))]
		SubCustAccount,
		}
	
	#endregion BuyRightChildEntityTypes
	
	#region BuyRightFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;BuyRightColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BuyRight"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BuyRightFilterBuilder : SqlFilterBuilder<BuyRightColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BuyRightFilterBuilder class.
		/// </summary>
		public BuyRightFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the BuyRightFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BuyRightFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BuyRightFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BuyRightFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BuyRightFilterBuilder
	
	#region BuyRightParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;BuyRightColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BuyRight"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BuyRightParameterBuilder : ParameterizedSqlFilterBuilder<BuyRightColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BuyRightParameterBuilder class.
		/// </summary>
		public BuyRightParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the BuyRightParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BuyRightParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BuyRightParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BuyRightParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BuyRightParameterBuilder
	
	#region BuyRightSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;BuyRightColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BuyRight"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class BuyRightSortBuilder : SqlSortBuilder<BuyRightColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BuyRightSqlSortBuilder class.
		/// </summary>
		public BuyRightSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion BuyRightSortBuilder
	
} // end namespace
