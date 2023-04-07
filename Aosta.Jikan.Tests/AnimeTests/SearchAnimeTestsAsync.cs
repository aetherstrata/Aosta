using Aosta.Core.Utils.Exceptions;
using Aosta.Jikan.Consts;
using Aosta.Jikan.Enums;
using Aosta.Jikan.Models.Search;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.AnimeTests
{
    public class SearchAnimeTestsAsync
    {
	    [Test]
        [TestCase(int.MinValue)]
        [TestCase(-1)]
        [TestCase(0)]
        public async Task InvalidPage_ShouldThrowValidationException(int page)
        {
	        var config = new AnimeSearchConfig { Page = page };
	        var func = JikanTests.Instance.Awaiting(x => x.SearchAnimeAsync(config));

	        await func.Should().ThrowExactlyAsync<ParameterValidationException>();
        }

        [Test]
        [TestCase(int.MinValue)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(26)]
        [TestCase(int.MaxValue)]
        public async Task InvalidPageSize_ShouldThrowValidationException(int pageSize)
        {
	        var config = new AnimeSearchConfig { PageSize = pageSize };
	        var func = JikanTests.Instance.Awaiting(x => x.SearchAnimeAsync(config));

	        await func.Should().ThrowExactlyAsync<ParameterValidationException>();
        }

        [Test]
        public async Task GivenSecondPage_ShouldReturnSecondPage()
        {
	        var config = new AnimeSearchConfig {Page = 2};
	        var anime = await JikanTests.Instance.SearchAnimeAsync(config);

	        using var _ = new AssertionScope();
            anime.Data.Should().HaveCount(ParameterConsts.MaximumPageSize);
            anime.Data.First().Title.Should().Be("Rurouni Kenshin: Meiji Kenkaku Romantan - Tsuioku-hen");
            anime.Pagination.LastVisiblePage.Should().BeGreaterThan(780);
            anime.Pagination.CurrentPage.Should().Be(2);
            anime.Pagination.Items.Count.Should().Be(25);
            anime.Pagination.Items.PerPage.Should().Be(25);
        }

        [Test]
        public async Task GivenValidPageSize_ShouldReturnPageSizeNumberOfRecords()
        {
	        const int pageSize = 5;
            var config = new AnimeSearchConfig {PageSize = pageSize};
            var anime = await JikanTests.Instance.SearchAnimeAsync(config);

            using var _ = new AssertionScope();
            anime.Data.Should().HaveCount(pageSize);
            anime.Data.First().Title.Should().Be("Cowboy Bebop");
            anime.Pagination.CurrentPage.Should().Be(1);
            anime.Pagination.Items.Count.Should().Be(pageSize);
            anime.Pagination.Items.PerPage.Should().Be(pageSize);
        }

        [Test]
        public async Task GivenValidPageAndPageSize_ShouldReturnPageSizeNumberOfRecordsFromNextPage()
        {
	        const int pageSize = 5;
            var config = new AnimeSearchConfig {Page = 2, PageSize = pageSize};

            var anime = await JikanTests.Instance.SearchAnimeAsync(config);

            using var _ = new AssertionScope();
            anime.Data.Should().HaveCount(pageSize);
            anime.Data.First().Title.Should().Be("Eyeshield 21");
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
	        var config = new AnimeSearchConfig {Query = query};

	        var returnedAnime = await JikanTests.Instance.SearchAnimeAsync(query);

	        returnedAnime.Should().NotBeNull();
        }

        [Test]
        public async Task OnePieceAiringQuery_ShouldReturnAiringOnePieceAnime()
        {
	        var config = new AnimeSearchConfig()
            {
                Query = "one p",
                Status = AiringStatus.Airing,
                Type = AnimeType.TV
            };

	        var onePieceAnime = await JikanTests.Instance.SearchAnimeAsync(config);

	        onePieceAnime.Data.First().Title.Should().Be("One Piece");
        }

        [Test]
        public async Task HaibaneQuery_ShouldReturnHaibaneRenmeiAnime()
        {
	        var result = await JikanTests.Instance.SearchAnimeAsync("haibane");

	        var firstResult = result.Data.First();
            using var _ = new AssertionScope();
            firstResult.Title.Should().Be("Haibane Renmei");
            firstResult.Type.Should().Be("TV");
            firstResult.Episodes.Should().Be(13);
            firstResult.MalId.Should().Be(387);
        }

        [Test]
        [TestCase("berserk")]
        [TestCase("danganronpa")]
        [TestCase("death")]
        public async Task TVConfig_ShouldReturnNotNullSearchAnime(string query)
        {
	        var config = new AnimeSearchConfig
            {
                Query = query,
                Type = AnimeType.TV
            };

	        var returnedAnime = await JikanTests.Instance.SearchAnimeAsync(config);

	        returnedAnime.Should().NotBeNull();
        }

        [Test]
        public async Task DanganronpaTVConfig_ShouldReturnDanganronpaAnime()
        {
	        var config = new AnimeSearchConfig
            {
                Query = "danganronpa",
                Type = AnimeType.TV
            };

	        var anime = await JikanTests.Instance.SearchAnimeAsync(config);

	        anime.Pagination.LastVisiblePage.Should().Be(1);
        }

        [Test]
        public async Task FairyTailTVAbove7Config_ShouldFilterFairyTailAnimeScore()
        {
	        var searchConfig = new AnimeSearchConfig
            {
                Query = "Fairy Tail",
                Type = AnimeType.TV,
                MinimumScore = 7
            };

	        var anime = await JikanTests.Instance.SearchAnimeAsync(searchConfig);

	        using var _ = new AssertionScope();
            anime.Data.Should().Contain(x => x.Title.Equals("Fairy Tail (2014)"));
            anime.Data.Should().Contain(x => x.Title.Equals("Fairy Tail: Final Series"));
            anime.Data.Should().Contain(x => x.Title.Equals("Fairy Tail"));
        }

        [Test]
        public async Task BlameSciFiConfig_ShouldFilterBleachSciFi()
        {
	        var searchConfig = new AnimeSearchConfig { Query = "Blame" } ;
            searchConfig.Genres.Add(AnimeGenreSearch.SciFi);

            var anime = await JikanTests.Instance.SearchAnimeAsync(searchConfig);

            anime.Data.Select(x => x.Title).Should().Contain("Blame! Movie");
        }

        [Test]
        public async Task BlameSciFiMovieConfig_ShouldFilterBleachMechaMovie()
        {
	        var searchConfig = new AnimeSearchConfig
            {
                Query = "Blame",
                Type = AnimeType.Movie
            };
            searchConfig.Genres.Add(AnimeGenreSearch.SciFi);

            var returnedAnime = await JikanTests.Instance.SearchAnimeAsync(searchConfig);

            returnedAnime.Data.First().Title.Should().Be("Blame! Movie");
        }

        [Test]
        public async Task OneSortByMembersConfig_ShouldSortByPopularityOPMFirst()
        {
	        var searchConfig = new AnimeSearchConfig
            {
                Query = "one",
                OrderBy = AnimeSearchOrderBy.Members,
                SortDirection = SortDirection.Descending
            };

	        var returnedAnime = await JikanTests.Instance.SearchAnimeAsync(searchConfig);

	        var titles = returnedAnime.Data.Select(x => x.Title);
            using var _ = new AssertionScope();
            titles.Should().Contain("One Piece");
            titles.Should().Contain("One Punch Man");
            titles.First().Should().Be("One Punch Man");
        }

		[Test]
		public async Task OneSortByIdConfig_ShouldSortByIdOnePieceFirst()
		{
			var searchConfig = new AnimeSearchConfig
			{
				Query = "one",
				OrderBy = AnimeSearchOrderBy.Id,
				SortDirection = SortDirection.Ascending
			};

			var returnedAnime = await JikanTests.Instance.SearchAnimeAsync(searchConfig);

			returnedAnime.Data.First().Title.Should().Be("One Piece");
		}

		[Test]
		public async Task ProducerKyotoAnimationConfig_ShouldReturnFMPAndLuckyStar()
		{
			var searchConfig = new AnimeSearchConfig
			{
				ProducerIds = { 2 }
			};

			var returnedAnime = await JikanTests.Instance.SearchAnimeAsync(searchConfig);

			returnedAnime.Data.Should().Contain(x => x.Title.Contains("Full Metal Panic? Fumoffu"));
			returnedAnime.Data.Should().Contain(x => x.Title.Contains("Lucky☆Star"));
		}

		[Test]
		public async Task IncorrectProducerConfig_ShouldReturnEmpty()
		{
			var searchConfig = new AnimeSearchConfig
			{
				ProducerIds = { -1 }
			};

			var returnedAnime = await JikanTests.Instance.SearchAnimeAsync(searchConfig);

			returnedAnime.Data.Should().BeEmpty();
		}

		[Test]
		public async Task EmptyQueryNullConfig_ShouldThrowValidationException()
		{
			var func = JikanTests.Instance.Awaiting(x => x.SearchAnimeAsync((AnimeSearchConfig)null));

			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}

		[Test]
		[TestCase((AiringStatus)int.MaxValue, null, null, null, null, null)]
		[TestCase((AiringStatus)int.MinValue, null, null, null, null, null)]
		[TestCase(null, (AnimeAgeRating)int.MaxValue, null, null, null, null)]
		[TestCase(null, (AnimeAgeRating)int.MinValue, null, null, null, null)]
		[TestCase(null, null, (AnimeType)int.MaxValue, null, null, null)]
		[TestCase(null, null, (AnimeType)int.MinValue, null, null, null)]
		[TestCase(null, null, null, (AnimeSearchOrderBy)int.MaxValue, null, null)]
		[TestCase(null, null, null, (AnimeSearchOrderBy)int.MinValue, null, null)]
		[TestCase(null, null, null, AnimeSearchOrderBy.Episodes, (SortDirection)int.MaxValue, null)]
		[TestCase(null, null, null, AnimeSearchOrderBy.Episodes, (SortDirection)int.MinValue, null)]
		[TestCase(null, null, null, null, null, (AnimeGenreSearch)int.MaxValue)]
		[TestCase(null, null, null, null, null, (AnimeGenreSearch)int.MinValue)]
		public async Task EmptyQueryWithConfigWithInvalidEnums_ShouldThrowValidationException(
			AiringStatus? airingStatus,
			AnimeAgeRating? rating,
			AnimeType? mangaType,
			AnimeSearchOrderBy? orderBy,
			SortDirection? sortDirection,
			AnimeGenreSearch? genreSearch
		)
		{
			var searchConfig = new AnimeSearchConfig()
			{
				Status = airingStatus.GetValueOrDefault(),
				Rating = rating.GetValueOrDefault(),
				Type = mangaType.GetValueOrDefault(),
				OrderBy = orderBy.GetValueOrDefault(),
				SortDirection = sortDirection.GetValueOrDefault(),
				Genres = genreSearch.HasValue ? new[] { genreSearch.Value } : Array.Empty<AnimeGenreSearch>()
			};

			var func = JikanTests.Instance.Awaiting(x => x.SearchAnimeAsync(searchConfig));

			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}

		[Test]
		public async Task EmptyQueryActionTvAnime_ShouldFindCowboyBebopAndOnPiece()
		{
			var searchConfig = new AnimeSearchConfig
			{
				Type = AnimeType.TV,
				Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action }
			};

			var returnedAnime = await JikanTests.Instance.SearchAnimeAsync(searchConfig);

			var titles = returnedAnime.Data.Select(x => x.Title);
			using var _ = new AssertionScope();
			titles.Should().Contain("Cowboy Bebop");
			titles.Should().Contain("One Piece");
		}

		[Test]
		[TestCase(int.MinValue)]
		[TestCase(-1)]
		[TestCase(0)]
		public async Task EmptyQueryActionTvAnimeInvalidPage_ShouldThrowValidationException(int page)
		{
			var searchConfig = new AnimeSearchConfig
			{
				Page = page,
				Type = AnimeType.TV,
				Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action }
			};

			var func = JikanTests.Instance.Awaiting(x => x.SearchAnimeAsync(searchConfig));

			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}

		[Test]
		public async Task EmptyQueryActionTvAnimeFirstPage_ShouldFindCowboyBebopAndOnPiece()
		{
			var searchConfig = new AnimeSearchConfig
			{
				Page = 1,
				Type = AnimeType.TV,
				Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action }
			};

			var returnedAnime = await JikanTests.Instance.SearchAnimeAsync(searchConfig);

			var titles = returnedAnime.Data.Select(x => x.Title);
			using var _ = new AssertionScope();
			titles.Should().Contain("Cowboy Bebop");
			titles.Should().Contain("One Piece");
		}

		[Test]
		public async Task EmptyQueryActionTvAnimeThirdPage_ShouldFindWolfRainAndInitialD()
		{
			var searchConfig = new AnimeSearchConfig
			{
				Page = 3,
				Type = AnimeType.TV,
				Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action }
			};

			var returnedAnime = await JikanTests.Instance.SearchAnimeAsync(searchConfig);

			var titles = returnedAnime.Data.Select(x => x.Title);
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
			var searchConfig = new AnimeSearchConfig
			{
				Page = page,
				Query = "girl",
				Status = AiringStatus.Complete,
				Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action }
			};

			var func = JikanTests.Instance.Awaiting(x => x.SearchAnimeAsync(searchConfig));

			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}

		[Test]
		public async Task OneQueryActionCompletedAnimeSecondPage_ShouldReturnNotEmptyCollection()
		{
			var searchConfig = new AnimeSearchConfig
			{
				Query = "one",
				Page = 2,
				Status = AiringStatus.Complete,
				Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action }
			};

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
			var searchConfig = new AnimeSearchConfig
			{
				Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action, AnimeGenreSearch.Comedy },
				OrderBy = AnimeSearchOrderBy.Score,
				SortDirection = SortDirection.Descending
			};

			var returnedAnime = await JikanTests.Instance.SearchAnimeAsync(searchConfig);

			returnedAnime.Data.Should().NotBeEmpty();
		}
    }
}