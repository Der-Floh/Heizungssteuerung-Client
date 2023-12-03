# TrainingDataCreator

Die Klasse TrainingDataCreator enthält alle Funktionalitäten, die notwendig sind um die Trainingsdaten für die KI zu erzeugen.

## Eigenschaften

TrainingDataCreator enthält folgende Eigenschaften:

- `int DecimalPlaces` - Anzahl der Dezimalstellen auf die gerundet werden soll
- `TemperatureRange TemperatureRangeUser` - Temperaturbereich in welchem die Wohlfühltemperatur liegen soll. (Siehe [TemperatureRange](./temperature-range))
- `TemperatureRange TemperatureRangeOuterDay` - Temperaturbereich in welchem die Außentemperatur liegen soll. (Siehe [TemperatureRange](./temperature-range))
- `TemperatureRange TemperatureRangeChangeNight` - Temperaturbereich um wie viel Grad sich die Temperatur Nachts ändert. (Siehe [TemperatureRange](./temperature-range))

## Funktionen

TrainingDataCreator enthält folgende Funktionen:

- `TrainingDataItem[] GenerateTrainingData(int amount)` - Gibt ein array von generierten [TrainingDataItem](./training-data-item) zurück
  - *`int amount`* - Anzahl der zu generierenden Datensätze
- `TrainingDataInput CreateInput()` - Generiert einen zufälligen (im angegebenen Bereich) [TrainingDataInput](./training-data-input) und gibt diesen zurück
- `TrainingDataOutput CreateOutput(TrainingDataInput input)` - Generiert den zu dem input passenden [TrainingDataOutput](./training-data-output) und gibt diesen zurück
