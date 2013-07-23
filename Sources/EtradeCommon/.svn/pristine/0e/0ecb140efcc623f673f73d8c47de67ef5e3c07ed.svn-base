using System.Diagnostics;
using ETradeCommon;
using InstallHelper;

namespace ETradeAutomation
{
    partial class TaskSchedulerRegister
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        #endregion

        #region Custom Actions
        protected override void OnAfterInstall(System.Collections.IDictionary savedState)
        {
            // Context contains assemblypath by default
            var assemblyPath = this.Context.Parameters["assemblypath"];

            var configuration = new ProgramConfiguration();
            configuration.ConfigFilePath = assemblyPath;
            configuration.ShowDialog();
            configuration.Focus();

            var webServiceConfiguration = new WebServiceConfiguration();
            webServiceConfiguration.ConfigFilePath = assemblyPath;
            webServiceConfiguration.ShowDialog();
            webServiceConfiguration.Focus();

            string targetdir = Context.Parameters["TARGETDIR"];

            // Create task for putting condition orders
            /*string batchCommandArgs = string.Format("/create /ru \"SYSTEM\" /sc DAILY /tn \"{0}\" /tr \"\\\"{1}{2}\\\"\" /st 08:01 ",
                Constants.CONDITION_ORDER_TASK, targetdir, Constants.CONDITION_ORDER_TASK_FILE);

            RunCommand(batchCommandArgs);*/

            // Create task for starting web services at the beginning of day
            string batchCommandArgs = string.Format("/create /ru \"SYSTEM\" /sc DAILY /tn \"{0}\" /tr \"\\\"{1}{2}\\\"\" /st 00:35 ",
                Constants.ACTIVATION_TASK, targetdir, Constants.ACTIVATION_TASK_FILE);

            RunCommand(batchCommandArgs);

            // Reset condition order table
            batchCommandArgs = string.Format("/create /ru \"SYSTEM\" /sc DAILY /tn \"{0}\" /tr \"\\\"{1}{2}\\\"\" /st 01:00 ",
                Constants.RESET_CONDITION_ORDER_TABLE_TASK, targetdir, Constants.RESET_CONDITION_ORDER_TABLE_TASK_FILE);

            RunCommand(batchCommandArgs);

            // Create task for getting USD
            batchCommandArgs = string.Format("/create /ru \"SYSTEM\" /sc MINUTE /mo 30 /tn \"{0}\" /tr \"\\\"{1}{2}\\\"\" ",
                Constants.CURRENCIES_TASK, targetdir, Constants.CURRENCIES_TASK_FILE);

            RunCommand(batchCommandArgs);

            // Create task to clean up expired cash advance
            batchCommandArgs = string.Format("/create /ru \"SYSTEM\" /sc DAILY /tn \"{0}\" /tr \"\\\"{1}{2}\\\"\" /st 17:00 ",
                Constants.CASH_ADVANCE_CLEAN_UP_TASK, targetdir, Constants.CASH_ADVANCE_CLEAN_UP_TASK_FILE);

            RunCommand(batchCommandArgs);

            //create task to update company info
            batchCommandArgs =
                string.Format("/create /ru \"SYSTEM\" /sc MINUTE /mo 10 /tn \"{0}\" /tr \"\\\"{1}{2}\\\"\" ",
                              Constants.UPDATE_COMPANY_INFO_TASK, targetdir, Constants.UPDATE_COMPANYINFO);
            RunCommand(batchCommandArgs);
            

            base.OnAfterInstall(savedState);
        }

        protected override void OnAfterUninstall(System.Collections.IDictionary savedState)
        {
            // Remove activation task
            string batchCommandArgs = string.Format("/Delete /tn \"{0}\" /f", Constants.ACTIVATION_TASK);

            RunCommand(batchCommandArgs);

            // Remove condition order task
            /*batchCommandArgs = string.Format("/Delete /tn \"{0}\" /f", Constants.CONDITION_ORDER_TASK);

            RunCommand(batchCommandArgs);*/

            // Remove Currencies updater
            batchCommandArgs = string.Format("/Delete /tn \"{0}\" /f", Constants.CURRENCIES_TASK);

            RunCommand(batchCommandArgs);

            // Remove Currencies updater
            batchCommandArgs = string.Format("/Delete /tn \"{0}\" /f", Constants.CASH_ADVANCE_CLEAN_UP_TASK);

            RunCommand(batchCommandArgs);

            // Reset condition order table
            batchCommandArgs = string.Format("/Delete /tn \"{0}\" /f", Constants.RESET_CONDITION_ORDER_TABLE_TASK);

            RunCommand(batchCommandArgs);

            // Remove Auto Update Company
            batchCommandArgs = string.Format("/Delete /tn \"{0}\" /f", Constants.UPDATE_COMPANY_INFO_TASK);

            RunCommand(batchCommandArgs);

            base.OnAfterUninstall(savedState);
        }

        #endregion

        private void RunCommand(string batchCommandArgs)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "schtasks";
            info.Arguments = batchCommandArgs;
            info.CreateNoWindow = true;
            info.UseShellExecute = false;

            Process p = new Process();
            p.StartInfo = info;

            p.Start();
            p.WaitForExit();
        }
    }
}