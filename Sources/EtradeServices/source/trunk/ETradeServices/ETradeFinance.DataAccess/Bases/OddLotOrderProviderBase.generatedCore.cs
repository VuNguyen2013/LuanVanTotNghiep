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
	/// This class is the base class for any <see cref="OddLotOrderProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class OddLotOrderProviderBaseCore : EntityProviderBase<ETradeFinance.Entities.OddLotOrder, ETradeFinance.Entities.OddLotOrderKey>
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
		public override bool Delete(TransactionManager transactionManager, ETradeFinance.Entities.OddLotOrderKey key)
		{
			return Delete(transactionManager, key.Id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_id">OddLotOrderID identifies OddLotOrder. Primary Key.</param>
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
		/// <param name="_id">OddLotOrderID identifies OddLotOrder. Primary Key.</param>
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
		public override ETradeFinance.Entities.OddLotOrder Get(TransactionManager transactionManager, ETradeFinance.Entities.OddLotOrderKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_OddLotOrder index.
		/// </summary>
		/// <param name="_id">OddLotOrderID identifies OddLotOrder</param>
		/// <returns>Returns an instance of the <see cref="ETradeFinance.Entities.OddLotOrder"/> class.</returns>
		public ETradeFinance.Entities.OddLotOrder GetById(System.Int64 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_OddLotOrder index.
		/// </summary>
		/// <param name="_id">OddLotOrderID identifies OddLotOrder</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeFinance.Entities.OddLotOrder"/> class.</returns>
		public ETradeFinance.Entities.OddLotOrder GetById(System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_OddLotOrder index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">OddLotOrderID identifies OddLotOrder</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeFinance.Entities.OddLotOrder"/> class.</returns>
		public ETradeFinance.Entities.OddLotOrder GetById(TransactionManager transactionManager, System.Int64 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_OddLotOrder index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">OddLotOrderID identifies OddLotOrder</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeFinance.Entities.OddLotOrder"/> class.</returns>
		public ETradeFinance.Entities.OddLotOrder GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_OddLotOrder index.
		/// </summary>
		/// <param name="_id">OddLotOrderID identifies OddLotOrder</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeFinance.Entities.OddLotOrder"/> class.</returns>
		public ETradeFinance.Entities.OddLotOrder GetById(System.Int64 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_OddLotOrder index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">OddLotOrderID identifies OddLotOrder</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ETradeFinance.Entities.OddLotOrder"/> class.</returns>
		public abstract ETradeFinance.Entities.OddLotOrder GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;OddLotOrder&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;OddLotOrder&gt;"/></returns>
		public static TList<OddLotOrder> Fill(IDataReader reader, TList<OddLotOrder> rows, int start, int pageLength)
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
				
				ETradeFinance.Entities.OddLotOrder c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("OddLotOrder")
					.Append("|").Append((System.Int64)reader[((int)OddLotOrderColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<OddLotOrder>(
					key.ToString(), // EntityTrackingKey
					"OddLotOrder",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ETradeFinance.Entities.OddLotOrder();
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
					c.Id = (System.Int64)reader[((int)OddLotOrderColumn.Id - 1)];
					c.SecSymbol = (System.String)reader[((int)OddLotOrderColumn.SecSymbol - 1)];
					c.Side = (System.String)reader[((int)OddLotOrderColumn.Side - 1)];
					c.Price = (reader.IsDBNull(((int)OddLotOrderColumn.Price - 1)))?null:(System.Decimal?)reader[((int)OddLotOrderColumn.Price - 1)];
					c.Volume = (reader.IsDBNull(((int)OddLotOrderColumn.Volume - 1)))?null:(System.Int64?)reader[((int)OddLotOrderColumn.Volume - 1)];
					c.SubCustAccountId = (System.String)reader[((int)OddLotOrderColumn.SubCustAccountId - 1)];
					c.Market = (System.String)reader[((int)OddLotOrderColumn.Market - 1)];
					c.ExecPrice = (reader.IsDBNull(((int)OddLotOrderColumn.ExecPrice - 1)))?null:(System.Decimal?)reader[((int)OddLotOrderColumn.ExecPrice - 1)];
					c.ExecVol = (reader.IsDBNull(((int)OddLotOrderColumn.ExecVol - 1)))?null:(System.Int64?)reader[((int)OddLotOrderColumn.ExecVol - 1)];
					c.CanceledVol = (reader.IsDBNull(((int)OddLotOrderColumn.CanceledVol - 1)))?null:(System.Int64?)reader[((int)OddLotOrderColumn.CanceledVol - 1)];
					c.Status = (reader.IsDBNull(((int)OddLotOrderColumn.Status - 1)))?null:(System.Int32?)reader[((int)OddLotOrderColumn.Status - 1)];
					c.BrokerId = (reader.IsDBNull(((int)OddLotOrderColumn.BrokerId - 1)))?null:(System.String)reader[((int)OddLotOrderColumn.BrokerId - 1)];
					c.RequestTime = (System.DateTime)reader[((int)OddLotOrderColumn.RequestTime - 1)];
					c.ExecTime = (reader.IsDBNull(((int)OddLotOrderColumn.ExecTime - 1)))?null:(System.DateTime?)reader[((int)OddLotOrderColumn.ExecTime - 1)];
					c.Note = (reader.IsDBNull(((int)OddLotOrderColumn.Note - 1)))?null:(System.String)reader[((int)OddLotOrderColumn.Note - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="ETradeFinance.Entities.OddLotOrder"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ETradeFinance.Entities.OddLotOrder"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ETradeFinance.Entities.OddLotOrder entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int64)reader[((int)OddLotOrderColumn.Id - 1)];
			entity.SecSymbol = (System.String)reader[((int)OddLotOrderColumn.SecSymbol - 1)];
			entity.Side = (System.String)reader[((int)OddLotOrderColumn.Side - 1)];
			entity.Price = (reader.IsDBNull(((int)OddLotOrderColumn.Price - 1)))?null:(System.Decimal?)reader[((int)OddLotOrderColumn.Price - 1)];
			entity.Volume = (reader.IsDBNull(((int)OddLotOrderColumn.Volume - 1)))?null:(System.Int64?)reader[((int)OddLotOrderColumn.Volume - 1)];
			entity.SubCustAccountId = (System.String)reader[((int)OddLotOrderColumn.SubCustAccountId - 1)];
			entity.Market = (System.String)reader[((int)OddLotOrderColumn.Market - 1)];
			entity.ExecPrice = (reader.IsDBNull(((int)OddLotOrderColumn.ExecPrice - 1)))?null:(System.Decimal?)reader[((int)OddLotOrderColumn.ExecPrice - 1)];
			entity.ExecVol = (reader.IsDBNull(((int)OddLotOrderColumn.ExecVol - 1)))?null:(System.Int64?)reader[((int)OddLotOrderColumn.ExecVol - 1)];
			entity.CanceledVol = (reader.IsDBNull(((int)OddLotOrderColumn.CanceledVol - 1)))?null:(System.Int64?)reader[((int)OddLotOrderColumn.CanceledVol - 1)];
			entity.Status = (reader.IsDBNull(((int)OddLotOrderColumn.Status - 1)))?null:(System.Int32?)reader[((int)OddLotOrderColumn.Status - 1)];
			entity.BrokerId = (reader.IsDBNull(((int)OddLotOrderColumn.BrokerId - 1)))?null:(System.String)reader[((int)OddLotOrderColumn.BrokerId - 1)];
			entity.RequestTime = (System.DateTime)reader[((int)OddLotOrderColumn.RequestTime - 1)];
			entity.ExecTime = (reader.IsDBNull(((int)OddLotOrderColumn.ExecTime - 1)))?null:(System.DateTime?)reader[((int)OddLotOrderColumn.ExecTime - 1)];
			entity.Note = (reader.IsDBNull(((int)OddLotOrderColumn.Note - 1)))?null:(System.String)reader[((int)OddLotOrderColumn.Note - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ETradeFinance.Entities.OddLotOrder"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ETradeFinance.Entities.OddLotOrder"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ETradeFinance.Entities.OddLotOrder entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int64)dataRow["ID"];
			entity.SecSymbol = (System.String)dataRow["SecSymbol"];
			entity.Side = (System.String)dataRow["Side"];
			entity.Price = Convert.IsDBNull(dataRow["Price"]) ? null : (System.Decimal?)dataRow["Price"];
			entity.Volume = Convert.IsDBNull(dataRow["Volume"]) ? null : (System.Int64?)dataRow["Volume"];
			entity.SubCustAccountId = (System.String)dataRow["SubCustAccountID"];
			entity.Market = (System.String)dataRow["Market"];
			entity.ExecPrice = Convert.IsDBNull(dataRow["ExecPrice"]) ? null : (System.Decimal?)dataRow["ExecPrice"];
			entity.ExecVol = Convert.IsDBNull(dataRow["ExecVol"]) ? null : (System.Int64?)dataRow["ExecVol"];
			entity.CanceledVol = Convert.IsDBNull(dataRow["CanceledVol"]) ? null : (System.Int64?)dataRow["CanceledVol"];
			entity.Status = Convert.IsDBNull(dataRow["Status"]) ? null : (System.Int32?)dataRow["Status"];
			entity.BrokerId = Convert.IsDBNull(dataRow["BrokerID"]) ? null : (System.String)dataRow["BrokerID"];
			entity.RequestTime = (System.DateTime)dataRow["RequestTime"];
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
		/// <param name="entity">The <see cref="ETradeFinance.Entities.OddLotOrder"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ETradeFinance.Entities.OddLotOrder Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ETradeFinance.Entities.OddLotOrder entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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
		/// Deep Save the entire object graph of the ETradeFinance.Entities.OddLotOrder object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ETradeFinance.Entities.OddLotOrder instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ETradeFinance.Entities.OddLotOrder Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ETradeFinance.Entities.OddLotOrder entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
	#region OddLotOrderChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ETradeFinance.Entities.OddLotOrder</c>
	///</summary>
	public enum OddLotOrderChildEntityTypes
	{
	}
	
	#endregion OddLotOrderChildEntityTypes
	
	#region OddLotOrderFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;OddLotOrderColumn&gt;"/> class
	/// that is used exclusively with a <see cref="OddLotOrder"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class OddLotOrderFilterBuilder : SqlFilterBuilder<OddLotOrderColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the OddLotOrderFilterBuilder class.
		/// </summary>
		public OddLotOrderFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the OddLotOrderFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public OddLotOrderFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the OddLotOrderFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public OddLotOrderFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion OddLotOrderFilterBuilder
	
	#region OddLotOrderParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;OddLotOrderColumn&gt;"/> class
	/// that is used exclusively with a <see cref="OddLotOrder"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class OddLotOrderParameterBuilder : ParameterizedSqlFilterBuilder<OddLotOrderColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the OddLotOrderParameterBuilder class.
		/// </summary>
		public OddLotOrderParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the OddLotOrderParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public OddLotOrderParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the OddLotOrderParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public OddLotOrderParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion OddLotOrderParameterBuilder
	
	#region OddLotOrderSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;OddLotOrderColumn&gt;"/> class
	/// that is used exclusively with a <see cref="OddLotOrder"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class OddLotOrderSortBuilder : SqlSortBuilder<OddLotOrderColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the OddLotOrderSqlSortBuilder class.
		/// </summary>
		public OddLotOrderSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion OddLotOrderSortBuilder
	
} // end namespace
