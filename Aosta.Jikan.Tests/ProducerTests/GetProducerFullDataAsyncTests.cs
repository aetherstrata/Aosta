using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.ProducerTests;

public class GetProducerFullDataAsyncTests
{
    [Test]
    [TestCase(int.MinValue)]
    [TestCase(-1)]
    [TestCase(0)]
    public async Task InvalidId_ShouldThrowValidationException(long id)
    {
        // When
        var func = JikanTests.Instance.Awaiting(x => x.GetProducerFullDataAsync(id));

        // Then
        await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
    }
    
    [Test]
    public async Task PierrotId_ShouldParsePierrot()
    {
        // When
        var results = await JikanTests.Instance.GetProducerFullDataAsync(1);

        // Then
        using var _ = new AssertionScope();
        results.Data.Titles.Should().Contain(x => x.Title.Equals("Pierrot"));
        results.Data.TotalCount.Should().BeGreaterThan(250);
        results.Data.Established.Should().HaveYear(1979);
        results.Data.External.Should().HaveCountGreaterOrEqualTo(5);
        results.Data.External.Should().Contain(x => x.Name.Equals("pierrot.jp") && x.Url.Equals("http://pierrot.jp/\r"));
    }
    
    [Test]
    public async Task KyoAniId_ShouldParsePierrot()
    {
        // When
        var results = await JikanTests.Instance.GetProducerFullDataAsync(2);

        // Then
        using var _ = new AssertionScope();
        results.Data.Titles.Should().Contain(x => x.Title.Equals("Kyoto Animation"));
        results.Data.TotalCount.Should().BeGreaterThan(120);
        results.Data.Established.Should().HaveYear(1985);
        results.Data.About.Should().NotBeNullOrEmpty();
        results.Data.External.Should().HaveCountGreaterOrEqualTo(5);
    }
}