using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ByteDev.Steam.Contract.Responses;

public class GetAppReviewsResponse
{
    [JsonPropertyName("success")]
    public int Success { get; init; }

    [JsonPropertyName("query_summary")]
    public QuerySummaryResponse QuerySummary { get; init; }

    [JsonPropertyName("reviews")]
    public IReadOnlyCollection<ReviewResponse> Reviews { get; init; }

    [JsonPropertyName("cursor")]
    public string Cursor { get; init; }
}

public class QuerySummaryResponse
{
    [JsonPropertyName("num_reviews")]
    public int NumReviews { get; init; }

    [JsonPropertyName("review_score")]
    public int ReviewScore { get; init; }

    [JsonPropertyName("review_score_desc")]
    public string ReviewScoreDesc { get; init; }

    [JsonPropertyName("total_positive")]
    public int TotalPositive { get; init; }

    [JsonPropertyName("total_negative")]
    public int TotalNegative { get; init; }

    [JsonPropertyName("total_reviews")]
    public int TotalReviews { get; init; }
}

public class ReviewResponse
{
    [JsonPropertyName("recommendationid")]
    public string RecommendationId { get; init; }

    [JsonPropertyName("author")]
    public ReviewAuthorResponse Author { get; init; }

    [JsonPropertyName("language")]
    public string Language { get; init; }

    [JsonPropertyName("review")]
    public string ReviewText { get; init; }

    [JsonPropertyName("timestamp_created")]
    public int TimestampCreated { get; init; }

    [JsonPropertyName("timestamp_updated")]
    public int TimestampUpdated { get; init; }

    [JsonPropertyName("voted_up")]
    public bool VotedUp { get; init; }

    [JsonPropertyName("votes_up")]
    public int VotesUp { get; init; }

    [JsonPropertyName("votes_funny")]
    public int VotesFunny { get; init; }

    [JsonPropertyName("weighted_vote_score")]
    public object WeightedVoteScore { get; init; } // Can be string or double

    [JsonPropertyName("comment_count")]
    public int CommentCount { get; init; }

    [JsonPropertyName("steam_purchase")]
    public bool SteamPurchase { get; init; }

    [JsonPropertyName("received_for_free")]
    public bool ReceivedForFree { get; init; }

    [JsonPropertyName("written_during_early_access")]
    public bool WrittenDuringEarlyAccess { get; init; }
}

public class ReviewAuthorResponse
{
    [JsonPropertyName("steamid")]
    public string SteamId { get; init; }

    [JsonPropertyName("num_games_owned")]
    public int NumGamesOwned { get; init; }

    [JsonPropertyName("num_reviews")]
    public int NumReviews { get; init; }

    [JsonPropertyName("playtime_forever")]
    public int PlaytimeForever { get; init; }

    [JsonPropertyName("playtime_last_two_weeks")]
    public int PlaytimeLastTwoWeeks { get; init; }

    [JsonPropertyName("playtime_at_review")]
    public int PlaytimeAtReview { get; init; }

    [JsonPropertyName("last_played")]
    public int LastPlayed { get; init; }
}