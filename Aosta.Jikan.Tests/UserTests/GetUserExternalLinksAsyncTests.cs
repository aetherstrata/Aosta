using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.UserTests;

public class GetUserExternalLinksAsyncTests
{
    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase("\n\n\t    \t")]
    public async Task InvalidUsername_ShouldThrowValidationException(string username)
    {
        // When
        var func = JikanTests.Instance.Awaiting(x => x.GetUserReviewsAsync(username));

        // Then
        await func.Should().ThrowExactlyAsync<ParameterValidationException>();
    }

    [Test]
    public async Task Ervelan_ShouldParseErvelanUrls()
    {
        // When
        var links = await JikanTests.Instance.GetUserExternalLinksAsync("Ervelan");

        // Then
        links.Data.Should().ContainSingle(x => x.Name.Equals("seiyuu.moe") && x.Url.Equals("https://seiyuu.moe/"));
    }

    [Test]
    public async Task SonMati_ShouldParseSonMatiUrls()
    {
        // When
        var links = await JikanTests.Instance.GetUserExternalLinksAsync("sonmati");

        // Then
        using var _ = new AssertionScope();
        links.Data.Should().HaveCount(2);
        links.Data.Should().ContainSingle(x => x.Name.Equals("vndb.org") && x.Url.Equals("https://vndb.org/u55837"));
        links.Data.Should().ContainSingle(x => x.Name.Equals("steamcommunity.com") && x.Url.Equals("http://steamcommunity.com/profiles/76561198169895549/"));
    }
}