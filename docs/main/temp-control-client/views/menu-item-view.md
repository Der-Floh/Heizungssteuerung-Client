# MenuItemView

Das MenuItemView ist ein UserControl, welches einen Menüpunkt in dem seitlichen Menü der Anwendung definiert.

## User Control (.axaml-Datei)

```XML
<StackPanel HorizontalAlignment="Left" VerticalAlignment="Top" Orientation="Horizontal">
	<local:SvgImage Stretch="UniformToFill" Name="MenuItemIcon" Margin="0 0 70 0" ImageTint="White"/>
	<TextBlock Name="MenuItemTextBlock" Margin="0 5 0 0"/>
</StackPanel>
```
`<StackPanel HorizontalAlignment="Left" VerticalAlignment="Top" Orientation="Horizontal">`
1. StackPanel 
   - Das StackPanel ordnet seine untergeordneten Steuerelemente durch horizontales oder vertikales Stapeln an. Das StackPanel wird oft verwendet, um einen kleinen Abschnitt der Benutzeroberfläche auf einer Seite anzuordnen.
   - Innerhalb eines StackPanels, wenn die Größeseigenschaft senkrecht zum Stapel auf einem untergeordneten Steuerelement nicht festgelegt ist, wird das untergeordnete Steuerelement gedehnt, um den verfügbaren Platz auszufüllen. Zum Beispiel wird in horizontaler Ausrichtung die Höhe der untergeordneten Steuerelemente gedehnt, wenn sie nicht festgelegt ist.
   - In Richtung des Stapels wird das StackPanel immer erweitert, um alle untergeordneten Steuerelemente unterzubringen.
2. VerticalAlignemt = "Top"
   - Die im TextBlock enthaltene Inhalte werden vertikal oben angeordnet.
3. HorizontalAlignemt = "Left"
   - Die im TextBlock enthaltene Inhalte werden horizontal links angeordnet.
  
## Klasse (.cs-Datei)

### Eigenschaften

MenuItemView enthält folgende Eigenschaften:

- `string Icon` - Der Pfad zum Svg-Bild des Menüpunktes
- `string Text` - Der Text des Menüpunktes

## Events

MenuItemView enthält folgende Events:

- `PropertyChangedEvent` - Event dass ausgeführt wird sobald eine der oben gennanten Eigenschaften verändert wird