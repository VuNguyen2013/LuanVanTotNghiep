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
            CANCELLED=2,
            MATCHED=3
        }
        public enum ORDER_STATUS_CLIENT
        {
            NEW_ORDER = 1,
            CONFIRMED_FIS = 2,
            CONFIRMED_SET = 3,
            ORDER_REJECTED = 4,
            FULL_MATCHED = 5,
            SEMI_MATCHED = 6,
            NEW_CANCEL = 7,
            WAITING_CANCEL = 8,
            CANCELLED = 9,
            CANCEL_REJECTED = 10,
            OTHER,
        }
        public enum CASH_TRANSFER_STATUS
        {
            SUCCESS=1,
            ERROR = 2,
            NOT_ENOGH = 3
        }
        public enum STOCK_TRANSFER_STATUS
        {
            SUCCESS = 1,
            ERROR = 2,
            NOT_ENOGH = 3
        }
    }
}
