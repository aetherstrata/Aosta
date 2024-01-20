using Aosta.Jikan.Exceptions;

using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.PersonTests;

public class GetPersonAnimeAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetPersonAnimeAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task YuasaId_ShouldParseMasaakiYuasaAnime()
	{
		// Given
		var yuasa = await JikanTests.Instance.GetPersonAnimeAsync(5068);

		// Then
		using (new AssertionScope())
		{
			yuasa.Data.Should().HaveCountGreaterThan(70);
			yuasa.Data.Should().Contain(x => x.Anime.Title.Equals("Ping Pong the Animation"));
			yuasa.Data.Should().Contain(x => x.Anime.Title.Equals("Yojouhan Shinwa Taikei"));
		}
	}

	[Test]
	public async Task SekiTomokazuId_ShouldParseSekiTomokazuAnime()
	{
		// Given
		var seki = await JikanTests.Instance.GetPersonAnimeAsync(1);

		// Then
		using (new AssertionScope())
		{
			seki.Data.Should().HaveCountLessThan(20);
			seki.Data.Should().Contain(x => x.Anime.Title.Equals("Anime Tenchou") && x.Position.Equals("add Theme Song Performance"));
		}
	}
}