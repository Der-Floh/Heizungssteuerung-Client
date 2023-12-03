# SettingComboBoxSingleView

Die von uns selbst definierte Einstellung "setting-combobox-single-view" besteht aus einer .axaml-Datei, welche den eigentlichen View darstellt, sowie aus einer zugehörigen .cs-Datei, welche die Klasse des zugehörigen Views darstellt.

Bei dieser Einstellung handelt es sich entsprechend des Namens um eine Einstellung, welche eine ComboBox enthält. 

## User Control (.axaml-Datei)

In der .axaml-Datei definieren wir das eigentliche Aussehen des Settings in XML.

```XML
	<WrapPanel Orientation="Horizontal" Background="#3B3B3B" VerticalAlignment="Stretch" HorizontalAlignment="Stretch">
	    <TextBlock Name="SingleTextBlock" Padding="10,0,10,0" VerticalAlignment="Center" HorizontalAlignment="Center"/>
	    <ComboBox Name="SingleComboBox" Cursor="Hand" Margin="5,5,5,5" VerticalAlignment="Center" HorizontalAlignment="Center"/>
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

`<TextBlock Name="SingleTextBlock" IsVisible="False" Padding="10,0,10,0" VerticalAlignment="Center" HorizontalAlignment="Center"/>`
1. TextBlock
   - Der TextBlock ist von seiner Funktionalität her identisch mit einem Label und zeigt einen definierten Text an. 
2. Name = "SingleTextBlock"
   - Dem TextBlock wird einem Namen mitgegeben, um diesen in der .cs-Datei, bzw. in der zugehörigen Klasse für dieses Setting wiederzufinden und zu verwenden.
3. isVisible = "false"
   - Das Property "isVisible" definiert, ob der jeweilige TextBlock eingeblendet, oder ausgeblendet werden soll.
4. Padding
   - Mit dem Property Padding kann die effektive Größe des jeweiligen Elements erhöht werden. In diesem Beispiel also um 10mm Links und Rechts.
5. VerticalAlignemt = "Center"
   - Die im TextBlock enthaltene Inhalte werden vertikal zentriert angeordnet.
6. HorizontalAlignemt = "Center"
   - Die im TextBlock enthaltene Inhalte werden horizontal zentriert angeordnet.
<br><br>

`<ComboBox Name="SingleComboBox" Cursor="Hand" Margin="5,5,5,5" VerticalAlignment="Center" HorizontalAlignment="Center"/>`
1. ComboBox 
   - Die Combo-Box zeigt ein ausgewähltes Element und einen Dropdown-Pfeil, der eine Liste von Optionen anzeigt. Die Länge und Höhe der Combo-Box werden durch das ausgewählte Element bestimmt, es sei denn, es ist anderweitig festgelegt.
2. Name = "SingleComboBox"
   - Der ComboBox wird einem Namen mitgegeben, um diese in der .cs-Datei, bzw. in der zugehörigen Klasse für dieses Setting wiederzufinden und zu verwenden.
3. Cursor = "Hand"
   - Mithilfe des Cursor-Properties kann festgelegt werden, welche Form der Mauszeiger des Users haben soll, wenn dieser mit der Maus über den Button hovert. In diesem Fall ist es das der "Hand"-Cursor.
4. Margin = "5,5,5,5"
   - Mit dem Margin-Property hat man die Möglichkeit, den Abstand zwischen einem Element und seinen untergeordneten Elementen oder gleichrangigen zu definieren.
5. VerticalAlignemt = "Center"
   - Die im TextBlock enthaltene Inhalte werden vertikal zentriert angeordnet.
6. HorizontalAlignemt = "Center"
   - Die im TextBlock enthaltene Inhalte werden horizontal zentriert angeordnet.
<br><br>

## Klasse (.cs-Datei) 
Die .cs-Datei, bzw. die Klasse besteht aus der Klasse "SettingComboBoxSingleView : UserControl", welche widerum aus einigen Unterklassen besteht, welche hier im Detail erklärt werden.
```c#
public partial class SettingComboBoxSingleView : UserControl
{
    public string? Text { get => _text; set { _text = value; SingleTextBlock.Text = value; } }
    private string? _text;

    public string? ToolTip { get => _toolTip; set { _toolTip = value; UpdateToolTipVisibility(); } }
    private string? _toolTip;

    public IEnumerable? Items { get => _items; set { _items = value; SingleComboBox.ItemsSource = value; } }
    private IEnumerable? _items;

    public object? SelectedItem { get => _selectedItem; set { _selectedItem = value; SingleComboBox.SelectedItem = value?.ToString(); } }
    private object? _selectedItem;

    public int SelectedIndex { get => _selectedIndex; set { _selectedIndex = value; SingleComboBox.SelectedIndex = value; } }
    private int _selectedIndex;
}
```
Mit diesen Get- und Set-Methoden werden die Werte, welche im View dargestellt werden, also z.B. das ToolTip der einzelnen Einstellung, oder der Text innerhalb des TextBlocks, erfasst und in globale Variablen geschrieben, welche von hier aus vom restlichen Programm weiterverwendet werden können.
<br><br>

```c#
public event EventHandler<SelectionChangedEventArgs>? SelectionChanged;

public SettingComboBoxSingleView()
{
    InitializeComponent();
    SingleComboBox.SelectionChanged += SingleComboBox_SelectionChanged;
    UpdateToolTipVisibility();
}
```
Das EventHandler-Event dient dazu, das OnClick-Event des Buttons an das restliche Programm weiterzureichen.
Wir stellen hiermit die Möglichkeit dar, diesen Wert von jeder Stelle im Programm zur Verfügung zu stellen. 
<br><br>

```c#
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
```
Mit UpdateToolTipVisibility wird geprüft, ob das jeweilige ToolTip einen Wert enthält, oder nicht. Je nachdem wird der Cursor des Mauszeigers geändert.
<br><br>

```c#
private void SingleComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
{
    _selectedItem = SingleComboBox.SelectedItem;
    _selectedIndex = SingleComboBox.SelectedIndex;
    SelectionChanged?.Invoke(sender, e);
}
```
Mit dieser Funktion stellen wir die Möglichkeit dar, den gewählten Index und das zugehörige Item der ComboBox an das restliche Programm weiterzurreichen.
