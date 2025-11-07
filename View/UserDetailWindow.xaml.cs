using OPPNandoSalvatierraSYSM9.Managers;
using OPPNandoSalvatierraSYSM9.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for UserDetailWindow.xaml
    /// </summary>
    public partial class UserDetailWindow : Window
    {
        public UserDetailWindow(UserManager userManager)
        {
            InitializeComponent();
            try
            {
                var vm = new UserDetailsViewModel((UserManager)Application.Current.Resources["UserManager"]);
                vm.CloseRequested += (_, __) => this.Close();
                DataContext = vm;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ett fel uppstod vid öppning av användardetaljer: {ex.Message}", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is UserDetailsViewModel vm && sender is PasswordBox pb) // gör så resten av programmet kan komma åt lösenordet t.ex. Save() för att 
                // bekräfta längden på lösenordet
                vm.NewPassword = pb.Password;
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is UserDetailsViewModel vm && sender is PasswordBox pb)
                vm.ConfirmPassword = pb.Password;
        }
    }
}
