using Aosta.Jikan.Exceptions;

using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.ClubTests;

public class GetClubRelationsAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetClubRelationsAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task BebopId_ShouldParseCowboyBebopClubRelatedEntities()
	{
		// When
		var club = await JikanTests.Instance.GetClubRelationsAsync(1);

		// Then
		using (new AssertionScope())
		{
			club.Data.Anime.Should().NotBeEmpty().And.HaveCount(3).And.Contain(x => x.Name.Equals("Cowboy Bebop"));
			club.Data.Manga.Should().NotBeEmpty().And.HaveCount(2).And.Contain(x => x.Name.Equals("Shooting Star Bebop: Cowboy Bebop"));
			club.Data.Characters.Should().NotBeEmpty().And.HaveCount(22).And.Contain(x => x.Name.Equals("Black, Jet"));
		}
	}

	[Test]
	public async Task NamineCafeId_ShouldParseNamineCafeClubRelatedEntities()
	{
		// When
		var club = await JikanTests.Instance.GetClubRelationsAsync(39921);

		// Then
		using (new AssertionScope())
		{
			club.Data.Anime.Should().Contain(x => x.Name.Equals("Bakemonogatari"));
			club.Data.Anime.Should().Contain(x => x.Name.Equals("Clannad"));
			club.Data.Manga.Should().Contain(x => x.Name.Equals("Fate/Zero"));
			club.Data.Characters.Should().Contain(x => x.Name.Equals("Naegi, Makoto"));
		}
	}
}