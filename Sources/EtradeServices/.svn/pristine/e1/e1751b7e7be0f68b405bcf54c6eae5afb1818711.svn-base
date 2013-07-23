#region Using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

using ETradeFinance.Entities;
using ETradeFinance.DataAccess;

#endregion

namespace ETradeFinance.DataAccess.Bases
{	
	///<summary>
	/// This class is the base class for any <see cref="XrOrdersProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class XrOrdersProviderBaseCore : EntityProviderBase<ETradeFinance.Entities.XrOrders, ETradeFinance.Entities.XrOrdersKey>
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
		public override bool Delete(TransactionManager transactionManager, ETradeFinance.Entities.XrOrdersKey key)
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
		public override ETradeFinance.Entities.XrOrders Get(TransactionManager transactionManager, ETradeFinance.Entities.XrOrdersKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_XRORDERS index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="ETradeFinance.Entities.XrOrders"/> class.</returns>
		public ETradeFinance.Entities.XrOrders GetById(System.Int64 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_XRORDERS index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeFinance.Entities.XrOrders"/> class.</returns>
		public ETradeFinance.Entities.XrOrders GetById(System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_XRORDERS index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeFinance.Entities.XrOrders"/> class.</returns>
		public ETradeFinance.Entities.XrOrders GetById(TransactionManager transactionManager, System.Int64 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_XRORDERS index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeFinance.Entities.XrOrders"/> class.</returns>
		public ETradeFinance.Entities.XrOrders GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_XRORDERS index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeFinance.Entities.XrOrders"/> class.</returns>
		public ETradeFinance.Entities.XrOrders GetById(System.Int64 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_XRORDERS index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ETradeFinance.Entities.XrOrders"/> class.</returns>
		public abstract ETradeFinance.Entities.XrOrders GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;XrOrders&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;XrOrders&gt;"/></returns>
		public static TList<XrOrders> Fill(IDataReader reader, TList<XrOrders> rows, int start, int pageLength)
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
				
				ETradeFinance.Entities.XrOrders c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("XrOrders")
					.Append("|").Append((System.Int64)reader[((int)XrOrdersColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<XrOrders>(
					key.ToString(), // EntityTrackingKey
					"XrOrders",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ETradeFinance.Entities.XrOrders();
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
					c.Id = (System.Int64)reader[((int)XrOrdersColumn.Id - 1)];
					c.SubAccountId = (System.String)reader[((int)XrOrdersColumn.SubAccountId - 1)];
					c.BuyRightId = (System.Int64)reader[((int)XrOrdersColumn.BuyRightId - 1)];
					c.SecSymbol = (reader.IsDBNull(((int)XrOrdersColumn.SecSymbol - 1)))?null:(System.String)reader[((int)XrOrdersColumn.SecSymbol - 1)];
					c.Market = (reader.IsDBNull(((int)XrOrdersColumn.Market - 1)))?null:(System.String)reader[((int)XrOrdersColumn.Market - 1)];
					c.Volume = (reader.IsDBNull(((int)XrOrdersColumn.Volume - 1)))?null:(System.Int64?)reader[((int)XrOrdersColumn.Volume - 1)];
					c.Price = (reader.IsDBNull(((int)XrOrdersColumn.Price - 1)))?null:(System.Decimal?)reader[((int)XrOrdersColumn.Price - 1)];
					c.RegisteredVol = (reader.IsDBNull(((int)XrOrdersColumn.RegisteredVol - 1)))?null:(System.Int64?)reader[((int)XrOrdersColumn.RegisteredVol - 1)];
					c.AvailableVol = (reader.IsDBNull(((int)XrOrdersColumn.AvailableVol - 1)))?null:(System.Int64?)reader[((int)XrOrdersColumn.AvailableVol - 1)];
					c.RequestVol = (reader.IsDBNull(((int)XrOrdersColumn.RequestVol - 1)))?null:(System.Int64?)reader[((int)XrOrdersColumn.RequestVol - 1)];
					c.RequestTime = (reader.IsDBNull(((int)XrOrdersColumn.RequestTime - 1)))?null:(System.DateTime?)reader[((int)XrOrdersColumn.RequestTime - 1)];
					c.ApprovedVol = (reader.IsDBNull(((int)XrOrdersColumn.ApprovedVol - 1)))?null:(System.Int64?)reader[((int)XrOrdersColumn.ApprovedVol - 1)];
					c.Status = (System.Int32)reader[((int)XrOrdersColumn.Status - 1)];
					c.BrokerId = (reader.IsDBNull(((int)XrOrdersColumn.BrokerId - 1)))?null:(System.String)reader[((int)XrOrdersColumn.BrokerId - 1)];
					c.ExecTime = (reader.IsDBNull(((int)XrOrdersColumn.ExecTime - 1)))?null:(System.DateTime?)reader[((int)XrOrdersColumn.ExecTime - 1)];
					c.Note = (reader.IsDBNull(((int)XrOrdersColumn.Note - 1)))?null:(System.String)reader[((int)XrOrdersColumn.Note - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="ETradeFinance.Entities.XrOrders"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ETradeFinance.Entities.XrOrders"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ETradeFinance.Entities.XrOrders entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int64)reader[((int)XrOrdersColumn.Id - 1)];
			entity.SubAccountId = (System.String)reader[((int)XrOrdersColumn.SubAccountId - 1)];
			entity.BuyRightId = (System.Int64)reader[((int)XrOrdersColumn.BuyRightId - 1)];
			entity.SecSymbol = (reader.IsDBNull(((int)XrOrdersColumn.SecSymbol - 1)))?null:(System.String)reader[((int)XrOrdersColumn.SecSymbol - 1)];
			entity.Market = (reader.IsDBNull(((int)XrOrdersColumn.Market - 1)))?null:(System.String)reader[((int)XrOrdersColumn.Market - 1)];
			entity.Volume = (reader.IsDBNull(((int)XrOrdersColumn.Volume - 1)))?null:(System.Int64?)reader[((int)XrOrdersColumn.Volume - 1)];
			entity.Price = (reader.IsDBNull(((int)XrOrdersColumn.Price - 1)))?null:(System.Decimal?)reader[((int)XrOrdersColumn.Price - 1)];
			entity.RegisteredVol = (reader.IsDBNull(((int)XrOrdersColumn.RegisteredVol - 1)))?null:(System.Int64?)reader[((int)XrOrdersColumn.RegisteredVol - 1)];
			entity.AvailableVol = (reader.IsDBNull(((int)XrOrdersColumn.AvailableVol - 1)))?null:(System.Int64?)reader[((int)XrOrdersColumn.AvailableVol - 1)];
			entity.RequestVol = (reader.IsDBNull(((int)XrOrdersColumn.RequestVol - 1)))?null:(System.Int64?)reader[((int)XrOrdersColumn.RequestVol - 1)];
			entity.RequestTime = (reader.IsDBNull(((int)XrOrdersColumn.RequestTime - 1)))?null:(System.DateTime?)reader[((int)XrOrdersColumn.RequestTime - 1)];
			entity.ApprovedVol = (reader.IsDBNull(((int)XrOrdersColumn.ApprovedVol - 1)))?null:(System.Int64?)reader[((int)XrOrdersColumn.ApprovedVol - 1)];
			entity.Status = (System.Int32)reader[((int)XrOrdersColumn.Status - 1)];
			entity.BrokerId = (reader.IsDBNull(((int)XrOrdersColumn.BrokerId - 1)))?null:(System.String)reader[((int)XrOrdersColumn.BrokerId - 1)];
			entity.ExecTime = (reader.IsDBNull(((int)XrOrdersColumn.ExecTime - 1)))?null:(System.DateTime?)reader[((int)XrOrdersColumn.ExecTime - 1)];
			entity.Note = (reader.IsDBNull(((int)XrOrdersColumn.Note - 1)))?null:(System.String)reader[((int)XrOrdersColumn.Note - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ETradeFinance.Entities.XrOrders"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ETradeFinance.Entities.XrOrders"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ETradeFinance.Entities.XrOrders entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int64)dataRow["ID"];
			entity.SubAccountId = (System.String)dataRow["SubAccountID"];
			entity.BuyRightId = (System.Int64)dataRow["BuyRightID"];
			entity.SecSymbol = Convert.IsDBNull(dataRow["SecSymbol"]) ? null : (System.String)dataRow["SecSymbol"];
			entity.Market = Convert.IsDBNull(dataRow["Market"]) ? null : (System.String)dataRow["Market"];
			entity.Volume = Convert.IsDBNull(dataRow["Volume"]) ? null : (System.Int64?)dataRow["Volume"];
			entity.Price = Convert.IsDBNull(dataRow["Price"]) ? null : (System.Decimal?)dataRow["Price"];
			entity.RegisteredVol = Convert.IsDBNull(dataRow["RegisteredVol"]) ? null : (System.Int64?)dataRow["RegisteredVol"];
			entity.AvailableVol = Convert.IsDBNull(dataRow["AvailableVol"]) ? null : (System.Int64?)dataRow["AvailableVol"];
			entity.RequestVol = Convert.IsDBNull(dataRow["RequestVol"]) ? null : (System.Int64?)dataRow["RequestVol"];
			entity.RequestTime = Convert.IsDBNull(dataRow["RequestTime"]) ? null : (System.DateTime?)dataRow["RequestTime"];
			entity.ApprovedVol = Convert.IsDBNull(dataRow["ApprovedVol"]) ? null : (System.Int64?)dataRow["ApprovedVol"];
			entity.Status = (System.Int32)dataRow["Status"];
			entity.BrokerId = Convert.IsDBNull(dataRow["BrokerID"]) ? null : (System.String)dataRow["BrokerID"];
			entity.ExecTime = Convert.IsDBNull(dataRow["ExecTime"]) ? null : (System.DateTime?)dataRow["ExecTime"];
			entity.Note = Convert.IsDBNull(dataRow["Note"]) ? null : (System.String)dataRow["Note"];
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
		/// <param name="entity">The <see cref="ETradeFinance.Entities.XrOrders"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ETradeFinance.Entities.XrOrders Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ETradeFinance.Entities.XrOrders entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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
		/// Deep Save the entire object graph of the ETradeFinance.Entities.XrOrders object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ETradeFinance.Entities.XrOrders instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ETradeFinance.Entities.XrOrders Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ETradeFinance.Entities.XrOrders entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
	#region XrOrdersChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ETradeFinance.Entities.XrOrders</c>
	///</summary>
	public enum XrOrdersChildEntityTypes
	{
	}
	
	#endregion XrOrdersChildEntityTypes
	
	#region XrOrdersFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;XrOrdersColumn&gt;"/> class
	/// that is used exclusively with a <see cref="XrOrders"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class XrOrdersFilterBuilder : SqlFilterBuilder<XrOrdersColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the XrOrdersFilterBuilder class.
		/// </summary>
		public XrOrdersFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the XrOrdersFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public XrOrdersFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the XrOrdersFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public XrOrdersFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion XrOrdersFilterBuilder
	
	#region XrOrdersParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;XrOrdersColumn&gt;"/> class
	/// that is used exclusively with a <see cref="XrOrders"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class XrOrdersParameterBuilder : ParameterizedSqlFilterBuilder<XrOrdersColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the XrOrdersParameterBuilder class.
		/// </summary>
		public XrOrdersParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the XrOrdersParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public XrOrdersParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the XrOrdersParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public XrOrdersParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion XrOrdersParameterBuilder
	
	#region XrOrdersSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;XrOrdersColumn&gt;"/> class
	/// that is used exclusively with a <see cref="XrOrders"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class XrOrdersSortBuilder : SqlSortBuilder<XrOrdersColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the XrOrdersSqlSortBuilder class.
		/// </summary>
		public XrOrdersSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion XrOrdersSortBuilder
	
} // end namespace
