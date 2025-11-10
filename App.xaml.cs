using OPPNandoSalvatierraSYSM9.Managers;
using System.Configuration;
using System.Data;
using System.Windows;

namespace OPPNandoSalvatierraSYSM9
{
    public partial class App : Application
    {
        public static UserManager UserManager { get; } = new UserManager();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // Om du vill behålla App.xaml-resursen kan du också lägga in den här:
            Resources["UserManager"] = UserManager;
        }
    }
}
