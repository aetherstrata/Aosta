using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.AnimeTests
{
	public class GetAnimeRecommendationsAsyncTests
	{
		[Test]
		[TestCase(long.MinValue)]
		[TestCase(-1)]
		[TestCase(0)]
		public async Task InvalidId_ShouldThrowValidationException(long malId)
		{
			var func = JikanTests.Instance.Awaiting(x => x.GetAnimeRecommendationsAsync(malId));

			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}

		[Test]
		public async Task BebopId_ShouldParseCowboyBebopRecommendations()
		{
			var bebop = await JikanTests.Instance.GetAnimeRecommendationsAsync(1);

			using var _ = new AssertionScope();
			bebop.Data.First().Entry.MalId.Should().Be(205);
			bebop.Data.First().Entry.Title.Should().Be("Samurai Champloo");
			bebop.Data.First().Votes.Should().BeGreaterThan(70);
			bebop.Data.Count.Should().BeGreaterThan(100);
		}
	}
}