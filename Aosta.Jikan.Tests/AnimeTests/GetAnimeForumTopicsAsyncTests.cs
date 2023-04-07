using Aosta.Core.Utils.Exceptions;
using Aosta.Jikan.Enums;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.AnimeTests
{
	public class GetAnimeForumTopicsAsyncTests
	{
		[Test]
		[TestCase(long.MinValue)]
		[TestCase(-1)]
		[TestCase(0)]
		public async Task InvalidId_ShouldThrowValidationException(long malId)
		{
			var func = JikanTests.Instance.Awaiting(x => x.GetAnimeForumTopicsAsync(malId));

			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}

		[Test]
		[TestCase((ForumTopicType)int.MaxValue)]
		[TestCase((ForumTopicType)int.MinValue)]
		public async Task InvalidEnum_ShouldThrowValidationException(ForumTopicType type)
		{
			var func = JikanTests.Instance.Awaiting(x => x.GetAnimeForumTopicsAsync(1, type));

			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
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
			var bebop = await JikanTests.Instance.GetAnimeForumTopicsAsync(1, ForumTopicType.Episode);

			using var _ = new AssertionScope();
			bebop.Data.Should().HaveCount(15);
			bebop.Data.Should().OnlyContain(topic => topic.Title.Contains("Cowboy Bebop Episode"));
		}

		[Test]
		public async Task BebopIdAndTypeOther_ShouldParseCowboyBebopTopicsWithoutEpisodeDiscussion()
		{
			var bebop = await JikanTests.Instance.GetAnimeForumTopicsAsync(1, ForumTopicType.Other);

			using var _ = new AssertionScope();
			bebop.Data.Should().HaveCount(15);
			bebop.Data.Should().NotContain(topic => topic.Title.Contains("Cowboy Bebop Episode "));
		}
	}
}