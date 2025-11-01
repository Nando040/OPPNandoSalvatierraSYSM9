using OPPNandoSalvatierraSYSM9.Managers;
using OPPNandoSalvatierraSYSM9.View;
using OPPNandoSalvatierraSYSM9.ViewModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace OPPNandoSalvatierraSYSM9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Konstruktor
      
        public MainWindow()
        {
            InitializeComponent();

            //Kod som hjälper till att hämta UserManager från App.xaml.cs till ViewModel

            var userManager = (UserManager)Application.Current.Resources["UserManager"];

            // Koden kopplas till MVVM genom att nå codebhind som finns i LoginViewModel

            var vm = new LoginViewModel(App.UserManager);
            DataContext = vm;


            vm.OpenRegisterRequested += (_, __) =>
                new RegisterWindow { Owner = this }.ShowDialog();
        }

     

        // Codebehinind för PasswordBox för att binda lösenordet till ViewModel

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm)
            {
                vm.Password = Passbox.Password;
            }
        }

     
    }
}