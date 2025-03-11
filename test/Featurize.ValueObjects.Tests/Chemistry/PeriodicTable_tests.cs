using Featurize.ValueObjects.Chemistry;

namespace Featurize.ValueObjects.Tests.Chemistry;

public class PeriodicTable_tests
{
    public class TryParse
    {
        [Test]
        public void Should_Return_True_on_Existing()
        {
            var result = PeriodicTable.TryParse("Fe", out var element);
            Assert.True(result);
            Assert.That(PeriodicTable.Iron, Is.EqualTo(element));
        }
    }
}