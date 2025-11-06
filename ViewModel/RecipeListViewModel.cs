using OPPNandoSalvatierraSYSM9.Managers;
using OPPNandoSalvatierraSYSM9.Model;
using OPPNandoSalvatierraSYSM9.MVVM;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace OPPNandoSalvatierraSYSM9.ViewModel
{
    public class RecipeListViewModel : ViewModelBase
    {
        private readonly UserManager _userManager; // Låter denna klassen veta vem som är inloggad just nu
        private Recipe _selectedRecipe; // recetp som den inloggade användaren har valt
        private UserManager userManager;

        public RecipeListViewModel(UserManager userManager) // en  konstruktorn tar emot UserManager som parameter
        {
            _userManager = userManager;

            Recipes = new ObservableCollection<Recipe>( // skapar en lista som Xaml kan använda sig av för att visa recepten
                                                        // det uppdateras automatiskt när något läggs till eller tas bort
                _userManager.CurrentUser?.Recipes != null // kollar om den inloggade användaren har några recept och hämntar från Recipes
                    ? _userManager.CurrentUser.Recipes
                    : new List<Recipe>() // om det inte finns några recept så skapas en tom lista
            );


            AddRecipeCommand = new RelayCommand(_ => AddRecipe()); // detta är en command som ser till att fönstret öppnas för att lägga till recept
            RemoveRecipeCommand = new RelayCommand(_ => RemoveRecipe(), _ => SelectedRecipe != null); // denna command tar bort det valda receptet från listan
            LogoutCommand = new RelayCommand(_ => Logout()); // denna command loggar ut den inloggade användaren och går tillbaka till inloggningsfönstret

            RecipeDetailCommand = new RelayCommand(_ => OnViewRecipeRequested?.Invoke(this, EventArgs.Empty), _ => SelectedRecipe != null);
            UserDetailsCommand = new RelayCommand(_ => OnViewUserDetailsRequested?.Invoke(this, EventArgs.Empty));
        }

        //Commands
        public ICommand AddRecipeCommand { get; }
        public ICommand RemoveRecipeCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand RecipeDetailCommand { get; }
        public ICommand UserDetailsCommand { get; }

        public string CurrentUserName => _userManager.CurrentUser?.DisplayName ?? "Ghost"; // en property som visar namnet på den inloggade användaren eller "Ghost" om ingen är inloggad

        public ObservableCollection<Recipe> Recipes { get; } // en property som håller alla recept i en lista som Xaml kan använda därav "observable"

        public Recipe SelectedRecipe  // Denna property håller koll på vilket recept man har valt i UI
        {
            get => _selectedRecipe; // visar dig vilket recept som användaren klickat på
            set
            {
                _selectedRecipe = value; // sätter det valda receptet till det användaren klickat på
                OnPropertyChanged(nameof(SelectedRecipe)); // event som meddelar att användaren har valt ett nytt recept
                CommandManager.InvalidateRequerySuggested(); // Är det som säger till knapparna att ändra sig baserat på vad som är valt
                // t.ex. som att remove knappen blir aktiv när ett recept är valt
            }
        }

        private void RemoveRecipe() // Metod som säger till appen att ta bort det valda receptet
        {
            if (SelectedRecipe == null) return; // om inget recept är valt så görs inget
            Recipes.Remove(SelectedRecipe); // tar bort det valda receptet från den lokala listan som föstret använder
            _userManager.CurrentUser?.Recipes.Remove(SelectedRecipe); // tar bort det valda receptet från den inloggade användarens receptlista
        }

        
        private void AddRecipe() // Metod som säger till appen att öppna fönstret för att lägga till ett nytt recept
        {
            OnAddRecipeRequested?.Invoke(this, EventArgs.Empty); // Koden som ser till att eventet körs(lägg till recept)
        }

        private void Logout() // Metod som loggar ut den inloggade användaren
        {
            _userManager.CurrentUser = null; // CurrentUser blir null = ingen är inloggad längre men appen körs fortfarande
            OnSignOutRequested?.Invoke(this, EventArgs.Empty); // Koden som ser till att eventet körs(logga ut)
        }



        
        public event EventHandler? OnSignOutRequested; // Event som meddelar RecipeListWindow att användaren vill logga ut
        public event EventHandler? OnAddRecipeRequested; // Event som meddelar RecipeListWindow att användaren vill lägga till ett nytt recept
        public event EventHandler? OnViewRecipeRequested;
        public event EventHandler? OnViewUserDetailsRequested;
        public void RefreshRecipes() // Metod som uppdaterar listan med recept från den inloggade användaren
        {
            Recipes.Clear(); // rensar den lokala listan först
            if (_userManager.CurrentUser?.Recipes == null) return; // om den inloggade användaren inte har några recept så görs inget mer
            foreach (var r in _userManager.CurrentUser.Recipes) // för varje r(recept) i den inloggade användarens receptlista
                Recipes.Add(r); // läggs det till i den lokala listan som visas i fönstret 
        }
    }
}
