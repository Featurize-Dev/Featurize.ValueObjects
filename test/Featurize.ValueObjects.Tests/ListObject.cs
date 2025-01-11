using FluentAssertions.Formatting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Featurize.ValueObjects.Tests;
internal class Verbouwingen_test
{
    [Test]
    public void creation_tests()
    {
        var v = VerbouwingCollection.Create([
            new Meerwerk(200_000), 
            new AanlegTuin(20_000, 20_000)]
        );

        var options = new JsonSerializerOptions();
        options.WriteIndented = true;
        var result = JsonSerializer.Serialize(v, options);

        v.Add(new Zonnenpanelen(20_000));

        var result2 = JsonSerializer.Serialize(v, options);
    }
}

public interface Verbouwing
{
    public string Name { get; }
    public decimal Amount { get; }
}
public record Verbouwing<TSelf>(Func<TSelf, decimal> calc) : Verbouwing
    where TSelf : Verbouwing<TSelf>
{
    public string Name => typeof(TSelf).Name;

    public decimal Amount => calc((TSelf)this);
}

public record Meerwerk(decimal Verbouwing)
        : Verbouwing<Meerwerk>(x => x.Verbouwing);
public record AanlegTuin(decimal KostenTuinman, decimal KostenTuinCentrum)
    : Verbouwing<AanlegTuin>(x => x.KostenTuinman + x.KostenTuinCentrum);
public record Zonnenpanelen(decimal Kostenpanelen)
    : Verbouwing<Zonnenpanelen>(x => x.Kostenpanelen * 0.80m);

public class VerbouwingCollection
{
    private IEnumerable<Verbouwing> _verbouwingen = [];
    private VerbouwingCollection() {}  
    public static VerbouwingCollection Create(IEnumerable<Verbouwing> verbouwingen)
    {
        return new VerbouwingCollection()
        {
            _verbouwingen = verbouwingen.ToList(),
        };
    }

    public void Add(Verbouwing verbouwing)
        => _verbouwingen = _verbouwingen.Concat([verbouwing]);

    [JsonExtensionData]
    public Dictionary<string, JsonElement> Data =>
        _verbouwingen.ToDictionary(x => x.Name, y => JsonSerializer.SerializeToElement(y.Amount));

    public decimal Totaal => _verbouwingen.Sum(x => x.Amount);
}
