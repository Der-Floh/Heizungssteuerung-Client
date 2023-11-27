using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Heizungssteuerung_Client.Data;
using System;
using System.ComponentModel;
using System.Globalization;
using Color = Avalonia.Media.Color;
using Point = Avalonia.Point;

namespace Heizungssteuerung_Client.Views;

public partial class UserTempPickerView : UserControl
{
    public double XTemperatureStart { get; set; } = -10;
    public double XTemperatureEnd { get; set; } = 40;
    public double XTemperatureStepSize { get; set; } = 5f;
    public double YTemperatureStart { get; set; } = 90;
    public double YTemperatureEnd { get; set; } = 0;
    public double YTemperatureStepSize { get; set; } = 0.5f;
    public int DecimalPlaces { get; set; } = 2;

    public int MarginLines { get; set; } = 40;
    public int MarginBottom { get; set; } = 40;
    public int MarginTop { get; set; } = 20;
    public double SliderHeight { get; set; }
    public IBrush GridColor { get; set; } = new SolidColorBrush(Color.FromArgb(255, 148, 151, 156));
    public IBrush TempColor { get; set; } = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));

    public Temperature[] Temperatures { get; set; }
    private int currentTempIndex = -1;
    private bool IsTempMoving
    {
        get => _isTempMoving;
        set
        {
            _isTempMoving = value;
            if (value)
                Temperatures[currentTempIndex].HandleColor = Temperature.DefaultOnHandleColor;
            else
                Temperatures[currentTempIndex].HandleColor = Temperature.DefaultOffHandleColor;
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
            if (value)
                Cursor = new Cursor(StandardCursorType.Hand);
            else
                Cursor = new Cursor(StandardCursorType.Arrow);
        }
    }
    private bool _isTempHovering = false;

    public UserTempPickerView()
    {
        Temperatures = CalculateTemperatures();

        InitializeComponent();

        PointerPressed += CoordinateSystem_PointerPressed;
        PointerReleased += CoordinateSystem_PointerReleased;
        PointerMoved += CoordinateSystem_PointerMoved;

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
            currentTempIndex = index;
            IsTempMoving = true;
        }
    }

    private void CoordinateSystem_PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (currentTempIndex >= 0)
        {
            IsTempMoving = false;
            currentTempIndex = -1;
        }
    }

    private void CoordinateSystem_PointerMoved(object? sender, PointerEventArgs e)
    {
        if (IsTempMoving)
        {
            double y = e.GetCurrentPoint(this).Position.Y;
            Temperatures[currentTempIndex].YValue = PositionToValue(y, SliderHeight);
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

        Typeface typeface = new Typeface(FontFamily.Name, FontStyle.Normal, FontWeight.Bold);
        CultureInfo de = new CultureInfo("de-DE");

        for (int i = 0; i < Temperatures.Length; i++)
        {
            context.DrawLine(pen, new Point(Temperatures[i].X, MarginTop), new Point(Temperatures[i].X, Bounds.Height - MarginBottom));

            double temp = Math.Round(Temperatures[i].XValue, DecimalPlaces);
            string text = temp.ToString("0.0") + "°";

            FormattedText formattedText = new FormattedText(text, de, FlowDirection.LeftToRight, typeface, FontSize, GridColor);
            Geometry? tempNumberTextGeometry = formattedText.BuildGeometry(new Point(0, 0));

            double preWidth = tempNumberTextGeometry.Bounds.Width - tempNumberTextGeometry.Bounds.Width;
            double xOffset = (tempNumberTextGeometry.Bounds.Width / 2f) + preWidth;

            context.DrawText(formattedText, new Point(Temperatures[i].X - xOffset, Bounds.Height - tempNumberTextGeometry.Bounds.Height - MarginBottom / 2f));
        }
    }

    private void DrawHandles(DrawingContext context)
    {
        Pen pen = new Pen(GridColor)
        {
            LineCap = PenLineCap.Round,
            Thickness = 3
        };

        Typeface typeface = new Typeface(FontFamily.Name, FontStyle.Normal, FontWeight.Bold);
        CultureInfo de = new CultureInfo("de-DE");

        for (int i = 0; i < Temperatures.Length; i++)
        {
            context.DrawEllipse(Temperatures[i].HandleColor, pen, new Point(Temperatures[i].X, Temperatures[i].Y), Temperatures[i].Radius, Temperatures[i].Radius);

            double tempValue = Math.Round(Temperatures[i].YValue, 1);
            string temp = tempValue.ToString("0.0") + "°";

            FormattedText formattedTempValueText = new FormattedText(temp, de, FlowDirection.LeftToRight, typeface, FontSize, TempColor);
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
        Pen pen = new Pen(GridColor)
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

    private Temperature[] CalculateTemperatures()
    {
        double min = Math.Min(XTemperatureStart, XTemperatureEnd);
        double max = Math.Max(XTemperatureStart, XTemperatureEnd);
        Temperature[] temperatures = GetTemperatures(min, max, XTemperatureStepSize);
        return temperatures;
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