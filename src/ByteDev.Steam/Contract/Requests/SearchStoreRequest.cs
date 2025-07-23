namespace ByteDev.Steam.Contract.Requests;

public class SearchStoreRequest
{
    public string Term { get; init; }

    public string CurrencyCode { get; init; } = "us";

    public string Language { get; init; } = "en";
}