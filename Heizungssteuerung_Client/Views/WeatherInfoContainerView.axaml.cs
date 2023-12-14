using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Heizungssteuerung_API;
using Heizungssteuerung_Client.Data;
using System;
using System.Threading.Tasks;

namespace Heizungssteuerung_Client.Views;

public partial class WeatherInfoContainerView : UserControl
{
    public string? ViewName { get => ContainerNameTextBlock.Text; set => ContainerNameTextBlock.Text = value; }
    public string? ViewIcon { get => ContainerSvgImage.Source; set => ContainerSvgImage.Source = value ?? string.Empty; }
    public UserTempPickerAdvancedView UserTempPickerAdvancedView { get; set; }
    public event EventHandler<RoutedEventArgs>? LoadingFinished;

    public WeatherInfoContainerView()
    {
        InitializeComponent();

        Loaded += WeatherInfoContainerView_Loaded;
        WeatherDataButton.Click += WeatherDataButton_Click;

        if (Data.OS.IsMobile())
        {
            UserTempPickerAdvancedView = new UserTempPickerAdvancedView
            {
                XTemperatureStart = 3,
                XTemperatureEnd = 12,
                XTemperatureStepSize = 3,
                YTemperatureStart = 50,
                YTemperatureEnd = -20,
                YTemperatureStepSize = 0.5,
                YTemperatureStartValue = 20,
                XAxisText = "Time in hours from now",
                YAxisText = "Temperature Outside in °C",
                XValuesStringAppend = "h",
                TempLineColor = new SolidColorBrush(ColorSettings.WeatherLineColor),
            };
        }
        else
        {
            UserTempPickerAdvancedView = new UserTempPickerAdvancedView
            {
                XTemperatureStart = 3,
                XTemperatureEnd = 24,
                XTemperatureStepSize = 3,
                YTemperatureStart = 50,
                YTemperatureEnd = -20,
                YTemperatureStepSize = 0.5,
                YTemperatureStartValue = 20,
                XAxisText = "Time in hours from now",
                YAxisText = "Temperature Outside in °C",
                XValuesStringAppend = "h",
                TempLineColor = new SolidColorBrush(ColorSettings.WeatherLineColor),
            };
        }
        UserTempPickerAdvancedView.InitTemperatures();
        UserTempPickerPanel.Children.Add(UserTempPickerAdvancedView);
    }

    private void WeatherInfoContainerView_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        UpdateWeatherData(true);
    }

    private void WeatherDataButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        UpdateWeatherData();
    }

    public void UpdateWeatherData(bool triggerLoaded = false)
    {
        _ = LoadWeatherData(triggerLoaded);
    }

    private async Task LoadWeatherData(bool triggerLoaded = false)
    {
        WeatherAPI weatherAPI = new WeatherAPI();

        double[] temperatures = await weatherAPI.GetFutureTemperatures(UserTempPickerAdvancedView.Temperatures.Count);

        for (int i = 0; i < UserTempPickerAdvancedView.Temperatures.Count; i++)
        {
            UserTempPickerAdvancedView.Temperatures[i].YValue = temperatures[i];
        }

        if (triggerLoaded)
            LoadingFinished?.Invoke(this, new RoutedEventArgs());
    }
}