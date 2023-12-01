using Avalonia.Media;
using Avalonia.Media.Immutable;
using SkiaSharp;

namespace Heizungssteuerung_Client.Utilities;

internal static class ColorExtentions
{
    internal static SKColor ToSKColor(this Color color) => new SKColor(color.R, color.G, color.B, color.A);
    internal static SKColor ToSKColor(this IBrush brush, float? alpha = null)
    {
        var color = ToColor(brush);
        var colorAlpha = color.A;
        if (alpha is not null)
        {
            colorAlpha = (byte)(alpha * 255);
        }
        return new SKColor(color.R, color.G, color.B, colorAlpha);
    }


    internal static Color ToColor(this IBrush brush)
    {
        return ((ImmutableSolidColorBrush)brush).Color;
    }

    internal static Color ToColor(this IBrush brush, byte alpha = 255)
    {
        var color = brush.ToColor();
        return new Color(alpha, color.R, color.G, color.B);
    }
}
