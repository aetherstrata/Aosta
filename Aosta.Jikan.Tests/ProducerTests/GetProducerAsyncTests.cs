using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.ProducerTests;

public class GetProducerAsyncTests
{
    [Test]
    [TestCase(int.MinValue)]
    [TestCase(-1)]
    [TestCase(0)]
    public async Task InvalidId_ShouldThrowValidationException(long id)
    {
        // When
        var func = JikanTests.Instance.Awaiting(x => x.GetProducerAsync(id));

        // Then
        await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
    }
    
    [Test]
    public async Task PierrotId_ShouldParsePierrot()
    {
        // When
        var results = await JikanTests.Instance.GetProducerAsync(1);

        // Then
        using var _ = new AssertionScope();
        results.Data.Titles.Should().Contain(x => x.Title.Equals("Pierrot"));
        results.Data.TotalCount.Should().BeGreaterThan(250);
        results.Data.Established.Should().HaveYear(1979);
    }
    
    [Test]
    public async Task KyoAniId_ShouldParsePierrot()
    {
        // When
        var results = await JikanTests.Instance.GetProducerAsync(2);

        // Then
        using var _ = new AssertionScope();
        results.Data.Titles.Should().Contain(x => x.Title.Equals("Kyoto Animation"));
        results.Data.TotalCount.Should().BeGreaterThan(120);
        results.Data.Established.Should().HaveYear(1985);
        results.Data.About.Should().NotBeNullOrEmpty();
    }
}