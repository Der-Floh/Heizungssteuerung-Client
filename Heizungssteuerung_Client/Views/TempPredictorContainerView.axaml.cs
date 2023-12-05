using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Heizungssteuerung_Client.Data;
using System;

namespace Heizungssteuerung_Client.Views;

public partial class TempPredictorContainerView : UserControl
{
    public string? ViewName { get => ContainerNameTextBlock.Text; set => ContainerNameTextBlock.Text = value; }
    public string ViewIcon { get => ContainerSvgImage.Source; set => ContainerSvgImage.Source = value; }
    public UserTempPickerAdvancedView UserTempPickerAdvancedView { get; set; }

    public event EventHandler<RoutedEventArgs>? PredictButton_Click;

    public TempPredictorContainerView()
    {
        InitializeComponent();

        PredictButton.Click += (sender, e) => PredictButton_Click?.Invoke(sender, e);

        UserTempPickerAdvancedView = new UserTempPickerAdvancedView
        {
            XTemperatureStart = 3,
            XTemperatureEnd = 24,
            XTemperatureStepSize = 3,
            YTemperatureStart = 120,
            YTemperatureEnd = 0,
            YTemperatureStepSize = 0.5,
            YTemperatureStartValue = 60,
            XAxisText = "Time in hours from now",
            YAxisText = "Boiler Temperature in °C",
            XValuesStringAppend = "h",
            LineColor = new SolidColorBrush(ColorSettings.WeatherLineColor),
        };
        UserTempPickerAdvancedView.InitTemperatures();
        UserTempPickerPanel.Children.Add(UserTempPickerAdvancedView);
    }
}