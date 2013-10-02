using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockCore.InfoSender
{
    public partial class MainForm : Form
    {
        InfoSender.Entities.SendData sendData = null;
        System.Threading.Thread matchedThread;
        public MainForm()
        {
            InitializeComponent();
        }
        private List<string> _listIP = new List<string>();
        private void lbIPAdd_Click(object sender, EventArgs e)
        {
            AddNewIP addNewIP = new AddNewIP();
            addNewIP.StartPosition = FormStartPosition.CenterParent;
            if (addNewIP.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }
        private class ListBoxItem
        {
            public string Value { get; set; }
            public string Name { get; set; }
        }
        private void LoadData()
        {
            var memberRep = new Repositories.MemberStockCompanyRepository();
            var listIPInfo = memberRep.GetAll();
            var listboxItem = new List<ListBoxItem>();
            foreach (var itemm in listIPInfo)
            {
                _listIP.Add(itemm.ServerIp);
                listboxItem.Add(new ListBoxItem() { Value = itemm.ServerIp,Name=itemm.ServerIp+" - "+itemm.ShortNameVi});
            }
            lbIP.DataSource = listboxItem;
            lbIP.ValueMember = "Value";
            lbIP.DisplayMember = "Name";
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btIPDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muỗn xóa?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (Entities.IPInfo.Delete(lbIP.SelectedValue.ToString()))
                {
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Lỗi hệ thống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btAction_Click(object sender, EventArgs e)
        {
            if (btAction.Text == "Bắt đầu")
            {
                btAction.Text = "Ngưng";
                tmAction.Start();
                sendData = new Entities.SendData(_listIP);
                sendData.SendInfo();                
                StockCore.Repositories.OrderRepository orderRep = new StockCore.Repositories.OrderRepository();
                matchedThread = new System.Threading.Thread(orderRep.DoMatchedOrder);
                matchedThread.Start();
            
            }
            else
            {
                sendData.threadSendMarketData.Abort();
                sendData.threadSendStockInfoData.Abort();
                btAction.Text = "Bắt đầu";
                tmAction.Stop();
                matchedThread.Abort();
                pnStatus.BackColor = Color.Transparent;
            }
        }

        private void tmAction_Tick(object sender, EventArgs e)
        {          
            if (pnStatus.BackColor == Color.DodgerBlue)
            {
                pnStatus.BackColor = Color.Transparent;
            }
            else if (sendData.Status)
            {
                pnStatus.BackColor = Color.DodgerBlue;
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
