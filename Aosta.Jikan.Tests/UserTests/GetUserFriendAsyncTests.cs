using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.UserTests;

public class GetUserFriendsAsyncTests
{
	[Test]
	[TestCase(null)]
	[TestCase("")]
	[TestCase("\n\n\t    \t")]
	public async Task InvalidUsername_ShouldThrowValidationException(string username)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetUserFriendsAsync(username));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task Ervelan_ShouldParseErvelanFriends()
	{
		// When
		var friends = await JikanTests.Instance.GetUserFriendsAsync("Ervelan");

		// Then
		var friendUsernames = friends.Data.Select(x => x.User.Username);
		using (new AssertionScope())
		{
			friends.Should().NotBeNull();
			friends.Data.Count.Should().BeGreaterThan(20);
			friendUsernames.Should().Contain("SonMati");
			friendUsernames.Should().Contain("Progeusz");
		}
	}

	[Test]
	[TestCase(null)]
	[TestCase("")]
	[TestCase("\n\n\t    \t")]
	public async Task InvalidUsernameWithPage_ShouldThrowValidationException(string username)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetUserFriendsAsync(username, 2));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	[TestCase(int.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task ValidUsernameWithInvalidPage_ShouldThrowValidationException(int page)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetUserFriendsAsync("Ervelan", page));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public void ErvelanTenthPage_ShouldReturnNoFriends()
	{
		// When
		var action = JikanTests.Instance.Awaiting(x => x.GetUserFriendsAsync("Ervelan", 10));

		action.Should().ThrowAsync<JikanRequestException>();
	}
}