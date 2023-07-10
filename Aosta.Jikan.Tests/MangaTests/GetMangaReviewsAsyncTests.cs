using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.MangaTests;

public class GetMangaReviewsAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long id)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetMangaReviewsAsync(id));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task BerserkId_ShouldParseBerserkReviews()
	{
		// When
		var berserk = await JikanTests.Instance.GetMangaReviewsAsync(2);

		// Then
		using (new AssertionScope())
		{
			berserk.Data.First().User.Username.Should().Be("TheCriticsClub");
			berserk.Data.First().MalId.Should().Be(4403);
			berserk.Data.First().Score.Should().BeGreaterThan(7);
			berserk.Data.First().Reactions.TotalReactions.Should().BeGreaterThan(1);
		}
	}
}