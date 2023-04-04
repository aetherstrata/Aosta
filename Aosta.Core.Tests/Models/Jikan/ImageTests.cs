using Aosta.Core.Data.Models.Jikan;
using Aosta.Core.Extensions;
using Aosta.Core.Jikan.Models.Response;

namespace Aosta.Core.Tests.Models.Jikan;

[TestFixture]
public class ImageTests
{
    [SetUp]
    public void SetUp()
    {
        _image1 = new ImageResponse
        {
            ImageUrl = "https://url.com/image.jpg",
            SmallImageUrl = "https://url.com/smallImage.jpg",
            MediumImageUrl = "https://url.com/mediumImage.jpg",
            LargeImageUrl = "https://url.com/largeImage.jpg",
            MaximumImageUrl = "https://url.com/MaximumImage.jpg"
        };
    }

    private ImageResponse _image1 = null!;

    [Test]
    public void ConversionTest()
    {
        var converted = _image1.ToRealmObject();

        AssertImagesAreEqual(converted, _image1);
    }

    [Test]
    public void DefaultValuesTest()
    {
        var newImage = new ImageObject(new ImageResponse());

        Assert.Multiple(() =>
        {
            Assert.That(newImage.ImageUrl, Is.Empty);
            Assert.That(newImage.SmallImageUrl, Is.Empty);
            Assert.That(newImage.MediumImageUrl, Is.Empty);
            Assert.That(newImage.LargeImageUrl, Is.Empty);
            Assert.That(newImage.MaximumImageUrl, Is.Empty);
        });
    }

    private static void AssertImagesAreEqual(ImageObject converted, ImageResponse input)
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