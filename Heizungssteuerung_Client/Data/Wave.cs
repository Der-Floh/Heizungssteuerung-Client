using Avalonia;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Heizungssteuerung_Client.Data;

public sealed class WaveGenerator
{
    public double WaveWidth { get; set; }
    public double WaveHeight { get; set; }

    private Stopwatch _stopwatch = new Stopwatch();

    public WaveGenerator()
    {
        _stopwatch.Start();
    }

    public Wave GenerateWave(double position = -1)
    {
        double wavePosition = WaveHeight / 2;
        if (position >= 0)
            wavePosition = position;
        return Wave.GenerateWave(WaveWidth, wavePosition, _stopwatch.Elapsed.TotalSeconds, 100);
    }
}

public sealed class Wave
{
    public List<WavePoint> WavePoints { get; set; } = new List<WavePoint>();

    public static Wave GenerateWave(double width, double height, double time, double noOfPoints, double baseSpeed = 8)
    {
        Wave wave = new Wave();

        double amplitude = 30;
        double frequency = 0.005;
        double speed = baseSpeed;
        double stepSize = width / noOfPoints;

        for (double x = 0; x <= width; x += stepSize)
        {
            double y = amplitude * Math.Sin(frequency * x + speed * time);
            wave.WavePoints.Add(new WavePoint { X = x, Y = height + y });
        }

        return wave;
    }
}

public sealed class WavePoint
{
    public double X { get; set; }
    public double Y { get; set; }

    public WavePoint(double x = 0, double y = 0)
    {
        X = x;
        Y = y;
    }

    public Point ToPoint()
    {
        return new Point(X, Y);
    }
}
