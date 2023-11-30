using Avalonia.Controls;
using Heizungssteuerung_Client.Data;
using Heizungssteuerung_SDK.Training;
using System;

namespace Heizungssteuerung_Client.Views;

public partial class SettingsView : UserControl
{
    public static IsolationClasses IsolationClass { get; set; } = IsolationClasses.A;
    public static decimal StepSizeTemperature { get; set; } = 0.5m;
    public static decimal MinOutsideTemperature { get => _minOutsideTemperature; set { _minOutsideTemperature = value; InitUserTemps(); } }
    private static decimal _minOutsideTemperature = -10.0m;
    public static decimal MaxOutsideTemperature { get => _maxOutsideTemperature; set { _maxOutsideTemperature = value; InitUserTemps(); } }
    public static decimal _maxOutsideTemperature = 40.0m;
    public static decimal OutsideTemperatureStepSize { get => _outsideTemperatureStepSize; set { _outsideTemperatureStepSize = value; InitUserTemps(); } }
    public static decimal _outsideTemperatureStepSize = 5m;
    public static decimal MinUserTemperature { get; set; } = 0.0m;
    public static decimal MaxUserTemperature { get; set; } = 40.0m;
    public static decimal TemperatureHandleSize { get; set; } = 30.0m;
    public static decimal RoundingPrecision { get; set; } = 2.0m;
    public static Temperature[] UserTemperatures { get; set; } = new Temperature[11];

    public SettingsView()
    {
        InitializeComponent();

        IsolationClassComboBox.ItemsSource = Enum.GetNames(typeof(IsolationClasses));

        IsolationClassComboBox.SelectedItem = IsolationClass;
        StepSizeTemperatureNumericUpDown.Value = StepSizeTemperature;
        OutsideTemperatureNumericUpDown.MinNumericUpDownValue = MinOutsideTemperature;
        OutsideTemperatureNumericUpDown.MaxNumericUpDownValue = MaxOutsideTemperature;
        UserTemperatureNumericUpDown.MinNumericUpDownValue = MinUserTemperature;
        UserTemperatureNumericUpDown.MaxNumericUpDownValue = MaxUserTemperature;
        TemperatureHandleSizeNumericUpDown.Value = TemperatureHandleSize;
        RoundingprecisionNumericUpDown.Value = RoundingPrecision;

        IsolationClassComboBox.PropertyChangedRuntime += IsolationClassComboBox_PropertyChanged;
        StepSizeTemperatureNumericUpDown.PropertyChangedRuntime += StepSizeTemperatureNumericUpDown_PropertyChanged;
        OutsideTemperatureNumericUpDown.PropertyChangedRuntime += OutsideTemperatureNumericUpDown_PropertyChanged;
        UserTemperatureNumericUpDown.PropertyChangedRuntime += UserTemperatureNumericUpDown_PropertyChanged;
        TemperatureHandleSizeNumericUpDown.PropertyChangedRuntime += TemperatureHandleSizeNumericUpDown_PropertyChanged;
        RoundingprecisionNumericUpDown.PropertyChangedRuntime += RoundingprecisionNumericUpDown_PropertyChanged;
    }

    public static void InitUserTemps()
    {
        UserTemperatures = CalculateTemperatures();
    }

    private static Temperature[] CalculateTemperatures()
    {
        double min = (double)Math.Min(MinOutsideTemperature, MaxOutsideTemperature);
        double max = (double)Math.Max(MinOutsideTemperature, MaxOutsideTemperature);
        Temperature[] temperatures = GetTemperatures(min, max, (double)OutsideTemperatureStepSize);
        return temperatures;
    }

    private static Temperature[] GetTemperatures(double minTemperature, double maxTemperature, double stepSize)
    {
        if (stepSize <= 0 || stepSize >= (maxTemperature - minTemperature))
            throw new ArgumentException("Invalid stepSize value.");

        int numTemperatures = (int)((maxTemperature - minTemperature) / stepSize) + 1;
        Temperature[] temperatures = new Temperature[numTemperatures];

        for (int i = 0; i < numTemperatures; i++)
        {
            Temperature temperature = new Temperature { XValue = minTemperature + i * stepSize };
            temperatures[i] = temperature;
        }

        return temperatures;
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
}