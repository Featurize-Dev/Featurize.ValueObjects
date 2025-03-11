namespace Featurize.ValueObjects.Measurements;

public readonly partial record struct Unit
{
    /// <summary>Standard Time Units</summary>
    public static Unit Nanosecond => new("ns", 1e-9, UnitType.Time, 1);
    public static Unit Microsecond => new("μs", 1e-6, UnitType.Time, 1);
    public static Unit Millisecond => new("ms", 1e-3, UnitType.Time, 1);
    public static Unit Second => new("s", 1, UnitType.Time, 1);
    public static Unit Minute => new("min", 60, UnitType.Time, 1);
    public static Unit Hour => new("h", 3600, UnitType.Time, 1);
    public static Unit Day => new("d", 86400, UnitType.Time, 1);
    public static Unit Week => new("wk", 604800, UnitType.Time, 1);
    public static Unit Fortnight => new("fortnight", 1209600, UnitType.Time, 1); // 14 days
    public static Unit Month => new("month", 2628000, UnitType.Time, 1); // Average (30.44 days)
    public static Unit Year => new("yr", 31556952, UnitType.Time, 1); // 365.2425 days (including leap years)
    public static Unit Decade => new("decade", 315569520, UnitType.Time, 1); // 10 years
    public static Unit Century => new("century", 3.1556952e9, UnitType.Time, 1); // 100 years
    public static Unit Millennium => new("millennium", 3.1556952e10, UnitType.Time, 1); // 1000 years

    /// <summary>Astronomical Time Units</summary>
    public static Unit SiderealDay => new("sidereal_day", 86164.1, UnitType.Time, 1); // Based on Earth's rotation
    public static Unit SiderealYear => new("sidereal_year", 31558149.8, UnitType.Time, 1); // Earth's orbit period
    public static Unit LunarMonth => new("lunar_month", 2551442.9, UnitType.Time, 1); // Time between new moons (~29.53 days)
    public static Unit GalacticYear => new("galactic_year", 7.884e15, UnitType.Time, 1); // Sun's orbit around Milky Way (~225M years)

    /// <summary>Scientific & High Precision Time Units</summary>
    public static Unit PlanckTime => new("planck_time", 5.391247e-44, UnitType.Time, 1); // Theoretical smallest time unit
    public static Unit Shake => new("shake", 1e-8, UnitType.Time, 1); // Used in nuclear physics (10 nanoseconds)
    public static Unit Jiffy => new("jiffy", 0.01, UnitType.Time, 1); // Used in computing (varies in physics)
    public static Unit Microfortnight => new("microfortnight", 1.2096, UnitType.Time, 1); // Humorous unit (1.2 seconds)
    //public static Unit Svedberg => new("S", 1e-13, UnitType.Time, 1); // Used in biochemistry for sedimentation rates

}
