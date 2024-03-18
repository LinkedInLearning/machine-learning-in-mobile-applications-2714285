using System;
using System.Globalization;
using Microsoft.Maui;

namespace MLSample.ValueConverters
{
    public class ColorValueConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            bool.TryParse(value?.ToString(), out var isClient);
            return isClient ? Colors.CadetBlue : Colors.LimeGreen;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
