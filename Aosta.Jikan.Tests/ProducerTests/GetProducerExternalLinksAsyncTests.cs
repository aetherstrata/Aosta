using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.ProducerTests;

public class GetProducerExternalLinksAsyncTests
{
    [Test]
    [TestCase(int.MinValue)]
    [TestCase(-1)]
    [TestCase(0)]
    public async Task InvalidId_ShouldThrowValidationException(long id)
    {
        // When
        var func = JikanTests.Instance.Awaiting(x => x.GetProducerExternalLinksAsync(id));

        // Then
        await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
    }
    
    [Test]
    public async Task PierrotId_ShouldParsePierrot()
    {
        // When
        var results = await JikanTests.Instance.GetProducerExternalLinksAsync(1);

        // Then
        using var _ = new AssertionScope();
        results.Data.Should().HaveCountGreaterOrEqualTo(5);
        results.Data.Should().Contain(x => x.Name.Equals("pierrot.jp") && x.Url.Equals("http://pierrot.jp/\r"));
    }
}