using Aosta.Jikan.Consts;
using Aosta.Jikan.Enums;
using Aosta.Jikan.Models.Search;
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
		var config = new MangaSearchConfig {Page = page};
            
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
		var config = new MangaSearchConfig {PageSize = pageSize};
            
		// When
		var func = JikanTests.Instance.Awaiting(x => x.SearchMangaAsync(config));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}
        
	[Test]
	public async Task GivenSecondPage_ShouldReturnSecondPage()
	{
		// Given
		var config = new MangaSearchConfig {Page = 2};
            
		// When
		var manga = await JikanTests.Instance.SearchMangaAsync(config);

		// Then
		using var _ = new AssertionScope();
		manga.Data.Should().HaveCount(ParameterConsts.MaximumPageSize);
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
		var config = new MangaSearchConfig {PageSize = pageSize};
            
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
		var config = new MangaSearchConfig {Page = 2, PageSize = pageSize};
            
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
	public async Task YotsubatoQueryNullConfig_ShouldThrowValidationException()
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.SearchMangaAsync((MangaSearchConfig) null));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}
        
	[Test]
	public async Task YotsubaPublishingQuery_ShouldReturnPublishedYotsubatoManga()
	{
		// Given
		var searchConfig = new MangaSearchConfig()
		{
			Query = "yotsuba",
			Status = PublishingStatus.Publishing
		};

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
		var searchConfig = new MangaSearchConfig()
		{
			Query = "yotsuba",
			Page = page
		};
            
		// When
		var func = JikanTests.Instance.Awaiting(x => x.SearchMangaAsync(searchConfig));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task GirlQuerySecondPage_ShouldFindGirlManga()
	{
		// Given
		var searchConfig = new MangaSearchConfig()
		{
			Query = "girl",
			Page = 2
		};
            
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
		var searchConfig = new MangaSearchConfig
		{
			Query = query,
			Type = MangaType.Manga
		};

		// When
		var returnedManga = await JikanTests.Instance.SearchMangaAsync(searchConfig);

		// Then
		returnedManga.Data.Should().NotBeEmpty();
	}

	[Test]
	public async Task DanganronpaMangaConfig_ShouldReturnDanganronpaManga()
	{
		// Given
		var searchConfig = new MangaSearchConfig
		{
			Query = "danganronpa",
			Type = MangaType.Manga
		};

		// When
		var danganronpaManga = await JikanTests.Instance.SearchMangaAsync(searchConfig);

		// Then
		danganronpaManga.Data.Should().HaveCountGreaterThan(12);
	}
        
        
	[Test]
	public async Task DanganronpaMangaAbove7Config_ShouldReturnDanganronpaMangaScore()
	{
		// Given
		var searchConfig = new MangaSearchConfig
		{
			Query = "danganronpa",
			Type = MangaType.Manga,
			MinimumScore = 7
		};

		// When
		var danganronpaManga = await JikanTests.Instance.SearchMangaAsync(searchConfig);

		danganronpaManga.Data.First().Title.Should().Contain("Dangan");
	}

	[Test]
	public async Task MangaGameGenreConfig_ShouldFilterMetalFightBeyblade()
	{
		// Given
		var searchConfig = new MangaSearchConfig
		{
			Query = "metal",
			Type = MangaType.Manga,
		};
		searchConfig.Genres.Add(MangaGenreSearch.Game);

		// When
		var returnedManga = await JikanTests.Instance.SearchMangaAsync(searchConfig);

		returnedManga.Data.First().Title.Should().Be("Metal Fight Beyblade");
	}

	[Test]
	public async Task MetalSortByMembersConfig_ShouldSortByPopularityFMAFirst()
	{
		// Given
		var searchConfig = new MangaSearchConfig
		{
			Query = "metal",
			OrderBy = MangaSearchOrderBy.Members,
			SortDirection = SortDirection.Descending
		};

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
		var searchConfig = new MangaSearchConfig
		{
			Query = "one",
			OrderBy = MangaSearchOrderBy.MalId,
			SortDirection = SortDirection.Ascending
		};

		// When
		var returnedManga = await JikanTests.Instance.SearchMangaAsync(searchConfig);

		// Then
		returnedManga.Data.First().Title.Should().Be("One Piece");
	}

	[Test]
	public async Task ShonenJumpMagazineConfig_ShouldReturnNarutoAndBleach()
	{
		// Given
		var searchConfig = new MangaSearchConfig
		{
			MagazineIds = { 83 }
		};

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
	[TestCase((PublishingStatus)int.MaxValue, null, null, null, null)]
	[TestCase((PublishingStatus)int.MinValue, null, null, null, null)]
	[TestCase(null, (MangaType)int.MaxValue, null, null, null)]
	[TestCase(null, (MangaType)int.MinValue, null, null, null)]
	[TestCase(null, null, (MangaSearchOrderBy)int.MaxValue, null, null)]
	[TestCase(null, null, (MangaSearchOrderBy)int.MinValue, null, null)]
	[TestCase(null, null, MangaSearchOrderBy.Chapters, (SortDirection)int.MaxValue, null)]
	[TestCase(null, null, MangaSearchOrderBy.Chapters, (SortDirection)int.MinValue, null)]
	[TestCase(null, null, null, null, (MangaGenreSearch)int.MaxValue)]
	[TestCase(null, null, null, null, (MangaGenreSearch)int.MinValue)]
	public async Task EmptyQueryWithConfigWithInvalidEnums_ShouldThrowValidationException(
		PublishingStatus? airingStatus,
		MangaType? mangaType,
		MangaSearchOrderBy? orderBy,
		SortDirection? sortDirection,
		MangaGenreSearch? genreSearch
	)
	{
		// Given
		var searchConfig = new MangaSearchConfig()
		{
			Status = airingStatus.GetValueOrDefault(),
			Type = mangaType.GetValueOrDefault(),
			OrderBy = orderBy.GetValueOrDefault(),
			SortDirection = sortDirection.GetValueOrDefault(),
			Genres = genreSearch.HasValue ? new[] { genreSearch.Value } : Array.Empty<MangaGenreSearch>()
		};

		// When
		var func = JikanTests.Instance.Awaiting(x => x.SearchMangaAsync(searchConfig));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task EmptyQueryActionManga_ShouldFindBerserkAndBlackCat()
	{
		// Given
		var searchConfig = new MangaSearchConfig
		{
			Genres = new List<MangaGenreSearch> { MangaGenreSearch.Action }
		};

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
		var searchConfig = new MangaSearchConfig
		{
			Page = 1,
			Genres = new List<MangaGenreSearch> { MangaGenreSearch.Action }
		};

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
		var searchConfig = new MangaSearchConfig
		{
			Page = 2,
			Genres = new List<MangaGenreSearch> { MangaGenreSearch.Action }
		};

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
		var searchConfig = new MangaSearchConfig()
		{
			Query = "ore",
			Page = page,
			Status = PublishingStatus.Publishing
		};

		// When
		var func = JikanTests.Instance.Awaiting(x => x.SearchMangaAsync(searchConfig));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task OreQueryComedyMangaSecondPage_ShouldReturnNotEmptyCollection()
	{
		// Given
		var searchConfig = new MangaSearchConfig
		{
			Query = "ore",
			Page = 2,
			Genres = new List<MangaGenreSearch> { MangaGenreSearch.Comedy },
			Type = MangaType.Manga
		};

		// When
		var returnedManga = await JikanTests.Instance.SearchMangaAsync(searchConfig);

		// Then
		returnedManga.Data.Should().NotBeNullOrEmpty();
	}

	[Test]
	public async Task GenreInclusion_ShouldReturnNotEmptyCollection()
	{
		// Given
		var searchConfig = new MangaSearchConfig
		{
			Genres = new List<MangaGenreSearch> { MangaGenreSearch.Comedy },
			Type = MangaType.Manga
		};

		// When
		var returnedManga = await JikanTests.Instance.SearchMangaAsync(searchConfig);

		// Then
		returnedManga.Data.Should().NotBeNullOrEmpty();
	}

	[Test]
	public async Task GenreExclusion_ShouldReturnNotEmptyCollection()
	{
		// Given
		var searchConfig = new MangaSearchConfig
		{
			ExcludedGenres = new List<MangaGenreSearch> { MangaGenreSearch.Comedy },
			Type = MangaType.Manga
		};

		// When
		var returnedManga = await JikanTests.Instance.SearchMangaAsync(searchConfig);

		// Then
		returnedManga.Data.Should().NotBeNullOrEmpty();
	}
}