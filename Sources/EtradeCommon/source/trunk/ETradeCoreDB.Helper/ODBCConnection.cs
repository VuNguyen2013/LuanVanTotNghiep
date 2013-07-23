// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ODBCConnection.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the ODBCConnection type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCoreDB.Helper
{
    using System.Data.Odbc;
    using ETradeCommon;

    public static class ODBCConnection
    {
        public static OdbcConnection Connect()
        {
            OdbcConnection conn;
            string connectString = "Dsn=" + AppConfig.AliasInformix;

            try
            {
                conn = new OdbcConnection(connectString);
                conn.Open();
                return conn;
            }
            catch (OdbcException e)
            {
                ExceptionHandler.HandleException(e, Constants.EXCEPTION_POLICY);
                return null;
            }
        }//Connect

        public static void DisConnect(OdbcConnection conn, OdbcDataReader dataReader)
        {
            try
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }
            catch (OdbcException e)
            {
                ExceptionHandler.HandleException(e, Constants.EXCEPTION_POLICY);
            }
        }//Disconnect
    }
}
