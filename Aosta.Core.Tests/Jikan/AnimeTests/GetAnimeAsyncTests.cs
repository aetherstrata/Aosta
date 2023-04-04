using Aosta.Core.Exceptions;
using Aosta.Core.Jikan.Enums;
using Aosta.Core.Jikan.Exceptions;

namespace Aosta.Core.Tests.Jikan.AnimeTests;

[TestFixture]
public class GetAnimeAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public void InvalidId_ShouldThrowValidationException(long malId)
	{
		Assert.ThrowsAsync<AostaValidationException>(async () => await JikanSetup.Instance.GetAnimeAsync(malId));
	}

	[Test]
	[TestCase(1)]
	[TestCase(5)]
	[TestCase(6)]
	public async Task CorrectId_ShouldReturnNotNullAnime(long malId)
	{
		var response = await JikanSetup.Instance.GetAnimeAsync(malId);

		Assert.That(response.Data, Is.Not.Null);
	}

	[Test]
	[TestCase(2)]
	[TestCase(3)]
	[TestCase(4)]
	public void WrongId_ShouldThrowException(long malId)
	{
		Assert.ThrowsAsync<JikanRequestException>(async () => await JikanSetup.Instance.GetAnimeAsync(malId));
	}

	[Test]
	public async Task GundamId_ShouldParseTitle()
	{
		var gundamAnime = await JikanSetup.Instance.GetAnimeAsync(80);

		Assert.That(gundamAnime.Data.Titles, Has.Count.GreaterThanOrEqualTo(1));
		Assert.That(gundamAnime.Data.Titles!.First().Title, Is.EqualTo("Kidou Senshi Gundam"));
	}

	[Test]
	public async Task BebopId_ShouldParseTitle()
	{
		var bebopAnime = await JikanSetup.Instance.GetAnimeAsync(1);

		Assert.That(bebopAnime.Data.Titles, Has.Count.GreaterThanOrEqualTo(1));
		Assert.That(bebopAnime.Data.Titles!.First().Title, Is.EqualTo("Cowboy Bebop"));
	}

	[Test]
	public async Task BebopId_ShouldParseImages()
	{
		var bebopAnime = await JikanSetup.Instance.GetAnimeAsync(1);

		Assert.Multiple(() =>
		{
			Assert.That(bebopAnime.Data.Images!.JPG.ImageUrl, Is.EqualTo("https://cdn.myanimelist.net/images/anime/1130/134970.jpg"));
			Assert.That(bebopAnime.Data.Images.JPG.SmallImageUrl, Is.EqualTo("https://cdn.myanimelist.net/images/anime/1130/134970t.jpg"));
			Assert.That(bebopAnime.Data.Images.JPG.MediumImageUrl, Is.Null);
			Assert.That(bebopAnime.Data.Images.JPG.LargeImageUrl, Is.EqualTo("https://cdn.myanimelist.net/images/anime/1130/134970l.jpg"));
			Assert.That(bebopAnime.Data.Images.JPG.MaximumImageUrl, Is.Null);

			Assert.That(bebopAnime.Data.Images.WebP.ImageUrl, Is.EqualTo("https://cdn.myanimelist.net/images/anime/1130/134970.webp"));
			Assert.That(bebopAnime.Data.Images.WebP.SmallImageUrl, Is.EqualTo("https://cdn.myanimelist.net/images/anime/1130/134970t.webp"));
			Assert.That(bebopAnime.Data.Images.WebP.MediumImageUrl, Is.Null);
			Assert.That(bebopAnime.Data.Images.WebP.LargeImageUrl, Is.EqualTo("https://cdn.myanimelist.net/images/anime/1130/134970l.webp"));
			Assert.That(bebopAnime.Data.Images.WebP.MaximumImageUrl, Is.Null);
		});
	}

	[Test]
	public async Task BebopId_ShouldParseCowboyBebopTitles()
	{
		var bebopAnime = await JikanSetup.Instance.GetAnimeAsync(1);

		Assert.That(bebopAnime.Data.Titles, Has.Count.EqualTo(3));

		var defaultTitle = bebopAnime.Data.Titles!.Where(x => x.Type.Equals("Default")).ToList();
		var englishTitle = bebopAnime.Data.Titles!.Where(x => x.Type.Equals("English")).ToList();
		var japaneseTitle = bebopAnime.Data.Titles!.Where(x => x.Type.Equals("Japanese")).ToList();

		Assert.Multiple(() =>
		{
			Assert.That(defaultTitle, Has.Count.EqualTo(1));
			Assert.That(defaultTitle.First().Title, Is.EqualTo("Cowboy Bebop"));

			Assert.That(englishTitle, Has.Count.EqualTo(1));
			Assert.That(englishTitle.First().Title, Is.EqualTo("Cowboy Bebop"));

			Assert.That(japaneseTitle, Has.Count.EqualTo(1));
			Assert.That(japaneseTitle.First().Title, Is.EqualTo("カウボーイビバップ"));
		});
	}

	[Test]
	public async Task BebopId_ShouldParseCowboyBebopTrailer()
	{
		var bebopAnime = await JikanSetup.Instance.GetAnimeAsync(1);

		Assert.Multiple(() =>
		{
			Assert.That(bebopAnime.Data.Trailer, Is.Not.Null);

			Assert.That(bebopAnime.Data.Trailer!.YoutubeId, Is.EqualTo("qig4KOK2R2g"));
			Assert.That(bebopAnime.Data.Trailer.Url, Is.EqualTo("https://www.youtube.com/watch?v=qig4KOK2R2g"));
			Assert.That(bebopAnime.Data.Trailer.EmbedUrl, Is.EqualTo("https://www.youtube.com/embed/qig4KOK2R2g?enablejsapi=1&wmode=opaque&autoplay=1"));

			Assert.That(bebopAnime.Data.Trailer.Image!.ImageUrl, Is.EqualTo("https://img.youtube.com/vi/qig4KOK2R2g/default.jpg"));
			Assert.That(bebopAnime.Data.Trailer.Image.SmallImageUrl, Is.EqualTo("https://img.youtube.com/vi/qig4KOK2R2g/sddefault.jpg"));
			Assert.That(bebopAnime.Data.Trailer.Image.MediumImageUrl, Is.EqualTo("https://img.youtube.com/vi/qig4KOK2R2g/mqdefault.jpg"));
			Assert.That(bebopAnime.Data.Trailer.Image.LargeImageUrl, Is.EqualTo("https://img.youtube.com/vi/qig4KOK2R2g/hqdefault.jpg"));
			Assert.That(bebopAnime.Data.Trailer.Image.MaximumImageUrl, Is.EqualTo("https://img.youtube.com/vi/qig4KOK2R2g/maxresdefault.jpg"));
		});
	}

	[Test]
	public async Task CardcaptorId_ShouldParseInformation()
	{
		// When
		var cardcaptor = await JikanSetup.Instance.GetAnimeAsync(232);

		// Then
		Assert.Multiple(() =>
		{
			Assert.That(cardcaptor.Data.Episodes, Is.EqualTo(70));
			Assert.That(cardcaptor.Data.Type, Is.EqualTo("TV"));
			Assert.That(cardcaptor.Data.Year, Is.EqualTo(1998));
			Assert.That(cardcaptor.Data.Season, Is.EqualTo(Season.Spring));
			Assert.That(cardcaptor.Data.Duration, Is.EqualTo("25 min per ep"));
			Assert.That(cardcaptor.Data.Rating, Is.EqualTo("PG - Children"));
			Assert.That(cardcaptor.Data.Source, Is.EqualTo("Manga"));
			Assert.That(cardcaptor.Data.Approved, Is.True);
			
			Assert.That(cardcaptor.Data.Broadcast!.Day, Is.EqualTo("Tuesdays"));
			Assert.That(cardcaptor.Data.Broadcast.String, Is.EqualTo("Tuesdays at 18:00 (JST)"));
			Assert.That(cardcaptor.Data.Broadcast.Time, Is.EqualTo("18:00"));
			Assert.That(cardcaptor.Data.Broadcast.Timezone, Is.EqualTo("Asia/Tokyo"));
		});
	}

	[Test]
	public async Task AkiraId_ShouldParseCollections()
	{
		var akiraAnime = await JikanSetup.Instance.GetAnimeAsync(47);

		Assert.Multiple(() =>
		{
			Assert.That(akiraAnime.Data.Approved, Is.True);

			Assert.That(akiraAnime.Data.Producers,Has.Count.EqualTo(4));
			Assert.That(akiraAnime.Data.Licensors, Has.Count.EqualTo(3));
			Assert.That(akiraAnime.Data.Studios, Has.Count.EqualTo(1));
			Assert.That(akiraAnime.Data.Genres, Has.Count.EqualTo(5));

			Assert.That(akiraAnime.Data.Licensors!.First().Name, Is.EqualTo("Funimation"));
			Assert.That(akiraAnime.Data.Studios!.First().Name, Is.EqualTo("Tokyo Movie Shinsha"));
			Assert.That(akiraAnime.Data.Genres!.First().Name, Is.EqualTo("Action"));
		});
	}
}