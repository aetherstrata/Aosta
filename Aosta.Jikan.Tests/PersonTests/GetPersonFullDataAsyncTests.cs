using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.PersonTests;

public class GetPersonFullDataAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetPersonFullDataAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<ParameterValidationException>();
	}
		
	[Test]
	public async Task YuasaId_ShouldParseMasaakiYuasa()
	{
		// Given
		var yuasa = await JikanTests.Instance.GetPersonFullDataAsync(5068);

		// Then
		using var _ = new AssertionScope();
		yuasa.Data.Mangaography.Should().BeEmpty();
		yuasa.Data.Animeography.Should().HaveCountGreaterThan(70);
		yuasa.Data.Animeography.Should().Contain(x => x.Anime.Title.Equals("Ping Pong the Animation"));
		yuasa.Data.Animeography.Should().Contain(x => x.Anime.Title.Equals("Yojouhan Shinwa Taikei"));
		yuasa.Data.VoiceActingRoles.Should().BeEmpty();
	}
		
	[Test]
	public async Task OdaId_ShouldParseEiichiroOda()
	{
		// Given
		var oda = await JikanTests.Instance.GetPersonFullDataAsync(1881);

		// Then
		using var _ = new AssertionScope();
		oda.Data.Mangaography.Should().HaveCount(15);
		oda.Data.Mangaography.Should().Contain(x => x.Manga.Title.Equals("One Piece") && x.Position.Equals("Story & Art"));
		oda.Data.Mangaography.Should().Contain(x => x.Manga.Title.Equals("Cross Epoch") && x.Position.Equals("Story & Art"));
		oda.Data.Mangaography.Should().Contain(x => x.Manga.Title.Equals("One Piece Novel: Mugiwara Stories") && x.Position.Equals("Art"));
		oda.Data.Animeography.Should().HaveCountGreaterThan(30);
		oda.Data.Animeography.Should().Contain(x => x.Anime.Title.Equals("One Piece") && x.Position.Equals("add Original Creator Airing"));
		oda.Data.VoiceActingRoles.Should().ContainSingle();
	}
}