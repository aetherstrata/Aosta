using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.AnimeTests
{
	public class GetAnimeUserUpdatesAsyncTests
	{
		[Test]
		[TestCase(long.MinValue)]
		[TestCase(-1)]
		[TestCase(0)]
		public async Task InvalidId_ShouldThrowValidationException(long malId)
		{
			var func = JikanTests.Instance.Awaiting(x => x.GetAnimeUserUpdatesAsync(malId));

			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}

		[Test]
		public async Task BebopId_ShouldParseCowboyBebopUserUpdates()
		{
			var bebop = await JikanTests.Instance.GetAnimeUserUpdatesAsync(1);

			var firstUpdate = bebop.Data.First();
			using var _ = new AssertionScope();
			bebop.Data.Should().HaveCount(75);
			firstUpdate.Date.Value.Should().BeBefore(DateTime.Now);
			firstUpdate.User.Should().NotBeNull();
			firstUpdate.User.Username.Should().NotBeNullOrWhiteSpace();
			firstUpdate.User.Url.Should().NotBeNullOrWhiteSpace();
		}

		[Test]
		[TestCase(long.MinValue)]
		[TestCase(-1)]
		[TestCase(0)]
		public async Task SecondPageInvalidId_ShouldThrowValidationException(long malId)
		{
			var func = JikanTests.Instance.Awaiting(x => x.GetAnimeUserUpdatesAsync(malId, 2));

			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}

		[Test]
		[TestCase(int.MinValue)]
		[TestCase(-1)]
		[TestCase(0)]
		public async Task ValidIdInvalidpage_ShouldThrowValidationException(int page)
		{
			var func = JikanTests.Instance.Awaiting(x => x.GetAnimeUserUpdatesAsync(1, page));

			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}

		[Test]
		public async Task BebopIdSecondPage_ShouldParseCowboyBebopUserUpdatesPaged()
		{
			var bebop = await JikanTests.Instance.GetAnimeUserUpdatesAsync(1, 2);

			var firstUpdate = bebop.Data.First();

			using var _ = new AssertionScope();
			bebop.Data.Should().HaveCount(75);
			firstUpdate.EpisodesTotal.Should().HaveValue().And.Be(26);
			firstUpdate.User.Should().NotBeNull();
			firstUpdate.User.Username.Should().NotBeNullOrWhiteSpace();
			firstUpdate.User.Url.Should().NotBeNullOrWhiteSpace();
		}
	}
}
