using System;

namespace ByteDev.Steam.Contract;

/// <summary>
/// Represents a builder for constructing URIs to Steam media (images).
/// </summary>
public class SteamMediaUriBuilder
{
    private readonly SteamApiClientConfig _config;
    private int _appId;
    private string _imgFileName;

    /// <summary>
    /// Initializes a new instance of the <see cref="SteamMediaUriBuilder"/> class.
    /// </summary>
    /// <param name="config">The Steam API client configuration.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="config"/> is <c>null</c>.</exception>
    public SteamMediaUriBuilder(SteamApiClientConfig config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    /// <summary>
    /// Sets the Steam application ID for the media URI.
    /// </summary>
    /// <param name="appId">Steam application ID.</param>
    /// <returns>The current <see cref="SteamMediaUriBuilder"/> instance.</returns>
    public SteamMediaUriBuilder WithAppId(int appId)
    {
        _appId = appId;
        return this;
    }

    /// <summary>
    /// Sets the image icon hash for the media URI. Icon hashes are the image file name without the .jpg file extension.
    /// </summary>
    /// <param name="imgIconHash">The image icon hash.</param>
    /// <returns>The current <see cref="SteamMediaUriBuilder"/> instance.</returns>
    public SteamMediaUriBuilder WithIconHash(string imgIconHash)
    {
        _imgFileName = imgIconHash + ".jpg";
        return this;
    }

    /// <summary>
    /// Builds the Steam media URI using the configured values.
    /// </summary>
    /// <returns>The constructed <see cref="Uri"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the application ID is not set or the image file name is not set.</exception>
    public Uri Build()
    {
        // http://media.steampowered.com/steamcommunity/public/images/apps/{AppId}/{ImgIconHash}.jpg

        if (_appId < 1)
            throw new InvalidOperationException("App ID must be set to value 1 or greater.");

        if (string.IsNullOrEmpty(_imgFileName))
            throw new InvalidOperationException($"Image file name was not set. Use {nameof(WithIconHash)}.");

        return new Uri($"http://{_config.HostMedia}/steamcommunity/public/images/apps/{_appId}/{_imgFileName}");
    }
}