using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Data;

namespace TheBureau.Converters
{
    public class BoolToReadOnlyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool val = (bool)value;
            return !val;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool val = (bool)value;
            return !val;
        }
    }
}