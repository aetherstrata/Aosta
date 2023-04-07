using Aosta.Core.Data;
using Aosta.Core.Data.Models.Jikan;
using Aosta.Core.Jikan.Models.Response;

namespace Aosta.Core.Tests.Models.Jikan;

[TestFixture]
public class TrailerTests
{
    [SetUp]
    public void SetUp()
    {
        _trailerResponse = new AnimeTrailerResponse
        {
            Url = "Url",
            EmbedUrl = "Embed Url",
            YoutubeId = "== ID ==",
            Image = new ImageResponse()
        };
    }

    private AnimeTrailerResponse _trailerResponse = null!;

    [Test]
    public void ConversionTest()
    {
        var converted = _trailerResponse.ToRealmObject();

        Assert.Multiple(() =>
        {
            Assert.That(converted.Url, Is.EqualTo(_trailerResponse.Url));
            Assert.That(converted.EmbedUrl, Is.EqualTo(_trailerResponse.EmbedUrl));
            Assert.That(converted.YoutubeId, Is.EqualTo(_trailerResponse.YoutubeId));
            Assert.That(converted.Image, Is.Not.Null);
        });
    }

    [Test]
    public void DefaultValuesTest()
    {
        var newTrailer = new TrailerObject(new AnimeTrailerResponse());

        Assert.Multiple(() =>
        {
            Assert.That(newTrailer.Url, Is.Empty);
            Assert.That(newTrailer.EmbedUrl, Is.Empty);
            Assert.That(newTrailer.YoutubeId, Is.Empty);
            Assert.That(newTrailer.Image, Is.Null);
        });
    }
}