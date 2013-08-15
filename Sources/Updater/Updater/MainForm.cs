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
        Thread threadHoseMarket;

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
                    }
                    _hoseStatus = true;
                }
                catch
                {
                    _hoseStatus = false;
                }
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
            try
            {
               
            }
            catch
            {
                _hnxStatus = false;
            }
        }
        private void GetUpComMarketData()
        {
            try
            {
            }
            catch
            {
                _upcomStatus = false;
            }
        }
        private void btAction_Click(object sender, EventArgs e)
        {
            if (btAction.Text == "Bắt đầu")
            {
                btAction.Text = "Ngưng";
                tmStatus.Start();
                threadHoseMarket = new Thread(GetHOSEMarketData);
                threadHoseMarket.Start();
                //GetHOSEMarketData();
                //GetHNXMarketData();
                //GetUpComMarketData();
            }
            else
            {
                btAction.Text = "Bắt đầu";
                threadHoseMarket.Abort();
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
            txtServerIP.Text = configuration.AppSettings.Settings["ServerIP"].Value;
        }

        private void txtServerIP_Leave(object sender, EventArgs e)
        {
            System.Net.IPAddress ipAdress;
            if (!System.Net.IPAddress.TryParse(txtServerIP.Text,out ipAdress))
            {
                MessageBox.Show("IP không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtServerIP.Focus();
                return;
            }
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["ServerIP"].Value = txtServerIP.Text;
            configuration.Save();
            ConfigurationManager.RefreshSection("appSettings");
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
