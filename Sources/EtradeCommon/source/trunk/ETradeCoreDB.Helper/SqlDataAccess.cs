// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlDataAccess.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the SqlDataAccess type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCoreDB.Helper
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Xml;

    [Serializable]
    public class SqlDataAccess : DataAccessBase
    {
        // Provide class constructors
        public SqlDataAccess()
        {
            this._Connection = new SqlConnection();
        }
        public SqlDataAccess(string connString)
        {
            this.ConnectionString = connString;
            this._Connection = new SqlConnection(connString);
        }

        public SqlDataAccess(string connString, int commandTimeout)
        {
            this.ConnectionString = connString;
            this._CommandTimeout = commandTimeout;
            this._Connection = new SqlConnection(connString);            
        }

        #region Internal methods

        internal override DbConnection GetDataProviderConnection()
        {
            return new SqlConnection();
        }

        internal override DbCommand GeDataProviderCommand()
        {
            SqlCommand command = new SqlCommand();
            if (this._CommandTimeout > 0)
                command.CommandTimeout = this._CommandTimeout;
            return command;            
        }

        internal override DbDataAdapter GetDataProviderDataAdapter()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            da.RowUpdated += new SqlRowUpdatedEventHandler(da_RowUpdated);
            return da;
        }

        internal void da_RowUpdated(object sender, SqlRowUpdatedEventArgs e)
        {
            OnDataAdapterRowUpdated(sender, e);
        }

        #endregion

        #region Public methods

        public override DbParameter[] CreateArrayParameters(int count)
        {
            return new SqlParameter[count];
        }

        public override DbParameter CreateParameter()
        {
            return new SqlParameter();
        }

        public override DbParameter CreateParameter(string parameterName, object value)
        {
            return new SqlParameter(parameterName, value);
        }

        public override DbParameter CreateParameter(string parameterName, DbType dbType, int size)
        {
            SqlParameter parameter = new SqlParameter(parameterName, SqlDbType.NVarChar, size);
            parameter.DbType = dbType;
            return parameter;
        }

        public override DbParameter CreateParameter(string parameterName, DbType dbType, int size, string sourceColumn)
        {
            SqlParameter parameter = new SqlParameter(parameterName, SqlDbType.NVarChar, size, sourceColumn);
            parameter.DbType = dbType;
            return parameter;
        }

        public override DbParameter SCreateParameter(string parameterName, DbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            SqlParameter parameter = new SqlParameter(parameterName, SqlDbType.NVarChar, size, direction, isNullable, precision, scale, sourceColumn, sourceVersion, value);
            parameter.DbType = dbType;
            return parameter;
        }

        public override DbParameter CreateParameter(string parameterName, DbType dbType, int size, ParameterDirection direction, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, bool sourceColumnNullMapping, object value, string xmlSchemaCollectionDatabase, string xmlSchemaCollectionOwningSchema, string xmlSchemaCollectionName)
        {
            SqlParameter parameter = new SqlParameter(parameterName, SqlDbType.NVarChar, size, direction, precision, scale, sourceColumn, sourceVersion, sourceColumnNullMapping, value, xmlSchemaCollectionDatabase, xmlSchemaCollectionOwningSchema, xmlSchemaCollectionName);
            parameter.DbType = dbType;
            return parameter;
        }

        public override DbParameter[] CreateParametersArray(int size)
        {
            return new SqlParameter[size];
        }

        public override DbParameter[] CreateParametersArray(params object[] parameterValues)
        {
            if (parameterValues.Length % 2 != 0) throw new Exception("parameterValues is not invalid.");

            DbParameter[] resultParams = new SqlParameter[parameterValues.Length / 2];

            for (int i = 0, j = resultParams.Length, pos = 0; i <= j; i += 2, pos++)
            {
                resultParams[pos] = new SqlParameter(parameterValues[i].ToString(), parameterValues[i + 1]);
            }

            return resultParams;
        }

        public override System.Xml.XmlReader ExecuteXmlReader(CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            if (_Connection == null) throw new ArgumentNullException("connection");

            bool mustCloseConnection = false;
            DbCommand cmd = null;
            try
            {
                cmd = this.PrepareCommand(commandType, commandText, commandParameters, out mustCloseConnection);

                // Create the DataAdapter & DataSet
                XmlReader retval = ((SqlCommand)cmd).ExecuteXmlReader();

                ImportReturnValues(cmd);
                // Detach the SqlParameters from the command object, so they can be used again
                cmd.Parameters.Clear();

                return retval;
            }
            catch
            {
                if (mustCloseConnection)
                    _Connection.Close();
                throw;
            }
        }


        /// <summary>
        /// Create SQLCommand for the Insert
        /// </summary>
        /// <param name="pTable">table name</param>
        /// <param name="pListCol">column list apart by comma ("column 1, column 2, ...")</param>
        /// <returns>SqlCommand</returns>        
        public override DbCommand BuildInsert(string pTable, string[] columns)
        {
            SqlCommand InsertCmd = new SqlCommand();
            //string[] columns = pListCol.Split(',');
            string listColumns = string.Join(", ", columns);
            string updtxt = "insert into " + pTable + "(" + listColumns + ") Values (";
            for (int i = 0; i < columns.Length; i++)
            {
                updtxt = updtxt + "@" + columns[i] + ",";
            }
            InsertCmd.CommandText = updtxt.Trim(',') + ")";
            InsertCmd.Connection = this._Connection as SqlConnection;
            for (int i = 0; i < columns.Length; i++)
            {
                SqlParameter p = new SqlParameter();
                p.ParameterName = "@" + columns[i];
                p.SourceColumn = columns[i];
                InsertCmd.Parameters.Add(p);
            }
            return InsertCmd;
        }

        /// <summary>
        /// Careate SQLCommand for the Update
        /// </summary>
        /// <param name="pTable">table name</param>
        /// <param name="pListCol">column list apart by comma ("column 1, column 2, ...")</param>
        /// <param name="pKeys">primary keys list apart by comma ("key 1, key 2, ...")</param>
        /// <returns>SqlCommand</returns>       
        public override DbCommand BuildUpdate(string pTable, string[] columns, string[] keys)
        {
            SqlCommand UpdateCmd = new SqlCommand();

            //string[] keys = pKeys.Split(';');
            //string[] columns = pListCol.Split(',');
            string updtxt = "update " + pTable + " set ";
            for (int i = 0; i < columns.Length; i++)
            {
                updtxt = updtxt + columns[i] + " = @" + columns[i] + ",";
            }
            updtxt = updtxt.Trim(',') + " WHERE ";
            for (int i = 0; i < keys.Length; i++)
            {
                updtxt = updtxt + keys[i] + " = @Original_" + keys[i] + " AND ";
            }
            updtxt = updtxt.Remove(updtxt.Length - 4, 4);
            UpdateCmd.CommandText = updtxt;
            UpdateCmd.Connection = this._Connection as SqlConnection;
            for (int i = 0; i < columns.Length; i++)
            {
                SqlParameter p = new SqlParameter();
                p.ParameterName = "@" + columns[i];
                p.SourceColumn = columns[i];
                UpdateCmd.Parameters.Add(p);
            }
            for (int i = 0; i < keys.Length; i++)
            {
                SqlParameter po = new SqlParameter();
                po.ParameterName = "@Original_" + keys[i];
                po.SourceColumn = keys[i];
                po.SourceVersion = System.Data.DataRowVersion.Original;
                UpdateCmd.Parameters.Add(po);
            }
            return UpdateCmd;
        }

        /// <summary>
        /// Create SQLCommand for the Delete
        /// </summary>
        /// <param name="pTable">Tên table</param>
        /// <param name="pKeys">primary keys list apart by semicolon ("key 1; key 2; ...")</param>
        /// <returns>SqlCommand được build với câu lệnh DELETE</returns>      
        public override DbCommand BuildDelete(string pTable, string[] keys)
        {
            SqlCommand DeleteCmd = new SqlCommand();
            //string[] keys = pKeys.Split(';');
            string updtxt = "Delete From " + pTable + " WHERE ";
            for (int i = 0; i < keys.Length; i++)
            {
                updtxt = updtxt + keys[i] + " = @Original_" + keys[i] + " AND ";
            }
            updtxt = updtxt.Remove(updtxt.Length - 4, 4);
            DeleteCmd.CommandText = updtxt;
            DeleteCmd.Connection = this._Connection as SqlConnection;
            for (int i = 0; i < keys.Length; i++)
            {
                SqlParameter po = new SqlParameter();
                po.ParameterName = "@Original_" + keys[i];
                po.SourceColumn = keys[i];
                po.SourceVersion = System.Data.DataRowVersion.Original;
                DeleteCmd.Parameters.Add(po);
            }
            return DeleteCmd;
        }

        #endregion
    }
}
