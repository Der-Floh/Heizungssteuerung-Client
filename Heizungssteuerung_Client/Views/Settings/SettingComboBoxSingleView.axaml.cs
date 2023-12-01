using Avalonia.Controls;
using Avalonia.Input;
using System;
using System.Collections;

namespace Heizungssteuerung_Client.Views.Settings;

public partial class SettingComboBoxSingleView : UserControl
{
    public string? Text { get => _text; set { _text = value; SingleTextBlock.Text = value; } }
    private string? _text;

    public string? ToolTip { get => _toolTip; set { _toolTip = value; UpdateToolTipVisibility(); } }
    private string? _toolTip;

    public IEnumerable? Items { get => _items; set { _items = value; SingleComboBox.ItemsSource = value; } }
    private IEnumerable? _items;

    public object? SelectedItem { get => _selectedItem; set { _selectedItem = value; SingleComboBox.SelectedItem = value?.ToString(); } }
    private object? _selectedItem;

    public int SelectedIndex { get => _selectedIndex; set { _selectedIndex = value; SingleComboBox.SelectedIndex = value; } }
    private int _selectedIndex;

    public event EventHandler<SelectionChangedEventArgs>? SelectionChanged;

    public SettingComboBoxSingleView()
    {
        InitializeComponent();

        SingleComboBox.SelectionChanged += SingleComboBox_SelectionChanged;

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

    private void SingleComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        _selectedItem = SingleComboBox.SelectedItem;
        _selectedIndex = SingleComboBox.SelectedIndex;
        SelectionChanged?.Invoke(sender, e);
    }
}