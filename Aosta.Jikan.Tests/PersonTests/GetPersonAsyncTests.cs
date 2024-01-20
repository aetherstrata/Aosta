using Aosta.Jikan.Exceptions;

using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.PersonTests;

public class GetPersonAsyncTests
{
	[Test]
	[TestCase(long.MinValue)]
	[TestCase(-1)]
	[TestCase(0)]
	public async Task InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetPersonAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<JikanParameterValidationException>();
	}

	[Test]
	[TestCase(1)]
	[TestCase(2)]
	[TestCase(3)]
	public async Task CorrectId_ShouldReturnNotNullPerson(long malId)
	{
		// Given
		var returnedPerson = await JikanTests.Instance.GetPersonAsync(malId);

		// Then
		returnedPerson.Should().NotBeNull();
	}

	[Test]
	[TestCase(13308)]
	[TestCase(13310)]
	[TestCase(13312)]
	public void WrongId_ShouldReturnNullPerson(long malId)
	{
		// When
		var func = JikanTests.Instance.Awaiting(x => x.GetPersonAsync(malId));

		// Then
		func.Should().ThrowExactlyAsync<JikanRequestException>();
	}

	[Test]
	public async Task WakamotoId_ShouldParseNorioWakamoto()
	{
		// Given
		var norioWakamoto = await JikanTests.Instance.GetPersonAsync(84);

		// Then
		using (new AssertionScope())
		{
			norioWakamoto.Data.Name.Should().Be("Norio Wakamoto");
			norioWakamoto.Data.Birthday.Value.Year.Should().Be(1945);
		}
	}
}
