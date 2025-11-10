using OPPNandoSalvatierraSYSM9.Model;
using OPPNandoSalvatierraSYSM9.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPPNandoSalvatierraSYSM9.Managers
{
    public class RecipeManager
    {
        //attributer/properties
        private List<Recipe> _recipes = new List<Recipe>(); //privat lista för att lagra recept i klassen

        //konstruktor

        public RecipeManager()
        {
            _recipes = new List<Recipe>();//skapar en ny tom lista när RecipeManager skapas
            _recipes.Add(new Recipe { Titel = "Tacos", Ägare = "user"});
            _recipes.Add(new Recipe { Titel = "Lasagne", Ägare = "user"});
        }

        //metoder
        public List<Recipe> Recipe4User(User user) //Metod som hämtar recept för inloggad User
        {
            return _recipes.Where(r => r.Ägare == user.Username).ToList();//koden som ser till att det händer
        }

        public void AddRecipe(Recipe recipe) //Metod för att lägga till recept i listan
        {
            _recipes.Add(recipe);//koden som ser till att det händer
        }

        public void RemoveRecipe(Recipe recipe)
        {//Metod för att ta bort recept från listan
            _recipes.Remove(recipe);//koden som ser till att det händer
        }

        public void UpdateRecipe(Recipe oldRecipe, Recipe newRecipe) //Metod för att uppdatera ett recept i listan
        {
            var index = _recipes.IndexOf(oldRecipe);
            if (index != -1)
            {
                _recipes[index] = newRecipe;
            }
        }

        public List<Recipe> GetRecipes()
        {
            return _recipes; //Metod för att hämta alla recept i listan
        }
    }
}
