using ByteDev.Steam.Contract.Requests;
using ByteDev.Steam.Contract.Responses;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ByteDev.Steam;

public interface ISteamApiClient : IDisposable
{
    Task<PlayerResponse> GetUserAsync(string steamId, CancellationToken cancellationToken = default);
    Task<GetUsersResponse> GetUsersAsync(IEnumerable<string> steamIds, CancellationToken cancellationToken = default);
    Task<GetUserFriendsResponse> GetUserFriendsAsync(string steamId, CancellationToken cancellationToken = default);
    Task<GetPlayerBansResponse> GetPlayerBansAsync(IEnumerable<string> steamIds, CancellationToken cancellationToken = default);
    Task<GetAllAppsResponse> GetAllAppsAsync(CancellationToken cancellationToken = default);
    Task<GetAppNewsResponse> GetAppNewsAsync(GetAppNewsRequest request, CancellationToken cancellationToken = default);
    Task<GetRecentlyPlayedGamesResponse> GetRecentlyPlayedGamesAsync(GetRecentlyPlayedGamesRequest request, CancellationToken cancellationToken = default);
    Task<GetOwnedGamesResponse> GetOwnedGamesAsync(GetOwnedGamesRequest request, CancellationToken cancellationToken = default);
    
    Task<GetAppDetailsResponse> GetAppDetailsAsync(GetAppDetailsRequest request, CancellationToken cancellationToken = default);
    Task<GetAppReviewsResponse> GetAppReviewsAsync(int appId, CancellationToken cancellationToken = default);
    Task<GetFeaturedAppsResponse> GetFeaturedAppsAsync(CancellationToken cancellationToken = default);
    Task<SearchStoreResponse> SearchStoreAsync(SearchStoreRequest request, CancellationToken cancellationToken = default);
    Task<GetPackageDetailsResponse> GetPackageDetailsAsync(int packageId, CancellationToken cancellationToken = default);
}