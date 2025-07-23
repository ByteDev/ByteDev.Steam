namespace ByteDev.Steam.Contract.Requests;

public class GetAppDetailsRequest
{
    public int AppId { get; init; }

    public string CurrencyCode { get; init; } = "us";

    public string Language { get; init; } = "en";
}