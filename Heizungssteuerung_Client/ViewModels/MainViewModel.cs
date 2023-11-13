using ReactiveUI;
using System;
using System.Collections.ObjectModel;

namespace Heizungssteuerung_Client.ViewModels;

public class MainViewModel : ViewModelBase
{
    private bool _isSplitViewPaneOpen = false;
    public bool IsSplitViewPaneOpen
    {
        get => _isSplitViewPaneOpen;
        set => this.RaiseAndSetIfChanged(ref _isSplitViewPaneOpen, value);
    }

    private ViewModelBase _currentPage = new HomePageViewModel();
    public ViewModelBase CurrentPage
    {
        get => _currentPage;
        set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }

    private ListItemTemplate _selectedListItem;
    public ListItemTemplate SelectedListItem
    {
        get => _selectedListItem;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedListItem, value);
            CurrentPage = new SettingsPageViewModel();

            // Optional: Aktualisieren Sie den ContentControl direkt, wenn gewünscht
            //SelectedListItem = CurrentPage;
        }
    }

    public ObservableCollection<ListItemTemplate> Items { get; } = new()
    {
        new ListItemTemplate(typeof(HomePageViewModel)),
        new ListItemTemplate(typeof(SettingsPageView)),
    };

    public void TriggerPane()
    {
        IsSplitViewPaneOpen = !IsSplitViewPaneOpen;
    }
}

public class ListItemTemplate
{
    public ListItemTemplate(Type type)
    {
        ModelType = type;
        Label = type.Name.Replace("PageViewModel", "");
    }
    public string Label { get; set; }
    public Type ModelType { get; set; }
}
