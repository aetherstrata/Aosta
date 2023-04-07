using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.UserTests;

public class GetUserReviewsAsyncTests
{
	[Test]
	[TestCase(null)]
	[TestCase("")]
	[TestCase("\n\n\t    \t")]
	public async Task InvalidUsername_ShouldThrowValidationException(string username)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetUserReviewsAsync(username));

		// Then
		await func.Should().ThrowExactlyAsync<ParameterValidationException>();
	}

	[Test]
	public async Task Ervelan_ShouldParseErvelanReviews()
	{
		// When
		var reviews = await JikanTests.Instance.GetUserReviewsAsync("Ervelan");

		// Then
		using (new AssertionScope())
		{
			reviews.Should().NotBeNull();
			reviews.Data.Should().BeEmpty();
		}
	}

	[Test]
	public async Task Ichiyonyuuzloty_ShouldParseIchiyonjuuzlotyReviews()
	{
		// When
		var reviews = await JikanTests.Instance.GetUserReviewsAsync("Ichiyonjuuzloty");

		// Then
		using (new AssertionScope())
		{
			reviews.Data.Should().HaveCount(2);
			reviews.Data.Should().Contain(x => x.Url.Equals("https://myanimelist.net/reviews.php?id=200623"));
		}
	}

	[Test]
	[TestCase(null)]
	[TestCase("")]
	[TestCase("\n\n\t    \t")]
	public async Task InvalidUsernameSecondPage_ShouldThrowValidationException(string username)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetUserReviewsAsync(username, 2));

		// Then
		await func.Should().ThrowExactlyAsync<ParameterValidationException>();
	}

	[Test]
	[TestCase(int.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task ValidUsernameInvalidPage_ShouldThrowValidationException(int page)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetUserReviewsAsync("Ervelan", page));

		// Then
		await func.Should().ThrowExactlyAsync<ParameterValidationException>();
	}

	[Test]
	public async Task IchiyonyuuzlotySecondPage_ShouldParseIchiyonjuuzlotyReviews()
	{
		// When
		var reviews = await JikanTests.Instance.GetUserReviewsAsync("Ichiyonjuuzloty", 2);

		// Then
		reviews.Data.Should().BeEmpty();
	}

	[Test]
	public async Task ArchaeonSecondPage_ShouldParseArchaeonReviews()
	{
		// When
		var reviews = await JikanTests.Instance.GetUserReviewsAsync("Archaeon", 2);

		// Then
		using var _ = new AssertionScope();
		reviews.Data.Should().NotBeEmpty().And.HaveCount(10);
		reviews.Data.Should().OnlyContain(x => x.Type.Equals("anime") && x.EpisodesWatched != null && x.ReviewScores.Animation != null);
	}
}