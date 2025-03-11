namespace Featurize.ValueObjects.Measurements.Converters;

public class UnknownUnitConverter : UnitConverter
{
    public override Unit Convert(Unit from, Unit to)
    {
        throw new InvalidOperationException($"Cannot convert unknown unit type {from.Type}.");
    }
}