using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Heizungssteuerung_SDK;
using System;
using System.Threading.Tasks;

namespace Heizungssteuerung_Client.Views;

public partial class UserTempPickerContainerView : UserControl
{
    public string? ViewName { get => ContainerNameTextBlock.Text; set => ContainerNameTextBlock.Text = value; }
    public string? ViewIcon { get => ContainerSvgImage.Source; set => ContainerSvgImage.Source = value ?? string.Empty; }
    public UserTempPickerAdvancedView UserTempPickerAdvancedView { get; set; }
    public event EventHandler<RoutedEventArgs>? LoadingFinished;

    public UserTempPickerContainerView()
    {
        InitializeComponent();

        Loaded += UserTempPickerContainerView_Loaded;
        TrainButton.Click += TrainButton_Click;

        Data.Settings.LoadUserTemps();

        if (Data.OS.IsMobile())
        {
            UserTempPickerAdvancedView = new UserTempPickerAdvancedView
            {
                Editable = true,
                XTemperatureStart = -10,
                XTemperatureEnd = 30,
                XTemperatureStepSize = 10,
                YTemperatureStart = 40,
                YTemperatureEnd = 0,
                YTemperatureStepSize = 0.5,
                YTemperatureStartValue = 25,
                XAxisText = "Temperature Outside in °C",
                YAxisText = "Comfort Temperature in °C",
            };
        }
        else
        {
            UserTempPickerAdvancedView = new UserTempPickerAdvancedView
            {
                Editable = true,
                XTemperatureStart = -10,
                XTemperatureEnd = 40,
                XTemperatureStepSize = 10,
                YTemperatureStart = 40,
                YTemperatureEnd = 0,
                YTemperatureStepSize = 0.5,
                YTemperatureStartValue = 25,
                XAxisText = "Temperature Outside in °C",
                YAxisText = "Comfort Temperature in °C",
            };
        }
        UserTempPickerAdvancedView.InitTemperatures();

        UserTempPickerAdvancedView.TempChanged += UserTempPickerAdvancedView_TempChanged;
        UserTempPickerPanel.Children.Add(UserTempPickerAdvancedView);
    }

    private void UserTempPickerAdvancedView_TempChanged(object? sender, int i)
    {
        Data.Settings.SetUserTemps(i, UserTempPickerAdvancedView.Temperatures[i].YValue);
        _ = Data.Settings.SaveUserTemps();
    }

    private void UserTempPickerContainerView_Loaded(object? sender, RoutedEventArgs e)
    {
        UserTempPickerAdvancedView.InitializeTemperaturePositions();
        foreach (var kvp in Data.Settings.UserTemps)
        {
            try
            {
                UserTempPickerAdvancedView.Temperatures[kvp.Key].YValue = kvp.Value;
            }
            catch { }
        }
        LoadingFinished?.Invoke(this, e);
    }

    private void TrainButton_Click(object? sender, RoutedEventArgs e)
    {
        Task.Run(async () =>
        {
            try
            {
                UserTempPickerAdvancedView.Editable = false;
                LoadingWaveView.Text = "Training AI";
                LoadingWaveView.WaveHeightPercent = 0.8;
                LoadingWaveView.DrawWave = true;
                await LoadingWaveView.EnterFromBottom();
                //await Task.Delay(10000);
                HeatingControlModel model = new HeatingControlModel();
                await model.Train();
                model.Save();
                float accuracy = await model.CalcAccuracy();
                await Dispatcher.UIThread.InvokeAsync(() => TrainButton.Text = $"Success (accuracy: {Math.Round(accuracy, 2)})", DispatcherPriority.Background);
                await LoadingWaveView.ExitToBottom();
                UserTempPickerAdvancedView.Editable = true;
            }
            catch (Exception ex)
            {
                UserTempPickerAdvancedView.Editable = true;
                await LoadingWaveView.ExitToBottom();
                await Dispatcher.UIThread.InvokeAsync(() => TrainButton.Text = $"Failed: {ex.Message}", DispatcherPriority.Background);
            }
        });
    }
}