using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.CharacterTests;

public class GetCharacterFullDataAsyncTests
{
    [Test]
    [TestCase(long.MinValue)]
    [TestCase(-1)]
    [TestCase(0)]
    public async Task InvalidId_ShouldThrowValidationException(long malId)
    {
        // When
        var func = JikanTests.Instance.Awaiting(x => x.GetCharacterFullDataAsync(malId));

        // Then
        await func.Should().ThrowExactlyAsync<ParameterValidationException>();
    }
        
    [Test]
    public async Task IchigoKurosakiId_ShouldParseIchigoKurosaki()
    {
        // When
        var ichigo = await JikanTests.Instance.GetCharacterFullDataAsync(5);

        // Then
        using var _ = new AssertionScope();
        ichigo.Data.Name.Should().Be("Ichigo Kurosaki");
        ichigo.Data.NameKanji.Should().Be("黒崎 一護");
        ichigo.Data.Animeography.Should().HaveCount(9);
        ichigo.Data.Animeography.Select(x => x.Anime.Title).Should().Contain("Bleach");
        ichigo.Data.Mangaography.Should().HaveCount(7);
        ichigo.Data.VoiceActors.Should().HaveCount(14);
        ichigo.Data.VoiceActors.Should().Contain(x => x.Person.Name.Equals("Morita, Masakazu"));
    }
}