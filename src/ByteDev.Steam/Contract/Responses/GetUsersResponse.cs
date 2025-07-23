using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ByteDev.Steam.Contract.Responses;

public class GetUsersResponse
{
    [JsonPropertyName("players")]
    public IReadOnlyCollection<PlayerResponse> Players { get; init; }
}

public class PlayerResponse
{
    [JsonPropertyName("steamid")]
    public string SteamId { get; init; }

    [JsonPropertyName("communityvisibilitystate")]
    public int CommunityVisibilityState { get; init; }

    [JsonPropertyName("profilestate")]
    public int ProfileState { get; init; }

    [JsonPropertyName("personaname")]
    public string PersonaName { get; init; }

    [JsonPropertyName("profileurl")]
    public string ProfileUrl { get; init; }

    [JsonPropertyName("avatar")]
    public string Avatar { get; init; }

    [JsonPropertyName("avatarmedium")]
    public string AvatarMedium { get; init; }

    [JsonPropertyName("avatarfull")]
    public string AvatarFull { get; init; }

    [JsonPropertyName("avatarhash")]
    public string AvatarHash { get; init; }

    [JsonPropertyName("lastlogoff")]
    public int LastLogoff { get; init; }

    [JsonPropertyName("personastate")]
    public int PersonaState { get; set; }

    [JsonPropertyName("primaryclanid")]
    public string PrimaryClanId { get; init; }

    [JsonPropertyName("timecreated")]
    public int TimeCreated { get; init; }

    [JsonPropertyName("personastateflags")]
    public int PersonaStateFlags { get; init; }

    [JsonPropertyName("loccountrycode")]
    public string LocCountryCode { get; init; }
}