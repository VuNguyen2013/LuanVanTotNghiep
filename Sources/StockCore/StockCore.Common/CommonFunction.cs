using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockCore.Common
{
    public class CommonFunction
    {
        public static string ConvertToSqlDateTime(DateTime input)
        {
            return input.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
