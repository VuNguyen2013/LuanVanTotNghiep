#region Using directives

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
	/// This class is the base class for any <see cref="LeProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class LeProviderBaseCore : EntityProviderBase<RTStockData.Entities.Le, RTStockData.Entities.LeKey>
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
		public override bool Delete(TransactionManager transactionManager, RTStockData.Entities.LeKey key)
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
		public override RTStockData.Entities.Le Get(TransactionManager transactionManager, RTStockData.Entities.LeKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_le index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.Le"/> class.</returns>
		public RTStockData.Entities.Le GetById(System.Int64 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_le index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.Le"/> class.</returns>
		public RTStockData.Entities.Le GetById(System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_le index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.Le"/> class.</returns>
		public RTStockData.Entities.Le GetById(TransactionManager transactionManager, System.Int64 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_le index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.Le"/> class.</returns>
		public RTStockData.Entities.Le GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_le index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.Le"/> class.</returns>
		public RTStockData.Entities.Le GetById(System.Int64 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_le index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.Le"/> class.</returns>
		public abstract RTStockData.Entities.Le GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Le&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Le&gt;"/></returns>
		public static TList<Le> Fill(IDataReader reader, TList<Le> rows, int start, int pageLength)
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
				
				RTStockData.Entities.Le c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Le")
					.Append("|").Append((System.Int64)reader[((int)LeColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Le>(
					key.ToString(), // EntityTrackingKey
					"Le",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new RTStockData.Entities.Le();
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
					c.Id = (System.Int64)reader[((int)LeColumn.Id - 1)];
					c.TradeDate = (reader.IsDBNull(((int)LeColumn.TradeDate - 1)))?null:(System.DateTime?)reader[((int)LeColumn.TradeDate - 1)];
					c.StockNo = (reader.IsDBNull(((int)LeColumn.StockNo - 1)))?null:(System.Int16?)reader[((int)LeColumn.StockNo - 1)];
					c.Price = (reader.IsDBNull(((int)LeColumn.Price - 1)))?null:(System.Int64?)reader[((int)LeColumn.Price - 1)];
					c.AccumulatedVol = (reader.IsDBNull(((int)LeColumn.AccumulatedVol - 1)))?null:(System.Int64?)reader[((int)LeColumn.AccumulatedVol - 1)];
					c.AccumulatedVal = (reader.IsDBNull(((int)LeColumn.AccumulatedVal - 1)))?null:(System.Int64?)reader[((int)LeColumn.AccumulatedVal - 1)];
					c.Highest = (reader.IsDBNull(((int)LeColumn.Highest - 1)))?null:(System.Int64?)reader[((int)LeColumn.Highest - 1)];
					c.Lowest = (reader.IsDBNull(((int)LeColumn.Lowest - 1)))?null:(System.Int64?)reader[((int)LeColumn.Lowest - 1)];
					c.Turn = (reader.IsDBNull(((int)LeColumn.Turn - 1)))?null:(System.Int64?)reader[((int)LeColumn.Turn - 1)];
					c.Time = (reader.IsDBNull(((int)LeColumn.Time - 1)))?null:(System.Int64?)reader[((int)LeColumn.Time - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="RTStockData.Entities.Le"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="RTStockData.Entities.Le"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, RTStockData.Entities.Le entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int64)reader[((int)LeColumn.Id - 1)];
			entity.TradeDate = (reader.IsDBNull(((int)LeColumn.TradeDate - 1)))?null:(System.DateTime?)reader[((int)LeColumn.TradeDate - 1)];
			entity.StockNo = (reader.IsDBNull(((int)LeColumn.StockNo - 1)))?null:(System.Int16?)reader[((int)LeColumn.StockNo - 1)];
			entity.Price = (reader.IsDBNull(((int)LeColumn.Price - 1)))?null:(System.Int64?)reader[((int)LeColumn.Price - 1)];
			entity.AccumulatedVol = (reader.IsDBNull(((int)LeColumn.AccumulatedVol - 1)))?null:(System.Int64?)reader[((int)LeColumn.AccumulatedVol - 1)];
			entity.AccumulatedVal = (reader.IsDBNull(((int)LeColumn.AccumulatedVal - 1)))?null:(System.Int64?)reader[((int)LeColumn.AccumulatedVal - 1)];
			entity.Highest = (reader.IsDBNull(((int)LeColumn.Highest - 1)))?null:(System.Int64?)reader[((int)LeColumn.Highest - 1)];
			entity.Lowest = (reader.IsDBNull(((int)LeColumn.Lowest - 1)))?null:(System.Int64?)reader[((int)LeColumn.Lowest - 1)];
			entity.Turn = (reader.IsDBNull(((int)LeColumn.Turn - 1)))?null:(System.Int64?)reader[((int)LeColumn.Turn - 1)];
			entity.Time = (reader.IsDBNull(((int)LeColumn.Time - 1)))?null:(System.Int64?)reader[((int)LeColumn.Time - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="RTStockData.Entities.Le"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="RTStockData.Entities.Le"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, RTStockData.Entities.Le entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int64)dataRow["id"];
			entity.TradeDate = Convert.IsDBNull(dataRow["TradeDate"]) ? null : (System.DateTime?)dataRow["TradeDate"];
			entity.StockNo = Convert.IsDBNull(dataRow["StockNo"]) ? null : (System.Int16?)dataRow["StockNo"];
			entity.Price = Convert.IsDBNull(dataRow["Price"]) ? null : (System.Int64?)dataRow["Price"];
			entity.AccumulatedVol = Convert.IsDBNull(dataRow["AccumulatedVol"]) ? null : (System.Int64?)dataRow["AccumulatedVol"];
			entity.AccumulatedVal = Convert.IsDBNull(dataRow["AccumulatedVal"]) ? null : (System.Int64?)dataRow["AccumulatedVal"];
			entity.Highest = Convert.IsDBNull(dataRow["Highest"]) ? null : (System.Int64?)dataRow["Highest"];
			entity.Lowest = Convert.IsDBNull(dataRow["Lowest"]) ? null : (System.Int64?)dataRow["Lowest"];
			entity.Turn = Convert.IsDBNull(dataRow["Turn"]) ? null : (System.Int64?)dataRow["Turn"];
			entity.Time = Convert.IsDBNull(dataRow["Time"]) ? null : (System.Int64?)dataRow["Time"];
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
		/// <param name="entity">The <see cref="RTStockData.Entities.Le"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">RTStockData.Entities.Le Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, RTStockData.Entities.Le entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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
		/// Deep Save the entire object graph of the RTStockData.Entities.Le object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">RTStockData.Entities.Le instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">RTStockData.Entities.Le Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, RTStockData.Entities.Le entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
	#region LeChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>RTStockData.Entities.Le</c>
	///</summary>
	public enum LeChildEntityTypes
	{
	}
	
	#endregion LeChildEntityTypes
	
	#region LeFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;LeColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Le"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class LeFilterBuilder : SqlFilterBuilder<LeColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the LeFilterBuilder class.
		/// </summary>
		public LeFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the LeFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public LeFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the LeFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public LeFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion LeFilterBuilder
	
	#region LeParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;LeColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Le"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class LeParameterBuilder : ParameterizedSqlFilterBuilder<LeColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the LeParameterBuilder class.
		/// </summary>
		public LeParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the LeParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public LeParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the LeParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public LeParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion LeParameterBuilder
	
	#region LeSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;LeColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Le"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class LeSortBuilder : SqlSortBuilder<LeColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the LeSqlSortBuilder class.
		/// </summary>
		public LeSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion LeSortBuilder
	
} // end namespace
