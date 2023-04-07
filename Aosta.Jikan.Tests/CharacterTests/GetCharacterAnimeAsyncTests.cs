using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.CharacterTests
{
	public class GetCharacterAnimeAsyncTests
	{
		[Test]
		[TestCase(long.MinValue)]
		[TestCase(-1)]
		[TestCase(0)]
		public async Task InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = JikanTests.Instance.Awaiting(x => x.GetCharacterAnimeAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}

		[Test]
		public async Task SpikeSpiegelId_ShouldParseSpikeSpiegelAnime()
		{
			// When
			var spike = await JikanTests.Instance.GetCharacterAnimeAsync(1);

			// Then
			using (new AssertionScope())
			{
				spike.Data.Should().HaveCount(3);
				spike.Data.Should().OnlyContain(x => x.Role.Equals("Main"));
				spike.Data.Should().OnlyContain(
					x => !string.IsNullOrWhiteSpace(x.Anime.Images.JPG.ImageUrl)
					&& !string.IsNullOrWhiteSpace(x.Anime.Images.JPG.SmallImageUrl)
					&& !string.IsNullOrWhiteSpace(x.Anime.Images.JPG.LargeImageUrl)
				);
			}
		}

		[Test]
		public async Task IchigoKurosakiId_ShouldParseIchigoKurosakiAnime()
		{
			// When
			var ichigo = await JikanTests.Instance.GetCharacterAnimeAsync(5);

			// Then
			using (new AssertionScope())
			{
				ichigo.Data.Should().HaveCount(9);
				ichigo.Data.Select(x => x.Anime.Title).Should().Contain("Bleach");
			}
		}
	}
}