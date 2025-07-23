namespace ByteDev.Steam;

/// <summary>
/// Represents configuration for a Steam API client.
/// </summary>
public class SteamApiClientConfig
{
    /// <summary>
    /// Steam API host name. By default: api.steampowered.com.
    /// </summary>
    public string HostApi { get; init; } = "api.steampowered.com";

    /// <summary>
    /// Steam Store API host name. By default: store.steampowered.com.
    /// </summary>
    public string HostStore { get; init; } = "store.steampowered.com";

    /// <summary>
    /// Steam Media host name. By default: media.steampowered.com.
    /// </summary>
    public string HostMedia { get; init; } = "media.steampowered.com";

    /// <summary>
    /// Indicates whether to cache on the client the response from a successful full app list request
    /// (the JSON response can be >12MB). By default false.
    /// </summary>
    public bool CacheAllAppsResponse { get; init; } = false;

    /// <summary>
    /// API key to use when communicating with some Steam API endpoints that require it.
    /// </summary>
    public string ApiKey { get; init; }
}