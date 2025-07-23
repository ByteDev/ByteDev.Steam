using ByteDev.Steam.Contract;
using NUnit.Framework;
using System;

namespace ByteDev.Steam.UnitTests.Contract;

[TestFixture]
public class SteamCurrencyTests
{
    [Test]
    public void WhenCodeIsValid_ThenSetProperties()
    {
        var sut = new SteamCurrency("us");

        Assert.That(sut.Code, Is.EqualTo("us"));
        Assert.That(sut.Name, Is.EqualTo("U.S. Dollar"));
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("u")]
    [TestCase("USA")]
    [TestCase("Us")]
    [TestCase("US")]
    public void WhenCodeIsInvalidFormat_ThenThrowException(string code)
    {
        var ex = Assert.Throws<ArgumentException>(() => _ = new SteamCurrency(code));

        Assert.That(ex!.Message, Is.EqualTo("Code was invalid. Code must be two lowercase characters (for example: us)."));
    }

    [Test]
    public void WhenCodeIsNotSupported_ThenThrowException()
    {
        var ex = Assert.Throws<ArgumentException>(() => _ = new SteamCurrency("zz"));

        Assert.That(ex!.Message, Does.StartWith("Code: 'zz' is not supported."));
        Assert.That(ex.ParamName, Is.EqualTo("code"));
    }

    [Test]
    public void WhenToString_ThenReturnsCode()
    {
        var sut = new SteamCurrency("us");

        Assert.That(sut.ToString(), Is.EqualTo("us"));
    }
}