using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;

namespace Heizungssteuerung_Client.Views.Settings;

public partial class SettingButtonSingleView : UserControl
{
    public bool TextOutsideButtonVisible { get => _textOutsideButtonVisible; set { _textOutsideButtonVisible = value; SingleTextBlock.IsVisible = value; } }
    private bool _textOutsideButtonVisible;

    public string? TextBlockText { get => _textBlockText; set { _textBlockText = value; SingleTextBlock.Text = value; } }
    private string? _textBlockText;

    public string? ToolTip { get => _toolTip; set { _toolTip = value; UpdateToolTipVisibility(); } }
    private string? _toolTip;

    public string? Text { get => _text; set { _text = value; SingleTextBlock.Text = value; SingleButton.Content = value; } }
    private string? _text;

    public event EventHandler<RoutedEventArgs>? Click;

    public SettingButtonSingleView()
    {
        InitializeComponent();

        SingleButton.Click += (sender, e) => Click?.Invoke(sender, e);

        UpdateToolTipVisibility();
    }

    private void UpdateToolTipVisibility()
    {
        if (string.IsNullOrEmpty(ToolTip))
            SingleTextBlock.Cursor = new Cursor(StandardCursorType.Arrow);
        else
            SingleTextBlock.Cursor = new Cursor(StandardCursorType.Help);
        Avalonia.Controls.ToolTip.SetTip(SingleButton, ToolTip);
        try
        {
            Avalonia.Controls.ToolTip.SetIsOpen(SingleButton, !string.IsNullOrEmpty(ToolTip));
        }
        catch { }
    }
}