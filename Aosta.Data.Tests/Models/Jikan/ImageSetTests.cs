using Aosta.Jikan.Models.Response;
using FluentAssertions.Execution;

using JikanMapper = Aosta.Data.Mapper.JikanMapper;

namespace Aosta.Data.Tests.Models.Jikan;

[TestFixture]
public class ImageSetTests
{
    [Test]
    public void SetConversionTest()
    {
        var converted = JikanMapper.ToModel(new ImagesSetResponse
        {
            JPG = new ImageResponse { ImageUrl = "jpg" },
            WebP = new ImageResponse { ImageUrl = "webp" }
        });

        using var _ = new AssertionScope();
        converted.WebP.ImageUrl.Should().Be("webp");
        converted.JPG.ImageUrl.Should().Be("jpg");
    }

    [Test]
    public void SetDefaultValuesTest()
    {
        var newSet = JikanMapper.ToModel(new ImagesSetResponse());

        using var _ = new AssertionScope();
        newSet.WebP.Should().BeNull();
        newSet.JPG.Should().BeNull();
    }
}