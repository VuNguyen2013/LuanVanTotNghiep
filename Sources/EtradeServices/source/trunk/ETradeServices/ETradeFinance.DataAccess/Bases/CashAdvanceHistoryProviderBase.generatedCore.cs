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
	/// This class is the base class for any <see cref="CashAdvanceHistoryProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class CashAdvanceHistoryProviderBaseCore : EntityProviderBase<ETradeFinance.Entities.CashAdvanceHistory, ETradeFinance.Entities.CashAdvanceHistoryKey>
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
		public override bool Delete(TransactionManager transactionManager, ETradeFinance.Entities.CashAdvanceHistoryKey key)
		{
			return Delete(transactionManager, key.Id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_id">CashAdvanceID identifies CashAdvance. Primary Key.</param>
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
		/// <param name="_id">CashAdvanceID identifies CashAdvance. Primary Key.</param>
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
		public override ETradeFinance.Entities.CashAdvanceHistory Get(TransactionManager transactionManager, ETradeFinance.Entities.CashAdvanceHistoryKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_CashAdvanceHistory index.
		/// </summary>
		/// <param name="_id">CashAdvanceID identifies CashAdvance</param>
		/// <returns>Returns an instance of the <see cref="ETradeFinance.Entities.CashAdvanceHistory"/> class.</returns>
		public ETradeFinance.Entities.CashAdvanceHistory GetById(System.Int64 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CashAdvanceHistory index.
		/// </summary>
		/// <param name="_id">CashAdvanceID identifies CashAdvance</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeFinance.Entities.CashAdvanceHistory"/> class.</returns>
		public ETradeFinance.Entities.CashAdvanceHistory GetById(System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CashAdvanceHistory index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">CashAdvanceID identifies CashAdvance</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeFinance.Entities.CashAdvanceHistory"/> class.</returns>
		public ETradeFinance.Entities.CashAdvanceHistory GetById(TransactionManager transactionManager, System.Int64 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CashAdvanceHistory index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">CashAdvanceID identifies CashAdvance</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeFinance.Entities.CashAdvanceHistory"/> class.</returns>
		public ETradeFinance.Entities.CashAdvanceHistory GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CashAdvanceHistory index.
		/// </summary>
		/// <param name="_id">CashAdvanceID identifies CashAdvance</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeFinance.Entities.CashAdvanceHistory"/> class.</returns>
		public ETradeFinance.Entities.CashAdvanceHistory GetById(System.Int64 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CashAdvanceHistory index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">CashAdvanceID identifies CashAdvance</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ETradeFinance.Entities.CashAdvanceHistory"/> class.</returns>
		public abstract ETradeFinance.Entities.CashAdvanceHistory GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;CashAdvanceHistory&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;CashAdvanceHistory&gt;"/></returns>
		public static TList<CashAdvanceHistory> Fill(IDataReader reader, TList<CashAdvanceHistory> rows, int start, int pageLength)
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
				
				ETradeFinance.Entities.CashAdvanceHistory c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("CashAdvanceHistory")
					.Append("|").Append((System.Int64)reader[((int)CashAdvanceHistoryColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<CashAdvanceHistory>(
					key.ToString(), // EntityTrackingKey
					"CashAdvanceHistory",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ETradeFinance.Entities.CashAdvanceHistory();
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
					c.Id = (System.Int64)reader[((int)CashAdvanceHistoryColumn.Id - 1)];
					c.SubAccountId = (System.String)reader[((int)CashAdvanceHistoryColumn.SubAccountId - 1)];
					c.AdvanceDate = (System.DateTime)reader[((int)CashAdvanceHistoryColumn.AdvanceDate - 1)];
					c.ContractNo = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.ContractNo - 1)))?null:(System.String)reader[((int)CashAdvanceHistoryColumn.ContractNo - 1)];
					c.OrderId = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.OrderId - 1)))?null:(System.Int32?)reader[((int)CashAdvanceHistoryColumn.OrderId - 1)];
					c.StockSymbol = (System.String)reader[((int)CashAdvanceHistoryColumn.StockSymbol - 1)];
					c.SellDueDate = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.SellDueDate - 1)))?null:(System.DateTime?)reader[((int)CashAdvanceHistoryColumn.SellDueDate - 1)];
					c.CashDueDate = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.CashDueDate - 1)))?null:(System.DateTime?)reader[((int)CashAdvanceHistoryColumn.CashDueDate - 1)];
					c.TotalSellValue = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.TotalSellValue - 1)))?null:(System.Decimal?)reader[((int)CashAdvanceHistoryColumn.TotalSellValue - 1)];
					c.CashAvilable = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.CashAvilable - 1)))?null:(System.Decimal?)reader[((int)CashAdvanceHistoryColumn.CashAvilable - 1)];
					c.CashRequest = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.CashRequest - 1)))?null:(System.Decimal?)reader[((int)CashAdvanceHistoryColumn.CashRequest - 1)];
					c.Fee = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.Fee - 1)))?null:(System.Decimal?)reader[((int)CashAdvanceHistoryColumn.Fee - 1)];
					c.CashReceived = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.CashReceived - 1)))?null:(System.Decimal?)reader[((int)CashAdvanceHistoryColumn.CashReceived - 1)];
					c.Status = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.Status - 1)))?null:(System.Int32?)reader[((int)CashAdvanceHistoryColumn.Status - 1)];
					c.TradeType = (System.Int32)reader[((int)CashAdvanceHistoryColumn.TradeType - 1)];
					c.BrokerId = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.BrokerId - 1)))?null:(System.String)reader[((int)CashAdvanceHistoryColumn.BrokerId - 1)];
					c.BrokerName = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.BrokerName - 1)))?null:(System.String)reader[((int)CashAdvanceHistoryColumn.BrokerName - 1)];
					c.Reason = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.Reason - 1)))?null:(System.String)reader[((int)CashAdvanceHistoryColumn.Reason - 1)];
					c.ExecTime = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.ExecTime - 1)))?null:(System.DateTime?)reader[((int)CashAdvanceHistoryColumn.ExecTime - 1)];
					c.Vat = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.Vat - 1)))?null:(System.Decimal?)reader[((int)CashAdvanceHistoryColumn.Vat - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="ETradeFinance.Entities.CashAdvanceHistory"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ETradeFinance.Entities.CashAdvanceHistory"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ETradeFinance.Entities.CashAdvanceHistory entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int64)reader[((int)CashAdvanceHistoryColumn.Id - 1)];
			entity.SubAccountId = (System.String)reader[((int)CashAdvanceHistoryColumn.SubAccountId - 1)];
			entity.AdvanceDate = (System.DateTime)reader[((int)CashAdvanceHistoryColumn.AdvanceDate - 1)];
			entity.ContractNo = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.ContractNo - 1)))?null:(System.String)reader[((int)CashAdvanceHistoryColumn.ContractNo - 1)];
			entity.OrderId = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.OrderId - 1)))?null:(System.Int32?)reader[((int)CashAdvanceHistoryColumn.OrderId - 1)];
			entity.StockSymbol = (System.String)reader[((int)CashAdvanceHistoryColumn.StockSymbol - 1)];
			entity.SellDueDate = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.SellDueDate - 1)))?null:(System.DateTime?)reader[((int)CashAdvanceHistoryColumn.SellDueDate - 1)];
			entity.CashDueDate = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.CashDueDate - 1)))?null:(System.DateTime?)reader[((int)CashAdvanceHistoryColumn.CashDueDate - 1)];
			entity.TotalSellValue = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.TotalSellValue - 1)))?null:(System.Decimal?)reader[((int)CashAdvanceHistoryColumn.TotalSellValue - 1)];
			entity.CashAvilable = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.CashAvilable - 1)))?null:(System.Decimal?)reader[((int)CashAdvanceHistoryColumn.CashAvilable - 1)];
			entity.CashRequest = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.CashRequest - 1)))?null:(System.Decimal?)reader[((int)CashAdvanceHistoryColumn.CashRequest - 1)];
			entity.Fee = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.Fee - 1)))?null:(System.Decimal?)reader[((int)CashAdvanceHistoryColumn.Fee - 1)];
			entity.CashReceived = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.CashReceived - 1)))?null:(System.Decimal?)reader[((int)CashAdvanceHistoryColumn.CashReceived - 1)];
			entity.Status = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.Status - 1)))?null:(System.Int32?)reader[((int)CashAdvanceHistoryColumn.Status - 1)];
			entity.TradeType = (System.Int32)reader[((int)CashAdvanceHistoryColumn.TradeType - 1)];
			entity.BrokerId = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.BrokerId - 1)))?null:(System.String)reader[((int)CashAdvanceHistoryColumn.BrokerId - 1)];
			entity.BrokerName = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.BrokerName - 1)))?null:(System.String)reader[((int)CashAdvanceHistoryColumn.BrokerName - 1)];
			entity.Reason = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.Reason - 1)))?null:(System.String)reader[((int)CashAdvanceHistoryColumn.Reason - 1)];
			entity.ExecTime = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.ExecTime - 1)))?null:(System.DateTime?)reader[((int)CashAdvanceHistoryColumn.ExecTime - 1)];
			entity.Vat = (reader.IsDBNull(((int)CashAdvanceHistoryColumn.Vat - 1)))?null:(System.Decimal?)reader[((int)CashAdvanceHistoryColumn.Vat - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ETradeFinance.Entities.CashAdvanceHistory"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ETradeFinance.Entities.CashAdvanceHistory"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ETradeFinance.Entities.CashAdvanceHistory entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int64)dataRow["ID"];
			entity.SubAccountId = (System.String)dataRow["SubAccountID"];
			entity.AdvanceDate = (System.DateTime)dataRow["AdvanceDate"];
			entity.ContractNo = Convert.IsDBNull(dataRow["ContractNo"]) ? null : (System.String)dataRow["ContractNo"];
			entity.OrderId = Convert.IsDBNull(dataRow["OrderID"]) ? null : (System.Int32?)dataRow["OrderID"];
			entity.StockSymbol = (System.String)dataRow["StockSymbol"];
			entity.SellDueDate = Convert.IsDBNull(dataRow["SellDueDate"]) ? null : (System.DateTime?)dataRow["SellDueDate"];
			entity.CashDueDate = Convert.IsDBNull(dataRow["CashDueDate"]) ? null : (System.DateTime?)dataRow["CashDueDate"];
			entity.TotalSellValue = Convert.IsDBNull(dataRow["TotalSellValue"]) ? null : (System.Decimal?)dataRow["TotalSellValue"];
			entity.CashAvilable = Convert.IsDBNull(dataRow["CashAvilable"]) ? null : (System.Decimal?)dataRow["CashAvilable"];
			entity.CashRequest = Convert.IsDBNull(dataRow["CashRequest"]) ? null : (System.Decimal?)dataRow["CashRequest"];
			entity.Fee = Convert.IsDBNull(dataRow["Fee"]) ? null : (System.Decimal?)dataRow["Fee"];
			entity.CashReceived = Convert.IsDBNull(dataRow["CashReceived"]) ? null : (System.Decimal?)dataRow["CashReceived"];
			entity.Status = Convert.IsDBNull(dataRow["Status"]) ? null : (System.Int32?)dataRow["Status"];
			entity.TradeType = (System.Int32)dataRow["TradeType"];
			entity.BrokerId = Convert.IsDBNull(dataRow["BrokerID"]) ? null : (System.String)dataRow["BrokerID"];
			entity.BrokerName = Convert.IsDBNull(dataRow["BrokerName"]) ? null : (System.String)dataRow["BrokerName"];
			entity.Reason = Convert.IsDBNull(dataRow["Reason"]) ? null : (System.String)dataRow["Reason"];
			entity.ExecTime = Convert.IsDBNull(dataRow["ExecTime"]) ? null : (System.DateTime?)dataRow["ExecTime"];
			entity.Vat = Convert.IsDBNull(dataRow["VAT"]) ? null : (System.Decimal?)dataRow["VAT"];
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
		/// <param name="entity">The <see cref="ETradeFinance.Entities.CashAdvanceHistory"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ETradeFinance.Entities.CashAdvanceHistory Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ETradeFinance.Entities.CashAdvanceHistory entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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
		/// Deep Save the entire object graph of the ETradeFinance.Entities.CashAdvanceHistory object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ETradeFinance.Entities.CashAdvanceHistory instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ETradeFinance.Entities.CashAdvanceHistory Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ETradeFinance.Entities.CashAdvanceHistory entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
	#region CashAdvanceHistoryChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ETradeFinance.Entities.CashAdvanceHistory</c>
	///</summary>
	public enum CashAdvanceHistoryChildEntityTypes
	{
	}
	
	#endregion CashAdvanceHistoryChildEntityTypes
	
	#region CashAdvanceHistoryFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;CashAdvanceHistoryColumn&gt;"/> class
	/// that is used exclusively with a <see cref="CashAdvanceHistory"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CashAdvanceHistoryFilterBuilder : SqlFilterBuilder<CashAdvanceHistoryColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CashAdvanceHistoryFilterBuilder class.
		/// </summary>
		public CashAdvanceHistoryFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the CashAdvanceHistoryFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CashAdvanceHistoryFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CashAdvanceHistoryFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CashAdvanceHistoryFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CashAdvanceHistoryFilterBuilder
	
	#region CashAdvanceHistoryParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;CashAdvanceHistoryColumn&gt;"/> class
	/// that is used exclusively with a <see cref="CashAdvanceHistory"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CashAdvanceHistoryParameterBuilder : ParameterizedSqlFilterBuilder<CashAdvanceHistoryColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CashAdvanceHistoryParameterBuilder class.
		/// </summary>
		public CashAdvanceHistoryParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the CashAdvanceHistoryParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CashAdvanceHistoryParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CashAdvanceHistoryParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CashAdvanceHistoryParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CashAdvanceHistoryParameterBuilder
	
	#region CashAdvanceHistorySortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;CashAdvanceHistoryColumn&gt;"/> class
	/// that is used exclusively with a <see cref="CashAdvanceHistory"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class CashAdvanceHistorySortBuilder : SqlSortBuilder<CashAdvanceHistoryColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CashAdvanceHistorySqlSortBuilder class.
		/// </summary>
		public CashAdvanceHistorySortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion CashAdvanceHistorySortBuilder
	
} // end namespace
