﻿using System;
using System.Configuration;
using System.Windows.Forms;
using System.Xml;

namespace InstallHelper
{
    public partial class WebServiceConfiguration : Form
    {
        public string ConfigFilePath { get; set; }

        public WebServiceConfiguration()
        {
            InitializeComponent();
            ConfigFilePath = @"C:\Program Files (x86)\OTS\ETrade Automation\ETradeAutomation.exe";
        }

        private void buttonSkip_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                string address = txtAddress.Text.Trim();
                string rtAddress = txtRTAddress.Text.Trim();
                string amAddress = txtAMAddress.Text.Trim();
                if (string.IsNullOrEmpty(address) && string.IsNullOrEmpty(rtAddress) && string.IsNullOrEmpty(amAddress))
                {
                    MessageBox.Show("Please input Web Service address!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAddress.Focus();
                    return;
                }
                var config = ConfigurationManager.OpenExeConfiguration(ConfigFilePath);
                var sectionGroup = config.GetSectionGroup("applicationSettings");

                if (sectionGroup != null)
                {
                    var section = sectionGroup.Sections["ETradeAutomation.Properties.Settings"] as ClientSettingsSection;
                    if (section != null)
                    {
                        if (!string.IsNullOrEmpty(address))
                        {
                            var settingElement =
                                section.Settings.Get("ETradeAutomation_ETradeWebService_ETradeServicesWebServices");

                            // Create new value node
                            var doc = new XmlDocument();
                            var newValue = doc.CreateElement("value");
                            newValue.InnerText = address;

                            // Set new address value
                            settingElement.Value.ValueXml = newValue;
                        }

                        if (!string.IsNullOrEmpty(rtAddress))
                        {
                            var settingElement =
                                section.Settings.Get("ETradeAutomation_RTWebService_Service");

                            // Create new value node
                            var doc = new XmlDocument();
                            var newValue = doc.CreateElement("value");
                            newValue.InnerText = rtAddress;

                            // Set new address value
                            settingElement.Value.ValueXml = newValue;
                        }

                        if (!string.IsNullOrEmpty(amAddress))
                        {
                            var settingElement =
                                section.Settings.Get("ETradeAutomation_AMServices_AccountManagerServices");

                            // Create new value node
                            var doc = new XmlDocument();
                            var newValue = doc.CreateElement("value");
                            newValue.InnerText = amAddress;

                            // Set new address value
                            settingElement.Value.ValueXml = newValue;
                        }
                        

                        // Save changes
                        section.SectionInformation.ForceSave = true;
                    }
                }
                config.Save(ConfigurationSaveMode.Modified);
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Cannot configure web service address. Please update configuration file after installing application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show(exception.Message + "|" + ConfigFilePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
