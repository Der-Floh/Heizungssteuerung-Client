using Avalonia.Controls;

namespace Heizungssteuerung_Client.Views.Settings;

public partial class SettingToggleSingleView : UserControl
{
    public string Text { get => _text; set { _text = value; SingleTextBlock.Text = value; } }
    private string _text;

    public bool IsChecked { get => _isChecked; set { _isChecked = value; SingleToggleButton.IsChecked = value; } }
    private bool _isChecked;

    public SettingToggleSingleView()
    {
        InitializeComponent();

        SingleToggleButton.IsCheckedChanged += SingleToggleButton_IsCheckedChanged;
    }

    private void SingleToggleButton_IsCheckedChanged(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _isChecked = SingleToggleButton.IsChecked ?? false;
    }
}