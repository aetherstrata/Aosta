using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.UserTests;

public class GetUserFullAsyncTests
{
    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase("\n\n\t    \t")]
    public async Task InvalidUsername_ShouldThrowValidationException(string username)
    {
        // When
        var func = JikanTests.Instance.Awaiting(x => x.GetUserFullDataAsync(username));

        // Then
        await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
    }

    [Test]
    public async Task Ervelan_ShouldParseErvelanProfile()
    {
        // When
        var user = await JikanTests.Instance.GetUserFullDataAsync("Ervelan");

        // Then
        using var _ = new AssertionScope();
        user.Should().NotBeNull();
        user.Data.Username.Should().Be("Ervelan");
        user.Data.MalId.Should().Be(289183);
        user.Data.Joined.Value.Year.Should().Be(2010);
        user.Data.Birthday.Value.Year.Should().Be(1993);
        user.Data.Gender.Should().Be("Male");
        user.Data.Favorites.Anime.Select(x => x.Title).Should().Contain("Haibane Renmei");
        user.Data.Favorites.Manga.Select(x => x.Title).Should().Contain("Berserk");
        user.Data.Favorites.Characters.Select(x => x.Name).Should().Contain("Oshino, Shinobu");
        user.Data.Favorites.People.Select(x => x.Name).Should().Contain("Araki, Hirohiko");
        user.Data.Updates.AnimeUpdates.Should().NotBeNullOrEmpty();
        user.Data.Updates.AnimeUpdates.First().User.Should().BeNull();
        user.Data.Updates.AnimeUpdates.First().Entry.Should().NotBeNull();
        user.Data.Updates.MangaUpdates.Should().NotBeNullOrEmpty();
        user.Data.Updates.MangaUpdates.First().User.Should().BeNull();
        user.Data.Updates.MangaUpdates.First().Entry.Should().NotBeNull();
    }
}