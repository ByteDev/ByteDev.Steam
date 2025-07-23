using System;
using System.IO;
using System.Runtime.Serialization;
using NUnit.Framework;

namespace ByteDev.Steam.UnitTests;

[TestFixture]
public class SteamApiClientExceptionTests
{
    private const string ExMessage = "some message";

    [Test]
    public void WhenNoArgs_ThenSetMessageToDefault()
    {
        var sut = new SteamApiClientException();

        Assert.That(sut.Message, Is.EqualTo("Error occurred within the Steam API client."));
    }

    [Test]
    public void WhenMessageSpecified_ThenSetMessage()
    {
        var sut = new SteamApiClientException(ExMessage);

        Assert.That(sut.Message, Is.EqualTo(ExMessage));
    }

    [Test]
    public void WhenMessageAndInnerExSpecified_ThenSetMessageAndInnerEx()
    {
        var innerException = new Exception();

        var sut = new SteamApiClientException(ExMessage, innerException);

        Assert.That(sut.Message, Is.EqualTo(ExMessage));
        Assert.That(sut.InnerException, Is.SameAs(innerException));
    }

    [Test]
    public void WhenSerialized_ThenDeserializeCorrectly()
    {
        var sut = new SteamApiClientException(ExMessage);

        var serializer = new DataContractSerializer(typeof(SteamApiClientException));

        using var stream = new MemoryStream();
            
        serializer.WriteObject(stream, sut);
        stream.Seek(0, SeekOrigin.Begin);

        var result = serializer.ReadObject(stream);

        Assert.That(result!.ToString(), Is.EqualTo(sut.ToString()));
    }
}