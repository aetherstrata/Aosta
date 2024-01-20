using Aosta.Jikan.Exceptions;

using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.ClubTests;

public class GetClubStaffAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetClubStaffAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task BebopId_ShouldParseCowboyBebopClubStaffList()
	{
		// When
		var club = await JikanTests.Instance.GetClubStaffAsync(1);

		// Then
		using (new AssertionScope())
		{
			club.Data.Should().NotBeEmpty().And.HaveCount(2);
			club.Data.First().Username.Should().Be("daya");
			club.Data.Last().Username.Should().Be("Xinil");
		}
	}
}