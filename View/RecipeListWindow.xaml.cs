using OPPNandoSalvatierraSYSM9.Managers;
using OPPNandoSalvatierraSYSM9.ViewModel;
using System.Windows;

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
        }
    }
}
