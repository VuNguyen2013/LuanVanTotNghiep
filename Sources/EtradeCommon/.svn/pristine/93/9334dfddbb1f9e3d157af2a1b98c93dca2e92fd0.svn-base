// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DaoCommon.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the RowUpdatedEventHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCoreDB.Helper
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.Common;
    using System.Data.Odbc;
    using ETradeCommon;

    //using .Mware.Its.Commons;

    public delegate void RowUpdatedEventHandler(object sender, RowUpdatedEventArgs e);

    /// <summary>
    /// Giup ghi nhan output value 
    /// 29/7/2007
    /// 
    /// </summary>
    [Serializable]
    public class OutputParameterItem
    {
        public string ParameterName = null;
        public object Value = null;
    }


    /// <summary>
    /// doi tuong ket qua tra ve cua executeQuery
    /// 29/7/2007
    /// 
    /// </summary>
    [Serializable]
    public class OuputParameterItems
    {
        public Hashtable HashOutputVariableValues = new Hashtable();
        public int EffectedRows = 0;

        public OuputParameterItems() { }

        public void importReturnValues(DbCommand cmd)
        {
            if (cmd == null)
                return;

            //--> lay thong tin do chay store co' duoc            
            foreach (DbParameter paramenter in cmd.Parameters)
            {
                if (paramenter.Direction == ParameterDirection.InputOutput || paramenter.Direction == ParameterDirection.Output ||
                    paramenter.Direction == ParameterDirection.ReturnValue)
                {
                    OutputParameterItem outPutVar = new OutputParameterItem();
                    outPutVar.ParameterName = paramenter.ParameterName.Replace("@", "").ToLower();
                    outPutVar.Value = paramenter.Value;
                    this.HashOutputVariableValues.Add(outPutVar.ParameterName, outPutVar);
                }
            }
        }

        /// <summary>
        /// Cac store co the tra value thong qua lenh RETURN 
        /// </summary>
        /// <returns></returns>
        public object getReturnValue()
        {
            return this.getParameterValue("RETURN_VALUE");
        }

        /// <summary>
        /// lay ra gia tri cua 1 bien OUTPUT duoc tra ve
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public object getParameterValue(string parameterName)
        {
            OutputParameterItem parameter = (OutputParameterItem)HashOutputVariableValues[parameterName.ToLower()];
            if (parameter != null)
                return parameter.Value;

            return null;
        }
    }

    [Serializable]
    public static class DaoCommon
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

        public static string GetFieldStringValue(DataRow drData, string fieldName)
        {
            if (drData[fieldName] == null)
                return "";
            else
                return drData[fieldName].ToString().Trim();
        }

        public static string GetFieldStringValue(OdbcDataReader dataReader, string fieldName)
        {
            if (dataReader[fieldName] == null)
                return "";
            else
                return dataReader[fieldName].ToString().Trim();
        }

        /// <summary>
        /// Get value for Price, money 
        /// </summary>
        /// <param name="drData">datarow</param>
        /// <param name="fieldName">the name of field need get</param>
        /// <returns>return decimal value</returns>
        public static Decimal GetFieldDecimalValue(DataRow drData, string fieldName)
        {
            if (drData[fieldName] == null || drData[fieldName].ToString().Trim() == "")
                return 0;
            else
                return Decimal.Parse(drData[fieldName].ToString().Trim());
        }

        public static char CharacterConverter(DataRow drow, string colName)
        {
            object obj = drow[colName];
            if (obj == DBNull.Value)
                return ' ';

            return Convert.ToChar(obj);
        }

        public static DateTime DatetimeConverter(DataRow drow, string colName)
        {
            object obj = drow[colName];
            if (obj == DBNull.Value)
                return new DateTime(1900, 1, 1);

            return Convert.ToDateTime(obj);
        }

        /// <summary>
        /// Get value for Price, money 
        /// </summary>
        /// <param name="dataReader">datarow</param>
        /// <param name="fieldName">the name of field need get</param>
        /// <returns>return decimal value</returns>
        public static Decimal GetFieldDecimalValue(OdbcDataReader dataReader, string fieldName)
        {
            if (dataReader[fieldName] == null || dataReader[fieldName].ToString().Trim() == "")
                return 0;
            else
                return Decimal.Parse(dataReader[fieldName].ToString().Trim());
        }

        /// <summary>
        /// Get value for Price, money 
        /// </summary>
        /// <param name="drData">datarow</param>
        /// <param name="fieldName">the name of field need get</param>
        /// <returns>return double value</returns>
        public static Double GetFieldDoubleValue(DataRow drData, string fieldName)
        {
            if (drData[fieldName] == null || drData[fieldName].ToString().Trim() == "")
                return 0;
            else
                return Double.Parse(drData[fieldName].ToString().Trim());
        }

        /// <summary>
        /// get value have integer type
        /// </summary>
        /// <param name="drData"></param>
        /// <param name="fieldName">the name of field</param>
        /// <returns>return int value</returns>
        public static int GetFieldIntegerValue(DataRow drData, string fieldName)
        {
            if (drData[fieldName] == null || drData[fieldName].ToString().Trim() == "")
                return 0;
            else
                return int.Parse(drData[fieldName].ToString().Trim());
        }

        /// <summary>
        /// get value have integer type
        /// </summary>
        /// <param name="drData"></param>
        /// <param name="fieldName">the name of field</param>
        /// <returns>return int value</returns>
        public static int GetFieldIntegerValue(OdbcDataReader drData, string fieldName)
        {
            if (drData[fieldName] == null || drData[fieldName].ToString().Trim() == "")
                return 0;
            else
                return int.Parse(drData[fieldName].ToString().Trim());
        }

        /// <summary>
        /// get value have DateTime type
        /// </summary>
        /// <param name="drData"></param>
        /// <param name="fieldName">the name of field</param>
        /// <returns>return DateTime value</returns>
        public static DateTime GetFieldDateTimeValue(DataRow drData, string fieldName)
        {
            if (drData[fieldName] == null || drData[fieldName].ToString().Trim() == "")
                return new DateTime();
            else
                return DateTime.Parse(drData[fieldName].ToString().Trim());
        }

        /// <summary>
        /// get value have DateTime type
        /// </summary>
        /// <param name="drData"></param>
        /// <param name="fieldName">the name of field</param>
        /// <returns>return DateTime value</returns>
        public static DateTime GetFieldDateTimeValue(OdbcDataReader drData, string fieldName)
        {
            if (drData[fieldName] == null || drData[fieldName].ToString().Trim() == "")
                return new DateTime();
            else
                return DateTime.Parse(drData[fieldName].ToString().Trim());
        }

        /// <summary>
        /// get value have integer type
        /// </summary>
        /// <param name="drData"></param>
        /// <param name="fieldName">the name of field</param>
        /// <returns>return int16 value</returns>
        public static Int16 GetFieldShortValue(DataRow drData, string fieldName)
        {
            if (drData[fieldName] == null || drData[fieldName].ToString().Trim() == "")
                return 0;
            else
                return Int16.Parse(drData[fieldName].ToString().Trim());
        }

        /// <summary>
        /// get value have integer type
        /// </summary>
        /// <param name="drData"></param>
        /// <param name="fieldName">the name of field</param>
        /// <returns>return long value</returns>
        public static long GetFieldLongValue(DataRow drData, string fieldName)
        {
            if (drData[fieldName] == null || drData[fieldName].ToString().Trim() == "")
                return 0;
            else
                return long.Parse(drData[fieldName].ToString().Trim());
        }

        public static string XMLEncode(string Value)
        {
            return Value.Replace("&", "&amp;").Replace("'", "").Replace("\"", "").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\\", "");
        }

        /// <summary>
        /// get value have boolean type
        /// </summary>
        /// <param name="drData"></param>
        /// <param name="fieldName">the name of field</param>
        /// <returns>return bool value</returns>
        public static bool GetFieldBoolValue(DataRow drData, string fieldName)
        {
            if (drData[fieldName] == null || drData[fieldName].ToString().Trim() == "")
                return false;
            else
                return bool.Parse(drData[fieldName].ToString().Trim());
        }

        /// <summary>
        /// get value have boolean type
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="fieldName">the name of field</param>
        /// <returns>return bool value</returns>
        public static bool GetFieldBoolValue(OdbcDataReader dataReader, string fieldName)
        {
            if (dataReader[fieldName] == null || dataReader[fieldName].ToString().Trim() == "")
                return false;
            else
                return bool.Parse(dataReader[fieldName].ToString().Trim());
        }
    }    
}
