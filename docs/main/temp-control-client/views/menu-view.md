# MenuView

Der MenuView ist ein UserControl, welches das seitliche Menü der Anwendung definiert.

## User Control (.axaml-Datei)

```XML
<SplitView Name="SplitViewCom" OpenPaneLength="300" CompactPaneLength="60" DisplayMode="CompactInline" Background="Black" PaneBackground="#1e1e1e">
    <SplitView.Pane>
        <StackPanel Spacing="5" Margin="5">
            <StackPanel Orientation="Horizontal">
                <Button HorizontalAlignment="Left" VerticalAlignment="Top" Name="TriggerPaneButton" Cursor="Hand" Width="50" Height="45" CornerRadius="10" Grid.Column="1">
                    <local:SvgImage Grid.Column="1" Stretch="UniformToFill" Source="/Assets/menu.svg" ImageTint="White"/>
                </Button>
                <TextBlock Name="TitleText" Grid.Column="2" Margin="5 12 0 0" FontSize="16" FontWeight="500"/>
            </StackPanel>
            <ListBox SelectionMode="Single" Name="SplitviewListBox" CornerRadius="10" Cursor="Hand"/>
        </StackPanel>
    </SplitView.Pane>
	<SplitView.Content>
		<Panel Name="ContentPanel" Background="#2d2d2d"/>
	</SplitView.Content>
</SplitView>
```

`<SplitView Name="SplitViewCom" OpenPaneLength="300" CompactPaneLength="60" DisplayMode="CompactInline" Background="Black" PaneBackground="#1e1e1e">`
1. SplitView
   - Ein Split-View präsentiert einen Container mit zwei Teilen: der Hauptinhaltszone und einer Seitenleiste. Die Hauptinhaltszone ist immer sichtbar. Die Seitenleiste kann erweitert und zusammengeklappt werden. Die zusammengeklappte Seitenleiste kann vollständig ausgeblendet oder leicht geöffnet bleiben - mit genügend Platz für beispielsweise einige Symboltasten.
2. Name = "SplitViewCom"
   - Dem Split View wird einem Namen mitgegeben, um diesen in der .cs-Datei, bzw. in der zugehörigen Klasse für dieses Setting wiederzufinden und zu verwenden.
3. OpenPaneLength = "300"
   - Definiert die Breite der Spalte, wenn diese geöffnet ist.
4. CompactPaneLength = "60"
   - Definiert die Breite der Spalte, wenn diese geschlossen ist.
5. DisplayMode = "CompactInLine"
   - Mit dem Property "DisplayMode" kann aus einer Vorauswahl verschiedener, von AvaloniaUI bereitgestellter, Darstellungsweisen für Split Views, ausgewählt werden. In diesem Fall nutzen wir den DisplayMode "CompactInLine" was man als User sehen kann. (Das ein- und ausklappbare Menü links)
6. Background = "Black"
   - Definiert die Hintergrundfarbe der linken Spalte, also das Menü.
7. PaneBackground
   - Definiert die Hintergrundfarbe der rechten Spalte, also der Inhalt.
<br><br>

`<local:SvgImage Grid.Column="1" Stretch="UniformToFill" Source="/Assets/menu.svg" ImageTint="White"/>`
1. local:SvgImage
   - Mit dieser Einstellung kann festgelegt werden, dass ein SVG-Bild eingefügt werden soll
2. Grid.Column = "1"
   - Die Rastersteuerung hat den Zweck, untergeordnete Steuerelemente in Spalten und Zeilen anzuordnen. Sie können absolute, relative oder proportionale Zeilen- und Spaltengeometrien für das Raster definieren.
   - Jedes untergeordnete Steuerelement im Raster kann in einer Zelle des Rasters positioniert werden, unter Verwendung von Spalten- und Zeilenkoordinaten. Diese sind nullbasiert, wobei beide standardmäßig den Wert null haben.
3. Stretch = "UniformToFill"
   - Mithilfe des Propertys Stretch kann definiert werden, wie sich das Bildformat bei veränderter Fenstergröße anpasst. UniformToFill bedeutet, dass das Bildformat beibehalten wird.
4. Source = "/Assets/menu.svg"
   - Das Property Source gibt den Speicherpfad des Bildes an.
5. ImageTint = "White"
   - Gibt die Die Färbung der SVG-Dateien an. In diesem Fall werden die SVG-Dateien weiß gefüllt.
<br><br>

`<ListBox SelectionMode="Single" Name="SplitviewListBox" CornerRadius="10" Cursor="Hand"/>`
1. ListBox
   - Die ListBox zeigt Elemente aus einer ItemsSource-Sammlung an, auf mehreren Zeilen, und ermöglicht einzelne oder mehrfache Auswahl.
   - Die Elemente in der Liste können zusammengesetzt, gebunden und als Vorlage gestaltet werden.
   - Die Höhe der Liste dehnt sich aus, um alle Elemente unterzubringen, es sei denn, sie wird explizit festgelegt (unter Verwendung des Höhenattributs) oder durch ein übergeordnetes Steuerelement, wie zum Beispiel das DockPanel.
2. SelectionMode = "Single"
   - Durch das Setzen des Properties "SelectionMode" auf "Single" kann immer nur ein Element gleichzeitig ausgewählt werden.
<br><br>
## Klasse (.cs-Datei)

### Eigenschaften

MenuView enthält folgende Eigenschaften:

- `List<MenuItemView> Contents` - Liste aller Menü Elemente (Siehe [MenuItemView](./menu-item-view))

### Funktionen

MenuView enthält folgende Funktionen:

- `void AddContent(string content, string icon, UserControl control)` - Fügt einen neuen Menüpunkt hinzu
  - *`string content` - Name des Menüpunktes*
  - *`string icon` - Pfad zum SVG-Bild des Menüpunktes*
  - *`UserControl control` - UserControl das angezeigt werden soll wenn der Menüpunkt angeklickt wird (Siehe [Avalonia.Controls](https://docs.avaloniaui.net/docs/reference/controls/usercontrol))*
