using Avalonia;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using Avalonia.Threading;
using SkiaSharp;
using System;
using System.Threading.Tasks;

namespace Heizungssteuerung_Client.Controls;

public class SkiaControl : Control, IDisposable
{
    private DateTime _lastInvalidateSkiaUpdate = DateTime.MinValue;

    private TimeSpan _invalidateSkia = TimeSpan.Zero;
    public TimeSpan InvalidateSkia
    {
        get => _invalidateSkia;
        set
        {
            if (_invalidateSkia == value)
                return;

            _invalidateSkia = value;

            if (!_isInitialized)
                return;
            UpdateSkiaUpdateTask();
        }
    }

    public Rect SkiaBounds
    {
        get => _skiaRendering.Bounds;
        set
        {
            _skiaRendering.Bounds = value;
        }
    }


    private readonly SkiaRendering _skiaRendering;
    private protected bool _disposed = false;
    private bool _isInitialized = false;
    public SkiaControl()
    {
        _skiaRendering = new SkiaRendering();
        _skiaRendering.OnRenderSkia += RenderSkia;
        Loaded += SkiaControl_Loaded;
    }

    private void UpdateSkiaUpdateTask()
    {
        _lastInvalidateSkiaUpdate = DateTime.Now;
        if (_invalidateSkia == TimeSpan.Zero)
            return;

        Dispatcher.UIThread.Post(() => _ = SkiaUpdateTask(), priority: DispatcherPriority.Render);
    }

    private void SkiaControl_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _isInitialized = true;
        UpdateSkiaUpdateTask();
    }

    private async Task SkiaUpdateTask()
    {
        var onStart = _lastInvalidateSkiaUpdate;

        while (_isInitialized)
        {
            if (_lastInvalidateSkiaUpdate != onStart)
                return;

            InvalidateVisual();
            await Task.Delay(InvalidateSkia);
        }
    }

    ~SkiaControl() => Dispose();

    protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromLogicalTree(e);
        Dispose();
    }

    public override void Render(DrawingContext context)
    {
        OnBeforRender();

        _skiaRendering.Bounds = new Rect(0, 0, Bounds.Width, Bounds.Height);
        context.Custom(_skiaRendering);
    }

    protected virtual void OnBeforRender() { }
    protected virtual void RenderSkia(SKCanvas canvas) { }
    protected virtual void OnDispose() { }

    protected virtual void DisposeBase(bool disposing)
    {
        if (_disposed)
            return;

        OnDispose();

        _isInitialized = false;
        _lastInvalidateSkiaUpdate = DateTime.MinValue;
        Loaded -= SkiaControl_Loaded;
        _skiaRendering.Dispose();

        _disposed = true;
    }

    public void Dispose()
    {
        DisposeBase(true);
        GC.SuppressFinalize(this);
    }

    private class SkiaRendering : ICustomDrawOperation
    {
        public Action<SKCanvas>? OnRenderSkia;
        public Rect Bounds { get; set; }

        public void Dispose()
        {
            OnRenderSkia = null;
        }

        public bool Equals(ICustomDrawOperation? other) => other == this;

        public bool HitTest(Point p) { return false; }

        public void Render(ImmediateDrawingContext context)
        {
            var leaseFeature = context.TryGetFeature<ISkiaSharpApiLeaseFeature>();
            if (leaseFeature is null)
            {
                return;
            }
            else
            {
                using var lease = leaseFeature.Lease();
                var canvas = lease.SkCanvas;
                OnRenderSkia?.Invoke(canvas);
            }
        }
    }
}
