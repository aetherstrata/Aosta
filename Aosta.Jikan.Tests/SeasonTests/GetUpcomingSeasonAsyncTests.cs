using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.SeasonTests
{
	public class GetUpcomingSeasonAsyncTests
	{
		[Test]
		public async Task ShouldParseUpcomingSeason()
		{
			// When
			var upcomingSeason = await JikanTests.Instance.GetUpcomingSeasonAsync();

			// Then
			using var _ = new AssertionScope();
			upcomingSeason.Pagination.HasNextPage.Should().BeTrue();
			upcomingSeason.Pagination.LastVisiblePage.Should().BeGreaterThan(10);
			upcomingSeason.Pagination.CurrentPage.Should().Be(1);
			upcomingSeason.Pagination.Items.Count.Should().Be(25);
			upcomingSeason.Pagination.Items.Total.Should().BeGreaterThan(300);
			upcomingSeason.Data.Select(x => x.Title).Should().Contain("Goblin Slayer 2nd Season");
		}
		
		[Test]
		[TestCase(int.MinValue)]
		[TestCase(-1)]
		[TestCase(0)]
		public async Task InvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = JikanTests.Instance.Awaiting(x => x.GetUpcomingSeasonAsync(page));

			// Then
			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}
		
		[Test]
		public async Task WithTooBigPage_ShouldParseAndReturnNothing()
		{
			// When
			var upcomingSeason = await JikanTests.Instance.GetUpcomingSeasonAsync(100);

			// Then
			upcomingSeason.Data.Should().BeEmpty();
		}
	}
}