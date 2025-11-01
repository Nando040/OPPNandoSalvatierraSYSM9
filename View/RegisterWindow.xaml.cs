using OPPNandoSalvatierraSYSM9.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OPPNandoSalvatierraSYSM9.View
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {

        private readonly UserManager _userManager; // kod som skickar data till UserManager ser till att man kan använda den i RegisterWindow men inte ändra den
        public RegisterWindow()
        {
            InitializeComponent();
            _userManager = App.UserManager; // hänvisar till UserManager som man skapade i App.xaml.cs
        }

        private PasswordBox GetConfirmPassword_RegW()
        {
            return ConfirmPassword_RegW;
        }

        private void RegisterButton_RegW_Click(object sender, RoutedEventArgs e)
        {
            var username = Username_RegW.Text?.Trim(); // Hämtar username från textboxen och tar bort eventuella mellanslag
            var passw1 = Password_RegW.Password; // Hämtar lösenordet från PasswordBox
            var passw2 = ConfirmPassword_RegW.Password; // Hämtar bekräftelselösenordet från PasswordBox

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(passw1) || string.IsNullOrWhiteSpace(passw2)) // Kontrollerar att inga fält är tomma
            {
                MessageBox.Show("Vänligen fyll i alla fält.");
                return;
            }

            if (passw1 != passw2) // Kontrollerar att lösenorden matchar
            {
                MessageBox.Show("Lösenorden matchar inte.");
                return;
            }

            if(_userManager.Register(username, passw1, out string error)) // Försöker registrera användaren via UserManager
            {
                MessageBox.Show("Registrering lyckades! Du kan nu logga in.");
                this.Close(); // Stänger registreringsfönstret vid lyckad registrering
            }
            else
            {
                MessageBox.Show($"Registrering misslyckades: {error}"); // Visar felmeddelande om registreringen misslyckas
            }

           


        }

        private void Password_RegW_PasswordChanged(object sender, RoutedEventArgs e)
        {
             var vm = Password_RegW.Password;
        }

        private void ConfirmPassword_RegW_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var vm = ConfirmPassword_RegW.Password;
        }
    }
}
