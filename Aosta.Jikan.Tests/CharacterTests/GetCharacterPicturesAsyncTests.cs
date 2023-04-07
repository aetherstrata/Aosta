using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.CharacterTests
{
	public class GetCharacterPicturesAsyncTests
	{
		[Test]
		[TestCase(long.MinValue)]
		[TestCase(-1)]
		[TestCase(0)]
		public async Task InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			var func = JikanTests.Instance.Awaiting(x => x.GetCharacterPicturesAsync(id));

			// Then
			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}

		[Test]
		public async Task SpikeSpiegelId_ShouldParseSpikeSpiegelImages()
		{
			// When
			var spike = await JikanTests.Instance.GetCharacterPicturesAsync(1);

			// Then
			using var _ = new AssertionScope();
			spike.Data.Should().HaveCount(15);
			spike.Data.Should().OnlyContain(x => !string.IsNullOrWhiteSpace(x.JPG.ImageUrl));
		}

		[Test]
		public async Task SharoId_ShouldParseKirimaSharoImages()
		{
			// When
			var kirimaSharo = await JikanTests.Instance.GetCharacterPicturesAsync(94947);

			// Then
			using var _ = new AssertionScope();
			kirimaSharo.Data.Should().HaveCount(9);
			kirimaSharo.Data.Should().OnlyContain(x => !string.IsNullOrWhiteSpace(x.JPG.ImageUrl));
		}
	}
}