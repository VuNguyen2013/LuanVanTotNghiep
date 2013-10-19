using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Updater
{
    public partial class MainForm : Form
    {
        private int HoseMarketPort = 11000;
        private int HNXMarketPort = 11001;
        private int UpComMarketPort = 11002;
        private int HoseStockInfoPort = 11003;
        private int HNXStockInfoPort = 11004;
        private int UpComStockInfoPort = 11005;
        private bool _hoseStatus = true;
        private bool _hnxStatus = true;
        private bool _upcomStatus = true;
        private Thread threadHoseMarket;
        private Thread threadHNXMarket;
        private Thread threadUpcomMarket;
        private Thread threadHoseStockInfo;
        private Thread threadHNXStockInfo;
        private Thread threadUpComStockInfo;
        private Thread sendNewOrderThread;

        public MainForm()
        {
            InitializeComponent();
        }
        private void GetHOSEMarketData()
        {
            while (true)
            {
                try
                {
                    UdpClient udpClient = new UdpClient();
                    udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, HoseMarketPort));
                    IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, HoseMarketPort);
                    byte[] content = udpClient.Receive(ref remoteIPEndPoint);
                    if (content.Length > 0)
                    {
                        string message = Encoding.ASCII.GetString(content);
                        udpClient.Close();
                        Repository.HoseMarketInfoRepository hoseRep = new Repository.HoseMarketInfoRepository();
                        hoseRep.Insert(message);
                    }
                    _hoseStatus = true;
                }
                catch
                {
                    _hoseStatus = false;
                }
                Thread.Sleep(100);
            }
        }
        private void SetBackColorStatus(Panel panel,bool status)
        {
            if (panel.BackColor == Color.DodgerBlue)
            {
                panel.BackColor = Color.Transparent;
            }
            else if(status)
            {
                panel.BackColor = Color.DodgerBlue;
            }
        }
        private void GetHNXMarketData()
        {
            while (true)
            {
                try
                {
                    UdpClient udpClient = new UdpClient();
                    udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, HNXMarketPort));
                    IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, HNXMarketPort);
                    byte[] content = udpClient.Receive(ref remoteIPEndPoint);
                    if (content.Length > 0)
                    {
                        string message = Encoding.ASCII.GetString(content);
                        udpClient.Close();
                        Repository.HNXMarketInfoRepository hnxRep = new Repository.HNXMarketInfoRepository();
                        hnxRep.Insert(message);
                    }
                    _hnxStatus = true;
                }
                catch
                {
                    _hnxStatus = false;
                }
            }
        }
        private void GetUpComMarketData()
        {
            while (true)
            {
                try
                {
                    UdpClient udpClient = new UdpClient();
                    udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, UpComMarketPort));
                    IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, UpComMarketPort);
                    byte[] content = udpClient.Receive(ref remoteIPEndPoint);
                    if (content.Length > 0)
                    {
                        string message = Encoding.ASCII.GetString(content);
                        udpClient.Close();
                        Repository.UpcomMarketInfoRepository upcomRep = new Repository.UpcomMarketInfoRepository();
                        upcomRep.Insert(message);
                    }
                    _upcomStatus = true;
                }
                catch
                {
                    _upcomStatus = false;
                }
            }
        }
        private void GetHoseStockInfoData()
        {            
            while (true)
            {
                try
                {
                    UdpClient udpClient = new UdpClient();
                    udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, HoseStockInfoPort));
                    IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, HoseStockInfoPort);
                    byte[] content = udpClient.Receive(ref remoteIPEndPoint);
                    if (content.Length > 0)
                    {
                        string message = Encoding.ASCII.GetString(content);
                        udpClient.Close();
                        Repository.HoseStockInfoRepository upcomRep = new Repository.HoseStockInfoRepository();
                        upcomRep.Insert(message);
                    }
                    _upcomStatus = true;
                }
                catch
                {
                    _upcomStatus = false;
                }
            }
        }
        private void GetHNXStockInfoData()
        {
            while (true)
            {
                try
                {
                    UdpClient udpClient = new UdpClient();
                    udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, HNXStockInfoPort));
                    IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, HNXStockInfoPort);
                    byte[] content = udpClient.Receive(ref remoteIPEndPoint);
                    if (content.Length > 0)
                    {
                        string message = Encoding.ASCII.GetString(content);
                        udpClient.Close();
                        Repository.HNXStockInfoRepository upcomRep = new Repository.HNXStockInfoRepository();
                        upcomRep.Insert(message);
                    }
                    _upcomStatus = true;
                }
                catch
                {
                    _upcomStatus = false;
                }
            }
        }
        private void GetUpComStockInfoData()
        {
            while (true)
            {
                try
                {
                    UdpClient udpClient = new UdpClient();
                    udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, UpComStockInfoPort));
                    IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, UpComStockInfoPort);
                    byte[] content = udpClient.Receive(ref remoteIPEndPoint);
                    if (content.Length > 0)
                    {
                        string message = Encoding.ASCII.GetString(content);
                        udpClient.Close();
                        Repository.UpComStockInfoRepository upcomRep = new Repository.UpComStockInfoRepository();
                        upcomRep.Insert(message);
                    }
                    _upcomStatus = true;
                }
                catch
                {
                    _upcomStatus = false;
                }
            }
        }
        private void btAction_Click(object sender, EventArgs e)
        {
            if (btAction.Text == "Bắt đầu")
            {
                btAction.Text = "Ngưng";
                tmStatus.Start();
                //threadHoseMarket = new Thread(GetHOSEMarketData);
                //threadHoseMarket.Start();
                //threadHNXMarket = new Thread(GetHNXMarketData);
                //threadHNXMarket.Start();
                //threadUpcomMarket = new Thread(GetUpComMarketData);
                //threadUpcomMarket.Start();
                threadHoseStockInfo = new Thread(GetHoseStockInfoData);
                threadHoseStockInfo.Start();
                threadHNXStockInfo = new Thread(GetHNXStockInfoData);
                threadHNXStockInfo.Start();
                threadUpComStockInfo = new Thread(GetUpComStockInfoData);
                threadUpComStockInfo.Start();    
                //send order thread
                Repository.OrderRepository orderRep = new Repository.OrderRepository();
                sendNewOrderThread = new Thread(orderRep.BrowseNewOrder);
                sendNewOrderThread.Start();

                SocketClient socketClient = new SocketClient();
                socketClient.StartListedFromServer();
            }
            else
            {
                btAction.Text = "Bắt đầu";
                threadHoseMarket.Abort();
                threadHNXMarket.Abort();
                threadUpcomMarket.Abort();
                sendNewOrderThread.Abort();
                tmStatus.Stop();
                pnHoseStatus.BackColor = Color.Transparent;
                pnHNXStatus.BackColor = Color.Transparent;
                pnUpComStatus.BackColor = Color.Transparent;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            HoseMarketPort = int.Parse(configuration.AppSettings.Settings["HoseMarketPort"].Value);
            HNXMarketPort = int.Parse(configuration.AppSettings.Settings["HNXMarketPort"].Value);
            UpComMarketPort = int.Parse(configuration.AppSettings.Settings["UpComMarketPort"].Value);
            HoseStockInfoPort = int.Parse(configuration.AppSettings.Settings["HoseStockInfoPort"].Value);
            HNXStockInfoPort = int.Parse(configuration.AppSettings.Settings["HNXStockInfoPort"].Value);
            UpComStockInfoPort = int.Parse(configuration.AppSettings.Settings["UpComStockInfoPort"].Value);
        }
       
        private void tmStatus_Tick(object sender, EventArgs e)
        {
            SetBackColorStatus(pnHoseStatus,_hoseStatus);
            SetBackColorStatus(pnHNXStatus,_hnxStatus);
            SetBackColorStatus(pnUpComStatus,_upcomStatus);  
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
