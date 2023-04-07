using Aosta.Core.Utils.Exceptions;

namespace Aosta.Jikan.Tests.MangaTests;

public class GetMangaCharactersAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long id)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetMangaCharactersAsync(id));

		// Then
		await func.Should().ThrowExactlyAsync<ParameterValidationException>();
	}

	[Test]
	public async Task MonsterId_ShouldParseMonsterCharacters()
	{
		// When
		var monster = await JikanTests.Instance.GetMangaCharactersAsync(1);

		// Then
		monster.Data.Should().HaveCount(34);
	}

	[Test]
	public async Task MonsterId_ShouldParseMonsterCharactersJohan()
	{
		// When
		var monster = await JikanTests.Instance.GetMangaCharactersAsync(1);

		// Then
		monster.Data.Select(x => x.Character.Name).Should().Contain("Liebert, Johan");
	}
}