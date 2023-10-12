using FluentAssertions;
using System.Text.Json;

namespace Featurize.ValueObjects.Tests;
internal class Country_Specs
{
    [Test]
    public void all_should_have_all_known_countries()
    {
        Country.All.Should().HaveCount(249);
    }

    [Test]
    public void country_parse_sccessfull()
    {
        var country = Country.Parse("NLD");

        country.Name.Should().Be("Netherlands");
        country.ISO2.Should().Be("NL");
        country.ISO3.Should().Be("NLD");
        country.Region.Should().Be("Europe");
        country.SubRegion.Should().Be("Western Europe");
    }

    [Test]
    public void country_can_be_unknown()
    {
        var country = Country.Parse("?");

        country.Should().BeEquivalentTo(Country.Unknown);
    }

    [Test]
    public void country_can_be_Empty()
    {
        var country = Country.Parse("");

        country.Should().BeEquivalentTo(Country.Empty);
        country.IsEmpty().Should().BeTrue();
    }

    [Test]
    [TestCase("I3", "NLD")]
    [TestCase("I2", "NL")]
    [TestCase("c", "528")]
    [TestCase("n", "Netherlands")]
    public void ToString_with_format(string format, string expected)
    {
        var country = Country.Parse("NLD");

        var value = country.ToString(format);

        value.Should().Be(expected);
    }

    [Test]
    public void can_be_serialized()
    {
        var country = Country.Parse("NLD");

        var result = JsonSerializer.Serialize(country);

        result.Should().Be($"\"{country.ISO3}\"");
    }

    [Test]
    public void can_be_deserialized()
    {
        var country = "\"NLD\"";

        var result = JsonSerializer.Deserialize<Country>(country);

        result.Should().BeEquivalentTo(Country.Parse("NLD"));
    }
}
