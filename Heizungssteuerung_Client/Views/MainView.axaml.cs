using Avalonia.Controls;
using Avalonia.Input;
using System.Collections.Generic;

namespace Heizungssteuerung_Client.Views;

public partial class MainView : UserControl
{
    public List<MenuItemView> Contents { get; private set; } = new List<MenuItemView>();

    public MainView()
    {
        InitializeComponent();
        AddContent("Hompage", "Assets/home.svg");
        AddContent("Settings", "Assets/settings.svg");
        AddContent("Training", "Assets/thermometer.svg");


        SplitviewListBox.SelectionChanged += SplitviewListBox_SelectionChanged;
        TriggerPaneButton.Tapped += TriggerPaneButton_Tapped;
    }

    public void AddContent(string content, string icon)
    {
        MenuItemView menuItemView = new MenuItemView
        {
            Text = content,
            Icon = icon,
        };
        Contents.Add(menuItemView);
        SplitviewListBox.Items.Add(menuItemView);
    }

    private void TriggerPaneButton_Tapped(object? sender, TappedEventArgs e)
    {
        SplitViewCom.IsPaneOpen = !SplitViewCom.IsPaneOpen;
        if (SplitViewCom.IsPaneOpen)
        {
            TitleText.Text = "KI-gesteuerte Heizungsanlage";
        }
        else
        {
            TitleText.Text = "";
        }

    }

    private void SplitviewListBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        string selectedContent = ((MenuItemView)SplitviewListBox.SelectedItem).Text;

        switch (selectedContent)
        {
            case "Homepage":
                // Öffne die HomePage
                //NavigationService.NavigateTo(typeof(HomePage));
                break;
            case "Settings":
                // Öffne die SettingsPage
                // Beispiel: NavigationService.NavigateTo(typeof(SettingsPage));
                break;
            case "Training":
                // Öffne die Training Page
                // Beispiel: NavigationService.NavigateTo(typeof(Training));
                break;
            default:
                // Standardverhalten, falls kein passendes ListItem ausgewählt wurde
                break;
        }
    }
}