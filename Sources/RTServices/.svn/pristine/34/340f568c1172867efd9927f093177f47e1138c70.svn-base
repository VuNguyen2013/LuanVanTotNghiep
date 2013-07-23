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
	/// This class is the base class for any <see cref="IndexsProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class IndexsProviderBaseCore : EntityProviderBase<RTStockData.Entities.Indexs, RTStockData.Entities.IndexsKey>
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
		public override bool Delete(TransactionManager transactionManager, RTStockData.Entities.IndexsKey key)
		{
			return Delete(transactionManager, key.VnindexId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_vnindexId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Int32 _vnindexId)
		{
			return Delete(null, _vnindexId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_vnindexId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Int32 _vnindexId);		
		
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
		public override RTStockData.Entities.Indexs Get(TransactionManager transactionManager, RTStockData.Entities.IndexsKey key, int start, int pageLength)
		{
			return GetByVnindexId(transactionManager, key.VnindexId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Indexs index.
		/// </summary>
		/// <param name="_vnindexId"></param>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.Indexs"/> class.</returns>
		public RTStockData.Entities.Indexs GetByVnindexId(System.Int32 _vnindexId)
		{
			int count = -1;
			return GetByVnindexId(null,_vnindexId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Indexs index.
		/// </summary>
		/// <param name="_vnindexId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.Indexs"/> class.</returns>
		public RTStockData.Entities.Indexs GetByVnindexId(System.Int32 _vnindexId, int start, int pageLength)
		{
			int count = -1;
			return GetByVnindexId(null, _vnindexId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Indexs index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_vnindexId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.Indexs"/> class.</returns>
		public RTStockData.Entities.Indexs GetByVnindexId(TransactionManager transactionManager, System.Int32 _vnindexId)
		{
			int count = -1;
			return GetByVnindexId(transactionManager, _vnindexId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Indexs index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_vnindexId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.Indexs"/> class.</returns>
		public RTStockData.Entities.Indexs GetByVnindexId(TransactionManager transactionManager, System.Int32 _vnindexId, int start, int pageLength)
		{
			int count = -1;
			return GetByVnindexId(transactionManager, _vnindexId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Indexs index.
		/// </summary>
		/// <param name="_vnindexId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.Indexs"/> class.</returns>
		public RTStockData.Entities.Indexs GetByVnindexId(System.Int32 _vnindexId, int start, int pageLength, out int count)
		{
			return GetByVnindexId(null, _vnindexId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Indexs index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_vnindexId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.Indexs"/> class.</returns>
		public abstract RTStockData.Entities.Indexs GetByVnindexId(TransactionManager transactionManager, System.Int32 _vnindexId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Indexs&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Indexs&gt;"/></returns>
		public static TList<Indexs> Fill(IDataReader reader, TList<Indexs> rows, int start, int pageLength)
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
				
				RTStockData.Entities.Indexs c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Indexs")
					.Append("|").Append((System.Int32)reader[((int)IndexsColumn.VnindexId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Indexs>(
					key.ToString(), // EntityTrackingKey
					"Indexs",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new RTStockData.Entities.Indexs();
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
					c.VnindexId = (System.Int32)reader[((int)IndexsColumn.VnindexId - 1)];
					c.TradedDate = (reader.IsDBNull(((int)IndexsColumn.TradedDate - 1)))?null:(System.DateTime?)reader[((int)IndexsColumn.TradedDate - 1)];
					c.Open = (reader.IsDBNull(((int)IndexsColumn.Open - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Open - 1)];
					c.Close = (reader.IsDBNull(((int)IndexsColumn.Close - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Close - 1)];
					c.Change = (reader.IsDBNull(((int)IndexsColumn.Change - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Change - 1)];
					c.Unchange = (reader.IsDBNull(((int)IndexsColumn.Unchange - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Unchange - 1)];
					c.High = (reader.IsDBNull(((int)IndexsColumn.High - 1)))?null:(System.Double?)reader[((int)IndexsColumn.High - 1)];
					c.Low = (reader.IsDBNull(((int)IndexsColumn.Low - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Low - 1)];
					c.Up = (reader.IsDBNull(((int)IndexsColumn.Up - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Up - 1)];
					c.Down = (reader.IsDBNull(((int)IndexsColumn.Down - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Down - 1)];
					c.Average = (reader.IsDBNull(((int)IndexsColumn.Average - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Average - 1)];
					c.Vol = (reader.IsDBNull(((int)IndexsColumn.Vol - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Vol - 1)];
					c.Val = (reader.IsDBNull(((int)IndexsColumn.Val - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Val - 1)];
					c.Attribute1 = (reader.IsDBNull(((int)IndexsColumn.Attribute1 - 1)))?null:(System.String)reader[((int)IndexsColumn.Attribute1 - 1)];
					c.Totaltrade = (reader.IsDBNull(((int)IndexsColumn.Totaltrade - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Totaltrade - 1)];
					c.Attribute3 = (reader.IsDBNull(((int)IndexsColumn.Attribute3 - 1)))?null:(System.DateTime?)reader[((int)IndexsColumn.Attribute3 - 1)];
					c.MarketId = (reader.IsDBNull(((int)IndexsColumn.MarketId - 1)))?null:(System.String)reader[((int)IndexsColumn.MarketId - 1)];
					c.Status = (reader.IsDBNull(((int)IndexsColumn.Status - 1)))?null:(System.Int16?)reader[((int)IndexsColumn.Status - 1)];
					c.Trans = (reader.IsDBNull(((int)IndexsColumn.Trans - 1)))?null:(System.Int32?)reader[((int)IndexsColumn.Trans - 1)];
					c.Upvolume = (reader.IsDBNull(((int)IndexsColumn.Upvolume - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Upvolume - 1)];
					c.Downvolume = (reader.IsDBNull(((int)IndexsColumn.Downvolume - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Downvolume - 1)];
					c.Nochangevolume = (reader.IsDBNull(((int)IndexsColumn.Nochangevolume - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Nochangevolume - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="RTStockData.Entities.Indexs"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="RTStockData.Entities.Indexs"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, RTStockData.Entities.Indexs entity)
		{
			if (!reader.Read()) return;
			
			entity.VnindexId = (System.Int32)reader[((int)IndexsColumn.VnindexId - 1)];
			entity.TradedDate = (reader.IsDBNull(((int)IndexsColumn.TradedDate - 1)))?null:(System.DateTime?)reader[((int)IndexsColumn.TradedDate - 1)];
			entity.Open = (reader.IsDBNull(((int)IndexsColumn.Open - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Open - 1)];
			entity.Close = (reader.IsDBNull(((int)IndexsColumn.Close - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Close - 1)];
			entity.Change = (reader.IsDBNull(((int)IndexsColumn.Change - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Change - 1)];
			entity.Unchange = (reader.IsDBNull(((int)IndexsColumn.Unchange - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Unchange - 1)];
			entity.High = (reader.IsDBNull(((int)IndexsColumn.High - 1)))?null:(System.Double?)reader[((int)IndexsColumn.High - 1)];
			entity.Low = (reader.IsDBNull(((int)IndexsColumn.Low - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Low - 1)];
			entity.Up = (reader.IsDBNull(((int)IndexsColumn.Up - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Up - 1)];
			entity.Down = (reader.IsDBNull(((int)IndexsColumn.Down - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Down - 1)];
			entity.Average = (reader.IsDBNull(((int)IndexsColumn.Average - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Average - 1)];
			entity.Vol = (reader.IsDBNull(((int)IndexsColumn.Vol - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Vol - 1)];
			entity.Val = (reader.IsDBNull(((int)IndexsColumn.Val - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Val - 1)];
			entity.Attribute1 = (reader.IsDBNull(((int)IndexsColumn.Attribute1 - 1)))?null:(System.String)reader[((int)IndexsColumn.Attribute1 - 1)];
			entity.Totaltrade = (reader.IsDBNull(((int)IndexsColumn.Totaltrade - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Totaltrade - 1)];
			entity.Attribute3 = (reader.IsDBNull(((int)IndexsColumn.Attribute3 - 1)))?null:(System.DateTime?)reader[((int)IndexsColumn.Attribute3 - 1)];
			entity.MarketId = (reader.IsDBNull(((int)IndexsColumn.MarketId - 1)))?null:(System.String)reader[((int)IndexsColumn.MarketId - 1)];
			entity.Status = (reader.IsDBNull(((int)IndexsColumn.Status - 1)))?null:(System.Int16?)reader[((int)IndexsColumn.Status - 1)];
			entity.Trans = (reader.IsDBNull(((int)IndexsColumn.Trans - 1)))?null:(System.Int32?)reader[((int)IndexsColumn.Trans - 1)];
			entity.Upvolume = (reader.IsDBNull(((int)IndexsColumn.Upvolume - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Upvolume - 1)];
			entity.Downvolume = (reader.IsDBNull(((int)IndexsColumn.Downvolume - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Downvolume - 1)];
			entity.Nochangevolume = (reader.IsDBNull(((int)IndexsColumn.Nochangevolume - 1)))?null:(System.Double?)reader[((int)IndexsColumn.Nochangevolume - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="RTStockData.Entities.Indexs"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="RTStockData.Entities.Indexs"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, RTStockData.Entities.Indexs entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.VnindexId = (System.Int32)dataRow["VNINDEX_ID"];
			entity.TradedDate = Convert.IsDBNull(dataRow["TRADED_DATE"]) ? null : (System.DateTime?)dataRow["TRADED_DATE"];
			entity.Open = Convert.IsDBNull(dataRow["OPEN"]) ? null : (System.Double?)dataRow["OPEN"];
			entity.Close = Convert.IsDBNull(dataRow["CLOSE"]) ? null : (System.Double?)dataRow["CLOSE"];
			entity.Change = Convert.IsDBNull(dataRow["CHANGE"]) ? null : (System.Double?)dataRow["CHANGE"];
			entity.Unchange = Convert.IsDBNull(dataRow["UNCHANGE"]) ? null : (System.Double?)dataRow["UNCHANGE"];
			entity.High = Convert.IsDBNull(dataRow["HIGH"]) ? null : (System.Double?)dataRow["HIGH"];
			entity.Low = Convert.IsDBNull(dataRow["LOW"]) ? null : (System.Double?)dataRow["LOW"];
			entity.Up = Convert.IsDBNull(dataRow["UP"]) ? null : (System.Double?)dataRow["UP"];
			entity.Down = Convert.IsDBNull(dataRow["DOWN"]) ? null : (System.Double?)dataRow["DOWN"];
			entity.Average = Convert.IsDBNull(dataRow["AVERAGE"]) ? null : (System.Double?)dataRow["AVERAGE"];
			entity.Vol = Convert.IsDBNull(dataRow["VOL"]) ? null : (System.Double?)dataRow["VOL"];
			entity.Val = Convert.IsDBNull(dataRow["VAL"]) ? null : (System.Double?)dataRow["VAL"];
			entity.Attribute1 = Convert.IsDBNull(dataRow["ATTRIBUTE1"]) ? null : (System.String)dataRow["ATTRIBUTE1"];
			entity.Totaltrade = Convert.IsDBNull(dataRow["TOTALTRADE"]) ? null : (System.Double?)dataRow["TOTALTRADE"];
			entity.Attribute3 = Convert.IsDBNull(dataRow["ATTRIBUTE3"]) ? null : (System.DateTime?)dataRow["ATTRIBUTE3"];
			entity.MarketId = Convert.IsDBNull(dataRow["Market_ID"]) ? null : (System.String)dataRow["Market_ID"];
			entity.Status = Convert.IsDBNull(dataRow["STATUS"]) ? null : (System.Int16?)dataRow["STATUS"];
			entity.Trans = Convert.IsDBNull(dataRow["TRANS"]) ? null : (System.Int32?)dataRow["TRANS"];
			entity.Upvolume = Convert.IsDBNull(dataRow["UPVOLUME"]) ? null : (System.Double?)dataRow["UPVOLUME"];
			entity.Downvolume = Convert.IsDBNull(dataRow["DOWNVOLUME"]) ? null : (System.Double?)dataRow["DOWNVOLUME"];
			entity.Nochangevolume = Convert.IsDBNull(dataRow["NOCHANGEVOLUME"]) ? null : (System.Double?)dataRow["NOCHANGEVOLUME"];
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
		/// <param name="entity">The <see cref="RTStockData.Entities.Indexs"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">RTStockData.Entities.Indexs Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, RTStockData.Entities.Indexs entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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
		/// Deep Save the entire object graph of the RTStockData.Entities.Indexs object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">RTStockData.Entities.Indexs instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">RTStockData.Entities.Indexs Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, RTStockData.Entities.Indexs entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
	#region IndexsChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>RTStockData.Entities.Indexs</c>
	///</summary>
	public enum IndexsChildEntityTypes
	{
	}
	
	#endregion IndexsChildEntityTypes
	
	#region IndexsFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;IndexsColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Indexs"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class IndexsFilterBuilder : SqlFilterBuilder<IndexsColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the IndexsFilterBuilder class.
		/// </summary>
		public IndexsFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the IndexsFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public IndexsFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the IndexsFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public IndexsFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion IndexsFilterBuilder
	
	#region IndexsParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;IndexsColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Indexs"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class IndexsParameterBuilder : ParameterizedSqlFilterBuilder<IndexsColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the IndexsParameterBuilder class.
		/// </summary>
		public IndexsParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the IndexsParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public IndexsParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the IndexsParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public IndexsParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion IndexsParameterBuilder
	
	#region IndexsSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;IndexsColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Indexs"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class IndexsSortBuilder : SqlSortBuilder<IndexsColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the IndexsSqlSortBuilder class.
		/// </summary>
		public IndexsSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion IndexsSortBuilder
	
} // end namespace
