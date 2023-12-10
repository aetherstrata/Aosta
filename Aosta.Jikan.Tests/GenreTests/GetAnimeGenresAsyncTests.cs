using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Tests.GenreTests;

public class GetAnimeGenresAsyncTests
{
	[Test]
	public async Task NoParameters_ShouldParseAllAvailableGenres()
	{
		// Given
		const int expectedGenreCount = 76;

		// When
		var result = await JikanTests.Instance.GetAnimeGenresAsync();

		// Then
		result.Data.Should().HaveCount(expectedGenreCount);
	}

	[Test]
	[TestCase(GenresFilter.Genres, 18)]
	[TestCase(GenresFilter.ExplicitGenres, 3)]
	[TestCase(GenresFilter.Themes, 50)]
	[TestCase(GenresFilter.Demographics, 5)]
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
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}
}