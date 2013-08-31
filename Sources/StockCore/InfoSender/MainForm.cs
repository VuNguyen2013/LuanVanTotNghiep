﻿using System;
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
        private bool _status = true;

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
            var listIPInfo = StockCore.InfoSender.Repositories.IpInfoRepository.GetAll();
            var listboxItem = new List<ListBoxItem>();
            foreach (var itemm in listIPInfo)
            {
                _listIP.Add(itemm.IP);
                listboxItem.Add(new ListBoxItem() { Value = itemm.IP,Name=itemm.IP+" - "+itemm.CompanyName});
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
            }
            else
            {
                btAction.Text = "Bắt đầu";
                tmAction.Stop();
                pnStatus.BackColor = Color.Transparent;
            }
        }

        private void tmAction_Tick(object sender, EventArgs e)
        {
            InfoSender.Entities.SendData senData = new Entities.SendData();
            senData.SendMarketData(_listIP,ref _status);            
            
            if (pnStatus.BackColor == Color.DodgerBlue)
            {
                pnStatus.BackColor = Color.Transparent;
            }
            else if (_status)
            {
                pnStatus.BackColor = Color.DodgerBlue;
            }
        }
    }
}