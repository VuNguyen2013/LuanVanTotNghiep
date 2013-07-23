// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataConverter.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the StringArrayDataConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCoreDB.Helper
{

    #region [==== StringArrayDataConverter ====]

    #endregion

    #region [==== NullableDataConverter<T> ====]

    #endregion

    #region //==== object: Bool maps column: char(1) ====
    public class BoolCharConverter : IDataConverter
    {
        public object ConvertToObjectValue(object columnValue)
        {
            return (columnValue != null && columnValue.ToString() != "0");
        }

        public object ConvertToColumnValue(object objectValue)
        {
            return ((bool)objectValue) ? "1" : "0";
        }
    }
    #endregion
}
