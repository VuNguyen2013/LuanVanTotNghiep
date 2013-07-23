using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace InstallHelper
{
    public partial class ProgramConfiguration : Form
    {
        public string ConfigFilePath{ get; set;}

        public ProgramConfiguration()
        {
            InitializeComponent();
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            string serverName = txtServerName.Text.Trim();
            string userName = txtUserName.Text.Trim();
            string password = txtPassword.Text;
            const string testResult = "Test results";
            if (string.IsNullOrEmpty(serverName))
            {
                MessageBox.Show("Please input server name!", testResult, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtServerName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("Please input user name!", testResult, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please input password!", testResult, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }
            // Test connection
            string connectionString = "Data Source={0}; User ID={1}; PassWord={2};";
            string catalog = "Initial Catalog={0}";
            SqlConnection sqlConnection = null;
            try
            {
                connectionString = string.Format(connectionString, serverName, userName, password);
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                // Test with database name
                string databaseName = comboDatabaseName.Text;
                if (!string.IsNullOrEmpty(databaseName))
                {
                    catalog = string.Format(catalog, databaseName);
                    connectionString = connectionString + catalog;

                    sqlConnection.Close();

                    sqlConnection = new SqlConnection(connectionString);
                    sqlConnection.Open();
                }
                
                MessageBox.Show("Test connection succeeded.", testResult, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Login failed for user '{0}'", userName), testResult, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                }
            }
        }

        private void buttonSkip_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboDatabaseName_DropDown(object sender, EventArgs e)
        {
            comboDatabaseName.Items.Clear();
            string serverName = txtServerName.Text.Trim();
            string userName = txtUserName.Text.Trim();
            string password = txtPassword.Text;
            string connectionString = "Data Source={0}; User ID={1}; PassWord={2};";
            try
            {
                connectionString = string.Format(connectionString, serverName, userName, password);
                var sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                var command = sqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_databases";
                var dataReader = command.ExecuteReader();
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        string dbName = dataReader.GetString(0);
                        comboDatabaseName.Items.Add(dbName);
                    }
                }
                
            }
            catch (Exception ex)
            {
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                string serverName = txtServerName.Text.Trim();
                string userName = txtUserName.Text.Trim();
                string password = txtPassword.Text;
                if (string.IsNullOrEmpty(serverName))
                {
                    MessageBox.Show("Please input server name!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtServerName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(userName))
                {
                    MessageBox.Show("Please input user name!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUserName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please input password!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }
                string databaseName = comboDatabaseName.Text;
                if (string.IsNullOrEmpty(databaseName))
                {
                    MessageBox.Show("Please input database name!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }
                // Test connection
                string connectionString = "Data Source={0}; User ID={1}; PassWord={2};Initial Catalog={3}";
                connectionString = string.Format(connectionString, serverName, userName, password, databaseName);

                var config = ConfigurationManager.OpenExeConfiguration(ConfigFilePath);

                config.AppSettings.Settings["ConnectionString"].Value = connectionString;

                config.Save(ConfigurationSaveMode.Modified);
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Cannot configure database. Please update configuration file after installing application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show(exception.Message + "|" + ConfigFilePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
