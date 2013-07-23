#region Using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

using RTStockData.Entities;
using RTStockData.Data;

#endregion

namespace RTStockData.Data.Bases
{	
	///<summary>
	/// This class is the base class for any <see cref="CompanyInfoLanguageProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class CompanyInfoLanguageProviderBaseCore : EntityProviderBase<RTStockData.Entities.CompanyInfoLanguage, RTStockData.Entities.CompanyInfoLanguageKey>
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
		public override bool Delete(TransactionManager transactionManager, RTStockData.Entities.CompanyInfoLanguageKey key)
		{
			return Delete(transactionManager, key.CompanyId, key.LanguageId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_companyId">. Primary Key.</param>
		/// <param name="_languageId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Int32 _companyId, System.String _languageId)
		{
			return Delete(null, _companyId, _languageId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_companyId">. Primary Key.</param>
		/// <param name="_languageId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Int32 _companyId, System.String _languageId);		
		
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
		public override RTStockData.Entities.CompanyInfoLanguage Get(TransactionManager transactionManager, RTStockData.Entities.CompanyInfoLanguageKey key, int start, int pageLength)
		{
			return GetByCompanyIdLanguageId(transactionManager, key.CompanyId, key.LanguageId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_CompanyInfoLanguage index.
		/// </summary>
		/// <param name="_companyId"></param>
		/// <param name="_languageId"></param>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.CompanyInfoLanguage"/> class.</returns>
		public RTStockData.Entities.CompanyInfoLanguage GetByCompanyIdLanguageId(System.Int32 _companyId, System.String _languageId)
		{
			int count = -1;
			return GetByCompanyIdLanguageId(null,_companyId, _languageId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CompanyInfoLanguage index.
		/// </summary>
		/// <param name="_companyId"></param>
		/// <param name="_languageId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.CompanyInfoLanguage"/> class.</returns>
		public RTStockData.Entities.CompanyInfoLanguage GetByCompanyIdLanguageId(System.Int32 _companyId, System.String _languageId, int start, int pageLength)
		{
			int count = -1;
			return GetByCompanyIdLanguageId(null, _companyId, _languageId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CompanyInfoLanguage index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_companyId"></param>
		/// <param name="_languageId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.CompanyInfoLanguage"/> class.</returns>
		public RTStockData.Entities.CompanyInfoLanguage GetByCompanyIdLanguageId(TransactionManager transactionManager, System.Int32 _companyId, System.String _languageId)
		{
			int count = -1;
			return GetByCompanyIdLanguageId(transactionManager, _companyId, _languageId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CompanyInfoLanguage index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_companyId"></param>
		/// <param name="_languageId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.CompanyInfoLanguage"/> class.</returns>
		public RTStockData.Entities.CompanyInfoLanguage GetByCompanyIdLanguageId(TransactionManager transactionManager, System.Int32 _companyId, System.String _languageId, int start, int pageLength)
		{
			int count = -1;
			return GetByCompanyIdLanguageId(transactionManager, _companyId, _languageId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CompanyInfoLanguage index.
		/// </summary>
		/// <param name="_companyId"></param>
		/// <param name="_languageId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.CompanyInfoLanguage"/> class.</returns>
		public RTStockData.Entities.CompanyInfoLanguage GetByCompanyIdLanguageId(System.Int32 _companyId, System.String _languageId, int start, int pageLength, out int count)
		{
			return GetByCompanyIdLanguageId(null, _companyId, _languageId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CompanyInfoLanguage index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_companyId"></param>
		/// <param name="_languageId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.CompanyInfoLanguage"/> class.</returns>
		public abstract RTStockData.Entities.CompanyInfoLanguage GetByCompanyIdLanguageId(TransactionManager transactionManager, System.Int32 _companyId, System.String _languageId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;CompanyInfoLanguage&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;CompanyInfoLanguage&gt;"/></returns>
		public static TList<CompanyInfoLanguage> Fill(IDataReader reader, TList<CompanyInfoLanguage> rows, int start, int pageLength)
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
				
				RTStockData.Entities.CompanyInfoLanguage c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("CompanyInfoLanguage")
					.Append("|").Append((System.Int32)reader[((int)CompanyInfoLanguageColumn.CompanyId - 1)])
					.Append("|").Append((System.String)reader[((int)CompanyInfoLanguageColumn.LanguageId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<CompanyInfoLanguage>(
					key.ToString(), // EntityTrackingKey
					"CompanyInfoLanguage",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new RTStockData.Entities.CompanyInfoLanguage();
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
					c.CompanyId = (System.Int32)reader[((int)CompanyInfoLanguageColumn.CompanyId - 1)];
					c.OriginalCompanyId = c.CompanyId;
					c.LanguageId = (System.String)reader[((int)CompanyInfoLanguageColumn.LanguageId - 1)];
					c.OriginalLanguageId = c.LanguageId;
					c.CompanyName = (reader.IsDBNull(((int)CompanyInfoLanguageColumn.CompanyName - 1)))?null:(System.String)reader[((int)CompanyInfoLanguageColumn.CompanyName - 1)];
					c.Description = (reader.IsDBNull(((int)CompanyInfoLanguageColumn.Description - 1)))?null:(System.String)reader[((int)CompanyInfoLanguageColumn.Description - 1)];
					c.Address = (reader.IsDBNull(((int)CompanyInfoLanguageColumn.Address - 1)))?null:(System.String)reader[((int)CompanyInfoLanguageColumn.Address - 1)];
					c.BusinessArea = (reader.IsDBNull(((int)CompanyInfoLanguageColumn.BusinessArea - 1)))?null:(System.String)reader[((int)CompanyInfoLanguageColumn.BusinessArea - 1)];
					c.Strategy = (reader.IsDBNull(((int)CompanyInfoLanguageColumn.Strategy - 1)))?null:(System.String)reader[((int)CompanyInfoLanguageColumn.Strategy - 1)];
					c.MarketPosition = (reader.IsDBNull(((int)CompanyInfoLanguageColumn.MarketPosition - 1)))?null:(System.String)reader[((int)CompanyInfoLanguageColumn.MarketPosition - 1)];
					c.ContactInformation = (reader.IsDBNull(((int)CompanyInfoLanguageColumn.ContactInformation - 1)))?null:(System.String)reader[((int)CompanyInfoLanguageColumn.ContactInformation - 1)];
					c.Director = (reader.IsDBNull(((int)CompanyInfoLanguageColumn.Director - 1)))?null:(System.String)reader[((int)CompanyInfoLanguageColumn.Director - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="RTStockData.Entities.CompanyInfoLanguage"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="RTStockData.Entities.CompanyInfoLanguage"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, RTStockData.Entities.CompanyInfoLanguage entity)
		{
			if (!reader.Read()) return;
			
			entity.CompanyId = (System.Int32)reader[((int)CompanyInfoLanguageColumn.CompanyId - 1)];
			entity.OriginalCompanyId = (System.Int32)reader["CompanyId"];
			entity.LanguageId = (System.String)reader[((int)CompanyInfoLanguageColumn.LanguageId - 1)];
			entity.OriginalLanguageId = (System.String)reader["LanguageId"];
			entity.CompanyName = (reader.IsDBNull(((int)CompanyInfoLanguageColumn.CompanyName - 1)))?null:(System.String)reader[((int)CompanyInfoLanguageColumn.CompanyName - 1)];
			entity.Description = (reader.IsDBNull(((int)CompanyInfoLanguageColumn.Description - 1)))?null:(System.String)reader[((int)CompanyInfoLanguageColumn.Description - 1)];
			entity.Address = (reader.IsDBNull(((int)CompanyInfoLanguageColumn.Address - 1)))?null:(System.String)reader[((int)CompanyInfoLanguageColumn.Address - 1)];
			entity.BusinessArea = (reader.IsDBNull(((int)CompanyInfoLanguageColumn.BusinessArea - 1)))?null:(System.String)reader[((int)CompanyInfoLanguageColumn.BusinessArea - 1)];
			entity.Strategy = (reader.IsDBNull(((int)CompanyInfoLanguageColumn.Strategy - 1)))?null:(System.String)reader[((int)CompanyInfoLanguageColumn.Strategy - 1)];
			entity.MarketPosition = (reader.IsDBNull(((int)CompanyInfoLanguageColumn.MarketPosition - 1)))?null:(System.String)reader[((int)CompanyInfoLanguageColumn.MarketPosition - 1)];
			entity.ContactInformation = (reader.IsDBNull(((int)CompanyInfoLanguageColumn.ContactInformation - 1)))?null:(System.String)reader[((int)CompanyInfoLanguageColumn.ContactInformation - 1)];
			entity.Director = (reader.IsDBNull(((int)CompanyInfoLanguageColumn.Director - 1)))?null:(System.String)reader[((int)CompanyInfoLanguageColumn.Director - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="RTStockData.Entities.CompanyInfoLanguage"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="RTStockData.Entities.CompanyInfoLanguage"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, RTStockData.Entities.CompanyInfoLanguage entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.CompanyId = (System.Int32)dataRow["CompanyId"];
			entity.OriginalCompanyId = (System.Int32)dataRow["CompanyId"];
			entity.LanguageId = (System.String)dataRow["LanguageId"];
			entity.OriginalLanguageId = (System.String)dataRow["LanguageId"];
			entity.CompanyName = Convert.IsDBNull(dataRow["CompanyName"]) ? null : (System.String)dataRow["CompanyName"];
			entity.Description = Convert.IsDBNull(dataRow["Description"]) ? null : (System.String)dataRow["Description"];
			entity.Address = Convert.IsDBNull(dataRow["Address"]) ? null : (System.String)dataRow["Address"];
			entity.BusinessArea = Convert.IsDBNull(dataRow["BusinessArea"]) ? null : (System.String)dataRow["BusinessArea"];
			entity.Strategy = Convert.IsDBNull(dataRow["Strategy"]) ? null : (System.String)dataRow["Strategy"];
			entity.MarketPosition = Convert.IsDBNull(dataRow["MarketPosition"]) ? null : (System.String)dataRow["MarketPosition"];
			entity.ContactInformation = Convert.IsDBNull(dataRow["ContactInformation"]) ? null : (System.String)dataRow["ContactInformation"];
			entity.Director = Convert.IsDBNull(dataRow["Director"]) ? null : (System.String)dataRow["Director"];
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
		/// <param name="entity">The <see cref="RTStockData.Entities.CompanyInfoLanguage"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">RTStockData.Entities.CompanyInfoLanguage Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, RTStockData.Entities.CompanyInfoLanguage entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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
		/// Deep Save the entire object graph of the RTStockData.Entities.CompanyInfoLanguage object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">RTStockData.Entities.CompanyInfoLanguage instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">RTStockData.Entities.CompanyInfoLanguage Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, RTStockData.Entities.CompanyInfoLanguage entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
	#region CompanyInfoLanguageChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>RTStockData.Entities.CompanyInfoLanguage</c>
	///</summary>
	public enum CompanyInfoLanguageChildEntityTypes
	{
	}
	
	#endregion CompanyInfoLanguageChildEntityTypes
	
	#region CompanyInfoLanguageFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;CompanyInfoLanguageColumn&gt;"/> class
	/// that is used exclusively with a <see cref="CompanyInfoLanguage"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CompanyInfoLanguageFilterBuilder : SqlFilterBuilder<CompanyInfoLanguageColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CompanyInfoLanguageFilterBuilder class.
		/// </summary>
		public CompanyInfoLanguageFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the CompanyInfoLanguageFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CompanyInfoLanguageFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CompanyInfoLanguageFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CompanyInfoLanguageFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CompanyInfoLanguageFilterBuilder
	
	#region CompanyInfoLanguageParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;CompanyInfoLanguageColumn&gt;"/> class
	/// that is used exclusively with a <see cref="CompanyInfoLanguage"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CompanyInfoLanguageParameterBuilder : ParameterizedSqlFilterBuilder<CompanyInfoLanguageColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CompanyInfoLanguageParameterBuilder class.
		/// </summary>
		public CompanyInfoLanguageParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the CompanyInfoLanguageParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CompanyInfoLanguageParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CompanyInfoLanguageParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CompanyInfoLanguageParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CompanyInfoLanguageParameterBuilder
	
	#region CompanyInfoLanguageSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;CompanyInfoLanguageColumn&gt;"/> class
	/// that is used exclusively with a <see cref="CompanyInfoLanguage"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class CompanyInfoLanguageSortBuilder : SqlSortBuilder<CompanyInfoLanguageColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CompanyInfoLanguageSqlSortBuilder class.
		/// </summary>
		public CompanyInfoLanguageSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion CompanyInfoLanguageSortBuilder
	
} // end namespace
