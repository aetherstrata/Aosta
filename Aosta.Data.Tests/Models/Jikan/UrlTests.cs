using Aosta.Jikan.Models.Response;
using FluentAssertions.Execution;

using JikanMapper = Aosta.Data.Mapper.JikanMapper;

namespace Aosta.Data.Tests.Models.Jikan;

[TestFixture]
public class UrlTests
{
    [Test]
    public void ConversionTest()
    {
        var converted = JikanMapper.ToModel(new MalUrlResponse
        {
            MalId = 1,
            Type = "type",
            Url = "url",
            Name = "name"
        });

        using var _ = new AssertionScope();
        converted.MalId.Should().Be(1);
        converted.Type.Should().Be("type");
        converted.Url.Should().Be("url");
        converted.Name.Should().Be("name");
    }

    [Test]
    public void DefaultValuesTest()
    {
        var newUrl = JikanMapper.ToModel(new MalUrlResponse());

        using var _ = new AssertionScope();
        newUrl.MalId.Should().Be(0);
        newUrl.Type.Should().BeNull();
        newUrl.Url.Should().BeNull();
        newUrl.Name.Should().BeNull();
    }
}