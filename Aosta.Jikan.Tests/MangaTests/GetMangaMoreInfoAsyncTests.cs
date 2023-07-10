namespace Aosta.Jikan.Tests.MangaTests;

public class GetMangaMoreInfoAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long id)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetMangaMoreInfoAsync(id));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task BerserkId_ShouldParseBerserkMoreInfo()
	{
		// When
		var berserk = await JikanTests.Instance.GetMangaMoreInfoAsync(2);

		// Then
		berserk.Data.Info.Should().Contain("The Prototype (1988)");
	}
}