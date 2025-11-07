using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace OPPNandoSalvatierraSYSM9.Converters
{
    public class NotConverter : IValueConverter // är en interface som finns i systems. dvs. .NET biblioteket
                                                        // som kan hämtas av alla som behöver den. I detta fall används den för att ändra bool värden i boxen som vi vill redigera
                                                        //så det är möjligt att sätta i detta fall nya Recept
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b) return !b; // om värdet är true/False så returneras motsatsen
            if (value is bool?) return value is bool nb && nb ? false : true;//dvs att true blir false och false blir true
                                                                             //= det går att redigera/detta är ReadOnly
            return DependencyProperty.UnsetValue;// om det inte är en bool så kan den inte läsa av
                                                 // och den meddelar att den inte kan hantera det så det är bättre att ignorera det
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) // denna metod gör samma sak som Convert
                                                                                                        // fast denna skickar data tiilbaka till VM så resten av appen vet vad som händer
        {
            if (value is bool b) return !b;
            if (value is bool?) return value is bool nb && nb ? false : true;
            return DependencyProperty.UnsetValue;
        }
    }
}
