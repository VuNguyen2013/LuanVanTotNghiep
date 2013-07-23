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
    /// This class is the base class for any <see cref="IndexVn30HistoryProviderBase"/> implementation.
    /// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
    ///</summary>
    public abstract partial class IndexVn30HistoryProviderBaseCore : EntityProviderBase<RTStockData.Entities.IndexVn30History, RTStockData.Entities.IndexVn30HistoryKey>
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
        public override bool Delete(TransactionManager transactionManager, RTStockData.Entities.IndexVn30HistoryKey key)
        {
            return Delete(transactionManager, key.Id);
        }

        /// <summary>
        /// 	Deletes a row from the DataSource.
        /// </summary>
        /// <param name="_id">. Primary Key.</param>
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
        /// <param name="_id">. Primary Key.</param>
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
        public override RTStockData.Entities.IndexVn30History Get(TransactionManager transactionManager, RTStockData.Entities.IndexVn30HistoryKey key, int start, int pageLength)
        {
            return GetById(transactionManager, key.Id, start, pageLength);
        }

        /// <summary>
        /// 	Gets rows from the datasource based on the primary key PK_IndexVN30_History index.
        /// </summary>
        /// <param name="_id"></param>
        /// <returns>Returns an instance of the <see cref="RTStockData.Entities.IndexVn30History"/> class.</returns>
        public RTStockData.Entities.IndexVn30History GetById(System.Int64 _id)
        {
            int count = -1;
            return GetById(null, _id, 0, int.MaxValue, out count);
        }

        /// <summary>
        /// 	Gets rows from the datasource based on the PK_IndexVN30_History index.
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="start">Row number at which to start reading, the first row is 0.</param>
        /// <param name="pageLength">Number of rows to return.</param>
        /// <remarks></remarks>
        /// <returns>Returns an instance of the <see cref="RTStockData.Entities.IndexVn30History"/> class.</returns>
        public RTStockData.Entities.IndexVn30History GetById(System.Int64 _id, int start, int pageLength)
        {
            int count = -1;
            return GetById(null, _id, start, pageLength, out count);
        }

        /// <summary>
        /// 	Gets rows from the datasource based on the PK_IndexVN30_History index.
        /// </summary>
        /// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
        /// <param name="_id"></param>
        /// <remarks></remarks>
        /// <returns>Returns an instance of the <see cref="RTStockData.Entities.IndexVn30History"/> class.</returns>
        public RTStockData.Entities.IndexVn30History GetById(TransactionManager transactionManager, System.Int64 _id)
        {
            int count = -1;
            return GetById(transactionManager, _id, 0, int.MaxValue, out count);
        }

        /// <summary>
        /// 	Gets rows from the datasource based on the PK_IndexVN30_History index.
        /// </summary>
        /// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
        /// <param name="_id"></param>
        /// <param name="start">Row number at which to start reading, the first row is 0.</param>
        /// <param name="pageLength">Number of rows to return.</param>
        /// <remarks></remarks>
        /// <returns>Returns an instance of the <see cref="RTStockData.Entities.IndexVn30History"/> class.</returns>
        public RTStockData.Entities.IndexVn30History GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength)
        {
            int count = -1;
            return GetById(transactionManager, _id, start, pageLength, out count);
        }

        /// <summary>
        /// 	Gets rows from the datasource based on the PK_IndexVN30_History index.
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="start">Row number at which to start reading, the first row is 0.</param>
        /// <param name="pageLength">Number of rows to return.</param>
        /// <param name="count">out parameter to get total records for query</param>
        /// <remarks></remarks>
        /// <returns>Returns an instance of the <see cref="RTStockData.Entities.IndexVn30History"/> class.</returns>
        public RTStockData.Entities.IndexVn30History GetById(System.Int64 _id, int start, int pageLength, out int count)
        {
            return GetById(null, _id, start, pageLength, out count);
        }


        /// <summary>
        /// 	Gets rows from the datasource based on the PK_IndexVN30_History index.
        /// </summary>
        /// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
        /// <param name="_id"></param>
        /// <param name="start">Row number at which to start reading, the first row is 0.</param>
        /// <param name="pageLength">Number of rows to return.</param>
        /// <param name="count">The total number of records.</param>
        /// <returns>Returns an instance of the <see cref="RTStockData.Entities.IndexVn30History"/> class.</returns>
        public abstract RTStockData.Entities.IndexVn30History GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength, out int count);

        #endregion "Get By Index Functions"

        #region Custom Methods


        #endregion

        #region Helper Functions

        /// <summary>
        /// Fill a TList&lt;IndexVn30History&gt; From a DataReader.
        /// </summary>
        /// <param name="reader">Datareader</param>
        /// <param name="rows">The collection to fill</param>
        /// <param name="start">Row number at which to start reading, the first row is 0.</param>
        /// <param name="pageLength">number of rows.</param>
        /// <returns>a <see cref="TList&lt;IndexVn30History&gt;"/></returns>
        public static TList<IndexVn30History> Fill(IDataReader reader, TList<IndexVn30History> rows, int start, int pageLength)
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

                RTStockData.Entities.IndexVn30History c = null;
                if (useEntityFactory)
                {
                    key = new System.Text.StringBuilder("IndexVn30History")
                    .Append("|").Append((System.Int64)reader[((int)IndexVn30HistoryColumn.Id - 1)]).ToString();
                    c = EntityManager.LocateOrCreate<IndexVn30History>(
                    key.ToString(), // EntityTrackingKey
                    "IndexVn30History",  //Creational Type
                    entityCreationFactoryType,  //Factory used to create entity
                    enableEntityTracking); // Track this entity?
                }
                else
                {
                    c = new RTStockData.Entities.IndexVn30History();
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
                    c.Id = (System.Int64)reader[((int)IndexVn30HistoryColumn.Id - 1)];
                    c.TradeDate = (reader.IsDBNull(((int)IndexVn30HistoryColumn.TradeDate - 1))) ? null : (System.DateTime?)reader[((int)IndexVn30HistoryColumn.TradeDate - 1)];
                    c.Index = (reader.IsDBNull(((int)IndexVn30HistoryColumn.Index - 1))) ? null : (System.Int64?)reader[((int)IndexVn30HistoryColumn.Index - 1)];
                    c.TotalShares = (reader.IsDBNull(((int)IndexVn30HistoryColumn.TotalShares - 1))) ? null : (System.Int64?)reader[((int)IndexVn30HistoryColumn.TotalShares - 1)];
                    c.TotalValues = (reader.IsDBNull(((int)IndexVn30HistoryColumn.TotalValues - 1))) ? null : (System.Int64?)reader[((int)IndexVn30HistoryColumn.TotalValues - 1)];
                    c.Up = (reader.IsDBNull(((int)IndexVn30HistoryColumn.Up - 1))) ? null : (System.Int64?)reader[((int)IndexVn30HistoryColumn.Up - 1)];
                    c.Down = (reader.IsDBNull(((int)IndexVn30HistoryColumn.Down - 1))) ? null : (System.Int64?)reader[((int)IndexVn30HistoryColumn.Down - 1)];
                    c.NoChange = (reader.IsDBNull(((int)IndexVn30HistoryColumn.NoChange - 1))) ? null : (System.Int64?)reader[((int)IndexVn30HistoryColumn.NoChange - 1)];
                    c.Time = (reader.IsDBNull(((int)IndexVn30HistoryColumn.Time - 1))) ? null : (System.Int64?)reader[((int)IndexVn30HistoryColumn.Time - 1)];
                    c.Change = (reader.IsDBNull(((int)IndexVn30HistoryColumn.Change - 1))) ? null : (System.Int64?)reader[((int)IndexVn30HistoryColumn.Change - 1)];
                    c.PerChange = (reader.IsDBNull(((int)IndexVn30HistoryColumn.PerChange - 1))) ? null : (System.Double?)reader[((int)IndexVn30HistoryColumn.PerChange - 1)];
                    c.EntityTrackingKey = key;
                    c.AcceptChanges();
                    c.SuppressEntityEvents = false;
                }
                rows.Add(c);
            }
            return rows;
        }
        /// <summary>
        /// Refreshes the <see cref="RTStockData.Entities.IndexVn30History"/> object from the <see cref="IDataReader"/>.
        /// </summary>
        /// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
        /// <param name="entity">The <see cref="RTStockData.Entities.IndexVn30History"/> object to refresh.</param>
        public static void RefreshEntity(IDataReader reader, RTStockData.Entities.IndexVn30History entity)
        {
            if (!reader.Read()) return;

            entity.Id = (System.Int64)reader[((int)IndexVn30HistoryColumn.Id - 1)];
            entity.TradeDate = (reader.IsDBNull(((int)IndexVn30HistoryColumn.TradeDate - 1))) ? null : (System.DateTime?)reader[((int)IndexVn30HistoryColumn.TradeDate - 1)];
            entity.Index = (reader.IsDBNull(((int)IndexVn30HistoryColumn.Index - 1))) ? null : (System.Int64?)reader[((int)IndexVn30HistoryColumn.Index - 1)];
            entity.TotalShares = (reader.IsDBNull(((int)IndexVn30HistoryColumn.TotalShares - 1))) ? null : (System.Int64?)reader[((int)IndexVn30HistoryColumn.TotalShares - 1)];
            entity.TotalValues = (reader.IsDBNull(((int)IndexVn30HistoryColumn.TotalValues - 1))) ? null : (System.Int64?)reader[((int)IndexVn30HistoryColumn.TotalValues - 1)];
            entity.Up = (reader.IsDBNull(((int)IndexVn30HistoryColumn.Up - 1))) ? null : (System.Int64?)reader[((int)IndexVn30HistoryColumn.Up - 1)];
            entity.Down = (reader.IsDBNull(((int)IndexVn30HistoryColumn.Down - 1))) ? null : (System.Int64?)reader[((int)IndexVn30HistoryColumn.Down - 1)];
            entity.NoChange = (reader.IsDBNull(((int)IndexVn30HistoryColumn.NoChange - 1))) ? null : (System.Int64?)reader[((int)IndexVn30HistoryColumn.NoChange - 1)];
            entity.Time = (reader.IsDBNull(((int)IndexVn30HistoryColumn.Time - 1))) ? null : (System.Int64?)reader[((int)IndexVn30HistoryColumn.Time - 1)];
            entity.Change = (reader.IsDBNull(((int)IndexVn30HistoryColumn.Change - 1))) ? null : (System.Int64?)reader[((int)IndexVn30HistoryColumn.Change - 1)];
            entity.PerChange = (reader.IsDBNull(((int)IndexVn30HistoryColumn.PerChange - 1))) ? null : (System.Double?)reader[((int)IndexVn30HistoryColumn.PerChange - 1)];
            entity.AcceptChanges();
        }

        /// <summary>
        /// Refreshes the <see cref="RTStockData.Entities.IndexVn30History"/> object from the <see cref="DataSet"/>.
        /// </summary>
        /// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
        /// <param name="entity">The <see cref="RTStockData.Entities.IndexVn30History"/> object.</param>
        public static void RefreshEntity(DataSet dataSet, RTStockData.Entities.IndexVn30History entity)
        {
            DataRow dataRow = dataSet.Tables[0].Rows[0];

            entity.Id = (System.Int64)dataRow["ID"];
            entity.TradeDate = Convert.IsDBNull(dataRow["TradeDate"]) ? null : (System.DateTime?)dataRow["TradeDate"];
            entity.Index = Convert.IsDBNull(dataRow["Index"]) ? null : (System.Int64?)dataRow["Index"];
            entity.TotalShares = Convert.IsDBNull(dataRow["TotalShares"]) ? null : (System.Int64?)dataRow["TotalShares"];
            entity.TotalValues = Convert.IsDBNull(dataRow["TotalValues"]) ? null : (System.Int64?)dataRow["TotalValues"];
            entity.Up = Convert.IsDBNull(dataRow["Up"]) ? null : (System.Int64?)dataRow["Up"];
            entity.Down = Convert.IsDBNull(dataRow["Down"]) ? null : (System.Int64?)dataRow["Down"];
            entity.NoChange = Convert.IsDBNull(dataRow["NoChange"]) ? null : (System.Int64?)dataRow["NoChange"];
            entity.Time = Convert.IsDBNull(dataRow["Time"]) ? null : (System.Int64?)dataRow["Time"];
            entity.Change = Convert.IsDBNull(dataRow["Change"]) ? null : (System.Int64?)dataRow["Change"];
            entity.PerChange = Convert.IsDBNull(dataRow["PerChange"]) ? null : (System.Double?)dataRow["PerChange"];
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
        /// <param name="entity">The <see cref="RTStockData.Entities.IndexVn30History"/> object to load.</param>
        /// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
        /// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
        /// <param name="childTypes">RTStockData.Entities.IndexVn30History Property Collection Type Array To Include or Exclude from Load</param>
        /// <param name="innerList">A collection of child types for easy access.</param>
        /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
        /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
        public override void DeepLoad(TransactionManager transactionManager, RTStockData.Entities.IndexVn30History entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
        {
            if (entity == null)
                return;

            //used to hold DeepLoad method delegates and fire after all the local children have been loaded.
            Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();

            //Fire all DeepLoad Items
            foreach (KeyValuePair<Delegate, object> pair in deepHandles.Values)
            {
                pair.Key.DynamicInvoke((object[])pair.Value);
            }
            deepHandles = null;
        }

        #endregion

        #region DeepSave Methods

        /// <summary>
        /// Deep Save the entire object graph of the RTStockData.Entities.IndexVn30History object with criteria based of the child 
        /// Type property array and DeepSaveType.
        /// </summary>
        /// <param name="transactionManager">The transaction manager.</param>
        /// <param name="entity">RTStockData.Entities.IndexVn30History instance</param>
        /// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
        /// <param name="childTypes">RTStockData.Entities.IndexVn30History Property Collection Type Array To Include or Exclude from Save</param>
        /// <param name="innerList">A Hashtable of child types for easy access.</param>
        public override bool DeepSave(TransactionManager transactionManager, RTStockData.Entities.IndexVn30History entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
            foreach (KeyValuePair<Delegate, object> pair in deepHandles.Values)
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

    #region IndexVn30HistoryChildEntityTypes

    ///<summary>
    /// Enumeration used to expose the different child entity types 
    /// for child properties in <c>RTStockData.Entities.IndexVn30History</c>
    ///</summary>
    public enum IndexVn30HistoryChildEntityTypes
    {
    }

    #endregion IndexVn30HistoryChildEntityTypes

    #region IndexVn30HistoryFilterBuilder

    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;IndexVn30HistoryColumn&gt;"/> class
    /// that is used exclusively with a <see cref="IndexVn30History"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class IndexVn30HistoryFilterBuilder : SqlFilterBuilder<IndexVn30HistoryColumn>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the IndexVn30HistoryFilterBuilder class.
        /// </summary>
        public IndexVn30HistoryFilterBuilder() : base() { }

        /// <summary>
        /// Initializes a new instance of the IndexVn30HistoryFilterBuilder class.
        /// </summary>
        /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
        public IndexVn30HistoryFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

        /// <summary>
        /// Initializes a new instance of the IndexVn30HistoryFilterBuilder class.
        /// </summary>
        /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
        /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
        public IndexVn30HistoryFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

        #endregion Constructors
    }

    #endregion IndexVn30HistoryFilterBuilder

    #region IndexVn30HistoryParameterBuilder

    /// <summary>
    /// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;IndexVn30HistoryColumn&gt;"/> class
    /// that is used exclusively with a <see cref="IndexVn30History"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class IndexVn30HistoryParameterBuilder : ParameterizedSqlFilterBuilder<IndexVn30HistoryColumn>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the IndexVn30HistoryParameterBuilder class.
        /// </summary>
        public IndexVn30HistoryParameterBuilder() : base() { }

        /// <summary>
        /// Initializes a new instance of the IndexVn30HistoryParameterBuilder class.
        /// </summary>
        /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
        public IndexVn30HistoryParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

        /// <summary>
        /// Initializes a new instance of the IndexVn30HistoryParameterBuilder class.
        /// </summary>
        /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
        /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
        public IndexVn30HistoryParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

        #endregion Constructors
    }

    #endregion IndexVn30HistoryParameterBuilder

    #region IndexVn30HistorySortBuilder

    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;IndexVn30HistoryColumn&gt;"/> class
    /// that is used exclusively with a <see cref="IndexVn30History"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class IndexVn30HistorySortBuilder : SqlSortBuilder<IndexVn30HistoryColumn>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the IndexVn30HistorySqlSortBuilder class.
        /// </summary>
        public IndexVn30HistorySortBuilder() : base() { }

        #endregion Constructors

    }
    #endregion IndexVn30HistorySortBuilder

} // end namespace
