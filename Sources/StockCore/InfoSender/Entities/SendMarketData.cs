using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Configuration;
using StockCore.Repositories;
using System.Threading;

namespace StockCore.InfoSender.Entities
{
    class SendData
    {
        private int HoseMarketPort = 11000;
        private int HNXMarketPort = 11001;
        private int UpComMarketPort = 11002;
        private int HoseStockInfoPort = 11003;
        private int HNXStockInfoPort = 11004;
        private int UpComStockInfoPort = 11005;
        public Thread threadSendMarketData;
        public Thread threadSendStockInfoData;
        private List<string> _listIp;
        public bool Status;
        private readonly System.Web.Script.Serialization.JavaScriptSerializer _serialization = new System.Web.Script.Serialization.JavaScriptSerializer();

        public SendData(List<string> listIp)
        {
            this._listIp = listIp;
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            HoseMarketPort = int.Parse(configuration.AppSettings.Settings["HoseMarketPort"].Value);
            HNXMarketPort = int.Parse(configuration.AppSettings.Settings["HNXMarketPort"].Value);
            UpComMarketPort = int.Parse(configuration.AppSettings.Settings["UpComMarketPort"].Value);
            HoseStockInfoPort = int.Parse(configuration.AppSettings.Settings["HoseStockInfoPort"].Value);
            HNXStockInfoPort = int.Parse(configuration.AppSettings.Settings["HNXStockInfoPort"].Value);
            UpComStockInfoPort = int.Parse(configuration.AppSettings.Settings["UpComStockInfoPort"].Value);
        }
        public void SendInfo()
        {
            threadSendMarketData = new Thread(SendMarketData);
            threadSendStockInfoData = new Thread(SendStockInfoData);
            threadSendMarketData.Start();
            threadSendStockInfoData.Start();
        }
        public void SendMarketData()
        {
            while (true)
            {
                HoseMarketInfoRepository hoseRepo = new HoseMarketInfoRepository();
                var hoseData = hoseRepo.GetAll();
                HNXMarketInfoRepository hnxRepo = new HNXMarketInfoRepository();
                var hnxData = hnxRepo.GetAll();
                UpcomMarketInfoRepository upcomRepo = new UpcomMarketInfoRepository();
                var upcomData = upcomRepo.GetAll();
                foreach (var ipStr in _listIp)
                {
                    //server send  hose data via UPD
                    IPAddress ipAddress = IPAddress.Parse(ipStr);
                    IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, HoseMarketPort);                   

                    byte[] hoseContent = Encoding.ASCII.GetBytes(_serialization.Serialize(hoseData));
                    UdpClient udpClient = new UdpClient();
                    try
                    {
                        udpClient.Send(hoseContent, hoseContent.Length, ipEndPoint);
                        Status = true;
                    }
                    catch
                    {
                        Status = false;
                        throw;
                    }

                    //server send  hnx data via UPD
                    ipEndPoint = new IPEndPoint(ipAddress, HNXMarketPort);                    

                    byte[] hnxContent = Encoding.ASCII.GetBytes(_serialization.Serialize(hnxData));
                    try
                    {
                        udpClient.Send(hnxContent, hnxContent.Length, ipEndPoint);
                        Status = true;
                    }
                    catch
                    {
                        Status = false;
                        throw;
                    }

                    //server send  upcom data via UPD
                    ipEndPoint = new IPEndPoint(ipAddress, UpComMarketPort);

                    byte[] upcomContent = Encoding.ASCII.GetBytes(_serialization.Serialize(hnxData));
                    try
                    {
                        udpClient.Send(upcomContent, upcomContent.Length, ipEndPoint);
                        Status = true;
                    }
                    catch
                    {
                        Status = false;
                        throw;
                    }

                }
                Thread.Sleep(2000);
            }
        
        }
        public void SendStockInfoData()
        {
            while (true)
            {
                HoseStockInfoRepository hoseRepo = new HoseStockInfoRepository();
                var hoseData = hoseRepo.GetAll();
                HNXStockInfoRepository hnxRepo = new HNXStockInfoRepository();
                var hnxData = hnxRepo.GetAll();
                UpComStockInfoRepository upcomRepo = new UpComStockInfoRepository();
                var upcomData = upcomRepo.GetAll();
                foreach (var ipStr in _listIp)
                {
                    //send hose stock info
                    IPAddress ipAddress = IPAddress.Parse(ipStr);
                    IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, HoseStockInfoPort);
                    UdpClient udpClient = new UdpClient();
                    try
                    {               
                        byte[] hoseContent = Encoding.ASCII.GetBytes(_serialization.Serialize(hoseData));                        
                        udpClient.Send(hoseContent, hoseContent.Length, ipEndPoint);
                        Status = true;                  

                    }
                    catch
                    {
                        Status = false;
                        throw;
                    }
                    //send hnx stock info
                    ipEndPoint = new IPEndPoint(ipAddress, HNXStockInfoPort);                    
                    try
                    {                      
                        
                        byte[] hnxContent = Encoding.ASCII.GetBytes(_serialization.Serialize(hnxData));
                        udpClient.Send(hnxContent, hnxContent.Length, ipEndPoint);
                        Status = true;
                    }
                    catch
                    {
                        Status = false;
                        throw;
                    }
                    //send upcom stock info
                    ipEndPoint = new IPEndPoint(ipAddress, UpComStockInfoPort);
                    try
                    {
                        
                        byte[] upcomContent = Encoding.ASCII.GetBytes(_serialization.Serialize(upcomData));
                        udpClient.Send(upcomContent, upcomContent.Length, ipEndPoint);
                        Status = true;
                    }
                    catch
                    {
                        Status = false;
                        throw;
                    }
                }
                Thread.Sleep(2000);
            }            
        }        
    }
}
