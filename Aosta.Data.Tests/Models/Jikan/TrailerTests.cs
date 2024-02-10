using Aosta.Jikan.Models.Response;
using FluentAssertions.Execution;

using JikanMapper = Aosta.Data.Mapper.JikanMapper;

namespace Aosta.Data.Tests.Models.Jikan;

[TestFixture]
public class TrailerTests
{
    [Test]
    public void ConversionTest()
    {
        var converted = JikanMapper.ToModel(new AnimeTrailerResponse
        {
            Url = "Url",
            EmbedUrl = "Embed Url",
            YoutubeId = "== ID ==",
            Image = new ImageResponse()
        });

        using var _ = new AssertionScope();
        converted.Url.Should().Be("Url");
        converted.EmbedUrl.Should().Be("Embed Url");
        converted.YoutubeId.Should().Be("== ID ==");
        converted.Image.Should().NotBeNull();
    }

    [Test]
    public void DefaultValuesTest()
    {
        var newTrailer = JikanMapper.ToModel(new AnimeTrailerResponse());

        using var _ = new AssertionScope();
        newTrailer.Url.Should().BeNull();
        newTrailer.EmbedUrl.Should().BeNull();
        newTrailer.YoutubeId.Should().BeNull();
        newTrailer.Image.Should().BeNull();
    }
}