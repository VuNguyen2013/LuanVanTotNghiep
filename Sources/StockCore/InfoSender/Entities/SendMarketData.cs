using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Configuration;
using StockCore.Repositories;

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
        private readonly System.Web.Script.Serialization.JavaScriptSerializer _serialization = new System.Web.Script.Serialization.JavaScriptSerializer();

        public SendData()
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            HoseMarketPort = int.Parse(configuration.AppSettings.Settings["HoseMarketPort"].Value);
            HNXMarketPort = int.Parse(configuration.AppSettings.Settings["HNXMarketPort"].Value);
            UpComMarketPort = int.Parse(configuration.AppSettings.Settings["UpComMarketPort"].Value);
            HoseStockInfoPort = int.Parse(configuration.AppSettings.Settings["HoseStockInfoPort"].Value);
            HNXStockInfoPort = int.Parse(configuration.AppSettings.Settings["HNXStockInfoPort"].Value);
            UpComStockInfoPort = int.Parse(configuration.AppSettings.Settings["UpComStockInfoPort"].Value);
        }

        public void SendMarketData(List<string> listIP,ref bool status)
        {
            
            foreach (var ipStr in listIP)
            {
                //server send  hose data via UPD
                IPAddress ipAddress = IPAddress.Parse(ipStr);
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, HoseMarketPort);

                HoseMarketInfoRepository hoseRepo = new HoseMarketInfoRepository();
                var hoseData = hoseRepo.GetAll();

                byte[] hoseContent = Encoding.ASCII.GetBytes(_serialization.Serialize(hoseData));
                UdpClient udpClient = new UdpClient();
                try
                {
                    udpClient.Send(hoseContent, hoseContent.Length, ipEndPoint);
                    status = true;
                }
                catch 
                {
                    status = false;
                    throw;
                }

                //server send  hnx data via UPD
                ipEndPoint = new IPEndPoint(ipAddress, HNXMarketPort);

                HNXMarketInfoRepository hnxRepo = new HNXMarketInfoRepository();
                var hnxData = hoseRepo.GetAll();

                byte[] hnxContent = Encoding.ASCII.GetBytes(_serialization.Serialize(hnxData));
                try
                {
                    udpClient.Send(hnxContent, hnxContent.Length, ipEndPoint);
                    status = true;
                }
                catch 
                {
                    status = false;
                    throw;
                }

                //server send  upcom data via UPD
                ipEndPoint = new IPEndPoint(ipAddress, UpComMarketPort);

                HNXMarketInfoRepository upcomRepo = new HNXMarketInfoRepository();
                var upcomData = hoseRepo.GetAll();

                byte[] upcomContent = Encoding.ASCII.GetBytes(_serialization.Serialize(hnxData));
                try
                {
                    udpClient.Send(upcomContent, upcomContent.Length, ipEndPoint);
                    status = true;
                }
                catch
                {
                    status = false;
                    throw;
                }
            
            }           
        
        }
    }
}
