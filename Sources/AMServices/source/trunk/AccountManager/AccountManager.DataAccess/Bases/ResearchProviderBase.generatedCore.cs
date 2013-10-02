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
	/// This class is the base class for any <see cref="ResearchProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class ResearchProviderBaseCore : EntityProviderBase<AccountManager.Entities.Research, AccountManager.Entities.ResearchKey>
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
		public override bool Delete(TransactionManager transactionManager, AccountManager.Entities.ResearchKey key)
		{
			return Delete(transactionManager, key.ResearchId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_researchId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.String _researchId)
		{
			return Delete(null, _researchId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_researchId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.String _researchId);		
		
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
		public override AccountManager.Entities.Research Get(TransactionManager transactionManager, AccountManager.Entities.ResearchKey key, int start, int pageLength)
		{
			return GetByResearchId(transactionManager, key.ResearchId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Research index.
		/// </summary>
		/// <param name="_researchId"></param>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.Research"/> class.</returns>
		public AccountManager.Entities.Research GetByResearchId(System.String _researchId)
		{
			int count = -1;
			return GetByResearchId(null,_researchId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Research index.
		/// </summary>
		/// <param name="_researchId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.Research"/> class.</returns>
		public AccountManager.Entities.Research GetByResearchId(System.String _researchId, int start, int pageLength)
		{
			int count = -1;
			return GetByResearchId(null, _researchId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Research index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_researchId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.Research"/> class.</returns>
		public AccountManager.Entities.Research GetByResearchId(TransactionManager transactionManager, System.String _researchId)
		{
			int count = -1;
			return GetByResearchId(transactionManager, _researchId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Research index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_researchId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.Research"/> class.</returns>
		public AccountManager.Entities.Research GetByResearchId(TransactionManager transactionManager, System.String _researchId, int start, int pageLength)
		{
			int count = -1;
			return GetByResearchId(transactionManager, _researchId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Research index.
		/// </summary>
		/// <param name="_researchId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.Research"/> class.</returns>
		public AccountManager.Entities.Research GetByResearchId(System.String _researchId, int start, int pageLength, out int count)
		{
			return GetByResearchId(null, _researchId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Research index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_researchId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AccountManager.Entities.Research"/> class.</returns>
		public abstract AccountManager.Entities.Research GetByResearchId(TransactionManager transactionManager, System.String _researchId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Research&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Research&gt;"/></returns>
		public static TList<Research> Fill(IDataReader reader, TList<Research> rows, int start, int pageLength)
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
				
				AccountManager.Entities.Research c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Research")
					.Append("|").Append((System.String)reader[((int)ResearchColumn.ResearchId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Research>(
					key.ToString(), // EntityTrackingKey
					"Research",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new AccountManager.Entities.Research();
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
					c.ResearchId = (System.String)reader[((int)ResearchColumn.ResearchId - 1)];
					c.OriginalResearchId = c.ResearchId;
					c.UploadedDate = (System.DateTime)reader[((int)ResearchColumn.UploadedDate - 1)];
					c.Tittle = (System.String)reader[((int)ResearchColumn.Tittle - 1)];
					c.Path = (System.String)reader[((int)ResearchColumn.Path - 1)];
					c.Downloads = (System.Int32)reader[((int)ResearchColumn.Downloads - 1)];
					c.Actived = (System.Boolean)reader[((int)ResearchColumn.Actived - 1)];
					c.UploadedUser = (System.String)reader[((int)ResearchColumn.UploadedUser - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="AccountManager.Entities.Research"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AccountManager.Entities.Research"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AccountManager.Entities.Research entity)
		{
			if (!reader.Read()) return;
			
			entity.ResearchId = (System.String)reader[((int)ResearchColumn.ResearchId - 1)];
			entity.OriginalResearchId = (System.String)reader["ResearchID"];
			entity.UploadedDate = (System.DateTime)reader[((int)ResearchColumn.UploadedDate - 1)];
			entity.Tittle = (System.String)reader[((int)ResearchColumn.Tittle - 1)];
			entity.Path = (System.String)reader[((int)ResearchColumn.Path - 1)];
			entity.Downloads = (System.Int32)reader[((int)ResearchColumn.Downloads - 1)];
			entity.Actived = (System.Boolean)reader[((int)ResearchColumn.Actived - 1)];
			entity.UploadedUser = (System.String)reader[((int)ResearchColumn.UploadedUser - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="AccountManager.Entities.Research"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AccountManager.Entities.Research"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AccountManager.Entities.Research entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.ResearchId = (System.String)dataRow["ResearchID"];
			entity.OriginalResearchId = (System.String)dataRow["ResearchID"];
			entity.UploadedDate = (System.DateTime)dataRow["UploadedDate"];
			entity.Tittle = (System.String)dataRow["Tittle"];
			entity.Path = (System.String)dataRow["Path"];
			entity.Downloads = (System.Int32)dataRow["Downloads"];
			entity.Actived = (System.Boolean)dataRow["Actived"];
			entity.UploadedUser = (System.String)dataRow["UploadedUser"];
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
		/// <param name="entity">The <see cref="AccountManager.Entities.Research"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AccountManager.Entities.Research Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AccountManager.Entities.Research entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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
		/// Deep Save the entire object graph of the AccountManager.Entities.Research object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AccountManager.Entities.Research instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AccountManager.Entities.Research Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AccountManager.Entities.Research entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
	#region ResearchChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>AccountManager.Entities.Research</c>
	///</summary>
	public enum ResearchChildEntityTypes
	{
	}
	
	#endregion ResearchChildEntityTypes
	
	#region ResearchFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;ResearchColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Research"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ResearchFilterBuilder : SqlFilterBuilder<ResearchColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ResearchFilterBuilder class.
		/// </summary>
		public ResearchFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ResearchFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ResearchFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ResearchFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ResearchFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ResearchFilterBuilder
	
	#region ResearchParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;ResearchColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Research"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ResearchParameterBuilder : ParameterizedSqlFilterBuilder<ResearchColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ResearchParameterBuilder class.
		/// </summary>
		public ResearchParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ResearchParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ResearchParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ResearchParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ResearchParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ResearchParameterBuilder
	
	#region ResearchSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;ResearchColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Research"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class ResearchSortBuilder : SqlSortBuilder<ResearchColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ResearchSqlSortBuilder class.
		/// </summary>
		public ResearchSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion ResearchSortBuilder
	
} // end namespace
