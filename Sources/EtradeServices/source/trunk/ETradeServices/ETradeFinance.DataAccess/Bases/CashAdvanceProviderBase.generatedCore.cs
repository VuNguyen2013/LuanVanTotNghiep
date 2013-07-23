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
	/// This class is the base class for any <see cref="CashAdvanceProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class CashAdvanceProviderBaseCore : EntityProviderBase<ETradeFinance.Entities.CashAdvance, ETradeFinance.Entities.CashAdvanceKey>
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
		public override bool Delete(TransactionManager transactionManager, ETradeFinance.Entities.CashAdvanceKey key)
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
		public override ETradeFinance.Entities.CashAdvance Get(TransactionManager transactionManager, ETradeFinance.Entities.CashAdvanceKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_CashAdvance index.
		/// </summary>
		/// <param name="_id">CashAdvanceID identifies CashAdvance</param>
		/// <returns>Returns an instance of the <see cref="ETradeFinance.Entities.CashAdvance"/> class.</returns>
		public ETradeFinance.Entities.CashAdvance GetById(System.Int64 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CashAdvance index.
		/// </summary>
		/// <param name="_id">CashAdvanceID identifies CashAdvance</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeFinance.Entities.CashAdvance"/> class.</returns>
		public ETradeFinance.Entities.CashAdvance GetById(System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CashAdvance index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">CashAdvanceID identifies CashAdvance</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeFinance.Entities.CashAdvance"/> class.</returns>
		public ETradeFinance.Entities.CashAdvance GetById(TransactionManager transactionManager, System.Int64 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CashAdvance index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">CashAdvanceID identifies CashAdvance</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeFinance.Entities.CashAdvance"/> class.</returns>
		public ETradeFinance.Entities.CashAdvance GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CashAdvance index.
		/// </summary>
		/// <param name="_id">CashAdvanceID identifies CashAdvance</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeFinance.Entities.CashAdvance"/> class.</returns>
		public ETradeFinance.Entities.CashAdvance GetById(System.Int64 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CashAdvance index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">CashAdvanceID identifies CashAdvance</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ETradeFinance.Entities.CashAdvance"/> class.</returns>
		public abstract ETradeFinance.Entities.CashAdvance GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#region _CashAdvance_DeleteOldData 
		
		/// <summary>
		///	This method wrap the '_CashAdvance_DeleteOldData' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void DeleteOldData()
		{
			 DeleteOldData(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the '_CashAdvance_DeleteOldData' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void DeleteOldData(int start, int pageLength)
		{
			 DeleteOldData(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the '_CashAdvance_DeleteOldData' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void DeleteOldData(TransactionManager transactionManager)
		{
			 DeleteOldData(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the '_CashAdvance_DeleteOldData' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public abstract void DeleteOldData(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;CashAdvance&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;CashAdvance&gt;"/></returns>
		public static TList<CashAdvance> Fill(IDataReader reader, TList<CashAdvance> rows, int start, int pageLength)
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
				
				ETradeFinance.Entities.CashAdvance c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("CashAdvance")
					.Append("|").Append((System.Int64)reader[((int)CashAdvanceColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<CashAdvance>(
					key.ToString(), // EntityTrackingKey
					"CashAdvance",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ETradeFinance.Entities.CashAdvance();
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
					c.Id = (System.Int64)reader[((int)CashAdvanceColumn.Id - 1)];
					c.SubAccountId = (System.String)reader[((int)CashAdvanceColumn.SubAccountId - 1)];
					c.AdvanceDate = (System.DateTime)reader[((int)CashAdvanceColumn.AdvanceDate - 1)];
					c.ContractNo = (reader.IsDBNull(((int)CashAdvanceColumn.ContractNo - 1)))?null:(System.String)reader[((int)CashAdvanceColumn.ContractNo - 1)];
					c.OrderId = (reader.IsDBNull(((int)CashAdvanceColumn.OrderId - 1)))?null:(System.Int32?)reader[((int)CashAdvanceColumn.OrderId - 1)];
					c.StockSymbol = (System.String)reader[((int)CashAdvanceColumn.StockSymbol - 1)];
					c.SellDueDate = (reader.IsDBNull(((int)CashAdvanceColumn.SellDueDate - 1)))?null:(System.DateTime?)reader[((int)CashAdvanceColumn.SellDueDate - 1)];
					c.CashDueDate = (reader.IsDBNull(((int)CashAdvanceColumn.CashDueDate - 1)))?null:(System.DateTime?)reader[((int)CashAdvanceColumn.CashDueDate - 1)];
					c.TotalSellValue = (reader.IsDBNull(((int)CashAdvanceColumn.TotalSellValue - 1)))?null:(System.Decimal?)reader[((int)CashAdvanceColumn.TotalSellValue - 1)];
					c.CashAvailable = (reader.IsDBNull(((int)CashAdvanceColumn.CashAvailable - 1)))?null:(System.Decimal?)reader[((int)CashAdvanceColumn.CashAvailable - 1)];
					c.CashRequest = (reader.IsDBNull(((int)CashAdvanceColumn.CashRequest - 1)))?null:(System.Decimal?)reader[((int)CashAdvanceColumn.CashRequest - 1)];
					c.Fee = (reader.IsDBNull(((int)CashAdvanceColumn.Fee - 1)))?null:(System.Decimal?)reader[((int)CashAdvanceColumn.Fee - 1)];
					c.CashReceived = (reader.IsDBNull(((int)CashAdvanceColumn.CashReceived - 1)))?null:(System.Decimal?)reader[((int)CashAdvanceColumn.CashReceived - 1)];
					c.Status = (reader.IsDBNull(((int)CashAdvanceColumn.Status - 1)))?null:(System.Int32?)reader[((int)CashAdvanceColumn.Status - 1)];
					c.TradeType = (System.Int32)reader[((int)CashAdvanceColumn.TradeType - 1)];
					c.BrokerId = (reader.IsDBNull(((int)CashAdvanceColumn.BrokerId - 1)))?null:(System.String)reader[((int)CashAdvanceColumn.BrokerId - 1)];
					c.Reason = (reader.IsDBNull(((int)CashAdvanceColumn.Reason - 1)))?null:(System.String)reader[((int)CashAdvanceColumn.Reason - 1)];
					c.ExecTime = (reader.IsDBNull(((int)CashAdvanceColumn.ExecTime - 1)))?null:(System.DateTime?)reader[((int)CashAdvanceColumn.ExecTime - 1)];
					c.Vat = (reader.IsDBNull(((int)CashAdvanceColumn.Vat - 1)))?null:(System.Decimal?)reader[((int)CashAdvanceColumn.Vat - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="ETradeFinance.Entities.CashAdvance"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ETradeFinance.Entities.CashAdvance"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ETradeFinance.Entities.CashAdvance entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int64)reader[((int)CashAdvanceColumn.Id - 1)];
			entity.SubAccountId = (System.String)reader[((int)CashAdvanceColumn.SubAccountId - 1)];
			entity.AdvanceDate = (System.DateTime)reader[((int)CashAdvanceColumn.AdvanceDate - 1)];
			entity.ContractNo = (reader.IsDBNull(((int)CashAdvanceColumn.ContractNo - 1)))?null:(System.String)reader[((int)CashAdvanceColumn.ContractNo - 1)];
			entity.OrderId = (reader.IsDBNull(((int)CashAdvanceColumn.OrderId - 1)))?null:(System.Int32?)reader[((int)CashAdvanceColumn.OrderId - 1)];
			entity.StockSymbol = (System.String)reader[((int)CashAdvanceColumn.StockSymbol - 1)];
			entity.SellDueDate = (reader.IsDBNull(((int)CashAdvanceColumn.SellDueDate - 1)))?null:(System.DateTime?)reader[((int)CashAdvanceColumn.SellDueDate - 1)];
			entity.CashDueDate = (reader.IsDBNull(((int)CashAdvanceColumn.CashDueDate - 1)))?null:(System.DateTime?)reader[((int)CashAdvanceColumn.CashDueDate - 1)];
			entity.TotalSellValue = (reader.IsDBNull(((int)CashAdvanceColumn.TotalSellValue - 1)))?null:(System.Decimal?)reader[((int)CashAdvanceColumn.TotalSellValue - 1)];
			entity.CashAvailable = (reader.IsDBNull(((int)CashAdvanceColumn.CashAvailable - 1)))?null:(System.Decimal?)reader[((int)CashAdvanceColumn.CashAvailable - 1)];
			entity.CashRequest = (reader.IsDBNull(((int)CashAdvanceColumn.CashRequest - 1)))?null:(System.Decimal?)reader[((int)CashAdvanceColumn.CashRequest - 1)];
			entity.Fee = (reader.IsDBNull(((int)CashAdvanceColumn.Fee - 1)))?null:(System.Decimal?)reader[((int)CashAdvanceColumn.Fee - 1)];
			entity.CashReceived = (reader.IsDBNull(((int)CashAdvanceColumn.CashReceived - 1)))?null:(System.Decimal?)reader[((int)CashAdvanceColumn.CashReceived - 1)];
			entity.Status = (reader.IsDBNull(((int)CashAdvanceColumn.Status - 1)))?null:(System.Int32?)reader[((int)CashAdvanceColumn.Status - 1)];
			entity.TradeType = (System.Int32)reader[((int)CashAdvanceColumn.TradeType - 1)];
			entity.BrokerId = (reader.IsDBNull(((int)CashAdvanceColumn.BrokerId - 1)))?null:(System.String)reader[((int)CashAdvanceColumn.BrokerId - 1)];
			entity.Reason = (reader.IsDBNull(((int)CashAdvanceColumn.Reason - 1)))?null:(System.String)reader[((int)CashAdvanceColumn.Reason - 1)];
			entity.ExecTime = (reader.IsDBNull(((int)CashAdvanceColumn.ExecTime - 1)))?null:(System.DateTime?)reader[((int)CashAdvanceColumn.ExecTime - 1)];
			entity.Vat = (reader.IsDBNull(((int)CashAdvanceColumn.Vat - 1)))?null:(System.Decimal?)reader[((int)CashAdvanceColumn.Vat - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ETradeFinance.Entities.CashAdvance"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ETradeFinance.Entities.CashAdvance"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ETradeFinance.Entities.CashAdvance entity)
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
			entity.CashAvailable = Convert.IsDBNull(dataRow["CashAvailable"]) ? null : (System.Decimal?)dataRow["CashAvailable"];
			entity.CashRequest = Convert.IsDBNull(dataRow["CashRequest"]) ? null : (System.Decimal?)dataRow["CashRequest"];
			entity.Fee = Convert.IsDBNull(dataRow["Fee"]) ? null : (System.Decimal?)dataRow["Fee"];
			entity.CashReceived = Convert.IsDBNull(dataRow["CashReceived"]) ? null : (System.Decimal?)dataRow["CashReceived"];
			entity.Status = Convert.IsDBNull(dataRow["Status"]) ? null : (System.Int32?)dataRow["Status"];
			entity.TradeType = (System.Int32)dataRow["TradeType"];
			entity.BrokerId = Convert.IsDBNull(dataRow["BrokerID"]) ? null : (System.String)dataRow["BrokerID"];
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
		/// <param name="entity">The <see cref="ETradeFinance.Entities.CashAdvance"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ETradeFinance.Entities.CashAdvance Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ETradeFinance.Entities.CashAdvance entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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
		/// Deep Save the entire object graph of the ETradeFinance.Entities.CashAdvance object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ETradeFinance.Entities.CashAdvance instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ETradeFinance.Entities.CashAdvance Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ETradeFinance.Entities.CashAdvance entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
	#region CashAdvanceChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ETradeFinance.Entities.CashAdvance</c>
	///</summary>
	public enum CashAdvanceChildEntityTypes
	{
	}
	
	#endregion CashAdvanceChildEntityTypes
	
	#region CashAdvanceFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;CashAdvanceColumn&gt;"/> class
	/// that is used exclusively with a <see cref="CashAdvance"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CashAdvanceFilterBuilder : SqlFilterBuilder<CashAdvanceColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CashAdvanceFilterBuilder class.
		/// </summary>
		public CashAdvanceFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the CashAdvanceFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CashAdvanceFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CashAdvanceFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CashAdvanceFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CashAdvanceFilterBuilder
	
	#region CashAdvanceParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;CashAdvanceColumn&gt;"/> class
	/// that is used exclusively with a <see cref="CashAdvance"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CashAdvanceParameterBuilder : ParameterizedSqlFilterBuilder<CashAdvanceColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CashAdvanceParameterBuilder class.
		/// </summary>
		public CashAdvanceParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the CashAdvanceParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CashAdvanceParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CashAdvanceParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CashAdvanceParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CashAdvanceParameterBuilder
	
	#region CashAdvanceSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;CashAdvanceColumn&gt;"/> class
	/// that is used exclusively with a <see cref="CashAdvance"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class CashAdvanceSortBuilder : SqlSortBuilder<CashAdvanceColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CashAdvanceSqlSortBuilder class.
		/// </summary>
		public CashAdvanceSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion CashAdvanceSortBuilder
	
} // end namespace
