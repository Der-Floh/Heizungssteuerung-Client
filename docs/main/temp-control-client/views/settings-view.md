# SettingsView

Das View "SettingsView" stellt die eigentliche Seite für die Einstellungen dar, welche der User in der Heizungssteuerung sehen kann.<br>
Es beinhaltet sämtliche von uns selbst definierte Einstellungen, woebei wir diese auf diesem View schlussendlich "mit Leben füllen".<br>
Heißt, hier legen wir fest, welchen Namen das Setting erhält, ob es einen Tooltip hat, ob bestimmte Default-Werte festgelegt werden, usw.

**Beispiel anhand einer Einstellung:**
```XML
<views:SettingView Name="OutsideTemperatureNumericUpDown"
				   SettingType="NumericUpDownMinMax"
				   Text="Outside Temperature Range"
				   Minimum="-40.0"
				   Maximum="70.0"
				   ToolTip="Specifiy the range of the minimum and maximum outside temperature."/>
```
Hierbei handelt es sich um die Einstellung, welche den Bereich der minimalen und maximalen Außentemperatur steuert.<br><br>

`SettingType`

Mithilfe vom Property "SettingType" können wir aus unserem Einstellungstypen-Pool eine der Einstellungs-Vorlagen auswählen. Sämtliche Inhalte dieser Einstellungs-Vorlage werden dabei übernommen, wie z.B. Textblöcke, Abstände, etc.<br><br>

`Text`
Zusätzlich legen wie hier fest, welcher Text im TextBlock der Vorlage angewendet werden soll.<br><br>

## Klasse
Die zugehörige Klasse besteht im Grunde genommen aus 3 Blöcken.

**Block 1 - Deklarierung Einstellungs-Variablen** 
```csharp
public static IsolationClasses IsolationClass { get; set; } = IsolationClasses.A;
public static decimal StepSizeTemperature { get; set; } = 0.5m;
public static decimal MinOutsideTemperature { get => _minOutsideTemperature; set { _minOutsideTemperature = value; InitUserTemps(); } }
private static decimal _minOutsideTemperature = -10.0m;
public static decimal MaxOutsideTemperature { get => _maxOutsideTemperature; set { _maxOutsideTemperature = value; InitUserTemps(); } }
public static decimal _maxOutsideTemperature = 40.0m;
public static decimal OutsideTemperatureStepSize { get => _outsideTemperatureStepSize; set { _outsideTemperatureStepSize = value; InitUserTemps(); } }
public static decimal _outsideTemperatureStepSize = 10m;
public static decimal MinUserTemperature { get; set; } = 0.0m;
public static decimal MaxUserTemperature { get; set; } = 40.0m;
public static decimal TemperatureHandleSize { get; set; } = 30.0m;
public static decimal RoundingPrecision { get; set; } = 2.0m;
public static Temperature[] UserTemperatures { get; set; } = new Temperature[6];
public static Temperature[] WeatherTemperatures { get; set; } = new Temperature[8];
```
Hier werden sämtliche Variablen, welche die Werte für die Einstellungen enthalten definiert.
Diese Variablen werden vom restlichen Programm verwendet um die vorgenommenen Einstellungen entsprechend umzusetzen.<br><br>

**Block 2 - SettingsView()**
```csharp
IsolationClassComboBox.SelectedItem = IsolationClass;
StepSizeTemperatureNumericUpDown.Value = StepSizeTemperature;
```
In der Funktion "SettingsView" werden die jeweiligen Controls der Einstellungen den vorher deklarierten Variablen zugeordnet.<br><br>

**Block 3 - PropertyChanged - Funktionen**
```csharp
private void OutsideTemperatureNumericUpDown_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
{
    if (e.PropertyName == nameof(OutsideTemperatureNumericUpDown.MinNumericUpDownValue))
        MinOutsideTemperature = OutsideTemperatureNumericUpDown.MinNumericUpDownValue;
    if (e.PropertyName == nameof(OutsideTemperatureNumericUpDown.MaxNumericUpDownValue))
        MaxOutsideTemperature = OutsideTemperatureNumericUpDown.MaxNumericUpDownValue;
}
```
Im dritten und letzten Block wird geprüft, ob jegliche Eingaben in den Einstellungen vorgenommen werden.
