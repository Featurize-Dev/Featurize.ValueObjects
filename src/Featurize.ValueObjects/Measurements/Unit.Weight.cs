namespace Featurize.ValueObjects.Measurements;

public readonly partial record struct Unit
{
    /// <summary>Metric Weight Units</summary>
    public static Unit Microgram => new("μg", 0.000001, UnitType.Weight, 1);
    public static Unit Milligram => new("mg", 0.001, UnitType.Weight, 1);
    public static Unit Centigram => new("cg", 0.01, UnitType.Weight, 1);
    public static Unit Decigram => new("dg", 0.1, UnitType.Weight, 1);
    public static Unit Gram => new("g", 1, UnitType.Weight, 1);
    public static Unit Dekagram => new("dag", 10, UnitType.Weight, 1);
    public static Unit Hectogram => new("hg", 100, UnitType.Weight, 1);
    public static Unit Kilogram => new("kg", 1000, UnitType.Weight, 1);
    public static Unit MetricTon => new("metric_ton", 1000000, UnitType.Weight, 1);

    /// <summary>Imperial & US Customary Weight Units</summary>
    public static Unit Ounce => new("oz", 28.3495, UnitType.Weight, 1);
    public static Unit Pound => new("lb", 453.592, UnitType.Weight, 1);
    public static Unit Stone => new("st", 6350.29, UnitType.Weight, 1);
    public static Unit ShortTon => new("short_ton", 907184.74, UnitType.Weight, 1); // US Ton (2000 lbs)
    public static Unit LongTon => new("long_ton", 1016046.91, UnitType.Weight, 1); // UK Ton (2240 lbs)
    public static Unit Grain => new("grain", 0.06479891, UnitType.Weight, 1);
    public static Unit Dram => new("dram", 1.7718451953125, UnitType.Weight, 1);
    public static Unit Quarter => new("quarter", 11339.8, UnitType.Weight, 1); // 1 quarter = 1/4 long ton
    public static Unit Hundredweight => new("cwt", 50802.34544, UnitType.Weight, 1); // 1 hundredweight = 112 pounds (UK)
    public static Unit TroyOunce => new("troy_oz", 31.1034768, UnitType.Weight, 1); // Used in gold, silver, etc.
    public static Unit TroyPound => new("troy_lb", 373.2417216, UnitType.Weight, 1);

    /// <summary>Asian Traditional Weight Units</summary>
    public static Unit Tael => new("tael", 37.5, UnitType.Weight, 1); // Common in China/Hong Kong/Taiwan
    public static Unit Catty => new("catty", 604.79, UnitType.Weight, 1); // Common in China, HK, Singapore
    public static Unit Picul => new("picul", 60479, UnitType.Weight, 1); // Used in Southeast Asia

    /// <summary>Miscellaneous & Rare Weight Units</summary>
    public static Unit Carat => new("carat", 0.2, UnitType.Weight, 1); // Used for gemstones
    public static Unit Slug => new("slug", 14593.903, UnitType.Weight, 1); // Used in physics (force/mass)
    public static Unit AtomicMassUnit => new("amu", 1.66053906660e-24, UnitType.Weight, 1); // Used in physics & chemistry

}
