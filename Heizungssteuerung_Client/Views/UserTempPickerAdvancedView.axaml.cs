using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Heizungssteuerung_Client.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Heizungssteuerung_Client.Views;

public partial class UserTempPickerAdvancedView : UserControl, INotifyPropertyChanged
{
    #region Property Settings
    public double XTemperatureStart { get => _xTemperatureStart; set { if (_xTemperatureStart == value) return; _xTemperatureStart = value; UpdateXTemperatureMinMax(value, XTemperatureEnd, XTemperatureStepSize); OnPropertyChanged(nameof(XTemperatureStart)); } }
    private double _xTemperatureStart;
    public double XTemperatureEnd { get => _xTemperatureEnd; set { if (_xTemperatureEnd == value) return; _xTemperatureEnd = value; UpdateXTemperatureMinMax(XTemperatureStart, value, XTemperatureStepSize); OnPropertyChanged(nameof(XTemperatureEnd)); } }
    private double _xTemperatureEnd;
    public double XTemperatureStepSize { get => _xTemperatureStepSize; set { if (_xTemperatureStepSize == value) return; _xTemperatureStepSize = value; UpdateXTemperatureMinMax(XTemperatureStart, XTemperatureEnd, value); OnPropertyChanged(nameof(XTemperatureStepSize)); } }
    private double _xTemperatureStepSize;
    public double YTemperatureStart { get => _yTemperatureStart; set { if (_yTemperatureStart == value) return; _yTemperatureStart = value; UpdateYTemperatureMinMax(value, YTemperatureEnd); OnPropertyChanged(nameof(YTemperatureStart)); } }
    private double _yTemperatureStart;
    public double YTemperatureEnd { get => _yTemperatureEnd; set { if (_yTemperatureEnd == value) return; _yTemperatureEnd = value; UpdateYTemperatureMinMax(YTemperatureStart, value); OnPropertyChanged(nameof(YTemperatureEnd)); } }
    private double _yTemperatureEnd;
    public double YTemperatureStepSize { get => _yTemperatureStepSize; set { if (_yTemperatureStepSize == value) return; _yTemperatureStepSize = value; OnPropertyChanged(nameof(YTemperatureStepSize)); } }
    private double _yTemperatureStepSize;
    public string? XAxisText { get => _xAxisText; set { if (_xAxisText == value) return; _xAxisText = value; OnPropertyChanged(nameof(XAxisText)); } }
    private string? _xAxisText;
    public string? YAxisText { get => _yAxisText; set { if (_yAxisText == value) return; _yAxisText = value; OnPropertyChanged(nameof(YAxisText)); } }
    private string? _yAxisText;
    public bool Editable { get => _editable; set { if (_editable == value) return; _editable = value; UpdateEditable(value); OnPropertyChanged(nameof(Editable)); } }
    private bool _editable;
    public int MarginLines { get => _marginLines; set { if (_marginLines == value) return; _marginLines = value; OnPropertyChanged(nameof(MarginLines)); } }
    private int _marginLines = 70;
    public int MarginBottom { get => _marginBottom; set { if (_marginBottom == value) return; _marginBottom = value; OnPropertyChanged(nameof(MarginBottom)); } }
    private int _marginBottom = 40;
    public int MarginTop { get => _marginTop; set { if (_marginTop == value) return; _marginTop = value; OnPropertyChanged(nameof(MarginTop)); } }
    private int _marginTop = 20;
    public double SliderHeight { get => _sliderHeight; set { if (_sliderHeight == value) return; _sliderHeight = value; OnPropertyChanged(nameof(SliderHeight)); } }
    private double _sliderHeight;
    public IBrush GridColor { get => _gridColor; set { if (_gridColor == value) return; _gridColor = value; OnPropertyChanged(nameof(GridColor)); } }
    private IBrush _gridColor = new SolidColorBrush(ColorSettings.GridColor);
    public IBrush TempColor { get => _tempColor; set { if (_tempColor == value) return; _tempColor = value; OnPropertyChanged(nameof(TempColor)); } }
    private IBrush _tempColor = new SolidColorBrush(ColorSettings.TextColor);
    public IBrush LineColor { get => _lineColor; set { if (_lineColor == value) return; _lineColor = value; OnPropertyChanged(nameof(LineColor)); } }
    private IBrush _lineColor = new SolidColorBrush(ColorSettings.TempLineColor);
    public string XValuesStringAppend { get => _xValuesStringAppend; set { if (_xValuesStringAppend == value) return; _xValuesStringAppend = value; OnPropertyChanged(nameof(XValuesStringAppend)); } }
    private string _xValuesStringAppend = "°";
    public string YValuesStringAppend { get => _yValuesStringAppend; set { if (_yValuesStringAppend == value) return; _yValuesStringAppend = value; OnPropertyChanged(nameof(YValuesStringAppend)); } }
    private string _yValuesStringAppend = "°";
    public List<Temperature> Temperatures { get => _temperatures; set { if (_temperatures == value) return; _temperatures = value; _temperatures = value; OnPropertyChanged(nameof(Temperatures)); } }
    private List<Temperature> _temperatures;
    public double YTemperatureStartValue { get => _yTemperatureStartValue; set { if (_yTemperatureStartValue == value) return; _yTemperatureStartValue = value; OnPropertyChanged(nameof(YTemperatureStartValue)); } }
    private double _yTemperatureStartValue;
    public int DecimalPlaces { get => _decimalPlaces; set { if (_decimalPlaces == value) return; _decimalPlaces = value; OnPropertyChanged(nameof(DecimalPlaces)); } }
    private int _decimalPlaces = 2;
    public double HandleSize { get => _handleSize; set { if (_handleSize == value) return; _handleSize = value; UpdateHandleSize(value); OnPropertyChanged(nameof(HandleSize)); } }
    private double _handleSize;

    public new event PropertyChangedEventHandler? PropertyChanged;
    #endregion


    #region Property Accessors
    public bool IsTempMoving { get => _isTempMoving; private set { if (_isTempMoving == value) return; _isTempMoving = value; UpdateIsTempMoving(value); } }
    private bool _isTempMoving;
    public bool IsTempHovering { get => _isTempHovering; private set { if (_isTempHovering == value) return; _isTempHovering = value; UpdateIsTempHovering(value); } }
    private bool _isTempHovering;
    #endregion


    #region Private Variables
    private int _currentTempIndex = -1;
    private Typeface _typeface;
    private CultureInfo _de = new CultureInfo("de-DE");
    private bool _positionInitialized;
    #endregion


    public UserTempPickerAdvancedView(List<Temperature>? temperatures = null)
    {
        if (temperatures is not null)
        {
            _temperatures = temperatures;
            InitTemperatureEvents();
        }
        InitializeComponent();

        _typeface = new Typeface(FontFamily.Name, FontStyle.Normal, FontWeight.Bold);

        Loaded += UserTempPickerAdvancedView_Loaded;
        SizeChanged += UserTempPickerAdvancedView_SizeChanged;
    }

    #region Event Handlers
    private void UserTempPickerAdvancedView_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        SliderHeight = Bounds.Height - MarginBottom - MarginTop;
        if (!_positionInitialized && Temperatures is not null)
            InitializeTemperaturePositions(Bounds.Width - MarginLines * 2, Bounds.Height);
    }

    private void UserTempPickerAdvancedView_SizeChanged(object? sender, SizeChangedEventArgs e)
    {
        SliderHeight = Bounds.Height - MarginBottom - MarginTop;
        if (Temperatures is not null)
            UpdateTemperaturePositions(Bounds.Width - MarginLines * 2, SliderHeight);
    }

    private void UserTempPickerAdvancedView_PointerPressed(object? sender, PointerPressedEventArgs e)
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

    private void UserTempPickerAdvancedView_PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (_currentTempIndex >= 0)
        {
            IsTempMoving = false;
            _currentTempIndex = -1;
        }
    }

    private void UserTempPickerAdvancedView_PointerMoved(object? sender, PointerEventArgs e)
    {
        if (IsTempMoving)
        {
            double y = e.GetCurrentPoint(this).Position.Y;
            Temperatures[_currentTempIndex].YValue = PositionToValue(y, SliderHeight);
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
    #endregion


    #region Rendering
    public override void Render(DrawingContext context)
    {
        if (Temperatures is not null)
        {
            DrawLines(context);
            DrawConnectionLines(context);
            DrawHandles(context);
        }
        DrawText(context);

        base.Render(context);
    }

    private void DrawLines(DrawingContext context)
    {
        Pen pen = new Pen(GridColor)
        {
            LineCap = PenLineCap.Round,
            Thickness = 3
        };

        for (int i = 0; i < Temperatures.Count; i++)
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

        for (int i = 0; i < Temperatures.Count; i++)
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

        for (int i = 0; i < Temperatures.Count; i++)
        {
            if (i < Temperatures.Count - 1)
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
    #endregion


    #region Specific Methods
    public void InitTemperatures()
    {
        Temperatures = CalculateTemperatures();
    }

    private List<Temperature> CalculateTemperatures()
    {
        double min = Math.Min(XTemperatureStart, XTemperatureEnd);
        double max = Math.Max(XTemperatureStart, XTemperatureEnd);
        List<Temperature> temperatures = GetTemperatures(min, max, XTemperatureStepSize);
        return temperatures;
    }

    private List<Temperature> GetTemperatures(double minTemperature, double maxTemperature, double stepSize)
    {
        if (stepSize <= 0 || stepSize >= (maxTemperature - minTemperature))
            throw new ArgumentException("Invalid stepSize value.");

        int numTemperatures = (int)((maxTemperature - minTemperature) / stepSize) + 1;
        List<Temperature> temperatures = new List<Temperature>();

        for (int i = 0; i < numTemperatures; i++)
        {
            Temperature temperature = new Temperature { XValue = minTemperature + i * stepSize };
            temperature.PropertyChanged += Temperature_PropertyChanged;
            temperatures.Add(temperature);
        }

        return temperatures;
    }

    public void InitializeTemperaturePositions()
    {
        InitializeTemperaturePositions(Bounds.Width - MarginLines * 2, Bounds.Height);
    }

    private void InitializeTemperaturePositions(double w, double h)
    {
        if (Temperatures.All(x => x.YValue == YTemperatureStartValue))
        {
            double lineSpacing = w / (Temperatures.Count - 1);
            for (int i = 0; i < Temperatures.Count; i++)
            {
                Temperatures[i].X = MarginLines + i * lineSpacing;
                Temperatures[i].YValue = PositionToValue(h / 2, h);
            }
        }
        else
        {
            double lineSpacing = w / (Temperatures.Count - 1);
            for (int i = 0; i < Temperatures.Count; i++)
            {
                Temperatures[i].X = MarginLines + i * lineSpacing;
                Temperatures[i].YValue = YTemperatureStartValue;
            }
        }
    }

    private void UpdateTemperaturePositions(double w, double h)
    {
        if (!_positionInitialized && Temperatures.All(x => x.YValue != YTemperatureStartValue))
        {
            double lineSpacing = w / (Temperatures.Count - 1);
            for (int i = 0; i < Temperatures.Count; i++)
            {
                Temperatures[i].X = MarginLines + i * lineSpacing;
                Temperatures[i].YValue = YTemperatureStartValue;
            }
        }
        else
        {
            double lineSpacing = w / (Temperatures.Count - 1);
            for (int i = 0; i < Temperatures.Count; i++)
            {
                Temperatures[i].X = MarginLines + i * lineSpacing;
                double newPos = ValueToPosition(Temperatures[i].YValue, h);
                if (Temperatures[i].Y != newPos)
                    Temperatures[i].Y = newPos;
            }
        }
    }

    private void InitTemperatureEvents()
    {
        foreach (Temperature temperature in Temperatures)
            temperature.PropertyChanged += Temperature_PropertyChanged;
    }
    #endregion


    #region Functional Methods
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

        for (int i = 0; i < Temperatures.Count; i++)
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

    private bool IsPointInsideCircle(double x, double y, double centerX, double centerY, double radius)
    {
        double distance = Math.Sqrt(Math.Pow(x - centerX, 2) + Math.Pow(y - centerY, 2));
        return distance <= radius;
    }
    #endregion


    #region Property Change Handlers
    private void UpdateEditable(bool value)
    {
        if (value)
        {
            PointerPressed += UserTempPickerAdvancedView_PointerPressed;
            PointerReleased += UserTempPickerAdvancedView_PointerReleased;
            PointerMoved += UserTempPickerAdvancedView_PointerMoved;
        }
        else
        {
            PointerPressed -= UserTempPickerAdvancedView_PointerPressed;
            PointerReleased -= UserTempPickerAdvancedView_PointerReleased;
            PointerMoved -= UserTempPickerAdvancedView_PointerMoved;
        }
    }


    private void UpdateIsTempMoving(bool value)
    {
        Temperatures[_currentTempIndex].HandleColor = value ? Temperature.DefaultOnHandleColor : Temperature.DefaultOffHandleColor; InvalidateVisual();
    }

    private void UpdateIsTempHovering(bool value)
    {
        Cursor = value ? new Cursor(StandardCursorType.Hand) : new Cursor(StandardCursorType.Arrow);
    }

    private void UpdateHandleSize(double radius)
    {
        foreach (Temperature temperature in Temperatures)
        {
            temperature.Radius = radius;
        }
        InvalidateVisual();
    }

    private void UpdateYTemperatureMinMax(double startTemp, double endTemp)
    {
        if (Temperatures is null)
            return;
        double max = Math.Max(startTemp, endTemp);
        double min = Math.Min(startTemp, endTemp);
        foreach (Temperature temperature in Temperatures)
        {
            temperature.YValue = Math.Clamp(temperature.YValue, min, max);
        }
        InvalidateVisual();
    }

    private void UpdateXTemperatureMinMax(double startTemp, double endTemp, double stepSize)
    {
        if (Temperatures is null)
            return;
        double max = Math.Max(startTemp, endTemp);
        double min = Math.Min(startTemp, endTemp);

        List<Temperature> temperatures = GetTemperatures(min, max, stepSize);
        for (int i = 0; i < temperatures.Count; i++)
        {
            if (i < Temperatures.Count)
                temperatures[i].YValue = Temperatures[i].YValue;
            else
                temperatures[i].YValue = YTemperatureStartValue;
        }
        Temperatures = temperatures;
        UpdateTemperaturePositions(Bounds.Width - MarginLines * 2, SliderHeight);
        InvalidateVisual();
    }
    #endregion

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void Temperature_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is null)
            return;

        Temperature temperature = (Temperature)sender;
        switch (e.PropertyName)
        {
            case nameof(temperature.Y):
                if (!_positionInitialized)
                    _positionInitialized = true;
                InvalidateVisual();
                break;
            case nameof(temperature.YValue):
                double pos = ValueToPosition(temperature.YValue, SliderHeight);
                temperature.Y = pos;
                break;
        }
    }
}