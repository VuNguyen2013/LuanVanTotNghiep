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
    public partial class AddNewIP : Form
    {
        public AddNewIP()
        {
            InitializeComponent();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void lbIPAdd_Click(object sender, EventArgs e)
        {
            System.Net.IPAddress ip;
            if (!System.Net.IPAddress.TryParse(txtIP.Text, out ip))
            {
                MessageBox.Show("IP không hợp lệ","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            var ipInfo = new Entities.IPInfo();
            ipInfo.IP = txtIP.Text;
            ipInfo.CompanyName = txtCompanyName.Text;
            ipInfo.Insert();
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }
    }
}
