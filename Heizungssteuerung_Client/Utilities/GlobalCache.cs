using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;

namespace Heizungssteuerung_Client.Utilities;

internal static class GlobalCache
{
    private static readonly ConcurrentDictionary<string, object> _cache = new();
    private static string? currentAssemblyName;

    private static void AddToCache(string path, object obj) => _cache.TryAdd(path, obj);
    private static object? GetFromCache(string path)
    {
        if (_cache.TryGetValue(path, out var obj))
            return obj;

        return null;
    }

    public static IImage? GetImage(string path, bool doCache = true)
    {
        if (string.IsNullOrEmpty(path))
            return null;

        if (path is string rawUri)
        {
            Uri uri;

            if (rawUri.StartsWith("avares://"))
            {
                uri = new Uri(rawUri);
            }
            else
            {
                if (currentAssemblyName is null)
                    currentAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;


                uri = new Uri($"avares://{currentAssemblyName}/{rawUri}");
            }


            var cachedObj = GetFromCache(uri.ToString());
            if (cachedObj is not null)
            {
                return (Bitmap)cachedObj;
            }

            var asset = AssetLoader.Open(uri);

            var bitmap = new Bitmap(asset);
            if (doCache)
            {
                AddToCache(uri.ToString(), bitmap);
            }

            return bitmap;
        }

        throw new NotSupportedException();
    }



    public static Stream? GetStream(string path, bool doCache = true)
    {
        if (string.IsNullOrEmpty(path))
            return null;

        if (path is string rawUri)
        {
            Uri uri;

            if (rawUri.StartsWith('/'))
            {
                rawUri = rawUri.Substring(1);
            }

            if (rawUri.StartsWith("avares://"))
            {
                uri = new Uri(rawUri);
            }
            else
            {
                if (currentAssemblyName is null)
                    currentAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;

                uri = new Uri($"avares://{currentAssemblyName}/{rawUri}");
            }

            var cachedObj = GetFromCache(uri.ToString());
            if (cachedObj is not null)
            {
                return new MemoryStream((byte[])cachedObj);
            }

            var asset = AssetLoader.Open(uri);

            byte[] data;
            using (var ms = new MemoryStream())
            {
                asset.CopyTo(ms);
                data = ms.ToArray();
            }

            if (doCache)
            {
                AddToCache(uri.ToString(), data);
            }

            return new MemoryStream(data);
        }

        throw new NotSupportedException();
    }
}