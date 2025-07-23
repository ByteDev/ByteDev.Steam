using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ByteDev.Steam.Contract.Responses;

public class GetRecentlyPlayedGamesResponse
{
    [JsonPropertyName("total_count")]
    public int Count { get; init; }

    [JsonPropertyName("games")]
    public IReadOnlyCollection<RecentPlayedGameResponse> Games { get; init; }
}

public class RecentPlayedGameResponse
{
    [JsonPropertyName("appid")]
    public int AppId { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("playtime_2weeks")]
    public int Playtime2weeks { get; init; }

    [JsonPropertyName("playtime_forever")]
    public int PlaytimeForever { get; init; }

    [JsonPropertyName("img_icon_url")]
    public string ImgIconHash { get; init; }

    [JsonPropertyName("playtime_windows_forever")]
    public int PlaytimeWindowsForever { get; init; }

    [JsonPropertyName("playtime_mac_forever")]
    public int PlaytimeMacForever { get; init; }

    [JsonPropertyName("playtime_linux_forever")]
    public int PlaytimeLinuxForever { get; init; }

    [JsonPropertyName("playtime_deck_forever")]
    public int PlaytimeDeckForever { get; init; }

    public Uri GetImgIconUrl(SteamApiClientConfig config)
    {
        if (string.IsNullOrEmpty(ImgIconHash))
            return null;

        return new SteamMediaUriBuilder(config)
            .WithAppId(AppId)
            .WithIconHash(ImgIconHash)
            .Build();
    }
}