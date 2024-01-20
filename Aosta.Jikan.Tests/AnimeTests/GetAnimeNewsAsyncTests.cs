using Aosta.Jikan.Exceptions;

using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.AnimeTests;

public class GetAnimeNewsAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long malId)
	{
		var func = JikanTests.Instance.Awaiting(x => x.GetAnimeNewsAsync(malId));

		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	[TestCase(int.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidPage_ShouldThrowValidationException(int page)
	{
		var func = JikanTests.Instance.Awaiting(x => x.GetAnimeNewsAsync(1, page));

		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task BebopId_ShouldParseCowboyBebopNews()
	{
		var bebop = await JikanTests.Instance.GetAnimeNewsAsync(1);

        using var _ = new AssertionScope();
        bebop.Data.Should().HaveCount(9);
        bebop.Data.Select(x => x.Author).Should().Contain("Snow");
        bebop.Pagination.Items.Should().BeNull();
        bebop.Pagination.CurrentPage.Should().BeNull();
        bebop.Pagination.LastVisiblePage.Should().BePositive();
    }

	[Test]
	public async Task BebopIdWithPage_ShouldParseCowboyBebopNews()
	{
		var bebop = await JikanTests.Instance.GetAnimeNewsAsync(1, 1);

        using var _ = new AssertionScope();
        bebop.Data.Should().HaveCount(9);
        bebop.Data.Select(x => x.Author).Should().Contain("Snow");
    }

	[Test]
	public async Task BebopIdWithNextPage_ShouldParseZeroNews()
	{
		var bebop = await JikanTests.Instance.GetAnimeNewsAsync(1, 2);

		bebop.Data.Should().BeEmpty();
	}
}
