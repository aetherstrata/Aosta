using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.AnimeTests
{
	public class GetAnimeThemesAsyncTests
	{
		[Test]
		[TestCase(long.MinValue)]
		[TestCase(-1)]
		[TestCase(0)]
		public async Task InvalidId_ShouldThrowValidationException(long malId)
		{
			var func = JikanTests.Instance.Awaiting(x => x.GetAnimeThemesAsync(malId));

			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}

		[Test]
		public async Task BebopId_ShouldParseCowboyBebopOpeningsAndEndings()
		{
			var result = await JikanTests.Instance.GetAnimeThemesAsync(1);

			using var _ = new AssertionScope();
			result.Data.Openings.Should().ContainSingle().Which.Equals("\"Tank!\" by The Seatbelts (eps 1-25)");
			result.Data.Endings.Should().HaveCount(3);
		}
	}
}