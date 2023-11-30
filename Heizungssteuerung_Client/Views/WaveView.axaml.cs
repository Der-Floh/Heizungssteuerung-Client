using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;
using Heizungssteuerung_Client.Data;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Heizungssteuerung_Client.Views;

public partial class WaveView : UserControl
{
    public string? Text { get; set; }
    public bool DrawWave { get => _drawWave; set { if (value == _drawWave) return; _drawWave = value; if (value) StartTimer(); else StopTimer(); } }
    private bool _drawWave;
    public double WaveHeightPercent { get; set; }
    public int TransitionSpeed { get; set; } = 20;

    private Timer? _timer;
    private Timer? _transitionTimer;
    private WaveGenerator _waveGenerator = new WaveGenerator();
    private Pen _pen;
    private double _currHeightPercent = 0;
    private bool _transitionFinished = true;

    public WaveView()
    {
        InitializeComponent();

        Loaded += WaveView_Loaded;
        SizeChanged += WaveView_SizeChanged;

        _pen = new Pen
        {
            LineCap = PenLineCap.Round,
            Thickness = 3,
            Brush = Temperature.DefaultOffHandleColor
        };
    }

    private void WaveView_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _waveGenerator.WaveWidth = Bounds.Width;
    }

    private void WaveView_SizeChanged(object? sender, SizeChangedEventArgs e)
    {
        _waveGenerator.WaveWidth = Bounds.Width;
    }

    public async Task EnterFromBottom()
    {
        if (TransitionSpeed < 0)
            TransitionSpeed *= -1;
        if (!_transitionFinished)
            return;
        _transitionFinished = false;
        StartTransitionTimer();
        while (!_transitionFinished)
            await Task.Delay(50);
    }

    public async Task ExitToBottom()
    {
        if (!_transitionFinished)
            return;
        _transitionFinished = false;
        if (TransitionSpeed > 0)
            TransitionSpeed *= -1;
        StartTransitionTimer();
        while (!_transitionFinished)
            await Task.Delay(50);
        _drawWave = false;
    }

    public override void Render(DrawingContext context)
    {
        if (_drawWave)
        {
            double height = Bounds.Height - Bounds.Height * _currHeightPercent;
            DrawRandomWave(context, height);
            DrawContentText(context, height);
        }
        base.Render(context);
    }

    public void DrawRandomWave(DrawingContext context, double height)
    {
        Wave wave = _waveGenerator.GenerateWave(height);
        for (int i = 0; i < wave.WavePoints.Count - 1; i++)
        {
            Point left = wave.WavePoints[i].ToPoint();
            Point right = wave.WavePoints[i + 1].ToPoint();
            StreamGeometry geometry = new StreamGeometry();
            using (StreamGeometryContext geometryContext = geometry.Open())
            {
                geometryContext.BeginFigure(left, true);
                geometryContext.LineTo(right);
                geometryContext.LineTo(new Point(right.X, Bounds.Height));
                geometryContext.LineTo(new Point(left.X, Bounds.Height));
                geometryContext.LineTo(left);

                geometryContext.EndFigure(true);
            }
            context.DrawLine(_pen, left, right);
            context.DrawGeometry(_pen.Brush, _pen, geometry);
        }
    }

    public void DrawContentText(DrawingContext context, double height)
    {
        Typeface typeface = new Typeface(FontFamily.Name, FontStyle.Normal, FontWeight.Bold);
        CultureInfo de = new CultureInfo("de-DE");
        FormattedText formattedText = new FormattedText(Text, de, FlowDirection.LeftToRight, typeface, FontSize, new SolidColorBrush(Colors.White));
        Geometry? tempNumberTextGeometry = formattedText.BuildGeometry(new Point(0, 0));

        double preWidth = tempNumberTextGeometry.Bounds.Width - tempNumberTextGeometry.Bounds.Width;
        double xOffset = (tempNumberTextGeometry.Bounds.Width / 2f) + preWidth;

        context.DrawText(formattedText, new Point(Bounds.Width / 2 - xOffset, height * 2));
    }

    private void StartTimer()
    {
        if (_timer is null)
            _timer = new Timer(TimerCallback, null, 0, 20);
    }

    private void StopTimer()
    {
        if (_timer is not null)
        {
            _timer.Dispose();
            _timer = null;
        }
    }

    private void TimerCallback(object? state)
    {
        Dispatcher.UIThread.InvokeAsync(InvalidateVisual, DispatcherPriority.Background);
    }

    private void StartTransitionTimer()
    {
        if (_transitionTimer is null)
            _transitionTimer = new Timer(TransitionTimerCallback, null, 0, Math.Abs(TransitionSpeed));
    }

    private void StopTransitionTimer()
    {
        if (_transitionTimer is not null)
        {
            _transitionTimer.Dispose();
            _transitionTimer = null;
        }
    }

    private void TransitionTimerCallback(object? state)
    {
        if (TransitionSpeed > 0)
        {
            _currHeightPercent += 0.01;
            if (_currHeightPercent >= WaveHeightPercent)
            {
                StopTransitionTimer();
                _transitionFinished = true;
            }
        }
        else if (TransitionSpeed < 0)
        {
            _currHeightPercent -= 0.01;
            if (_currHeightPercent <= 0)
            {
                StopTransitionTimer();
                _transitionFinished = true;
            }
        }
    }
}