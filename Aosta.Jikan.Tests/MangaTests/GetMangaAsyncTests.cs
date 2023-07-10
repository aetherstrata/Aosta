using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.MangaTests;

public class GetMangaAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long id)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetMangaAsync(id));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	[TestCase(1)]
	[TestCase(2)]
	[TestCase(3)]
	public async Task CorrectId_ShouldReturnNotNullManga(long malId)
	{
		// When
		var returedManga = await JikanTests.Instance.GetMangaAsync(malId);

		// Then
		returedManga.Should().NotBeNull();
	}

	[Test]
	[TestCase(-1)]
	[TestCase(5)]
	[TestCase(6)]
	public void WrongId_ShouldReturnNullManga(long malId)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetMangaAsync(malId));

		// Then
		func.Should().ThrowExactlyAsync<JikanRequestException>();
	}

	[Test]
	public async Task BerserkId_ShouldParseBerserk()
	{
		// When
		var berserkManga = await JikanTests.Instance.GetMangaAsync(2);

		// Then
		berserkManga.Data.Title.Should().Be("Berserk");
	}

	[Test]
	public async Task MonsterId_ShouldParseMonster()
	{
		// When
		var monsterManga = await JikanTests.Instance.GetMangaAsync(1);

		// Then
		monsterManga.Data.Title.Should().Be("Monster");
	}

	[Test]
	public async Task YotsubatoId_ShouldParseYotsubatoInformation()
	{
		// When
		var yotsubatoManga = await JikanTests.Instance.GetMangaAsync(104);

		// Then
		using (new AssertionScope())
		{
			yotsubatoManga.Data.Status.Should().Be("Publishing");
			yotsubatoManga.Data.Published.From.Value.Year.Should().Be(2003);
			yotsubatoManga.Data.Chapters.Should().BeNull();
			yotsubatoManga.Data.Volumes.Should().BeNull();
			yotsubatoManga.Data.Type.Should().Be("Manga");
			yotsubatoManga.Data.Approved.Should().BeTrue();
		}
	}
		
		
	[Test]
	public async Task YotsubatoId_ShouldParseYotsubatoTitles()
	{
		// When
		var yotsubatoManga = await JikanTests.Instance.GetMangaAsync(104);

		// Then
		using var _ = new AssertionScope();
		yotsubatoManga.Data.Titles.Should().HaveCountGreaterOrEqualTo(8);
		yotsubatoManga.Data.Titles.Should().ContainSingle(x => x.Type.Equals("Default") && x.Title.Equals("Yotsuba to!"));
		yotsubatoManga.Data.Titles.Should().Contain(x => x.Type.Equals("Synonym") && x.Title.Equals("Yotsuba and!"));
		yotsubatoManga.Data.Titles.Should().ContainSingle(x => x.Type.Equals("Japanese") && x.Title.Equals("よつばと!"));
		yotsubatoManga.Data.Titles.Should().ContainSingle(x => x.Type.Equals("English") && x.Title.Equals("Yotsuba&!"));
		yotsubatoManga.Data.Titles.Should().ContainSingle(x => x.Type.Equals("German") && x.Title.Equals("Yotsuba&!"));
		yotsubatoManga.Data.Titles.Should().ContainSingle(x => x.Type.Equals("Spanish") && x.Title.Equals("¡Yotsuba!"));
	}

	[Test]
	public async Task OnePieceId_ShouldParseOnePieceCollections()
	{
		// When
		var onePieceManga = await JikanTests.Instance.GetMangaAsync(13);

		// Then
		using (new AssertionScope())
		{
			onePieceManga.Data.Authors.Should().ContainSingle();
			onePieceManga.Data.Serializations.Should().ContainSingle();
			onePieceManga.Data.Genres.Should().HaveCount(3);
			onePieceManga.Data.Authors.First().Name.Should().Be("Oda, Eiichiro");
			onePieceManga.Data.Serializations.First().Name.Should().Be("Shounen Jump (Weekly)");
			onePieceManga.Data.Genres.First().Name.Should().Be("Action");
			onePieceManga.Data.Demographics.First().Name.Should().Be("Shounen");
			onePieceManga.Data.Themes.Should().BeEmpty();
		}
	}
}