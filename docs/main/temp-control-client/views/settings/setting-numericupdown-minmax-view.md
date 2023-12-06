# SettingNumericUpDownMinMaxView

Die von uns selbst definierte Einstellung "setting-numericupdown-minmax-view" besteht aus einer .axaml-Datei, welche den eigentlichen View darstellt, sowie aus einer zugehörigen .cs-Datei, welche die Klasse des zugehörigen Views darstellt.

Bei dieser Einstellung handelt es sich entsprechend des Namens um eine Einstellung, welche zwei NumericUpdown-Controls enthält.

## User Control (.axaml-Datei)

In der .axaml-Datei definieren wir das eigentliche Aussehen des Settings in XML.

```XML
<WrapPanel Name="MinMaxStackPanel" Orientation="Horizontal" Background="#3B3B3B" VerticalAlignment="Stretch" HorizontalAlignment="Stretch">
    <TextBlock Name="MinMaxTextBlock" Padding="10,0,10,0" VerticalAlignment="Center" HorizontalAlignment="Center"/>
    <StackPanel Orientation="Horizontal" VerticalAlignment="Center" HorizontalAlignment="Center">
        <StackPanel Name="MinStackPanel" Orientation="Vertical" Margin="5" VerticalAlignment="Center" HorizontalAlignment="Center">
            <TextBlock Name="MinTextBlock" Text="Minium" VerticalAlignment="Top" HorizontalAlignment="Center"/>
            <NumericUpDown Name="MinNumericUpDown" Value="0.0" FontFamily="Monospace" Margin="5,5,5,0" Cursor="Hand" VerticalAlignment="Center" HorizontalAlignment="Center"/>
        </StackPanel>
        <StackPanel Name="MaxStackPanel" Orientation="Vertical" Margin="5" VerticalAlignment="Center" HorizontalAlignment="Center">
            <TextBlock Name="MaxTextBlock" Text="Maximum" VerticalAlignment="Top" HorizontalAlignment="Center"/>
            <NumericUpDown Name="MaxNumericUpDown" Value="0.0" FontFamily="Monospace" Margin="5,5,5,0" Cursor="Hand" VerticalAlignment="Center" HorizontalAlignment="Center"/>
        </StackPanel>
    </StackPanel>
</WrapPanel>
```

Dieser Code kann als "Grundlage" für diese Einstellung verstanden werden.<br><br>

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
<br><br>

`<TextBlock Name="MinMaxTextBlock" Padding="10,0,10,0" VerticalAlignment="Center" HorizontalAlignment="Center"/>`
1. TextBlock
   - Der TextBlock ist von seiner Funktionalität her identisch mit einem Label und zeigt einen definierten Text an. 
2. Name = "SingleTextBlock"
   - Dem TextBlock wird einem Namen mitgegeben, um diesen in der .cs-Datei, bzw. in der zugehörigen Klasse für dieses Setting wiederzufinden und zu verwenden.
4. Padding
   - Mit dem Property Padding kann die effektive Größe des jeweiligen Elements erhöht werden. In diesem Beispiel also um 10mm Links und Rechts.
5. VerticalAlignemt = "Center"
   - Die im TextBlock enthaltene Inhalte werden vertikal zentriert angeordnet.
6. HorizontalAlignemt = "Center"
   - Die im TextBlock enthaltene Inhalte werden horizontal zentriert angeordnet.
<br><br>

`<StackPanel Orientation="Horizontal" VerticalAlignment="Center" HorizontalAlignment="Center">`
1. StackPanel
   - Das Stackpanel ordnet seine untergeordneten Steuerelemente durch Stapeln horizontal oder vertikal an. Das Stackpanel wird oft verwendet, um einen kleinen Abschnitt der Benutzeroberfläche auf einer Seite anzuordnen.
   - Innerhalb eines Stackpanels, wenn die Größeigenschaft senkrecht zum Stapel auf einem untergeordneten Steuerelement nicht festgelegt ist, wird das untergeordnete Steuerelement gestreckt, um den verfügbaren Platz auszufüllen. Zum Beispiel dehnt sich in horizontaler Ausrichtung die Höhe der untergeordneten Steuerelemente aus, wenn sie nicht festgelegt ist.
   - In Richtung des Stapels wird das Stackpanel immer erweitert, um alle untergeordneten Steuerelemente aufzunehmen.
2. VerticalAlignemt = "Center"
   - Die im StackPanel enthaltene Inhalte werden vertikal zentriert angeordnet.
3. HorizontalAlignemt = "Center"
   - Die im StackPanel enthaltene Inhalte werden horizontal zentriert angeordnet.
<br><br>

`<StackPanel Name="MinStackPanel" Orientation="Vertical" Margin="5" VerticalAlignment="Center" HorizontalAlignment="Center">`
1. Name = MinStackPanel
   - Dem StackPanel wird einem Namen mitgegeben, um diesen in der .cs-Datei, bzw. in der zugehörigen Klasse für dieses Setting wiederzufinden und zu verwenden.
2. Orientation = "Vertical"
   - Mit dem Orientation-Property wird festgelegt, in welche Richtung die dem StackPanel untergeordnete Elemente neu angeordnet werden, wenn die Fenstergröße angepasst wird. In diesem Fall werden die Elemente Vertikal neu ausgerichtet.
3. Margin
   - Mit dem Margin-Property hat man die Möglichkeit, den Abstand zwischen einem Element und seinen untergeordneten Elementen oder gleichrangigen zu definieren.
4. VerticalAlignemt = "Center"
   - Die im TextBlock enthaltene Inhalte werden vertikal zentriert angeordnet.
5. HorizontalAlignemt = "Center"
   - Die im TextBlock enthaltene Inhalte werden horizontal zentriert angeordnet.

`<NumericUpDown Name="MinNumericUpDown" Value="0.0" FontFamily="Monospace" Margin="5,5,5,0" Cursor="Hand" VerticalAlignment="Center" HorizontalAlignment="Center"/>`
1. NumericUpDown
   - Das NumericUpDown-Control ist eine bearbeitbare numerische Eingabe mit angeschlossenen Auf- und Ab-Drehtasten. Nicht-numerische Zeichen werden in der Eingabe ignoriert. Der Wert kann auch durch Klicken auf die Tasten oder durch Verwendung der Pfeiltasten auf der Tastatur geändert werden. Das Mausrad (sofern vorhanden) ändert ebenfalls den Wert.
2. Name = "MinNumericUpDown"
   - Dem NumericUpDown wird einem Namen mitgegeben, um diesen in der .cs-Datei, bzw. in der zugehörigen Klasse für dieses Setting wiederzufinden und zu verwenden.
3. Value = 0.0
   - Mit dem Property "Value" wird ein Default-Wert zugewiesen. In diesem Fall sieht der User also den Wert "0.0" wenn er das View öffnet.
4. FontFamily = "Monospace"
   - Mit dem FontFamily-Property wird die Schriftart für dieses Control geändert. In diesem Fall auf "Monospace".
5. Margin
   - Mit dem Margin-Property hat man die Möglichkeit, den Abstand zwischen einem Element und seinen untergeordneten Elementen oder gleichrangigen zu definieren.
6. Cursor = "Hand"
   - Mithilfe des Cursor-Properties kann festgelegt werden, welche Form der Mauszeiger des Users haben soll, wenn dieser mit der Maus über den Button hovert. In diesem Fall ist es das der "Hand"-Cursor.
7. VerticalAlignemt = "Center"
   - Die im TextBlock enthaltene Inhalte werden vertikal zentriert angeordnet.
8. HorizontalAlignemt = "Center"
   - Die im TextBlock enthaltene Inhalte werden horizontal zentriert angeordnet.

## Klasse (.cs-Datei) 
Die .cs-Datei, bzw. die Klasse besteht aus der Klasse "SettingNumericUpDownMinMaxView : UserControl", welche widerum aus einigen Unterklassen besteht, welche hier im Detail erklärt werden.
```csharp
public partial class SettingNumericUpDownMinMaxView : UserControl
{
    public string Text { get; set; }
    public string ToolTip { get; set; }
    public decimal MinNumericUpDownValue { get; set; }
    public decimal MinNumericUpDownIncrement { get; set; }
    public decimal MinNumericUpDownMinimum { get; set; }
    public decimal MinNumericUpDownMaximum { get; set; }
    public decimal MaxNumericUpDownValue { get; set; }
    public decimal MaxNumericUpDownIncrement { get; set; }
    public decimal MaxNumericUpDownMinimum { get; set; }
    public decimal MaxNumericUpDownMaximum { get; set; }
}
```
Mit diesen Get- und Set-Methoden werden die Werte, welche im View dargestellt werden, also z.B. das ToolTip der einzelnen Einstellung, oder der Text innerhalb des TextBlocks, erfasst und in globale Variablen geschrieben, welche von hier aus vom restlichen Programm weiterverwendet werden können.
<br><br>

```csharp
    public event EventHandler<NumericUpDownValueChangedEventArgs>? MinNumericUpDownValueChanged;
    public event EventHandler<NumericUpDownValueChangedEventArgs>? MaxNumericUpDownValueChanged;

    public SettingNumericUpDownMinMaxView()
    {
        InitializeComponent();

        MinNumericUpDown.ValueChanged += (sender, e) => MinNumericUpDownValueChanged?.Invoke(sender, e);
        MaxNumericUpDown.ValueChanged += (sender, e) => MaxNumericUpDownValueChanged?.Invoke(sender, e);
    }
```
Das EventHandler-Event dient dazu, das OnClick-Event des Buttons an das restliche Programm weiterzureichen.
Wir stellen hiermit die Möglichkeit dar, diesen Wert von jeder Stelle im Programm zur Verfügung zu stellen. 
<br><br>

```csharp
    private void UpdateToolTipVisibility()
    {
        if (string.IsNullOrEmpty(ToolTip))
            MinMaxTextBlock.Cursor = new Cursor(StandardCursorType.Arrow);
        else
            MinMaxTextBlock.Cursor = new Cursor(StandardCursorType.Help);
        Avalonia.Controls.ToolTip.SetTip(MinMaxTextBlock, ToolTip);
        try
        {
            Avalonia.Controls.ToolTip.SetIsOpen(MinMaxTextBlock, !string.IsNullOrEmpty(ToolTip));
        }
        catch { }
    }
}
```
Mit UpdateToolTipVisibility wird geprüft, ob das jeweilige ToolTip einen Wert enthält, oder nicht. Je nachdem wird der Cursor des Mauszeigers geändert.
<br><br>
