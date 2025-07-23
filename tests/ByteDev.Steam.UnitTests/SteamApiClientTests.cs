using ByteDev.Collections;
using ByteDev.Exceptions;
using ByteDev.Steam.Contract.Requests;
using ByteDev.Steam.UnitTests.TestResponses;
using ByteDev.Testing.Http;
using NUnit.Framework;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ByteDev.Steam.UnitTests;

[TestFixture]
public class SteamApiClientTests
{
    [TestFixture]
    public class GetUserAsync : SteamApiClientTests
    {
        [Test]
        public async Task WhenUserExist_ThenReturnReponse()
        {
            var handler = new FakeHttpMessageHandler(
                new FakeRequestOutcome(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new JsonContent(TestApiResponses.GetPlayerSummaries())
                }));

            using var sut = CreateSut(handler);

            var result = await sut.GetUserAsync("100");

            Assert.That(result.PersonaName, Is.EqualTo("supernashwan"));
        }
    }

    [TestFixture]
    public class GetAppPriceOverviewAsync : SteamApiClientTests
    {
        [Test]
        public async Task WhenAppExists_ThenReturnDeserializedResponse()
        {
            var handler = new FakeHttpMessageHandler(
                new FakeRequestOutcome(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new JsonContent(TestApiResponses.StoreApiAppDetails())
                }));

            var request = new GetAppDetailsRequest
            {
                AppId = 1029690
            };

            using var sut = CreateSut(handler);

            var result = await sut.GetAppDetailsAsync(request);

            Assert.That(result.Data.AppId, Is.EqualTo(request.AppId));
            Assert.That(result.Data.PriceOverview.Currency, Is.EqualTo("GBP"));
            Assert.That(result.Data.PriceOverview.InitialPrice, Is.EqualTo(4499));
            Assert.That(result.Data.PriceOverview.FinalPrice, Is.EqualTo(4499));
            Assert.That(result.Data.PriceOverview.DiscountPercent, Is.EqualTo(0));
            Assert.That(result.Data.PriceOverview.FormattedInitialPrice, Is.Empty);
            Assert.That(result.Data.PriceOverview.FormattedFinalPrice, Is.EqualTo("£44.99"));
        }

        [Test]
        public void WhenAppIsNotReturnedInResponse_ThenThrowException()
        {
            var handler = new FakeHttpMessageHandler(
                new FakeRequestOutcome(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new JsonContent(TestApiResponses.EmptyJsonObject())
                }));

            var request = new GetAppDetailsRequest
            {
                AppId = 11
            };

            using var sut = CreateSut(handler);

            var ex = Assert.ThrowsAsync<SteamApiClientException>(() => _ = sut.GetAppDetailsAsync(request));
            Assert.That(ex!.InnerException!.GetType(), Is.EqualTo(typeof(ApiHttpResponseException)));
        }
    }

    [TestFixture]
    public class GetUsersAsync : SteamApiClientTests
    {
        [Test]
        public async Task WhenUsersExist_ThenReturnDeserializedResponse()
        {
            var handler = new FakeHttpMessageHandler(
                new FakeRequestOutcome(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new JsonContent(TestApiResponses.GetPlayerSummaries())
                }));

            using var sut = CreateSut(handler);

            var result = await sut.GetUsersAsync(new[] { "100", "101" });

            Assert.That(result.Players.Count, Is.EqualTo(2));
        }
    }

    [TestFixture]
    public class GetOwnedGamesAsync : SteamApiClientTests
    {
        [Test]
        public async Task WhenUserExists_ThenReturnDeserializedResponse()
        {
            var handler = new FakeHttpMessageHandler(
                new FakeRequestOutcome(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new JsonContent(TestApiResponses.GetOwnedGames())
                }));

            var request = new GetOwnedGamesRequest
            {
                SteamId = "111"
            };

            var sut = CreateSut(handler);

            var result = await sut.GetOwnedGamesAsync(request);

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Games.First().AppId, Is.EqualTo(1164940));
            Assert.That(result.Games.First().Name, Is.EqualTo("Trepang2"));
            Assert.That(result.Games.Second().AppId, Is.EqualTo(1659040));
            Assert.That(result.Games.Second().Name, Is.EqualTo("HITMAN World of Assassination"));
        }
    }

    [TestFixture]
    public class GetRecentlyPlayedGamesAsync : SteamApiClientTests
    {
        [Test]
        public async Task WhenUserExists_ThenReturnDeserializedResponse()
        {
            var handler = new FakeHttpMessageHandler(
                new FakeRequestOutcome(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new JsonContent(TestApiResponses.GetRecentlyPlayedGames())
                }));

            var request = new GetRecentlyPlayedGamesRequest();

            using var sut = CreateSut(handler);

            var result = await sut.GetRecentlyPlayedGamesAsync(request);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.Games.Single().AppId, Is.EqualTo(1029690));
            Assert.That(result.Games.Single().Name, Is.EqualTo("Sniper Elite 5"));
        }

        [Test]
        public async Task WhenGameHasImgIconHash_ThenReturnImgIconUrl()
        {
            var config = new SteamApiClientConfig();

            var handler = new FakeHttpMessageHandler(
                new FakeRequestOutcome(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new JsonContent(TestApiResponses.GetRecentlyPlayedGames())
                }));

            var request = new GetRecentlyPlayedGamesRequest();

            using var sut = CreateSut(handler);

            var result = await sut.GetRecentlyPlayedGamesAsync(request);

            var uri = result.Games.Single().GetImgIconUrl(config);

            Assert.That(uri.AbsoluteUri, Is.EqualTo("http://media.steampowered.com/steamcommunity/public/images/apps/1029690/00bce3b25aed4dfd96e864e03dd315448dcb834b.jpg"));
        }
    }

    [TestFixture]
    public class GetAllAppsAsync : SteamApiClientTests
    {
        [Test]
        public async Task WhenCalled_ThenReturnDeserializedResponse()
        {
            var handler = CreateFakeHandlerGetAppList(TestApiResponses.GetAppList1());

            using var sut = CreateSut(handler);

            var result = await sut.GetAllAppsAsync();

            Assert.That(result.Apps.Count, Is.EqualTo(2));
            Assert.That(result.Apps.First().Id, Is.EqualTo(7));
            Assert.That(result.Apps.First().Name, Is.EqualTo("Steam Client"));
            Assert.That(result.Apps.Second().Id, Is.EqualTo(10));
            Assert.That(result.Apps.Second().Name, Is.EqualTo("Counter-Strike"));
        }

        [Test]
        public async Task WhenCacheResponseIsTrue_ThenReturnCached()
        {
            var handler = CreateFakeHandlerGetAppList(TestApiResponses.GetAppList1());

            var config = new SteamApiClientConfig
            {
                CacheAllAppsResponse = true
            };

            using var sut = CreateSut(handler, config);

            var result1 = await sut.GetAllAppsAsync();
            var result2 = await sut.GetAllAppsAsync();

            Assert.That(result2.DateTimeUtc, Is.EqualTo(result1.DateTimeUtc));
        }

        [Test]
        public async Task WhenCacheResponseIsFalse_ThenReturnFromServer()
        {
            var json = TestApiResponses.GetAppList1();

            var handler = new FakeHttpMessageHandler(new[]
            {
                new FakeRequestOutcome(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new JsonContent(json)
                }),
                new FakeRequestOutcome(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new JsonContent(json)
                })
            });

            var config = new SteamApiClientConfig
            {
                CacheAllAppsResponse = false
            };

            using var sut = CreateSut(handler, config);

            var result1 = await sut.GetAllAppsAsync();
            var result2 = await sut.GetAllAppsAsync();

            Assert.That(result2.DateTimeUtc, Is.Not.EqualTo(result1.DateTimeUtc));
        }
    }

    private static FakeHttpMessageHandler CreateFakeHandlerGetAppList(string json)
    {
        return new FakeHttpMessageHandler(
            new FakeRequestOutcome(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new JsonContent(json)
            }));
    }

    private static SteamApiClient CreateSut(FakeHttpMessageHandler handler)
    {
        return new SteamApiClient(new HttpClient(handler), new SteamApiClientConfig { ApiKey = "ABC123" });
    }

    private static SteamApiClient CreateSut(FakeHttpMessageHandler handler, SteamApiClientConfig config)
    {
        return new SteamApiClient(new HttpClient(handler), config);
    }
}