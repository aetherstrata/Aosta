using Aosta.Core.Utils.Exceptions;

namespace Aosta.Jikan.Tests.PersonTests
{
	public class GetPersonPicturesAsyncTests
	{
		[Test]
		[TestCase(long.MinValue)]
		[TestCase(-1)]
		[TestCase(0)]
		public async Task InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = JikanTests.Instance.Awaiting(x => x.GetPersonPicturesAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}

		[Test]
		public async Task WakamotoId_ShouldParseNorioWakamotoImages()
		{
			// Given
			var norioWakamoto = await JikanTests.Instance.GetPersonPicturesAsync(84);

			// Then
			norioWakamoto.Data.Should().HaveCount(4);
		}

		[Test]
		public async Task SugitaId_ShouldParseSugitaTomokazuImages()
		{
			// Given
			var norioWakamoto = await JikanTests.Instance.GetPersonPicturesAsync(2);

			// Then
			norioWakamoto.Data.Should().HaveCount(8);
		}
	}
}
