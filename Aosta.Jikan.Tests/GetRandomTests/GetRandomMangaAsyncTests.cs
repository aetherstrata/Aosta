namespace Aosta.Jikan.Tests.GetRandomTests;

public class GetRandomMangaAsyncTests
{
	[Test]
	public async Task ShouldReturnNotNullManga()
	{
		// When
		var manga = await JikanTests.Instance.GetRandomMangaAsync();

		// Then
		manga.Data.Should().NotBeNull();
	}
}