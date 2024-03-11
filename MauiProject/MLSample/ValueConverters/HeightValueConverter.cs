using System;
using System.Globalization;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Platform;
using Microsoft.Maui.Graphics.Skia;

namespace MLSample.ValueConverters
{
    public class HeightValueConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            int returnValue = 20;
            var screenWidth = (DeviceDisplay.Current.MainDisplayInfo.Width / DeviceDisplay.Current.MainDisplayInfo.Density) - 40;
            string? targetString = null;
            if (value != null && value is string)
            {
                targetString = (string)value;
            }

            if (targetString != null)
            {
                BitmapExportContext bmp = new SkiaBitmapExportContext((int)DeviceDisplay.Current.MainDisplayInfo.Width, (int)DeviceDisplay.Current.MainDisplayInfo.Height, 1.0f);
                ICanvas canvas = bmp.Canvas;

                var width = canvas.GetStringSize(targetString, new Microsoft.Maui.Graphics.Font("OpenSansRegular"), 14).Width;
                int lines = (int)Math.Ceiling(width / screenWidth);
                returnValue = returnValue * lines;

            }

            return returnValue.ToString();
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}