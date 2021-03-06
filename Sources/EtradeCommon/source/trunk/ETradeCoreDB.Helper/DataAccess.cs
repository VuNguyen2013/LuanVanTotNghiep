// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataAccess.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the DataAccessLayer implemented data provider types.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


namespace ETradeCoreDB.Helper
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.Common;
    using System.Xml;

    /// <summary>
    /// Defines the DataAccessLayer implemented data provider types.
    /// </summary>
    public enum DataProviderType
    {
        Access,
        Odbc,
        OleDb,
        Oracle,
        Sql,
        DB2,
        Informix
    }

    public abstract class DataAccessBase
    {
        #region Private, Protected data members, methods and constructors

        // Private members
        protected OuputParameterItems _outputParameterItems;
        private string _ConnectionString;
        protected DbConnection _Connection;
        protected DbTransaction _Transaction;
        protected DataProviderType _DataProviderType;

        protected int _CommandTimeout = -1;

        public event RowUpdatedEventHandler DataAdapterRowUpdated;

        /// <summary>
        /// cho phep truy xuat den lastest returned values
        /// Khi chay, cac tham so output, return cua store duoc chua vao day.
        /// </summary>
        public OuputParameterItems ReturnedParameterValues
        {
            get
            {
                if (_outputParameterItems == null)
                {
                    _outputParameterItems = new OuputParameterItems();
                }
                //_outputParameterItems.importReturnValues(cmd);
                return _outputParameterItems;
            }
        }

        public void ImportReturnValues(DbCommand cmd)
        {
            _outputParameterItems = new OuputParameterItems();
            _outputParameterItems.EffectedRows = 0;
            _outputParameterItems.importReturnValues(cmd);
        }

        /// <summary>
        /// Gets or sets string used to connect to database
        /// </summary>
        public string ConnectionString
        {
            get
            {
                if (_ConnectionString == null || _ConnectionString.Length == 0)
                    throw new ArgumentException("Invalid database connection string.");

                return _ConnectionString;
            }
            set
            {
                _ConnectionString = value;
            }
        }

        // Constructors

        /// <summary>
        /// Since this is an abstract class, for better documentation and readability of source code, 
        /// class is defined with an explicit protected constructor
        /// </summary>
        protected DataAccessBase() { }

        // Private Methods        

        /// <summary>
        /// This method is used to attach array of SqlParameters to a DbCommand.
        /// 
        /// This method will assign a value of DbNull to any parameter with a direction of
        /// InputOutput and a value of null.  
        /// 
        /// This behavior will prevent default values from being used, but
        /// this will be the less common case than an intended pure output parameter (derived as InputOutput)
        /// where the user provided no input value.
        /// </summary>
        /// <param name="command">The command to which the parameters will be added</param>
        /// <param name="commandParameters">An array of DbParameters to be added to command</param>
        protected void AttachParameters(DbCommand command, DbParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            command.Parameters.Clear();
            if (commandParameters != null && commandParameters.Length > 0)
            {
                foreach (DbParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput ||
                            p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }

        /// <summary>
        /// This method assigns dataRow column values to an array of DbParameters
        /// </summary>
        /// <param name="commandParameters">Array of DbParameters to be assigned values</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values</param>
        protected void AssignParameterValues(DbParameter[] commandParameters, DataRow dataRow)
        {
            if ((commandParameters == null) || (dataRow == null))
            {
                // Do nothing if we get no data
                return;
            }

            int i = 0;
            // Set the parameters values
            foreach (DbParameter commandParameter in commandParameters)
            {
                // Check the parameter name
                if (commandParameter.ParameterName == null ||
                    commandParameter.ParameterName.Length <= 1)
                    throw new Exception(
                        string.Format(
                            "Please provide a valid parameter name on the parameter #{0}, the ParameterName property has the following value: '{1}'.",
                            i, commandParameter.ParameterName));

                if (dataRow.Table.Columns.IndexOf(commandParameter.ParameterName.Substring(1)) != -1)
                    commandParameter.Value = dataRow[commandParameter.ParameterName.Substring(1)];

                i++;
            }
        }

        /// <summary>
        /// This method assigns an array of values to an array of DbParameters
        /// </summary>
        /// <param name="commandParameters">Array of DbParameters to be assigned values</param>
        /// <param name="parameterValues">Array of objects holding the values to be assigned</param>
        protected void AssignParameterValues(DbParameter[] commandParameters, object[] parameterValues)
        {
            if ((commandParameters == null) || (parameterValues == null))
            {
                // Do nothing if we get no data
                return;
            }

            // We must have the same number of values as we pave parameters to put them in
            if (commandParameters.Length != parameterValues.Length)
            {
                throw new ArgumentException("Parameter count does not match Parameter Value count.");
            }

            // Iterate through the DbParameters, assigning the values from the corresponding position in the 
            // value array
            for (int i = 0, j = commandParameters.Length; i < j; i++)
            {
                // If the current array value derives from DbParameter, then assign its Value property
                if (parameterValues[i] is DbParameter)
                {
                    DbParameter paramInstance = (DbParameter)parameterValues[i];
                    if (paramInstance.Value == null)
                    {
                        commandParameters[i].Value = DBNull.Value;
                    }
                    else
                    {
                        commandParameters[i].Value = paramInstance.Value;
                    }
                }
                else if (parameterValues[i] == null)
                {
                    commandParameters[i].Value = DBNull.Value;
                }
                else
                {
                    commandParameters[i].Value = parameterValues[i];
                }
            }
        }

        /// <summary>
        /// This method opens (if necessary) and assigns a connection, transaction, command type and parameters 
        /// to the provided command
        /// </summary>        
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters to be associated with the command or 'null' if no parameters are required</param>
        /// <param name="mustCloseConnection"><c>true</c> if the connection was opened by the method, otherwose is false.</param>
        protected DbCommand PrepareCommand(CommandType commandType, string commandText, DbParameter[] commandParameters, out bool mustCloseConnection)
        {
            // provide the specific data provider connection object, if the connection object is null
            if (_Connection == null)
            {
                _Connection = GetDataProviderConnection();
                _Connection.ConnectionString = this.ConnectionString;
            }

            // if the provided connection is not open, we will open it
            if (_Connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                _Connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }

            DbCommand cmd = GeDataProviderCommand();
            cmd.Connection = _Connection;
            cmd.CommandText = commandText;
            // set the command type
            cmd.CommandType = commandType;

            // if a transaction is provided, then assign it.
            if (_Transaction != null)
            {
                if (_Transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

                //cmd.Transaction = _Transaction;
                cmd.Transaction = _Transaction;
            }

            // Attach the command parameters if they are provided
            if (commandParameters != null)
            {
                //AttachParameters(cmd, commandParameters);
                AttachParameters(cmd, commandParameters);
            }
            return cmd;
        }

        #endregion Private data members, methods and constructors

        #region Abstract methods

        #region Internal abstract methods

        /// <summary>
        /// Data provider specific implementation for accessing relational databases.
        /// </summary>
        internal abstract DbConnection GetDataProviderConnection();

        /// <summary>
        /// Data provider specific implementation for executing SQL statement while connected to a data source.
        /// </summary>
        internal abstract DbCommand GeDataProviderCommand();

        /// <summary>
        /// Data provider specific implementation for filling the DataSet.
        /// </summary>
        internal abstract DbDataAdapter GetDataProviderDataAdapter();

        #endregion

        #region Public abstract methods

        /// <summary>
        /// Initializes a new instance of the array DbParameters class.
        /// </summary>
        /// <returns></returns>
        public abstract DbParameter[] CreateArrayParameters(int count);

        /// <summary>
        /// Initializes a new instance of the DbParameter class.
        /// </summary>
        /// <returns></returns>
        public abstract DbParameter CreateParameter();

        /// <Summary>
        /// Initializes a new instance of the DbParameter class
        /// that uses the parameter name and a value of the new DbParameter.
        ///</Summary>                
        /// <param name="parameterName">The name of the parameter to map.</param>
        /// <param name="value">An System.Object that is the value of the DbParameter.</param>
        /// <returns></returns>
        public abstract DbParameter CreateParameter(string parameterName, object avalue);

        /// <summary>
        /// Initializes a new instance of the DbParameter class
        /// that uses the parameter name and the data type.</summary>
        /// <param name="parameterName">The name of the parameter to map.</param>
        /// <param name="dbType">One of the System.Data.SqlDbType values.</param>        
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// The value supplied in the dbType parameter is an invalid back-end data type.
        /// </exception>

        /// <summary>
        /// Initializes a new instance of the DbParameter class
        /// that uses the parameter name, the System.Data.SqlDbType, and the size.
        /// </summary>
        /// <param name="parameterName">The name of the parameter to map.</param>
        /// <param name="dbType">One of the System.Data.SqlDbType values.</param>
        /// <param name="size">The length of the parameter.</param>
        /// <returns></returns>
        public abstract DbParameter CreateParameter(string parameterName, DbType dbType, int size);

        /// <summary>
        /// Initializes a new instance of the DbParameter class
        /// that uses the parameter name, the System.Data.SqlDbType, the size, and the
        /// source column name.
        /// </summary>
        /// <param name="parameterName">The name of the parameter to map.</param>
        /// <param name="dbType">One of the System.Data.SqlDbType values.</param>
        /// <param name="size">The length of the parameter.</param>
        /// <param name="sourceColumn">The name of the source column.</param>
        /// <returns></returns>
        public abstract DbParameter CreateParameter(string parameterName, DbType dbType, int size, string sourceColumn);

        /// <summary>
        /// Initializes a new instance of the DbParameter class
        /// that uses the parameter name, the type of the parameter, the size of the
        /// parameter, a System.Data.ParameterDirection, the precision of the parameter,
        /// the scale of the parameter, the source column, a System.Data.DataRowVersion
        /// to use, and the value of the parameter.
        /// </summary>
        /// <param name="parameterName">The name of the parameter to map.</param>
        /// <param name="dbType">One of the System.Data.SqlDbType values.</param>
        /// <param name="size">The length of the parameter.</param>
        /// <param name="direction">One of the System.Data.ParameterDirection values.</param>
        /// <param name="isNullable">true if the value of the field can be null; otherwise false.</param>
        /// <param name="precision">The total number of digits to the left and right of the decimal point to
        /// which DbParameter.Value is resolved.</param>        
        /// <param name="scale">The total number of decimal places to which DbParameter.Value is resolved.</param>
        /// <param name="sourceColumn">The name of the source column.</param>
        /// <param name="sourceVersion">One of the System.Data.DataRowVersion values.</param>
        /// <param name="value">An System.Object that is the value of the DbParameter.</param>
        /// <returns></returns>        
        public abstract DbParameter SCreateParameter(string parameterName, DbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value);

        /// <summary>
        /// Initializes a new instance of the DbParameter class
        /// that uses the parameter name, the type of the parameter, the length of the
        /// parameter the direction, the precision, the scale, the name of the source
        /// column, one of the System.Data.DataRowVersion values, a Boolean for source
        /// column mapping, the value of the SqlParameter, the name of the database where
        /// the schema collection for this XML instance is located, the owning relational
        /// schema where the schema collection for this XML instance is located, and
        /// the name of the schema collection for this parameter.
        /// </summary>
        /// <param name="parameterName">The name of the parameter to map.</param>
        /// <param name="dbType">One of the System.Data.DbType values.</param>
        /// <param name="size">The length of the parameter.</param>
        /// <param name="direction">One of the System.Data.ParameterDirection values.</param>
        /// <param name="precision">The total number of digits to the left and right of the decimal point to
        /// which System.Data.SqlClient.SqlParameter.Value is resolved.</param>        
        /// <param name="scale">The total number of decimal places to which DbParameter.Value</param>
        /// <param name="sourceColumn">The name of the source column.</param>
        /// <param name="sourceVersion">One of the System.Data.DataRowVersion values.</param>
        /// <param name="sourceColumnNullMapping">true if the source column is nullable; false if it is not.</param>
        /// <param name="value">An System.Object that is the value of the DbParameter.</param>
        /// <param name="xmlSchemaCollectionDatabase">The name of the database where the schema collection for this XML instance</param>
        /// <param name="xmlSchemaCollectionOwningSchema">The owning relational schema where the schema collection for this XML instance is located.</param>
        /// <param name="xmlSchemaCollectionName">The name of the schema collection for this parameter.</param>
        /// <returns></returns>
        public abstract DbParameter CreateParameter(string parameterName, DbType dbType, int size, ParameterDirection direction, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, bool sourceColumnNullMapping, object value, string xmlSchemaCollectionDatabase, string xmlSchemaCollectionOwningSchema, string xmlSchemaCollectionName);

        /// <summary>
        /// Initializes a new instance array of the DbParameter class.
        /// </summary>
        /// <param name="size">size of the array</param>
        /// <returns></returns>
        public abstract DbParameter[] CreateParametersArray(int size);

        /// <summary>
        /// Initializes a new instance array of the DbParameter class.
        /// </summary>
        /// <param name="parameterValues">array ParametersName and values<c>@parameterName1, value1, @parameterName2, value2...</c></param>
        /// <returns></returns>
        public abstract DbParameter[] CreateParametersArray(params object[] parameterValues);

        #endregion

        #region Public abstract Build DBCommand methods


        /// <summary>
        /// Create DbCommand for the Insert
        /// </summary>
        /// <param name="table">table name</param>
        /// <param name="columns">columns list</param>
        /// <returns>DbCommand</returns>        
        public abstract DbCommand BuildInsert(string table, string[] columns);

        /// <summary>
        /// Careate DbCommand for the Update
        /// </summary>
        /// <param name="table">table name</param>
        /// <param name="columns">columns list</param>
        /// <param name="keys">primary keys list </param>
        /// <returns>DbCommand</returns>        
        public abstract DbCommand BuildUpdate(string table, string[] columns, string[] keys);

        /// <summary>
        /// Create DbCommand for the Delete
        /// </summary>
        /// <param name="table">table name</param>
        /// <param name="keys">primary keys list</param>
        /// <returns>DbCommand is builded with DELETE command</returns>      
        public abstract DbCommand BuildDelete(string table, string[] keys);

        #endregion

        #endregion Abstract methods

        #region Database Transaction

        /// <summary>
        /// Begins a database transaction.
        /// </summary>
        public void BeginTransaction()
        {
            try
            {
                // provide the specific data provider connection object, if the connection object is null
                if (_Connection == null)
                {
                    _Connection = GetDataProviderConnection();
                    _Connection.ConnectionString = this.ConnectionString;
                }

                // open connection
                _Connection.Open();
                // begin a database transaction with a read committed isolation level
                _Transaction = _Connection.BeginTransaction(IsolationLevel.ReadCommitted);
            }
            catch
            {
                _Connection.Close();
            }
        }

        /// <summary>
        /// Ducle 2010-07-01: move connection closing block out of finally
        /// Commits the database transaction.
        /// </summary>
        public void CommitTransaction()
        {
            if (_Transaction == null)
            {
                return;
            }

            try
            {
                // Commit transaction
                _Transaction.Commit();

                _Connection.Close();
                _Transaction = null;
            }
            catch
            {
                // rollback transaction
                RollbackTransaction();
                _Connection.Close();
                _Transaction = null;
            }
            /*finally
            {
                // DucLe comment out 2010-07-01
                //_Connection.Close();
                //_Transaction = null;
            }*/
        }

        /// <summary>
        /// Ducle 2010-07-01: move connection closing block out of finally
        /// Rolls back a transaction from a pending state.
        /// </summary>
        public void RollbackTransaction()
        {
            if (_Transaction == null)
                return;

            try
            {
                _Transaction.Rollback();
                _Connection.Close();
                _Transaction = null;
            }
            catch { }
            /*finally
            {
                _Connection.Close();
                _Transaction = null;
            }*/
        }

        #endregion Database Transaction

        #region Execute queries

        #region Execute data

        #region ExecuteNonQuery

        /// <summary>
        /// Executes an SQL statement against the Connection object of a .NET Framework data provider, and returns the number of rows affected.
        /// </summary>
        /// <remarks>e.g.:
        /// int result = ExcuteNonQuery("Insert Into User(username,password) values('abc','123')");
        /// </remarks>
        /// <param name="commandText">T-SQL CommandText</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public int ExecuteNonQuery(string commandText)
        {
            return this.ExecuteNonQuery(CommandType.Text, commandText, null);
        }

        /// <summary>
        /// Executes an SQL parameterized statement against the Connection object of a .NET Framework data provider, and returns the number of rows affected.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>        
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public int ExecuteNonQuery(string commandText, DbParameter[] commandParameters)
        {
            return this.ExecuteNonQuery(CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset and takes no parameters) against the Connection object
        /// of a .NET Framework data provider
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>        
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>        
        public int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            return this.ExecuteNonQuery(commandType, commandText, null);
        }

        /// <summary>
        /// DucLe 2010-07-01 move code block out of finally
        /// Execute a SqlCommand (that returns no resultset) against the Connection object of a .NET Framework data provider
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>        
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>                
        public int ExecuteNonQuery(CommandType commandType, string commandText, DbParameter[] commandParameters)
        {
            lock (this)
            {
                bool mustCloseConnection = false;
                DbCommand cmd = null;
                try
                {
                    cmd = PrepareCommand(commandType, commandText, commandParameters, out mustCloseConnection);

                    // execute command
                    int intAffectedRows = cmd.ExecuteNonQuery();

                    ImportReturnValues(cmd);

                    if (cmd != null)
                        cmd.Parameters.Clear();

                    if (mustCloseConnection)
                        _Connection.Close();

                    // return no of affected records
                    return intAffectedRows;
                }
                catch
                {
                    if (_Transaction != null)
                        RollbackTransaction();

                    if (cmd != null)
                        cmd.Parameters.Clear();

                    if (mustCloseConnection)
                        _Connection.Close();

                    return -1;
                }

            }

        }

        /// <summary>
        /// Execute a stored procedure via a DbCommand (that returns no resultset) against the Connection object of a .NET Framework data provider.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int result = ExecuteNonQuery("PublishOrders", 24, 36);
        /// </remarks>        
        /// <param name="spName">The name of the stored prcedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public virtual int ExecuteNonQuery(string spName, params object[] parameterValues)
        {
            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                DbParameter[] commandParameters = ParameterCache.GetSpParameterSet(_Connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return this.ExecuteNonQuery(CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return this.ExecuteNonQuery(CommandType.StoredProcedure, spName);
            }
        }

        #endregion ExecuteNonQuery

        #region ExcecuteDataTable

        /// <summary>
        /// Execute a DbCommand (that returns a dataset) against the Connection object of a .NET Framework data provider.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  ExecuteDataTable("orders", "Select * From GetOrders");
        /// </remarks>
        /// <param name="tableName">The name will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)</param>
        /// <param name="commandText">T-SQL command</param>     
        /// <returns>A DataTable containing the resultset generated by the command</returns>
        public DataTable ExecuteDataTable(string tableName, string commandText)
        {
            return this.ExecuteDataTable(tableName, CommandType.Text, commandText);
        }

        /// <summary>
        /// Execute a DbCommand (that returns a dataset) against the Connection object of a .NET Framework data provider. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataTable("Orders", "Select * From GetOrders Where Prodid=@prodid", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="tableName">This name will be used to create table mappings allowing the DataTables to be referenced
        /// <param name="commandText">The stored procedure name or T-SQL command</param>        
        /// <param name="commandParameters">An array of DbParamters used to execute the command</param>
        /// by a user defined name (probably the actual table name)</param>
        /// <returns>A DataTable containing the resultset generated by the command</returns>
        public DataTable ExecuteDataTable(string tableName, string commandText, DbParameter[] commandParameters)
        {
            return this.ExecuteDataTable(tableName, CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a stored procedure via a DbCommand (that returns a resultset) against the Connection object of a .NET Framework data provider.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.        
        /// e.g.:  
        ///  DataTable dt = ExecuteDataset("GetOrders", 24, 36);
        /// </remarks>        
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A dataTable containing the resultset generated by the command</returns>
        public virtual DataTable ExecuteDataTable(string tableName, string spName, params object[] parameterValues)
        {
            if (_Connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                DbParameter[] commandParameters = ParameterCache.GetSpParameterSet(_Connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return this.ExecuteDataTable(tableName, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params                
                return this.ExecuteDataTable(tableName, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a DbCommand (that returns a dataset) against the Connection object of a .NET Framework data provider.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  ExecuteDataTable("orders", CommandType.StoredProcedure, "GetOrders");
        /// </remarks>   
        /// <param name="tableName">This name will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <return>A dataTable wich will contain the resultset generated by the command</return>
        public DataTable ExecuteDataTable(string tableName, CommandType commandType, string commandText)
        {
            return this.ExecuteDataTable(tableName, commandType, commandText, null);
        }

        /// <summary>
        /// Ducle 2010-07-01 move finally block out
        /// Public method that execute a SqlCommand (that returns a resultset) against 
        /// the Connection object of a .NET Framework data provider, and returns the number of rows affected.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataTable("Orders", CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>        
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>        
        /// <param name="commandParameters">An array of DbParamters used to execute the command</param>
        /// <return>A dataTable wich will contain the resultset generated by the command</param>
        public DataTable ExecuteDataTable(string tableName, CommandType commandType, string commandText, DbParameter[] commandParameters)
        {
            #region Test

            ////ParamsInfo[] inputParams = new ParamsInfo[] { new ParamsInfo( 0, "@AccountNo", DB2Type.Char, 10, accountNo ),
            ////                                                  new ParamsInfo( 1, "@Page", DB2Type.Decimal, 3, page )};
            ////ParamsInfo[] ouputParams = new ParamsInfo[] { new ParamsInfo( 0, "@AccountNo1", DB2Type.Char, 10, "" ),
            ////                                                  new ParamsInfo( 1, "@SecSymbol", DB2Type.Char, 8, "" ),
            ////                                                  new ParamsInfo( 2, "@TrusteeId", DB2Type.Char, 2, "" ),
            ////                                                  new ParamsInfo( 3, "@StartVolume", DB2Type.Decimal, 10, "" ),
            ////                                                  new ParamsInfo( 4, "@StartPrice", DB2Type.Decimal, 13, "" ),
            ////                                                  new ParamsInfo( 5, "@AvaiVolume", DB2Type.Decimal, 13, "" ),
            ////                                                  new ParamsInfo( 6, "@ActualVolume", DB2Type.Decimal, 10, "" ),
            ////                                                  new ParamsInfo( 7, "@AvgPrice", DB2Type.Decimal, 13, "" ),
            ////                                                  new ParamsInfo( 8, "@TodayRealize", DB2Type.Decimal, 13, "" ),
            ////                                                  new ParamsInfo( 9, "@SecType", DB2Type.Integer, 4, "" )
            ////                                   };



            //string sqlCmd = "call db2inst1.STR_PORTFOLIO(?,?)";

            //DB2Command cmd = (DB2Command)_Connection.CreateCommand();
            ////_Connection.Open();
            //cmd.CommandText = sqlCmd;
            //cmd.CommandType = CommandType.Text;
            //DbCommand cmd1;

            //DB2Parameter parm = new DB2Parameter("@AccountNo","0017891");// DB2Type.Char, 7);
            ////parm = cmd.Parameters.Add("@AccountNo", DB2Type.Char, 7);
            //parm.Direction = ParameterDirection.Input;
            ////parm.Value = "0017891";
            //cmd.Parameters.Add(parm);
            //DB2Parameter parm1 = new DB2Parameter("@Page", "0");
            ////parm1 = cmd.Parameters.Add("@Page", DB2Type.Decimal, 3);
            //parm1.Direction = ParameterDirection.Input;
            ////parm1.Value = 0;
            //cmd.Parameters.Add(parm1);
            //DB2DataAdapter da1 = new DB2DataAdapter();
            //da1.SelectCommand = cmd;
            ////_Connection.Open();
            //DataTable dt1 = new DataTable();
            //da1.FillSchema(dt1, SchemaType.Mapped);
            //da1.Fill(dt1);
            //////for (int i = 0; i < ouputParams.Length; i++)
            //////{
            //////    ParamsInfo paramInfo = ouputParams[i];

            //////    parm = cmd.Parameters.Add(paramInfo.name, paramInfo.type, paramInfo.size);
            //////    parm.Direction = ParameterDirection.Output;
            //////}

            //////Call store procedure
            ////cmd.ExecuteNonQuery();

            //////Retry out put
            ////DbDataReader dataReader = cmd.ExecuteReader();

            ////while (dataReader.Read())
            ////{
            ////    string val = "";
            ////}
            ////_Connection.Close();
            #endregion
            lock (this)
            {

                bool mustCloseConnection = false;
                DbCommand cmd = null;
                try
                {
                    cmd = PrepareCommand(commandType, commandText, commandParameters, out mustCloseConnection);

                    DataTable dt = new DataTable();
                    DbDataAdapter da = GetDataProviderDataAdapter();
                    da.SelectCommand = cmd;
                    // Add the table mappings specified by the user
                    if (tableName != null && tableName.Length > 0)
                    {
                        da.TableMappings.Add("Table", tableName);
                    }
                    string msgLog = mustCloseConnection == true ? "true" : "false";
                    string time = DateTime.Now.ToString("yyyyMMdd hh:mm:ss");
                    string tmpParams = "";

                    if (commandParameters != null)
                    {
                        foreach (DbParameter param in commandParameters)
                        {
                            tmpParams += param.ParameterName + ": " + param.Value.ToString() + "; ";
                        }
                    }

                    //da.FillSchema(dt, SchemaType.Mapped);
                    //fill the DataSet using default values for DataTable names, etc.
                    da.Fill(dt);

                    ImportReturnValues(cmd);

                    if (cmd != null)
                        cmd.Parameters.Clear();

                    if (mustCloseConnection)
                        _Connection.Close();

                    return dt;
                }
                catch (Exception ex)
                {
                    if (_Transaction == null)
                        _Connection.Close();
                    else
                        RollbackTransaction();

                    if (cmd != null)
                        cmd.Parameters.Clear();

                    if (mustCloseConnection)
                        _Connection.Close();

                    throw ex;

                }
            }
        }

        #endregion

        #region ExecuteDataSet

        /// <summary>
        /// Execute a DbCommand (that returns a dataset) against the Connection object of a .NET Framework data provider.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  ExecuteDataset(new string[] {"orders"}, "Select * From GetOrders");
        /// </remarks>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)</param>
        /// <param name="commandText">T-SQL command</param>     
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public DataSet ExecuteDataSet(string[] tableNames, string commandText)
        {
            return this.ExecuteDataSet(tableNames, CommandType.Text, commandText);
        }

        /// <summary>
        /// Execute a DbCommand (that returns a dataset) against the Connection object of a .NET Framework data provider. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataset("Select * From GetOrders Where Prodid=@prodid", ds, new string[] {"orders"}, new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
        /// <param name="commandParameters">An array of DbParamters used to execute the command</param>
        /// by a user defined name (probably the actual table name)</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public DataSet ExecuteDataSet(string[] tableNames, string commandText, DbParameter[] commandParameters)
        {
            return this.ExecuteDataSet(tableNames, CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a stored procedure via a DbCommand (that returns a resultset) against the Connection object of a .NET Framework data provider.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.        
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset("GetOrders", 24, 36);
        /// </remarks>        
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public virtual DataSet ExecuteDataSet(string[] tableNames, string spName, params object[] parameterValues)
        {
            if (_Connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                DbParameter[] commandParameters = ParameterCache.GetSpParameterSet(_Connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return this.ExecuteDataSet(tableNames, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params                
                return this.ExecuteDataSet(tableNames, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a DbCommand (that returns a dataset) against the Connection object of a .NET Framework data provider.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  ExecuteDataset(new string[] {"orders"}, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>   
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <return>A dataset wich will contain the resultset generated by the command</return>
        public DataSet ExecuteDataSet(string[] tableNames, CommandType commandType, string commandText)
        {
            return this.ExecuteDataSet(tableNames, commandType, commandText, null);
        }

        /// <summary>
        /// DucLe 2010-07-01 move finally block out
        /// Public method that execute a SqlCommand (that returns a resultset) against 
        /// the Connection object of a .NET Framework data provider, and returns the number of rows affected.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataset(new string[] { "Orders" }, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>        
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>        
        /// <param name="commandParameters">An array of DbParamters used to execute the command</param>
        /// <return>A dataset wich will contain the resultset generated by the command</param>
        public DataSet ExecuteDataSet(string[] tableNames, CommandType commandType, string commandText, DbParameter[] commandParameters)
        {
            lock (this)
            {

                bool mustCloseConnection = false;
                DbCommand cmd = null;
                try
                {
                    cmd = PrepareCommand(commandType, commandText, commandParameters, out mustCloseConnection);
                    //create the DataAdapter & DataSet
                    DbDataAdapter da = GetDataProviderDataAdapter();
                    da.SelectCommand = cmd;
                    DataSet ds = new DataSet();

                    // Add the table mappings specified by the user
                    if (tableNames != null && tableNames.Length > 0)
                    {
                        string tableName = "Table";
                        for (int index = 0; index < tableNames.Length; index++)
                        {
                            if (tableNames[index] == null || tableNames[index].Length == 0) throw new ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames");
                            da.TableMappings.Add(tableName, tableNames[index]);
                            tableName += (index + 1).ToString();
                        }
                    }

                    //da.FillSchema(ds, SchemaType.Mapped);
                    //fill the DataSet using default values for DataTable names, etc.
                    da.Fill(ds);

                    ImportReturnValues(cmd);

                    // Detach the SqlParameters from the command object, so they can be used again
                    if (cmd != null)
                        cmd.Parameters.Clear();

                    if (mustCloseConnection)
                        _Connection.Close();

                    //return the dataset
                    return ds;
                }
                catch (Exception ex)
                {
                    if (_Transaction == null)
                        _Connection.Close();
                    else
                        RollbackTransaction();

                    // Detach the SqlParameters from the command object, so they can be used again
                    if (cmd != null)
                        cmd.Parameters.Clear();

                    if (mustCloseConnection)
                        _Connection.Close();

                    throw ex;
                }
                /*finally
                {
                    // Detach the SqlParameters from the command object, so they can be used again
                    if (cmd != null)
                        cmd.Parameters.Clear();

                    if (mustCloseConnection)
                        _Connection.Close();
                }*/

            }
        }

        #endregion ExecuteDataSet

        #region ExecuteScalar

        /// <summary>
        /// Executes the query, and returns the first column of the first row in the resultset returned by the query. Extra columns or rows are ignored.
        /// </summary>       
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar("Select count(*) From Orders");
        /// </remarks>                
        /// <param name="commandText">T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>        
        public object ExecuteScalar(string commandText)
        {
            return this.ExecuteScalar(CommandType.Text, commandText, null);
        }

        /// <summary>
        /// Executes a parameterized query, and returns the first column of the first row in the resultset returned by the query. Extra columns or rows are ignored.
        /// </summary>        
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24));
        /// </remarks>        
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParamters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public object ExecuteScalar(string commandText, DbParameter[] commandParameters)
        {
            return this.ExecuteScalar(CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// Executes the query, and returns the first column of the first row in the resultset returned by the query. Extra columns or rows are ignored.
        /// </summary>       
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>        
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public object ExecuteScalar(CommandType commandType, string commandText)
        {
            return this.ExecuteScalar(commandType, commandText, null);
        }

        /// <summary>
        /// DucLe 2010-07-01 move finally block out
        /// Execute a DbCommand (that returns a 1x1 resultset) against the database specified in the _Connection        
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24));
        /// </remarks>        
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParamters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>        
        public object ExecuteScalar(CommandType commandType, string commandText, DbParameter[] commandParameters)
        {
            lock (this)
            {
                bool mustCloseConnection = false;
                DbCommand cmd = null;
                try
                {
                    cmd = PrepareCommand(commandType, commandText, commandParameters, out mustCloseConnection);
                    // execute command
                    object objValue = cmd.ExecuteScalar();

                    ImportReturnValues(cmd);

                    if (cmd != null)
                        cmd.Parameters.Clear();

                    if (mustCloseConnection)
                        _Connection.Close();

                    // check on value
                    if (objValue != DBNull.Value)
                        // return value
                        return objValue;
                    else
                        // return null instead of dbnull value
                        throw null;
                }
                catch (Exception ex)
                {
                    if (_Transaction != null)
                        this.RollbackTransaction();

                    if (cmd != null)
                        cmd.Parameters.Clear();

                    if (mustCloseConnection)
                        _Connection.Close();

                    throw new Exception(ex.Message, ex.InnerException);
                }
                /*finally
                {
                    //if (_Transaction == null)
                    //{
                    //    _Connection.Close();
                    //    cmd.Dispose();
                    //}
                    // Detach the SqlParameters from the command object, so they can be used again
                    if (cmd != null)
                        cmd.Parameters.Clear();

                    if (mustCloseConnection)
                        _Connection.Close();
                }*/

            }
        }

        /// <summary>
        /// Execute a stored procedure via a DbCommand (that returns a 1x1 resultset) against the specified SqlConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar("GetOrderCount", 24, 36);
        /// </remarks>        
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual object ExecuteScalar(string spName, params object[] parameterValues)
        {
            if (_Connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                DbParameter[] commandParameters = ParameterCache.GetSpParameterSet(_Connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return ExecuteScalar(CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteScalar(CommandType.StoredProcedure, spName);
            }
        }

        #endregion ExecuteScalar

        #region ExecuteDataReader

        /// <summary>
        /// Execute a DbCommand (that returns a resultset) against the Connection object of a .NET Framework data provider.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader("Select * From Orders");
        /// </remarks>                
        /// <param name="commandText">The stored procedure name or T-SQL command</param>        
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        public DbDataReader ExecuteDataReader(string commandText)
        {

            return this.ExecuteDataReader(CommandType.Text, commandText, null);
        }

        /// <summary>
        /// Execute a DbCommand (that returns a resultset) against the Connection object of a .NET Framework data provider.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader("Select * From Orders Where Prodid=@prodid", new SqlParameter("@prodid", 24));
        /// </remarks>                
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParamters used to execute the command</param>
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        public DbDataReader ExecuteDataReader(string commandText, DbParameter[] commandParameters)
        {
            return this.ExecuteDataReader(CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a stored procedure via a DbCommand (that returns a resultset) against the Connection object of a .NET Framework data provider.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.         
        /// e.g.:  
        ///  DbDataReader dr = ExecuteReader("GetOrders", 24, 36);
        /// </remarks>        
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        public virtual DbDataReader ExecuteDataReader(string spName, params object[] parameterValues)
        {
            if (_Connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                DbParameter[] commandParameters = ParameterCache.GetSpParameterSet(_Connection, spName);

                this.AssignParameterValues(commandParameters, parameterValues);

                return this.ExecuteDataReader(CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return this.ExecuteDataReader(CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a DbCommand (that returns a resultset) against the Connection object of a .NET Framework data provider.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader(CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>        
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>        
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        public DbDataReader ExecuteDataReader(CommandType commandType, string commandText)
        {
            return this.ExecuteDataReader(commandType, commandText, null);
        }

        /// <summary>
        /// DucLe 2010-07-01 move finally block out
        /// Execute a DbCommand (that returns a resultset) against the Connection object of a .NET Framework data provider.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader(CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>        
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParamters used to execute the command</param>
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        public virtual DbDataReader ExecuteDataReader(CommandType commandType, string commandText, DbParameter[] commandParameters)
        {
            lock (this)
            {
                bool mustCloseConnection = false;
                DbCommand cmd = null;
                try
                {
                    cmd = PrepareCommand(commandType, commandText, commandParameters, out mustCloseConnection);

                    DbDataReader dr;
                    //IBM.Data.Informix.IfxCommand cmd1 = new IBM.Data.Informix.IfxCommand();
                    //cmd1.Connection = (IBM.Data.Informix.IfxConnection)_Connection;
                    //cmd1.CommandText = "select * from custinfo";
                    //cmd1.CommandType = CommandType.Text;
                    //IBM.Data.Informix.IfxDataReader dr1 = cmd1.ExecuteReader();


                    if (_Transaction == null)
                    {
                        // Generate the reader. CommandBehavior.CloseConnection causes the
                        // the connection to be closed when the reader object is closed
                        cmd.ExecuteNonQuery();
                        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    }
                    else
                        dr = cmd.ExecuteReader();

                    // giu lai cac returned value 
                    ImportReturnValues(cmd);

                    if (cmd != null)
                        cmd.Parameters.Clear();

                    return dr;
                }
                catch (Exception ex)
                {
                    if (_Transaction == null)
                    {
                        _Connection.Close();
                        if (cmd != null)
                            cmd.Dispose();
                    }
                    else
                        this.RollbackTransaction();

                    if (cmd != null)
                        cmd.Parameters.Clear();

                    throw ex;
                }
                /*finally
                {
                    // Detach the SqlParameters from the command object, so they can be used again
                    if (cmd != null)
                        cmd.Parameters.Clear();

                    //if (mustCloseConnection)
                    // _Connection.Close();
                }*/

            }
        }


        #endregion ExecuteDataReader

        #region ExecuteXmlReader

        /// <summary>
        /// Execute a DbCommand (that returns a resultset and takes no parameters) against the provided DbConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader("Select * From GetOrders");
        /// </remarks>        
        /// <param name="commandText">T-SQL command using "FOR XML AUTO"</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        public XmlReader ExecuteXmlReader(string commandText)
        {
            // Pass through the call providing null for the set of DbParameters
            return ExecuteXmlReader(CommandType.Text, commandText, (DbParameter[])null);
        }

        /// <summary>
        /// Execute a DbCommand (that returns a resultset) against the Connection object of a .NET Framework data provider.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader("Select * From GetOrders Where Prodid=@prodid", new SqlParameter("@prodid", 24));
        /// </remarks>        
        /// <param name="commandText">The stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <param name="commandParameters">An array of DbParamters used to execute the command</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        public XmlReader ExecuteXmlReader(string commandText, params DbParameter[] commandParameters)
        {
            return this.ExecuteXmlReader(CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a stored procedure via a DbCommand (that returns a resultset) against the Connection object of a .NET Framework data provider.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader("GetOrders", 24, 36);
        /// </remarks>        
        /// <param name="spName">The name of the stored procedure using "FOR XML AUTO"</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        public virtual XmlReader ExecuteXmlReader(string spName, params object[] parameterValues)
        {
            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                DbParameter[] commandParameters = ParameterCache.GetSpParameterSet(_Connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return this.ExecuteXmlReader(CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return this.ExecuteXmlReader(CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a DbCommand (that returns a resultset) against the Connection object of a .NET Framework data provider.        
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader(CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>        
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <param name="commandParameters">An array of DbParamters used to execute the command</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        public XmlReader ExecuteXmlReader(CommandType commandType, string commandText)
        {
            return this.ExecuteXmlReader(commandType, commandText, (DbParameter)null);
        }

        /// <summary>
        /// Execute a DbCommand (that returns a resultset) against the Connection object of a .NET Framework data provider.        
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader(CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>        
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <param name="commandParameters">An array of DbParamters used to execute the command</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        public abstract XmlReader ExecuteXmlReader(CommandType commandType, string commandText, params DbParameter[] commandParameters);
        //{
        //    if (connection == null) throw new ArgumentNullException("connection");

        //    bool mustCloseConnection = false;
        //    // Create a command and prepare it for execution
        //    SqlCommand cmd = new SqlCommand();
        //    try
        //    {
        //        PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

        //        // Create the DataAdapter & DataSet
        //        XmlReader retval = cmd.ExecuteXmlReader();

        //        // Detach the SqlParameters from the command object, so they can be used again
        //        cmd.Parameters.Clear();

        //        return retval;
        //    }
        //    catch
        //    {
        //        if (mustCloseConnection)
        //            connection.Close();
        //        throw;
        //    }
        //}

        #endregion ExecuteXmlReader

        #endregion Execute

        #region FillDataTable

        /// <summary>
        /// Execute a DbCommand (that returns a resultset and takes no parameters) against the Connection object of a .NET Framework data provider.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataTable("orders", "Select * From GetOrders", dt);
        /// </remarks>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)</param>
        /// <param name="commandText">T-SQL command</param>
        /// <param name="dataTable">A dataTable wich will contain the resultset generated by the command</param>        
        public void FillDataset(string tableName, string commandText, DataTable dataTable)
        {
            this.FillDataTable(tableName, CommandType.Text, commandText, dataTable);
        }

        /// <summary>
        /// Execute a DbCommand (that returns a resultset and takes no parameters) against the Connection object of a .NET Framework data provider. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataTable("orders", "Select * From GetOrders Where Prodid=@prodid", dt, new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="dataTable">A dataTable wich will contain the resultset generated by the command</param>
        /// <param name="commandParameters">An array of DbParamters used to execute the command</param>
        /// by a user defined name (probably the actual table name)</param>
        public void FillDataTable(string tableName, string commandText, DataTable dataTable, params DbParameter[] commandParameters)
        {
            this.FillDataTable(tableName, CommandType.Text, commandText, dataTable, commandParameters);
        }

        /// <summary>
        /// Execute a stored procedure via a DbCommand (that returns a resultset) against the Connection object of a .NET Framework data provider.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  FillDataTable("orders", "GetOrders", ds, 24);
        /// </remarks>        
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataTable">A dataTable wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)</param>    
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        public virtual void FillDataTable(string tableName, string spName, DataTable dataTable, params object[] parameterValues)
        {
            if (_Connection == null) throw new ArgumentNullException("connection");
            if (dataTable == null) throw new ArgumentNullException("dataTable");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                DbParameter[] commandParameters = ParameterCache.GetSpParameterSet(_Connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                this.FillDataTable(tableName, CommandType.StoredProcedure, spName, dataTable, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                this.FillDataTable(tableName, CommandType.StoredProcedure, spName, dataTable);
            }
        }

        /// <summary>
        /// Execute a DbCommand (that returns a resultset and takes no parameters) against the Connection object of a .NET Framework data provider.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataTable("orders", CommandType.StoredProcedure, "GetOrders", dt);
        /// </remarks>   
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="dataTable">A dataTable wich will contain the resultset generated by the command</param>        
        public void FillDataTable(string tableName, CommandType commandType, string commandText, DataTable dataTable)
        {
            FillDataTable(tableName, commandType, commandText, dataTable, null);
        }

        /// <summary>
        /// DucLe 2010-07-01 move finally block out
        /// Public method that execute a SqlCommand (that returns a resultset) against 
        /// the Connection object of a .NET Framework data provider, and returns the number of rows affected.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataTable("Orders", CommandType.StoredProcedure, "GetOrders", dt, new SqlParameter("@prodid", 24));
        /// </remarks>        
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="dataTable">A dataTable wich will contain the resultset generated by the command</param>        
        /// <param name="commandParameters">An array of DbParamters used to execute the command</param>
        private void FillDataTable(string tableName, CommandType commandType, string commandText,
            DataTable dataTable, params DbParameter[] commandParameters)
        {
            lock (this)
            {
                bool mustCloseConnection = false;
                DbCommand cmd = null;
                try
                {
                    if (dataTable == null) throw new ArgumentNullException("dataTable");

                    cmd = this.PrepareCommand(commandType, commandText, commandParameters, out mustCloseConnection);
                    DbDataAdapter da = GetDataProviderDataAdapter();
                    da.SelectCommand = cmd;
                    // Create the DataAdapter & DataSet                       
                    using (da)
                    {

                        // Add the table mappings specified by the user
                        if (tableName != null && tableName.Length > 0)
                        {
                            da.TableMappings.Add("Table", tableName);
                        }

                        //Fill schema for dataset.
                        //da.FillSchema(dataTable, SchemaType.Mapped);

                        // Fill the DataSet using default values for DataTable names, etc
                        da.Fill(dataTable);

                        ImportReturnValues(cmd);

                        if (cmd != null)
                            cmd.Parameters.Clear();

                        if (mustCloseConnection)
                            _Connection.Close();
                    }
                }
                catch
                {
                    if (_Transaction != null)
                        RollbackTransaction();

                    if (cmd != null)
                        cmd.Parameters.Clear();

                    if (mustCloseConnection)
                        _Connection.Close();
                }
                /*finally
                {
                    // Detach the SqlParameters from the command object, so they can be used again
                    if (cmd != null)
                        cmd.Parameters.Clear();

                    if (mustCloseConnection)
                        _Connection.Close();
                }*/
            }
        }

        #endregion FillDataTable

        #region FillDataset

        /// <summary>
        /// Execute a DbCommand (that returns a resultset and takes no parameters) against the Connection object of a .NET Framework data provider.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataset(new string[] {"orders"}, "Select * From GetOrders", ds);
        /// </remarks>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)</param>
        /// <param name="commandText">T-SQL command</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>        
        public void FillDataset(string[] tableNames, string commandText, DataSet dataSet)
        {
            this.FillDataset(tableNames, CommandType.Text, commandText, dataSet);
        }

        /// <summary>
        /// Execute a DbCommand (that returns a resultset and takes no parameters) against the Connection object of a .NET Framework data provider. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataset("Select * From GetOrders Where Prodid=@prodid", ds, new string[] {"orders"}, new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
        /// <param name="commandParameters">An array of DbParamters used to execute the command</param>
        /// by a user defined name (probably the actual table name)</param>
        public void FillDataset(string[] tableNames, string commandText, DataSet dataSet, params DbParameter[] commandParameters)
        {
            this.FillDataset(tableNames, CommandType.Text, commandText, dataSet, commandParameters);
        }

        /// <summary>
        /// Execute a stored procedure via a DbCommand (that returns a resultset) against the Connection object of a .NET Framework data provider.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  FillDataset(new string[] {"orders"}, "GetOrders", ds, 24);
        /// </remarks>        
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)</param>    
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        public virtual void FillDataset(string[] tableNames, string spName, DataSet dataSet, params object[] parameterValues)
        {
            if (_Connection == null) throw new ArgumentNullException("connection");
            if (dataSet == null) throw new ArgumentNullException("dataSet");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                DbParameter[] commandParameters = ParameterCache.GetSpParameterSet(_Connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                this.FillDataset(tableNames, CommandType.StoredProcedure, spName, dataSet, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                this.FillDataset(tableNames, CommandType.StoredProcedure, spName, dataSet);
            }
        }

        /// <summary>
        /// Execute a DbCommand (that returns a resultset and takes no parameters) against the Connection object of a .NET Framework data provider.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataset(new string[] {"orders"}, CommandType.StoredProcedure, "GetOrders", ds);
        /// </remarks>   
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>        
        public void FillDataset(string[] tableNames, CommandType commandType, string commandText, DataSet dataSet)
        {
            FillDataset(tableNames, commandType, commandText, dataSet, null);
        }

        /// <summary>
        /// DucLe 2010-07-01 move finally block out
        /// Public method that execute a SqlCommand (that returns a resultset) against 
        /// the Connection object of a .NET Framework data provider, and returns the number of rows affected.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataset(new string[] { "Orders" }, CommandType.StoredProcedure, "GetOrders", ds, new SqlParameter("@prodid", 24));
        /// </remarks>        
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>        
        /// <param name="commandParameters">An array of DbParamters used to execute the command</param>
        private void FillDataset(string[] tableNames, CommandType commandType, string commandText,
            DataSet dataSet, params DbParameter[] commandParameters)
        {
            lock (this)
            {
                bool mustCloseConnection = false;
                DbCommand cmd = null;
                try
                {
                    if (dataSet == null) throw new ArgumentNullException("dataSet");

                    cmd = this.PrepareCommand(commandType, commandText, commandParameters, out mustCloseConnection);

                    DbDataAdapter da = GetDataProviderDataAdapter();
                    da.SelectCommand = cmd;

                    // Create the DataAdapter & DataSet                       
                    using (da)
                    {

                        // Add the table mappings specified by the user
                        if (tableNames != null && tableNames.Length > 0)
                        {
                            string tableName = "Table";
                            for (int index = 0; index < tableNames.Length; index++)
                            {
                                if (tableNames[index] == null || tableNames[index].Length == 0) throw new ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames");
                                da.TableMappings.Add(tableName, tableNames[index]);
                                tableName += (index + 1).ToString();
                            }
                        }

                        //Fill schema for dataset.
                        //da.FillSchema(dataSet, SchemaType.Mapped);

                        // Fill the DataSet using default values for DataTable names, etc
                        da.Fill(dataSet);

                        ImportReturnValues(cmd);

                        if (cmd != null)
                            cmd.Parameters.Clear();

                        if (mustCloseConnection)
                            _Connection.Close();
                    }
                }
                catch
                {
                    if (_Transaction != null)
                        RollbackTransaction();

                    if (cmd != null)
                        cmd.Parameters.Clear();

                    if (mustCloseConnection)
                        _Connection.Close();
                }
                /*finally
                {
                    // Detach the SqlParameters from the command object, so they can be used again
                    if (cmd != null)
                        cmd.Parameters.Clear();

                    if (mustCloseConnection)
                        _Connection.Close();
                }*/

            }
        }

        #endregion FillDataset

        #region UpdateDataset

        /// <summary>
        /// Executes the respective command for each inserted, updated, or deleted row in the DataSet.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  UpdateDataset(insertCommand, deleteCommand, updateCommand, dataSet, "Order");
        /// </remarks>
        /// <param name="insertCommand">A valid transact-SQL statement or stored procedure to insert new records into the data source</param>
        /// <param name="deleteCommand">A valid transact-SQL statement or stored procedure to delete records from the data source</param>
        /// <param name="updateCommand">A valid transact-SQL statement or stored procedure used to update records in the data source</param>
        /// <param name="dataSet">The DataSet used to update the data source</param>
        /// <param name="tableName">The DataTable used to update the data source.</param>
        public void UpdateDataset(DbCommand insertCommand, DbCommand deleteCommand, DbCommand updateCommand, DataSet dataSet, string tableName)
        {
            if (insertCommand == null) throw new ArgumentNullException("insertCommand");
            if (deleteCommand == null) throw new ArgumentNullException("deleteCommand");
            if (updateCommand == null) throw new ArgumentNullException("updateCommand");
            if (tableName == null || tableName.Length == 0) throw new ArgumentNullException("tableName");

            // Create a SqlDataAdapter, and dispose of it after we are done
            using (DbDataAdapter dataAdapter = (DbDataAdapter)((ICloneable)GetDataProviderDataAdapter()).Clone())
            {
                // Set the data adapter commands
                dataAdapter.UpdateCommand = updateCommand;
                dataAdapter.InsertCommand = insertCommand;
                dataAdapter.DeleteCommand = deleteCommand;

                // Update the dataset changes in the data source
                dataAdapter.Update(dataSet, tableName);

                // Commit all the changes made to the DataSet
                dataSet.AcceptChanges();
            }
        }

        /// <summary>
        /// Executes the respective command for each inserted, updated, or deleted row in the DataTable.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  UpdateDataset(insertCommand, deleteCommand, updateCommand, dataSet, "Order");
        /// </remarks>
        /// <param name="insertCommand">A valid transact-SQL statement or stored procedure to insert new records into the data source</param>
        /// <param name="deleteCommand">A valid transact-SQL statement or stored procedure to delete records from the data source</param>
        /// <param name="updateCommand">A valid transact-SQL statement or stored procedure used to update records in the data source</param>
        /// <param name="dataTable">The DataTable used to update the data source</param>        
        public void UpdateDataTable(DbCommand insertCommand, DbCommand deleteCommand, DbCommand updateCommand, DataTable dataTable)
        {
            //if (insertCommand == null) throw new ArgumentNullException("insertCommand");
            //if (deleteCommand == null) throw new ArgumentNullException("deleteCommand");
            //if (updateCommand == null) throw new ArgumentNullException("updateCommand");
            if (dataTable == null) throw new ArgumentNullException("DataTable");

            // Create a SqlDataAdapter, and dispose of it after we are done
            using (DbDataAdapter dataAdapter = (DbDataAdapter)((ICloneable)GetDataProviderDataAdapter()).Clone())
            {
                // Set the data adapter commands
                if (updateCommand != null)
                    dataAdapter.UpdateCommand = updateCommand;
                if (insertCommand != null)
                    dataAdapter.InsertCommand = insertCommand;
                if (deleteCommand != null)
                    dataAdapter.DeleteCommand = deleteCommand;

                // Update the dataset changes in the data source
                dataAdapter.Update(dataTable);

                // Commit all the changes made to the DataSet
                dataTable.AcceptChanges();
            }
        }

        #endregion UpdateDataset

        #region Create command from the store produre

        /// <summary>
        /// Simplify the creation of a Dbcommand object by allowing
        /// a stored procedure and optional parameters to be provided
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlCommand command = CreateCommand(conn, "AddCustomer", "CustomerID", "CustomerName");
        /// </remarks>
        /// <param name="connection">A valid SqlConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="sourceColumns">An array of string to be assigned as the source columns of the stored procedure parameters</param>
        /// <returns>A valid DbCommand object</returns>
        public virtual DbCommand CreateCommand(string spName, params string[] sourceColumns)
        {
            if (_Connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // Create a SqlCommand            
            DbCommand cmd = this.GeDataProviderCommand();
            cmd.CommandText = spName;
            cmd.CommandType = CommandType.StoredProcedure;

            // If we receive parameter values, we need to figure out where they go
            if ((sourceColumns != null) && (sourceColumns.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                DbParameter[] commandParameters = ParameterCache.GetSpParameterSet(_Connection, spName);

                // Assign the provided source columns to these parameters based on parameter order
                for (int index = 0; index < sourceColumns.Length; index++)
                    commandParameters[index].SourceColumn = sourceColumns[index];

                // Attach the discovered parameters to the SqlCommand object
                this.AttachParameters(cmd, commandParameters);
            }

            return cmd;
        }

        #endregion CreateCommand

        #region Excute typed params

        #region ExecuteNonQueryTypedParams

        /// <summary>
        /// Execute a stored procedure via a DbCommand (that returns no resultset) against the Connection object of a 
        /// .NET Framework data provider using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on row values.
        /// </summary>       
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public virtual int ExecuteNonQueryTypedParams(String spName, DataRow dataRow)
        {
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                DbParameter[] commandParameters = ParameterCache.GetSpParameterSet(_Connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return this.ExecuteNonQuery(CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return this.ExecuteNonQuery(CommandType.StoredProcedure, spName);
            }
        }

        #endregion ExecuteNonQueryTypedParams

        #region ExecuteDatasetTypedParams

        /// <summary>
        /// Execute a stored procedure via a DbCommand (that returns a resultset) against the Connection object of a 
        /// .NET Framework data provider using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on row values.
        /// </summary>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public virtual DataSet ExecuteDatasetTypedParams(string[] tableNames, String spName, DataRow dataRow)
        {
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            //If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                DbParameter[] commandParameters = ParameterCache.GetSpParameterSet(_Connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return this.ExecuteDataSet(tableNames, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return this.ExecuteDataSet(tableNames, CommandType.StoredProcedure, spName);
            }
        }

        #endregion

        #region ExecuteReaderTypedParams

        /// <summary>
        /// Execute a stored procedure via a DbCommand (that returns a resultset) against the Connection object of a 
        /// .NET Framework data provider using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>        
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>A SqlDataReader containing the resultset generated by the command</returns>
        public virtual DbDataReader ExecuteReaderTypedParams(String spName, DataRow dataRow)
        {
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                DbParameter[] commandParameters = ParameterCache.GetSpParameterSet(_Connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return this.ExecuteDataReader(CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return this.ExecuteDataReader(CommandType.StoredProcedure, spName);
            }
        }

        #endregion ExecuteReaderTypedParams

        #region ExecuteScalarTypedParams

        /// <summary>
        /// Execute a stored procedure via a DbCommand (that returns a 1x1 resultset) against the Connection object of a 
        /// .NET Framework data provider using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>        
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual object ExecuteScalarTypedParams(String spName, DataRow dataRow)
        {
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                DbParameter[] commandParameters = ParameterCache.GetSpParameterSet(_Connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return this.ExecuteScalar(CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return this.ExecuteScalar(CommandType.StoredProcedure, spName);
            }
        }

        #endregion ExecuteScalarTypedParams

        #region ExecuteXmlReaderTypedParams

        /// <summary>
        /// Execute a stored procedure via a DbCommand (that returns a resultset) against the Connection object of a 
        /// .NET Framework data provider using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <param name="connection">A valid SqlConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        public virtual XmlReader ExecuteXmlReaderTypedParams(String spName, DataRow dataRow)
        {
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                DbParameter[] commandParameters = ParameterCache.GetSpParameterSet(_Connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return this.ExecuteXmlReader(CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return this.ExecuteXmlReader(CommandType.StoredProcedure, spName);
            }
        }

        #endregion ExecuteXmlReaderTypedParams

        #endregion Excute typed params

        #endregion Execute queries

        #region Event Methods

        protected void OnDataAdapterRowUpdated(object sender, RowUpdatedEventArgs e)
        {
            DataAdapterRowUpdated(sender, e);
        }

        #endregion
    }

    /// <summary>
    /// ParameterCache provides functions to leverage a static cache of procedure parameters, and the
    /// ability to discover parameters for stored procedures at run-time.
    /// </summary>
    [Serializable]
    public sealed class ParameterCache
    {
        #region private methods, variables, and constructors

        private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// Resolve at run time the appropriate set of SqlParameters for a stored procedure
        /// </summary>
        /// <param name="connection">A valid SqlConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="includeReturnValueParameter">Whether or not to include their return value parameter</param>
        /// <returns>The parameter array discovered.</returns>
        private static DbParameter[] DiscoverSpParameterSet(DbConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException("spName");

            DbParameter[] discoveredParameters = new DbParameter[0];

            switch (connection.GetType().Name)
            {
                case "DB2Connection":
                    IBM.Data.DB2.DB2Command db2Cmd = new IBM.Data.DB2.DB2Command(spName, (IBM.Data.DB2.DB2Connection)connection);
                    db2Cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    IBM.Data.DB2.DB2CommandBuilder.DeriveParameters(db2Cmd);
                    connection.Close();

                    if (!includeReturnValueParameter)
                    {
                        db2Cmd.Parameters.RemoveAt(0);
                    }

                    discoveredParameters = new IBM.Data.DB2.DB2Parameter[db2Cmd.Parameters.Count];

                    db2Cmd.Parameters.CopyTo(discoveredParameters, 0);

                    // Init the parameters with a DBNull value
                    foreach (DbParameter discoveredParameter in discoveredParameters)
                    {
                        discoveredParameter.Value = DBNull.Value;
                    }

                    return discoveredParameters;

                case "IfxConnection":
                    IBM.Data.Informix.IfxCommand ifxCmd = new IBM.Data.Informix.IfxCommand(spName, (IBM.Data.Informix.IfxConnection)connection);
                    ifxCmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    IBM.Data.Informix.IfxCommandBuilder.DeriveParameters(ifxCmd);
                    connection.Close();

                    if (!includeReturnValueParameter)
                    {
                        ifxCmd.Parameters.RemoveAt(0);
                    }

                    discoveredParameters = new IBM.Data.Informix.IfxParameter[ifxCmd.Parameters.Count];

                    ifxCmd.Parameters.CopyTo(discoveredParameters, 0);

                    // Init the parameters with a DBNull value
                    foreach (DbParameter discoveredParameter in discoveredParameters)
                    {
                        discoveredParameter.Value = DBNull.Value;
                    }

                    return discoveredParameters;

                case "OleDbConnection":

                    System.Data.OleDb.OleDbCommand oledbCmd = new System.Data.OleDb.OleDbCommand(spName, (System.Data.OleDb.OleDbConnection)connection);
                    oledbCmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    System.Data.OleDb.OleDbCommandBuilder.DeriveParameters(oledbCmd);
                    connection.Close();

                    if (!includeReturnValueParameter)
                    {
                        oledbCmd.Parameters.RemoveAt(0);
                    }

                    discoveredParameters = new System.Data.OleDb.OleDbParameter[oledbCmd.Parameters.Count];

                    oledbCmd.Parameters.CopyTo(discoveredParameters, 0);

                    // Init the parameters with a DBNull value
                    foreach (DbParameter discoveredParameter in discoveredParameters)
                    {
                        discoveredParameter.Value = DBNull.Value;
                    }

                    return discoveredParameters;

                case "OdbcConnection":

                    System.Data.Odbc.OdbcCommand odbcCmd = new System.Data.Odbc.OdbcCommand(spName, (System.Data.Odbc.OdbcConnection)connection);
                    odbcCmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    System.Data.Odbc.OdbcCommandBuilder.DeriveParameters(odbcCmd);
                    connection.Close();

                    if (!includeReturnValueParameter)
                    {
                        odbcCmd.Parameters.RemoveAt(0);
                    }

                    discoveredParameters = new System.Data.Odbc.OdbcParameter[odbcCmd.Parameters.Count];

                    odbcCmd.Parameters.CopyTo(discoveredParameters, 0);

                    // Init the parameters with a DBNull value
                    foreach (DbParameter discoveredParameter in discoveredParameters)
                    {
                        discoveredParameter.Value = DBNull.Value;
                    }

                    return discoveredParameters;

                case "OracleConnection":

                    //DbParameter[] discoveredParameters;
                    return discoveredParameters;

                case "SqlConnection":

                    System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand(spName, (System.Data.SqlClient.SqlConnection)connection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    System.Data.SqlClient.SqlCommandBuilder.DeriveParameters(sqlCmd);
                    connection.Close();

                    if (!includeReturnValueParameter)
                    {
                        sqlCmd.Parameters.RemoveAt(0);
                    }

                    discoveredParameters = new System.Data.SqlClient.SqlParameter[sqlCmd.Parameters.Count];

                    sqlCmd.Parameters.CopyTo(discoveredParameters, 0);

                    // Init the parameters with a DBNull value
                    foreach (DbParameter discoveredParameter in discoveredParameters)
                    {
                        discoveredParameter.Value = DBNull.Value;
                    }

                    return discoveredParameters;

                default:
                    throw new ArgumentException("Invalid data access layer provider type.");
            }
        }

        /// <summary>
        /// Deep copy of cached SqlParameter array
        /// </summary>
        /// <param name="originalParameters"></param>
        /// <returns></returns>
        private static DbParameter[] CloneParameters(DbParameter[] originalParameters)
        {
            DbParameter[] clonedParameters = new DbParameter[originalParameters.Length];

            for (int i = 0, j = originalParameters.Length; i < j; i++)
            {
                clonedParameters[i] = (DbParameter)((ICloneable)originalParameters[i]).Clone();
            }

            return clonedParameters;
        }

        #endregion private methods, variables, and constructors

        #region caching functions

        /// <summary>
        /// Add parameter array to the cache
        /// </summary>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParamters to be cached</param>
        public static void CacheParameterSet(string connectionString, string commandText, params DbParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("connectionString");
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException("commandText");

            string hashKey = connectionString + ":" + commandText;

            paramCache[hashKey] = commandParameters;
        }

        /// <summary>
        /// Retrieve a parameter array from the cache
        /// </summary>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An array of SqlParamters</returns>
        public static DbParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            string hashKey = connectionString + ":" + commandText;

            DbParameter[] cachedParameters = paramCache[hashKey] as DbParameter[];
            if (cachedParameters == null)
            {
                return null;
            }
            else
            {
                return CloneParameters(cachedParameters);
            }
        }

        #endregion caching functions

        #region Parameter Discovery Functions

        /// <summary>
        /// Retrieves the set of SqlParameters appropriate for the stored procedure
        /// </summary>
        /// <remarks>
        /// This method will query the database for this information, and then store it in a cache for future requests.
        /// </remarks>
        /// <param name="connection">A valid SqlConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <returns>An array of SqlParameters</returns>
        internal static DbParameter[] GetSpParameterSet(DbConnection connection, string spName)
        {
            return GetSpParameterSet(connection, spName, false);
        }

        /// <summary>
        /// Retrieves the set of SqlParameters appropriate for the stored procedure
        /// </summary>
        /// <remarks>
        /// This method will query the database for this information, and then store it in a cache for future requests.
        /// </remarks>
        /// <param name="connection">A valid SqlConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="includeReturnValueParameter">A bool value indicating whether the return value parameter should be included in the results</param>
        /// <returns>An array of SqlParameters</returns>
        internal static DbParameter[] GetSpParameterSet(DbConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            using (DbConnection clonedConnection = (DbConnection)((ICloneable)connection).Clone())
            {
                return GetSpParameterSetInternal(clonedConnection, spName, includeReturnValueParameter);
            }
        }

        /// <summary>
        /// Retrieves the set of SqlParameters appropriate for the stored procedure
        /// </summary>
        /// <param name="connection">A valid SqlConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="includeReturnValueParameter">A bool value indicating whether the return value parameter should be included in the results</param>
        /// <returns>An array of SqlParameters</returns>
        private static DbParameter[] GetSpParameterSetInternal(DbConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException("spName");

            string hashKey = connection.ConnectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");

            DbParameter[] cachedParameters = paramCache[hashKey] as DbParameter[];
            if (cachedParameters == null)
            {
                DbParameter[] spParameters = DiscoverSpParameterSet(connection, spName, includeReturnValueParameter);
                paramCache[hashKey] = spParameters;
                cachedParameters = spParameters;
            }

            return CloneParameters(cachedParameters);
        }

        #endregion Parameter Discovery Functions
    }

    /// <summary>
    /// Loads different data access layer provider depending on the configuration settings file or the caller defined data provider type.
    /// </summary>
    [Serializable]
    public sealed class DataAccessFactory
    {

        // Since this class provides only static methods, make the default constructor private to prevent 
        // instances from being created with "new DataAccessLayerFactory()"
        private DataAccessFactory() { }

        /// <summary>
        /// Constructs a data access layer data provider based on application configuration settings.
        /// Application configuration file must contain two keys: 
        ///		1. "DataProviderType" key, with one of the DataProviderType enumerator.
        ///		2. "ConnectionString" key, holds the database connection string.
        /// </summary>
        public static DataAccessBase GetDataAccessLayer()
        {
            // Make sure application configuration file contains required configuration keys
            if (System.Configuration.ConfigurationSettings.AppSettings["DataProviderType"] == null
                || System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"] == null)
                throw new ArgumentNullException("Please specify a 'DataProviderType' and 'ConnectionString' configuration keys in the application configuration file.");

            DataProviderType dataProvider;

            try
            {
                // try to parse the data provider type from configuration file
                dataProvider =
                    (DataProviderType)System.Enum.Parse(typeof(DataProviderType),
                    System.Configuration.ConfigurationSettings.AppSettings["DataProviderType"].ToString(),
                    true);
            }
            catch
            {

                throw new ArgumentException("Invalid data access layer provider type.");
            }

            // return data access layer provider
            return GetDataAccessLayer(
                dataProvider,
                System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].ToString());
        }

        /// <summary>
        /// Constructs a data access layer based on caller specific data provider.
        /// Caller of this method must provide the database connection string through ConnectionString property.
        /// </summary>
        public static DataAccessBase GetDataAccessLayer(DataProviderType dataProviderType)
        {
            return GetDataAccessLayer(dataProviderType, null);
        }

        /// <summary>
        /// Constructs a data access layer data provider.
        /// </summary>
        public static DataAccessBase GetDataAccessLayer(DataProviderType dataProviderType, string connectionString)
        {
            // construct specific data access provider class
            switch (dataProviderType)
            {
                case DataProviderType.DB2:
                    return new DB2DataAccess(connectionString);
                case DataProviderType.Informix:
                    return new InformixDataAccess(connectionString);
                case DataProviderType.Access:
                case DataProviderType.OleDb:
                    return new OleDbDataAccess(connectionString);

                case DataProviderType.Odbc:
                    return new ODBCDataAccess(connectionString);

                case DataProviderType.Oracle:
                    return null;// new OracleDataAccessLayer(connectionString);

                case DataProviderType.Sql:
                    return new SqlDataAccess(connectionString);

                default:
                    throw new ArgumentException("Invalid data access layer provider type.");
            }
        }

        /// <summary>
        /// Constructs a data access layer data provider.
        /// </summary>
        public static DataAccessBase GetDataAccessLayer(DataProviderType dataProviderType, string connectionString, int commandTimout)
        {
            // construct specific data access provider class
            switch (dataProviderType)
            {
                case DataProviderType.DB2:
                    return new DB2DataAccess(connectionString, commandTimout);
                case DataProviderType.Informix:
                    return new InformixGenericDataAccess(connectionString, commandTimout);
                case DataProviderType.Access:
                case DataProviderType.OleDb:
                    return new OleDbDataAccess(connectionString, commandTimout);

                case DataProviderType.Odbc:
                    return new ODBCDataAccess(connectionString, commandTimout);

                case DataProviderType.Oracle:
                    return null;// new OracleDataAccessLayer(connectionString);

                case DataProviderType.Sql:
                    return new SqlDataAccess(connectionString, commandTimout);

                default:
                    throw new ArgumentException("Invalid data access layer provider type.");
            }
        }
    }
}
