using Aosta.Jikan.Exceptions;

namespace Aosta.Jikan.Tests.AnimeTests;

public class GetAnimeMoreInfoAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long malId)
	{
		var func = JikanTests.Instance.Awaiting(x => x.GetAnimeMoreInfoAsync(malId));

		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task BebopId_ShouldParseCowboyBebopMoreInfo()
	{
		var bebop = await JikanTests.Instance.GetAnimeMoreInfoAsync(1);

		bebop.Data.Info.Should().StartWith("Suggested Order of Viewing");
	}
}