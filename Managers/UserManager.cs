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
        private User _currentUser;
        private List<User> _users;

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
            get { return _currentUser != null; }
        }


        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
