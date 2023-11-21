using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace Heizungssteuerung_Client.Views.Settings;

public partial class SettingButtonSingleView : UserControl
{
    public bool TextOutsideButton { get => _textOutsideButton; set { _textOutsideButton = value; SingleTextBlock.IsVisible = value; } }
    private bool _textOutsideButton;
    public string Text { get => _text; set { _text = value; SingleTextBlock.Text = value; SingleButton.Content = value; } }
    private string _text;

    public event EventHandler<RoutedEventArgs>? Click;

    public SettingButtonSingleView()
    {
        InitializeComponent();

        SingleButton.Click += SingleButton_Click;
    }

    private void SingleButton_Click(object? sender, RoutedEventArgs e) => OnClick(sender, e);

    protected virtual void OnClick(object? sender, RoutedEventArgs e) => Click?.Invoke(sender, e);
}