#region Using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

using AccountManager.Entities;
using AccountManager.DataAccess;

#endregion

namespace AccountManager.DataAccess.Bases
{	
	///<summary>
	/// This class is the base class for any <see cref="SmsCountProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class SmsCountProviderBaseCore : EntityProviderBase<AccountManager.Entities.SmsCount, AccountManager.Entities.SmsCountKey>
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
		public override bool Delete(TransactionManager transactionManager, AccountManager.Entities.SmsCountKey key)
		{
			return Delete(transactionManager, key.SendDate);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_sendDate">SMS Sent date. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.DateTime _sendDate)
		{
			return Delete(null, _sendDate);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_sendDate">SMS Sent date. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.DateTime _sendDate);		
		
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
		public override AccountManager.Entities.SmsCount Get(TransactionManager transactionManager, AccountManager.Entities.SmsCountKey key, int start, int pageLength)
		{
			return GetBySendDate(transactionManager, key.SendDate, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_SMSCOUNT index.
		/// </summary>
		/// <param name="_sendDate">SMS Sent date</param>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.SmsCount"/> class.</returns>
		public AccountManager.Entities.SmsCount GetBySendDate(System.DateTime _sendDate)
		{
			int count = -1;
			return GetBySendDate(null,_sendDate, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_SMSCOUNT index.
		/// </summary>
		/// <param name="_sendDate">SMS Sent date</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.SmsCount"/> class.</returns>
		public AccountManager.Entities.SmsCount GetBySendDate(System.DateTime _sendDate, int start, int pageLength)
		{
			int count = -1;
			return GetBySendDate(null, _sendDate, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_SMSCOUNT index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_sendDate">SMS Sent date</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.SmsCount"/> class.</returns>
		public AccountManager.Entities.SmsCount GetBySendDate(TransactionManager transactionManager, System.DateTime _sendDate)
		{
			int count = -1;
			return GetBySendDate(transactionManager, _sendDate, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_SMSCOUNT index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_sendDate">SMS Sent date</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.SmsCount"/> class.</returns>
		public AccountManager.Entities.SmsCount GetBySendDate(TransactionManager transactionManager, System.DateTime _sendDate, int start, int pageLength)
		{
			int count = -1;
			return GetBySendDate(transactionManager, _sendDate, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_SMSCOUNT index.
		/// </summary>
		/// <param name="_sendDate">SMS Sent date</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.SmsCount"/> class.</returns>
		public AccountManager.Entities.SmsCount GetBySendDate(System.DateTime _sendDate, int start, int pageLength, out int count)
		{
			return GetBySendDate(null, _sendDate, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_SMSCOUNT index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_sendDate">SMS Sent date</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.SmsCount"/> class.</returns>
		public abstract AccountManager.Entities.SmsCount GetBySendDate(TransactionManager transactionManager, System.DateTime _sendDate, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#region _SMSCount_GetTotal 
		
		/// <summary>
		///	This method wrap the '_SMSCount_GetTotal' stored procedure. 
		/// </summary>
		/// <param name="fromDate"> A <c>System.String</c> instance.</param>
		/// <param name="toDate"> A <c>System.String</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet GetTotal(System.String fromDate, System.String toDate)
		{
			return GetTotal(null, 0, int.MaxValue , fromDate, toDate);
		}
		
		/// <summary>
		///	This method wrap the '_SMSCount_GetTotal' stored procedure. 
		/// </summary>
		/// <param name="fromDate"> A <c>System.String</c> instance.</param>
		/// <param name="toDate"> A <c>System.String</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet GetTotal(int start, int pageLength, System.String fromDate, System.String toDate)
		{
			return GetTotal(null, start, pageLength , fromDate, toDate);
		}
				
		/// <summary>
		///	This method wrap the '_SMSCount_GetTotal' stored procedure. 
		/// </summary>
		/// <param name="fromDate"> A <c>System.String</c> instance.</param>
		/// <param name="toDate"> A <c>System.String</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet GetTotal(TransactionManager transactionManager, System.String fromDate, System.String toDate)
		{
			return GetTotal(transactionManager, 0, int.MaxValue , fromDate, toDate);
		}
		
		/// <summary>
		///	This method wrap the '_SMSCount_GetTotal' stored procedure. 
		/// </summary>
		/// <param name="fromDate"> A <c>System.String</c> instance.</param>
		/// <param name="toDate"> A <c>System.String</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet GetTotal(TransactionManager transactionManager, int start, int pageLength , System.String fromDate, System.String toDate);
		
		#endregion
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;SmsCount&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;SmsCount&gt;"/></returns>
		public static TList<SmsCount> Fill(IDataReader reader, TList<SmsCount> rows, int start, int pageLength)
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
				
				AccountManager.Entities.SmsCount c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("SmsCount")
					.Append("|").Append((System.DateTime)reader[((int)SmsCountColumn.SendDate - 1)]).ToString();
					c = EntityManager.LocateOrCreate<SmsCount>(
					key.ToString(), // EntityTrackingKey
					"SmsCount",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new AccountManager.Entities.SmsCount();
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
					c.SendDate = (System.DateTime)reader[((int)SmsCountColumn.SendDate - 1)];
					c.OriginalSendDate = c.SendDate;
					c.Total = (System.Int32)reader[((int)SmsCountColumn.Total - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="AccountManager.Entities.SmsCount"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AccountManager.Entities.SmsCount"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AccountManager.Entities.SmsCount entity)
		{
			if (!reader.Read()) return;
			
			entity.SendDate = (System.DateTime)reader[((int)SmsCountColumn.SendDate - 1)];
			entity.OriginalSendDate = (System.DateTime)reader["SendDate"];
			entity.Total = (System.Int32)reader[((int)SmsCountColumn.Total - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="AccountManager.Entities.SmsCount"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AccountManager.Entities.SmsCount"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AccountManager.Entities.SmsCount entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.SendDate = (System.DateTime)dataRow["SendDate"];
			entity.OriginalSendDate = (System.DateTime)dataRow["SendDate"];
			entity.Total = (System.Int32)dataRow["Total"];
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
		/// <param name="entity">The <see cref="AccountManager.Entities.SmsCount"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AccountManager.Entities.SmsCount Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AccountManager.Entities.SmsCount entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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
		/// Deep Save the entire object graph of the AccountManager.Entities.SmsCount object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AccountManager.Entities.SmsCount instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AccountManager.Entities.SmsCount Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AccountManager.Entities.SmsCount entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
	#region SmsCountChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>AccountManager.Entities.SmsCount</c>
	///</summary>
	public enum SmsCountChildEntityTypes
	{
	}
	
	#endregion SmsCountChildEntityTypes
	
	#region SmsCountFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;SmsCountColumn&gt;"/> class
	/// that is used exclusively with a <see cref="SmsCount"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class SmsCountFilterBuilder : SqlFilterBuilder<SmsCountColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SmsCountFilterBuilder class.
		/// </summary>
		public SmsCountFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the SmsCountFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public SmsCountFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the SmsCountFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public SmsCountFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion SmsCountFilterBuilder
	
	#region SmsCountParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;SmsCountColumn&gt;"/> class
	/// that is used exclusively with a <see cref="SmsCount"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class SmsCountParameterBuilder : ParameterizedSqlFilterBuilder<SmsCountColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SmsCountParameterBuilder class.
		/// </summary>
		public SmsCountParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the SmsCountParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public SmsCountParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the SmsCountParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public SmsCountParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion SmsCountParameterBuilder
	
	#region SmsCountSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;SmsCountColumn&gt;"/> class
	/// that is used exclusively with a <see cref="SmsCount"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class SmsCountSortBuilder : SqlSortBuilder<SmsCountColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SmsCountSqlSortBuilder class.
		/// </summary>
		public SmsCountSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion SmsCountSortBuilder
	
} // end namespace
