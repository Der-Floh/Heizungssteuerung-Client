using Avalonia.Controls;

namespace Heizungssteuerung_Client.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        SettingsView.InitUserTemps();

        InitializeComponent();
    }
}