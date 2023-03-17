using Aosta.Core.Data.Enums;
using Aosta.Core.Data.Models;
using Aosta.Core.Extensions;
using JikanDotNet;

namespace Aosta.Core.Tests.Enums;

[TestFixture]
public class ContentTypeTests
{
    [Test]
    public void ValuesChangedTest()
    {
        Assert.Multiple(() =>
        {
            Assert.That((int)ContentType.Unknown, Is.EqualTo(-1));
            Assert.That((int)ContentType.Movie, Is.EqualTo(0));
            Assert.That((int)ContentType.Music, Is.EqualTo(1));
            Assert.That((int)ContentType.ONA, Is.EqualTo(2));
            Assert.That((int)ContentType.OVA, Is.EqualTo(3));
            Assert.That((int)ContentType.Special, Is.EqualTo(4));
            Assert.That((int)ContentType.TV, Is.EqualTo(5));
        });
    }


    [Test]
    public void QuantityChangedTest()
    {
        Assert.That(Enum.GetValues<ContentType>(), Has.Length.EqualTo(7));
    }

    [Test]
    public void CachedStringsChangedTest()
    {
        Assert.Multiple(() =>
        {
            Assert.That(ContentType.Unknown.ToStringCached(), Is.EqualTo("Unknown"));
            Assert.That(ContentType.Movie.ToStringCached(), Is.EqualTo("Movie"));
            Assert.That(ContentType.Music.ToStringCached(), Is.EqualTo("Music"));
            Assert.That(ContentType.ONA.ToStringCached(), Is.EqualTo("ONA"));
            Assert.That(ContentType.OVA.ToStringCached(), Is.EqualTo("OVA"));
            Assert.That(ContentType.Special.ToStringCached(), Is.EqualTo("Special"));
            Assert.That(ContentType.TV.ToStringCached(), Is.EqualTo("TV"));
        });
    }

    [Test]
    public void StringParseTest()
    {
        var content = new Anime { MalId = 0, Type = "TV", }.ToRealmObject();

        Assert.That(content.Type, Is.EqualTo(ContentType.TV));
    }

    [Test]
    public void InvalidStringParseTest()
    {
        Assert.Throws<ArgumentException>(() => Enum.Parse<ContentType>(string.Empty));
        Assert.Throws<ArgumentException>(() => Enum.Parse<ContentType>("Invalid"));
    }
}