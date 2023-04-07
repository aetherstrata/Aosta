namespace Aosta.Jikan.Tests.GetRandomTests
{
	public class GetRandomCharacterAsyncTests
	{
		[Test]
		public async Task ShouldReturnNotNullCharacter()
		{
			// When
			var character = await JikanTests.Instance.GetRandomCharacterAsync();

			// Then
			character.Data.Should().NotBeNull();
		}
	}
}