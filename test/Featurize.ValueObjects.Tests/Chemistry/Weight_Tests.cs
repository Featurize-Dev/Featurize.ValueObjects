using Featurize.ValueObjects.Metric;
using FluentAssertions;

namespace Featurize.ValueObjects.Tests.Chemistry;
public class Weight_Tests
{
    public class TryParse
    {
        
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
