// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DBHelper.cs" company="">
//   2010
// </copyright>
// <summary>
//   Defines the DBType type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCoreDB.Helper
{
    using System;

    public enum DBType
    {
        MSDB,
        FISDB,
        SBA
    }

    [Serializable]
    public class DBHelper
    {
        private DBType _dbType;
        private string _connString;
        private int _commandTimeout = -1;

        private DataAccessBase _DBInstance;

        public DBHelper(DBType type, string connString)
        {
            _dbType = type;
            _connString = connString;
        }

        public DBHelper(DBType type, string connString, int commandTimeout)
        {
            _dbType = type;
            _connString = connString;
            _commandTimeout = commandTimeout;
        }

        public DataAccessBase DBInstance
        {
            get
            {
                if (_DBInstance == null)
                {
                    switch (_dbType)
                    {
                        case DBType.MSDB:
                            _DBInstance = DataAccessFactory.GetDataAccessLayer(DataProviderType.Sql, _connString, _commandTimeout);
                            break;
                        case DBType.FISDB:
                            _DBInstance = DataAccessFactory.GetDataAccessLayer(DataProviderType.DB2, _connString, _commandTimeout);
                            break;
                        case DBType.SBA:
                            _DBInstance = DataAccessFactory.GetDataAccessLayer(DataProviderType.Informix, _connString, _commandTimeout);
                            break;
                        default:
                            _DBInstance = DataAccessFactory.GetDataAccessLayer(DataProviderType.OleDb, _connString, _commandTimeout);
                            break;
                    }
                }
                return _DBInstance;
            }
        }
    }
}

