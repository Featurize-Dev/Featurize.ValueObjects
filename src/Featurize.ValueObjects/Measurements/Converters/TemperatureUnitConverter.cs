namespace Featurize.ValueObjects.Measurements.Converters;

public class TemperatureUnitConverter : UnitConverter
{
    public override Unit Convert(Unit from, Unit to)
    {
        if (from.IsOfUnit(to))
            return from;

        double valueInCelsius = ConvertToCelsius(from);
        double convertedValue = ConvertFromCelsius(valueInCelsius, to);

        return to.WithValue(convertedValue);
    }

    /// <summary>
    /// Converts a given temperature unit to Celsius.
    /// </summary>
    private static double ConvertToCelsius(Unit from)
    {
        if (from.IsOfUnit(Unit.Celsius))
            return from.Value;
        if (from.IsOfUnit(Unit.Fahrenheit))
            return (from.Value - 32) * 5 / 9;
        if (from.IsOfUnit(Unit.Kelvin))
            return from.Value - 273.15;
        if (from.IsOfUnit(Unit.Rankine))
            return (from.Value - 491.67) * 5 / 9;
        if (from.IsOfUnit(Unit.Delisle))
            return 100 - (from.Value * 2 / 3);
        if (from.IsOfUnit(Unit.Newton))
            return from.Value * (100.0 / 33.0);
        if (from.IsOfUnit(Unit.Réaumur))
            return from.Value * (100.0 / 80.0);
        if (from.IsOfUnit(Unit.Rømer))
            return (from.Value - 7.5) * 40 / 21;

        throw new InvalidOperationException($"Cannot convert from {from.Name}.");
    }

    /// <summary>
    /// Converts a Celsius value to the target temperature unit.
    /// </summary>
    private static double ConvertFromCelsius(double celsius, Unit to)
    {
        if (to.IsOfUnit(Unit.Celsius))
            return celsius;
        if (to.IsOfUnit(Unit.Fahrenheit))
            return (celsius * 9 / 5) + 32;
        if (to.IsOfUnit(Unit.Kelvin))
            return celsius + 273.15;
        if (to.IsOfUnit(Unit.Rankine))
            return (celsius + 273.15) * 9 / 5;
        if (to.IsOfUnit(Unit.Delisle))
            return (100 - celsius) * 3 / 2;
        if (to.IsOfUnit(Unit.Newton))
            return celsius * (33.0 / 100.0);
        if (to.IsOfUnit(Unit.Réaumur))
            return celsius * (80.0 / 100.0);
        if (to.IsOfUnit(Unit.Rømer))
            return (celsius * 21 / 40) + 7.5;

        throw new InvalidOperationException($"Cannot convert to {to.Name}.");
    }
}
