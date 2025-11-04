using OPPNandoSalvatierraSYSM9.Managers;
using OPPNandoSalvatierraSYSM9.MVVM;
using System;
using System.Windows;
using System.Windows.Input;

namespace OPPNandoSalvatierraSYSM9.ViewModel
{
    public class RecipeListViewModel : ViewModelBase
    {
        private readonly UserManager _userManager;

        public ICommand LogoutCommand { get; }

        public RecipeListViewModel(UserManager userManager)
        {
            _userManager = userManager;
            LogoutCommand = new RelayCommand(_ => Logout());
        }

        private void Logout()
        {
            _userManager.Logout();
            
            // Stäng RecipeListWindow och öppna MainWindow
            Application.Current.Dispatcher.Invoke(() =>
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();
                Application.Current.MainWindow = mainWindow;
                
                // Hitta och stäng RecipeListWindow
                foreach (Window window in Application.Current.Windows)
                {
                    if (window is View.RecipeListWindow)
                    {
                        window.Close();
                        break;
                    }
                }
            });
        }
    }
}
