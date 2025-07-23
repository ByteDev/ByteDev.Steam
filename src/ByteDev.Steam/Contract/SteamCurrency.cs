using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ByteDev.Steam.Contract;

/// <summary>
/// Represents a currency in Steam system.
/// </summary>
/// <remarks>
/// Original values taken from: https://gist.github.com/Decicus/63b2a805a2644032f0ef422d6be5f4a9
/// </remarks>
public class SteamCurrency
{
    private static readonly Dictionary<string, string> CodeNamePairs = new ()
    {
        { "ae", "UAE Dirham" },
        { "az", "CIS - U.S. Dollar" },
        { "br", "Brazilian Real" },
        { "ca", "Canadian Dollar" },
        { "ch", "Swiss Franc" },
        { "cl", "Chilean Peso" },
        { "cn", "Chinese Yuan" },
        { "co", "Colombian Peso" },
        { "eu", "Euro" },
        { "hk", "Hong Kong Dollar" },
        { "id", "Indonesian Rupiah" },
        { "in", "Indian Rupee" },
        { "jp", "Japanese Yen" },
        { "kr", "South Korean Won" },
        { "mx", "Mexican Peso" },
        { "my", "Malaysian Ringgit" },
        { "no", "Norwegian Krone" },
        { "nz", "New Zealand Dollar" },
        { "pe", "Peruvian Nuevo Sol" },
        { "ph", "Philippine Peso" },
        { "ru", "Russian Ruble" },
        { "sa", "Saudi Riyal" },
        { "sg", "Singapore Dollar" },
        { "th", "Thai Baht" },
        { "tr", "Turkish Lira" },
        { "tw", "Taiwan Dollar" },
        { "uk", "British Pound" },
        { "us", "U.S. Dollar" },
        { "za", "South African Rand" }
    };

    public string Code { get; init; }
    public string Name { get; init; }

    public SteamCurrency(string code)
    {
        if (code == null || !Regex.IsMatch(code, "^[a-z]{2}$"))
            throw new ArgumentException("Code was invalid. Code must be two lowercase characters (for example: us).");

        try
        {
            Name = CodeNamePairs[code];
            Code = code;
        }
        catch (KeyNotFoundException ex)
        {
            throw new ArgumentException($"Code: '{code}' is not supported.", nameof(code), ex);
        }
    }

    public static SteamCurrency CreateUsd()
    {
        return new SteamCurrency("us");
    }

    public static SteamCurrency CreateGbp()
    {
        return new SteamCurrency("uk");
    }

    public override string ToString()
    {
        return Code;
    }
}