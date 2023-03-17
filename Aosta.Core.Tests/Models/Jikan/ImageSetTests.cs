using Aosta.Core.Data.Models.Jikan;
using Aosta.Core.Extensions;
using JikanDotNet;

namespace Aosta.Core.Tests.Models.Jikan;

[TestFixture]
public class ImageSetTests
{
    private ImagesSet _set = null!;

    [SetUp]
    public void SetUp()
    {
        _set = new ImagesSet
        {
            JPG = new Image(),
            WebP = new Image()
        };
    }

    [Test]
    public void SetConversionTest()
    {
        _set.WebP.ImageUrl = "webp";
        _set.JPG.ImageUrl = "jpg";

        var converted = _set.ToRealmObject();

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
        var newSet = new ImageSetObject(new ImagesSet());

        Assert.Multiple(() =>
        {
            Assert.That(newSet.JPG, Is.Null);
            Assert.That(newSet.WebP, Is.Null);
        });
    }
}