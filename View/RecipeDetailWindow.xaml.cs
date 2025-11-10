using OPPNandoSalvatierraSYSM9.Managers;
using OPPNandoSalvatierraSYSM9.Model;
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
using static System.Net.Mime.MediaTypeNames;

namespace OPPNandoSalvatierraSYSM9.View
{
    /// <summary>
    /// Interaction logic for RecipeDetailWindow.xaml
    /// </summary>
    public partial class RecipeDetailWindow : Window
    {
        public RecipeDetailWindow(UserManager userManager, Recipe recipe)
        {
            InitializeComponent();

            var vm = new RecipeDetailViewModel((UserManager)System.Windows.Application.Current.Resources["UserManager"], recipe); // En variabel som kopplar upp Filen till resten av Logiken av programmet
            vm.CloseRequested += (_, __) => this.Close(); // Metod som kopplar upp det till eventet CloseRequested i ViewModel
            DataContext = vm; // Kopplar upp denna filen till xaml och ViewModelen dvs till all {Binded} i xaml
        }

        
    }
    
}
