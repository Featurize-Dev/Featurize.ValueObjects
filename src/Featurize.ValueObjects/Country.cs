using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Featurize.ValueObjects;


/// <summary>
/// Represents a Country.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay}")]
[JsonConverter(typeof(ValueObjectJsonConverter))]
public record struct Country() : IValueObject<Country>
{
    private string _value = string.Empty;
    private CountryRecord _countryRecord = new();

    /// <summary>
    /// Unknown country.
    /// </summary>
    public static Country Unknown => new() { _value = ValueObject.UnknownValue };

    /// <summary>
    /// Empty country object.
    /// </summary>
    public static Country Empty => new();

    /// <summary>
    /// Name of the country
    /// </summary>
    public readonly string Name => _countryRecord.Name;

    /// <summary>
    /// ISO 3166 Alpha-2 representation of a country.
    /// </summary>
    public readonly string ISO2 => _countryRecord.ISO2;
    
    /// <summary>
    /// ISO 3166 Alpha-3 representation of a country.
    /// </summary>
    public readonly string ISO3 => _countryRecord.ISO3;

    /// <summary>
    /// ISO 3166 Numeric-3 representation of a country.
    /// </summary>
    public readonly string Code => _countryRecord.Code;

    /// <summary>
    /// The region of the country.
    /// </summary>
    public readonly string Region => _countryRecord.Region;

    /// <summary>
    /// The sub region of the country.
    /// </summary>
    public readonly string SubRegion => _countryRecord.SubRegion;
    
    /// <inheritdoc />
    public override readonly string ToString()
    {
        return ISO3;
    }

    /// <summary>
    /// Returns a string representing a country
    /// </summary>
    /// <param name="format">
    /// Supported formats
    /// <list type="bullet">
    /// <item>
    /// <description>A3 = ISO 3166 Alpha-3</description>
    /// </item>
    /// <item>
    /// <description>A2 = ISO 3166 Alpha-2</description>
    /// </item>
    /// <item>
    /// <description>N3 = ISO 3166 Num-3</description>
    /// </item>
    /// <item>
    /// <description>N = Name of country</description>
    /// </item>
    /// </list>
    /// </param>
    /// <returns>String representing a country</returns>
    /// <exception cref="FormatException"></exception>
    public readonly string ToString(string format) => format switch
    {
        "A3" => ISO3,
        "A2" => ISO2,
        "N3" => Code,
        "N" => Name,
        _ => throw new FormatException(),
    };

    /// <inheritdoc />
    public static Country Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result : throw new FormatException();

    /// <inheritdoc />
    public static Country Parse(string s)
        => Parse(s, CultureInfo.CurrentCulture);

    /// <inheritdoc />
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Country result)
    {
        result = Unknown;
        if (s == ValueObject.UnknownValue)
        {
            return true;
        }

        if (string.IsNullOrEmpty(s))
        {
            result = Empty;
            return true;
        }
        
        result = CountryLookupTable.All.FirstOrDefault(x => x.ISO3 == s);
        if(result == Empty)
        {
            result = Unknown;
            return false;
        }

        return true;
    }

    /// <inheritdoc />
    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Country result)
        => TryParse(s, CultureInfo.CurrentCulture, out result);


    internal static Country Create(CountryRecord record)
    {
        return new Country()
        {
            _value = record.ISO3,
            _countryRecord = record,
        };
    }

    /// <summary>
    /// Returns all countries.
    /// </summary>
    public static IEnumerable<Country> All => CountryLookupTable.All;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly string DebuggerDisplay => this.IsEmpty() ? "{empty}" : $"{Name} ({ISO3})";
}


internal static class CountryLookupTable
{
    private static readonly List<Country> _all = new();
    static CountryLookupTable()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            TrimOptions = TrimOptions.InsideQuotes,
            BadDataFound = null
        };

        var type = typeof(Country);
        var stream = typeof(Country).Assembly.GetManifestResourceStream($"{type.Namespace}.Country.csv") 
            ?? throw new InvalidOperationException();

        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, config);
        {
            csv.Read();
            csv.ReadHeader();
            while (csv.Read())
            {
                var record = csv.GetRecord<CountryRecord>() 
                    ?? throw new InvalidOperationException();

                _all.Add(Country.Create(record));
            }
        }
    }

    public static IEnumerable<Country> All => _all.ToArray();
}

internal record class CountryRecord
{
    [Name("name")]
    public string Name { get;set; } = string.Empty;
    
    [Name("alpha-2")]
    public string ISO2 { get;set; } = string.Empty;

    [Name("alpha-3")]
    public string ISO3 { get;set; } = string.Empty;

    [Name("country-code")]
    public string Code { get;set; } = string.Empty;

    [Name("region")]
    public string Region { get;set; } = string.Empty;

    [Name("sub-region")]
    public string SubRegion { get; set; } = string.Empty;
}