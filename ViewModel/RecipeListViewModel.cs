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
        private readonly UserManager _userManager;
        private Recipe _selectedRecipe;

        public RecipeListViewModel(UserManager userManager)
        {
            _userManager = userManager;

            Recipes = new ObservableCollection<Recipe>(
                _userManager.CurrentUser?.Recipes != null
                    ? _userManager.CurrentUser.Recipes
                    : new List<Recipe>()
            );

            AddRecipeCommand = new RelayCommand(_ => AddRecipe());
            RemoveRecipeCommand = new RelayCommand(_ => RemoveRecipe(), _ => SelectedRecipe != null);
            LogoutCommand = new RelayCommand(_ => Logout());
        }

        public ICommand AddRecipeCommand { get; }
        public ICommand RemoveRecipeCommand { get; }
        public ICommand EditRecipeCommand { get; }
        public ICommand LogoutCommand { get; }

        public string CurrentUserName => _userManager.CurrentUser?.DisplayName ?? "Ghost";

        public ObservableCollection<Recipe> Recipes { get; }

        public Recipe SelectedRecipe
        {
            get => _selectedRecipe;
            set
            {
                _selectedRecipe = value;
                OnPropertyChanged(nameof(SelectedRecipe));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private void RemoveRecipe()
        {
            if (SelectedRecipe == null) return;
            Recipes.Remove(SelectedRecipe);
            _userManager.CurrentUser?.Recipes.Remove(SelectedRecipe);
        }

        // Instead of performing UI directly here, raise an event so the view can open the AddRecipeWindow.
        private void AddRecipe()
        {
            OnAddRecipeRequested?.Invoke(this, EventArgs.Empty);
        }

        private void Logout()
        {
            _userManager.CurrentUser = null;
            OnSignOutRequested?.Invoke(this, EventArgs.Empty);
        }



        // Events the view can subscribe to
        public event EventHandler? OnSignOutRequested;
        public event EventHandler? OnAddRecipeRequested;

        // Call this from the view after an AddRecipeWindow closes so the ObservableCollection is refreshed.
        public void RefreshRecipes()
        {
            Recipes.Clear();
            if (_userManager.CurrentUser?.Recipes == null) return;
            foreach (var r in _userManager.CurrentUser.Recipes)
                Recipes.Add(r);
        }
    }
}
