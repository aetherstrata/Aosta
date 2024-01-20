using Aosta.Jikan.Exceptions;

using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.CharacterTests;

public class GetCharacterAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetCharacterAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	[TestCase(1)]
	[TestCase(2)]
	[TestCase(3)]
	public async Task CorrectId_ShouldReturnNotNullCharacter(long malId)
	{
		// When
		var returnedCharacter = await JikanTests.Instance.GetCharacterAsync(malId);

		// Then
		returnedCharacter.Should().NotBeNull();
	}

	[Test]
	[TestCase(8)]
	[TestCase(9)]
	[TestCase(10)]
	public void WrongId_ShouldReturnNullCharacter(long malId)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetCharacterAsync(malId));

		// Then
		func.Should().ThrowExactlyAsync<JikanRequestException>();
	}

	[Test]
	public async Task IchigoKurosakiId_ShouldParseIchigoKurosaki()
	{
		// When
		var ichigo = await JikanTests.Instance.GetCharacterAsync(5);

		// Then
		using var _ = new AssertionScope();
		ichigo.Data.Name.Should().Be("Ichigo Kurosaki");
		ichigo.Data.NameKanji.Should().Be("黒崎 一護");
	}

	[Test]
	public async Task IchigoKurosakiId_ShouldParseIchigoKurosakiAboutNotNull()
	{
		// When
		var ichigo = await JikanTests.Instance.GetCharacterAsync(5);

		// Then
		ichigo.Data.About.Should().NotBeNullOrEmpty();
	}

	[Test]
	public async Task GetBlackId_ShouldParseJetBlackNicknames()
	{
		// When
		var jetBlack = await JikanTests.Instance.GetCharacterAsync(3);

		// Then
		jetBlack.Data.Nicknames.Should().HaveCount(2);
	}
}