using Aosta.Jikan.Enums;
using Aosta.Jikan.Query.Enums;
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
		var func = JikanTests.Instance.Awaiting(x => x.GetUserHistoryAsync(username, UserHistoryTypeFilter.Manga));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	[TestCase((UserHistoryTypeFilter)int.MaxValue)]
	[TestCase((UserHistoryTypeFilter)int.MinValue)]
	public async Task ErvelanWithInvalidExtension_ShouldThrowValidationException(UserHistoryTypeFilter type)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetUserHistoryAsync("Ervelan", type));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	[TestCase(UserHistoryTypeFilter.Anime)]
	[TestCase(UserHistoryTypeFilter.Manga)]
	public async Task ErvelanHistoryWithFilter_ShouldParseErvelanMangaHistory(UserHistoryTypeFilter type)
	{
		// When
		var userHistory = await JikanTests.Instance.GetUserHistoryAsync("Ervelan", type);

		// Then
		using (new AssertionScope())
		{
			userHistory.Should().NotBeNull();
			userHistory.Data.Count.Should().BeGreaterOrEqualTo(0);
		}
	}
}