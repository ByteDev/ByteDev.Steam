using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ByteDev.Steam.Contract.Responses;

public class GetAppNewsResponse
{
    [JsonPropertyName("appnews")]
    public AppNewsResponse AppNews { get; init; }
}

public class AppNewsResponse
{
    [JsonPropertyName("appid")]
    public int AppId { get; init; }

    [JsonPropertyName("count")]
    public int Count { get; init; }

    [JsonPropertyName("newsitems")]
    public IEnumerable<NewsItemResponse> NewsItems { get; init; }
}

public class NewsItemResponse
{
    [JsonPropertyName("gid")]
    public string Gid { get; init; }

    [JsonPropertyName("title")]
    public string Title { get; init; }

    [JsonPropertyName("url")]
    public string Url { get; init; }

    [JsonPropertyName("is_external_url")]
    public bool IsExternalUrl { get; init; }

    [JsonPropertyName("author")]
    public string Author { get; init; }

    [JsonPropertyName("contents")]
    public string Contents { get; init; }

    [JsonPropertyName("feedlabel")]
    public string FeedLabel { get; init; }

    [JsonPropertyName("date")]
    public int Date { get; init; }

    [JsonPropertyName("feedname")]
    public string FeedName { get; init; }

    [JsonPropertyName("feed_type")]
    public int FeedType { get; init; }

    [JsonPropertyName("appid")]
    public int AppId { get; init; }
}
