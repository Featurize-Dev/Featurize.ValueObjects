using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Featurize.ValueObjects.Measurements.Converters;

/// <summary>
/// Base class for unit conversion logic.
/// </summary>
public abstract class UnitConverter
{
    /// <summary>
    /// Converts a unit to another unit of the same type.
    /// </summary>
    /// <param name="from">The unit to convert from.</param>
    /// <param name="to">The unit to convert to.</param>
    /// <returns>A new unit with the converted value.</returns>
    public abstract Unit Convert(Unit from, Unit to);

    /// <summary>
    /// Retrieves the appropriate converter for a unit type.
    /// </summary>
    /// <returns>An instance of <see cref="UnitConverter"/> for the given unit type.</returns>
    internal static UnitConverter GetConverter(UnitType type)
    {
        return type switch
        {
            UnitType.Length => new LengthUnitConverter(),
            UnitType.Weight => new WeightUnitConverter(),
            UnitType.Volume => new VolumeUnitConverter(),
            UnitType.Temperature => new TemperatureUnitConverter(),
            UnitType.Time => new TimeUnitConverter(),
            UnitType.Area => new AreaUnitConverter(),
            UnitType.Energy => new EnergyUnitConverter(),
            _ => new UnknownUnitConverter()
        };
    }
}
