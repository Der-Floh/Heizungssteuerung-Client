using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace Heizungssteuerung_Client.Data;

public sealed class FontSizeConverter : IValueConverter
{
    public double ConversionFactor { get; set; } = 24;
    public double DefaultSize { get; set; } = 12;

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double size && !double.IsNaN(size) && size > 0)
        {
            if (parameter is double format && !double.IsNaN(format))
            {
                format = Math.Clamp(format, 1, 2);
                if (format <= 1)
                    return size / (ConversionFactor / format);
                else
                    return size / (ConversionFactor * format);
            }
            return size / ConversionFactor;
        }

        return DefaultSize;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}

