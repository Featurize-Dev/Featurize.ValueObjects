using Featurize.ValueObjects.Measurements;

namespace Featurize.ValueObjects.Tests.Chemistry;

public class Unit_Tests
{
    public class Parse
    {
        [Test]
        public void ValidUnitString_ReturnsCorrectUnit()
        {
            var unit = Unit.Parse("5 m");
            Assert.That("m", Is.EqualTo(unit.Name));
            Assert.That(5, Is.EqualTo((double)unit));
        }
    }

    public class TryParse
    {
        
        [Test]
        public void ValidUnitString_ReturnsTrueAndCorrectUnit()
        {
            bool success = Unit.TryParse("10 km", out var unit);
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.True);
                Assert.That("km", Is.EqualTo(unit.Name));
                Assert.That(10, Is.EqualTo((double)unit));
            });
        }

        [Test]
        public void InvalidUnitString_ReturnsFalse()
        {
            bool success = Unit.TryParse("invalid", out var unit);
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.False);
                Assert.That(Unit.Unknown, Is.EqualTo(unit));
            });
        }
    }

    public class Add
    {
        [Test]
        public void SameTypeUnits_ReturnsCorrectSum()
        {
            Unit unit1 = 3 * Unit.Meter;
            Unit unit2 = 2 * Unit.Meter;
            Unit result = unit1 + unit2;

            Assert.That(5, Is.EqualTo((double)result));
            Assert.That("m", Is.EqualTo(result.Name));
        }
    }

    public class ConvertTo
    {
        [Test]
        public void Lenght()
        {
            var distance = 5 + Unit.Kilometer;
            var meters = distance.ConvertTo(Unit.Meter);

            Assert.That(meters, Is.EqualTo(5000 + Unit.Meter));
        }

        [Test]
        public void Weight()
        {
            var weight = 5 + Unit.Kilogram;
            var grams = weight.ConvertTo(Unit.Gram);
            Assert.That(5000 + Unit.Gram, Is.EqualTo(grams));
        }

        [Test]
        public void Temperature()
        {
            var temperature = 0 + Unit.Celsius;
            var fahrenheit = temperature.ConvertTo(Unit.Fahrenheit);
            var kelvin = temperature.ConvertTo(Unit.Kelvin);
            var rankine = temperature.ConvertTo(Unit.Rankine);
            var delisle = temperature.ConvertTo(Unit.Delisle);
            var newton = temperature.ConvertTo(Unit.Newton);
            
            Assert.Multiple(() =>
            {
                Assert.That(32 + Unit.Fahrenheit, Is.EqualTo(fahrenheit));
                Assert.That(273.15 + Unit.Kelvin, Is.EqualTo(kelvin));
                Assert.That(491.66999999999996 + Unit.Rankine, Is.EqualTo(rankine));
                Assert.That(150 + Unit.Delisle, Is.EqualTo(delisle));
                Assert.That(0 + Unit.Newton, Is.EqualTo(newton));
            });
        }
    }

    public class EqualsWithConversion()
    {
        [Test]
        public void SameTypeUnits_ReturnsTrue()
        {
            var unit1 = 5 + Unit.Meter;
            var unit2 = 5000 + Unit.Millimeter;
            Assert.That(unit1.EqualsWithConversion(unit2), Is.True);
        }
    }
}

public class UnitProvider_Test
{
    public class TryGetUnit
    {
        [Test]
        public void ValidUnitName_ReturnsTrueAndCorrectUnit()
        {
            var provider = UnitProvider.Instance;
            bool success = provider.TryGetUnit("m", out var unit);
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.True);
                Assert.That("m", Is.EqualTo(unit.Name));
                Assert.That(1, Is.EqualTo((double)unit));
            });
        }
        [Test]
        public void InvalidUnitName_ReturnsFalse()
        {
            var provider = UnitProvider.Instance;
            bool success = provider.TryGetUnit("invalid", out var unit);
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.False);
                Assert.That(Unit.Unknown, Is.EqualTo(unit));
            });
        }
    }
}