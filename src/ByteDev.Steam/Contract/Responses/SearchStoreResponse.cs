using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ByteDev.Steam.Contract.Responses;

public class SearchStoreResponse
{
    [JsonPropertyName("total")]
    public int Total { get; init; }

    [JsonPropertyName("items")]
    public IReadOnlyCollection<SearchStoreItemResponse> Items { get; init; }
}

public class SearchStoreItemResponse
{
    [JsonPropertyName("type")]
    public string Type { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("price")]
    public SearchStorePriceReponse Price { get; init; }

    [JsonPropertyName("tiny_image")]
    public string TinyImage { get; init; }

    [JsonPropertyName("metascore")]
    public string MetaScore { get; init; }

    [JsonPropertyName("platforms")]
    public SearchStorePlatformResponse Platforms { get; init; }

    [JsonPropertyName("streamingvideo")]
    public bool StreamingVideo { get; init; }

    [JsonPropertyName("controller_support")]
    public string ControllerSupport { get; init; }
}

public class SearchStorePriceReponse
{
    [JsonPropertyName("currency")]
    public string Currency { get; init; }

    [JsonPropertyName("initial")]
    public int Initial { get; init; }

    [JsonPropertyName("final")]
    public int Final { get; init; }
}

public class SearchStorePlatformResponse
{
    [JsonPropertyName("windows")]
    public bool Windows { get; init; }

    [JsonPropertyName("mac")]
    public bool Mac { get; init; }

    [JsonPropertyName("linux")]
    public bool Linux { get; init; }
}
