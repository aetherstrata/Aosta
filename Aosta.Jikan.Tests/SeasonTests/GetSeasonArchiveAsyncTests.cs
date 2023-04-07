using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.SeasonTests
{
	public class GetSeasonArchiveAsyncTests
	{
		[Test]
		public async Task NoParameter_ShouldParseFirstQueryableYear()
		{
			// When
			var seasonArchives = await JikanTests.Instance.GetSeasonArchiveAsync();

			// Then
			var oldestSeason = seasonArchives.Data.Last();
			using (new AssertionScope())
			{
				oldestSeason.Year.Should().Be(1917);
				oldestSeason.Season.Should().HaveCount(4);
			}
		}

		[Test]
		public async Task NoParameter_ShouldParseLatestQueryableYear()
		{
			// When
			var seasonArchives = await JikanTests.Instance.GetSeasonArchiveAsync();

			// Then
			using (new AssertionScope())
			{
				seasonArchives.Data.First().Year.Should().BeGreaterOrEqualTo(DateTime.UtcNow.Year);
				seasonArchives.Data.Last().Season.Should().HaveCountGreaterOrEqualTo(1);
				seasonArchives.Data.Last().Season.Should().HaveCountLessOrEqualTo(4);
			}
		}
	}
}