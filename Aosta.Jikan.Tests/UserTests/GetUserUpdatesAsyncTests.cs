using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.UserTests;

public class GetUserUpdatesAsyncTests
{
    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase("\n\n\t    \t")]
    public async Task InvalidUsername_ShouldThrowValidationException(string username)
    {
        // When
        var func = JikanTests.Instance.Awaiting(x => x.GetUserUpdatesAsync(username));

        // Then
        await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
    }

    [Test]
    public async Task GetUserProfileAsync_Ervelan_ShouldParseErvelanProfile()
    {
        // When
        var user = await JikanTests.Instance.GetUserUpdatesAsync("Ervelan");

        // Then
        using var _ = new AssertionScope();
        user.Should().NotBeNull();
        user.Data.AnimeUpdates.Should().NotBeNullOrEmpty();
        user.Data.AnimeUpdates.First().User.Should().BeNull();
        user.Data.AnimeUpdates.First().Entry.Should().NotBeNull();
        user.Data.MangaUpdates.Should().NotBeNullOrEmpty();
        user.Data.MangaUpdates.First().User.Should().BeNull();
        user.Data.MangaUpdates.First().Entry.Should().NotBeNull();
    }
}