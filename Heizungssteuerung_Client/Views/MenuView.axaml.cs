using Avalonia.Controls;
using Avalonia.Input;
using System.Collections.Generic;

namespace Heizungssteuerung_Client.Views;

public partial class MenuView : UserControl
{
    public List<MenuItemView> Contents { get; private set; } = new List<MenuItemView>();

    private int _prevIndex = -1;
    private SettingsView _settingsView;
    private TempPredictorContainerView _tempPredictorContainerView;
    private UserTempPickerContainerView _userTempPickerContainerView;
    private WeatherInfoContainerView _weatherInfoContainerView;

    public MenuView()
    {
        _settingsView = new SettingsView();
        _tempPredictorContainerView = new TempPredictorContainerView { ViewName = "Cauldron Prediction", ViewIcon = "Assets/home.svg" };
        _userTempPickerContainerView = new UserTempPickerContainerView { ViewName = "Comfort Temperatures", ViewIcon = "Assets/home.svg" };
        _weatherInfoContainerView = new WeatherInfoContainerView { ViewName = "Weather Data", ViewIcon = "Assets/home.svg" };
        _weatherInfoContainerView.UpdateWeatherData();

        InitializeComponent();

        AddContent("Homepage", "Assets/home.svg", _tempPredictorContainerView);
        AddContent("Training", "Assets/device_thermostat.svg", _userTempPickerContainerView);
        AddContent("Weather", "Assets/partly_cloudy_day.svg", _weatherInfoContainerView);
        AddContent("Settings", "Assets/settings.svg", _settingsView);

        SplitviewListBox.SelectionChanged += SplitviewListBox_SelectionChanged;
        TriggerPaneButton.Tapped += TriggerPaneButton_Tapped;

        SplitviewListBox.SelectedIndex = 0;
    }

    public void AddContent(string content, string icon, UserControl control)
    {
        MenuItemView menuItemView = new MenuItemView
        {
            Text = content,
            Icon = icon,
        };
        Contents.Add(menuItemView);
        SplitviewListBox.Items.Add(menuItemView);
        control.IsVisible = false;
        ContentPanel.Children.Add(control);
    }

    private void TriggerPaneButton_Tapped(object? sender, TappedEventArgs e)
    {
        SplitViewCom.IsPaneOpen = !SplitViewCom.IsPaneOpen;
        TitleText.Text = SplitViewCom.IsPaneOpen ? "KI-gesteuerte Heizungsanlage" : null;
    }

    private void SplitviewListBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        ContentPanel.Children[SplitviewListBox.SelectedIndex].IsVisible = true;
        if (_prevIndex != -1)
            ContentPanel.Children[_prevIndex].IsVisible = false;
        _prevIndex = SplitviewListBox.SelectedIndex;

        if (ContentPanel.Children[_prevIndex] is WeatherInfoContainerView)
            _weatherInfoContainerView.UpdateWeatherData();
    }
}