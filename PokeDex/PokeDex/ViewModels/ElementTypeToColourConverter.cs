using PokeDex.Models;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Xamarin.Forms;

namespace PokeDex.ViewModels
{
    class ElementTypeToColourConverter : IValueConverter
    {
        ElementalColours elementalColours = new ElementalColours();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return new SolidColorBrush(Color.FromHex(elementalColours.typeColour[(string)value]));
            }
            else
            {
                return new SolidColorBrush(Color.Transparent);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
