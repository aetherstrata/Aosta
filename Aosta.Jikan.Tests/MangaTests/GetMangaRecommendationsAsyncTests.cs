using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.MangaTests
{
	public class GetMangaRecommendationsAsyncTests
	{
		[Test]
		[TestCase(long.MinValue)]
		[TestCase(-1)]
		[TestCase(0)]
		public async Task InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			var func = JikanTests.Instance.Awaiting(x => x.GetMangaRecommendationsAsync(id));

			// Then
			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}

		[Test]
		public async Task BerserkId_ShouldParseBerserkRecommendations()
		{
			// When
			var berserk = await JikanTests.Instance.GetMangaRecommendationsAsync(2);

			// Then
			using (new AssertionScope())
			{
				//Claymore
				berserk.Data.First().Entry.MalId.Should().Be(583);
				berserk.Data.First().Votes.Should().BeGreaterThan(25);
				berserk.Data.Count.Should().BeGreaterThan(90);
			}
		}
	}
}