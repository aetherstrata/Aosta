using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.MangaTests
{
	public class GetMangaRelationsAsyncTests
	{
		[Test]
		[TestCase(long.MinValue)]
		[TestCase(-1)]
		[TestCase(0)]
		public async Task InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			var func = JikanTests.Instance.Awaiting(x => x.GetMangaRelationsAsync(id));

			// Then
			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}

		[Test]
		public async Task MonsterId_ShouldParseMonsterRelations()
		{
			// When
			var monster = await JikanTests.Instance.GetMangaRelationsAsync(1);

			// Then
			using var _ = new AssertionScope();
			monster.Data.Should().HaveCount(2);
			monster.Data.Should().ContainSingle(x => x.Relation.Equals("Adaptation") && x.Entry.Count == 1);
			monster.Data.Should().ContainSingle(x => x.Relation.Equals("Side story") && x.Entry.Count == 1);
		}
	}
}