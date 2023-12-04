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
