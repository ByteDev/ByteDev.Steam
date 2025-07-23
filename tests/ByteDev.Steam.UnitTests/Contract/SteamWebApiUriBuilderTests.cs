using System;
using ByteDev.Steam.Contract;
using NUnit.Framework;

namespace ByteDev.Steam.UnitTests.Contract;

[TestFixture]
public class SteamWebApiUriBuilderTests
{
    [Test]
    public void WhenConfigIsNull_ThenThrowException()
    {
        Assert.Throws<ArgumentNullException>(() => _ = new SteamWebApiUriBuilder(null));
    }

    [TestCase(null)]
    [TestCase("")]
    public void WhenUseApiKey_AndKeyIsNullOrEmpty_ThenThrowException(string apiKey)
    {
        var config = new SteamApiClientConfig { ApiKey = apiKey };

        var sut = new SteamWebApiUriBuilder(config);

        Assert.Throws<InvalidOperationException>(() => _ = sut.UseApiKey());
    }

    [Test]
    public void WhenCallingUseApiKeyTwice_ThenSetKeyOnce()
    {
        var config = new SteamApiClientConfig { ApiKey = "ABC123" };

        var result = new SteamWebApiUriBuilder(config)
            .UseApiKey()
            .UseApiKey()
            .WithInterface("MyInterface")
            .WithMethod("MyMethod")
            .Build();

        Assert.That(result.AbsoluteUri, Is.EqualTo("https://api.steampowered.com/MyInterface/MyMethod/v1/?format=json&key=ABC123"));
    }

    [Test]
    public void WhenInterfaceNotSet_ThenThrowException()
    {
        var config = new SteamApiClientConfig { ApiKey = "ABC123" };

        var sut = new SteamWebApiUriBuilder(config);

        Assert.Throws<InvalidOperationException>(() => _ = sut.WithMethod("MyMethod").Build());
    }

    [Test]
    public void WhenMethodNotSet_ThenThrowException()
    {
        var config = new SteamApiClientConfig { ApiKey = "ABC123" };

        var sut = new SteamWebApiUriBuilder(config);

        Assert.Throws<InvalidOperationException>(() => _ = sut.WithInterface("MyInterface").Build());
    }
}