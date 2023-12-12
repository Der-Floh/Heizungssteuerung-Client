using Avalonia.Controls;
using Avalonia.Input;
using Heizungssteuerung_Client.Data;
using Heizungssteuerung_SDK;
using Heizungssteuerung_SDK.Training;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Heizungssteuerung_Client.Views;

public partial class MenuView : UserControl
{
    public List<MenuItemView> Contents { get; private set; } = new List<MenuItemView>();

    private int _prevIndex = -1;
    private SettingsView _settingsView;
    private TempPredictorContainerView _tempPredictorContainerView;
    private TempPredictorOutsideContainerView _tempPredictorOutsideContainerView;
    private UserTempPickerContainerView _userTempPickerContainerView;
    private WeatherInfoContainerView _weatherInfoContainerView;
    private bool _modelLoaded;
    private HeatingControlModel _model = new HeatingControlModel();
    private bool _settingsViewLoaded, _tempPredictorContainerViewLoaded, _tempPredictorOutsideContainerViewLoaded, _userTempPickerContainerViewLoaded, _weatherInfoContainerViewLoaded;

    public MenuView()
    {
        _settingsView = new SettingsView { ViewName = "Settings", ViewIcon = "Assets/settings.svg" };
        _settingsView.Loaded += _settingsView_Loaded;
        _settingsView.PropertyChanged += _settingsView_PropertyChanged;

        _tempPredictorContainerView = new TempPredictorContainerView { ViewName = "Boiler Prediction", ViewIcon = "Assets/home.svg" };
        _tempPredictorContainerView.Loaded += _tempPredictorContainerView_Loaded;
        _tempPredictorContainerView.PredictButton_Click += _tempPredictorContainerView_PredictButton_Click;

        _tempPredictorOutsideContainerView = new TempPredictorOutsideContainerView { ViewName = "Boiler Prediction Outside", ViewIcon = "Assets/device_thermostat.svg" };
        _tempPredictorOutsideContainerView.Loaded += _tempPredictorOutsideContainerView_Loaded;
        _tempPredictorOutsideContainerView.PredictButton_Click += _tempPredictorOutsideContainerView_PredictButton_Click;

        _userTempPickerContainerView = new UserTempPickerContainerView { ViewName = "Comfort Temperatures", ViewIcon = "Assets/thermometer_add.svg" };
        _userTempPickerContainerView.LoadingFinished += _userTempPickerContainerView_LoadingFinished;

        _weatherInfoContainerView = new WeatherInfoContainerView { ViewName = "Weather Data", ViewIcon = "Assets/partly_cloudy_day.svg" };
        _weatherInfoContainerView.LoadingFinished += _weatherInfoContainerView_LoadingFinished;

        InitializeComponent();

        AddContent("Homepage", _tempPredictorContainerView.ViewIcon, _tempPredictorContainerView);
        AddContent("Outside", _tempPredictorOutsideContainerView.ViewIcon, _tempPredictorOutsideContainerView);
        AddContent("Training", _userTempPickerContainerView.ViewIcon, _userTempPickerContainerView);
        AddContent("Weather", _weatherInfoContainerView.ViewIcon, _weatherInfoContainerView);
        AddContent("Settings", _settingsView.ViewIcon, _settingsView);

        SplitviewListBox.SelectionChanged += SplitviewListBox_SelectionChanged;
        TriggerPaneButton.Tapped += TriggerPaneButton_Tapped;

        SplitviewListBox.SelectedIndex = 0;
    }

    private void _tempPredictorOutsideContainerView_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e) => _tempPredictorOutsideContainerViewLoaded = true;
    private void _settingsView_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e) => _settingsViewLoaded = true;
    private void _tempPredictorContainerView_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e) => _tempPredictorContainerViewLoaded = true;
    private void _userTempPickerContainerView_LoadingFinished(object? sender, Avalonia.Interactivity.RoutedEventArgs e) => _userTempPickerContainerViewLoaded = true;
    private void _weatherInfoContainerView_LoadingFinished(object? sender, Avalonia.Interactivity.RoutedEventArgs e) => _weatherInfoContainerViewLoaded = true;

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
                _tempPredictorContainerView.UserTempPickerAdvancedView.HandleSize = (double)_settingsView.TemperatureHandleSize;
                _tempPredictorOutsideContainerView.UserTempPickerAdvancedView.HandleSize = (double)_settingsView.TemperatureHandleSize;
                _userTempPickerContainerView.UserTempPickerAdvancedView.HandleSize = (double)_settingsView.TemperatureHandleSize;
                _weatherInfoContainerView.UserTempPickerAdvancedView.HandleSize = (double)_settingsView.TemperatureHandleSize;
                break;
            case nameof(_settingsView.PredictTemperatureStepSize):
                _tempPredictorOutsideContainerView.UserTempPickerAdvancedView.XTemperatureStepSize = (double)_settingsView.PredictTemperatureStepSize;
                break;
        }
    }

    private void _tempPredictorOutsideContainerView_PredictButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _ = PredictTempView();
    }

    private void _tempPredictorContainerView_PredictButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _ = PredictTimeView();
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

    public async Task PredictTimeView()
    {
        while (!_settingsViewLoaded || !_tempPredictorContainerViewLoaded || !_userTempPickerContainerViewLoaded || !_weatherInfoContainerViewLoaded)
            await Task.Delay(50);

        if (!TryLoadModel())
            return;

        for (int i = 0; i < _tempPredictorContainerView.UserTempPickerAdvancedView.Temperatures.Count; i++)
        {
            double weatherTemp = _weatherInfoContainerView.UserTempPickerAdvancedView.Temperatures[i].YValue;
            double userTemperature = _userTempPickerContainerView.UserTempPickerAdvancedView.Temperatures.FindNearestTemperature(weatherTemp);
            TrainingDataInput input = new TrainingDataInput
            {
                ComfortTemperature = (float)userTemperature,
                AverageTemperatureOuterDay = (float)weatherTemp,
                IsolationClass = _settingsView.IsolationClass
            };
            _tempPredictorContainerView.UserTempPickerAdvancedView.Temperatures[i].YValue = _model.Predict(input);
        }
    }

    public async Task PredictTempView()
    {
        while (!_settingsViewLoaded || !_tempPredictorOutsideContainerViewLoaded || !_userTempPickerContainerViewLoaded || !_weatherInfoContainerViewLoaded)
            await Task.Delay(50);

        if (!TryLoadModel())
            return;

        for (int i = 0; i < _tempPredictorOutsideContainerView.UserTempPickerAdvancedView.Temperatures.Count; i++)
        {
            double weatherTemp = _tempPredictorOutsideContainerView.UserTempPickerAdvancedView.Temperatures[i].XValue;
            double userTemperature = _userTempPickerContainerView.UserTempPickerAdvancedView.Temperatures.FindNearestTemperature(weatherTemp);
            TrainingDataInput input = new TrainingDataInput
            {
                ComfortTemperature = (float)userTemperature,
                AverageTemperatureOuterDay = (float)weatherTemp,
                IsolationClass = _settingsView.IsolationClass
            };
            _tempPredictorOutsideContainerView.UserTempPickerAdvancedView.Temperatures[i].YValue = _model.Predict(input);
        }
    }

    public bool TryLoadModel()
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
                return false;
            }
        }
        return true;
    }

    private void TriggerPaneButton_Tapped(object? sender, TappedEventArgs e)
    {
        SplitViewCom.IsPaneOpen = !SplitViewCom.IsPaneOpen;
        TitleText.Text = SplitViewCom.IsPaneOpen ? "Heating Control" : null;
    }

    private void SplitviewListBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        ContentPanel.Children[SplitviewListBox.SelectedIndex].IsVisible = true;
        if (_prevIndex != -1)
            ContentPanel.Children[_prevIndex].IsVisible = false;
        _prevIndex = SplitviewListBox.SelectedIndex;

        if (ContentPanel.Children[SplitviewListBox.SelectedIndex] is TempPredictorContainerView)
            _ = PredictTimeView();

        if (ContentPanel.Children[SplitviewListBox.SelectedIndex] is TempPredictorOutsideContainerView)
            _ = PredictTempView();
    }
}