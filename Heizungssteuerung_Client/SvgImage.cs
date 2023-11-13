using Avalonia;
using Avalonia.Interactivity;
using Avalonia.Media;
using Heizungssteuerung_Client.Controls;
using Heizungssteuerung_Client.Utilities;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;

namespace Heizungssteuerung_Client;

public enum ImageMode
{
    None,
    SVG,
    PNG
}

public class SvgImage : SkiaControl
{
    public ImageMode SelectedImageMode { get; private set; }

    static SvgImage()
    {
        AffectsRender<SvgImage>(
            SourceProperty,
            StretchProperty,
            ImageTintProperty
            );
    }


    public static readonly StyledProperty<string> SourceProperty =
         AvaloniaProperty.Register<SvgImage, string>(nameof(Source));

    public string Source
    {
        get => GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    public static readonly StyledProperty<IBrush?> ImageTintProperty =
      AvaloniaProperty.Register<SvgImage, IBrush?>(nameof(ImageTint), null);

    public IBrush? ImageTint
    {
        get => GetValue(ImageTintProperty);
        set => SetValue(ImageTintProperty, value);
    }

    public static readonly StyledProperty<Stretch> StretchProperty =
       AvaloniaProperty.Register<SvgImage, Stretch>(nameof(Stretch), Stretch.None);

    public Stretch Stretch
    {
        get => GetValue(StretchProperty);
        set => SetValue(StretchProperty, value);
    }


    private Size _imageSize;
    private readonly SKPaint _imagePaint = new SKPaint() { IsAntialias = true };
    private Stream? _sourceStream;
    private readonly SkiaSharp.Extended.Svg.SKSvg _svgImage = new();

    private Stretch _renderSaveStretch = Stretch.None;
    private SKBitmap? _resourceBitmap;

    private readonly List<IDisposable> _disposables = new();
    public SvgImage()
    {
        var sourceDisposable = this.GetObservable(SourceProperty).Subscribe(OnSourceChanged);
        var stretchDisposable = this.GetObservable(StretchProperty).Subscribe(OnStretchChanged);
        var imageTintDisposable = this.GetObservable(ImageTintProperty).Subscribe(OnImageTintChanged);
        var opacityDisposable = this.GetObservable(OpacityProperty).Subscribe(OnOpacityChanged);


        _disposables.Add(sourceDisposable);
        _disposables.Add(stretchDisposable);
        _disposables.Add(imageTintDisposable);
        _disposables.Add(_imagePaint);
    }

    protected override void RenderSkia(SKCanvas canvas)
    {
        if (_sourceStream is null)
            return;

        if (_renderSaveStretch != Stretch.None)
            ScaleCanvas(canvas);




        if (_svgImage.Picture is null)
        {

            var point = new SKPoint(0, 0);

            canvas.DrawBitmap(_resourceBitmap, point, _imagePaint);
        }
        else
        {
            canvas.DrawPicture(_svgImage.Picture, _imagePaint);
        }
    }

    private void ScaleCanvas(SKCanvas canvas)
    {
        var imageHeight = (float)_imageSize.Height;
        var imageWidth = (float)_imageSize.Width;

        var destHeight = (float)Bounds.Height;
        var destWidth = (float)Bounds.Width;
        if (destHeight == float.NaN || destHeight == 0)
            destHeight = imageHeight;
        if (destWidth == float.NaN || destWidth == 0)
            destWidth = imageWidth;


        var scaleX = 1f;
        var scaleY = 1f;

        switch (_renderSaveStretch)
        {
            case Stretch.None:
                return;

            case Stretch.Fill:
                scaleX = destWidth / imageWidth;
                scaleY = destHeight / imageHeight;
                break;

            case Stretch.UniformToFill:
                var scaleToFill = Math.Max(destWidth / imageWidth, destHeight / imageHeight);
                scaleX = scaleY = scaleToFill;
                break;

            case Stretch.Uniform:
                var scaleUniform = Math.Min(destWidth / imageWidth, destHeight / imageHeight);
                scaleX = scaleY = scaleUniform;
                break;
        }

        canvas.Scale(scaleX, scaleY);
    }

    protected override void OnDispose()
    {
        if (_sourceStream is not null)
            _sourceStream.Dispose();

        if (_resourceBitmap is not null)
            _resourceBitmap.Dispose();

        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }

    private void OnStretchChanged(Stretch stretch)
    {
        _renderSaveStretch = Stretch;
    }

    private void OnOpacityChanged(double opacity)
    {
        OnImageTintChanged(ImageTint);
    }


    private void OnImageTintChanged(IBrush? brush)
    {
        if (brush is null)
        {
            _imagePaint.ColorFilter = null;
            return;
        }

        _imagePaint.ColorFilter = SKColorFilter.CreateBlendMode(
            brush.ToSKColor((float)Opacity),
            SKBlendMode.SrcIn);
    }

    private void OnSourceChanged(string source)
    {
        if (string.IsNullOrEmpty(Source))
            return;

        var fileExtension = Path.GetExtension(Source);

        if (fileExtension is null)
            return;

        _sourceStream = GlobalCache.GetStream(Source);
        if (_sourceStream is null)
            return;

        switch (fileExtension)
        {
            case ".png":
                SelectedImageMode = ImageMode.PNG;
                _resourceBitmap = SKBitmap.Decode(_sourceStream);

                if (_resourceBitmap is null)
                    return;
                _imageSize = new Size(_resourceBitmap.Info.Width, _resourceBitmap.Info.Height);


                break;

            case ".svg":
                SelectedImageMode = ImageMode.SVG;
                _svgImage.Load(_sourceStream);

                if (_svgImage.Picture is null)
                    return;
                _imageSize = new Size(_svgImage.Picture.CullRect.Width, _svgImage.Picture.CullRect.Height);
                break;

            default:
                SelectedImageMode = ImageMode.None;

                break;
        }

        if (SkiaBounds.Height == 0 || SkiaBounds.Height == double.NaN)
        {
            SkiaBounds = new Rect(SkiaBounds.X, SkiaBounds.Y, SkiaBounds.Width, _imageSize.Height);
            MinHeight = _imageSize.Height;
        }

        if (SkiaBounds.Width == 0 || SkiaBounds.Width == double.NaN)
        {
            SkiaBounds = new Rect(SkiaBounds.X, SkiaBounds.Y, _imageSize.Width, SkiaBounds.Height);
            MinWidth = _imageSize.Width;
        }
    }
}
