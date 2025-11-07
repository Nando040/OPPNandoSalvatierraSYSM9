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
        public MainWindow()
        {
            InitializeComponent();

            

            var userManager = (UserManager)Application.Current.Resources["UserManager"];//Kod som hjälper till att hämta UserManager från App.xaml.cs till ViewModel

            

            var vm = new LoginViewModel(userManager);// Koden kopplas till MVVM genom att nå codebhind som finns i LoginViewModel
            DataContext = vm;// Sätter DataContext för MainWindow till vm (LoginViewModel)

            vm.OnLoginSuccess += (s, e) =>// Detta säger till eventet OnLogin... att ignorera (Sender,args)
              //och att den ska fokusera på kodden som kommer efteråt
            {
                
                Dispatcher.Invoke(() =>// ser till att koden körs på xaml(UI tråden)
                {
                    var list = new RecipeListWindow();// Skapar ett nytt fönster av typen RecipeListWindow och skickar med userManager
                    Application.Current.MainWindow = list;// Sätter det nya fönstret som huvudfönster i applikationen
                    list.Show();// Visar RecipeListWindow


                    Application.Current.MainWindow = list;// Sätter det nya fönstret som huvudfönster i applikationen


                    Close();// Stänger MainWindow (inloggningsfönstret)
                });
            };



            vm.OpenRegisterRequested += (_, __) =>
            {
                var registerWindow = new RegisterWindow(); // Skapar ett nytt fönster av typen RegisterWindow

                Application.Current.MainWindow = registerWindow;// Sätter det nya fönstret som huvudfönster i applikationen
                registerWindow.Show();

                this.Close();// Stänger MainWindow (inloggningsfönstret)
            };
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e) //Event som körs när lösenordet ändras i PasswordBox
        {
            if (DataContext is LoginViewModel vm) //kod som säkertställer att DataContext är kopplat till LoginViewModel
            {
                vm.Password = Passbox.Password; //Sätter lösenordet i ViewModel till det som finns i PasswordBox
                //med andra ord ser till så att lösenordet i ViewModel uppdateras när användaren skriver in sitt lösenord
                CommandManager.InvalidateRequerySuggested();// Commandot som används för att uppdatera CanExecute statusen för kommandon
            }
        }

        //reflektion "kommwer snart"
     
    }
}