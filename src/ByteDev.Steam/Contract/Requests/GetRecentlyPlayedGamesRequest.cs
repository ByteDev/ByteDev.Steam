namespace ByteDev.Steam.Contract.Requests;

public class GetRecentlyPlayedGamesRequest
{
    /// <summary>
    /// Player ID.
    /// </summary>
    public string SteamId { get; init; }

    /// <summary>
    /// Number of games to respond with.
    /// </summary>
    public int Count { get; init; } = 0;
}