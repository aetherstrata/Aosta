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
		using (new AssertionScope())
		{
			animeList.Should().NotBeNull();
			animeList.Data.Count.Should().Be(300);
			animeList.Data.Select(x => x.Anime.Title).Should().Contain("Akira");
		}
	}

	[Test]
	[TestCase(null)]
	[TestCase("")]
	[TestCase("\n\n\t    \t")]
	public async Task InvalidUsernameSecondPage_ShouldThrowValidationException(string username)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetUserAnimeListAsync(username, 2));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	[TestCase(int.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task ValidUsernameInvalidPage_ShouldThrowValidationException(int page)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetUserAnimeListAsync("Ervelan", page));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task ErvelanSecondPage_ShouldParseErvelanAnimeListSecondPage()
	{
		// When
		var animeList = await JikanTests.Instance.GetUserAnimeListAsync("Ervelan", 2);

		// Then
		using (new AssertionScope())
		{
			animeList.Should().NotBeNull();
			animeList.Data.Count.Should().Be(300);
		}
	}

	[Test]
	public async Task onrix_ShouldParseOnrixAnimeList()
	{
		// When
		var animeList = await JikanTests.Instance.GetUserAnimeListAsync("onrix");

		// Then
		using (new AssertionScope())
		{
			animeList.Should().NotBeNull();
			animeList.Data.Count.Should().Be(122);
		}
	}
}