using Aosta.Core.Database.Mapper;
using Aosta.Jikan.Models.Response;
using FluentAssertions.Execution;

namespace Aosta.Core.Tests.Models.Jikan;

[TestFixture]
public class TitleTests
{
    [Test]
    public void ConversionTest()
    {
        var converted = new TitleEntryResponse
        {
            Type = "type",
            Title = "title"
        }.ToRealmModel();

        using var _ = new AssertionScope();
        converted.Type.Should().Be("type");
        converted.Title.Should().Be("title");
    }

    [Test]
    public void DefaultValuesTest()
    {
        var newTitle = new TitleEntryResponse().ToRealmModel();

        using var _ = new AssertionScope();
        newTitle.Type.Should().BeNull();
        newTitle.Title.Should().BeNull();
    }
}