using Aosta.Jikan.Exceptions;

using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.UserTests;

public class GetUserAnimeListAsyncTests
{
	[Test]
	[TestCase(null)]
	[TestCase("")]
	[TestCase("\n\n\t    \t")]
	public async Task InvalidUsername_ShouldThrowValidationException(string username)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetUserAnimeListAsync(username));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task Ervelan_ShouldParseErvelanAnimeList()
	{
		// When
		var animeList = await JikanTests.Instance.GetUserAnimeListAsync("Ervelan");

		// Then
		using var _ = new AssertionScope();
        animeList.Should().NotBeNull();
        animeList.Data.Count.Should().Be(300);
        animeList.Data.Select(x => x.Anime.Title).Should().Contain("Akira");
	}

	[Test]
	public async Task onrix_ShouldParseOnrixAnimeList()
	{
		// When
		var animeList = await JikanTests.Instance.GetUserAnimeListAsync("onrix");

		// Then
		using var _ = new AssertionScope();
		animeList.Should().NotBeNull();
		animeList.Data.Count.Should().Be(122);
	}
}
