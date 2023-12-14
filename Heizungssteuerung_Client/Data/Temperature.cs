using Avalonia;
using Avalonia.Media;
using System;
using System.ComponentModel;
using System.Globalization;

namespace Heizungssteuerung_Client.Data;
public class Temperature : INotifyPropertyChanged
{
    public string FontFamily { get => _fontFamily; set { if (_fontFamily == value) return; _fontFamily = value; _typeface = new Typeface(FontFamily, FontStyle.Normal, FontWeight.Bold); OnPropertyChanged(nameof(FontFamily)); } }
    private string _fontFamily;
    public double FontSize { get => _fontSize; set { if (_fontSize == value) return; _fontSize = value; OnPropertyChanged(nameof(FontSize)); } }
    private double _fontSize;
    public double LineThickness { get => _lineThickness; set { if (_lineThickness == value) return; _lineThickness = value; _HandleColorPen = GetPen(HandleColor); _LineColorPen = GetPen(LineColor); _TextColorPen = GetPen(TextColor); OnPropertyChanged(nameof(LineThickness)); } }
    private double _lineThickness = 3;
    public double X { get => _x; set { if (_x == value) return; _x = value; OnPropertyChanged(nameof(X)); } }
    private double _x = -1;
    public double Y { get => _y; set { if (_y == value) return; _y = value; OnPropertyChanged(nameof(Y)); } }
    private double _y = -1;
    public double Radius { get => _radius; set { if (_radius == value) return; _radius = value; OnPropertyChanged(nameof(Radius)); } }
    private double _radius;
    public double XValue { get => _xValue; set { if (_xValue == value) return; _xValue = value; OnPropertyChanged(nameof(XValue)); } }
    private double _xValue;
    public double YValue { get => _yValue; set { if (_yValue == value) return; _yValue = value; OnPropertyChanged(nameof(YValue)); } }
    private double _yValue;
    public IBrush HandleColor { get => _handleColor; set { if (_handleColor == value) return; _handleColor = value; _HandleColorPen = GetPen(value); OnPropertyChanged(nameof(HandleColor)); } }
    private IBrush _handleColor;
    public IBrush LineColor { get => _lineColor; set { if (_lineColor == value) return; _lineColor = value; _LineColorPen = GetPen(value); OnPropertyChanged(nameof(LineColor)); } }
    private IBrush _lineColor;
    public IBrush TextColor { get => _textColor; set { if (_textColor == value) return; _textColor = value; _TextColorPen = GetPen(value); OnPropertyChanged(nameof(TextColor)); } }
    private IBrush _textColor;

    public event PropertyChangedEventHandler? PropertyChanged;

    private Typeface _typeface;
    private Pen _HandleColorPen;
    private Pen _LineColorPen;
    private Pen _TextColorPen;

    public Temperature(string fontFamily, double fontSize)
    {
        FontFamily = fontFamily;
        FontSize = fontSize;
        _radius = OS.IsMobile() ? 24 : 30;

        HandleColor = new SolidColorBrush(ColorSettings.OffHandleColor);
        LineColor = new SolidColorBrush(ColorSettings.TempLineColor);
        TextColor = new SolidColorBrush(ColorSettings.TextColor);
    }

    public void Draw(DrawingContext context, string yValuesStringAppend)
    {
        context.DrawEllipse(HandleColor, _LineColorPen, new Point(X, Y), Radius, Radius);

        double tempValue = Math.Round(YValue, 1);
        string temp = tempValue.ToString("0.0") + yValuesStringAppend;

        FormattedText formattedTempValueText = new FormattedText(temp, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, _typeface, FontSize, TextColor);
        Geometry? tempNumberTextGeometry = formattedTempValueText.BuildGeometry(new Point(0, 0));

        double xOffset = tempNumberTextGeometry.Bounds.Width / 2f;
        double yOffset = tempNumberTextGeometry.Bounds.Height / 1.5f;
        context.DrawText(formattedTempValueText, new Point(X - xOffset, Y - yOffset));
    }

    public void DrawConnection(DrawingContext context, Temperature toTemperature)
    {
        context.DrawLine(_LineColorPen, new Point(X, Y), new Point(toTemperature.X, toTemperature.Y));
    }

    private Pen GetPen(IBrush color)
    {
        return new Pen(color)
        {
            LineCap = PenLineCap.Round,
            Thickness = LineThickness,
        };
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
