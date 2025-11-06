using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPPNandoSalvatierraSYSM9.Model
{
     public class Recipe
    {
        public string Titel { get; set; } = ""; // lägger till (" ") för att undvika null värde, så det inte kraschar

        public string Ingredienser { get; set; } = ""; 

        public string Instruktioner { get; set; } = ""; 

        public string Kategori { get; set; } = "";

        public string Tid { get; set; } = "";

        public DateTime Datum { get; set; }

        public string Ägare { get; set; } //user/username



        public override string ToString()
        {
            return Titel;
        }

      


    }
}
