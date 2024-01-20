using Aosta.Jikan.Exceptions;

using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.AnimeTests;

public class GetAnimeRelationsAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long malId)
	{
		var func = JikanTests.Instance.Awaiting(x => x.GetAnimeRelationsAsync(malId));

		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}


	[Test]
	public async Task BebopId_ShouldParseCowboyBebopRelations()
	{
		var bebop = await JikanTests.Instance.GetAnimeRelationsAsync(1);

		using var _ = new AssertionScope();
		bebop.Data.Should().HaveCount(3);
		bebop.Data.Should().ContainSingle(x => x.Relation.Equals("Adaptation") && x.Entry.Count == 2);
		bebop.Data.Should().ContainSingle(x => x.Relation.Equals("Side story") && x.Entry.Count == 2);
		bebop.Data.Should().ContainSingle(x => x.Relation.Equals("Summary") && x.Entry.Count == 1);
	}
}