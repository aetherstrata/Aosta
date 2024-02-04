using Aosta.Jikan.Exceptions;
using Aosta.Jikan.Query;
using Aosta.Jikan.Query.Parameters;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.ClubTests;

public class SearchClubAsyncTests
{
    [Test]
    public async Task EmptyConfig_ShouldReturnFirst25People()
    {
        // Given
        var config = ClubSearchQueryParameters.Create();

        // When
        var clubs = await JikanTests.Instance.SearchClubAsync(config);

        // Then
        using var _ = new AssertionScope();
        clubs.Data.Should().HaveCount(JikanParameterConsts.MAXIMUM_PAGE_SIZE);
        clubs.Data.First().Name.Should().Be("Cowboy Bebop");
    }

    [Test]
    [TestCase(int.MinValue)]
    [TestCase(-1)]
    [TestCase(0)]
    public async Task InvalidPage_ShouldThrowValidationException(int page)
    {
        // Given
        var config = ClubSearchQueryParameters.Create().Page(page);

        // When
        var func = JikanTests.Instance.Awaiting(x => x.SearchClubAsync(config));

        // Then
        await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
    }

    [Test]
    [TestCase(int.MinValue)]
    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(26)]
    [TestCase(int.MaxValue)]
    public async Task InvalidPageSize_ShouldThrowValidationException(int pageSize)
    {
        // Given
        var config = ClubSearchQueryParameters.Create().Limit(pageSize);

        // When
        var func = JikanTests.Instance.Awaiting(x => x.SearchClubAsync(config));

        // Then
        await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
    }

    [Test]
    public async Task GivenSecondPage_ShouldReturnSecondPage()
    {
        // Given
        var config = ClubSearchQueryParameters.Create().Page(2);

        // When
        var characters = await JikanTests.Instance.SearchClubAsync(config);

        // Then
        characters.Data.Should().HaveCount(JikanParameterConsts.MAXIMUM_PAGE_SIZE);
    }

    [Test]
    public async Task GivenValidPageSize_ShouldReturnPageSizeNumberOfRecords()
    {
        // Given
        const int pageSize = 5;
        var config = ClubSearchQueryParameters.Create().Limit(pageSize);

        // When
        var characters = await JikanTests.Instance.SearchClubAsync(config);

        // Then
        using var _ = new AssertionScope();
        characters.Data.Should().HaveCount(pageSize);
        characters.Data.Skip(1).First().Name.Should().Be("Cowboy Bebop");
    }

    [Test]
    public async Task GivenValidPageAndPageSize_ShouldReturnPageSizeNumberOfRecordsFromNextPage()
    {
        // Given
        const int pageSize = 5;
        var config =ClubSearchQueryParameters.Create().Page(2).Limit(pageSize);

        // When
        var characters = await JikanTests.Instance.SearchClubAsync(config);

        // Then
        using var _ = new AssertionScope();
        characters.Data.Should().HaveCount(pageSize);
        characters.Data.First().Name.Should().Be("Anime Cafe");
    }

    [Test]
    [TestCase('1')]
    [TestCase('0')]
    [TestCase('[')]
    [TestCase('\n')]
    [TestCase('_')]
    public async Task InvalidLetter_ShouldThrowValidationException(char notLetter)
    {
        // Given
        var config =ClubSearchQueryParameters.Create().Letter(notLetter);

        // When
        var func = JikanTests.Instance.Awaiting(x => x.SearchClubAsync(config));

        // Then
        await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
    }

    [Test]
    [TestCase('A')]
    [TestCase('L')]
    [TestCase('S')]
    public async Task ValidLetter_ShouldReturnRecordsOnlyStartingOnLetter(char letter)
    {
        // Given
        var config = ClubSearchQueryParameters.Create().Letter(letter);

        // When
        var people = await JikanTests.Instance.SearchClubAsync(config);

        // Then
        people.Data.Should().OnlyContain(x => x.Name.StartsWith(letter));
    }
}
