using OPPNandoSalvatierraSYSM9.Managers;
using OPPNandoSalvatierraSYSM9.ViewModel;
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
    /// Interaction logic for RecipeListWindow.xaml
    /// </summary>
    public partial class RecipeListWindow : Window
    {
        public RecipeListWindow(UserManager userManager)
        {
            InitializeComponent();

            var vm = new RecipeListViewModel(userManager);
            DataContext = vm;

            // Subscribe to sign-out request and open MainWindow, then close this window.
            vm.OnSignOutRequested += (_, __) =>
            {
               
                Application.Current.Dispatcher.Invoke(() => // ser till att koden körs på xaml(UI tråden)
                // med andra ord ser till att koden körs på appens huvudtråd
                {
                    var mainWindow = new MainWindow(); // Skapar ett nytt fönster av typen MainWindow
                    Application.Current.MainWindow = mainWindow;// Sätter det nya fönstret som huvudfönster i applikationen

                    mainWindow.Show();// Visar MainWindow

                    this.Close();// Stänger RecipeListWindow
                });
            };
        }
    }
}
