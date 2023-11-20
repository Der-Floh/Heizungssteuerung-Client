using Avalonia.Media;
using System.ComponentModel;

namespace Heizungssteuerung_Client.Data;
public class Temperature : INotifyPropertyChanged
{
    public double X { get => _x; set { if (value != _x) { _x = value; OnPropertyChanged(nameof(X)); } } }
    private double _x = -1;
    public double Y { get => _y; set { if (value != _y) { _y = value; OnPropertyChanged(nameof(Y)); } } }
    private double _y = -1;
    public double Radius { get => _radius; set { if (value != _radius) { _radius = value; OnPropertyChanged(nameof(Radius)); } } }
    private double _radius = 30;
    public double XValue { get => _xValue; set { if (value != _xValue) { _xValue = value; OnPropertyChanged(nameof(XValue)); } } }
    private double _xValue;
    public double YValue { get => _yValue; set { if (value != _yValue) { _yValue = value; OnPropertyChanged(nameof(YValue)); } } }
    private double _yValue;
    public IBrush HandleColor { get; set; } = DefaultOffHandleColor;
    public static IBrush DefaultOnHandleColor { get; } = new SolidColorBrush(Color.FromArgb(255, 85, 177, 85));
    public static IBrush DefaultOffHandleColor { get; } = new SolidColorBrush(Color.FromArgb(255, 244, 74, 85));


    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
