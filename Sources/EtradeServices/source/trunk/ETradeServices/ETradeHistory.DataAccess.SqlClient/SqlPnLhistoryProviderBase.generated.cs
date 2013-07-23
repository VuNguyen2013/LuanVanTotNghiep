﻿
/*
	File Generated by NetTiers templates [www.nettiers.com]
	Generated on : Friday, November 12, 2010
	Important: Do not modify this file. Edit the file SqlPnLhistoryProvider.cs instead.
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
using ETradeHistory.Entities;
using ETradeHistory.DataAccess;
using ETradeHistory.DataAccess.Bases;

#endregion

namespace ETradeHistory.DataAccess.SqlClient
{
	///<summary>
	/// This class is the SqlClient Data Access Logic Component implementation for the <see cref="PnLhistory"/> entity.
	///</summary>
	public abstract partial class SqlPnLhistoryProviderBase : PnLhistoryProviderBase
	{
		#region Declarations
		
		string _connectionString;
	    bool _useStoredProcedure;
	    string _providerInvariantName;
			
		#endregion "Declarations"
			
		#region Constructors
		
		/// <summary>
		/// Creates a new <see cref="SqlPnLhistoryProviderBase"/> instance.
		/// </summary>
		public SqlPnLhistoryProviderBase()
		{
		}
	
	/// <summary>
	/// Creates a new <see cref="SqlPnLhistoryProviderBase"/> instance.
	/// Uses connection string to connect to datasource.
	/// </summary>
	/// <param name="connectionString">The connection string to the database.</param>
	/// <param name="useStoredProcedure">A boolean value that indicates if we should use stored procedures or embedded queries.</param>
	/// <param name="providerInvariantName">Name of the invariant provider use by the DbProviderFactory.</param>
	public SqlPnLhistoryProviderBase(string connectionString, bool useStoredProcedure, string providerInvariantName)
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
		/// <param name="_id">ID identifies PnLHistory. Primary Key.</param>	
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
        /// <exception cref="System.Exception">The command could not be executed.</exception>
        /// <exception cref="System.Data.DataException">The <paramref name="transactionManager"/> is not open.</exception>
        /// <exception cref="System.Data.Common.DbException">The command could not be executed.</exception>
		public override bool Delete(TransactionManager transactionManager, long _id)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			DbCommand commandWrapper = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PnLHistory_Delete", _useStoredProcedure);
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
				string entityKey = EntityLocator.ConstructKeyFromPkItems(typeof(PnLhistory)
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
		/// <returns>Returns a typed collection of ETradeHistory.Entities.PnLhistory objects.</returns>
		public override TList<PnLhistory> Find(TransactionManager transactionManager, string whereClause, int start, int pageLength, out int count)
		{
			count = -1;
			if (whereClause.IndexOf(";") > -1)
				return new TList<PnLhistory>();
	
			SqlDatabase database = new SqlDatabase(this._connectionString);
			DbCommand commandWrapper = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PnLHistory_Find", _useStoredProcedure);

		bool searchUsingOR = false;
		if (whereClause.IndexOf(" OR ") > 0) // did they want to do "a=b OR c=d OR..."?
			searchUsingOR = true;
		
		database.AddInParameter(commandWrapper, "@SearchUsingOR", DbType.Boolean, searchUsingOR);
		
		database.AddInParameter(commandWrapper, "@Id", DbType.Int64, DBNull.Value);
		database.AddInParameter(commandWrapper, "@TradeTime", DbType.DateTime, DBNull.Value);
		database.AddInParameter(commandWrapper, "@RefOrderId", DbType.AnsiString, DBNull.Value);
		database.AddInParameter(commandWrapper, "@FisOrderId", DbType.Int32, DBNull.Value);
		database.AddInParameter(commandWrapper, "@SecSymbol", DbType.AnsiString, DBNull.Value);
		database.AddInParameter(commandWrapper, "@Price", DbType.Decimal, DBNull.Value);
		database.AddInParameter(commandWrapper, "@AvgPrice", DbType.Decimal, DBNull.Value);
		database.AddInParameter(commandWrapper, "@Volume", DbType.Int32, DBNull.Value);
		database.AddInParameter(commandWrapper, "@Profit", DbType.Decimal, DBNull.Value);
		database.AddInParameter(commandWrapper, "@ProfitabilityRatio", DbType.Decimal, DBNull.Value);
		database.AddInParameter(commandWrapper, "@SubCustAccountId", DbType.AnsiString, DBNull.Value);
		database.AddInParameter(commandWrapper, "@Market", DbType.AnsiString, DBNull.Value);
	
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
				if (clause.Trim().StartsWith("tradetime ") || clause.Trim().StartsWith("tradetime="))
				{
					database.SetParameterValue(commandWrapper, "@TradeTime", 
						clause.Trim().Remove(0,9).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("reforderid ") || clause.Trim().StartsWith("reforderid="))
				{
					database.SetParameterValue(commandWrapper, "@RefOrderId", 
						clause.Trim().Remove(0,10).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("fisorderid ") || clause.Trim().StartsWith("fisorderid="))
				{
					database.SetParameterValue(commandWrapper, "@FisOrderId", 
						clause.Trim().Remove(0,10).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("secsymbol ") || clause.Trim().StartsWith("secsymbol="))
				{
					database.SetParameterValue(commandWrapper, "@SecSymbol", 
						clause.Trim().Remove(0,9).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("price ") || clause.Trim().StartsWith("price="))
				{
					database.SetParameterValue(commandWrapper, "@Price", 
						clause.Trim().Remove(0,5).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("avgprice ") || clause.Trim().StartsWith("avgprice="))
				{
					database.SetParameterValue(commandWrapper, "@AvgPrice", 
						clause.Trim().Remove(0,8).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("volume ") || clause.Trim().StartsWith("volume="))
				{
					database.SetParameterValue(commandWrapper, "@Volume", 
						clause.Trim().Remove(0,6).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("profit ") || clause.Trim().StartsWith("profit="))
				{
					database.SetParameterValue(commandWrapper, "@Profit", 
						clause.Trim().Remove(0,6).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("profitabilityratio ") || clause.Trim().StartsWith("profitabilityratio="))
				{
					database.SetParameterValue(commandWrapper, "@ProfitabilityRatio", 
						clause.Trim().Remove(0,18).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("subcustaccountid ") || clause.Trim().StartsWith("subcustaccountid="))
				{
					database.SetParameterValue(commandWrapper, "@SubCustAccountId", 
						clause.Trim().Remove(0,16).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("market ") || clause.Trim().StartsWith("market="))
				{
					database.SetParameterValue(commandWrapper, "@Market", 
						clause.Trim().Remove(0,6).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
	
				throw new ArgumentException("Unable to use this part of the where clause in this version of Find: " + clause);
			}
					
			IDataReader reader = null;
			//Create Collection
			TList<PnLhistory> rows = new TList<PnLhistory>();
	
				
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
		/// <returns>Returns a typed collection of ETradeHistory.Entities.PnLhistory objects.</returns>
		public override TList<PnLhistory> Find(TransactionManager transactionManager, IFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
		{
			SqlFilterParameterCollection filter = null;
			
			if (parameters == null)
				filter = new SqlFilterParameterCollection();
			else 
				filter = parameters.GetParameters();
				
			SqlDatabase database = new SqlDatabase(this._connectionString);
			DbCommand commandWrapper = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PnLHistory_Find_Dynamic", typeof(PnLhistoryColumn), filter, orderBy, start, pageLength);
		
			SqlFilterParameter param;

			for ( int i = 0; i < filter.Count; i++ )
			{
				param = filter[i];
				database.AddInParameter(commandWrapper, param.Name, param.DbType, param.GetValue());
			}

			TList<PnLhistory> rows = new TList<PnLhistory>();
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
		/// <returns>Returns a typed collection of ETradeHistory.Entities.PnLhistory objects.</returns>
        /// <exception cref="System.Exception">The command could not be executed.</exception>
        /// <exception cref="System.Data.DataException">The <paramref name="transactionManager"/> is not open.</exception>
        /// <exception cref="System.Data.Common.DbException">The command could not be executed.</exception>
		public override TList<PnLhistory> GetAll(TransactionManager transactionManager, int start, int pageLength, out int count)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			DbCommand commandWrapper = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PnLHistory_Get_List", _useStoredProcedure);
			
			IDataReader reader = null;
		
			//Create Collection
			TList<PnLhistory> rows = new TList<PnLhistory>();
			
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
		/// <returns>Returns a typed collection of ETradeHistory.Entities.PnLhistory objects.</returns>
		public override TList<PnLhistory> GetPaged(TransactionManager transactionManager, string whereClause, string orderBy, int start, int pageLength, out int count)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			DbCommand commandWrapper = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PnLHistory_GetPaged", _useStoredProcedure);
		
			
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
			TList<PnLhistory> rows = new TList<PnLhistory>();
			
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
		/// 	Gets rows from the datasource based on the PK_PnLHistory index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">ID identifies PnLHistory</param>
		/// <param name="start">Row number at which to start reading.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query.</param>
		/// <returns>Returns an instance of the <see cref="ETradeHistory.Entities.PnLhistory"/> class.</returns>
		/// <remarks></remarks>
        /// <exception cref="System.Exception">The command could not be executed.</exception>
        /// <exception cref="System.Data.DataException">The <paramref name="transactionManager"/> is not open.</exception>
        /// <exception cref="System.Data.Common.DbException">The command could not be executed.</exception>
		public override ETradeHistory.Entities.PnLhistory GetById(TransactionManager transactionManager, long _id, int start, int pageLength, out int count)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			DbCommand commandWrapper = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PnLHistory_GetById", _useStoredProcedure);
			
				database.AddInParameter(commandWrapper, "@Id", DbType.Int64, _id);
			
			IDataReader reader = null;
			TList<PnLhistory> tmp = new TList<PnLhistory>();
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
		///		After inserting into the datasource, the ETradeHistory.Entities.PnLhistory object will be updated
		/// 	to refelect any changes made by the datasource. (ie: identity or computed columns)
		/// </remarks>	
		public override void BulkInsert(TransactionManager transactionManager, TList<ETradeHistory.Entities.PnLhistory> entities)
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
			bulkCopy.DestinationTableName = "PnLHistory";
			
			DataTable dataTable = new DataTable();
			DataColumn col0 = dataTable.Columns.Add("ID", typeof(long));
			col0.AllowDBNull = false;		
			DataColumn col1 = dataTable.Columns.Add("TradeTime", typeof(System.DateTime));
			col1.AllowDBNull = false;		
			DataColumn col2 = dataTable.Columns.Add("RefOrderID", typeof(string));
			col2.AllowDBNull = true;		
			DataColumn col3 = dataTable.Columns.Add("FISOrderID", typeof(System.Int32?));
			col3.AllowDBNull = true;		
			DataColumn col4 = dataTable.Columns.Add("SecSymbol", typeof(string));
			col4.AllowDBNull = false;		
			DataColumn col5 = dataTable.Columns.Add("Price", typeof(decimal));
			col5.AllowDBNull = false;		
			DataColumn col6 = dataTable.Columns.Add("AvgPrice", typeof(System.Decimal?));
			col6.AllowDBNull = true;		
			DataColumn col7 = dataTable.Columns.Add("Volume", typeof(int));
			col7.AllowDBNull = false;		
			DataColumn col8 = dataTable.Columns.Add("Profit", typeof(System.Decimal?));
			col8.AllowDBNull = true;		
			DataColumn col9 = dataTable.Columns.Add("ProfitabilityRatio", typeof(System.Decimal?));
			col9.AllowDBNull = true;		
			DataColumn col10 = dataTable.Columns.Add("SubCustAccountID", typeof(string));
			col10.AllowDBNull = false;		
			DataColumn col11 = dataTable.Columns.Add("Market", typeof(string));
			col11.AllowDBNull = false;		
			
			bulkCopy.ColumnMappings.Add("ID", "ID");
			bulkCopy.ColumnMappings.Add("TradeTime", "TradeTime");
			bulkCopy.ColumnMappings.Add("RefOrderID", "RefOrderID");
			bulkCopy.ColumnMappings.Add("FISOrderID", "FISOrderID");
			bulkCopy.ColumnMappings.Add("SecSymbol", "SecSymbol");
			bulkCopy.ColumnMappings.Add("Price", "Price");
			bulkCopy.ColumnMappings.Add("AvgPrice", "AvgPrice");
			bulkCopy.ColumnMappings.Add("Volume", "Volume");
			bulkCopy.ColumnMappings.Add("Profit", "Profit");
			bulkCopy.ColumnMappings.Add("ProfitabilityRatio", "ProfitabilityRatio");
			bulkCopy.ColumnMappings.Add("SubCustAccountID", "SubCustAccountID");
			bulkCopy.ColumnMappings.Add("Market", "Market");
			
			foreach(ETradeHistory.Entities.PnLhistory entity in entities)
			{
				if (entity.EntityState != EntityState.Added)
					continue;
					
				DataRow row = dataTable.NewRow();
				
					row["ID"] = entity.Id;
							
				
					row["TradeTime"] = entity.TradeTime;
							
				
					row["RefOrderID"] = entity.RefOrderId;
							
				
					row["FISOrderID"] = entity.FisOrderId.HasValue ? (object) entity.FisOrderId  : System.DBNull.Value;
							
				
					row["SecSymbol"] = entity.SecSymbol;
							
				
					row["Price"] = entity.Price;
							
				
					row["AvgPrice"] = entity.AvgPrice.HasValue ? (object) entity.AvgPrice  : System.DBNull.Value;
							
				
					row["Volume"] = entity.Volume;
							
				
					row["Profit"] = entity.Profit.HasValue ? (object) entity.Profit  : System.DBNull.Value;
							
				
					row["ProfitabilityRatio"] = entity.ProfitabilityRatio.HasValue ? (object) entity.ProfitabilityRatio  : System.DBNull.Value;
							
				
					row["SubCustAccountID"] = entity.SubCustAccountId;
							
				
					row["Market"] = entity.Market;
							
				
				dataTable.Rows.Add(row);
			}		
			
			// send the data to the server		
			bulkCopy.WriteToServer(dataTable);		
			
			// update back the state
			foreach(ETradeHistory.Entities.PnLhistory entity in entities)
			{
				if (entity.EntityState != EntityState.Added)
					continue;
			
				entity.AcceptChanges();
			}
		}
				
		/// <summary>
		/// 	Inserts a ETradeHistory.Entities.PnLhistory object into the datasource using a transaction.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="entity">ETradeHistory.Entities.PnLhistory object to insert.</param>
		/// <remarks>
		///		After inserting into the datasource, the ETradeHistory.Entities.PnLhistory object will be updated
		/// 	to refelect any changes made by the datasource. (ie: identity or computed columns)
		/// </remarks>	
		/// <returns>Returns true if operation is successful.</returns>
        /// <exception cref="System.Exception">The command could not be executed.</exception>
        /// <exception cref="System.Data.DataException">The <paramref name="transactionManager"/> is not open.</exception>
        /// <exception cref="System.Data.Common.DbException">The command could not be executed.</exception>
		public override bool Insert(TransactionManager transactionManager, ETradeHistory.Entities.PnLhistory entity)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			DbCommand commandWrapper = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PnLHistory_Insert", _useStoredProcedure);
			
			database.AddOutParameter(commandWrapper, "@Id", DbType.Int64, 8);
			database.AddInParameter(commandWrapper, "@TradeTime", DbType.DateTime, entity.TradeTime );
			database.AddInParameter(commandWrapper, "@RefOrderId", DbType.AnsiString, entity.RefOrderId );
			database.AddInParameter(commandWrapper, "@FisOrderId", DbType.Int32, (entity.FisOrderId.HasValue ? (object) entity.FisOrderId  : System.DBNull.Value));
			database.AddInParameter(commandWrapper, "@SecSymbol", DbType.AnsiString, entity.SecSymbol );
			database.AddInParameter(commandWrapper, "@Price", DbType.Decimal, entity.Price );
			database.AddInParameter(commandWrapper, "@AvgPrice", DbType.Decimal, (entity.AvgPrice.HasValue ? (object) entity.AvgPrice  : System.DBNull.Value));
			database.AddInParameter(commandWrapper, "@Volume", DbType.Int32, entity.Volume );
			database.AddInParameter(commandWrapper, "@Profit", DbType.Decimal, (entity.Profit.HasValue ? (object) entity.Profit  : System.DBNull.Value));
			database.AddInParameter(commandWrapper, "@ProfitabilityRatio", DbType.Decimal, (entity.ProfitabilityRatio.HasValue ? (object) entity.ProfitabilityRatio  : System.DBNull.Value));
			database.AddInParameter(commandWrapper, "@SubCustAccountId", DbType.AnsiString, entity.SubCustAccountId );
			database.AddInParameter(commandWrapper, "@Market", DbType.AnsiString, entity.Market );
			
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
			entity.Id = (long)_id;
			
			
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
		/// <param name="entity">ETradeHistory.Entities.PnLhistory object to update.</param>
		/// <remarks>
		///		After updating the datasource, the ETradeHistory.Entities.PnLhistory object will be updated
		/// 	to refelect any changes made by the datasource. (ie: identity or computed columns)
		/// </remarks>
		/// <returns>Returns true if operation is successful.</returns>
        /// <exception cref="System.Exception">The command could not be executed.</exception>
        /// <exception cref="System.Data.DataException">The <paramref name="transactionManager"/> is not open.</exception>
        /// <exception cref="System.Data.Common.DbException">The command could not be executed.</exception>
		public override bool Update(TransactionManager transactionManager, ETradeHistory.Entities.PnLhistory entity)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			DbCommand commandWrapper = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PnLHistory_Update", _useStoredProcedure);
			
			database.AddInParameter(commandWrapper, "@Id", DbType.Int64, entity.Id );
			database.AddInParameter(commandWrapper, "@TradeTime", DbType.DateTime, entity.TradeTime );
			database.AddInParameter(commandWrapper, "@RefOrderId", DbType.AnsiString, entity.RefOrderId );
			database.AddInParameter(commandWrapper, "@FisOrderId", DbType.Int32, (entity.FisOrderId.HasValue ? (object) entity.FisOrderId : System.DBNull.Value) );
			database.AddInParameter(commandWrapper, "@SecSymbol", DbType.AnsiString, entity.SecSymbol );
			database.AddInParameter(commandWrapper, "@Price", DbType.Decimal, entity.Price );
			database.AddInParameter(commandWrapper, "@AvgPrice", DbType.Decimal, (entity.AvgPrice.HasValue ? (object) entity.AvgPrice : System.DBNull.Value) );
			database.AddInParameter(commandWrapper, "@Volume", DbType.Int32, entity.Volume );
			database.AddInParameter(commandWrapper, "@Profit", DbType.Decimal, (entity.Profit.HasValue ? (object) entity.Profit : System.DBNull.Value) );
			database.AddInParameter(commandWrapper, "@ProfitabilityRatio", DbType.Decimal, (entity.ProfitabilityRatio.HasValue ? (object) entity.ProfitabilityRatio : System.DBNull.Value) );
			database.AddInParameter(commandWrapper, "@SubCustAccountId", DbType.AnsiString, entity.SubCustAccountId );
			database.AddInParameter(commandWrapper, "@Market", DbType.AnsiString, entity.Market );
			
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
