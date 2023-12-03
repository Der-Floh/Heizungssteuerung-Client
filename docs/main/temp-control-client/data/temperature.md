# Temperature

Die Temperature Klasse ist eine darstellung der Daten, die eine Position und zugehörig zu dieser einen bestimmten Wert haben.

Die Klasse wird verwendet um die Informationen der einzelnen Regler des [UserTempPickerViews](../views/user-temp-picker-view.md) zu speichern.

## Eigenschaften

Temperature enthält folgende Eigenschaften:

- `Guid Id` - Id der Temperature
- `double X` - X Koordinate der Temperature
- `double XValue` - X Wert der Koordinate
- `double Y` - Y Koordinate der Temperature
- `double YValue` - Y Wert der Koordinate
- `double Radius` - Radius des Reglers (Visuell)

## Events

Temperature enthält folgende Events:

- `PropertyChangedEvent` - Event dass ausgeführt wird sobald eine der oben gennanten Eigenschaften verändert wird
