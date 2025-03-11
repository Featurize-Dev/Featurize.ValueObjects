namespace Featurize.ValueObjects.Measurements;

public readonly partial record struct Unit
{
    /// <summary>Metric Volume Units</summary>
    public static Unit CubicMillimeter => new("mm³", 0.000000001, UnitType.Volume, 1);
    public static Unit CubicCentimeter => new("cm³", 0.001, UnitType.Volume, 1);
    public static Unit Milliliter => new("ml", 0.001, UnitType.Volume, 1);
    public static Unit Centiliter => new("cl", 0.01, UnitType.Volume, 1);
    public static Unit Deciliter => new("dl", 0.1, UnitType.Volume, 1);
    public static Unit Liter => new("l", 1, UnitType.Volume, 1);
    public static Unit Decaliter => new("dal", 10, UnitType.Volume, 1);
    public static Unit Hectoliter => new("hl", 100, UnitType.Volume, 1);
    public static Unit Kiloliter => new("kl", 1000, UnitType.Volume, 1);
    public static Unit CubicMeter => new("m³", 1000, UnitType.Volume, 1);

    /// <summary>Imperial & US Customary Volume Units</summary>
    public static Unit FluidOunce => new("fl_oz", 0.0295735, UnitType.Volume, 1);
    public static Unit Cup => new("cup", 0.236588, UnitType.Volume, 1);
    public static Unit Pint => new("pint", 0.473176, UnitType.Volume, 1);
    public static Unit Quart => new("quart", 0.946353, UnitType.Volume, 1);
    public static Unit Gallon => new("gallon", 3.78541, UnitType.Volume, 1);
    public static Unit Barrel => new("barrel", 158.987, UnitType.Volume, 1);
    public static Unit CubicInch => new("in³", 0.0163871, UnitType.Volume, 1);
    public static Unit CubicFoot => new("ft³", 28.3168, UnitType.Volume, 1);
    public static Unit CubicYard => new("yd³", 764.555, UnitType.Volume, 1);
    public static Unit Peck => new("peck", 8.80977, UnitType.Volume, 1); // Dry volume measure
    public static Unit Bushel => new("bushel", 35.2391, UnitType.Volume, 1); // Dry volume measure

    /// <summary>Scientific & Astronomical Volume Units</summary>
    public static Unit CubicKilometer => new("km³", 1000000000000, UnitType.Volume, 1);
    public static Unit AcreFoot => new("acre_ft", 1233.48, UnitType.Volume, 1); // Used in large-scale water measurement
    public static Unit Megaliter => new("ML", 1000000, UnitType.Volume, 1);
    public static Unit Gigaliter => new("GL", 1000000000, UnitType.Volume, 1);
    public static Unit Teaspoon => new("tsp", 0.00492892, UnitType.Volume, 1);
    public static Unit Tablespoon => new("tbsp", 0.0147868, UnitType.Volume, 1);
    public static Unit Drop => new("drop", 0.00005, UnitType.Volume, 1); // Pharmaceutical measure
    public static Unit Jigger => new("jigger", 0.0443603, UnitType.Volume, 1); // Cocktail measure
    public static Unit Dash => new("dash", 0.000621, UnitType.Volume, 1); // Cooking measure
    public static Unit Pinch => new("pinch", 0.000308, UnitType.Volume, 1); // Cooking measure
    public static Unit Shot => new("shot", 0.044, UnitType.Volume, 1); // Common bar measure

}

