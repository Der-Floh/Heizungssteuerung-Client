using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Heizungssteuerung_Client.Data;
using System;
using System.ComponentModel;
using System.Globalization;
using Point = Avalonia.Point;

namespace Heizungssteuerung_Client.Views;

public partial class UserTempPickerView : UserControl
{
    public bool SyncSettings { get => _syncSettings; set { _syncSettings = value; if (value) InitTemperatureEvents(); else InitTemperatures(); } }
    private bool _syncSettings;
    public bool SyncWeatherSettings { get; set; }
    public double XTemperatureStart { get; set; } = (double)SettingsView.MinOutsideTemperature;
    public double XTemperatureEnd { get; set; } = (double)SettingsView.MaxOutsideTemperature;
    public double XTemperatureStepSize { get; set; } = (double)SettingsView.OutsideTemperatureStepSize;
    public double YTemperatureStart { get; set; } = (double)SettingsView.MaxUserTemperature;
    public double YTemperatureEnd { get; set; } = (double)SettingsView.MinUserTemperature;
    public double YTemperatureStepSize { get; set; } = (double)SettingsView.StepSizeTemperature;
    public string? XAxisText { get; set; }
    public string? YAxisText { get; set; }
    public int DecimalPlaces { get; set; } = (int)SettingsView.RoundingPrecision;
    public bool Editable
    {
        get => _editable;
        set
        {
            if (_editable == value)
                return;
            _editable = value;
            if (value)
            {
                PointerPressed += CoordinateSystem_PointerPressed;
                PointerReleased += CoordinateSystem_PointerReleased;
                PointerMoved += CoordinateSystem_PointerMoved;
            }
            else
            {
                PointerPressed -= CoordinateSystem_PointerPressed;
                PointerReleased -= CoordinateSystem_PointerReleased;
                PointerMoved -= CoordinateSystem_PointerMoved;
            }
        }
    }
    private bool _editable;

    public int MarginLines { get; set; } = 70;
    public int MarginBottom { get; set; } = 40;
    public int MarginTop { get; set; } = 20;
    public double SliderHeight { get; set; }
    public IBrush GridColor { get; set; } = new SolidColorBrush(ColorSettings.GridColor);
    public IBrush TempColor { get; set; } = new SolidColorBrush(ColorSettings.TextColor);
    public IBrush LineColor { get; set; } = new SolidColorBrush(ColorSettings.TempLineColor);
    public string XValuesStringAppend { get; set; } = "°";
    public string YValuesStringAppend { get; set; } = "°";

    public Temperature[] Temperatures
    {
        get
        {
            if (SyncSettings)
                return SettingsView.UserTemperatures;
            else if (SyncWeatherSettings)
                return SettingsView.WeatherTemperatures;
            else
                return _temperatures;
        }
        set
        {
            if (SyncSettings)
                SettingsView.UserTemperatures = value;
            else if (SyncWeatherSettings)
                SettingsView.WeatherTemperatures = value;
            else
                _temperatures = value;
        }
    }
    private Temperature[]? _temperatures;
    public int WaveUpdateSpeed { get; set; } = 20;
    private int _currentTempIndex = -1;
    private bool IsTempMoving
    {
        get => _isTempMoving;
        set
        {
            _isTempMoving = value;
            Temperatures[_currentTempIndex].HandleColor = value ? Temperature.DefaultOnHandleColor : Temperature.DefaultOffHandleColor;
            InvalidateVisual();
        }
    }
    private bool _isTempMoving = false;
    private bool IsTempHovering
    {
        get => _isTempHovering;
        set
        {
            _isTempHovering = value;
            Cursor = value ? new Cursor(StandardCursorType.Hand) : new Cursor(StandardCursorType.Arrow);
        }
    }
    private bool _isTempHovering = false;

    private Typeface _typeface;
    private CultureInfo _de = new CultureInfo("de-DE");

    public UserTempPickerView()
    {
        InitializeComponent();

        _typeface = new Typeface(FontFamily.Name, FontStyle.Normal, FontWeight.Bold);

        if (SyncSettings)
            InitTemperatureEvents();
        else
            InitTemperatures();

        SizeChanged += CoordinateSystem_SizeChanged;

        Loaded += CoordinateSystem_Loaded;
    }

    private void CoordinateSystem_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        SliderHeight = Bounds.Height - MarginBottom - MarginTop;
        InitializeTemperaturePositions(Bounds.Width - MarginLines * 2, Bounds.Height);
    }

    private void CoordinateSystem_SizeChanged(object? sender, SizeChangedEventArgs e)
    {
        SliderHeight = Bounds.Height - MarginBottom - MarginTop;
        UpdateTemperaturePositions(Bounds.Width - MarginLines * 2, SliderHeight);
    }

    public override void Render(DrawingContext context)
    {
        DrawLines(context);
        DrawConnectionLines(context);
        DrawHandles(context);
        DrawText(context);

        base.Render(context);
    }

    private void CoordinateSystem_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        PointerPoint point = e.GetCurrentPoint(this);
        double x = point.Position.X;
        double y = point.Position.Y;

        int index = GetNearestTemperatureIndex(x, y);
        if (index != -1 && IsPointInsideCircle(x, y, Temperatures[index].X, Temperatures[index].Y, Temperatures[index].Radius))
        {
            _currentTempIndex = index;
            IsTempMoving = true;
        }
    }

    private void CoordinateSystem_PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (_currentTempIndex >= 0)
        {
            IsTempMoving = false;
            _currentTempIndex = -1;
        }
    }

    private void CoordinateSystem_PointerMoved(object? sender, PointerEventArgs e)
    {
        if (IsTempMoving)
        {
            double y = e.GetCurrentPoint(this).Position.Y;
            Temperatures[_currentTempIndex].YValue = PositionToValue(y, SliderHeight);
            //double posTop = MarginTop + temperatures[currentTempIndex].Radius;
            //double posBottom = Bounds.Height - MarginBottom - temperatures[currentTempIndex].Radius;
            //bool isTopOk = y >= posTop;
            //bool isBottomOk = y <= posBottom;
            //if (isTopOk && isBottomOk)
            //    temperatures[currentTempIndex].YValue = PositionToValue(y, SliderHeight);
            //else if (isTopOk)
            //    temperatures[currentTempIndex].YValue = PositionToValue(posBottom, SliderHeight);
            //else if (isBottomOk)
            //    temperatures[currentTempIndex].YValue = PositionToValue(posTop, SliderHeight);
        }
        else
        {
            PointerPoint point = e.GetCurrentPoint(this);
            double x = point.Position.X;
            double y = point.Position.Y;
            int index = GetNearestTemperatureIndex(x, y);
            if (index != -1)
                IsTempHovering = IsPointInsideCircle(x, y, Temperatures[index].X, Temperatures[index].Y, Temperatures[index].Radius);
        }
    }

    private void DrawLines(DrawingContext context)
    {
        Pen pen = new Pen(GridColor)
        {
            LineCap = PenLineCap.Round,
            Thickness = 3
        };

        for (int i = 0; i < Temperatures.Length; i++)
        {
            context.DrawLine(pen, new Point(Temperatures[i].X, MarginTop), new Point(Temperatures[i].X, Bounds.Height - MarginBottom));

            double temp = Math.Round(Temperatures[i].XValue, DecimalPlaces);
            string text = temp.ToString("0.0") + XValuesStringAppend;

            FormattedText formattedText = new FormattedText(text, _de, FlowDirection.LeftToRight, _typeface, FontSize, GridColor);
            Geometry? tempNumberTextGeometry = formattedText.BuildGeometry(new Point(0, 0));

            double preWidth = tempNumberTextGeometry.Bounds.Width - tempNumberTextGeometry.Bounds.Width;
            double xOffset = (tempNumberTextGeometry.Bounds.Width / 2f) + preWidth;

            context.DrawText(formattedText, new Point(Temperatures[i].X - xOffset, Bounds.Height - tempNumberTextGeometry.Bounds.Height - MarginBottom / 2f));
        }
    }

    private void DrawHandles(DrawingContext context)
    {
        Pen pen = new Pen(LineColor)
        {
            LineCap = PenLineCap.Round,
            Thickness = 3
        };

        for (int i = 0; i < Temperatures.Length; i++)
        {
            context.DrawEllipse(Temperatures[i].HandleColor, pen, new Point(Temperatures[i].X, Temperatures[i].Y), Temperatures[i].Radius, Temperatures[i].Radius);

            double tempValue = Math.Round(Temperatures[i].YValue, 1);
            string temp = tempValue.ToString("0.0") + YValuesStringAppend;

            FormattedText formattedTempValueText = new FormattedText(temp, _de, FlowDirection.LeftToRight, _typeface, FontSize, TempColor);
            Geometry? tempNumberTextGeometry = formattedTempValueText.BuildGeometry(new Point(0, 0));

            double preWidth = tempNumberTextGeometry.Bounds.Width - tempNumberTextGeometry.Bounds.Width;
            double preHeight = tempNumberTextGeometry.Bounds.Height - tempNumberTextGeometry.Bounds.Height;
            double xOffset = (tempNumberTextGeometry.Bounds.Width / 2f) + preWidth;
            double yOffset = (tempNumberTextGeometry.Bounds.Height / 1.5f) + preHeight;
            context.DrawText(formattedTempValueText, new Point(Temperatures[i].X - xOffset, Temperatures[i].Y - yOffset));
        }
    }

    private void DrawConnectionLines(DrawingContext context)
    {
        Pen pen = new Pen(LineColor)
        {
            LineCap = PenLineCap.Round,
            Thickness = 3
        };

        for (int i = 0; i < Temperatures.Length; i++)
        {
            if (i < Temperatures.Length - 1)
            {
                context.DrawLine(pen, new Point(Temperatures[i].X, Temperatures[i].Y), new Point(Temperatures[i + 1].X, Temperatures[i + 1].Y));
            }
        }
    }

    private void DrawText(DrawingContext context)
    {
        if (!string.IsNullOrEmpty(XAxisText))
        {
            FormattedText formattedXText = new FormattedText(XAxisText, _de, FlowDirection.LeftToRight, _typeface, FontSize, GridColor);
            Geometry? tempXTextGeometry = formattedXText.BuildGeometry(new Point(0, 0));

            double preWidth = tempXTextGeometry.Bounds.Width - tempXTextGeometry.Bounds.Width;
            double preHeight = tempXTextGeometry.Bounds.Height - tempXTextGeometry.Bounds.Height;
            double xOffset = (tempXTextGeometry.Bounds.Width / 2f) + preWidth;
            double yOffset = (tempXTextGeometry.Bounds.Height / 2f) + preHeight;
            context.DrawText(formattedXText, new Point(Bounds.Width / 2 - xOffset, Bounds.Height - yOffset * 3));
        }

        if (!string.IsNullOrEmpty(YAxisText))
        {
            Pen pen = new Pen(GridColor)
            {
                Thickness = 0.1
            };

            FormattedText formattedYText = new FormattedText(YAxisText, _de, FlowDirection.LeftToRight, _typeface, FontSize, GridColor);
            Geometry? tempYTextGeometry = formattedYText.BuildGeometry(new Point(0, 0));
            double xOffset = tempYTextGeometry.Bounds.Width / 2.0;
            double yOffset = tempYTextGeometry.Bounds.Height / 2.0;

            Matrix rotation = Matrix.CreateRotation(-90 * (Math.PI / 180));
            Matrix translation = Matrix.CreateTranslation(yOffset, Bounds.Height / 2 + xOffset);
            Geometry? geometry = formattedYText.BuildGeometry(new Point(0, 0));
            geometry.Transform = new MatrixTransform(rotation * translation);

            context.DrawGeometry(pen.Brush, pen, geometry);
        }
    }

    private void TemperaturePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is null)
            return;

        Temperature temperature = (Temperature)sender;
        switch (e.PropertyName)
        {
            case nameof(temperature.Y):
                InvalidateVisual();
                break;
            case nameof(temperature.YValue):
                double pos = ValueToPosition(temperature.YValue, SliderHeight);
                temperature.Y = pos;
                //double posTop = MarginTop + temperature.Radius;
                //double posBottom = Bounds.Height - MarginBottom - temperature.Radius;
                //bool isTopOk = pos >= posTop;
                //bool isBottomOk = pos <= posBottom;
                //if (isTopOk && isBottomOk)
                //    temperature.Y = pos;
                //else if (isTopOk)
                //    temperature.Y = posBottom;
                //else if (isBottomOk)
                //    temperature.Y = posTop;
                break;
        }
    }

    private Temperature[] GetTemperatures(double minTemperature, double maxTemperature, double stepSize)
    {
        if (stepSize <= 0 || stepSize >= (maxTemperature - minTemperature))
            throw new ArgumentException("Invalid stepSize value.");

        int numTemperatures = (int)((maxTemperature - minTemperature) / stepSize) + 1;
        Temperature[] temperatures = new Temperature[numTemperatures];

        for (int i = 0; i < numTemperatures; i++)
        {
            Temperature temperature = new Temperature { XValue = minTemperature + i * stepSize };
            temperature.PropertyChanged += TemperaturePropertyChanged;
            temperatures[i] = temperature;
        }

        return temperatures;
    }

    public void InitTemperatures()
    {
        Temperatures = CalculateTemperatures();
    }

    private Temperature[] CalculateTemperatures()
    {
        double min = Math.Min(XTemperatureStart, XTemperatureEnd);
        double max = Math.Max(XTemperatureStart, XTemperatureEnd);
        Temperature[] temperatures = GetTemperatures(min, max, XTemperatureStepSize);
        return temperatures;
    }

    private void InitTemperatureEvents()
    {
        foreach (Temperature temperature in Temperatures)
        {
            temperature.PropertyChanged += TemperaturePropertyChanged;
        };
    }

    private double PositionToValue(double sliderPosition, double height)
    {
        double valueRange = YTemperatureEnd - YTemperatureStart;
        double positionRatio = sliderPosition / height;
        double sliderValue = YTemperatureStart + positionRatio * valueRange;

        sliderValue = RoundToStepSize(sliderValue, YTemperatureStepSize);

        double max = Math.Max(YTemperatureStart, YTemperatureEnd);
        double min = Math.Min(YTemperatureStart, YTemperatureEnd);
        sliderValue = Math.Clamp(sliderValue, min, max);

        return sliderValue;
    }

    private double ValueToPosition(double value, double height)
    {
        value = RoundToStepSize(value, YTemperatureStepSize);

        double max = Math.Max(YTemperatureStart, YTemperatureEnd);
        double min = Math.Min(YTemperatureStart, YTemperatureEnd);
        value = Math.Clamp(value, min, max);

        double normalizedPosition = (value - YTemperatureStart) / (YTemperatureEnd - YTemperatureStart);
        double screenPosition = normalizedPosition * height;

        return screenPosition;
    }

    private double RoundToStepSize(double value, double stepSize)
    {
        double roundedValue = Math.Round(value / stepSize) * stepSize;
        double halfStep = stepSize / 2.0;

        if (value - roundedValue >= halfStep)
            roundedValue += stepSize;

        return roundedValue;
    }

    private int GetNearestTemperatureIndex(double x, double y)
    {
        int nearestTemperatureIndex = -1;
        double minDistance = double.MaxValue;

        for (int i = 0; i < Temperatures.Length; i++)
        {
            double distance = Math.Sqrt(Math.Pow(x - Temperatures[i].X, 2) + Math.Pow(y - Temperatures[i].Y, 2));
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestTemperatureIndex = i;
            }
        }
        return nearestTemperatureIndex;
    }

    private void InitializeTemperaturePositions(double w, double h)
    {
        double lineSpacing = w / (Temperatures.Length - 1);
        for (int i = 0; i < Temperatures.Length; i++)
        {
            Temperatures[i].X = MarginLines + i * lineSpacing;
            Temperatures[i].YValue = PositionToValue(h / 2, h);
        }
    }

    private void UpdateTemperaturePositions(double w, double h)
    {
        double lineSpacing = w / (Temperatures.Length - 1);
        for (int i = 0; i < Temperatures.Length; i++)
        {
            Temperatures[i].X = MarginLines + i * lineSpacing;
            double newPos = ValueToPosition(Temperatures[i].YValue, h);
            if (Temperatures[i].Y != newPos)
                Temperatures[i].Y = newPos;
        }
    }

    private bool IsPointInsideCircle(double x, double y, double centerX, double centerY, double radius)
    {
        double distance = Math.Sqrt(Math.Pow(x - centerX, 2) + Math.Pow(y - centerY, 2));
        return distance <= radius;
    }
}