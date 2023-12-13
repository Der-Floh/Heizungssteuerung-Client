using System;

namespace Heizungssteuerung_Client.Data;

public static class OS
{
    public static bool IsWindows() => OperatingSystem.IsWindows();
    public static bool IsLinux() => OperatingSystem.IsLinux();
    public static bool IsMacOS() => OperatingSystem.IsMacOS();
    public static bool IsAndroid() => OperatingSystem.IsAndroid();
    public static bool IsIOS() => OperatingSystem.IsIOS();
    public static bool IsBrowser() => OperatingSystem.IsBrowser();
    public static bool IsFreeBSD() => OperatingSystem.IsFreeBSD();
    public static bool IsMacCatalyst() => OperatingSystem.IsMacCatalyst();
    public static bool IsTvOS() => OperatingSystem.IsTvOS();
    public static bool IsWatchOS() => OperatingSystem.IsWatchOS();
    public static bool IsDesktop() => IsWindows() || IsLinux() || IsMacOS() || IsMacCatalyst() || IsFreeBSD();
    public static bool IsMobile() => IsAndroid() || IsIOS() || IsTvOS() || IsWatchOS();
}
