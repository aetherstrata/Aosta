using Aosta.Data.Mapper;
using Aosta.Jikan.Models.Response;
using FluentAssertions.Execution;

namespace Aosta.Data.Tests.Models.Jikan;

[TestFixture]
public class ImageSetTests
{
    [Test]
    public void SetConversionTest()
    {
        var converted = new ImagesSetResponse
        {
            JPG = new ImageResponse { ImageUrl = "jpg" },
            WebP = new ImageResponse { ImageUrl = "webp" }
        }.ToModel();

        using var _ = new AssertionScope();
        converted.WebP.ImageUrl.Should().Be("webp");
        converted.JPG.ImageUrl.Should().Be("jpg");
    }

    [Test]
    public void SetDefaultValuesTest()
    {
        var newSet = new ImagesSetResponse().ToModel();

        using var _ = new AssertionScope();
        newSet.WebP.Should().BeNull();
        newSet.JPG.Should().BeNull();
    }
}