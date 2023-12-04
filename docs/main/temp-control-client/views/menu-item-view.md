# MenuItemView

Das MenuItemView ist ein UserControl, welches einen Menüpunkt in dem seitlichen Menü der Anwendung definiert.

## User Control (.axaml-Datei)

```XML
<StackPanel HorizontalAlignment="Left" VerticalAlignment="Top" Orientation="Horizontal">
	<local:SvgImage Stretch="UniformToFill" Name="MenuItemIcon" Margin="0 0 70 0" ImageTint="White"/>
	<TextBlock Name="MenuItemTextBlock" Margin="0 5 0 0"/>
</StackPanel>
```

## Klasse (.cs-Datei)

### Eigenschaften

MenuItemView enthält folgende Eigenschaften:

- `string Icon` - Der Pfad zum Svg-Bild des Menüpunktes
- `string Text` - Der Text des Menüpunktes

## Events

MenuItemView enthält folgende Events:

- `PropertyChangedEvent` - Event dass ausgeführt wird sobald eine der oben gennanten Eigenschaften verändert wird