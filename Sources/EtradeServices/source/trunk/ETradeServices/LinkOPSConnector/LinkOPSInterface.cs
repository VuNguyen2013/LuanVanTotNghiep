using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

using ETradeCommon;

namespace LinkOPSConnector
{
    class LinkOPSInterface
    {
        private bool _isLogged;
	    private bool _isStartedRecoverying;
	    private bool _isRecoverying;
	    private long _numLostPackage;
	    private long _lostCount;
	    private long _oldSequence;

        private Thread _receiverThread = null;
        private bool _isRunning;

        byte[] RemainBuffer = new byte[Common.MAX_MSG_LEN];
	    int RemainBufLen;

        private static int _socketHandler = 0;

        [DllImport("SocketLib.dll", EntryPoint = "CreateSocketHandler",
            ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CreateSocketHandler();

        [DllImport("SocketLib.dll", EntryPoint = "DestroySocketHandler",
             ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DestroySocketHandler(int handle);

        [DllImport("SocketLib.dll", EntryPoint = "SocketHandlerInitLog",
            ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SocketHandlerInitLog(int handle, string logFile);

        [DllImport("SocketLib.dll", EntryPoint = "SocketHandlerConnect",
            ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool SocketHandlerConnect(int handle, string server, int port);

        [DllImport("SocketLib.dll", EntryPoint = "SocketHandlerDisconnect",
            ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool SocketHandlerDisconnect(int handle);

        [DllImport("SocketLib.dll", EntryPoint = "SocketHandlerIsConnected",
            ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool SocketHandlerIsConnected(int handle);

        [DllImport("SocketLib.dll", EntryPoint = "SocketHandlerSendMessage",
            ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool SocketHandlerSendMessage(int handle, byte[] message, int length);

        [DllImport("SocketLib.dll", EntryPoint = "SocketHandlerReceiveMessage",
            ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool SocketHandlerReceiveMessage(int handle, byte[] message, ref int length);
        
        private Queue<OrderInfo> queueOrderInfo;

        public bool Logged
        {
            get
            {
                return _isLogged;
            }

            set
            {
                _isLogged = value;
            }
        }

        public bool IsConnected()
        {
            return SocketHandlerIsConnected(_socketHandler);
        }

        public LinkOPSInterface()
        {
            Logged = false;

            _isStartedRecoverying = false;
            _isRecoverying		  = false;
            _numLostPackage		  = 0;
            _lostCount			  = 0;
            _oldSequence		  = 0;

            queueOrderInfo = new Queue<OrderInfo>();
            _socketHandler = CreateSocketHandler();
     
        }

        public void InitLog(string  fileName)
        {
            SocketHandlerInitLog(_socketHandler, fileName);	
        }

        public bool Connect(string serverIPAddress, int port)
        {
            bool connected = SocketHandlerConnect(_socketHandler, serverIPAddress, port);

            if (connected)
            {
                StartReceiver();
            }

            return connected;
        }

        public bool Disconnect()
        {
            StopReceiver();

            return SocketHandlerDisconnect(_socketHandler);
        }

        public bool SendMessage (byte[] data)
        {
            if (!IsConnected())
            {
                return false;
            }

            LogHandler.LogLinkOPS("[SEND]:<-- " + Common.GetRawData(data, data.Length + 1), GetType() + ".SendMessage()", TraceEventType.Information);

            return SocketHandlerSendMessage(_socketHandler, data, data.Length);
        }


        public bool HasOrder()
        {
            return (queueOrderInfo.Count > 0);   
        }

        public OrderInfo GetOrderFromQueue()
        {
	        try
	        {
                if (!HasOrder())
                {
                    return null;
                }
	        
		        OrderInfo orderInfo = (OrderInfo)queueOrderInfo.Dequeue();

	            return orderInfo;
	        }
	        catch(Exception e)
	        {
                LogHandler.LogLinkOPS("GetOrderFromQueue: Exception = " + e, GetType() + ".GetOrderFromQueue()", TraceEventType.Error);

		        return null;	
	        }
        }

        public void OrderEnqueue (byte[]  data)
        {
	        try
	        {
                DataHeader header = (DataHeader)Common.RawDeserialize(data, typeof(DataHeader));

                string msgType = Common.GetString(header.MessageType);

                switch(msgType)
                {
                    case Common.DATA_NEW_CANCEL_ACK:
                    {
                        NewOrderAckInfo info;

                        try
                        {
                            info = (NewOrderAckInfo)Common.RawDeserialize(data, typeof(NewOrderAckInfo));
                        }
                        catch (Exception ex)
                        {
                            LogHandler.LogLinkOPS("DATA_NEW_CANCEL_ACK: Exception = " + ex, GetType() + ".OrderEnqueue()", TraceEventType.Error);

                            break;
                        }

                        OrderEnqueue(info);

                        break;
                    }
                    case Common.DATA_EXEC_REPORT:
                    {
                        ExecuteReportInfo info;
                        
                        try
                        {
                            info = (ExecuteReportInfo)Common.RawDeserialize(data, typeof(ExecuteReportInfo));
                        }
                        catch (Exception ex)
                        {
                            LogHandler.LogLinkOPS("DATA_EXEC_REPORT: Exception = " + ex, GetType() + ".OrderEnqueue()", TraceEventType.Error);

                            break;
                        }

                        OrderEnqueue(info);

                        break;
                    }
                    case Common.DATA_NEW_ORDER_FROM_BROKER:
                    {
                        NewOrderFromBrokerInfo info;

                        try
                        {
                            info = (NewOrderFromBrokerInfo)Common.RawDeserialize(data, typeof(NewOrderFromBrokerInfo));
                        }
                        catch (Exception ex)
                        {
                            LogHandler.LogLinkOPS("DATA_NEW_ORDER_FROM_BROKER: Exception = " + ex, GetType() + ".OrderEnqueue()", TraceEventType.Error);

                            break;
                        }

                        OrderEnqueue(info);

                        break;
                    }
                    case Common.DATA_CHANGE_ORDER_FROM_BROKER:
                    {
                        ChangeOrderFromBrokerInfo info;

                        try
                        {
                            info = (ChangeOrderFromBrokerInfo)Common.RawDeserialize(data, typeof(ChangeOrderFromBrokerInfo));
                        }
                        catch (Exception ex)
                        {
                            LogHandler.LogLinkOPS("DATA_CHANGE_ORDER_FROM_BROKER: Exception = " + ex, GetType() + ".OrderEnqueue()", TraceEventType.Error);

                            break;
                        }

                        OrderEnqueue(info);

                        break;
                    }
                    case Common.DATA_CANCEL_DEAL:
                    {
                        CancelDealInfo info;

                        try
                        {
                            info = (CancelDealInfo)Common.RawDeserialize(data, typeof(CancelDealInfo));
                        }
                        catch (Exception ex)
                        {
                            LogHandler.LogLinkOPS("DATA_CANCEL_DEAL: Exception = " + ex, GetType() + ".OrderEnqueue()", TraceEventType.Error);

                            break;
                        }

                        OrderEnqueue(info);

                        break;
                    }
                }
	        }
	        catch(Exception ex)
	        {
                LogHandler.LogLinkOPS("OrderEnqueue (string  data): Exception = " + ex, GetType() + ".OrderEnqueue()", TraceEventType.Error);
	        }
        }

        public void OrderEnqueue (NewOrderAckInfo info)
        {
            try
            {
                OrderInfo orderInfo = new OrderInfo();

                orderInfo.Sequence		= Int32.Parse(Common.GetString(info.header.Sequence));
                orderInfo.Time			= Common.GetString(info.header.PkgTime);
                orderInfo.Type			= Common.GetString(info.header.MessageType);
                orderInfo.RefOrderID	= Common.GetString(info.header.RefOrderID);
                orderInfo.FISOrderID	= Int32.Parse(Common.GetString(info.FISOrderID));
                orderInfo.execTransType = Int32.Parse(Common.GetString(info.ExecutionTransType));
                orderInfo.sourceID = Int32.Parse(Common.GetString(info.SourceID));
                var status = Int32.Parse(Common.GetString(info.OrdStatus));
                orderInfo.Status = (int)Common.GetStatus(status, orderInfo.sourceID);
                orderInfo.OrdRejReason	= Common.GetString(info.OrdRejReason);
                orderInfo.OrdRejText = Common.GetString(info.OrdRejText);

                queueOrderInfo.Enqueue(orderInfo);
            }
            catch (Exception ex)
            {
                LogHandler.LogLinkOPS("OrderEnqueue (NewOrderAckInfo info): Exception = " + ex, GetType() + ".OrderEnqueue()", TraceEventType.Error);
            }
        }

        public void OrderEnqueue (ExecuteReportInfo info)
        {
            try
            {
                OrderInfo orderInfo = new OrderInfo();

                orderInfo.Sequence = Int32.Parse(Common.GetString(info.header.Sequence));
                orderInfo.Time = Common.GetString(info.header.PkgTime);
                orderInfo.Type = Common.GetString(info.header.MessageType);
                orderInfo.RefOrderID = Common.GetString(info.header.RefOrderID);
                orderInfo.FISOrderID = Int32.Parse(Common.GetString(info.FISOrderID));
                orderInfo.execTransType = Int32.Parse(Common.GetString(info.ExecutionTransType));
                orderInfo.Symbol = Common.GetString(info.SecSymbol);
                orderInfo.Side			= (char)info.Side;
                orderInfo.Volume		= Int32.Parse(Common.GetString(info.Volume));
                orderInfo.Price			= float.Parse(Common.GetString(info.Price));
                orderInfo.Status		= (int)ORDER_STATUS.ORD_FINISHED;
                orderInfo.sourceID = Int32.Parse(Common.GetString(info.SourceID));

                queueOrderInfo.Enqueue(orderInfo);
            }
            catch (Exception ex)
            {
                LogHandler.LogLinkOPS("OrderEnqueue (ExecuteReportInfo info): Exception = " + ex, GetType() + ".OrderEnqueue()", TraceEventType.Error);
            }
        }

        public void OrderEnqueue (NewOrderFromBrokerInfo info)
        {
            try
            {
                OrderInfo orderInfo = new OrderInfo();

                orderInfo.Sequence = Int32.Parse(Common.GetString(info.header.Sequence));
                orderInfo.Time = Common.GetString(info.header.PkgTime);
                orderInfo.Type = Common.GetString(info.header.MessageType);
                orderInfo.RefOrderID = Common.GetString(info.header.RefOrderID);
                orderInfo.FISOrderID = Int32.Parse(Common.GetString(info.FISOrderID));
                orderInfo.execTransType = (int)TRANS_TYPE.TRANS_NEW;
                orderInfo.Symbol = Common.GetString(info.SecSymbol);
                orderInfo.Side = (char)info.Side;
                orderInfo.Volume = Int32.Parse(Common.GetString(info.Volume));
                orderInfo.Price = float.Parse(Common.GetString(info.Price));
                orderInfo.ConPrice      = (char)info.ConPrice;
                orderInfo.Account = Common.GetString(info.Account);
                orderInfo.Status = (int)ORDER_STATUS.ORD_PENDING;

                queueOrderInfo.Enqueue(orderInfo);
            }
            catch (Exception ex)
            {
                LogHandler.LogLinkOPS("OrderEnqueue (NewOrderFromBrokerInfo info): Exception = " + ex, GetType() + ".OrderEnqueue()", TraceEventType.Error);
            }
        }

        public void OrderEnqueue (ChangeOrderFromBrokerInfo info)
        {
            try
            {
                OrderInfo orderInfo = new OrderInfo();

                orderInfo.Sequence = Int32.Parse(Common.GetString(info.header.Sequence));
                orderInfo.Time = Common.GetString(info.header.PkgTime);
                orderInfo.Type = Common.GetString(info.header.MessageType);
                orderInfo.RefOrderID = Common.GetString(info.header.RefOrderID);
                orderInfo.FISOrderID = Int32.Parse(Common.GetString(info.FISOrderID));
                orderInfo.execTransType = (int)TRANS_TYPE.TRANS_CHANGE_ACC;
                orderInfo.Account = Common.GetString(info.Account);

                orderInfo.Side = (char) info.New_Side;
                orderInfo.Symbol = Common.GetString(info.New_Stock);
                orderInfo.Volume = Int32.Parse(Common.GetString(info.New_Volume));
                orderInfo.Price = float.Parse(Common.GetString(info.New_ShowPrice));

                queueOrderInfo.Enqueue(orderInfo);
            }
            catch (Exception ex)
            {
                LogHandler.LogLinkOPS("OrderEnqueue (NewOrderFromBrokerInfo info): Exception = " + ex, GetType() + ".OrderEnqueue()", TraceEventType.Error);
            }
        }

        public void OrderEnqueue (CancelDealInfo info)
        {
            try
            {
                OrderInfo orderInfo = new OrderInfo();

                orderInfo.Sequence = Int32.Parse(Common.GetString(info.header.Sequence));
                orderInfo.Time = Common.GetString(info.header.PkgTime);
                orderInfo.Type = Common.GetString(info.header.MessageType);
                orderInfo.RefOrderID = Common.GetString(info.header.RefOrderID);
                orderInfo.FISOrderID = Int32.Parse(Common.GetString(info.FISOrderID));
                orderInfo.Volume = Int32.Parse(Common.GetString(info.Volume));
                orderInfo.execTransType = (int)TRANS_TYPE.TRANS_CANCEL;
                orderInfo.Status		= (int)ORDER_STATUS.ORD_FINISHED;
                orderInfo.sourceID = Int32.Parse(Common.GetString(info.SourceID));

                queueOrderInfo.Enqueue(orderInfo);
            }
            catch (Exception ex)
            {
                LogHandler.LogLinkOPS("OrderEnqueue (CancelDealInfo info): Exception = " + ex, GetType() + ".OrderEnqueue()", TraceEventType.Error);
            }
        }

        public void COnDataArrival(byte[] messages, int len)
        {
	        bool isFistMessage = true;
	        int index = 0, lastindex = 0;
        	
	        if (len <= 0) 
	        {
		        return;
	        }

	        try
	        {
		        for (index = 0; index < len; index++)
		        {
			        if (messages[index] == Common.ETX_DEFAULT)
			        {
				        try
				        {
                            byte[] message = new byte[Common.MAX_RECV_LEN];
					        int messageLen = 0;

					        if (isFistMessage && RemainBufLen > 0)
					        {
                                Array.Copy(RemainBuffer, 0, message, 0, RemainBufLen);

                                Array.Copy(messages, lastindex, message, RemainBufLen, index - lastindex);
        						
						        messageLen = RemainBufLen + index - lastindex;

						        isFistMessage = false;
						        RemainBufLen  = 0;
					        }
					        else
					        {
                                Array.Copy(messages, lastindex, message, 0, index - lastindex);

						        messageLen = index - lastindex;
					        }

					        lastindex = index + 1;

        					
					        message[messageLen] = Common.ETX_DEFAULT;

                            LogHandler.LogLinkOPS("[RECV]:<-- " + Common.GetRawData(message, messageLen + 1), GetType() + ".COnDataArrival()", TraceEventType.Information);

					        ProcessData(message);
				        }
				        catch(Exception ex)
				        {
                            LogHandler.LogLinkOPS("COnDataArrival (in loop): Exception = " + ex, GetType() + ".COnDataArrival()", TraceEventType.Error);
				        }
			        }
		        }

		        if (lastindex < index)
		        {
                    Array.Copy(messages, lastindex, RemainBuffer, 0, index - lastindex);

			        RemainBufLen = index - lastindex;
		        }
	        }
            catch (Exception ex)
            {
                LogHandler.LogLinkOPS("COnDataArrival: Exception = " + ex, GetType() + ".COnDataArrival()", TraceEventType.Error);
            }
        }

        int count = 0;

        void ProcessData(byte[] data)
        {
	        MessageHeader header;

            try
            {
                header = (MessageHeader)Common.RawDeserialize(data, typeof(MessageHeader));

                string pkgType = Common.GetString(header.PkgType);

                switch (pkgType)
                {
                    case Common.TYPE_HEARTBEAT:
                    {
                        HeartbeatInfo heartbeat = (HeartbeatInfo)Common.RawDeserialize(data, typeof(HeartbeatInfo));

                        string requestType = Common.GetString(heartbeat.RequestType);

                        if (requestType == Common.TYPE_LOGON)
                        {
                            if (heartbeat.ResultFlag == Common.RESULT_OK)
                            { 
                                // Login ok
                                LogHandler.LogLinkOPS("Logged on!", GetType() + ".ProcessData()", TraceEventType.Information);
                                Logged = true;
                            }
                        }
                        else if (requestType == Common.TYPE_LOGOUT)
                        {
                            if (heartbeat.ResultFlag == Common.RESULT_OK)
                            { 
                                // Logout ok
                                LogHandler.LogLinkOPS("Logged out!", GetType() + ".ProcessData()", TraceEventType.Information);
                                Logged = false;
                            }
                        }
                        else // This is Test request
                        {
                            if (heartbeat.ResultFlag == Common.RESULT_OK)
                            { 
                                // Keep alive ok
                                LogHandler.LogLinkOPS("Keep alive ok!", GetType() + ".ProcessData()", TraceEventType.Information);
                            }
                        }

                        break;
                    }
                    case Common.TYPE_RECOVERY_ACK:
                    {
                        count++;
                        RecoveryAckInfo recoveryAckInfo = (RecoveryAckInfo)Common.RawDeserialize(data, typeof(RecoveryAckInfo));

                        LogHandler.LogLinkOPS("Recoverying !: " + Common.GetString(recoveryAckInfo.header.PkgTime) + " - " + Common.GetString(recoveryAckInfo.BeginSeqNum) + " - " + Common.GetString(recoveryAckInfo.EndSeqNum), GetType() + ".ProcessData()", TraceEventType.Information);
 
                        _isRecoverying = true;
                        _isStartedRecoverying = false;
                        _lostCount = 0;

                        break;
                    }
                    case Common.TYPE_RECOVERY_COMPLETE:
                    {
                        LogHandler.LogLinkOPS("Complete Recoverying!", GetType() + ".ProcessData()", TraceEventType.Information);
                        _isRecoverying = false;
                        _isStartedRecoverying = false;

                        break;
                    }
                    case Common.TYPE_DATA:
                    {
                        string sequence     = Common.GetString(header.Sequence);
                        long seq            = Int64.Parse(sequence);

                        OrderEnqueue(data);

                        // Application was just started, or not lost package
                        if (_oldSequence == 0 || seq == _oldSequence + 1 || _isStartedRecoverying || _isRecoverying)
                        {
                            // Save Start Sequence
                            if (!_isRecoverying) // not update sequence when recovery is in progress.
                            {
                                if (_oldSequence < seq)
                                {
                                    _oldSequence = seq;
                                }
                            }
                            else // Recovery is in progress, cout number of recoveried pacakges.
                            {
                                _lostCount++;

                                if (_lostCount == _numLostPackage) // Stop recoverying when enough lost packages.
                                {
                                    _isRecoverying = false;
                                    _isStartedRecoverying = false;
                                }
                            }
                        }
                        else if (seq > _oldSequence + 1) // Data is lost, send recovery request to FIS
                        {
                            _isStartedRecoverying = true;
                            _numLostPackage = seq - _oldSequence - 1;

                            long lastSeqNum = _oldSequence;
                            long beginSeqNum = _oldSequence + 1;
                            long endSeqNum = seq - 1;

                            RecoveryRequestInfo recoveryRequestInfo = new RecoveryRequestInfo(lastSeqNum.ToString(),
                                                                                                beginSeqNum.ToString(),
                                                                                                endSeqNum.ToString());

                            byte[] message = Common.SerializeExact(recoveryRequestInfo);

                            SendMessage(message);
                        }

                        break;
                    }
                    default:
                        break;
                }
            }
	        catch(Exception ex)
	        {
                LogHandler.LogLinkOPS("ProcessData: Exception = " + ex, GetType() + ".ProcessData()", TraceEventType.Error);
	        }
        }

        public void StartReceiver()
        {
            if (!_isRunning)
            {
                _receiverThread = new Thread(new ThreadStart(ReceiverProcess));
                _receiverThread.IsBackground = true;
                _isRunning = true;
                _receiverThread.Start();
            }
        }

        public void StopReceiver()
        {
            _isRunning = false;

            if (_receiverThread != null)
            {
                _receiverThread.Abort();

                _receiverThread = null;
            }
        }

        private void ReceiverProcess()
        {
            byte[] message = new byte[Common.MAX_RECV_LEN];
            int length = 0;

            while (_isRunning)
            {
                Thread.Sleep(1);

                if (SocketHandlerReceiveMessage(_socketHandler, message, ref length))
                {
                    if (length <= 0)
                    {
                        _isRunning = false;
                        Logged = false;

                        break;
                    }

                    COnDataArrival(message, length);
                }
            }

            return;
        }
    }
}
