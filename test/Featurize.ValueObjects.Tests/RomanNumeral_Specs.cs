using FluentAssertions;

namespace Featurize.ValueObjects.Tests;
internal class RomanNumeral_Specs
{
    [TestCase("I", 1)]
    [TestCase("II", 2)]
    [TestCase("III", 3)]
    [TestCase("IV", 4)]
    [TestCase("V", 5)]
    [TestCase("VI", 6)]
    [TestCase("VII", 7)]
    [TestCase("VIII", 8)]
    [TestCase("IX", 9)]
    [TestCase("X", 10)]
    [TestCase("XI", 11)]
    [TestCase("XII", 12)]
    [TestCase("XIII", 13)]
    [TestCase("XIV", 14)]
    [TestCase("XV", 15)]
    [TestCase("L", 50)]
    [TestCase("C", 100)]
    [TestCase("D", 500)]
    [TestCase("M", 1000)]
    public void Should_be_valid_value(string str, int value)
    {
        var n = RomanNumeral.Parse(str);

        (n == value).Should().BeTrue();
    }


    [TestCase(1994, "MCMXCIV")]
    [TestCase(58, "LVIII")]
    public void should_convertto_string(int value, string str)
    { 
        var a = RomanNumeral.Parse(str);
        (a == value).Should().BeTrue();

        RomanNumeral val = str;

        (val == value).Should().BeTrue();

        (val++ == value++).Should().BeTrue();
        
    }

}
