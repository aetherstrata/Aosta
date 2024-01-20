using Aosta.Jikan.Exceptions;
using Aosta.Jikan.Query.Enums;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.AnimeTests;

public class GetAnimeForumTopicsAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long malId)
	{
		var func = JikanTests.Instance.Awaiting(x => x.GetAnimeForumTopicsAsync(malId));

		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	[TestCase((ForumTopicTypeFilter)int.MaxValue)]
	[TestCase((ForumTopicTypeFilter)int.MinValue)]
	public async Task InvalidEnum_ShouldThrowValidationException(ForumTopicTypeFilter type)
	{
		var func = JikanTests.Instance.Awaiting(x => x.GetAnimeForumTopicsAsync(1, type));

		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task BebopId_ShouldParseCowboyBebopTopics()
	{
		var bebop = await JikanTests.Instance.GetAnimeForumTopicsAsync(1);

		bebop.Data.Should().HaveCount(15);
	}

	[Test]
	public async Task BebopIdAndTypeEpisode_ShouldParseCowboyBebopTopicsWithEpisodeDiscussionOnly()
	{
		var bebop = await JikanTests.Instance.GetAnimeForumTopicsAsync(1, ForumTopicTypeFilter.Episode);

		using var _ = new AssertionScope();
		bebop.Data.Should().HaveCount(15);
		bebop.Data.Should().OnlyContain(topic => topic.Title.Contains("Cowboy Bebop Episode"));
	}

	[Test]
	public async Task BebopIdAndTypeOther_ShouldParseCowboyBebopTopicsWithoutEpisodeDiscussion()
	{
		var bebop = await JikanTests.Instance.GetAnimeForumTopicsAsync(1, ForumTopicTypeFilter.Other);

		using var _ = new AssertionScope();
		bebop.Data.Should().HaveCount(15);
		bebop.Data.Should().NotContain(topic => topic.Title.Contains("Cowboy Bebop Episode "));
	}
}