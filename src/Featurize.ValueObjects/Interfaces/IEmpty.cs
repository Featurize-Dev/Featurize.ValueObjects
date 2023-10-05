namespace Featurize.ValueObjects.Interfaces;

public interface IEmpty<TSelf>
    where TSelf : IEmpty<TSelf>
{
    public static abstract TSelf Empty { get; }
    public bool IsEmpty();
}
