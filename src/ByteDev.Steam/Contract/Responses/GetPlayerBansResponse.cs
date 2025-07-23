using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ByteDev.Steam.Contract.Responses;

public class GetPlayerBansResponse
{
    [JsonPropertyName("players")]
    public IReadOnlyCollection<PlayerBanResponse> Players { get; set; }
}

public class PlayerBanResponse
{
    [JsonPropertyName("SteamId")]
    public string SteamId { get; set; }

    [JsonPropertyName("CommunityBanned")]
    public bool CommunityBanned { get; set; }

    [JsonPropertyName("VACBanned")]
    public bool VacBanned { get; set; }

    [JsonPropertyName("NumberOfVACBans")]
    public int NumberOfVacBans { get; set; }

    [JsonPropertyName("DaysSinceLastBan")]
    public int DaysSinceLastBan { get; set; }

    [JsonPropertyName("NumberOfGameBans")]
    public int NumberOfGameBans { get; set; }

    [JsonPropertyName("EconomyBan")]
    public string EconomyBan { get; set; }
}