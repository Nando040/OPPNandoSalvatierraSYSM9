using OPPNandoSalvatierraSYSM9.Managers;
using OPPNandoSalvatierraSYSM9.View;
using OPPNandoSalvatierraSYSM9.ViewModel;
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
        //Konstruktor
      
        public MainWindow()
        {
            InitializeComponent();

            //Kod som hjälper till att hämta UserManager från App.xaml.cs till ViewModel

            var userManager = (UserManager)Application.Current.Resources["UserManager"];

            // Koden kopplas till MVVM genom att nå codebhind som finns i LoginViewModel

            var vm = new LoginViewModel(userManager);
            DataContext = vm;

              vm.OnLoginSuccess += (s, e) =>// Detta säger till eventet OnLogin... att ignorera (Sender,args)
              //och att den ska fokusera på kodden som kommer efteråt
            {
                
                Dispatcher.Invoke(() =>
                {
                    var recipeListWindow = new RecipeListWindow(userManager);
                    recipeListWindow.Show();

                    
                    Application.Current.MainWindow = recipeListWindow;

                    
                    Close();
                });
            };
            


            vm.OpenRegisterRequested += (_, __) => // Dett säger till eventet Open... att ignorera (Sender,args)
            //och att den ska fokusera på kodden som kommer efteråt
                new RegisterWindow { Owner = this }.ShowDialog();// Denna säger till ViewModel att öppna ett nytt
            //fönster(RegisterWindow) som ska ägas/vara barn till LoginVindow
        }

     

        // Codebehinind för PasswordBox för att binda lösenordet till ViewModel

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm)
            {
                vm.Password = Passbox.Password;
                CommandManager.InvalidateRequerySuggested();
            }
        }

     
    }
}