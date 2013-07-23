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
	/// This class is the base class for any <see cref="QuickOrderProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class QuickOrderProviderBaseCore : EntityProviderBase<ETradeOrders.Entities.QuickOrder, ETradeOrders.Entities.QuickOrderKey>
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
		public override bool Delete(TransactionManager transactionManager, ETradeOrders.Entities.QuickOrderKey key)
		{
			return Delete(transactionManager, key.QuickOrderId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_quickOrderId">QuickOrderID identifies QuickOrder. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(int _quickOrderId)
		{
			return Delete(null, _quickOrderId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_quickOrderId">QuickOrderID identifies QuickOrder. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, int _quickOrderId);		
		
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
		public override ETradeOrders.Entities.QuickOrder Get(TransactionManager transactionManager, ETradeOrders.Entities.QuickOrderKey key, int start, int pageLength)
		{
			return GetByQuickOrderId(transactionManager, key.QuickOrderId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_QuickOrder index.
		/// </summary>
		/// <param name="_quickOrderId">QuickOrderID identifies QuickOrder</param>
		/// <returns>Returns an instance of the <see cref="ETradeOrders.Entities.QuickOrder"/> class.</returns>
		public ETradeOrders.Entities.QuickOrder GetByQuickOrderId(int _quickOrderId)
		{
			int count = -1;
			return GetByQuickOrderId(null,_quickOrderId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_QuickOrder index.
		/// </summary>
		/// <param name="_quickOrderId">QuickOrderID identifies QuickOrder</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeOrders.Entities.QuickOrder"/> class.</returns>
		public ETradeOrders.Entities.QuickOrder GetByQuickOrderId(int _quickOrderId, int start, int pageLength)
		{
			int count = -1;
			return GetByQuickOrderId(null, _quickOrderId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_QuickOrder index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_quickOrderId">QuickOrderID identifies QuickOrder</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeOrders.Entities.QuickOrder"/> class.</returns>
		public ETradeOrders.Entities.QuickOrder GetByQuickOrderId(TransactionManager transactionManager, int _quickOrderId)
		{
			int count = -1;
			return GetByQuickOrderId(transactionManager, _quickOrderId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_QuickOrder index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_quickOrderId">QuickOrderID identifies QuickOrder</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeOrders.Entities.QuickOrder"/> class.</returns>
		public ETradeOrders.Entities.QuickOrder GetByQuickOrderId(TransactionManager transactionManager, int _quickOrderId, int start, int pageLength)
		{
			int count = -1;
			return GetByQuickOrderId(transactionManager, _quickOrderId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_QuickOrder index.
		/// </summary>
		/// <param name="_quickOrderId">QuickOrderID identifies QuickOrder</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ETradeOrders.Entities.QuickOrder"/> class.</returns>
		public ETradeOrders.Entities.QuickOrder GetByQuickOrderId(int _quickOrderId, int start, int pageLength, out int count)
		{
			return GetByQuickOrderId(null, _quickOrderId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_QuickOrder index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_quickOrderId">QuickOrderID identifies QuickOrder</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ETradeOrders.Entities.QuickOrder"/> class.</returns>
		public abstract ETradeOrders.Entities.QuickOrder GetByQuickOrderId(TransactionManager transactionManager, int _quickOrderId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;QuickOrder&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;QuickOrder&gt;"/></returns>
		public static TList<QuickOrder> Fill(IDataReader reader, TList<QuickOrder> rows, int start, int pageLength)
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
				
				ETradeOrders.Entities.QuickOrder c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("QuickOrder")
					.Append("|").Append((int)reader[((int)QuickOrderColumn.QuickOrderId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<QuickOrder>(
					key.ToString(), // EntityTrackingKey
					"QuickOrder",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ETradeOrders.Entities.QuickOrder();
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
					c.QuickOrderId = (int)reader[((int)QuickOrderColumn.QuickOrderId - 1)];
					c.SecSymbol = (string)reader[((int)QuickOrderColumn.SecSymbol - 1)];
					c.Side = (string)reader[((int)QuickOrderColumn.Side - 1)];
					c.Volume = (int)reader[((int)QuickOrderColumn.Volume - 1)];
					c.SubCustAccountId = (string)reader[((int)QuickOrderColumn.SubCustAccountId - 1)];
					c.Market = (string)reader[((int)QuickOrderColumn.Market - 1)];
					c.TradeTime = (System.DateTime)reader[((int)QuickOrderColumn.TradeTime - 1)];
					c.TypeOfQuick = (reader.IsDBNull(((int)QuickOrderColumn.TypeOfQuick - 1)))?null:(System.Int16?)reader[((int)QuickOrderColumn.TypeOfQuick - 1)];
					c.Status = (reader.IsDBNull(((int)QuickOrderColumn.Status - 1)))?null:(string)reader[((int)QuickOrderColumn.Status - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="ETradeOrders.Entities.QuickOrder"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ETradeOrders.Entities.QuickOrder"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ETradeOrders.Entities.QuickOrder entity)
		{
			if (!reader.Read()) return;
			
			entity.QuickOrderId = (int)reader[((int)QuickOrderColumn.QuickOrderId - 1)];
			entity.SecSymbol = (string)reader[((int)QuickOrderColumn.SecSymbol - 1)];
			entity.Side = (string)reader[((int)QuickOrderColumn.Side - 1)];
			entity.Volume = (int)reader[((int)QuickOrderColumn.Volume - 1)];
			entity.SubCustAccountId = (string)reader[((int)QuickOrderColumn.SubCustAccountId - 1)];
			entity.Market = (string)reader[((int)QuickOrderColumn.Market - 1)];
			entity.TradeTime = (System.DateTime)reader[((int)QuickOrderColumn.TradeTime - 1)];
			entity.TypeOfQuick = (reader.IsDBNull(((int)QuickOrderColumn.TypeOfQuick - 1)))?null:(System.Int16?)reader[((int)QuickOrderColumn.TypeOfQuick - 1)];
			entity.Status = (reader.IsDBNull(((int)QuickOrderColumn.Status - 1)))?null:(string)reader[((int)QuickOrderColumn.Status - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ETradeOrders.Entities.QuickOrder"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ETradeOrders.Entities.QuickOrder"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ETradeOrders.Entities.QuickOrder entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.QuickOrderId = (int)dataRow["QuickOrderID"];
			entity.SecSymbol = (string)dataRow["SecSymbol"];
			entity.Side = (string)dataRow["Side"];
			entity.Volume = (int)dataRow["Volume"];
			entity.SubCustAccountId = (string)dataRow["SubCustAccountID"];
			entity.Market = (string)dataRow["Market"];
			entity.TradeTime = (System.DateTime)dataRow["TradeTime"];
			entity.TypeOfQuick = Convert.IsDBNull(dataRow["TypeOfQuick"]) ? null : (System.Int16?)dataRow["TypeOfQuick"];
			entity.Status = Convert.IsDBNull(dataRow["Status"]) ? null : (string)dataRow["Status"];
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
		/// <param name="entity">The <see cref="ETradeOrders.Entities.QuickOrder"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ETradeOrders.Entities.QuickOrder Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ETradeOrders.Entities.QuickOrder entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByQuickOrderId methods when available
			
			#region ExecOrderCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<ExecOrder>|ExecOrderCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ExecOrderCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.ExecOrderCollection = DataRepository.ExecOrderProvider.GetByQuickOrderId(transactionManager, entity.QuickOrderId);

				if (deep && entity.ExecOrderCollection.Count > 0)
				{
					deepHandles.Add("ExecOrderCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<ExecOrder>) DataRepository.ExecOrderProvider.DeepLoad,
						new object[] { transactionManager, entity.ExecOrderCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
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
		/// Deep Save the entire object graph of the ETradeOrders.Entities.QuickOrder object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ETradeOrders.Entities.QuickOrder instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ETradeOrders.Entities.QuickOrder Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ETradeOrders.Entities.QuickOrder entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
			#region List<ExecOrder>
				if (CanDeepSave(entity.ExecOrderCollection, "List<ExecOrder>|ExecOrderCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(ExecOrder child in entity.ExecOrderCollection)
					{
						if(child.QuickOrderIdSource != null)
						{
							child.QuickOrderId = child.QuickOrderIdSource.QuickOrderId;
						}
						else
						{
							child.QuickOrderId = entity.QuickOrderId;
						}

					}

					if (entity.ExecOrderCollection.Count > 0 || entity.ExecOrderCollection.DeletedItems.Count > 0)
					{
						//DataRepository.ExecOrderProvider.Save(transactionManager, entity.ExecOrderCollection);
						
						deepHandles.Add("ExecOrderCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< ExecOrder >) DataRepository.ExecOrderProvider.DeepSave,
							new object[] { transactionManager, entity.ExecOrderCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
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
	
	#region QuickOrderChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ETradeOrders.Entities.QuickOrder</c>
	///</summary>
	public enum QuickOrderChildEntityTypes
	{

		///<summary>
		/// Collection of <c>QuickOrder</c> as OneToMany for ExecOrderCollection
		///</summary>
		[ChildEntityType(typeof(TList<ExecOrder>))]
		ExecOrderCollection,
	}
	
	#endregion QuickOrderChildEntityTypes
	
	#region QuickOrderFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;QuickOrderColumn&gt;"/> class
	/// that is used exclusively with a <see cref="QuickOrder"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class QuickOrderFilterBuilder : SqlFilterBuilder<QuickOrderColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the QuickOrderFilterBuilder class.
		/// </summary>
		public QuickOrderFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the QuickOrderFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public QuickOrderFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the QuickOrderFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public QuickOrderFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion QuickOrderFilterBuilder
	
	#region QuickOrderParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;QuickOrderColumn&gt;"/> class
	/// that is used exclusively with a <see cref="QuickOrder"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class QuickOrderParameterBuilder : ParameterizedSqlFilterBuilder<QuickOrderColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the QuickOrderParameterBuilder class.
		/// </summary>
		public QuickOrderParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the QuickOrderParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public QuickOrderParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the QuickOrderParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public QuickOrderParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion QuickOrderParameterBuilder
	
	#region QuickOrderSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;QuickOrderColumn&gt;"/> class
	/// that is used exclusively with a <see cref="QuickOrder"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class QuickOrderSortBuilder : SqlSortBuilder<QuickOrderColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the QuickOrderSqlSortBuilder class.
		/// </summary>
		public QuickOrderSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion QuickOrderSortBuilder
	
} // end namespace
