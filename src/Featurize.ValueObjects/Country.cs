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
[System.ComponentModel.TypeConverter(typeof(ValueObjectTypeConverter))]
[JsonConverter(typeof(ValueObjectConverter<Country>))]
[DebuggerDisplay("{DebuggerDisplay}")]
public record Country : IValueObject<Country>
{
    private string _value = string.Empty;
    private CountryRecord _countryRecord = new();

    private Country() { }

    /// <summary>
    /// Unknown country.
    /// </summary>
    public static Country Unknown => new() { _value = "?" };

    /// <summary>
    /// Empty country object.
    /// </summary>
    public static Country Empty => new();

    /// <summary>
    /// Name of the country
    /// </summary>
    public string Name => _countryRecord.Name;

    /// <summary>
    /// ISO 3166 Alpha-2 representation of a country.
    /// </summary>
    public string ISO2 => _countryRecord.ISO2;
    
    /// <summary>
    /// ISO 3166 Alpha-3 representation of a country.
    /// </summary>
    public string ISO3 => _countryRecord.ISO3;

    /// <summary>
    /// ISO 3166 Numeric-3 representation of a country.
    /// </summary>
    public string Code => _countryRecord.Code;

    /// <summary>
    /// The region of the country.
    /// </summary>
    public string Region => _countryRecord.Region;

    /// <summary>
    /// The sub region of the country.
    /// </summary>
    public string SubRegion => _countryRecord.SubRegion;
    
    /// <inheritdoc />
    public override string ToString()
    {
        return ISO3;
    }

    public string ToString(string format)
    {
        return format switch
        {
            "I3" => ISO3,
            "I2" => ISO2,
            "c" => Code,
            "n" => Name,
            _ => throw new FormatException(),
        };
    }

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
        if (s == "?")
        {
            return true;
        }

        
        result = CountryLookupTable.All.FirstOrDefault(x => x.ISO3 == s);
        if(result is null)
        {
            result = Unknown;
            return false;
        }

        return true;
    }

    /// <inheritdoc />
    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Country result)
        => TryParse(s, CultureInfo.CurrentCulture, out result);

    /// <inheritdoc />
    public bool IsEmpty() => _value == string.Empty;

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
    private string DebuggerDisplay => IsEmpty() ? "{empty}" : $"{Name} ({ISO3})";
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