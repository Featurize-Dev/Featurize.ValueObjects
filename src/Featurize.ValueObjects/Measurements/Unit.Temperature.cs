namespace Featurize.ValueObjects.Measurements;

public readonly partial record struct Unit
{
    /// <summary>Metric Temperature Units</summary>
    public static Unit Kelvin => new("K", 1, UnitType.Temperature, 1);
    public static Unit Celsius => new("°C", 1, UnitType.Temperature, 1);
    public static Unit Fahrenheit => new("°F", 1, UnitType.Temperature, 1);
    public static Unit Rankine => new("°R", 1, UnitType.Temperature, 1);
    public static Unit Delisle => new("°De", 1, UnitType.Temperature, 1);
    public static Unit Newton => new("°N", 1, UnitType.Temperature, 1);
    public static Unit Réaumur => new("°Ré", 1, UnitType.Temperature, 1);
    public static Unit Rømer => new("°Rø", 1, UnitType.Temperature, 1);
    
}
