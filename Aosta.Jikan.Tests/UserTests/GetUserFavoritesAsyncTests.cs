using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.UserTests;

public class GetUserFavoritesAsyncTests
{
	[Test]
	[TestCase(null)]
	[TestCase("")]
	[TestCase("\n\n\t    \t")]
	public async Task InvalidUsername_ShouldThrowValidationException(string username)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetUserFavoritesAsync(username));

		// Then
		await func.Should().ThrowExactlyAsync<ParameterValidationException>();
	}

	[Test]
	public async Task Ervelan_ShouldParseErvelanFavorites()
	{
		// When
		var user = await JikanTests.Instance.GetUserFavoritesAsync("Ervelan");

		// Then
		using (new AssertionScope())
		{
			user.Data.Anime.Select(x => x.Title).Should().Contain("Haibane Renmei");
			user.Data.Manga.Select(x => x.Title).Should().Contain("Berserk");
			user.Data.Characters.Select(x => x.Name).Should().Contain("Oshino, Shinobu");
			user.Data.People.Select(x => x.Name).Should().Contain("Araki, Hirohiko");
		}
	}

	[Test]
	public async Task Ervelan_ShouldParseErvelanFavoritesExtraData()
	{
		// When
		var user = await JikanTests.Instance.GetUserFavoritesAsync("Ervelan");

		// Then
		using (new AssertionScope())
		{
			user.Data.Anime.Should().Contain(x => x.Title.Equals("Haibane Renmei") && x.StartYear == 2002);
			user.Data.Manga.Should().Contain(x => x.Title.Equals("Berserk") && x.StartYear == 1989);
		}
	}
	[Test]
	public async Task Nekomata1037_ShouldParseNekomataFavorites()
	{
		// When
		var user = await JikanTests.Instance.GetUserFavoritesAsync("Nekomata1037");

		// Then
		using (new AssertionScope())
		{
			user.Data.Anime.Select(x => x.Title).Should().Contain("Steins;Gate");
			user.Data.Manga.Select(x => x.Title).Should().Contain("Vinland Saga");
			user.Data.Characters.Select(x => x.Name).Should().Contain("Makise, Kurisu");
			user.Data.People.Select(x => x.Name).Should().Contain("Sayuri");
		}
	}
}