using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.UserTests;

public class GetUserClubsAsyncTests
{
	[Test]
	[TestCase(null)]
	[TestCase("")]
	[TestCase("\n\n\t    \t")]
	public async Task InvalidUsername_ShouldThrowValidationException(string username)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetUserClubsAsync(username));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task Progeusz_ShouldParseProgeuszClubs()
	{
		// When
		var clubs = await JikanTests.Instance.GetUserClubsAsync("Progeusz");

		// Then
		using (new AssertionScope())
		{
			clubs.Data.Should().HaveCount(19);
			clubs.Data.Should().Contain(x => x.Name.Equals("Hatsune Miku - the Goddess ~FanClub~"));
			clubs.Data.Should().Contain(x => x.MalId.Equals(65525)); // Manga Sales Rankings
		}
	}

	[Test]
	public async Task Ervelan_ShouldParseErvelanClubs()
	{
		// When
		var clubs = await JikanTests.Instance.GetUserClubsAsync("Ervelan");

		// Then
		using (new AssertionScope())
		{
			clubs.Data.Should().HaveCount(15);
			clubs.Data.Should().Contain(x => x.Name.Equals("JoJo's Bizarre Adventure Club"));
			clubs.Data.Should().Contain(x => x.MalId.Equals(14689)); // Sakuya fanclub
		}
	}

	[Test]
	[TestCase(null)]
	[TestCase("")]
	[TestCase("\n\n\t    \t")]
	public async Task InvalidUsernameSecondPage_ShouldThrowValidationException(string username)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetUserClubsAsync(username, 2));

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
		var func = JikanTests.Instance.Awaiting(x => x.GetUserClubsAsync("Ervelan", page));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task ArchaeonSecondPage_ShouldParseArchaeonClubs()
	{
		// When
		var clubs = await JikanTests.Instance.GetUserClubsAsync("Archaeon", 2);

		// Then
		clubs.Data.Should().HaveCountGreaterThan(30);
	}
}
