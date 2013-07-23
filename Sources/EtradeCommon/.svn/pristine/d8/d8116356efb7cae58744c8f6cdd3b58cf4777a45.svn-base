// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConstructorMap.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the ConstructorMap type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCoreDB.Helper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    //using .Mware.Its.Commons.Repository;

    public class ConstructorMap
    {
        Dictionary<String, IDataConverter> _columnConverters = new Dictionary<string, IDataConverter>();

        public IDataConverter this[string columnName]
        {
            get { return _columnConverters[columnName]; }
            set { _columnConverters[columnName] = value; }
        }

        public string[] ColumnNames
        {
            get
            {
                string[] names = new string[_columnConverters.Keys.Count];
                _columnConverters.Keys.CopyTo(names, 0);
                return names;
            }
        }

        public ConstructorMap() { }

        public ConstructorMap(params string[] columnNames)
        {
            foreach (string columnName in columnNames)
            {
                _columnConverters.Add(columnName, null);
            }
        }

        public ConstructorMap(object[] maps)
        {
            for (int i = 0; i + 1 < maps.Length; i += 2)
            {
                _columnConverters.Add((string)maps[i], (IDataConverter)maps[i + 1]);
            }
        }

        public ConstructorMap(object[] baseMaps, object[] secondMaps)
        {
            for (int i = 0; i + 1 < baseMaps.Length; i += 2)
            {
                _columnConverters.Add((string)baseMaps[i], (IDataConverter)baseMaps[i + 1]);
            }

            for (int i = 0; i + 1 < secondMaps.Length; i += 2)
            {
                _columnConverters.Add((string)secondMaps[i], (IDataConverter)secondMaps[i + 1]);
            }
        }

        public ConstructorMap(Dictionary<String, IDataConverter> columns)
        {
            foreach (KeyValuePair<String, IDataConverter> pair in columns)
            {
                _columnConverters.Add(pair.Key, pair.Value);
            }
        }

        public void AddColumnConverter(string columnName, IDataConverter converter)
        {
            if (_columnConverters.ContainsKey(columnName) == false)
            {
                AddColumnName(columnName, converter);
            }
            else
            {
                _columnConverters[columnName] = converter;
            }
        }

        public void AddColumnName(string columnName)
        {
            AddColumnName(columnName, null);
        }

        public void AddColumnName(string columnName, IDataConverter converter)
        {
            _columnConverters.Add(columnName, converter);
        }

        public AbstractDataObject CreateFilledObject(Type objectType, IDataReader data)
        {
            ArrayList objectParameters = new ArrayList();

            foreach (string columnName in ColumnNames)
            {
                IDataConverter converter = this[columnName];
                object value = data[columnName];
                if (value == DBNull.Value)
                {
                    value = null;
                }
                if (converter != null)
                {
                    value = converter.ConvertToObjectValue(value);
                }
                objectParameters.Add(value);
            }

            return (AbstractDataObject)Activator.CreateInstance(objectType, objectParameters.ToArray());
        }

        public static AbstractDataObject CreateFilledObject(Type objectType, IDataReader data, int[] cols)
        {
            ArrayList objectParameters = new ArrayList();
            foreach (int col in cols)
            {
                object value = data[col];
                if (value == DBNull.Value)
                    value = null;
                objectParameters.Add(value);
            }

            return (AbstractDataObject)Activator.CreateInstance(objectType, objectParameters.ToArray());
        }

        /// <summary>
        /// startColl: inclusive; endColl: exclusive
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="data"></param>
        /// <param name="startCol"></param>
        /// <param name="endCol"></param>
        /// <returns></returns>
        public static AbstractDataObject CreateFilledObject(Type objectType, IDataReader data, int startCol, int endCol)
        {
            ArrayList objectParameters = new ArrayList();
            for (int col = startCol; col < endCol; col++)
            {
                object value = data[col];
                if (value == DBNull.Value)
                    value = null;
                objectParameters.Add(value);
            }

            return (AbstractDataObject)Activator.CreateInstance(objectType, objectParameters.ToArray());
        }
    }
}
