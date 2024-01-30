using Aosta.Data.Database.Mapper;
using Aosta.Jikan.Models.Response;
using FluentAssertions.Execution;

namespace Aosta.Data.Tests.Models.Jikan;

[TestFixture]
public class TrailerTests
{
    [Test]
    public void ConversionTest()
    {
        var converted = new AnimeTrailerResponse
        {
            Url = "Url",
            EmbedUrl = "Embed Url",
            YoutubeId = "== ID ==",
            Image = new ImageResponse()
        }.ToRealmModel();

        using var _ = new AssertionScope();
        converted.Url.Should().Be("Url");
        converted.EmbedUrl.Should().Be("Embed Url");
        converted.YoutubeId.Should().Be("== ID ==");
        converted.Image.Should().NotBeNull();
    }

    [Test]
    public void DefaultValuesTest()
    {
        var newTrailer = new AnimeTrailerResponse().ToRealmModel();

        using var _ = new AssertionScope();
        newTrailer.Url.Should().BeNull();
        newTrailer.EmbedUrl.Should().BeNull();
        newTrailer.YoutubeId.Should().BeNull();
        newTrailer.Image.Should().BeNull();
    }
}