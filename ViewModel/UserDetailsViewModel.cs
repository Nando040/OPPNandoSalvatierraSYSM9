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
        private readonly UserManager _userManager;
        private readonly User? _första;
        private bool _isEditing;
        private string _country = "Sweden";
        private string _username = "";

        public UserDetailsViewModel(UserManager userManager)
        {
            _userManager = userManager;
            _första = _userManager.CurrentUser; // may be null, handle below

            if (_första != null)
            {
                Username = _första.Username;
                Country = _första.Country;
            }

            Countries = new ObservableCollection<string> { "Sweden", "Norway", "Denmark" };

            EditCommand = new RelayCommand(_ => IsEditing = true, _ => !IsEditing);
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
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }

        public bool IsReadOnly => !IsEditing;

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

        public string NewPassword { get; set; } = "";

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
            if (string.IsNullOrWhiteSpace(Username))
            {
                MessageBox.Show("Användarnamn behövs");
                return;
            }

            if (_första == null)
            {
                MessageBox.Show("Ingen användare är inloggad.");
                return;
            }

            if (!Username.Equals(_första.Username, StringComparison.OrdinalIgnoreCase) && _userManager.UserExists(Username))
            {
                MessageBox.Show("Användarnamn finns redan");
                return;
            }
            if (!string.IsNullOrEmpty(NewPassword) && NewPassword.Length < 5)
            {
                MessageBox.Show("Lösen ord för kort");
                return;
            }

            _första.Username = Username;
            _första.Country = Country;
            if (!string.IsNullOrEmpty(NewPassword))
            {
                _första.Password = NewPassword;
            }

            IsEditing = false;
            CloseRequested?.Invoke(this, EventArgs.Empty);

        }

        private void Cancel()
        {
            if (_första != null)
            {
                Username = _första.Username;
                Country = _första.Country;
            }
            else
            {
                Username = string.Empty;
                Country = "Sweden";
            }

            NewPassword = "";
            IsEditing = false;
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }



    }
}
