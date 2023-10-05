namespace Featurize.ValueObjects.Interfaces;

public interface IUnknown<TSelf>
    where TSelf : IUnknown<TSelf>
{
    public static abstract TSelf Unknown { get; }
}
