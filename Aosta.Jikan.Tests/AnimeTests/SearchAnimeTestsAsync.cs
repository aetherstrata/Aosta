using Aosta.Jikan.Enums;
using Aosta.Jikan.Query;
using Aosta.Jikan.Query.Enums;
using Aosta.Jikan.Query.Parameters;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.AnimeTests;

public class SearchAnimeTestsAsync
{
	[Test]
	[TestCase(int.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidPage_ShouldThrowValidationException(int page)
	{
		var config = new AnimeSearchQueryParameters()
			.SetPage(page);

		var func = JikanTests.Instance.Awaiting(x => x.SearchAnimeAsync(config));

		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	[TestCase(int.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	[TestCase(26)]
	[TestCase(int.MaxValue)]
	public async Task InvalidPageSize_ShouldThrowValidationException(int pageSize)
	{
		var config = new AnimeSearchQueryParameters().SetLimit(pageSize);
		var func = JikanTests.Instance.Awaiting(x => x.SearchAnimeAsync(config));

		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task GivenSecondPage_ShouldReturnSecondPage()
	{
		var config = new AnimeSearchQueryParameters().SetPage(2);
		var anime = await JikanTests.Instance.SearchAnimeAsync(config);

		using var _ = new AssertionScope();
		anime.Data.Should().HaveCount(JikanParameterConsts.MAXIMUM_PAGE_SIZE);
		anime.Data.First().Titles.First(x => x.Type.Equals("Default")).Title.Should().Be("Rurouni Kenshin: Meiji Kenkaku Romantan - Tsuioku-hen");
		anime.Pagination.LastVisiblePage.Should().BeGreaterThan(780);
		anime.Pagination.CurrentPage.Should().Be(2);
		anime.Pagination.Items.Count.Should().Be(25);
		anime.Pagination.Items.PerPage.Should().Be(25);
	}

	[Test]
	public async Task GivenValidPageSize_ShouldReturnPageSizeNumberOfRecords()
	{
		const int pageSize = 5;
		var config = new AnimeSearchQueryParameters().SetLimit(pageSize);
		var anime = await JikanTests.Instance.SearchAnimeAsync(config);

		using var _ = new AssertionScope();
		anime.Data.Should().HaveCount(pageSize);
		anime.Data.First().Titles.First(x => x.Type.Equals("Default")).Title.Should().Be("Cowboy Bebop");
		anime.Pagination.CurrentPage.Should().Be(1);
		anime.Pagination.Items.Count.Should().Be(pageSize);
		anime.Pagination.Items.PerPage.Should().Be(pageSize);
	}

	[Test]
	public async Task GivenValidPageAndPageSize_ShouldReturnPageSizeNumberOfRecordsFromNextPage()
	{
		const int pageSize = 5;
		var config = new AnimeSearchQueryParameters().SetPage(2).SetLimit(pageSize);

		var anime = await JikanTests.Instance.SearchAnimeAsync(config);

		using var _ = new AssertionScope();
		anime.Data.Should().HaveCount(pageSize);
		anime.Data.First().Titles.First(x => x.Type.Equals("Default")).Title.Should().Be("Eyeshield 21");
		anime.Pagination.CurrentPage.Should().Be(2);
		anime.Pagination.Items.Count.Should().Be(pageSize);
		anime.Pagination.Items.PerPage.Should().Be(pageSize);
	}

	[Test]
	[TestCase("berserk")]
	[TestCase("danganronpa")]
	[TestCase("death")]
	public async Task NonEmptyQuery_ShouldReturnNotNullSearchAnime(string query)
	{
		var returnedAnime = await JikanTests.Instance.SearchAnimeAsync(query);

		returnedAnime.Should().NotBeNull();
	}

	[Test]
	public async Task OnePieceAiringQuery_ShouldReturnAiringOnePieceAnime()
	{
		var config = new AnimeSearchQueryParameters()
			.SetQuery("one p")
			.SetStatus(AiringStatusFilter.Airing)
			.SetType(AnimeTypeFilter.TV);

		var onePieceAnime = await JikanTests.Instance.SearchAnimeAsync(config);

		onePieceAnime.Data.First().Titles.First(x => x.Type.Equals("Default")).Title.Should().Be("One Piece");
	}

	[Test]
	public async Task HaibaneQuery_ShouldReturnHaibaneRenmeiAnime()
	{
		var result = await JikanTests.Instance.SearchAnimeAsync("haibane");

		var firstResult = result.Data.First();
		using var _ = new AssertionScope();
		firstResult.Titles.First(x => x.Type.Equals("Default")).Title.Should().Be("Haibane Renmei");
		firstResult.Type.Should().Be(AnimeType.TV);
		firstResult.Episodes.Should().Be(13);
		firstResult.MalId.Should().Be(387);
	}

	[Test]
	[TestCase("berserk")]
	[TestCase("danganronpa")]
	[TestCase("death")]
	public async Task TVConfig_ShouldReturnNotNullSearchAnime(string query)
	{
		var config = new AnimeSearchQueryParameters()
			.SetQuery(query)
			.SetType(AnimeTypeFilter.TV);

		var returnedAnime = await JikanTests.Instance.SearchAnimeAsync(config);

		returnedAnime.Should().NotBeNull();
	}

	[Test]
	public async Task DanganronpaTVConfig_ShouldReturnDanganronpaAnime()
	{
		var config = new AnimeSearchQueryParameters().SetQuery("danganronpa").SetType(AnimeTypeFilter.TV);

		var anime = await JikanTests.Instance.SearchAnimeAsync(config);

		anime.Pagination.LastVisiblePage.Should().Be(1);
	}

	[Test]
	public async Task FairyTailTVAbove7Config_ShouldFilterFairyTailAnimeScore()
	{
		var searchConfig = new AnimeSearchQueryParameters()
			.SetQuery("Fairy Tail")
			.SetType(AnimeTypeFilter.TV)
			.SetMinScore(7);

		var anime = await JikanTests.Instance.SearchAnimeAsync(searchConfig);

		using var _ = new AssertionScope();
		anime.Data.Should().Contain(x => x.Titles.First(entry => entry.Type.Equals("Default")).Title.Equals("Fairy Tail (2014)"));
		anime.Data.Should().Contain(x => x.Titles.First(entry => entry.Type.Equals("Default")).Title.Equals("Fairy Tail: Final Series"));
		anime.Data.Should().Contain(x => x.Titles.First(entry => entry.Type.Equals("Default")).Title.Equals("Fairy Tail"));
	}

	[Test]
	public async Task BlameSciFiConfig_ShouldFilterBleachSciFi()
	{
		var searchConfig = new AnimeSearchQueryParameters().SetQuery("Blame").SetGenres(24);

		var anime = await JikanTests.Instance.SearchAnimeAsync(searchConfig);

		anime.Data.Select(x => x.Titles.First(x => x.Type.Equals("Default")).Title).Should().Contain("Blame! Movie");
	}

	[Test]
	public async Task BlameSciFiMovieConfig_ShouldFilterBleachMechaMovie()
	{
		var searchConfig = new AnimeSearchQueryParameters()
			.SetQuery("Blame")
			.SetType(AnimeTypeFilter.Movie)
			.SetGenres(24);

		var returnedAnime = await JikanTests.Instance.SearchAnimeAsync(searchConfig);

		returnedAnime.Data.First().Titles.First(x => x.Type.Equals("Default")).Title.Should().Be("Blame! Movie");
	}

	[Test]
	public async Task OneSortByMembersConfig_ShouldSortByPopularityOPMFirst()
	{
		var searchConfig = new AnimeSearchQueryParameters()
			.SetQuery("one")
			.SetOrder(AnimeSearchOrderBy.Members)
			.SetSortDirection(SortDirection.Descending);

		var returnedAnime = await JikanTests.Instance.SearchAnimeAsync(searchConfig);

		var titles = returnedAnime.Data.Select(x => x.Titles.First(x => x.Type.Equals("Default")).Title);
		using var _ = new AssertionScope();
		titles.Should().Contain("One Piece");
		titles.Should().Contain("One Punch Man");
		titles.First().Should().Be("One Punch Man");
	}

	[Test]
	public async Task OneSortByIdConfig_ShouldSortByIdOnePieceFirst()
	{
		var searchConfig = new AnimeSearchQueryParameters()
			.SetQuery("one")
			.SetOrder(AnimeSearchOrderBy.Id)
			.SetSortDirection(SortDirection.Ascending);

		var returnedAnime = await JikanTests.Instance.SearchAnimeAsync(searchConfig);

		returnedAnime.Data.First().Titles.First(x => x.Type.Equals("Default")).Title.Should().Be("One Piece");
	}

	[Test]
	public async Task ProducerKyotoAnimationConfig_ShouldReturnFMPAndLuckyStar()
	{
		var searchConfig = new AnimeSearchQueryParameters().SetProducers(2);

		var returnedAnime = await JikanTests.Instance.SearchAnimeAsync(searchConfig);

		returnedAnime.Data.Should().Contain(x => x.Titles.First(entry => entry.Type.Equals("Default")).Title.Contains("Full Metal Panic? Fumoffu"));
		returnedAnime.Data.Should().Contain(x => x.Titles.First(entry => entry.Type.Equals("Default")).Title.Contains("Lucky☆Star"));
	}

	[Test]
	public void IncorrectProducerConfig_ShouldReturnEmpty()
	{
		var searchFunc = () => new AnimeSearchQueryParameters().SetProducers(-1);

		searchFunc.Should().ThrowExactly<JikanParameterValidationException>();
	}

	[Test]
	public async Task EmptyQueryNullConfig_ShouldThrowValidationException()
	{
		var func = JikanTests.Instance.Awaiting(x => x.SearchAnimeAsync((AnimeSearchQueryParameters)null));

		await func.Should().ThrowExactlyAsync<NullReferenceException>();
	}

	[Test]
	public async Task EmptyQueryActionTvAnime_ShouldFindCowboyBebopAndOnPiece()
	{
		var searchConfig = new AnimeSearchQueryParameters().SetType(AnimeTypeFilter.TV).SetGenres(1);

		var returnedAnime = await JikanTests.Instance.SearchAnimeAsync(searchConfig);

		var titles = returnedAnime.Data.Select(x => x.Titles.First(entry => entry.Type.Equals("Default")).Title);

        using var _ = new AssertionScope();
		titles.Should().Contain("Cowboy Bebop").And.Contain("One Piece");
	}

	[Test]
	[TestCase(int.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public void EmptyQueryActionTvAnimeInvalidPage_ShouldThrowValidationException(int page)
    {
        var searchFunc = page.Invoking(p => new AnimeSearchQueryParameters().SetPage(p).SetType(AnimeTypeFilter.TV).SetGenres(1));

		searchFunc.Should().ThrowExactly<JikanParameterValidationException>();
	}

	[Test]
	public async Task EmptyQueryActionTvAnimeFirstPage_ShouldFindCowboyBebopAndOnPiece()
	{
		var searchConfig = new AnimeSearchQueryParameters().SetPage(1).SetType(AnimeTypeFilter.TV).SetGenres(1);

		var returnedAnime = await JikanTests.Instance.SearchAnimeAsync(searchConfig);

		var titles = returnedAnime.Data.Select(x => x.Titles.First(x => x.Type.Equals("Default")).Title);
		using var _ = new AssertionScope();
		titles.Should().Contain("Cowboy Bebop");
		titles.Should().Contain("One Piece");
	}

	[Test]
	public async Task EmptyQueryActionTvAnimeThirdPage_ShouldFindWolfRainAndInitialD()
	{
		var searchConfig = new AnimeSearchQueryParameters()
			.SetPage(3)
			.SetType(AnimeTypeFilter.TV)
			.SetGenres(1);

		var returnedAnime = await JikanTests.Instance.SearchAnimeAsync(searchConfig);

		var titles = returnedAnime.Data.Select(x => x.Titles.First(x => x.Type.Equals("Default")).Title);
		using var _ = new AssertionScope();
		titles.Should().Contain("Initial D First Stage");
		titles.Should().Contain("Wolf's Rain");
	}

	[Test]
	[TestCase(int.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task GirlQueryActionCompletedAnimeInvalidPage_ShouldThrowValidationException(int page)
	{
		var searchConfig = new AnimeSearchQueryParameters()
			.SetPage(page)
			.SetQuery("girl")
			.SetStatus(AiringStatusFilter.Complete)
			.SetGenres(1);

		var func = JikanTests.Instance.Awaiting(x => x.SearchAnimeAsync(searchConfig));

		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task OneQueryActionCompletedAnimeSecondPage_ShouldReturnNotEmptyCollection()
	{
		var searchConfig = new AnimeSearchQueryParameters()
			.SetPage(2)
			.SetQuery("one")
			.SetStatus(AiringStatusFilter.Complete)
			.SetGenres(1);

		var returnedAnime = await JikanTests.Instance.SearchAnimeAsync(searchConfig);

		using (new AssertionScope())
		{
			returnedAnime.Should().NotBeNull();
			returnedAnime.Data.Should().NotBeEmpty();
		}
	}

	[Test]
	public async Task GenreInclusion_ShouldReturnNotEmptyCollection()
	{
		var searchConfig = new AnimeSearchQueryParameters()
			.SetGenres(new long[] {1, 4})
			.SetOrder(AnimeSearchOrderBy.Score)
			.SetSortDirection(SortDirection.Descending);

		var returnedAnime = await JikanTests.Instance.SearchAnimeAsync(searchConfig);

		returnedAnime.Data.Should().NotBeEmpty();
	}
}
