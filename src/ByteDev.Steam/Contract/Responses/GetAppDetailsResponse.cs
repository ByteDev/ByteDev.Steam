using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ByteDev.Steam.Contract.Responses;

public class GetAppDetailsResponse
{
    [JsonPropertyName("success")]
    public bool IsSuccess { get; init; }

    [JsonPropertyName("data")]
    public GetAppDetailsDataResponse Data { get; init; }
}

public class GetAppDetailsDataResponse
{
    [JsonPropertyName("steam_appid")]
    public int AppId { get; init; }

    [JsonPropertyName("type")]
    public string Type { get; init; } // e.g. game, dlc, demo, etc.

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("required_age")]
    public int RequiredAge { get; init; }

    [JsonPropertyName("is_free")]
    public bool IsFree { get; init; }

    [JsonPropertyName("dlc")]
    public IReadOnlyCollection<int> DlcAppIds { get; init; }

    [JsonPropertyName("about_the_game")]
    public string About { get; init; }

    [JsonPropertyName("short_description")]
    public string ShortDescription { get; init; }

    [JsonPropertyName("detailed_description")]
    public string DetailedDescription { get; init; }

    [JsonPropertyName("supported_languages")]
    public string SupportedLanguages { get; init; }

    [JsonPropertyName("reviews")]
    public string Reviews { get; init; }

    [JsonPropertyName("header_image")]
    public string HeaderImage { get; init; }

    [JsonPropertyName("capsule_image")]
    public string CapsuleImage { get; init; }

    [JsonPropertyName("capsule_imagev5")]
    public string CapsuleImagev5 { get; init; }

    [JsonPropertyName("website")]
    public string WebsiteUrl { get; init; }

    [JsonPropertyName("pc_requirements")]
    public GetAppDetailsRequirementsResponse PcRequirements { get; init; }

    [JsonPropertyName("mac_requirements")]
    public GetAppDetailsRequirementsResponse MacRequirements { get; init; }

    [JsonPropertyName("linux_requirements")]
    public GetAppDetailsRequirementsResponse LinuxRequirements { get; init; }

    [JsonPropertyName("legal_notice")]
    public string LegalNotice { get; init; }

    [JsonPropertyName("drm_notice")]
    public string DrmNotice { get; init; }

    [JsonPropertyName("developers")]
    public IReadOnlyCollection<string> DeveloperNames { get; init; }

    [JsonPropertyName("publishers")]
    public IReadOnlyCollection<string> PublisherNames { get; init; }

    [JsonPropertyName("price_overview")]
    public GetAppDetailsPriceOverviewResponse PriceOverview { get; init; }

    [JsonPropertyName("packages")]
    public IReadOnlyCollection<int> PackageIds { get; init; }

    [JsonPropertyName("package_groups")]
    public IReadOnlyCollection<GetAppDetailsPackageGroupResponse> PackageGroups { get; init; }

    [JsonPropertyName("platforms")]
    public GetAppDetailsPlatformsResponse Platforms { get; init; }

    [JsonPropertyName("metacritic")]
    public GetAppDetailsMetaCriticResponse MetaCritic { get; init; }

    [JsonPropertyName("categories")]
    public IReadOnlyCollection<GetAppDetailsCategoryResponse> Categories { get; init; }

    [JsonPropertyName("genres")]
    public IReadOnlyCollection<GetAppDetailsGenreResponse> Genres { get; init; }

    [JsonPropertyName("screenshots")]
    public IReadOnlyCollection<GetAppDetailsScreenshotResponse> Screenshots { get; init; }

    [JsonPropertyName("movies")]
    public IReadOnlyCollection<GetAppDetailsMovieResponse> Movies { get; init; }

    [JsonPropertyName("recommendations")]
    public GetAppDetailsRecommendationsResponse Recommendations { get; init; }

    [JsonPropertyName("achievements")]
    public GetAppDetailsAchievementsResponse Achievements { get; init; }

    [JsonPropertyName("release_date")]
    public GetAppDetailsReleaseDateResponse ReleaseDate { get; init; }

    [JsonPropertyName("support_info")]
    public GetAppDetailsSupportInfoResponse SupportInfo { get; init; }

    [JsonPropertyName("background")]
    public string BackgroundImageUrl { get; init; }

    [JsonPropertyName("background_raw")]
    public string BackgroundImageRawUrl { get; init; }

    [JsonPropertyName("content_descriptors")]
    public GetAppDetailsContentDescriptorsResponse ContentDescriptors { get; init; }

    [JsonPropertyName("ratings")]
    public GetAppDetailsRatingsResponse Ratings { get; init; }
}

public class GetAppDetailsAchievementsResponse
{
    [JsonPropertyName("total")]
    public int Total { get; init; }

    [JsonPropertyName("highlighted")]
    public IReadOnlyCollection<GetAppDetailsHighlightedResponse> Highlighted { get; init; }
}

public class GetAppDetailsCategoryResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("description")]
    public string Description { get; init; }
}

public class GetAppDetailsContentDescriptorsResponse
{
    [JsonPropertyName("ids")]
    public IReadOnlyCollection<int> Ids { get; init; }

    // [JsonPropertyName("notes")]
    // public object Notes { get; init; }
}

public class GetAppDetailsGenreResponse
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("description")]
    public string Description { get; init; }
}

public class GetAppDetailsHighlightedResponse
{
    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("path")]
    public string Path { get; init; }
}

public class GetAppDetailsRequirementsResponse
{
    [JsonPropertyName("minimum")]
    public string Minimum { get; init; }

    [JsonPropertyName("recommended")]
    public string Recommended { get; init; }
}

public class GetAppDetailsMetaCriticResponse
{
    [JsonPropertyName("score")]
    public int Score { get; init; }

    [JsonPropertyName("url")]
    public string Url { get; init; }
}

public class GetAppDetailsMovieResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("thumbnail")]
    public string Thumbnail { get; init; }

    [JsonPropertyName("webm")]
    public GetAppDetailsMovieUrlsResponse Webm { get; init; }

    [JsonPropertyName("mp4")]
    public GetAppDetailsMovieUrlsResponse Mp4 { get; init; }

    [JsonPropertyName("highlight")]
    public bool IsHighlight { get; init; }
}

public class GetAppDetailsMovieUrlsResponse
{
    [JsonPropertyName("480")]
    public string Res480Url { get; init; }

    [JsonPropertyName("max")]
    public string ResMaxUrl { get; init; }
}

public class GetAppDetailsPackageGroupResponse
{
    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("title")]
    public string Title { get; init; }

    [JsonPropertyName("description")]
    public string Description { get; init; }

    [JsonPropertyName("selection_text")]
    public string SelectionText { get; init; }

    [JsonPropertyName("save_text")]
    public string SaveText { get; init; }

    [JsonPropertyName("display_type")]
    public int DisplayType { get; init; }

    [JsonPropertyName("is_recurring_subscription")]
    public string IsRecurringSubscription { get; init; }

    [JsonPropertyName("subs")]
    public IReadOnlyCollection<GetAppDetailsSubResponse> Subs { get; init; } // AKA Packages
}

public class GetAppDetailsPlatformsResponse
{
    [JsonPropertyName("windows")]
    public bool Windows { get; init; }

    [JsonPropertyName("mac")]
    public bool Mac { get; init; }

    [JsonPropertyName("linux")]
    public bool Linux { get; init; }
}

public class GetAppDetailsPriceOverviewResponse
{
    [JsonPropertyName("currency")]
    public string Currency { get; init; } // e.g. USD, GBP, EUR, etc.

    [JsonPropertyName("initial")]
    public int InitialPrice { get; init; } // Price in smallest unit, e.g. cents for USD (e.g. 999 for $9.99)

    [JsonPropertyName("final")]
    public int FinalPrice { get; init; } // Price in smallest unit, e.g. cents for USD (e.g. 999 for $9.99)

    [JsonPropertyName("discount_percent")]
    public int DiscountPercent { get; init; }

    [JsonPropertyName("initial_formatted")]
    public string FormattedInitialPrice { get; init; }

    [JsonPropertyName("final_formatted")]
    public string FormattedFinalPrice { get; init; }
}

public class GetAppDetailsRecommendationsResponse
{
    [JsonPropertyName("total")]
    public int Total { get; init; }
}

public class GetAppDetailsReleaseDateResponse
{
    [JsonPropertyName("coming_soon")]
    public bool IsComingSoon { get; init; }

    [JsonPropertyName("date")]
    public string Date { get; init; }
}

public class GetAppDetailsScreenshotResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("path_thumbnail")]
    public string ImageThumbnailUrl { get; init; }

    [JsonPropertyName("path_full")]
    public string ImageFullUrl { get; init; }
}

public class GetAppDetailsSubResponse
{
    [JsonPropertyName("packageid")]
    public int PackageId { get; init; }

    [JsonPropertyName("percent_savings_text")]
    public string PercentSavingsText { get; init; }

    [JsonPropertyName("percent_savings")]
    public int PercentSavings { get; init; }

    [JsonPropertyName("option_text")]
    public string OptionText { get; init; }

    [JsonPropertyName("option_description")]
    public string OptionDescription { get; init; }

    [JsonPropertyName("can_get_free_license")]
    public string CanGetFreeLicense { get; init; }

    [JsonPropertyName("is_free_license")]
    public bool IsFreeLicense { get; init; }

    [JsonPropertyName("price_in_cents_with_discount")]
    public int PriceInCentsWithDiscount { get; init; }
}

public class GetAppDetailsSupportInfoResponse
{
    [JsonPropertyName("url")]
    public string Url { get; init; }

    [JsonPropertyName("email")]
    public string Email { get; init; }
}

public class GetAppDetailsRatingsResponse
{
    [JsonPropertyName("dejus")]
    public GetAppDetailsRatingResponse Dejus { get; init; }

    [JsonPropertyName("steam_germany")]
    public GetAppDetailsRatingResponse SteamGermany { get; init; }
}

public class GetAppDetailsRatingResponse
{
    [JsonPropertyName("rating_generated")]
    public string RatingGenerated { get; init; }

    [JsonPropertyName("rating")]
    public string Rating { get; init; }

    [JsonPropertyName("required_age")]
    public string RequiredAge { get; init; }

    [JsonPropertyName("banned")]
    public string Banned { get; init; }

    [JsonPropertyName("use_age_gate")]
    public string UseAgeGate { get; init; }

    [JsonPropertyName("descriptors")]
    public string Descriptors { get; init; }
}
