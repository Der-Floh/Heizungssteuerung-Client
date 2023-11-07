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

    public ObservableCollection<ListItemTemplate> Items { get; } = new()
    {
        new ListItemTemplate(typeof(HomePageViewModel)),
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
