using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETradeWebServices.Entities
{
    public class MChartData
    {
        public List<MMainMatchedPricesInfo> DetailMatched { get; set; }
        public List<MDealDetail> DealDetail { get; set; }
        public float PriorClosePrice { get; set; }
    }
    public class MMainMatchedPricesInfo
    {
        public float Price { get; set; }
        public float Volume { get; set; }
    }
    public class MDealDetail
    {
        public float Id { get; set; }
        public float Time { get; set; }
        public double Price { get; set; }
        public double Changed { get; set; }
        public float Vol { get; set; }
        public float Val { get; set; }
    }
    public class MDataTransactionRow
    {
        #region Property
        private string _StockSymbol;
        private float _Price;
        private float _Vol;
        private float _Val;
        private float _Highest;
        private float _Lowest;
        private int _Time;
        private string _Side;

        public string StockSymbol
        {
            get
            {
                if (_StockSymbol.Length > 3)
                    return _StockSymbol.Substring(0, 1) + _StockSymbol.Substring(2);
                return _StockSymbol;
            }
            set { _StockSymbol = value; }
        }

        public float Price
        {
            get { return _Price; }
            set { _Price = value; }
        }

        public float Vol
        {
            get { return _Vol; }
            set { _Vol = value; }
        }

        public float Val
        {
            get { return _Val; }
            set { _Val = value; }
        }

        public float Highest
        {
            get { return _Highest; }
            set { _Highest = value; }
        }

        public float Lowest
        {
            get { return _Lowest; }
            set { _Lowest = value; }
        }

        public int Time
        {
            get { return _Time; }
            set { _Time = value; }
        }

        public string Side
        {
            get { return _Side; }
            set { _Side = value; }
        }
        #endregion
        #region Methods
        public MDataTransactionRow()
        {            
        }
        public MDataTransactionRow(System.Data.Common.DbDataReader reader)
        {
            MappingData(reader);
        }
        protected void MappingData(System.Data.Common.DbDataReader reader)
        {
            try
            {
                _StockSymbol = (reader["StockSymbol"] != DBNull.Value) ? ((string)reader["StockSymbol"]).Trim() : "";
                _Price = (reader["Price"] != DBNull.Value) ? float.Parse(reader["Price"].ToString()) : 0;
                _Vol = (reader["Vol"] != DBNull.Value) ? float.Parse(reader["Vol"].ToString()) : 0;
                _Val = (reader["Val"] != DBNull.Value) ? float.Parse(reader["Val"].ToString()) : 0;
                _Highest = (reader["Highest"] != DBNull.Value) ? float.Parse(reader["Highest"].ToString()) : 0;
                _Lowest = (reader["Lowest"] != DBNull.Value) ? float.Parse(reader["Lowest"].ToString()) : 0;
                _Side = (reader["Side"] != DBNull.Value) ? (string)(reader["Side"].ToString()) : "";
                _Time = (reader["Time"] != DBNull.Value) ? int.Parse(reader["Time"].ToString()) : 0;

            }
            catch (Exception ex)
            {
                ETradeCommon.LogHandler.Log("page:MChartData, " + ex.Message, "MappingData", System.Diagnostics.TraceEventType.Error);
            }
        #endregion Methods
        }
    }
}