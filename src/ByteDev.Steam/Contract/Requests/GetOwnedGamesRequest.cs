namespace ByteDev.Steam.Contract.Requests;

public class GetOwnedGamesRequest
{
    public string SteamId { get; init; }

    public bool IncludeExtraAppInfo { get; init; } = false;

    public bool IncludePlayedFreeGames { get; init; } = false;
}