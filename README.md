[![Build status](https://ci.appveyor.com/api/projects/status/github/bytedev/ByteDev.Steam?branch=master&svg=true)](https://ci.appveyor.com/project/bytedev/ByteDev-Steam/branch/master)
[![NuGet Package](https://img.shields.io/nuget/v/ByteDev.Steam.svg)](https://www.nuget.org/packages/ByteDev.Steam)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/ByteDev/ByteDev.Steam/blob/master/LICENSE)

# ByteDev.Steam

SDK for communicating with the Steam APIs.

## Installation

ByteDev.Steam is hosted as a package on nuget.org.  To install from the Package Manager Console in Visual Studio run:

`Install-Package ByteDev.Steam`

Further details can be found on the [nuget page](https://www.nuget.org/packages/ByteDev.Steam/).

## Release Notes

Releases follow semantic versioning.

Full details of the release notes can be viewed on [GitHub](https://github.com/ByteDev/ByteDev.Steam/blob/master/docs/RELEASE-NOTES.md).

## Usage

### API Key

A number of the API endpoints require that you have an API key.

To get a key goto: https://steamcommunity.com/dev/apikey.

You will require an existing account with Steam first to be able to then get a API key.

### `SteamApiClient`

`SteamApiClient` provides client functionality to communicate with the Steam APIs.

Create a `SteamApiClient` and call the required method:

```csharp
// Create an instance of HttpClient however you see fit
private static readonly HttpClient _httpClient = new ();

// ...

var config = new SteamApiClientConfig
{
	ApiKey = "YourApiKey",
	CacheAllAppsResponse = true
};

ISteamApiClient client = new SteamApiClient(_httpClient, config);

var response = await CreateSut().GetAllAppsAsync();
```

### `SteamApiClient` - Methods

Steam Web API (api.steampowered.com):

- `GetUserAsync` - Get user details for a specific user by their Steam ID.
- `GetUsersAsync` - Get user details for a number of users.
- `GetUserFriendsAsync` - Get friend details for a particular user.
- `GetPlayerBansAsync` - Get ban info for a particular user.
- `GetAllAppsAsync` - Get details (`AppId` and `Name`) of all apps (games) in the Steam library.
- `GetAppNewsAsync` - Get news for a particular app.
- `GetRecentlyPlayedGamesAsync` - Get recently played games for a particular user.
- `GetOwnedGamesAsync` - Get games owned by a particular user.

Steam Store API (store.steampowered.com):

- `GetAppDetailsAsync` - Get details for a particular app (game), including price overview.
- `GetAppReviewsAsync` - Get reviews for a particular app (game).
- `GetFeaturedAppsAsync` - Get featured apps (games) on the Steam Store.
- `SearchStoreAsync` - Search the Steam Store for items (apps etc.) matching a search term.
- `GetPackageDetailsAsync` - Get details for a particular package (collection of apps, DLC, etc.).

---

## Terms

- Steam ID (`string`) = unique ID to a Steam account or user.
- App ID (`int`) = unique ID for an application/game on Steam. For example `10` for Counter Strike.
- Package ID = collection of one or more games, DLC, or other content that can be purchased together as a single unit

---

## Further Info

- [Steamworks Documentation](https://partner.steamgames.com/doc/webapi/isteamapps)
- [Better Steam Web API Documentation](https://steamwebapi.azurewebsites.net/)
- [Value - Steam Web API](https://developer.valvesoftware.com/wiki/Steam_Web_API)
- [The Ultimate Steam Web API Guide](https://zuplo.com/blog/2024/10/04/what-is-the-steam-web-api)
- [Steam API Endpoints (Zudoku)](https://zudoku.dev/demo/~endpoints/~endpoints?api-url=https%3A%2F%2Fraw.githubusercontent.com%2Fzuplo%2FSteam-OpenAPI%2Frefs%2Fheads%2Fmain%2Fsteam-public.json#get-app-list-operation-of-i-steam-apps)
- [SO: Get Price Information](https://stackoverflow.com/questions/13784059/how-to-get-the-price-of-an-app-in-steam-webapi)

`ISteamStoreService` is now depreciated by Steam.