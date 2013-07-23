// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InformixGenericDataAccess.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the InformixGenericDataAccess type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCoreDB.Helper
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Xml;

    public class InformixGenericDataAccess : DataAccessBase
    {
        readonly DbProviderFactory _factory = DbProviderFactories.GetFactory("IBM.Data.Informix");

        public InformixGenericDataAccess()
        {
            this._Connection = _factory.CreateConnection();
        }

        public InformixGenericDataAccess(string connString)
        {
            this.ConnectionString = connString;
            _Connection = _factory.CreateConnection();
            this._Connection.ConnectionString = connString;
        }

        public InformixGenericDataAccess(string connString, int commandTimeout)
        {
            this.ConnectionString = connString;
            this._Connection = _factory.CreateConnection();
            this._Connection.ConnectionString = connString;
            this._CommandTimeout = commandTimeout;
        }

        internal override DbConnection GetDataProviderConnection()
        {
            return this._factory.CreateConnection();
        }

        internal override DbCommand GeDataProviderCommand()
        {
            DbCommand dbCommand = this._factory.CreateCommand();
            if (this._CommandTimeout > 0)
            {
                dbCommand.CommandTimeout = this._CommandTimeout;
            }

            return dbCommand;
        }

        internal override DbDataAdapter GetDataProviderDataAdapter()
        {
            return this._factory.CreateDataAdapter();
        }

        public override DbParameter[] CreateArrayParameters(int count)
        {
            return new DbParameter[count];
        }

        public override DbParameter CreateParameter()
        {
            return this._factory.CreateParameter();
        }

        public override DbParameter CreateParameter(string parameterName, object avalue)
        {
            DbParameter parameter = this._factory.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = avalue;

            return parameter;
        }

        public override DbParameter CreateParameter(string parameterName, DbType dbType, int size)
        {
            DbParameter parameter = this._factory.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.DbType = dbType;
            parameter.Size = size;

            return parameter;
        }

        public override DbParameter CreateParameter(string parameterName, DbType dbType, int size, string sourceColumn)
        {
            DbParameter parameter = this.CreateParameter(parameterName, dbType, size);
            parameter.SourceColumn = sourceColumn;

            return parameter;
        }

        public override DbParameter SCreateParameter(string parameterName, DbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            throw new NotImplementedException();
        }

        public override DbParameter CreateParameter(string parameterName, DbType dbType, int size, ParameterDirection direction, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, bool sourceColumnNullMapping, object value, string xmlSchemaCollectionDatabase, string xmlSchemaCollectionOwningSchema, string xmlSchemaCollectionName)
        {
            throw new NotImplementedException();
        }

        public override DbParameter[] CreateParametersArray(int size)
        {
            return new DbParameter[size];
        }

        public override DbParameter[] CreateParametersArray(params object[] parameterValues)
        {
            if (parameterValues.Length % 2 != 0)
            {
                throw new Exception("parameterValues is not invalid.");
            }

            int paramLength = parameterValues.Length / 2;

            DbParameter[] resultParams = new DbParameter[paramLength];

            for (int i = 0, pos = 0; i <= paramLength; i += 2, pos++)
            {
                resultParams[pos] = this.CreateParameter(parameterValues[i].ToString(), parameterValues[i + 1]);
            }

            return resultParams;
        }

        public override DbCommand BuildInsert(string table, string[] columns)
        {
            throw new NotImplementedException();
        }

        public override DbCommand BuildUpdate(string table, string[] columns, string[] keys)
        {
            throw new NotImplementedException();
        }

        public override DbCommand BuildDelete(string table, string[] keys)
        {
            throw new NotImplementedException();
        }

        public override XmlReader ExecuteXmlReader(CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            throw new ArgumentNullException("InformixConnection don't support XmlReader method"); 
        }

        public override DbDataReader ExecuteDataReader(CommandType commandType, string commandText, DbParameter[] commandParameters)
        {
            bool mustCloseConnection = false;
            DbCommand cmd = null;
            try
            {
                cmd = PrepareCommand(commandType, commandText, commandParameters, out mustCloseConnection);

                DbDataReader dr;

                if (_Transaction == null)
                {
                    // Generate the reader. CommandBehavior.CloseConnection causes the
                    // the connection to be closed when the reader object is closed
                    cmd.ExecuteNonQuery();
                    dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
                else
                {
                    dr = cmd.ExecuteReader();
                }

                // giu lai cac returned value 
                ImportReturnValues(cmd);

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
                {
                    this.RollbackTransaction();
                }

                throw ex;
            }
        }
    }
}