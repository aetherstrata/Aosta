using Aosta.Jikan.Enums;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.UserTests;

public class GetUserHistoryAsyncTests
{
	[Test]
	[TestCase(null)]
	[TestCase("")]
	[TestCase("\n\n\t    \t")]
	public async Task InvalidUsername_ShouldThrowValidationException(string username)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetUserHistoryAsync(username));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task Nekomata_ShouldParseNekomataHistory()
	{
		// When
		var userHistory = await JikanTests.Instance.GetUserHistoryAsync("Nekomata1037");

		// Then
		using (new AssertionScope())
		{
			userHistory.Should().NotBeNull();
			userHistory.Data.Count.Should().BeGreaterOrEqualTo(0);
		}
	}

	[Test]
	[TestCase(null)]
	[TestCase("")]
	[TestCase("\n\n\t    \t")]
	public async Task InvalidUsernameWithExtension_ShouldThrowValidationException(string username)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetUserHistoryAsync(username, UserHistoryExtension.Manga));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	[TestCase((UserHistoryExtension)int.MaxValue)]
	[TestCase((UserHistoryExtension)int.MinValue)]
	public async Task ErvelanWithInvalidExtension_ShouldThrowValidationException(UserHistoryExtension userHistoryExtension)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetUserHistoryAsync("Ervelan", userHistoryExtension));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	[TestCase(UserHistoryExtension.Anime)]
	[TestCase(UserHistoryExtension.Manga)]
	public async Task ErvelanHistoryWithFilter_ShouldParseErvelanMangaHistory(UserHistoryExtension userHistoryExtension)
	{
		// When
		var userHistory = await JikanTests.Instance.GetUserHistoryAsync("Ervelan", userHistoryExtension);

		// Then
		using (new AssertionScope())
		{
			userHistory.Should().NotBeNull();
			userHistory.Data.Count.Should().BeGreaterOrEqualTo(0);
		}
	}
}