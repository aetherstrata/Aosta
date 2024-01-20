using Aosta.Jikan.Exceptions;

using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.AnimeTests;

public class GetAnimeStatisticsAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long malId)
	{
		var func = JikanTests.Instance.Awaiting(x => x.GetAnimeStatisticsAsync(malId));

		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task BebopId_ShouldParseCowboyBebopStats()
	{
		var bebop = await JikanTests.Instance.GetAnimeStatisticsAsync(1);

		using var _ = new AssertionScope();
		bebop.Data.ScoreStats.Should().NotBeNull();
		bebop.Data.Completed.Should().BeGreaterThan(450000);
		bebop.Data.PlanToWatch.Should().BeGreaterThan(50000);
		bebop.Data.Total.Should().BeGreaterThan(1500000);
		bebop.Data.ScoreStats.Should().HaveCount(10);
		bebop.Data.ScoreStats.Should().Contain(score => score.Score == 5 && score.Votes > 10000);
	}
}