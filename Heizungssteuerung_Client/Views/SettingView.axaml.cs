using Avalonia.Controls;
using Heizungssteuerung_Client.Data;
using Heizungssteuerung_Client.Views.Settings;
using System.Collections;
using System.ComponentModel;

namespace Heizungssteuerung_Client.Views;

public partial class SettingView : UserControl, INotifyPropertyChanged
{
    public SettingTypes SettingType { get => _settingType; set { _settingType = value; SetSettingType(value); } }
    private SettingTypes _settingType;

    public string Text { get => _text; set { if (_text == value) return; _text = value; SetText(value); OnPropertyChanged(nameof(Text)); } }
    private string _text;

    public decimal Value { get => _value; set { if (_value == value) return; _value = value; SetValue(value); OnPropertyChanged(nameof(Value)); } }
    private decimal _value;

    public decimal Minimum { get => _minimum; set { if (_minimum == value) return; _minimum = value; SetMinimum(value); OnPropertyChanged(nameof(Minimum)); } }
    private decimal _minimum;

    public decimal Maximum { get => _maximum; set { if (_maximum == value) return; _maximum = value; SetMaximum(value); OnPropertyChanged(nameof(Maximum)); } }
    private decimal _maximum;

    public IEnumerable Items { get => _items; set { if (_items == value) return; _items = value; SetItems(value); OnPropertyChanged(nameof(Items)); } }
    private IEnumerable _items;

    public object? SelectedItem { get => _selectedItem; set { if (_selectedItem == value) return; _selectedItem = value; SetSelectedItem(value); OnPropertyChanged(nameof(SelectedItem)); } }
    private object? _selectedItem;

    public int SelectedIndex { get => _selectedIndex; set { if (_selectedIndex == value) return; _selectedIndex = value; SetSelectedIndex(value); OnPropertyChanged(nameof(SelectedIndex)); } }
    private int _selectedIndex;

    private SettingNumericUpDownSingleView? settingNumericUpDownSingleView;
    private SettingToggleSingleView? settingToggleSingleView;
    private SettingComboBoxSingleView? SettingComboBoxSingleView;
    private SettingRadioButtonMultipleView? settingRadioButtonMultipleView;

    public event PropertyChangedEventHandler? PropertyChanged;

    public SettingView()
    {
        InitializeComponent();
    }

    public void SetSettingType(SettingTypes newType)
    {
        switch (newType)
        {
            case SettingTypes.NumericUpDown:
                SettingPanel.Children.Clear();
                settingNumericUpDownSingleView = new SettingNumericUpDownSingleView();
                SettingPanel.Children.Add(settingNumericUpDownSingleView);
                break;
            case SettingTypes.Toggle:
                SettingPanel.Children.Clear();
                settingToggleSingleView = new SettingToggleSingleView();
                SettingPanel.Children.Add(settingToggleSingleView);
                break;
            case SettingTypes.ComboBox:
                SettingPanel.Children.Clear();
                SettingComboBoxSingleView = new SettingComboBoxSingleView();
                SettingPanel.Children.Add(SettingComboBoxSingleView);
                break;
            case SettingTypes.RadioButton:
                SettingPanel.Children.Clear();
                settingRadioButtonMultipleView = new SettingRadioButtonMultipleView();
                SettingPanel.Children.Add(settingRadioButtonMultipleView);
                break;
        }
    }
    private void SetText(string text)
    {
        switch (SettingType)
        {
            case SettingTypes.NumericUpDown:
                if (settingNumericUpDownSingleView is not null)
                    settingNumericUpDownSingleView.Text = text;
                break;
            case SettingTypes.Toggle:
                if (settingToggleSingleView is not null)
                    settingToggleSingleView.Text = text;
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
        }
    }
    private void SetItems(IEnumerable Items)
    {
        switch (SettingType)
        {
            case SettingTypes.ComboBox:
                if (SettingComboBoxSingleView is not null)
                    SettingComboBoxSingleView.Items = Items;
                break;
        }
    }
    private void SetSelectedItem(object? SelectedItem)
    {
        switch (SettingType)
        {
            case SettingTypes.ComboBox:
                if (SettingComboBoxSingleView is not null)
                    SettingComboBoxSingleView.SelectedItem = SelectedItem;
                break;
        }

    }
    private void SetSelectedIndex(int SelectedIndex)
    {
        switch (SettingTypes.ComboBox)
        {
            case SettingTypes.ComboBox:
                if (SettingComboBoxSingleView is not null)
                    SettingComboBoxSingleView.SelectedIndex = SelectedIndex;
                break;
        }
    }
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}