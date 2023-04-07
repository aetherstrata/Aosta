﻿using Aosta.Core.Utils.Exceptions;
using Aosta.Jikan.Enums;

namespace Aosta.Jikan.Tests.GenreTests;

public class GetMangaGenresAsyncTests
{
	[Test]
	public async Task NoParameters_ShouldParseAllAvailableGenres()
	{
		// Given
		const int expectedGenreCount = 91;

		// When
		var result = await JikanTests.Instance.GetMangaGenresAsync();

		// Then
		result.Data.Should().HaveCount(expectedGenreCount);
	}

	[Test]
	[TestCase(GenresFilter.Genres, 64)]
	[TestCase(GenresFilter.ExplicitGenres, 3)]
	[TestCase(GenresFilter.Themes, 19)]
	[TestCase(GenresFilter.Demographics, 5)]
	public async Task WithFilter_ShouldParseFilteredGenres(GenresFilter filter, int expectedGenreCount)
	{
		// When
		var result = await JikanTests.Instance.GetMangaGenresAsync(filter);

		// Then
		result.Data.Should().HaveCount(expectedGenreCount);
	}

	[Test]
	[TestCase((GenresFilter)int.MaxValue)]
	[TestCase((GenresFilter)int.MinValue)]
	public async Task InvalidFilter_ShouldThrowValidationException(GenresFilter filter)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetMangaGenresAsync(filter));

		// Then
		await func.Should().ThrowExactlyAsync<ParameterValidationException>();
	}
}