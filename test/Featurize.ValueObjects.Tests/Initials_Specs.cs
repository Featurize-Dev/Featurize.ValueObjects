using System.Text.Json;

namespace Featurize.ValueObjects.Tests;

public class Initialls_Specs
{
    public class Serialization
    {
        [TestCase("P.", ExpectedResult = "\"P.\"")]
        [TestCase("", ExpectedResult = "\"\"")]
        [TestCase("?", ExpectedResult = "\"?\"")]
        public string should_return_valid_json(string value)
        {
            return JsonSerializer.Serialize(Initials.Parse(value));
        }
    }

    public class Deserialization
    {
        [TestCase("\"P.\"", ExpectedResult = "P.")]
        [TestCase("\"\"", ExpectedResult = "")]
        [TestCase("\"?\"", ExpectedResult = "?")]
        [TestCase("232323", ExpectedResult = "?")]
        public string should_return_valid_initals(string json)
        {
            return JsonSerializer.Deserialize<Initials>(json);
        }
    }

    public class Parse
    {
        [TestCase("A.F.Th.", ExpectedResult = "A.F.Th.")]
        [TestCase("Aft", ExpectedResult = "A.F.T.")]
        [TestCase("", ExpectedResult = "")]
        [TestCase("?", ExpectedResult = "?")]
        public string should_return_valid_initials(string value)
        {
            var result = Initials.Parse(value);
            return result;
        }
    }

}