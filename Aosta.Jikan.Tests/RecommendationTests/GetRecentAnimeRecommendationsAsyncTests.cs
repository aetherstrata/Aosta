using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.RecommendationTests;

public class GetRecentAnimeRecommendationsAsyncTests
{
    [Test]
    [TestCase(int.MinValue)]
    [TestCase(-1)]
    [TestCase(0)]
    public async Task InvalidPage_ShouldThrowValidationException(int page)
    {
        // When
        var func = JikanTests.Instance.Awaiting(x => x.GetRecentAnimeRecommendationsAsync(page));

        // Then
        await func.Should().ThrowExactlyAsync<ParameterValidationException>();
    }

    [Test]
    public async Task ShouldParseFirstPageReviews()
    {
        // When
        var recs = await JikanTests.Instance.GetRecentAnimeRecommendationsAsync();

        // Then
        using var _ = new AssertionScope();
        recs.Pagination.HasNextPage.Should().BeTrue();
        recs.Pagination.LastVisiblePage.Should().Be(20);
        recs.Data.Should().HaveCount(100);
        recs.Data.Should().OnlyContain(x => x.Entries.Count == 2);
    }

    [Test]
    public async Task FirstPage_ShouldParseFirstPageReviews()
    {
        // When
        var recs = await JikanTests.Instance.GetRecentAnimeRecommendationsAsync(1);

        // Then
        using var _ = new AssertionScope();
        recs.Pagination.HasNextPage.Should().BeTrue();
        recs.Pagination.LastVisiblePage.Should().Be(20);
        recs.Data.Should().HaveCount(100);
        recs.Data.Should().OnlyContain(x => x.Entries.Count == 2);
    }
    
    [Test]
    public async Task SecondPage_ShouldParseSecondPageReviews()
    {
        // When
        var recs = await JikanTests.Instance.GetRecentAnimeRecommendationsAsync(2);

        // Then
        using var _ = new AssertionScope();
        recs.Pagination.HasNextPage.Should().BeTrue();
        recs.Pagination.LastVisiblePage.Should().Be(20);
        recs.Data.Should().HaveCount(100);
        recs.Data.Should().OnlyContain(x => x.Entries.Count == 2);
    }
}