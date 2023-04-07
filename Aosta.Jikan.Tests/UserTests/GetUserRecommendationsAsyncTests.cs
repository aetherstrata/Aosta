using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.UserTests;

public class GetUserRecommendationsAsyncTests
{
	[Test]
	[TestCase(null)]
	[TestCase("")]
	[TestCase("\n\n\t    \t")]
	public async Task InvalidUsername_ShouldThrowValidationException(string username)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetUserRecommendationsAsync(username));

		// Then
		await func.Should().ThrowExactlyAsync<ParameterValidationException>();
	}

	[Test]
	public async Task Ervelan_ShouldParseErvelanRecommendations()
	{
		// When
		var Recommendations = await JikanTests.Instance.GetUserRecommendationsAsync("Ervelan");

		// Then
		using (new AssertionScope())
		{
			Recommendations.Should().NotBeNull();
			Recommendations.Data.Should().BeEmpty();
		}
	}

	[Test]
	public async Task Progeusz_ShouldParseProgeuszRecommendations()
	{
		// When
		var Recommendations = await JikanTests.Instance.GetUserRecommendationsAsync("Progeusz");

		// Then
		using (new AssertionScope())
		{
			Recommendations.Data.Should().ContainSingle().Which.Content.StartsWith("Both anime are survival death games.");
			Recommendations.Pagination.HasNextPage.Should().BeFalse();
		}
	}

	[Test]
	[TestCase(null)]
	[TestCase("")]
	[TestCase("\n\n\t    \t")]
	public async Task InvalidUsernameSecondPage_ShouldThrowValidationException(string username)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetUserRecommendationsAsync(username, 2));

		// Then
		await func.Should().ThrowExactlyAsync<ParameterValidationException>();
	}

	[Test]
	[TestCase(int.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task ValidUsernameInvalidPage_ShouldThrowValidationException(int page)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetUserRecommendationsAsync("Ervelan", page));

		// Then
		await func.Should().ThrowExactlyAsync<ParameterValidationException>();
	}

	[Test]
	public async Task IchiyonyuuzlotySecondPage_ShouldParseIchiyonjuuzlotyRecommendations()
	{
		// When
		var Recommendations = await JikanTests.Instance.GetUserRecommendationsAsync("Ichiyonjuuzloty", 2);

		// Then
		Recommendations.Data.Should().BeEmpty();
	}
}