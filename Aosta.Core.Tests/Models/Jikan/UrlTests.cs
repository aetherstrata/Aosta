using Aosta.Core.Data.Models.Jikan;
using Aosta.Core.Extensions;
using Aosta.Core.Jikan.Models.Response;

namespace Aosta.Core.Tests.Models.Jikan;

[TestFixture]
public class UrlTests
{
    [SetUp]
    public void SetUp()
    {
        _urlResponse = new MalUrlResponse
        {
            MalId = 1,
            Type = "type",
            Url = "url",
            Name = "name"
        };
    }

    private MalUrlResponse _urlResponse = null!;

    [Test]
    public void ConversionTest()
    {
        var converted = _urlResponse.ToRealmObject();

        Assert.Multiple(() =>
        {
            Assert.That(converted.MalId, Is.EqualTo(1));
            Assert.That(converted.Type, Is.EqualTo("type"));
            Assert.That(converted.Url, Is.EqualTo("url"));
            Assert.That(converted.Name, Is.EqualTo("name"));
        });
    }

    [Test]
    public void DefaultValuesTest()
    {
        var newUrl = new UrlObject(new MalUrlResponse());

        Assert.Multiple(() =>
        {
            Assert.That(newUrl.MalId, Is.EqualTo(0));
            Assert.That(newUrl.Type, Is.Empty);
            Assert.That(newUrl.Url, Is.Empty);
            Assert.That(newUrl.Name, Is.Empty);
        });
    }
}