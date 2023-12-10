using Avalonia.Controls;
using Heizungssteuerung_SDK.Training;
using System;
using System.ComponentModel;

namespace Heizungssteuerung_Client.Views;

public partial class SettingsView : UserControl, INotifyPropertyChanged
{
    public string? ViewName { get; set; }
    public string? ViewIcon { get; set; }

    public IsolationClasses IsolationClass { get => Enum.Parse<IsolationClasses>(IsolationClassComboBox.SelectedItem.ToString()); set { if (Enum.Parse<IsolationClasses>(IsolationClassComboBox.SelectedItem.ToString()) == value) return; IsolationClassComboBox.SelectedItem = value; OnPropertyChanged(nameof(IsolationClass)); } }
    public decimal StepSizeTemperature { get => StepSizeTemperatureNumericUpDown.Value; set { if (StepSizeTemperatureNumericUpDown.Value == value) return; StepSizeTemperatureNumericUpDown.Value = value; OnPropertyChanged(nameof(StepSizeTemperature)); } }
    public decimal MinOutsideTemperature { get => OutsideTemperatureNumericUpDown.MinNumericUpDownValue; set { if (OutsideTemperatureNumericUpDown.MinNumericUpDownValue == value) return; OutsideTemperatureNumericUpDown.MinNumericUpDownValue = value; OnPropertyChanged(nameof(MinOutsideTemperature)); } }
    public decimal MaxOutsideTemperature { get => OutsideTemperatureNumericUpDown.MaxNumericUpDownValue; set { if (OutsideTemperatureNumericUpDown.MaxNumericUpDownValue == value) return; OutsideTemperatureNumericUpDown.MaxNumericUpDownValue = value; OnPropertyChanged(nameof(MaxOutsideTemperature)); } }
    public decimal MinUserTemperature { get => UserTemperatureNumericUpDown.MinNumericUpDownValue; set { if (UserTemperatureNumericUpDown.MinNumericUpDownValue == value) return; UserTemperatureNumericUpDown.MinNumericUpDownValue = value; OnPropertyChanged(nameof(MinUserTemperature)); } }
    public decimal MaxUserTemperature { get => UserTemperatureNumericUpDown.MaxNumericUpDownValue; set { if (UserTemperatureNumericUpDown.MaxNumericUpDownValue == value) return; UserTemperatureNumericUpDown.MaxNumericUpDownValue = value; OnPropertyChanged(nameof(MaxUserTemperature)); } }
    public decimal TemperatureHandleSize { get => TemperatureHandleSizeNumericUpDown.Value; set { if (TemperatureHandleSizeNumericUpDown.Value == value) return; TemperatureHandleSizeNumericUpDown.Value = value; OnPropertyChanged(nameof(TemperatureHandleSize)); } }
    public decimal PredictTemperatureStepSize { get => PredictTempNumericUpDown.Value; set { if (PredictTempNumericUpDown.Value == value) return; PredictTempNumericUpDown.Value = value; OnPropertyChanged(nameof(PredictTemperatureStepSize)); } }
    public decimal DecimalPlaces { get => RoundingprecisionNumericUpDown.Value; set { if (RoundingprecisionNumericUpDown.Value == value) return; RoundingprecisionNumericUpDown.Value = value; OnPropertyChanged(nameof(DecimalPlaces)); } }

    public bool InstantSave { get => _instantSave; set { if (_instantSave == value) return; _instantSave = value; HandleInstantSaveChanged(value); } }
    private bool _instantSave;

    public new event PropertyChangedEventHandler? PropertyChanged;

    public SettingsView()
    {
        InitializeComponent();

        IsolationClassComboBox.ItemsSource = Enum.GetNames(typeof(IsolationClasses));
        IsolationClassComboBox.SelectedItem = IsolationClasses.C;

        IsolationClassComboBox.PropertyChangedRuntime += IsolationClassComboBox_PropertyChanged;
        StepSizeTemperatureNumericUpDown.PropertyChangedRuntime += StepSizeTemperatureNumericUpDown_PropertyChanged;
        OutsideTemperatureNumericUpDown.PropertyChangedRuntime += OutsideTemperatureNumericUpDown_PropertyChanged;
        UserTemperatureNumericUpDown.PropertyChangedRuntime += UserTemperatureNumericUpDown_PropertyChanged;
        TemperatureHandleSizeNumericUpDown.PropertyChangedRuntime += TemperatureHandleSizeNumericUpDown_PropertyChanged;
        PredictTempNumericUpDown.PropertyChangedRuntime += PredictTempNumericUpDown_PropertyChangedRuntime;
        RoundingprecisionNumericUpDown.PropertyChangedRuntime += RoundingprecisionNumericUpDown_PropertyChanged;
        SaveButton.Click += SaveButton_Click;

        LoadSettings();
    }

    public void LoadSettings()
    {
        Data.Settings.Load();

        string? valueInstantSave = Data.Settings.Get(nameof(InstantSave));
        if (!string.IsNullOrEmpty(valueInstantSave) && bool.TryParse(valueInstantSave, out bool boolValue))
            InstantSave = boolValue;

        string? valueIsolationClass = Data.Settings.Get(nameof(IsolationClass));
        if (!string.IsNullOrEmpty(valueIsolationClass) && Enum.TryParse(valueIsolationClass, out IsolationClasses enumValue))
            IsolationClassComboBox.SelectedItem = enumValue;

        string? valueStepSizeTemperature = Data.Settings.Get(nameof(StepSizeTemperature));
        if (!string.IsNullOrEmpty(valueStepSizeTemperature) && decimal.TryParse(valueStepSizeTemperature, out decimal decimalValue))
            StepSizeTemperatureNumericUpDown.Value = decimalValue;

        string? valueMinOutsideTemperature = Data.Settings.Get(nameof(MinOutsideTemperature));
        if (!string.IsNullOrEmpty(valueMinOutsideTemperature) && decimal.TryParse(valueMinOutsideTemperature, out decimalValue))
            OutsideTemperatureNumericUpDown.MinNumericUpDownValue = decimalValue;

        string? valueMaxOutsideTemperature = Data.Settings.Get(nameof(MaxOutsideTemperature));
        if (!string.IsNullOrEmpty(valueMaxOutsideTemperature) && decimal.TryParse(valueMaxOutsideTemperature, out decimalValue))
            OutsideTemperatureNumericUpDown.MaxNumericUpDownValue = decimalValue;

        string? valueMinUserTemperature = Data.Settings.Get(nameof(MinUserTemperature));
        if (!string.IsNullOrEmpty(valueMinUserTemperature) && decimal.TryParse(valueMinUserTemperature, out decimalValue))
            UserTemperatureNumericUpDown.MinNumericUpDownValue = decimalValue;

        string? valueMaxUserTemperature = Data.Settings.Get(nameof(MaxUserTemperature));
        if (!string.IsNullOrEmpty(valueMaxUserTemperature) && decimal.TryParse(valueMaxUserTemperature, out decimalValue))
            UserTemperatureNumericUpDown.MaxNumericUpDownValue = decimalValue;

        string? valueTemperatureHandleSize = Data.Settings.Get(nameof(TemperatureHandleSize));
        if (!string.IsNullOrEmpty(valueTemperatureHandleSize) && decimal.TryParse(valueTemperatureHandleSize, out decimalValue))
            TemperatureHandleSizeNumericUpDown.Value = decimalValue;

        string? valuePredictTemperatureStepSize = Data.Settings.Get(nameof(PredictTemperatureStepSize));
        if (!string.IsNullOrEmpty(valuePredictTemperatureStepSize) && decimal.TryParse(valuePredictTemperatureStepSize, out decimalValue))
            PredictTempNumericUpDown.Value = decimalValue;

        string? valueDecimalPlaces = Data.Settings.Get(nameof(DecimalPlaces));
        if (!string.IsNullOrEmpty(valueDecimalPlaces) && decimal.TryParse(valueDecimalPlaces, out decimalValue))
            RoundingprecisionNumericUpDown.Value = decimalValue;
    }

    private void HandleInstantSaveChanged(bool value)
    {
        SaveButton.IsVisible = !value;
        if (value)
        {
            if (SettingsGrid.RowDefinitions.Count >= 5)
                SettingsGrid.RowDefinitions.RemoveAt(SettingsGrid.RowDefinitions.Count - 1);
        }
        else if (SettingsGrid.RowDefinitions.Count < 5)
            SettingsGrid.RowDefinitions.Add(new RowDefinition(GridLength.Parse("*")));
    }

    private void SaveButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _ = Data.Settings.Save();
    }

    private void IsolationClassComboBox_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IsolationClassComboBox.SelectedItem))
        {
            Data.Settings.Set(nameof(IsolationClass), IsolationClass.ToString());
            OnPropertyChanged(nameof(IsolationClass));
        }
    }

    private void StepSizeTemperatureNumericUpDown_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(StepSizeTemperatureNumericUpDown.Value))
        {
            Data.Settings.Set(nameof(StepSizeTemperature), StepSizeTemperature.ToString());
            OnPropertyChanged(nameof(StepSizeTemperature));
        }
    }

    private void OutsideTemperatureNumericUpDown_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(OutsideTemperatureNumericUpDown.MinNumericUpDownValue))
        {
            Data.Settings.Set(nameof(MinOutsideTemperature), MinOutsideTemperature.ToString());
            OnPropertyChanged(nameof(MinOutsideTemperature));
        }
        if (e.PropertyName == nameof(OutsideTemperatureNumericUpDown.MaxNumericUpDownValue))
        {
            Data.Settings.Set(nameof(MaxOutsideTemperature), MaxOutsideTemperature.ToString());
            OnPropertyChanged(nameof(MaxOutsideTemperature));
        }
    }

    private void UserTemperatureNumericUpDown_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(UserTemperatureNumericUpDown.MinNumericUpDownValue))
        {
            Data.Settings.Set(nameof(MinUserTemperature), MinUserTemperature.ToString());
            OnPropertyChanged(nameof(MinUserTemperature));
        }
        if (e.PropertyName == nameof(UserTemperatureNumericUpDown.MaxNumericUpDownValue))
        {
            Data.Settings.Set(nameof(MaxUserTemperature), MaxUserTemperature.ToString());
            OnPropertyChanged(nameof(MaxUserTemperature));
        }
    }

    private void TemperatureHandleSizeNumericUpDown_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TemperatureHandleSizeNumericUpDown.Value))
        {
            Data.Settings.Set(nameof(TemperatureHandleSize), TemperatureHandleSize.ToString());
            OnPropertyChanged(nameof(TemperatureHandleSize));
        }
    }

    private void PredictTempNumericUpDown_PropertyChangedRuntime(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(PredictTempNumericUpDown.Value))
        {
            Data.Settings.Set(nameof(PredictTemperatureStepSize), PredictTemperatureStepSize.ToString());
            OnPropertyChanged(nameof(PredictTemperatureStepSize));
        }
    }

    private void RoundingprecisionNumericUpDown_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(RoundingprecisionNumericUpDown.Value))
        {
            Data.Settings.Set(nameof(DecimalPlaces), DecimalPlaces.ToString());
            OnPropertyChanged(nameof(DecimalPlaces));
        }
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        if (InstantSave)
            _ = Data.Settings.Save();
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}