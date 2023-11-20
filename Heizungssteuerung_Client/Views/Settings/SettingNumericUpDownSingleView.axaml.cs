using Avalonia.Controls;

namespace Heizungssteuerung_Client.Views.Settings;

public partial class SettingNumericUpDownSingleView : UserControl
{
    public string Text { get => _text; set { _text = value; SingleTextBlock.Text = value; } }
    private string _text;
    public decimal Value { get => _value; set { _value = value; SingleNumericUpDown.Value = value; } }
    private decimal _value;
    public decimal Minimum { get => _minimum; set { _minimum = value; SingleNumericUpDown.Minimum = value; } }
    private decimal _minimum;
    public decimal Maximum { get => _maximum; set { _maximum = value; SingleNumericUpDown.Maximum = value; } }
    private decimal _maximum;

    public SettingNumericUpDownSingleView()
    {
        InitializeComponent();
    }
}