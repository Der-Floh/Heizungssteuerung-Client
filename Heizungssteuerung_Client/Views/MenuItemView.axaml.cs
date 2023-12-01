using Avalonia.Controls;
using System.ComponentModel;

namespace Heizungssteuerung_Client.Views;

public partial class MenuItemView : UserControl, INotifyPropertyChanged
{
    public string Icon { get => _icon; set { if (_icon == value) return; _icon = value; MenuItemIcon.Source = value; OnPropertyChanged(nameof(Icon)); } }
    private string _icon;

    public string Text { get => _text; set { if (_text == value) return; _text = value; MenuItemTextBlock.Text = value; OnPropertyChanged(nameof(Text)); } }
    private string _text;

    public event PropertyChangedEventHandler? PropertyChanged;

    public MenuItemView()
    {
        InitializeComponent();
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}