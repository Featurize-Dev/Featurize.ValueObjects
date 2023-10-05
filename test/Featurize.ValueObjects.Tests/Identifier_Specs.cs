using Featurize.ValueObjects.Identifiers;
using Featurize.ValueObjects.Identifiers.Behaviours;
using FluentAssertions;
using System.Text.Json;

namespace Featurize.ValueObjects.Tests;

public class HydoBehaviour : HidBehaviour
{
    //public override string Name => "TEST";
}

public class Identifier_Specs
{
    public class Parse
    {
        [Test]
        public void hid_should_be_able_to_create()
        {
            var id = Id<HydoBehaviour>.Next();

            id.ToString().Should().Contain("HYDO");
        }

        [Test]
        public void guid_should_be_able_to_parse()
        {
            var guid = Guid.NewGuid();

            var id = Id<GuidBehaviour>.Create(guid);
            var second = Id<GuidBehaviour>.Parse(guid.ToString());

            id.Should().Be(second);
        }

        [Test]
        public void guid_should_throw_if_invalid()
        {
            var parse = () => Id<GuidBehaviour>.Parse("jlaskjdla");
            parse.Should().Throw<FormatException>();
        }

        [Test]
        public void guid_create_should_throw_if_invalid()
        {
            var parse = () => Id<GuidBehaviour>.Create("jlaskjdla");
            parse.Should().Throw<NotSupportedException>();
        }

        [Test]
        public void int32_should_throw_if_invalid()
        {
            var parse = () => Id<Int32Behaviour>.Parse("jlaskjdla");
            parse.Should().Throw<FormatException>();
        }

        [Test]
        public void int32_create_should_throw_if_invalid()
        {
            var parse = () => Id<Int32Behaviour>.Create("jlaskjdla");
            parse.Should().Throw<NotSupportedException>();
        }

        [Test]
        public void int_should_be_able_to_parse()
        {
            var id = Id<Int32Behaviour>.Parse("1");
            id.Should().Be(Id<Int32Behaviour>.Create(1));
        }
    }

    public class Serializer
    {
        [Test]
        public void should_serialize()
        {
            var original = Id<HydoBehaviour>.Next();
            var result = JsonSerializer.Serialize(original);

            var id = JsonSerializer.Deserialize<Id<HydoBehaviour>>(result);

            id.Should().Be(original);
        }
    }
}