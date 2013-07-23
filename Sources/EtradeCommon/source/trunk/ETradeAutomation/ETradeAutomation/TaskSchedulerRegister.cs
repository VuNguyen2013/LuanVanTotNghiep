using System.ComponentModel;
using System.Configuration.Install;


namespace ETradeAutomation
{
    [RunInstaller(true)]
    public partial class TaskSchedulerRegister : Installer
    {
        public TaskSchedulerRegister()
        {
            InitializeComponent();
        }
    }
}
