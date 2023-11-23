using Avalonia.Controls;
using Heizungssteuerung_Client.Data;
using System;

namespace Heizungssteuerung_Client.Views;

public partial class UserTempPickerContainerView : UserControl
{
    public UserTempPickerContainerView()
    {
        InitializeComponent();

        IsolcationClassComboBox.ItemsSource = Enum.GetNames(typeof(IsolationClasses));

        TrainButton.Click += TrainButton_Click;
    }

    private void TrainButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

    }
}