# NeuralNetworkModel

Die Klasse NeuralNetworkModel beinhaltet alle Informationen eines Batch laufs, um daraus die KI zu trainieren.

## Eigenschaften

NeuralNetworkModel enthält folgende Eigenschaften:

- `Tensor X` - Input Tensor
- `Tensor Y` - Output Tensor
- `Tensor Prediction` - Prediction Tensor der KI
- `Tensor Cost` - Ausgerechnete Kosten der Prediction
- `Operation Optimizer` - Der verwendete Optimizer der die Kosten minimiert
