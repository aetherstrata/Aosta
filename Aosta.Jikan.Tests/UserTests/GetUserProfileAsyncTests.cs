using Aosta.Core.Utils.Exceptions;
using FluentAssertions.Execution;

namespace Aosta.Jikan.Tests.UserTests
{
	public class GetUserProfileAsyncTests
	{
		[Test]
		[TestCase(null)]
		[TestCase("")]
		[TestCase("\n\n\t    \t")]
		public async Task InvalidUsername_ShouldThrowValidationException(string username)
		{
			// When
			var func = JikanTests.Instance.Awaiting(x => x.GetUserProfileAsync(username));

			// Then
			await func.Should().ThrowExactlyAsync<ParameterValidationException>();
		}

		[Test]
		public async Task Ervelan_ShouldParseErvelanProfile()
		{
			// When
			var user = await JikanTests.Instance.GetUserProfileAsync("Ervelan");

			// Then
			using (new AssertionScope())
			{
				user.Should().NotBeNull();
				user.Data.Username.Should().Be("Ervelan");
				user.Data.MalId.Should().Be(289183);
				user.Data.Joined.Value.Year.Should().Be(2010);
				user.Data.Birthday.Value.Year.Should().Be(1993);
				user.Data.Gender.Should().Be("Male");
			}
		}

		[Test]
		public async Task GetUserProfile_Nekomata1037_ShouldParseNekomataProfile()
		{
			// When
			var user = await JikanTests.Instance.GetUserProfileAsync("Nekomata1037");

			// Then
			using (new AssertionScope())
			{
				user.Data.Should().NotBeNull();
				user.Data.Username.Should().Be("Nekomata1037");
				user.Data.MalId.Should().Be(4901676);
				user.Data.Joined.Value.Year.Should().Be(2015);
			}
		}
	}
}