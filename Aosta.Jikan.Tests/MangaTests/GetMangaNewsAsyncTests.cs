using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.MangaTests
{
	public class GetMangaNewsAsyncTests
	{
		[Test]
		[TestCase(long.MinValue)]
		[TestCase(-1)]
		[TestCase(0)]
		public async Task InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = JikanTests.Instance.Awaiting(x => x.GetMangaNewsAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}

		[Test]
		[TestCase(int.MinValue)]
		[TestCase(-1)]
		[TestCase(0)]
		public async Task InvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = JikanTests.Instance.Awaiting(x => x.GetMangaNewsAsync(1, page));

			// Then
			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}

		[Test]
		public async Task OnePieceId_ShouldParseOnePieceNews()
		{
			// When
			var onePiece = await JikanTests.Instance.GetMangaNewsAsync(13);

			// Then
			using (new AssertionScope())
			{
				onePiece.Data.Should().HaveCount(30);
				onePiece.Data.Select(x => x.Author).Should().Contain("Aiimee");
			}
		}

		[Test]
		public async Task OnePieceIdWithPage_ShouldParseOnePieceNews()
		{
			// When
			var onePiece = await JikanTests.Instance.GetMangaNewsAsync(13, 1);

			// Then
			using (new AssertionScope())
			{
				onePiece.Data.Should().HaveCount(30);
				onePiece.Pagination.HasNextPage.Should().BeTrue();
				onePiece.Data.Select(x => x.Author).Should().Contain("Aiimee");
			}
		}

		[Test]
		public async Task OnePieceIdWithNextPage_ShouldParseOnePieceNews()
		{
			// When
			var onePiece = await JikanTests.Instance.GetMangaNewsAsync(13, 2);

			// Then
			using var _ = new AssertionScope();
			onePiece.Data.Should().NotBeEmpty();
			onePiece.Pagination.HasNextPage.Should().BeTrue();
			onePiece.Data.Select(x => x.Author).Should().Contain("Snow");
		}

		[Test]
		public async Task MonsterId_ShouldParseMonsterNews()
		{
			// When
			var monster = await JikanTests.Instance.GetMangaNewsAsync(1);

			// Then
			using (new AssertionScope())
			{
				monster.Data.Should().HaveCount(11);
				monster.Data.Select(x => x.Author).Should().Contain("Xinil");
			}
		}
	}
}