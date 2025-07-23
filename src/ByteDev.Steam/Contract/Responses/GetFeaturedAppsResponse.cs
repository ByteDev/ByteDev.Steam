using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ByteDev.Steam.Contract.Responses;

public class GetFeaturedAppsResponse
{
    // [JsonPropertyName("large_capsules")]
    // public List<object> LargeCapsules { get; init; }

    [JsonPropertyName("featured_win")]
    public IReadOnlyCollection<FeaturedAppResponse> FeaturedAppsWin { get; init; }

    [JsonPropertyName("featured_mac")]
    public IReadOnlyCollection<FeaturedAppResponse> FeaturedAppsMac { get; init; }

    [JsonPropertyName("featured_linux")]
    public IReadOnlyCollection<FeaturedAppResponse> FeaturedAppsLinux { get; init; }

    [JsonPropertyName("layout")]
    public string Layout { get; init; }

    [JsonPropertyName("status")]
    public int Status { get; init; }
}

public class FeaturedAppResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("type")]
    public int Type { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("discounted")]
    public bool Discounted { get; init; }

    [JsonPropertyName("discount_percent")]
    public int DiscountPercent { get; init; }

    [JsonPropertyName("original_price")]
    public int? OriginalPrice { get; init; }

    [JsonPropertyName("final_price")]
    public int FinalPrice { get; init; }

    [JsonPropertyName("currency")]
    public string Currency { get; init; }

    [JsonPropertyName("large_capsule_image")]
    public string LargeCapsuleImage { get; init; }

    [JsonPropertyName("small_capsule_image")]
    public string SmallCapsuleImage { get; init; }

    [JsonPropertyName("windows_available")]
    public bool IsWindowsAvailable { get; init; }

    [JsonPropertyName("mac_available")]
    public bool IsMacAvailable { get; init; }

    [JsonPropertyName("linux_available")]
    public bool IsLinuxAvailable { get; init; }

    [JsonPropertyName("streamingvideo_available")]
    public bool IsStreamingVideoAvailable { get; init; }

    [JsonPropertyName("discount_expiration")]
    public int DiscountExpiration { get; init; }

    [JsonPropertyName("header_image")]
    public string HeaderImage { get; init; }

    [JsonPropertyName("controller_support")]
    public string ControllerSupport { get; init; }
}