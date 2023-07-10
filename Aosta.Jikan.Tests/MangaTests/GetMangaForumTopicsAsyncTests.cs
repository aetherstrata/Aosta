using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.MangaTests;

public class GetMangaForumTopicsAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetMangaForumTopicsAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task MonsterId_ShouldParseMonsterTopics()
	{
		// When
		var monster = await JikanTests.Instance.GetMangaForumTopicsAsync(1);

		// Then
		using var _ = new AssertionScope();
		monster.Data.Should().Contain(x => x.Title.StartsWith("Monster Chapter"));
		monster.Data.Should().HaveCount(15);
	}

	[Test]
	public async Task GetMangaForumTopics_MonsterId_ShouldParseMonsterTopicsIds()
	{
		// When
		var monster = await JikanTests.Instance.GetMangaForumTopicsAsync(1);

		// Then
		var topics = monster.Data.Select(x => x.MalId);
		using (new AssertionScope())
		{
			topics.Should().Contain(396141L);
			topics.Should().Contain(396155L);
		}
	}
}