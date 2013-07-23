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
	/// This class is the base class for any <see cref="ConditionOrderDetailProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class ConditionOrderDetailProviderBaseCore : EntityProviderBase<ETradeOrders.Entities.ConditionOrderDetail, ETradeOrders.Entities.ConditionOrderDetailKey>
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
		public override bool Delete(TransactionManager transactionManager, ETradeOrders.Entities.ConditionOrderDetailKey key)
		{
			return Delete(transactionManager, key.DetailId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_detailId">Auto increase key. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(long _detailId)
		{
			return Delete(null, _detailId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_detailId">Auto increase key. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, long _detailId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ConditionOrderDetail_ConditionOrder key.
		///		FK_ConditionOrderDetail_ConditionOrder Description: 
		/// </summary>
		/// <param name="_conditionOrderId">Foreign key to ConditionOrder table</param>
		/// <returns>Returns a typed collection of ETradeOrders.Entities.ConditionOrderDetail objects.</returns>
		public TList<ConditionOrderDetail> GetByConditionOrderId(long _conditionOrderId)
		{
			int count = -1;
			return GetByConditionOrderId(_conditionOrderId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ConditionOrderDetail_ConditionOrder key.
		///		FK_ConditionOrderDetail_ConditionOrder Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_conditionOrderId">Foreign key to ConditionOrder table</param>
		/// <returns>Returns a typed collection of ETradeOrders.Entities.ConditionOrderDetail objects.</returns>
		/// <remarks></remarks>
		public TList<ConditionOrderDetail> GetByConditionOrderId(TransactionManager transactionManager, long _conditionOrderId)
		{
			int count = -1;
			return GetByConditionOrderId(transactionManager, _conditionOrderId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_ConditionOrderDetail_ConditionOrder key.
		///		FK_ConditionOrderDetail_ConditionOrder Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_conditionOrderId">Foreign key to ConditionOrder table</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ETradeOrders.Entities.ConditionOrderDetail objects.</returns>
		public TList<ConditionOrderDetail> GetByConditionOrderId(TransactionManager transactionManager, long _conditionOrderId, int start, int pageLength)
		{
			int count = -1;
			return GetByConditionOrderId(transactionManager, _conditionOrderId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ConditionOrderDetail_ConditionOrder key.
		///		fkConditionOrderDetailConditionOrder Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_conditionOrderId">Foreign key to ConditionOrder table</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ETradeOrders.Entities.ConditionOrderDetail objects.</returns>
		public TList<ConditionOrderDetail> GetByConditionOrderId(long _conditionOrderId, int start, int pageLength)
		{
			int count =  -1;
			return GetByConditionOrderId(null, _conditionOrderId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ConditionOrderDetail_ConditionOrder key.
		///		fkConditionOrderDetailConditionOrder Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_conditionOrderId">Foreign key to ConditionOrder table</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ETradeOrders.Entities.ConditionOrderDetail objects.</returns>
		public TList<ConditionOrderDetail> GetByConditionOrderId(long _conditionOrderId, int start, int pageLength,out int count)
		{
			return GetByConditionOrderId(null, _conditionOrderId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ConditionOrderDetail_ConditionOrder key.
		///		FK_ConditionOrderDetail_ConditionOrder Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_conditionOrderId">Foreign key to ConditionOrder table</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of ETradeOrders.Entities.ConditionOrderDetail objects.</returns>
		public abstract TList<ConditionOrderDetail> GetByConditionOrderId(TransactionManager transactionManager, long _conditionOrderId, int start, int pageLength, out int count);
		
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
		public override ETradeOrders.Entities.ConditionOrderDetail Get(TransactionManager transactionManager, ETradeOrders.Entities.ConditionOrderDetailKey key, int start, int pageLength)
		{
			return GetByDetailId(transactionManager, key.DetailId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_CONDITIONORDERDETAIL index.
		/// </summary>
		/// <param name="_detailId">Auto increase key</param>
		/// <returns>Returns an instance of the <see cref="ETradeOrders.Entities.ConditionOrderDetail"/> class.</returns>
		public ETradeOrders.Entities.ConditionOrderDetail GetByDetailId(long _detailId)
		{
			int count = -1;
			return GetByDetailId(null,_detailId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CONDITIONORDERDETAIL index.
		/// </summary>
		/// <param name="_detailId">Auto increase key</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeOrders.Entities.ConditionOrderDetail"/> class.</returns>
		public ETradeOrders.Entities.ConditionOrderDetail GetByDetailId(long _detailId, int start, int pageLength)
		{
			int count = -1;
			return GetByDetailId(null, _detailId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CONDITIONORDERDETAIL index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_detailId">Auto increase key</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeOrders.Entities.ConditionOrderDetail"/> class.</returns>
		public ETradeOrders.Entities.ConditionOrderDetail GetByDetailId(TransactionManager transactionManager, long _detailId)
		{
			int count = -1;
			return GetByDetailId(transactionManager, _detailId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CONDITIONORDERDETAIL index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_detailId">Auto increase key</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeOrders.Entities.ConditionOrderDetail"/> class.</returns>
		public ETradeOrders.Entities.ConditionOrderDetail GetByDetailId(TransactionManager transactionManager, long _detailId, int start, int pageLength)
		{
			int count = -1;
			return GetByDetailId(transactionManager, _detailId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CONDITIONORDERDETAIL index.
		/// </summary>
		/// <param name="_detailId">Auto increase key</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeOrders.Entities.ConditionOrderDetail"/> class.</returns>
		public ETradeOrders.Entities.ConditionOrderDetail GetByDetailId(long _detailId, int start, int pageLength, out int count)
		{
			return GetByDetailId(null, _detailId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CONDITIONORDERDETAIL index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_detailId">Auto increase key</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ETradeOrders.Entities.ConditionOrderDetail"/> class.</returns>
		public abstract ETradeOrders.Entities.ConditionOrderDetail GetByDetailId(TransactionManager transactionManager, long _detailId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;ConditionOrderDetail&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;ConditionOrderDetail&gt;"/></returns>
		public static TList<ConditionOrderDetail> Fill(IDataReader reader, TList<ConditionOrderDetail> rows, int start, int pageLength)
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
				
				ETradeOrders.Entities.ConditionOrderDetail c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("ConditionOrderDetail")
					.Append("|").Append((long)reader[((int)ConditionOrderDetailColumn.DetailId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<ConditionOrderDetail>(
					key.ToString(), // EntityTrackingKey
					"ConditionOrderDetail",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ETradeOrders.Entities.ConditionOrderDetail();
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
					c.DetailId = (long)reader[((int)ConditionOrderDetailColumn.DetailId - 1)];
					c.Volume = (int)reader[((int)ConditionOrderDetailColumn.Volume - 1)];
					c.MatchedVolume = (int)reader[((int)ConditionOrderDetailColumn.MatchedVolume - 1)];
					c.AvgPrice = (reader.IsDBNull(((int)ConditionOrderDetailColumn.AvgPrice - 1)))?null:(System.Decimal?)reader[((int)ConditionOrderDetailColumn.AvgPrice - 1)];
					c.OrderStatus = (reader.IsDBNull(((int)ConditionOrderDetailColumn.OrderStatus - 1)))?null:(System.Int16?)reader[((int)ConditionOrderDetailColumn.OrderStatus - 1)];
					c.ConditionOrderId = (long)reader[((int)ConditionOrderDetailColumn.ConditionOrderId - 1)];
					c.FisOrderId = (reader.IsDBNull(((int)ConditionOrderDetailColumn.FisOrderId - 1)))?null:(System.Int32?)reader[((int)ConditionOrderDetailColumn.FisOrderId - 1)];
					c.OrdRejReason = (reader.IsDBNull(((int)ConditionOrderDetailColumn.OrdRejReason - 1)))?null:(System.Int32?)reader[((int)ConditionOrderDetailColumn.OrdRejReason - 1)];
					c.NumOfMatch = (reader.IsDBNull(((int)ConditionOrderDetailColumn.NumOfMatch - 1)))?null:(System.Int32?)reader[((int)ConditionOrderDetailColumn.NumOfMatch - 1)];
					c.CancelledVol = (reader.IsDBNull(((int)ConditionOrderDetailColumn.CancelledVol - 1)))?null:(System.Int32?)reader[((int)ConditionOrderDetailColumn.CancelledVol - 1)];
					c.CreatedDateTime = (System.DateTime)reader[((int)ConditionOrderDetailColumn.CreatedDateTime - 1)];
					c.UpdatedDateTime = (reader.IsDBNull(((int)ConditionOrderDetailColumn.UpdatedDateTime - 1)))?null:(System.DateTime?)reader[((int)ConditionOrderDetailColumn.UpdatedDateTime - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="ETradeOrders.Entities.ConditionOrderDetail"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ETradeOrders.Entities.ConditionOrderDetail"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ETradeOrders.Entities.ConditionOrderDetail entity)
		{
			if (!reader.Read()) return;
			
			entity.DetailId = (long)reader[((int)ConditionOrderDetailColumn.DetailId - 1)];
			entity.Volume = (int)reader[((int)ConditionOrderDetailColumn.Volume - 1)];
			entity.MatchedVolume = (int)reader[((int)ConditionOrderDetailColumn.MatchedVolume - 1)];
			entity.AvgPrice = (reader.IsDBNull(((int)ConditionOrderDetailColumn.AvgPrice - 1)))?null:(System.Decimal?)reader[((int)ConditionOrderDetailColumn.AvgPrice - 1)];
			entity.OrderStatus = (reader.IsDBNull(((int)ConditionOrderDetailColumn.OrderStatus - 1)))?null:(System.Int16?)reader[((int)ConditionOrderDetailColumn.OrderStatus - 1)];
			entity.ConditionOrderId = (long)reader[((int)ConditionOrderDetailColumn.ConditionOrderId - 1)];
			entity.FisOrderId = (reader.IsDBNull(((int)ConditionOrderDetailColumn.FisOrderId - 1)))?null:(System.Int32?)reader[((int)ConditionOrderDetailColumn.FisOrderId - 1)];
			entity.OrdRejReason = (reader.IsDBNull(((int)ConditionOrderDetailColumn.OrdRejReason - 1)))?null:(System.Int32?)reader[((int)ConditionOrderDetailColumn.OrdRejReason - 1)];
			entity.NumOfMatch = (reader.IsDBNull(((int)ConditionOrderDetailColumn.NumOfMatch - 1)))?null:(System.Int32?)reader[((int)ConditionOrderDetailColumn.NumOfMatch - 1)];
			entity.CancelledVol = (reader.IsDBNull(((int)ConditionOrderDetailColumn.CancelledVol - 1)))?null:(System.Int32?)reader[((int)ConditionOrderDetailColumn.CancelledVol - 1)];
			entity.CreatedDateTime = (System.DateTime)reader[((int)ConditionOrderDetailColumn.CreatedDateTime - 1)];
			entity.UpdatedDateTime = (reader.IsDBNull(((int)ConditionOrderDetailColumn.UpdatedDateTime - 1)))?null:(System.DateTime?)reader[((int)ConditionOrderDetailColumn.UpdatedDateTime - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ETradeOrders.Entities.ConditionOrderDetail"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ETradeOrders.Entities.ConditionOrderDetail"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ETradeOrders.Entities.ConditionOrderDetail entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.DetailId = (long)dataRow["DetailId"];
			entity.Volume = (int)dataRow["Volume"];
			entity.MatchedVolume = (int)dataRow["MatchedVolume"];
			entity.AvgPrice = Convert.IsDBNull(dataRow["AvgPrice"]) ? null : (System.Decimal?)dataRow["AvgPrice"];
			entity.OrderStatus = Convert.IsDBNull(dataRow["OrderStatus"]) ? null : (System.Int16?)dataRow["OrderStatus"];
			entity.ConditionOrderId = (long)dataRow["ConditionOrderID"];
			entity.FisOrderId = Convert.IsDBNull(dataRow["FISOrderID"]) ? null : (System.Int32?)dataRow["FISOrderID"];
			entity.OrdRejReason = Convert.IsDBNull(dataRow["OrdRejReason"]) ? null : (System.Int32?)dataRow["OrdRejReason"];
			entity.NumOfMatch = Convert.IsDBNull(dataRow["NumOfMatch"]) ? null : (System.Int32?)dataRow["NumOfMatch"];
			entity.CancelledVol = Convert.IsDBNull(dataRow["CancelledVol"]) ? null : (System.Int32?)dataRow["CancelledVol"];
			entity.CreatedDateTime = (System.DateTime)dataRow["CreatedDateTime"];
			entity.UpdatedDateTime = Convert.IsDBNull(dataRow["UpdatedDateTime"]) ? null : (System.DateTime?)dataRow["UpdatedDateTime"];
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
		/// <param name="entity">The <see cref="ETradeOrders.Entities.ConditionOrderDetail"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ETradeOrders.Entities.ConditionOrderDetail Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ETradeOrders.Entities.ConditionOrderDetail entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region ConditionOrderIdSource	
			if (CanDeepLoad(entity, "ConditionOrder|ConditionOrderIdSource", deepLoadType, innerList) 
				&& entity.ConditionOrderIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.ConditionOrderId;
				ConditionOrder tmpEntity = EntityManager.LocateEntity<ConditionOrder>(EntityLocator.ConstructKeyFromPkItems(typeof(ConditionOrder), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.ConditionOrderIdSource = tmpEntity;
				else
					entity.ConditionOrderIdSource = DataRepository.ConditionOrderProvider.GetByConditionOrderId(transactionManager, entity.ConditionOrderId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ConditionOrderIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.ConditionOrderIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.ConditionOrderProvider.DeepLoad(transactionManager, entity.ConditionOrderIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion ConditionOrderIdSource
			
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
		/// Deep Save the entire object graph of the ETradeOrders.Entities.ConditionOrderDetail object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ETradeOrders.Entities.ConditionOrderDetail instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ETradeOrders.Entities.ConditionOrderDetail Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ETradeOrders.Entities.ConditionOrderDetail entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region ConditionOrderIdSource
			if (CanDeepSave(entity, "ConditionOrder|ConditionOrderIdSource", deepSaveType, innerList) 
				&& entity.ConditionOrderIdSource != null)
			{
				DataRepository.ConditionOrderProvider.Save(transactionManager, entity.ConditionOrderIdSource);
				entity.ConditionOrderId = entity.ConditionOrderIdSource.ConditionOrderId;
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
	
	#region ConditionOrderDetailChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ETradeOrders.Entities.ConditionOrderDetail</c>
	///</summary>
	public enum ConditionOrderDetailChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>ConditionOrder</c> at ConditionOrderIdSource
		///</summary>
		[ChildEntityType(typeof(ConditionOrder))]
		ConditionOrder,
		}
	
	#endregion ConditionOrderDetailChildEntityTypes
	
	#region ConditionOrderDetailFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;ConditionOrderDetailColumn&gt;"/> class
	/// that is used exclusively with a <see cref="ConditionOrderDetail"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ConditionOrderDetailFilterBuilder : SqlFilterBuilder<ConditionOrderDetailColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ConditionOrderDetailFilterBuilder class.
		/// </summary>
		public ConditionOrderDetailFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ConditionOrderDetailFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ConditionOrderDetailFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ConditionOrderDetailFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ConditionOrderDetailFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ConditionOrderDetailFilterBuilder
	
	#region ConditionOrderDetailParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;ConditionOrderDetailColumn&gt;"/> class
	/// that is used exclusively with a <see cref="ConditionOrderDetail"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ConditionOrderDetailParameterBuilder : ParameterizedSqlFilterBuilder<ConditionOrderDetailColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ConditionOrderDetailParameterBuilder class.
		/// </summary>
		public ConditionOrderDetailParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ConditionOrderDetailParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ConditionOrderDetailParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ConditionOrderDetailParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ConditionOrderDetailParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ConditionOrderDetailParameterBuilder
	
	#region ConditionOrderDetailSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;ConditionOrderDetailColumn&gt;"/> class
	/// that is used exclusively with a <see cref="ConditionOrderDetail"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class ConditionOrderDetailSortBuilder : SqlSortBuilder<ConditionOrderDetailColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ConditionOrderDetailSqlSortBuilder class.
		/// </summary>
		public ConditionOrderDetailSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion ConditionOrderDetailSortBuilder
	
} // end namespace
