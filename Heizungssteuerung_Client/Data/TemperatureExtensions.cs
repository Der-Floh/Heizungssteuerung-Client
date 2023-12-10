using System;
using System.Collections.Generic;
using System.Linq;

namespace Heizungssteuerung_Client.Data;

public static class TemperatureExtensions
{
    public static double FindNearestTemperature(this IEnumerable<Temperature> temperatures, double targetTemperature)
    {
        if (temperatures == null || temperatures.Count() == 0)
            throw new ArgumentException("Temperature array cannot be null or empty.");

        Temperature[] temperatureArray = temperatures.ToArray();

        double nearestTemperature = temperatureArray[0].XValue;
        double minDifference = Math.Abs(targetTemperature - nearestTemperature);
        double nearestTemp = temperatureArray[0].YValue;

        foreach (Temperature temperature in temperatureArray)
        {
            double difference = Math.Abs(targetTemperature - temperature.XValue);

            if (difference < minDifference)
            {
                minDifference = difference;
                nearestTemperature = temperature.XValue;
                nearestTemp = temperature.YValue;
            }
        }

        return nearestTemp;
    }
}
