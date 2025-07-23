using System;
using System.Collections.Specialized;
using ByteDev.ResourceIdentifier;

namespace ByteDev.Steam.Contract;

public class SteamWebApiUriBuilder
{
    private readonly SteamApiClientConfig _config;

    private readonly NameValueCollection _nameValues = new ()
    {
        { "format", "json" } // supported formats: "json", "vdf", "xml"
    };

    private string _interface;
    private string _method;
    private string _methodVersion = "v1";

    public SteamWebApiUriBuilder(SteamApiClientConfig config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    public SteamWebApiUriBuilder UseApiKey()
    {
        if (string.IsNullOrEmpty(_config.ApiKey))
            throw new InvalidOperationException("API key is required but is null or empty.");

        if (_nameValues["key"] == null)
            _nameValues.Add("key", _config.ApiKey);

        return this;
    }

    public SteamWebApiUriBuilder WithInterface(string @interface) // e.g. "ISteamUser"
    {
        _interface = @interface;
        return this;
    }

    public SteamWebApiUriBuilder WithMethod(string method) // e.g. "GetFriendList"
    {
        _method = method;
        return this;
    }

    public SteamWebApiUriBuilder WithMethodVersion(string methodVersion) // e.g. "v1"
    {
        _methodVersion = methodVersion;
        return this;
    }

    public SteamWebApiUriBuilder WithParam(string name, string value)
    {
        _nameValues.Add(name, value);
        return this;
    }

    public Uri Build()
    {
        // https://api.steampowered.com/<interface>/<method>/<method_version>/

        if (string.IsNullOrEmpty(_interface))
            throw new InvalidOperationException("Interface must be provided.");

        if (string.IsNullOrEmpty(_method))
            throw new InvalidOperationException("Method must be provided.");

        return new Uri($"https://{_config.HostApi}/")
            .AppendPath(_interface)
            .AppendPath(_method)
            .AppendPath(_methodVersion)
            .AppendPath("/")
            .SetQuery(_nameValues);
    }
}