using Featurize.ValueObjects.Metric;
using FluentAssertions;

namespace Featurize.ValueObjects.Tests.Chemistry;
public class Weight_Tests
{
    public class TryParse
    {
        [Test]
        public void Should_Return_True_on_Existing()
        {
            var m = MetricSystem.Length.Metre * 10;
            var dm = m.ConvertTo(MetricSystem.Length.Decimetre);
            var cm = m.ConvertTo(MetricSystem.Length.Centimetre);
            var km = MetricSystem.Length.Kilometre * 20;

            var lenght = MetricSystem.Length.Metre * 20;

            var len = km / 20;

            var gram = MetricSystem.Mass.Gram * 20;

            var result = km * (m * 20);
            
            var m1 = cm.ConvertTo(MetricSystem.Length.Metre);

            var toKilo = m.ConvertTo(MetricSystem.Length.Kilometre);
            var toBase = km.ConvertTo(MetricSystem.Length.Metre);

            Assert.That(toKilo, Is.EqualTo(km));
            Assert.That(toBase, Is.EqualTo(m));

        }

        [Test]
        public void MetricTon_Tests()
        {
            var km20 = MetricSystem.Length.Kilometre * 20;

            var distance = Unit.Parse("20 km");
            var distance2 = Unit.Parse("200 m");

            var weight = Unit.Parse("300 kg");

            var result = MetricSystem.TryParse("20 km", null, out var km);

            Assert.That(km20, Is.EqualTo(km));
        }
    }

    public class ImperialSystem_Tests
    {
        [Test]
        public void Feet()
        {
            var yard = ImperialSystem.Yard;
            var feet3 = ImperialSystem.Foot * 3;

            var result = yard.ConvertTo(ImperialSystem.Foot);


            Assert.That(feet3, Is.EqualTo(result));
        }
    }
}
