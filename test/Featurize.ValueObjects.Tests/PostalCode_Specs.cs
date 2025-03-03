using Featurize.ValueObjects.Formatting;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
            var p1 = Postcode.Parse("1234 AB");
            ("1234 AB" == p1).Should().BeTrue();
            (p1 == "1234 AB").Should().BeTrue();
        }

        [Test]
        public void Should_Not_Be_Equal()
        {
            var p1 = Postcode.Parse("1235 AB");
            ("1234 AB" != p1).Should().BeTrue();
            (p1 != "1234 AB").Should().BeTrue();
        }
    }

    public class To_String
    {
        [Test]
        public void Should_Return_WithoutSpaces()
        {
            var postcode = Postcode.Parse("1234 AB", PostcodeFormatInfo.NL);
            var toString = postcode.ToString("C");
            toString.Should().Be("1234AB");
        }

        [Test]
        public void Should_Return_Dutch_Postcode_With_Spaces()
        {
            var postcode = Postcode.Parse("1234 AB", PostcodeFormatInfo.NL);
            var toString = postcode.ToString();
            toString.Should().Be("1234 AB");
        }

        [Test]
        public void Should_Return_Unknown_Postcode_Without_Spaces()
        {
            var postcode = Postcode.Parse("HA3 0JA", PostcodeFormatInfo.Unknown);
            var toString = postcode.ToString("C");
            toString.Should().Be("HA30JA");
        }

        [Test]
        public void Should_Return_Unknown_Postcode_With_Spaces()
        {
            var postcode = Postcode.Parse("HA3 0JA", PostcodeFormatInfo.Unknown);
            var toString = postcode.ToString();
            toString.Should().Be("HA3 0JA");
        }
    }
}
