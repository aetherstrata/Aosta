using Aosta.Jikan.Enums;
using Aosta.Jikan.Query.Enums;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.ScheduleTests;

public class GetScheduleAsyncTests
{
	[Test]
	public async Task AllSchedule_ShouldParseCurrentSchedule()
	{
		// When
		var currentSeason = await JikanTests.Instance.GetScheduleAsync();

		// Then
		using var _ = new AssertionScope();
		currentSeason.Data.Should().NotBeEmpty();
		currentSeason.Pagination.HasNextPage.Should().BeTrue();
		currentSeason.Pagination.LastVisiblePage.Should().BeGreaterOrEqualTo(3);
		currentSeason.Pagination.CurrentPage.Should().Be(1);
		currentSeason.Pagination.Items.Count.Should().Be(25);
		currentSeason.Pagination.Items.PerPage.Should().Be(25);
		currentSeason.Pagination.Items.Total.Should().BeGreaterThan(100);
	}

	[Test]
	public async Task AllScheduleWithPage_ShouldParseCurrentSchedule()
	{
		// When
		var currentSeason = await JikanTests.Instance.GetScheduleAsync(3);

		// Then
		using var _ = new AssertionScope();
		currentSeason.Pagination.CurrentPage.Should().Be(3);
		currentSeason.Pagination.HasNextPage.Should().BeTrue();
		currentSeason.Pagination.Items.Count.Should().Be(25);
	}

	[Test]
	[TestCase(int.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task WithInvalidPage_ShouldThrowValidationException(int page)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetScheduleAsync(page));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task AllScheduleWithTooBigPage_ShouldParseAndReturnEmpty()
	{
		// When
		var currentSeason = await JikanTests.Instance.GetScheduleAsync(100);

		// Then
		currentSeason.Data.Should().BeEmpty();
	}

	[Test]
	public async Task Monday_ShouldParseMondaySchedule()
	{
		// When
		var currentSeason = await JikanTests.Instance.GetScheduleAsync(ScheduledDayFilter.Monday);

		// Then
		var mondayScheduleTitles = currentSeason.Data.Select(x => x.Titles.First(x => x.Type.Equals("Default")).Title);
		using (new AssertionScope())
		{
			currentSeason.Pagination.HasNextPage.Should().BeFalse();
			currentSeason.Pagination.LastVisiblePage.Should().Be(1);
			mondayScheduleTitles.Should().Contain("Golden Kamuy 4th Season");
		}
	}

	[Test]
	public async Task Friday_ShouldParseFridaySchedule()
	{
		// When
		var currentSeason = await JikanTests.Instance.GetScheduleAsync(ScheduledDayFilter.Friday);

		// Then
		var fridayScheduleTitles = currentSeason.Data.Select(x => x.Titles.First(x => x.Type.Equals("Default")).Title);
		using (new AssertionScope())
		{
			fridayScheduleTitles.Should().Contain("Doraemon (2005)");
			fridayScheduleTitles.Should().Contain("Pokemon (2019)");
		}
	}

	[Test]
	[TestCase((ScheduledDayFilter)int.MaxValue)]
	[TestCase((ScheduledDayFilter)int.MinValue)]
	public async Task InvalidScheduledDay_ShouldThrowValidationException(ScheduledDayFilter schedule)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetScheduleAsync(schedule));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}
}