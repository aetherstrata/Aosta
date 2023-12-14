using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.AnimeTests;

public class GetAnimeExternalLinksAsyncTests
{
    [Test]
    [TestCase(long.MinValue)]
    [TestCase(-1)]
    [TestCase(0)]
    public async Task InvalidId_ShouldThrowValidationException(long malId)
    {
        var func = JikanTests.Instance.Awaiting(x => x.GetAnimeExternalLinksAsync(malId));

        await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
    }

    [Test]
    public async Task BebopId_ShouldReturnBebopLinks()
    {
        var links = await JikanTests.Instance.GetAnimeExternalLinksAsync(1);

        using var _ = new AssertionScope();
        links.Data.Should().Contain(x => x.Name.Equals("Wikipedia") && x.Url.Equals("http://en.wikipedia.org/wiki/Cowboy_Bebop"));
        links.Data.Should().Contain(x => x.Name.Equals("AniDB") && x.Url.Equals("https://anidb.net/perl-bin/animedb.pl?show=anime&aid=23"));
    }
}
