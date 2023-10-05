using FluentAssertions;
using System.Globalization;

namespace Featurize.ValueObjects.Tests;

public class Parse
{
    [TestCase("", ExpectedResult = "")]
    [TestCase("?", ExpectedResult = "?")]
    [TestCase("p.evers@tjip.com", ExpectedResult = "p.evers@tjip.com")]
    public string should_be_able_to_parse(string email)
    {
        return EmailAddress.Parse(email, CultureInfo.InvariantCulture).ToString();
    }

    [TestCase("simple", "example.com", false)]
    [TestCase("very.common", "example.com", false)]
    [TestCase("disposable.style.email.with+symbol", "example.com", false)]
    [TestCase("other.email-with-hyphen", "example.com", false)]
    [TestCase("fully-qualified-domain", "example.com", false)]
    [TestCase("user.name+tag+sorting", "example.com", false)]
    [TestCase("x", "example.com", false)]
    [TestCase("example-indeed", "strange-example.com", false)]
    [TestCase("test/test", "test.com", false)]
    [TestCase("admin", "mailserver1", false)]
    [TestCase("example", "s.example", false)]
    //[TestCase("\" \"", "example.org", false)]
    //[TestCase("\"john..doe\"", "example.org", false)]
    [TestCase("mailhost!username", "example.org", false)]
    //[TestCase("\"very.(),:;<>[]\\\".VERY.\\\"very@\\\\ \\\"very\\\".unusual\"", "strange.example.com", false)]
    [TestCase("user%example.com", "example.org", false)]
    [TestCase("user-", "example.org", false)]
    [TestCase("pmdevers", "[123.123.123.123]", true)]
    [TestCase("postmaster", "[IPv6:2001:0db8:85a3:0000:0000:8a2e:0370:7334]", true)]
    public void email_domain_should_be_able_correct(string local, string domain, bool isIp)
    {
        var e = EmailAddress.Parse($"{local}@{domain}", CultureInfo.InvariantCulture);
        e.Domain.Should().Be(domain);

        e.IsIPBased.Should().Be(isIp);
        e.Local.Should().Be(local);
    }

    [TestCase("Abc.example.com")]
    public void invalid_email_address(string email)
    {
        var e = () => EmailAddress.Parse(email, CultureInfo.InvariantCulture);

        e.Should().Throw<FormatException>();

    }

}
