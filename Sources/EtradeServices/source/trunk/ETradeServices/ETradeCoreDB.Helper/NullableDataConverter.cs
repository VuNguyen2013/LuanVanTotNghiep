// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullableDataConverter.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the NullableDataConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCoreDB.Helper
{
    using System;

    public class NullableDataConverter<T> :
        IDataConverter where T : struct
    {
        public object ConvertToObjectValue(
            object columnValue)
        {
            return (T?)columnValue;
        }

        public object ConvertToColumnValue(
            object objectValue)
        {
            if (!(objectValue is Nullable<T>))
            {
                throw new ArgumentException();
            }
            return (T)objectValue;
        }
    }
}