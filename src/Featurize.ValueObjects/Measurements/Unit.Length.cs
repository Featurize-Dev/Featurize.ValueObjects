namespace Featurize.ValueObjects.Measurements;

public readonly partial record struct Unit
{
    /// <summary>Metric Length Units</summary>
    public static Unit Nanometer => new("nm", 0.000000001, UnitType.Length, 1);
    public static Unit Micrometer => new("μm", 0.000001, UnitType.Length, 1);
    public static Unit Millimeter => new("mm", 0.001, UnitType.Length, 1);
    public static Unit Centimeter => new("cm", 0.01, UnitType.Length, 1);
    public static Unit Decimeter => new("dm", 0.1, UnitType.Length, 1);
    public static Unit Meter => new("m", 1, UnitType.Length, 1);
    public static Unit Dekameter => new("dam", 10, UnitType.Length, 1);
    public static Unit Hectometer => new("hm", 100, UnitType.Length, 1);
    public static Unit Kilometer => new("km", 1000, UnitType.Length, 1);

    /// <summary>Imperial & US Customary Length Units</summary>
    public static Unit Inch => new("inch", 0.0254, UnitType.Length, 1);
    public static Unit Foot => new("ft", 0.3048, UnitType.Length, 1);
    public static Unit Yard => new("yd", 0.9144, UnitType.Length, 1);
    public static Unit Mile => new("mi", 1609.344, UnitType.Length, 1);
    public static Unit NauticalMile => new("nmi", 1852, UnitType.Length, 1);
    public static Unit Fathom => new("fathom", 1.8288, UnitType.Length, 1); // Used in maritime measurement
    public static Unit Rod => new("rod", 5.0292, UnitType.Length, 1);
    public static Unit Chain => new("chain", 20.1168, UnitType.Length, 1);
    public static Unit League => new("league", 4828.032, UnitType.Length, 1); // Common in literature

    /// <summary>Astronomical & Scientific Length Units</summary>
    public static Unit AstronomicalUnit => new("AU", 149597870700, UnitType.Length, 1); // Average Earth-Sun distance
    public static Unit LightYear => new("ly", 9.4607e15, UnitType.Length, 1); // Distance light travels in a year
    public static Unit Parsec => new("pc", 3.0857e16, UnitType.Length, 1); // Used in astronomy (3.26 light-years)
    public static Unit PlanckLength => new("planck", 1.616255e-35, UnitType.Length, 1); // Theoretical minimum length in physics

    /// <summary>Asian & Historical Length Units</summary>
    public static Unit Hand => new("hand", 0.1016, UnitType.Length, 1); // Used for measuring horses
    public static Unit Cubit => new("cubit", 0.4572, UnitType.Length, 1); // Ancient Egyptian/Biblical unit
    public static Unit Span => new("span", 0.2286, UnitType.Length, 1); // Half a cubit
    public static Unit Smoot => new("smoot", 1.7018, UnitType.Length, 1); // MIT joke measurement
    public static Unit Chi => new("chi", 0.3333, UnitType.Length, 1); // Traditional Chinese unit
    public static Unit Shaku => new("shaku", 0.303, UnitType.Length, 1); // Traditional Japanese unit
    public static Unit Ken => new("ken", 1.818, UnitType.Length, 1); // Traditional Japanese unit
    public static Unit Li => new("li", 500, UnitType.Length, 1); // Traditional Chinese unit (~500m)
}
