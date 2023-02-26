using System.Diagnostics;
using Aosta.Core.Data.Models.Jikan;
using Aosta.Core.Extensions;
using JikanDotNet;

namespace Aosta.Core.Tests.Models.Jikan;

[TestFixture]
public class ImageTests
{
    private Image _image1 = null!;
    private Image _image2 = null!;

    private ImagesSet _set = null!;

    [SetUp]
    public void SetUp()
    {
        _image1 = new Image
        {
            ImageUrl = "https://url.com/image.jpg",
            SmallImageUrl = "https://url.com/smallImage.jpg",
            MediumImageUrl = "https://url.com/mediumImage.jpg",
            LargeImageUrl = "https://url.com/largeImage.jpg",
            MaximumImageUrl = "https://url.com/MaximumImage.jpg"
        };

        _image2 = new Image
        {
            ImageUrl = "https://url.com/image.webp",
            SmallImageUrl = "https://url.com/smallImage.webp",
            MediumImageUrl = "https://url.com/mediumImage.webp",
            LargeImageUrl = "https://url.com/largeImage.webp",
            MaximumImageUrl = "https://url.com/MaximumImage.webp"
        };

        _set = new ImagesSet()
        {
            JPG = _image1,
            WebP = _image2
        };
    }

    [Test]
    public void SingleConversionTest()
    {
        var converted = _image1.ToRealmObject();

        AssertImagesAreEqual(converted, _image1);
    }

    [Test]
    public void SingeDefaultValuesTest()
    {
        var newImage = new ImageObject(new Image());

        Assert.Multiple(() =>
        {
            Assert.That(newImage.ImageUrl, Is.Empty);
            Assert.That(newImage.SmallImageUrl, Is.Empty);
            Assert.That(newImage.MediumImageUrl, Is.Empty);
            Assert.That(newImage.LargeImageUrl, Is.Empty);
            Assert.That(newImage.MaximumImageUrl, Is.Empty);
        });
    }

    [Test]
    public void SetConversionTest()
    {
        var converted = _set.ToRealmObject();

        AssertImagesAreEqual(converted.JPG, _set.JPG);
        AssertImagesAreEqual(converted.WebP, _set.WebP);
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

    private static void AssertImagesAreEqual(ImageObject converted, Image input)
    {
        Assert.Multiple(() =>
        {
            Assert.That(converted.ImageUrl, Is.EqualTo(input.ImageUrl));
            Assert.That(converted.MaximumImageUrl, Is.EqualTo(input.MaximumImageUrl));
            Assert.That(converted.LargeImageUrl, Is.EqualTo(input.LargeImageUrl));
            Assert.That(converted.MediumImageUrl, Is.EqualTo(input.MediumImageUrl));
            Assert.That(converted.SmallImageUrl, Is.EqualTo(input.SmallImageUrl));
        });
    }
}