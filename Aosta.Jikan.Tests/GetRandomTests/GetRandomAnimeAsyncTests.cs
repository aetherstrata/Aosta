namespace Aosta.Jikan.Tests.GetRandomTests;

public class GetRandomAnimeAsyncTests
{
	[Test]
	public async Task ShouldReturnNotNullAnime()
	{
		// When
		var anime = await JikanTests.Instance.GetRandomAnimeAsync();

		// Then
		anime.Data.Should().NotBeNull();
	}
}