using OPPNandoSalvatierraSYSM9.Managers;
using OPPNandoSalvatierraSYSM9.ViewModel;
using System;
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
        private UserManager? _userManager;
        private RecipeManager? _recipeManager;
        public RecipeListWindow()
        {
            InitializeComponent();

            var userManager = App.UserManager;
            _userManager = userManager;
            var vm = new RecipeListViewModel(userManager); // En variabel som kopplar upp Filen till resten av Logiken av programmet
            DataContext = vm; // Kopplar upp denna filen till xaml och ViewModelen dvs till all {Bindind} i xaml
            Loaded += (_, __) => vm.RefreshRecipes();

            //metoder
            vm.OnSignOutRequested += (_, __) =>// en Metod som kopplar upp det till eventet OnSignOutRequested
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

            
            vm.OnAddRecipeRequested += (_, __) =>// en Metod som kopplar upp det till eventet OnAddRecipeRequested
            {
                var addWindow = new AddRecipeWindow(userManager); // skapar ett nytt fönster för lägga till recept som är kopplat
                // till användarna i UserManager
                addWindow.Owner = this;// Detta säger att AddRecipeWindow är barn till RecipeListWindow
                addWindow.ShowDialog();// Öppnar ny fönster till Addrecipe, och att det måste stängas inna föräldern kan öppnas igen
                vm.RefreshRecipes();// Inlagda receptet är nu Uppdaterat och de andra filer i programmet kan se det efter
                //fönstret har stängts
            };

            vm.OnViewRecipeRequested += (_, __) => // en Metod som kopplar upp det till eventet OnViewRecipeRequested
            {
                if (vm.SelectedRecipe == null)
                return;
                var detail = new RecipeDetailWindow(userManager, vm.SelectedRecipe);
                detail.Owner = this;
                detail.ShowDialog();
                vm.RefreshRecipes();
            };

            vm.OnViewUserDetailsRequested += (_, __) => // en Metod som kopplar upp det till eventet OnViewUserDetailsRequested
            {
              
                 var userDetail = new UserDetailWindow(userManager);
                 userDetail.Owner = this;
                 userDetail.ShowDialog();
             
            };
        }

        private void InfoBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show
            ( "Här har du alla enkla recept som användarna lägger upp!\n\n" +
              "Dela med dig och bli en COOKMASTER med oss idag!",
              "OM CookMaster", MessageBoxButton.OK, MessageBoxImage.Information);

        }
    }
}
