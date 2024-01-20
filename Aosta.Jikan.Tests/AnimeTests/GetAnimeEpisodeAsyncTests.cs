using Aosta.Jikan.Exceptions;

using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.AnimeTests;

public class GetAnimeEpisodeAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long malId)
	{
		var func = JikanTests.Instance.Awaiting(x => x.GetAnimeEpisodeAsync(malId, 1));

		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	[TestCase(int.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task ValidIdInvalidPage_ShouldThrowValidationException(int page)
	{
		var func = JikanTests.Instance.Awaiting(x => x.GetAnimeEpisodeAsync(1, page));

		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task CardcaptorFirstEpisode_ShouldParseCardcaptorFirstEpisodeTitles()
	{
		var cardcaptorFirstEpisode = await JikanTests.Instance.GetAnimeEpisodeAsync(232, 1);

		using var _ = new AssertionScope();
		cardcaptorFirstEpisode.Data.Title.Should().Be("Sakura and the Strange Magical Book");
		cardcaptorFirstEpisode.Data.TitleRomanji.Should().Be("Sakura to Fushigi na Mahou no Hon");
		cardcaptorFirstEpisode.Data.TitleJapanese.Should().Be("さくらと不思議な魔法の本");
	}

	[Test]
	public async Task CardcaptorFirstEpisode_ShouldParseCardcaptorFirstEpisodeBasicData()
	{
		var cardcaptorFirstEpisode = await JikanTests.Instance.GetAnimeEpisodeAsync(232, 1);

		using var _ = new AssertionScope();
		cardcaptorFirstEpisode.Data.Duration.Should().Be(1500);
		cardcaptorFirstEpisode.Data.Filler.Should().BeFalse();
		cardcaptorFirstEpisode.Data.Recap.Should().BeFalse();
	}

	[Test]
	public async Task CardcaptorFirstEpisode_ShouldParseCardcaptorFirstEpisodeAiredDateTime()
	{
		var cardcaptorFirstEpisode = await JikanTests.Instance.GetAnimeEpisodeAsync(232, 1);

		using var _ = new AssertionScope();
		cardcaptorFirstEpisode.Data.Aired.Should().HaveDay(7).And.HaveMonth(4).And.HaveYear(1998);
		cardcaptorFirstEpisode.Data.Aired.Should().HaveHour(0).And.HaveMinute(0).And.HaveSecond(0);
		cardcaptorFirstEpisode.Data.Aired.Should().HaveOffset(TimeSpan.FromHours(9));
	}

	[Test]
	public async Task CardcaptorTenthEpisode_ShouldParseSynopsis()
	{
		var cardcaptorTenthEpisode = await JikanTests.Instance.GetAnimeEpisodeAsync(232, 10);

		cardcaptorTenthEpisode.Data.Synopsis.Should().StartWith("It's Sports Day at Sakura's school");
	}
}