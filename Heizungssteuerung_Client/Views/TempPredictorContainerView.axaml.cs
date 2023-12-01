using Avalonia.Controls;
using Avalonia.Media;
using Heizungssteuerung_API;
using Heizungssteuerung_Client.Data;
using Heizungssteuerung_SDK;
using Heizungssteuerung_SDK.Training;
using System;
using System.Threading.Tasks;

namespace Heizungssteuerung_Client.Views;

public partial class TempPredictorContainerView : UserControl
{
    public TempPredictorContainerView()
    {
        InitializeComponent();

        PredictButton.Click += PredictButton_Click;
        WeatherDataButton.Click += WeatherDataButton_Click;

        UserTemperaturePickerView.LineColor = new SolidColorBrush(ColorSettings.WeatherLineColor);
        UserTemperaturePickerView.InitTemperatures();
    }

    private void WeatherDataButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        LoadWeatherData();
    }

    private async Task LoadWeatherData()
    {
        WeatherAPI weatherAPI = new WeatherAPI();
        double[] temperatures = await weatherAPI.GetFutureTemperatures(UserTemperaturePickerView.Temperatures.Length);

        for (int i = 0; i < UserTemperaturePickerView.Temperatures.Length; i++)
        {
            UserTemperaturePickerView.Temperatures[i].YValue = temperatures[i];
        }
    }

    private void PredictButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (SettingsView.UserTemperatures is null || SettingsView.UserTemperatures.Length == 0)
            return;

        HeatingControlModel model = new HeatingControlModel();
        model.Load();
        for (int i = 0; i < UserTemperaturePickerView.Temperatures.Length; i++)
        {
            double userTemperature = FindNearestTemperature(UserTemperaturePickerView.Temperatures[i], SettingsView.UserTemperatures);
            TrainingDataInput input = new TrainingDataInput
            {
                ComfortTemperature = (float)userTemperature,
                AverageTemperatureOuterDay = (float)UserTemperaturePickerView.Temperatures[i].YValue,
                IsolationClass = SettingsView.IsolationClass
            };
            UserTemperaturePickerView.Temperatures[i].YValue = model.Predict(input);
        }
    }

    private double FindNearestTemperature(Temperature targetTemperature, Temperature[] temperatureArray)
    {
        if (temperatureArray == null || temperatureArray.Length == 0)
            throw new ArgumentException("Temperature array cannot be null or empty.");

        double nearestTemperature = temperatureArray[0].YValue;
        double minDifference = Math.Abs(targetTemperature.YValue - nearestTemperature);

        foreach (Temperature temperature in temperatureArray)
        {
            double difference = Math.Abs(targetTemperature.YValue - temperature.YValue);

            if (difference < minDifference)
            {
                minDifference = difference;
                nearestTemperature = temperature.YValue;
            }
        }

        return nearestTemperature;
    }
}