using Aosta.Core.Data.Models.Jikan;
using Aosta.Core.Extensions;
using JikanDotNet;

namespace Aosta.Core.Tests.Models.Jikan;

[TestFixture]
public class TrailerTests
{
    [SetUp]
    public void SetUp()
    {
        _trailer = new AnimeTrailer
        {
            Url = "Url",
            EmbedUrl = "Embed Url",
            YoutubeId = "== ID ==",
            Image = new Image()
        };
    }

    private AnimeTrailer _trailer = null!;

    [Test]
    public void ConversionTest()
    {
        var converted = _trailer.ToRealmObject();

        Assert.Multiple(() =>
        {
            Assert.That(converted.Url, Is.EqualTo(_trailer.Url));
            Assert.That(converted.EmbedUrl, Is.EqualTo(_trailer.EmbedUrl));
            Assert.That(converted.YoutubeId, Is.EqualTo(_trailer.YoutubeId));
            Assert.That(converted.Image, Is.Not.Null);
        });
    }

    [Test]
    public void DefaultValuesTest()
    {
        var newTrailer = new TrailerObject(new AnimeTrailer());

        Assert.Multiple(() =>
        {
            Assert.That(newTrailer.Url, Is.Empty);
            Assert.That(newTrailer.EmbedUrl, Is.Empty);
            Assert.That(newTrailer.YoutubeId, Is.Empty);
            Assert.That(newTrailer.Image, Is.Null);
        });
    }
}