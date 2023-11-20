using System.ComponentModel;

namespace Heizungssteuerung_Client.Data;

public enum IsolationClasses
{
    [Description("A+")] AA,
    [Description("A")] A,
    [Description("B")] B,
    [Description("C")] C,
    [Description("D")] D,
    [Description("E")] E,
    [Description("F")] F,
    [Description("G")] G,
    [Description("H")] H,
}
