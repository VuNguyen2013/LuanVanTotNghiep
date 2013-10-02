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
	/// This class is the base class for any <see cref="ConfigurationsProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class ConfigurationsProviderBaseCore : EntityProviderBase<AccountManager.Entities.Configurations, AccountManager.Entities.ConfigurationsKey>
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
		public override bool Delete(TransactionManager transactionManager, AccountManager.Entities.ConfigurationsKey key)
		{
			return Delete(transactionManager, key.Name);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_name">Name of item. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.String _name)
		{
			return Delete(null, _name);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_name">Name of item. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.String _name);		
		
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
		public override AccountManager.Entities.Configurations Get(TransactionManager transactionManager, AccountManager.Entities.ConfigurationsKey key, int start, int pageLength)
		{
			return GetByName(transactionManager, key.Name, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_CONFIGURATIONS index.
		/// </summary>
		/// <param name="_name">Name of item</param>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.Configurations"/> class.</returns>
		public AccountManager.Entities.Configurations GetByName(System.String _name)
		{
			int count = -1;
			return GetByName(null,_name, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CONFIGURATIONS index.
		/// </summary>
		/// <param name="_name">Name of item</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.Configurations"/> class.</returns>
		public AccountManager.Entities.Configurations GetByName(System.String _name, int start, int pageLength)
		{
			int count = -1;
			return GetByName(null, _name, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CONFIGURATIONS index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_name">Name of item</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.Configurations"/> class.</returns>
		public AccountManager.Entities.Configurations GetByName(TransactionManager transactionManager, System.String _name)
		{
			int count = -1;
			return GetByName(transactionManager, _name, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CONFIGURATIONS index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_name">Name of item</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.Configurations"/> class.</returns>
		public AccountManager.Entities.Configurations GetByName(TransactionManager transactionManager, System.String _name, int start, int pageLength)
		{
			int count = -1;
			return GetByName(transactionManager, _name, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CONFIGURATIONS index.
		/// </summary>
		/// <param name="_name">Name of item</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.Configurations"/> class.</returns>
		public AccountManager.Entities.Configurations GetByName(System.String _name, int start, int pageLength, out int count)
		{
			return GetByName(null, _name, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CONFIGURATIONS index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_name">Name of item</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.Configurations"/> class.</returns>
		public abstract AccountManager.Entities.Configurations GetByName(TransactionManager transactionManager, System.String _name, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Configurations&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Configurations&gt;"/></returns>
		public static TList<Configurations> Fill(IDataReader reader, TList<Configurations> rows, int start, int pageLength)
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
				
				AccountManager.Entities.Configurations c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Configurations")
					.Append("|").Append((System.String)reader[((int)ConfigurationsColumn.Name - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Configurations>(
					key.ToString(), // EntityTrackingKey
					"Configurations",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new AccountManager.Entities.Configurations();
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
					c.Name = (System.String)reader[((int)ConfigurationsColumn.Name - 1)];
					c.OriginalName = c.Name;
					c.Value = (reader.IsDBNull(((int)ConfigurationsColumn.Value - 1)))?null:(System.String)reader[((int)ConfigurationsColumn.Value - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="AccountManager.Entities.Configurations"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AccountManager.Entities.Configurations"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AccountManager.Entities.Configurations entity)
		{
			if (!reader.Read()) return;
			
			entity.Name = (System.String)reader[((int)ConfigurationsColumn.Name - 1)];
			entity.OriginalName = (System.String)reader["Name"];
			entity.Value = (reader.IsDBNull(((int)ConfigurationsColumn.Value - 1)))?null:(System.String)reader[((int)ConfigurationsColumn.Value - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="AccountManager.Entities.Configurations"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AccountManager.Entities.Configurations"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AccountManager.Entities.Configurations entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Name = (System.String)dataRow["Name"];
			entity.OriginalName = (System.String)dataRow["Name"];
			entity.Value = Convert.IsDBNull(dataRow["Value"]) ? null : (System.String)dataRow["Value"];
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
		/// <param name="entity">The <see cref="AccountManager.Entities.Configurations"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AccountManager.Entities.Configurations Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AccountManager.Entities.Configurations entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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
		/// Deep Save the entire object graph of the AccountManager.Entities.Configurations object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AccountManager.Entities.Configurations instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AccountManager.Entities.Configurations Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AccountManager.Entities.Configurations entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
	#region ConfigurationsChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>AccountManager.Entities.Configurations</c>
	///</summary>
	public enum ConfigurationsChildEntityTypes
	{
	}
	
	#endregion ConfigurationsChildEntityTypes
	
	#region ConfigurationsFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;ConfigurationsColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Configurations"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ConfigurationsFilterBuilder : SqlFilterBuilder<ConfigurationsColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ConfigurationsFilterBuilder class.
		/// </summary>
		public ConfigurationsFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ConfigurationsFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ConfigurationsFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ConfigurationsFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ConfigurationsFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ConfigurationsFilterBuilder
	
	#region ConfigurationsParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;ConfigurationsColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Configurations"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ConfigurationsParameterBuilder : ParameterizedSqlFilterBuilder<ConfigurationsColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ConfigurationsParameterBuilder class.
		/// </summary>
		public ConfigurationsParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ConfigurationsParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ConfigurationsParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ConfigurationsParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ConfigurationsParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ConfigurationsParameterBuilder
	
	#region ConfigurationsSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;ConfigurationsColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Configurations"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class ConfigurationsSortBuilder : SqlSortBuilder<ConfigurationsColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ConfigurationsSqlSortBuilder class.
		/// </summary>
		public ConfigurationsSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion ConfigurationsSortBuilder
	
} // end namespace
