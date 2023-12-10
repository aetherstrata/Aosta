using Aosta.Jikan.Query;
using Aosta.Jikan.Query.Enums;
using Aosta.Jikan.Query.Parameters;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.CharacterTests;

public class SearchCharacterAsyncTests
{
    [Test]
    public async Task EmptyConfig_ShouldReturnFirst25People()
    {
        // Given
        var config = new CharacterSearchQueryParameters();

        // When
        var characters = await JikanTests.Instance.SearchCharacterAsync(config);

        // Then
        using var _ = new AssertionScope();
        characters.Data.Should().HaveCount(JikanParameterConsts.MAXIMUM_PAGE_SIZE);
        characters.Data.First().Name.Should().Be("Spike Spiegel");
        characters.Data.First().NameKanji.Should().Be("スパイク・スピーゲル");
        characters.Pagination.LastVisiblePage.Should().BeGreaterThan(2350);
    }

    [Test]
    [TestCase(int.MinValue)]
    [TestCase(-1)]
    [TestCase(0)]
    public async Task InvalidPage_ShouldThrowValidationException(int page)
    {
        // Given
        var config = new CharacterSearchQueryParameters().SetPage(page);

        // When
        var func = JikanTests.Instance.Awaiting(x => x.SearchCharacterAsync(config));

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
        var config = new CharacterSearchQueryParameters().SetLimit(pageSize);

        // When
        var func = JikanTests.Instance.Awaiting(x => x.SearchCharacterAsync(config));

        // Then
        await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
    }

    [Test]
    public async Task GivenSecondPage_ShouldReturnSecondPage()
    {
        // Given
        var config = new CharacterSearchQueryParameters().SetPage(2);

        // When
        var characters = await JikanTests.Instance.SearchCharacterAsync(config);

        // Then
        using var _ = new AssertionScope();
        characters.Data.Should().HaveCount(JikanParameterConsts.MAXIMUM_PAGE_SIZE);
        characters.Data.First().Name.Should().Be("Leorio Paladiknight");
        characters.Data.First().NameKanji.Should().StartWith("レオリオ=パラディナｲﾄ");
        characters.Pagination.LastVisiblePage.Should().BeGreaterThan(2350);
        characters.Pagination.CurrentPage.Should().Be(2);
        characters.Pagination.Items.Count.Should().Be(25);
        characters.Pagination.Items.PerPage.Should().Be(25);
    }

    [Test]
    public async Task GivenValidPageSize_ShouldReturnPageSizeNumberOfRecords()
    {
        // Given
        const int pageSize = 5;
        var config = new CharacterSearchQueryParameters().SetLimit(pageSize);

        // When
        var characters = await JikanTests.Instance.SearchCharacterAsync(config);

        // Then
        using var _ = new AssertionScope();
        characters.Data.Should().HaveCount(pageSize);
        characters.Data.First().Name.Should().Be("Spike Spiegel");
        characters.Data.First().NameKanji.Should().Be("スパイク・スピーゲル");
        characters.Pagination.CurrentPage.Should().Be(1);
        characters.Pagination.Items.Count.Should().Be(pageSize);
        characters.Pagination.Items.PerPage.Should().Be(pageSize);
    }

    [Test]
    public async Task GivenValidPageAndPageSize_ShouldReturnPageSizeNumberOfRecordsFromNextPage()
    {
        // Given
        const int pageSize = 5;
        var config = new CharacterSearchQueryParameters().SetPage(2).SetLimit(pageSize);

        // When
        var characters = await JikanTests.Instance.SearchCharacterAsync(config);

        // Then
        using var _ = new AssertionScope();
        characters.Data.Should().HaveCount(pageSize);
        characters.Data.First().Name.Should().Be("Rukia Kuchiki");
        characters.Pagination.CurrentPage.Should().Be(2);
        characters.Pagination.Items.Count.Should().Be(pageSize);
        characters.Pagination.Items.PerPage.Should().Be(pageSize);
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
        var config = new CharacterSearchQueryParameters().SetLetter(notLetter);

        // When
        var func = JikanTests.Instance.Awaiting(x => x.SearchCharacterAsync(config));

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
        var config = new CharacterSearchQueryParameters().SetLetter(letter);

        // When
        var people = await JikanTests.Instance.SearchCharacterAsync(config);

        // Then
        using var _ = new AssertionScope();
        people.Data.Should().HaveCount(JikanParameterConsts.MAXIMUM_PAGE_SIZE);
        people.Data.Should().OnlyContain(x => x.Name.StartsWith(letter));
    }

    [Test]
    public async Task LupinQuery_ShouldReturnLupins()
    {
        // Given
        var config = new CharacterSearchQueryParameters().SetQuery("Lupin");

        // When
        var people = await JikanTests.Instance.SearchCharacterAsync(config);

        // Then
        using var _ = new AssertionScope();
        people.Data.Should().Contain(x => x.Name.Equals("Fake Lupin"));
        people.Data.Should().Contain(x => x.Name.Equals("Arsène Lupin"));
        people.Data.Should().Contain(x => x.Name.Equals("Lupin II"));
        people.Data.First().Name.Should().Be("Lupin");
    }

    [Test]
    public async Task LupinQueryByPopularity_ShouldReturnLupinsWithKurobaFirst()
    {
        // Given
        var config = new CharacterSearchQueryParameters()
            .SetQuery("Lupin")
            .SetOrder(CharacterSearchOrderBy.Favorites)
            .SetSortDirection(SortDirection.Descending);

        // When
        var people = await JikanTests.Instance.SearchCharacterAsync(config);

        // Then
        using var _ = new AssertionScope();
        people.Data.Should().Contain(x => x.Name.Equals("Fake Lupin"));
        people.Data.Should().Contain(x => x.Name.Equals("Arsène Lupin"));
        people.Data.Should().Contain(x => x.Name.Equals("Lupin II"));
        people.Data.First().Name.Should().Be("Lupin");
    }

    [Test]
    public async Task LupinQueryByReversePopularity_ShouldReturnLupinsWithKurobaLast()
    {
        // Given
        var config =  new CharacterSearchQueryParameters()
            .SetQuery("Lupin")
            .SetOrder(CharacterSearchOrderBy.Favorites)
            .SetSortDirection(SortDirection.Ascending);

        // When
        var people = await JikanTests.Instance.SearchCharacterAsync(config);

        // Then
        using var _ = new AssertionScope();
        people.Data.Should().Contain(x => x.Name.Equals("Fake Lupin"));
        people.Data.Should().Contain(x => x.Name.Equals("Arsène Lupin"));
        people.Data.Should().Contain(x => x.Name.Equals("Lupin II"));
        people.Data.Last().Name.Should().Be("Arsene Lupin III");
        people.Data.First().Favorites.Should().BeGreaterOrEqualTo(0);
    }

    [Test]
    public async Task LupinQueryByMalIdWithLimit2_ShouldReturnThirdAndKaitoKuroba()
    {
        // Given
        var config = new CharacterSearchQueryParameters()
            .SetQuery("Lupin")
            .SetOrder(CharacterSearchOrderBy.Favorites)
            .SetPage(2);

        // When
        var people = await JikanTests.Instance.SearchCharacterAsync(config);

        // Then
        using var _ = new AssertionScope();
        people.Data.Should().HaveCount(2);
        people.Data.Should().Contain(x => x.Name.Equals("Lupin"));
    }

    [Test]
    public async Task KirumiQuery_ShouldReturnSingleKirumi()
    {
        // Given
        var config = new CharacterSearchQueryParameters()
            .SetQuery("kirumi to");

        // When
        var people = await JikanTests.Instance.SearchCharacterAsync(config);

        // Then
        using var _ = new AssertionScope();
        people.Data.Should().ContainSingle();

        var result = people.Data.First();
        result.Name.Should().Be("Kirumi Toujou");
        result.Nicknames.Should().BeEmpty();
        result.About.Should().NotBeEmpty();
        result.MalId.Should().Be(157604);
    }
}
