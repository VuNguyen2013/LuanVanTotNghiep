// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommonEnums.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the CommonEnums type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCommon.Enums
{
    public class CommonEnums
    {
        public enum XRORDER_STATUS
        {
            ALL = 0,
            PENDING,
            PROCESSING,
            CANCELLED,//Canceled by customer or system
            FINISHED,
            REJECTED// Rejected by broker
        }

        public enum MARGIN_PORTFOLIO_TYPE
        {
            TYPE_2=' ',
            TYPE_4='P',
            TYPE_9='E',
            TYPE_12='R',
            TYPE_21='p',
            TYPE_42='U',
            TYPE_43='W',
            TYPE_49='D'
        }
        public enum ODD_LOT_ORDER_STATUS
        {
            ALL = 0,
            PENDING,
            PROCESSING,
            CANCELLED,//Canceled by customer or system
            FINISHED,
            REJECTED// Rejected by broker
        }

        /// <summary>
        /// Use for status of changing or cancelling orders.
        /// </summary>
        public enum CHANGED_ORDER_STATUS
        {
            PROCESSING = 1,
            ACCEPTED,
            REJECTED,
            CANCELLED
        }

        public enum BUY_RIGHT_STATUS
        {
            ALL = 0,
            PENDING,
            PROCESSING,
            CANCELLED,//Canceled by customer or system
            FINISHED,
            REJECTED// Rejected by broker
        }

        public enum STOCK_TRANSFER_TYPE
        {            
            STOCK_TRANSFER=0
        }        

        public enum STOCK_TRANSFER_STATUS
        {
            ALL = 0,
            PENDING,
            PROCESSING,
            CANCELLED,//Canceled by customer or system
            FINISHED,
            REJECTED// Rejected by broker
        }

        public enum CASH_TRANSFER_TYPE
        {
            CASH_WITHDRAWALS=0,
            CASH_TRANSFER
        }

        public enum CASH_TRANSFER_STATUS
        {
            ALL = 0,
            PENDING,
            PROCESSING,
            CANCELLED,//Canceled by customer or system
            FINISHED,
            REJECTED// Rejected by broker
        }

        public enum BANK_ACCOUNT_TYPE
        {          
            BANKACC = 0,   //RecieveType & PaymentType= 02        
            COMPACC       //RecieveType & PaymentType= 60
        } ;

        public enum ACCOUNT_TYPE
        {
            NORMAL = 0,
            MARGIN = 1,
            OTHER = 2
        } ;

        public enum MARKET_ID
        {
            HOSE = 1,
            HNX = 2,
            UPCoM = 3,
            INVALID = -1
        };

        public enum MARKET_SIGN
        {
            HOSE = 'O',
            HNX = 'N',
            UPCOM = 'C',
            ALL = 'A'
        };

        public enum STOCK_TYPE
        {
            STOCK = 'S',
            BOND = 'D',
            FUND = 'U',
        };

        public enum STATUS_STOCK
        {
            SUSPENSION = 'S',
            HALT = 'H',
            HALT_SESSION = 'A', //Bì ngưng giao dịch khớp lệnh
            HALT_PT = 'P', //Bị ngưng giao dịch thoà thuận
        };

        public enum STATUS_STOCK_INFOSHOW
        {
            NORMAL = 0,
            TEMP_SUSPENSION = 1,
            SUSPENSION = 2,
            BE_SERVEILANCED = 3,
            BE_WARNED = 4,
            SHARES_HOLDERS_MEETING = 5,
        };

        public enum LOCK_ACCOUNT_REASON
        {
            NOTHING = 0,
            WRONG_PASS,
            WRONG_PIN,
            BY_BROKER
        }

        public enum ORDER_SOURCE
        {
            FROM_WEB = 'W',
            FROM_IFIS_BROKER = 'B',
            FROM_IVR = 'I',
            FROM_SMS = 'S',
            FROM_MOBILE = 'M'
        };

        /// <summary>
        /// Order status will be returned by web services and then displaying on web.
        /// </summary>
        public enum ORDER_STATUS
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

        public enum ORDER_DETAIL_STATUS
        {
            OTHER = -1,
            // For order status
            NEW_CONDITION_ORDER = 1, //new condition order for next section
            CONDITION_ORDER_WAITING, //condition order is putted and waiting for process
            CHANGE_CONDITION_ORDER, //update condition order
            CANCEL_CONDITION_ORDER,
            CONDITION_ORDER_REJECTED,
            CONDITION_ORDER_ERROR,
            NEW_ORDER_WAITING,
            ORDER_SENDING,
            UPDATE_ORDER,
            UPDATE_ORDER_ACCEPTED,
            UPDATE_ORDER_REJECTED,
            CANCEL_ORDER,
            CANCEL_ORDER_ACCEPTED,
            CANCEL_ORDER_REJECTED,
            ORDER_ACCEPTED,
            ORDER_REJECTED,
            ORDER_ERROR, //Reject when error or unsent condition order.
            ORDER_MATCHED,
            CANCEL_AUTO_ACCEPTED,
            UPDATE_FROM_TOOL,
            UPDATE_FIS_VALUE,
            CANCEL_ORDER_NO_CORELATIVE_ORDER
        }

        public enum MARKET_STATUS
        {
            /// <summary>
            /// Value I
            /// </summary>
            INIT_APP = 'I',
            /// <summary>
            /// Value ' '
            /// </summary>
            UNVAILABLE = ' ',
            /// <summary>
            /// Value S
            /// </summary>
            READY = 'S',
            /// <summary>
            /// Value W
            /// </summary>
            WAITING = 'W',
            /// <summary>
            /// Value P
            /// </summary>
            PRE_OPEN = 'P', //Session 1 HOSE
            /// <summary>
            /// Value O
            /// </summary>
            OPEN = 'O', // HOSE, UPCOM, HNX
            /// <summary>
            /// Value A
            /// </summary>
            PRE_CLOSE = 'A', // Session 3 HOSE
            /// <summary>
            /// Value L
            /// </summary>
            PRE_CLOSE2 = 'L',
            /// <summary>
            /// Value C
            /// </summary>
            CLOSE = 'C',// HOSE, UPCOM, HNX
            /// <summary>
            /// Value K
            /// </summary>
            CLOSE_PT = 'K',
            /// <summary>
            /// Value H
            /// </summary>
            HAFT = 'H',/// <summary>
            /// Value U
            /// </summary>
            OPEN_2 = 'U', //UPCOM reopen.
            /// <summary>
            /// Value G
            /// </summary>
            CLOSING = 'G', // the last minute before market close
            /// <summary>
            /// Value R
            /// </summary>
            AFTER_CLOSE = 'R'
        };

        public enum REJECT_REASON
        {
            NOTHING = -1,
            MP_WITHOUT_CONTRA_SIDE = 0, //MP order without contra-side” 0
            ILLEGAL_PRICE_SPREAD = 1,		//Illegal price spread” 1
            INCORRECT_VOL = 2,				//Incorrect volume for specified board” 2
            MARKET_CLOSE = 3,				//Illegal request - Market Closed” 3
            INCORRECT_STOCK = 4,			//Incorrect Stock Symbol” 4
            INCORRECT_FIRM = 5,				//Incorrect Firm” 5
            INCORRECT_TRADER_ID = 6,		//Incorrect Trader ID” 6
            INCORRECT_CONFIRM_NO = 7,		//Incorrect confirm number” 7
            LATE_REQ_ACTION = 8,			//Too late to perform requested action” 8
            INCORRECT_REFER_NO = 9,			//Incorrect Reference Number” 9
            INCORRECT_CONDITION = 10,		//Incorrect Conditions” 10
            TRADING_HALT = 11,				//Trading halted in Stock” 11
            INCORRECT_BOARD = 12,			//Incorrect Board” 12
            MISSING_CLIENT_ID = 13,			//Security in DS - Missing Client ID” 13
            INCORRECT_ORDER_TYPE = 14,		//Incorrect Order Type” 14
            INCORRECT_FLAG = 15,				//Incorrect Port / Client flag” 15
            INCORRECT_CODE = 16,				//Incorrect Request Code or Reply Code” 16
            INCORRECT_SIDE = 17,				//Incorrect Side: must be Buy or Sell” 17
            INCORRECT_ORDER_NO = 18,			//Incorrect Order Number” 18
            INCORRECT_TIME = 19,				//Incorrect Time” 19
            INCORRECT_DATE = 20,				//Incorrect Date” 20
            NOT_DO_ODD_LOT_BOARD = 21,		//Cannot do on Odd-Lot board” 21
            INCORRECT_SUB_BROKER_ID = 22,	//Incorrect Sub-Broker ID” 22
            ILLEGAL_TRUSTEE_ID = 23,			//Illegal Trustee ID” 23
            SECURITY_SUSPEND = 24,			//Security suspended” 24
            MISSING_PC_FLAG = 25,			//Missing P/C Flag” 25
            MISSING_SUB_BROKER_ID = 26,		//Missing Sub-Broker ID” 26
            NO_VAILABLE_ROOM = 27,			//No available room for Thai Trust Fund” 27
            MARKET_INTERMISSION = 28,		//Market in Intermission” 28
            MARKET_HALT = 29,				//Market Halted” 29
            INCORRECT_PUB_VOL = 30,			//Incorrect Published Volume” 30
            DISALLOW_CHANGE_DEAL = 31,		//Changing Deal information disallowed” 31
            DISALLW_PUB_VOL = 32,			//Publish Vol disallowed at this time” 32
            DISALLOW_TRADING_STOCK = 33,		//Trading disallowed for this stock” 33
            PRICE_ABOVE_CEILING = 34,		//Incorrect price - above ceiling” 34
            PRICE_BELOW_FLOOR = 35,			//Incorrect price - below floor” 35
            PTHR_INCORRECT_FORMAT = 36,		//Put-Through price incorrect format” 36
            DISALLW_CANCEL_AUTOMATCH_DEAL = 37, //Cancel of automatch deal disallowed” 37
            PTHR_INCORRECT_VOL = 38,				//Incorrect Volume for Put-Through deal” 38
            INCORRECT_MARKET_MAKER = 39,			//Incorrect Market Maker” 39
            ILLEGAL_SHORT_SALES_ORDER = 40,		//Illegal Short Sales Order” 40
            ILLEGAL_MARKET_ID = 41,				//Illegal Market ID” 41
            ILLEGAL_MARKET_TYPE = 42,			//Illegal Message Type/Header” 42
            ILLEGAL_MESSAGE_LENGTH = 43,			//Illegal Message Length” 43
            PRICE_OVER = 71,				//Warning! Price over 10 %”
            DISAPPROVE_ORDER = 81,			//Disapprove Order”
            REJECT_FROM_FIS = 82,			//Reject form FIS”
            HALTED_TRADER_ID = 97,			//TraderID is halted.
            UNIDENTIFIED_ERROR = 99,		//Unidentified Error”
            INCORRECT_ACCOUNT_ID = 100,		//Not exist account
            NOT_ENOUGH_CASH = 101,				//Not enough cash to buy
            NOT_ENOUGH_STOCK = 102,				//Not enough stock to sale
            NOT_BUY_SELL_THE_SAME_STOCK = 103,	//Not allow buy/sell the same stock 10
            NOT_CANCEL_ORDER_FROM_DIFF_SOURCE = 104,	//Not allow cancel the order sent by difference source 104
            NOT_CANCEL_ATO_ATC = 105,				//Not allow cancel ATO and ATC. 105
            NOT_CANCEL_IN_THIS_PERIOD_PHASE = 106,	//Not allow cance the order in same period phase 106
            OVER_REMAIN_VOLUME = 107,				// Not allow buy, over volume for foreign investor. 107
            STOCK_IS_HALT = 108,					// Stock is halted. 108
            OVER_MAX_VOL = 109,					// Over maximum board volume 109
            NOT_ALLOW_TRADE_BONDS = 110,			// Not allow trading BONDS 110
            NOT_CANCEL_ORDER_CANCELED = 111,      // Order already put cancel, not allow cancel one 111

            /// <summary>
            /// This occurs when tradePrice not multiple of 100 (HOSE)
            /// </summary>
            ERROR_PRICE_NOT_MULTIPLE_100_FOR_HOSE = 112,

            /// <summary>
            /// This occurs when tradePrice not multiple of 500 (HOSE)
            /// </summary>
            ERROR_PRICE_NOT_MULTIPLE_500_FOR_HOSE = 113,

            /// <summary>
            /// This occurs when tradePrice not multiple of 1000 (HOSE)
            /// </summary>
            ERROR_PRICE_NOT_MULTIPLE_1000_FOR_HOSE = 114,

            /// <summary>
            /// This occurs when investor put order with condPrice is 'A' or 'C' for HNX
            /// </summary>
            ERROR_HNX_NOT_USE_ATO_ATC = 115,

            /// <summary>
            /// This occurs when tradePrice not multiple of 100 (HNX)
            /// </summary>
            ERROR_PRICE_NOT_MULTIPLE_100_FOR_HNX = 116,

            ERROR_LOCK_ACCOUNT = 117,

            ERROR_ACCOUNT_NOT_BUY_PERMISSION = 118,

            ERROR_ACCOUNT_NOT_SELL_PERMISSION = 119,

            ERROR_ACCOUNT_NOT_TRADE_PERMISSION = 120,

            ERROR_NOT_AVAILABLE_STOCK = 121,

            ERROR_MARKET_CLOSE = 122,

            ERROR_ATO_NOT_ALLOWED = 123,

            ERROR_ATC_NOT_ALLOWED = 124,

            /// <summary>
            /// </summary>
            NOT_CANCEL_ORDER_MATCHED = 125,

            ERROR_UPCOM_NOT_USE_ATO_ATC = 126,

            /// <summary>
            /// Step price of UPCOM
            /// </summary>
            ERROR_PRICE_NOT_MULTIPLE_100_FOR_UPCOM = 127,
            
            IS_VALID = 150,                        // Order is valid.

            /// <summary>
            /// This account does not have permission to put condition order
            /// </summary>
            ERROR_ACCOUNT_NOT_CONDITION_ORDER,

            /// <summary>
            /// This time is not in allowed time to put condition order.
            /// </summary>
            NOT_ADVANCE_TIME,
            ERROR_MARGIN_ACCOUNT_CANNOT_BUY_THAT_SYMBOL,
            ERROR_OVER_LIMIT_LOAN_PER_CUSTOMER,
            ERROR_OVER_LIMIT_LOAN_PER_SECSYMBOL,
            ERROR_OVER_LIMIT_COMPANY_CAPITAL,
            ERROR_OVER_LIMIT_MAX_BUY,
            ERROR_OVER_LIMIT_MAX_BUY_OF_SECSYMBOL,
            ERROR_PUT_ORDER_FAILED,
            ERROR_FOREIGN_ACCOUNT_CANNOT_BUY_THAT_SYMBOL,
            NO_RESPONSE,
            ERROR_MP_NOT_ALLOWED,
            NOT_LO_ORDER,
            NO_CORRELATIVE_ORDER,
            ERROR_CONDITION_PRICE_IS_ZERO,
            ERROR_MOK_NOT_ALLOWED,
            ERROR_MAK_NOT_ALLOWED,
            ILLEGAL_CONPRICE,
            ERROR_HOSE_NOT_USE_MAK_MOK,
            ERROR_NOT_ALLOW_UPDATE,
            ERROR_ODD_LOT_INVALID_VOLUME,
            ERROR_ODD_LOT_INVALID_CONPRICE
        }

        public enum RET_CODE_SMS
        {
            MISSING_PARAMETER=-1,
            ERROR_NETWORK=-2,
            ERROR_ACCOUNT=-3,
            ERROR_ACCOUNT_LOCK=-4,
            ERROR_ACCOUNT2=-5,
            ERROR_API=-6,
            ERROR_IP=-7,
            ERROR_SYSTEM=-8,
            ERROR_ACCOUNT_EMPTY=-9,
            ERROR_PHONE=-10,
            ERROR_PHONE_BACLIST=-11,
            ERROR_ACCOUNT_EMPTY2=-12
        }

        public enum RET_CODE
        {
            /// <summary>
            /// return if success
            /// </summary>
            SUCCESS = 0,

            /// <summary>
            /// return if fail
            /// </summary>
            FAIL = 1,

            /// <summary>
            /// return if a method throw a excetion
            /// </summary>
            SYSTEM_ERROR = 2,

            /// <summary>
            /// return if not found data 
            /// </summary>
            NO_EXISTED_DATA = 3,

            /// <summary>
            /// return if data is existing
            /// </summary>
            EXISTED_DATA = 4,

            /// <summary>
            /// return if accountId not existed in OTS DB
            /// </summary>
            ERROR_ACCOUNT = 5,

            /// <summary>
            /// Use for login. Incorrect username or password.
            /// </summary>
            INCORRECT_USER_PASSWORD = 6,

            /// <summary>
            /// Incorrect login password
            /// </summary>
            INCORRECTo_PASSWORD = 7,

            /// <summary>
            /// Old password is not match with new password
            /// </summary>
            PASSWORD_NOT_MATCH = 8,

            /// <summary>
            /// Password is empty
            /// </summary>
            PASSWORD_EMPTY = 9,

            /// <summary>
            /// Account has not loged in
            /// </summary>
            NOT_LOGIN = 10,

            /// <summary>
            /// Password is inactived
            /// </summary>
            PASSWORD_INACTIVED,

            /// <summary>
            /// Account is inactive
            /// </summary>
            ACCOUNT_INACTIVE,

            /// <summary>
            /// Send warning SMS because of login failed many times.
            /// </summary>
            SEND_WARNING_SMS,

            /// <summary>
            /// Show captchar 
            /// </summary>
            SHOW_CAPTCHA,

            /// <summary>
            /// Account is locked
            /// </summary>
            ACCOUNT_LOCKED,

            /// <summary>
            /// Not stock available
            /// </summary>
            ERROR_NOT_STOCK_AVAILABLE,

            /// <summary>
            /// Not cash available
            /// </summary>
            ERROR_NOT_CASH_AVAILABLE,

            IS_VALID,

            /// <summary>
            /// Permission to do something
            /// </summary>
            NOT_ALLOW,

            INCORRECT_PIN,

            /// <summary>
            /// Error at GW, not connected.
            /// </summary>
            ERROR_GW_NOT_CONNECTED,

            /// <summary>
            /// Error at GW, can not send the orders to GW
            /// </summary>
            ERROR_GW_NOT_SEND,

            /// <summary>
            /// The data is in incorrect format
            /// </summary>
            INCORRECT_FORMAT,

            ERROR_INVALID_CASH_ADVANCE,
            ERROR_NOT_ENOUGH_CASH_TO_ADVANCE,
            ERROR_CANNOT_ADVANCE_IN_DUE_DATE,
            ERROR_CANNOT_ADVANCE_OUTOF_TIME,
            ERROR_CANNOT_ADVANCE_FOR_TRADING_AT_AFTERNOON,
            ERROR_CANNOT_CANCEL_IN_PROCESSING,
            ERROR_CANNOT_CANCEL_ADVANCE_FINISHED,
            ERROR_CANNOT_CANCEL_ADVANCE_REFJECTED,
            ERROR_CANNOT_CANCEL_ADVANCE_CANCELED,
            /// <summary>
            /// The data is overlap with the compared data
            /// </summary>
            RANGE_OVERLAP,
            OUT_OF_NEXT_DAY_ORDER_TIME,
            ADVANCE_ORDER_STATUS_INCORRECT_STATE,

            ERROR_NOT_SAME_ACCOUNT,
            ERROR_REQUEST_AMOUNT,
            ERROR_INVALID_WITHDRAWAL,
            ERROR_CANNOT_CANCEL_CASH_TRANSFER,
            ERROR_CANNOT_CANCEL_STOCK_TRANSFER,
            ERROR_CANNOT_CANCEL_BUY_RIGHT,
            ERROR_CANNOT_CANCEL_ODD_LOT_ORDER,           
            ERROR_CANNOT_DELETE,
            ERROR_OVER_REQUEST_CAN_BUY_RIGHT,
            ERROR_NOT_EXIST_BUY_RIGHT,
            ERROR_REQUEST_VOLUME_BUY_RIGHT,
            ERROR_CANNOT_CANCEL_XRORDER,
            ERROR_NOT_EXIST_SUB_ACCOUNT,
            ERROR_EMPTY,
            ERROR_MIN_LENGTH,
            ERROR_MAX_LENGTH,
            ERROR_INVALID_DATETIME,
            ERROR_DEBT_ACCOUNT,
            INVALID_EE_RATIO,
            INCORECT_STATE,
            INCORECT_INFORMATION,
            ERROR_SENT_MESSAGE,
            CAN_NOT_CHANGE_PASS_RSA,
            ERROR_NOT_ENOUGH_CASH,
            INVAILID_API_KEY
        }

        public enum SEC_TYPE
        {
            /// <summary>
            /// Value 2
            /// </summary>
            SELLABLE_SHARE = 2, //Sellable Share
            /// <summary>
            /// Value 3
            /// </summary>
            NEW_SHARE = 3, //New Share
            /// <summary>
            /// Value 42
            /// </summary>
            WAIT_RECV = 42, ///Wait to Receive
            /// <summary>
            /// Value 43
            /// </summary>
            WAIT_SEND = 43, //Wait to Send
            /// <summary>
            /// Value 45
            /// </summary>
            NON_SELLABLE_BY_CREDIT = 45, //Non-Sellable by Credit
            /// <summary>
            /// Value 46
            /// </summary>
            NON_SELLABLE_BY_MORTGAGE = 46, //Non-Sellable by Mortgage
            /// <summary>
            /// Value 47
            /// </summary>
            NON_SELLABLE_BY_LENDING_MONEY = 47, //Non-Sellable by Lending Money
            /// <summary>
            /// Value 49
            /// </summary>
            LIMIT_TRANSFER = 49 //Limit Transfer (Silence Period)
        }

        public enum AUTHENTICATION_TYPE
        {
            PIN_PASS = 0,
            OTP_SMS = 1,
            OTP_EMAIL = 2,
            RSA = 3
        }  

        public enum SERVICE_TYPE
        {
            WEBTRADE = 'H',
        }

        public enum ORDER_TYPE
        {
            MATCHED = 'M',
            SEMI_MATCHED = 'm',
            CANCELLED = 'C',
            REJECTED = 'R'
        }

        public enum BROKER_TYPE
        {
            ADMIN = 1,
            BROKER
        }

        public enum BROKER_PERMISSIONS
        {
            /// <summary>
            /// Can view account register
            /// </summary>
            CAN_VIEW_ACCOUNT_REGISTER = 1,
            /// <summary>
            /// Can process account register
            /// </summary>
            CAN_PROCESS_ACCOUNT_REGISTER,

            /// <summary>
            /// permission can active main account
            /// </summary>
            CAN_ACTIVE,
            /// <summary>
            /// permission can edit main account
            /// </summary>
            CAN_EDIT,
            /// <summary>
            /// permission can change pass
            /// </summary>
            CAN_CHANGE_PASS,
            /// <summary>
            /// permission can change pin
            /// </summary>
            CAN_CHANGE_PIN,
            /// <summary>
            /// Can view cash advance
            /// </summary>
            CAN_VIEW_CASH_ADVANCE,
            /// <summary>
            /// Can process cash advance
            /// </summary>
            CAN_PROCESS_CASH_ADVANCE,
            /// <summary>
            /// Can view cash transfer/ with drawal
            /// </summary>
            CAN_VIEW_CASH_TRANSFER,
            /// <summary>
            /// Can process cash transfer/ with drawal
            /// </summary>
            CAN_PROCESS_CASH_TRANSFER,
            /// <summary>
            /// Can view stock transfer
            /// </summary>
            CAN_VIEW_STOCK_TRANSFER,
            /// <summary>
            /// Can process stock transfer
            /// </summary>
            CAN_PROCESS_STOCK_TRANSFER,
            /// <summary>
            /// Can view odd lot order
            /// </summary>
            CAN_VIEW_ODD_LOT_ORDER,
            /// <summary>
            /// Can process odd lot order
            /// </summary>
            CAN_PROCESS_ODD_LOT_ORDER,
            ///// <summary>
            ///// Can view buy right
            ///// </summary>
            //CAN_VIEW_BUY_RIGHT,
            ///// <summary>
            ///// Can process buy right
            ///// </summary>
            //CAN_PROCESS_BUY_RIGHT,
            /// <summary>
            /// Can view xrorder
            /// </summary>
            CAN_VIEW_XRORDER,
            /// <summary>
            /// Can process xrorder
            /// </summary>
            CAN_PROCESS_XRORDER,
            /// <summary>
            /// Can add news
            /// </summary>
            CAN_ADD_NEWS,
            /// <summary>
            /// Can add documents
            /// </summary>
            CAN_ADD_DOCUMENT,
            /// <summary>
            /// Can view history
            /// </summary>
            CAN_VIEW_HISTORY,
            /// <summary>
            /// Can receive sms new cash transfer
            /// </summary>
            RECEIVE_SMS_CASH_TRANSFER,
            /// <summary>
            /// Can process config AM web
            /// </summary>
            CAN_PROCESS_CONFIG
        }

        public enum FILTER_ORDER_STATUS
        {
            /// <summary>
            /// Get all order status
            /// </summary>
            ALL = 0,

            /// <summary>
            /// order was matched
            /// </summary>
            MATCHED = 1,

            /// <summary>
            /// order was cancelled
            /// </summary>
            CANCELLED = 2,

            /// <summary>
            /// order was rejected
            /// </summary>
            REJECTED = 3,

            /// <summary>
            /// order was changed
            /// </summary>
            CHANGED = 4,

            NOTMATCH = 5,
        }

        public enum SUB_ACCOUNT_PERMISSIONS
        {
            /// <summary>
            /// permission can trading from web.
            /// </summary>
            CAN_TRADE = 1,
            /// <summary>
            /// it's permission that allow user to use the website to buy stocks
            /// </summary>
            CAN_BUY = 2,
            /// <summary>
            /// It's permission that allow user to use the website to sell stocks
            /// </summary>
            CAN_SELL = 3,
            /// <summary>
            /// It's permission that allow user to use the website to send request for cash advance. 
            /// </summary>
            CASH_ADVANCE = 4,
            /// <summary>
            /// It's permission that allow user to use the website to send request for cash transfer. 
            /// </summary>
            CASH_TRANSFER = 5,
            /// <summary>
            /// It's permission that allow user to use the website to send request for stock transfer. 
            /// </summary>
            STOCK_TRANSFER = 6,
            /// <summary>
            /// It's permission that allow user to use the website to send request for sell odd lot.
            /// </summary>
            ODD_SLOT_EXCHANGE = 7,
            /// <summary>
            /// It's permission that allow user to use the website to send request for viewing the reseach and analyze documents
            /// </summary>
            VIEW_RESEARCH_ANALYZE = 8,
            /// <summary>
            /// It's permission that allow user to use the website to send request for viewing the order status. Default is yes
            /// </summary>
            VIEW_ORDER_STATUS = 9,
            /// <summary>
            /// It's permission that allow user to use the website to send request for viewing the statement. default is yes
            /// </summary>
            VIEW_STATMENT = 10,
            /// <summary>
            /// It's permission that allow user to use the website to send request for viewing the balance. default is yes
            /// </summary>
            VIEW_BALANCE = 11,
            /// <summary>
            /// It's permission that allow user to use the website to send request for buy/sell quikly. default is yes
            /// </summary>
            QUICK_ORDER = 12,
            /// <summary>
            /// It's permission that allow user to use the website to send request for buy/sell advance with condition. default is yes
            /// </summary>
            CONDITION_ORDER = 13,

            VIEW_INFORMATION_ACCOUNT = 14,

            PRICE_TO_BUY = 15,
        }

        public enum TRADE_RULE
        {
            /// <summary>
            /// Volume unit of HOSE is 10
            /// </summary>
            VOL_UNIT_HOSE = 10,

            /// <summary>
            /// Volume unit of HNX is 100
            /// </summary>
            VOL_UNIT_HNX = 100,

            /// <summary>
            /// Max allowed volume of HOSE is 19990
            /// </summary>
            VOL_MAX_HOSE = 19990,

            /// <summary>
            /// HNX Gateway limit volume
            /// </summary>
            VOL_MAX_HNX = 1000000,

            /// <summary>
            /// PRICE_LEVEL_1_HOSE
            /// </summary>
            PRICE_LEVEL_1_HOSE = 50000,

            /// <summary>
            /// PRICE_LEVEL_2_HOSE
            /// </summary>
            PRICE_LEVEL_2_HOSE = 100000,

            /// <summary>
            /// Step price for tradePrice less than 50,000 VND
            /// </summary>
            PRICE_STEP_LEVEL_1_HOSE = 100,

            /// <summary>
            /// Step price for tradePrice less than 100,000 VND 
            /// and price equal 50,000 VND or above 50,000 VND
            /// </summary>
            PRICE_STEP_LEVEL_2_HOSE = 500,

            /// <summary>
            /// Step price for tradePrice is over 100,000 VND
            /// </summary>
            PRICE_STEP_LEVEL_3_HOSE = 1000,

            /// <summary>
            /// Step price of HNX
            /// </summary>
            PRICE_STEP_HNX = 100,

            /// <summary>
            /// Money unit of HOSE
            /// </summary>
            MONEY_UNIT_HOSE = 1000,

            /// <summary>
            /// Money unit of HNX
            /// </summary>
            MONEY_UNIT_HNX = 1000,

            MONEY_UNIT_UPCOM = 1000,

            /// <summary>
            /// Step price of UPCOM
            /// </summary>
            PRICE_STEP_UPCOM = 100,

            VOL_UNIT_UPCOM = 100,
        };

        public enum TRADE_SIDE
        {
            BUY = 'B',

            SELL = 'S'
        }

        public enum CUSTOMER_TYPE
        {
            INTERNAL = 1,

            FOREIGN = 2,

            ORGANIZATION
        }

        public enum SESSION_KEY
        {
            PIN,
            PASSWORD,
            CUSTOMER_ACCOUNT,
            TRADING_ACCOUNT,
            CUSTOMER_TYPE,
            LIST_SUB_ACCOUNTS,
            STOCK_ORDERS,
            ADVANCE_ORDERS,
            API_KEY,
        }

        public enum SEARCH_BOOL
        {
            ALL = -1,
            FALSE,
            TRUE
        }

        public enum ORDER_SESSION
        {
            /*
             * HOSE, HNX, UPCOM: Real status: blank; State machine: blank
             */
            SESSION0 = '0',
            /*
             * HOSE, HNX, UPCOM: Real status: S; State machine: S
             */
            SESSION1 = '1',
            /*
             * HOSE: Real status: P; State machine: P
             * HNX, UPCOM: Real status: O; State machine: O
             */
            SESSION2 = '2',
            /*
             * HOSE: Real status: P; State machine: O
             * HNX: Real status: O; State machine: C
             * UPCOM: Real status: O; State machine: H
             */
            SESSION3 = '3',
            /*
             * HOSE: Real status: O; State machine: O
             * HNX: Real status: C; State machine: C
             * UPCOM: Real status: H; State machine: H
             */
            SESSION4 = '4',
            /*
             * HOSE: Real status: O; State machine: H
             * UPCOM: Real status: U; State machine: U
             */
            SESSION5 = '5',
            /*
             * HOSE: Real status: H; State machine: H
             * UPCOM: Real status: U; State machine: C
             */
            SESSION6 = '6',
            /*
             * HOSE: Real status: O; State machine: O
             * UPCOM: Real status: C; State machine: C
             */
            SESSION7 = '7',
            /*
             * HOSE: Real status: O; State machine: A
             */
            SESSION8 = '8',
            /*
             * HOSE: Real status: A; State machine: A
             */
            SESSION9 = '9',
            /*
             * HOSE: Real status: A; State machine: C
             */
            SESSION10 = 'A',
            /*
             * HOSE: Real status: C; State machine: C
             */
            SESSION11 = 'B',
            /*
             * HOSE: Real status: K; State machine: K
             */
            SESSION12 = 'C',

            /*
             * HOSE: Real status: Don't care; State machine: K
             * HNX, UPCOM: Real status: Don't care; State machine: C
             */
            SESSION13 = 'D',

            /// <summary>
            /// Hnx, Upcom. After Close: R
            /// </summary>
            SESSION14 = 'E',

            /// <summary>
            /// HNX, UPCOM. ClOSE 2
            /// </summary>
            SESSION15 = 'F',
        };

        public enum CORE_TRADE_PERMISSION
        {
            CANBUY = '1',

            CANSELL = '1',

            STATUS = 'N',
        }

        public enum ORDERHIST_SOURCE
        {
            /// <summary>
            /// Get Order history from FISDB
            /// </summary>
            FISDB = 0,

            /// <summary>
            /// Get Order history from SBA
            /// </summary>
            SBA = 1,

            /// <summary>
            /// Get Order history from OTSDB
            /// </summary>
            OTSDB_2,

            /// <summary>
            /// Get Order history from other source
            /// </summary>
            OTHER = 3
        }

        public enum DEALHIST_SOURCE
        {
            /// <summary>
            /// Get deal history from FISDB
            /// </summary>
            FISDB = 0,

            /// <summary>
            /// Get deal history from SBA
            /// </summary>
            SBA = 1,

            /// <summary>
            /// Get deal history from OTSDB
            /// </summary>
            OTSDB_2,

            /// <summary>
            /// Get deal history from other source
            /// </summary>
            OTHER = 3
        }

        public enum PERMISSION_TYPE
        {
            PORFOLIO = 1,

            VALIDATE_ORDER = 2
        }

        public enum ADVANCE_STATUS
        {
            ALL = 0,
            PENDING = 1,
            PROCESSING = 2,
            FINISHED = 3,
            CANCELLED = 4,
            REJECTED = 5
        }

        public enum CONFIGURATIONS
        {
            ADVFEERATIO = 0,

            ADVMINFEE,

            ADVVATRATIO ,

            TRADEFEERATIO,

            TRADEVATRATIO,

            PERCENT_WITH_DRAW,

            PERCENT_VAT,

            USD,

            IS_ENABLE_ODD_LOT_ORDER,

            IS_ENABLE_BUY_RIGHT
        }

        public enum RIGHTTYPE
        {
            RIGHT_TO_BUY = 0,
            STOCK_DIVIDENT = 1,
            STOCK_BONUS = 2,
            CASH_DIVIDENT = 3,
            ALL_RIGHT = 4,
        }

        public enum ADVANCE_TYPE
        {
            TRADING = 1,
            WITHDRAW
        }

        public enum FEE_TYPE
        { 
            FEE_TRADE=0,

            FEE_CASH_ADVANCE
        }

        public enum COND_PRICE
        {
            ATO = -1,
            ATC = -2,
            MP = -3,
            MAK = -4,
            MOK = -5
        }

        public enum ACTION_TYPE
        {
            CREATE = 0,
            LOGIN,
            LOGIN_FAILED,
            LOGOUT,
            CHANGE_PASSWORD,
            CHANGE_PIN,
            ACCOUNT_LOCKED,
            BROKER_CHANGE_INFORMATION,
            BROKER_ACTIVATE_ACCOUNT,
            BROKER_ACTIVATE_PIN,
            BROKER_ACTIVATE_PASS,
            BROKER_LOCK_ACCOUNT,
            BROKER_LOCK_PASS,
            BROKER_LOCK_PIN,
            BROKER_ACTIVATE_RSA,
            CUSTOMER_FORGET_PASS,
            CUSTOMER_FORGET_PIN
        }

        //Status of condition orders
        public enum CONDITION_ORDER_STATUS
        {
            WAITING = 0,
            ACTIVED,
            CANCELLED,
            EXPIRED,
            DONE,
            REJECTED
        }

        public enum ORDER_CON_PRICE
        {
            ATO = 'A',
            ATC = 'C',
            LO = ' ',
            MP = 'M',
            PT = 'P',
            MAK = 'K',
            MOK = 'O',
            SOB = 'S',
            OBO = 'B',
            PLO = 'P',
            SOS = '<',
            SOL = '>'
        }

        public enum CONDITION_ORDER_TYPE
        {
            NORMAL = 0,
            ATO,
            ATC,
            MP,
            MAK,
            MOK
        }

        public enum PUT_ORDER_SESSION
        {
            UNAVAILABLE = -1,
            TRADING_TIME_SESSION = 0,
            CLOSE_SESSION = 1
        }

        public enum TOOL_USING
        {
            WEB = 1,
            ETRADE_MONITOR = 2,
            MOBILE = 3
        }

        public enum PRICE_ORDER_TYPE
        {
            ATO = -1,
            ATC = -2,
            MP = -3,
            MAK = -4,
            MOK = -5
        }

        public enum CONDITION
        {
            NO_CONDITION = ' ',
            IOC = 'I',
            FOK = 'F',
            ODD = 'O'
        }
        public enum TRANS_TYPE
        {
            /// <summary>
            /// Value = 0
            /// </summary>
            TRANS_NEW,
            /// <summary>
            /// Value = 1
            /// </summary>
            TRANS_CANCEL,
            /// <summary>
            /// Value = 2
            /// </summary>
            TRANS_CHANGE_ACC,
            /// <summary>
            /// Value = 3
            /// </summary>
            TRANS_CANCEL_WITHOUT_APPRO
        }
       
    }
}
