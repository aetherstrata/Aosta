using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.AnimeTests;

public class GetAnimeFullDataAsyncTests
{
    [Test]
    [TestCase(long.MinValue)]
    [TestCase(-1)]
    [TestCase(0)]
    public async Task InvalidId_ShouldThrowValidationException(long malId)
    {
        var func = JikanTests.Instance.Awaiting(x => x.GetAnimeFullDataAsync(malId));

        await func.Should().ThrowExactlyAsync<ParameterValidationException>();
    }


    [Test]
    public async Task BebopId_ShouldParseCowboyBebop()
    {
        var bebopAnime = await JikanTests.Instance.GetAnimeFullDataAsync(1);

        using var _ = new AssertionScope();
        bebopAnime.Data.Title.Should().Be("Cowboy Bebop");
        bebopAnime.Data.ExternalLinks.Should().Contain(x => x.Name.Equals("Wikipedia") && x.Url.Equals("http://en.wikipedia.org/wiki/Cowboy_Bebop"));
        bebopAnime.Data.ExternalLinks.Should().Contain(x => x.Name.Equals("AnimeDB") && x.Url.Equals("http://anidb.info/perl-bin/animedb.pl?show=anime&aid=23"));
        bebopAnime.Data.MusicThemes.Openings.Should().ContainSingle().Which
            .Equals("\"Tank!\" by The Seatbelts (eps 1-25)");
        bebopAnime.Data.MusicThemes.Endings.Should().HaveCount(3);
        bebopAnime.Data.Relations.Should().HaveCount(3);
        bebopAnime.Data.Relations.Should()
            .ContainSingle(x => x.Relation.Equals("Adaptation") && x.Entry.Count == 2);
        bebopAnime.Data.Relations.Should()
            .ContainSingle(x => x.Relation.Equals("Side story") && x.Entry.Count == 2);
        bebopAnime.Data.Relations.Should().ContainSingle(x => x.Relation.Equals("Summary") && x.Entry.Count == 1);
        bebopAnime.Data.StreamingLinks.Should().Contain(x => x.Name.Equals("Crunchyroll") && x.Url.Equals("http://www.crunchyroll.com/series-271225"));
        bebopAnime.Data.StreamingLinks.Should().Contain(x => x.Name.Equals("Netflix") && x.Url.Equals("https://www.netflix.com/title/80001305"));
    }
}