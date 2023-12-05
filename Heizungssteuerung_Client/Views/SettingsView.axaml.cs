using Avalonia.Controls;
using Heizungssteuerung_SDK.Training;
using System;
using System.ComponentModel;

namespace Heizungssteuerung_Client.Views;

public partial class SettingsView : UserControl, INotifyPropertyChanged
{
    public IsolationClasses IsolationClass { get => Enum.Parse<IsolationClasses>(IsolationClassComboBox.SelectedItem.ToString()); set { if (Enum.Parse<IsolationClasses>(IsolationClassComboBox.SelectedItem.ToString()) == value) return; IsolationClassComboBox.SelectedItem = value; OnPropertyChanged(nameof(IsolationClass)); } }
    public decimal StepSizeTemperature { get => StepSizeTemperatureNumericUpDown.Value; set { if (StepSizeTemperatureNumericUpDown.Value == value) return; StepSizeTemperatureNumericUpDown.Value = value; OnPropertyChanged(nameof(StepSizeTemperature)); } }
    public decimal MinOutsideTemperature { get => OutsideTemperatureNumericUpDown.MinNumericUpDownValue; set { if (OutsideTemperatureNumericUpDown.MinNumericUpDownValue == value) return; OutsideTemperatureNumericUpDown.MinNumericUpDownValue = value; OnPropertyChanged(nameof(MinOutsideTemperature)); } }
    public decimal MaxOutsideTemperature { get => OutsideTemperatureNumericUpDown.MaxNumericUpDownValue; set { if (OutsideTemperatureNumericUpDown.MaxNumericUpDownValue == value) return; OutsideTemperatureNumericUpDown.MaxNumericUpDownValue = value; OnPropertyChanged(nameof(MaxOutsideTemperature)); } }
    public decimal MinUserTemperature { get => UserTemperatureNumericUpDown.MinNumericUpDownValue; set { if (UserTemperatureNumericUpDown.MinNumericUpDownValue == value) return; UserTemperatureNumericUpDown.MinNumericUpDownValue = value; OnPropertyChanged(nameof(MinUserTemperature)); } }
    public decimal MaxUserTemperature { get => UserTemperatureNumericUpDown.MaxNumericUpDownValue; set { if (UserTemperatureNumericUpDown.MaxNumericUpDownValue == value) return; UserTemperatureNumericUpDown.MaxNumericUpDownValue = value; OnPropertyChanged(nameof(MaxUserTemperature)); } }
    public decimal TemperatureHandleSize { get => TemperatureHandleSizeNumericUpDown.Value; set { if (TemperatureHandleSizeNumericUpDown.Value == value) return; TemperatureHandleSizeNumericUpDown.Value = value; OnPropertyChanged(nameof(TemperatureHandleSize)); } }
    public decimal DecimalPlaces { get => RoundingprecisionNumericUpDown.Value; set { if (RoundingprecisionNumericUpDown.Value == value) return; RoundingprecisionNumericUpDown.Value = value; OnPropertyChanged(nameof(DecimalPlaces)); } }

    public new event PropertyChangedEventHandler? PropertyChanged;

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
    }

    private void IsolationClassComboBox_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IsolationClassComboBox.SelectedItem))
            OnPropertyChanged(nameof(IsolationClass));
    }

    private void StepSizeTemperatureNumericUpDown_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(StepSizeTemperatureNumericUpDown.Value))
            OnPropertyChanged(nameof(StepSizeTemperature));
    }

    private void OutsideTemperatureNumericUpDown_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(OutsideTemperatureNumericUpDown.MinNumericUpDownValue))
            OnPropertyChanged(nameof(MinOutsideTemperature));
        if (e.PropertyName == nameof(OutsideTemperatureNumericUpDown.MaxNumericUpDownValue))
            OnPropertyChanged(nameof(MaxOutsideTemperature));
    }

    private void UserTemperatureNumericUpDown_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(UserTemperatureNumericUpDown.MinNumericUpDownValue))
            OnPropertyChanged(nameof(MinUserTemperature));
        if (e.PropertyName == nameof(UserTemperatureNumericUpDown.MaxNumericUpDownValue))
            OnPropertyChanged(nameof(MaxUserTemperature));
    }

    private void TemperatureHandleSizeNumericUpDown_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TemperatureHandleSizeNumericUpDown.Value))
            OnPropertyChanged(nameof(TemperatureHandleSize));
    }

    private void RoundingprecisionNumericUpDown_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(RoundingprecisionNumericUpDown.Value))
            OnPropertyChanged(nameof(DecimalPlaces));
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}