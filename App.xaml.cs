using OPPNandoSalvatierraSYSM9.Managers;
using System.Configuration;
using System.Data;
using System.Windows;

namespace OPPNandoSalvatierraSYSM9
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static UserManager UserManager { get; private set; }

        public App()
        {
            // Skapa UserManager direkt när appen startar
            UserManager = new UserManager();
        }
    }

}
