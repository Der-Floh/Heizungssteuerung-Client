using Avalonia.Controls;
using Heizungssteuerung_Client.Data;
using System;

namespace Heizungssteuerung_Client.Views;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();

        IsolationClassComboBox.ItemsSource = Enum.GetNames(typeof(IsolationClasses));
        IsolationClassComboBox.SelectedIndex = 0;
    }
    /*
     Settings:
    - IsolationClass (double)
    - TemperatureRange 1 (Outside Temperature) Default = -10 bis +40 Min und Max Feld als Double
    - TemperatureRange 2 (User Temperature) Default = 0 bis 30 Min und Max Feld als Double
    - Temperature Handle Size Radius Default = 30
    - Rounding Precision Default = 1 Dezimalstelle
    - Step Size Temperature Default = 0,5 Grad
    - Reset Settings (Button)
    - Save Settings (Button)

    Optional: 
    - Theme
    - Heater Type
     */
}