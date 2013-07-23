#region Using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

using ETradeHistory.Entities;
using ETradeHistory.DataAccess;

#endregion

namespace ETradeHistory.DataAccess.Bases
{	
	///<summary>
	/// This class is the base class for any <see cref="TradedHistoryProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class TradedHistoryProviderBaseCore : EntityProviderBase<ETradeHistory.Entities.TradedHistory, ETradeHistory.Entities.TradedHistoryKey>
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
		public override bool Delete(TransactionManager transactionManager, ETradeHistory.Entities.TradedHistoryKey key)
		{
			return Delete(transactionManager, key.Id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_id">ID identifies TradedHistory. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(long _id)
		{
			return Delete(null, _id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">ID identifies TradedHistory. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, long _id);		
		
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
		public override ETradeHistory.Entities.TradedHistory Get(TransactionManager transactionManager, ETradeHistory.Entities.TradedHistoryKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_TradedHistory index.
		/// </summary>
		/// <param name="_id">ID identifies TradedHistory</param>
		/// <returns>Returns an instance of the <see cref="ETradeHistory.Entities.TradedHistory"/> class.</returns>
		public ETradeHistory.Entities.TradedHistory GetById(long _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_TradedHistory index.
		/// </summary>
		/// <param name="_id">ID identifies TradedHistory</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeHistory.Entities.TradedHistory"/> class.</returns>
		public ETradeHistory.Entities.TradedHistory GetById(long _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_TradedHistory index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">ID identifies TradedHistory</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeHistory.Entities.TradedHistory"/> class.</returns>
		public ETradeHistory.Entities.TradedHistory GetById(TransactionManager transactionManager, long _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_TradedHistory index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">ID identifies TradedHistory</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeHistory.Entities.TradedHistory"/> class.</returns>
		public ETradeHistory.Entities.TradedHistory GetById(TransactionManager transactionManager, long _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_TradedHistory index.
		/// </summary>
		/// <param name="_id">ID identifies TradedHistory</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeHistory.Entities.TradedHistory"/> class.</returns>
		public ETradeHistory.Entities.TradedHistory GetById(long _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_TradedHistory index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">ID identifies TradedHistory</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ETradeHistory.Entities.TradedHistory"/> class.</returns>
		public abstract ETradeHistory.Entities.TradedHistory GetById(TransactionManager transactionManager, long _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;TradedHistory&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;TradedHistory&gt;"/></returns>
		public static TList<TradedHistory> Fill(IDataReader reader, TList<TradedHistory> rows, int start, int pageLength)
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
				
				ETradeHistory.Entities.TradedHistory c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("TradedHistory")
					.Append("|").Append((long)reader[((int)TradedHistoryColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<TradedHistory>(
					key.ToString(), // EntityTrackingKey
					"TradedHistory",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ETradeHistory.Entities.TradedHistory();
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
					c.Id = (long)reader[((int)TradedHistoryColumn.Id - 1)];
					c.TradeTime = (System.DateTime)reader[((int)TradedHistoryColumn.TradeTime - 1)];
					c.SubCustAccountId = (string)reader[((int)TradedHistoryColumn.SubCustAccountId - 1)];
					c.Type = (reader.IsDBNull(((int)TradedHistoryColumn.Type - 1)))?null:(string)reader[((int)TradedHistoryColumn.Type - 1)];
					c.FisOrderId = (reader.IsDBNull(((int)TradedHistoryColumn.FisOrderId - 1)))?null:(System.Int32?)reader[((int)TradedHistoryColumn.FisOrderId - 1)];
					c.SecSymbol = (string)reader[((int)TradedHistoryColumn.SecSymbol - 1)];
					c.Side = (string)reader[((int)TradedHistoryColumn.Side - 1)];
					c.Price = (decimal)reader[((int)TradedHistoryColumn.Price - 1)];
					c.ConPrice = (reader.IsDBNull(((int)TradedHistoryColumn.ConPrice - 1)))?null:(string)reader[((int)TradedHistoryColumn.ConPrice - 1)];
					c.Volume = (long)reader[((int)TradedHistoryColumn.Volume - 1)];
					c.ExecutedVol = (reader.IsDBNull(((int)TradedHistoryColumn.ExecutedVol - 1)))?null:(System.Int64?)reader[((int)TradedHistoryColumn.ExecutedVol - 1)];
					c.ExecutedPrice = (reader.IsDBNull(((int)TradedHistoryColumn.ExecutedPrice - 1)))?null:(System.Decimal?)reader[((int)TradedHistoryColumn.ExecutedPrice - 1)];
					c.CancelledVolume = (reader.IsDBNull(((int)TradedHistoryColumn.CancelledVolume - 1)))?null:(System.Int64?)reader[((int)TradedHistoryColumn.CancelledVolume - 1)];
					c.MatchedTime = (reader.IsDBNull(((int)TradedHistoryColumn.MatchedTime - 1)))?null:(System.DateTime?)reader[((int)TradedHistoryColumn.MatchedTime - 1)];
					c.CancelledTime = (reader.IsDBNull(((int)TradedHistoryColumn.CancelledTime - 1)))?null:(System.DateTime?)reader[((int)TradedHistoryColumn.CancelledTime - 1)];
					c.OrdRejReason = (reader.IsDBNull(((int)TradedHistoryColumn.OrdRejReason - 1)))?null:(System.Int32?)reader[((int)TradedHistoryColumn.OrdRejReason - 1)];
					c.CancelledRejReason = (reader.IsDBNull(((int)TradedHistoryColumn.CancelledRejReason - 1)))?null:(System.Int32?)reader[((int)TradedHistoryColumn.CancelledRejReason - 1)];
					c.SourceId = (reader.IsDBNull(((int)TradedHistoryColumn.SourceId - 1)))?null:(System.Int16?)reader[((int)TradedHistoryColumn.SourceId - 1)];
					c.Market = (string)reader[((int)TradedHistoryColumn.Market - 1)];
					c.RefOrderId = (reader.IsDBNull(((int)TradedHistoryColumn.RefOrderId - 1)))?null:(string)reader[((int)TradedHistoryColumn.RefOrderId - 1)];
					c.EffDate = (reader.IsDBNull(((int)TradedHistoryColumn.EffDate - 1)))?null:(System.DateTime?)reader[((int)TradedHistoryColumn.EffDate - 1)];
					c.ExpDate = (reader.IsDBNull(((int)TradedHistoryColumn.ExpDate - 1)))?null:(System.DateTime?)reader[((int)TradedHistoryColumn.ExpDate - 1)];
					c.MinValue = (reader.IsDBNull(((int)TradedHistoryColumn.MinValue - 1)))?null:(System.Decimal?)reader[((int)TradedHistoryColumn.MinValue - 1)];
					c.MaxValue = (reader.IsDBNull(((int)TradedHistoryColumn.MaxValue - 1)))?null:(System.Decimal?)reader[((int)TradedHistoryColumn.MaxValue - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="ETradeHistory.Entities.TradedHistory"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ETradeHistory.Entities.TradedHistory"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ETradeHistory.Entities.TradedHistory entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (long)reader[((int)TradedHistoryColumn.Id - 1)];
			entity.TradeTime = (System.DateTime)reader[((int)TradedHistoryColumn.TradeTime - 1)];
			entity.SubCustAccountId = (string)reader[((int)TradedHistoryColumn.SubCustAccountId - 1)];
			entity.Type = (reader.IsDBNull(((int)TradedHistoryColumn.Type - 1)))?null:(string)reader[((int)TradedHistoryColumn.Type - 1)];
			entity.FisOrderId = (reader.IsDBNull(((int)TradedHistoryColumn.FisOrderId - 1)))?null:(System.Int32?)reader[((int)TradedHistoryColumn.FisOrderId - 1)];
			entity.SecSymbol = (string)reader[((int)TradedHistoryColumn.SecSymbol - 1)];
			entity.Side = (string)reader[((int)TradedHistoryColumn.Side - 1)];
			entity.Price = (decimal)reader[((int)TradedHistoryColumn.Price - 1)];
			entity.ConPrice = (reader.IsDBNull(((int)TradedHistoryColumn.ConPrice - 1)))?null:(string)reader[((int)TradedHistoryColumn.ConPrice - 1)];
			entity.Volume = (long)reader[((int)TradedHistoryColumn.Volume - 1)];
			entity.ExecutedVol = (reader.IsDBNull(((int)TradedHistoryColumn.ExecutedVol - 1)))?null:(System.Int64?)reader[((int)TradedHistoryColumn.ExecutedVol - 1)];
			entity.ExecutedPrice = (reader.IsDBNull(((int)TradedHistoryColumn.ExecutedPrice - 1)))?null:(System.Decimal?)reader[((int)TradedHistoryColumn.ExecutedPrice - 1)];
			entity.CancelledVolume = (reader.IsDBNull(((int)TradedHistoryColumn.CancelledVolume - 1)))?null:(System.Int64?)reader[((int)TradedHistoryColumn.CancelledVolume - 1)];
			entity.MatchedTime = (reader.IsDBNull(((int)TradedHistoryColumn.MatchedTime - 1)))?null:(System.DateTime?)reader[((int)TradedHistoryColumn.MatchedTime - 1)];
			entity.CancelledTime = (reader.IsDBNull(((int)TradedHistoryColumn.CancelledTime - 1)))?null:(System.DateTime?)reader[((int)TradedHistoryColumn.CancelledTime - 1)];
			entity.OrdRejReason = (reader.IsDBNull(((int)TradedHistoryColumn.OrdRejReason - 1)))?null:(System.Int32?)reader[((int)TradedHistoryColumn.OrdRejReason - 1)];
			entity.CancelledRejReason = (reader.IsDBNull(((int)TradedHistoryColumn.CancelledRejReason - 1)))?null:(System.Int32?)reader[((int)TradedHistoryColumn.CancelledRejReason - 1)];
			entity.SourceId = (reader.IsDBNull(((int)TradedHistoryColumn.SourceId - 1)))?null:(System.Int16?)reader[((int)TradedHistoryColumn.SourceId - 1)];
			entity.Market = (string)reader[((int)TradedHistoryColumn.Market - 1)];
			entity.RefOrderId = (reader.IsDBNull(((int)TradedHistoryColumn.RefOrderId - 1)))?null:(string)reader[((int)TradedHistoryColumn.RefOrderId - 1)];
			entity.EffDate = (reader.IsDBNull(((int)TradedHistoryColumn.EffDate - 1)))?null:(System.DateTime?)reader[((int)TradedHistoryColumn.EffDate - 1)];
			entity.ExpDate = (reader.IsDBNull(((int)TradedHistoryColumn.ExpDate - 1)))?null:(System.DateTime?)reader[((int)TradedHistoryColumn.ExpDate - 1)];
			entity.MinValue = (reader.IsDBNull(((int)TradedHistoryColumn.MinValue - 1)))?null:(System.Decimal?)reader[((int)TradedHistoryColumn.MinValue - 1)];
			entity.MaxValue = (reader.IsDBNull(((int)TradedHistoryColumn.MaxValue - 1)))?null:(System.Decimal?)reader[((int)TradedHistoryColumn.MaxValue - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ETradeHistory.Entities.TradedHistory"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ETradeHistory.Entities.TradedHistory"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ETradeHistory.Entities.TradedHistory entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (long)dataRow["ID"];
			entity.TradeTime = (System.DateTime)dataRow["TradeTime"];
			entity.SubCustAccountId = (string)dataRow["SubCustAccountID"];
			entity.Type = Convert.IsDBNull(dataRow["Type"]) ? null : (string)dataRow["Type"];
			entity.FisOrderId = Convert.IsDBNull(dataRow["FISOrderID"]) ? null : (System.Int32?)dataRow["FISOrderID"];
			entity.SecSymbol = (string)dataRow["SecSymbol"];
			entity.Side = (string)dataRow["Side"];
			entity.Price = (decimal)dataRow["Price"];
			entity.ConPrice = Convert.IsDBNull(dataRow["ConPrice"]) ? null : (string)dataRow["ConPrice"];
			entity.Volume = (long)dataRow["Volume"];
			entity.ExecutedVol = Convert.IsDBNull(dataRow["ExecutedVol"]) ? null : (System.Int64?)dataRow["ExecutedVol"];
			entity.ExecutedPrice = Convert.IsDBNull(dataRow["ExecutedPrice"]) ? null : (System.Decimal?)dataRow["ExecutedPrice"];
			entity.CancelledVolume = Convert.IsDBNull(dataRow["CancelledVolume"]) ? null : (System.Int64?)dataRow["CancelledVolume"];
			entity.MatchedTime = Convert.IsDBNull(dataRow["MatchedTime"]) ? null : (System.DateTime?)dataRow["MatchedTime"];
			entity.CancelledTime = Convert.IsDBNull(dataRow["CancelledTime"]) ? null : (System.DateTime?)dataRow["CancelledTime"];
			entity.OrdRejReason = Convert.IsDBNull(dataRow["OrdRejReason"]) ? null : (System.Int32?)dataRow["OrdRejReason"];
			entity.CancelledRejReason = Convert.IsDBNull(dataRow["CancelledRejReason"]) ? null : (System.Int32?)dataRow["CancelledRejReason"];
			entity.SourceId = Convert.IsDBNull(dataRow["SourceID"]) ? null : (System.Int16?)dataRow["SourceID"];
			entity.Market = (string)dataRow["Market"];
			entity.RefOrderId = Convert.IsDBNull(dataRow["RefOrderID"]) ? null : (string)dataRow["RefOrderID"];
			entity.EffDate = Convert.IsDBNull(dataRow["EffDate"]) ? null : (System.DateTime?)dataRow["EffDate"];
			entity.ExpDate = Convert.IsDBNull(dataRow["ExpDate"]) ? null : (System.DateTime?)dataRow["ExpDate"];
			entity.MinValue = Convert.IsDBNull(dataRow["MinValue"]) ? null : (System.Decimal?)dataRow["MinValue"];
			entity.MaxValue = Convert.IsDBNull(dataRow["MaxValue"]) ? null : (System.Decimal?)dataRow["MaxValue"];
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
		/// <param name="entity">The <see cref="ETradeHistory.Entities.TradedHistory"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ETradeHistory.Entities.TradedHistory Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ETradeHistory.Entities.TradedHistory entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
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
		/// Deep Save the entire object graph of the ETradeHistory.Entities.TradedHistory object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ETradeHistory.Entities.TradedHistory instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ETradeHistory.Entities.TradedHistory Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ETradeHistory.Entities.TradedHistory entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
	#region TradedHistoryChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ETradeHistory.Entities.TradedHistory</c>
	///</summary>
	public enum TradedHistoryChildEntityTypes
	{
	}
	
	#endregion TradedHistoryChildEntityTypes
	
	#region TradedHistoryFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;TradedHistoryColumn&gt;"/> class
	/// that is used exclusively with a <see cref="TradedHistory"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class TradedHistoryFilterBuilder : SqlFilterBuilder<TradedHistoryColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TradedHistoryFilterBuilder class.
		/// </summary>
		public TradedHistoryFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the TradedHistoryFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public TradedHistoryFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the TradedHistoryFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public TradedHistoryFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion TradedHistoryFilterBuilder
	
	#region TradedHistoryParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;TradedHistoryColumn&gt;"/> class
	/// that is used exclusively with a <see cref="TradedHistory"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class TradedHistoryParameterBuilder : ParameterizedSqlFilterBuilder<TradedHistoryColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TradedHistoryParameterBuilder class.
		/// </summary>
		public TradedHistoryParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the TradedHistoryParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public TradedHistoryParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the TradedHistoryParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public TradedHistoryParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion TradedHistoryParameterBuilder
	
	#region TradedHistorySortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;TradedHistoryColumn&gt;"/> class
	/// that is used exclusively with a <see cref="TradedHistory"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class TradedHistorySortBuilder : SqlSortBuilder<TradedHistoryColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TradedHistorySqlSortBuilder class.
		/// </summary>
		public TradedHistorySortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion TradedHistorySortBuilder
	
} // end namespace
