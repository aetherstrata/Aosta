using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.MangaTests;

public class GetMangaExternalLinksAsyncTests
{
    [Test]
    [TestCase(long.MinValue)]
    [TestCase(-1)]
    [TestCase(0)]
    public async Task InvalidId_ShouldThrowValidationException(long malId)
    {
        // When
        var func = JikanTests.Instance.Awaiting(x => x.GetMangaExternalLinksAsync(malId));

        // Then
        await func.Should().ThrowExactlyAsync<ParameterValidationException>();
    }
    
    [Test]
    public async Task MonsterId_ShouldReturnMonsterLinks()
    {
        // When
        var links = await JikanTests.Instance.GetMangaExternalLinksAsync(1);

        // Then
        using var _ = new AssertionScope();
        links.Data.Should().ContainSingle();
        links.Data.Should().Contain(x => x.Name.Equals("Wikipedia") && x.Url.Equals("http://ja.wikipedia.org/wiki/MONSTER"));
    }
}