using OPPNandoSalvatierraSYSM9.Managers;
using OPPNandoSalvatierraSYSM9.Model;
using OPPNandoSalvatierraSYSM9.MVVM;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace OPPNandoSalvatierraSYSM9.ViewModel
{
    public class RecipeDetailViewModel : ViewModelBase
    {
        private readonly UserManager _userManager;
        private readonly Recipe _äkta;

        public RecipeDetailViewModel(UserManager userManager, Recipe recipe)
        {
            _äkta = recipe;
            _userManager = userManager;

            Title = recipe.Titel;
            Ingredients = recipe.Ingredienser;
            Instructions = recipe.Instruktioner;
            Category = recipe.Kategori;
            Date = recipe.Datum;

            Categories = new ObservableCollection<string>(new[]
            {
                "Breakfast",
                "Lunch",
                "Dinner",
                "Dessert",
                "Snack",
                "Drinks"
            });

            EditCommand = new RelayCommand(_ => IsEditing = true, _ => !IsEditing);
            SaveCommand = new RelayCommand(_ => Save(), _ => IsEditing && CanSave());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        public bool IsEditing { get => _isEditing; set { _isEditing = value; OnPropertyChanged(); } }
        private bool _isEditing = false;

        public string Title { get => _title; set { _title = value; OnPropertyChanged(); } }
        private string _title = "";

        public string Ingredients { get => _ingredients; set { _ingredients = value; OnPropertyChanged(); } }
        private string _ingredients = "";

        public string Instructions { get => _instructions; set { _instructions = value; OnPropertyChanged(); } }
        private string _instructions = "";

        public string Category { get => _category; set { _category = value; OnPropertyChanged(); } }
        private string _category = "";

        public DateTime Date { get => _date; set { _date = value; OnPropertyChanged(); } }
        private DateTime _date = DateTime.Today;

        public ObservableCollection<string> Categories { get; }

        public ICommand RecipeDetailCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand EditCommand { get; }

        public event EventHandler? CloseRequested;

        private bool CanSave() =>
            !string.IsNullOrWhiteSpace(Title) &&
            !string.IsNullOrWhiteSpace(Ingredients) &&
            !string.IsNullOrWhiteSpace(Instructions) &&
            !string.IsNullOrWhiteSpace(Category);

        private void Save()
        {
            _äkta.Titel = Title;
            _äkta.Ingredienser = Ingredients;
            _äkta.Instruktioner = Instructions;
            _äkta.Kategori = Category;
            _äkta.Datum = Date;

            IsEditing = false;
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }

        private void Cancel()
        {
            Title = _äkta.Titel;
            Ingredients = _äkta.Ingredienser;
            Instructions = _äkta.Instruktioner;
            Category = _äkta.Kategori;
            Date = _äkta.Datum;

            IsEditing = false;
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
