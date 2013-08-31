﻿#region Using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

using RTStockData.Entities;
using RTStockData.Data;

#endregion

namespace RTStockData.Data.Bases
{	
	///<summary>
	/// This class is the base class for any <see cref="UpcomTransactionsProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class UpcomTransactionsProviderBaseCore : EntityProviderBase<RTStockData.Entities.UpcomTransactions, RTStockData.Entities.UpcomTransactionsKey>
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
		public override bool Delete(TransactionManager transactionManager, RTStockData.Entities.UpcomTransactionsKey key)
		{
			return Delete(transactionManager, key.Id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_id">. Primary Key.</param>
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
		/// <param name="_id">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Int64 _id);		
		
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
		public override RTStockData.Entities.UpcomTransactions Get(TransactionManager transactionManager, RTStockData.Entities.UpcomTransactionsKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_upcom_transactions index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.UpcomTransactions"/> class.</returns>
		public RTStockData.Entities.UpcomTransactions GetById(System.Int64 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_upcom_transactions index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.UpcomTransactions"/> class.</returns>
		public RTStockData.Entities.UpcomTransactions GetById(System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_upcom_transactions index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.UpcomTransactions"/> class.</returns>
		public RTStockData.Entities.UpcomTransactions GetById(TransactionManager transactionManager, System.Int64 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_upcom_transactions index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.UpcomTransactions"/> class.</returns>
		public RTStockData.Entities.UpcomTransactions GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_upcom_transactions index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.UpcomTransactions"/> class.</returns>
		public RTStockData.Entities.UpcomTransactions GetById(System.Int64 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_upcom_transactions index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.UpcomTransactions"/> class.</returns>
		public abstract RTStockData.Entities.UpcomTransactions GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;UpcomTransactions&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;UpcomTransactions&gt;"/></returns>
		public static TList<UpcomTransactions> Fill(IDataReader reader, TList<UpcomTransactions> rows, int start, int pageLength)
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
				
				RTStockData.Entities.UpcomTransactions c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("UpcomTransactions")
					.Append("|").Append((System.Int64)reader[((int)UpcomTransactionsColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<UpcomTransactions>(
					key.ToString(), // EntityTrackingKey
					"UpcomTransactions",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new RTStockData.Entities.UpcomTransactions();
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
					c.Id = (System.Int64)reader[((int)UpcomTransactionsColumn.Id - 1)];
					c.TradeDate = (reader.IsDBNull(((int)UpcomTransactionsColumn.TradeDate - 1)))?null:(System.DateTime?)reader[((int)UpcomTransactionsColumn.TradeDate - 1)];
					c.StockSymbol = (reader.IsDBNull(((int)UpcomTransactionsColumn.StockSymbol - 1)))?null:(System.String)reader[((int)UpcomTransactionsColumn.StockSymbol - 1)];
					c.Price = (reader.IsDBNull(((int)UpcomTransactionsColumn.Price - 1)))?null:(System.Int32?)reader[((int)UpcomTransactionsColumn.Price - 1)];
					c.Vol = (reader.IsDBNull(((int)UpcomTransactionsColumn.Vol - 1)))?null:(System.Int32?)reader[((int)UpcomTransactionsColumn.Vol - 1)];
					c.Val = (reader.IsDBNull(((int)UpcomTransactionsColumn.Val - 1)))?null:(System.Int64?)reader[((int)UpcomTransactionsColumn.Val - 1)];
					c.AccumulatedVol = (reader.IsDBNull(((int)UpcomTransactionsColumn.AccumulatedVol - 1)))?null:(System.Int32?)reader[((int)UpcomTransactionsColumn.AccumulatedVol - 1)];
					c.AccumulatedVal = (reader.IsDBNull(((int)UpcomTransactionsColumn.AccumulatedVal - 1)))?null:(System.Int64?)reader[((int)UpcomTransactionsColumn.AccumulatedVal - 1)];
					c.Highest = (reader.IsDBNull(((int)UpcomTransactionsColumn.Highest - 1)))?null:(System.Int32?)reader[((int)UpcomTransactionsColumn.Highest - 1)];
					c.Lowest = (reader.IsDBNull(((int)UpcomTransactionsColumn.Lowest - 1)))?null:(System.Int32?)reader[((int)UpcomTransactionsColumn.Lowest - 1)];
					c.Time = (reader.IsDBNull(((int)UpcomTransactionsColumn.Time - 1)))?null:(System.Int32?)reader[((int)UpcomTransactionsColumn.Time - 1)];
					c.Side = (reader.IsDBNull(((int)UpcomTransactionsColumn.Side - 1)))?null:(System.String)reader[((int)UpcomTransactionsColumn.Side - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="RTStockData.Entities.UpcomTransactions"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="RTStockData.Entities.UpcomTransactions"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, RTStockData.Entities.UpcomTransactions entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int64)reader[((int)UpcomTransactionsColumn.Id - 1)];
			entity.TradeDate = (reader.IsDBNull(((int)UpcomTransactionsColumn.TradeDate - 1)))?null:(System.DateTime?)reader[((int)UpcomTransactionsColumn.TradeDate - 1)];
			entity.StockSymbol = (reader.IsDBNull(((int)UpcomTransactionsColumn.StockSymbol - 1)))?null:(System.String)reader[((int)UpcomTransactionsColumn.StockSymbol - 1)];
			entity.Price = (reader.IsDBNull(((int)UpcomTransactionsColumn.Price - 1)))?null:(System.Int32?)reader[((int)UpcomTransactionsColumn.Price - 1)];
			entity.Vol = (reader.IsDBNull(((int)UpcomTransactionsColumn.Vol - 1)))?null:(System.Int32?)reader[((int)UpcomTransactionsColumn.Vol - 1)];
			entity.Val = (reader.IsDBNull(((int)UpcomTransactionsColumn.Val - 1)))?null:(System.Int64?)reader[((int)UpcomTransactionsColumn.Val - 1)];
			entity.AccumulatedVol = (reader.IsDBNull(((int)UpcomTransactionsColumn.AccumulatedVol - 1)))?null:(System.Int32?)reader[((int)UpcomTransactionsColumn.AccumulatedVol - 1)];
			entity.AccumulatedVal = (reader.IsDBNull(((int)UpcomTransactionsColumn.AccumulatedVal - 1)))?null:(System.Int64?)reader[((int)UpcomTransactionsColumn.AccumulatedVal - 1)];
			entity.Highest = (reader.IsDBNull(((int)UpcomTransactionsColumn.Highest - 1)))?null:(System.Int32?)reader[((int)UpcomTransactionsColumn.Highest - 1)];
			entity.Lowest = (reader.IsDBNull(((int)UpcomTransactionsColumn.Lowest - 1)))?null:(System.Int32?)reader[((int)UpcomTransactionsColumn.Lowest - 1)];
			entity.Time = (reader.IsDBNull(((int)UpcomTransactionsColumn.Time - 1)))?null:(System.Int32?)reader[((int)UpcomTransactionsColumn.Time - 1)];
			entity.Side = (reader.IsDBNull(((int)UpcomTransactionsColumn.Side - 1)))?null:(System.String)reader[((int)UpcomTransactionsColumn.Side - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="RTStockData.Entities.UpcomTransactions"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="RTStockData.Entities.UpcomTransactions"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, RTStockData.Entities.UpcomTransactions entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int64)dataRow["id"];
			entity.TradeDate = Convert.IsDBNull(dataRow["TradeDate"]) ? null : (System.DateTime?)dataRow["TradeDate"];
			entity.StockSymbol = Convert.IsDBNull(dataRow["StockSymbol"]) ? null : (System.String)dataRow["StockSymbol"];
			entity.Price = Convert.IsDBNull(dataRow["Price"]) ? null : (System.Int32?)dataRow["Price"];
			entity.Vol = Convert.IsDBNull(dataRow["Vol"]) ? null : (System.Int32?)dataRow["Vol"];
			entity.Val = Convert.IsDBNull(dataRow["Val"]) ? null : (System.Int64?)dataRow["Val"];
			entity.AccumulatedVol = Convert.IsDBNull(dataRow["AccumulatedVol"]) ? null : (System.Int32?)dataRow["AccumulatedVol"];
			entity.AccumulatedVal = Convert.IsDBNull(dataRow["AccumulatedVal"]) ? null : (System.Int64?)dataRow["AccumulatedVal"];
			entity.Highest = Convert.IsDBNull(dataRow["Highest"]) ? null : (System.Int32?)dataRow["Highest"];
			entity.Lowest = Convert.IsDBNull(dataRow["Lowest"]) ? null : (System.Int32?)dataRow["Lowest"];
			entity.Time = Convert.IsDBNull(dataRow["Time"]) ? null : (System.Int32?)dataRow["Time"];
			entity.Side = Convert.IsDBNull(dataRow["Side"]) ? null : (System.String)dataRow["Side"];
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
		/// <param name="entity">The <see cref="RTStockData.Entities.UpcomTransactions"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">RTStockData.Entities.UpcomTransactions Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, RTStockData.Entities.UpcomTransactions entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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
		/// Deep Save the entire object graph of the RTStockData.Entities.UpcomTransactions object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">RTStockData.Entities.UpcomTransactions instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">RTStockData.Entities.UpcomTransactions Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, RTStockData.Entities.UpcomTransactions entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
	#region UpcomTransactionsChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>RTStockData.Entities.UpcomTransactions</c>
	///</summary>
	public enum UpcomTransactionsChildEntityTypes
	{
	}
	
	#endregion UpcomTransactionsChildEntityTypes
	
	#region UpcomTransactionsFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;UpcomTransactionsColumn&gt;"/> class
	/// that is used exclusively with a <see cref="UpcomTransactions"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class UpcomTransactionsFilterBuilder : SqlFilterBuilder<UpcomTransactionsColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the UpcomTransactionsFilterBuilder class.
		/// </summary>
		public UpcomTransactionsFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the UpcomTransactionsFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public UpcomTransactionsFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the UpcomTransactionsFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public UpcomTransactionsFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion UpcomTransactionsFilterBuilder
	
	#region UpcomTransactionsParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;UpcomTransactionsColumn&gt;"/> class
	/// that is used exclusively with a <see cref="UpcomTransactions"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class UpcomTransactionsParameterBuilder : ParameterizedSqlFilterBuilder<UpcomTransactionsColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the UpcomTransactionsParameterBuilder class.
		/// </summary>
		public UpcomTransactionsParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the UpcomTransactionsParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public UpcomTransactionsParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the UpcomTransactionsParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public UpcomTransactionsParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion UpcomTransactionsParameterBuilder
	
	#region UpcomTransactionsSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;UpcomTransactionsColumn&gt;"/> class
	/// that is used exclusively with a <see cref="UpcomTransactions"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class UpcomTransactionsSortBuilder : SqlSortBuilder<UpcomTransactionsColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the UpcomTransactionsSqlSortBuilder class.
		/// </summary>
		public UpcomTransactionsSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion UpcomTransactionsSortBuilder
	
} // end namespace