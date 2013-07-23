using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace LinkOPSConnector
{
    public enum PACKAGE_TYPE 
    {
        LOGIN = 0,
        LOGOUT,
        TEST,
        RECOVERY,
        NEW_ORDER,
        CHANGE_ORDER,
        CANCEL_ORDER,
    }

    public enum ORDER_STATUS
    {
        ORD_NOTHING = -1,
        ORD_PENDING = 0,
        ORD_WAITING = 1,
        ORD_FINISHED = 2,
        ORD_REJECTED = 3,
        ORD_UNKNOWN,
    }

    public enum SOURCE_ID
    { 
        FROM_FIS = 0,
        FROM_SET = 3
    }

    public enum TRANS_TYPE
    { 
        TRANS_NEW,
        TRANS_CANCEL,
        TRANS_CHANGE_ACC
    }

    public enum PACKAGE_LENGTH
    {
        LOGIN_LEN        = 68,
        LOGOUT_LEN       = 24,
        TEST_LEN         = 34,
        RECOVERY_LEN     = 42,
        NEW_ORDER_LEN    = 161,
        CANCEL_ORDER_LEN = 111,
        CHANGE_ORDER_LEN = 150
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MessageHeader
    {
        public short Length;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Sequence;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] PkgType;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
        public byte[] PkgTime; 
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DataHeader
    {
        public short Length;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Sequence;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] PkgType;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
        public byte[] PkgTime;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] MessageType;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public byte[] RefOrderID;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    // The logon message is used to authenticate a user attempting to establish
    // a connection from a remote system. The logon message must be the first 
    // message sent by the application requesting to initiate an XXX-FIS session.  
    public class LoginInfo
    {
        private MessageHeader header;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] HeartBtInt;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Common.USER_LEN)]
        public byte[] LoginID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Common.USER_LEN)]
        public byte[] Password;
        private byte ETX;

        public LoginInfo(string heartBtInt, string loginID, string password)
        {
            header.Length   = IPAddress.HostToNetworkOrder((short)PACKAGE_LENGTH.LOGIN_LEN);
            header.Sequence = Common.GetBytes(Common.NON_SEQ);
            header.PkgType  = Common.GetBytes(Common.TYPE_LOGON);
            header.PkgTime  = Common.GetBytes(Common.GetDate());
            HeartBtInt      = Common.GetBytes(heartBtInt);
            LoginID         = Common.GetBytes(loginID.PadRight(Common.USER_LEN));
            Password        = Common.GetBytes(password.PadRight(Common.USER_LEN));
            ETX             = Common.ETX_DEFAULT;
        }
    }

    // The logout message is used to initiate or confirm the termination of the XXX-FIS session. 
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class LogoutInfo
    {
        private MessageHeader header;
        private byte ETX;

        public LogoutInfo()
        {
            header.Length   = IPAddress.HostToNetworkOrder((short)PACKAGE_LENGTH.LOGOUT_LEN);
            header.Sequence = Common.GetBytes(Common.NON_SEQ);
            header.PkgType  = Common.GetBytes(Common.TYPE_LOGOUT);
            header.PkgTime  = Common.GetBytes(Common.GetDate());
            ETX             = Common.ETX_DEFAULT;
        }
    }

    // The test request message is used to force a heartbeat from the opposite side. 
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class TestRequestInfo
    {
        private MessageHeader header;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        private byte[] TestReqID;
        private byte ETX;

        public TestRequestInfo()
        {
            header.Length   = IPAddress.HostToNetworkOrder((short)PACKAGE_LENGTH.TEST_LEN);
            header.Sequence = Common.GetBytes(Common.NON_SEQ);
            header.PkgType  = Common.GetBytes(Common.TYPE_TEST);
            header.PkgTime  = Common.GetBytes(Common.GetDate());
            TestReqID       = Common.GetBytes(Common.GetTimestamp());
            ETX             = Common.ETX_DEFAULT;
        }
    }

    // To do data recovery when some package are loss.
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class RecoveryRequestInfo
    {
        private MessageHeader header;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] LastSeqNum;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] BeginSeqNum;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] EndSeqNum;
        public byte ETX;

        public RecoveryRequestInfo()
        {
            header.Length   = IPAddress.HostToNetworkOrder((short)PACKAGE_LENGTH.RECOVERY_LEN);
            header.Sequence = Common.GetBytes(Common.NON_SEQ);
            header.PkgType  = Common.GetBytes(Common.TYPE_RECOVERY);
            header.PkgTime  = Common.GetBytes(Common.GetDate());
            ETX             = Common.ETX_DEFAULT;
        }

        public RecoveryRequestInfo(string lastSeqNum, string beginSeqNum, string endSeqNum)
        {
            header.Length = IPAddress.HostToNetworkOrder((short)PACKAGE_LENGTH.RECOVERY_LEN);
            header.Sequence = Common.GetBytes(Common.NON_SEQ);
            header.PkgType  = Common.GetBytes(Common.TYPE_RECOVERY);
            header.PkgTime  = Common.GetBytes(Common.GetDate());
            ETX             = Common.ETX_DEFAULT;

            LastSeqNum  = Common.GetBytes(lastSeqNum);
            BeginSeqNum = Common.GetBytes(beginSeqNum);
            EndSeqNum   = Common.GetBytes(endSeqNum);
        }
    }

    // Define for Data message.
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class NewOrderInfo
    {
        public DataHeader header;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Common.TRADERID_LEN)]
        public byte[] EnterID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Common.SECSYMBOL_LEN)]
        public byte[] SecSymbol;
        public byte Side;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
        public byte[] Price;
        public byte ConPrice;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Volume;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] PublishVol;
        public byte Condition;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Common.ACCOUNT_LEN)]
        public byte[] Account;
        public byte TTF;
        public byte ThaiOrderType;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] CheckFlag;
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
        //public byte[] StopPrice;
        private byte ETX;

        public NewOrderInfo()
        {
            header.Length      = IPAddress.HostToNetworkOrder((short)PACKAGE_LENGTH.NEW_ORDER_LEN);
            header.Sequence    = Common.GetBytes(Common.NON_SEQ);
            header.PkgType     = Common.GetBytes(Common.TYPE_DATA);
            header.PkgTime     = Common.GetBytes(Common.GetDate());
            header.MessageType = Common.GetBytes(Common.DATA_NEW_ORDER);
            ETX                = Common.ETX_DEFAULT;
            Condition          = (byte)' ';
            TTF                = (byte)' ';
            ThaiOrderType      = (byte)' ';
            CheckFlag          = Common.GetBytes(Common.NON_CHECKFLAG);
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class ChangeOrderInfo
    {
        public DataHeader header;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[] FISOrderID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Common.TRADERID_LEN)]
        public byte[] EnterID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Common.ACCOUNT_LEN)]
        public byte[] Account;
        public byte PortOrClient;
        public byte TTF;
        public byte ThaiOrderType;

        // New field for changing Price.
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
        public byte[] Old_Price;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
        public byte[] New_Price;
        private byte ETX;

        public ChangeOrderInfo()
        {
            header.Length      = IPAddress.HostToNetworkOrder((short)PACKAGE_LENGTH.CHANGE_ORDER_LEN);
            header.Sequence    = Common.GetBytes(Common.NON_SEQ);
            header.PkgType     = Common.GetBytes(Common.TYPE_DATA);
            header.PkgTime     = Common.GetBytes(Common.GetDate());
            header.MessageType = Common.GetBytes(Common.DATA_CHANGE_ORDER);
            ETX                = Common.ETX_DEFAULT;
            TTF                = (byte)' ';
            ThaiOrderType      = (byte)' ';

        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class CancelOrderRequestInfo
    {
        public DataHeader header;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[] FISOrderID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Common.TRADERID_LEN)]
        public byte[] EnterID;
        private byte ETX;

        public CancelOrderRequestInfo()
        {
            header.Length      = IPAddress.HostToNetworkOrder((short)PACKAGE_LENGTH.CANCEL_ORDER_LEN);
            header.Sequence    = Common.GetBytes(Common.NON_SEQ);
            header.PkgType     = Common.GetBytes(Common.TYPE_DATA);
            header.PkgTime     = Common.GetBytes(Common.GetDate());
            header.MessageType = Common.GetBytes(Common.DATA_CANCEL_ORDER);
            ETX                = Common.ETX_DEFAULT;
        }
    }

    // The HeartBtInt field is used to declare the timeout interval for
    // generating heartbeats. XXX will specify the interval.
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HeartbeatInfo
    {
        public MessageHeader header;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] RequestType;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[] Reserve;
        public byte ResultFlag;
        public byte ETX;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RecoveryAckInfo
    {
        public MessageHeader header;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] BeginSeqNum; 
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] EndSeqNum;
        public byte ETX;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RecoveryCompleteInfo
    {
        public MessageHeader header;
        public byte ETX;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NewOrderFromBrokerInfo
    {
        public DataHeader header;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[] FISOrderID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        public byte[] EnterID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] SecSymbol;
        public byte Side;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
        public byte[] Price;
        public byte ConPrice;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Volume;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] PublishVol;
        public byte Condition;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Common.ACCOUNT_LEN)]
        public byte[] Account;
        public byte TTF;
        public byte ThaiOrderType;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] CheckFlag;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ChangeOrderFromBrokerInfo
    {
        public DataHeader header;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[] FISOrderID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Common.TRADERID_LEN)]
        public byte[] EnterID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Common.ACCOUNT_LEN)]
        public byte[] Account;
        public byte PortOrClient;
        public byte TTF;
        public byte ThaiOrderType;

        // New field for changing Price.
        public byte New_Side;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] New_Stock;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] New_Volume;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
        public byte[] New_ShowPrice;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NewOrderAckInfo
    {
        public DataHeader header;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[] FISOrderID;
        public byte ExecutionTransType;
        public byte OrdStatus;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] OrdRejReason;
        public byte SourceID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] OrdRejText;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ExecuteReportInfo
    {
        public DataHeader header;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[] FISOrderID;
        public byte ExecutionTransType;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
        public byte[] TransTime;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Common.SECSYMBOL_LEN)]
        public byte[] SecSymbol;
        public byte Side;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Volume;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
        public byte[] Price;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] ConfirmNo;
        public byte SourceID;
        public byte ExecType;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CancelDealInfo
    {
        public DataHeader header;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[] FISOrderID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
        public byte[] TransTime;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] SecSymbol;
        public byte Side;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Volume;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] ConfirmNo;
        public byte SourceID;
    }

    public class OrderInfo
    {
        public int Sequence;
        public string Time;
        public string Type;
        public string RefOrderID;
        public int FISOrderID;    // FIS order number 
        public string Symbol;     // Local Exchange Code 
        public char Side;          // B=Buy|S=Sell 
        public float Price;         // Range from 0.000001 to 999999.999999 %13.6f 
        public char ConPrice;      // ‘ ‘(blank)=no condition| A=ATO|M=MP|C=ATC 
        public int Volume;        // <= 1000000 
        public string Account;       // FIS Account ID 
        public int Status;     // 0=Accepted|8=Rejected|7=Warning 
        public string OrdRejReason;
        public int execTransType; // 0=New|1=Cancel|2=Change Acc 
        public int sourceID;        // 0=FIS|3=SET
        public string OrdRejText;

        public OrderInfo()
        {
            Sequence = 0;
            Time = Common.GetDate();
            Type = "";
            RefOrderID = "";
            FISOrderID = 0;
            Symbol = "";
            Side = ' ';
            Price = 0;
            ConPrice = ' ';
            Volume = 0;
            Account = "";
            Status = 0;
            OrdRejReason = "";
            execTransType = 0;
            sourceID = -1;
            OrdRejText = "";
        }

    }
}
