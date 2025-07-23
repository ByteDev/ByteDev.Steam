using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ByteDev.Steam.Contract.Responses;

public class GetPackageDetailsResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; init; }

    [JsonPropertyName("data")]
    public GetPackageDetailsDataResponse Data { get; init; }
}

public class GetPackageDetailsDataResponse
{
    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("page_image")]
    public string PageImage { get; init; }

    [JsonPropertyName("header_image")]
    public string HeaderImage { get; init; }

    [JsonPropertyName("small_logo")]
    public string SmallLogo { get; init; }

    [JsonPropertyName("apps")]
    public IReadOnlyCollection<GetPackageDetailsAppResponse> Apps { get; init; }

    [JsonPropertyName("price")]
    public GetPackageDetailsPriceResponse Price { get; init; }

    [JsonPropertyName("platforms")]
    public GetPackageDetailsPlatformsResponse Platforms { get; init; }

    [JsonPropertyName("controller")]
    public GetPackageDetailsControllerResponse Controller { get; init; }

    [JsonPropertyName("release_date")]
    public GetPackageDetailsReleaseResponse Release { get; init; }
}

public class GetPackageDetailsAppResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }
}

public class GetPackageDetailsControllerResponse
{
    [JsonPropertyName("full_gamepad")]
    public bool FullGamepad { get; init; }
}

public class GetPackageDetailsPlatformsResponse
{
    [JsonPropertyName("windows")]
    public bool Windows { get; init; }

    [JsonPropertyName("mac")]
    public bool Mac { get; init; }

    [JsonPropertyName("linux")]
    public bool Linux { get; init; }
}

public class GetPackageDetailsPriceResponse
{
    [JsonPropertyName("currency")]
    public string Currency { get; init; }

    [JsonPropertyName("initial")]
    public int Initial { get; init; }

    [JsonPropertyName("final")]
    public int Final { get; init; }

    [JsonPropertyName("discount_percent")]
    public int DiscountPercent { get; init; }

    [JsonPropertyName("individual")]
    public int Individual { get; init; }
}

public class GetPackageDetailsReleaseResponse
{
    [JsonPropertyName("coming_soon")]
    public bool ComingSoon { get; init; }

    [JsonPropertyName("date")]
    public string Date { get; init; }
}
