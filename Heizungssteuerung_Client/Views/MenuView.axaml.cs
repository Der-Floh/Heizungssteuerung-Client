using Avalonia.Controls;
using Avalonia.Input;
using Heizungssteuerung_Client.Data;
using Heizungssteuerung_SDK;
using Heizungssteuerung_SDK.Training;
using System;
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
    private bool _modelLoaded;
    private HeatingControlModel _model = new HeatingControlModel();

    public MenuView()
    {
        _settingsView = new SettingsView();
        _settingsView.PropertyChanged += _settingsView_PropertyChanged;
        _tempPredictorContainerView = new TempPredictorContainerView { ViewName = "Boiler Prediction", ViewIcon = "Assets/home.svg" };
        _tempPredictorContainerView.PredictButton_Click += _tempPredictorContainerView_PredictButton_Click;
        _userTempPickerContainerView = new UserTempPickerContainerView { ViewName = "Comfort Temperatures", ViewIcon = "Assets/home.svg" };
        _weatherInfoContainerView = new WeatherInfoContainerView { ViewName = "Weather Data", ViewIcon = "Assets/home.svg" };

        InitializeComponent();

        AddContent("Homepage", "Assets/home.svg", _tempPredictorContainerView);
        AddContent("Training", "Assets/device_thermostat.svg", _userTempPickerContainerView);
        AddContent("Weather", "Assets/partly_cloudy_day.svg", _weatherInfoContainerView);
        AddContent("Settings", "Assets/settings.svg", _settingsView);

        SplitviewListBox.SelectionChanged += SplitviewListBox_SelectionChanged;
        TriggerPaneButton.Tapped += TriggerPaneButton_Tapped;

        SplitviewListBox.SelectedIndex = 0;
    }

    private void _settingsView_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_settingsView.StepSizeTemperature):
                _userTempPickerContainerView.UserTempPickerAdvancedView.YTemperatureStepSize = (double)_settingsView.StepSizeTemperature;
                break;
            case nameof(_settingsView.MinOutsideTemperature):
                _userTempPickerContainerView.UserTempPickerAdvancedView.XTemperatureStart = (double)_settingsView.MinOutsideTemperature;
                break;
            case nameof(_settingsView.MaxOutsideTemperature):
                _userTempPickerContainerView.UserTempPickerAdvancedView.XTemperatureEnd = (double)_settingsView.MaxOutsideTemperature;
                break;
            case nameof(_settingsView.MinUserTemperature):
                _userTempPickerContainerView.UserTempPickerAdvancedView.YTemperatureEnd = (double)_settingsView.MinUserTemperature;
                break;
            case nameof(_settingsView.MaxUserTemperature):
                _userTempPickerContainerView.UserTempPickerAdvancedView.YTemperatureStart = (double)_settingsView.MaxUserTemperature;
                break;
            case nameof(_settingsView.TemperatureHandleSize):
                _userTempPickerContainerView.UserTempPickerAdvancedView.HandleSize = (double)_settingsView.TemperatureHandleSize;
                break;
            case nameof(_settingsView.DecimalPlaces):
                _userTempPickerContainerView.UserTempPickerAdvancedView.DecimalPlaces = (int)_settingsView.DecimalPlaces;
                break;
        }
    }

    private void _tempPredictorContainerView_PredictButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Predict();
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

    public void Predict()
    {
        try
        {
            if (!_modelLoaded)
            {
                _model.Load();
                _modelLoaded = true;
            }
        }
        catch
        {
            if (!_modelLoaded)
            {
                _tempPredictorContainerView.PredictButton.Text = "Error: Model couldn't be loaded.";
                return;
            }
        }
        for (int i = 0; i < _tempPredictorContainerView.UserTempPickerAdvancedView.Temperatures.Count; i++)
        {
            double weatherTemp = _weatherInfoContainerView.UserTempPickerAdvancedView.Temperatures[i].YValue;
            double userTemperature = FindNearestTemperature(weatherTemp, _userTempPickerContainerView.UserTempPickerAdvancedView.Temperatures.ToArray());
            TrainingDataInput input = new TrainingDataInput
            {
                ComfortTemperature = (float)userTemperature,
                AverageTemperatureOuterDay = (float)weatherTemp,
                IsolationClass = _settingsView.IsolationClass
            };
            _tempPredictorContainerView.UserTempPickerAdvancedView.Temperatures[i].YValue = _model.Predict(input);
        }
    }

    private double FindNearestTemperature(double targetTemperature, Temperature[] temperatureArray)
    {
        if (temperatureArray == null || temperatureArray.Length == 0)
            throw new ArgumentException("Temperature array cannot be null or empty.");

        double nearestTemperature = temperatureArray[0].XValue;
        double minDifference = Math.Abs(targetTemperature - nearestTemperature);
        double nearestTemp = temperatureArray[0].YValue;

        foreach (Temperature temperature in temperatureArray)
        {
            double difference = Math.Abs(targetTemperature - temperature.XValue);

            if (difference < minDifference)
            {
                minDifference = difference;
                nearestTemperature = temperature.XValue;
                nearestTemp = temperature.YValue;
            }
        }

        return nearestTemp;
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
    }
}