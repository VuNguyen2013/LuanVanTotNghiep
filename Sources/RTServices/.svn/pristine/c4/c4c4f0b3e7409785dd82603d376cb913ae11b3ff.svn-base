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
	/// This class is the base class for any <see cref="CompanyInfoProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class CompanyInfoProviderBaseCore : EntityProviderBase<RTStockData.Entities.CompanyInfo, RTStockData.Entities.CompanyInfoKey>
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
		public override bool Delete(TransactionManager transactionManager, RTStockData.Entities.CompanyInfoKey key)
		{
			return Delete(transactionManager, key.CompanyId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_companyId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Int32 _companyId)
		{
			return Delete(null, _companyId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_companyId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Int32 _companyId);		
		
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
		public override RTStockData.Entities.CompanyInfo Get(TransactionManager transactionManager, RTStockData.Entities.CompanyInfoKey key, int start, int pageLength)
		{
			return GetByCompanyId(transactionManager, key.CompanyId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_CompanyInfo index.
		/// </summary>
		/// <param name="_companyId"></param>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.CompanyInfo"/> class.</returns>
		public RTStockData.Entities.CompanyInfo GetByCompanyId(System.Int32 _companyId)
		{
			int count = -1;
			return GetByCompanyId(null,_companyId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CompanyInfo index.
		/// </summary>
		/// <param name="_companyId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.CompanyInfo"/> class.</returns>
		public RTStockData.Entities.CompanyInfo GetByCompanyId(System.Int32 _companyId, int start, int pageLength)
		{
			int count = -1;
			return GetByCompanyId(null, _companyId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CompanyInfo index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_companyId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.CompanyInfo"/> class.</returns>
		public RTStockData.Entities.CompanyInfo GetByCompanyId(TransactionManager transactionManager, System.Int32 _companyId)
		{
			int count = -1;
			return GetByCompanyId(transactionManager, _companyId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CompanyInfo index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_companyId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.CompanyInfo"/> class.</returns>
		public RTStockData.Entities.CompanyInfo GetByCompanyId(TransactionManager transactionManager, System.Int32 _companyId, int start, int pageLength)
		{
			int count = -1;
			return GetByCompanyId(transactionManager, _companyId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CompanyInfo index.
		/// </summary>
		/// <param name="_companyId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.CompanyInfo"/> class.</returns>
		public RTStockData.Entities.CompanyInfo GetByCompanyId(System.Int32 _companyId, int start, int pageLength, out int count)
		{
			return GetByCompanyId(null, _companyId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CompanyInfo index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_companyId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.CompanyInfo"/> class.</returns>
		public abstract RTStockData.Entities.CompanyInfo GetByCompanyId(TransactionManager transactionManager, System.Int32 _companyId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#region _CompanyInfo_GetCompanyInfoByLanguageId 
		
		/// <summary>
		///	This method wrap the '_CompanyInfo_GetCompanyInfoByLanguageId' stored procedure. 
		/// </summary>
		/// <param name="languageId"> A <c>System.String</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet GetCompanyInfoByLanguageId(System.String languageId)
		{
			return GetCompanyInfoByLanguageId(null, 0, int.MaxValue , languageId);
		}
		
		/// <summary>
		///	This method wrap the '_CompanyInfo_GetCompanyInfoByLanguageId' stored procedure. 
		/// </summary>
		/// <param name="languageId"> A <c>System.String</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet GetCompanyInfoByLanguageId(int start, int pageLength, System.String languageId)
		{
			return GetCompanyInfoByLanguageId(null, start, pageLength , languageId);
		}
				
		/// <summary>
		///	This method wrap the '_CompanyInfo_GetCompanyInfoByLanguageId' stored procedure. 
		/// </summary>
		/// <param name="languageId"> A <c>System.String</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet GetCompanyInfoByLanguageId(TransactionManager transactionManager, System.String languageId)
		{
			return GetCompanyInfoByLanguageId(transactionManager, 0, int.MaxValue , languageId);
		}
		
		/// <summary>
		///	This method wrap the '_CompanyInfo_GetCompanyInfoByLanguageId' stored procedure. 
		/// </summary>
		/// <param name="languageId"> A <c>System.String</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet GetCompanyInfoByLanguageId(TransactionManager transactionManager, int start, int pageLength , System.String languageId);

	    /// <summary>
	    /// Get CompanyInfo Language
	    /// </summary>
	    /// <param name="code">The Code</param>
	    /// <param name="transactionManager"></param>
	    /// <param name="start"></param>
	    /// <param name="pageLength"></param>
	    /// <returns></returns>
	    public abstract DataSet GetAllCompanyInfoLanguage(TransactionManager transactionManager, int start, int pageLength,
	                                                      System.String code);	
		#endregion
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;CompanyInfo&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;CompanyInfo&gt;"/></returns>
		public static TList<CompanyInfo> Fill(IDataReader reader, TList<CompanyInfo> rows, int start, int pageLength)
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
				
				RTStockData.Entities.CompanyInfo c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("CompanyInfo")
					.Append("|").Append((System.Int32)reader[((int)CompanyInfoColumn.CompanyId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<CompanyInfo>(
					key.ToString(), // EntityTrackingKey
					"CompanyInfo",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new RTStockData.Entities.CompanyInfo();
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
					c.CompanyId = (System.Int32)reader[((int)CompanyInfoColumn.CompanyId - 1)];
					c.Code = (System.String)reader[((int)CompanyInfoColumn.Code - 1)];
					c.ShortName = (reader.IsDBNull(((int)CompanyInfoColumn.ShortName - 1)))?null:(System.String)reader[((int)CompanyInfoColumn.ShortName - 1)];
					c.Phone = (reader.IsDBNull(((int)CompanyInfoColumn.Phone - 1)))?null:(System.String)reader[((int)CompanyInfoColumn.Phone - 1)];
					c.Email = (reader.IsDBNull(((int)CompanyInfoColumn.Email - 1)))?null:(System.String)reader[((int)CompanyInfoColumn.Email - 1)];
					c.Fax = (reader.IsDBNull(((int)CompanyInfoColumn.Fax - 1)))?null:(System.String)reader[((int)CompanyInfoColumn.Fax - 1)];
					c.Website = (reader.IsDBNull(((int)CompanyInfoColumn.Website - 1)))?null:(System.String)reader[((int)CompanyInfoColumn.Website - 1)];
					c.LastModified = (reader.IsDBNull(((int)CompanyInfoColumn.LastModified - 1)))?null:(System.DateTime?)reader[((int)CompanyInfoColumn.LastModified - 1)];
					c.IsPublished = (reader.IsDBNull(((int)CompanyInfoColumn.IsPublished - 1)))?null:(System.Boolean?)reader[((int)CompanyInfoColumn.IsPublished - 1)];
					c.AuthorId = (reader.IsDBNull(((int)CompanyInfoColumn.AuthorId - 1)))?null:(System.Int32?)reader[((int)CompanyInfoColumn.AuthorId - 1)];
					c.SectorId = (reader.IsDBNull(((int)CompanyInfoColumn.SectorId - 1)))?null:(System.Guid?)reader[((int)CompanyInfoColumn.SectorId - 1)];
					c.MarketId = (System.Int32)reader[((int)CompanyInfoColumn.MarketId - 1)];
					c.PageView = (reader.IsDBNull(((int)CompanyInfoColumn.PageView - 1)))?null:(System.Int32?)reader[((int)CompanyInfoColumn.PageView - 1)];
					c.IndustryGroup = (reader.IsDBNull(((int)CompanyInfoColumn.IndustryGroup - 1)))?null:(System.Int32?)reader[((int)CompanyInfoColumn.IndustryGroup - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="RTStockData.Entities.CompanyInfo"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="RTStockData.Entities.CompanyInfo"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, RTStockData.Entities.CompanyInfo entity)
		{
			if (!reader.Read()) return;
			
			entity.CompanyId = (System.Int32)reader[((int)CompanyInfoColumn.CompanyId - 1)];
			entity.Code = (System.String)reader[((int)CompanyInfoColumn.Code - 1)];
			entity.ShortName = (reader.IsDBNull(((int)CompanyInfoColumn.ShortName - 1)))?null:(System.String)reader[((int)CompanyInfoColumn.ShortName - 1)];
			entity.Phone = (reader.IsDBNull(((int)CompanyInfoColumn.Phone - 1)))?null:(System.String)reader[((int)CompanyInfoColumn.Phone - 1)];
			entity.Email = (reader.IsDBNull(((int)CompanyInfoColumn.Email - 1)))?null:(System.String)reader[((int)CompanyInfoColumn.Email - 1)];
			entity.Fax = (reader.IsDBNull(((int)CompanyInfoColumn.Fax - 1)))?null:(System.String)reader[((int)CompanyInfoColumn.Fax - 1)];
			entity.Website = (reader.IsDBNull(((int)CompanyInfoColumn.Website - 1)))?null:(System.String)reader[((int)CompanyInfoColumn.Website - 1)];
			entity.LastModified = (reader.IsDBNull(((int)CompanyInfoColumn.LastModified - 1)))?null:(System.DateTime?)reader[((int)CompanyInfoColumn.LastModified - 1)];
			entity.IsPublished = (reader.IsDBNull(((int)CompanyInfoColumn.IsPublished - 1)))?null:(System.Boolean?)reader[((int)CompanyInfoColumn.IsPublished - 1)];
			entity.AuthorId = (reader.IsDBNull(((int)CompanyInfoColumn.AuthorId - 1)))?null:(System.Int32?)reader[((int)CompanyInfoColumn.AuthorId - 1)];
			entity.SectorId = (reader.IsDBNull(((int)CompanyInfoColumn.SectorId - 1)))?null:(System.Guid?)reader[((int)CompanyInfoColumn.SectorId - 1)];
			entity.MarketId = (System.Int32)reader[((int)CompanyInfoColumn.MarketId - 1)];
			entity.PageView = (reader.IsDBNull(((int)CompanyInfoColumn.PageView - 1)))?null:(System.Int32?)reader[((int)CompanyInfoColumn.PageView - 1)];
			entity.IndustryGroup = (reader.IsDBNull(((int)CompanyInfoColumn.IndustryGroup - 1)))?null:(System.Int32?)reader[((int)CompanyInfoColumn.IndustryGroup - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="RTStockData.Entities.CompanyInfo"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="RTStockData.Entities.CompanyInfo"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, RTStockData.Entities.CompanyInfo entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.CompanyId = (System.Int32)dataRow["CompanyId"];
			entity.Code = (System.String)dataRow["Code"];
			entity.ShortName = Convert.IsDBNull(dataRow["ShortName"]) ? null : (System.String)dataRow["ShortName"];
			entity.Phone = Convert.IsDBNull(dataRow["Phone"]) ? null : (System.String)dataRow["Phone"];
			entity.Email = Convert.IsDBNull(dataRow["Email"]) ? null : (System.String)dataRow["Email"];
			entity.Fax = Convert.IsDBNull(dataRow["Fax"]) ? null : (System.String)dataRow["Fax"];
			entity.Website = Convert.IsDBNull(dataRow["Website"]) ? null : (System.String)dataRow["Website"];
			entity.LastModified = Convert.IsDBNull(dataRow["LastModified"]) ? null : (System.DateTime?)dataRow["LastModified"];
			entity.IsPublished = Convert.IsDBNull(dataRow["IsPublished"]) ? null : (System.Boolean?)dataRow["IsPublished"];
			entity.AuthorId = Convert.IsDBNull(dataRow["AuthorId"]) ? null : (System.Int32?)dataRow["AuthorId"];
			entity.SectorId = Convert.IsDBNull(dataRow["SectorId"]) ? null : (System.Guid?)dataRow["SectorId"];
			entity.MarketId = (System.Int32)dataRow["MarketId"];
			entity.PageView = Convert.IsDBNull(dataRow["PageView"]) ? null : (System.Int32?)dataRow["PageView"];
			entity.IndustryGroup = Convert.IsDBNull(dataRow["IndustryGroup"]) ? null : (System.Int32?)dataRow["IndustryGroup"];
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
		/// <param name="entity">The <see cref="RTStockData.Entities.CompanyInfo"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">RTStockData.Entities.CompanyInfo Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, RTStockData.Entities.CompanyInfo entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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
		/// Deep Save the entire object graph of the RTStockData.Entities.CompanyInfo object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">RTStockData.Entities.CompanyInfo instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">RTStockData.Entities.CompanyInfo Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, RTStockData.Entities.CompanyInfo entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
	#region CompanyInfoChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>RTStockData.Entities.CompanyInfo</c>
	///</summary>
	public enum CompanyInfoChildEntityTypes
	{
	}
	
	#endregion CompanyInfoChildEntityTypes
	
	#region CompanyInfoFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;CompanyInfoColumn&gt;"/> class
	/// that is used exclusively with a <see cref="CompanyInfo"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CompanyInfoFilterBuilder : SqlFilterBuilder<CompanyInfoColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CompanyInfoFilterBuilder class.
		/// </summary>
		public CompanyInfoFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the CompanyInfoFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CompanyInfoFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CompanyInfoFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CompanyInfoFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CompanyInfoFilterBuilder
	
	#region CompanyInfoParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;CompanyInfoColumn&gt;"/> class
	/// that is used exclusively with a <see cref="CompanyInfo"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CompanyInfoParameterBuilder : ParameterizedSqlFilterBuilder<CompanyInfoColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CompanyInfoParameterBuilder class.
		/// </summary>
		public CompanyInfoParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the CompanyInfoParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CompanyInfoParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CompanyInfoParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CompanyInfoParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CompanyInfoParameterBuilder
	
	#region CompanyInfoSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;CompanyInfoColumn&gt;"/> class
	/// that is used exclusively with a <see cref="CompanyInfo"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class CompanyInfoSortBuilder : SqlSortBuilder<CompanyInfoColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CompanyInfoSqlSortBuilder class.
		/// </summary>
		public CompanyInfoSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion CompanyInfoSortBuilder
	
} // end namespace
