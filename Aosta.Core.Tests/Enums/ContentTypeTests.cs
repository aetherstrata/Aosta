using Aosta.Core.Data.Enums;
using Aosta.Core.Extensions;
using Aosta.Core.Jikan.Models.Response;

namespace Aosta.Core.Tests.Enums;

[TestFixture]
public class ContentTypeTests : IEnumTests
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
    public void JikanStringParseTest()
    {
        var content = new AnimeResponse { MalId = 0, Type = "TV" }.ToRealmObject();
        Assert.That(content.Type, Is.EqualTo(ContentType.TV));
    }

    [Test]
    public void JikanInvalidStringParseTest()
    {
        var content = new AnimeResponse { MalId = 0, Type = "Invalid" }.ToRealmObject();
        Assert.That(content.Type, Is.EqualTo(ContentType.Unknown));
    }

    [Test]
    public void JikanNullStringParseTest()
    {
        var content = new AnimeResponse { MalId = 0, Type = null }.ToRealmObject();
        Assert.That(content.Type, Is.EqualTo(ContentType.Unknown));
    }
}