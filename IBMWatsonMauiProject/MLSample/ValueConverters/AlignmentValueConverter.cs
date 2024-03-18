using System;
using System.Globalization;

namespace MLSample.ValueConverters
{
    public class AlignmentValueConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            bool.TryParse(value?.ToString(), out var isClient);
            return isClient ? TextAlignment.End : TextAlignment.Start;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
