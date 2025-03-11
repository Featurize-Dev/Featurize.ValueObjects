namespace Featurize.ValueObjects.Measurements.Converters;

public class TimeUnitConverter : UnitConverter
{
    public override Unit Convert(Unit from, Unit to)
    {
        double newValue = from.Value * (from.Factor / to.Factor);
        return to.WithValue(newValue);
    }
}
