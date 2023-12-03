# Wave

Die Wave.cs Datei enthält mehrere kleinere Klassen, die dazu gedacht sind eine Welle zu generieren.

## WaveGenerator

Die WaveGenerator Klasse ist die Generator Klasse und generiert eine Welle von bestimmter Höhe und Breite.

### Eigenschaften

WaveGenerator enthält folgende Eigenschaften:

- `double WaveWidth` - Breite der Welle
- `double WaveHeight` - Höhe der Welle

### Funktionen

WaveGenerator enthält folgende Funktionen:

- `Wave GenerateWave()` - Gibt die generierte Welle in der Breite und Höhe der Eigenschaften zurück. (Siehe [Wave](#wave-1))

## Wave

Die Wave Klasse beinhaltet die Informationen der Welle zu einem bestimmten Zeitpunkt

### Eigenschaften

Wave enthält folgende Eigenschaften:

- `List<WavePoint> WavePoints` - Liste aller Punkte der Welle. (Siehe [WavePoint](#wavepoint))

### Funktionen

Wave enthält folgende Funktionen:

- `Wave GenerateWave(double width, double height, double time, double noOfPoints, double baseSpeed)` - Generiert eine Welle auf basis der mitgegebenen Parameter
  - *`double width`* - Gibt die Breite der Welle an
  - *`double height`* - Gibt die Höhe der Welle an
  - *`double time`* - Zeitpunkt zudem die Welle generiert wird
  - *`double noOfPoints`* - Anzahl der Punkte den die Welle haben soll
  - *`double baseSpeed`* - Geschwindigkeit mit der sich der angegebene Zeitpunkt ändert

## WavePoint

Die Klasse WavePoint beinhaltet die Informationen eines einzelnen Punktes der Welle

### Eigenschaften

WavePoint enthält folgende Eigenschaften:

- `double X` - X Position des Punktes
- `double Y` - Y Position des Punktes

### Funktionen

WavePoint enthält folgende Funktionen:

- `Point ToPoint()` - Gibt den WavePoint als normalen Avalonia Point zurück. (Siehe [Avalonia.Point](https://reference.avaloniaui.net/api/Avalonia/Point/))
