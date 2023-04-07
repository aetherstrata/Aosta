using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.AnimeTests
{
	public class GetAnimeEpisodesAsyncTests
	{
		[Test]
		[TestCase(long.MinValue)]
		[TestCase(-1)]
		[TestCase(0)]
		public async Task InvalidId_ShouldThrowValidationException(long malId)
		{
			var func = JikanTests.Instance.Awaiting(x => x.GetAnimeEpisodesAsync(malId));

			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}

		[Test]
		public async Task BebopId_ShouldParseCowboyBebopEpisodeWithAiredDate()
		{
			var expectedDate = new DateTime(1998, 10, 24);
			var bebop = await JikanTests.Instance.GetAnimeEpisodesAsync(1);

			using (new AssertionScope())
			{
				bebop.Data.Should().HaveCount(26);
				bebop.Data.First().Aired.Should().BeSameDateAs(expectedDate);
			}
		}

		[Test]
		public async Task BebopId_ShouldParseCowboyBebopFirstEpisodeTitles()
		{
			var bebop = await JikanTests.Instance.GetAnimeEpisodesAsync(1);
			var firstEpisodeTitle = bebop.Data.First();

			using (new AssertionScope())
			{
				firstEpisodeTitle.Title.Should().Be("Asteroid Blues");
				firstEpisodeTitle.TitleRomanji.Should().StartWith("Asteroid Blues");
				firstEpisodeTitle.TitleJapanese.Should().Be("アステロイド・ブルース");
			}
		}

		[Test]
		public async Task BebopId_ShouldParseCowboyBebopLastEpisodeTitles()
		{
			var bebop = await JikanTests.Instance.GetAnimeEpisodesAsync(1);
			var firstEpisodeTitle = bebop.Data.Last();

			using (new AssertionScope())
			{
				firstEpisodeTitle.Title.Should().Be("The Real Folk Blues (Part 2)");
				firstEpisodeTitle.TitleRomanji.Should().StartWith("The Real Folk Blues (Kouhen)");
				firstEpisodeTitle.TitleJapanese.Should().Be("ザ・リアル・フォークブルース（後編）");
			}
		}

		[Test]
		public async Task CardcaptorId_ShouldParseCardcaptorSakuraFirstEpisodeForumTopic()
		{
			var cardcaptor = await JikanTests.Instance.GetAnimeEpisodesAsync(232);

			var firstEpisode = cardcaptor.Data.First();

			using (new AssertionScope())
			{
				firstEpisode.Url.Should().Be("https://myanimelist.net/anime/232/Cardcaptor_Sakura/episode/1");
				firstEpisode.MalId.Should().Be(1);
			}
		}
	}
}