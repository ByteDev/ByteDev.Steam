using System.Text.Json.Serialization;

namespace ByteDev.Steam.Contract.Responses;

public class GetAppDetailsPricingResponse
{
    [JsonIgnore]
    public int AppId { get; internal set; }

    [JsonPropertyName("currency")]
    public string Currency { get; init; }

    [JsonPropertyName("initial")]
    public int Initial { get; init; }

    [JsonPropertyName("final")]
    public int Final { get; init; }

    [JsonPropertyName("discount_percent")]
    public int DiscountPercent { get; init; }

    [JsonPropertyName("initial_formatted")]
    public string InitialFormatted { get; init; }

    [JsonPropertyName("final_formatted")]
    public string FinalFormatted { get; init; }
}