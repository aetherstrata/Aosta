namespace Aosta.Jikan.Tests.GetRandomTests
{
	public class GetRandomPersonAsyncTests
	{
		[Test]
		public async Task ShouldReturnNotNullPerson()
		{
			// When
			var person = await JikanTests.Instance.GetRandomPersonAsync();

			// Then
			person.Data.Should().NotBeNull();
		}
	}
}