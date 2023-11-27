using Avalonia.Controls;
using Heizungssteuerung_SDK.Training;
using System;

namespace Heizungssteuerung_Client.Views;

public partial class SettingsView : UserControl
{
    public static IsolationClasses IsolationClass { get; set; }
    public static decimal StepSizeTemperature { get; set; }
    public static decimal MinOutsideTemperature { get; set; }
    public static decimal MaxOutsideTemperature { get; set; }
    public static decimal MinUserTemperature { get; set; }
    public static decimal MaxUserTemperature { get; set; }
    public static decimal TemperatureHandleSize { get; set; }
    public static decimal RoundingPrecision { get; set; }
    public static string? ThemeSetting { get; set; }

    public SettingsView()
    {
        InitializeComponent();

        IsolationClassComboBox.ItemsSource = Enum.GetNames(typeof(IsolationClasses));
        ThemeRadioButtons.ItemsSource = new string[] { "Light", "Dark" };


        IsolationClassComboBox.PropertyChangedRuntime += IsolationClassComboBox_PropertyChanged;
        StepSizeTemperatureNumericUpDown.PropertyChangedRuntime += StepSizeTemperatureNumericUpDown_PropertyChanged;
        OutsideTemperatureNumericUpDown.PropertyChangedRuntime += OutsideTemperatureNumericUpDown_PropertyChanged;
        UserTemperatureNumericUpDown.PropertyChangedRuntime += UserTemperatureNumericUpDown_PropertyChanged;
        TemperatureHandleSizeNumericUpDown.PropertyChangedRuntime += TemperatureHandleSizeNumericUpDown_PropertyChanged;
        RoundingprecisionNumericUpDown.PropertyChangedRuntime += RoundingprecisionNumericUpDown_PropertyChanged;
        ThemeRadioButtons.PropertyChangedRuntime += ThemeRadioButtons_PropertyChanged;
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
        if (e.PropertyName == nameof(OutsideTemperatureNumericUpDown.MinNumericUpDownValue))
            MinOutsideTemperature = OutsideTemperatureNumericUpDown.MinNumericUpDownValue;
        if (e.PropertyName == nameof(OutsideTemperatureNumericUpDown.MaxNumericUpDownValue))
            MaxOutsideTemperature = OutsideTemperatureNumericUpDown.MaxNumericUpDownValue;
    }
    private void UserTemperatureNumericUpDown_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(UserTemperatureNumericUpDown.MinNumericUpDownValue))
            MinUserTemperature = UserTemperatureNumericUpDown.MinNumericUpDownValue;
        if (e.PropertyName == nameof(UserTemperatureNumericUpDown.MaxNumericUpDownValue))
            MaxUserTemperature = UserTemperatureNumericUpDown.MaxNumericUpDownValue;
    }
    private void TemperatureHandleSizeNumericUpDown_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TemperatureHandleSizeNumericUpDown.Value))
            TemperatureHandleSize = TemperatureHandleSizeNumericUpDown.Value;
    }
    private void RoundingprecisionNumericUpDown_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(RoundingprecisionNumericUpDown.Value))
            RoundingPrecision = RoundingprecisionNumericUpDown.Value;
    }
    private void ThemeRadioButtons_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ThemeRadioButtons.SelectedItem))
        {
            RadioButton? button = ThemeRadioButtons.SelectedItem as RadioButton;
            if (button is not null)
                ThemeSetting = button.Content.ToString();
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

    private void ThemeRadioButtons_PropertyChangedRuntime(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ThemeRadioButtons.SelectedItem))
        {
            RadioButton button = ThemeRadioButtons.SelectedItem as RadioButton;
            string test = button.Content.ToString();
        }
    }
}