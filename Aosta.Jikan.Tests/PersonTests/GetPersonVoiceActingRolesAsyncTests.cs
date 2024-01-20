using Aosta.Jikan.Exceptions;

using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.PersonTests;

public class GetPersonVoiceActingRolesAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetPersonVoiceActingRolesAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task YuasaId_ShouldParseMasaakiYuasaVoiceActingroles()
	{
		// Given
		var yuasa = await JikanTests.Instance.GetPersonVoiceActingRolesAsync(5068);

		// Then
		yuasa.Data.Should().BeEmpty();
	}

	[Test]
	public async Task SekiTomokazuId_ShouldParseSekiTomokazuVoiceActingRoles()
	{
		// Given
		var seki = await JikanTests.Instance.GetPersonVoiceActingRolesAsync(1);

		// Then
		using (new AssertionScope())
		{
			seki.Data.Should().HaveCountGreaterThan(400);
			seki.Data.Should().Contain(x => x.Anime.Title.Equals("JoJo no Kimyou na Bouken Part 6: Stone Ocean") && x.Character.Name.Equals("Pucci, Enrico"));
			seki.Data.Should().Contain(x => x.Anime.Title.Equals("Fate/stay night: Unlimited Blade Works") && x.Character.Name.Equals("Gilgamesh"));
		}
	}

	[Test]
	public async Task SugitaTomokazuId_ShouldParseSugitaTomokazuVoiceActingRoles()
	{
		// Given
		var sugita = await JikanTests.Instance.GetPersonVoiceActingRolesAsync(2);

		// Then
		using (new AssertionScope())
		{
			sugita.Data.Should().HaveCountGreaterThan(450);
			sugita.Data.Should().Contain(x => x.Anime.Title.Equals("JoJo no Kimyou na Bouken (TV)") && x.Character.Name.Equals("Joestar, Joseph"));
			sugita.Data.Should().Contain(x => x.Anime.Title.Equals("Gintama") && x.Character.Name.Equals("Sakata, Gintoki"));
		}
	}
}
