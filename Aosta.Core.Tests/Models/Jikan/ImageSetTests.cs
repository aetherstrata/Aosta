using Aosta.Core.Data.Models.Jikan;
using Aosta.Core.Extensions;
using Aosta.Core.Jikan.Models.Response;

namespace Aosta.Core.Tests.Models.Jikan;

[TestFixture]
public class ImageSetTests
{
    private ImagesSetResponse _setResponse = null!;

    [SetUp]
    public void SetUp()
    {
        _setResponse = new ImagesSetResponse
        {
            JPG = new ImageResponse(),
            WebP = new ImageResponse()
        };
    }

    [Test]
    public void SetConversionTest()
    {
        _setResponse.WebP.ImageUrl = "webp";
        _setResponse.JPG.ImageUrl = "jpg";

        var converted = _setResponse.ToRealmObject();

        Assert.Multiple(() =>
        {
            Assert.That(converted.WebP, Is.Not.Null);
            Assert.That(converted.JPG, Is.Not.Null);
        });

        Assert.Multiple(() =>
        {
            Assert.That(converted.WebP?.ImageUrl, Is.EqualTo("webp"));
            Assert.That(converted.JPG?.ImageUrl, Is.EqualTo("jpg"));
        });
    }

    [Test]
    public void SetDefaultValuesTest()
    {
        var newSet = new ImageSetObject(new ImagesSetResponse());

        Assert.Multiple(() =>
        {
            Assert.That(newSet.JPG, Is.Null);
            Assert.That(newSet.WebP, Is.Null);
        });
    }
}