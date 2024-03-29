using Aosta.Jikan.Exceptions;
using Aosta.Jikan.Query;
using Aosta.Jikan.Query.Enums;
using Aosta.Jikan.Query.Parameters;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.PersonTests;

public class SearchPersonAsyncTests
{
    [Test]
    public async Task EmptyConnfig_ShouldReturnFirst25People()
    {
        // Given
        var config = PersonSearchQueryParameters.Create();

        // When
        var people = await JikanTests.Instance.SearchPersonAsync(config);

        // Then
        using var _ = new AssertionScope();
        people.Data.Should().HaveCount(JikanParameterConsts.MAXIMUM_PAGE_SIZE);
        people.Data.First().Name.Should().Be("Tomokazu Seki");
        people.Pagination.LastVisiblePage.Should().BeGreaterThan(370);
        people.Pagination.CurrentPage.Should().Be(1);
        people.Pagination.Items.Count.Should().Be(25);
        people.Pagination.Items.PerPage.Should().Be(25);
    }

    [Test]
    [TestCase(int.MinValue)]
    [TestCase(-1)]
    [TestCase(0)]
    public async Task InvalidPage_ShouldThrowValidationException(int page)
    {
        // Given
        var config = PersonSearchQueryParameters.Create().Page(page);

        // When
        var func = JikanTests.Instance.Awaiting(x => x.SearchPersonAsync(config));

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
        var config = PersonSearchQueryParameters.Create().Limit(pageSize);

        // When
        var func = JikanTests.Instance.Awaiting(x => x.SearchPersonAsync(config));

        // Then
        await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
    }

    [Test]
    public async Task GivenSecondPage_ShouldReturnSecondPage()
    {
        // Given
        var config = PersonSearchQueryParameters.Create().Page(2);

        // When
        var people = await JikanTests.Instance.SearchPersonAsync(config);

        // Then
        using var _ = new AssertionScope();
        people.Data.Should().HaveCount(JikanParameterConsts.MAXIMUM_PAGE_SIZE);
        people.Data.First().Name.Should().Be("Travis Willingham");
        people.Pagination.LastVisiblePage.Should().BeGreaterThan(370);
        people.Pagination.CurrentPage.Should().Be(2);
        people.Pagination.Items.Count.Should().Be(25);
        people.Pagination.Items.PerPage.Should().Be(25);
    }

    [Test]
    public async Task GivenValidPageSize_ShouldReturnPageSizeNumberOfRecords()
    {
        // Given
        const int pageSize = 5;
        var config = PersonSearchQueryParameters.Create().Limit(pageSize);

        // When
        var people = await JikanTests.Instance.SearchPersonAsync(config);

        // Then
        using var _ = new AssertionScope();
        people.Data.Should().HaveCount(pageSize);
        people.Data.First().Name.Should().Be("Tomokazu Seki");
        people.Pagination.CurrentPage.Should().Be(1);
        people.Pagination.Items.Count.Should().Be(pageSize);
        people.Pagination.Items.PerPage.Should().Be(pageSize);
    }

    [Test]
    public async Task GivenValidPageAndPageSize_ShouldReturnPageSizeNumberOfRecordsFromNextPage()
    {
        // Given
        const int pageSize = 5;
        var config = PersonSearchQueryParameters.Create().Page(2).Limit(pageSize);

        // When
        var people = await JikanTests.Instance.SearchPersonAsync(config);

        // Then
        using var _ = new AssertionScope();
        people.Data.Should().HaveCount(pageSize);
        people.Data.First().Name.Should().Be("Toshiyuki Morikawa");
        people.Pagination.CurrentPage.Should().Be(2);
        people.Pagination.Items.Count.Should().Be(pageSize);
        people.Pagination.Items.PerPage.Should().Be(pageSize);
    }

    [Test]
    [TestCase('1')]
    [TestCase('0')]
    [TestCase('[')]
    [TestCase('\n')]
    [TestCase('_')]
    public void InvalidLetter_ShouldThrowValidationException(char notLetter)
    {
        // Given
        var config = PersonSearchQueryParameters.Create();

        // When
        var func = config.Invoking(x => x.Letter(notLetter));

        // Then
        func.Should().ThrowExactly<JikanParameterValidationException>();
    }

    [Test]
    [TestCase('A')]
    [TestCase('L')]
    [TestCase('S')]
    public async Task ValidLetter_ShouldReturnRecordsOnlyStartingOnLetter(char letter)
    {
        // Given
        var config = PersonSearchQueryParameters.Create().Letter(letter);

        // When
        var people = await JikanTests.Instance.SearchPersonAsync(config);

        // Then
        using var _ = new AssertionScope();
        people.Data.Should().HaveCount(JikanParameterConsts.MAXIMUM_PAGE_SIZE);
        people.Data.Should().OnlyContain(x => x.Name.StartsWith(letter));
    }

    [Test]
    public async Task KanaQuery_ShouldReturnKanas()
    {
        // Given
        var config = PersonSearchQueryParameters.Create().Query("Kana");

        // When
        var people = await JikanTests.Instance.SearchPersonAsync(config);

        // Then
        using var _ = new AssertionScope();
        people.Data.Should().Contain(x => x.Name.Equals("Kana Hanazawa"));
        people.Data.Should().Contain(x => x.Name.Equals("Kana Ueda"));
        people.Data.Should().Contain(x => x.Name.Equals("Yukana"));
        people.Data.First().Name.Should().Be("Kana Ueda");
    }

    [Test]
    public async Task KanaQueryByPopularity_ShouldReturnKanaWithHanazawaFirst()
    {
        // Given
        var config = PersonSearchQueryParameters.Create()
            .Query("Kana")
            .Sort(PersonSearchOrderBy.Favorites)
            .SortDirection(SortDirection.Descending);

        // When
        var people = await JikanTests.Instance.SearchPersonAsync(config);

        // Then
        using var _ = new AssertionScope();
        people.Data.Should().Contain(x => x.Name.Equals("Kana Hanazawa"));
        people.Data.Should().Contain(x => x.Name.Equals("Kana Ueda"));
        people.Data.Should().Contain(x => x.Name.Equals("Yukana"));
        people.Data.First().Name.Should().Be("Kana Hanazawa");
    }

    [Test]
    public async Task KanaQueryByReversePopularity_ShouldReturnKanaWithoutHanazawa()
    {
        // Given
        var config =  PersonSearchQueryParameters.Create()
            .Query("Kana")
            .Sort(PersonSearchOrderBy.Favorites)
            .SortDirection(SortDirection.Ascending);

        // When
        var people = await JikanTests.Instance.SearchPersonAsync(config);

        // Then
        using var _ = new AssertionScope();
        people.Data.Should().NotContain(x => x.Name.Equals("Kana Hanazawa"));
        people.Data.Should().NotContain(x => x.Name.Equals("Kana Ueda"));
        people.Data.Should().NotContain(x => x.Name.Equals("Yukana"));
        people.Data.Should().OnlyContain(x => x.Name.Contains("kana") || x.Name.Contains("Kana"));
        people.Data.First().MemberFavorites.Should().Be(0);
    }

    [Test]
    public async Task KanaQueryByMalIdWithLimit2_ShouldReturnMikaKanaiAndKanakoSakai()
    {
        // Given
        var config =  PersonSearchQueryParameters.Create()
            .Query("Kana")
            .Sort(PersonSearchOrderBy.MalId)
            .Limit(2);

        // When
        var people = await JikanTests.Instance.SearchPersonAsync(config);

        // Then
        using var _ = new AssertionScope();
        people.Data.Should().HaveCount(2);
        people.Data.Should().Contain(x => x.Name.Equals("Mika Kanai"));
        people.Data.Should().Contain(x => x.Name.Equals("Kanako Sakai"));
    }

    [Test]
    public async Task MiyukiSawaQuery_ShouldReturnSingleSawashiro()
    {
        // Given
        var config = PersonSearchQueryParameters.Create().Query("miyuki sawa");

        // When
        var people = await JikanTests.Instance.SearchPersonAsync(config);

        // Then
        var result = people.Data.First();

        using var _ = new AssertionScope();
        result.Name.Should().Be("Miyuki Sawashiro");
        result.GivenName.Should().Be("みゆき");
        result.FamilyName.Should().Be("沢城");
        result.Birthday.Should().BeSameDateAs(new DateTime(1985,6,2, 0, 0, 0, DateTimeKind.Utc));
        result.MalId.Should().Be(99);
        result.WebsiteUrl.Should().BeNull();
    }
}
