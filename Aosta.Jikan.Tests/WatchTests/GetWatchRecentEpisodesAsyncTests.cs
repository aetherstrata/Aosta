using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.WatchTests;

public class GetWatchRecentEpisodesAsyncTests
{
    [Test]
    public async Task ShouldReturnNonEmptyCollection()
    {
        // When
        var episodes = await JikanTests.Instance.GetWatchRecentEpisodesAsync();

        // Then
        using var _ = new AssertionScope();
        episodes.Data.Should().NotBeEmpty();
        episodes.Data.First().Episodes.Should().NotBeEmpty();
        episodes.Data.First().RegionLocked.Should().BeFalse();
        episodes.Data.First().Episodes.Should().OnlyContain(x => x.Premium.HasValue);
    }
}