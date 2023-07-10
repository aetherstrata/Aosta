using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.AnimeTests;

public class GetAnimeCharactersAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long malId)
	{
		var func = JikanTests.Instance.Awaiting(x => x.GetAnimeCharactersAsync(malId));

		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task BebopId_ShouldParseCowboyBebopCharacters()
	{
		var bebop = await JikanTests.Instance.GetAnimeCharactersAsync(1);

		bebop.Data.Should().Contain(x => x.Character.Name.Equals("Black, Jet"));
	}

	[Test]
	public async Task BebopId_ShouldParseJetBlackPictures()
	{
		var bebop = await JikanTests.Instance.GetAnimeCharactersAsync(1);

		var jetBlack = bebop.Data.First(x => x.Character.Name.Equals("Black, Jet"));

		using (new AssertionScope())
		{
			jetBlack.Character.Images.WebP.SmallImageUrl.Should().NotBeNullOrEmpty();
			jetBlack.Character.Images.WebP.ImageUrl.Should().NotBeNullOrEmpty();
			jetBlack.Character.Images.JPG.ImageUrl.Should().NotBeNullOrEmpty();
		}
	}

	[Test]
	public async Task BebopId_ShouldParseJetBlackFavorites()
	{
		var bebop = await JikanTests.Instance.GetAnimeCharactersAsync(1);

		var jetBlack = bebop.Data.First(x => x.Character.Name.Equals("Black, Jet"));
		jetBlack.Favorites.Should().BeGreaterThan(1900);
	}

	[Test]
	public async Task BebopId_ShouldParseSpikeSpiegelVoiceActors()
	{
		var bebop = await JikanTests.Instance.GetAnimeCharactersAsync(1);

		var spikeSpiegel = bebop.Data.First(x => x.Character.MalId.Equals(1));

		using (new AssertionScope())
		{
			spikeSpiegel.VoiceActors.Should().ContainSingle(x => x.Language.Equals("Japanese"));
			spikeSpiegel.VoiceActors.Should().ContainSingle(x => x.Person.Name.Equals("Yamadera, Kouichi"));
			spikeSpiegel.VoiceActors.Should().ContainSingle(x => x.Language.Equals("English"));
			spikeSpiegel.VoiceActors.Should().ContainSingle(x => x.Person.Name.Equals("Blum, Steven"));
		}
	}
}