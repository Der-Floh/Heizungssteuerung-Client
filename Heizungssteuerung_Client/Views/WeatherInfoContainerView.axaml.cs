using Avalonia.Controls;
using Avalonia.Media;
using Heizungssteuerung_API;
using Heizungssteuerung_Client.Data;
using System.Threading.Tasks;

namespace Heizungssteuerung_Client.Views;

public partial class WeatherInfoContainerView : UserControl
{
    public string? ViewName { get => ContainerNameTextBlock.Text; set => ContainerNameTextBlock.Text = value; }
    public string ViewIcon { get => ContainerSvgImage.Source; set => ContainerSvgImage.Source = value; }

    public WeatherInfoContainerView()
    {
        InitializeComponent();

        Loaded += WeatherInfoContainerView_Loaded;
        WeatherDataButton.Click += WeatherDataButton_Click;

        UserTemperaturePickerView.LineColor = new SolidColorBrush(ColorSettings.WeatherLineColor);
        UserTemperaturePickerView.InitTemperatures();
    }

    private void WeatherInfoContainerView_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        UpdateWeatherData();
    }

    private void WeatherDataButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        UpdateWeatherData();
    }

    public void UpdateWeatherData()
    {
        _ = LoadWeatherData();
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
}