# SettingView
Das View "SettingView" stellt das einzelne View das angelegt wird, dar. 
<br><br>

## User Control (.axaml-Datei) 
```XML
<Border Name="SettingBorder" BorderBrush="#3B3B3B" BorderThickness="2" CornerRadius="5" Margin="5" VerticalAlignment="Center" HorizontalAlignment="Center">
	<Panel Name="SettingPanel"/>
</Border>
```
Im View geben wir, aus rein ästhetischen Gründen, den einzelnen Einstellungen noch einen Rahmen hinzu, damit diese im Endeffekt abgerundete Ecken haben.
<br><br> 

## Klasse (.cs-Datei) 
iufgsdiuhf

### Eigenschaften

SettingView enthält folgende Einegschaften:

- `string Text` - Gibt den Text der Einstellung an
- `string ToolTip` - Gibt den ToolTip der Einstellung an
- `decimal Value` - Gibt den Wert für die NumericUpDown-Controls an (Siehe [SettingNumericUpDownSingleView](./settings/setting-numericupdown-single-view))
- `decimal Increment` - Gibt den Wertzuwachs der NumericUpDown-Controls an (Siehe [SettingNumericUpDownSingleView](./settings/setting-numericupdown-single-view))
- `decimal Minimum` - Gibt den minimalen Wert für die NumericUpDown-Controls an (Siehe [SettingNumericUpDownSingleView](./settings/setting-numericupdown-single-view))
- `decimal Maximum` - Gibt den maximalen Wert für die NumericUpDown-Controls an (Siehe [SettingNumericUpDownSingleView](./settings/setting-numericupdown-single-view))
- `decimal MinNumericUpDownValue` - Gibt den Wert des ersten NumericUpDown-Controls einer Min/Max-Einstellung an (Siehe [SettingNumericUpDownMinMaxView](./settings/setting-numericupdown-minmax-view))
- `decimal MinNumericUpDownIncrement` - Gibt den Wertzuwachs des ersten NumericUpDown-Controls einer Min/Max-Einstellung an (Siehe [SettingNumericUpDownMinMaxView](./settings/setting-numericupdown-minmax-view))
- `decimal MinNumericUpDownMinimum` - Gibt den minimalen Wert des ersten NumericUpDown-Controls einer Min/Max-Einstellung an (Siehe [SettingNumericUpDownMinMaxView](./settings/setting-numericupdown-minmax-view))
- `decimal MinNumericUpDownMaximum` - Gibt den maximalen Wert des ersten NumericUpDown-Controls einer Min/Max-Einstellung an (Siehe [SettingNumericUpDownMinMaxView](./settings/setting-numericupdown-minmax-view))
- `decimal MaxNumericUpDownValue` - Gibt den Wert des zweiten NumericUpDown-Controls einer Min/Max-Einstellung an (Siehe [SettingNumericUpDownMinMaxView](./settings/setting-numericupdown-minmax-view))
- `decimal MaxNumericUpDownIncrement` - Gibt den Wertzuwachs des zweiten NumericUpDown-Controls einer Min/Max-Einstellung an (Siehe [SettingNumericUpDownMinMaxView](./settings/setting-numericupdown-minmax-view))
- `decimal MaxNumericUpDownMinimum` - Gibt den minimalen Wert des zweiten NumericUpDown-Controls einer Min/Max-Einstellung an (Siehe [SettingNumericUpDownMinMaxView](./settings/setting-numericupdown-minmax-view))
- `decimal MaxNumericUpDownMaximum` - Gibt den maximalen Wert des zweiten NumericUpDown-Controls einer Min/Max-Einstellung an (Siehe [SettingNumericUpDownMinMaxView](./settings/setting-numericupdown-minmax-view))
- `IEnumerable? ItemSource` - Gibt die Quelle der ComboBox-Elemente an (Siehe [SettingComboBoxSingleView](./settings/setting-combobox-single-view))
- `object SelectedItem` - Gibt das ausgewählte Element bspws. aus einer ComboBox an (Siehe [SettingComboBoxSingleView](./settings/setting-combobox-single-view))
- `int SelectedIndex` - Gibt den Index des aktuell ausgewählten Elements an (Siehe [SettingComboBoxSingleView](./settings/setting-combobox-single-view))

### Events

SettingView enthält folgende Events:

- `PropertyChanged` - Event dass ausgeführt wird sobald eine der oben gennanten Eigenschaften durch Code verändert wird
- `PropertyChangedRuntime` - Event dass ausgeführt wird sobald eine der oben gennanten Eigenschaften während der Laufzeit verändert wird
- `Click` - Event dass ausgeführt wird, sobald der [SettingButtonSingleView](./settings/setting-button-single-view) geklickt wird
