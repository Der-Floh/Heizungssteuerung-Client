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
Hierbei handelt es sich um die Einstellung, welche den Bereich der minimalen und maximalen Außentemperatur steuert.
Mithilfe vom Property "SettingType" können wir aus unserem Einstellungstypen-Pool eine der Einstellungs-Vorlagen auswählen. Sämtliche Inhalte dieser Einstellungs-Vorlage werden dabei übernommen, wie z.B. Textblöcke, Abstände, etc.
Zusätzlich legen wie hier fest, welcher Text im TextBlock der Vorlage angewendet werden soll
