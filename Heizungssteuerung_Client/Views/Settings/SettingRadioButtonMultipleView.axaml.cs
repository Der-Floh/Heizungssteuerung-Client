using Avalonia.Controls;

namespace Heizungssteuerung_Client.Views.Settings;

public partial class SettingRadioButtonMultipleView : UserControl
{
    public string Text { get => _text; set { _text = value; MultipleTextBlock.Text = value; } }
    private string _text;

    public bool IsChecked { get => _isChecked; set { _isChecked = value; MultipleRadioButton.IsChecked = value; } }
    private bool _isChecked;
    public SettingRadioButtonMultipleView()
    {
        InitializeComponent();

        MultipleRadioButton.IsCheckedChanged += MultipleRadioButton_IsCheckedChanged;
    }
    private void MultipleRadioButton_IsCheckedChanged(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _isChecked = MultipleRadioButton.IsChecked ?? false;
    }
}