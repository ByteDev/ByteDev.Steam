using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ByteDev.Steam.Contract.Responses;

public class GetUserFriendsResponse
{
    [JsonPropertyName("friendslist")]
    public FriendsListResponse FriendsList { get; init; }
}

public class FriendsListResponse
{
    [JsonPropertyName("friends")]
    public IEnumerable<FriendResponse> Friends { get; init; }
}

public class FriendResponse
{
    [JsonPropertyName("steamid")]
    public string SteamId { get; init; }

    [JsonPropertyName("relationship")]
    public string Relationship { get; init; }

    [JsonPropertyName("friend_since")]
    public int FriendSince { get; init; }
}
