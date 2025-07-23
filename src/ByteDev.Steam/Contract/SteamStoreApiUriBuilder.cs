using System;
using System.Collections.Specialized;
using ByteDev.ResourceIdentifier;

namespace ByteDev.Steam.Contract;

public class SteamStoreApiUriBuilder
{
    private readonly SteamApiClientConfig _config;

    private readonly NameValueCollection _nameValues = new ();

    private string _op;
    private bool _useApiBasePath;

    public SteamStoreApiUriBuilder(SteamApiClientConfig config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    public SteamStoreApiUriBuilder UseApiBasePath()
    {
        _useApiBasePath = true;
        return this;
    }

    public SteamStoreApiUriBuilder WithOperation(string op)
    {
        _op = op;
        return this;
    }

    public SteamStoreApiUriBuilder WithParam(string name, string value)
    {
        _nameValues.Add(name, value);
        return this;
    }

    public Uri Build()
    {
        // https://store.steampowered.com/{api}/<op>/?{query}

        if (string.IsNullOrEmpty(_op))
            throw new InvalidOperationException("Operation must be provided.");

        var uri = new Uri($"https://{_config.HostStore}/");

        if (_useApiBasePath)
            uri = uri.AppendPath("api");

        return uri
            .AppendPath(_op)
            .SetQuery(_nameValues);
    }
}