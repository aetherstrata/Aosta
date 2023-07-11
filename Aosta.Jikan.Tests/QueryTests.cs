using Aosta.Jikan.Enums;
using Aosta.Jikan.Queries;
using Aosta.Utils.Extensions;

namespace Aosta.Jikan.Tests;

[TestFixture]
internal class QueryTests
{
    private static readonly string[] Endpoint =
    {
        "api",
        "v2",
        "items"
    };

    private IQuery _query;

    [Test]
    public void GetQuery_shouldReturnEndpoint_whenNoParam()
    {
        _query = new Query(Endpoint);

        _query.GetQuery().Should().Be("api/v2/items");
    }

    [Test]
    public void GetQuery_shouldAddParamName_whenTrueBool()
    {
        _query = new Query(Endpoint)
            .WithParameter("kids", true);

        _query.GetQuery().Should().Be("api/v2/items?kids");
    }

    [Test]
    public void GetQuery_shouldNotAddParamName_whenFalseBool()
    {
        _query = new Query(Endpoint)
            .WithParameter("kids", false);

        _query.GetQuery().Should().Be("api/v2/items");
    }

    [Test]
    public void GetQuery_shouldAddParam_whenInteger()
    {
        _query = new Query(Endpoint)
            .WithParameter("kids", 14);

        _query.GetQuery().Should().Be("api/v2/items?kids=14");
    }

    [Test]
    public void GetQuery_shouldAddParam_whenString()
    {
        _query = new Query(Endpoint)
            .WithParameter("kids", "other");

        _query.GetQuery().Should().Be("api/v2/items?kids=other");
    }

    [Test]
    public void GetQuery_shouldAddParam_whenEnum()
    {
        _query = new Query(Endpoint)
            .WithParameter("airing", AiringStatusFilter.Airing.StringValue());

        _query.GetQuery().Should().Be("api/v2/items?airing=airing");
    }

    [Test]
    public void GetQuery_shouldConcatParams_whenMultiple()
    {
        _query = new Query(Endpoint)
            .WithParameter("sfw", true)
            .WithParameter("unapproved", false)
            .WithParameter("page", 4)
            .WithParameter("q", "Naruto")
            .WithParameter("airing", AiringStatusFilter.Airing.StringValue());

        _query.GetQuery().Should().Be("api/v2/items?sfw&page=4&q=Naruto&airing=airing");
    }

    [Test]
    public void GetQuery_shouldConcatParams_whenSet()
    {
        var set = new QueryParameterSet
        {
            { "sfw", true },
            { "unapproved", false },
            { "page", 4 },
            { "q", "Naruto" },
            { "airing", AiringStatusFilter.Airing.StringValue() }
        };

        _query = new Query(Endpoint)
            .WithParameterRange(set);

        _query.GetQuery().Should().Be("api/v2/items?sfw&page=4&q=Naruto&airing=airing");
    }

    [Test]
    public void QueryParameterSet_shouldThrow_onDuplicateParamName()
    {
        var set = new QueryParameterSet { { "name", 1 } };

        var act = () => set.Add("name", "test");

        act.Should().ThrowExactly<JikanDuplicateParameterException>();
    }
}