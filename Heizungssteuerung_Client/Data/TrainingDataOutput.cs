using System;

namespace Heizungssteuerung_Client.Data;

public sealed class TrainingDataOutput
{
    public float BoilerTemperatureDay { get; set; }
    public float BoilerTemperatureNight { get; set; }
    public PrintedData PrintedData { get; set; }

    public override string ToString() => $"TempDay: {BoilerTemperatureDay}°{Environment.NewLine}TempNight: {BoilerTemperatureNight}°";
    public string ToCsv()
    {
        string csv = string.Empty;
        switch (PrintedData)
        {
            case PrintedData.Day:
                csv = $"\"{BoilerTemperatureDay}\"";
                break;
            case PrintedData.Night:
                csv = $"\"{BoilerTemperatureNight}\"";
                break;
            case PrintedData.All:
                csv = $"\"{BoilerTemperatureDay}\"{TrainingDataCreator.Separator}\"{BoilerTemperatureNight}\"";
                break;
        }
        return csv;
    }
}
