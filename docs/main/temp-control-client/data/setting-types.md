# SettingTypes

Das Objekt SettingTypes enthält sämtliche von uns selbst definierte Einstellungen, welche wir auf unserer Einstellungsseite erstellt haben.

Die einzelnen Einstellungstypen sind in einem enum definiert und können an jeder anderer Stelle im Programm gefunden und verwendet werden.

```c#
namespace Heizungssteuerung_Client.Data;

public enum SettingTypes
{
    None,
    Button,
    NumericUpDown,
    NumericUpDownMinMax,
    ComboBox,
    Toggle,
    RadioButton,
    Slider,
}
```

Wir haben dieses Vorgehen gewählt, um die Einstellungstypen, welche z.T. öfters vorkommen nicht jedes Mal neu zu programmieren.<br> 
Indem wir Einstellungen zu Typen definieren, können wir diese am Ende beliebig oft nutzen und dabei grundlegende Eigenschaften wie die Größe oder Farbe der jeweiligen Einstellung bereits mitgeben. 
