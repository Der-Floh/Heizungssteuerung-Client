using Avalonia.Controls;
using Heizungssteuerung_Client.Data;
using Heizungssteuerung_Client.Views.Settings;
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

    private SettingNumericUpDownSingleView? settingNumericUpDownSingleView;
    private SettingToggleSingleView? settingToggleSingleView;

    public event PropertyChangedEventHandler PropertyChanged;

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

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}