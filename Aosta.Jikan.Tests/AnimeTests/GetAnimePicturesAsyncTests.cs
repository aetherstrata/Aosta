using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.AnimeTests;

public class GetAnimePicturesAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long malId)
	{
		var func = JikanTests.Instance.Awaiting(x => x.GetAnimePicturesAsync(malId));

		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task BebopId_ShouldParseCowboyBebopImages()
	{
		var bebop = await JikanTests.Instance.GetAnimePicturesAsync(1);

		using var _ = new AssertionScope();
		bebop.Data.Should().HaveCount(14);
		bebop.Data.First().JPG.ImageUrl.Should().NotBeNullOrWhiteSpace();
		bebop.Data.First().JPG.SmallImageUrl.Should().NotBeNullOrWhiteSpace();
		bebop.Data.First().JPG.LargeImageUrl.Should().NotBeNullOrWhiteSpace();
		bebop.Data.First().WebP.ImageUrl.Should().NotBeNullOrWhiteSpace();
		bebop.Data.First().WebP.SmallImageUrl.Should().NotBeNullOrWhiteSpace();
		bebop.Data.First().WebP.LargeImageUrl.Should().NotBeNullOrWhiteSpace();
	}
}
