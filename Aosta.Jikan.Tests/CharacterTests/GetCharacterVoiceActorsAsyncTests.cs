using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.CharacterTests;

public class GetCharacterVoiceActorsAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetCharacterVoiceActorsAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task SpikeSpiegelId_ShouldParseSpikeSpiegelVoiceActors()
	{
		// When
		var spike = await JikanTests.Instance.GetCharacterVoiceActorsAsync(1);

		// Then
		using var _ = new AssertionScope();
		spike.Data.Should().HaveCount(13);
		spike.Data.Should().Contain(x => x.Language.Equals("Japanese") && x.Person.Name.Equals("Yamadera, Kouichi"));
		spike.Data.Should().Contain(x => x.Language.Equals("English") && x.Person.Name.Equals("Blum, Steven"));
		spike.Data.Should().Contain(x => x.Language.Equals("German") && x.Person.Name.Equals("Neumann, Viktor"));
	}

	[Test]
	public async Task FayeValentinelId_ShouldParseFayeValentineVoiceActors()
	{
		// When
		var faye = await JikanTests.Instance.GetCharacterVoiceActorsAsync(2);

		// Then
		using (new AssertionScope())
		{
			faye.Data.Should().HaveCount(12);
			faye.Data.Should().Contain(x => x.Language.Equals("Japanese") && x.Person.Name.Equals("Hayashibara, Megumi"));
			faye.Data.Should().Contain(x => x.Language.Equals("English") && x.Person.Name.Equals("Lee, Wendee"));
		}
	}
}