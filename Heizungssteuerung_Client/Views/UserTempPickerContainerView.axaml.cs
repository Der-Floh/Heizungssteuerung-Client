using Avalonia.Controls;
using Avalonia.Threading;
using Heizungssteuerung_SDK.Training;
using System;
using System.Threading.Tasks;

namespace Heizungssteuerung_Client.Views;

public partial class UserTempPickerContainerView : UserControl
{
    public UserTempPickerContainerView()
    {
        InitializeComponent();

        IsolcationClassComboBox.ItemsSource = Enum.GetNames(typeof(IsolationClasses));
        IsolcationClassComboBox.SelectedItem = SettingsView.IsolationClass;

        TrainButton.Click += TrainButton_Click;
    }

    private void TrainButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Task.Run(async () =>
        {
            try
            {
                UserTemperaturePickerView.Editable = false;
                LoadingWaveView.Text = "Training AI";
                LoadingWaveView.WaveHeightPercent = 0.8;
                LoadingWaveView.DrawWave = true;
                await LoadingWaveView.EnterFromBottom();
                await Task.Delay(10000);
                //HeatingControlModel model = new HeatingControlModel();
                //await model.Train();
                //model.Save();
                //float accuracy = await model.CalcAccuracy();
                //await Dispatcher.UIThread.InvokeAsync(() => TrainButton.Text = $"Success (accuracy: {Math.Round(accuracy, 2)})", DispatcherPriority.Background);
                await LoadingWaveView.ExitToBottom();
                UserTemperaturePickerView.Editable = true;
            }
            catch (Exception ex)
            {
                UserTemperaturePickerView.Editable = true;
                await LoadingWaveView.ExitToBottom();
                await Dispatcher.UIThread.InvokeAsync(() => TrainButton.Text = $"Failed: {ex.Message}", DispatcherPriority.Background);
            }
        });
    }
}