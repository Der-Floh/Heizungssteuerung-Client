using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Heizungssteuerung_Client.Data;
using System;

namespace Heizungssteuerung_Client.Views;

public partial class TempPredictorOutsideContainerView : UserControl
{
    public string? ViewName { get => ContainerNameTextBlock.Text; set => ContainerNameTextBlock.Text = value; }
    public string? ViewIcon { get => ContainerSvgImage.Source; set => ContainerSvgImage.Source = value ?? string.Empty; }
    public UserTempPickerAdvancedView UserTempPickerAdvancedView { get; set; }

    public event EventHandler<RoutedEventArgs>? PredictButton_Click;

    public TempPredictorOutsideContainerView()
    {
        InitializeComponent();

        PredictButton.Click += (sender, e) => PredictButton_Click?.Invoke(sender, e);

        if (Data.OS.IsMobile())
        {
            UserTempPickerAdvancedView = new UserTempPickerAdvancedView
            {
                XTemperatureStart = -10,
                XTemperatureEnd = 30,
                XTemperatureStepSize = 10,
                YTemperatureStart = 120,
                YTemperatureEnd = 0,
                YTemperatureStepSize = 0.5,
                YTemperatureStartValue = 60,
                XAxisText = "Temperature Outside in °C",
                YAxisText = "Boiler Temperature in °C",
                TempLineColor = new SolidColorBrush(ColorSettings.WeatherLineColor),
            };
        }
        else
        {
            UserTempPickerAdvancedView = new UserTempPickerAdvancedView
            {
                XTemperatureStart = -10,
                XTemperatureEnd = 40,
                XTemperatureStepSize = 5,
                YTemperatureStart = 120,
                YTemperatureEnd = 0,
                YTemperatureStepSize = 0.5,
                YTemperatureStartValue = 60,
                XAxisText = "Temperature Outside in °C",
                YAxisText = "Boiler Temperature in °C",
                TempLineColor = new SolidColorBrush(ColorSettings.WeatherLineColor),
            };
        }
        UserTempPickerAdvancedView.InitTemperatures();
        UserTempPickerPanel.Children.Add(UserTempPickerAdvancedView);
    }
}