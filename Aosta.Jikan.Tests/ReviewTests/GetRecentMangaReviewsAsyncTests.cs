using Aosta.Jikan.Exceptions;

using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.ReviewTests;

public class GetRecentMangaReviewsAsyncTests
{
    [Test]
    [TestCase(int.MinValue)]
    [TestCase(-1)]
    [TestCase(0)]
    public async Task InvalidPage_ShouldThrowValidationException(int page)
    {
        // When
        var func = JikanTests.Instance.Awaiting(x => x.GetRecentMangaReviewsAsync(page));

        // Then
        await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
    }

    [Test]
    public async Task ShouldParseFirstPageReviews()
    {
        // When
        var reviews = await JikanTests.Instance.GetRecentMangaReviewsAsync();

        // Then
        using var _ = new AssertionScope();
        reviews.Pagination.HasNextPage.Should().BeTrue();
        reviews.Data.Should().HaveCount(50);
        reviews.Data.Should().OnlyContain(x => x.Type == "manga");
    }

    [Test]
    public async Task FirstPage_ShouldParseFirstPageReviews()
    {
        // When
        var reviews = await JikanTests.Instance.GetRecentMangaReviewsAsync(1);

        // Then
        using var _ = new AssertionScope();
        reviews.Pagination.HasNextPage.Should().BeTrue();
        reviews.Data.Should().HaveCount(50);
        reviews.Data.Should().OnlyContain(x => x.Type == "manga");
    }

    [Test]
    public async Task SecondPage_ShouldParseSecondPageReviews()
    {
        // When
        var reviews = await JikanTests.Instance.GetRecentMangaReviewsAsync(2);

        // Then
        using var _ = new AssertionScope();
        reviews.Pagination.HasNextPage.Should().BeTrue();
        reviews.Data.Should().HaveCount(50);
        reviews.Data.Should().OnlyContain(x => x.Type == "manga");
    }
}
