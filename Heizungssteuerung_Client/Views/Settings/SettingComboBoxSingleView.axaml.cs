using Avalonia.Controls;
using System.Collections;

namespace Heizungssteuerung_Client.Views.Settings;

public partial class SettingComboBoxSingleView : UserControl
{
    public string Text { get => _text; set { _text = value; SingleTextBlock.Text = value; } }
    private string _text;

    public IEnumerable Items { get => _items; set { _items = value; SingleComboBox.ItemsSource = value; } }
    private IEnumerable _items;

    public object? SelectedItem { get => _selectedItem; set { _selectedItem = value; SingleComboBox.SelectedItem = value; } }
    private object? _selectedItem;

    public int SelectedIndex { get => _selectedIndex; set { _selectedIndex = value; SingleComboBox.SelectedIndex = value; } }
    private int _selectedIndex;

    public SettingComboBoxSingleView()
    {
        InitializeComponent();

        SingleComboBox.SelectionChanged += SingleComboBox_SelectionChanged;
    }

    private void SingleComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        _selectedItem = SingleComboBox.SelectedItem;
        _selectedIndex = SingleComboBox.SelectedIndex;
    }
}