using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.AnimeTests
{
	public class GetAnimeReviewsAsyncTests
	{
		[Test]
		[TestCase(long.MinValue)]
		[TestCase(-1)]
		[TestCase(0)]
		public async Task InvalidId_ShouldThrowValidationException(long malId)
		{
			var func = JikanTests.Instance.Awaiting(x => x.GetAnimeReviewsAsync(malId));

			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}

		[Test]
		public async Task BebopId_ShouldParseCowboyBebopReviews()
		{
			var bebop = await JikanTests.Instance.GetAnimeReviewsAsync(1);

			var firstReview = bebop.Data.First();
			using (new AssertionScope())
			{
				firstReview.User.Username.Should().Be("TheLlama");
				firstReview.MalId.Should().Be(7406);
				firstReview.EpisodesWatched.Should().Be(26);
				firstReview.Votes.Should().BeGreaterThan(1400);

				firstReview.ReviewScores.Overall.Should().Be(10);
				firstReview.ReviewScores.Animation.Should().Be(9);
			}
		}

		[Test]
		[TestCase(long.MinValue)]
		[TestCase(-1)]
		[TestCase(0)]
		public async Task SecondPageWithInvalidId_ShouldThrowValidationException(long malId)
		{
			var func = JikanTests.Instance.Awaiting(x => x.GetAnimeReviewsAsync(malId, 2));

			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}

		[Test]
		[TestCase(int.MinValue)]
		[TestCase(-1)]
		[TestCase(0)]
		public async Task CorrectIdWrongPage_ShouldThrowValidationException(int page)
		{
			var func = JikanTests.Instance.Awaiting(x => x.GetAnimeReviewsAsync(1, page));

			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}

		[Test]
		public async Task BebopIdSecondPage_ShouldParseCowboyBebopReviewsPaged()
		{
			var bebop = await JikanTests.Instance.GetAnimeReviewsAsync(1, 2);

			var firstReview = bebop.Data.First();

			using var _ = new AssertionScope();
			firstReview.EpisodesWatched.Should().Be(26);
			firstReview.Votes.Should().BeGreaterThan(5);
		}
	}
}