namespace Featurize.ValueObjects.Measurements.Converters;

public class LengthUnitConverter : UnitConverter
{
    public override Unit Convert(Unit from, Unit to)
    {
        double newValue = from.Value * (from.Factor / to.Factor);
        return to.WithValue(newValue);
    }
}
