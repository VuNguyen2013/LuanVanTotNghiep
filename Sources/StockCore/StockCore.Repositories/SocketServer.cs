using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace StockCore.Repositories
{
    public class SocketServer
    {
        public readonly int ServerPort = int.Parse(CommonFuntions.GetValueFromConfig("ServerPort"));
        public readonly IPAddress ServerIP = IPAddress.Parse(CommonFuntions.GetValueFromConfig("ServerIP"));
        public void SendOrderResultData(string result)
        {
            try
            {
                TcpListener tcpListener = new TcpListener(ServerIP, ServerPort);
                tcpListener.Start();
                Socket socket = tcpListener.AcceptSocket();
                var stream = new NetworkStream(socket);
                var streamWriter = new StreamWriter(stream);
                streamWriter.AutoFlush = true;

                streamWriter.Write(result);

                stream.Close();
                socket.Close();
                tcpListener.Stop();
            }
            catch (Exception ex)
            {
            }  
        }
    }
}
