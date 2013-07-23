#region Using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

using ETradeOrders.Entities;
using ETradeOrders.DataAccess;

#endregion

namespace ETradeOrders.DataAccess.Bases
{	
	///<summary>
	/// This class is the base class for any <see cref="ConditionOrderProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class ConditionOrderProviderBaseCore : EntityProviderBase<ETradeOrders.Entities.ConditionOrder, ETradeOrders.Entities.ConditionOrderKey>
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
		public override bool Delete(TransactionManager transactionManager, ETradeOrders.Entities.ConditionOrderKey key)
		{
			return Delete(transactionManager, key.ConditionOrderId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_conditionOrderId">ConditionOrderID identifies ConditionOrder. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(long _conditionOrderId)
		{
			return Delete(null, _conditionOrderId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_conditionOrderId">ConditionOrderID identifies ConditionOrder. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, long _conditionOrderId);		
		
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
		public override ETradeOrders.Entities.ConditionOrder Get(TransactionManager transactionManager, ETradeOrders.Entities.ConditionOrderKey key, int start, int pageLength)
		{
			return GetByConditionOrderId(transactionManager, key.ConditionOrderId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_ConditionOrder index.
		/// </summary>
		/// <param name="_conditionOrderId">ConditionOrderID identifies ConditionOrder</param>
		/// <returns>Returns an instance of the <see cref="ETradeOrders.Entities.ConditionOrder"/> class.</returns>
		public ETradeOrders.Entities.ConditionOrder GetByConditionOrderId(long _conditionOrderId)
		{
			int count = -1;
			return GetByConditionOrderId(null,_conditionOrderId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_ConditionOrder index.
		/// </summary>
		/// <param name="_conditionOrderId">ConditionOrderID identifies ConditionOrder</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeOrders.Entities.ConditionOrder"/> class.</returns>
		public ETradeOrders.Entities.ConditionOrder GetByConditionOrderId(long _conditionOrderId, int start, int pageLength)
		{
			int count = -1;
			return GetByConditionOrderId(null, _conditionOrderId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_ConditionOrder index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_conditionOrderId">ConditionOrderID identifies ConditionOrder</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeOrders.Entities.ConditionOrder"/> class.</returns>
		public ETradeOrders.Entities.ConditionOrder GetByConditionOrderId(TransactionManager transactionManager, long _conditionOrderId)
		{
			int count = -1;
			return GetByConditionOrderId(transactionManager, _conditionOrderId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_ConditionOrder index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_conditionOrderId">ConditionOrderID identifies ConditionOrder</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeOrders.Entities.ConditionOrder"/> class.</returns>
		public ETradeOrders.Entities.ConditionOrder GetByConditionOrderId(TransactionManager transactionManager, long _conditionOrderId, int start, int pageLength)
		{
			int count = -1;
			return GetByConditionOrderId(transactionManager, _conditionOrderId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_ConditionOrder index.
		/// </summary>
		/// <param name="_conditionOrderId">ConditionOrderID identifies ConditionOrder</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeOrders.Entities.ConditionOrder"/> class.</returns>
		public ETradeOrders.Entities.ConditionOrder GetByConditionOrderId(long _conditionOrderId, int start, int pageLength, out int count)
		{
			return GetByConditionOrderId(null, _conditionOrderId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_ConditionOrder index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_conditionOrderId">ConditionOrderID identifies ConditionOrder</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ETradeOrders.Entities.ConditionOrder"/> class.</returns>
		public abstract ETradeOrders.Entities.ConditionOrder GetByConditionOrderId(TransactionManager transactionManager, long _conditionOrderId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#region _ConditionOrder_GetListTodayOrders 
		
		/// <summary>
		///	This method wrap the '_ConditionOrder_GetListTodayOrders' stored procedure. 
		/// </summary>
		/// <param name="market"> A <c>System.String</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;ConditionOrder&gt;"/> instance.</returns>
		public TList<ConditionOrder> GetListTodayOrders(System.String market)
		{
			return GetListTodayOrders(null, 0, int.MaxValue , market);
		}
		
		/// <summary>
		///	This method wrap the '_ConditionOrder_GetListTodayOrders' stored procedure. 
		/// </summary>
		/// <param name="market"> A <c>System.String</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;ConditionOrder&gt;"/> instance.</returns>
		public TList<ConditionOrder> GetListTodayOrders(int start, int pageLength, System.String market)
		{
			return GetListTodayOrders(null, start, pageLength , market);
		}
				
		/// <summary>
		///	This method wrap the '_ConditionOrder_GetListTodayOrders' stored procedure. 
		/// </summary>
		/// <param name="market"> A <c>System.String</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;ConditionOrder&gt;"/> instance.</returns>
		public TList<ConditionOrder> GetListTodayOrders(TransactionManager transactionManager, System.String market)
		{
			return GetListTodayOrders(transactionManager, 0, int.MaxValue , market);
		}
		
		/// <summary>
		///	This method wrap the '_ConditionOrder_GetListTodayOrders' stored procedure. 
		/// </summary>
		/// <param name="market"> A <c>System.String</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;ConditionOrder&gt;"/> instance.</returns>
		public abstract TList<ConditionOrder> GetListTodayOrders(TransactionManager transactionManager, int start, int pageLength , System.String market);
		
		#endregion
		
		#region _ConditionOrder_UpdateExpiredData 
		
		/// <summary>
		///	This method wrap the '_ConditionOrder_UpdateExpiredData' stored procedure. 
		/// </summary>
		/// <param name="condition"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void UpdateExpiredData(System.Int32? condition)
		{
			 UpdateExpiredData(null, 0, int.MaxValue , condition);
		}
		
		/// <summary>
		///	This method wrap the '_ConditionOrder_UpdateExpiredData' stored procedure. 
		/// </summary>
		/// <param name="condition"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void UpdateExpiredData(int start, int pageLength, System.Int32? condition)
		{
			 UpdateExpiredData(null, start, pageLength , condition);
		}
				
		/// <summary>
		///	This method wrap the '_ConditionOrder_UpdateExpiredData' stored procedure. 
		/// </summary>
		/// <param name="condition"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void UpdateExpiredData(TransactionManager transactionManager, System.Int32? condition)
		{
			 UpdateExpiredData(transactionManager, 0, int.MaxValue , condition);
		}
		
		/// <summary>
		///	This method wrap the '_ConditionOrder_UpdateExpiredData' stored procedure. 
		/// </summary>
		/// <param name="condition"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public abstract void UpdateExpiredData(TransactionManager transactionManager, int start, int pageLength , System.Int32? condition);
		
		#endregion
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;ConditionOrder&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;ConditionOrder&gt;"/></returns>
		public static TList<ConditionOrder> Fill(IDataReader reader, TList<ConditionOrder> rows, int start, int pageLength)
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
				
				ETradeOrders.Entities.ConditionOrder c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("ConditionOrder")
					.Append("|").Append((long)reader[((int)ConditionOrderColumn.ConditionOrderId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<ConditionOrder>(
					key.ToString(), // EntityTrackingKey
					"ConditionOrder",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ETradeOrders.Entities.ConditionOrder();
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
					c.ConditionOrderId = (long)reader[((int)ConditionOrderColumn.ConditionOrderId - 1)];
					c.SecSymbol = (string)reader[((int)ConditionOrderColumn.SecSymbol - 1)];
					c.Side = (string)reader[((int)ConditionOrderColumn.Side - 1)];
					c.Price = (decimal)reader[((int)ConditionOrderColumn.Price - 1)];
					c.Volume = (int)reader[((int)ConditionOrderColumn.Volume - 1)];
					c.MatchedVolume = (int)reader[((int)ConditionOrderColumn.MatchedVolume - 1)];
					c.SubCustAccountId = (string)reader[((int)ConditionOrderColumn.SubCustAccountId - 1)];
					c.MainCustAccountId = (string)reader[((int)ConditionOrderColumn.MainCustAccountId - 1)];
					c.Market = (string)reader[((int)ConditionOrderColumn.Market - 1)];
					c.EffDate = (reader.IsDBNull(((int)ConditionOrderColumn.EffDate - 1)))?null:(System.DateTime?)reader[((int)ConditionOrderColumn.EffDate - 1)];
					c.ExpDate = (reader.IsDBNull(((int)ConditionOrderColumn.ExpDate - 1)))?null:(System.DateTime?)reader[((int)ConditionOrderColumn.ExpDate - 1)];
					c.TypeOfCond = (short)reader[((int)ConditionOrderColumn.TypeOfCond - 1)];
					c.MaxValue = (reader.IsDBNull(((int)ConditionOrderColumn.MaxValue - 1)))?null:(System.Decimal?)reader[((int)ConditionOrderColumn.MaxValue - 1)];
					c.MinValue = (reader.IsDBNull(((int)ConditionOrderColumn.MinValue - 1)))?null:(System.Decimal?)reader[((int)ConditionOrderColumn.MinValue - 1)];
					c.Status = (string)reader[((int)ConditionOrderColumn.Status - 1)];
					c.TradeTime = (System.DateTime)reader[((int)ConditionOrderColumn.TradeTime - 1)];
					c.DoneTime = (reader.IsDBNull(((int)ConditionOrderColumn.DoneTime - 1)))?null:(System.DateTime?)reader[((int)ConditionOrderColumn.DoneTime - 1)];
					c.RejectReason = (reader.IsDBNull(((int)ConditionOrderColumn.RejectReason - 1)))?null:(System.Int32?)reader[((int)ConditionOrderColumn.RejectReason - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="ETradeOrders.Entities.ConditionOrder"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ETradeOrders.Entities.ConditionOrder"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ETradeOrders.Entities.ConditionOrder entity)
		{
			if (!reader.Read()) return;
			
			entity.ConditionOrderId = (long)reader[((int)ConditionOrderColumn.ConditionOrderId - 1)];
			entity.SecSymbol = (string)reader[((int)ConditionOrderColumn.SecSymbol - 1)];
			entity.Side = (string)reader[((int)ConditionOrderColumn.Side - 1)];
			entity.Price = (decimal)reader[((int)ConditionOrderColumn.Price - 1)];
			entity.Volume = (int)reader[((int)ConditionOrderColumn.Volume - 1)];
			entity.MatchedVolume = (int)reader[((int)ConditionOrderColumn.MatchedVolume - 1)];
			entity.SubCustAccountId = (string)reader[((int)ConditionOrderColumn.SubCustAccountId - 1)];
			entity.MainCustAccountId = (string)reader[((int)ConditionOrderColumn.MainCustAccountId - 1)];
			entity.Market = (string)reader[((int)ConditionOrderColumn.Market - 1)];
			entity.EffDate = (reader.IsDBNull(((int)ConditionOrderColumn.EffDate - 1)))?null:(System.DateTime?)reader[((int)ConditionOrderColumn.EffDate - 1)];
			entity.ExpDate = (reader.IsDBNull(((int)ConditionOrderColumn.ExpDate - 1)))?null:(System.DateTime?)reader[((int)ConditionOrderColumn.ExpDate - 1)];
			entity.TypeOfCond = (short)reader[((int)ConditionOrderColumn.TypeOfCond - 1)];
			entity.MaxValue = (reader.IsDBNull(((int)ConditionOrderColumn.MaxValue - 1)))?null:(System.Decimal?)reader[((int)ConditionOrderColumn.MaxValue - 1)];
			entity.MinValue = (reader.IsDBNull(((int)ConditionOrderColumn.MinValue - 1)))?null:(System.Decimal?)reader[((int)ConditionOrderColumn.MinValue - 1)];
			entity.Status = (string)reader[((int)ConditionOrderColumn.Status - 1)];
			entity.TradeTime = (System.DateTime)reader[((int)ConditionOrderColumn.TradeTime - 1)];
			entity.DoneTime = (reader.IsDBNull(((int)ConditionOrderColumn.DoneTime - 1)))?null:(System.DateTime?)reader[((int)ConditionOrderColumn.DoneTime - 1)];
			entity.RejectReason = (reader.IsDBNull(((int)ConditionOrderColumn.RejectReason - 1)))?null:(System.Int32?)reader[((int)ConditionOrderColumn.RejectReason - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ETradeOrders.Entities.ConditionOrder"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ETradeOrders.Entities.ConditionOrder"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ETradeOrders.Entities.ConditionOrder entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.ConditionOrderId = (long)dataRow["ConditionOrderID"];
			entity.SecSymbol = (string)dataRow["SecSymbol"];
			entity.Side = (string)dataRow["Side"];
			entity.Price = (decimal)dataRow["Price"];
			entity.Volume = (int)dataRow["Volume"];
			entity.MatchedVolume = (int)dataRow["MatchedVolume"];
			entity.SubCustAccountId = (string)dataRow["SubCustAccountID"];
			entity.MainCustAccountId = (string)dataRow["MainCustAccountID"];
			entity.Market = (string)dataRow["Market"];
			entity.EffDate = Convert.IsDBNull(dataRow["EffDate"]) ? null : (System.DateTime?)dataRow["EffDate"];
			entity.ExpDate = Convert.IsDBNull(dataRow["ExpDate"]) ? null : (System.DateTime?)dataRow["ExpDate"];
			entity.TypeOfCond = (short)dataRow["TypeOfCond"];
			entity.MaxValue = Convert.IsDBNull(dataRow["MaxValue"]) ? null : (System.Decimal?)dataRow["MaxValue"];
			entity.MinValue = Convert.IsDBNull(dataRow["MinValue"]) ? null : (System.Decimal?)dataRow["MinValue"];
			entity.Status = (string)dataRow["Status"];
			entity.TradeTime = (System.DateTime)dataRow["TradeTime"];
			entity.DoneTime = Convert.IsDBNull(dataRow["DoneTime"]) ? null : (System.DateTime?)dataRow["DoneTime"];
			entity.RejectReason = Convert.IsDBNull(dataRow["RejectReason"]) ? null : (System.Int32?)dataRow["RejectReason"];
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
		/// <param name="entity">The <see cref="ETradeOrders.Entities.ConditionOrder"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ETradeOrders.Entities.ConditionOrder Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ETradeOrders.Entities.ConditionOrder entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByConditionOrderId methods when available
			
			#region ConditionOrderDetailCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<ConditionOrderDetail>|ConditionOrderDetailCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ConditionOrderDetailCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.ConditionOrderDetailCollection = DataRepository.ConditionOrderDetailProvider.GetByConditionOrderId(transactionManager, entity.ConditionOrderId);

				if (deep && entity.ConditionOrderDetailCollection.Count > 0)
				{
					deepHandles.Add("ConditionOrderDetailCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<ConditionOrderDetail>) DataRepository.ConditionOrderDetailProvider.DeepLoad,
						new object[] { transactionManager, entity.ConditionOrderDetailCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region ExecOrderCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<ExecOrder>|ExecOrderCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ExecOrderCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.ExecOrderCollection = DataRepository.ExecOrderProvider.GetByConditionOrderId(transactionManager, entity.ConditionOrderId);

				if (deep && entity.ExecOrderCollection.Count > 0)
				{
					deepHandles.Add("ExecOrderCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<ExecOrder>) DataRepository.ExecOrderProvider.DeepLoad,
						new object[] { transactionManager, entity.ExecOrderCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the ETradeOrders.Entities.ConditionOrder object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ETradeOrders.Entities.ConditionOrder instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ETradeOrders.Entities.ConditionOrder Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ETradeOrders.Entities.ConditionOrder entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
			#region List<ConditionOrderDetail>
				if (CanDeepSave(entity.ConditionOrderDetailCollection, "List<ConditionOrderDetail>|ConditionOrderDetailCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(ConditionOrderDetail child in entity.ConditionOrderDetailCollection)
					{
						if(child.ConditionOrderIdSource != null)
						{
							child.ConditionOrderId = child.ConditionOrderIdSource.ConditionOrderId;
						}
						else
						{
							child.ConditionOrderId = entity.ConditionOrderId;
						}

					}

					if (entity.ConditionOrderDetailCollection.Count > 0 || entity.ConditionOrderDetailCollection.DeletedItems.Count > 0)
					{
						//DataRepository.ConditionOrderDetailProvider.Save(transactionManager, entity.ConditionOrderDetailCollection);
						
						deepHandles.Add("ConditionOrderDetailCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< ConditionOrderDetail >) DataRepository.ConditionOrderDetailProvider.DeepSave,
							new object[] { transactionManager, entity.ConditionOrderDetailCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
	
			#region List<ExecOrder>
				if (CanDeepSave(entity.ExecOrderCollection, "List<ExecOrder>|ExecOrderCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(ExecOrder child in entity.ExecOrderCollection)
					{
						if(child.ConditionOrderIdSource != null)
						{
							child.ConditionOrderId = child.ConditionOrderIdSource.ConditionOrderId;
						}
						else
						{
							child.ConditionOrderId = entity.ConditionOrderId;
						}

					}

					if (entity.ExecOrderCollection.Count > 0 || entity.ExecOrderCollection.DeletedItems.Count > 0)
					{
						//DataRepository.ExecOrderProvider.Save(transactionManager, entity.ExecOrderCollection);
						
						deepHandles.Add("ExecOrderCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< ExecOrder >) DataRepository.ExecOrderProvider.DeepSave,
							new object[] { transactionManager, entity.ExecOrderCollection, deepSaveType, childTypes, innerList }
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
	
	#region ConditionOrderChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ETradeOrders.Entities.ConditionOrder</c>
	///</summary>
	public enum ConditionOrderChildEntityTypes
	{

		///<summary>
		/// Collection of <c>ConditionOrder</c> as OneToMany for ConditionOrderDetailCollection
		///</summary>
		[ChildEntityType(typeof(TList<ConditionOrderDetail>))]
		ConditionOrderDetailCollection,

		///<summary>
		/// Collection of <c>ConditionOrder</c> as OneToMany for ExecOrderCollection
		///</summary>
		[ChildEntityType(typeof(TList<ExecOrder>))]
		ExecOrderCollection,
	}
	
	#endregion ConditionOrderChildEntityTypes
	
	#region ConditionOrderFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;ConditionOrderColumn&gt;"/> class
	/// that is used exclusively with a <see cref="ConditionOrder"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ConditionOrderFilterBuilder : SqlFilterBuilder<ConditionOrderColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ConditionOrderFilterBuilder class.
		/// </summary>
		public ConditionOrderFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ConditionOrderFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ConditionOrderFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ConditionOrderFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ConditionOrderFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ConditionOrderFilterBuilder
	
	#region ConditionOrderParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;ConditionOrderColumn&gt;"/> class
	/// that is used exclusively with a <see cref="ConditionOrder"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ConditionOrderParameterBuilder : ParameterizedSqlFilterBuilder<ConditionOrderColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ConditionOrderParameterBuilder class.
		/// </summary>
		public ConditionOrderParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ConditionOrderParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ConditionOrderParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ConditionOrderParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ConditionOrderParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ConditionOrderParameterBuilder
	
	#region ConditionOrderSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;ConditionOrderColumn&gt;"/> class
	/// that is used exclusively with a <see cref="ConditionOrder"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class ConditionOrderSortBuilder : SqlSortBuilder<ConditionOrderColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ConditionOrderSqlSortBuilder class.
		/// </summary>
		public ConditionOrderSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion ConditionOrderSortBuilder
	
} // end namespace
