namespace Aosta.Jikan.Tests.GetRandomTests
{
	public class GetRandomUserAsyncTests
	{
		[Test]
		public async Task ShouldReturnNotNullUser()
		{
			// When
			var user = await JikanTests.Instance.GetRandomUserAsync();

			// Then
			user.Data.Should().NotBeNull();
		}
	}
}