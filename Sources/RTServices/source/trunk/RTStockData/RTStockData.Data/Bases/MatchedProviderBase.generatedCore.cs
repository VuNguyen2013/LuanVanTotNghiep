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
	/// This class is the base class for any <see cref="MatchedProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class MatchedProviderBaseCore : EntityProviderBase<RTStockData.Entities.Matched, RTStockData.Entities.MatchedKey>
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
		public override bool Delete(TransactionManager transactionManager, RTStockData.Entities.MatchedKey key)
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
		public override RTStockData.Entities.Matched Get(TransactionManager transactionManager, RTStockData.Entities.MatchedKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Matched index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.Matched"/> class.</returns>
		public RTStockData.Entities.Matched GetById(System.Int64 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Matched index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.Matched"/> class.</returns>
		public RTStockData.Entities.Matched GetById(System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Matched index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.Matched"/> class.</returns>
		public RTStockData.Entities.Matched GetById(TransactionManager transactionManager, System.Int64 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Matched index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.Matched"/> class.</returns>
		public RTStockData.Entities.Matched GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Matched index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.Matched"/> class.</returns>
		public RTStockData.Entities.Matched GetById(System.Int64 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Matched index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.Matched"/> class.</returns>
		public abstract RTStockData.Entities.Matched GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Matched&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Matched&gt;"/></returns>
		public static TList<Matched> Fill(IDataReader reader, TList<Matched> rows, int start, int pageLength)
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
				
				RTStockData.Entities.Matched c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Matched")
					.Append("|").Append((System.Int64)reader[((int)MatchedColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Matched>(
					key.ToString(), // EntityTrackingKey
					"Matched",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new RTStockData.Entities.Matched();
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
					c.Id = (System.Int64)reader[((int)MatchedColumn.Id - 1)];
					c.Code = (reader.IsDBNull(((int)MatchedColumn.Code - 1)))?null:(System.String)reader[((int)MatchedColumn.Code - 1)];
					c.TradedDate = (reader.IsDBNull(((int)MatchedColumn.TradedDate - 1)))?null:(System.DateTime?)reader[((int)MatchedColumn.TradedDate - 1)];
					c.Ceiling = (reader.IsDBNull(((int)MatchedColumn.Ceiling - 1)))?null:(System.Double?)reader[((int)MatchedColumn.Ceiling - 1)];
					c.Floor = (reader.IsDBNull(((int)MatchedColumn.Floor - 1)))?null:(System.Double?)reader[((int)MatchedColumn.Floor - 1)];
					c.RefPrice = (reader.IsDBNull(((int)MatchedColumn.RefPrice - 1)))?null:(System.Double?)reader[((int)MatchedColumn.RefPrice - 1)];
					c.ClosePrice = (reader.IsDBNull(((int)MatchedColumn.ClosePrice - 1)))?null:(System.Double?)reader[((int)MatchedColumn.ClosePrice - 1)];
					c.Change = (reader.IsDBNull(((int)MatchedColumn.Change - 1)))?null:(System.Double?)reader[((int)MatchedColumn.Change - 1)];
					c.Percent = (reader.IsDBNull(((int)MatchedColumn.Percent - 1)))?null:(System.Double?)reader[((int)MatchedColumn.Percent - 1)];
					c.Volume = (reader.IsDBNull(((int)MatchedColumn.Volume - 1)))?null:(System.Double?)reader[((int)MatchedColumn.Volume - 1)];
					c.PutThrough = (reader.IsDBNull(((int)MatchedColumn.PutThrough - 1)))?null:(System.Double?)reader[((int)MatchedColumn.PutThrough - 1)];
					c.ForeignBuy = (reader.IsDBNull(((int)MatchedColumn.ForeignBuy - 1)))?null:(System.Double?)reader[((int)MatchedColumn.ForeignBuy - 1)];
					c.ForeignSell = (reader.IsDBNull(((int)MatchedColumn.ForeignSell - 1)))?null:(System.Double?)reader[((int)MatchedColumn.ForeignSell - 1)];
					c.OpenPrice = (reader.IsDBNull(((int)MatchedColumn.OpenPrice - 1)))?null:(System.Double?)reader[((int)MatchedColumn.OpenPrice - 1)];
					c.HighestPrice = (reader.IsDBNull(((int)MatchedColumn.HighestPrice - 1)))?null:(System.Double?)reader[((int)MatchedColumn.HighestPrice - 1)];
					c.LowestPrice = (reader.IsDBNull(((int)MatchedColumn.LowestPrice - 1)))?null:(System.Double?)reader[((int)MatchedColumn.LowestPrice - 1)];
					c.MarketId = (reader.IsDBNull(((int)MatchedColumn.MarketId - 1)))?null:(System.String)reader[((int)MatchedColumn.MarketId - 1)];
					c.Value = (reader.IsDBNull(((int)MatchedColumn.Value - 1)))?null:(System.Double?)reader[((int)MatchedColumn.Value - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="RTStockData.Entities.Matched"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="RTStockData.Entities.Matched"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, RTStockData.Entities.Matched entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int64)reader[((int)MatchedColumn.Id - 1)];
			entity.Code = (reader.IsDBNull(((int)MatchedColumn.Code - 1)))?null:(System.String)reader[((int)MatchedColumn.Code - 1)];
			entity.TradedDate = (reader.IsDBNull(((int)MatchedColumn.TradedDate - 1)))?null:(System.DateTime?)reader[((int)MatchedColumn.TradedDate - 1)];
			entity.Ceiling = (reader.IsDBNull(((int)MatchedColumn.Ceiling - 1)))?null:(System.Double?)reader[((int)MatchedColumn.Ceiling - 1)];
			entity.Floor = (reader.IsDBNull(((int)MatchedColumn.Floor - 1)))?null:(System.Double?)reader[((int)MatchedColumn.Floor - 1)];
			entity.RefPrice = (reader.IsDBNull(((int)MatchedColumn.RefPrice - 1)))?null:(System.Double?)reader[((int)MatchedColumn.RefPrice - 1)];
			entity.ClosePrice = (reader.IsDBNull(((int)MatchedColumn.ClosePrice - 1)))?null:(System.Double?)reader[((int)MatchedColumn.ClosePrice - 1)];
			entity.Change = (reader.IsDBNull(((int)MatchedColumn.Change - 1)))?null:(System.Double?)reader[((int)MatchedColumn.Change - 1)];
			entity.Percent = (reader.IsDBNull(((int)MatchedColumn.Percent - 1)))?null:(System.Double?)reader[((int)MatchedColumn.Percent - 1)];
			entity.Volume = (reader.IsDBNull(((int)MatchedColumn.Volume - 1)))?null:(System.Double?)reader[((int)MatchedColumn.Volume - 1)];
			entity.PutThrough = (reader.IsDBNull(((int)MatchedColumn.PutThrough - 1)))?null:(System.Double?)reader[((int)MatchedColumn.PutThrough - 1)];
			entity.ForeignBuy = (reader.IsDBNull(((int)MatchedColumn.ForeignBuy - 1)))?null:(System.Double?)reader[((int)MatchedColumn.ForeignBuy - 1)];
			entity.ForeignSell = (reader.IsDBNull(((int)MatchedColumn.ForeignSell - 1)))?null:(System.Double?)reader[((int)MatchedColumn.ForeignSell - 1)];
			entity.OpenPrice = (reader.IsDBNull(((int)MatchedColumn.OpenPrice - 1)))?null:(System.Double?)reader[((int)MatchedColumn.OpenPrice - 1)];
			entity.HighestPrice = (reader.IsDBNull(((int)MatchedColumn.HighestPrice - 1)))?null:(System.Double?)reader[((int)MatchedColumn.HighestPrice - 1)];
			entity.LowestPrice = (reader.IsDBNull(((int)MatchedColumn.LowestPrice - 1)))?null:(System.Double?)reader[((int)MatchedColumn.LowestPrice - 1)];
			entity.MarketId = (reader.IsDBNull(((int)MatchedColumn.MarketId - 1)))?null:(System.String)reader[((int)MatchedColumn.MarketId - 1)];
			entity.Value = (reader.IsDBNull(((int)MatchedColumn.Value - 1)))?null:(System.Double?)reader[((int)MatchedColumn.Value - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="RTStockData.Entities.Matched"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="RTStockData.Entities.Matched"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, RTStockData.Entities.Matched entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int64)dataRow["ID"];
			entity.Code = Convert.IsDBNull(dataRow["Code"]) ? null : (System.String)dataRow["Code"];
			entity.TradedDate = Convert.IsDBNull(dataRow["TradedDate"]) ? null : (System.DateTime?)dataRow["TradedDate"];
			entity.Ceiling = Convert.IsDBNull(dataRow["Ceiling"]) ? null : (System.Double?)dataRow["Ceiling"];
			entity.Floor = Convert.IsDBNull(dataRow["Floor"]) ? null : (System.Double?)dataRow["Floor"];
			entity.RefPrice = Convert.IsDBNull(dataRow["RefPrice"]) ? null : (System.Double?)dataRow["RefPrice"];
			entity.ClosePrice = Convert.IsDBNull(dataRow["ClosePrice"]) ? null : (System.Double?)dataRow["ClosePrice"];
			entity.Change = Convert.IsDBNull(dataRow["Change"]) ? null : (System.Double?)dataRow["Change"];
			entity.Percent = Convert.IsDBNull(dataRow["Percent"]) ? null : (System.Double?)dataRow["Percent"];
			entity.Volume = Convert.IsDBNull(dataRow["Volume"]) ? null : (System.Double?)dataRow["Volume"];
			entity.PutThrough = Convert.IsDBNull(dataRow["PutThrough"]) ? null : (System.Double?)dataRow["PutThrough"];
			entity.ForeignBuy = Convert.IsDBNull(dataRow["ForeignBuy"]) ? null : (System.Double?)dataRow["ForeignBuy"];
			entity.ForeignSell = Convert.IsDBNull(dataRow["ForeignSell"]) ? null : (System.Double?)dataRow["ForeignSell"];
			entity.OpenPrice = Convert.IsDBNull(dataRow["OpenPrice"]) ? null : (System.Double?)dataRow["OpenPrice"];
			entity.HighestPrice = Convert.IsDBNull(dataRow["HighestPrice"]) ? null : (System.Double?)dataRow["HighestPrice"];
			entity.LowestPrice = Convert.IsDBNull(dataRow["LowestPrice"]) ? null : (System.Double?)dataRow["LowestPrice"];
			entity.MarketId = Convert.IsDBNull(dataRow["Market_id"]) ? null : (System.String)dataRow["Market_id"];
			entity.Value = Convert.IsDBNull(dataRow["Value"]) ? null : (System.Double?)dataRow["Value"];
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
		/// <param name="entity">The <see cref="RTStockData.Entities.Matched"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">RTStockData.Entities.Matched Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, RTStockData.Entities.Matched entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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
		/// Deep Save the entire object graph of the RTStockData.Entities.Matched object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">RTStockData.Entities.Matched instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">RTStockData.Entities.Matched Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, RTStockData.Entities.Matched entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
	#region MatchedChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>RTStockData.Entities.Matched</c>
	///</summary>
	public enum MatchedChildEntityTypes
	{
	}
	
	#endregion MatchedChildEntityTypes
	
	#region MatchedFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;MatchedColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Matched"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MatchedFilterBuilder : SqlFilterBuilder<MatchedColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MatchedFilterBuilder class.
		/// </summary>
		public MatchedFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the MatchedFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public MatchedFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the MatchedFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public MatchedFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion MatchedFilterBuilder
	
	#region MatchedParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;MatchedColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Matched"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MatchedParameterBuilder : ParameterizedSqlFilterBuilder<MatchedColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MatchedParameterBuilder class.
		/// </summary>
		public MatchedParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the MatchedParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public MatchedParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the MatchedParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public MatchedParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion MatchedParameterBuilder
	
	#region MatchedSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;MatchedColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Matched"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class MatchedSortBuilder : SqlSortBuilder<MatchedColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MatchedSqlSortBuilder class.
		/// </summary>
		public MatchedSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion MatchedSortBuilder
	
} // end namespace
