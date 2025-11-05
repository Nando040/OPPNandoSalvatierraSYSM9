using OPPNandoSalvatierraSYSM9.Managers;
using OPPNandoSalvatierraSYSM9.Model;
using OPPNandoSalvatierraSYSM9.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OPPNandoSalvatierraSYSM9.ViewModel
{
    public class AddRecipeViewModel : ViewModelBase
    {
        private readonly UserManager _userManager;// Readonly gör att denna klassen har tillgång till UserManagers användare men kan bara läsa
        //inte ändra eller byta ut data

        public AddRecipeViewModel(UserManager userManager)
        {
            _userManager = userManager;
            Categories = new ObservableCollection<string>(new[]// en lista som Xaml(UI) kan få fram i comboboxen
            {
                "Breakfast",
                "Lunch",
                "Dinner",
                "Dessert",
                "Snack",
                "Drinks"
            });
            Date= DateTime.Today;// gör att dagensdatum sätt på den öppna xamlfönstret

            SaveRecipeCommand = new RelayCommand(_ => SaveRecipe(), _ => CanSave());// denna koden gör aå att Commanden kopplas till ViewModel
            CancelCommand = new RelayCommand(_ => Cancel());// som ovanför men till CancelCommand
        }
        //Properties döe xaml filen
        public string RecipeName { get; set; } = " ";
        public string Ingredients { get; set; } = " ";
        public string Instructions { get; set; } = " ";
        public string Category { get; set; } = " ";

        public DateTime Date { get; set; }

        public ObservableCollection<string> Categories { get; }

        //Commands
        public ICommand SaveRecipeCommand { get; } 
        public ICommand CancelCommand { get; }

        public event EventHandler? CloseRequested; // Event till att stänga Fönstret

        public bool CanSave() => // hela denna kodblocket kollar att fälten inte är tomma eller bara har mellanslag till respektive
            //sort eller rad som man ska fylla i xaml filen
            !string.IsNullOrWhiteSpace(RecipeName) &&
            !string.IsNullOrWhiteSpace(Ingredients) &&
            !string.IsNullOrWhiteSpace(Instructions) &&
            !string.IsNullOrWhiteSpace(Category);

        private void SaveRecipe()// Metod som gör så att man kan spara recept
        {
            var r = new Recipe // skabar ett nytt recept ovjekt som man kan fylla i information
            {
                Titel = RecipeName,

                Instruktioner = Instructions,
            };

            _userManager.CurrentUser?.Recipes.Add(r); // denna koden lägger till receptet man skrivit in till den inloggade användaren
            CloseRequested?.Invoke(this, EventArgs.Empty);// stäng fönster metod
        }

        private void Cancel() => CloseRequested?.Invoke(this, EventArgs.Empty);//kod som ser till att fönstret stängs



    }
}
