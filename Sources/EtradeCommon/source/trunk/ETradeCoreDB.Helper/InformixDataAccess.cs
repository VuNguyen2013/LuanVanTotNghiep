// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InformixDataAccess.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the InformixDataAccess type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


using System;
using System.Data;
using System.Data.Common;
using IBM.Data.Informix;

namespace ETradeCoreDB.Helper
{
    [Serializable]
    public class InformixDataAccess : DataAccessBase
    {
        // Provide class constructors
        public InformixDataAccess() {
            this._Connection = new IfxConnection();
        }
        public InformixDataAccess(string connString)
        {           
            this.ConnectionString = connString;

            this._Connection = new IfxConnection(connString);               
        }

        public InformixDataAccess(string connString, int commandTimeout)
        {
            this.ConnectionString = connString;
            this._CommandTimeout = commandTimeout;
            this._Connection = new IfxConnection(connString);
        }
       
        #region Internal methods

        internal override DbConnection GetDataProviderConnection()
        {
            return new IfxConnection();
        }

        internal override DbCommand GeDataProviderCommand()
        {
            IfxCommand command = new IfxCommand();
            if (this._CommandTimeout > 0)
                command.CommandTimeout = this._CommandTimeout;
            return command;            
        }

        internal override DbDataAdapter GetDataProviderDataAdapter()
        {
            IfxDataAdapter da = new IfxDataAdapter();
            da.RowUpdated += new IfxRowUpdatedEventHandler(da_RowUpdated);
            return da;
        }

        internal void da_RowUpdated(object sender, IfxRowUpdatedEventArgs e)
        {
            OnDataAdapterRowUpdated(sender, e);
        }              

        #endregion

        #region Public methods

        public override DbParameter[] CreateArrayParameters(int count)
        {
            return new IfxParameter[count];
        }

        public override DbParameter CreateParameter()
        {
            return new IfxParameter();
        }

        public override DbParameter CreateParameter(string parameterName, object value)
        {
            return new IfxParameter(parameterName, value.ToString());
        }

        public override DbParameter CreateParameter(string parameterName, DbType dbType, int size)
        {
            IfxParameter parameter = new IfxParameter(parameterName, IfxType.VarChar, size);
            parameter.DbType = dbType;
            return parameter;
        }

        public override DbParameter CreateParameter(string parameterName, DbType dbType, int size, string sourceColumn)
        {
            IfxParameter parameter = new IfxParameter(parameterName, IfxType.VarChar, size, sourceColumn);
            parameter.DbType = dbType;
            return parameter;
        }

        public override DbParameter SCreateParameter(string parameterName, DbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            IfxParameter parameter = new IfxParameter(parameterName, IfxType.VarChar, size, direction, isNullable, precision, scale, sourceColumn, sourceVersion, value);
            parameter.DbType = dbType;
            return parameter;
        }

        public override DbParameter CreateParameter(string parameterName, DbType dbType, int size, ParameterDirection direction, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, bool sourceColumnNullMapping, object value, string xmlSchemaCollectionDatabase, string xmlSchemaCollectionOwningSchema, string xmlSchemaCollectionName)
        {            
            IfxParameter parameter = new IfxParameter(parameterName, IfxType.VarChar, size, direction,sourceColumnNullMapping, precision, scale, sourceColumn, sourceVersion, value);
            parameter.DbType = dbType;
            return parameter;
        }

        public override DbParameter[] CreateParametersArray(int size)
        {
            return new IfxParameter[size];
        }

        public override DbParameter[] CreateParametersArray(params object[] parameterValues)
        {
            if (parameterValues.Length % 2 != 0) throw new Exception("parameterValues is not invalid.");

            DbParameter[] resultParams = new IfxParameter[parameterValues.Length / 2];

            for (int i = 0, j = resultParams.Length, pos = 0; i <= j; i += 2, pos++)
            {
                resultParams[pos] = new IfxParameter(parameterValues[i].ToString(), parameterValues[i + 1]);
            }

            return resultParams;
        }

        public override System.Xml.XmlReader ExecuteXmlReader(CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            throw new ArgumentNullException("InformixConnection don't support XmlReader method"); 
            
        }


        /// <summary>
        /// Create IfxCommand for the Insert
        /// </summary>
        /// <param name="pTable">table name</param>
        /// <param name="pListCol">column list apart by comma ("column 1, column 2, ...")</param>
        /// <returns>IfxCommand</returns>        
        public override DbCommand BuildInsert(string pTable, string[] columns)
        {
            IfxCommand InsertCmd = new IfxCommand();
            //string[] columns = pListCol.Split(',');
            string listColumns = string.Join(", ", columns);
            string updtxt = "insert into " + pTable + "(" + listColumns + ") Values (";
            for (int i = 0; i < columns.Length; i++)
            {
                updtxt = updtxt + "@" + columns[i] + ",";
            }
            InsertCmd.CommandText = updtxt.Trim(',') + ")";
            InsertCmd.Connection = this._Connection as IfxConnection;
            for (int i = 0; i < columns.Length; i++)
            {
                IfxParameter p = new IfxParameter();
                p.ParameterName = "@" + columns[i];
                p.SourceColumn = columns[i];
                InsertCmd.Parameters.Add(p);
            }
            return InsertCmd;
        }

        /// <summary>
        /// Careate IfxCommand for the Update
        /// </summary>
        /// <param name="pTable">table name</param>
        /// <param name="pListCol">column list apart by comma ("column 1, column 2, ...")</param>
        /// <param name="pKeys">primary keys list apart by comma ("key 1, key 2, ...")</param>
        /// <returns>IfxCommand</returns>       
        public override DbCommand BuildUpdate(string pTable, string[] columns, string[] keys)
        {
            IfxCommand UpdateCmd = new IfxCommand();

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
            UpdateCmd.Connection = this._Connection as IfxConnection;
            for (int i = 0; i < columns.Length; i++)
            {
                IfxParameter p = new IfxParameter();
                p.ParameterName = "@" + columns[i];
                p.SourceColumn = columns[i];
                UpdateCmd.Parameters.Add(p);
            }
            for (int i = 0; i < keys.Length; i++)
            {
                IfxParameter po = new IfxParameter();
                po.ParameterName = "@Original_" + keys[i];
                po.SourceColumn = keys[i];
                po.SourceVersion = System.Data.DataRowVersion.Original;
                UpdateCmd.Parameters.Add(po);
            }
            return UpdateCmd;
        }

        /// <summary>
        /// Create IfxCommand for the Delete
        /// </summary>
        /// <param name="pTable">Tên table</param>
        /// <param name="pKeys">primary keys list apart by semicolon ("key 1; key 2; ...")</param>
        /// <returns>IfxCommand được build với câu lệnh DELETE</returns>      
        public override DbCommand BuildDelete(string pTable, string[] keys)
        {
            IfxCommand DeleteCmd = new IfxCommand();
            //string[] keys = pKeys.Split(';');
            string updtxt = "Delete From " + pTable + " WHERE ";
            for (int i = 0; i < keys.Length; i++)
            {
                updtxt = updtxt + keys[i] + " = @Original_" + keys[i] + " AND ";
            }
            updtxt = updtxt.Remove(updtxt.Length - 4, 4);
            DeleteCmd.CommandText = updtxt;
            DeleteCmd.Connection = this._Connection as IfxConnection;
            for (int i = 0; i < keys.Length; i++)
            {
                IfxParameter po = new IfxParameter();
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
