using System;
using System.Collections.Generic;
using System.Text;

namespace Heizungssteuerung_Client.Data;

public sealed class TrainingDataCreator
{
    public static char Separator { get; set; } = ';';
    public int DecimalPlaces { get; set; } = 2;
    public PrintedData PrintedData { get; set; } = PrintedData.Day;
    public TemperatureRange TemperatureRangeUser { get; set; } = new TemperatureRange(15f, 25f);
    public TemperatureRange TemperatureRangeOuterDay { get; set; } = new TemperatureRange(-10f, 40f);
    public TemperatureRange TemperatureRangeOuterDayChangeRangeNight { get; set; } = new TemperatureRange(-10f, -5f);
    public float MaxHeatingTemperature { get; set; } = 20f;
    public TrainingDataItem[]? TrainingDataItems { get; set; }

    public void SaveTrainingDataSet(IEnumerable<TrainingDataItem>? trainingDataItems = null)
    {
        if (trainingDataItems is null)
            trainingDataItems = TrainingDataItems;
        if (trainingDataItems is null)
            throw new ArgumentNullException(nameof(trainingDataItems));

        //string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "TrainingData.json");
        //JsonHandler.WriteJson(trainingDataItems, path);
    }

    public void SaveTrainingDataSetCsv(IEnumerable<TrainingDataItem>? trainingDataItems = null)
    {
        if (trainingDataItems is null)
            trainingDataItems = TrainingDataItems;
        if (trainingDataItems is null)
            throw new ArgumentNullException(nameof(trainingDataItems));

        StringBuilder csv = new StringBuilder();
        switch (PrintedData)
        {
            case PrintedData.Day:
                csv.AppendLine($"TemperatureUser{Separator}TemperatureOuterDay{Separator}IsolationClass{Separator}TemperatureDay");
                break;
            case PrintedData.Night:
                csv.AppendLine($"TemperatureUser{Separator}TemperatureOuterNight{Separator}IsolationClass{Separator}TemperatureNight");
                break;
            case PrintedData.All:
                csv.AppendLine($"TemperatureUser{Separator}TemperatureOuterDay{Separator}TemperatureOuterNight{Separator}IsolationClass{Separator}TemperatureDay{Separator}TemperatureNight");
                break;
        }

        foreach (TrainingDataItem item in trainingDataItems)
        {
            if (item.Input is null || item.Output is null)
                continue;
            csv.AppendLine(item.Input.ToCsv() + Separator + item.Output.ToCsv());
        }

        //string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "TrainingData.csv");
        //File.WriteAllText(path, csv.ToString());
    }

    public IEnumerable<TrainingDataItem> GenerateTrainingDataSet(int amount)
    {
        List<TrainingDataItem> trainingDataItems = new List<TrainingDataItem>();
        for (int i = 0; i < amount; i++)
        {
            TrainingDataItem item = new TrainingDataItem();
            item.Input = CreateInput();
            item.Output = CreateOutput(item.Input);
            trainingDataItems.Add(item);
        }
        TrainingDataItems = trainingDataItems.ToArray();
        return trainingDataItems;
    }

    public TrainingDataInput CreateInput()
    {
        TrainingDataInput trainingDataInput = new TrainingDataInput();
        return trainingDataInput;
    }

    public TrainingDataOutput CreateOutput(TrainingDataInput input)
    {
        TrainingDataOutput trainingDataOutput = new TrainingDataOutput();

        float isolationValue = GetIsolationClassValue(input.IsolationClass);
        float darDay = input.AverageTemperatureOuterDay - input.ComfortTemperature;
        float darNight = input.AverageTemperatureOuterNight - input.ComfortTemperature;

        float niveau = 1;
        double boilerTemperatureDay = input.ComfortTemperature + niveau - isolationValue * darDay * (1.4347 + 0.021 * darDay + 247.9 * Math.Pow(10, -6) * Math.Pow(darDay, 2));
        double boilerTemperatureNight = input.ComfortTemperature + niveau - isolationValue * darNight * (1.4347 + 0.021 * darNight + 247.9 * Math.Pow(10, -6) * Math.Pow(darNight, 2));

        trainingDataOutput.BoilerTemperatureDay = (float)Math.Round(boilerTemperatureDay, DecimalPlaces);
        trainingDataOutput.BoilerTemperatureNight = (float)Math.Round(boilerTemperatureNight, DecimalPlaces);

        return trainingDataOutput;
    }

    private float GetRandomFloat(float minValue, float maxValue)
    {
        if (minValue == maxValue)
            return (float)Math.Round(minValue, DecimalPlaces);

        if (minValue > maxValue)
            throw new ArgumentException("minValue must be less than maxValue");

        Random rand = new Random();
        float randomValue = (float)(rand.NextDouble() * (maxValue - minValue) + minValue);
        return (float)Math.Round(randomValue, DecimalPlaces);
    }

    private int GetRandomInt(int minValue, int maxValue)
    {
        if (minValue == maxValue)
            return minValue;

        if (minValue > maxValue)
            throw new ArgumentException("minValue must be less than maxValue");

        Random rand = new Random();
        return rand.Next(minValue, maxValue + 1);
    }

    private float GetIsolationClassValue(IsolationClasses value)
    {
        float min = 1;
        float max = 1.6f;

        int enumCount = Enum.GetValues(typeof(IsolationClasses)).Length;
        int enumValue = (int)value;
        float step = (max - min) / (enumCount - 1);
        return min + enumValue * step;
    }
}
