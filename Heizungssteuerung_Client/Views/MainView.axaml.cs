using Avalonia.Controls;

namespace Heizungssteuerung_Client.Views;

public partial class MainView : UserControl
{
    //public bool IsSplitViewPaneOpen
    //{
    //    get => _isSplitViewPaneOpen;
    //    set
    //    {
    //        _isSplitViewPaneOpen = value;
    //        SplitViewCom.IsPaneOpen = value;
    //        if (value)
    //            TriggerPaneButton.Content = "-";
    //        else
    //            TriggerPaneButton.Content = "+";
    //    }
    //}
    //private bool _isSplitViewPaneOpen = false;
    //public ICommand TriggerPaneCommand { get; set; }
    public MainView()
    {
        InitializeComponent();
        //TriggerPaneButton.Click += TriggerPaneButton_Click;
    }
    //private void TriggerPaneButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    //{
    //    IsSplitViewPaneOpen = !IsSplitViewPaneOpen;
    //}
}