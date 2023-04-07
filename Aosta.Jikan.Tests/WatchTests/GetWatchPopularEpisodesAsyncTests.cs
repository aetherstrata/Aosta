using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.WatchTests
{
    public class GetWatchPopularEpisodesAsyncTests
    {
        [Test]
        public async Task ShouldReturnNonEmptyCollection()
        {
            // When
            var episodes = await JikanTests.Instance.GetWatchPopularEpisodesAsync();

            // Then
            using var _ = new AssertionScope();
            episodes.Data.Should().NotBeEmpty();
            episodes.Pagination.HasNextPage.Should().BeFalse();
            episodes.Pagination.LastVisiblePage.Should().Be(1);
            episodes.Data.First().Episodes.Should().HaveCount(2);
            episodes.Data.First().RegionLocked.Should().BeTrue();
            episodes.Data.First().Episodes.Should().HaveCount(2);
            episodes.Data.First().Episodes.Should().OnlyContain(x => x.Premium.HasValue);
        }
    }
}