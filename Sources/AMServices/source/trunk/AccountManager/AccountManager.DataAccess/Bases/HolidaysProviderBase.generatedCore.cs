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
	/// This class is the base class for any <see cref="HolidaysProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class HolidaysProviderBaseCore : EntityProviderBase<AccountManager.Entities.Holidays, AccountManager.Entities.HolidaysKey>
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
		public override bool Delete(TransactionManager transactionManager, AccountManager.Entities.HolidaysKey key)
		{
			return Delete(transactionManager, key.Holiday);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_holiday">Ngay le (Chi luu tru ngay, khong luu tru gio). Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.DateTime _holiday)
		{
			return Delete(null, _holiday);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_holiday">Ngay le (Chi luu tru ngay, khong luu tru gio). Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.DateTime _holiday);		
		
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
		public override AccountManager.Entities.Holidays Get(TransactionManager transactionManager, AccountManager.Entities.HolidaysKey key, int start, int pageLength)
		{
			return GetByHoliday(transactionManager, key.Holiday, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_HOLIDAYS index.
		/// </summary>
		/// <param name="_holiday">Ngay le (Chi luu tru ngay, khong luu tru gio)</param>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.Holidays"/> class.</returns>
		public AccountManager.Entities.Holidays GetByHoliday(System.DateTime _holiday)
		{
			int count = -1;
			return GetByHoliday(null,_holiday, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_HOLIDAYS index.
		/// </summary>
		/// <param name="_holiday">Ngay le (Chi luu tru ngay, khong luu tru gio)</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.Holidays"/> class.</returns>
		public AccountManager.Entities.Holidays GetByHoliday(System.DateTime _holiday, int start, int pageLength)
		{
			int count = -1;
			return GetByHoliday(null, _holiday, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_HOLIDAYS index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_holiday">Ngay le (Chi luu tru ngay, khong luu tru gio)</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.Holidays"/> class.</returns>
		public AccountManager.Entities.Holidays GetByHoliday(TransactionManager transactionManager, System.DateTime _holiday)
		{
			int count = -1;
			return GetByHoliday(transactionManager, _holiday, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_HOLIDAYS index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_holiday">Ngay le (Chi luu tru ngay, khong luu tru gio)</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.Holidays"/> class.</returns>
		public AccountManager.Entities.Holidays GetByHoliday(TransactionManager transactionManager, System.DateTime _holiday, int start, int pageLength)
		{
			int count = -1;
			return GetByHoliday(transactionManager, _holiday, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_HOLIDAYS index.
		/// </summary>
		/// <param name="_holiday">Ngay le (Chi luu tru ngay, khong luu tru gio)</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.Holidays"/> class.</returns>
		public AccountManager.Entities.Holidays GetByHoliday(System.DateTime _holiday, int start, int pageLength, out int count)
		{
			return GetByHoliday(null, _holiday, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_HOLIDAYS index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_holiday">Ngay le (Chi luu tru ngay, khong luu tru gio)</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.Holidays"/> class.</returns>
		public abstract AccountManager.Entities.Holidays GetByHoliday(TransactionManager transactionManager, System.DateTime _holiday, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Holidays&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Holidays&gt;"/></returns>
		public static TList<Holidays> Fill(IDataReader reader, TList<Holidays> rows, int start, int pageLength)
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
				
				AccountManager.Entities.Holidays c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Holidays")
					.Append("|").Append((System.DateTime)reader[((int)HolidaysColumn.Holiday - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Holidays>(
					key.ToString(), // EntityTrackingKey
					"Holidays",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new AccountManager.Entities.Holidays();
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
					c.Holiday = (System.DateTime)reader[((int)HolidaysColumn.Holiday - 1)];
					c.OriginalHoliday = c.Holiday;
					c.Note = (reader.IsDBNull(((int)HolidaysColumn.Note - 1)))?null:(System.String)reader[((int)HolidaysColumn.Note - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="AccountManager.Entities.Holidays"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AccountManager.Entities.Holidays"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AccountManager.Entities.Holidays entity)
		{
			if (!reader.Read()) return;
			
			entity.Holiday = (System.DateTime)reader[((int)HolidaysColumn.Holiday - 1)];
			entity.OriginalHoliday = (System.DateTime)reader["Holiday"];
			entity.Note = (reader.IsDBNull(((int)HolidaysColumn.Note - 1)))?null:(System.String)reader[((int)HolidaysColumn.Note - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="AccountManager.Entities.Holidays"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AccountManager.Entities.Holidays"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AccountManager.Entities.Holidays entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Holiday = (System.DateTime)dataRow["Holiday"];
			entity.OriginalHoliday = (System.DateTime)dataRow["Holiday"];
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
		/// <param name="entity">The <see cref="AccountManager.Entities.Holidays"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AccountManager.Entities.Holidays Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AccountManager.Entities.Holidays entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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
		/// Deep Save the entire object graph of the AccountManager.Entities.Holidays object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AccountManager.Entities.Holidays instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AccountManager.Entities.Holidays Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AccountManager.Entities.Holidays entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
	#region HolidaysChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>AccountManager.Entities.Holidays</c>
	///</summary>
	public enum HolidaysChildEntityTypes
	{
	}
	
	#endregion HolidaysChildEntityTypes
	
	#region HolidaysFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;HolidaysColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Holidays"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HolidaysFilterBuilder : SqlFilterBuilder<HolidaysColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HolidaysFilterBuilder class.
		/// </summary>
		public HolidaysFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the HolidaysFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public HolidaysFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the HolidaysFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public HolidaysFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion HolidaysFilterBuilder
	
	#region HolidaysParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;HolidaysColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Holidays"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HolidaysParameterBuilder : ParameterizedSqlFilterBuilder<HolidaysColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HolidaysParameterBuilder class.
		/// </summary>
		public HolidaysParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the HolidaysParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public HolidaysParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the HolidaysParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public HolidaysParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion HolidaysParameterBuilder
	
	#region HolidaysSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;HolidaysColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Holidays"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class HolidaysSortBuilder : SqlSortBuilder<HolidaysColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HolidaysSqlSortBuilder class.
		/// </summary>
		public HolidaysSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion HolidaysSortBuilder
	
} // end namespace
