using Avalonia.Controls;
using Avalonia.Media;
using Heizungssteuerung_Client.Data;
using Heizungssteuerung_SDK;
using Heizungssteuerung_SDK.Training;
using System;
using System.Threading;

namespace Heizungssteuerung_Client.Views;

public partial class TempPredictorContainerView : UserControl
{
    public string? ViewName { get => ContainerNameTextBlock.Text; set => ContainerNameTextBlock.Text = value; }
    public string ViewIcon { get => ContainerSvgImage.Source; set => ContainerSvgImage.Source = value; }

    private Timer? _errorTimer;
    private bool _modelLoaded;
    private HeatingControlModel _model = new HeatingControlModel();

    public TempPredictorContainerView()
    {
        InitializeComponent();

        PredictButton.Click += PredictButton_Click;

        UserTemperaturePickerView.LineColor = new SolidColorBrush(ColorSettings.WeatherLineColor);
        UserTemperaturePickerView.InitTemperatures();
    }

    public void Predict()
    {
        if (SettingsView.UserTemperatures is null || SettingsView.UserTemperatures.Length == 0)
            return;

        try
        {
            if (!_modelLoaded)
            {
                _model.Load();
                _modelLoaded = true;
            }
        }
        catch
        {
            if (!_modelLoaded)
                return;
        }
        for (int i = 0; i < UserTemperaturePickerView.Temperatures.Length; i++)
        {
            double weatherTemp = SettingsView.WeatherTemperatures[i].YValue;
            double userTemperature = FindNearestTemperature(weatherTemp, SettingsView.UserTemperatures);
            TrainingDataInput input = new TrainingDataInput
            {
                ComfortTemperature = (float)userTemperature,
                AverageTemperatureOuterDay = (float)weatherTemp,
                IsolationClass = SettingsView.IsolationClass
            };
            UserTemperaturePickerView.Temperatures[i].YValue = _model.Predict(input);
        }
    }

    private void PredictButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Predict();
    }

    private double FindNearestTemperature(double targetTemperature, Temperature[] temperatureArray)
    {
        if (temperatureArray == null || temperatureArray.Length == 0)
            throw new ArgumentException("Temperature array cannot be null or empty.");

        double nearestTemperature = temperatureArray[0].XValue;
        double minDifference = Math.Abs(targetTemperature - nearestTemperature);
        double nearestTemp = -1;

        foreach (Temperature temperature in temperatureArray)
        {
            double difference = Math.Abs(targetTemperature - temperature.XValue);

            if (difference < minDifference)
            {
                minDifference = difference;
                nearestTemperature = temperature.XValue;
                nearestTemp = temperature.YValue;
            }
        }

        return nearestTemp;
    }
}