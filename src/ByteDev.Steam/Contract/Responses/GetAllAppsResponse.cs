using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ByteDev.Steam.Contract.Responses;

public class GetAllAppsResponse
{
    [JsonIgnore]
    public DateTime DateTimeUtc { get; internal set; }

    [JsonPropertyName("apps")]
    public IReadOnlyCollection<AppResponse> Apps { get; init; }
}

public class AppResponse
{
    [JsonPropertyName("appid")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }
}