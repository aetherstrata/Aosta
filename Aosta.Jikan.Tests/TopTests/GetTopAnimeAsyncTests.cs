using Aosta.Core.Utils.Exceptions;
using Aosta.Jikan.Enums;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.TopTests
{
	public class GetTopAnimeAsyncTests
	{
		[Test]
		public async Task NoParameter_ShouldParseTopAnime()
		{
			// When
			var top = await JikanTests.Instance.GetTopAnimeAsync();

			top.Should().NotBeNull();
		}

		[Test]
		public async Task NoParameter_ShouldParseFMA()
		{
			// When
			var top = await JikanTests.Instance.GetTopAnimeAsync();

			// Then
			top.Data.First().Title.Should().Be("Fullmetal Alchemist: Brotherhood");
			top.Data.First().Episodes.Should().Be(64);
			top.Data.First().Source.Should().Be("Manga");
			top.Data.First().Duration.Should().Be("24 min per ep");
			top.Data.First().Producers.Should().HaveCount(4);
			top.Data.First().Licensors.Should().HaveCount(2);
			top.Data.First().Studios.Should().ContainSingle().Which.Name.Should().Be("Bones");
		}

		[Test]
		public async Task NoParameter_ShouldParseLOGHType()
		{
			// When
			var top = await JikanTests.Instance.GetTopAnimeAsync();

			// Then
			var logh = top.Data.Single(x => x.Title == "Ginga Eiyuu Densetsu");
			logh.Type.Should().Be("OVA");
		}

		[Test]
		[TestCase(int.MinValue)]
		[TestCase(-1)]
		[TestCase(0)]
		public async Task InvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = JikanTests.Instance.Awaiting(x => x.GetTopAnimeAsync(page));

			// Then
			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}

		[Test]
		public async Task SecondPage_ShouldParseAnimeSecondPage()
		{
			// When
			var top = await JikanTests.Instance.GetTopAnimeAsync(2);

			// Then
			var titles = top.Data.Select(x => x.Title);
			using (new AssertionScope())
			{
				titles.Should().Contain("Monogatari Series: Second Season");
				titles.Should().Contain("Cowboy Bebop");
			}
		}

		[Test]
		[TestCase(int.MinValue)]
		[TestCase(-1)]
		[TestCase(0)]
		public async Task ValidFilterInvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = JikanTests.Instance.Awaiting(x => x.GetTopAnimeAsync(TopAnimeFilter.Airing, page));

			// Then
			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}
		
		[Test]
		public async Task FilterParameter_ShouldParseOP()
		{
			// When
			var top = await JikanTests.Instance.GetTopAnimeAsync(TopAnimeFilter.Airing) ;

			// Then
			top.Data.First().Title.Should().Be("One Piece");
		}
		
		[Test]
		public async Task FilterParameterWithSecondPage_ShouldParseNotOP()
		{
			// When
			var top = await JikanTests.Instance.GetTopAnimeAsync(TopAnimeFilter.Airing, 2) ;

			// Then
			using var _ = new AssertionScope();
			top.Data.Should().HaveCount(25);
			top.Data.First().Title.Should().NotBe("One Piece");
		}
	}
}