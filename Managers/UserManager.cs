using OPPNandoSalvatierraSYSM9.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace OPPNandoSalvatierraSYSM9.Managers
{
    public class UserManager : INotifyPropertyChanged
    {

        // Attributer/properties
        private User? _currentUser;
        private List<User> _users = new();

        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
                // Meddelar UI om att CurrentUser har ändrats
                OnPropertyChanged(nameof(IsAuthenticated));
                // Meddelar UI om att IsAuthenticated har ändrats
            }

        }

        public bool IsAuthenticated
        {
            get { return _currentUser != null; } // kod som ser till om en användare är inloggad
        }

        // Metoder
        public bool Login(string username, string password)
        {
            foreach (var u in _users)
            {
                if (string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase)
                    && u.Password == password)
                {
                    CurrentUser = u;
                    return true;
                }
            }
            return false;
        }

        public void Logout()
        {
            CurrentUser = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    


    }
}
