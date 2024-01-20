using Aosta.Jikan.Exceptions;

using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.AnimeTests;

public class GetAnimeVideosAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long malId)
	{
		var func = JikanTests.Instance.Awaiting(x => x.GetAnimeVideosAsync(malId));

		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task BebopId_ShouldParseCowboyBebopVideos()
	{
		var bebop = await JikanTests.Instance.GetAnimeVideosAsync(1);

		using var _ = new AssertionScope();
		bebop.Data.PromoVideos.Should().HaveCount(3);
		bebop.Data.PromoVideos.Select(x => x.Title).Should().Contain("PV 2");
		bebop.Data.EpisodeVideos.Should().HaveCount(26);
		bebop.Data.EpisodeVideos.Select(x => x.Title).Should().Contain("Pierrot Le Fou");
		bebop.Data.MusicVideos.Should().BeEmpty();
	}

	[Test]
	public async Task NarutoId_ShouldParseNarutoMusicVideos()
	{
		var naruto = await JikanTests.Instance.GetAnimeVideosAsync(20);

		using var _  = new AssertionScope();
		naruto.Data.MusicVideos.Should().HaveCountGreaterOrEqualTo(5);
		naruto.Data.MusicVideos.Select(x => x.Title).Should().Contain("OP 2 (Artist ver.)");
		naruto.Data.MusicVideos.Select(x => x.Metadata.Author).Should().Contain("Analogfish");
	}

	[Test]
	public async Task OnePieceId_ShouldParseOnePieceMusicVideos()
	{
		var op = await JikanTests.Instance.GetAnimeVideosAsync(21);

		op.Data.MusicVideos.Should().HaveCountGreaterOrEqualTo(20);
	}
}