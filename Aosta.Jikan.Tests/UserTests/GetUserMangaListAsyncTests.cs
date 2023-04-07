using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.UserTests;

public class GetUserMangaListAsyncTests
{
	[Test]
	[TestCase(null)]
	[TestCase("")]
	[TestCase("\n\n\t    \t")]
	public async Task InvalidUsername_ShouldThrowValidationException(string username)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetUserMangaListAsync(username));

		// Then
		await func.Should().ThrowExactlyAsync<ParameterValidationException>();
	}

	[Test]
	public async Task Ervelan_ShouldParseErvelanMangaList()
	{
		// When
		var mangaList = await JikanTests.Instance.GetUserMangaListAsync("Ervelan");

		// Then
		using (new AssertionScope())
		{
			mangaList.Should().NotBeNull();
			mangaList.Data.Count.Should().BeGreaterThan(90);
			mangaList.Data.Select(x => x.Manga.Title).Should().Contain("Dr. Stone");
		}
	}
	[Test]
	public async Task ErvelanSecondPage_ShouldParseErvelanMangaList()
	{
		// When
		var mangaList = await JikanTests.Instance.GetUserMangaListAsync("Ervelan", 2);

		// Then
		using (new AssertionScope())
		{
			mangaList.Should().NotBeNull();
			mangaList.Data.Should().BeEmpty();
		}
	}


	[Test]
	public void onrix_ShouldParseOnrixMangaList()
	{
		// When
		var action = JikanTests.Instance.Awaiting(x => x.GetUserMangaListAsync("onrix"));

		// Then
		action.Should().ThrowAsync<JikanRequestException>();
	}

	[Test]
	public async Task SonMati_ShouldParseSonMatiMangaList()
	{
		// When
		var mangaList = await JikanTests.Instance.GetUserMangaListAsync("SonMati");

		// Then
		using (new AssertionScope())
		{
			mangaList.Should().NotBeNull();
			mangaList.Data.Should().HaveCount(300);
		}
	}

	[Test]
	public async Task MithogawaSecondPage_ShouldParseMithogawaMangaList()
	{
		// When
		var mangaList = await JikanTests.Instance.GetUserMangaListAsync("Mithogawa", 2);

		// Then
		using (new AssertionScope())
		{
			mangaList.Should().NotBeNull();
			mangaList.Data.Should().HaveCount(300);
			mangaList.Data.Should().NotContain(x => x.Manga.Title.Equals("Baki-dou"));
		}
	}
}