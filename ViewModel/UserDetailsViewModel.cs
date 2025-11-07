using OPPNandoSalvatierraSYSM9.Managers;
using OPPNandoSalvatierraSYSM9.Model;
using OPPNandoSalvatierraSYSM9.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace OPPNandoSalvatierraSYSM9.ViewModel
{
    public class UserDetailsViewModel : ViewModelBase
    {
        // Privata fält för konstruktorer och properties
        private readonly UserManager _userManager;
        private readonly User? _första; // Håller referens till den aktuella(första användarnamn innan redigering) användaren
        private bool _isEditing; // Ser till att man kan redigera fälten
        private string _country = ""; // Standardvärde för country
        private string _username = "";
        private string _newPassword = "";
        private string _confirmPassword = "";

        // Konstruktor
        public UserDetailsViewModel(UserManager userManager)
        {
            _userManager = userManager;
            _första = _userManager.CurrentUser; 

            if (_första != null)
            {
                Username = _första.Username;
                Country = _första.Country;
            }

            Countries = new ObservableCollection<string> { "Sweden", "Norway", "Denmark" };

            EditCommand = new RelayCommand(_ => IsEditing = true, _ => !IsEditing); // aktiverar commandon i fönstret
            SaveCommand = new RelayCommand(_ => Save(), _ => IsEditing);
            CancelCommand = new RelayCommand(_ => Cancel(), _ => IsEditing);


        }

       


        // Properties

        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                _isEditing = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsReadOnly)); // meddealar att informationen har ändrats 
            }
        }

        public bool IsReadOnly => !IsEditing; // gör så att man inte kan ändra fälten när IsEditing är false

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Country
        {
            get => _country;
            set
            {
                _country = value;
                OnPropertyChanged();
            }
        }

        public string NewPassword
        {
            get => _newPassword;
            set { _newPassword = value ?? ""; OnPropertyChanged(); }
        }
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set { _confirmPassword = value ?? ""; OnPropertyChanged(); }
        }

        public ObservableCollection<string> Countries { get; }

        // Commands

        public ICommand EditCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        // Events

        public event EventHandler? CloseRequested;

        // Methods

        private void Save()
        {
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3) // Användarnamn får inte vara tomt
            {
                MessageBox.Show("Användarnamnet är mindre än 3 tecken \n eller ej inmatad!");

                return;
            }

            if (_första == null) // Denna säger att det måste finnas en inloggad användare för att kunna spara ändringar
            {
                MessageBox.Show("Ingen användare är inloggad.");
                return;
            }

            if (!Username.Equals(_första.Username, StringComparison.OrdinalIgnoreCase) && _userManager.UserExists(Username)) // rätt så självklar(kollar om användarnamnet redan finns)
            {
                MessageBox.Show("Användarnamn finns redan");
                return;
            }
            if (!string.IsNullOrWhiteSpace(NewPassword))
            {
                if (NewPassword.Length < 5) // Lösenord måste vara minst 5 tecken
                {
                    MessageBox.Show("Lösenord måste vara minst 5 tecken långt.");
                    return;
                }
                if (NewPassword != ConfirmPassword) // Kollar så att lösenordet och bekräfta lösenordet är samma
                {
                    MessageBox.Show("Lösenorden matchar inte.");
                    return;
                }
            }

            _första.Username = Username;// uppdaterar användarnamnet och landet för den inloggade användaren
            _första.Country = Country;// uppdaterar användarnamnet och landet för den inloggade användaren

            if (!string.IsNullOrWhiteSpace(NewPassword))
            {
                _första.Password = NewPassword;
            }

            NewPassword = "";
            ConfirmPassword = "";


            IsEditing = false;
            CloseRequested?.Invoke(this, EventArgs.Empty);

        }

        private void Cancel() // metod som säger till appen att avbryta redigeringen
        {
            if (_första != null) 
            {
                Username = _första.Username;
                Country = _första.Country;
            }
            else
            {
                Username = string.Empty; // detta är standardvärden om ingen användare är inloggad
                Country = string.Empty;
            }

            NewPassword = "";
            ConfirmPassword = "";
            IsEditing = false; // töm alla fält och återgå till ursprungliga värden
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }


        // Inte alls färdigt än! Målet var att fönstret skulle öppnas utan problem
    }
}
