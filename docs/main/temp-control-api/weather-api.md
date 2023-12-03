# WeatherAPI

Die WeatherAPI Klasse beinhaltet die Funktionalitäten bestimmte Wetterdaten wie **Regen** oder **Temperatur** abzurufen.

Sie verwendent dafür das [Weather.NET](https://github.com/eloyespinosa/Weather.NET/) NuGet Package.

Die Wetter API die verwendet wird, wird von [Open Weather Map](https://openweathermap.org/) bereit gestellt.

## Eigenschaften

WeatherAPI enthält folgende Eigenschaften:

- `string APIKey` - Der API Key der benutzt wird um mit der Wetterdaten API zu authentifizieren
- `string City` - Die Stadt von der sämtliche Wetterdaten stammen

## Funktionen

WeatherAPI enthält folgende Funktionen:

- `double GetCurrentTemperature()` - Gibt die aktuelle Außentemperatur als double zurück
- `Weather GetCurrentWeather()` - Gibt das aktuelel Wetter als Weather zurück. (Siehe [Weather](./weather))
- `double GetFutureTemperatures(int forecastCount)` - Gibt zukünftige Temperaturen als array zurück
  - *`int forecastCount` - Anzahl der forecasts. Jeder forecast ist 3 Stunden in der Zukunft*
- `double GetFutureDaysTemperatures(int dayCount)` - Gibt zukünftige Temperaturen als array zurück
  - *`int dayCount` - Anzahl der Tage. Jeder Tag besteht aus 8 forecasts*
- `Weather GetFutureWeather(int forecastCount)` - Gibt zukünftige Wetterdaten als array zurück. (Siehe [Weather](./weather))
  - *`int forecastCount` - Anzahl der forecasts. Jeder forecast ist 3 Stunden in der Zukunft*
- `Weather GetFutureDaysWeather(int dayCount)` - Gibt zukünftige Wetterdaten als array zurück. (Siehe [Weather](./weather))
  - *`int dayCount` - Anzahl der Tage. Jeder Tag besteht aus 8 forecasts*
