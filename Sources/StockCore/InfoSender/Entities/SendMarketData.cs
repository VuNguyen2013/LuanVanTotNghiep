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
            //server send data via UPD
            foreach (var ipStr in listIP)
            {
                IPAddress ipAddress = IPAddress.Parse(ipStr);
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, HoseMarketPort);

                HoseMarketInfoRepository hoseRepo = new HoseMarketInfoRepository();
                var data = hoseRepo.GetAll();

                byte[] content = Encoding.ASCII.GetBytes(_serialization.Serialize(data));
                UdpClient udpClient = new UdpClient();
                try
                {
                    udpClient.Send(content, content.Length, ipEndPoint);
                    udpClient.Close();
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
