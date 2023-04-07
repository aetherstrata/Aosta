using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.MangaTests;

public class GetMangaStatisticsAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long id)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetMangaStatisticsAsync(id));

		// Then
		await func.Should().ThrowExactlyAsync<ParameterValidationException>();
	}

	[Test]
	public async Task GetMangaStatistics_MonsterId_ShouldParseMonsterStats()
	{
		// When
		var monster = await JikanTests.Instance.GetMangaStatisticsAsync(1);

		// Then
		using (new AssertionScope())
		{
			monster.Data.ScoreStats.Should().NotBeNull();
			monster.Data.Completed.Should().BeGreaterThan(25000);
			monster.Data.Dropped.Should().BeGreaterThan(500);
			monster.Data.Total.Should().BeGreaterThan(160000);
			monster.Data.ScoreStats.Should().Contain(x => x.Score.Equals(8) && x.Votes > 8500);
		}
	}
}