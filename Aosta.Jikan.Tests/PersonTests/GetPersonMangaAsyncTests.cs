using Aosta.Jikan.Exceptions;

using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.PersonTests;

public class GetPersonMangaAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetPersonMangaAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task YuasaId_ShouldParseMasaakiYuasaAnime()
	{
		// Given
		var yuasa = await JikanTests.Instance.GetPersonMangaAsync(5068);

		// Then
		yuasa.Data.Should().BeEmpty();
	}

	[Test]
	public async Task EiichiroOdaId_ShouldParseEiichiroOda()
	{
		// Given
		var oda = await JikanTests.Instance.GetPersonMangaAsync(1881);

		// Then
		using (new AssertionScope())
		{
			oda.Data.Should().HaveCount(15);
			oda.Data.Should().Contain(x => x.Manga.Title.Equals("One Piece") && x.Position.Equals("Story & Art"));
			oda.Data.Should().Contain(x => x.Manga.Title.Equals("Cross Epoch") && x.Position.Equals("Story & Art"));
			oda.Data.Should().Contain(x => x.Manga.Title.Equals("One Piece Novel: Mugiwara Stories") && x.Position.Equals("Art"));
		}
	}
}
