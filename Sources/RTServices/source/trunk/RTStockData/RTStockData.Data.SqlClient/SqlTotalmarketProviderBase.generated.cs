﻿
/*
	File Generated by NetTiers templates [www.nettiers.com]
	Important: Do not modify this file. Edit the file SqlTotalmarketProvider.cs instead.
*/

#region using directives

using System;
using System.Data;
using System.Data.Common;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using System.Collections;
using System.Collections.Specialized;

using System.Diagnostics;
using RTStockData.Entities;
using RTStockData.Data;
using RTStockData.Data.Bases;

#endregion

namespace RTStockData.Data.SqlClient
{
	///<summary>
	/// This class is the SqlClient Data Access Logic Component implementation for the <see cref="Totalmarket"/> entity.
	///</summary>
	public abstract partial class SqlTotalmarketProviderBase : TotalmarketProviderBase
	{
		#region Declarations
		
		string _connectionString;
	    bool _useStoredProcedure;
	    string _providerInvariantName;
			
		#endregion "Declarations"
			
		#region Constructors
		
		/// <summary>
		/// Creates a new <see cref="SqlTotalmarketProviderBase"/> instance.
		/// </summary>
		public SqlTotalmarketProviderBase()
		{
		}
	
	/// <summary>
	/// Creates a new <see cref="SqlTotalmarketProviderBase"/> instance.
	/// Uses connection string to connect to datasource.
	/// </summary>
	/// <param name="connectionString">The connection string to the database.</param>
	/// <param name="useStoredProcedure">A boolean value that indicates if we should use stored procedures or embedded queries.</param>
	/// <param name="providerInvariantName">Name of the invariant provider use by the DbProviderFactory.</param>
	public SqlTotalmarketProviderBase(string connectionString, bool useStoredProcedure, string providerInvariantName)
	{
		this._connectionString = connectionString;
		this._useStoredProcedure = useStoredProcedure;
		this._providerInvariantName = providerInvariantName;
	}
		
	#endregion "Constructors"
	
		#region Public properties
	/// <summary>
    /// Gets or sets the connection string.
    /// </summary>
    /// <value>The connection string.</value>
    public string ConnectionString
	{
		get {return this._connectionString;}
		set {this._connectionString = value;}
	}
	
	/// <summary>
    /// Gets or sets a value indicating whether to use stored procedures.
    /// </summary>
    /// <value><c>true</c> if we choose to use use stored procedures; otherwise, <c>false</c>.</value>
	public bool UseStoredProcedure
	{
		get {return this._useStoredProcedure;}
		set {this._useStoredProcedure = value;}
	}
	
	/// <summary>
    /// Gets or sets the invariant provider name listed in the DbProviderFactories machine.config section.
    /// </summary>
    /// <value>The name of the provider invariant.</value>
    public string ProviderInvariantName
    {
        get { return this._providerInvariantName; }
        set { this._providerInvariantName = value; }
    }
	#endregion
	
		#region Get Many To Many Relationship Functions
		#endregion
	
		#region Delete Functions
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_id">. Primary Key.</param>	
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
        /// <exception cref="System.Exception">The command could not be executed.</exception>
        /// <exception cref="System.Data.DataException">The <paramref name="transactionManager"/> is not open.</exception>
        /// <exception cref="System.Data.Common.DbException">The command could not be executed.</exception>
		public override bool Delete(TransactionManager transactionManager, System.Int64 _id)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			DbCommand commandWrapper = StoredProcedureProvider.GetCommandWrapper(database, "dbo.totalmarket_Delete", _useStoredProcedure);
			database.AddInParameter(commandWrapper, "@Id", DbType.Int64, _id);
			
			//Provider Data Requesting Command Event
			OnDataRequesting(new CommandEventArgs(commandWrapper, "Delete")); 

			int results = 0;
			
			if (transactionManager != null)
			{	
				results = Utility.ExecuteNonQuery(transactionManager, commandWrapper);
			}
			else
			{
				results = Utility.ExecuteNonQuery(database,commandWrapper);
			}
			
			//Stop Tracking Now that it has been updated and persisted.
			if (DataRepository.Provider.EnableEntityTracking)
			{
				string entityKey = EntityLocator.ConstructKeyFromPkItems(typeof(Totalmarket)
					,_id);
				EntityManager.StopTracking(entityKey);
			}
			
			//Provider Data Requested Command Event
			OnDataRequested(new CommandEventArgs(commandWrapper, "Delete")); 

			commandWrapper = null;
			
			return Convert.ToBoolean(results);
		}//end Delete
		#endregion

		#region Find Functions

		#region Parsed Find Methods
		/// <summary>
		/// 	Returns rows meeting the whereClause condition from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="whereClause">Specifies the condition for the rows returned by a query (Name='John Doe', Name='John Doe' AND Id='1', Name='John Doe' OR Id='1').</param>
		/// <param name="start">Row number at which to start reading.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out. The number of rows that match this query.</param>
		/// <remarks>Operators must be capitalized (OR, AND).</remarks>
		/// <returns>Returns a typed collection of RTStockData.Entities.Totalmarket objects.</returns>
		public override TList<Totalmarket> Find(TransactionManager transactionManager, string whereClause, int start, int pageLength, out int count)
		{
			count = -1;
			if (whereClause.IndexOf(";") > -1)
				return new TList<Totalmarket>();
	
			SqlDatabase database = new SqlDatabase(this._connectionString);
			DbCommand commandWrapper = StoredProcedureProvider.GetCommandWrapper(database, "dbo.totalmarket_Find", _useStoredProcedure);

		bool searchUsingOR = false;
		if (whereClause.IndexOf(" OR ") > 0) // did they want to do "a=b OR c=d OR..."?
			searchUsingOR = true;
		
		database.AddInParameter(commandWrapper, "@SearchUsingOR", DbType.Boolean, searchUsingOR);
		
		database.AddInParameter(commandWrapper, "@Id", DbType.Int64, DBNull.Value);
		database.AddInParameter(commandWrapper, "@TradeDate", DbType.DateTime, DBNull.Value);
		database.AddInParameter(commandWrapper, "@SetIndex", DbType.Int64, DBNull.Value);
		database.AddInParameter(commandWrapper, "@TotalTrade", DbType.Int64, DBNull.Value);
		database.AddInParameter(commandWrapper, "@Totalshare", DbType.Int64, DBNull.Value);
		database.AddInParameter(commandWrapper, "@TotalValue", DbType.Int64, DBNull.Value);
		database.AddInParameter(commandWrapper, "@UpVolume", DbType.Int64, DBNull.Value);
		database.AddInParameter(commandWrapper, "@DownVolume", DbType.Int64, DBNull.Value);
		database.AddInParameter(commandWrapper, "@NoChangeVolume", DbType.Int64, DBNull.Value);
		database.AddInParameter(commandWrapper, "@Advances", DbType.Int64, DBNull.Value);
		database.AddInParameter(commandWrapper, "@Declines", DbType.Int64, DBNull.Value);
		database.AddInParameter(commandWrapper, "@Nochange", DbType.Int64, DBNull.Value);
		database.AddInParameter(commandWrapper, "@Marketid", DbType.AnsiString, DBNull.Value);
		database.AddInParameter(commandWrapper, "@Filler", DbType.AnsiString, DBNull.Value);
		database.AddInParameter(commandWrapper, "@Time", DbType.Int64, DBNull.Value);
		database.AddInParameter(commandWrapper, "@Status", DbType.AnsiStringFixedLength, DBNull.Value);
	
			// replace all instances of 'AND' and 'OR' because we already set searchUsingOR
			whereClause = whereClause.Replace(" AND ", "|").Replace(" OR ", "|") ; 
			string[] clauses = whereClause.ToLower().Split('|');
		
			// Here's what's going on below: Find a field, then to get the value we
			// drop the field name from the front, trim spaces, drop the '=' sign,
			// trim more spaces, and drop any outer single quotes.
			// Now handles the case when two fields start off the same way - like "Friendly='Yes' AND Friend='john'"
				
			char[] equalSign = {'='};
			char[] singleQuote = {'\''};
	   		foreach (string clause in clauses)
			{
				if (clause.Trim().StartsWith("id ") || clause.Trim().StartsWith("id="))
				{
					database.SetParameterValue(commandWrapper, "@Id", 
						clause.Trim().Remove(0,2).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("tradedate ") || clause.Trim().StartsWith("tradedate="))
				{
					database.SetParameterValue(commandWrapper, "@TradeDate", 
						clause.Trim().Remove(0,9).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("setindex ") || clause.Trim().StartsWith("setindex="))
				{
					database.SetParameterValue(commandWrapper, "@SetIndex", 
						clause.Trim().Remove(0,8).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("totaltrade ") || clause.Trim().StartsWith("totaltrade="))
				{
					database.SetParameterValue(commandWrapper, "@TotalTrade", 
						clause.Trim().Remove(0,10).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("totalshare ") || clause.Trim().StartsWith("totalshare="))
				{
					database.SetParameterValue(commandWrapper, "@Totalshare", 
						clause.Trim().Remove(0,10).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("totalvalue ") || clause.Trim().StartsWith("totalvalue="))
				{
					database.SetParameterValue(commandWrapper, "@TotalValue", 
						clause.Trim().Remove(0,10).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("upvolume ") || clause.Trim().StartsWith("upvolume="))
				{
					database.SetParameterValue(commandWrapper, "@UpVolume", 
						clause.Trim().Remove(0,8).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("downvolume ") || clause.Trim().StartsWith("downvolume="))
				{
					database.SetParameterValue(commandWrapper, "@DownVolume", 
						clause.Trim().Remove(0,10).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("nochangevolume ") || clause.Trim().StartsWith("nochangevolume="))
				{
					database.SetParameterValue(commandWrapper, "@NoChangeVolume", 
						clause.Trim().Remove(0,14).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("advances ") || clause.Trim().StartsWith("advances="))
				{
					database.SetParameterValue(commandWrapper, "@Advances", 
						clause.Trim().Remove(0,8).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("declines ") || clause.Trim().StartsWith("declines="))
				{
					database.SetParameterValue(commandWrapper, "@Declines", 
						clause.Trim().Remove(0,8).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("nochange ") || clause.Trim().StartsWith("nochange="))
				{
					database.SetParameterValue(commandWrapper, "@Nochange", 
						clause.Trim().Remove(0,8).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("marketid ") || clause.Trim().StartsWith("marketid="))
				{
					database.SetParameterValue(commandWrapper, "@Marketid", 
						clause.Trim().Remove(0,8).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("filler ") || clause.Trim().StartsWith("filler="))
				{
					database.SetParameterValue(commandWrapper, "@Filler", 
						clause.Trim().Remove(0,6).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("time ") || clause.Trim().StartsWith("time="))
				{
					database.SetParameterValue(commandWrapper, "@Time", 
						clause.Trim().Remove(0,4).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("status ") || clause.Trim().StartsWith("status="))
				{
					database.SetParameterValue(commandWrapper, "@Status", 
						clause.Trim().Remove(0,6).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
	
				throw new ArgumentException("Unable to use this part of the where clause in this version of Find: " + clause);
			}
					
			IDataReader reader = null;
			//Create Collection
			TList<Totalmarket> rows = new TList<Totalmarket>();
	
				
			try
			{
				//Provider Data Requesting Command Event
				OnDataRequesting(new CommandEventArgs(commandWrapper, "Find", rows)); 

				if (transactionManager != null)
				{
					reader = Utility.ExecuteReader(transactionManager, commandWrapper);
				}
				else
				{
					reader = Utility.ExecuteReader(database, commandWrapper);
				}		
				
				Fill(reader, rows, start, pageLength);
				
				if(reader.NextResult())
				{
					if(reader.Read())
					{
						count = reader.GetInt32(0);
					}
				}
				
				//Provider Data Requested Command Event
				OnDataRequested(new CommandEventArgs(commandWrapper, "Find", rows)); 
			}
			finally
			{
				if (reader != null) 
					reader.Close();	
					
				commandWrapper = null;
			}
			return rows;
		}

		#endregion Parsed Find Methods
		
		#region Parameterized Find Methods
		
		/// <summary>
		/// 	Returns rows from the DataSource that meet the parameter conditions.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="parameters">A collection of <see cref="SqlFilterParameter"/> objects.</param>
		/// <param name="orderBy">Specifies the sort criteria for the rows in the DataSource (Name ASC; BirthDay DESC, Name ASC);</param>
		/// <param name="start">Row number at which to start reading.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out. The number of rows that match this query.</param>
		/// <returns>Returns a typed collection of RTStockData.Entities.Totalmarket objects.</returns>
		public override TList<Totalmarket> Find(TransactionManager transactionManager, IFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
		{
			SqlFilterParameterCollection filter = null;
			
			if (parameters == null)
				filter = new SqlFilterParameterCollection();
			else 
				filter = parameters.GetParameters();
				
			SqlDatabase database = new SqlDatabase(this._connectionString);
			DbCommand commandWrapper = StoredProcedureProvider.GetCommandWrapper(database, "dbo.totalmarket_Find_Dynamic", typeof(TotalmarketColumn), filter, orderBy, start, pageLength);
		
			SqlFilterParameter param;

			for ( int i = 0; i < filter.Count; i++ )
			{
				param = filter[i];
				database.AddInParameter(commandWrapper, param.Name, param.DbType, param.GetValue());
			}

			TList<Totalmarket> rows = new TList<Totalmarket>();
			IDataReader reader = null;
			
			try
			{
				//Provider Data Requesting Command Event
				OnDataRequesting(new CommandEventArgs(commandWrapper, "Find", rows)); 

				if ( transactionManager != null )
				{
					reader = Utility.ExecuteReader(transactionManager, commandWrapper);
				}
				else
				{
					reader = Utility.ExecuteReader(database, commandWrapper);
				}
				
				Fill(reader, rows, 0, int.MaxValue);
				count = rows.Count;
				
				if ( reader.NextResult() )
				{
					if ( reader.Read() )
					{
						count = reader.GetInt32(0);
					}
				}
				
				//Provider Data Requested Command Event
				OnDataRequested(new CommandEventArgs(commandWrapper, "Find", rows)); 
			}
			finally
			{
				if ( reader != null )
					reader.Close();
					
				commandWrapper = null;
			}
			
			return rows;
		}
		
		#endregion Parameterized Find Methods
		
		#endregion Find Functions
	
		#region GetAll Methods
				
		/// <summary>
		/// 	Gets All rows from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="start">Row number at which to start reading.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out. The number of rows that match this query.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of RTStockData.Entities.Totalmarket objects.</returns>
        /// <exception cref="System.Exception">The command could not be executed.</exception>
        /// <exception cref="System.Data.DataException">The <paramref name="transactionManager"/> is not open.</exception>
        /// <exception cref="System.Data.Common.DbException">The command could not be executed.</exception>
		public override TList<Totalmarket> GetAll(TransactionManager transactionManager, int start, int pageLength, out int count)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			DbCommand commandWrapper = StoredProcedureProvider.GetCommandWrapper(database, "dbo.totalmarket_Get_List", _useStoredProcedure);
			
			IDataReader reader = null;
		
			//Create Collection
			TList<Totalmarket> rows = new TList<Totalmarket>();
			
			try
			{
				//Provider Data Requesting Command Event
				OnDataRequesting(new CommandEventArgs(commandWrapper, "GetAll", rows)); 
					
				if (transactionManager != null)
				{
					reader = Utility.ExecuteReader(transactionManager, commandWrapper);
				}
				else
				{
					reader = Utility.ExecuteReader(database, commandWrapper);
				}		
		
				Fill(reader, rows, start, pageLength);
				count = -1;
				if(reader.NextResult())
				{
					if(reader.Read())
					{
						count = reader.GetInt32(0);
					}
				}
				
				//Provider Data Requested Command Event
				OnDataRequested(new CommandEventArgs(commandWrapper, "GetAll", rows)); 
			}
			finally 
			{
				if (reader != null) 
					reader.Close();
					
				commandWrapper = null;	
			}
			return rows;
		}//end getall
		
		#endregion
				
		#region GetPaged Methods
				
		/// <summary>
		/// Gets a page of rows from the DataSource.
		/// </summary>
		/// <param name="start">Row number at which to start reading.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">Number of rows in the DataSource.</param>
		/// <param name="whereClause">Specifies the condition for the rows returned by a query (Name='John Doe', Name='John Doe' AND Id='1', Name='John Doe' OR Id='1').</param>
		/// <param name="orderBy">Specifies the sort criteria for the rows in the DataSource (Name ASC; BirthDay DESC, Name ASC);</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of RTStockData.Entities.Totalmarket objects.</returns>
		public override TList<Totalmarket> GetPaged(TransactionManager transactionManager, string whereClause, string orderBy, int start, int pageLength, out int count)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			DbCommand commandWrapper = StoredProcedureProvider.GetCommandWrapper(database, "dbo.totalmarket_GetPaged", _useStoredProcedure);
		
			
            if (commandWrapper.CommandType == CommandType.Text
                && commandWrapper.CommandText != null)
            {
                commandWrapper.CommandText = commandWrapper.CommandText.Replace(SqlUtil.PAGE_INDEX, string.Concat(SqlUtil.PAGE_INDEX, Guid.NewGuid().ToString("N").Substring(0, 8)));
            }
			
			database.AddInParameter(commandWrapper, "@WhereClause", DbType.String, whereClause);
			database.AddInParameter(commandWrapper, "@OrderBy", DbType.String, orderBy);
			database.AddInParameter(commandWrapper, "@PageIndex", DbType.Int32, start);
			database.AddInParameter(commandWrapper, "@PageSize", DbType.Int32, pageLength);
		
			IDataReader reader = null;
			//Create Collection
			TList<Totalmarket> rows = new TList<Totalmarket>();
			
			try
			{
				//Provider Data Requesting Command Event
				OnDataRequesting(new CommandEventArgs(commandWrapper, "GetPaged", rows)); 

				if (transactionManager != null)
				{
					reader = Utility.ExecuteReader(transactionManager, commandWrapper);
				}
				else
				{
					reader = Utility.ExecuteReader(database, commandWrapper);
				}
				
				Fill(reader, rows, 0, int.MaxValue);
				count = rows.Count;

				if(reader.NextResult())
				{
					if(reader.Read())
					{
						count = reader.GetInt32(0);
					}
				}
				
				//Provider Data Requested Command Event
				OnDataRequested(new CommandEventArgs(commandWrapper, "GetPaged", rows)); 

			}
			catch(Exception)
			{			
				throw;
			}
			finally
			{
				if (reader != null) 
					reader.Close();
				
				commandWrapper = null;
			}
			
			return rows;
		}
		
		#endregion	
		
		#region Get By Foreign Key Functions
	#endregion
	
		#region Get By Index Functions

		#region GetById
					
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_totalmarket index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query.</param>
		/// <returns>Returns an instance of the <see cref="RTStockData.Entities.Totalmarket"/> class.</returns>
		/// <remarks></remarks>
        /// <exception cref="System.Exception">The command could not be executed.</exception>
        /// <exception cref="System.Data.DataException">The <paramref name="transactionManager"/> is not open.</exception>
        /// <exception cref="System.Data.Common.DbException">The command could not be executed.</exception>
		public override RTStockData.Entities.Totalmarket GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength, out int count)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			DbCommand commandWrapper = StoredProcedureProvider.GetCommandWrapper(database, "dbo.totalmarket_GetById", _useStoredProcedure);
			
				database.AddInParameter(commandWrapper, "@Id", DbType.Int64, _id);
			
			IDataReader reader = null;
			TList<Totalmarket> tmp = new TList<Totalmarket>();
			try
			{
				//Provider Data Requesting Command Event
				OnDataRequesting(new CommandEventArgs(commandWrapper, "GetById", tmp)); 

				if (transactionManager != null)
				{
					reader = Utility.ExecuteReader(transactionManager, commandWrapper);
				}
				else
				{
					reader = Utility.ExecuteReader(database, commandWrapper);
				}		
		
				//Create collection and fill
				Fill(reader, tmp, start, pageLength);
				count = -1;
				if(reader.NextResult())
				{
					if(reader.Read())
					{
						count = reader.GetInt32(0);
					}
				}
				
				//Provider Data Requested Command Event
				OnDataRequested(new CommandEventArgs(commandWrapper, "GetById", tmp));
			}
			finally 
			{
				if (reader != null) 
					reader.Close();
					
				commandWrapper = null;
			}
			
			if (tmp.Count == 1)
			{
				return tmp[0];
			}
			else if (tmp.Count == 0)
			{
				return null;
			}
			else
			{
				throw new DataException("Cannot find the unique instance of the class.");
			}
			
			//return rows;
		}
		
		#endregion

	#endregion Get By Index Functions

		#region Insert Methods
		/// <summary>
		/// Lets you efficiently bulk insert many entities to the database.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entities">The entities.</param>
		/// <remarks>
		///		After inserting into the datasource, the RTStockData.Entities.Totalmarket object will be updated
		/// 	to refelect any changes made by the datasource. (ie: identity or computed columns)
		/// </remarks>	
		public override void BulkInsert(TransactionManager transactionManager, TList<RTStockData.Entities.Totalmarket> entities)
		{
			//System.Data.SqlClient.SqlBulkCopy bulkCopy = new System.Data.SqlClient.SqlBulkCopy(this._connectionString, System.Data.SqlClient.SqlBulkCopyOptions.CheckConstraints); //, null);
			
			System.Data.SqlClient.SqlBulkCopy bulkCopy = null;
	
			if (transactionManager != null && transactionManager.IsOpen)
			{			
				System.Data.SqlClient.SqlConnection cnx = transactionManager.TransactionObject.Connection as System.Data.SqlClient.SqlConnection;
				System.Data.SqlClient.SqlTransaction transaction = transactionManager.TransactionObject as System.Data.SqlClient.SqlTransaction;
				bulkCopy = new System.Data.SqlClient.SqlBulkCopy(cnx, System.Data.SqlClient.SqlBulkCopyOptions.CheckConstraints, transaction); //, null);
			}
			else
			{
				bulkCopy = new System.Data.SqlClient.SqlBulkCopy(this._connectionString, System.Data.SqlClient.SqlBulkCopyOptions.CheckConstraints); //, null);
			}
			
			bulkCopy.BulkCopyTimeout = 360;
			bulkCopy.DestinationTableName = "totalmarket";
			
			DataTable dataTable = new DataTable();
			DataColumn col0 = dataTable.Columns.Add("id", typeof(System.Int64));
			col0.AllowDBNull = false;		
			DataColumn col1 = dataTable.Columns.Add("TradeDate", typeof(System.DateTime));
			col1.AllowDBNull = true;		
			DataColumn col2 = dataTable.Columns.Add("SetIndex", typeof(System.Int64));
			col2.AllowDBNull = true;		
			DataColumn col3 = dataTable.Columns.Add("TotalTrade", typeof(System.Int64));
			col3.AllowDBNull = true;		
			DataColumn col4 = dataTable.Columns.Add("Totalshare", typeof(System.Int64));
			col4.AllowDBNull = true;		
			DataColumn col5 = dataTable.Columns.Add("TotalValue", typeof(System.Int64));
			col5.AllowDBNull = true;		
			DataColumn col6 = dataTable.Columns.Add("UpVolume", typeof(System.Int64));
			col6.AllowDBNull = true;		
			DataColumn col7 = dataTable.Columns.Add("DownVolume", typeof(System.Int64));
			col7.AllowDBNull = true;		
			DataColumn col8 = dataTable.Columns.Add("NoChangeVolume", typeof(System.Int64));
			col8.AllowDBNull = true;		
			DataColumn col9 = dataTable.Columns.Add("Advances", typeof(System.Int64));
			col9.AllowDBNull = true;		
			DataColumn col10 = dataTable.Columns.Add("Declines", typeof(System.Int64));
			col10.AllowDBNull = true;		
			DataColumn col11 = dataTable.Columns.Add("Nochange", typeof(System.Int64));
			col11.AllowDBNull = true;		
			DataColumn col12 = dataTable.Columns.Add("Marketid", typeof(System.String));
			col12.AllowDBNull = true;		
			DataColumn col13 = dataTable.Columns.Add("Filler", typeof(System.String));
			col13.AllowDBNull = true;		
			DataColumn col14 = dataTable.Columns.Add("Time", typeof(System.Int64));
			col14.AllowDBNull = true;		
			DataColumn col15 = dataTable.Columns.Add("Status", typeof(System.String));
			col15.AllowDBNull = true;		
			
			bulkCopy.ColumnMappings.Add("id", "id");
			bulkCopy.ColumnMappings.Add("TradeDate", "TradeDate");
			bulkCopy.ColumnMappings.Add("SetIndex", "SetIndex");
			bulkCopy.ColumnMappings.Add("TotalTrade", "TotalTrade");
			bulkCopy.ColumnMappings.Add("Totalshare", "Totalshare");
			bulkCopy.ColumnMappings.Add("TotalValue", "TotalValue");
			bulkCopy.ColumnMappings.Add("UpVolume", "UpVolume");
			bulkCopy.ColumnMappings.Add("DownVolume", "DownVolume");
			bulkCopy.ColumnMappings.Add("NoChangeVolume", "NoChangeVolume");
			bulkCopy.ColumnMappings.Add("Advances", "Advances");
			bulkCopy.ColumnMappings.Add("Declines", "Declines");
			bulkCopy.ColumnMappings.Add("Nochange", "Nochange");
			bulkCopy.ColumnMappings.Add("Marketid", "Marketid");
			bulkCopy.ColumnMappings.Add("Filler", "Filler");
			bulkCopy.ColumnMappings.Add("Time", "Time");
			bulkCopy.ColumnMappings.Add("Status", "Status");
			
			foreach(RTStockData.Entities.Totalmarket entity in entities)
			{
				if (entity.EntityState != EntityState.Added)
					continue;
					
				DataRow row = dataTable.NewRow();
				
					row["id"] = entity.Id;
							
				
					row["TradeDate"] = entity.TradeDate.HasValue ? (object) entity.TradeDate  : System.DBNull.Value;
							
				
					row["SetIndex"] = entity.SetIndex.HasValue ? (object) entity.SetIndex  : System.DBNull.Value;
							
				
					row["TotalTrade"] = entity.TotalTrade.HasValue ? (object) entity.TotalTrade  : System.DBNull.Value;
							
				
					row["Totalshare"] = entity.Totalshare.HasValue ? (object) entity.Totalshare  : System.DBNull.Value;
							
				
					row["TotalValue"] = entity.TotalValue.HasValue ? (object) entity.TotalValue  : System.DBNull.Value;
							
				
					row["UpVolume"] = entity.UpVolume.HasValue ? (object) entity.UpVolume  : System.DBNull.Value;
							
				
					row["DownVolume"] = entity.DownVolume.HasValue ? (object) entity.DownVolume  : System.DBNull.Value;
							
				
					row["NoChangeVolume"] = entity.NoChangeVolume.HasValue ? (object) entity.NoChangeVolume  : System.DBNull.Value;
							
				
					row["Advances"] = entity.Advances.HasValue ? (object) entity.Advances  : System.DBNull.Value;
							
				
					row["Declines"] = entity.Declines.HasValue ? (object) entity.Declines  : System.DBNull.Value;
							
				
					row["Nochange"] = entity.Nochange.HasValue ? (object) entity.Nochange  : System.DBNull.Value;
							
				
					row["Marketid"] = entity.Marketid;
							
				
					row["Filler"] = entity.Filler;
							
				
					row["Time"] = entity.Time.HasValue ? (object) entity.Time  : System.DBNull.Value;
							
				
					row["Status"] = entity.Status;
							
				
				dataTable.Rows.Add(row);
			}		
			
			// send the data to the server		
			bulkCopy.WriteToServer(dataTable);		
			
			// update back the state
			foreach(RTStockData.Entities.Totalmarket entity in entities)
			{
				if (entity.EntityState != EntityState.Added)
					continue;
			
				entity.AcceptChanges();
			}
		}
				
		/// <summary>
		/// 	Inserts a RTStockData.Entities.Totalmarket object into the datasource using a transaction.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="entity">RTStockData.Entities.Totalmarket object to insert.</param>
		/// <remarks>
		///		After inserting into the datasource, the RTStockData.Entities.Totalmarket object will be updated
		/// 	to refelect any changes made by the datasource. (ie: identity or computed columns)
		/// </remarks>	
		/// <returns>Returns true if operation is successful.</returns>
        /// <exception cref="System.Exception">The command could not be executed.</exception>
        /// <exception cref="System.Data.DataException">The <paramref name="transactionManager"/> is not open.</exception>
        /// <exception cref="System.Data.Common.DbException">The command could not be executed.</exception>
		public override bool Insert(TransactionManager transactionManager, RTStockData.Entities.Totalmarket entity)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			DbCommand commandWrapper = StoredProcedureProvider.GetCommandWrapper(database, "dbo.totalmarket_Insert", _useStoredProcedure);
			
			database.AddOutParameter(commandWrapper, "@Id", DbType.Int64, 8);
			database.AddInParameter(commandWrapper, "@TradeDate", DbType.DateTime, (entity.TradeDate.HasValue ? (object) entity.TradeDate  : System.DBNull.Value));
			database.AddInParameter(commandWrapper, "@SetIndex", DbType.Int64, (entity.SetIndex.HasValue ? (object) entity.SetIndex  : System.DBNull.Value));
			database.AddInParameter(commandWrapper, "@TotalTrade", DbType.Int64, (entity.TotalTrade.HasValue ? (object) entity.TotalTrade  : System.DBNull.Value));
			database.AddInParameter(commandWrapper, "@Totalshare", DbType.Int64, (entity.Totalshare.HasValue ? (object) entity.Totalshare  : System.DBNull.Value));
			database.AddInParameter(commandWrapper, "@TotalValue", DbType.Int64, (entity.TotalValue.HasValue ? (object) entity.TotalValue  : System.DBNull.Value));
			database.AddInParameter(commandWrapper, "@UpVolume", DbType.Int64, (entity.UpVolume.HasValue ? (object) entity.UpVolume  : System.DBNull.Value));
			database.AddInParameter(commandWrapper, "@DownVolume", DbType.Int64, (entity.DownVolume.HasValue ? (object) entity.DownVolume  : System.DBNull.Value));
			database.AddInParameter(commandWrapper, "@NoChangeVolume", DbType.Int64, (entity.NoChangeVolume.HasValue ? (object) entity.NoChangeVolume  : System.DBNull.Value));
			database.AddInParameter(commandWrapper, "@Advances", DbType.Int64, (entity.Advances.HasValue ? (object) entity.Advances  : System.DBNull.Value));
			database.AddInParameter(commandWrapper, "@Declines", DbType.Int64, (entity.Declines.HasValue ? (object) entity.Declines  : System.DBNull.Value));
			database.AddInParameter(commandWrapper, "@Nochange", DbType.Int64, (entity.Nochange.HasValue ? (object) entity.Nochange  : System.DBNull.Value));
			database.AddInParameter(commandWrapper, "@Marketid", DbType.AnsiString, entity.Marketid );
			database.AddInParameter(commandWrapper, "@Filler", DbType.AnsiString, entity.Filler );
			database.AddInParameter(commandWrapper, "@Time", DbType.Int64, (entity.Time.HasValue ? (object) entity.Time  : System.DBNull.Value));
			database.AddInParameter(commandWrapper, "@Status", DbType.AnsiStringFixedLength, entity.Status );
			
			int results = 0;
			
			//Provider Data Requesting Command Event
			OnDataRequesting(new CommandEventArgs(commandWrapper, "Insert", entity));
				
			if (transactionManager != null)
			{
				results = Utility.ExecuteNonQuery(transactionManager, commandWrapper);
			}
			else
			{
				results = Utility.ExecuteNonQuery(database,commandWrapper);
			}
					
			object _id = database.GetParameterValue(commandWrapper, "@Id");
			entity.Id = (System.Int64)_id;
			
			
			entity.AcceptChanges();
	
			//Provider Data Requested Command Event
			OnDataRequested(new CommandEventArgs(commandWrapper, "Insert", entity));

			return Convert.ToBoolean(results);
		}	
		#endregion

		#region Update Methods
				
		/// <summary>
		/// 	Update an existing row in the datasource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="entity">RTStockData.Entities.Totalmarket object to update.</param>
		/// <remarks>
		///		After updating the datasource, the RTStockData.Entities.Totalmarket object will be updated
		/// 	to refelect any changes made by the datasource. (ie: identity or computed columns)
		/// </remarks>
		/// <returns>Returns true if operation is successful.</returns>
        /// <exception cref="System.Exception">The command could not be executed.</exception>
        /// <exception cref="System.Data.DataException">The <paramref name="transactionManager"/> is not open.</exception>
        /// <exception cref="System.Data.Common.DbException">The command could not be executed.</exception>
		public override bool Update(TransactionManager transactionManager, RTStockData.Entities.Totalmarket entity)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			DbCommand commandWrapper = StoredProcedureProvider.GetCommandWrapper(database, "dbo.totalmarket_Update", _useStoredProcedure);
			
			database.AddInParameter(commandWrapper, "@Id", DbType.Int64, entity.Id );
			database.AddInParameter(commandWrapper, "@TradeDate", DbType.DateTime, (entity.TradeDate.HasValue ? (object) entity.TradeDate : System.DBNull.Value) );
			database.AddInParameter(commandWrapper, "@SetIndex", DbType.Int64, (entity.SetIndex.HasValue ? (object) entity.SetIndex : System.DBNull.Value) );
			database.AddInParameter(commandWrapper, "@TotalTrade", DbType.Int64, (entity.TotalTrade.HasValue ? (object) entity.TotalTrade : System.DBNull.Value) );
			database.AddInParameter(commandWrapper, "@Totalshare", DbType.Int64, (entity.Totalshare.HasValue ? (object) entity.Totalshare : System.DBNull.Value) );
			database.AddInParameter(commandWrapper, "@TotalValue", DbType.Int64, (entity.TotalValue.HasValue ? (object) entity.TotalValue : System.DBNull.Value) );
			database.AddInParameter(commandWrapper, "@UpVolume", DbType.Int64, (entity.UpVolume.HasValue ? (object) entity.UpVolume : System.DBNull.Value) );
			database.AddInParameter(commandWrapper, "@DownVolume", DbType.Int64, (entity.DownVolume.HasValue ? (object) entity.DownVolume : System.DBNull.Value) );
			database.AddInParameter(commandWrapper, "@NoChangeVolume", DbType.Int64, (entity.NoChangeVolume.HasValue ? (object) entity.NoChangeVolume : System.DBNull.Value) );
			database.AddInParameter(commandWrapper, "@Advances", DbType.Int64, (entity.Advances.HasValue ? (object) entity.Advances : System.DBNull.Value) );
			database.AddInParameter(commandWrapper, "@Declines", DbType.Int64, (entity.Declines.HasValue ? (object) entity.Declines : System.DBNull.Value) );
			database.AddInParameter(commandWrapper, "@Nochange", DbType.Int64, (entity.Nochange.HasValue ? (object) entity.Nochange : System.DBNull.Value) );
			database.AddInParameter(commandWrapper, "@Marketid", DbType.AnsiString, entity.Marketid );
			database.AddInParameter(commandWrapper, "@Filler", DbType.AnsiString, entity.Filler );
			database.AddInParameter(commandWrapper, "@Time", DbType.Int64, (entity.Time.HasValue ? (object) entity.Time : System.DBNull.Value) );
			database.AddInParameter(commandWrapper, "@Status", DbType.AnsiStringFixedLength, entity.Status );
			
			int results = 0;
			
			//Provider Data Requesting Command Event
			OnDataRequesting(new CommandEventArgs(commandWrapper, "Update", entity));

			if (transactionManager != null)
			{
				results = Utility.ExecuteNonQuery(transactionManager, commandWrapper);
			}
			else
			{
				results = Utility.ExecuteNonQuery(database,commandWrapper);
			}
			
			//Stop Tracking Now that it has been updated and persisted.
			if (DataRepository.Provider.EnableEntityTracking)
				EntityManager.StopTracking(entity.EntityTrackingKey);
			
			
			entity.AcceptChanges();
			
			//Provider Data Requested Command Event
			OnDataRequested(new CommandEventArgs(commandWrapper, "Update", entity));

			return Convert.ToBoolean(results);
		}
			
		#endregion
		
		#region Custom Methods
	
		#endregion
	}//end class
} // end namespace
