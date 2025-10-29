using OPPNandoSalvatierraSYSM9.Managers;
using OPPNandoSalvatierraSYSM9.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OPPNandoSalvatierraSYSM9.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {

        // Attributer/properties
        private readonly UserManager _userManager;
        private string _username;
        public string _password;
        public string _error;

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); CommandManager.InvalidateRequerySuggested(); }
        }
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); CommandManager.InvalidateRequerySuggested(); }
        }

        public string Error
        {
            get => _error;
            set { _error = value; OnPropertyChanged(); }
        }

       
        public ICommand LoginCommand { get; } // används för att Koppla Buttons utan Click i MainWindow.xaml.cs

        //Konstruktor
        public LoginViewModel(UserManager userManager)
        {
            _userManager = userManager;
            LoginCommand = new RelayCommand(execute => Login(), canExecute => CanLogin());
        }

        private bool CanLogin() =>
            !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password); // Ser till om att både username och password är inmatade

        public Managers.UserManager UserManager { get; set; }


        // Metoder
        private void Login()
        {
            if (_userManager.Login(Username, Password))
             
                OnLoginSuccess?.Invoke(this, System.EventArgs.Empty);
            else
                Error = "Fel användarnamn eller lösenord.";
            // Meddelar användaren om inloggningen misslyckades
        }

        public event System.EventHandler OnLoginSuccess; // meddelar andra filer om att inloggningen lyckades



        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        // Ser till att binding funkar i XAML


    }
}
