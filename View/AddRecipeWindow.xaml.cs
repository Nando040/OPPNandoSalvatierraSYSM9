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
    /// Interaction logic for AddRecipeWindow.xaml
    /// </summary>
    public partial class AddRecipeWindow : Window
    {
        public AddRecipeWindow(UserManager userManager)
        {
            InitializeComponent();
            var vm = new AddRecipeViewModel(userManager);
            vm.CloseRequested += (_, __) => this.Close();
            DataContext = vm;
        }
    }
}
