#region

using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Xml;
using RTDataServices;

#endregion

namespace RTWebService.Utils
{
    public class Common
    {
        #region AccountType enum

        public enum ACCOUNT_TYPE
        {
            ADMIN = 1,
            BROKER = 2,
            INVESTOR = 3
        } ;

        #endregion

        #region ActionType enum

        public enum ACTION_TYPE
        {
            LOGON,
            LOGOUT,
            NEW_ORDER,
            CANCEL_ORD,
            CHANGE_ACC
        }

        #endregion

        #region DATA_ACTION_TYPE enum

        public enum DATA_ACTION_TYPE
        {
            ADD = 1,
            UPDATE = 2,
            REMOVE = 3
        }

        #endregion

        #region INFO_TYPE enum

        public enum INFO_TYPE
        {
            STOCK = 1,
            MARKET = 2
        }

        #endregion

        #region MARKET_STATUS enum

        public enum MARKET_STATUS
        {
            PRE_OPEN = 'P',
            OPEN = 'O',
            PRE_CLOSE = 'A',
            CLOSE = 'C',
            UNVAILABLE = ' ',
            CLOSE_PT = 'K',
            INIT_APP = 'I',
            READY = 'S',
            WAITING = 'W',
            HAFT = 'H',
        } ;

        #endregion

        #region MarketId enum

        public enum MARKET_ID
        {
            HOSE = 1,
            HASTC = 2,
            UPCoM = 3,
            INVALID = -1
        } ;

        #endregion

        #region MarketTime enum

        public enum MARKET_TIME
        {
            PREOPEN = 83000,
            OPEN = 84500,
            PRECLOSE = 103000,
            PUTTHROUGTH = 104500,
            CLOSE = 110000
        }

        #endregion

        #region NoticeType enum

        public enum NOTICE_TYPE
        {
            ACTIVE_QUERY,
            END_TRADING_TIME,
            REAL_TIME,
            NOT_DEFINED,
        }

        #endregion

        #region ORDER_SESSION enum

        public enum ORDER_SESSION
        {
            SESSION0 = '0',
            SESSION1 = '1',
            SESSION2 = '2',
            SESSION3 = '3',
            SESSION4 = '4',
            SESSION5 = '5',
            SESSION6 = '6',
            SESSION7 = '7',
            SESSION8 = '8',
        } ;

        #endregion

        #region OrderSource enum

        public enum OrderSource
        {
            FROM_WEB = 'W',
            FROM_BROKER = 'B',
            FROM_IVR = 'I',
            FROM_SMS = 'S'
        } ;

        #endregion

        #region REJECT_CODE enum

        public enum REJECT_CODE
        {
            MP_WITHOUT_CONTRA_SIDE = 0, //MP order without contra-side” 0
            ILLEGAL_PRICE_SPREAD, //Illegal price spread” 1
            INCORRECT_VOL, //Incorrect volume for specified board” 2
            MARKET_CLOSE, //Illegal request - Market Closed” 3
            INCORRECT_STOCK, //Incorrect Stock Symbol” 4
            INCORRECT_FIRM, //Incorrect Firm” 5
            INCORRECT_TRADER_ID, //Incorrect Trader ID” 6
            INCORRECT_CONFIRM_NO, //Incorrect confirm number” 7
            LATE_REQ_ACTION, //Too late to perform requested action” 8
            INCORRECT_REFER_NO, //Incorrect Reference Number” 9
            INCORRECT_CONDITION, //Incorrect Conditions” 10
            TRADING_HALT, //Trading halted in Stock” 11
            INCORRECT_BOARD, //Incorrect Board” 12
            MISSING_CLIENT_ID, //Security in DS - Missing Client ID” 13
            INCORRECT_ORDER_TYPE, //Incorrect Order Type” 14
            INCORRECT_FLAG, //Incorrect Port / Client flag” 15
            INCORRECT_CODE, //Incorrect Request Code or Reply Code” 16
            INCORRECT_SIDE, //Incorrect Side: must be Buy or Sell” 17
            INCORRECT_ORDER_NO, //Incorrect Order Number” 18
            INCORRECT_TIME, //Incorrect Time” 19
            INCORRECT_DATE, //Incorrect Date” 20
            NOT_DO_ODD_LOT_BOARD, //Cannot do on Odd-Lot board” 21
            INCORRECT_SUB_BROKER_ID, //Incorrect Sub-Broker ID” 22
            ILLEGAL_TRUSTEE_ID, //Illegal Trustee ID” 23
            SECURITY_SUSPEND, //Security suspended” 24
            MISSING_PC_FLAG, //Missing P/C Flag” 25
            MISSING_SUB_BROKER_ID, //Missing Sub-Broker ID” 26
            NO_VAILABLE_ROOM, //No available room for Thai Trust Fund” 27
            MARKET_INTERMISSION, //Market in Intermission” 28
            MARKET_HALT, //Market Halted” 29
            INCORRECT_PUB_VOL, //Incorrect Published Volume” 30
            DISALLOW_CHANGE_DEAL, //Changing Deal information disallowed” 31
            DISALLW_PUB_VOL, //Publish Vol disallowed at this time” 32
            DISALLOW_TRADING_STOCK, //Trading disallowed for this stock” 33
            PRICE_ABOVE_CEILING, //Incorrect price - above ceiling” 34
            PRICE_BELOW_FLOOR, //Incorrect price - below floor” 35
            PTHR_INCORRECT_FORMAT, //Put-Through price incorrect format” 36
            DISALLW_CANCEL_AUTOMATCH_DEAL, //Cancel of automatch deal disallowed” 37
            PTHR_INCORRECT_VOL, //Incorrect Volume for Put-Through deal” 38
            INCORRECT_MARKET_MAKER, //Incorrect Market Maker” 39
            ILLEGAL_SHORT_SALES_ORDER, //Illegal Short Sales Order” 40
            ILLEGAL_MARKET_ID, //Illegal Market ID” 41
            ILLEGAL_MARKET_TYPE, //Illegal Message Type/Header” 42
            ILLEGAL_MESSAGE_LENGTH, //Illegal Message Length” 43
            PRICE_OVER = 71, //Warning! Price over 10 %”
            DISAPPROVE_ORDER = 81, //Disapprove Order”
            REJECT_FROM_FIS = 82, //Reject form FIS”
            HALTED_TRADER_ID = 97, //TraderID is halted.
            UNIDENTIFIED_ERROR = 99, //Unidentified Error”
            INCORRECT_ACCOUNT_ID = 100, //Not exist account
            NOT_ENOUGH_CASH, //Not enough cash to buy
            NOT_ENOUGH_STOCK, //Not enough stock to sale
            NOT_BUY_SELL_THE_SAME_STOCK, //Not allow buy/sell the same stock 10
            NOT_CANCEL_ORDER_FROM_DIFF_SOURCE, //Not allow cancel the order sent by difference source 104
            NOT_CANCEL_ATO_ATC, //Not allow cancel ATO and ATC. 105
            NOT_CANCEL_IN_THIS_PERIOD_PHASE, //Not allow cance the order in same period phase 106
            OVER_REMAIN_VOLUME, // Not allow buy, over volume for foreign investor. 107
            STOCK_IS_HALT, // Stock is halted. 108
            OVER_MAX_VOL, // Over maximum board volume 109
            NOT_ALLOW_TRADE_BONDS, // Not allow trading BONDS 110
            NOT_CANCEL_ORDER_CANCELED, // Order already put cancel, not allow cancel one 111
            IS_VALID = 150 //Valid
        } ;

        #endregion

        #region RET_CODE enum

        public enum RET_CODE
        {
            SUCCESS = 0,
            NO_EXIST_DATA,
            SYSTEM_ERROR,
            ERROR_ACCOUNT,
            ERROR_SENDMAIL,
            ERROR_LOGOUT,
            ERROR_OTP,
            ERROR_SECRETQUESION,
            ERROR_SECRETANSWER,
            ERROR_SESSION,
            ERROR_CHANGEPASS,
            ERROR_ADDACCOUNT,
            ERROR_DELETEACCOUNT,
            ERROR_INACTIVE,
            ERROR_PIPECONNECTION,
            ERROR_CANCELORDER,
            ERROR_CHANGEORDER,
            ERROR_ORDERPIN,
            ERROR_CONNECTION,
            ERROR_EXISTEDCARDID,
            ERROR_LOCKEDACCOUNT,
            ERROR_USER_INPUT,
            ERROR_BROKERHASINVESTOR,
            ERROR_BROKERNOTEXIST
        } ;

        #endregion

        #region SEC_TYPE enum

        public enum SEC_TYPE
        {
            GOVERMENT_BOND = 'G',
            GOVERMENT_BOND_FR = 'H',
        } ;

        #endregion

        #region SOURCE_TYPE enum

        public enum SOURCE_TYPE
        {
            LOCAL,
            NETWORK,
        }

        #endregion

        #region SourceId enum

        public enum SourceId
        {
            FROM_FIS = 0,
            FROM_SET = 3
        } ;

        #endregion

        #region StatusType enum

        public enum StatusType
        {
            MATCHED = 'M',
            CANCELLED = 'C',
            REJECTED = 'R'
        } ;

        #endregion

        #region TRADE_RULE enum

        public enum TRADE_RULE
        {
            VOL_UNIT_HOSE = 10,
            VOL_UNIT_HASTC = 100,
            VOL_MAX_HOSE = 19990,
            VOL_MAX_HASTC = 49900,
            PRICE_LEVEL_1_HOSE = 50000,
            PRICE_LEVEL_2_HOSE = 100000,
            PRICE_STEP_LEVEL_1_HOSE = 100,
            PRICE_STEP_LEVEL_2_HOSE = 500,
            PRICE_STEP_LEVEL_3_HOSE = 1000,
            PRICE_STEP_HASTC = 100,
            MONEY_UNIT_HOSE = 1000,
            MONEY_UNIT_HASTC = 1000,
        } ;

        #endregion

        #region TransType enum

        public enum TransType
        {
            TRANS_NEW = 0,
            TRANS_CANCEL,
            TRANS_CHANGE
        }

        #endregion

        #region UpcomTradingTime enum

        public enum UpcomTradingTime
        {
            OPEN = 83000,
            BEGIN_HAFT_TIME = 113000,
            END_HAFT_TIME = 133000,
            CLOSE = 150000
        }

        #endregion

        #region UserType enum

        public enum UserType
        {
            USER_ALL = 'A',
            USER_WEB = 'W',
            USER_IVR = 'I',
        }

        #endregion

        public const int TRADERID_LEN = 11;
        public const int ACCOUNT_LEN = 10;
        public const int SECSYMBOL_LEN = 8;
        public const int USER_LEN = 20;
        public const int MESSAGE_TYPE_LEN = 2;
        public const int REFORDERID_LEN = 64;
        public const int PASSWORD_LEN = 8;
        public const int SESSION_LEN = 20;

        public const string HALT_FLAG = "H";
        public const Decimal MONEY_UNIT = 1000;
        //public const Decimal PRICE_UNIT = 1000;
        public const int STOCK_CODE_LENGTH = 3;

        public const int INVALID = -1;
        public const double DIV_NUMBER = 100.0;
        public const double PRICE_UNIT = 1000.0;
        public const int VOLUME_UNIT = 10;

        //constant for SMS services.
        public const string SMS_COMPANY_NAME_ID = "TCSC";
        public const int SMS_MESSAGE_LENGTH = 160;
        public const string HOSE_MARKET = "HO";
        public const string HNX_MARKET = "HNX";
        public const string UPCOM_MARKET = "UPCOM";
        public const string TCSC_ACCOUNT_HEADER = "085C";

        // Broker permission
        public const short PERMISSION_SEND_SMS = 1;

        public const string DATE_FORMAT_CODE = "dd/MM/yyyy hh:mm:ss";
        public const string DATE_FORMAT_SQL = "{0:yyyy-MM-dd hh:mm:ss tt}";

        public const int HOSE_VOLUME_UNIT = 10;
        public const int HNX_VOLUME_UNIT = 100;
        public const int UPCOM_VOLUME_UNIT = 100;
        private static readonly string _serviceName = ReadFromWebConfig("serviceName");
        public static string RunMode = ReadFromWebConfig("RunMode");
        private static readonly string LOG_DIR = ReadFromWebConfig("LogDir");

        private static readonly Boolean _enableSendMail =
            Boolean.Parse(ReadFromWebConfig("enableSendErrorReportByEmail"));

        private static readonly string _mailToAddress = ReadFromWebConfig("receivedErrorReportAddr");

        //Create the XML object to return.
        public static XmlDocument XMLObjResult(string xmlString, RET_CODE retCode)
        {
            string content = "";

            try
            {
                switch (retCode)
                {
                    case RET_CODE.NO_EXIST_DATA:
                    case RET_CODE.SYSTEM_ERROR:
                    case RET_CODE.ERROR_ACCOUNT:
                    case RET_CODE.ERROR_CANCELORDER:
                    case RET_CODE.ERROR_CHANGEORDER:
                    case RET_CODE.ERROR_CHANGEPASS:
                    case RET_CODE.ERROR_INACTIVE:
                    case RET_CODE.ERROR_LOGOUT:
                    case RET_CODE.ERROR_PIPECONNECTION:
                    case RET_CODE.ERROR_ORDERPIN:
                    case RET_CODE.ERROR_OTP:
                    case RET_CODE.ERROR_SECRETQUESION:
                    case RET_CODE.ERROR_SECRETANSWER:
                    case RET_CODE.ERROR_SENDMAIL:
                    case RET_CODE.ERROR_ADDACCOUNT:
                    case RET_CODE.ERROR_SESSION:
                    case RET_CODE.ERROR_CONNECTION:
                        xmlString = "<message>" + retCode + "</message>";

                        break;
                    default:
                        break;
                }

                content = "<result code=\"" + (int) retCode + "\">" +
                          xmlString +
                          "</result>";

                XmlDocument retXMLDoc = new XmlDocument();

                retXMLDoc.LoadXml(content);

                return retXMLDoc;
            }

            catch (Exception e)
            {
                content = "<result code=\"" + (int) RET_CODE.SYSTEM_ERROR + "\">" +
                          "<message>" + RET_CODE.SYSTEM_ERROR + e.Message + "</message>" +
                          "</result>";

                XmlDocument retXMLDoc = new XmlDocument();

                retXMLDoc.LoadXml(content);

                return retXMLDoc;
            }
        }

        //Create the XML object to return.
        public static XmlDocument XMLObjResult(string xmlString, REJECT_CODE rejCode)
        {
            string content = "";

            try
            {
                xmlString = "<message>" + rejCode + "</message>";

                content = "<result code=\"" + (int) rejCode + "\">" +
                          xmlString +
                          "</result>";

                XmlDocument retXMLDoc = new XmlDocument();

                retXMLDoc.LoadXml(content);

                return retXMLDoc;
            }

            catch (Exception e)
            {
                content = "<result code=\"" + (int) RET_CODE.SYSTEM_ERROR + "\">" +
                          "<message>" + RET_CODE.SYSTEM_ERROR + e.Message + "</message>" +
                          "</result>";

                XmlDocument retXMLDoc = new XmlDocument();

                retXMLDoc.LoadXml(content);

                return retXMLDoc;
            }
        }

        // Convert any structures to byte[].
        public static byte[] SerializeExact(object anything)
        {
            int structSize = Marshal.SizeOf(anything);
            IntPtr buffer = Marshal.AllocHGlobal(structSize);

            Marshal.StructureToPtr(anything, buffer, false);

            byte[] streamData = new byte[structSize];

            Marshal.Copy(buffer, streamData, 0, structSize);
            Marshal.FreeHGlobal(buffer);

            return streamData;
        }

        // Convert byte[] to any structures.
        public static object RawDeserialize(byte[] rawData, Type anyType)
        {
            int rawSize = Marshal.SizeOf(anyType);

            if (rawSize > rawData.Length) return null;

            IntPtr buffer = Marshal.AllocHGlobal(rawSize);

            Marshal.Copy(rawData, 0, buffer, rawSize);

            object retObject = Marshal.PtrToStructure(buffer, anyType);

            Marshal.FreeHGlobal(buffer);

            return retObject;
        }

        public static byte[] GetBytes(string data)
        {
            return Encoding.ASCII.GetBytes(data);
        }

        public static string GetString(byte[] data)
        {
            return Encoding.ASCII.GetString(data);
        }

        public static MARKET_ID MarketID(string market)
        {
            switch (market)
            {
                case HOSE_MARKET:
                    return MARKET_ID.HOSE;
                case HNX_MARKET:
                    return MARKET_ID.HASTC;
                case UPCOM_MARKET:
                    return MARKET_ID.UPCoM;
                default:
                    return MARKET_ID.INVALID;
            }
        }

        public static string MarketID(MARKET_ID marketID)
        {
            switch (marketID)
            {
                case MARKET_ID.HOSE:
                    return HOSE_MARKET;
                case MARKET_ID.HASTC:
                    return HNX_MARKET;
                case MARKET_ID.UPCoM:
                    return UPCOM_MARKET;
                default:
                    return "";
            }
        }

        public static string Value(DataRow row, string sFieldName)
        {
            string sValue = row[sFieldName].ToString();

            return (sValue == "") ? INVALID.ToString() : sValue;
        }

        public static string FixValue(string sValue)
        {
            return (sValue == INVALID.ToString()) ? "0" : sValue;
        }


        public static string FixedTime(string sTime) //HH:MM:SS
        {
            return sTime.Substring(0, 2) + sTime.Substring(3, 2) + sTime.Substring(6, 2);
        }

        public static Int32 Int32Value(string str)
        {
            try
            {
                return Int32.Parse(str.Trim());
            }
            catch (Exception)
            {
                return INVALID;
            }
        }

        public static Int64 Int64Value(string str)
        {
            try
            {
                return Int64.Parse(str.Trim());
            }
            catch (Exception)
            {
                return INVALID;
            }
        }

        public static Double DoubleValue(string str)
        {
            try
            {
                return Double.Parse(str.Trim(), CultureInfo.InvariantCulture.NumberFormat);
            }
            catch (Exception)
            {
                return INVALID;
            }
        }

        public static String PriceValue(double value)
        {
            return String.Format("{0:0.0}", value);
        }

        public static String IndexValue(double value)
        {
            return String.Format("{0:0.00}", value);
        }

        public static String VolumeValue(long value)
        {
            return String.Format("{0:0,0}", value);
        }

        public static String MoneyValue(decimal value)
        {
            return String.Format("{0:0,0.##}", value);
        }

        public static String StringValue(string str)
        {
            try
            {
                return str.Trim();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string GetString(byte x)
        {
            byte[] s = new byte[1];

            s[0] = x;

            return GetString(s);
        }

        public static DateTime GetDate(string data)
        {
            DateTime date;

            try
            {
                date = DateTime.Parse(data);
            }
            catch
            {
                date = DateTime.Now;
            }

            return date;
        }

        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyyMMdd-HHmmss");
        }

        public static string GetShortDate()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }

        public static string SMSDate()
        {
            return DateTime.Now.ToString("dd/MM/yyyy");
        }

        public static int DateTime2Int(DateTime dt)
        {
            return dt.Hour*10000 + dt.Minute*100 + dt.Second;
        }

        public static string SMSDateT(string dateT, Decimal Value, string delimit)
        {
            if (Value > 0)
            {
                return dateT + "+" + MoneyValue(Value).Trim() + delimit;
            }
            else if (Value < 0)
            {
                return dateT + MoneyValue(Value).Trim() + delimit;
            }

            return null;
        }


        public static byte[] GetSubArrary(byte[] source, int position, int length)
        {
            byte[] dest = new byte[length];

            for (int index = 0; index < length; index++)
            {
                dest[index] = source[position + index];
            }

            return dest;
        }

        public static bool IsTradingTime()
        {
            if (RunMode == "TEST")
            {
                return true;
            }

            DateTime currentTime = DateTime.Now;

            return (currentTime.DayOfWeek != DayOfWeek.Saturday &&
                    currentTime.DayOfWeek != DayOfWeek.Sunday &&
                    currentTime.Hour > 7 && currentTime.Hour < 12);
        }

        public static bool IsTradingTimeOfUPCoM()
        {
            if (RunMode == "TEST")
            {
                return true;
            }

            DateTime currentTime = DateTime.Now;

            return (currentTime.DayOfWeek != DayOfWeek.Saturday &&
                    currentTime.DayOfWeek != DayOfWeek.Sunday &&
                    ((currentTime.Hour > 9 && currentTime.Hour < 16) ||
                     (currentTime.Hour == 9 && (currentTime.Minute >= 30))));
        }

        public static void Log(string market, string sErrMsg)
        {
            bool isNotOpened = true;

            string sLogFormat = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " ==> ";
            string logFile = "RTDataServices-" + DateTime.Now.ToString("ddMMyyyy") + ".log";

            string sCurrentDir = LOG_DIR;

            while (isNotOpened)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(sCurrentDir + "\\" + _serviceName + logFile, true);
                    sw.WriteLine(market + ": " + sLogFormat + sErrMsg);
                    sw.Flush();
                    sw.Close();

                    isNotOpened = false;
                }
                catch
                {
                    Thread.Sleep(5);
                }
            }

            SendErrorReport(sErrMsg);
        }

        public static void Log(string sErrMsg)
        {
            bool isNotOpened = true;

            string sLogFormat = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " ==> ";
            string logFile = "RTDataServices-" + DateTime.Now.ToString("ddMMyyyy") + ".log";

            string sCurrentDir = LOG_DIR;

            while (isNotOpened)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(sCurrentDir + "\\" + _serviceName + logFile, true);
                    sw.WriteLine(sLogFormat + sErrMsg);
                    sw.Flush();
                    sw.Close();

                    isNotOpened = false;
                }
                catch
                {
                    Thread.Sleep(5);
                }
            }

            SendErrorReport(sErrMsg);
        }

        public static DataTable getDifferentRecords(DataTable dtFirst, DataTable dtSecond)
        {
            //Create Empty Table  
            DataTable dtResult = new DataTable("dtResult");

            dtResult = dtSecond.Clone();

            //If dtFirst Row not in dtSecond, Add to dtResult.  
            dtResult.BeginLoadData();
            int minRows = 0;
            if (dtFirst.Rows.Count <= dtSecond.Rows.Count)
            {
                minRows = dtFirst.Rows.Count;
            }
            else
            {
                minRows = dtSecond.Rows.Count;
            }
            for (int rowIndex = 0; rowIndex < minRows; rowIndex++)
            {
                for (int colIndex = 0; colIndex < dtFirst.Columns.Count; colIndex++)
                {
                    if (dtFirst.Rows[rowIndex][colIndex].ToString() != dtSecond.Rows[rowIndex][colIndex].ToString())
                    {
                        dtResult.LoadDataRow(dtSecond.Rows[rowIndex].ItemArray, false);

                        break;
                    }
                }
            }
            if (minRows < dtSecond.Rows.Count)
            {
                for (int rowIndex = minRows; rowIndex < dtSecond.Rows.Count; rowIndex++)
                {
                    dtResult.LoadDataRow(dtSecond.Rows[rowIndex].ItemArray, false);
                }
            }
            dtResult.EndLoadData();

            return dtResult;
        }

        public static void Log(string sErrMsg, bool enableSendmail)
        {
            bool isNotOpened = true;

            string sLogFormat = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " ==> ";

            //string sCurrentDir = @"C:\Windows\Temp\";
            string sCurrentDir = LOG_DIR;
            while (isNotOpened)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(sCurrentDir + "\\" + _serviceName + "RTDataServices.log", true);
                    sw.WriteLine(sLogFormat + sErrMsg);
                    sw.Flush();
                    sw.Close();

                    isNotOpened = false;
                }
                catch
                {
                    Thread.Sleep(5);
                }
            }

            if (enableSendmail)
            {
                SendErrorReport(sErrMsg);
            }
        }


        public static void LogTransaction(string sTransMsg)
        {
            bool isNotOpened = true;

            string sDate = GetShortDate();
            string sTime = DateTime.Now.ToLongTimeString();
            //string sCurrentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //string sCurrentDir = @"C:\Windows\Temp\";
            string sCurrentDir = LOG_DIR;

            while (isNotOpened)
            {
                try
                {
                    StreamWriter sw =
                        new StreamWriter(sCurrentDir + "\\RTDataServices-Transaction-Log-" + sDate + ".csv", true);
                    sw.WriteLine(sTime + "," + sTransMsg);
                    sw.Flush();
                    sw.Close();

                    isNotOpened = false;
                }
                catch
                {
                    Thread.Sleep(5);
                }
            }
        }

        public static string ReadFromWebConfig(string key)
        {
            NameValueCollection config = (NameValueCollection) ConfigurationSettings.GetConfig("appSettings");
            if (config != null)
            {
                return config[key];
            }
            return null;
        }

        public static void WriteToWebConfig(string key, string value)
        {
            NameValueCollection config = (NameValueCollection) ConfigurationSettings.GetConfig("appSettings");

            if (config != null)
            {
                config[key] = value;
            }
        }

        public static string XMLConform(string input)
        {
            return XmlConvert.EncodeName(input);
        }

        public static string MapAccountNo(String IVRAccountNo)
        {
            return IVRAccountNo + "1";
        }

        public static string GetErrorMessage(string errorCode)
        {
            switch (errorCode)
            {
                case "00":
                    return "MP order without contra-side";
                case "01":
                    return "Illegal price spread";
                case "02":
                    return "Incorrect volume for specified board";
                case "03":
                    return "Illegal request - Market Closed";
                case "04":
                    return "Incorrect Stock Symbol";
                case "05":
                    return "Incorrect Firm";
                case "06":
                    return "Incorrect Trader ID";
                case "07":
                    return "Incorrect confirm number";
                case "08":
                    return "Too late to perform requested action";
                case "09":
                    return "Incorrect Reference Number";
                case "10":
                    return "Incorrect Conditions";
                case "11":
                    return "Trading halted in Stock";
                case "12":
                    return "Incorrect Board";
                case "13":
                    return "Security in DS - Missing Client ID";
                case "14":
                    return "Incorrect Order Type";
                case "15":
                    return "Incorrect Port / Client flag";
                case "16":
                    return "Incorrect Request Code or Reply Code";
                case "17":
                    return "Incorrect Side: must be Buy or Sell";
                case "18":
                    return "Incorrect Order Number";
                case "19":
                    return "Incorrect Time";
                case "20":
                    return "Incorrect Date";
                case "21":
                    return "Cannot do on Odd-Lot board";
                case "22":
                    return "Incorrect Sub-Broker ID";
                case "23":
                    return "Illegal Trustee ID";
                case "24":
                    return "Security suspended";
                case "25":
                    return "Missing P/C Flag";
                case "26":
                    return "Missing Sub-Broker ID";
                case "27":
                    return "No available room for Thai Trust Fund";
                case "28":
                    return "Market in Intermission";
                case "29":
                    return "Market Halted";
                case "30":
                    return "Incorrect Published Volume";
                case "31":
                    return "Changing Deal information disallowed";
                case "32":
                    return "Publish Vol disallowed at this time";
                case "33":
                    return "Trading disallowed for this stock";
                case "34":
                    return "Incorrect price - above ceiling";
                case "35":
                    return "Incorrect price - below floor";
                case "36":
                    return "Put-Through price incorrect format";
                case "37":
                    return "Cancel of automatch deal disallowed";
                case "38":
                    return "Incorrect Volume for Put-Through deal";
                case "39":
                    return "Incorrect Market Maker";
                case "40":
                    return "Illegal Short Sales Order";
                case "41":
                    return "Illegal Market ID";
                case "42":
                    return "Illegal Message Type/Header";
                case "43":
                    return "Illegal Message Length";
                case "71":
                    return "Warning! Price over 10 %";
                case "81":
                    return "Disapprove Order";
                case "82":
                    return "Reject form FIS";
                case "99":
                    return "Unidentified Error";
                default:
                    return "";
            }
        }

        public static string DecodeUtf8(string s_Input)
        {
            byte[] u8_Utf = new byte[s_Input.Length];

            for (int i = 0; i < s_Input.Length; i++)
            {
                // If there are characters above 255 it is IMPOSSIBLE that it is an UTF8 string.
                // It is already in Unicode format, there is nothing to do!

                if (s_Input[i] > 255)
                    return s_Input;

                u8_Utf[i] = (byte) s_Input[i];
            }

            return Encoding.UTF8.GetString(u8_Utf);
        }

        public static string getOrderStatus(string OrderStatus)
        {
            switch (OrderStatus)
            {
                case "A":
                case "MAC":
                case "OA":
                case "x":
                case "X":
                case "XA":
                case "XC":
                case "XAC":
                case "C":
                case "O":
                case "UO":
                case "UX":
                case "U":
                    return "Cancel";
                case "SD":
                case "CD":
                case "R":
                case "RC":
                case "D":
                    return "Reject";
                case "MD":
                case "m":
                case "M":
                case "MA":
                    return "Match";
                case "PO":
                case "POA":
                case "PX":
                case "PXC":
                case "DS":
                case "DC":
                case "XS":
                case "XB":
                    return "NotMatch";


                    //case "OAC": return "Change"; break;
                    // case "OC": return "Change"; break;
                    // case "MC": return "Change"; break;

                default:
                    return string.Empty;
            }
        }

        public static Decimal FormatToDecimal(string valuesDecimal)
        {
            return ((string.IsNullOrEmpty(valuesDecimal) || valuesDecimal.Trim().ToLower() == "null")
                        ? 0
                        : Convert.ToDecimal(valuesDecimal));
        }

        public static Int32 FormatToInt32(string valuesInt32)
        {
            return ((string.IsNullOrEmpty(valuesInt32) || valuesInt32.Trim().ToLower() == "null")
                        ? 0
                        : Convert.ToInt32(valuesInt32));
        }

        //for both HOSE and HASTC
        public static ORDER_SESSION OrderSession(MARKET_STATUS marketStatus, MARKET_STATUS webOTStatus)
        {
            if (marketStatus == MARKET_STATUS.UNVAILABLE && webOTStatus == MARKET_STATUS.READY)
            {
                return ORDER_SESSION.SESSION1;
            }

            if (marketStatus == MARKET_STATUS.PRE_OPEN && webOTStatus == MARKET_STATUS.PRE_OPEN)
            {
                return ORDER_SESSION.SESSION2;
            }

            if (marketStatus == MARKET_STATUS.PRE_OPEN && webOTStatus == MARKET_STATUS.OPEN)
            {
                return ORDER_SESSION.SESSION3;
            }
            if (marketStatus == MARKET_STATUS.OPEN && webOTStatus == MARKET_STATUS.OPEN)
            {
                return ORDER_SESSION.SESSION4;
            }

            if (marketStatus == MARKET_STATUS.PRE_CLOSE && webOTStatus == MARKET_STATUS.PRE_CLOSE)
            {
                return ORDER_SESSION.SESSION5;
            }

            if (marketStatus == MARKET_STATUS.PRE_CLOSE && webOTStatus == MARKET_STATUS.CLOSE ||
                marketStatus == MARKET_STATUS.OPEN && webOTStatus == MARKET_STATUS.CLOSE) //Hastc
            {
                return ORDER_SESSION.SESSION6;
            }

            if (marketStatus == MARKET_STATUS.CLOSE && webOTStatus == MARKET_STATUS.CLOSE)
            {
                return ORDER_SESSION.SESSION7;
            }

            if (marketStatus == MARKET_STATUS.CLOSE_PT && webOTStatus == MARKET_STATUS.CLOSE_PT)
            {
                return ORDER_SESSION.SESSION8;
            }

            return ORDER_SESSION.SESSION0;
        }

        private static string ServiceID(string serviceName)
        {
            switch (serviceName)
            {
                case "WEB":
                    return "01";
                case "IVR":
                    return "02";
                case "SMS":
                    return "03";
                default:
                    return "";
            }
        }

        public static bool SendEmail(string mailTo, string subject, string body)
        {
            MailMessage mail = new MailMessage();

            mail.IsBodyHtml = true;
            //mail.From = new MailAddress(mailFrom, nameFrom);
            mail.To.Add(new MailAddress(mailTo, mailTo));

            mail.Subject = subject;
            mail.Body = body;

            try
            {
                new SmtpClient().Send(mail);
            }
            catch (Exception ex)
            {
                Log(ex.ToString());

                return false;
            }

            return true;
        }

        public static void SendErrorReport(string errorMessage)
        {
            if (!_enableSendMail || _mailToAddress == "")
            {
                return;
            }

            string subject = "RTDataServices Sevice - Errors Report";
            string content = "<strong>There are errors in your RTDataServices system.</strong>";

            content += "<br><br>";

            content += errorMessage;

            content += "<br><br>DO NOT REPLY TO THIS MESSAGE";

            SendEmail(_mailToAddress, subject, content);
        }

        public static int GetSize<T>()
        {
            Type tt = typeof (T);
            int size;
            if (tt.IsValueType)
            {
                Console.WriteLine("{0} is a value type", tt.Name);
                size = Marshal.SizeOf(tt);
            }
            else
            {
                Console.WriteLine("{0} is a reference type", tt.Name);
                size = IntPtr.Size;
            }
            Console.WriteLine("Size = {0}", size);
            return size;
        }

        public static void DeleteFile(string fileName)
        {
            try
            {
                File.Delete(fileName);
            }
            catch (Exception ex)
            {
                Log("Delete file:" + ex);
            }
        }

        public static string GetIP(string source)
        {
            string[] subStrings = source.Split('\\');
            string IP;

            try
            {
                IP = subStrings[2];
            }
            catch
            {
                IP = null;
            }

            return IP;
        }

        public static bool Logon(string IP, string username, string password)
        {
            try
            {
                string domain;

                if (username.Contains("@"))
                {
                    string[] subStrings = username.Split('@');

                    username = subStrings[0];
                    domain = subStrings[1];
                }
                else
                {
                    domain = IP;
                }

                NetworkSharing.ServerLogon(domain, username, password);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsFileUpdated(string sFile)
        {
            FileInfo fi = new FileInfo(sFile);

            return touchFile(fi);
        }

        public static bool touchFile(FileInfo fsi)
        {
            // Update the CreationTime, LastWriteTime and LastAccessTime.
            try
            {
                if (fsi.LastWriteTime.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Log(e.Message);

                return false;
            }
        }

        public static string createFullAccount(string shortAccount)
        {
            if (string.IsNullOrEmpty(shortAccount))
            {
                return "";
            }
            else
            {
                shortAccount = shortAccount.Substring(0, shortAccount.Length - 1);
                shortAccount = TCSC_ACCOUNT_HEADER + shortAccount;
                return shortAccount;
            }
        }

        /// <summary>
        ///   Check this account has the permission to receive from broker or not.
        /// </summary>
        /// <param name = "serviceType"></param>
        /// <returns></returns>
        public static bool checkReceiveSmsPerm(short serviceType)
        {
            bool smsPermission = false;
            bool realTimePermission = false;
            bool endTradingTimePermission = false;
            checkServiceType(serviceType, ref smsPermission, ref realTimePermission, ref endTradingTimePermission);
            return smsPermission;
        }

        /// <summary>
        ///   Check if this account has permission to receive real time marched order or not.
        /// </summary>
        /// <param name = "serviceType"></param>
        /// <returns></returns>
        public static bool checkRealTimePerm(short serviceType)
        {
            bool smsPermission = false;
            bool realTimePermission = false;
            bool endTradingTimePermission = false;
            checkServiceType(serviceType, ref smsPermission, ref realTimePermission, ref endTradingTimePermission);
            return realTimePermission;
        }

        public static bool checkEndTradingTimePerm(short serviceType)
        {
            bool smsPermission = false;
            bool realTimePermission = false;
            bool endTradingTimePermission = false;
            checkServiceType(serviceType, ref smsPermission, ref realTimePermission, ref endTradingTimePermission);
            return endTradingTimePermission;
        }

        /// <summary>
        ///   Check permission from serviceType.
        /// </summary>
        /// <param name = "serviceType"></param>
        /// <param name = "smsPermission"></param>
        /// <param name = "realTimePermission"></param>
        /// <param name = "endTradingTimePermission"></param>
        public static void checkServiceType(short serviceType, ref bool smsPermission, ref bool realTimePermission,
                                            ref bool endTradingTimePermission)
        {
            if (serviceType >= 100)
            {
                endTradingTimePermission = true;
                serviceType = (short) (serviceType - 100);
            }
            else
            {
                endTradingTimePermission = false;
            }
            if (serviceType >= 10)
            {
                realTimePermission = true;
                serviceType = (short) (serviceType - 10);
            }
            else
            {
                realTimePermission = false;
            }
            if (serviceType == 1)
            {
                smsPermission = true;
            }
            else
            {
                smsPermission = false;
            }
        }

        /// <summary>
        ///   Create serviceType value from permission.
        /// </summary>
        /// <param name = "smsPermission"></param>
        /// <param name = "realTimePermission"></param>
        /// <param name = "endTradingTimePermission"></param>
        /// <returns></returns>
        public static short createServiceType(bool smsPermission, bool realTimePermission, bool endTradingTimePermission)
        {
            short serviceType = 0;
            if (smsPermission)
            {
                serviceType = (short) (serviceType + 1);
            }
            if (realTimePermission)
            {
                serviceType = (short) (serviceType + 10);
            }
            if (endTradingTimePermission)
            {
                serviceType = (short) (serviceType + 100);
            }
            return serviceType;
        }

        public static string formatTime(string timeString)
        {
            string returnValue = "";
            if (!string.IsNullOrEmpty(timeString) && (timeString.Length == 6))
            {
                returnValue = timeString.Substring(0, 2) + "h:" + timeString.Substring(2, 2) + "m:" +
                              timeString.Substring(4, 2) + "s";
            }
            return returnValue;
        }
    }
}