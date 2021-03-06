// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ODBCDataAccess.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the ODBCDataAccess type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCoreDB.Helper
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.Odbc;

    [Serializable]
    public class ODBCDataAccess : DataAccessBase
    {
        // Provide class constructors
        public ODBCDataAccess() {
            this._Connection = new OdbcConnection();
        }
        public ODBCDataAccess(string connString)
        {           
            this.ConnectionString = connString;
            this._Connection = new OdbcConnection(connString);
        }

        public ODBCDataAccess(string connString, int commandTimeout)
        {
            this.ConnectionString = connString;
            this._CommandTimeout = commandTimeout;
            this._Connection = new OdbcConnection(connString);
        }

        #region Internal methods

        internal override DbConnection GetDataProviderConnection()
        {
            return new OdbcConnection();
        }

        internal override DbCommand GeDataProviderCommand()
        {
            OdbcCommand command = new OdbcCommand();
            if (this._CommandTimeout > 0)
                command.CommandTimeout = this._CommandTimeout;
            return command;            
        }

        internal override DbDataAdapter GetDataProviderDataAdapter()
        {
            OdbcDataAdapter da = new OdbcDataAdapter();
            da.RowUpdated += new OdbcRowUpdatedEventHandler(da_RowUpdated);
            return da;
        }

        internal void da_RowUpdated(object sender, OdbcRowUpdatedEventArgs e)
        {
            OnDataAdapterRowUpdated(sender, e);
        }       

        #endregion

        #region Public methods

        public override DbParameter[] CreateArrayParameters(int count)
        {
            return new OdbcParameter[count];
        }

        public override DbParameter CreateParameter()
        {
            return new OdbcParameter();
        }

        public override DbParameter CreateParameter(string parameterName, object value)
        {
            return new OdbcParameter(parameterName, value.ToString());
        }

        //public override DbParameter CreateParameter(string parameterName, DbType dbType)
        //{
        //    OdbcParameter parameter = new OdbcParameter(parameterName, OdbcType.NVarChar);
        //    parameter.DbType = dbType;
        //    return parameter;
        //}

        public override DbParameter CreateParameter(string parameterName, DbType dbType, int size)
        {
            OdbcParameter parameter = new OdbcParameter(parameterName, OdbcType.NVarChar, size);
            parameter.DbType = dbType;
            return parameter;
        }

        public override DbParameter CreateParameter(string parameterName, DbType dbType, int size, string sourceColumn)
        {
            OdbcParameter parameter = new OdbcParameter(parameterName, OdbcType.NVarChar, size, sourceColumn);
            parameter.DbType = dbType;
            return parameter;
        }

        public override DbParameter SCreateParameter(string parameterName, DbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            OdbcParameter parameter = new OdbcParameter(parameterName, OdbcType.NVarChar, size, direction, isNullable, precision, scale, sourceColumn, sourceVersion, value);
            parameter.DbType = dbType;
            return parameter;
        }

        public override DbParameter CreateParameter(string parameterName, DbType dbType, int size, ParameterDirection direction, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, bool sourceColumnNullMapping, object value, string xmlSchemaCollectionDatabase, string xmlSchemaCollectionOwningSchema, string xmlSchemaCollectionName)
        {
            OdbcParameter parameter = new OdbcParameter(parameterName, OdbcType.NVarChar, size, direction, precision, scale, sourceColumn, sourceVersion, sourceColumnNullMapping, value);
            parameter.DbType = dbType;
            return parameter;
        }

        public override DbParameter[] CreateParametersArray(int size)
        {
            return new OdbcParameter[size];
        }

        public override DbParameter[] CreateParametersArray(params object[] parameterValues)
        {
            if (parameterValues.Length % 2 != 0) throw new Exception("parameterValues is not invalid.");

            DbParameter[] resultParams = new OdbcParameter[parameterValues.Length / 2];

            for (int i = 0, j = resultParams.Length, pos = 0; i <= j; i += 2, pos++)
            {
                resultParams[pos] = new OdbcParameter(parameterValues[i].ToString(), parameterValues[i + 1]);
            }

            return resultParams;
        }

        public override System.Xml.XmlReader ExecuteXmlReader(CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            throw new ArgumentNullException("OdbcConnection don't support XmlReader method");            
        }


        /// <summary>
        /// Create OdbcCommand for the Insert
        /// </summary>
        /// <param name="pTable">table name</param>
        /// <param name="pListCol">column list apart by comma ("column 1, column 2, ...")</param>
        /// <returns>OdbcCommand</returns>        
        public override DbCommand BuildInsert(string pTable, string[] columns)
        {
            OdbcCommand InsertCmd = new OdbcCommand();
            //string[] columns = pListCol.Split(',');
            string listColumns = string.Join(", ", columns);
            string updtxt = "insert into " + pTable + "(" + listColumns + ") Values (";
            for (int i = 0; i < columns.Length; i++)
            {
                updtxt = updtxt + "@" + columns[i] + ",";
            }
            InsertCmd.CommandText = updtxt.Trim(',') + ")";
            InsertCmd.Connection = this._Connection as OdbcConnection;
            for (int i = 0; i < columns.Length; i++)
            {
                OdbcParameter p = new OdbcParameter();
                p.ParameterName = "@" + columns[i];
                p.SourceColumn = columns[i];
                InsertCmd.Parameters.Add(p);
            }
            return InsertCmd;
        }

        /// <summary>
        /// Careate OdbcCommand for the Update
        /// </summary>
        /// <param name="pTable">table name</param>
        /// <param name="pListCol">column list apart by comma ("column 1, column 2, ...")</param>
        /// <param name="pKeys">primary keys list apart by comma ("key 1, key 2, ...")</param>
        /// <returns>OdbcCommand</returns>       
        public override DbCommand BuildUpdate(string pTable, string[] columns, string[] keys)
        {
            OdbcCommand UpdateCmd = new OdbcCommand();

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
            UpdateCmd.Connection = this._Connection as OdbcConnection;
            for (int i = 0; i < columns.Length; i++)
            {
                OdbcParameter p = new OdbcParameter();
                p.ParameterName = "@" + columns[i];
                p.SourceColumn = columns[i];
                UpdateCmd.Parameters.Add(p);
            }
            for (int i = 0; i < keys.Length; i++)
            {
                OdbcParameter po = new OdbcParameter();
                po.ParameterName = "@Original_" + keys[i];
                po.SourceColumn = keys[i];
                po.SourceVersion = System.Data.DataRowVersion.Original;
                UpdateCmd.Parameters.Add(po);
            }
            return UpdateCmd;
        }

        /// <summary>
        /// Create OdbcCommand for the Delete
        /// </summary>
        /// <param name="pTable">Tên table</param>
        /// <param name="pKeys">primary keys list apart by semicolon ("key 1; key 2; ...")</param>
        /// <returns>OdbcCommand được build với câu lệnh DELETE</returns>      
        public override DbCommand BuildDelete(string pTable, string[] keys)
        {
            OdbcCommand DeleteCmd = new OdbcCommand();
            //string[] keys = pKeys.Split(';');
            string updtxt = "Delete From " + pTable + " WHERE ";
            for (int i = 0; i < keys.Length; i++)
            {
                updtxt = updtxt + keys[i] + " = @Original_" + keys[i] + " AND ";
            }
            updtxt = updtxt.Remove(updtxt.Length - 4, 4);
            DeleteCmd.CommandText = updtxt;
            DeleteCmd.Connection = this._Connection as OdbcConnection;
            for (int i = 0; i < keys.Length; i++)
            {
                OdbcParameter po = new OdbcParameter();
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
