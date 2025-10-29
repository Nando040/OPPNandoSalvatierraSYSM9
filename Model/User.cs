using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPPNandoSalvatierraSYSM9.Model
{
    public class User
    {
        //Attributer/properties
        public string Username { get; set; }
        public string Password { get; set; }

        public string DisplayName { get; set; }

        public string Role { get; set; }

        public string Country { get; set; }

        //Konstruktor
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
