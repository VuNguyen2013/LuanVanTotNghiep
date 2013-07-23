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
	/// This class is the base class for any <see cref="BasketInfoProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class BasketInfoProviderBaseCore : EntityProviderBase<RTStockData.Entities.BasketInfo, RTStockData.Entities.BasketInfoKey>
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
		public override bool Delete(TransactionManager transactionManager, RTStockData.Entities.BasketInfoKey key)
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
		public override RTStockData.Entities.BasketInfo Get(TransactionManager transactionManager, RTStockData.Entities.BasketInfoKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_BasketInfo index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.BasketInfo"/> class.</returns>
		public RTStockData.Entities.BasketInfo GetById(System.Int64 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_BasketInfo index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.BasketInfo"/> class.</returns>
		public RTStockData.Entities.BasketInfo GetById(System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_BasketInfo index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.BasketInfo"/> class.</returns>
		public RTStockData.Entities.BasketInfo GetById(TransactionManager transactionManager, System.Int64 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_BasketInfo index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.BasketInfo"/> class.</returns>
		public RTStockData.Entities.BasketInfo GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_BasketInfo index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.BasketInfo"/> class.</returns>
		public RTStockData.Entities.BasketInfo GetById(System.Int64 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_BasketInfo index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.BasketInfo"/> class.</returns>
		public abstract RTStockData.Entities.BasketInfo GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;BasketInfo&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;BasketInfo&gt;"/></returns>
		public static TList<BasketInfo> Fill(IDataReader reader, TList<BasketInfo> rows, int start, int pageLength)
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
				
				RTStockData.Entities.BasketInfo c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("BasketInfo")
					.Append("|").Append((System.Int64)reader[((int)BasketInfoColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<BasketInfo>(
					key.ToString(), // EntityTrackingKey
					"BasketInfo",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new RTStockData.Entities.BasketInfo();
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
					c.Id = (System.Int64)reader[((int)BasketInfoColumn.Id - 1)];
					c.BasketId = (reader.IsDBNull(((int)BasketInfoColumn.BasketId - 1)))?null:(System.Decimal?)reader[((int)BasketInfoColumn.BasketId - 1)];
					c.IndexId = (reader.IsDBNull(((int)BasketInfoColumn.IndexId - 1)))?null:(System.Decimal?)reader[((int)BasketInfoColumn.IndexId - 1)];
					c.IndexCode = (reader.IsDBNull(((int)BasketInfoColumn.IndexCode - 1)))?null:(System.String)reader[((int)BasketInfoColumn.IndexCode - 1)];
					c.StockCode = (reader.IsDBNull(((int)BasketInfoColumn.StockCode - 1)))?null:(System.String)reader[((int)BasketInfoColumn.StockCode - 1)];
					c.TotalCeilingQtty = (reader.IsDBNull(((int)BasketInfoColumn.TotalCeilingQtty - 1)))?null:(System.Decimal?)reader[((int)BasketInfoColumn.TotalCeilingQtty - 1)];
					c.AddDate = (reader.IsDBNull(((int)BasketInfoColumn.AddDate - 1)))?null:(System.DateTime?)reader[((int)BasketInfoColumn.AddDate - 1)];
					c.CurrentWeight = (reader.IsDBNull(((int)BasketInfoColumn.CurrentWeight - 1)))?null:(System.Decimal?)reader[((int)BasketInfoColumn.CurrentWeight - 1)];
					c.TradeDate = (reader.IsDBNull(((int)BasketInfoColumn.TradeDate - 1)))?null:(System.DateTime?)reader[((int)BasketInfoColumn.TradeDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="RTStockData.Entities.BasketInfo"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="RTStockData.Entities.BasketInfo"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, RTStockData.Entities.BasketInfo entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int64)reader[((int)BasketInfoColumn.Id - 1)];
			entity.BasketId = (reader.IsDBNull(((int)BasketInfoColumn.BasketId - 1)))?null:(System.Decimal?)reader[((int)BasketInfoColumn.BasketId - 1)];
			entity.IndexId = (reader.IsDBNull(((int)BasketInfoColumn.IndexId - 1)))?null:(System.Decimal?)reader[((int)BasketInfoColumn.IndexId - 1)];
			entity.IndexCode = (reader.IsDBNull(((int)BasketInfoColumn.IndexCode - 1)))?null:(System.String)reader[((int)BasketInfoColumn.IndexCode - 1)];
			entity.StockCode = (reader.IsDBNull(((int)BasketInfoColumn.StockCode - 1)))?null:(System.String)reader[((int)BasketInfoColumn.StockCode - 1)];
			entity.TotalCeilingQtty = (reader.IsDBNull(((int)BasketInfoColumn.TotalCeilingQtty - 1)))?null:(System.Decimal?)reader[((int)BasketInfoColumn.TotalCeilingQtty - 1)];
			entity.AddDate = (reader.IsDBNull(((int)BasketInfoColumn.AddDate - 1)))?null:(System.DateTime?)reader[((int)BasketInfoColumn.AddDate - 1)];
			entity.CurrentWeight = (reader.IsDBNull(((int)BasketInfoColumn.CurrentWeight - 1)))?null:(System.Decimal?)reader[((int)BasketInfoColumn.CurrentWeight - 1)];
			entity.TradeDate = (reader.IsDBNull(((int)BasketInfoColumn.TradeDate - 1)))?null:(System.DateTime?)reader[((int)BasketInfoColumn.TradeDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="RTStockData.Entities.BasketInfo"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="RTStockData.Entities.BasketInfo"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, RTStockData.Entities.BasketInfo entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int64)dataRow["ID"];
			entity.BasketId = Convert.IsDBNull(dataRow["BasketId"]) ? null : (System.Decimal?)dataRow["BasketId"];
			entity.IndexId = Convert.IsDBNull(dataRow["IndexId"]) ? null : (System.Decimal?)dataRow["IndexId"];
			entity.IndexCode = Convert.IsDBNull(dataRow["IndexCode"]) ? null : (System.String)dataRow["IndexCode"];
			entity.StockCode = Convert.IsDBNull(dataRow["StockCode"]) ? null : (System.String)dataRow["StockCode"];
			entity.TotalCeilingQtty = Convert.IsDBNull(dataRow["TotalCeilingQtty"]) ? null : (System.Decimal?)dataRow["TotalCeilingQtty"];
			entity.AddDate = Convert.IsDBNull(dataRow["AddDate"]) ? null : (System.DateTime?)dataRow["AddDate"];
			entity.CurrentWeight = Convert.IsDBNull(dataRow["CurrentWeight"]) ? null : (System.Decimal?)dataRow["CurrentWeight"];
			entity.TradeDate = Convert.IsDBNull(dataRow["TradeDate"]) ? null : (System.DateTime?)dataRow["TradeDate"];
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
		/// <param name="entity">The <see cref="RTStockData.Entities.BasketInfo"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">RTStockData.Entities.BasketInfo Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, RTStockData.Entities.BasketInfo entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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
		/// Deep Save the entire object graph of the RTStockData.Entities.BasketInfo object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">RTStockData.Entities.BasketInfo instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">RTStockData.Entities.BasketInfo Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, RTStockData.Entities.BasketInfo entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
	#region BasketInfoChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>RTStockData.Entities.BasketInfo</c>
	///</summary>
	public enum BasketInfoChildEntityTypes
	{
	}
	
	#endregion BasketInfoChildEntityTypes
	
	#region BasketInfoFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;BasketInfoColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BasketInfo"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BasketInfoFilterBuilder : SqlFilterBuilder<BasketInfoColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BasketInfoFilterBuilder class.
		/// </summary>
		public BasketInfoFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the BasketInfoFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BasketInfoFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BasketInfoFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BasketInfoFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BasketInfoFilterBuilder
	
	#region BasketInfoParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;BasketInfoColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BasketInfo"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BasketInfoParameterBuilder : ParameterizedSqlFilterBuilder<BasketInfoColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BasketInfoParameterBuilder class.
		/// </summary>
		public BasketInfoParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the BasketInfoParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BasketInfoParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BasketInfoParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BasketInfoParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BasketInfoParameterBuilder
	
	#region BasketInfoSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;BasketInfoColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BasketInfo"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class BasketInfoSortBuilder : SqlSortBuilder<BasketInfoColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BasketInfoSqlSortBuilder class.
		/// </summary>
		public BasketInfoSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion BasketInfoSortBuilder
	
} // end namespace
