namespace Featurize.ValueObjects.Measurements;

public readonly partial record struct Unit
{
    /// <summary>Metric Energy Units</summary>
    public static Unit Joule => new("J", 1, UnitType.Energy, 1);
    public static Unit Kilojoule => new("kJ", 1000, UnitType.Energy, 1);
    public static Unit Megajoule => new("MJ", 1e6, UnitType.Energy, 1);
    public static Unit Gigajoule => new("GJ", 1e9, UnitType.Energy, 1);
    public static Unit Terajoule => new("TJ", 1e12, UnitType.Energy, 1);
    public static Unit WattHour => new("Wh", 3600, UnitType.Energy, 1);
    public static Unit KilowattHour => new("kWh", 3.6e6, UnitType.Energy, 1);
    public static Unit MegawattHour => new("MWh", 3.6e9, UnitType.Energy, 1);
    public static Unit GigawattHour => new("GWh", 3.6e12, UnitType.Energy, 1);
    public static Unit TerawattHour => new("TWh", 3.6e15, UnitType.Energy, 1);

    /// <summary>Imperial & Customary Energy Units</summary>
    public static Unit Calorie => new("cal", 4.184, UnitType.Energy, 1);
    public static Unit Kilocalorie => new("kcal", 4184, UnitType.Energy, 1); // Used in nutrition
    public static Unit BritishThermalUnit => new("BTU", 1055.06, UnitType.Energy, 1);
    public static Unit Therm => new("therm", 105505600, UnitType.Energy, 1); // Used in gas industry
    public static Unit FootPound => new("ft·lbf", 1.35582, UnitType.Energy, 1); // Used in engineering

    /// <summary>Scientific Energy Units</summary>
    public static Unit ElectronVolt => new("eV", 1.60218e-19, UnitType.Energy, 1);
    public static Unit KiloElectronVolt => new("keV", 1.60218e-16, UnitType.Energy, 1);
    public static Unit MegaElectronVolt => new("MeV", 1.60218e-13, UnitType.Energy, 1);
    public static Unit GigaElectronVolt => new("GeV", 1.60218e-10, UnitType.Energy, 1);
    public static Unit TeraElectronVolt => new("TeV", 1.60218e-7, UnitType.Energy, 1);
    public static Unit Hartree => new("Eh", 4.3597447222071e-18, UnitType.Energy, 1); // Used in quantum mechanics

    /// <summary>Large-Scale Energy Units</summary>
    public static Unit BarrelOfOilEquivalent => new("BOE", 6.12e9, UnitType.Energy, 1); // Energy content of a barrel of oil
    public static Unit TonOfTNT => new("ton_TNT", 4.184e9, UnitType.Energy, 1);
    public static Unit KilotonOfTNT => new("kton_TNT", 4.184e12, UnitType.Energy, 1);
    public static Unit MegatonOfTNT => new("Mton_TNT", 4.184e15, UnitType.Energy, 1);

}
