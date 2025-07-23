using System;
using ByteDev.Steam.Contract;
using NUnit.Framework;

namespace ByteDev.Steam.UnitTests.Contract;

[TestFixture]
public class SteamMediaUriBuilderTests
{
    private const int AppId = 1029690;
    private const string ImgIconHash = "00bce3b25aed4dfd96e864e03dd315448dcb834b";

    [Test]
    public void WhenAppIdAndIconHashSet_ThenReturnUri()
    {
        var result = CreateSut()
            .WithAppId(AppId)
            .WithIconHash(ImgIconHash)
            .Build();

        Assert.That(result.AbsoluteUri, Is.EqualTo("http://media.steampowered.com/steamcommunity/public/images/apps/1029690/00bce3b25aed4dfd96e864e03dd315448dcb834b.jpg"));
    }

    [Test]
    public void WhenAppIdLessThanOne_ThenThrowException()
    {
        Assert.Throws<InvalidOperationException>(() => _ = CreateSut().WithAppId(0).WithIconHash(ImgIconHash).Build());
    }

    [Test]
    public void WhenImageFileNotSet_ThenThrowException()
    {
        Assert.Throws<InvalidOperationException>(() => _ = CreateSut().WithAppId(AppId).Build());
    }

    private SteamMediaUriBuilder CreateSut()
    {
        return new SteamMediaUriBuilder(new SteamApiClientConfig());
    }
}