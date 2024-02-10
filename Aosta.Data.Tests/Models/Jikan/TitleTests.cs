using Aosta.Jikan.Models.Response;
using FluentAssertions.Execution;

using JikanMapper = Aosta.Data.Mapper.JikanMapper;

namespace Aosta.Data.Tests.Models.Jikan;

[TestFixture]
public class TitleTests
{
    [Test]
    public void ConversionTest()
    {
        var converted = JikanMapper.ToModel(new TitleEntryResponse
        {
            Type = "type",
            Title = "title"
        });

        using var _ = new AssertionScope();
        converted.Type.Should().Be("type");
        converted.Title.Should().Be("title");
    }
}
