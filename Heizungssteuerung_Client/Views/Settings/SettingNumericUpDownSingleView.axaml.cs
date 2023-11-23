using Avalonia.Controls;
using Avalonia.Input;
using System;

namespace Heizungssteuerung_Client.Views.Settings;

public partial class SettingNumericUpDownSingleView : UserControl
{
    public string? Text { get => _text; set { _text = value; SingleTextBlock.Text = value; } }
    private string? _text;

    public string? ToolTip { get => _toolTip; set { _toolTip = value; UpdateToolTipVisibility(); } }
    private string? _toolTip;

    public decimal Value { get => _value; set { _value = value; SingleNumericUpDown.Value = value; } }
    private decimal _value;

    public decimal Increment { get => _increment; set { _increment = value; SingleNumericUpDown.Increment = value; } }
    private decimal _increment;

    public decimal Minimum { get => _minimum; set { _minimum = value; SingleNumericUpDown.Minimum = value; } }
    private decimal _minimum;

    public decimal Maximum { get => _maximum; set { _maximum = value; SingleNumericUpDown.Maximum = value; } }
    private decimal _maximum;

    public event EventHandler<NumericUpDownValueChangedEventArgs>? NumericUpDownValueChanged;

    public SettingNumericUpDownSingleView()
    {
        InitializeComponent();

        SingleNumericUpDown.ValueChanged += (sender, e) => NumericUpDownValueChanged?.Invoke(sender, e);

        UpdateToolTipVisibility();
    }

    private void UpdateToolTipVisibility()
    {
        if (string.IsNullOrEmpty(ToolTip))
            SingleTextBlock.Cursor = new Cursor(StandardCursorType.Arrow);
        else
            SingleTextBlock.Cursor = new Cursor(StandardCursorType.Help);
        Avalonia.Controls.ToolTip.SetTip(SingleTextBlock, ToolTip);
        try
        {
            Avalonia.Controls.ToolTip.SetIsOpen(SingleTextBlock, !string.IsNullOrEmpty(ToolTip));
        }
        catch { }
    }
}