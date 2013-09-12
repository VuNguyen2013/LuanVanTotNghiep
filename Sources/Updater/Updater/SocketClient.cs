using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Updater
{
    class SocketClient
    {
        public readonly int ServerPort = int.Parse(Common.GetValueFromConfig("ServerPort"));
        public readonly IPAddress ServerIP = IPAddress.Parse(Common.GetValueFromConfig("ServerIP"));
        private Thread receiveDataThread;
        public void StartListedFromServer()
        {
            receiveDataThread = new Thread(ReceiveDataFromServer);
            receiveDataThread.Start();
        }
        public void ReceiveDataFromServer()
        {
            while (true)
            {
                try
                {
                    TcpClient tcpClient = new TcpClient();
                    tcpClient.Connect(ServerIP, ServerPort);
                    Stream stream = tcpClient.GetStream();
                    var reader = new StreamReader(stream);
                    var str = reader.ReadLine();
                    var strArr = str.Split('|');
                    if (strArr.Length > 0)
                    {
                        if (strArr[0] != StaticValues.MemberStockCompanyID)
                            return;
                        Updater.Repository.OrderRepository orderRep = new Repository.OrderRepository();
                        orderRep.UpdateStatusFromCore(strArr);
                    }
                    stream.Close();
                    tcpClient.Close();
                }
                catch
                {
                }
            }
        }
    }
}
