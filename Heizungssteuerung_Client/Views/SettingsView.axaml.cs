using Avalonia.Controls;
using Heizungssteuerung_Client.Data;
using System;

namespace Heizungssteuerung_Client.Views;

public partial class SettingsView : UserControl
{
    public IsolationClasses IsolationClass { get; set; }
    public decimal StepSizeTemperature { get; set; }
    public decimal MinOutsideTemperature { get; set; }
    public decimal MaxOutsideTemperature { get; set; }
    public decimal MinUserTemperature { get; set; }
    public decimal MaxUserTemperature { get; set; }
    public decimal TemperatureHandleSize { get; set; }
    public decimal Roundingprecision { get; set; }
    public string Theme { get; set; }

    public SettingsView()
    {
        InitializeComponent();

        IsolationClassComboBox.ItemsSource = Enum.GetNames(typeof(IsolationClasses));
        IsolationClassComboBox.PropertyChangedRuntime += IsolationClassComboBox_PropertyChanged;

        StepSizeTemperatureNumericUpDown.PropertyChangedRuntime += StepSizeTemperatureNumericUpDown_PropertyChanged;
        OutsideTemperatureNumericUpDown.PropertyChangedRuntime += OutsideTemperatureNumericUpDown_PropertyChanged;
        UserTemperatureNumericUpDown.PropertyChangedRuntime += UserTemperatureNumericUpDown_PropertyChanged;
        TemperatureHandleSizeNumericUpDown.PropertyChangedRuntime += TemperatureHandleSizeNumericUpDown_PropertyChanged;
        RoundingprecisionNumericUpDown.PropertyChangedRuntime += RoundingprecisionNumericUpDown_PropertyChanged;
        ThemeRadioButtons.PropertyChangedRuntime += ThemeRadioButtons_PropertyChanged;

        ThemeRadioButtons.ItemsSource = new string[] { "Light", "Dark" };
    }

    private void IsolationClassComboBox_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IsolationClassComboBox.SelectedItem))
            IsolationClass = Enum.Parse<IsolationClasses>(IsolationClassComboBox.SelectedItem.ToString());
    }
    private void StepSizeTemperatureNumericUpDown_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(StepSizeTemperatureNumericUpDown.Value))
            StepSizeTemperature = StepSizeTemperatureNumericUpDown.Value;
    }
    private void OutsideTemperatureNumericUpDown_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(OutsideTemperatureNumericUpDown.Minimum))
            MinOutsideTemperature = OutsideTemperatureNumericUpDown.Value;
        if (e.PropertyName == nameof(OutsideTemperatureNumericUpDown.Maximum))
            MaxOutsideTemperature = OutsideTemperatureNumericUpDown.Value;
    }
    private void UserTemperatureNumericUpDown_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(UserTemperatureNumericUpDown.Minimum))
            MinUserTemperature = UserTemperatureNumericUpDown.Value;
        if (e.PropertyName == nameof(UserTemperatureNumericUpDown.Maximum))
            MaxUserTemperature = UserTemperatureNumericUpDown.Value;
    }
    private void TemperatureHandleSizeNumericUpDown_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TemperatureHandleSizeNumericUpDown.Value))
            TemperatureHandleSize = TemperatureHandleSizeNumericUpDown.Value;
    }
    private void RoundingprecisionNumericUpDown_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(RoundingprecisionNumericUpDown.Value))
            Roundingprecision = RoundingprecisionNumericUpDown.Value;
    }
    private void ThemeRadioButtons_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ThemeRadioButtons.SelectedItem))
        {
            RadioButton? button = ThemeRadioButtons.SelectedItem as RadioButton;
            if (button is not null)
                Theme = button.Content.ToString();
        }
    }

    /*
Settings:
- IsolationClass (double)
- TemperatureRange 1 (Outside Temperature) Default = -10 bis +40 Min und Max Feld als Double
- TemperatureRange 2 (User Temperature) Default = 0 bis 30 Min und Max Feld als Double
- Temperature Handle Size Radius Default = 30
- Rounding Precision Default = 1 Dezimalstelle
- Step Size Temperature Default = 0,5 Grad
- Reset Settings (Button)
- Save Settings (Button)

Optional: 
- Theme
- Heater Type
*/
}