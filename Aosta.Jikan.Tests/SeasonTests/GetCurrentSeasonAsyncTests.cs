using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.SeasonTests
{
	public class GetCurrentSeasonAsyncTests
	{
		[Test]
		public async Task ShouldParseCurrentSeason()
		{
			// When
			var currentSeason = await JikanTests.Instance.GetCurrentSeasonAsync();

			// Then
			using var _ = new AssertionScope();
			currentSeason.Pagination.HasNextPage.Should().BeTrue();
			currentSeason.Pagination.LastVisiblePage.Should().BeGreaterThan(2);
			currentSeason.Pagination.CurrentPage.Should().Be(1);
			currentSeason.Pagination.Items.Count.Should().Be(25);
			currentSeason.Pagination.Items.Total.Should().BeGreaterThan(30);
			currentSeason.Data.Select(x => x.Title).Should().Contain("Bleach: Sennen Kessen-hen");
		}
		
		[Test]
		[TestCase(int.MinValue)]
		[TestCase(-1)]
		[TestCase(0)]
		public async Task InvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = JikanTests.Instance.Awaiting(x => x.GetCurrentSeasonAsync(page));

			// Then
			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}
		
		[Test]
		public async Task WithTooBigPage_ShouldParseAndReturnNothing()
		{
			// When
			var upcomingSeason = await JikanTests.Instance.GetCurrentSeasonAsync(100);

			// Then
			upcomingSeason.Data.Should().BeEmpty();
		}
		
		[Test]
		public async Task WithCorrectPage_ShouldParseCurrentSeason()
		{
			// When
			var currentSeason = await JikanTests.Instance.GetCurrentSeasonAsync(1);

			// Then
			using var _ = new AssertionScope();
			currentSeason.Pagination.HasNextPage.Should().BeTrue();
			currentSeason.Pagination.LastVisiblePage.Should().BeGreaterThan(2);
			currentSeason.Pagination.CurrentPage.Should().Be(1);
			currentSeason.Pagination.Items.Count.Should().Be(25);
			currentSeason.Pagination.Items.Total.Should().BeGreaterThan(3);
			currentSeason.Data.Select(x => x.Title).Should().Contain("Bleach: Sennen Kessen-hen");
		}
	}
}