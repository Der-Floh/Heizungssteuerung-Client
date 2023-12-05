# SettingNumericUpDownSingleView

Die von uns selbst definierte Einstellung "setting-numericupdown-single-view" besteht aus einer .axaml-Datei, welche den eigentlichen View darstellt, sowie aus einer zugehörigen .cs-Datei, welche die Klasse des zugehörigen Views darstellt.

Bei dieser Einstellung handelt es sich entsprechend des Namens um eine Einstellung, welche ein NumericUpdown-Control enthält.

## User Control (.axaml-Datei)

In der .axaml-Datei definieren wir das eigentliche Aussehen des Settings in XML.

```XML
<WrapPanel Orientation="Horizontal" Background="#3B3B3B" VerticalAlignment="Stretch" HorizontalAlignment="Stretch">
	<TextBlock Name="SingleTextBlock" Padding="10,0,10,0" VerticalAlignment="Center" HorizontalAlignment="Center"/>
	<NumericUpDown Name="SingleNumericUpDown" Value="0.0" FontFamily="Monospace" Cursor="Hand" Margin="5" VerticalAlignment="Center" HorizontalAlignment="Center"/>
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
Die .cs-Datei, bzw. die Klasse besteht aus der Klasse "SettingNumericUpDownSingleView : UserControl", welche widerum aus einigen Unterklassen besteht, welche hier im Detail erklärt werden.
```csharp
public partial class SettingNumericUpDownSingleView : UserControl
{
    public string? Text { get => _text; set { _text = value; SingleTextBlock.Text = value; } }
    private string? _text;

    public string? ToolTip { get => _toolTip; set { _toolTip = value; UpdateToolTipVisibility(); } }
    private string? _toolTip;

    public decimal Value { get => _value; set { _value = value; SingleNumericUpDown.Value = value; } }
    private decimal _value;

    public decimal Increment { get => _increment; set { _increment = value; SingleNumericUpDown.Increment = value; } }
    private decimal _increment;

    public decimal Minimum { get => _minimum; set { _minimum = value; SingleNumericUpDown.Minimum = value; } }
    private decimal _minimum;

    public decimal Maximum { get => _maximum; set { _maximum = value; SingleNumericUpDown.Maximum = value; } }
    private decimal _maximum;
}
```
Mit diesen Get- und Set-Methoden werden die Werte, welche im View dargestellt werden, also z.B. das ToolTip der einzelnen Einstellung, oder der Text innerhalb des TextBlocks, erfasst und in globale Variablen geschrieben, welche von hier aus vom restlichen Programm weiterverwendet werden können.
<br><br>

```csharp
    public event EventHandler<NumericUpDownValueChangedEventArgs>? NumericUpDownValueChanged;

    public SettingNumericUpDownSingleView()
    {
        InitializeComponent();

        SingleNumericUpDown.ValueChanged += (sender, e) => NumericUpDownValueChanged?.Invoke(sender, e);

        UpdateToolTipVisibility();
    }
```
Das EventHandler-Event dient dazu, das OnClick-Event des Buttons an das restliche Programm weiterzureichen.
Wir stellen hiermit die Möglichkeit dar, diesen Wert von jeder Stelle im Programm zur Verfügung zu stellen. 
<br><br>

```csharp
    private void UpdateToolTipVisibility()
    {
        if (string.IsNullOrEmpty(ToolTip))
            SingleTextBlock.Cursor = new Cursor(StandardCursorType.Arrow);
        else
            SingleTextBlock.Cursor = new Cursor(StandardCursorType.Help);
        Avalonia.Controls.ToolTip.SetTip(SingleTextBlock, ToolTip);
        try
        {
            Avalonia.Controls.ToolTip.SetIsOpen(SingleTextBlock, !string.IsNullOrEmpty(ToolTip));
        }
        catch { }
    }
}
```
Mit UpdateToolTipVisibility wird geprüft, ob das jeweilige ToolTip einen Wert enthält, oder nicht. Je nachdem wird der Cursor des Mauszeigers geändert.
<br><br>
