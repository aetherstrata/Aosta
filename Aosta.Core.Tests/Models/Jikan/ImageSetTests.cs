using Aosta.Core.Database.Mapper;
using Aosta.Jikan.Models.Response;
using FluentAssertions.Execution;

namespace Aosta.Core.Tests.Models.Jikan;

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
        }.ToRealmModel();

        using var _ = new AssertionScope();
        converted.WebP.ImageUrl.Should().Be("webp");
        converted.JPG.ImageUrl.Should().Be("jpg");
    }

    [Test]
    public void SetDefaultValuesTest()
    {
        var newSet = new ImagesSetResponse().ToRealmModel();

        using var _ = new AssertionScope();
        newSet.WebP.ImageUrl.Should().BeNull();
        newSet.JPG.ImageUrl.Should().BeNull();
    }
}