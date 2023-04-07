using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.ProducerTests
{
	public class GetProducersAsyncTests
	{
		[Test]
		[TestCase(int.MinValue)]
		[TestCase(-1)]
		[TestCase(0)]
		public async Task InvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = JikanTests.Instance.Awaiting(x => x.GetProducersAsync(page));

			// Then
			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}

		[Test]
		public async Task NoParameter_ShouldParsePaginationData()
		{
			// When
			var results = await JikanTests.Instance.GetProducersAsync();

			// Then
			using var _ = new AssertionScope();
			results.Data.Should().HaveCount(25);
			results.Pagination.HasNextPage.Should().BeTrue();
			results.Pagination.LastVisiblePage.Should().BeGreaterThan(50);
		}

		[Test]
		public async Task NoParameter_ShouldParseProducers()
		{
			// When
			var results = await JikanTests.Instance.GetProducersAsync();

			// Then
			var names = results.Data.SelectMany(x => x.Titles).Select(y => y.Title);
			using var _ = new AssertionScope();
			names.Should().Contain("Pierrot");
			names.Should().Contain("Kyoto Animation");
			names.Should().Contain("Gonzo");
		}

		[Test]
		public async Task SecondPage_ShouldParseProducers()
		{
			// When
			var results = await JikanTests.Instance.GetProducersAsync(2);

			// Then
			var names = results.Data.SelectMany(x => x.Titles).Select(y => y.Title);
			using var _ = new AssertionScope();
			names.Should().Contain("Manglobe");
			names.Should().Contain("Studio Deen");
			names.Should().Contain("Satelight");
		}
	}
}