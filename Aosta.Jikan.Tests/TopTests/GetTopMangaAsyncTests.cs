using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.TopTests;

public class GetTopMangaAsyncTests
{
	[Test]
	public async Task NoParameter_ShouldParseTopManga()
	{
		// When
		var top = await JikanTests.Instance.GetTopMangaAsync();

		// Then
		top.Should().NotBeNull();
	}

	[Test]
	public async Task NoParameter_ShouldParseBerserk()
	{
		// When
		var top = await JikanTests.Instance.GetTopMangaAsync();

		// Then
		using var _ = new AssertionScope();
		top.Data.First().Title.Should().Be("Berserk");
		top.Data.First().Publishing.Should().BeFalse();
		top.Data.First().Type.Should().Be("Manga");
		top.Data.First().Rank.Should().Be(1);
		top.Data.First().Authors.Should().ContainSingle().Which.Name.Should().Be("Miura, Kentarou");
		top.Data.First().Serializations.Should().ContainSingle().Which.Name.Should().Be("Young Animal");
	}

	[Test]
	[TestCase(int.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidPage_ShouldThrowValidationException(int page)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetTopMangaAsync(page));

		// Then
		await func.Should().ThrowExactlyAsync<ParameterValidationException>();
	}

	[Test]
	public async Task SecondPage_ShouldParseSecondPage()
	{
		// When
		var top = await JikanTests.Instance.GetTopMangaAsync(2);

		// Then
		var titles = top.Data.Select(x => x.Title);
		using var _ = new AssertionScope();
		titles.Should().Contain("Made in Abyss");
		titles.Should().Contain("Mushishi");
		titles.Should().Contain("Nana");
	}
}