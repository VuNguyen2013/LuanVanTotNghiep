using System;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ETradeCommon;
using LinkOPSConnector;

namespace ETradeGWServices
{
    class MessageHandler
    {
        private LinkOPS linkOPS = new LinkOPS();

        #region Methods
        public void InitLinkOPS()
        {
            LogHandler.Log("Init LinkOPS start: " + linkOPS + " " + ConfigurationManager.AppSettings["LinkOPSLogFolder"],
                           "InitLinkOPS", TraceEventType.Information);
            //Moved from MessageHandler.cs
            DateTime currentTime = DateTime.Now;

            //LinkOPSInitLog(_linkOPSHandle, Common.Config.Log_Folder + "LinkOPS." + currentTime.ToString("yyyyMMdd-HHmmss") + ".log");
            linkOPS.InitLog(ConfigurationManager.AppSettings["LinkOPSLogFolder"] + "LinkOPS." + currentTime.ToString("yyyyMMdd-HHmmss") + ".log");
            LogHandler.Log("Init LinkOPS end: " + linkOPS + " " + ConfigurationManager.AppSettings["LinkOPSLogFolder"],
                           "InitLinkOPS", TraceEventType.Information);
        }

        public bool Connect(string ipAddress, string port)
        {
            try
            {
                return linkOPS.Connect( ipAddress, Int32.Parse(port));
            }
            catch (Exception e)
            {
                LogHandler.Log("Error Connect LinkOPS: " + ConfigurationManager.AppSettings["LinkOPSLogFolder"],
                               "Connect", TraceEventType.Error);

                return false;
            }
        }

        public bool Disconnect()
        {
            try
            {
                return linkOPS.Disconnect();
            }
            catch (Exception ex)
            {
                LogHandler.Log("Error Disconnect LinkOPS: " + ConfigurationManager.AppSettings["LinkOPSLogFolder"],
                               "Disconnect", TraceEventType.Error);

                return false;
            }
        }

        public bool IsConnected()
        {
            return linkOPS.IsConnected();
        }

        public bool IsLogged()
        {
            return linkOPS.IsLogged();
        }

        public bool Logon(int heartBeatDuration, string username, string password)
        {
            return linkOPS.Logon(heartBeatDuration, username, password);
        }

        public bool KeepAlive()
        {
            return linkOPS.KeepAlive();
        }

        public bool Logout()
        {
            return linkOPS.Logout();
        }

        public bool Recovery(int lastSeq, int beginSeq, int endSeq)
        {
            return linkOPS.Recovery(lastSeq, beginSeq, endSeq);
        }

        public bool NewOrder(string refOrderID, string enterID, string secSymbol, char side, float price, char conPrice, int volume, string account,float stopPrice, char condition)
        {
            return linkOPS.NewOrder(refOrderID, enterID, secSymbol, side, price, conPrice, volume, account,stopPrice,condition);
        }

        public bool CancelOrder(string refOrderID, string enterID, int fisOrderID)
        {
            return linkOPS.CancelOrder(refOrderID, enterID, fisOrderID);
        }

        public bool ChangeOrder(string refOrderID, string enterID, int fisOrderID, string account, char portOrClient, float oldPrice, float newPrice)
        {
            return linkOPS.ChangeOrder(refOrderID, enterID, fisOrderID, account, portOrClient, oldPrice, newPrice);
        }

        public bool HasOrder()
        {
            return linkOPS.HasOrder();
        }

        public bool GetOrder(ref int seq, ref string time, ref string type, ref string refOrderID, ref int fisOrderID,
                             ref string symbol, ref char side, ref float price, ref char conPrice, ref int volume,
                             ref string account, ref int status, ref string ordRejReason, ref int execType, ref int sourceID, ref string orderRejText)
        {
            try
            {
                LinkOPSConnector.OrderInfo orderInfo = linkOPS.GetOrder();

                seq          = orderInfo.Sequence;
                time         = orderInfo.Time;
                type         = orderInfo.Type;
                refOrderID   = orderInfo.RefOrderID;
                symbol       = orderInfo.Symbol;
                side         = orderInfo.Side;
                price        = orderInfo.Price;
                conPrice     = orderInfo.ConPrice;
                volume       = orderInfo.Volume;
                account      = orderInfo.Account;
                status       = orderInfo.Status;
                ordRejReason = orderInfo.OrdRejReason;
                execType     = orderInfo.execTransType;
                sourceID     = orderInfo.sourceID;
                fisOrderID = orderInfo.FISOrderID;
                orderRejText = orderInfo.OrdRejText;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}