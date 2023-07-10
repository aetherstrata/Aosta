using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.TopTests;

public class GetTopPeopleAsyncTests
{
	[Test]
	public async Task NoParameters_ShouldParseKanaHanazawa()
	{
		// When
		var top = await JikanTests.Instance.GetTopPeopleAsync();

		// Then
		var kana = top.Data.Skip(1).First();
		using (new AssertionScope())
		{
			kana.Name.Should().Be("Kana Hanazawa");
			kana.GivenName.Should().Be("香菜");
			kana.FamilyName.Should().Be("花澤");
			kana.MalId.Should().Be(185);
			kana.Birthday.Value.Year.Should().Be(1989);
			kana.MemberFavorites.Should().BeGreaterThan(95000);
		}
	}

	[Test]
	public async Task NoParameters_ShouldParseHiroshiKamiya()
	{
		// When
		var top = await JikanTests.Instance.GetTopPeopleAsync();

		// Then
		var kamiya = top.Data.First();
		using (new AssertionScope())
		{
			kamiya.Name.Should().Be("Hiroshi Kamiya");
			kamiya.GivenName.Should().Be("浩史");
			kamiya.FamilyName.Should().Be("神谷");
			kamiya.MalId.Should().Be(118);
			kamiya.Birthday.Value.Year.Should().Be(1975);
			kamiya.MemberFavorites.Should().BeGreaterThan(99000);
		}
	}

	[Test]
	[TestCase(int.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidPage_ShouldThrowValidationException(int page)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetTopPeopleAsync(page));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	public async Task SecondPage_ShouldFindMaasakiYuasa()
	{
		// When
		var top = await JikanTests.Instance.GetTopPeopleAsync(2);

		// Then
		top.Data.Select(x => x.Name).Should().Contain("Masaaki Yuasa");
	}
}