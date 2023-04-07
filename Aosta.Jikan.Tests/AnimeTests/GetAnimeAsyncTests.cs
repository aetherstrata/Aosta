using Aosta.Core.Utils.Exceptions;
using Aosta.Jikan.Enums;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.AnimeTests;

[TestFixture]
public class GetAnimeAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long malId)
	{
		var func = JikanTests.Instance.Awaiting(x => x.GetAnimeAsync(malId));

		await func.Should().ThrowExactlyAsync<ParameterValidationException>();
	}

	[Test]
	[TestCase(1)]
	[TestCase(5)]
	[TestCase(6)]
	public async Task CorrectId_ShouldReturnNotNullAnime(long malId)
	{
		var response = await JikanTests.Instance.GetAnimeAsync(malId);

		response.Data.Should().NotBeNull();
	}

	[Test]
	[TestCase(2)]
	[TestCase(3)]
	[TestCase(4)]
	public async Task WrongId_ShouldThrowException(long malId)
	{
		var func = JikanTests.Instance.Awaiting(x => x.GetAnimeAsync(malId));

		await func.Should().ThrowExactlyAsync<JikanRequestException>();
	}

	[Test]
	public async Task GundamId_ShouldParseTitle()
	{
		var gundamAnime = await JikanTests.Instance.GetAnimeAsync(80);

		gundamAnime.Data.Titles.Should().HaveCountGreaterOrEqualTo(1);
		gundamAnime.Data.Titles!.First().Title.Should().Be("Kidou Senshi Gundam");
	}

	[Test]
	public async Task BebopId_ShouldParseTitle()
	{
		var bebopAnime = await JikanTests.Instance.GetAnimeAsync(1);

		bebopAnime.Data.Titles.Should().HaveCountGreaterOrEqualTo(1);
		bebopAnime.Data.Titles.First().Title.Should().Be("Cowboy Bebop");
	}

	[Test]
	public async Task BebopId_ShouldParseImages()
	{
		var bebopAnime = await JikanTests.Instance.GetAnimeAsync(1);

		using (new AssertionScope())
		{
			bebopAnime.Data.Images.JPG.ImageUrl.Should().Be("https://cdn.myanimelist.net/images/anime/4/19644.jpg");
			bebopAnime.Data.Images.JPG.SmallImageUrl.Should().Be("https://cdn.myanimelist.net/images/anime/4/19644t.jpg");
			bebopAnime.Data.Images.JPG.MediumImageUrl.Should().BeNull();
			bebopAnime.Data.Images.JPG.LargeImageUrl.Should().Be("https://cdn.myanimelist.net/images/anime/4/19644l.jpg");
			bebopAnime.Data.Images.JPG.MaximumImageUrl.Should().BeNull();

			bebopAnime.Data.Images.WebP.ImageUrl.Should().Be("https://cdn.myanimelist.net/images/anime/4/19644.webp");
			bebopAnime.Data.Images.WebP.SmallImageUrl.Should().Be("https://cdn.myanimelist.net/images/anime/4/19644t.webp");
			bebopAnime.Data.Images.WebP.MediumImageUrl.Should().BeNull();
			bebopAnime.Data.Images.WebP.LargeImageUrl.Should().Be("https://cdn.myanimelist.net/images/anime/4/19644l.webp");
			bebopAnime.Data.Images.WebP.MaximumImageUrl.Should().BeNull();
		}
	}

	[Test]
	public async Task BebopId_ShouldParseTitles()
	{
		var bebopAnime = await JikanTests.Instance.GetAnimeAsync(1);

		bebopAnime.Data.Titles.Should().HaveCount(3);

		using (new AssertionScope())
		{
			bebopAnime.Data.Titles.First(x => x.Type.Equals("Default")).Title.Should().Be("Cowboy Bebop");
			bebopAnime.Data.Titles.First(x => x.Type.Equals("English")).Title.Should().Be("Cowboy Bebop");
			bebopAnime.Data.Titles.First(x => x.Type.Equals("Japanese")).Title.Should().Be("カウボーイビバップ");
		}
	}

	[Test]
	public async Task BebopId_ShouldParseTrailer()
	{
		var bebopAnime = await JikanTests.Instance.GetAnimeAsync(1);

		using (new AssertionScope())
		{
			bebopAnime.Data.Trailer.Should().NotBeNull();

			bebopAnime.Data.Trailer.YoutubeId.Should().Be("qig4KOK2R2g");
			bebopAnime.Data.Trailer.Url.Should().Be("https://www.youtube.com/watch?v=qig4KOK2R2g");
			bebopAnime.Data.Trailer.EmbedUrl.Should().Be("https://www.youtube.com/embed/qig4KOK2R2g?enablejsapi=1&wmode=opaque&autoplay=1");

			bebopAnime.Data.Trailer.Image.ImageUrl.Should().Be("https://img.youtube.com/vi/qig4KOK2R2g/default.jpg");
			bebopAnime.Data.Trailer.Image.SmallImageUrl.Should().Be("https://img.youtube.com/vi/qig4KOK2R2g/sddefault.jpg");
			bebopAnime.Data.Trailer.Image.MediumImageUrl.Should().Be("https://img.youtube.com/vi/qig4KOK2R2g/mqdefault.jpg");
			bebopAnime.Data.Trailer.Image.LargeImageUrl.Should().Be("https://img.youtube.com/vi/qig4KOK2R2g/hqdefault.jpg");
			bebopAnime.Data.Trailer.Image.MaximumImageUrl.Should().Be("https://img.youtube.com/vi/qig4KOK2R2g/maxresdefault.jpg");
		}
	}

	[Test]
	public async Task CardcaptorId_ShouldParseInformation()
	{
		var cardcaptor = await JikanTests.Instance.GetAnimeAsync(232);

		using (new AssertionScope())
		{
			cardcaptor.Data.Episodes.Should().Be(70);
			cardcaptor.Data.Type.Should().Be("TV");
			cardcaptor.Data.Year.Should().Be(1998);
			cardcaptor.Data.Season.Should().Be(Season.Spring);
			cardcaptor.Data.Duration.Should().Be("25 min per ep");
			cardcaptor.Data.Rating.Should().Be("PG - Children");
			cardcaptor.Data.Source.Should().Be("Manga");
			cardcaptor.Data.Approved.Should().BeTrue();

			cardcaptor.Data.Broadcast.Day.Should().Be("Tuesdays");
			cardcaptor.Data.Broadcast.String.Should().Be("Tuesdays at 18:00 (JST)");
			cardcaptor.Data.Broadcast.Time.Should().Be("18:00");
			cardcaptor.Data.Broadcast.Timezone.Should().Be("Asia/Tokyo");
		}
	}

	[Test]
	public async Task AkiraId_ShouldParseCollections()
	{
		var akiraAnime = await JikanTests.Instance.GetAnimeAsync(47);

		using (new AssertionScope())
		{
			akiraAnime.Data.Approved.Should().BeTrue();

			akiraAnime.Data.Producers.Should().HaveCount(4);
			akiraAnime.Data.Licensors.Should().HaveCount(3);
			akiraAnime.Data.Studios.Should().HaveCount(1);
			akiraAnime.Data.Genres.Should().HaveCount(5);

			akiraAnime.Data.Licensors.First().Name.Should().Be("Funimation");
			akiraAnime.Data.Studios.First().Name.Should().Be("Tokyo Movie Shinsha");
			akiraAnime.Data.Genres.First().Name.Should().Be("Action");
		}
	}
}