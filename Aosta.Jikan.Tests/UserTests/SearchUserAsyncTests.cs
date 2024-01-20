using Aosta.Jikan.Exceptions;
using Aosta.Jikan.Query.Enums;
using Aosta.Jikan.Query.Parameters;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.UserTests;

public class SearchUserAsyncTests
{
    [Test]
    public async Task EmptyConfig_ShouldReturnFirst25People()
    {
        // Given
        var config = new UserSearchQueryParameters();

        // When
        var users = await JikanTests.Instance.SearchUserAsync(config);

        // Then
        using var _ = new AssertionScope();
        users.Data.Should().HaveCount(20);
        users.Data.First().Username.Should().Be("ErickGabriel555");
    }

    [Test]
    [TestCase(int.MinValue)]
    [TestCase(-1)]
    [TestCase(0)]
    public async Task InvalidPage_ShouldThrowValidationException(int page)
    {
        // Given
        var config = new UserSearchQueryParameters().SetPage(page);

        // When
        var func = JikanTests.Instance.Awaiting(x => x.SearchUserAsync(config));

        // Then
        await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
    }

    [Test]
    public async Task GivenSecondPage_ShouldReturnSecondPage()
    {
        // Given
        var config = new UserSearchQueryParameters().SetPage(2);

        // When
        var users = await JikanTests.Instance.SearchUserAsync(config);

        // Then
        using var _ = new AssertionScope();
        users.Data.Should().HaveCount(20);
        users.Data.First().Username.Should().Be("EnricoA128");
        users.Data.First().Images.Should().NotBeNull();
    }

    [Test]
    [TestCase((UserGenderFilter)int.MinValue)]
    [TestCase((UserGenderFilter)int.MaxValue)]
    public async Task InvalidGenderEnumValue_ShouldThrowValidationException(UserGenderFilter gender)
    {
        // Given
        var config =new UserSearchQueryParameters().SetGender(gender);

        // When
        var func = JikanTests.Instance.Awaiting(x => x.SearchUserAsync(config));

        // Then
        await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
    }

    [Test]
    [TestCase(UserGenderFilter.Any, "ErickGabriel555")]
    [TestCase(UserGenderFilter.Male, "ErickGabriel555")]
    [TestCase(UserGenderFilter.Female, "ErickGabriel555")]
    [TestCase(UserGenderFilter.NonBinary, "ErickGabriel555")]
    public async Task ValidGenderEnumValue_ShouldReturnUsers(UserGenderFilter gender, string expectedFirstUser)
    {
        // Given
        var config =new UserSearchQueryParameters().SetGender(gender);

        // When
        var users = await JikanTests.Instance.SearchUserAsync(config);

        // Then
        using var _ = new AssertionScope();
        users.Data.Should().HaveCount(20);
        users.Data.First().Username.Should().Be(expectedFirstUser);
    }

    [Test]
    public async Task SonMatiQuery_ShouldReturnSonMati()
    {
        // Given
        var config = new UserSearchQueryParameters().SetQuery("SonMati");

        // When
        var users = await JikanTests.Instance.SearchUserAsync(config);

        // Then
        users.Data.Should().Contain(x => x.Username.Equals("SonMati") && x.Url.Equals("https://myanimelist.net/profile/SonMati"));
    }

    [Test]
    public async Task WithLocation_ShouldReturnFilteredByLocation()
    {
        // Given
        var config = new UserSearchQueryParameters().SetLocation("mysłowice");

        // When
        var users = await JikanTests.Instance.SearchUserAsync(config);

        // Then
        using var _ = new AssertionScope();
        users.Data.Should().HaveCount(20);
        users.Data.First().Username.Should().Be("P3gueiA1ds");
    }

    [Test]
    public async Task WithMinAge_ShouldReturnFilteredByMinAge()
    {
        // Given
        var config = new UserSearchQueryParameters().SetMinimumAge(20);

        // When
        var users = await JikanTests.Instance.SearchUserAsync(config);

        // Then
        using var _ = new AssertionScope();
        users.Data.Should().HaveCount(20);
        users.Data.First().Username.Should().Be("DeafExorcist");
    }

    [Test]
    public async Task WithMaxAge_ShouldReturnFilteredByMaxAge()
    {
        // Given
        var config =  new UserSearchQueryParameters().SetMaximumAge(20);

        // When
        var users = await JikanTests.Instance.SearchUserAsync(config);

        // Then
        using var _ = new AssertionScope();
        users.Data.Should().HaveCount(20);
        users.Data.First().Username.Should().Be("KarlaD05");
    }
}
