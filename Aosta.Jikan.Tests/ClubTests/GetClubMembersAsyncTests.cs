using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.ClubTests;

public class GetClubMembersAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetClubMembersAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task BebopId_ShouldParseCowboyBebopClubMemberList()
	{
		// When
		var club = await JikanTests.Instance.GetClubMembersAsync(1);

		// Then
		using (new AssertionScope())
		{
			club.Data.Should().NotBeEmpty();
			club.Data.First().Username.Should().Be("--Pascal--");
			club.Pagination.HasNextPage.Should().BeFalse();
			club.Pagination.LastVisiblePage.Should().Be(1);
		}
	}

	[Test]
	[TestCase(int.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidPage_ShouldThrowValidationException(int page)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetClubMembersAsync(1, page));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task BebopIdSecondPage_ShouldParseCowboyBebopClubMemberListPaged()
	{
		// When
		var members = await JikanTests.Instance.GetClubMembersAsync(1, 2);

		// Then
		using (new AssertionScope())
		{
			members.Data.Should().NotBeEmpty();
			members.Data.Should().HaveCount(36);
		}
	}
}