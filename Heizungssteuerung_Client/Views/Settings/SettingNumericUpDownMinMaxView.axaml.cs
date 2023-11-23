using Avalonia.Controls;
using Avalonia.Input;
using System;

namespace Heizungssteuerung_Client.Views.Settings;

public partial class SettingNumericUpDownMinMaxView : UserControl
{
    public string? Text { get => _text; set { _text = value; MinMaxTextBlock.Text = value; } }
    private string? _text;

    public string? ToolTip { get => _toolTip; set { _toolTip = value; UpdateToolTipVisibility(); } }
    private string? _toolTip;

    public decimal MinNumericUpDownValue { get => _minNumericUpDownValue; set { _minNumericUpDownValue = value; MinNumericUpDown.Value = value; } }
    private decimal _minNumericUpDownValue;

    public decimal MinNumericUpDownIncrement { get => _minNumericUpDownIncrement; set { _minNumericUpDownIncrement = value; MinNumericUpDown.Increment = value; } }
    private decimal _minNumericUpDownIncrement;

    public decimal MinNumericUpDownMinimum { get => _minNumericUpDownMinimum; set { _minNumericUpDownMinimum = value; MinNumericUpDown.Minimum = value; } }
    private decimal _minNumericUpDownMinimum;

    public decimal MinNumericUpDownMaximum { get => _minNumericUpDownMaximum; set { _minNumericUpDownMaximum = value; MinNumericUpDown.Maximum = value; } }
    private decimal _minNumericUpDownMaximum;

    public decimal MaxNumericUpDownValue { get => _maxNumericUpDownValue; set { _maxNumericUpDownValue = value; MaxNumericUpDown.Value = value; } }
    private decimal _maxNumericUpDownValue;

    public decimal MaxNumericUpDownIncrement { get => _maxNumericUpDownIncrement; set { _maxNumericUpDownIncrement = value; MaxNumericUpDown.Increment = value; } }
    private decimal _maxNumericUpDownIncrement;

    public decimal MaxNumericUpDownMinimum { get => _maxNumericUpDownMinimum; set { _maxNumericUpDownMinimum = value; MaxNumericUpDown.Minimum = value; } }
    private decimal _maxNumericUpDownMinimum;

    public decimal MaxNumericUpDownMaximum { get => _maxNumericUpDownMaximum; set { _maxNumericUpDownMaximum = value; MaxNumericUpDown.Maximum = value; } }
    private decimal _maxNumericUpDownMaximum;

    public event EventHandler<NumericUpDownValueChangedEventArgs>? MinNumericUpDownValueChanged;
    public event EventHandler<NumericUpDownValueChangedEventArgs>? MaxNumericUpDownValueChanged;

    public SettingNumericUpDownMinMaxView()
    {
        InitializeComponent();

        MinNumericUpDown.ValueChanged += (sender, e) => MinNumericUpDownValueChanged?.Invoke(sender, e);
        MaxNumericUpDown.ValueChanged += (sender, e) => MaxNumericUpDownValueChanged?.Invoke(sender, e);
    }

    private void UpdateToolTipVisibility()
    {
        if (string.IsNullOrEmpty(ToolTip))
            MinMaxTextBlock.Cursor = new Cursor(StandardCursorType.Arrow);
        else
            MinMaxTextBlock.Cursor = new Cursor(StandardCursorType.Help);
        Avalonia.Controls.ToolTip.SetTip(MinMaxTextBlock, ToolTip);
        try
        {
            Avalonia.Controls.ToolTip.SetIsOpen(MinMaxTextBlock, !string.IsNullOrEmpty(ToolTip));
        }
        catch { }
    }
}