using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.ClubTests;

public class GetClubAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetClubAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task BebopId_ShouldParseCowboyBebopClub()
	{
		// When
		var club = await JikanTests.Instance.GetClubAsync(1);

		// Then
		using (new AssertionScope())
		{
			club.Should().NotBeNull();
			club.Data.Category.Should().Be("anime");
			club.Data.Name.Should().Be("Cowboy Bebop");
			club.Data.Access.Should().Be("public");
			club.Data.Created.Should().BeSameDateAs(System.DateTime.Parse("2007-03-29"));
		}
	}

	[Test]
	public async Task AnimeCafeId_ShouldParseAnimeCafeClub()
	{
		// When
		var club = await JikanTests.Instance.GetClubAsync(73113);

		// Then
		using (new AssertionScope())
		{
			club.Should().NotBeNull();
			club.Data.Category.Should().Be("anime");
			club.Data.Access.Should().Be("public");
		}
	}
}