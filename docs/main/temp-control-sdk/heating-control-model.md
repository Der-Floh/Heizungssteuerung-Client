# HeatingControlModel

Die Klasse HeatingControlModel ist die Klasse, die alle Funktionalitäten des KI Modells beinhaltet.

## Eigenschaften

HeatingControlModel enthält folgende Eigenschaften:

- `int BatchSize` - Gibt an wie viele Datensätze pro Epoche zum Training verwendet werden
- `int Epochs` - Gibt an wie viele Epochen das Modell trainiert wird
- `float LearningRate` - 
- `string FileName` - Gibt den Namen des Ordners an in dem ein Modell gespeichert wird

## Funktionen

HeatingControlModel enthält folgende Funktionen:

- `float Predict(TrainingDataInput input)` - Gibt die Heizkesseltemperatur zurück, die das KI Modell vorhergesagt hat
  - *`TrainingDataInput input` - Der Input den das KI Modell für die Vorhersage verwendet (Siehe [TrainingDataInput](./training/training-data-input))*
- `void Save()` - Speichert das aktuelle KI Modell in den Standard Pfad mit angegebenen Ordnernamen
- `void Load()` - Lädt ein gespeichertes KI Modell vom Standard Pfad mit angegebenen Ordnernamen
- `NeuralNetworkModel Train()` - Trainiert ein neues KI Modell mit den oben angebenen Werten und gibt dieses zurück (Siehe [NeuralNetworkModel](./neural-network-model))
- `float CalcAccuracy(int dataSets)` - Rechnet die durchschnittliche Abweichung des Modells aus und gibt diese zurück
  - *`int dataSets` - Anzahl der Datensätze die für die durchschnittliche Abweichung verwendet werden*
