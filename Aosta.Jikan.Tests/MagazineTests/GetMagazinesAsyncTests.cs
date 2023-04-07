using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.MagazineTests
{
	public class GetMagazinesAsyncTests
	{
		[Test]
		[TestCase(int.MinValue)]
		[TestCase(-1)]
		[TestCase(0)]
		public async Task InvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = JikanTests.Instance.Awaiting(x => x.GetMagazinesAsync(page));

			// Then
			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}

		[Test]
		public async Task NoParameter_ShouldParsePaginationData()
		{
			// When
			var results = await JikanTests.Instance.GetMagazinesAsync();

			// Then
			using var _ = new AssertionScope();
			results.Data.Should().HaveCount(25);
			results.Pagination.HasNextPage.Should().BeTrue();
			results.Pagination.LastVisiblePage.Should().BeGreaterOrEqualTo(49);
		}

		[Test]
		public async Task NoParameter_ShouldParseMagazines()
		{
			// When
			var results = await JikanTests.Instance.GetMagazinesAsync();

			// Then
			var names = results.Data.Select(x => x.Name);
			using var _ = new AssertionScope();
			names.Should().Contain("Big Comic Original");
			names.Should().Contain("Young Animal");
			names.Should().Contain("Young Magazine (Monthly)");
		}

		[Test]
		public async Task SecondPage_ShouldParseMagazines()
		{
			// When
			var results = await JikanTests.Instance.GetMagazinesAsync(2);

			// Then
			var names = results.Data.Select(x => x.Name);
			using var _ = new AssertionScope();
			names.Should().Contain("GFantasy");
			names.Should().Contain("Shounen Magazine (Monthly)");
			names.Should().Contain("Betsucomi");
		}
	}
}