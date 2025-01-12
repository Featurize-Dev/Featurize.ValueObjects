using Featurize.ValueObjects.Finance;

namespace Featurize.ValueObjects.Tests.Finacial;
public class Amount_Tests
{
    [Test]
    public void Parsing()
    {
        Currency.Default = Currency.Dollar;

        var created = Amount.Create(20);

        var parsed = Amount.Parse("20");

        var tweeEuro = Amount.One * 2;

        var tweeentwintig = parsed + tweeEuro;
        var veertig = parsed * 2;
        var tien = parsed / 2;

        var currency = Currency.Parse("$");

        var money = Money.Parse("$ 20,00");

        List<Money> all = [Money.Parse("$ 10,00"), Money.Parse("$ 15,00"), Money.Parse("€ 20,00"), Money.Parse("EUR 30,00")];

        var mo = all.Sum(Currency.Euro);

        var mo1 = all.Sum(Currency.Dollar);

        var mon = tweeEuro + Currency.Euro;
    }
}
