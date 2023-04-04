using Aosta.Core.Data.Models.Jikan;
using Aosta.Core.Extensions;
using Aosta.Core.Jikan.Models.Response;

namespace Aosta.Core.Tests.Models.Jikan;

[TestFixture]
public class TitleTests
{
    [SetUp]
    public void SetUp()
    {
        _title = new TitleEntryResponse
        {
            Type = "type",
            Title = "title"
        };
    }

    private TitleEntryResponse _title = null!;

    [Test]
    public void ConversionTest()
    {
        var converted = _title.ToRealmObject();

        Assert.Multiple(() =>
        {
            Assert.That(converted.Type, Is.EqualTo("type"));
            Assert.That(converted.Title, Is.EqualTo("title"));
        });
    }

    [Test]
    public void DefaultValuesTest()
    {
        var newTitle = new TitleObject(new TitleEntryResponse());

        Assert.Multiple(() =>
        {
            Assert.That(newTitle.Type, Is.Empty);
            Assert.That(newTitle.Title, Is.Empty);
        });
    }
}