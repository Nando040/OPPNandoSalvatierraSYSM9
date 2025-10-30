using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPPNandoSalvatierraSYSM9.Model
{
     public class Recipe
    {
        public string Titel { get; set; } = "";

        public string Instruktioner { get; set; } = ""; // lägger till (" ") för att undvika null värde, så det inte kraschar


    }
}
