using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ByteDev.Steam.Contract.Responses;

public class GetOwnedGamesResponse
{
    [JsonPropertyName("game_count")]
    public int Count { get; init; }

    [JsonPropertyName("games")]
    public IReadOnlyCollection<OwnedGameResponse> Games { get; init; }
}

public class OwnedGameResponse
{
    [JsonPropertyName("appid")]
    public int AppId { get; init; }

    [JsonPropertyName("playtime_forever")]
    public int PlaytimeForever { get; init; }

    [JsonPropertyName("playtime_windows_forever")]
    public int PlaytimeWindowsForever { get; init; }

    [JsonPropertyName("playtime_mac_forever")]
    public int PlaytimeMacForever { get; init; }

    [JsonPropertyName("playtime_linux_forever")]
    public int PlaytimeLinuxForever { get; init; }

    [JsonPropertyName("playtime_deck_forever")]
    public int PlaytimeDeckForever { get; init; }

    [JsonPropertyName("rtime_last_played")]
    public int RealtimeLastPlayed { get; init; }

    [JsonPropertyName("playtime_disconnected")]
    public int PlaytimeDisconnected { get; init; }

    #region Extended fields (include_appinfo=true)

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("img_icon_url")]
    public string ImgIconHash { get; init; }

    [JsonPropertyName("has_community_visible_stats")]
    public bool HasCommunityVisibleStats { get; init; }

    [JsonPropertyName("content_descriptorids")]
    public IReadOnlyCollection<int> ContentDescriptorIds { get; init; }

    #endregion
}