using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.WatchTests;

public class GetWatchPopularPromosAsyncTests
{
    [Test]
    public async Task ShouldReturnNonEmptyCollection()
    {
        // When
        var promos = await JikanTests.Instance.GetWatchPopularPromosAsync();

        // Then
        using var _ = new AssertionScope();
        promos.Data.Should().NotBeEmpty();
        promos.Data.First().Trailer.Url.Should().NotBeNullOrEmpty();
        promos.Data.First().Trailer.YoutubeId.Should().NotBeNullOrEmpty();
        promos.Data.First().Trailer.EmbedUrl.Should().NotBeNullOrEmpty();
        promos.Data.First().Entry.Title.Should().NotBeNullOrEmpty();
        promos.Data.First().Entry.Url.Should().NotBeNullOrEmpty();
        promos.Data.First().Entry.Images.Should().NotBeNull();
        promos.Data.First().Title.Should().NotBeNullOrEmpty();
    }
}