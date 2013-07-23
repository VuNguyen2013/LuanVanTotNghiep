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
	/// This class is the base class for any <see cref="NearestWorkingDatesProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class NearestWorkingDatesProviderBaseCore : EntityProviderBase<RTStockData.Entities.NearestWorkingDates, RTStockData.Entities.NearestWorkingDatesKey>
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
		public override bool Delete(TransactionManager transactionManager, RTStockData.Entities.NearestWorkingDatesKey key)
		{
			return Delete(transactionManager, key.MarketId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_marketId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Int32 _marketId)
		{
			return Delete(null, _marketId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_marketId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Int32 _marketId);		
		
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
		public override RTStockData.Entities.NearestWorkingDates Get(TransactionManager transactionManager, RTStockData.Entities.NearestWorkingDatesKey key, int start, int pageLength)
		{
			return GetByMarketId(transactionManager, key.MarketId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_NearestWorkingDates index.
		/// </summary>
		/// <param name="_marketId"></param>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.NearestWorkingDates"/> class.</returns>
		public RTStockData.Entities.NearestWorkingDates GetByMarketId(System.Int32 _marketId)
		{
			int count = -1;
			return GetByMarketId(null,_marketId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_NearestWorkingDates index.
		/// </summary>
		/// <param name="_marketId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.NearestWorkingDates"/> class.</returns>
		public RTStockData.Entities.NearestWorkingDates GetByMarketId(System.Int32 _marketId, int start, int pageLength)
		{
			int count = -1;
			return GetByMarketId(null, _marketId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_NearestWorkingDates index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_marketId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.NearestWorkingDates"/> class.</returns>
		public RTStockData.Entities.NearestWorkingDates GetByMarketId(TransactionManager transactionManager, System.Int32 _marketId)
		{
			int count = -1;
			return GetByMarketId(transactionManager, _marketId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_NearestWorkingDates index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_marketId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.NearestWorkingDates"/> class.</returns>
		public RTStockData.Entities.NearestWorkingDates GetByMarketId(TransactionManager transactionManager, System.Int32 _marketId, int start, int pageLength)
		{
			int count = -1;
			return GetByMarketId(transactionManager, _marketId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_NearestWorkingDates index.
		/// </summary>
		/// <param name="_marketId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.NearestWorkingDates"/> class.</returns>
		public RTStockData.Entities.NearestWorkingDates GetByMarketId(System.Int32 _marketId, int start, int pageLength, out int count)
		{
			return GetByMarketId(null, _marketId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_NearestWorkingDates index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_marketId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.NearestWorkingDates"/> class.</returns>
		public abstract RTStockData.Entities.NearestWorkingDates GetByMarketId(TransactionManager transactionManager, System.Int32 _marketId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;NearestWorkingDates&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;NearestWorkingDates&gt;"/></returns>
		public static TList<NearestWorkingDates> Fill(IDataReader reader, TList<NearestWorkingDates> rows, int start, int pageLength)
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
				
				RTStockData.Entities.NearestWorkingDates c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("NearestWorkingDates")
					.Append("|").Append((System.Int32)reader[((int)NearestWorkingDatesColumn.MarketId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<NearestWorkingDates>(
					key.ToString(), // EntityTrackingKey
					"NearestWorkingDates",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new RTStockData.Entities.NearestWorkingDates();
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
					c.MarketId = (System.Int32)reader[((int)NearestWorkingDatesColumn.MarketId - 1)];
					c.OriginalMarketId = c.MarketId;
					c.T = (reader.IsDBNull(((int)NearestWorkingDatesColumn.T - 1)))?null:(System.DateTime?)reader[((int)NearestWorkingDatesColumn.T - 1)];
					c.T1 = (reader.IsDBNull(((int)NearestWorkingDatesColumn.T1 - 1)))?null:(System.DateTime?)reader[((int)NearestWorkingDatesColumn.T1 - 1)];
					c.T2 = (reader.IsDBNull(((int)NearestWorkingDatesColumn.T2 - 1)))?null:(System.DateTime?)reader[((int)NearestWorkingDatesColumn.T2 - 1)];
					c.T3 = (reader.IsDBNull(((int)NearestWorkingDatesColumn.T3 - 1)))?null:(System.DateTime?)reader[((int)NearestWorkingDatesColumn.T3 - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="RTStockData.Entities.NearestWorkingDates"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="RTStockData.Entities.NearestWorkingDates"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, RTStockData.Entities.NearestWorkingDates entity)
		{
			if (!reader.Read()) return;
			
			entity.MarketId = (System.Int32)reader[((int)NearestWorkingDatesColumn.MarketId - 1)];
			entity.OriginalMarketId = (System.Int32)reader["MarketId"];
			entity.T = (reader.IsDBNull(((int)NearestWorkingDatesColumn.T - 1)))?null:(System.DateTime?)reader[((int)NearestWorkingDatesColumn.T - 1)];
			entity.T1 = (reader.IsDBNull(((int)NearestWorkingDatesColumn.T1 - 1)))?null:(System.DateTime?)reader[((int)NearestWorkingDatesColumn.T1 - 1)];
			entity.T2 = (reader.IsDBNull(((int)NearestWorkingDatesColumn.T2 - 1)))?null:(System.DateTime?)reader[((int)NearestWorkingDatesColumn.T2 - 1)];
			entity.T3 = (reader.IsDBNull(((int)NearestWorkingDatesColumn.T3 - 1)))?null:(System.DateTime?)reader[((int)NearestWorkingDatesColumn.T3 - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="RTStockData.Entities.NearestWorkingDates"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="RTStockData.Entities.NearestWorkingDates"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, RTStockData.Entities.NearestWorkingDates entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.MarketId = (System.Int32)dataRow["MarketId"];
			entity.OriginalMarketId = (System.Int32)dataRow["MarketId"];
			entity.T = Convert.IsDBNull(dataRow["T"]) ? null : (System.DateTime?)dataRow["T"];
			entity.T1 = Convert.IsDBNull(dataRow["T1"]) ? null : (System.DateTime?)dataRow["T1"];
			entity.T2 = Convert.IsDBNull(dataRow["T2"]) ? null : (System.DateTime?)dataRow["T2"];
			entity.T3 = Convert.IsDBNull(dataRow["T3"]) ? null : (System.DateTime?)dataRow["T3"];
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
		/// <param name="entity">The <see cref="RTStockData.Entities.NearestWorkingDates"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">RTStockData.Entities.NearestWorkingDates Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, RTStockData.Entities.NearestWorkingDates entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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
		/// Deep Save the entire object graph of the RTStockData.Entities.NearestWorkingDates object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">RTStockData.Entities.NearestWorkingDates instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">RTStockData.Entities.NearestWorkingDates Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, RTStockData.Entities.NearestWorkingDates entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
	#region NearestWorkingDatesChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>RTStockData.Entities.NearestWorkingDates</c>
	///</summary>
	public enum NearestWorkingDatesChildEntityTypes
	{
	}
	
	#endregion NearestWorkingDatesChildEntityTypes
	
	#region NearestWorkingDatesFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;NearestWorkingDatesColumn&gt;"/> class
	/// that is used exclusively with a <see cref="NearestWorkingDates"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class NearestWorkingDatesFilterBuilder : SqlFilterBuilder<NearestWorkingDatesColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the NearestWorkingDatesFilterBuilder class.
		/// </summary>
		public NearestWorkingDatesFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the NearestWorkingDatesFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public NearestWorkingDatesFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the NearestWorkingDatesFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public NearestWorkingDatesFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion NearestWorkingDatesFilterBuilder
	
	#region NearestWorkingDatesParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;NearestWorkingDatesColumn&gt;"/> class
	/// that is used exclusively with a <see cref="NearestWorkingDates"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class NearestWorkingDatesParameterBuilder : ParameterizedSqlFilterBuilder<NearestWorkingDatesColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the NearestWorkingDatesParameterBuilder class.
		/// </summary>
		public NearestWorkingDatesParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the NearestWorkingDatesParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public NearestWorkingDatesParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the NearestWorkingDatesParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public NearestWorkingDatesParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion NearestWorkingDatesParameterBuilder
	
	#region NearestWorkingDatesSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;NearestWorkingDatesColumn&gt;"/> class
	/// that is used exclusively with a <see cref="NearestWorkingDates"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class NearestWorkingDatesSortBuilder : SqlSortBuilder<NearestWorkingDatesColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the NearestWorkingDatesSqlSortBuilder class.
		/// </summary>
		public NearestWorkingDatesSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion NearestWorkingDatesSortBuilder
	
} // end namespace
