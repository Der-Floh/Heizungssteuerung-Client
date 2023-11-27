using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;

namespace Heizungssteuerung_Client.Views.Settings;

public partial class SettingToggleSingleView : UserControl
{
    public string? Text { get => _text; set { _text = value; SingleTextBlock.Text = value; } }
    private string? _text;

    public string? ToolTip { get => _toolTip; set { _toolTip = value; UpdateToolTipVisibility(); } }
    private string? _toolTip;

    public bool IsChecked { get => _isChecked; set { _isChecked = value; SingleToggleButton.IsChecked = value; } }
    private bool _isChecked;

    public event EventHandler<RoutedEventArgs>? IsCheckedChanged;

    public SettingToggleSingleView()
    {
        InitializeComponent();

        SingleToggleButton.IsCheckedChanged += SingleToggleButton_IsCheckedChanged;

        UpdateToolTipVisibility();
    }

    private void SingleToggleButton_IsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        _isChecked = SingleToggleButton.IsChecked ?? false;
        IsCheckedChanged?.Invoke(sender, e);
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