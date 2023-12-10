using Aosta.Jikan.Enums;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.SeasonTests;

public class GetSeasonAsyncTests
{
	[Test]
	[TestCase(int.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	[TestCase(1)]
	[TestCase(999)]
	[TestCase(10000)]
	[TestCase(int.MaxValue)]
	public async Task InvalidYear_ShouldThrowValidationException(int year)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetSeasonAsync(year, Season.Fall));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	[TestCase((Season)int.MaxValue)]
	[TestCase((Season)int.MinValue)]
	public async Task InvalidSeasonValidYear_ShouldThrowValidationException(Season Season)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetSeasonAsync(2021, Season));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	[TestCase(1000)]
	[TestCase(1900)]
	[TestCase(2100)]
	[TestCase(9999)]
	public async Task ValidYearNotExistingSeason_ShouldReturnSeasonWithNulls(int year)
	{
		// When
		var season = await JikanTests.Instance.GetSeasonAsync(year, Season.Winter);

		// Then
		season.Data.Should().BeEmpty();
	}

	[Test]
	public async Task Winter2000_ShouldParseWinter2000()
	{
		// When
		var winter2000 = await JikanTests.Instance.GetSeasonAsync(2000, Season.Winter);

		// Then
		var titles = winter2000.Data.Select(x => x.Titles.First(x => x.Type.Equals("Default")).Title);
		using (new AssertionScope())
		{
			titles.Should().Contain("Boogiepop wa Warawanai");
			titles.Should().Contain("Ojamajo Doremi Sharp");
		}
	}

	[Test]
	public async Task Spring1970_ShouldParseSpring1970()
	{
		// When
		var spring1970 = await JikanTests.Instance.GetSeasonAsync(1970, Season.Spring);

		// Then
		spring1970.Data.Should().HaveCount(7);
	}

	[Test]
	public async Task Winter2017_ShouldParseYoujoSenki()
	{
		// When
		var winter2017 = await JikanTests.Instance.GetSeasonAsync(2017, Season.Winter);

		// Then
		using (new AssertionScope())
		{
			winter2017.Pagination.Items.Count.Should().Be(25);
			winter2017.Pagination.Items.Total.Should().Be(57);

			var youjoSenki = winter2017.Data.FirstOrDefault(x => x.Titles.First(x => x.Type.Equals("Default")).Title.Equals("Youjo Senki"));

			youjoSenki.Type.Should().Be(AnimeType.TV);
			youjoSenki.Status.Should().Be(AiringStatus.Completed);
			youjoSenki.Episodes.Should().Be(12);
			youjoSenki.Airing.Should().BeFalse();
			youjoSenki.Duration.Should().Be("24 min per ep");
			youjoSenki.Rating.Should().Be("R - 17+ (violence & profanity)");
			youjoSenki.Score.Should().BeLessOrEqualTo(8.00);
			youjoSenki.ScoredBy.Should().BeGreaterThan(390000);
			youjoSenki.Members.Should().BeGreaterThan(740000);
			youjoSenki.Favorites.Should().BeGreaterThan(9000);
			youjoSenki.Season.Should().Be(Season.Winter);
			youjoSenki.Year.Should().Be(2017);
		}
	}

	[Test]
	[TestCase(int.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task Spring1970_WithInvalidPage_ShouldThrowValidationException(int page)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetSeasonAsync(1970, Season.Winter, page));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task Winter2017SecondPage_ShouldParseYowamushiPedal()
	{
		// When
		const int page = 2;
		var winter2017 = await JikanTests.Instance.GetSeasonAsync(2017, Season.Winter, page);

		// Then
		using var _ = new AssertionScope();

		winter2017.Pagination.Items.Count.Should().Be(25);
		winter2017.Pagination.Items.Total.Should().Be(57);
		winter2017.Pagination.CurrentPage.Should().Be(page);

		var yowamushiPedal = winter2017.Data.FirstOrDefault(x => x.Titles.First(x => x.Type.Equals("Default")).Title.Equals("Yowamushi Pedal: New Generation"));

		yowamushiPedal.Type.Should().Be(AnimeType.TV);
		yowamushiPedal.Status.Should().Be(AiringStatus.Completed);
		yowamushiPedal.Episodes.Should().Be(25);
		yowamushiPedal.Airing.Should().BeFalse();
		yowamushiPedal.Duration.Should().Be("23 min per ep");
		yowamushiPedal.Rating.Should().Be("PG-13 - Teens 13 or older");
		yowamushiPedal.Score.Should().BeGreaterThan(7.00);
		yowamushiPedal.ScoredBy.Should().BeGreaterThan(35000);
		yowamushiPedal.Members.Should().BeGreaterThan(74000);
		yowamushiPedal.Favorites.Should().BeGreaterThan(100);
		yowamushiPedal.Season.Should().Be(Season.Winter);
		yowamushiPedal.Year.Should().Be(2017);
	}
}
