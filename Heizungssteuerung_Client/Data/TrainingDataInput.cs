using System;

namespace Heizungssteuerung_Client.Data;

public enum PrintedData
{
    Day,
    Night,
    All,
}

public sealed class TrainingDataInput
{
    public float ComfortTemperature { get; set; }
    public float AverageTemperatureOuterDay { get; set; }
    public float AverageTemperatureOuterNight { get; set; }
    public IsolationClasses IsolationClass { get; set; }
    public PrintedData PrintedData { get; set; }

    public override string ToString() => $"TempUser: {ComfortTemperature}°{Environment.NewLine}AvgTempOuterDay: {AverageTemperatureOuterDay}°{Environment.NewLine}AvgTempOuterNight: {AverageTemperatureOuterNight}°{Environment.NewLine}IsoClass: {IsolationClass}";
    public string ToCsv()
    {
        string csv = string.Empty;
        switch (PrintedData)
        {
            case PrintedData.Day:
                csv = $"\"{ComfortTemperature}\"{TrainingDataCreator.Separator}\"{AverageTemperatureOuterDay}\"{TrainingDataCreator.Separator}\"{IsolationClass}\"";
                break;
            case PrintedData.Night:
                csv = $"\"{ComfortTemperature}\"{TrainingDataCreator.Separator}\"{AverageTemperatureOuterNight}\"{TrainingDataCreator.Separator}\"{IsolationClass}\"";
                break;
            case PrintedData.All:
                csv = $"\"{ComfortTemperature}\"{TrainingDataCreator.Separator}\"{AverageTemperatureOuterDay}\"{TrainingDataCreator.Separator}\"{AverageTemperatureOuterNight}\"{TrainingDataCreator.Separator}\"{IsolationClass}\"";
                break;
        }
        return csv;
    }
}
