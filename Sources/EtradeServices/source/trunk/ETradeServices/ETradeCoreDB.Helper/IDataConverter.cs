// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Data.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the IDataConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCoreDB.Helper
{
    public interface IDataConverter
    {
        /// <summary>
        /// Converts to object value.
        /// </summary>
        /// <param name="columnValue">The column value.</param>
        /// <returns></returns>
        object ConvertToObjectValue(object columnValue);

        /// <summary>
        /// Converts to column value.
        /// </summary>
        /// <param name="objectValue">The object value.</param>
        /// <returns></returns>
        object ConvertToColumnValue(object objectValue);
    }    
}
