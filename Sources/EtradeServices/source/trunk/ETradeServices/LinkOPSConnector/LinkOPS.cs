using System;
using System.Diagnostics;
using ETradeCommon;

namespace LinkOPSConnector
{
    public class LinkOPS
    {
        LinkOPSInterface linkOPSInterface = null;

		public LinkOPS()
		{
            linkOPSInterface = new LinkOPSInterface();
		}

		public bool Connect(string server, int port)
		{
			return linkOPSInterface.Connect(server, port);
		}

        public bool Disconnect()
		{
            return linkOPSInterface.Disconnect();
		}

        public bool IsConnected()
		{
            return linkOPSInterface.IsConnected();
		}

        public bool IsLogged()
		{
            return linkOPSInterface.Logged;
		}

        public void InitLog(string fileName)
		{
            linkOPSInterface.InitLog(fileName);		
		}


		public bool Logon(int heartBeatDuration, string username, string password)
		{
            LoginInfo loginInfo = new LoginInfo(heartBeatDuration.ToString("0000"), username, password);

            SendMessage(loginInfo);

		    return true;
		}

        public bool Logout()
		{
            LogoutInfo logoutInfo = new LogoutInfo();

            return SendMessage(logoutInfo);   
		}

        public bool KeepAlive()
		{
			try
			{
				TestRequestInfo testRequestInfo = new TestRequestInfo();

			    return SendMessage(testRequestInfo);
			}
			catch(Exception e)
			{
                LogHandler.LogLinkOPS("KeepAlive: Exception = " + e, GetType() + ".NewOrder()", TraceEventType.Error);

			    return false;
			}
		}

        public bool NewOrder(string refOrderID, string enterID, string secSymbol, char side, float price, char conPrice, int volume, string account, float stopPrice, char condition)
		{
			try
			{
			
                NewOrderInfo newOrder = new NewOrderInfo();

                newOrder.header.RefOrderID = Common.GetBytes(refOrderID);
                newOrder.EnterID = Common.GetBytes(enterID.PadRight(Common.TRADERID_LEN));
                newOrder.SecSymbol = Common.GetBytes(secSymbol.PadRight(Common.SECSYMBOL_LEN));
                newOrder.Side = (byte)side;
                newOrder.Price = Common.GetBytes(price.ToString(Common.ZERO_PRICE));
                newOrder.ConPrice = (byte)conPrice;
                newOrder.Volume = Common.GetBytes(volume.ToString().PadRight(8));
                newOrder.PublishVol = newOrder.Volume;
                newOrder.Account = Common.GetBytes(account.PadRight(Common.ACCOUNT_LEN));
                //newOrder.StopPrice = Common.GetBytes(stopPrice.ToString(Common.ZERO_PRICE));
                newOrder.Condition = (byte)condition;
                

				return SendMessage(newOrder);
			}
			catch(Exception e)
			{
                LogHandler.LogLinkOPS("NewOrder Account = " + account + " refOrderID = " + refOrderID + " side = " + side + " Symbol = " + secSymbol + " price = " + price + " conPrice = " + conPrice + " Exception = " + e,
                                GetType() + ".NewOrder()", TraceEventType.Error);

				return false;
			}	
		}

        public bool CancelOrder(string refOrderID, string enterID, int fisOrderID)
		{
			try
			{
                CancelOrderRequestInfo orderCancel = new CancelOrderRequestInfo();

                orderCancel.header.RefOrderID = Common.GetBytes(refOrderID);
                orderCancel.EnterID = Common.GetBytes(enterID.PadRight(Common.TRADERID_LEN));
                orderCancel.FISOrderID = Common.GetBytes(fisOrderID.ToString(Common.NON_FISORDERID));

                return SendMessage(orderCancel);
			}
			catch(Exception e)
			{
                LogHandler.LogLinkOPS("CancelOrder refOrderID = " + refOrderID + " fisOrderID = " + fisOrderID  + " Exception = " + e,
                                GetType() + ".CancelOrder()", TraceEventType.Error);

				return false;
			}

		}

        public bool ChangeOrder(string refOrderID, string enterID, int fisOrderID, string account, char portOrClient, float oldPrice, float newPrice)
		{
			try
			{
                ChangeOrderInfo orderChange  = new ChangeOrderInfo();

                orderChange.header.RefOrderID = Common.GetBytes(refOrderID);
                orderChange.EnterID           = Common.GetBytes(enterID.PadRight(Common.TRADERID_LEN));
                orderChange.FISOrderID        = Common.GetBytes(fisOrderID.ToString(Common.NON_FISORDERID));
                orderChange.Account           = Common.GetBytes(account.PadRight(Common.ACCOUNT_LEN));
                orderChange.PortOrClient      = (byte)portOrClient;
                orderChange.TTF               = (byte)' ';
                orderChange.Old_Price = Common.GetBytes(oldPrice.ToString(Common.ZERO_PRICE));
                orderChange.New_Price = Common.GetBytes(newPrice.ToString(Common.ZERO_PRICE));

                return SendMessage(orderChange);
			}
			catch(Exception e)
			{
                LogHandler.LogLinkOPS("ChangeOrder Account = " + account + " refOrderID = " + refOrderID + " fisOrderID = " + fisOrderID +
                                        " Old Price = " + oldPrice + " New Price = " + newPrice + " Exception = " + e,
                                GetType() + ".ChangeOrder()", TraceEventType.Error);

				return false;
			}
		}

        public bool Recovery(int lastSeq, int beginSeq, int endSeq)
		{
            try
            {
                RecoveryRequestInfo recoveryRequestInfo = new RecoveryRequestInfo(lastSeq.ToString(Common.NON_SEQ),
                                                                                  beginSeq.ToString(Common.NON_SEQ), endSeq.ToString(Common.NON_SEQ));

                return SendMessage(recoveryRequestInfo);
            }
            catch (Exception e)
            {
                LogHandler.LogLinkOPS("Recovery lastSeq = " + lastSeq + " beginSeq = " + beginSeq + " endSeq = " + endSeq + " Exception = " + e,
                                GetType() + ".Recovery()", TraceEventType.Error);

                return false;
            }
		}

        public bool HasOrder()
		{
            return linkOPSInterface.HasOrder();
		}

        public OrderInfo GetOrder()
		{
            return linkOPSInterface.GetOrderFromQueue();
		}

        private bool SendMessage(LoginInfo info)
        {
            if (!IsConnected())
            {
                return false;
            }

            byte[] data = Common.SerializeExact(info);

            return SendMessage(data);
        }

        private bool SendMessage(LogoutInfo info)
        {
            if (!IsConnected())
            {
                return false;
            }

            byte[] data = Common.SerializeExact(info);

            return SendMessage(data);
        }

        private bool SendMessage(RecoveryRequestInfo info)
        {
            if (!IsConnected())
            {
                return false;
            }

            byte[] data = Common.SerializeExact(info);

            return SendMessage(data);
        }

        private bool SendMessage(TestRequestInfo info)
        {
            if (!IsConnected())
            {
                return false;
            }

            byte[] data = Common.SerializeExact(info);

            return SendMessage(data);
        }

        private bool SendMessage(NewOrderInfo info)
        {
            if (!IsConnected())
            {
                return false;
            }

            byte[] data = Common.SerializeExact(info);

            return SendMessage(data);
        }

        private bool SendMessage(CancelOrderRequestInfo info)
        {
            if (!IsConnected())
            {
                return false;
            }

            byte[] data = Common.SerializeExact(info);

            return SendMessage(data);
        }

        private bool SendMessage(ChangeOrderInfo info)
        {
            if (!IsConnected())
            {
                return false;
            }

            byte[] data = Common.SerializeExact(info);

            return SendMessage(data);
        }

        public bool SendMessage(byte[] data)
        {
            if (!IsConnected())
            {
                return false;
            }

            return linkOPSInterface.SendMessage(data);
        }
    }
}