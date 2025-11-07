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

            //properties som ska in i konstruktorn
            Title = recipe.Titel; 
            Ingredients = recipe.Ingredienser;
            Instructions = recipe.Instruktioner;
            Category = recipe.Kategori;
            Date = recipe.Datum;

            Categories = new ObservableCollection<string>(new[] // Skapar en lista med förutbestämda kategorier för combobox i fönstret
            {
                "Breakfast",
                "Lunch",
                "Dinner",
                "Dessert",
                "Snack",
                "Drinks"
            });

            EditCommand = new RelayCommand(_ => IsEditing = true, _ => !IsEditing); // Detta meddelar att xaml fönstret om att det ska påbörjar redigeringsläge
            SaveCommand = new RelayCommand(_ => Save(), _ => IsEditing && CanSave()); // Detta meddelar xaml fönstret att spara ändringar 
            CancelCommand = new RelayCommand(_ => Cancel()); // Detta stänger fönstret utan att spara ändringar
        }

        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                if (_isEditing == value) return;
                _isEditing = value;
                OnPropertyChanged();                   // IsEditing
                OnPropertyChanged(nameof(IsReadOnly)); // Property som aktiverar redigeringsläge
            }
        }

        private bool _isEditing = false;// standard är false dvs ReadOnly
        public bool IsReadOnly => !IsEditing; // Property som används i xaml för att bestämma om fälten är redigerbara eller inte

        // Properties som man kan ändra i fönstret plus mellanslag om det behhövs
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

        public ObservableCollection<string> Categories { get; } // lista för combobox i xaml
        // commands
        public ICommand RecipeDetailCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand EditCommand { get; }

        public event EventHandler? CloseRequested;// event till att stänga Fönstret

        private bool CanSave() => // kollar att fälten inte är tomma eller bara har mellanslag så att det går att spara
            !string.IsNullOrWhiteSpace(Title) &&
            !string.IsNullOrWhiteSpace(Ingredients) &&
            !string.IsNullOrWhiteSpace(Instructions) &&
            !string.IsNullOrWhiteSpace(Category);

        private void Save()// metod som ser till att de nya värden sparas 
        {
            _äkta.Titel = Title;
            _äkta.Ingredienser = Ingredients;
            _äkta.Instruktioner = Instructions;
            _äkta.Kategori = Category;
            _äkta.Datum = Date;

            IsEditing = false;
            CloseRequested?.Invoke(this, EventArgs.Empty);//stänger fönstret och går tillbaka till föregående fönster
        }

        private void Cancel() // metod som ser till att ändringar inte sparas och återgår till gamla värden
        {
            Title = _äkta.Titel;
            Ingredients = _äkta.Ingredienser;
            Instructions = _äkta.Instruktioner;
            Category = _äkta.Kategori;
            Date = _äkta.Datum;

            IsEditing = false;
            CloseRequested?.Invoke(this, EventArgs.Empty); //stänger fönstret och går tillbaka till föregående fönster
        }
    }
    // fattas infoknapp men komma innan inlämning
}
