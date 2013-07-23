// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseDao.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the BaseDao type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCoreDB.Helper
{
    using ETradeCommon;

    /// <summary>
    /// Base class of Dao layer
    /// </summary>
    public class BaseDao
    {
        /// <summary>
        /// Creates the Ms DB instance.
        /// </summary>
        /// <returns>DataAccessBase class</returns>
        protected DataAccessBase CreateMSDBInstance()
        {
            var helper = new DBHelper(DBType.MSDB, AppConfig.MsDbConnectionString);

            return helper.DBInstance;
        }

        /// <summary>
        /// Creates the Ms DB instance.
        /// </summary>
        /// <returns>DataAccessBase class</returns>
        protected DataAccessBase CreateStockDBInstance()
        {
            var helper = new DBHelper(DBType.MSDB, AppConfig.MsDbConnectionString);

            return helper.DBInstance;
        }

        /// <summary>
        /// Creates the fis DB instance.
        /// </summary>
        /// <returns>Data Access Base</returns>
        protected DataAccessBase CreateFisDBInstance()
        {
            var helper = new DBHelper(DBType.FISDB, AppConfig.FisDbConnectionString);

            return helper.DBInstance;
        }

        /// <summary>
        /// Creates the sba DB instance.
        /// </summary>
        /// <returns>Data Access Base</returns>
        protected DataAccessBase CreateSbaDBInstance()
        {
            var helper = new DBHelper(DBType.SBA, AppConfig.SbaConnectionString);

            return helper.DBInstance;
        }
    }
}