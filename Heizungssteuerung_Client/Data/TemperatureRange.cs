namespace Heizungssteuerung_Client.Data;

public sealed class TemperatureRange
{
    public float Min { get; set; }
    public float Max { get; set; }

    public TemperatureRange(float min, float max)
    {
        Min = min;
        Max = max;
    }
}
