using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using System;
using System.Collections.Generic;

namespace Heizungssteuerung_Client.Views.Settings;

public partial class SettingRadioButtonMultipleView : UserControl
{
    public string? Text { get => _text; set { _text = value; MultipleTextBlock.Text = value; } }
    private string? _text;

    public string? ToolTip { get => _toolTip; set { _toolTip = value; UpdateToolTipVisibility(); } }
    private string? _toolTip;

    public bool ShowTextBlock { get => _showTextBlock; set { _showTextBlock = value; MultipleTextBlock.IsVisible = value; } }
    private bool _showTextBlock = true;

    public int CheckedIndex { get => RadioButtons.IndexOf(RadioButtons.Find(x => x.IsChecked ?? false)); set => RadioButtons[value].IsChecked = true; }

    public RadioButton CheckedButton { get => RadioButtons.Find(x => x.IsChecked ?? false); set => RadioButtons[RadioButtons.IndexOf(value)].IsChecked = true; }

    public List<RadioButton> RadioButtons { get => radioButtons; set { radioButtons = value; foreach (RadioButton radioButton in value) AddRadioButton(radioButton); } }
    private List<RadioButton> radioButtons = new List<RadioButton>();

    public event EventHandler<RoutedEventArgs>? SelectionChanged;

    public SettingRadioButtonMultipleView()
    {
        InitializeComponent();

        UpdateToolTipVisibility();
    }

    public void AddRadioButton(string? text, bool isChecked = false)
    {
        RadioButton radioButton = new RadioButton
        {
            Content = text,
            Padding = Thickness.Parse("4,0,12,0"),
            Margin = Thickness.Parse("0,5,0,5"),
            HorizontalAlignment = HorizontalAlignment.Right,
            Cursor = new Cursor(StandardCursorType.Hand),
            IsChecked = isChecked,
        };
        radioButton.IsCheckedChanged += MultipleRadioButton_IsCheckedChanged;
        RadioButtons.Add(radioButton);
        MultipleStackPanel.Children.Add(radioButton);
    }

    public void AddRadioButton(RadioButton radioButton, bool autoFillProperties = true)
    {
        if (autoFillProperties)
        {
            radioButton.Padding = Thickness.Parse("4,0,12,0");
            radioButton.Margin = Thickness.Parse("0,5,0,5");
            radioButton.HorizontalAlignment = HorizontalAlignment.Right;
            radioButton.Cursor = new Cursor(StandardCursorType.Hand);
        }
        radioButton.IsCheckedChanged += MultipleRadioButton_IsCheckedChanged;
        RadioButtons.Add(radioButton);
        MultipleStackPanel.Children.Add(radioButton);
    }

    private void UpdateToolTipVisibility()
    {
        if (string.IsNullOrEmpty(ToolTip))
            MultipleTextBlock.Cursor = new Cursor(StandardCursorType.Arrow);
        else
            MultipleTextBlock.Cursor = new Cursor(StandardCursorType.Help);
        Avalonia.Controls.ToolTip.SetTip(MultipleTextBlock, ToolTip);
        try
        {
            Avalonia.Controls.ToolTip.SetIsOpen(MultipleTextBlock, !string.IsNullOrEmpty(ToolTip));
        }
        catch { }
    }

    private void MultipleRadioButton_IsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        SelectionChanged?.Invoke(sender, e);
    }
}