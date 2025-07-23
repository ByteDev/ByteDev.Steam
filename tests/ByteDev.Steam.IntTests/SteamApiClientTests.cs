using ByteDev.Exceptions;
using ByteDev.Steam.Contract;
using ByteDev.Steam.Contract.Requests;
using NUnit.Framework;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ByteDev.Steam.IntTests;

[TestFixture]
public class SteamApiClientTests
{
    private static readonly HttpClient _httpClient = new ();

    [TestFixture]
    public class GetUserAsync : SteamApiClientTests
    {
        [Test]
        public void WhenApiKeyIsInvalid_ThenThrowException()
        {
            var config = new SteamApiClientConfig
            {
                ApiKey = "invalidKey"
            };

            using var sut = CreateSut(config);

            var ex = Assert.ThrowsAsync<SteamApiClientException>(() => _ = sut.GetUserAsync(TestSteamIds.Supernashwan));
            Assert.That(ex!.InnerException!.GetType(), Is.EqualTo(typeof(ApiHttpResponseException)));
            Assert.That(ex.InnerException!.Message, Does.StartWith("API returned forbidden. Please check your API key is valid."));
        }

        [Test]
        public async Task WhenUserNotExist_ThenReturnNull()
        {
            using var sut = CreateSut();

            var result = await sut.GetUserAsync(TestSteamIds.NotExist);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task WhenUserExist_ThenReturnUser()
        {
            using var sut = CreateSut();

            var result = await sut.GetUserAsync(TestSteamIds.Supernashwan);

            Assert.That(result.PersonaName, Is.EqualTo("supernashwan"));
        }
    }

    [TestFixture]
    public class GetUsersAsync : SteamApiClientTests
    {
        [Test]
        public async Task WhenUserNotExist_ThenReturnEmpty()
        {
            using var sut = CreateSut();

            var result = await sut.GetUsersAsync(new[] { TestSteamIds.NotExist });

            Assert.That(result.Players, Is.Empty);
        }

        [Test]
        public async Task WhenUserExistAndNotExist_ThenReturnOnlyExist()
        {
            using var sut = CreateSut();

            var result = await sut.GetUsersAsync(new[] { TestSteamIds.NotExist, TestSteamIds.Supernashwan });

            Assert.That(result.Players.Count, Is.EqualTo(1));
            Assert.That(result.Players.Any(p => p.SteamId == TestSteamIds.Supernashwan), Is.True);
        }

        [Test]
        public async Task WhenUsersExists_ThenReturnUser()
        {
            using var sut = CreateSut();

            var result = await sut.GetUsersAsync(new[] { TestSteamIds.Supernashwan, TestSteamIds.Headrush });

            Assert.That(result.Players.Count, Is.EqualTo(2));
            Assert.That(result.Players.Any(p => p.SteamId == TestSteamIds.Supernashwan), Is.True);
            Assert.That(result.Players.Any(p => p.SteamId == TestSteamIds.Headrush), Is.True);
        }
    }

    [TestFixture]
    public class GetUserFriendsAsync : SteamApiClientTests
    {
        [Test]
        public void WhenUserNotExist_ThenThrowException()
        {
            using var sut = CreateSut();

            var ex = Assert.ThrowsAsync<SteamApiClientException>(() => _ = sut.GetUserFriendsAsync(TestSteamIds.NotExist));
            var inner = ex!.InnerException as ApiHttpResponseException;
            Assert.That(inner!.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));
        }

        [Test]
        public async Task WhenUserExist_ThenReturnFriends()
        {
            using var sut = CreateSut();

            var result = await sut.GetUserFriendsAsync(TestSteamIds.Supernashwan);

            Assert.That(result.FriendsList.Friends.Any(), Is.True);
        }
    }
        
    [TestFixture]
    public class GetPlayerBansAsync : SteamApiClientTests
    {
        [Test]
        public async Task WhenUserNotExist_ThenReturnEmpty()
        {
            using var sut = CreateSut();

            var result = await sut.GetPlayerBansAsync(new[] { TestSteamIds.NotExist });

            Assert.That(result.Players, Is.Empty);
        }

        [Test]
        public async Task WhenUsersHaveBanOrNoBan_ThenReturnUsers()
        {
            using var sut = CreateSut();

            var result = await sut.GetPlayerBansAsync(new[]
            {
                TestSteamIds.Supernashwan, TestSteamIds.Headrush
            });

            Assert.That(result.Players.Count, Is.EqualTo(2));
            Assert.That(result.Players.Count(p => p.SteamId == TestSteamIds.Supernashwan), Is.EqualTo(1));
            Assert.That(result.Players.Count(p => p.SteamId == TestSteamIds.Headrush), Is.EqualTo(1));
        }
    }

    [TestFixture]
    public class GetAllAppsAsync : SteamApiClientTests
    {
        [Test]
        public async Task WhenCalled_ThenReturnsAllApps()
        {
            using var sut = CreateSut();

            var result = await sut.GetAllAppsAsync();

            Assert.That(result.Apps.Count, Is.GreaterThan(10000));
        }
    }

    [TestFixture]
    public class GetAppNewsAsync : SteamApiClientTests
    {
        [Test]
        public void WhenAppNotExist_ThenThrowsException()
        {
            var request = new GetAppNewsRequest
            {
                AppId = TestAppIds.NotExist
            };

            using var sut = CreateSut();

            var ex = Assert.ThrowsAsync<SteamApiClientException>(() => _ = sut.GetAppNewsAsync(request));
            Assert.That(ex!.InnerException!.GetType(), Is.EqualTo(typeof(ApiHttpResponseException)));
            Assert.That(ex!.InnerException!.Message, Does.StartWith("API returned forbidden. Please check your API key is valid."));
        }

        [Test]
        public async Task WhenAppExists_ThenReturnNews()
        {
            var request = new GetAppNewsRequest
            {
                AppId = TestAppIds.CounterStrike,
                Count = 3
            };

            using var sut = CreateSut();

            var result = await sut.GetAppNewsAsync(request);

            Assert.That(result.AppNews.AppId, Is.EqualTo(request.AppId));
            Assert.That(result.AppNews.NewsItems.Count(), Is.EqualTo(request.Count));
        }
    }

    [TestFixture]
    public class GetRecentlyPlayedGamesAsync : SteamApiClientTests
    {
        [Test]
        public void WhenUserNotExist_ThenThrowException()
        {
            var request = new GetRecentlyPlayedGamesRequest
            {
                SteamId = TestSteamIds.NotExist
            };

            using var sut = CreateSut();

            var ex = Assert.ThrowsAsync<SteamApiClientException>(() => _ = sut.GetRecentlyPlayedGamesAsync(request));
            Assert.That(ex!.InnerException!.GetType(), Is.EqualTo(typeof(ApiHttpResponseException)));
            Assert.That(ex.InnerException!.Message, Does.StartWith("API returned unexpected status."));
        }

        [Test]
        public async Task WhenUserExists_ThenReturnPlayedGames()
        {
            var request = new GetRecentlyPlayedGamesRequest
            {
                SteamId = TestSteamIds.Supernashwan
            };

            using var sut = CreateSut();

            var result = await sut.GetRecentlyPlayedGamesAsync(request);

            Assert.That(result.Games.Count, Is.GreaterThan(0));
        }
    }

    [TestFixture]
    public class GetOwnedGamesAsync : SteamApiClientTests
    {
        [Test]
        public void WhenUserNotExist_ThenThrowException()
        {
            var request = new GetOwnedGamesRequest
            {
                SteamId = TestSteamIds.NotExist
            };

            using var sut = CreateSut();

            var ex = Assert.ThrowsAsync<SteamApiClientException>(() => _ = sut.GetOwnedGamesAsync(request));
            Assert.That(ex!.InnerException!.GetType(), Is.EqualTo(typeof(ApiHttpResponseException)));
            Assert.That(ex.InnerException!.Message, Does.StartWith("API returned unexpected status."));
        }

        [Test]
        public async Task WhenUserExists_ThenReturnOwnedGames()
        {
            var request = new GetOwnedGamesRequest
            {
                SteamId = TestSteamIds.Supernashwan,
                IncludeExtraAppInfo = true,
                IncludePlayedFreeGames = false
            };

            using var sut = CreateSut();

            var result = await sut.GetOwnedGamesAsync(request);

            Assert.That(result.Games.Count, Is.GreaterThan(0));
        }
    }

    [TestFixture]
    public class GetAppDetailsAsync : SteamApiClientTests
    {
        [Test]
        public async Task WhenAppNotExist_ThenReturnNoDetails()
        {
            var request = new GetAppDetailsRequest
            {
                AppId = TestAppIds.NotExist
            };

            using var sut = CreateSut();

            var result = await sut.GetAppDetailsAsync(request);

            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.Data, Is.Null);
        }

        [Test]
        public async Task WhenAppExists_ThenReturnDetails()
        {
            var request = new GetAppDetailsRequest
            {
                AppId = TestAppIds.SniperElite5
            };

            using var sut = CreateSut();

            var result = await sut.GetAppDetailsAsync(request);

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data.AppId, Is.EqualTo(request.AppId));
            Assert.That(result.Data.Type, Is.EqualTo("game"));
            Assert.That(result.Data.Name, Is.EqualTo("Sniper Elite 5"));
        }

        [Test]
        public async Task WhenAppExists_AndDefaultCurrency_ThenReturnPriceOverview()
        {
            var request = new GetAppDetailsRequest
            {
                AppId = TestAppIds.SniperElite5
            };

            using var sut = CreateSut();

            var result = await sut.GetAppDetailsAsync(request);

            Assert.That(result.Data.PriceOverview.Currency, Is.EqualTo("USD"));
            Assert.That(result.Data.PriceOverview.FormattedFinalPrice.Substring(0, 1), Is.EqualTo("$"));
        }

        [Test]
        public async Task WhenAppExists_AndCurrencyGb_ThenReturnPriceOverview()
        {
            var currency = SteamCurrency.CreateGbp();

            var request = new GetAppDetailsRequest
            {
                AppId = TestAppIds.SniperElite5,
                CurrencyCode = currency.Code
            };

            using var sut = CreateSut();

            var result = await sut.GetAppDetailsAsync(request);

            Assert.That(result.Data.PriceOverview.Currency, Is.EqualTo("GBP"));
            Assert.That(result.Data.PriceOverview.FormattedFinalPrice.Substring(0, 1), Is.EqualTo("£"));
        }
    }

    [TestFixture]
    public class GetAppReviewsAsync : SteamApiClientTests
    {
        [Test]
        public async Task WhenAppExist_ThenReturnReviews()
        {
            using var sut = CreateSut();

            var result = await sut.GetAppReviewsAsync(TestAppIds.SniperElite5);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Success, Is.EqualTo(1));
            Assert.That(result.Reviews.Count, Is.GreaterThan(0));
            Assert.That(result.QuerySummary.TotalReviews, Is.GreaterThan(0));
        }

        [Test]
        public async Task WhenAppDoesNotExist_ThenReturnNoReviews()
        {
            using var sut = CreateSut();

            var result = await sut.GetAppReviewsAsync(TestAppIds.NotExist);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Success, Is.EqualTo(1));
            Assert.That(result.Reviews, Is.Empty);
            Assert.That(result.QuerySummary.TotalReviews, Is.Zero);
        }
    }

    [TestFixture]
    public class GetFeaturedAppsAsync : SteamApiClientTests
    {
        [Test]
        public async Task WhenCalled_ThenReturnsFeaturedApps()
        {
            using var sut = CreateSut();

            var result = await sut.GetFeaturedAppsAsync();

            Assert.That(result.FeaturedAppsWin, Is.Not.Empty);
            Assert.That(result.FeaturedAppsMac, Is.Not.Empty);
            Assert.That(result.FeaturedAppsLinux, Is.Not.Empty);
        }
    }

    [TestFixture]
    public class GetStoreSearchAsync : SteamApiClientTests
    {
        [Test]
        public async Task WhenTermMatches_ThenReturnsResults()
        {
            var request = new SearchStoreRequest { Term = "sniper" };

            using var sut = CreateSut();

            var result = await sut.SearchStoreAsync(request);

            Assert.That(result.Total, Is.GreaterThan(0));
            Assert.That(result.Items.Any(i => i.Name == "Sniper Elite 5"), Is.True);
        }

        [Test]
        public async Task WhenTermDoesNotMatch_ThenReturnEmptyItems()
        {
            var request = new SearchStoreRequest { Term = "abc123xyz890" };

            using var sut = CreateSut();

            var result = await sut.SearchStoreAsync(request);
            
            Assert.That(result.Total, Is.EqualTo(0));
            Assert.That(result.Items, Is.Empty);
        }
    }

    [TestFixture]
    public class GetPackageDetailsAsync : SteamApiClientTests
    {
        [Test]
        public async Task WhenPackageDoesNotExist_ThenReturnNullData()
        {
            using var sut = CreateSut();

            var result = await sut.GetPackageDetailsAsync(TestPackageIds.NotExist);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Data, Is.Null);
        }

        [Test]
        public async Task WhenPackageExists_ThenReturnPackageDetails()
        {
            using var sut = CreateSut();

            var result = await sut.GetPackageDetailsAsync(TestPackageIds.Borderlands2);

            Assert.That(result.Success, Is.True);
            Assert.That(result.Data.Apps.Count, Is.GreaterThan(0));
            Assert.That(result.Data.Name, Is.EqualTo("Borderlands 2 Game of the Year"));
        }
    }

    private static SteamApiClient CreateSut(SteamApiClientConfig? config = null)
    {
        if (config == null)
            config = CreateConfig();

        return new SteamApiClient(_httpClient, config);
    }

    private static SteamApiClientConfig CreateConfig()
    {
        return new SteamApiClientConfig
        {
            ApiKey = SecretsFactory.GetApiKey(),
            CacheAllAppsResponse = false
        };
    }
}