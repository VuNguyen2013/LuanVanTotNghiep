using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockCore.Common
{
    public class Enums
    {
        public enum PUT_ORDER_STATUS
        {
            SUCCESS = 0,
            PRICE_GREATER_CEIL=1,
            PRICE_LESS_FLOOR=2,
            NOT_IN_TIME=3,
            CORE_ERROR = 4,
            INVAILID_VOL=5,
            INVAILID_ACCOUNT=6,
            NOT_ENOUGH_BYCREDIT=7,
            NOT_ENOUGH_STOCK=8,
            INVAILID_STOCK_BALANCE=9
        }
        public enum RET_CODE
        {
            SUCCESS = 0,
            ERROR = 1,
            NO_EXIST_DATA=2
        }
        public enum ORDER_STATUS
        {
            ALL_MATCHED=0,
            PARTIAL_MATCHED = 1,
            WAITING_MATCH = 2,
        }
        public enum Side
        {
            BUY='B',
            SELL='S'
        }
        public enum MARKET_ID
        {
            HOSE=1,
            HNX=2,
            UPCOM=3
        }
        public enum DEDUCTION_STATUS
        {
            PROCESSED=0,
            NEW=1,
            CANCELLED=2
        }
    }
}
