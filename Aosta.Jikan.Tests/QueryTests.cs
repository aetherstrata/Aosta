using Aosta.Jikan.Exceptions;
using Aosta.Jikan.Query;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Tests;

[TestFixture]
internal class QueryTests
{
    private static readonly string[] s_Endpoint =
    [
        "api",
        "v2",
        "items"
    ];

    private JikanQuery _query;

    [Test]
    public void GetQuery_shouldReturnEndpoint_whenNoParam()
    {
        _query = new JikanQuery(s_Endpoint);

        _query.GetQuery().Should().Be("api/v2/items");
    }

    [Test]
    public void GetQuery_shouldAddParamName_whenTrueBool()
    {
        _query = new JikanQuery(s_Endpoint)
            .Add(QueryParameter.KIDS, true);

        _query.GetQuery().Should().Be("api/v2/items?kids");
    }

    [Test]
    public void GetQuery_shouldNotAddParamName_whenFalseBool()
    {
        _query = new JikanQuery(s_Endpoint)
            .Add(QueryParameter.KIDS, false);

        _query.GetQuery().Should().Be("api/v2/items");
    }

    [Test]
    public void GetQuery_shouldAddParam_whenInteger()
    {
        _query = new JikanQuery(s_Endpoint)
            .Add(QueryParameter.KIDS, 14);

        _query.GetQuery().Should().Be("api/v2/items?kids=14");
    }

    [Test]
    public void GetQuery_shouldAddParam_whenString()
    {
        _query = new JikanQuery(s_Endpoint)
            .Add(QueryParameter.KIDS, "other");

        _query.GetQuery().Should().Be("api/v2/items?kids=other");
    }

    [Test]
    public void GetQuery_shouldAddParam_whenEnum()
    {
        _query = new JikanQuery(s_Endpoint)
            .Add(QueryParameter.STATUS, AiringStatusFilter.Airing);

        _query.GetQuery().Should().Be("api/v2/items?status=airing");
    }

    [Test]
    public void GetQuery_shouldConcatParams_whenMultiple()
    {
        _query = new JikanQuery(s_Endpoint)
        {
            { QueryParameter.SAFE_FOR_WORK, true },
            { QueryParameter.UNAPPROVED, false },
            { QueryParameter.PAGE, 4 },
            { QueryParameter.QUERY, "Naruto" },
            { QueryParameter.STATUS, AiringStatusFilter.Airing }
        };

        _query.GetQuery().Should().Be("api/v2/items?sfw&page=4&q=Naruto&airing=airing");
    }

    [Test]
    public void GetQuery_shouldConcatParams_whenSet()
    {
        var set = new JikanQueryParameterSet
        {
            { QueryParameter.SAFE_FOR_WORK, true },
            { QueryParameter.UNAPPROVED, false },
            { QueryParameter.PAGE, 4 },
            { QueryParameter.QUERY, "Naruto" },
            { QueryParameter.STATUS, AiringStatusFilter.Airing }
        };

        _query = new JikanQuery(s_Endpoint).AddRange(set);

        _query.GetQuery().Should().Be("api/v2/items?sfw&page=4&q=Naruto&status=airing");
    }

    [Test]
    public void QueryParameterSet_shouldThrow_onDuplicateParamName()
    {
        var set = new JikanQueryParameterSet { { "name", 1 } };

        var act = () => set.Add("name", "test");

        act.Should().ThrowExactly<JikanDuplicateParameterException>();
    }
}
