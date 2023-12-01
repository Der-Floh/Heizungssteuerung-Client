# SettingButtonSingleView

Die von uns selbst definierte Einstellung "setting-button-single-view" besteht aus einer .axaml-Datei, sowie aus einer zugehörigen .cs-Datei

Bei dieser Einstellung handelt es sich entsprechend des Namens um eine Einstellung, welche einen einzelnen Button enthält. 

## .axaml-Datei

In der .axaml-Datei definieren wir das eigentliche Aussehen des Settings in XML.

```XML
	<WrapPanel Orientation="Horizontal" Background="#3B3B3B" VerticalAlignment="Stretch" HorizontalAlignment="Stretch">
		<TextBlock Name="SingleTextBlock" IsVisible="False" Padding="10,0,10,0" VerticalAlignment="Center" HorizontalAlignment="Center"/>
		<Button Name="SingleButton" Cursor="Hand" FontFamily="Monospace" VerticalAlignment="Center" HorizontalAlignment="Center"/>
	</WrapPanel>
```

Dieser Code kann als "Grundlage" für diese Einstellung verstanden werden.

**Erklärung der einzelnen verwendeten Befehle**

`<WrapPanel Orientation="Horizontal" Background="#3B3B3B" VerticalAlignment="Stretch" HorizontalAlignment="Stretch">`
1. WrapPanel
   - Das Wrap-Panel ordnet standardmäßig (mehrere) untergeordnete Elemente von links nach rechts an und beginnt eine neue Zeile, wenn der verfügbare Platz nicht ausreicht, einschließlich Ränder und Rahmen.
2. Orientation = "Horizontal"
   - Die untergeordneten Elemente werden horizontal neu angeordnet, wenn die Fenstergröße verändert wird.
3. Background 
   - Die Hintergrundfarbe des WrapPanels wird hiermit definiert
4. VerticalAlignment = "Stretch"
   - Die im WrapPanel enthaltenen Inhalte werden vertikal an die Größe des WrapPanels gestreckt
5. HorizontalAlignment = "Stretch"
   - Die im WrapPanel enthaltenen Inhalte werden horizontal an die Größe des WrapPanels gestreckt

`<TextBlock Name="SingleTextBlock" IsVisible="False" Padding="10,0,10,0" VerticalAlignment="Center" HorizontalAlignment="Center"/>`
1. TextBlock
   - Der TextBlock ist von seiner Funktionalität her identisch mit einem Label und zeigt einen definierten Text an. 
2. isVisible = "false"
   - Das Property "isVisible" definiert, ob der jeweilige TextBlock eingeblendet, oder ausgeblendet werden soll.
3. Padding
   - Mit dem Property Padding kann die effektive Größe des jeweiligen Elements erhöht werden. In diesem Beispiel also um 10mm Links und Rechts.
## .cs-Datei