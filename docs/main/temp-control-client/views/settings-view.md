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
public IsolationClasses IsolationClass { get; set; }
public decimal StepSizeTemperature { get; set; }
public decimal MinOutsideTemperature { get; set; }
public decimal MaxOutsideTemperature { get; set; }
public decimal OutsideTemperatureStepSize { get; set; }
public decimal MinUserTemperature { get; set; }
public decimal MaxUserTemperature { get; set; }
public decimal TemperatureHandleSize { get; set; }
public decimal RoundingPrecision { get; set; }
```
Hier werden sämtliche Variablen, welche die Werte für die Einstellungen enthalten definiert.
Diese Variablen werden vom restlichen Programm verwendet um die vorgenommenen Einstellungen entsprechend umzusetzen.<br><br>

**Block 2 - Konstruktor**
```csharp
IsolationClassComboBox.PropertyChangedRuntime += IsolationClassComboBox_PropertyChanged;
LoadSettings();
```
In dem Konstruktor werden die **Changed Events** der jeweiligen Controls der zugeordnet und anschließen die gepseicherten Einstellungen geladen.<br><br>

**Block 3 - PropertyChanged - Funktionen**
```csharp
private void OutsideTemperatureNumericUpDown_PropertyChanged(object? sender, PropertyChangedEventArgs e)
{
    if (e.PropertyName == nameof(OutsideTemperatureNumericUpDown.MinNumericUpDownValue))
        OnPropertyChanged(nameof(MinOutsideTemperature));
    if (e.PropertyName == nameof(OutsideTemperatureNumericUpDown.MaxNumericUpDownValue))
        OnPropertyChanged(nameof(MaxOutsideTemperature));
}
```
Im dritten und letzten Block wird geprüft, ob jegliche Eingaben in den Einstellungen vorgenommen werden.
