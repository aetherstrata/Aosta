using Aosta.Jikan.Exceptions;

using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.AnimeTests;

public class GetAnimeStaffAsyncTests
{
    private const string watanabe_name = "Watanabe, Shinichirou";

    [Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long malId)
	{
		var func = JikanTests.Instance.Awaiting(x => x.GetAnimeStaffAsync(malId));

		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task BebopId_ShouldParseCowboyBebopStaff()
	{
		var bebop = await JikanTests.Instance.GetAnimeStaffAsync(1);

		bebop.Data.Should().Contain(x => x.Person.Name.Equals(watanabe_name));
	}

	[Test]
	public async Task BebopId_ShouldParseShinichiroWatanabeDetails()
	{
		var bebop = await JikanTests.Instance.GetAnimeStaffAsync(1);
		var shinichiroWatanabe = bebop.Data.First(x => x.Person.Name.Equals(watanabe_name));

		using var _ = new AssertionScope();
		shinichiroWatanabe.Position.Should().HaveCount(4);
		shinichiroWatanabe.Position.Should().Contain("Director");
		shinichiroWatanabe.Position.Should().Contain("Script");
		shinichiroWatanabe.Person.Name.Should().Be(watanabe_name);
		shinichiroWatanabe.Person.MalId.Should().Be(2009);
	}

	[Test]
	public async Task BebopId_ShouldParseShinichiroWatanabePictures()
	{
		var bebop = await JikanTests.Instance.GetAnimeStaffAsync(1);
		var shinichiroWatanabe = bebop.Data.First(x => x.Person.Name.Equals(watanabe_name));

		shinichiroWatanabe.Person.Images.JPG.ImageUrl.Should().NotBeNullOrEmpty();
	}
}
