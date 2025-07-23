using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ByteDev.Steam.Contract.Requests;
using ByteDev.Steam.Contract.Responses;

namespace ByteDev.Steam;

public class SteamApiClient : ISteamApiClient
{
    private readonly HttpClient _httpClient;
    private readonly SteamApiClientConfig _config;
    private readonly ApiHttpRequestFactory _apiHttpRequestFactory;
    private readonly ApiHttpResponseHandler _apiHttpResponseHandler;
    private readonly SemaphoreSlim _getAllAppsSemaphore = new (1, 1);

    private GetAllAppsResponse _getAllAppsResponse;
    private bool _isDisposed;

    public SteamApiClient(HttpClient httpClient, SteamApiClientConfig config)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _config = config ?? throw new ArgumentNullException(nameof(config));

        _apiHttpRequestFactory = new ApiHttpRequestFactory(config);
        _apiHttpResponseHandler = new ApiHttpResponseHandler();
    }

    public void Dispose()
    {
        Dispose(true);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed)
            return;

        if (disposing)
        {
            _getAllAppsSemaphore?.Dispose();
        }

        _isDisposed = true;
    }

    #region Steam Web API (api.steampowered.com)

    public async Task<PlayerResponse> GetUserAsync(string steamId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(steamId))
            throw new ArgumentException("Value cannot be null or empty.", nameof(steamId));

        try
        {
            var httpRequest = _apiHttpRequestFactory.CreateGetPlayerSummaries(new[] { steamId });

            var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);

            return await _apiHttpResponseHandler.HandleGetUserAsync(httpResponse, steamId, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new SteamApiClientException("Error while trying to get user.", ex);
        }
    }

    public async Task<GetUsersResponse> GetUsersAsync(IEnumerable<string> steamIds, CancellationToken cancellationToken = default)
    {
        if (steamIds == null)
            throw new ArgumentNullException(nameof(steamIds));

        if (!steamIds.Any())
            throw new ArgumentException("Value cannot be empty.", nameof(steamIds));

        try
        {
            var httpRequest = _apiHttpRequestFactory.CreateGetPlayerSummaries(steamIds);

            var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);

            return await _apiHttpResponseHandler.HandleWithNewRootAsync<GetUsersResponse>(httpResponse, "response", cancellationToken);
        }
        catch (Exception ex)
        {
            throw new SteamApiClientException("Error while trying to get users.", ex);
        }
    }

    public async Task<GetUserFriendsResponse> GetUserFriendsAsync(string steamId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(steamId))
            throw new ArgumentException("Value cannot be null or empty.", nameof(steamId));

        try
        {
            var httpRequest = _apiHttpRequestFactory.CreateGetFriendList(steamId);

            var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);

            return await _apiHttpResponseHandler.HandleAsync<GetUserFriendsResponse>(httpResponse, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new SteamApiClientException("Error while trying to get user friends.", ex);
        }
    }

    public async Task<GetPlayerBansResponse> GetPlayerBansAsync(IEnumerable<string> steamIds, CancellationToken cancellationToken = default)
    {
        if (steamIds == null)
            throw new ArgumentNullException(nameof(steamIds));

        try
        {
            var httpRequest = _apiHttpRequestFactory.CreateGetPlayerBans(steamIds);

            var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);

            return await _apiHttpResponseHandler.HandleAsync<GetPlayerBansResponse>(httpResponse, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new SteamApiClientException("Error while trying to get player bans.", ex);
        }
    }

    public async Task<GetAllAppsResponse> GetAllAppsAsync(CancellationToken cancellationToken = default)
    {
        if (_config.CacheAllAppsResponse && _getAllAppsResponse != null)
            return _getAllAppsResponse;

        try
        {
            await _getAllAppsSemaphore.WaitAsync(cancellationToken);

            try
            {
                if (_config.CacheAllAppsResponse && _getAllAppsResponse != null)
                    return _getAllAppsResponse;

                var httpRequest = _apiHttpRequestFactory.CreateGetAppList();
                var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);

                _getAllAppsResponse = await _apiHttpResponseHandler.HandleWithNewRootAsync<GetAllAppsResponse>(httpResponse, "applist", cancellationToken);
                _getAllAppsResponse.DateTimeUtc = DateTime.UtcNow;

                return _getAllAppsResponse;
            }
            finally
            {
                _getAllAppsSemaphore.Release();
            }
        }
        catch (Exception ex)
        {
            throw new SteamApiClientException("Error while trying to get all apps.", ex);
        }
    }
    
    public async Task<GetAppNewsResponse> GetAppNewsAsync(GetAppNewsRequest request, CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        try
        {
            var httpRequest = _apiHttpRequestFactory.CreateGetNewsForApp(request);

            var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);

            return await _apiHttpResponseHandler.HandleAsync<GetAppNewsResponse>(httpResponse, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new SteamApiClientException("Error while trying to get app news.", ex);
        }
    }

    // "Recently" = Games played in the last 2 weeks
    public async Task<GetRecentlyPlayedGamesResponse> GetRecentlyPlayedGamesAsync(GetRecentlyPlayedGamesRequest request, CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        try
        {
            var httpRequest = _apiHttpRequestFactory.CreateGetRecentlyPlayedGames(request);

            var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);

            return await _apiHttpResponseHandler.HandleWithNewRootAsync<GetRecentlyPlayedGamesResponse>(httpResponse, "response", cancellationToken);
        }
        catch (Exception ex)
        {
            throw new SteamApiClientException("Error while trying to get recently played games.", ex);
        }
    }

    public async Task<GetOwnedGamesResponse> GetOwnedGamesAsync(GetOwnedGamesRequest request, CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        try
        {
            var httpRequest = _apiHttpRequestFactory.CreateGetOwnedGames(request);

            var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);

            return await _apiHttpResponseHandler.HandleWithNewRootAsync<GetOwnedGamesResponse>(httpResponse, "response", cancellationToken);
        }
        catch (Exception ex)
        {
            throw new SteamApiClientException("Error while trying to get owned games.", ex);
        }
    }

    #endregion

    #region Steam Store API (store.steampowered.com)

    public async Task<GetAppDetailsResponse> GetAppDetailsAsync(GetAppDetailsRequest request, CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        try
        {
            var httpRequest = _apiHttpRequestFactory.CreateStoreGetAppDetails(request);

            var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);

            return await _apiHttpResponseHandler.HandleWithNewRootAsync<GetAppDetailsResponse>(httpResponse, request.AppId.ToString(), cancellationToken);
        }
        catch (Exception ex)
        {
            throw new SteamApiClientException("Error while trying to get app details.", ex);
        }
    }

    public async Task<GetAppReviewsResponse> GetAppReviewsAsync(int appId, CancellationToken cancellationToken = default)
    {
        try
        {
            var httpRequest = _apiHttpRequestFactory.CreateStoreGetAppReviews(appId);

            var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);

            return await _apiHttpResponseHandler.HandleAsync<GetAppReviewsResponse>(httpResponse, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new SteamApiClientException("Error while trying to get app reviews.", ex);
        }
    }

    public async Task<GetFeaturedAppsResponse> GetFeaturedAppsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var httpRequest = _apiHttpRequestFactory.CreateStoreGetFeaturedApps();

            var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);

            return await _apiHttpResponseHandler.HandleAsync<GetFeaturedAppsResponse>(httpResponse, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new SteamApiClientException("Error while trying to get featured apps.", ex);
        }
    }

    public async Task<SearchStoreResponse> SearchStoreAsync(SearchStoreRequest request, CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        try
        {
            var httpRequest = _apiHttpRequestFactory.CreateStoreSearch(request);

            var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);

            return await _apiHttpResponseHandler.HandleAsync<SearchStoreResponse>(httpResponse, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new SteamApiClientException("Error while trying to search the store.", ex);
        }
    }

    public async Task<GetPackageDetailsResponse> GetPackageDetailsAsync(int packageId, CancellationToken cancellationToken = default)
    {
        try
        {
            var httpRequest = _apiHttpRequestFactory.CreateStoreGetPackageDetails(packageId);

            var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);

            return await _apiHttpResponseHandler.HandleWithNewRootAsync<GetPackageDetailsResponse>(httpResponse, packageId.ToString(), cancellationToken);
        }
        catch (Exception ex)
        {
            throw new SteamApiClientException("Error while trying to get package details.", ex);
        }
    }

    #endregion
}
