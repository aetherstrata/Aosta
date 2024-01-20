using Aosta.Jikan.Exceptions;

using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.AnimeTests;

public class GetAnimeStreamingLinksAsyncTests
{
    [Test]
    [TestCase(long.MinValue)]
    [TestCase(-1)]
    [TestCase(0)]
    public async Task InvalidId_ShouldThrowValidationException(long malId)
    {
        var func = JikanTests.Instance.Awaiting(x => x.GetAnimeStreamingLinksAsync(malId));

        await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
    }
    
    [Test]
    public async Task BebopId_ShouldReturnBebopLinks()
    {
        var links = await JikanTests.Instance.GetAnimeStreamingLinksAsync(1);

        using var _ = new AssertionScope();
        links.Data.Should().HaveCount(4);
        links.Data.Should().Contain(x => x.Name.Equals("Crunchyroll") && x.Url.Equals("http://www.crunchyroll.com/series-271225"));
        links.Data.Should().Contain(x => x.Name.Equals("Netflix") && x.Url.Equals("https://www.netflix.com/title/80001305"));
    }
}