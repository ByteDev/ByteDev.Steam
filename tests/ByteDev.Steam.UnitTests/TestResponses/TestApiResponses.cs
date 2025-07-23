using System.IO;

namespace ByteDev.Steam.UnitTests.TestResponses;

internal static class TestApiResponses
{
    public static string EmptyJsonObject() => File.ReadAllText(@"TestResponses\EmptyJsonObject.json");

    public static string GetAppList1() => File.ReadAllText(@"TestResponses\ISteamApps-GetAppList-1.json");
    public static string GetAppList2() => File.ReadAllText(@"TestResponses\ISteamApps-GetAppList-2.json");

    public static string GetRecentlyPlayedGames() => File.ReadAllText(@"TestResponses\IPlayerService-GetRecentlyPlayedGames.json");

    public static string GetOwnedGames() => File.ReadAllText(@"TestResponses\IPlayerService-GetOwnedGames.json");

    public static string GetPlayerSummaries() => File.ReadAllText(@"TestResponses\ISteamUser-GetPlayerSummaries.json");

    public static string GetPlayerBans() => File.ReadAllText(@"TestResponses\ISteamUser-GetPlayerBans.json");

    public static string StoreApiAppDetails() => File.ReadAllText(@"TestResponses\StoreApi-appdetails.json");
}