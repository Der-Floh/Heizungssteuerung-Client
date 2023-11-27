using Avalonia.Controls;
using Avalonia.Interactivity;
using Heizungssteuerung_Client.Data;
using Heizungssteuerung_Client.Views.Settings;
using System;
using System.Collections;
using System.ComponentModel;

namespace Heizungssteuerung_Client.Views;

public partial class SettingView : UserControl, INotifyPropertyChanged
{
    public SettingTypes SettingType { get => _settingType; set { if (_settingType == value) return; _settingType = value; OnPropertyChanged(nameof(SettingType)); } }
    private SettingTypes _settingType;

    public string? Text { get => _text; set { if (_text == value) return; _text = value; OnPropertyChanged(nameof(Text)); } }
    private string? _text;

    public string? ToolTip { get => _toolTip; set { if (_toolTip == value) return; _toolTip = value; OnPropertyChanged(nameof(ToolTip)); } }
    private string? _toolTip;

    public decimal Value { get => _value; set { if (_value == value) return; _value = value; OnPropertyChanged(nameof(Value)); } }
    private decimal _value;

    public decimal Increment { get => _increment; set { if (_increment == value) return; _increment = value; OnPropertyChanged(nameof(Increment)); } }
    private decimal _increment;

    public decimal Minimum { get => _minimum; set { if (_minimum == value) return; _minimum = value; OnPropertyChanged(nameof(Minimum)); } }
    private decimal _minimum;

    public decimal Maximum { get => _maximum; set { if (_maximum == value) return; _maximum = value; OnPropertyChanged(nameof(Maximum)); } }
    private decimal _maximum;
    public decimal MinNumericUpDownValue { get => _minNumericUpDownValue; set { if (_minNumericUpDownValue == value) return; _minNumericUpDownValue = value; OnPropertyChanged(nameof(MinNumericUpDownValue)); } }
    private decimal _minNumericUpDownValue;

    public decimal MinNumericUpDownIncrement { get => _minNumericUpDownIncrement; set { if (_minNumericUpDownIncrement == value) return; _minNumericUpDownIncrement = value; OnPropertyChanged(nameof(MinNumericUpDownIncrement)); } }
    private decimal _minNumericUpDownIncrement;

    public decimal MinNumericUpDownMinimum { get => _minNumericUpDownMinimum; set { if (_minNumericUpDownMinimum == value) return; _minNumericUpDownMinimum = value; OnPropertyChanged(nameof(MinNumericUpDownMinimum)); } }
    private decimal _minNumericUpDownMinimum;

    public decimal MinNumericUpDownMaximum { get => _minNumericUpDownMaximum; set { if (_minNumericUpDownMaximum == value) return; _minNumericUpDownMaximum = value; OnPropertyChanged(nameof(MinNumericUpDownMaximum)); } }
    private decimal _minNumericUpDownMaximum;
    public decimal MaxNumericUpDownValue { get => _maxNumericUpDownValue; set { if (_maxNumericUpDownValue == value) return; _maxNumericUpDownValue = value; OnPropertyChanged(nameof(MaxNumericUpDownValue)); } }
    private decimal _maxNumericUpDownValue;

    public decimal MaxNumericUpDownIncrement { get => _maxNumericUpDownIncrement; set { if (_maxNumericUpDownIncrement == value) return; _maxNumericUpDownIncrement = value; OnPropertyChanged(nameof(MaxNumericUpDownIncrement)); } }
    private decimal _maxNumericUpDownIncrement;

    public decimal MaxNumericUpDownMinimum { get => _maxNumericUpDownMinimum; set { if (_maxNumericUpDownMinimum == value) return; _maxNumericUpDownMinimum = value; OnPropertyChanged(nameof(MaxNumericUpDownMinimum)); } }
    private decimal _maxNumericUpDownMinimum;

    public decimal MaxNumericUpDownMaximum { get => _maxNumericUpDownMaximum; set { if (_maxNumericUpDownMaximum == value) return; _maxNumericUpDownMaximum = value; OnPropertyChanged(nameof(MaxNumericUpDownMaximum)); } }
    private decimal _maxNumericUpDownMaximum;

    public IEnumerable? ItemsSource { get => _itemsSource; set { if (_itemsSource == value) return; _itemsSource = value; OnPropertyChanged(nameof(ItemsSource)); } }
    private IEnumerable? _itemsSource;

    public object? SelectedItem { get => _selectedItem; set { if (_selectedItem == value) return; _selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); } }
    private object? _selectedItem;

    public int SelectedIndex { get => _selectedIndex; set { if (_selectedIndex == value) return; _selectedIndex = value; OnPropertyChanged(nameof(SelectedIndex)); } }
    private int _selectedIndex;

    public bool IsChecked { get => _isChecked; set { if (_isChecked == value) return; _isChecked = value; OnPropertyChanged(nameof(IsChecked)); } }
    private bool _isChecked;

    private SettingButtonSingleView? settingButtonSingleView;
    private SettingNumericUpDownSingleView? settingNumericUpDownSingleView;
    private SettingNumericUpDownMinMaxView? settingNumericUpDownMinMaxView;
    private SettingComboBoxSingleView? settingComboBoxSingleView;
    private SettingToggleSingleView? settingToggleSingleView;
    private SettingRadioButtonMultipleView? settingRadioButtonMultipleView;

    public new event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangedEventHandler? PropertyChangedRuntime;
    public event EventHandler<RoutedEventArgs>? Click;

    public SettingView()
    {
        InitializeComponent();

        PropertyChanged += SettingView_PropertyChanged;

        Increment = 1;
    }

    private void SettingView_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(SettingType):
                SetSettingType(SettingType);
                break;
            case nameof(Text):
                SetText(Text);
                break;
            case nameof(ToolTip):
                SetToolTip(ToolTip);
                break;
            case nameof(Value):
                SetValue(Value);
                break;
            case nameof(Increment):
                SetIncrement(Increment);
                break;
            case nameof(Minimum):
                SetMinimum(Minimum);
                break;
            case nameof(Maximum):
                SetMaximum(Maximum);
                break;
            case nameof(MinNumericUpDownValue):
                SetValueMinMax(MinNumericUpDownValue, true);
                break;
            case nameof(MinNumericUpDownIncrement):
                SetIncrementMinMax(MinNumericUpDownIncrement, true);
                break;
            case nameof(MinNumericUpDownMinimum):
                SetMinimumMinMax(MinNumericUpDownMinimum, true);
                break;
            case nameof(MinNumericUpDownMaximum):
                SetMaximumMinMax(MinNumericUpDownMaximum, true);
                break;
            case nameof(MaxNumericUpDownValue):
                SetValueMinMax(MaxNumericUpDownValue, false);
                break;
            case nameof(MaxNumericUpDownIncrement):
                SetIncrementMinMax(MaxNumericUpDownIncrement, false);
                break;
            case nameof(MaxNumericUpDownMinimum):
                SetMinimumMinMax(MaxNumericUpDownMinimum, false);
                break;
            case nameof(MaxNumericUpDownMaximum):
                SetMaximumMinMax(MaxNumericUpDownMinimum, false);
                break;
            case nameof(ItemsSource):
                SetItems(ItemsSource);
                break;
            case nameof(SelectedItem):
                SetSelectedItem(SelectedItem);
                break;
            case nameof(SelectedIndex):
                SetSelectedIndex(SelectedIndex);
                break;
            case nameof(IsChecked):
                SetIsChecked(IsChecked);
                break;
        }
    }

    public void SetSettingType(SettingTypes newType)
    {
        switch (newType)
        {
            case SettingTypes.Button:
                SettingPanel.Children.Clear();
                settingButtonSingleView = new SettingButtonSingleView();
                settingButtonSingleView.Click += SettingButtonSingleView_Click;
                SettingPanel.Children.Add(settingButtonSingleView);
                break;
            case SettingTypes.NumericUpDown:
                SettingPanel.Children.Clear();
                settingNumericUpDownSingleView = new SettingNumericUpDownSingleView();
                settingNumericUpDownSingleView.NumericUpDownValueChanged += SettingNumericUpDownSingleView_NumericUpDownValueChanged;
                SettingPanel.Children.Add(settingNumericUpDownSingleView);
                break;
            case SettingTypes.NumericUpDownMinMax:
                SettingPanel.Children.Clear();
                settingNumericUpDownMinMaxView = new SettingNumericUpDownMinMaxView();
                settingNumericUpDownMinMaxView.MinNumericUpDownValueChanged += SettingNumericUpDownMinMaxView_MinNumericUpDownValueChanged;
                settingNumericUpDownMinMaxView.MaxNumericUpDownValueChanged += SettingNumericUpDownMinMaxView_MaxNumericUpDownValueChanged;
                SettingPanel.Children.Add(settingNumericUpDownMinMaxView);
                break;
            case SettingTypes.ComboBox:
                SettingPanel.Children.Clear();
                settingComboBoxSingleView = new SettingComboBoxSingleView();
                settingComboBoxSingleView.SelectionChanged += SettingComboBoxSingleView_SelectionChanged;
                SettingPanel.Children.Add(settingComboBoxSingleView);
                break;
            case SettingTypes.Toggle:
                SettingPanel.Children.Clear();
                settingToggleSingleView = new SettingToggleSingleView();
                settingToggleSingleView.IsCheckedChanged += SettingToggleSingleView_SelectionChanged;
                SettingPanel.Children.Add(settingToggleSingleView);
                break;
            case SettingTypes.RadioButton:
                SettingPanel.Children.Clear();
                settingRadioButtonMultipleView = new SettingRadioButtonMultipleView();
                settingRadioButtonMultipleView.SelectionChanged += SettingRadioButtonMultipleView_SelectionChanged;
                SettingPanel.Children.Add(settingRadioButtonMultipleView);
                break;
        }
    }

    private void SetText(string? text)
    {
        switch (SettingType)
        {
            case SettingTypes.Button:
                if (settingButtonSingleView is not null)
                    settingButtonSingleView.Text = text;
                break;
            case SettingTypes.NumericUpDown:
                if (settingNumericUpDownSingleView is not null)
                    settingNumericUpDownSingleView.Text = text;
                break;
            case SettingTypes.NumericUpDownMinMax:
                if (settingNumericUpDownMinMaxView is not null)
                    settingNumericUpDownMinMaxView.Text = text;
                break;
            case SettingTypes.ComboBox:
                if (settingComboBoxSingleView is not null)
                    settingComboBoxSingleView.Text = text;
                break;
            case SettingTypes.Toggle:
                if (settingToggleSingleView is not null)
                    settingToggleSingleView.Text = text;
                break;
            case SettingTypes.RadioButton:
                if (settingRadioButtonMultipleView is not null)
                    settingRadioButtonMultipleView.Text = text;
                break;
        }
    }

    private void SetToolTip(string? toolTip)
    {
        switch (SettingType)
        {
            case SettingTypes.Button:
                if (settingButtonSingleView is not null)
                    settingButtonSingleView.ToolTip = toolTip;
                break;
            case SettingTypes.NumericUpDown:
                if (settingNumericUpDownSingleView is not null)
                    settingNumericUpDownSingleView.ToolTip = toolTip;
                break;
            case SettingTypes.NumericUpDownMinMax:
                if (settingNumericUpDownMinMaxView is not null)
                    settingNumericUpDownMinMaxView.ToolTip = toolTip;
                break;
            case SettingTypes.ComboBox:
                if (settingComboBoxSingleView is not null)
                    settingComboBoxSingleView.ToolTip = toolTip;
                break;
            case SettingTypes.Toggle:
                if (settingToggleSingleView is not null)
                    settingToggleSingleView.ToolTip = toolTip;
                break;
            case SettingTypes.RadioButton:
                if (settingRadioButtonMultipleView is not null)
                    settingRadioButtonMultipleView.ToolTip = toolTip;
                break;
        }
    }

    private void SetValue(decimal value)
    {
        switch (SettingType)
        {
            case SettingTypes.NumericUpDown:
                if (settingNumericUpDownSingleView is not null)
                    settingNumericUpDownSingleView.Value = value;
                break;
            case SettingTypes.NumericUpDownMinMax:
                if (settingNumericUpDownMinMaxView is not null)
                {
                    settingNumericUpDownMinMaxView.MinNumericUpDownValue = value;
                    settingNumericUpDownMinMaxView.MaxNumericUpDownValue = value;
                }
                break;
        }
    }

    private void SetValueMinMax(decimal value, bool minNumericUpDown)
    {
        switch (SettingType)
        {
            case SettingTypes.NumericUpDownMinMax:
                if (settingNumericUpDownMinMaxView is not null)
                {
                    if (minNumericUpDown)
                        settingNumericUpDownMinMaxView.MinNumericUpDownValue = value;
                    else
                        settingNumericUpDownMinMaxView.MaxNumericUpDownValue = value;
                }
                break;
        }
    }

    private void SetIncrement(decimal increment)
    {
        switch (SettingType)
        {
            case SettingTypes.NumericUpDown:
                if (settingNumericUpDownSingleView is not null)
                    settingNumericUpDownSingleView.Increment = increment;
                break;
            case SettingTypes.NumericUpDownMinMax:
                if (settingNumericUpDownMinMaxView is not null)
                {
                    settingNumericUpDownMinMaxView.MinNumericUpDownIncrement = increment;
                    settingNumericUpDownMinMaxView.MaxNumericUpDownIncrement = increment;
                }
                break;
        }
    }

    private void SetIncrementMinMax(decimal increment, bool minNumericUpDown)
    {
        switch (SettingType)
        {
            case SettingTypes.NumericUpDownMinMax:
                if (settingNumericUpDownMinMaxView is not null)
                {
                    if (minNumericUpDown)
                        settingNumericUpDownMinMaxView.MinNumericUpDownIncrement = increment;
                    else
                        settingNumericUpDownMinMaxView.MaxNumericUpDownIncrement = increment;
                }
                break;
        }
    }

    private void SetMinimum(decimal minimum)
    {
        switch (SettingType)
        {
            case SettingTypes.NumericUpDown:
                if (settingNumericUpDownSingleView is not null)
                    settingNumericUpDownSingleView.Minimum = minimum;
                break;
            case SettingTypes.NumericUpDownMinMax:
                if (settingNumericUpDownMinMaxView is not null)
                {
                    settingNumericUpDownMinMaxView.MinNumericUpDownMinimum = minimum;
                    settingNumericUpDownMinMaxView.MaxNumericUpDownMinimum = minimum;
                }
                break;
        }
    }

    private void SetMinimumMinMax(decimal minimum, bool minNumericUpDown)
    {
        switch (SettingType)
        {
            case SettingTypes.NumericUpDownMinMax:
                if (settingNumericUpDownMinMaxView is not null)
                {
                    if (minNumericUpDown)
                        settingNumericUpDownMinMaxView.MinNumericUpDownMinimum = minimum;
                    else
                        settingNumericUpDownMinMaxView.MaxNumericUpDownMinimum = minimum;
                }
                break;
        }
    }

    private void SetMaximum(decimal maximum)
    {
        switch (SettingType)
        {
            case SettingTypes.NumericUpDown:
                if (settingNumericUpDownSingleView is not null)
                    settingNumericUpDownSingleView.Maximum = maximum;
                break;
            case SettingTypes.NumericUpDownMinMax:
                if (settingNumericUpDownMinMaxView is not null)
                {
                    settingNumericUpDownMinMaxView.MinNumericUpDownMaximum = maximum;
                    settingNumericUpDownMinMaxView.MaxNumericUpDownMaximum = maximum;
                }
                break;
        }
    }
    private void SetMaximumMinMax(decimal maximum, bool minNumericUpDown)
    {
        switch (SettingType)
        {
            case SettingTypes.NumericUpDownMinMax:
                if (settingNumericUpDownMinMaxView is not null)
                {
                    if (minNumericUpDown)
                        settingNumericUpDownMinMaxView.MinNumericUpDownMaximum = maximum;
                    else
                        settingNumericUpDownMinMaxView.MaxNumericUpDownMaximum = maximum;
                }
                break;
        }
    }

    private void SetItems(IEnumerable? Items)
    {
        if (Items is null)
            return;
        switch (SettingType)
        {
            case SettingTypes.ComboBox:
                if (settingComboBoxSingleView is not null)
                {
                    settingComboBoxSingleView.Items = Items;
                    settingComboBoxSingleView.SelectedIndex = 0;
                }
                break;
            case SettingTypes.RadioButton:
                if (settingRadioButtonMultipleView is not null)
                {
                    foreach (var item in Items)
                    {
                        if (item is RadioButton radioButton)
                            settingRadioButtonMultipleView.AddRadioButton(radioButton);
                        else
                            settingRadioButtonMultipleView.AddRadioButton(item.ToString());
                    }
                    settingRadioButtonMultipleView.CheckedIndex = 0;
                }
                break;
        }
    }

    private void SetSelectedItem(object? SelectedItem)
    {
        switch (SettingType)
        {
            case SettingTypes.ComboBox:
                if (settingComboBoxSingleView is not null)
                    settingComboBoxSingleView.SelectedItem = SelectedItem;
                break;
            case SettingTypes.RadioButton:
                if (settingRadioButtonMultipleView is not null)
                    settingRadioButtonMultipleView.CheckedButton = SelectedItem as RadioButton;
                break;
        }

    }
    private void SetSelectedIndex(int SelectedIndex)
    {
        switch (SettingType)
        {
            case SettingTypes.ComboBox:
                if (settingComboBoxSingleView is not null)
                    settingComboBoxSingleView.SelectedIndex = SelectedIndex;
                break;
            case SettingTypes.RadioButton:
                if (settingRadioButtonMultipleView is not null)
                    settingRadioButtonMultipleView.CheckedIndex = SelectedIndex;
                break;
        }
    }

    private void SetIsChecked(bool isChecked)
    {
        switch (SettingType)
        {
            case SettingTypes.Toggle:
                if (settingToggleSingleView is not null)
                    settingToggleSingleView.IsChecked = isChecked;
                break;
        }
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnPropertyChangedRuntime(string propertyName)
    {
        PropertyChangedRuntime?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void SettingComboBoxSingleView_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (settingComboBoxSingleView is null)
            return;
        _selectedIndex = settingComboBoxSingleView.SelectedIndex;
        OnPropertyChangedRuntime(nameof(SelectedIndex));
        _selectedItem = settingComboBoxSingleView.SelectedItem;
        OnPropertyChangedRuntime(nameof(SelectedItem));
    }

    private void SettingRadioButtonMultipleView_SelectionChanged(object? sender, RoutedEventArgs e)
    {
        if (settingRadioButtonMultipleView is null)
            return;
        _selectedIndex = settingRadioButtonMultipleView.CheckedIndex;
        OnPropertyChangedRuntime(nameof(SelectedIndex));
        _selectedItem = settingRadioButtonMultipleView.CheckedButton;
        OnPropertyChangedRuntime(nameof(SelectedItem));
    }
    private void SettingToggleSingleView_SelectionChanged(object? sender, RoutedEventArgs e)
    {
        if (settingToggleSingleView is null)
            return;
        _isChecked = settingToggleSingleView.IsChecked;
        OnPropertyChangedRuntime(nameof(IsChecked));
    }
    private void SettingNumericUpDownMinMaxView_MaxNumericUpDownValueChanged(object? sender, NumericUpDownValueChangedEventArgs e)
    {
        if (settingNumericUpDownMinMaxView is null)
            return;
        _maxNumericUpDownValue = e.NewValue ?? _maxNumericUpDownValue;
        OnPropertyChangedRuntime(nameof(MaxNumericUpDownValue));
    }
    private void SettingNumericUpDownMinMaxView_MinNumericUpDownValueChanged(object? sender, NumericUpDownValueChangedEventArgs e)
    {
        if (settingNumericUpDownMinMaxView is null)
            return;
        _minNumericUpDownValue = e.NewValue ?? _minNumericUpDownValue;
        OnPropertyChangedRuntime(nameof(MinNumericUpDownValue));
    }
    private void SettingNumericUpDownSingleView_NumericUpDownValueChanged(object? sender, NumericUpDownValueChangedEventArgs e)
    {
        if (settingNumericUpDownSingleView is null)
            return;
        _value = e.NewValue ?? _value;
        OnPropertyChangedRuntime(nameof(Value));
    }
    private void SettingButtonSingleView_Click(object? sender, RoutedEventArgs e)
    {
        Click?.Invoke(sender, e);
    }
}