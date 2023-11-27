using Avalonia.Controls;
using Avalonia.Threading;
using Heizungssteuerung_Client.Data;
using Heizungssteuerung_SDK;
using Heizungssteuerung_SDK.Training;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Heizungssteuerung_Client.Views;

public partial class UserTempPickerContainerView : UserControl
{
    private Timer? _timer;
    private int _loadingIndex = 0;
    private string _loadingChars = @"◜◠◝◞◡◟";

    public UserTempPickerContainerView()
    {
        InitializeComponent();

        IsolcationClassComboBox.ItemsSource = Enum.GetNames(typeof(IsolationClasses));

        TrainButton.Click += TrainButton_Click;
        PredictButton.Click += PredictButton_Click;
    }

    private void PredictButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Temperature[] temperatures = UserTemperaturePickerView.Temperatures;
        if (temperatures is null)
            return;
        IsolationClasses isolationClass = Enum.Parse<IsolationClasses>(IsolcationClassComboBox.SelectedItem.ToString());

        HeatingControlModel model = new HeatingControlModel();
        model.Load();
        foreach (Temperature temperature in temperatures)
        {
            TrainingDataInput input = new TrainingDataInput
            {
                ComfortTemperature = (float)temperature.YValue,
                AverageTemperatureOuterDay = (float)temperature.XValue,
                IsolationClass = isolationClass
            };
            temperature.YValue = model.Predict(input);
        }
    }

    private void TrainButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Task.Run(async () =>
        {
            try
            {
                StartTimer();
                HeatingControlModel model = new HeatingControlModel();
                await model.Train();
                model.Save();
                float accuracy = await model.CalcAccuracy();
                StopTimer();
                await Dispatcher.UIThread.InvokeAsync(() => TrainButton.Text = $"Success (accuracy: {Math.Round(accuracy, 2)})", DispatcherPriority.Background);
            }
            catch (Exception ex)
            {
                StopTimer();
                await Dispatcher.UIThread.InvokeAsync(() => TrainButton.Text = $"Failed: {ex.Message}", DispatcherPriority.Background);
            }
        });
    }

    private void StartTimer()
    {
        if (_timer is null)
            _timer = new Timer(TimerCallback, null, 0, 100);
    }

    private void StopTimer()
    {
        if (_timer is not null)
        {
            _timer.Dispose();
            _timer = null;
        }
    }

    private void TimerCallback(object? state)
    {
        Dispatcher.UIThread.InvokeAsync(() => TrainButton.Text = _loadingChars[_loadingIndex].ToString(), DispatcherPriority.Background);
        _loadingIndex++;
        if (_loadingIndex >= _loadingChars.Length)
            _loadingIndex = 0;
    }
}