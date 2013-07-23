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
	/// This class is the base class for any <see cref="IndexInfoProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class IndexInfoProviderBaseCore : EntityProviderBase<RTStockData.Entities.IndexInfo, RTStockData.Entities.IndexInfoKey>
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
		public override bool Delete(TransactionManager transactionManager, RTStockData.Entities.IndexInfoKey key)
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
		public override RTStockData.Entities.IndexInfo Get(TransactionManager transactionManager, RTStockData.Entities.IndexInfoKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_IndexInfo index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.IndexInfo"/> class.</returns>
		public RTStockData.Entities.IndexInfo GetById(System.Int64 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_IndexInfo index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.IndexInfo"/> class.</returns>
		public RTStockData.Entities.IndexInfo GetById(System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_IndexInfo index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.IndexInfo"/> class.</returns>
		public RTStockData.Entities.IndexInfo GetById(TransactionManager transactionManager, System.Int64 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_IndexInfo index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.IndexInfo"/> class.</returns>
		public RTStockData.Entities.IndexInfo GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_IndexInfo index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.IndexInfo"/> class.</returns>
		public RTStockData.Entities.IndexInfo GetById(System.Int64 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_IndexInfo index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.IndexInfo"/> class.</returns>
		public abstract RTStockData.Entities.IndexInfo GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;IndexInfo&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;IndexInfo&gt;"/></returns>
		public static TList<IndexInfo> Fill(IDataReader reader, TList<IndexInfo> rows, int start, int pageLength)
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
				
				RTStockData.Entities.IndexInfo c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("IndexInfo")
					.Append("|").Append((System.Int64)reader[((int)IndexInfoColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<IndexInfo>(
					key.ToString(), // EntityTrackingKey
					"IndexInfo",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new RTStockData.Entities.IndexInfo();
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
					c.Id = (System.Int64)reader[((int)IndexInfoColumn.Id - 1)];
					c.IndexId = (reader.IsDBNull(((int)IndexInfoColumn.IndexId - 1)))?null:(System.Int64?)reader[((int)IndexInfoColumn.IndexId - 1)];
					c.IndexCode = (reader.IsDBNull(((int)IndexInfoColumn.IndexCode - 1)))?null:(System.String)reader[((int)IndexInfoColumn.IndexCode - 1)];
					c.Name = (reader.IsDBNull(((int)IndexInfoColumn.Name - 1)))?null:(System.String)reader[((int)IndexInfoColumn.Name - 1)];
					c.Description = (reader.IsDBNull(((int)IndexInfoColumn.Description - 1)))?null:(System.String)reader[((int)IndexInfoColumn.Description - 1)];
					c.TradingDate = (reader.IsDBNull(((int)IndexInfoColumn.TradingDate - 1)))?null:(System.DateTime?)reader[((int)IndexInfoColumn.TradingDate - 1)];
					c.Time = (reader.IsDBNull(((int)IndexInfoColumn.Time - 1)))?null:(System.Int64?)reader[((int)IndexInfoColumn.Time - 1)];
					c.CurrentStatus = (reader.IsDBNull(((int)IndexInfoColumn.CurrentStatus - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.CurrentStatus - 1)];
					c.TotalStock = (reader.IsDBNull(((int)IndexInfoColumn.TotalStock - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.TotalStock - 1)];
					c.Advances = (reader.IsDBNull(((int)IndexInfoColumn.Advances - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.Advances - 1)];
					c.Nochange = (reader.IsDBNull(((int)IndexInfoColumn.Nochange - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.Nochange - 1)];
					c.Declines = (reader.IsDBNull(((int)IndexInfoColumn.Declines - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.Declines - 1)];
					c.TotalQtty = (reader.IsDBNull(((int)IndexInfoColumn.TotalQtty - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.TotalQtty - 1)];
					c.TotalValue = (reader.IsDBNull(((int)IndexInfoColumn.TotalValue - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.TotalValue - 1)];
					c.PriorIndexVal = (reader.IsDBNull(((int)IndexInfoColumn.PriorIndexVal - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.PriorIndexVal - 1)];
					c.ChgIndex = (reader.IsDBNull(((int)IndexInfoColumn.ChgIndex - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.ChgIndex - 1)];
					c.PctIndex = (reader.IsDBNull(((int)IndexInfoColumn.PctIndex - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.PctIndex - 1)];
					c.CurrentIndex = (reader.IsDBNull(((int)IndexInfoColumn.CurrentIndex - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.CurrentIndex - 1)];
					c.HighestIndex = (reader.IsDBNull(((int)IndexInfoColumn.HighestIndex - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.HighestIndex - 1)];
					c.LowestIndex = (reader.IsDBNull(((int)IndexInfoColumn.LowestIndex - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.LowestIndex - 1)];
					c.SessionNo = (reader.IsDBNull(((int)IndexInfoColumn.SessionNo - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.SessionNo - 1)];
					c.TypeIndex = (reader.IsDBNull(((int)IndexInfoColumn.TypeIndex - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.TypeIndex - 1)];
					c.CloseIndex = (reader.IsDBNull(((int)IndexInfoColumn.CloseIndex - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.CloseIndex - 1)];
					c.TradeDate = (reader.IsDBNull(((int)IndexInfoColumn.TradeDate - 1)))?null:(System.DateTime?)reader[((int)IndexInfoColumn.TradeDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="RTStockData.Entities.IndexInfo"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="RTStockData.Entities.IndexInfo"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, RTStockData.Entities.IndexInfo entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int64)reader[((int)IndexInfoColumn.Id - 1)];
			entity.IndexId = (reader.IsDBNull(((int)IndexInfoColumn.IndexId - 1)))?null:(System.Int64?)reader[((int)IndexInfoColumn.IndexId - 1)];
			entity.IndexCode = (reader.IsDBNull(((int)IndexInfoColumn.IndexCode - 1)))?null:(System.String)reader[((int)IndexInfoColumn.IndexCode - 1)];
			entity.Name = (reader.IsDBNull(((int)IndexInfoColumn.Name - 1)))?null:(System.String)reader[((int)IndexInfoColumn.Name - 1)];
			entity.Description = (reader.IsDBNull(((int)IndexInfoColumn.Description - 1)))?null:(System.String)reader[((int)IndexInfoColumn.Description - 1)];
			entity.TradingDate = (reader.IsDBNull(((int)IndexInfoColumn.TradingDate - 1)))?null:(System.DateTime?)reader[((int)IndexInfoColumn.TradingDate - 1)];
			entity.Time = (reader.IsDBNull(((int)IndexInfoColumn.Time - 1)))?null:(System.Int64?)reader[((int)IndexInfoColumn.Time - 1)];
			entity.CurrentStatus = (reader.IsDBNull(((int)IndexInfoColumn.CurrentStatus - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.CurrentStatus - 1)];
			entity.TotalStock = (reader.IsDBNull(((int)IndexInfoColumn.TotalStock - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.TotalStock - 1)];
			entity.Advances = (reader.IsDBNull(((int)IndexInfoColumn.Advances - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.Advances - 1)];
			entity.Nochange = (reader.IsDBNull(((int)IndexInfoColumn.Nochange - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.Nochange - 1)];
			entity.Declines = (reader.IsDBNull(((int)IndexInfoColumn.Declines - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.Declines - 1)];
			entity.TotalQtty = (reader.IsDBNull(((int)IndexInfoColumn.TotalQtty - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.TotalQtty - 1)];
			entity.TotalValue = (reader.IsDBNull(((int)IndexInfoColumn.TotalValue - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.TotalValue - 1)];
			entity.PriorIndexVal = (reader.IsDBNull(((int)IndexInfoColumn.PriorIndexVal - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.PriorIndexVal - 1)];
			entity.ChgIndex = (reader.IsDBNull(((int)IndexInfoColumn.ChgIndex - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.ChgIndex - 1)];
			entity.PctIndex = (reader.IsDBNull(((int)IndexInfoColumn.PctIndex - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.PctIndex - 1)];
			entity.CurrentIndex = (reader.IsDBNull(((int)IndexInfoColumn.CurrentIndex - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.CurrentIndex - 1)];
			entity.HighestIndex = (reader.IsDBNull(((int)IndexInfoColumn.HighestIndex - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.HighestIndex - 1)];
			entity.LowestIndex = (reader.IsDBNull(((int)IndexInfoColumn.LowestIndex - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.LowestIndex - 1)];
			entity.SessionNo = (reader.IsDBNull(((int)IndexInfoColumn.SessionNo - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.SessionNo - 1)];
			entity.TypeIndex = (reader.IsDBNull(((int)IndexInfoColumn.TypeIndex - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.TypeIndex - 1)];
			entity.CloseIndex = (reader.IsDBNull(((int)IndexInfoColumn.CloseIndex - 1)))?null:(System.Decimal?)reader[((int)IndexInfoColumn.CloseIndex - 1)];
			entity.TradeDate = (reader.IsDBNull(((int)IndexInfoColumn.TradeDate - 1)))?null:(System.DateTime?)reader[((int)IndexInfoColumn.TradeDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="RTStockData.Entities.IndexInfo"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="RTStockData.Entities.IndexInfo"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, RTStockData.Entities.IndexInfo entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int64)dataRow["ID"];
			entity.IndexId = Convert.IsDBNull(dataRow["IndexId"]) ? null : (System.Int64?)dataRow["IndexId"];
			entity.IndexCode = Convert.IsDBNull(dataRow["IndexCode"]) ? null : (System.String)dataRow["IndexCode"];
			entity.Name = Convert.IsDBNull(dataRow["Name"]) ? null : (System.String)dataRow["Name"];
			entity.Description = Convert.IsDBNull(dataRow["Description"]) ? null : (System.String)dataRow["Description"];
			entity.TradingDate = Convert.IsDBNull(dataRow["TradingDate"]) ? null : (System.DateTime?)dataRow["TradingDate"];
			entity.Time = Convert.IsDBNull(dataRow["Time"]) ? null : (System.Int64?)dataRow["Time"];
			entity.CurrentStatus = Convert.IsDBNull(dataRow["CurrentStatus"]) ? null : (System.Decimal?)dataRow["CurrentStatus"];
			entity.TotalStock = Convert.IsDBNull(dataRow["TotalStock"]) ? null : (System.Decimal?)dataRow["TotalStock"];
			entity.Advances = Convert.IsDBNull(dataRow["Advances"]) ? null : (System.Decimal?)dataRow["Advances"];
			entity.Nochange = Convert.IsDBNull(dataRow["Nochange"]) ? null : (System.Decimal?)dataRow["Nochange"];
			entity.Declines = Convert.IsDBNull(dataRow["Declines"]) ? null : (System.Decimal?)dataRow["Declines"];
			entity.TotalQtty = Convert.IsDBNull(dataRow["TotalQtty"]) ? null : (System.Decimal?)dataRow["TotalQtty"];
			entity.TotalValue = Convert.IsDBNull(dataRow["TotalValue"]) ? null : (System.Decimal?)dataRow["TotalValue"];
			entity.PriorIndexVal = Convert.IsDBNull(dataRow["PriorIndexVal"]) ? null : (System.Decimal?)dataRow["PriorIndexVal"];
			entity.ChgIndex = Convert.IsDBNull(dataRow["ChgIndex"]) ? null : (System.Decimal?)dataRow["ChgIndex"];
			entity.PctIndex = Convert.IsDBNull(dataRow["PctIndex"]) ? null : (System.Decimal?)dataRow["PctIndex"];
			entity.CurrentIndex = Convert.IsDBNull(dataRow["CurrentIndex"]) ? null : (System.Decimal?)dataRow["CurrentIndex"];
			entity.HighestIndex = Convert.IsDBNull(dataRow["HighestIndex"]) ? null : (System.Decimal?)dataRow["HighestIndex"];
			entity.LowestIndex = Convert.IsDBNull(dataRow["LowestIndex"]) ? null : (System.Decimal?)dataRow["LowestIndex"];
			entity.SessionNo = Convert.IsDBNull(dataRow["SessionNo"]) ? null : (System.Decimal?)dataRow["SessionNo"];
			entity.TypeIndex = Convert.IsDBNull(dataRow["TypeIndex"]) ? null : (System.Decimal?)dataRow["TypeIndex"];
			entity.CloseIndex = Convert.IsDBNull(dataRow["CloseIndex"]) ? null : (System.Decimal?)dataRow["CloseIndex"];
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
		/// <param name="entity">The <see cref="RTStockData.Entities.IndexInfo"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">RTStockData.Entities.IndexInfo Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, RTStockData.Entities.IndexInfo entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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
		/// Deep Save the entire object graph of the RTStockData.Entities.IndexInfo object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">RTStockData.Entities.IndexInfo instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">RTStockData.Entities.IndexInfo Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, RTStockData.Entities.IndexInfo entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
	#region IndexInfoChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>RTStockData.Entities.IndexInfo</c>
	///</summary>
	public enum IndexInfoChildEntityTypes
	{
	}
	
	#endregion IndexInfoChildEntityTypes
	
	#region IndexInfoFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;IndexInfoColumn&gt;"/> class
	/// that is used exclusively with a <see cref="IndexInfo"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class IndexInfoFilterBuilder : SqlFilterBuilder<IndexInfoColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the IndexInfoFilterBuilder class.
		/// </summary>
		public IndexInfoFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the IndexInfoFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public IndexInfoFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the IndexInfoFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public IndexInfoFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion IndexInfoFilterBuilder
	
	#region IndexInfoParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;IndexInfoColumn&gt;"/> class
	/// that is used exclusively with a <see cref="IndexInfo"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class IndexInfoParameterBuilder : ParameterizedSqlFilterBuilder<IndexInfoColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the IndexInfoParameterBuilder class.
		/// </summary>
		public IndexInfoParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the IndexInfoParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public IndexInfoParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the IndexInfoParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public IndexInfoParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion IndexInfoParameterBuilder
	
	#region IndexInfoSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;IndexInfoColumn&gt;"/> class
	/// that is used exclusively with a <see cref="IndexInfo"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class IndexInfoSortBuilder : SqlSortBuilder<IndexInfoColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the IndexInfoSqlSortBuilder class.
		/// </summary>
		public IndexInfoSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion IndexInfoSortBuilder
	
} // end namespace
