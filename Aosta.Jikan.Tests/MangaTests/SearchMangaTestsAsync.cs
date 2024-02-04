using Aosta.Jikan.Exceptions;
using Aosta.Jikan.Query;
using Aosta.Jikan.Query.Enums;
using Aosta.Jikan.Query.Parameters;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.MangaTests;

public class SearchMangaAsyncTests
{
	[Test]
	[TestCase(int.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidPage_ShouldThrowValidationException(int page)
	{
		// Given
		var config = MangaSearchQueryParameters.Create().Page(page);

		// When
		var func = JikanTests.Instance.Awaiting(x => x.SearchMangaAsync(config));

		// Then
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
		// Given
		var config = MangaSearchQueryParameters.Create().Limit(pageSize);

		// When
		var func = JikanTests.Instance.Awaiting(x => x.SearchMangaAsync(config));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task GivenSecondPage_ShouldReturnSecondPage()
	{
		// Given
		var config = MangaSearchQueryParameters.Create().Page(2);

		// When
		var manga = await JikanTests.Instance.SearchMangaAsync(config);

		// Then
		using var _ = new AssertionScope();
		manga.Data.Should().HaveCount(JikanParameterConsts.MAXIMUM_PAGE_SIZE);
		manga.Data.First().Title.Should().Be("Nana");
		manga.Pagination.LastVisiblePage.Should().BeGreaterThan(780);
		manga.Pagination.CurrentPage.Should().Be(2);
		manga.Pagination.Items.Count.Should().Be(25);
		manga.Pagination.Items.PerPage.Should().Be(25);
	}

	[Test]
	public async Task GivenValidPageSize_ShouldReturnPageSizeNumberOfRecords()
	{
		// Given
		const int pageSize = 5;
		var config = MangaSearchQueryParameters.Create().Limit(pageSize);

		// When
		var manga = await JikanTests.Instance.SearchMangaAsync(config);

		// Then
		using var _ = new AssertionScope();
		manga.Data.Should().HaveCount(pageSize);
		manga.Data.First().Title.Should().Be("Monster");
		manga.Pagination.CurrentPage.Should().Be(1);
		manga.Pagination.Items.Count.Should().Be(pageSize);
		manga.Pagination.Items.PerPage.Should().Be(pageSize);
	}

	[Test]
	public async Task GivenValidPageAndPageSize_ShouldReturnPageSizeNumberOfRecordsFromNextPage()
	{
		// Given
		const int pageSize = 5;
		var config = MangaSearchQueryParameters.Create().Page(2).Limit(pageSize);

		// When
		var manga = await JikanTests.Instance.SearchMangaAsync(config);

		// Then
		using var _ = new AssertionScope();
		manga.Data.Should().HaveCount(pageSize);
		manga.Data.First().Title.Should().Be("Full Moon wo Sagashite");
		manga.Pagination.CurrentPage.Should().Be(2);
		manga.Pagination.Items.Count.Should().Be(pageSize);
		manga.Pagination.Items.PerPage.Should().Be(pageSize);
	}

	[Test]
	[TestCase("berserk")]
	[TestCase("monster")]
	[TestCase("death")]
	public async Task NonEmptyQuery_ShouldReturnNotNullSearchManga(string query)
	{
		// When
		var returnedManga = await JikanTests.Instance.SearchMangaAsync(query);

		// Then
		returnedManga.Data.Should().NotBeEmpty();
	}

	[Test]
	public async Task SearchManga_DanganronpaQuery_ShouldReturnDanganronpaManga()
	{
		// When
		var danganronpaManga = await JikanTests.Instance.SearchMangaAsync("danganronpa");

		danganronpaManga.Data.Should().HaveCountGreaterThan(15);
	}

	[Test]
	public async Task YotsubaQuery_ShouldReturnYotsubatoManga()
	{
		// When
		var yotsubato = await JikanTests.Instance.SearchMangaAsync("yotsuba");

		// Then
		var firstResult = yotsubato.Data.First();
		using var _ = new AssertionScope();
		firstResult.Title.Should().Be("Yotsuba to!");
		firstResult.Type.Should().Be("Manga");
		firstResult.Volumes.Should().BeNull();
		firstResult.MalId.Should().Be(104);
	}

	[Test]
	public async Task YotsubaPublishingQuery_ShouldReturnPublishedYotsubatoManga()
	{
		// Given
		var searchConfig = MangaSearchQueryParameters.Create()
			.Query("yotsuba")
			.Status(PublishingStatusFilter.Publishing);

		// When
		var yotsubato = await JikanTests.Instance.SearchMangaAsync(searchConfig);

		// Then
		var firstResult = yotsubato.Data.First();
		using var _ = new AssertionScope();
		firstResult.Title.Should().Be("Yotsuba to!");
		firstResult.Type.Should().Be("Manga");
		firstResult.Volumes.Should().BeNull();
		firstResult.MalId.Should().Be(104);
	}

	[Test]
	[TestCase(int.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task YotsubaQueryInvalidPage_ShouldThrowValidationException(int page)
	{
		// Given
		var searchConfig = MangaSearchQueryParameters.Create()
			.Query("yotsuba")
			.Page(page);

		// When
		var func = JikanTests.Instance.Awaiting(x => x.SearchMangaAsync(searchConfig));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task GirlQuerySecondPage_ShouldFindGirlManga()
	{
		// Given
		var searchConfig = MangaSearchQueryParameters.Create()
			.Query("girl")
			.Page(2);

		// When
		var returnedAnime = await JikanTests.Instance.SearchMangaAsync(searchConfig);

		// Then
		using var _ = new AssertionScope();
		returnedAnime.Data.Select(x => x.Title).Should().Contain("Misty Girl");
		returnedAnime.Pagination.LastVisiblePage.Should().BeGreaterThan(15);
	}

	[Test]
	[TestCase("berserk")]
	[TestCase("monster")]
	[TestCase("death")]
	public async Task MangaConfig_ShouldReturnNotNullSearchManga(string query)
	{
		// Given
		var searchConfig = MangaSearchQueryParameters.Create()
			.Query(query)
			.Type(MangaTypeFilter.Manga);

		// When
		var returnedManga = await JikanTests.Instance.SearchMangaAsync(searchConfig);

		// Then
		returnedManga.Data.Should().NotBeEmpty();
	}

	[Test]
	public async Task DanganronpaMangaConfig_ShouldReturnDanganronpaManga()
	{
		// Given
		var searchConfig = MangaSearchQueryParameters.Create()
			.Query("danganronpa")
			.Type(MangaTypeFilter.Manga);

		// When
		var danganronpaManga = await JikanTests.Instance.SearchMangaAsync(searchConfig);

		// Then
		danganronpaManga.Data.Should().HaveCountGreaterThan(12);
	}


	[Test]
	public async Task DanganronpaMangaAbove7Config_ShouldReturnDanganronpaMangaScore()
	{
		// Given
		var searchConfig = MangaSearchQueryParameters.Create()
			.Query("danganronpa")
			.Type(MangaTypeFilter.Manga)
			.MinScore(7);

		// When
		var danganronpaManga = await JikanTests.Instance.SearchMangaAsync(searchConfig);

		danganronpaManga.Data.First().Title.Should().Contain("Dangan");
	}

	[Test]
	public async Task MangaGameGenreConfig_ShouldFilterMetalFightBeyblade()
	{
		// Given
		var searchConfig = MangaSearchQueryParameters.Create()
			.Query("metal")
			.Type(MangaTypeFilter.Manga)
			.Genres(new long[] {11});

		// When
		var returnedManga = await JikanTests.Instance.SearchMangaAsync(searchConfig);

		returnedManga.Data.First().Title.Should().Be("Metal Fight Beyblade");
	}

	[Test]
	public async Task MetalSortByMembersConfig_ShouldSortByPopularityFMAFirst()
	{
		// Given
		var searchConfig = MangaSearchQueryParameters.Create()
			.Query("metal")
			.Order(MangaSearchOrderBy.Members)
			.SortDirection(SortDirection.Descending);

		// When
		var returnedManga = await JikanTests.Instance.SearchMangaAsync(searchConfig);

		// Then
		var titles = returnedManga.Data.Select(x => x.Title);
		using (new AssertionScope())
		{
			titles.Should().Contain("Fullmetal Alchemist");
			titles.Should().Contain("Metallica Metalluca");
			titles.Should().Contain("Full Metal Panic!");
			returnedManga.Data.First().Title.Should().Be("Fullmetal Alchemist");
		}
	}

	[Test]
	public async Task OneSortByIdConfig_ShouldSortByIdOnePieceFirst()
	{
		// Given
		var searchConfig = MangaSearchQueryParameters.Create()
			.Query("one")
			.Order(MangaSearchOrderBy.MalId)
			.SortDirection(SortDirection.Ascending);

		// When
		var returnedManga = await JikanTests.Instance.SearchMangaAsync(searchConfig);

		// Then
		returnedManga.Data.First().Title.Should().Be("One Piece");
	}

	[Test]
	public async Task ShonenJumpMagazineConfig_ShouldReturnNarutoAndBleach()
	{
		// Given
		var searchConfig = MangaSearchQueryParameters.Create()
			.Magazines(new long[] { 83 });

		// When
		var returnedManga = await JikanTests.Instance.SearchMangaAsync(searchConfig);

		// Then
		using (new AssertionScope())
		{
			returnedManga.Data.First().Title.Should().Be("Naruto");
			returnedManga.Data.Skip(1).First().Title.Should().Be("Bleach");
			returnedManga.Data.Should().HaveCount(25);
		}
	}

	[Test]
	[TestCase((PublishingStatusFilter)int.MaxValue, null, null, null, null)]
	[TestCase((PublishingStatusFilter)int.MinValue, null, null, null, null)]
	[TestCase(null, (MangaTypeFilter)int.MaxValue, null, null, null)]
	[TestCase(null, (MangaTypeFilter)int.MinValue, null, null, null)]
	[TestCase(null, null, (MangaSearchOrderBy)int.MaxValue, null, null)]
	[TestCase(null, null, (MangaSearchOrderBy)int.MinValue, null, null)]
	[TestCase(null, null, MangaSearchOrderBy.Chapters, (SortDirection)int.MaxValue, null)]
	[TestCase(null, null, MangaSearchOrderBy.Chapters, (SortDirection)int.MinValue, null)]
	[TestCase(null, null, null, null, int.MaxValue)]
	[TestCase(null, null, null, null, int.MinValue)]
	public async Task EmptyQueryWithConfigWithInvalidEnums_ShouldThrowValidationException(
		PublishingStatusFilter? airingStatus,
		MangaTypeFilter? mangaType,
		MangaSearchOrderBy? orderBy,
		SortDirection? sortDirection,
		int? genreId
	)
	{
		// Given
		var searchConfig = MangaSearchQueryParameters.Create()
			.Status(airingStatus.GetValueOrDefault())
			.Type(mangaType.GetValueOrDefault())
			.Order(orderBy.GetValueOrDefault())
			.SortDirection(sortDirection.GetValueOrDefault())
			.Genres(genreId.HasValue ? [genreId.Value] : Array.Empty<long>());

		// When
		var func = JikanTests.Instance.Awaiting(x => x.SearchMangaAsync(searchConfig));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task EmptyQueryActionManga_ShouldFindBerserkAndBlackCat()
	{
		// Given
		var searchConfig = MangaSearchQueryParameters.Create()
			.Genres(new long[] { 1 });

		// When
		var returnedManga = await JikanTests.Instance.SearchMangaAsync(searchConfig);

		// Then
		var titles = returnedManga.Data.Select(x => x.Title);
		using var _ = new AssertionScope();
		titles.Should().Contain("Berserk");
		titles.Should().Contain("Black Cat");
	}

	[Test]
	public async Task EmptyQueryActionMangaFirstPage_ShouldFindBerserkAndBlackCat()
	{
		// Given
		var searchConfig = MangaSearchQueryParameters.Create()
			.Page(1)
			.Genres(new long[] { 1 });

		// When
		var returnedManga = await JikanTests.Instance.SearchMangaAsync(searchConfig);

		// Then
		var titles = returnedManga.Data.Select(x => x.Title);
		using var _ = new AssertionScope();
		titles.Should().Contain("Berserk");
		titles.Should().Contain("Black Cat");
	}

	[Test]
	public async Task EmptyQueryActionMangaSecondPage_ShouldFindHakushoAndAirGear()
	{
		// Given
		var searchConfig = MangaSearchQueryParameters.Create()
			.Page(2)
			.Genres(new long[] { 1 });

		// When
		var returnedManga = await JikanTests.Instance.SearchMangaAsync(searchConfig);

		// Then
		var titles = returnedManga.Data.Select(x => x.Title);
		using var _ = new AssertionScope();
		titles.Should().Contain("Yuu☆Yuu☆Hakusho");
		titles.Should().Contain("Air Gear");
	}

	[Test]
	[TestCase(int.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task OreQueryWithConfigInvalidPage_ShouldThrowValidationException(int page)
	{
		// Given
		var searchConfig = MangaSearchQueryParameters.Create()
			.Query("ore")
			.Page(page)
			.Status(PublishingStatusFilter.Publishing);

		// When
		var func = JikanTests.Instance.Awaiting(x => x.SearchMangaAsync(searchConfig));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task OreQueryComedyMangaSecondPage_ShouldReturnNotEmptyCollection()
	{
		// Given
		var searchConfig = MangaSearchQueryParameters.Create()
			.Query("ore")
			.Page(2)
			.Type(MangaTypeFilter.Manga);

		// When
		var returnedManga = await JikanTests.Instance.SearchMangaAsync(searchConfig);

		// Then
		returnedManga.Data.Should().NotBeNullOrEmpty();
	}

	[Test]
	public async Task GenreInclusion_ShouldReturnNotEmptyCollection()
	{
		// Given
		var searchConfig = MangaSearchQueryParameters.Create()
			.Query("ore")
			.Page(2)
			.Genres(new long[] { 4 })
			.Type(MangaTypeFilter.Manga);

		// When
		var returnedManga = await JikanTests.Instance.SearchMangaAsync(searchConfig);

		// Then
		returnedManga.Data.Should().NotBeNullOrEmpty();
	}

	[Test]
	public async Task GenreExclusion_ShouldReturnNotEmptyCollection()
	{
		// Given
		var searchConfig = MangaSearchQueryParameters.Create()
			.Query("ore")
			.Page(2)
			.ExcludedGenres(new long[] { 4 })
			.Type(MangaTypeFilter.Manga);

		// When
		var returnedManga = await JikanTests.Instance.SearchMangaAsync(searchConfig);

		// Then
		returnedManga.Data.Should().NotBeNullOrEmpty();
	}
}
