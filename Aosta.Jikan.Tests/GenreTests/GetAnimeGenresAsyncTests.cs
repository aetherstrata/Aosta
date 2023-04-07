using Aosta.Core.Utils.Exceptions;
using Aosta.Jikan.Enums;

namespace Aosta.Jikan.Tests.GenreTests
{
	public class GetAnimeGenresAsyncTests
	{
		[Test]
		public async Task NoParameters_ShouldParseAllAvailableGenres()
		{
			// Given
			const int expectedGenreCount = 114;

			// When
			var result = await JikanTests.Instance.GetAnimeGenresAsync();

			// Then
			result.Data.Should().HaveCount(expectedGenreCount);
		}

		[Test]
		[TestCase(GenresFilter.Genres, 55)]
		[TestCase(GenresFilter.ExplicitGenres, 6)]
		[TestCase(GenresFilter.Themes, 41)]
		[TestCase(GenresFilter.Demographics, 12)]
		public async Task WithFilter_ShouldParseFilteredGenres(GenresFilter filter, int expectedGenreCount)
		{
			// When
			var result = await JikanTests.Instance.GetAnimeGenresAsync(filter);

			// Then
			result.Data.Should().HaveCount(expectedGenreCount);
		}

		[Test]
		[TestCase((GenresFilter)int.MaxValue)]
		[TestCase((GenresFilter)int.MinValue)]
		public async Task InvalidFilter_ShouldThrowValidationException(GenresFilter filter)
		{
			// When
			var func = JikanTests.Instance.Awaiting(x => x.GetAnimeGenresAsync(filter));

			// Then
			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}
	}
}