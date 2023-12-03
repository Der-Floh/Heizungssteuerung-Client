# IsolationClasses

Das Enum IsolationClasses enthält sämtliche Isolationsklassen, die ein Gebäude haben kann.

```c#
public enum IsolationClasses
{
    AA,
    A,
    B,
    C,
    D,
    E,
    F,
    G,
    H,
}
```

Um den wirklichen dezimalen Wert der Isolation zu erhalten, kann die `ToValue()` Funktion von IsolationClasses aufgerufen werden. (Siehe [EnumExtensions](./enum-extensions))