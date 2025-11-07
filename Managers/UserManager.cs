using OPPNandoSalvatierraSYSM9.Model;
using OPPNandoSalvatierraSYSM9.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace OPPNandoSalvatierraSYSM9.Managers
{
    public class UserManager : ViewModelBase
    {

        // Attributer/properties
        private User? _currentUser; // Håller _currentUser intern endast UserManager får ändra innehållet om vem som är _currenUser
        private List<User> _users; // Är en intern privat lista som endast UserManager har i minnet med alla användare

        public User? CurrentUser // Properties till Current User som säger att User kan vara null(med ? tecknet)
        {
            get { return _currentUser; } // Hämtar när någon hänvisar till Propertyn CurrentUser så behöver man inte skriva hela texten varje gång
            set // denna koden körs när någon ändrar värdet
            {
                _currentUser = value; // nya inmatade värdet ändras i _currentUser
                OnPropertyChanged(nameof(CurrentUser)); // eventet ändrar allt som har med propetyn(CurrentUser) att göra
                // med andra ord tvingar allt som är bundet till dett att hänga med uppdateringen
                OnPropertyChanged(nameof(IsAuthenticated)); // samma som koden ovanför men denna är nu för (IAuthenticated)
                // vilket är ett måste då Propertyn (CurrentUser) även bestämmer om IsAuthenticated är True/False
                
            }

        }

        public bool ÄrAdmin => CurrentUser?.Role?.Equals("Admin", StringComparison.OrdinalIgnoreCase) == true; // Property som ser om den inloggade användaren är Admin eller inte

        public bool IsAuthenticated // property som ser om någon är inloggad eller inte
        {
            get { return _currentUser != null; } // koden som ser till om en användare är inloggad
        }

        // Konstruktor

        public UserManager()
        {
            _users = new List<User>(); // En lista där användarna ska lagras(som en anteckningsblock)
            SeedDefaultUsers(); // Metod som skapar standardanvändare

        }

        // metoder

        private void SeedDefaultUsers() // just nu bara inför prototyp
        {
            var admin = new User("admin", "password") //Admin användare som ska kunna logga in och redigera appen
            {
                DisplayName = "Administrator",
                Role = "Admin",
                Country = "Sweden"
            };

            var user = new User("user", "password") // Vanlig användare/kund som ska använda appen
            {
                DisplayName = "användare(test)",
                Role = "User",
                Country = "Sweden"
            };
            // här lägger vi till användarna i listan/Anteckningsblocket
            _users.Add(admin);
            _users.Add(user);

            // Lägg till några testrecept för användaren
            user.Recipes.Add(new Recipe
            {
                Titel = "Spaghetti Bolognese",
                Instruktioner = "1. Koka pasta enligt anvisningar på förpackningen.\n2. Stek köttfärs tills " +
                "den är genomstekt.\n3. Tillsätt tomatsås och kryddor, låt sjuda i 15 minuter.\n4. Servera köttfärssåsen över pastan."
            });

            user.Recipes.Add(new Recipe
            {
                Titel = "Pannkakor",
                Instruktioner = "1. Blanda mjöl, ägg, mjölk och en nypa salt till en slät smet.\n2. Hetta upp" +
                " en stekpanna med lite smör.\n3. Häll i en skopa smet och stek tills pannkakan är gyllenbrun på båda sidor.\n4. Servera med sylt och grädde."
            }); // test Recept för prototyp programmet
        }

        public bool Login(string username, string password) // Parametrar som kunden/användaren själv väljer
        {
            foreach (var u in _users)
            {
                if (string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase) //ser till att inloggningen inte är case-sensitive
                    && u.Password == password)
                {
                    CurrentUser = u; // sätter den inloggade användaren som CurrentUser
                    return true;
                }
            }
            return false;
        }

        public bool Register(string username, string password, string country, out string error) //Parametrar som kunden/användaren själv väljer och felmeddelande om det aktiveras
        {
            error = "";

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                error = "Mata in både användarnamn och lösenord.";
                return false;
            } // Kollar så att användarnamn och lösenord inte är tomma

            if (UserExists(username))
            {
                error = "Användarnamnet används redan, testa ett annat.";
                return false;
            } // Kollar så att användarnamnet inte redan finns

            var user = new User(username, password)
            {
                Country = string.IsNullOrWhiteSpace(country) ? "Sweden" : country,
                Role = "User"
            }; // Skapar en ny användare med valt land (eller standard "Sweden") och Role

            _users.Add(user);
            return true;

        }

        public bool UserExists(string username) // Metod som kollar om användarnamnet redan finns
        {
            foreach (var u in _users) // Lopp som går genom Variabeln (u)=användare i listan _users
                if (string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase))// If sats som jämför string(namn)inmatningen
                    //för att se om det redan finns.
                    return true;//  om det är sant, säger den att avlsuta eventet direkt för det redan dinns en användare med samma namn
            return false; // Om det inte finns så avslutar det fortfarande momentet men då vet UserManager att det inte finns samma namn.
        }

        public IEnumerable<Recipe> HämtaAllaRecept()
        {
            return _users.SelectMany(u => u.Recipes).ToList();
        }

        public User? HittaÄgareTillRecept(Recipe recipe)
        {
            return _users.FirstOrDefault(u => u.Recipes.Contains(recipe));
        }

        public bool RemoveRecipeEverywhere(Recipe recipe, out string? fel) // Metod som jag gör så att Admin kan ta bort recept från alla användare
        {
            fel = null;

            if (recipe == null)
            {
                fel = "Ogiltigt recept.";
                return false;
            }

            // Försök hitta användaren som äger receptet
            var ägare = HittaÄgareTillRecept(recipe);
            if (ägare == null)
            {
                fel = "Kunde inte hitta receptets ägare.";
                return false;
            }

            // Ta bort receptet från ägarens lista
            var borttagen = ägare.Recipes.Remove(recipe);
            if (!borttagen)
            {
                fel = "Borttagningen misslyckades.";
                return false;
            }

            return true;
        }















    }
}
