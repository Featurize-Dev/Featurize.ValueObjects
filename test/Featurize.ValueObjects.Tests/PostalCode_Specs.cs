using Featurize.ValueObjects.Formatting;
using Featurize.ValueObjects.RealEstate;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Featurize.ValueObjects.Tests;
public class Postcode_Tests
{
    public class Parse
    {
        
    }

    public class Serialization
    {
       
    }

    public class Deserialization
    {
        

    }

    public class EqualityTests
    {
        [Test]
        public void Should_Be_Equal()
        {
            var p1 = PostalCode.Parse("1234 AB");
            ("1234 AB" == p1).Should().BeTrue();
            (p1 == "1234 AB").Should().BeTrue();
        }

        [Test]
        public void Should_Not_Be_Equal()
        {
            var p1 = PostalCode.Parse("1235 AB");
            ("1234 AB" != p1).Should().BeTrue();
            (p1 != "1234 AB").Should().BeTrue();
        }
    }

    public class To_String
    {
        [Test]
        public void Should_Return_WithoutSpaces()
        {
            var postcode = PostalCode.Parse("1234 AB", PostalCodeFormatInfo.NL);
            var toString = postcode.ToString("C");
            toString.Should().Be("1234AB");
        }

        [Test]
        public void Should_Return_Dutch_Postcode_With_Spaces()
        {
            var postcode = PostalCode.Parse("1234 AB", PostalCodeFormatInfo.NL);
            var toString = postcode.ToString();
            toString.Should().Be("1234 AB");
        }

        [Test]
        public void Should_Return_Unknown_Postcode_Without_Spaces()
        {
            var postcode = PostalCode.Parse("HA3 0JA", PostalCodeFormatInfo.Unknown);
            var toString = postcode.ToString("C");
            toString.Should().Be("HA30JA");
        }

        [Test]
        public void Should_Return_Unknown_Postcode_With_Spaces()
        {
            var postcode = PostalCode.Parse("HA3 0JA", PostalCodeFormatInfo.Unknown);
            var toString = postcode.ToString();
            toString.Should().Be("HA3 0JA");
        }

    }
}


public class PostalcodeFormatInfo_tests
{
    [Test]
    public void Test()
    {
        var infos = typeof(PostalCodeFormatInfo).GetProperties()
            .Where(x => x.PropertyType == typeof(PostalCodeFormatInfo))
            .Select(x => x.GetValue(null, null))
            .Distinct()
            .ToArray();

        var result = typeof(PostalCodeFormatInfo).GetProperty("NL")?.GetValue(null, null);

        var info = PostalCodeFormatInfo.GetInstance(new CultureInfo("nl-NL"));

        result.Should().Be(PostalCodeFormatInfo.NL);
    }
}