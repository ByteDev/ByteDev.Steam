using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using ByteDev.Steam.Contract;
using ByteDev.Steam.Contract.Requests;
using ByteDev.Steam.Infrastructure;

namespace ByteDev.Steam;

internal class ApiHttpRequestFactory
{
    private readonly SteamApiClientConfig _config;

    public ApiHttpRequestFactory(SteamApiClientConfig config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    #region Steam API (api.steampowered.com)

    public HttpRequestMessage CreateGetPlayerSummaries(IEnumerable<string> steamIds)
    {
        // http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/

        var uri = new SteamWebApiUriBuilder(_config)
            .UseApiKey()
            .WithInterface("ISteamUser")
            .WithMethod("GetPlayerSummaries")
            .WithMethodVersion("v2")
            .WithParam("steamids", steamIds.ToCsv())
            .Build();

        return new HttpRequestMessage(HttpMethod.Get, uri);
    }

    public HttpRequestMessage CreateGetFriendList(string steamId)
    {
        // http://api.steampowered.com/ISteamUser/GetFriendList/v1/

        var uri = new SteamWebApiUriBuilder(_config)
            .UseApiKey()
            .WithInterface("ISteamUser")
            .WithMethod("GetFriendList")
            .WithParam("steamid", steamId)
            .WithParam("relationship", "friend")
            .Build();

        return new HttpRequestMessage(HttpMethod.Get, uri);
    }

    public HttpRequestMessage CreateGetPlayerBans(IEnumerable<string> steamIds)
    {
        // https://api.steampowered.com/ISteamUser/GetPlayerBans/v1

        var uri = new SteamWebApiUriBuilder(_config)
            .UseApiKey()
            .WithInterface("ISteamUser")
            .WithMethod("GetPlayerBans")
            .WithParam("steamids", steamIds.ToCsv())
            .Build();

        return new HttpRequestMessage(HttpMethod.Get, uri);
    }

    public HttpRequestMessage CreateGetAppList()
    {
        // http://api.steampowered.com/ISteamApps/GetAppList/v2/

        var uri = new SteamWebApiUriBuilder(_config)
            .WithInterface("ISteamApps")
            .WithMethod("GetAppList")
            .WithMethodVersion("v2")
            .Build();

        return new HttpRequestMessage(HttpMethod.Get, uri);
    }

    public HttpRequestMessage CreateGetNewsForApp(GetAppNewsRequest request)
    {
        // https://api.steampowered.com/ISteamNews/GetNewsForApp/v2/

        var builder = new SteamWebApiUriBuilder(_config)
            .WithInterface("ISteamNews")
            .WithMethod("GetNewsForApp")
            .WithMethodVersion("v2")
            .WithParam("appid", request.AppId.ToString())
            .WithParam("maxlength", request.MaxLength.ToString())
            .WithParam("count", request.Count.ToString());
            
        if (request.FeedNames.Any())
            builder.WithParam("feeds", request.FeedNames.ToCsv());

        if (request.PostsBefore.HasValue)
        {
            long unixEpochTime = request.PostsBefore.Value.ToUnixTimeSeconds();
            builder.WithParam("enddate", unixEpochTime.ToString());
        }

        // TODO: add in "tags" param (Comma-separated list of tags to filter by (e.g. 'patchnodes') )

        var uri = builder.Build();

        return new HttpRequestMessage(HttpMethod.Get, uri);
    }

    public HttpRequestMessage CreateGetRecentlyPlayedGames(GetRecentlyPlayedGamesRequest request)
    {
        // https://api.steampowered.com/IPlayerService/GetRecentlyPlayedGames/v1/

        var uri = new SteamWebApiUriBuilder(_config)
            .UseApiKey()
            .WithInterface("IPlayerService")
            .WithMethod("GetRecentlyPlayedGames")
            .WithParam("steamid", request.SteamId)
            .WithParam("count", request.Count.ToString())
            .Build();

        return new HttpRequestMessage(HttpMethod.Get, uri);
    }

    public HttpRequestMessage CreateGetOwnedGames(GetOwnedGamesRequest request)
    {
        // http://api.steampowered.com/IPlayerService/GetOwnedGames/v1/

        var uri = new SteamWebApiUriBuilder(_config)
            .UseApiKey()
            .WithInterface("IPlayerService")
            .WithMethod("GetOwnedGames")
            .WithParam("skip_unvetted_apps", "false")
            .WithParam("steamid", request.SteamId)
            .WithParam("include_appinfo", request.IncludeExtraAppInfo.ToString().ToLower())
            .WithParam("include_played_free_games", request.IncludePlayedFreeGames.ToString().ToLower())
            // appids_filter param, CSV of AppIds - appears to not work anymore(?)
            .Build();

        return new HttpRequestMessage(HttpMethod.Get, uri);
    }

    #endregion

    #region Steam Store API

    public HttpRequestMessage CreateStoreGetAppDetails(GetAppDetailsRequest request)
    {
        // http://store.steampowered.com/api/appdetails

        var uri = new SteamStoreApiUriBuilder(_config)
            .UseApiBasePath()
            .WithOperation("appdetails")
            .WithParam("appids", request.AppId.ToString()) // API no longer accepts multiple appIds (single only now)
            .WithParam("cc", request.CurrencyCode)
            .WithParam("l", request.Language)
            .Build();

        return new HttpRequestMessage(HttpMethod.Get, uri);
    }

    public HttpRequestMessage CreateStoreGetAppReviews(int appId)
    {
        // https://store.steampowered.com/appreviews/{appId}?json=1

        var uri = new SteamStoreApiUriBuilder(_config)
            .WithOperation($"appreviews/{appId}")
            .WithParam("json", "1")
            .Build();

        return new HttpRequestMessage(HttpMethod.Get, uri);
    }

    public HttpRequestMessage CreateStoreGetFeaturedApps()
    {
        // https://store.steampowered.com/api/featured

        var uri = new SteamStoreApiUriBuilder(_config)
            .UseApiBasePath()
            .WithOperation("featured")
            .Build();

        return new HttpRequestMessage(HttpMethod.Get, uri);
    }

    public HttpRequestMessage CreateStoreSearch(SearchStoreRequest request)
    {
        // https://store.steampowered.com/api/storesearch/?term={Uri.EscapeDataString(term)}&cc={currencyCode}&l={language}

        var builder = new SteamStoreApiUriBuilder(_config)
            .UseApiBasePath()
            .WithOperation("storesearch")
            .WithParam("term", Uri.EscapeDataString(request.Term));

        if (!string.IsNullOrEmpty(request.CurrencyCode))
            builder.WithParam("cc", request.CurrencyCode);

        if (!string.IsNullOrEmpty(request.Language))
            builder.WithParam("l", request.Language);

        var uri = builder.Build();
                
        return new HttpRequestMessage(HttpMethod.Get, uri);
    }

    public HttpRequestMessage CreateStoreGetPackageDetails(int packageId)
    {
        // https://store.steampowered.com/api/packagedetails?packageids={packageIds}
        // packageIds can be a CSV

        var uri = new SteamStoreApiUriBuilder(_config)
            .UseApiBasePath()
            .WithOperation("packagedetails")
            .WithParam("packageids", packageId.ToString())
            .Build();

        return new HttpRequestMessage(HttpMethod.Get, uri);
    }

    // 403 Forbidden: https://store.steampowered.com/api/languages/
    // 403 Forbidden: https://store.steampowered.com/api/bundledetails?bundleids={bundleId}
    // 403 Forbidden: https://store.steampowered.com/api/saledetails?saleid={Uri.EscapeDataString(saleId)}
    // 200 OK (but data seems useless): // https://store.steampowered.com/api/featuredcategories

    #endregion
}