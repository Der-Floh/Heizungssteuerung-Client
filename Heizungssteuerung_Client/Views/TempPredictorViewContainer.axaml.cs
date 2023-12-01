using Avalonia.Controls;
using Heizungssteuerung_API;
using Heizungssteuerung_SDK;
using Heizungssteuerung_SDK.Training;
using System.Threading.Tasks;

namespace Heizungssteuerung_Client.Views;

public partial class TempPredictorViewContainer : UserControl
{
    public TempPredictorViewContainer()
    {
        InitializeComponent();

        PredictButton.Click += PredictButton_Click;
        WeatherDataButton.Click += WeatherDataButton_Click;
        UserTemperaturePickerView.Loaded += UserTemperaturePickerView_Loaded;
    }

    private void UserTemperaturePickerView_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        UserTemperaturePickerView.XTemperatureStart = 3;
        UserTemperaturePickerView.XTemperatureEnd = 30;
        UserTemperaturePickerView.XTemperatureStepSize = 3;
        UserTemperaturePickerView.YTemperatureStart = 40;
        UserTemperaturePickerView.YTemperatureEnd = -10;
        UserTemperaturePickerView.YTemperatureStepSize = 0.5;
        //UserTemperaturePickerView.InitTemperatures();
        //LoadWeatherData();
    }

    private void WeatherDataButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        LoadWeatherData();
    }

    private async Task LoadWeatherData()
    {
        WeatherAPI weatherAPI = new WeatherAPI();
        double[] temperatures = await weatherAPI.GetFutureTemperatures(10);

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
        for (int i = 0; i < SettingsView.UserTemperatures.Length; i++)
        {
            TrainingDataInput input = new TrainingDataInput
            {
                ComfortTemperature = (float)SettingsView.UserTemperatures[i].YValue,
                AverageTemperatureOuterDay = (float)UserTemperaturePickerView.Temperatures[i].XValue,
                IsolationClass = SettingsView.IsolationClass
            };
            UserTemperaturePickerView.Temperatures[i].YValue = model.Predict(input);
        }
    }
}