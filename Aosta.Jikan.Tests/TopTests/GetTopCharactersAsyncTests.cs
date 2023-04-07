using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.TopTests
{
	public class GetTopCharactersAsyncTests
	{
		[Test]
		public async Task NoParameters_ShouldParseLelouchLamperouge()
		{
			// When
			var top = await JikanTests.Instance.GetTopCharactersAsync();

			// Then
			var lelouch = top.Data.First();
			using (new AssertionScope())
			{
				lelouch.Name.Should().Be("Lelouch Lamperouge");
				lelouch.Nicknames.Should().HaveCount(5);
				lelouch.MalId.Should().Be(417);
				lelouch.Favorites.Should().BeGreaterThan(85000);
			}
		}

		[Test]
		public async Task NoParameters_ShouldParseLLawliet()
		{
			// When
			var top = await JikanTests.Instance.GetTopCharactersAsync();

			// Then
			var l = top.Data.Skip(2).First();
			using (new AssertionScope())
			{
				l.Name.Should().Be("L Lawliet");
				l.Nicknames.Should().HaveCount(4);
				l.MalId.Should().Be(71);
				l.Favorites.Should().BeGreaterThan(65000);
			}
		}

		[Test]
		public async Task NoParameters_ShouldParseLuffyAnimeography()
		{
			// When
			var top = await JikanTests.Instance.GetTopCharactersAsync();

			// Then
			using (new AssertionScope())
			{
				top.Data.Skip(3).First().Name.Should().Be("Luffy Monkey D.");
				top.Data.Skip(3).First().About.Should().StartWith("Age: 17; 19");
			}
		}

		[Test]
		[TestCase(int.MinValue)]
		[TestCase(-1)]
		[TestCase(0)]
		public async Task InvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = JikanTests.Instance.Awaiting(x => x.GetTopCharactersAsync(page));

			// Then
			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}

		[Test]
		public async Task FifthPage_ShouldFindTachibanaKanade()
		{
			// When
			var top = await JikanTests.Instance.GetTopCharactersAsync(5);

			// Then
			top.Data.Select(x => x.Name).Should().Contain("Kanade Tachibana");
		}
	}
}