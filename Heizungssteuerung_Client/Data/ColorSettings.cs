using Avalonia.Media;

namespace Heizungssteuerung_Client.Data;

public static class ColorSettings
{
    public static Color OffHandleColor { get; set; } = Color.FromArgb(255, 30, 30, 30);
    public static Color OnHandleColor { get; set; } = Color.FromArgb(255, 56, 56, 56);
    public static Color GridColor { get; set; } = Color.FromArgb(255, 148, 151, 156);
    public static Color TempLineColor { get; set; } = Color.FromArgb(255, 244, 74, 85);
    public static Color WeatherLineColor { get; set; } = Color.FromArgb(255, 0, 128, 128);
    public static Color TextColor { get; set; } = Color.FromArgb(255, 255, 255, 255);
    public static Color TempWaterColor { get; set; } = Color.FromArgb(255, 244, 74, 85);
}
