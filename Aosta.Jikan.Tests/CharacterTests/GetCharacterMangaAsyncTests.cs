using Aosta.Jikan.Exceptions;

using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.CharacterTests;

public class GetCharacterMangaAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetCharacterMangaAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task SpikeSpiegelId_ShouldParseSpikeSpiegelAnime()
	{
		// When
		var spike = await JikanTests.Instance.GetCharacterMangaAsync(1);

		// Then
		using (new AssertionScope())
		{
			spike.Data.Should().HaveCount(2);
			spike.Data.Should().OnlyContain(x => x.Role.Equals("Main"));
			spike.Data.Should().OnlyContain(
				x => !string.IsNullOrWhiteSpace(x.Manga.Images.JPG.ImageUrl)
				     && !string.IsNullOrWhiteSpace(x.Manga.Images.JPG.SmallImageUrl)
				     && !string.IsNullOrWhiteSpace(x.Manga.Images.JPG.LargeImageUrl)
			);
		}
	}
}