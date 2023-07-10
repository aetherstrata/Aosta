using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.MangaTests;

public class GetMangaFullDataAsyncTests
{
    [Test]
    [TestCase(long.MinValue)]
    [TestCase(-1)]
    [TestCase(0)]
    public async Task InvalidId_ShouldThrowValidationException(long malId)
    {
        // When
        var func = JikanTests.Instance.Awaiting(x => x.GetMangaFullDataAsync(malId));

        // Then
        await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
    }


    [Test]
    public async Task MonsterId_ShouldParseMonster()
    {
        // When
        var monsterManga = await JikanTests.Instance.GetMangaFullDataAsync(1);

        // Then
        using var _ = new AssertionScope();
        monsterManga.Data.Title.Should().Be("Monster");
        monsterManga.Data.ExternalLinks.Should().ContainSingle();
        monsterManga.Data.ExternalLinks.Should().Contain(x => x.Name.Equals("Wikipedia") && x.Url.Equals("http://ja.wikipedia.org/wiki/MONSTER"));
        monsterManga.Data.Relations.Should().HaveCount(2);
        monsterManga.Data.Relations.Should().ContainSingle(x => x.Relation.Equals("Adaptation") && x.Entry.Count == 1);
        monsterManga.Data.Relations.Should().ContainSingle(x => x.Relation.Equals("Side story") && x.Entry.Count == 1);
    }
}