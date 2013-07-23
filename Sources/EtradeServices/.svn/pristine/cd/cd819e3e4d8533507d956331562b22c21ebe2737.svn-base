using System.IO;
using System.Reflection;

namespace ETradeGWServices
{
    class ETradeGWCommonConstants
    {
        // Message Type
        public const string TYPE_LOGON = "LI";
        public const string TYPE_HEARTBEAT = "HB";
        public const string TYPE_LOGOUT = "LO";
        public const string TYPE_TEST = "TR";
        public const string TYPE_RECOVERY = "RR";
        public const string TYPE_RECOVERY_ACK = "RA";
        public const string TYPE_RECOVERY_COMPLETE = "RC";
        public const string TYPE_DATA = "DT";

        // Data Type
        public const string DATA_NEW_ORDER = "7a";
        public const string DATA_NEW_CANCEL_ACK = "7b";
        public const string DATA_EXEC_REPORT = "7e";
        public const string DATA_CANCEL_ORDER = "7c";
        public const string DATA_NEW_ORDER_FROM_BROKER = "6a";
        public const string DATA_CHANGE_ORDER = "7d";
        public const string DATA_CHANGE_ORDER_FROM_BROKER = "6d";
        public const string DATA_CANCEL_DEAL = "3D";

        public const string NON_SEQ = "000000";
        public const string NON_VOLUME = "00000000";
        public const string NON_CHECKFLAG = "00000000";
        public const string NON_TRADERID = "00000000000";
        public const string NON_FISORDERID = "0000000000";
        public const string ZERO_PRICE = "000000.000000";
        public const string ZERO_REFORDERID = "0000000000000000000000000000000000000000000000000000000000000000";
        public const char RESULT_OK = '0';
        public const byte ETX_DEFAULT = 0x03;
        public const int PIPE_BUFFER_ZIZE = 1024000;

        public const string DIR_SEND = "SEND";
        public const string DIR_RECV = "RECV";

        public const int TRADERID_LEN = 11;
        public const int ACCOUNT_LEN = 10;
        public const int SECSYMBOL_LEN = 8;
        public const int USER_LEN = 20;
        public const int MESSAGE_TYPE_LEN = 2;
        public const int REFORDERID_LEN = 64;
        public const int SOURCE_ORDER_POS = 54;

        // Data position
        public const int MESSAGE_TYPE_POS = 25;
        public const int REFORDERID_POS = 27;

        private static readonly string FullPath = Assembly.GetExecutingAssembly().Location;
        public static string CurrentDir = Path.GetDirectoryName(FullPath);
        private static readonly string FileName = Path.GetFileNameWithoutExtension(FullPath);

        public static string ConfigFile = CurrentDir + @"\" + FileName + ".ini";
        public static string LogFile = CurrentDir + @"\" + FileName + ".log";
        public static string MessageFile = CurrentDir + @"\" + FileName + ".csv";

        public const string DEFAULT_SERVICENAME = "ETradeGW";

    }
}
