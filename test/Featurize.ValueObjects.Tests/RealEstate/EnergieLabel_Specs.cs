using Featurize.ValueObjects.RealEstate;
using FluentAssertions;

namespace Featurize.ValueObjects.Tests.RealEstate;
internal class EnergieLabel_Specs
{
    public class Create
    {
        [Test]
        public void Should_Be_As_Expected()
        {
            var A4 = Energielabel.Parse("A++++");
            var A3 = Energielabel.Parse("A+++");
            var A2 = Energielabel.Parse("A++");
            var A1 = Energielabel.Parse("A+");
            var A = Energielabel.Parse("A");
            var B = Energielabel.Parse("B");
            var C = Energielabel.Parse("C");
            var D = Energielabel.Parse("D");
            var E = Energielabel.Parse("E");
            var F = Energielabel.Parse("F");
            var G = Energielabel.Parse("G");
            var Empty = Energielabel.Parse(string.Empty);

            A4.Should().Be(Energielabel.A4);
            A3.Should().Be(Energielabel.A3); 
            A2.Should().Be(Energielabel.A2);
            A1.Should().Be(Energielabel.A1);
            A.Should().Be(Energielabel.A);
            B.Should().Be(Energielabel.B);
            C.Should().Be(Energielabel.C);
            D.Should().Be(Energielabel.D);
            E.Should().Be(Energielabel.E);
            F.Should().Be(Energielabel.F);
            G.Should().Be(Energielabel.Unknown);
            Empty.Should().Be(Energielabel.Empty);


        }
    }

}
