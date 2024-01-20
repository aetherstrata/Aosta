using Aosta.Jikan.Exceptions;

using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.TopTests;

public class GetTopReviewsAsyncTests
{
	[Test]
	[TestCase(int.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidPage_ShouldThrowValidationException(int page)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetTopReviewsAsync(page));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task NoParameter_ShouldParseTopReviews()
	{
		// When
		var reviews = await JikanTests.Instance.GetTopReviewsAsync();

		// Then
		using var _ = new AssertionScope();
		reviews.Data.Count.Should().Be(50);
		reviews.Pagination.HasNextPage.Should().BeTrue();
	}

	[Test]
	public async Task sECONDpAGE_ShouldParseTopReviewsSecondPage()
	{
		// When
		var reviews = await JikanTests.Instance.GetTopReviewsAsync(2);

		// Then
		using var _ = new AssertionScope();
		reviews.Data.Count.Should().Be(50);
		reviews.Pagination.HasNextPage.Should().BeTrue();
	}
}
