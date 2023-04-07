using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.MangaTests;

public class GetMangaUserUpdatesAsyncTests
{
	[Test]
	public async Task MonsterId_ShouldParseMonsterUserUpdates()
	{
		// When
		var monster = await JikanTests.Instance.GetMangaUserUpdatesAsync(1);

		// Then
		var firstUpdate = monster.Data.First();
		using (new AssertionScope())
		{
			monster.Data.Should().HaveCount(75);
			firstUpdate.Date.Value.Should().BeBefore(DateTime.Now);
			firstUpdate.ChaptersTotal.Should().Be(162);
		}
	}

	[Test]
	public async Task MonsterIdSecondPage_ShouldParseMonsterUserUpdatesPaged()
	{
		// When
		var monster = await JikanTests.Instance.GetMangaUserUpdatesAsync(1, 2);

		// Then
		var firstUpdate = monster.Data.First();
		using (new AssertionScope())
		{
			monster.Data.Should().HaveCount(75);
			firstUpdate.Date.Value.Should().BeBefore(DateTime.Now);
			firstUpdate.ChaptersTotal.Should().Be(162);
		}
	}
}