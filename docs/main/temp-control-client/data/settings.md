# Settings

Die statische Klasse Settings beinhaltet alle Einstellungen und enthält außerdem alle Funktionalität um diese zu Ändern, zu Speichern oder zu Laden.

## Eigenschaften

Settings enthält folgende Eigenschaften:

- `ConcurrentDictionary<string, string> Default` - Dictionary, welches alle Standard Einstellungen beinhaltet
- `ConcurrentDictionary<int, double> UserTemps` - Dictionary, welches die Werte für die Wohlfühltemperaturen beinhaltet

## Funktionen

Settings enthält folgende Funktionen:

- `bool Contains(string name)` - Gibt zurück ob ein bestimmter Schlüssel im `Default` Dictionary vorhanden ist
- `string Get(string name)` - Gibt den Wert zurück, den ein bestimmer Schlüssel im `Default` Dictionary hat
- `void Set(string name, string value)` - Setzt den Wert eines bestimmten Schlüssels im `Default` Dictionary
- `bool Remove(string name)` - Entfernt einen bestimmten Schlüssel mit Wert aus dem `Default` Dictionary
- `async Task Save()` - Speichert die aktuellen Werte im `Default` Dictionary
- `void Load()` - Lädt die gespeicherten Werte in das `Default` Dictionary
- `string ToJson()` - Konvertiert die Werte im `Default` in Json Format
- `void FromJson(string json)` - Konvertiert ein Json Text in das `Default` Dictionary

- `bool ContainsUserTemps(string name)` - Gibt zurück ob ein bestimmter Schlüssel im `UserTemps` Dictionary vorhanden ist
- `string GetUserTemps(string name)` - Gibt den Wert zurück, den ein bestimmer Schlüssel im `UserTemps` Dictionary hat
- `void SetUserTemps(string name, string value)` - Setzt den Wert eines bestimmten Schlüssels im `UserTemps` Dictionary
- `bool RemoveUserTemps(string name)` - Entfernt einen bestimmten Schlüssel mit Wert aus dem `UserTemps` Dictionary
- `async Task SaveUserTemps()` - Speichert die aktuellen Werte im `UserTemps` Dictionary
- `void LoadUserTemps()` - Lädt die gespeicherten Werte in das `UserTemps` Dictionary
- `string ToJsonUserTemps()` - Konvertiert die Werte im `UserTemps` in Json Format
- `void FromJsonUserTemps(string json)` - Konvertiert ein Json Text in das `UserTemps` Dictionary
