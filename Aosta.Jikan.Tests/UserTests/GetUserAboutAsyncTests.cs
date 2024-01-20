using Aosta.Jikan.Exceptions;

namespace Aosta.Jikan.Tests.UserTests;

public class GetUserAboutAsyncTests
{
	[Test]
	[TestCase(null)]
	[TestCase("")]
	[TestCase("\n\n\t    \t")]
	public async Task nvalidUsername_ShouldThrowValidationException(string username)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetUserAboutAsync(username));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task Ervelan_ShouldParseErvelanAbout()
	{
		// When
		var user = await JikanTests.Instance.GetUserAboutAsync("Ervelan");

		// Then
		user.Data.About.Should().Contain("Welcome to my profile!");
	}
}
