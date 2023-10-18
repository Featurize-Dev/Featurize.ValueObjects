using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Identifiers;
using Featurize.ValueObjects.Identifiers.Behaviours;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Featurize.ValueObjects.Tests;

public sealed class JsonValueObjectConverterFactory_specs
{
    [TestCase(typeof(Country))]
    [TestCase(typeof(EmailAddress))]
    [TestCase(typeof(Encrypted<string>))]
    [TestCase(typeof(Initials))]
    [TestCase(typeof(Id<GuidBehaviour>))]
    public void CanConvert(Type type)
    {
        new ValueObjectJsonConverter().CanConvert(type).Should().BeTrue();
    }
}
