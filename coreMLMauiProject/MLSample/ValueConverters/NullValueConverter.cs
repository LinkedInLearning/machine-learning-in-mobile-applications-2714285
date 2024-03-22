using System;
using System.Globalization;

namespace MLSample.ValueConverters
{
    public class NullValueConverter : IValueConverter
    { 
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var convertedValue = value as double?;
            if (convertedValue.HasValue)
            {
                return convertedValue.Value.ToString("G", NumberFormatInfo.CurrentInfo);
            }
            else
            {
                return string.Empty;
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            double? convertedValue = null;
            value = value?.ToString() == "."? "0.": value;
            if (double.TryParse(value?.ToString(), out double tempConvertedValue))
            {
                convertedValue = tempConvertedValue;
            }
            return convertedValue;
        }
    }
}
