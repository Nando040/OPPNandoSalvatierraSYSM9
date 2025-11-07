using OPPNandoSalvatierraSYSM9.Managers;
using OPPNandoSalvatierraSYSM9.MVVM;
using OPPNandoSalvatierraSYSM9.View;
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
        public string _error = " ";



        public string Username // Property för Username i LoginView så att UserManager kan använda den
        {
            get => _username; //hämtar värdet av _username och ber om att ge användarnamnet
            set { _username = value; OnPropertyChanged(); CommandManager.InvalidateRequerySuggested(); } //sätter värdet av _username och meddelar UI att propertyn har ändrats
            // och använder eventen för att uppdatera CanExecute statusen för kommandon så att knappar kan aktiveras/deaktiveras baserat på inmatningen
        }
        public string Password // Property för Password i LoginView så att UserManager kan använda den
        {
            get => _password; //hämtar värdet av _password med andra ord"ge mig nuvarande Lösenord"
            set { _password = value; OnPropertyChanged(); CommandManager.InvalidateRequerySuggested(); }// hantera lösenordet som matas in av användaren
            //använder båda event för att meddela UI att propertyn har ändrats
        }

        public string Error // Property för att visa felmeddelande i LoginView om inloggningen misslyckas
        {
            get => _error; // hämtar feltexten
            set { 
                _error = value; OnPropertyChanged(); } // ser till att feltexten syns
        }

       
        public ICommand LoginCommand { get; } // används för att Koppla Buttons utan Click i MainWindow.xaml.cs}
        public ICommand OpenRegisterCommand { get; } // används för att öppna RegisterWindow utan Click i MainWindow.xaml.cs


        //Konstruktor
        public LoginViewModel(UserManager userManager) // konstruktorn tar emot all data från UserManager som är lagrade i parametrarna
        {
            _userManager = userManager;
            LoginCommand = new RelayCommand(execute => Login(), canExecute => CanLogin()); // Gör så att Xaml kan se om login knappen är aktiv
            OpenRegisterCommand = new RelayCommand(_ => OpenRegister());// Gör så att Xaml kan se om register knappen är aktiv
        }


        private bool CanLogin() => // med denna metoden så vet UI(xaml) om knappen ska vara aktiv eller inte
            //om den ska vara ljusgrå(går inte att trycka( eller grå(går att trycka)
            !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password); // Ser till om att både username och password är inmatade
        



        // Metoder
        private void Login()// metoden som hanterar inloggningen
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Fyll i både användarnamn och lösenord.", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var userInput = Username.Trim();

            if (_userManager.Login(userInput, Password))
            {
                Error = "";
                MessageBox.Show($"Välkommen {_userManager.CurrentUser?.DisplayName ?? userInput}!", "Inloggning lyckades", MessageBoxButton.OK, MessageBoxImage.Information);
                OnLoginSuccess?.Invoke(this, System.EventArgs.Empty);
            }
            else
            {
                Error = "Fel användarnamn eller lösenord.";
                MessageBox.Show("Fel användarnamn eller lösenord.", "Inloggning misslyckades", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event EventHandler? OpenRegisterRequested; // event som meddelar MainWindow att öppna RegisterWindow
        private void OpenRegister()// metod för att öppna RegisterWindow
        {
            OpenRegisterRequested?.Invoke(this, EventArgs.Empty);// koden som ser till att eventet körs
        }

        public event EventHandler? OnLoginSuccess; // meddelar andra filer om att inloggningen lyckades



   
     
        


       

        



    }
}
