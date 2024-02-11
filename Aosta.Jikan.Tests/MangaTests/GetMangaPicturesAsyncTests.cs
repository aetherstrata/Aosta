using Aosta.Jikan.Exceptions;

namespace Aosta.Jikan.Tests.MangaTests;

public class GetMangaPicturesAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long id)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetMangaPicturesAsync(id));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task MonsterId_ShouldParseMonsterImages()
	{
		// When
		var monster = await JikanTests.Instance.GetMangaPicturesAsync(1);

		// Then
		monster.Data.Should().HaveCount(6);
	}
}
