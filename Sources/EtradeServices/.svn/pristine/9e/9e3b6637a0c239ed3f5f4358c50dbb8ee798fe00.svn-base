#region Using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

using ETradeOrders.Entities;
using ETradeOrders.DataAccess;

#endregion

namespace ETradeOrders.DataAccess.Bases
{	
	///<summary>
	/// This class is the base class for any <see cref="ExecOrderProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class ExecOrderProviderBaseCore : EntityProviderBase<ETradeOrders.Entities.ExecOrder, ETradeOrders.Entities.ExecOrderKey>
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
		public override bool Delete(TransactionManager transactionManager, ETradeOrders.Entities.ExecOrderKey key)
		{
			return Delete(transactionManager, key.OrderId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_orderId">OrderID identifies ExecOrder. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(int _orderId)
		{
			return Delete(null, _orderId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_orderId">OrderID identifies ExecOrder. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, int _orderId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ExecOrder_ConditionOrder key.
		///		FK_ExecOrder_ConditionOrder Description: 
		/// </summary>
		/// <param name="_conditionOrderId">ConditionOrderID is of ExecOrder</param>
		/// <returns>Returns a typed collection of ETradeOrders.Entities.ExecOrder objects.</returns>
		public TList<ExecOrder> GetByConditionOrderId(System.Int64? _conditionOrderId)
		{
			int count = -1;
			return GetByConditionOrderId(_conditionOrderId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ExecOrder_ConditionOrder key.
		///		FK_ExecOrder_ConditionOrder Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_conditionOrderId">ConditionOrderID is of ExecOrder</param>
		/// <returns>Returns a typed collection of ETradeOrders.Entities.ExecOrder objects.</returns>
		/// <remarks></remarks>
		public TList<ExecOrder> GetByConditionOrderId(TransactionManager transactionManager, System.Int64? _conditionOrderId)
		{
			int count = -1;
			return GetByConditionOrderId(transactionManager, _conditionOrderId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_ExecOrder_ConditionOrder key.
		///		FK_ExecOrder_ConditionOrder Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_conditionOrderId">ConditionOrderID is of ExecOrder</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ETradeOrders.Entities.ExecOrder objects.</returns>
		public TList<ExecOrder> GetByConditionOrderId(TransactionManager transactionManager, System.Int64? _conditionOrderId, int start, int pageLength)
		{
			int count = -1;
			return GetByConditionOrderId(transactionManager, _conditionOrderId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ExecOrder_ConditionOrder key.
		///		fkExecOrderConditionOrder Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_conditionOrderId">ConditionOrderID is of ExecOrder</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ETradeOrders.Entities.ExecOrder objects.</returns>
		public TList<ExecOrder> GetByConditionOrderId(System.Int64? _conditionOrderId, int start, int pageLength)
		{
			int count =  -1;
			return GetByConditionOrderId(null, _conditionOrderId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ExecOrder_ConditionOrder key.
		///		fkExecOrderConditionOrder Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_conditionOrderId">ConditionOrderID is of ExecOrder</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ETradeOrders.Entities.ExecOrder objects.</returns>
		public TList<ExecOrder> GetByConditionOrderId(System.Int64? _conditionOrderId, int start, int pageLength,out int count)
		{
			return GetByConditionOrderId(null, _conditionOrderId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ExecOrder_ConditionOrder key.
		///		FK_ExecOrder_ConditionOrder Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_conditionOrderId">ConditionOrderID is of ExecOrder</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of ETradeOrders.Entities.ExecOrder objects.</returns>
		public abstract TList<ExecOrder> GetByConditionOrderId(TransactionManager transactionManager, System.Int64? _conditionOrderId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ExecOrder_QuickOrder key.
		///		FK_ExecOrder_QuickOrder Description: 
		/// </summary>
		/// <param name="_quickOrderId">ID is of OrderInfo</param>
		/// <returns>Returns a typed collection of ETradeOrders.Entities.ExecOrder objects.</returns>
		public TList<ExecOrder> GetByQuickOrderId(System.Int32? _quickOrderId)
		{
			int count = -1;
			return GetByQuickOrderId(_quickOrderId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ExecOrder_QuickOrder key.
		///		FK_ExecOrder_QuickOrder Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_quickOrderId">ID is of OrderInfo</param>
		/// <returns>Returns a typed collection of ETradeOrders.Entities.ExecOrder objects.</returns>
		/// <remarks></remarks>
		public TList<ExecOrder> GetByQuickOrderId(TransactionManager transactionManager, System.Int32? _quickOrderId)
		{
			int count = -1;
			return GetByQuickOrderId(transactionManager, _quickOrderId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_ExecOrder_QuickOrder key.
		///		FK_ExecOrder_QuickOrder Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_quickOrderId">ID is of OrderInfo</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ETradeOrders.Entities.ExecOrder objects.</returns>
		public TList<ExecOrder> GetByQuickOrderId(TransactionManager transactionManager, System.Int32? _quickOrderId, int start, int pageLength)
		{
			int count = -1;
			return GetByQuickOrderId(transactionManager, _quickOrderId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ExecOrder_QuickOrder key.
		///		fkExecOrderQuickOrder Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_quickOrderId">ID is of OrderInfo</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ETradeOrders.Entities.ExecOrder objects.</returns>
		public TList<ExecOrder> GetByQuickOrderId(System.Int32? _quickOrderId, int start, int pageLength)
		{
			int count =  -1;
			return GetByQuickOrderId(null, _quickOrderId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ExecOrder_QuickOrder key.
		///		fkExecOrderQuickOrder Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_quickOrderId">ID is of OrderInfo</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ETradeOrders.Entities.ExecOrder objects.</returns>
		public TList<ExecOrder> GetByQuickOrderId(System.Int32? _quickOrderId, int start, int pageLength,out int count)
		{
			return GetByQuickOrderId(null, _quickOrderId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ExecOrder_QuickOrder key.
		///		FK_ExecOrder_QuickOrder Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_quickOrderId">ID is of OrderInfo</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of ETradeOrders.Entities.ExecOrder objects.</returns>
		public abstract TList<ExecOrder> GetByQuickOrderId(TransactionManager transactionManager, System.Int32? _quickOrderId, int start, int pageLength, out int count);
		
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
		public override ETradeOrders.Entities.ExecOrder Get(TransactionManager transactionManager, ETradeOrders.Entities.ExecOrderKey key, int start, int pageLength)
		{
			return GetByOrderId(transactionManager, key.OrderId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_ExecOrder index.
		/// </summary>
		/// <param name="_orderId">OrderID identifies ExecOrder</param>
		/// <returns>Returns an instance of the <see cref="ETradeOrders.Entities.ExecOrder"/> class.</returns>
		public ETradeOrders.Entities.ExecOrder GetByOrderId(int _orderId)
		{
			int count = -1;
			return GetByOrderId(null,_orderId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_ExecOrder index.
		/// </summary>
		/// <param name="_orderId">OrderID identifies ExecOrder</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeOrders.Entities.ExecOrder"/> class.</returns>
		public ETradeOrders.Entities.ExecOrder GetByOrderId(int _orderId, int start, int pageLength)
		{
			int count = -1;
			return GetByOrderId(null, _orderId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_ExecOrder index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_orderId">OrderID identifies ExecOrder</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeOrders.Entities.ExecOrder"/> class.</returns>
		public ETradeOrders.Entities.ExecOrder GetByOrderId(TransactionManager transactionManager, int _orderId)
		{
			int count = -1;
			return GetByOrderId(transactionManager, _orderId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_ExecOrder index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_orderId">OrderID identifies ExecOrder</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeOrders.Entities.ExecOrder"/> class.</returns>
		public ETradeOrders.Entities.ExecOrder GetByOrderId(TransactionManager transactionManager, int _orderId, int start, int pageLength)
		{
			int count = -1;
			return GetByOrderId(transactionManager, _orderId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_ExecOrder index.
		/// </summary>
		/// <param name="_orderId">OrderID identifies ExecOrder</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeOrders.Entities.ExecOrder"/> class.</returns>
		public ETradeOrders.Entities.ExecOrder GetByOrderId(int _orderId, int start, int pageLength, out int count)
		{
			return GetByOrderId(null, _orderId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_ExecOrder index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_orderId">OrderID identifies ExecOrder</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ETradeOrders.Entities.ExecOrder"/> class.</returns>
		public abstract ETradeOrders.Entities.ExecOrder GetByOrderId(TransactionManager transactionManager, int _orderId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#region _ExecOrder_GetMaxSequence 
		
		/// <summary>
		///	This method wrap the '_ExecOrder_GetMaxSequence' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet GetMaxSequence()
		{
			return GetMaxSequence(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the '_ExecOrder_GetMaxSequence' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet GetMaxSequence(int start, int pageLength)
		{
			return GetMaxSequence(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the '_ExecOrder_GetMaxSequence' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet GetMaxSequence(TransactionManager transactionManager)
		{
			return GetMaxSequence(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the '_ExecOrder_GetMaxSequence' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet GetMaxSequence(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region _ExecOrder_DeleteOldData 
		
		/// <summary>
		///	This method wrap the '_ExecOrder_DeleteOldData' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void DeleteOldData()
		{
			 DeleteOldData(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the '_ExecOrder_DeleteOldData' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void DeleteOldData(int start, int pageLength)
		{
			 DeleteOldData(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the '_ExecOrder_DeleteOldData' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void DeleteOldData(TransactionManager transactionManager)
		{
			 DeleteOldData(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the '_ExecOrder_DeleteOldData' stored procedure. 
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
		/// Fill a TList&lt;ExecOrder&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;ExecOrder&gt;"/></returns>
		public static TList<ExecOrder> Fill(IDataReader reader, TList<ExecOrder> rows, int start, int pageLength)
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
				
				ETradeOrders.Entities.ExecOrder c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("ExecOrder")
					.Append("|").Append((int)reader[((int)ExecOrderColumn.OrderId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<ExecOrder>(
					key.ToString(), // EntityTrackingKey
					"ExecOrder",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ETradeOrders.Entities.ExecOrder();
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
					c.OrderId = (int)reader[((int)ExecOrderColumn.OrderId - 1)];
					c.RefOrderId = (reader.IsDBNull(((int)ExecOrderColumn.RefOrderId - 1)))?null:(string)reader[((int)ExecOrderColumn.RefOrderId - 1)];
					c.MessageType = (reader.IsDBNull(((int)ExecOrderColumn.MessageType - 1)))?null:(string)reader[((int)ExecOrderColumn.MessageType - 1)];
					c.FisOrderId = (reader.IsDBNull(((int)ExecOrderColumn.FisOrderId - 1)))?null:(System.Int32?)reader[((int)ExecOrderColumn.FisOrderId - 1)];
					c.SecSymbol = (string)reader[((int)ExecOrderColumn.SecSymbol - 1)];
					c.Side = (string)reader[((int)ExecOrderColumn.Side - 1)];
					c.Price = (decimal)reader[((int)ExecOrderColumn.Price - 1)];
					c.AvgPrice = (reader.IsDBNull(((int)ExecOrderColumn.AvgPrice - 1)))?null:(System.Decimal?)reader[((int)ExecOrderColumn.AvgPrice - 1)];
					c.ConPrice = (reader.IsDBNull(((int)ExecOrderColumn.ConPrice - 1)))?null:(string)reader[((int)ExecOrderColumn.ConPrice - 1)];
					c.Volume = (int)reader[((int)ExecOrderColumn.Volume - 1)];
					c.ExecutedVol = (reader.IsDBNull(((int)ExecOrderColumn.ExecutedVol - 1)))?null:(System.Int32?)reader[((int)ExecOrderColumn.ExecutedVol - 1)];
					c.ExecutedPrice = (reader.IsDBNull(((int)ExecOrderColumn.ExecutedPrice - 1)))?null:(System.Decimal?)reader[((int)ExecOrderColumn.ExecutedPrice - 1)];
					c.CancelVolume = (reader.IsDBNull(((int)ExecOrderColumn.CancelVolume - 1)))?null:(System.Int32?)reader[((int)ExecOrderColumn.CancelVolume - 1)];
					c.CancelledVolume = (reader.IsDBNull(((int)ExecOrderColumn.CancelledVolume - 1)))?null:(System.Int32?)reader[((int)ExecOrderColumn.CancelledVolume - 1)];
					c.SubCustAccountId = (string)reader[((int)ExecOrderColumn.SubCustAccountId - 1)];
					c.ExecTransType = (reader.IsDBNull(((int)ExecOrderColumn.ExecTransType - 1)))?null:(System.Int32?)reader[((int)ExecOrderColumn.ExecTransType - 1)];
					c.TradeTime = (reader.IsDBNull(((int)ExecOrderColumn.TradeTime - 1)))?null:(System.DateTime?)reader[((int)ExecOrderColumn.TradeTime - 1)];
					c.MatchedTime = (reader.IsDBNull(((int)ExecOrderColumn.MatchedTime - 1)))?null:(System.DateTime?)reader[((int)ExecOrderColumn.MatchedTime - 1)];
					c.CancelledTime = (reader.IsDBNull(((int)ExecOrderColumn.CancelledTime - 1)))?null:(System.DateTime?)reader[((int)ExecOrderColumn.CancelledTime - 1)];
					c.OrderStatus = (reader.IsDBNull(((int)ExecOrderColumn.OrderStatus - 1)))?null:(System.Int16?)reader[((int)ExecOrderColumn.OrderStatus - 1)];
					c.OrdRejReason = (reader.IsDBNull(((int)ExecOrderColumn.OrdRejReason - 1)))?null:(System.Int32?)reader[((int)ExecOrderColumn.OrdRejReason - 1)];
					c.ConfirmNo = (reader.IsDBNull(((int)ExecOrderColumn.ConfirmNo - 1)))?null:(string)reader[((int)ExecOrderColumn.ConfirmNo - 1)];
					c.CancelledConfirmNo = (reader.IsDBNull(((int)ExecOrderColumn.CancelledConfirmNo - 1)))?null:(string)reader[((int)ExecOrderColumn.CancelledConfirmNo - 1)];
					c.SourceId = (reader.IsDBNull(((int)ExecOrderColumn.SourceId - 1)))?null:(System.Int16?)reader[((int)ExecOrderColumn.SourceId - 1)];
					c.ExecType = (reader.IsDBNull(((int)ExecOrderColumn.ExecType - 1)))?null:(string)reader[((int)ExecOrderColumn.ExecType - 1)];
					c.CancelledExecType = (reader.IsDBNull(((int)ExecOrderColumn.CancelledExecType - 1)))?null:(string)reader[((int)ExecOrderColumn.CancelledExecType - 1)];
					c.PortOrClient = (reader.IsDBNull(((int)ExecOrderColumn.PortOrClient - 1)))?null:(string)reader[((int)ExecOrderColumn.PortOrClient - 1)];
					c.Market = (string)reader[((int)ExecOrderColumn.Market - 1)];
					c.MarketStatus = (reader.IsDBNull(((int)ExecOrderColumn.MarketStatus - 1)))?null:(string)reader[((int)ExecOrderColumn.MarketStatus - 1)];
					c.OrderSource = (reader.IsDBNull(((int)ExecOrderColumn.OrderSource - 1)))?null:(string)reader[((int)ExecOrderColumn.OrderSource - 1)];
					c.IsNewOrder = (reader.IsDBNull(((int)ExecOrderColumn.IsNewOrder - 1)))?null:(System.Boolean?)reader[((int)ExecOrderColumn.IsNewOrder - 1)];
					c.Sequence = (int)reader[((int)ExecOrderColumn.Sequence - 1)];
					c.NumOfMatch = (reader.IsDBNull(((int)ExecOrderColumn.NumOfMatch - 1)))?null:(System.Int32?)reader[((int)ExecOrderColumn.NumOfMatch - 1)];
					c.QuickOrderId = (reader.IsDBNull(((int)ExecOrderColumn.QuickOrderId - 1)))?null:(System.Int32?)reader[((int)ExecOrderColumn.QuickOrderId - 1)];
					c.ConditionOrderId = (reader.IsDBNull(((int)ExecOrderColumn.ConditionOrderId - 1)))?null:(System.Int64?)reader[((int)ExecOrderColumn.ConditionOrderId - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="ETradeOrders.Entities.ExecOrder"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ETradeOrders.Entities.ExecOrder"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ETradeOrders.Entities.ExecOrder entity)
		{
			if (!reader.Read()) return;
			
			entity.OrderId = (int)reader[((int)ExecOrderColumn.OrderId - 1)];
			entity.RefOrderId = (reader.IsDBNull(((int)ExecOrderColumn.RefOrderId - 1)))?null:(string)reader[((int)ExecOrderColumn.RefOrderId - 1)];
			entity.MessageType = (reader.IsDBNull(((int)ExecOrderColumn.MessageType - 1)))?null:(string)reader[((int)ExecOrderColumn.MessageType - 1)];
			entity.FisOrderId = (reader.IsDBNull(((int)ExecOrderColumn.FisOrderId - 1)))?null:(System.Int32?)reader[((int)ExecOrderColumn.FisOrderId - 1)];
			entity.SecSymbol = (string)reader[((int)ExecOrderColumn.SecSymbol - 1)];
			entity.Side = (string)reader[((int)ExecOrderColumn.Side - 1)];
			entity.Price = (decimal)reader[((int)ExecOrderColumn.Price - 1)];
			entity.AvgPrice = (reader.IsDBNull(((int)ExecOrderColumn.AvgPrice - 1)))?null:(System.Decimal?)reader[((int)ExecOrderColumn.AvgPrice - 1)];
			entity.ConPrice = (reader.IsDBNull(((int)ExecOrderColumn.ConPrice - 1)))?null:(string)reader[((int)ExecOrderColumn.ConPrice - 1)];
			entity.Volume = (int)reader[((int)ExecOrderColumn.Volume - 1)];
			entity.ExecutedVol = (reader.IsDBNull(((int)ExecOrderColumn.ExecutedVol - 1)))?null:(System.Int32?)reader[((int)ExecOrderColumn.ExecutedVol - 1)];
			entity.ExecutedPrice = (reader.IsDBNull(((int)ExecOrderColumn.ExecutedPrice - 1)))?null:(System.Decimal?)reader[((int)ExecOrderColumn.ExecutedPrice - 1)];
			entity.CancelVolume = (reader.IsDBNull(((int)ExecOrderColumn.CancelVolume - 1)))?null:(System.Int32?)reader[((int)ExecOrderColumn.CancelVolume - 1)];
			entity.CancelledVolume = (reader.IsDBNull(((int)ExecOrderColumn.CancelledVolume - 1)))?null:(System.Int32?)reader[((int)ExecOrderColumn.CancelledVolume - 1)];
			entity.SubCustAccountId = (string)reader[((int)ExecOrderColumn.SubCustAccountId - 1)];
			entity.ExecTransType = (reader.IsDBNull(((int)ExecOrderColumn.ExecTransType - 1)))?null:(System.Int32?)reader[((int)ExecOrderColumn.ExecTransType - 1)];
			entity.TradeTime = (reader.IsDBNull(((int)ExecOrderColumn.TradeTime - 1)))?null:(System.DateTime?)reader[((int)ExecOrderColumn.TradeTime - 1)];
			entity.MatchedTime = (reader.IsDBNull(((int)ExecOrderColumn.MatchedTime - 1)))?null:(System.DateTime?)reader[((int)ExecOrderColumn.MatchedTime - 1)];
			entity.CancelledTime = (reader.IsDBNull(((int)ExecOrderColumn.CancelledTime - 1)))?null:(System.DateTime?)reader[((int)ExecOrderColumn.CancelledTime - 1)];
			entity.OrderStatus = (reader.IsDBNull(((int)ExecOrderColumn.OrderStatus - 1)))?null:(System.Int16?)reader[((int)ExecOrderColumn.OrderStatus - 1)];
			entity.OrdRejReason = (reader.IsDBNull(((int)ExecOrderColumn.OrdRejReason - 1)))?null:(System.Int32?)reader[((int)ExecOrderColumn.OrdRejReason - 1)];
			entity.ConfirmNo = (reader.IsDBNull(((int)ExecOrderColumn.ConfirmNo - 1)))?null:(string)reader[((int)ExecOrderColumn.ConfirmNo - 1)];
			entity.CancelledConfirmNo = (reader.IsDBNull(((int)ExecOrderColumn.CancelledConfirmNo - 1)))?null:(string)reader[((int)ExecOrderColumn.CancelledConfirmNo - 1)];
			entity.SourceId = (reader.IsDBNull(((int)ExecOrderColumn.SourceId - 1)))?null:(System.Int16?)reader[((int)ExecOrderColumn.SourceId - 1)];
			entity.ExecType = (reader.IsDBNull(((int)ExecOrderColumn.ExecType - 1)))?null:(string)reader[((int)ExecOrderColumn.ExecType - 1)];
			entity.CancelledExecType = (reader.IsDBNull(((int)ExecOrderColumn.CancelledExecType - 1)))?null:(string)reader[((int)ExecOrderColumn.CancelledExecType - 1)];
			entity.PortOrClient = (reader.IsDBNull(((int)ExecOrderColumn.PortOrClient - 1)))?null:(string)reader[((int)ExecOrderColumn.PortOrClient - 1)];
			entity.Market = (string)reader[((int)ExecOrderColumn.Market - 1)];
			entity.MarketStatus = (reader.IsDBNull(((int)ExecOrderColumn.MarketStatus - 1)))?null:(string)reader[((int)ExecOrderColumn.MarketStatus - 1)];
			entity.OrderSource = (reader.IsDBNull(((int)ExecOrderColumn.OrderSource - 1)))?null:(string)reader[((int)ExecOrderColumn.OrderSource - 1)];
			entity.IsNewOrder = (reader.IsDBNull(((int)ExecOrderColumn.IsNewOrder - 1)))?null:(System.Boolean?)reader[((int)ExecOrderColumn.IsNewOrder - 1)];
			entity.Sequence = (int)reader[((int)ExecOrderColumn.Sequence - 1)];
			entity.NumOfMatch = (reader.IsDBNull(((int)ExecOrderColumn.NumOfMatch - 1)))?null:(System.Int32?)reader[((int)ExecOrderColumn.NumOfMatch - 1)];
			entity.QuickOrderId = (reader.IsDBNull(((int)ExecOrderColumn.QuickOrderId - 1)))?null:(System.Int32?)reader[((int)ExecOrderColumn.QuickOrderId - 1)];
			entity.ConditionOrderId = (reader.IsDBNull(((int)ExecOrderColumn.ConditionOrderId - 1)))?null:(System.Int64?)reader[((int)ExecOrderColumn.ConditionOrderId - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ETradeOrders.Entities.ExecOrder"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ETradeOrders.Entities.ExecOrder"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ETradeOrders.Entities.ExecOrder entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.OrderId = (int)dataRow["OrderID"];
			entity.RefOrderId = Convert.IsDBNull(dataRow["RefOrderID"]) ? null : (string)dataRow["RefOrderID"];
			entity.MessageType = Convert.IsDBNull(dataRow["MessageType"]) ? null : (string)dataRow["MessageType"];
			entity.FisOrderId = Convert.IsDBNull(dataRow["FISOrderID"]) ? null : (System.Int32?)dataRow["FISOrderID"];
			entity.SecSymbol = (string)dataRow["SecSymbol"];
			entity.Side = (string)dataRow["Side"];
			entity.Price = (decimal)dataRow["Price"];
			entity.AvgPrice = Convert.IsDBNull(dataRow["AvgPrice"]) ? null : (System.Decimal?)dataRow["AvgPrice"];
			entity.ConPrice = Convert.IsDBNull(dataRow["ConPrice"]) ? null : (string)dataRow["ConPrice"];
			entity.Volume = (int)dataRow["Volume"];
			entity.ExecutedVol = Convert.IsDBNull(dataRow["ExecutedVol"]) ? null : (System.Int32?)dataRow["ExecutedVol"];
			entity.ExecutedPrice = Convert.IsDBNull(dataRow["ExecutedPrice"]) ? null : (System.Decimal?)dataRow["ExecutedPrice"];
			entity.CancelVolume = Convert.IsDBNull(dataRow["CancelVolume"]) ? null : (System.Int32?)dataRow["CancelVolume"];
			entity.CancelledVolume = Convert.IsDBNull(dataRow["CancelledVolume"]) ? null : (System.Int32?)dataRow["CancelledVolume"];
			entity.SubCustAccountId = (string)dataRow["SubCustAccountID"];
			entity.ExecTransType = Convert.IsDBNull(dataRow["ExecTransType"]) ? null : (System.Int32?)dataRow["ExecTransType"];
			entity.TradeTime = Convert.IsDBNull(dataRow["TradeTime"]) ? null : (System.DateTime?)dataRow["TradeTime"];
			entity.MatchedTime = Convert.IsDBNull(dataRow["MatchedTime"]) ? null : (System.DateTime?)dataRow["MatchedTime"];
			entity.CancelledTime = Convert.IsDBNull(dataRow["CancelledTime"]) ? null : (System.DateTime?)dataRow["CancelledTime"];
			entity.OrderStatus = Convert.IsDBNull(dataRow["OrderStatus"]) ? null : (System.Int16?)dataRow["OrderStatus"];
			entity.OrdRejReason = Convert.IsDBNull(dataRow["OrdRejReason"]) ? null : (System.Int32?)dataRow["OrdRejReason"];
			entity.ConfirmNo = Convert.IsDBNull(dataRow["ConfirmNo"]) ? null : (string)dataRow["ConfirmNo"];
			entity.CancelledConfirmNo = Convert.IsDBNull(dataRow["CancelledConfirmNo"]) ? null : (string)dataRow["CancelledConfirmNo"];
			entity.SourceId = Convert.IsDBNull(dataRow["SourceID"]) ? null : (System.Int16?)dataRow["SourceID"];
			entity.ExecType = Convert.IsDBNull(dataRow["ExecType"]) ? null : (string)dataRow["ExecType"];
			entity.CancelledExecType = Convert.IsDBNull(dataRow["CancelledExecType"]) ? null : (string)dataRow["CancelledExecType"];
			entity.PortOrClient = Convert.IsDBNull(dataRow["PortOrClient"]) ? null : (string)dataRow["PortOrClient"];
			entity.Market = (string)dataRow["Market"];
			entity.MarketStatus = Convert.IsDBNull(dataRow["MarketStatus"]) ? null : (string)dataRow["MarketStatus"];
			entity.OrderSource = Convert.IsDBNull(dataRow["OrderSource"]) ? null : (string)dataRow["OrderSource"];
			entity.IsNewOrder = Convert.IsDBNull(dataRow["IsNewOrder"]) ? null : (System.Boolean?)dataRow["IsNewOrder"];
			entity.Sequence = (int)dataRow["Sequence"];
			entity.NumOfMatch = Convert.IsDBNull(dataRow["NumOfMatch"]) ? null : (System.Int32?)dataRow["NumOfMatch"];
			entity.QuickOrderId = Convert.IsDBNull(dataRow["QuickOrderID"]) ? null : (System.Int32?)dataRow["QuickOrderID"];
			entity.ConditionOrderId = Convert.IsDBNull(dataRow["ConditionOrderID"]) ? null : (System.Int64?)dataRow["ConditionOrderID"];
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
		/// <param name="entity">The <see cref="ETradeOrders.Entities.ExecOrder"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ETradeOrders.Entities.ExecOrder Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ETradeOrders.Entities.ExecOrder entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region ConditionOrderIdSource	
			if (CanDeepLoad(entity, "ConditionOrder|ConditionOrderIdSource", deepLoadType, innerList) 
				&& entity.ConditionOrderIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.ConditionOrderId ?? (long)0);
				ConditionOrder tmpEntity = EntityManager.LocateEntity<ConditionOrder>(EntityLocator.ConstructKeyFromPkItems(typeof(ConditionOrder), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.ConditionOrderIdSource = tmpEntity;
				else
					entity.ConditionOrderIdSource = DataRepository.ConditionOrderProvider.GetByConditionOrderId(transactionManager, (entity.ConditionOrderId ?? (long)0));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ConditionOrderIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.ConditionOrderIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.ConditionOrderProvider.DeepLoad(transactionManager, entity.ConditionOrderIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion ConditionOrderIdSource

			#region QuickOrderIdSource	
			if (CanDeepLoad(entity, "QuickOrder|QuickOrderIdSource", deepLoadType, innerList) 
				&& entity.QuickOrderIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.QuickOrderId ?? (int)0);
				QuickOrder tmpEntity = EntityManager.LocateEntity<QuickOrder>(EntityLocator.ConstructKeyFromPkItems(typeof(QuickOrder), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.QuickOrderIdSource = tmpEntity;
				else
					entity.QuickOrderIdSource = DataRepository.QuickOrderProvider.GetByQuickOrderId(transactionManager, (entity.QuickOrderId ?? (int)0));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'QuickOrderIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.QuickOrderIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.QuickOrderProvider.DeepLoad(transactionManager, entity.QuickOrderIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion QuickOrderIdSource
			
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
		/// Deep Save the entire object graph of the ETradeOrders.Entities.ExecOrder object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ETradeOrders.Entities.ExecOrder instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ETradeOrders.Entities.ExecOrder Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ETradeOrders.Entities.ExecOrder entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region ConditionOrderIdSource
			if (CanDeepSave(entity, "ConditionOrder|ConditionOrderIdSource", deepSaveType, innerList) 
				&& entity.ConditionOrderIdSource != null)
			{
				DataRepository.ConditionOrderProvider.Save(transactionManager, entity.ConditionOrderIdSource);
				entity.ConditionOrderId = entity.ConditionOrderIdSource.ConditionOrderId;
			}
			#endregion 
			
			#region QuickOrderIdSource
			if (CanDeepSave(entity, "QuickOrder|QuickOrderIdSource", deepSaveType, innerList) 
				&& entity.QuickOrderIdSource != null)
			{
				DataRepository.QuickOrderProvider.Save(transactionManager, entity.QuickOrderIdSource);
				entity.QuickOrderId = entity.QuickOrderIdSource.QuickOrderId;
			}
			#endregion 
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
	
	#region ExecOrderChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ETradeOrders.Entities.ExecOrder</c>
	///</summary>
	public enum ExecOrderChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>ConditionOrder</c> at ConditionOrderIdSource
		///</summary>
		[ChildEntityType(typeof(ConditionOrder))]
		ConditionOrder,
			
		///<summary>
		/// Composite Property for <c>QuickOrder</c> at QuickOrderIdSource
		///</summary>
		[ChildEntityType(typeof(QuickOrder))]
		QuickOrder,
		}
	
	#endregion ExecOrderChildEntityTypes
	
	#region ExecOrderFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;ExecOrderColumn&gt;"/> class
	/// that is used exclusively with a <see cref="ExecOrder"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ExecOrderFilterBuilder : SqlFilterBuilder<ExecOrderColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ExecOrderFilterBuilder class.
		/// </summary>
		public ExecOrderFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ExecOrderFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ExecOrderFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ExecOrderFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ExecOrderFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ExecOrderFilterBuilder
	
	#region ExecOrderParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;ExecOrderColumn&gt;"/> class
	/// that is used exclusively with a <see cref="ExecOrder"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ExecOrderParameterBuilder : ParameterizedSqlFilterBuilder<ExecOrderColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ExecOrderParameterBuilder class.
		/// </summary>
		public ExecOrderParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ExecOrderParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ExecOrderParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ExecOrderParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ExecOrderParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ExecOrderParameterBuilder
	
	#region ExecOrderSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;ExecOrderColumn&gt;"/> class
	/// that is used exclusively with a <see cref="ExecOrder"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class ExecOrderSortBuilder : SqlSortBuilder<ExecOrderColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ExecOrderSqlSortBuilder class.
		/// </summary>
		public ExecOrderSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion ExecOrderSortBuilder
	
} // end namespace
