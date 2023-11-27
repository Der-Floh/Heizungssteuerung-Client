using Avalonia.Controls;
using Heizungssteuerung_SDK.Training;
using System;

namespace Heizungssteuerung_Client.Views;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();

        IsolationClassComboBox.ItemsSource = Enum.GetNames(typeof(IsolationClasses));

        ThemeRadioButtons.ItemsSource = new string[] { "Light", "Dark" };

        ThemeRadioButtons.PropertyChangedRuntime += ThemeRadioButtons_PropertyChangedRuntime;
    }

    private void ThemeRadioButtons_PropertyChangedRuntime(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ThemeRadioButtons.SelectedItem))
        {
            RadioButton button = ThemeRadioButtons.SelectedItem as RadioButton;
            string test = button.Content.ToString();
        }
    }
}