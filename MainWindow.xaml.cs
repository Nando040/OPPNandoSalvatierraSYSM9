using OPPNandoSalvatierraSYSM9.Managers;
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
        private string _username;

        public string _password;

        public string _error;

        public string Username
        {
            get => _username; 
            set { _username = value; OnPropertyChanged(); }
        }
        public string Password
        {
            get => _password; 
            set { _password = value; OnPropertyChanged(); }
        }

        public string Error
        {
            get => _error; 
            set { _error = value; OnPropertyChanged(); }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = Passbox.Password;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var userManager = (UserManager)Application.Current.Resources["UserManager"];
            var success = userManager.Login(Username, Password);

            if (success)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                Error = "Fel användarnamn eller lösenord.";
            }

        }



        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

      
    }
}