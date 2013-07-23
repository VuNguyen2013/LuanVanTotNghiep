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
	/// This class is the base class for any <see cref="PnLhistoryProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class PnLhistoryProviderBaseCore : EntityProviderBase<ETradeHistory.Entities.PnLhistory, ETradeHistory.Entities.PnLhistoryKey>
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
		public override bool Delete(TransactionManager transactionManager, ETradeHistory.Entities.PnLhistoryKey key)
		{
			return Delete(transactionManager, key.Id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_id">ID identifies PnLHistory. Primary Key.</param>
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
		/// <param name="_id">ID identifies PnLHistory. Primary Key.</param>
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
		public override ETradeHistory.Entities.PnLhistory Get(TransactionManager transactionManager, ETradeHistory.Entities.PnLhistoryKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_PnLHistory index.
		/// </summary>
		/// <param name="_id">ID identifies PnLHistory</param>
		/// <returns>Returns an instance of the <see cref="ETradeHistory.Entities.PnLhistory"/> class.</returns>
		public ETradeHistory.Entities.PnLhistory GetById(long _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PnLHistory index.
		/// </summary>
		/// <param name="_id">ID identifies PnLHistory</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeHistory.Entities.PnLhistory"/> class.</returns>
		public ETradeHistory.Entities.PnLhistory GetById(long _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PnLHistory index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">ID identifies PnLHistory</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeHistory.Entities.PnLhistory"/> class.</returns>
		public ETradeHistory.Entities.PnLhistory GetById(TransactionManager transactionManager, long _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PnLHistory index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">ID identifies PnLHistory</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeHistory.Entities.PnLhistory"/> class.</returns>
		public ETradeHistory.Entities.PnLhistory GetById(TransactionManager transactionManager, long _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PnLHistory index.
		/// </summary>
		/// <param name="_id">ID identifies PnLHistory</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeHistory.Entities.PnLhistory"/> class.</returns>
		public ETradeHistory.Entities.PnLhistory GetById(long _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PnLHistory index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">ID identifies PnLHistory</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ETradeHistory.Entities.PnLhistory"/> class.</returns>
		public abstract ETradeHistory.Entities.PnLhistory GetById(TransactionManager transactionManager, long _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;PnLhistory&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;PnLhistory&gt;"/></returns>
		public static TList<PnLhistory> Fill(IDataReader reader, TList<PnLhistory> rows, int start, int pageLength)
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
				
				ETradeHistory.Entities.PnLhistory c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("PnLhistory")
					.Append("|").Append((long)reader[((int)PnLhistoryColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<PnLhistory>(
					key.ToString(), // EntityTrackingKey
					"PnLhistory",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ETradeHistory.Entities.PnLhistory();
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
					c.Id = (long)reader[((int)PnLhistoryColumn.Id - 1)];
					c.TradeTime = (System.DateTime)reader[((int)PnLhistoryColumn.TradeTime - 1)];
					c.RefOrderId = (reader.IsDBNull(((int)PnLhistoryColumn.RefOrderId - 1)))?null:(string)reader[((int)PnLhistoryColumn.RefOrderId - 1)];
					c.FisOrderId = (reader.IsDBNull(((int)PnLhistoryColumn.FisOrderId - 1)))?null:(System.Int32?)reader[((int)PnLhistoryColumn.FisOrderId - 1)];
					c.SecSymbol = (string)reader[((int)PnLhistoryColumn.SecSymbol - 1)];
					c.Price = (decimal)reader[((int)PnLhistoryColumn.Price - 1)];
					c.AvgPrice = (reader.IsDBNull(((int)PnLhistoryColumn.AvgPrice - 1)))?null:(System.Decimal?)reader[((int)PnLhistoryColumn.AvgPrice - 1)];
					c.Volume = (int)reader[((int)PnLhistoryColumn.Volume - 1)];
					c.Profit = (reader.IsDBNull(((int)PnLhistoryColumn.Profit - 1)))?null:(System.Decimal?)reader[((int)PnLhistoryColumn.Profit - 1)];
					c.ProfitabilityRatio = (reader.IsDBNull(((int)PnLhistoryColumn.ProfitabilityRatio - 1)))?null:(System.Decimal?)reader[((int)PnLhistoryColumn.ProfitabilityRatio - 1)];
					c.SubCustAccountId = (string)reader[((int)PnLhistoryColumn.SubCustAccountId - 1)];
					c.Market = (string)reader[((int)PnLhistoryColumn.Market - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="ETradeHistory.Entities.PnLhistory"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ETradeHistory.Entities.PnLhistory"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ETradeHistory.Entities.PnLhistory entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (long)reader[((int)PnLhistoryColumn.Id - 1)];
			entity.TradeTime = (System.DateTime)reader[((int)PnLhistoryColumn.TradeTime - 1)];
			entity.RefOrderId = (reader.IsDBNull(((int)PnLhistoryColumn.RefOrderId - 1)))?null:(string)reader[((int)PnLhistoryColumn.RefOrderId - 1)];
			entity.FisOrderId = (reader.IsDBNull(((int)PnLhistoryColumn.FisOrderId - 1)))?null:(System.Int32?)reader[((int)PnLhistoryColumn.FisOrderId - 1)];
			entity.SecSymbol = (string)reader[((int)PnLhistoryColumn.SecSymbol - 1)];
			entity.Price = (decimal)reader[((int)PnLhistoryColumn.Price - 1)];
			entity.AvgPrice = (reader.IsDBNull(((int)PnLhistoryColumn.AvgPrice - 1)))?null:(System.Decimal?)reader[((int)PnLhistoryColumn.AvgPrice - 1)];
			entity.Volume = (int)reader[((int)PnLhistoryColumn.Volume - 1)];
			entity.Profit = (reader.IsDBNull(((int)PnLhistoryColumn.Profit - 1)))?null:(System.Decimal?)reader[((int)PnLhistoryColumn.Profit - 1)];
			entity.ProfitabilityRatio = (reader.IsDBNull(((int)PnLhistoryColumn.ProfitabilityRatio - 1)))?null:(System.Decimal?)reader[((int)PnLhistoryColumn.ProfitabilityRatio - 1)];
			entity.SubCustAccountId = (string)reader[((int)PnLhistoryColumn.SubCustAccountId - 1)];
			entity.Market = (string)reader[((int)PnLhistoryColumn.Market - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ETradeHistory.Entities.PnLhistory"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ETradeHistory.Entities.PnLhistory"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ETradeHistory.Entities.PnLhistory entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (long)dataRow["ID"];
			entity.TradeTime = (System.DateTime)dataRow["TradeTime"];
			entity.RefOrderId = Convert.IsDBNull(dataRow["RefOrderID"]) ? null : (string)dataRow["RefOrderID"];
			entity.FisOrderId = Convert.IsDBNull(dataRow["FISOrderID"]) ? null : (System.Int32?)dataRow["FISOrderID"];
			entity.SecSymbol = (string)dataRow["SecSymbol"];
			entity.Price = (decimal)dataRow["Price"];
			entity.AvgPrice = Convert.IsDBNull(dataRow["AvgPrice"]) ? null : (System.Decimal?)dataRow["AvgPrice"];
			entity.Volume = (int)dataRow["Volume"];
			entity.Profit = Convert.IsDBNull(dataRow["Profit"]) ? null : (System.Decimal?)dataRow["Profit"];
			entity.ProfitabilityRatio = Convert.IsDBNull(dataRow["ProfitabilityRatio"]) ? null : (System.Decimal?)dataRow["ProfitabilityRatio"];
			entity.SubCustAccountId = (string)dataRow["SubCustAccountID"];
			entity.Market = (string)dataRow["Market"];
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
		/// <param name="entity">The <see cref="ETradeHistory.Entities.PnLhistory"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ETradeHistory.Entities.PnLhistory Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ETradeHistory.Entities.PnLhistory entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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
		/// Deep Save the entire object graph of the ETradeHistory.Entities.PnLhistory object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ETradeHistory.Entities.PnLhistory instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ETradeHistory.Entities.PnLhistory Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ETradeHistory.Entities.PnLhistory entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
	#region PnLhistoryChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ETradeHistory.Entities.PnLhistory</c>
	///</summary>
	public enum PnLhistoryChildEntityTypes
	{
	}
	
	#endregion PnLhistoryChildEntityTypes
	
	#region PnLhistoryFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;PnLhistoryColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PnLhistory"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PnLhistoryFilterBuilder : SqlFilterBuilder<PnLhistoryColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PnLhistoryFilterBuilder class.
		/// </summary>
		public PnLhistoryFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PnLhistoryFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PnLhistoryFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PnLhistoryFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PnLhistoryFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PnLhistoryFilterBuilder
	
	#region PnLhistoryParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;PnLhistoryColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PnLhistory"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PnLhistoryParameterBuilder : ParameterizedSqlFilterBuilder<PnLhistoryColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PnLhistoryParameterBuilder class.
		/// </summary>
		public PnLhistoryParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PnLhistoryParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PnLhistoryParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PnLhistoryParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PnLhistoryParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PnLhistoryParameterBuilder
	
	#region PnLhistorySortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;PnLhistoryColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PnLhistory"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class PnLhistorySortBuilder : SqlSortBuilder<PnLhistoryColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PnLhistorySqlSortBuilder class.
		/// </summary>
		public PnLhistorySortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion PnLhistorySortBuilder
	
} // end namespace
