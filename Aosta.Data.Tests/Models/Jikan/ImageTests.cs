using Aosta.Jikan.Models.Response;
using FluentAssertions.Execution;

using JikanMapper = Aosta.Data.Mapper.JikanMapper;

namespace Aosta.Data.Tests.Models.Jikan;

[TestFixture]
public class ImageTests
{
    [Test]
    public void ConversionTest()
    {
        var converted = JikanMapper.ToModel(new ImageResponse
        {
            ImageUrl = "https://url.com/image.jpg",
            SmallImageUrl = "https://url.com/smallImage.jpg",
            MediumImageUrl = "https://url.com/mediumImage.jpg",
            LargeImageUrl = "https://url.com/largeImage.jpg",
            MaximumImageUrl = "https://url.com/MaximumImage.jpg"
        });

        using var _ = new AssertionScope();
        converted.ImageUrl.Should().Be("https://url.com/image.jpg");
        converted.SmallImageUrl.Should().Be("https://url.com/smallImage.jpg");
        converted.MediumImageUrl.Should().Be("https://url.com/mediumImage.jpg");
        converted.LargeImageUrl.Should().Be("https://url.com/largeImage.jpg");
        converted.MaximumImageUrl.Should().Be("https://url.com/MaximumImage.jpg");
    }

    [Test]
    public void DefaultValuesTest()
    {
        var newImage = JikanMapper.ToModel(new ImageResponse());

        using var _ = new AssertionScope();
        newImage.ImageUrl.Should().BeNull();
        newImage.SmallImageUrl.Should().BeNull();
        newImage.MediumImageUrl.Should().BeNull();
        newImage.LargeImageUrl.Should().BeNull();
        newImage.MaximumImageUrl.Should().BeNull();
    }
}