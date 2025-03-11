namespace Featurize.ValueObjects.Measurements;

public readonly partial record struct Unit
{
    /// <summary>Metric Area Units</summary>
    public static Unit SquareMillimeter => new("mm²", 0.000001, UnitType.Area, 1);
    public static Unit SquareCentimeter => new("cm²", 0.0001, UnitType.Area, 1);
    public static Unit SquareDecimeter => new("dm²", 0.01, UnitType.Area, 1);
    public static Unit SquareMeter => new("m²", 1, UnitType.Area, 1);
    public static Unit Are => new("are", 100, UnitType.Area, 1);
    public static Unit Decare => new("decare", 1000, UnitType.Area, 1);
    public static Unit Hectare => new("ha", 10000, UnitType.Area, 1);
    public static Unit SquareKilometer => new("km²", 1000000, UnitType.Area, 1);

    /// <summary>Imperial & US Customary Area Units</summary>
    public static Unit SquareInch => new("in²", 0.00064516, UnitType.Area, 1);
    public static Unit SquareFoot => new("ft²", 0.092903, UnitType.Area, 1);
    public static Unit SquareYard => new("yd²", 0.836127, UnitType.Area, 1);
    public static Unit SquareMile => new("mi²", 2589988.11, UnitType.Area, 1);
    public static Unit Acre => new("acre", 4046.86, UnitType.Area, 1);
    public static Unit Rood => new("rood", 1011.71, UnitType.Area, 1); // 1/4 acre

    /// <summary>Asian & Historical Area Units</summary>
    public static Unit Ping => new("ping", 3.30579, UnitType.Area, 1); // Traditional Taiwanese/Japanese unit
    public static Unit TatamiMat => new("tatami", 1.62, UnitType.Area, 1); // Traditional Japanese room size
    public static Unit Mu => new("mu", 666.67, UnitType.Area, 1); // Traditional Chinese farmland measurement
    public static Unit Dunam => new("dunam", 1000, UnitType.Area, 1); // Used in Middle Eastern land measurement
    public static Unit Rai => new("rai", 1600, UnitType.Area, 1); // Thai unit of land measurement

    /// <summary>Scientific & Large-Scale Area Units</summary>
    public static Unit Barn => new("barn", 1e-28, UnitType.Area, 1); // Nuclear physics unit
    public static Unit SquareRod => new("square_rod", 25.2929, UnitType.Area, 1); // Used in early land surveying
    public static Unit FootballField => new("football_field", 5351.2, UnitType.Area, 1); // Approximate American football field area

}
